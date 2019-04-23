using System;
using System.Xml;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using eFacturesCat.Transform;
using eFacturesCat.Deliver;
using eFacturesCat.Commons;

namespace eFacturesCat.Deliver.Pimefactura
{
    /// <summary>
    /// "pimefactura" endPoint for deliver invoices
    /// "pimefactura" is a envoicing service of PIMEC (www.pimefactura.com)
    /// Is a SOAP WebService
    /// </summary>
    public class EndPointPimefactura:EndPoint
    {
        /// <summary>
        /// Production environment EndPoint
        /// </summary>
        private static String urlProd = "http://pimefactura.com/axis2/services/PimecInvoiceReceptor.PimecInvoiceReceptor";
        /// <summary>
        /// Pre-production environment End Poing
        /// </summary>
        private static String urlPrepro = "http://new.pimefactura.com/axis2/services/PimecInvoiceReceptor.PimecInvoiceReceptor";
        /// <summary>
        /// Issuer Web Service key for sending invoices
        /// </summary>
        private String ak;
        /// <summary>
        /// Url to be used (assigned to urlProd or urlPrepro)
        /// </summary>
        private String url;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="ak">Issuer Web Service Key</param>
        /// <param name="envirnonment">Environment (Production or PreProduction)</param>
        public EndPointPimefactura(String ak, String envirnonment)
        {
            this.ak = ak;
            if (envirnonment == Constants.prod_environment)
                url = urlProd;
            else
                url = urlPrepro;
        }

        /// <summary>
        /// Overrides generic deliverInvoice method for sending invoices
        /// </summary>
        /// <param name="xmlInvoice">XmlInvoice to be delivered</param>
        /// <returns>Results of delivery</returns>
        public override DeliverResponse deliverInvoice(XMLInvoice xmlInvoice)
        {
            DeliverResponsePimefactura dr = new DeliverResponsePimefactura();
            try
            {
                String result = "0";
                PimefacturaWsXml inv = new PimefacturaWsXml();
                string sb64 = Convert.ToBase64String(xmlInvoice.toByteArray());
                XmlDocument omrequest = inv.wrapInvoice(sb64, ak, 0, "param2", "param3");
                PimecInvoiceReceptor pimecService = new PimecInvoiceReceptor(url);
                result = pimecService.summitInvoice(omrequest.DocumentElement);
                dr.setResponse(result);
            }
            catch (Exception ex)
            {
                dr.setError(DeliverResponse.ConnectError, ex.Message);
            }
            return dr;
        }

        /// <summary>
        /// Overrides generic deliverInvoice method for sending invoices
        /// </summary>
        /// <param name="xmlInvoice">XmlInvoice to be delivered</param>
        /// <param name="invoiceType">Invoice type</param>
        /// <param name="invoiceVersion">Invoice version</param>
        /// <returns>Results of delivery</returns>
        public override DeliverResponse deliverInvoice(XMLInvoice xmlInvoice, String invoiceType, String invoiceVersion)
        {
            DeliverResponsePimefactura dr = new DeliverResponsePimefactura();
            try
            {
                String result = "0";
                PimefacturaWsXml inv = new PimefacturaWsXml();
                string sb64 = Convert.ToBase64String(xmlInvoice.toByteArray());
                XmlDocument omrequest = inv.wrapInvoice(sb64, ak, 0, "param2", "param3");
                PimecInvoiceReceptor pimecService = new PimecInvoiceReceptor(url);
                result = pimecService.summitInvoice(omrequest.DocumentElement);
                dr.setResponse(result);
            }
            catch (Exception ex)
            {
                dr.setError(DeliverResponse.ConnectError, ex.Message);
            }
            return dr;
        }
        /// <summary>
        /// Method that overrides close session to EndPoint
        /// </summary>
        public override void close()
        {
            ak = null;
            url = null;
        }

    }
}
