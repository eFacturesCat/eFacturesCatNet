using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Xml;
using eFacturesCat.Transform;
using System.Security.Cryptography.X509Certificates;

namespace eFacturesCat.Secure
{
    /// <summary>
    /// Abstract class for "secured" invoices (i.e. Signed Xml Invoices)
    /// </summary>
    public abstract class SecuredInvoice
    {

        private XMLInvoice xmlInvoice { get; set; }
        public XMLInvoice xmlInvoiceSecured { get; set; }

        /// <summary>
        /// Constructor with unsecured invoice
        /// </summary>
        /// <param name="xmlInvoice">XmlInvoice to be securized</param>
        public SecuredInvoice(XMLInvoice xmlInvoice)
        {
            this.xmlInvoice = xmlInvoice;
        }

        /// <summary>
        /// Constructor with filename of secured invoice
        /// </summary>
        /// <param name="fileName">fileName of secured invoice</param>
        public SecuredInvoice(string fileName) { }

        /// <summary>
        /// Save the invoice secured
        /// </summary>
        /// <param name="fileName"></param>
        public void saveInvoiceSigned(String fileName)
        {
            xmlInvoiceSecured.saveXML(fileName);
        }

        /// <summary>
        /// General Method to Secure Invoices with XAdES-EPES Enveloped Signature (i.e. Spanish facturae)
        /// </summary>
        /// <param name="cert"></param>
        /// <param name="policy"></param>
        /// <param name="invoiceType"></param>
        public void signInvoiceXadesEpesEnveloped(X509Certificate2 cert, String policy)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlInvoice.xmlInputStream);
            xmlDoc.PreserveWhitespace = true;
            // Eliminate innecesary namespaces (xsi)
            removeInnecessaryAttributes(xmlDoc.DocumentElement.Attributes);
            XAdES_EPES_facturae xmlSignature = new XAdES_EPES_facturae(xmlDoc, eFacturesCat.Commons.Constants.ROLE, cert);
            xmlInvoiceSecured = (XMLInvoice)Activator.CreateInstance(xmlInvoice.GetType(), new Object[] {xmlDoc});
        }

        private XmlAttributeCollection removeInnecessaryAttributes(XmlAttributeCollection attr)
        {
            foreach (XmlAttribute att in attr)
            {
                if (att.Name.Contains("xsi"))
                {
                    attr.Remove(att);
                    removeInnecessaryAttributes(attr);
                    break;
                }

            }
            return attr;
        }
    }
}
