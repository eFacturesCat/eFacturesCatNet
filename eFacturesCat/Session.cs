using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using eFacturesCat.Commons;
using eFacturesCat.Secure;
using eFacturesCat.Deliver;
using eFacturesCat.Transform;
using System.Security.Cryptography.X509Certificates;

namespace eFacturesCat
{
    /// <summary>
    /// Session class for batch invoice processing
    /// </summary>
    /// <author>@santicasas</author>
    public class Session
    {
        public X509Certificate2 sessionCertificate { get; private set; }
        public EndPoint sessionEndPoint { get; private set; }

        /// <summary>
        /// Constructor for signing and deliver
        /// </summary>
        /// <param name="cert">Certificate for signing</param>
        /// <param name="ep">EndPoint for deliver</param>
        public Session(X509Certificate2 cert, EndPoint ep)
        {
            sessionCertificate = cert;
            sessionEndPoint = ep;
        }

        /// <summary>
        /// Constructor only for sign
        /// </summary>
        /// <param name="cert">Certificate for signing</param>
        public Session(X509Certificate2 cert)
        {
            sessionCertificate = cert;
            sessionEndPoint = null;
        }

        /// <summary>
        /// Constructor only for deliver
        /// </summary>
        /// <param name="ep">EndPoint for deliver</param>
        public Session(EndPoint ep)
        {
            sessionCertificate = null;
            sessionEndPoint = ep;
        }

        /// <summary>
        /// Check Certificate Status
        /// </summary>
        /// <param name="urlOCSP">String to OCSP Service. If null, try to find inside certificate</param>
        /// <returns></returns>
        public Response checkCertificate(String urlOCSP)
        {
            return CertUtils.checkCertificate(sessionCertificate, urlOCSP);
        }

        /// <summary>
        /// Sign
        /// </summary>
        /// <param name="xmlInvoice">Invoice to be signed</param>
        /// <param name="invoiceFormat">Invoice Format</param>
        /// <param name="sInvoice">Signed Invoice</param>
        /// <returns>Response of result of the method</returns>
        public Response signInvoice(XMLInvoice xmlInvoice, String invoiceFormat, out SecuredInvoice sInvoice)
        {
            sInvoice = null;
            if (invoiceFormat == Constants.facturae32_EPES)
            {
                SecuredFacturae3_2 sFe32;
                Response resp = signFacturae3_2_EPES((Facturae_3_2)xmlInvoice, out sFe32);
                sInvoice = sFe32;
                return resp;
            }
            return new Response(Response.Error, Response.WrongInvoice, invoiceFormat + " not supported");
        }

        /// <summary>
        /// Deliver
        /// </summary>
        /// <param name="invoiceFormat">Invoice Format</param>
        /// <param name="sInvoice">Signed Invoice</param>
        /// <returns>Response of result of the method</returns>
        public Response Deliver(String invoiceFormat, SecuredInvoice sInvoice)
        {
            if (invoiceFormat == Constants.facturae32_EPES)
                return DeliverFacturae3_2_EPES((SecuredFacturae3_2)sInvoice);
            return new Response(Response.Error, Response.WrongInvoice, invoiceFormat + " not supported");
        }

        private Response DeliverFacturae3_2_EPES(SecuredFacturae3_2 sFe32)
        {
            // Deliver signed invoice to session pimefactura endpoint
            try
            {
                DeliverInvoice di = new DeliverInvoice(sFe32.xmlInvoiceSecured, sessionEndPoint);
                DeliverResponse dr = di.deliverInvoice();
                return dr;
            }
            catch (Exception ex)
            {
                return new Response(Response.Error, Response.ConnectError, ex.Message);
            }

        }

        /// <summary>
        /// Sign and Deliver Invoice
        /// </summary>
        /// <param name="xmlInvoice">Invoice to be signed</param>
        /// <param name="invoiceFormat">Invoice Format</param>
        /// <param name="sInvoice">Signed Invoice</param>
        /// <returns>Response of result of the method</returns>
        public Response signAndDeliver(XMLInvoice xmlInvoice, String invoiceFormat, out SecuredInvoice sInvoice)
        {
            sInvoice = null;
            if (invoiceFormat == Constants.facturae32_EPES)
            {
                SecuredFacturae3_2 sFe32;
                Response resp = signAndDeliverFacturae3_2_EPES((Facturae_3_2)xmlInvoice, out sFe32);
                sInvoice = sFe32;
                return resp;
            }
            return new Response(Response.Error, Response.WrongInvoice, invoiceFormat + " not supported");
        }


        private Response signFacturae3_2_EPES(Facturae_3_2 fe32, out SecuredFacturae3_2 sFe32)
        {
            sFe32 = null;
            // Create secured invoice from unsigned invoice
            try
            {
                sFe32 = new SecuredFacturae3_2(fe32);
            }
            catch (Exception ex)
            {
                return new Response(Response.Error, Response.WrongInvoice, ex.Message);
            }

            // Secure with session certificate
            try
            {
                sFe32.secureInvoice(sessionCertificate, Constants.XAdES_EPES_Enveloped);
            }
            catch (Exception ex)
            {
                return new Response(Response.Error, Response.SigningError, ex.Message);
            }
            return new Response(Response.Correct, "", "");
        }

        private Response signAndDeliverFacturae3_2_EPES(Facturae_3_2 fe32, out SecuredFacturae3_2 sFe32)
        {
            sFe32 = null;
            Response respSignature = signFacturae3_2_EPES(fe32, out sFe32);
            if (respSignature.result != Response.Correct) return respSignature;
            return DeliverFacturae3_2_EPES(sFe32);
        }

        /// <summary>
        /// Close Session
        /// </summary>
        public void close()
        {
            sessionCertificate = null;
            if (sessionEndPoint != null)
            {
                sessionEndPoint.close();
                sessionEndPoint = null;
            }
        }
    }
}
