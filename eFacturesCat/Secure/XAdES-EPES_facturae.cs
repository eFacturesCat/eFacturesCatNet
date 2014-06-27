using System;
using System.Xml;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.Security.Cryptography.X509Certificates;
using Org.BouncyCastle.X509;

namespace eFacturesCat.Secure
{
    /// <summary>
    /// Class for XAdES-EPES Signature for spanish eInvoicing format "facturae"
    /// Facturae signature policy is available at: http://www.facturae.es/politica_de_firma_formato_facturae/politica_de_firma_formato_facturae_v3_1.pdf
    /// </summary>
    /// <author>@santicasas: Santi Casas</author>
    /// <remarks>Special thanks to @albalia, @julianinza and @jlc_eljuss for give me the knoweldge required to build this code</remarks>
    public class XAdES_EPES_facturae
    {
        /// <summary>
        /// String field to store Signing Date
        /// </summary>
        private string signingDateStr = string.Empty;
        
        /// <summary>
        /// Constructor for XAdES-EPES signature for spanish einvoice format "facturae"
        /// </summary>
        /// <param name="xmlDoc">XmlDocument to be signed</param>
        /// <param name="role">Role of the signer. Look at facturae signature policy</param>
        /// <param name="cert">Signing Certificate (must have privatekey)</param>
        public XAdES_EPES_facturae(XmlDocument xmlDoc, string role, X509Certificate2 cert)
        {
            try
            {
                //TODO: Optional previus certificate check


                KeyInfo keyInfo = CertUtils.getKeyInfo(cert);
                XmlDocument xmlSignedProperties = new XmlDocument();
                XmlElement e = getXmlSignedProperties_EPES(xmlSignedProperties, cert, role, true, xmlDoc);
                xmlSignedProperties.AppendChild(e);
                XmlDocument xmlSignedPropertiesSinNamespace = new XmlDocument();
                XmlElement e2 = getXmlSignedProperties_EPES(xmlSignedPropertiesSinNamespace, cert, role, false, xmlDoc);
                xmlSignedPropertiesSinNamespace.AppendChild(e2);
                DataObject obj = getObjectSignedProperties(xmlSignedPropertiesSinNamespace);
                sign(xmlDoc, xmlSignedProperties, cert, keyInfo, obj, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Transforms xmlSignedProperties into an DataObject to insert into the Signature
        /// </summary>
        /// <param name="xmlSignedProperties">XML with complete SignedProperties</param>
        /// <returns>The DataObject</returns>
        private DataObject getObjectSignedProperties(XmlDocument xmlSignedProperties)
        {
            try
            {
                DataObject obj = new DataObject();
                obj.Id = SC.SIGNATUREOBJECT;
                obj.Data = xmlSignedProperties.ChildNodes;
                return obj;
            }
            catch (Exception)
            {
                throw new Exception(SC.ErrorDataObjectSignedProperies);
            }

        }

        /// <summary>
        /// Generate and get a XAdES-BES singature structure
        /// </summary>
        /// <param name="doc">XmlDocument to be created element</param>
        /// <param name="certificate">Signing Certificate</param>
        /// <param name="namespaces">With (true) or without (false) namespaces</param>
        /// <param name="xmlDoc">XmlDoccument to be signed</param>
        /// <returns>An XmlElement for SignedProperties</returns>
        private XmlElement getXmlSignedProperties_BES(XmlDocument doc, System.Security.Cryptography.X509Certificates.X509Certificate2 certificate, bool namespaces, XmlDocument xmlDoc)
        {
            string uri1 = SC.URI_ETSI;
            string uri2 = SC.URI_XMLDSIGN;

            XmlElement qualifyingProperties = getQualifyingProperties(doc, namespaces, xmlDoc);
            XmlElement signingCertificate = doc.CreateElement(SC.SigningCertificate, uri1);
            XmlElement cert = doc.CreateElement(SC.Cert, uri1);
            XmlElement certDigest = doc.CreateElement(SC.CertDigest, uri1);
            XmlElement digestMethod = doc.CreateElement(SC.DigestMethod, uri2);
            XmlElement digestValue = doc.CreateElement(SC.DigestValue, uri2);
            XmlElement issuerSerial = doc.CreateElement(SC.IssuerSerial, uri1);
            XmlElement x509IssuerName = doc.CreateElement(SC.X509IssuerName, uri2);
            XmlElement x509SerialNumber = doc.CreateElement(SC.X509SerialNumber, uri2);
            XmlElement signingTime = doc.CreateElement(SC.SigningTime, uri1);
            XmlAttribute algorithm = doc.CreateAttribute(SC.Algorithm);


            if (namespaces)
            {
                DateTime d = DateTime.Now;
                signingTime.InnerText = d.ToString("yyyy-MM-ddTHH:mm:sszzz");
                signingDateStr = signingTime.InnerText;
            }
            else
            {
                signingTime.InnerText = signingDateStr;
            }


            algorithm.InnerText = SC.URI_XMLDSIGNSHA1;
            digestValue.InnerText = Convert.ToBase64String(certificate.GetCertHash());

            Org.BouncyCastle.X509.X509Certificate certSel = CertUtils.ConvertToBCCert(certificate);
            try
            {
                x509IssuerName.InnerText = CertUtils.adjustIssuerName(certSel.IssuerDN.ToString(false, Org.BouncyCastle.Asn1.X509.X509Name.RFC2253Symbols));
            }
            catch (Exception)
            {
                throw new Exception(SC.DNNotValid);
            }

            Org.BouncyCastle.Math.BigInteger serialNumber = new Org.BouncyCastle.Math.BigInteger(Org.BouncyCastle.Utilities.Encoders.Hex.Decode(certificate.GetSerialNumberString()));
            x509SerialNumber.InnerText = serialNumber.ToString();

            digestMethod.Attributes.Append(algorithm);

            certDigest.AppendChild(digestMethod);
            certDigest.AppendChild(digestValue);
            issuerSerial.AppendChild(x509IssuerName);
            issuerSerial.AppendChild(x509SerialNumber);
            cert.AppendChild(certDigest);
            cert.AppendChild(issuerSerial);
            signingCertificate.AppendChild(cert);

            qualifyingProperties.ChildNodes[0].ChildNodes[0].AppendChild(signingTime);
            qualifyingProperties.ChildNodes[0].ChildNodes[0].AppendChild(signingCertificate);
            return qualifyingProperties;
        }

        /// <summary>
        /// Create XAdES QualifyinProperties XML Node
        /// </summary>
        /// <param name="doc">XmlDocument to be created element</param>
        /// <param name="namespaces">With (true) or without (false) namespaces</param>
        /// <param name="xmlDoc">XmlDoccument to be signed</param>
        /// <returns></returns>
        private XmlElement getQualifyingProperties(XmlDocument doc, bool namespaces, XmlDocument xmlDoc)
        {            
            XmlElement qualifyingProperties = doc.CreateElement(SC.QualifyingProperties, SC.URI_ETSI);
            XmlElement signedProperties = doc.CreateElement(SC.SignedProperties, SC.URI_ETSI);
            XmlElement signedSignatureProperties = doc.CreateElement(SC.SignedSignatureProperties, SC.URI_ETSI);
            XmlAttribute id = doc.CreateAttribute(SC.Id);
            XmlAttribute target = doc.CreateAttribute(SC.Target);
            XmlAttribute id2 = doc.CreateAttribute(SC.Id);
            XmlAttribute id3 = doc.CreateAttribute(SC.Id);
            target.InnerText = "#" + SC.SIGNATURE_1;
            id.InnerText = SC.OBJECTQUALIFYINGPROPERTIES;
            id2.InnerText = SC.OBJECTSIGNEDPROPERTIES;
            id3.InnerText = SC.OBJECTSIGNEDSIGNATUREPROPERTIES;

            qualifyingProperties.Attributes.Append(id);
            qualifyingProperties.Attributes.Append(target);
            signedProperties.Attributes.Append(id2);
            signedSignatureProperties.Attributes.Append(id3);

            if (namespaces)
            {
                for (int a = 0; a < xmlDoc.DocumentElement.Attributes.Count; a++)
                {
                    if (!xmlDoc.DocumentElement.Attributes[a].Name.Equals("xmlns"))
                    {
                        XmlAttribute xmlns = doc.CreateAttribute(@"" + xmlDoc.DocumentElement.Attributes[a].Name);
                        xmlns.Value = xmlDoc.DocumentElement.Attributes[a].Value;
                        signedProperties.Attributes.Append(xmlns);
                    }
                }
            }

            signedProperties.AppendChild(signedSignatureProperties);
            qualifyingProperties.AppendChild(signedProperties);
            return qualifyingProperties;
        }

        /// <summary>
        /// Sign method
        /// </summary>
        /// <param name="xmlDoc">xmlDocument to be signed</param>
        /// <param name="xmlSignedProperties">xmlDocument with EPES Signed Properties to be signed</param>
        /// <param name="cert">Signing certificate (must have PrivateKey</param>
        /// <param name="keyInfo">keyInfo structure for signature</param>
        /// <param name="obj">DataObject</param>
        /// <param name="detached">Detached true/false</param>
        private void sign(XmlDocument xmlDoc, XmlDocument xmlSignedProperties, X509Certificate2 cert, KeyInfo keyInfo, DataObject obj, bool detached)
        {

            try
            {
                Reference reference = new Reference();
                reference.Id = SC.REFERENCE_1;
                reference.Uri = "";
                Reference reference2 = new Reference();
                reference2.Id = SC.REFERENCE_2;
                reference2.Type = SC.URI_SIGNEDPROPERTIES;
                reference2.Uri = "#" + SC.OBJECTSIGNEDPROPERTIES;
                Reference reference3 = new Reference();
                reference3.Id = SC.REFERENCE_3;
                reference3.Uri = "#" + SC.OBJECTKEYINFO;

                obj.Id = SC.SIGNATUREOBJECT;

                SignedXml_XAdES_version signedXml = new SignedXml_XAdES_version(xmlDoc);
                signedXml.SignedInfo.Id = SC.SIGNEDINFO;
                signedXml.Signature.Id = SC.SIGNATURE_1;
                signedXml.SigningKey = cert.PrivateKey;
                if (signedXml.SigningKey == null)
                    throw new Exception(SC.ErrorPrivateKey);
                signedXml.doc2 = xmlSignedProperties;
                signedXml.KeyInfo = keyInfo;
                signedXml.AddObject(obj);
                signedXml.AddReference(reference);
                signedXml.AddReference(reference2);
                signedXml.AddReference(reference3);

                if (!detached)
                {
                    XmlDsigEnvelopedSignatureTransform env = new XmlDsigEnvelopedSignatureTransform();
                    reference.AddTransform(env);
                }

                try
                {
                    signedXml.ComputeSignature();
                }
                catch (Exception e)
                {
                    if (e.Message.Equals(SC.ErrorSignatureSignedProperties) || e.Message.Equals(SC.ErrorSignatureKeyInfo) || e.Message.Equals(SC.ErrorUnknowElementToSign))
                        throw e;
                    throw new Exception(SC.ErrorCertificateNull);
                }



                XmlElement xmlDigitalSignature = signedXml.GetXml();
                XmlNode docCompleto = xmlDoc.ImportNode(xmlDigitalSignature, true);
                if (!detached)
                {
                    xmlDoc.DocumentElement.AppendChild(docCompleto);
                }
                else
                {
                    xmlDoc.AppendChild(docCompleto);
                }

            }
            catch (Exception ex)
            {
                if (ex.Message.Equals(SC.ErrorPrivateKey) || ex.Message.Equals(SC.ErrorSignatureSignedProperties) || ex.Message.Equals(SC.ErrorSignatureKeyInfo) || ex.Message.Equals(SC.ErrorUnknowElementToSign) || ex.Message.Equals(SC.ErrorCertificateNull))
                    throw ex;
                throw new Exception(SC.ErrorToSign);
            }

        }

        /// <summary>
        /// Generate and get a XAdES-EPES signature structure
        /// </summary>
        /// <param name="doc">XmlDocument to be created element</param>
        /// <param name="certificate">Signing Certificate</param>
        /// <param name="role">Signer Role</param>
        /// <param name="namespaces">With (true) or without (false) namespaces</param>
        /// <param name="xmlDoc">XmlDoccument to be signed</param>
        /// <returns>An XmlElement for SignedProperties</returns>
        private XmlElement getXmlSignedProperties_EPES(XmlDocument doc, System.Security.Cryptography.X509Certificates.X509Certificate2 certificate, string role, bool namespaces, XmlDocument xmlDoc)
        {
            string uri1 = SC.URI_ETSI;
            string uri2 = SC.URI_XMLDSIGN;
            string urlPolicy = SC.URI_POLICY;
            string descripcion = SC.DESCRIPCION;

            XmlElement qualifyingProperties = getXmlSignedProperties_BES(doc, certificate, namespaces, xmlDoc);
            XmlElement signaturePolicyIdentifier = doc.CreateElement(SC.SignaturePolicyIdentifier, uri1);
            XmlElement signaturePolicyId = doc.CreateElement(SC.SignaturePolicyId, uri1);
            XmlElement sigPolicyId = doc.CreateElement(SC.SigPolicyId, uri1);
            XmlElement identifier = doc.CreateElement(SC.Identifier, uri1);
            XmlAttribute qualifier = doc.CreateAttribute(SC.Qualifier);

            XmlElement description = doc.CreateElement(SC.Description, uri1);
            XmlElement sigPolicyHash = doc.CreateElement(SC.SigPolicyHash, uri1);
            XmlElement digestMethod = doc.CreateElement(SC.DigestMethod, uri2);
            XmlAttribute algorithm = doc.CreateAttribute(SC.Algorithm);

            XmlElement digestValue = doc.CreateElement(SC.DigestValue, uri2);
            XmlElement signerRole = doc.CreateElement(SC.SignerRole, uri1);
            XmlElement claimedRoles = doc.CreateElement(SC.ClaimedRoles, uri1);
            XmlElement claimedRole = doc.CreateElement(SC.ClaimedRole, uri1);

            qualifier.InnerText = "OIDAsURI";
            identifier.InnerText = urlPolicy;
            description.InnerText = descripcion;
            algorithm.InnerText = SC.URI_XMLDSIGNSHA1;
            digestValue.InnerText = SC.URI_POLICY_HASH;
            claimedRole.InnerText = role;

            identifier.Attributes.Append(qualifier);
            digestMethod.Attributes.Append(algorithm);

            sigPolicyId.AppendChild(identifier);
            sigPolicyId.AppendChild(description);
            sigPolicyHash.AppendChild(digestMethod);
            sigPolicyHash.AppendChild(digestValue);

            signaturePolicyId.AppendChild(sigPolicyId);
            signaturePolicyId.AppendChild(sigPolicyHash);

            signaturePolicyIdentifier.AppendChild(signaturePolicyId);

            claimedRoles.AppendChild(claimedRole);
            signerRole.AppendChild(claimedRoles);

            qualifyingProperties.ChildNodes[0].ChildNodes[0].AppendChild(signaturePolicyIdentifier);
            qualifyingProperties.ChildNodes[0].ChildNodes[0].AppendChild(signerRole);
            return qualifyingProperties;
        }

    }

    /// <summary>
    /// XAdES version of SignedXml
    /// </summary>
    class SignedXml_XAdES_version : SignedXml
    {

        /// <summary>
        /// XmlDocument with XAdES Signedproperties
        /// </summary>
        private XmlDocument pDoc2;
        public XmlDocument doc2
        {
            get
            {
                return pDoc2;
            }
            set
            {
                pDoc2 = value;
            }
        }


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="doc">XmlDocument SignedProperties</param>
        public SignedXml_XAdES_version(XmlDocument doc)
            : base(doc)
        {

        }

        /// <summary>
        /// This methods overrides the original SignedXml method with XAdES adaptions
        /// </summary>
        /// <param name="doc">Signed XmlDocument.</param>
        /// <param name="id">Element Id to get</param>
        /// <returns></returns>
        public override XmlElement GetIdElement(XmlDocument doc, string id)
        {
            try
            {
                if (id.Equals(SC.OBJECTSIGNEDPROPERTIES))
                {
                    try
                    {
                        return base.GetIdElement(doc2, id);
                    }
                    catch (Exception)
                    {
                        throw new Exception(SC.ErrorSignatureSignedProperties);
                    }

                }
                else if (id.Equals(SC.OBJECTKEYINFO))
                {
                    try
                    {
                        string uri1 = SC.URI_XMLDSIGN;
                        XmlDocument s = new XmlDocument();
                        XmlElement myKeyInfo = s.CreateElement(SC.KeyInfo, uri1);

                        if (doc != null)
                        {
                            for (int a = 0; a < doc.DocumentElement.Attributes.Count; a++)
                            {
                                if (!doc.DocumentElement.Attributes[a].Name.Equals("xmlns") && doc.DocumentElement.Attributes[a].Name.Contains("xmlns"))
                                {
                                    XmlAttribute xmlns = s.CreateAttribute(@"" + doc.DocumentElement.Attributes[a].Name);
                                    xmlns.Value = doc.DocumentElement.Attributes[a].Value;
                                    myKeyInfo.Attributes.Append(xmlns);
                                }
                            }
                        }

                        XmlAttribute atrbId = s.CreateAttribute(SC.Id);
                        atrbId.Value = SC.OBJECTKEYINFO;
                        myKeyInfo.Attributes.Append(atrbId);
                        s.AppendChild(myKeyInfo);
                        XmlElement X509Data = s.CreateElement(SC.X509Data, uri1);
                        XmlElement x509Certificate = s.CreateElement(SC.X509Certificate, uri1);
                        x509Certificate.InnerText = base.KeyInfo.GetXml().GetElementsByTagName(SC.X509Certificate).Item(0).InnerText;
                        X509Data.AppendChild(x509Certificate);
                        s.DocumentElement.AppendChild(X509Data);

                        XmlElement KeyValue = s.CreateElement(SC.KeyValue, uri1);
                        XmlElement rSAKeyValue = s.CreateElement(SC.RSAKeyValue, uri1);
                        XmlElement modulus = s.CreateElement(SC.Modulus, uri1);
                        XmlElement exponent = s.CreateElement(SC.Exponent, uri1);
                        modulus.InnerText = base.KeyInfo.GetXml().GetElementsByTagName(SC.Modulus).Item(0).InnerText;
                        exponent.InnerText = base.KeyInfo.GetXml().GetElementsByTagName(SC.Exponent).Item(0).InnerText;
                        rSAKeyValue.AppendChild(modulus);
                        rSAKeyValue.AppendChild(exponent);
                        KeyValue.AppendChild(rSAKeyValue);
                        s.DocumentElement.AppendChild(KeyValue);

                        return s.DocumentElement;
                    }
                    catch (Exception)
                    {
                        throw new Exception(SC.ErrorSignatureKeyInfo);
                    }


                }
                else
                {
                    try
                    {
                        return base.GetIdElement(doc, id);
                    }
                    catch (Exception)
                    {
                        throw new Exception(SC.ErrorUnknowElementToSign);
                    }
                }

            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }

    /// <summary>
    /// Constants for XAdES signatures
    /// </summary>
    public class SC
    {

        public static string APLICACIONOCSPREQUEST = "application/ocsp-request";
        public static string APLICACIONTIMESTAMPQUERY = "application/timestamp-query";
        public static string OBJECTQUALIFYINGPROPERTIES = "SignatureQP_11";
        public static string OBJECTSIGNEDPROPERTIES = "SignatureSP_11";
        public static string OBJECTSIGNEDSIGNATUREPROPERTIES = "SignatureSSP_11";
        public static string OBJECTKEYINFO = "SignatureKI_11";
        public static string SIGNATUREOBJECT = "SignatureXAdESProps";
        public static string SIGNATURE_1 = "Signature_1";
        public static string SIGNEDINFO = "SignatureSI_11";
        public static string REFERENCE_1 = "SignedRef_1";
        public static string REFERENCE_2 = "SignedRef_2";
        public static string REFERENCE_3 = "SignedRef_3";
        public static string DESCRIPCION = "Spanish Facturae Signature Policy 3.1";

        public static string URI_ETSI = "http://uri.etsi.org/01903/v1.3.2#";
        public static string URI_XMLDSIGN = "http://www.w3.org/2000/09/xmldsig#";
        public static string URI_XMLDSIGNSHA1 = "http://www.w3.org/2000/09/xmldsig#sha1";
        public static string URI_SIGNEDPROPERTIES = "http://uri.etsi.org/01903#SignedProperties";
        public static string URI_POLICY = "http://www.facturae.es/politica_de_firma_formato_facturae/politica_de_firma_formato_facturae_v3_1.pdf";
        public static string URI_POLICY_HASH = "Ohixl6upD6av8N7pEvDABhEL6hM=";

        public static string Id = "Id";
        public static string Algorithm = "Algorithm";
        public static string Target = "Target";

        public static string Cert = "Cert";
        public static string CertDigest = "CertDigest";
        public static string DigestMethod = "DigestMethod";
        public static string DigestValue = "DigestValue";
        public static string IssuerSerial = "IssuerSerial";
        public static string X509IssuerName = "X509IssuerName";
        public static string X509SerialNumber = "X509SerialNumber";
        public static string X509Certificate = "X509Certificate";
        public static string KeyInfo = "KeyInfo";
        public static string X509Data = "X509Data";
        public static string KeyValue = "KeyValue";
        public static string RSAKeyValue = "RSAKeyValue";
        public static string Modulus = "Modulus";
        public static string Exponent = "Exponent";
        public static string SignaturePolicyIdentifier = "SignaturePolicyIdentifier";
        public static string SignaturePolicyId = "SignaturePolicyId";
        public static string SigPolicyId = "SigPolicyId";
        public static string Identifier = "Identifier";
        public static string Qualifier = "Qualifier";
        public static string Description = "Description";
        public static string SigPolicyHash = "SigPolicyHash";
        public static string SignerRole = "SignerRole";
        public static string ClaimedRoles = "ClaimedRoles";
        public static string ClaimedRole = "ClaimedRole";
        public static string SigningCertificate = "SigningCertificate";
        public static string SigningTime = "SigningTime";
        public static string QualifyingProperties = "QualifyingProperties";
        public static string SignedProperties = "SignedProperties";
        public static string SignedSignatureProperties = "SignedSignatureProperties";
        public static string DNNotValid = "DNNotValid";
        public static string ErrorSignatureSignedProperties = "ErrorSignatureSignedProperties";
        public static string ErrorSignatureKeyInfo = "ErrorSignatureKeyInfo";
        public static string ErrorUnknowElementToSign = "ErrorUnknowElementToSign";
        public static string ErrorCertificateNull = "ErrorCertificateNull";
        public static string ErrorKeyInfo = "ErrorKeyInfo";
        public static string ErrorDataObjectSignedProperies = "ErrorDataObjectSignedProperies";
        public static string ErrorPrivateKey = "ErrorPrivateKey";
        public static string ErrorToSign = "ErrorToSign";

    }
}

