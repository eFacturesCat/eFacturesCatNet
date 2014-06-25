using System;
using System.Collections.Generic;
//using System.Linq;
using System.IO;
using System.Text;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.X509;
using Org.BouncyCastle.Ocsp;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.Xml;
using eFacturesCat.Commons;

namespace eFacturesCat.Secure
{
    /// <summary>
    /// Utils class to "play" with X509 certificates
    /// </summary>
    public class CertUtils
    {
        private static string OCSP_request = "application/ocsp-request";
        private static string OCSP_POST = "POST";

        private static string OID_KeyUsage = "2.5.29.15";
        private static string OID_AIA = "1.3.6.1.5.5.7.1.1";
        private static string OID_OCSP = "1.3.6.1.5.5.7.48.1";
        private static string OID_CRLDistributionPoints = "2.5.29.31";

        private static string ExceptionGettingParentCert = "Error getting parent certificate";
        private static string ExceptionGettingAIA = "Error getting AIA from certificate";
        private static string ExceptionCheckOnLineOCSP = "ErrorCheckOnLineOCSP";
        private static string ExceptionUnknownCertificate = "ErrorUnknownCertificate";
        private static string ExceptionCertificateId = "ErrorCertificateId";
        private static string ExceptionWarningSignatureValidCertificateRevoked = "WarningSignatureValidCertificateRevoked";
        private static string ExceptionCertificateRevoked = "ErrorCertificateRevoked";
        private static string ExceptionOIDCRL = "ErrorOIDCRL";
        private static string ExceptionCertificateRevokedDate = "ErrorCertificateRevokedDate";
        private static string ExceptionNextUpdateCRL = "ErrorNextUpdateCRL";
        private static string ExceptionCheckOnLineCRL = "ErrorCheckOnLineCRL";


        /// <summary>
        /// Get KeyInfo from Certificate for Signature
        /// </summary>
        /// <param name="cert">Signing Certificate</param>
        /// <returns>The KeyInfo</returns>
        public static KeyInfo getKeyInfo(X509Certificate2 cert)
        {
            try
            {
                KeyInfo keyInfo = new KeyInfo();
                keyInfo.Id = SC.OBJECTKEYINFO;

                Org.BouncyCastle.X509.X509Certificate certSel = CertUtils.ConvertToBCCert(cert);
                Org.BouncyCastle.Crypto.Parameters.RsaKeyParameters p = (Org.BouncyCastle.Crypto.Parameters.RsaKeyParameters)certSel.GetPublicKey();

                RSAParameters pa = new RSAParameters();
                System.Text.ASCIIEncoding codificador = new System.Text.ASCIIEncoding();

                pa.Exponent = p.Exponent.ToByteArrayUnsigned();
                pa.Modulus = p.Modulus.ToByteArrayUnsigned();

                RSAKeyValue r = new RSAKeyValue();
                r.Key.ImportParameters(pa);

                keyInfo.AddClause(new KeyInfoX509Data(cert));
                keyInfo.AddClause(r);


                return keyInfo;
            }
            catch (Exception)
            {
                throw new Exception(SC.ErrorKeyInfo);
            }

        }


        /// <summary>
        /// Adjust issuer name string for XAdES Signautre
        /// </summary>
        /// <param name="name">Original Issuer Name</param>
        /// <returns>Adjusted issuer name</returns>
        public static string adjustIssuerName(string name)
        {
            string[] a = name.Split('=');
            List<string> l = new List<string>();
            string n = "";
            for (int i = 0; i < a.Length - 1; i++)
            {
                if (i == 0)
                {
                    n = a[i] + "=" + a[i + 1].Substring(0, a[i + 1].IndexOf(','));

                }
                else
                {
                    if (i == a.Length - 2)
                    {
                        n = a[i].Substring(a[i].LastIndexOf(',') + 1) + "=" + a[i + 1];
                    }
                    else
                        n = a[i].Substring(a[i].LastIndexOf(',') + 1) + "=" + a[i + 1].Substring(0, a[i + 1].LastIndexOf(','));
                }
                l.Add(n);
            }
            int j = l.Count - 1;
            string issuerName = "";
            while (j > 0)
            {
                //issuerName += string.Concat(l.ElementAt(j), ",");
                issuerName += string.Concat(l[j], ",");
                j--;
            }
            //issuerName += l.ElementAt(0);
            issuerName += l[0];
            return issuerName;
        }


        /// <summary>
        /// Certificate conversion (from dotNet to BouncyCastle)
        /// </summary>
        /// <param name="cert">dotNet certificate</param>
        /// <returns>BC Certificate</returns>
        public static Org.BouncyCastle.X509.X509Certificate ConvertToBCCert(X509Certificate2 cert)
        {
            X509CertificateParser certParser = new X509CertificateParser();
            return certParser.ReadCertificate(cert.RawData);
        }


        /// <summary>
        /// Show Windows Certificate Store to select one
        /// </summary>
        /// <param name="title">Title of the form</param>
        /// <param name="subtitle">Subtitle of the form</param>
        /// <returns>Certificate selected. Null if none</returns>
        public static X509Certificate2 selectCertificateFromWindowsStore(String title, String subtitle)
        {
            X509Certificate2 cert = null;
            X509Certificate2Collection sel = X509Certificate2UI.SelectFromCollection(GetAll(), title, subtitle, X509SelectionFlag.SingleSelection);
            if (sel.Count > 0)
            {
                X509Certificate2Enumerator en = sel.GetEnumerator();
                en.MoveNext();
                cert = en.Current;
            }
            return cert;
        }

        /// <summary>
        /// Get Parent Certificate (from windows store) from a Certificate
        /// </summary>
        /// <param name="cert">certificate to get parent</param>
        /// <returns>Parent Certificate</returns>
        private static X509Certificate2 getParentCertificate(System.Security.Cryptography.X509Certificates.X509Certificate cert)
        {
            bool isCertFound = false;
            try
            {
                X509Store store = new X509Store(StoreName.CertificateAuthority);
                store.Open(OpenFlags.ReadOnly);
                X509Certificate2Collection certCollection = store.Certificates;
                X509Certificate2Enumerator iterador = ((X509Certificate2Enumerator)certCollection.GetEnumerator());
                X509Certificate2 certFound = null;

                while (iterador.MoveNext() && !(isCertFound))
                {
                    if (((X509Certificate2)iterador.Current).Subject.Equals(cert.Issuer))
                    {
                        certFound = ((X509Certificate2)iterador.Current);
                        isCertFound = true;

                    }
                }
                if (!(isCertFound))
                {
                    store = new X509Store(StoreName.Root);
                    store.Open(OpenFlags.ReadOnly);
                    certCollection = store.Certificates;
                    iterador = ((X509Certificate2Enumerator)certCollection.GetEnumerator());
                    while (iterador.MoveNext() && !(isCertFound))
                    {
                        if (((X509Certificate2)iterador.Current).Subject.Equals(cert.Issuer))
                        {
                            certFound = ((X509Certificate2)iterador.Current);
                            isCertFound = true;
                        }
                    }
                }
                store.Close();
                return certFound;

            }
            catch (Exception)
            {
                throw new Exception(ExceptionGettingParentCert);

            }
        }



        /// <summary>
        /// Check certificate status
        /// </summary>
        /// <param name="cert">X509Certificate2</param>
        /// <param name="urlOCSP">if null try to get from certificate</param>
        /// <returns>Response object with results</returns>
        public static Response checkCertificate(X509Certificate2 cert, string urlOCSP)
        {
            // Get Parent Certificate
            Org.BouncyCastle.X509.X509Certificate certBC = ConvertToBCCert(cert);
            System.Security.Cryptography.X509Certificates.X509Certificate2 parentCert;
            try
            {
                parentCert = getParentCertificate(cert);
            }
            catch (Exception e)
            {
                return new Response(Response.Error, Response.CertificateError, e.Message);
            }

            if (parentCert == null)
                return new Response(Response.Error, Response.CertificateError, "Untrusted certificate");

            // Check cert is correct signed by parent
            Org.BouncyCastle.X509.X509Certificate parentCertBC = ConvertToBCCert(parentCert);
            try
            {
                certBC.Verify(parentCertBC.GetPublicKey());
            }
            catch (Exception)
            {
                return new Response(Response.Error, Response.CertificateError, "Wrong signed certificate");
            }

            // Check if certificate is expired
            if (isExpired(cert))
            {
                return new Response(Response.Error, Response.CertificateError, "Certificate expired");
            }

            // Try to check via OCSP

            try
            {
                checkOCSP(cert, parentCert, urlOCSP);
                // Certificate is good
                return new Response();
            }
            catch (Exception exc)
            {
                if (exc.Message.Equals(ExceptionUnknownCertificate))
                {
                    return new Response(Response.Error, Response.CertificateError, ExceptionUnknownCertificate);
                }
                try
                {
                    checkCRL(cert);
                    return new Response();
                }
                catch (Exception ex)
                {
                    if (ex.Message.Equals(ExceptionCheckOnLineCRL))
                        return new Response(Response.Warning, Response.CertificateError, "Can't get revokation information");
                    return new Response(Response.Error, Response.CertificateError, ex.Message);
                }
            }
        }

        /// <summary>
        /// get the URLs list for CRLs for a given certificate
        /// </summary>
        /// <param name="cert"></param>
        /// <returns>The list</returns>
        private static List<string> getCRLsUrlsList(X509Certificate2 cert)
        {
            try
            {
                System.Security.Cryptography.X509Certificates.X509Extension extAIA = null;
                foreach (System.Security.Cryptography.X509Certificates.X509Extension ext in cert.Extensions)
                {
                    if (ext.Oid.Value == OID_CRLDistributionPoints) extAIA = ext;
                }
                if (extAIA == null)
                    throw new Exception();
                else
                {

                    DerSequence extAIADS = new DerSequence(Asn1Object.FromByteArray(extAIA.RawData));
                    string[] array = Encoding.ASCII.GetString(extAIA.RawData).Split('?');
                    List<string> URLsList = new List<string>();
                    for (int i = 0; i < array.Length; i++)
                    {
                        if (array[i].Length > 7)
                        {
                            URLsList.Add(array[i].Substring(1));
                        }
                    }
                    return URLsList;
                }
            }
            catch (Exception)
            {
                throw new Exception(ExceptionOIDCRL);
            }

        }

        /// <summary>
        /// Try to check certificate status via CRL
        /// </summary>
        /// <param name="cert"></param>
        private static void checkCRL(X509Certificate2 cert)
        {
            List<string> CRLsUrlsList = null;
            byte[] arrayByte = null;
            try
            {
                CRLsUrlsList = getCRLsUrlsList(cert);
            }
            catch (Exception e)
            {
                throw e;
            }
            for (int i = 0; i < CRLsUrlsList.Count; i++)
            {
                try
                {

                    //WebRequest wr = WebRequest.Create(CRLsUrlsList.ElementAt(i));
                    WebRequest wr = WebRequest.Create(CRLsUrlsList[i]);
                    wr.Timeout = 25000; // 25 seconds

                    WebResponse wresp = wr.GetResponse();
                    {
                        arrayByte = Utils.ReadFully(wresp.GetResponseStream(), 0);
                    }
                    try
                    {
                        X509CrlParser parser = new X509CrlParser();
                        X509Crl crl = parser.ReadCrl(arrayByte);
                        bool revoked = crl.IsRevoked(ConvertToBCCert(cert));
                        DateTime signDate = DateTime.Now;
                        DateTime thisMoment = DateTime.Now;
                        if (revoked)
                        {
                            if (signDate == DateTime.MinValue)
                            {
                                throw new Exception(ExceptionCertificateRevoked);
                            }
                            X509CrlEntry entry = crl.GetRevokedCertificate(ConvertToBCCert(cert).SerialNumber);
                            if (entry.RevocationDate < signDate)
                                throw new Exception(ExceptionCertificateRevokedDate);
                            if (entry.RevocationDate > thisMoment)
                                throw new Exception(ExceptionCertificateRevokedDate);
                            if ((thisMoment > entry.RevocationDate) && (entry.RevocationDate > signDate))
                                throw new Exception(ExceptionCertificateRevoked);
                        }
                        else
                        {
                            if (crl.NextUpdate.Value < thisMoment)
                                throw new Exception(ExceptionNextUpdateCRL);
                            else
                                return;
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                catch (Exception e)
                {
                    //Se comprueba que es la última porque si no lo es puede que haya una
                    //url que si que funcione.
                    if ((i == CRLsUrlsList.Count - 1) && ((e.Message.Equals(ExceptionCertificateRevokedDate)) || (e.Message.Equals(ExceptionCertificateRevoked)) || (e.Message.Equals(ExceptionNextUpdateCRL))))
                        throw e;
                    else
                        throw new Exception(ExceptionCheckOnLineCRL);

                }
            }
        }
        /// <summary>
        /// Try to check certificate status via OCSP
        /// </summary>
        /// <param name="cert">Certificado de firma</param>
        /// <param name="parentCert">Certificado del almacen</param>
        private static void checkOCSP(System.Security.Cryptography.X509Certificates.X509Certificate2 cert, System.Security.Cryptography.X509Certificates.X509Certificate2 parentCert, String urlOCSP)
        {
            if (urlOCSP == null)
            {
                // Try to get from Certificate
                try
                {
                    urlOCSP = getUrlOCSPfromCertificate(cert);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            if (urlOCSP != "" && urlOCSP != null && cert != null && cert != null)
            {
                try
                {
                    requestOCSP(parentCert, cert, urlOCSP);
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
            else
            {
                throw new Exception(ExceptionCheckOnLineOCSP);
            }
        }

        /// <summary>
        /// Request OCSP to service
        /// </summary>
        /// <param name="parentCert"></param>
        /// <param name="cert"></param>
        /// <param name="serviceAddr"></param>
        private static void requestOCSP(X509Certificate2 parentCert, X509Certificate2 cert, string serviceAddr)
        {

            OcspResp ocspRespone = null;
            Org.BouncyCastle.X509.X509Certificate BCparentCert = ConvertToBCCert(parentCert);
            Org.BouncyCastle.X509.X509Certificate BCcert = ConvertToBCCert(cert);
            CertificateID id = new CertificateID(CertificateID.HashSha1, BCparentCert, BCcert.SerialNumber);
            try
            {
                OcspReqGenerator gen = new OcspReqGenerator();
                gen.AddRequest(id);
                OcspReq request = gen.Generate();
                byte[] ocspReqEncoded = request.GetEncoded();
                Uri url = new Uri(serviceAddr);
                HttpWebRequest conn = (HttpWebRequest)WebRequest.Create(url);
                conn.ContentType = OCSP_request;
                conn.Method = OCSP_POST;
                Stream outS = conn.GetRequestStream();
                outS.Write(ocspReqEncoded, 0, ocspReqEncoded.Length);
                outS.Close();

                WebResponse wr = conn.GetResponse();
                Stream fis = wr.GetResponseStream();

                MemoryStream mem = new MemoryStream();
                byte[] buffer = new byte[1024];
                int bytesRead = fis.Read(buffer, 0, buffer.Length);
                while (bytesRead > 0)
                {
                    mem.Write(buffer, 0, bytesRead);
                    bytesRead = fis.Read(buffer, 0, buffer.Length);
                }

                byte[] respBytes = mem.ToArray();
                ocspRespone = new OcspResp(respBytes);

            }
            catch (Exception)
            {
                throw new Exception(ExceptionCheckOnLineOCSP);
            }
            switch (ocspRespone.Status)
            {
                case OcspRespStatus.InternalError:
                    //returnStatus = "An internal error occured in the OCSP Server!";
                    throw new Exception(ExceptionCheckOnLineOCSP);
                case OcspRespStatus.MalformedRequest:
                    //returnStatus = "Your request did not fit the RFC 2560 syntax!";
                    throw new Exception(ExceptionCheckOnLineOCSP);
                case OcspRespStatus.SigRequired:
                    //returnStatus = "Your request was not signed!";
                    throw new Exception(ExceptionCheckOnLineOCSP);
                case OcspRespStatus.TryLater:
                    //returnStatus = "The server was too busy to answer you!";
                    throw new Exception(ExceptionCheckOnLineOCSP);
                case OcspRespStatus.Unauthorized:
                    //returnStatus = "The server could not authenticate you!";
                    throw new Exception(ExceptionCheckOnLineOCSP);
                case OcspRespStatus.Successful:

                    BasicOcspResp basicOCSP = (BasicOcspResp)ocspRespone.GetResponseObject();
                    SingleResp resp = basicOCSP.Responses[0];
                    CertificateID respCertID = resp.GetCertID();
                    Object certStatus = resp.GetCertStatus();
                    if (certStatus == null)
                    {
                        if (id.Equals(respCertID) == false)
                            throw new Exception(ExceptionCertificateId);
                    }
                    else
                    {
                        if (certStatus is RevokedStatus)
                        {
                            DateTime signDate = DateTime.Now;
                            if (signDate == DateTime.MinValue)
                            {
                                throw new Exception(ExceptionCertificateRevoked);
                            }
                            if ((((RevokedStatus)certStatus).RevocationTime) > signDate)
                            {
                                throw new Exception(ExceptionWarningSignatureValidCertificateRevoked);
                            }
                            throw new Exception(ExceptionCertificateRevoked);
                        }
                        if (certStatus is UnknownStatus)
                            throw new Exception(ExceptionUnknownCertificate);
                    }
                    break;
                default:
                    throw new Exception(ExceptionCheckOnLineOCSP);
            }

        }

        /// <summary>
        /// get URL for OCSP from Certificate
        /// </summary>
        /// <param name="cert">cert</param>
        /// <returns>A string containing the URL for OCSP</returns>
        public static string getUrlOCSPfromCertificate(X509Certificate2 cert)
        {
            try
            {
                System.Security.Cryptography.X509Certificates.X509Extension extAIA = null;
                foreach (System.Security.Cryptography.X509Certificates.X509Extension ext in cert.Extensions)
                {
                    if (ext.Oid.Value == OID_AIA) extAIA = ext;
                }
                if (extAIA == null)
                    return null;
                else
                {

                    DerSequence extAIADS = new DerSequence(Asn1Object.FromByteArray(extAIA.RawData));
                    DerObjectIdentifier oidOCSP = new DerObjectIdentifier(OID_OCSP);

                    foreach (Asn1Encodable aeRoot in extAIADS)
                    {
                        Asn1Sequence aeRootSeq = Asn1Sequence.GetInstance(aeRoot.ToAsn1Object());
                        foreach (Asn1Encodable ae in aeRootSeq)
                        {
                            Asn1Sequence s = Asn1Sequence.GetInstance(ae.ToAsn1Object());

                            if (s.Count < 2 || s.Count > 3)
                                return null;

                            DerObjectIdentifier oid = DerObjectIdentifier.GetInstance(s[0].ToAsn1Object());

                            bool isCritical = s.Count == 3
                                && DerBoolean.GetInstance(s[1].ToAsn1Object()).IsTrue;

                            Asn1OctetString octets = Asn1OctetString.GetInstance(s[s.Count - 1].ToAsn1Object());

                            string str;
                            System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
                            str = enc.GetString(octets.GetOctets());

                            if (oid.Equals(oidOCSP)) return str;

                        }
                    }
                }
            }
            catch (Exception)
            {
                throw new Exception(ExceptionGettingAIA);
            }
            return null;

        }

        /// <summary>
        /// Is the certificate expired?
        /// </summary>
        /// <param name="cert">Certificate to check</param>
        /// <returns>true if expired, false if not.</returns>
        private static bool isExpired(System.Security.Cryptography.X509Certificates.X509Certificate cert)
        {
            DateTime effectiveDate = Convert.ToDateTime(cert.GetEffectiveDateString());
            DateTime expirationDate = Convert.ToDateTime(cert.GetExpirationDateString());
            DateTime thisMoment = DateTime.Now;
            if ((thisMoment > effectiveDate) && (thisMoment < expirationDate))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Get a Whole Certificate Collection from Windows Store
        /// </summary>
        /// <returns>Certificate Collection</returns>
        public static X509Certificate2Collection GetAll()
        {

            try
            {
                X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
                store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);

                X509Certificate2Collection collection = (X509Certificate2Collection)store.Certificates;
                X509Certificate2Collection signatureCertificate = new X509Certificate2Collection();
                for (int i = 0; i < collection.Count; i++)
                {
                    foreach (System.Security.Cryptography.X509Certificates.X509Extension extension in collection[i].Extensions)
                    {                        
                        if (extension.Oid.Value == OID_KeyUsage)
                        {
                            X509KeyUsageExtension ext = (X509KeyUsageExtension)extension;
                            Type t = ext.KeyUsages.GetType();
                            if (System.Enum.IsDefined(t, X509KeyUsageFlags.DigitalSignature))
                            {
                                signatureCertificate.Add(collection[i]);
                            }
                        }
                    }
                }
                store.Close();
                return signatureCertificate;
            }
            catch (CryptographicException)
            {
                return null;
            }
        }
    }
}

