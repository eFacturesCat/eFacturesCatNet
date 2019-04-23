using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Text;
using System.Xml;
using System.IO;
using System.Xml.Schema;
using eFacturesCat.Commons;

namespace eFacturesCat.Transform
{
    /// <summary>
    /// Class for spanish facturae 3.2 invoice (not secured - previus to sign)
    /// </summary>
    public class GlobalInvoice : XMLInvoice
    {

        public eFacturesCat.Transform.facturae32.Facturae facturae32 { get; private set; }
        public eFacturesCat.Transform.facturae321.Facturae facturae321 { get; private set; }
        public eFacturesCat.Transform.facturae322.Facturae facturae322 { get; private set; }

        public String invoiceType { get; set; }
        public String invoiceVersion { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="fileName">Invoice filename</param>
        public GlobalInvoice(String fileName) : base(fileName) { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="doc">XmlDocument</param>
        public GlobalInvoice(XmlDocument doc) : base(doc) {
            switch (doc.DocumentElement.NamespaceURI)
            {
                case Constants.NAMESPACE_FACTURAE_3_2:
                    {
                        this.invoiceType = Constants.INVOICE_TYPE_FACTURAE;
                        this.invoiceVersion = Constants.FACTURAE_VERSION_3_2;
                    } break;
                case Constants.NAMESPACE_FACTURAE_3_2_1:
                    {
                        this.invoiceType = Constants.INVOICE_TYPE_FACTURAE;
                        this.invoiceVersion = Constants.FACTURAE_VERSION_3_2_1;
                    } break;
                case Constants.NAMESPACE_FACTURAE_3_2_2:
                    {
                        this.invoiceType = Constants.INVOICE_TYPE_FACTURAE;
                        this.invoiceVersion = Constants.FACTURAE_VERSION_3_2_2;
                    } break;
                case Constants.NAMESPACE_UBL_2_1:
                    {
                        this.invoiceType = Constants.INVOICE_TYPE_UBL;
                        this.invoiceVersion = Constants.UBL_VERSION_2_1;
                    } break;
                default:
                    {
                        //Para deajrlo como está ahora, todo lo que no conozcamos los marcamos como Facturae 3.2
                        this.invoiceType = Constants.INVOICE_TYPE_FACTURAE;
                        this.invoiceVersion = Constants.FACTURAE_VERSION_3_2;
                        //Lo correcto sería lanzar una excepción de este tipo:
                        //throw new Exception("Invoice namespace is unknown.");
                    }break;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="stream"></param>
        public GlobalInvoice(StreamReader stream) : base(stream) { }

        /// <summary>
        /// Convert Facturae Class to XMLInputStream
        /// </summary>
        public void serialize() 
        {
            switch (this.invoiceType)
            {    
                case Constants.INVOICE_TYPE_FACTURAE:{
                    switch (this.invoiceVersion)
                    {
                        case Constants.FACTURAE_VERSION_3_2:
                            {
                                this.setXmlString(facturae32.Serialize());
                            } break;
                        case Constants.FACTURAE_VERSION_3_2_1:
                            {
                                this.setXmlString(facturae321.Serialize());
                            } break;
                        case Constants.FACTURAE_VERSION_3_2_2:
                            {
                                this.setXmlString(facturae321.Serialize());
                            } break;
                        default:
                            throw new Exception("Unknown Facturae version: " + this.invoiceVersion);
                    }      
                }break;
                case Constants.INVOICE_TYPE_UBL:{
                    throw new Exception("UBL format not suported to Serialize");
                }
                default:
                    throw new Exception("Unknown invoice format: "+ this.invoiceType);
            }            
        }

        /// <summary>
        /// Convert XMLInputStream to Facturae Class
        /// </summary>
        public override void deserialize()
        {
            try
            {
                switch (this.invoiceType)
                {
                    case Constants.INVOICE_TYPE_FACTURAE:
                    {
                        // Parse Schema
                        XmlReaderSettings xmlsettings = new XmlReaderSettings();

                        XmlTextReader xtrSchemaFacturae32 = new XmlTextReader(new StringReader(global::eFacturesCat.Properties.Resources.Facturaev3_2));
                        XmlSchema xmlSchemaFacturae32 = new XmlSchema();
                        xmlSchemaFacturae32 = XmlSchema.Read(xtrSchemaFacturae32, null);

                        XmlTextReader xtrSchemaFacturae321 = new XmlTextReader(new StringReader(global::eFacturesCat.Properties.Resources.Facturaev3_2_1));
                        XmlSchema xmlSchemaFacturae321 = new XmlSchema();
                        xmlSchemaFacturae321 = XmlSchema.Read(xtrSchemaFacturae321, null);

                        XmlTextReader xtrSchemaFacturae322 = new XmlTextReader(new StringReader(global::eFacturesCat.Properties.Resources.Facturaev3_2_2));
                        XmlSchema xmlSchemaFacturae322 = new XmlSchema();
                        xmlSchemaFacturae322 = XmlSchema.Read(xtrSchemaFacturae322, null);

                        //XML-DESIGN-CORE
                        XmlTextReader xtrSchemaXmlDSignCore = new XmlTextReader(new StringReader(global::eFacturesCat.Properties.Resources.xmldsig_core_schema));
                        XmlSchema xmlSchemaXmlDSignCore = new XmlSchema();
                        xmlSchemaXmlDSignCore = XmlSchema.Read(xtrSchemaXmlDSignCore, null);

                        /******* FACE B2B V 1.0  *******/
                        XmlTextReader xtrSchemaFACeB2Bv1_0 = new XmlTextReader(new StringReader(global::eFacturesCat.Properties.Resources.fb2b_extension));
                        XmlSchema xmlSchemaFACeB2Bv1_0 = new XmlSchema();
                        xmlSchemaFACeB2Bv1_0 = XmlSchema.Read(xtrSchemaFACeB2Bv1_0, null);


                        /******* FACE B2B V 1.1 *******/
                        XmlTextReader xtrSchemaFACeB2Bv1_1 = new XmlTextReader(new StringReader(global::eFacturesCat.Properties.Resources.fb2b_extensionv_1_1));
                        XmlSchema xmlSchemaFACeB2Bv1_1 = new XmlSchema();
                        xmlSchemaFACeB2Bv1_1 = XmlSchema.Read(xtrSchemaFACeB2Bv1_1, null);

                        /******* UtilitiesExtension *******/
                        XmlTextReader xtrSchemaUtilitiesExtension = new XmlTextReader(new StringReader(global::eFacturesCat.Properties.Resources.UtilitiesExtension));
                        XmlSchema xmlSchemaUtilitiesExtension = new XmlSchema();
                        xmlSchemaUtilitiesExtension = XmlSchema.Read(xtrSchemaUtilitiesExtension, null);

                        switch (this.invoiceVersion)
                        {
                            case Constants.FACTURAE_VERSION_3_2:
                            {
                                xmlsettings.Schemas.Add(xmlSchemaFacturae32);
                            } break;
                            case Constants.FACTURAE_VERSION_3_2_1:
                            {
                                xmlsettings.Schemas.Add(xmlSchemaFacturae321);
                            } break;
                            case Constants.FACTURAE_VERSION_3_2_2:
                            {
                                xmlsettings.Schemas.Add(xmlSchemaFacturae322);
                            } break;
                            default:
                                throw new Exception("Unknown Facturae version: " + this.invoiceVersion);
                        }
                        xmlsettings.Schemas.Add(xmlSchemaXmlDSignCore);
                        xmlsettings.Schemas.Add(xmlSchemaFACeB2Bv1_0);
                        xmlsettings.Schemas.Add(xmlSchemaFACeB2Bv1_1);
                        xmlsettings.Schemas.Add(xmlSchemaUtilitiesExtension);

                        xmlsettings.ValidationType = ValidationType.Schema;
                        XmlTextReader xtrXML = new XmlTextReader(new StringReader(this.ToString()));

                        XmlReader reader = XmlReader.Create(xtrXML, xmlsettings);
                        while (reader.Read()) { }

                        //Deserialize
                        switch (this.invoiceVersion)
                        {
                            case Constants.FACTURAE_VERSION_3_2:
                                {
                                    eFacturesCat.Transform.facturae32.Facturae feTmp;
                                    eFacturesCat.Transform.facturae32.Facturae.Deserialize(this.ToString(), out feTmp);
                                    facturae32 = feTmp;
                                    isValidXml = true;
                                    xmlErrorStr = null;
                                } break;
                            case Constants.FACTURAE_VERSION_3_2_1:
                                {
                                    eFacturesCat.Transform.facturae321.Facturae feTmp;
                                    eFacturesCat.Transform.facturae321.Facturae.Deserialize(this.ToString(), out feTmp);
                                    facturae321 = feTmp;
                                    isValidXml = true;
                                    xmlErrorStr = null;
                                } break;
                            case Constants.FACTURAE_VERSION_3_2_2:
                                {
                                    eFacturesCat.Transform.facturae322.Facturae feTmp;
                                    eFacturesCat.Transform.facturae322.Facturae.Deserialize(this.ToString(), out feTmp);
                                    facturae322 = feTmp;
                                    isValidXml = true;
                                    xmlErrorStr = null;
                                } break;
                            default:
                                throw new Exception("Unknown Facturae version: " + this.invoiceVersion);
                        }
                    } break;

                    case Constants.INVOICE_TYPE_UBL:
                    {
                        // Parse Schema
                        XmlReaderSettings xmlsettings = new XmlReaderSettings();

                        XmlTextReader xtrSchemaUBLCommonAggregateComponents_2_1 = new XmlTextReader(new StringReader(global::eFacturesCat.Properties.Resources.UBL_CommonAggregateComponents_2_1));
                        XmlSchema xmlSchemaUBLCommonAggregateComponents_2_1 = new XmlSchema();
                        xmlSchemaUBLCommonAggregateComponents_2_1 = XmlSchema.Read(xtrSchemaUBLCommonAggregateComponents_2_1, null);

                        XmlTextReader xtrSchemaUBLCommonBasicComponents_2_1 = new XmlTextReader(new StringReader(global::eFacturesCat.Properties.Resources.UBL_CommonBasicComponents_2_1));
                        XmlSchema xmlSchemaUBLCommonBasicComponents_2_1 = new XmlSchema();
                        xmlSchemaUBLCommonBasicComponents_2_1 = XmlSchema.Read(xtrSchemaUBLCommonBasicComponents_2_1, null);

                        XmlTextReader xtrSchemaUBLCommonExtensionComponents_2_1 = new XmlTextReader(new StringReader(global::eFacturesCat.Properties.Resources.UBL_CommonExtensionComponents_2_1));
                        XmlSchema xmlSchemaUBLCommonExtensionComponents_2_1 = new XmlSchema();
                        xmlSchemaUBLCommonExtensionComponents_2_1 = XmlSchema.Read(xtrSchemaUBLCommonExtensionComponents_2_1, null);

                        XmlTextReader xtrSchemaUBLCommonSignatureComponents_2_1 = new XmlTextReader(new StringReader(global::eFacturesCat.Properties.Resources.UBL_CommonSignatureComponents_2_1));
                        XmlSchema xmlSchemaUBLCommonSignatureComponents_2_1 = new XmlSchema();
                        xmlSchemaUBLCommonSignatureComponents_2_1 = XmlSchema.Read(xtrSchemaUBLCommonSignatureComponents_2_1, null);

                        XmlTextReader xtrSchemaUBLCoreComponentParameters_2_1 = new XmlTextReader(new StringReader(global::eFacturesCat.Properties.Resources.UBL_CoreComponentParameters_2_1));
                        XmlSchema xmlSchemaUBLCoreComponentParameters_2_1 = new XmlSchema();
                        xmlSchemaUBLCoreComponentParameters_2_1 = XmlSchema.Read(xtrSchemaUBLCoreComponentParameters_2_1, null);

                        XmlTextReader xtrSchemaUBLExtensionContentDataType_2_1 = new XmlTextReader(new StringReader(global::eFacturesCat.Properties.Resources.UBL_ExtensionContentDataType_2_1));
                        XmlSchema xmlSchemaUBLExtensionContentDataType_2_1 = new XmlSchema();
                        xmlSchemaUBLExtensionContentDataType_2_1 = XmlSchema.Read(xtrSchemaUBLExtensionContentDataType_2_1, null);

                        XmlTextReader xtrSchemaUBLQualifiedDataTypes_2_1 = new XmlTextReader(new StringReader(global::eFacturesCat.Properties.Resources.UBL_QualifiedDataTypes_2_1));
                        XmlSchema xmlSchemaUBLQualifiedDataTypes_2_1 = new XmlSchema();
                        xmlSchemaUBLQualifiedDataTypes_2_1 = XmlSchema.Read(xtrSchemaUBLQualifiedDataTypes_2_1, null);

                        XmlTextReader xtrSchemaUBLSignatureAggregateComponents_2_1 = new XmlTextReader(new StringReader(global::eFacturesCat.Properties.Resources.UBL_SignatureAggregateComponents_2_1));
                        XmlSchema xmlSchemaUBLSignatureAggregateComponents_2_1 = new XmlSchema();
                        xmlSchemaUBLSignatureAggregateComponents_2_1 = XmlSchema.Read(xtrSchemaUBLSignatureAggregateComponents_2_1, null);

                        XmlTextReader xtrSchemaUBLSignatureBasicComponents_2_1 = new XmlTextReader(new StringReader(global::eFacturesCat.Properties.Resources.UBL_SignatureBasicComponents_2_1));
                        XmlSchema xmlSchemaUBLSignatureBasicComponents_2_1 = new XmlSchema();
                        xmlSchemaUBLSignatureBasicComponents_2_1 = XmlSchema.Read(xtrSchemaUBLSignatureBasicComponents_2_1, null);

                        XmlTextReader xtrSchemaUBLUnqualifiedDataTypes_2_1 = new XmlTextReader(new StringReader(global::eFacturesCat.Properties.Resources.UBL_UnqualifiedDataTypes_2_1));
                        XmlSchema xmlSchemaUBLUnqualifiedDataTypes_2_1 = new XmlSchema();
                        xmlSchemaUBLUnqualifiedDataTypes_2_1 = XmlSchema.Read(xtrSchemaUBLUnqualifiedDataTypes_2_1, null);

                        XmlTextReader xtrSchemaUBLXAdESv132_2_1 = new XmlTextReader(new StringReader(global::eFacturesCat.Properties.Resources.UBL_XAdESv132_2_1));
                        XmlSchema xmlSchemaUBLXAdESv132_2_1 = new XmlSchema();
                        xmlSchemaUBLXAdESv132_2_1 = XmlSchema.Read(xtrSchemaUBLXAdESv132_2_1, null);

                        XmlTextReader xtrSchemaUBLXAdESv141_2_1 = new XmlTextReader(new StringReader(global::eFacturesCat.Properties.Resources.UBL_XAdESv141_2_1));
                        XmlSchema xmlSchemaUBLXAdESv141_2_1 = new XmlSchema();
                        xmlSchemaUBLXAdESv141_2_1 = XmlSchema.Read(xtrSchemaUBLXAdESv141_2_1, null);

                        XmlTextReader xtrSchemaUBLxmldsig_core_schema_2_1 = new XmlTextReader(new StringReader(global::eFacturesCat.Properties.Resources.UBL_xmldsig_core_schema_2_1));
                        XmlSchema xmlSchemaUBLxmldsig_core_schema_2_1 = new XmlSchema();
                        xmlSchemaUBLxmldsig_core_schema_2_1 = XmlSchema.Read(xtrSchemaUBLxmldsig_core_schema_2_1, null);

                        XmlTextReader xtrSchemaUBLCCTS_CCT_SchemaModule_2_1 = new XmlTextReader(new StringReader(global::eFacturesCat.Properties.Resources.CCTS_CCT_SchemaModule_2_1));
                        XmlSchema xmlSchemaUBLCCTS_CCT_SchemaModule_2_1 = new XmlSchema();
                        xmlSchemaUBLCCTS_CCT_SchemaModule_2_1 = XmlSchema.Read(xtrSchemaUBLCCTS_CCT_SchemaModule_2_1, null);

                        XmlTextReader xtrSchemaUBLInvoice_2_1 = new XmlTextReader(new StringReader(global::eFacturesCat.Properties.Resources.UBL_Invoice_2_1));
                        XmlSchema xmlSchemaUBLInvoice_2_1 = new XmlSchema();
                        xmlSchemaUBLInvoice_2_1 = XmlSchema.Read(xtrSchemaUBLInvoice_2_1, null);

                        
                        xmlsettings.Schemas.Add(xmlSchemaUBLCCTS_CCT_SchemaModule_2_1);
                        xmlsettings.Schemas.Add(xmlSchemaUBLQualifiedDataTypes_2_1);
                        xmlsettings.Schemas.Add(xmlSchemaUBLUnqualifiedDataTypes_2_1);
                        xmlsettings.Schemas.Add(xmlSchemaUBLCommonBasicComponents_2_1);
                        xmlsettings.Schemas.Add(xmlSchemaUBLCommonAggregateComponents_2_1);
                        xmlsettings.Schemas.Add(xmlSchemaUBLCommonExtensionComponents_2_1);
                        /*xmlsettings.Schemas.Add(xmlSchemaUBLCommonSignatureComponents_2_1);
                        xmlsettings.Schemas.Add(xmlSchemaUBLCoreComponentParameters_2_1);
                        xmlsettings.Schemas.Add(xmlSchemaUBLExtensionContentDataType_2_1);                        
                        xmlsettings.Schemas.Add(xmlSchemaUBLSignatureAggregateComponents_2_1);
                        xmlsettings.Schemas.Add(xmlSchemaUBLSignatureBasicComponents_2_1);
                        xmlsettings.Schemas.Add(xmlSchemaUBLXAdESv132_2_1);
                        xmlsettings.Schemas.Add(xmlSchemaUBLXAdESv141_2_1);
                        xmlsettings.Schemas.Add(xmlSchemaUBLxmldsig_core_schema_2_1);*/
                        xmlsettings.Schemas.Add(xmlSchemaUBLInvoice_2_1);

                        xmlsettings.ValidationType = ValidationType.Schema;
                        XmlTextReader xtrXML = new XmlTextReader(new StringReader(this.ToString()));

                        XmlReader reader = XmlReader.Create(xtrXML, xmlsettings);
                        while (reader.Read()) { }

                        isValidXml = true;
                        xmlErrorStr = null;

                        //throw new Exception("UBL format not suported to Deserialize");
                    }break;
                    default:
                        throw new Exception("Unknown invoice format: " + this.invoiceType);
                }   
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
            switch (this.invoiceType)
            {
                case Constants.INVOICE_TYPE_FACTURAE:
                {
                    switch (this.invoiceVersion)
                    {
                        case Constants.FACTURAE_VERSION_3_2:
                            {
                                if (facturae32 != null)
                                {
                                    if (facturae32.Parties.BuyerParty.Item.GetType() == typeof(eFacturesCat.Transform.facturae32.LegalEntityType))
                                    {
                                        eFacturesCat.Transform.facturae32.LegalEntityType entity = (eFacturesCat.Transform.facturae32.LegalEntityType)facturae32.Parties.BuyerParty.Item;
                                        if (entity.ContactDetails != null)
                                            if (entity.ContactDetails.ElectronicMail != null)
                                                return entity.ContactDetails.ElectronicMail;
                                    }
                                    else
                                    {
                                        eFacturesCat.Transform.facturae32.IndividualType entity = (eFacturesCat.Transform.facturae32.IndividualType)facturae32.Parties.BuyerParty.Item;
                                        if (entity.ContactDetails != null)
                                            if (entity.ContactDetails.ElectronicMail != null)
                                                return entity.ContactDetails.ElectronicMail;
                                    }

                                }
                            } break;
                        case Constants.FACTURAE_VERSION_3_2_1:
                            {
                                if (facturae321 != null)
                                {
                                    if (facturae321.Parties.BuyerParty.Item.GetType() == typeof(eFacturesCat.Transform.facturae321.LegalEntityType))
                                    {
                                        eFacturesCat.Transform.facturae321.LegalEntityType entity = (eFacturesCat.Transform.facturae321.LegalEntityType)facturae321.Parties.BuyerParty.Item;
                                        if (entity.ContactDetails != null)
                                            if (entity.ContactDetails.ElectronicMail != null)
                                                return entity.ContactDetails.ElectronicMail;
                                    }
                                    else
                                    {
                                        eFacturesCat.Transform.facturae321.IndividualType entity = (eFacturesCat.Transform.facturae321.IndividualType)facturae321.Parties.BuyerParty.Item;
                                        if (entity.ContactDetails != null)
                                            if (entity.ContactDetails.ElectronicMail != null)
                                                return entity.ContactDetails.ElectronicMail;
                                    }

                                }
                            } break;
                        case Constants.FACTURAE_VERSION_3_2_2:
                            {
                                if (facturae322 != null)
                                {
                                    if (facturae322.Parties.BuyerParty.Item.GetType() == typeof(eFacturesCat.Transform.facturae322.LegalEntityType))
                                    {
                                        eFacturesCat.Transform.facturae322.LegalEntityType entity = (eFacturesCat.Transform.facturae322.LegalEntityType)facturae322.Parties.BuyerParty.Item;
                                        if (entity.ContactDetails != null)
                                            if (entity.ContactDetails.ElectronicMail != null)
                                                return entity.ContactDetails.ElectronicMail;
                                    }
                                    else
                                    {
                                        eFacturesCat.Transform.facturae322.IndividualType entity = (eFacturesCat.Transform.facturae322.IndividualType)facturae322.Parties.BuyerParty.Item;
                                        if (entity.ContactDetails != null)
                                            if (entity.ContactDetails.ElectronicMail != null)
                                                return entity.ContactDetails.ElectronicMail;
                                    }

                                }
                            } break;
                        default:
                            throw new Exception("Unknown Facturae version: " + this.invoiceVersion);
                    }
                }break;
                case Constants.INVOICE_TYPE_UBL: {
                    throw new Exception("UBL format not suported");
                }
                default:
                    throw new Exception("Unknown invoice format: " + this.invoiceType);
            }
            return null;
        }
    }
}
