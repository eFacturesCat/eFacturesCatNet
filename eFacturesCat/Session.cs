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
        /// <param name="invoiceType">Invoice type (facturae, UBL)</param>
        /// <param name="invoiceVersion">Invoice version (3.2, 3.2.1, 3.2.2, 2.1)</param>
        /// <param name="sInvoice">Signed Invoice</param>
        /// <returns>Response of result of the method</returns>
        public Response signInvoice(XMLInvoice xmlInvoice, String invoiceType, String invoiceVersion, out SecuredInvoice sInvoice)
        {
            sInvoice = null;
            switch (invoiceType)
            {
                case Constants.INVOICE_TYPE_FACTURAE: {
                    switch (invoiceVersion)
                    {
                        case Constants.FACTURAE_VERSION_3_2:
                            {
                                SecuredFacturae3_2 sFe32;
                                Response resp = signFacturae3_2_EPES((GlobalInvoice)xmlInvoice, out sFe32);
                                sInvoice = sFe32;
                                return resp;
                            }
                        case Constants.FACTURAE_VERSION_3_2_1:
                            {
                                SecuredFacturae3_2_1 sFe321;
                                Response resp = signFacturae3_2_1_EPES((GlobalInvoice)xmlInvoice, out sFe321);
                                sInvoice = sFe321;
                                return resp;
                            }
                        case Constants.FACTURAE_VERSION_3_2_2:
                            {
                                SecuredFacturae3_2_2 sFe322;
                                Response resp = signFacturae3_2_2_EPES((GlobalInvoice)xmlInvoice, out sFe322);
                                sInvoice = sFe322;
                                return resp;
                            }
                        default:
                            return new Response(Response.Error, Response.WrongInvoice, "Invoice version: " + invoiceVersion + " not supported");
                    }   
                }
                case Constants.INVOICE_TYPE_UBL:
                    {
                        return new Response(Response.Error, Response.WrongInvoice, "UBL invoices can not be signed.");
                    } 
                default:
                    return new Response(Response.Error, Response.WrongInvoice, "Invoice type: " + invoiceType + " not supported");
            }
        }

        /// <summary>
        /// Deliver
        /// </summary>
        /// <param name="invoiceType">Invoice type (facturae, UBL)</param>
        /// <param name="invoiceVersion">Invoice version (3.2, 3.2.1, 3.2.2, 2.1)</param>
        /// <param name="sInvoice">Signed Invoice</param>
        /// <returns>Response of result of the method</returns>
        public Response Deliver(String invoiceType, String invoiceVersion, XMLInvoice sInvoiceSecured)
        {
            switch (invoiceType)
            {
                case Constants.INVOICE_TYPE_FACTURAE:
                    {
                        return DeliverFacturaeEPES(sInvoiceSecured, invoiceVersion);
                    }
                case Constants.INVOICE_TYPE_UBL:
                    {
                        return DeliverUBL(sInvoiceSecured, invoiceVersion);
                    }
                default:
                    return new Response(Response.Error, Response.WrongInvoice, "Invoice type: "+ invoiceType + " not supported");
            }
        }

        private Response DeliverFacturaeEPES(XMLInvoice aXmlInvoice, String facturaeVersion)
        {
            // Deliver signed invoice to session pimefactura endpoint
            try
            {
                DeliverInvoice di = new DeliverInvoice(aXmlInvoice, sessionEndPoint, Constants.INVOICE_TYPE_FACTURAE, facturaeVersion);
                DeliverResponse dr = di.deliverInvoice();
                return dr;
            }
            catch (Exception ex)
            {
                return new Response(Response.Error, Response.ConnectError, ex.Message);
            }

        }

        private Response DeliverUBL(XMLInvoice aXmlInvoice, String sUBLVersion)
        {
            // Deliver signed invoice to session pimefactura endpoint
            try
            {
                DeliverInvoice di = new DeliverInvoice(aXmlInvoice, sessionEndPoint, Constants.INVOICE_TYPE_UBL, sUBLVersion);
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
        /// <param name="invoiceType">Invoice type (facturae, UBL)</param>
        /// <param name="invoiceVersion">Invoice version (3.2, 3.2.1, 3.2.2, 2.1)</param>
        /// <param name="sInvoice">Signed Invoice</param>
        /// <returns>Response of result of the method</returns>
        public Response signAndDeliver(XMLInvoice xmlInvoice, String invoiceType, String invoiceVersion, out SecuredInvoice sInvoice)
        {
            sInvoice = null;
            switch (invoiceType)
            {
                case Constants.INVOICE_TYPE_FACTURAE:
                    {
                        switch (invoiceVersion)
                        {
                            case Constants.FACTURAE_VERSION_3_2:
                                {
                                    SecuredFacturae3_2 sFe32;
                                    Response resp = signAndDeliverFacturae3_2_EPES((GlobalInvoice)xmlInvoice, out sFe32);
                                    sInvoice = sFe32;
                                    return resp;
                                }
                            case Constants.FACTURAE_VERSION_3_2_1:
                                {
                                    SecuredFacturae3_2_1 sFe321;
                                    Response resp = signAndDeliverFacturae3_2_1_EPES((GlobalInvoice)xmlInvoice, out sFe321);
                                    sInvoice = sFe321;
                                    return resp;
                                }
                            case Constants.FACTURAE_VERSION_3_2_2:
                                {
                                    SecuredFacturae3_2_2 sFe322;
                                    Response resp = signAndDeliverFacturae3_2_2_EPES((GlobalInvoice)xmlInvoice, out sFe322);
                                    sInvoice = sFe322;
                                    return resp;
                                }
                            default:
                                return new Response(Response.Error, Response.WrongInvoice, "Invoice vVersion: " + invoiceVersion + " not supported");
                        }
                    }
                case Constants.INVOICE_TYPE_UBL:
                    {
                        return new Response(Response.Error, Response.WrongInvoice, "UBL invoices can not be signed.");
                    }
                default:
                    return new Response(Response.Error, Response.WrongInvoice, "Invoice type: " + invoiceType + " not supported");
            }
        }


        private Response signFacturae3_2_EPES(GlobalInvoice fe32, out SecuredFacturae3_2 sFe32)
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

        private Response signFacturae3_2_1_EPES(GlobalInvoice fe321, out SecuredFacturae3_2_1 sFe321)
        {
            sFe321 = null;
            // Create secured invoice from unsigned invoice
            try
            {
                sFe321 = new SecuredFacturae3_2_1(fe321);
            }
            catch (Exception ex)
            {
                return new Response(Response.Error, Response.WrongInvoice, ex.Message);
            }

            // Secure with session certificate
            try
            {
                sFe321.secureInvoice(sessionCertificate, Constants.XAdES_EPES_Enveloped);
            }
            catch (Exception ex)
            {
                return new Response(Response.Error, Response.SigningError, ex.Message);
            }
            return new Response(Response.Correct, "", "");
        }

        private Response signFacturae3_2_2_EPES(GlobalInvoice fe322, out SecuredFacturae3_2_2 sFe322)
        {
            sFe322 = null;
            // Create secured invoice from unsigned invoice
            try
            {
                sFe322 = new SecuredFacturae3_2_2(fe322);
            }
            catch (Exception ex)
            {
                return new Response(Response.Error, Response.WrongInvoice, ex.Message);
            }

            // Secure with session certificate
            try
            {
                sFe322.secureInvoice(sessionCertificate, Constants.XAdES_EPES_Enveloped);
            }
            catch (Exception ex)
            {
                return new Response(Response.Error, Response.SigningError, ex.Message);
            }
            return new Response(Response.Correct, "", "");
        }

        private Response signAndDeliverFacturae3_2_EPES(GlobalInvoice fe32, out SecuredFacturae3_2 sFe32)
        {
            sFe32 = null;
            Response respSignature = signFacturae3_2_EPES(fe32, out sFe32);
            if (respSignature.result != Response.Correct) return respSignature;
            return DeliverFacturaeEPES(sFe32.xmlInvoiceSecured, Constants.FACTURAE_VERSION_3_2);
        }

        private Response signAndDeliverFacturae3_2_1_EPES(GlobalInvoice fe321, out SecuredFacturae3_2_1 sFe321)
        {
            sFe321 = null;
            Response respSignature = signFacturae3_2_1_EPES(fe321, out sFe321);
            if (respSignature.result != Response.Correct) return respSignature;
            return DeliverFacturaeEPES(sFe321.xmlInvoiceSecured, Constants.FACTURAE_VERSION_3_2_1);
        }

        private Response signAndDeliverFacturae3_2_2_EPES(GlobalInvoice fe322, out SecuredFacturae3_2_2 sFe322)
        {
            sFe322 = null;
            Response respSignature = signFacturae3_2_2_EPES(fe322, out sFe322);
            if (respSignature.result != Response.Correct) return respSignature;
            return DeliverFacturaeEPES(sFe322.xmlInvoiceSecured, Constants.FACTURAE_VERSION_3_2_2);
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
