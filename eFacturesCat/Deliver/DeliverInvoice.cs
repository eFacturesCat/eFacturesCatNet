using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using eFacturesCat.Transform;

namespace eFacturesCat.Deliver
{
    /// <summary>
    /// Generic class for Invoice Delivery
    /// </summary>
    public class DeliverInvoice
    {
        private XMLInvoice xmlInvoice;
        private EndPoint endPoint;
        private String invoiceType;
        private String invoiceVersion;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="xmlInvoice">xmlInvoice to deliver</param>
        /// <param name="endPoint">endpoint to send invoice</param>
        public DeliverInvoice(XMLInvoice xmlInvoice, EndPoint endPoint, String invoiceType, String invoiceVersion)
        {
            this.endPoint = endPoint;
            this.xmlInvoice = xmlInvoice;
            this.invoiceType = invoiceType;
            this.invoiceVersion = invoiceVersion;
        }

        /// <summary>
        /// Method for deliver invoice
        /// </summary>
        /// <returns>Results of delivey</returns>
        public DeliverResponse deliverInvoice()
        {
            return (endPoint.deliverInvoice(xmlInvoice, this.invoiceType, this.invoiceVersion));
        }
        
                
    }
}
