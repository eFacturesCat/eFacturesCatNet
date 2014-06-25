using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using eFacturesCat.Transform;

namespace eFacturesCat.Deliver
{
    /// <summary>
    /// Abstract Class for endPoints to deliver invoices
    /// </summary>
    public abstract class EndPoint
    {
        /// <summary>
        /// Method to deliverInvoice
        /// </summary>
        /// <param name="xmlInvoice">XmlInovice to be delivered</param>
        /// <returns>Result of delivery</returns>
        public abstract DeliverResponse deliverInvoice(XMLInvoice xmlInvoice);

        /// <summary>
        /// Method to close session to EndPoint
        /// </summary>
        public abstract void close();
    }
}
