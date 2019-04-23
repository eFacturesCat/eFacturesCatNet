using System;
using System.IO;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Xml;
using eFacturesCat.Generate;
using eFacturesCat.Commons;

namespace eFacturesCat.Transform
{
    /// <summary>
    /// XMLInvoice Generic Class
    /// </summary>
    public abstract class XMLInvoice
    {
        #region "Fields"

        //private Invoice invoice;
        //private String XSLT_uri;
        //private XmlDocument domInvoice;

        #endregion

        #region "Properties"    
        /// <summary>
        /// Invoice File StreamReader (stored into String)
        /// </summary>
        public StreamReader xmlInputStream {
            get
            {
                byte[] bytes = Encoding.UTF8.GetBytes(xmlString);
                return new StreamReader(new MemoryStream(bytes));
            }
        }

        private string xmlString;
        public bool isValidXml;
        public string xmlErrorStr;

        #endregion

        #region "Constructors"

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="fileName">Invoice filename</param>
        public XMLInvoice(String fileName)
        {
            StreamReader stream = new StreamReader(fileName);
            xmlString = stream.ReadToEnd();
            stream.Dispose();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="xmlInputStream">StreamReader</param>
        public XMLInvoice(StreamReader xmlInputStream)
        {
            xmlString = xmlInputStream.ReadToEnd(); ;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="doc">XmlDocument</param>
        public XMLInvoice(XmlDocument doc)
        {
            xmlString = doc.InnerXml;
        }

        #endregion

        #region "Public Methods"

        /// <summary>
        /// set xmlString
        /// </summary>
        /// <param name="xmlStr">XML String</param>
        public void setXmlString(String xmlStr)
        {
            xmlString = xmlStr;
        }

        /// <summary>
        /// Save Invoice into file
        /// </summary>
        /// <param name="fileName">Invoice filename</param>
        public void saveXML(String fileName)
        {
            xmlInputStream.BaseStream.Position = 0;
            StreamWriter sw = new StreamWriter(fileName);
            sw.Write(xmlInputStream.ReadToEnd());
            sw.Close();
        }

        /// <summary>
        /// Returns a byte array of the invoice
        /// </summary>
        /// <returns>The byte array</returns>
        public byte[] toByteArray()
        {
            xmlInputStream.BaseStream.Position = 0;
            var bytes = default(byte[]);
            using (var memstream = new MemoryStream())
            {
                //dotnet framework 4.0
                //xmlInputStream.BaseStream.CopyTo(memstream);
                Utils.CopyStream(xmlInputStream.BaseStream , memstream);
                bytes = memstream.ToArray();
            }
            return bytes;
        }

        public override string ToString()
        {
            return xmlString;
        }

        public abstract void deserialize();
        public abstract String getBuyerEmail();

        #endregion

    }
}
