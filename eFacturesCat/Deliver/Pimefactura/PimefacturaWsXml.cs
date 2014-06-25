using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace eFacturesCat.Deliver.Pimefactura
{
    /// <summary>
    /// Xml structure to send invoice via WebServices to "pimefactura"
    /// </summary>
    internal class PimefacturaWsXml
    {
        #region "Properties"

        /// <summary>
        /// The invoice
        /// </summary>
        public Facturas facturas { get; set; }

        #endregion

        #region "Public Methods"

        /// <summary>
        /// Generate de Request (param1 must be "0")
        /// </summary>
        /// <param name="base64Document">Invoice B64 encoded</param>
        /// <param name="ak">Issuer Web Service Key</param>
        /// <param name="param1">param1</param>
        /// <param name="param2">param2</param>
        /// <param name="param3">param3</param>
        /// <returns></returns>
        public XmlDocument wrapInvoice(string base64Document, string ak, int param1, string param2, string param3)
        {
            Facturas f = new Facturas(base64Document, ak, param1, param2, param3);
            return wrap(f);
        }

        /// <summary>
        /// Method that creates the xml of the invoice by setting data
        /// </summary>
        /// <param name="facturas">Invoice with all the data to create the xml</param>
        /// <returns></returns>
        public XmlDocument wrap(Facturas facturas)
        {
            XmlDocument xdoc = null;
            try
            {
                string temp = Path.GetTempPath() + @"Sarchivosp1.xml";
                XmlSerializer serializer = new XmlSerializer(typeof(Facturas));
                System.IO.StreamWriter w = new System.IO.StreamWriter(temp);
                serializer.Serialize(w, facturas);
                w.Close();
                xdoc = new XmlDocument();
                xdoc.Load(temp);
                File.Delete(temp);
            }
            catch (Exception) { }

            return xdoc;
        }

        #endregion
    }

    /// <summary>
    /// Class containing all necessary information for service
    /// </summary>
    public class Facturas
    {
        #region "Properties"

        public Factura factura { get; set; }

        public string ak { get; set; }

        public int param1 { get; set; }

        public string param2 { get; set; }

        public string param3 { get; set; }

        #endregion

        #region "Constructors"

        /// <summary>
        /// Constructor
        /// </summary>
        public Facturas()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="base64Document">b64 encoded invoice</param>
        /// <param name="ak">Issuer Web Service Key</param>
        /// <param name="param1"></param>
        /// <param name="param2"></param>
        /// <param name="param3"></param>
        public Facturas(string base64Document, string ak, int param1, string param2, string param3)
        {
            this.factura = new Factura(base64Document);
            this.ak = ak;
            this.param1 = param1;
            this.param2 = param2;
            this.param3 = param3;
        }

        #endregion
    }

    /// <summary>
    /// Class than stores b64 encoded invoice
    /// </summary>
    public class Factura
    {
        #region "Properties"

        /// <summary>
        /// b64 encoded invoice
        /// </summary>
        public string base64Document { get; set; }

        #endregion

        #region "Constructor"

        /// <summary>
        /// Constructor
        /// </summary>
        public Factura()
        {            
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="base64Document">b64 encoded invoice</param>
        public Factura(string base64Document)
        {
            this.base64Document = base64Document;
        }

        #endregion

    }
}
