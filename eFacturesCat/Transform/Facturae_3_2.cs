using System;
using System.Collections.Generic;
using System.Xml.Serialization;
//using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
//using eFacturesCat.Generate;
using eFacturesCat.Transform.facturae32;
using System.Xml.Schema;

namespace eFacturesCat.Transform
{
    /// <summary>
    /// Class for spanish facturae 3.2 invoice (not secured - previus to sign)
    /// </summary>
    public class Facturae_3_2:XMLInvoice
    {

        public Facturae fe { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="fileName">Invoice filename</param>
        public Facturae_3_2(String fileName) : base(fileName) { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="doc">XmlDocument</param>
        public Facturae_3_2(XmlDocument doc) : base(doc) { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="stream"></param>
        public Facturae_3_2(StreamReader stream) : base(stream) { }

        /// <summary>
        /// Convert XMLInputStream to Facturae Class
        /// </summary>
        public override void deserialize()
        {
            // Parse Schema
            XmlReaderSettings xmlsettings = new XmlReaderSettings();
            XmlTextReader xtrSchema = new XmlTextReader(new StringReader(global::eFacturesCat.Properties.Resources.Facturaev3_2));
            XmlSchema xmlSchema = new XmlSchema();
            xmlSchema = XmlSchema.Read(xtrSchema, null);
            xmlsettings.Schemas.Add(xmlSchema);
            xmlsettings.ValidationType = ValidationType.Schema;

            XmlTextReader xtrXML = new XmlTextReader(new StringReader(this.ToString()));


            try
            {
                XmlReader reader = XmlReader.Create(xtrXML, xmlsettings);
                while (reader.Read()) { }

                // Deserialize
                Facturae feTmp;
                Facturae.Deserialize(this.ToString(), out feTmp);
                fe = feTmp;
                isValidXml = true;
                xmlErrorStr = null;
            }
            catch (Exception ex)
            {
                isValidXml = false;
                xmlErrorStr = ex.Message;
            }

        }

        /// <SUMMARY>
        /// This method is invoked when the XML does not match
        /// the XML Schema.
        /// </SUMMARY>
        /// <PARAM name="sender"></PARAM>
        /// <PARAM name="args"></PARAM>
        //private void ValidationCallBack(object sender,
        //                           ValidationEventArgs args)
        //{
        //    // The xml does not match the schema.
        //    isValidXml = false;
        //} 

        public override String getBuyerEmail()
        {
            if (fe != null)
            {
                if (fe.Parties.BuyerParty.Item.GetType() == typeof(LegalEntityType))
                {
                    LegalEntityType entity = (LegalEntityType)fe.Parties.BuyerParty.Item;
                    if (entity.ContactDetails != null)
                        if (entity.ContactDetails.ElectronicMail != null)
                            return entity.ContactDetails.ElectronicMail;
                }
                else
                {
                    IndividualType entity = (IndividualType)fe.Parties.BuyerParty.Item;
                    if (entity.ContactDetails != null)
                        if (entity.ContactDetails.ElectronicMail != null)
                            return entity.ContactDetails.ElectronicMail;
                }

            }
            return null;
        }
    }
}
