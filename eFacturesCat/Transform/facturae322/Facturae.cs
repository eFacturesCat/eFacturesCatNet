namespace eFacturesCat.Transform.facturae322
{
    using System;
    using System.Diagnostics;
    using System.Xml.Serialization;
    using System.Collections;
    using System.Xml.Schema;
    using System.ComponentModel;
    using System.IO;
    using System.Text;
    using System.Collections.Generic;

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml", IsNullable = false)]
    public partial class Facturae
    {

        private FileHeaderType fileHeaderField;

        private PartiesType partiesField;

        private List<InvoiceType> invoicesField;

        private ExtensionsType extensionsField;

        private SignatureType signatureField;

        private static System.Xml.Serialization.XmlSerializer serializer;

        public Facturae()
        {
            this.extensionsField = new ExtensionsType();
            this.invoicesField = new List<InvoiceType>();
            this.partiesField = new PartiesType();
            this.fileHeaderField = new FileHeaderType();
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public FileHeaderType FileHeader
        {
            get
            {
                return this.fileHeaderField;
            }
            set
            {
                this.fileHeaderField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public PartiesType Parties
        {
            get
            {
                return this.partiesField;
            }
            set
            {
                this.partiesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("Invoice", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
        public List<InvoiceType> Invoices
        {
            get
            {
                return this.invoicesField;
            }
            set
            {
                this.invoicesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public ExtensionsType Extensions
        {
            get
            {
                return this.extensionsField;
            }
            set
            {
                this.extensionsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
        public SignatureType Signature
        {
            get
            {
                return this.signatureField;
            }
            set
            {
                this.signatureField = value;
            }
        }

        private static System.Xml.Serialization.XmlSerializer Serializer
        {
            get
            {
                if ((serializer == null))
                {
                    serializer = new System.Xml.Serialization.XmlSerializer(typeof(Facturae));
                }
                return serializer;
            }
        }

        #region Serialize/Deserialize
        /// <summary>
        /// Serializes current Facturae object into an XML document
        /// </summary>
        /// <returns>string XML value</returns>
        public virtual string Serialize()
        {
            System.IO.StreamReader streamReader = null;
            System.IO.MemoryStream memoryStream = null;
            try
            {
                memoryStream = new System.IO.MemoryStream();
                Serializer.Serialize(memoryStream, this);
                memoryStream.Seek(0, System.IO.SeekOrigin.Begin);
                streamReader = new System.IO.StreamReader(memoryStream);
                return streamReader.ReadToEnd();
            }
            finally
            {
                if ((streamReader != null))
                {
                    streamReader.Dispose();
                }
                if ((memoryStream != null))
                {
                    memoryStream.Dispose();
                }
            }
        }

        /// <summary>
        /// Deserializes workflow markup into an Facturae object
        /// </summary>
        /// <param name="xml">string workflow markup to deserialize</param>
        /// <param name="obj">Output Facturae object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        public static bool Deserialize(string xml, out Facturae obj, out System.Exception exception)
        {
            exception = null;
            obj = default(Facturae);
            try
            {
                obj = Deserialize(xml);
                return true;
            }
            catch (System.Exception ex)
            {
                exception = ex;
                return false;
            }
        }

        public static bool Deserialize(string xml, out Facturae obj)
        {
            System.Exception exception = null;
            return Deserialize(xml, out obj, out exception);
        }

        public static Facturae Deserialize(string xml)
        {
            System.IO.StringReader stringReader = null;
            try
            {
                stringReader = new System.IO.StringReader(xml);
                return ((Facturae)(Serializer.Deserialize(System.Xml.XmlReader.Create(stringReader))));
            }
            finally
            {
                if ((stringReader != null))
                {
                    stringReader.Dispose();
                }
            }
        }

        /// <summary>
        /// Serializes current Facturae object into file
        /// </summary>
        /// <param name="fileName">full path of outupt xml file</param>
        /// <param name="exception">output Exception value if failed</param>
        /// <returns>true if can serialize and save into file; otherwise, false</returns>
        public virtual bool SaveToFile(string fileName, out System.Exception exception)
        {
            exception = null;
            try
            {
                SaveToFile(fileName);
                return true;
            }
            catch (System.Exception e)
            {
                exception = e;
                return false;
            }
        }

        public virtual void SaveToFile(string fileName)
        {
            System.IO.StreamWriter streamWriter = null;
            try
            {
                string xmlString = Serialize();
                System.IO.FileInfo xmlFile = new System.IO.FileInfo(fileName);
                streamWriter = xmlFile.CreateText();
                streamWriter.WriteLine(xmlString);
                streamWriter.Close();
            }
            finally
            {
                if ((streamWriter != null))
                {
                    streamWriter.Dispose();
                }
            }
        }

        /// <summary>
        /// Deserializes xml markup from file into an Facturae object
        /// </summary>
        /// <param name="fileName">string xml file to load and deserialize</param>
        /// <param name="obj">Output Facturae object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        public static bool LoadFromFile(string fileName, out Facturae obj, out System.Exception exception)
        {
            exception = null;
            obj = default(Facturae);
            try
            {
                obj = LoadFromFile(fileName);
                return true;
            }
            catch (System.Exception ex)
            {
                exception = ex;
                return false;
            }
        }

        public static bool LoadFromFile(string fileName, out Facturae obj)
        {
            System.Exception exception = null;
            return LoadFromFile(fileName, out obj, out exception);
        }

        public static Facturae LoadFromFile(string fileName)
        {
            System.IO.FileStream file = null;
            System.IO.StreamReader sr = null;
            try
            {
                file = new System.IO.FileStream(fileName, FileMode.Open, FileAccess.Read);
                sr = new System.IO.StreamReader(file);
                string xmlString = sr.ReadToEnd();
                sr.Close();
                file.Close();
                return Deserialize(xmlString);
            }
            finally
            {
                if ((file != null))
                {
                    file.Dispose();
                }
                if ((sr != null))
                {
                    sr.Dispose();
                }
            }
        }
        #endregion
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public partial class FileHeaderType
    {

        private SchemaVersionType schemaVersionField;

        private ModalityType modalityField;

        private InvoiceIssuerTypeType invoiceIssuerTypeField;

        private ThirdPartyType thirdPartyField;

        private BatchType batchField;

        private FactoringAssignmentDataType factoringAssignmentDataField;

        public FileHeaderType()
        {
            this.schemaVersionField = SchemaVersionType.Item322;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public SchemaVersionType SchemaVersion
        {
            get
            {
                return this.schemaVersionField;
            }
            set
            {
                this.schemaVersionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public ModalityType Modality
        {
            get
            {
                return this.modalityField;
            }
            set
            {
                this.modalityField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public InvoiceIssuerTypeType InvoiceIssuerType
        {
            get
            {
                return this.invoiceIssuerTypeField;
            }
            set
            {
                this.invoiceIssuerTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public ThirdPartyType ThirdParty
        {
            get
            {
                return this.thirdPartyField;
            }
            set
            {
                this.thirdPartyField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public BatchType Batch
        {
            get
            {
                return this.batchField;
            }
            set
            {
                this.batchField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public FactoringAssignmentDataType FactoringAssignmentData
        {
            get
            {
                return this.factoringAssignmentDataField;
            }
            set
            {
                this.factoringAssignmentDataField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public enum SchemaVersionType
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("3.2.2")]
        Item322,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public enum ModalityType
    {

        /// <remarks/>
        I,

        /// <remarks/>
        L,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public enum InvoiceIssuerTypeType
    {

        /// <remarks/>
        EM,

        /// <remarks/>
        RE,

        /// <remarks/>
        TE,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public partial class ThirdPartyType
    {

        private TaxIdentificationType taxIdentificationField;

        private object itemField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public TaxIdentificationType TaxIdentification
        {
            get
            {
                return this.taxIdentificationField;
            }
            set
            {
                this.taxIdentificationField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Individual", typeof(IndividualType), Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlElementAttribute("LegalEntity", typeof(LegalEntityType), Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public object Item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public partial class TaxIdentificationType
    {

        private PersonTypeCodeType personTypeCodeField;

        private ResidenceTypeCodeType residenceTypeCodeField;

        private string taxIdentificationNumberField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public PersonTypeCodeType PersonTypeCode
        {
            get
            {
                return this.personTypeCodeField;
            }
            set
            {
                this.personTypeCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public ResidenceTypeCodeType ResidenceTypeCode
        {
            get
            {
                return this.residenceTypeCodeField;
            }
            set
            {
                this.residenceTypeCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string TaxIdentificationNumber
        {
            get
            {
                return this.taxIdentificationNumberField;
            }
            set
            {
                this.taxIdentificationNumberField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public enum PersonTypeCodeType
    {

        /// <remarks/>
        F,

        /// <remarks/>
        J,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public enum ResidenceTypeCodeType
    {

        /// <remarks/>
        E,

        /// <remarks/>
        R,

        /// <remarks/>
        U,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    [System.Xml.Serialization.XmlRootAttribute("Object", Namespace = "http://www.w3.org/2000/09/xmldsig#", IsNullable = false)]
    public partial class ObjectType
    {

        private System.Xml.XmlNode[] anyField;

        private string idField;

        private string mimeTypeField;

        private string encodingField;

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        public System.Xml.XmlNode[] Any
        {
            get
            {
                return this.anyField;
            }
            set
            {
                this.anyField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "ID")]
        public string Id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string MimeType
        {
            get
            {
                return this.mimeTypeField;
            }
            set
            {
                this.mimeTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "anyURI")]
        public string Encoding
        {
            get
            {
                return this.encodingField;
            }
            set
            {
                this.encodingField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    [System.Xml.Serialization.XmlRootAttribute("SPKIData", Namespace = "http://www.w3.org/2000/09/xmldsig#", IsNullable = false)]
    public partial class SPKIDataType
    {

        private byte[][] sPKISexpField;

        private System.Xml.XmlElement anyField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("SPKISexp", DataType = "base64Binary")]
        public byte[][] SPKISexp
        {
            get
            {
                return this.sPKISexpField;
            }
            set
            {
                this.sPKISexpField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        public System.Xml.XmlElement Any
        {
            get
            {
                return this.anyField;
            }
            set
            {
                this.anyField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    [System.Xml.Serialization.XmlRootAttribute("PGPData", Namespace = "http://www.w3.org/2000/09/xmldsig#", IsNullable = false)]
    public partial class PGPDataType
    {

        private object[] itemsField;

        private ItemsChoiceType1[] itemsElementNameField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        [System.Xml.Serialization.XmlElementAttribute("PGPKeyID", typeof(byte[]), DataType = "base64Binary")]
        [System.Xml.Serialization.XmlElementAttribute("PGPKeyPacket", typeof(byte[]), DataType = "base64Binary")]
        [System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")]
        public object[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ItemsElementName")]
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public ItemsChoiceType1[] ItemsElementName
        {
            get
            {
                return this.itemsElementNameField;
            }
            set
            {
                this.itemsElementNameField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.w3.org/2000/09/xmldsig#", IncludeInSchema = false)]
    public enum ItemsChoiceType1
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("##any:")]
        Item,

        /// <remarks/>
        PGPKeyID,

        /// <remarks/>
        PGPKeyPacket,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    public partial class X509IssuerSerialType
    {

        private string x509IssuerNameField;

        private string x509SerialNumberField;

        /// <remarks/>
        public string X509IssuerName
        {
            get
            {
                return this.x509IssuerNameField;
            }
            set
            {
                this.x509IssuerNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
        public string X509SerialNumber
        {
            get
            {
                return this.x509SerialNumberField;
            }
            set
            {
                this.x509SerialNumberField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    [System.Xml.Serialization.XmlRootAttribute("X509Data", Namespace = "http://www.w3.org/2000/09/xmldsig#", IsNullable = false)]
    public partial class X509DataType
    {

        private object[] itemsField;

        private ItemsChoiceType[] itemsElementNameField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        [System.Xml.Serialization.XmlElementAttribute("X509CRL", typeof(byte[]), DataType = "base64Binary")]
        [System.Xml.Serialization.XmlElementAttribute("X509Certificate", typeof(byte[]), DataType = "base64Binary")]
        [System.Xml.Serialization.XmlElementAttribute("X509IssuerSerial", typeof(X509IssuerSerialType))]
        [System.Xml.Serialization.XmlElementAttribute("X509SKI", typeof(byte[]), DataType = "base64Binary")]
        [System.Xml.Serialization.XmlElementAttribute("X509SubjectName", typeof(string))]
        [System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")]
        public object[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ItemsElementName")]
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public ItemsChoiceType[] ItemsElementName
        {
            get
            {
                return this.itemsElementNameField;
            }
            set
            {
                this.itemsElementNameField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.w3.org/2000/09/xmldsig#", IncludeInSchema = false)]
    public enum ItemsChoiceType
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("##any:")]
        Item,

        /// <remarks/>
        X509CRL,

        /// <remarks/>
        X509Certificate,

        /// <remarks/>
        X509IssuerSerial,

        /// <remarks/>
        X509SKI,

        /// <remarks/>
        X509SubjectName,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    [System.Xml.Serialization.XmlRootAttribute("RetrievalMethod", Namespace = "http://www.w3.org/2000/09/xmldsig#", IsNullable = false)]
    public partial class RetrievalMethodType
    {

        private TransformType[] transformsField;

        private string uRIField;

        private string typeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Transform", IsNullable = false)]
        public TransformType[] Transforms
        {
            get
            {
                return this.transformsField;
            }
            set
            {
                this.transformsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "anyURI")]
        public string URI
        {
            get
            {
                return this.uRIField;
            }
            set
            {
                this.uRIField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "anyURI")]
        public string Type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    [System.Xml.Serialization.XmlRootAttribute("Transform", Namespace = "http://www.w3.org/2000/09/xmldsig#", IsNullable = false)]
    public partial class TransformType
    {

        private object[] itemsField;

        private string[] textField;

        private string algorithmField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        [System.Xml.Serialization.XmlElementAttribute("XPath", typeof(string))]
        public object[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string[] Text
        {
            get
            {
                return this.textField;
            }
            set
            {
                this.textField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "anyURI")]
        public string Algorithm
        {
            get
            {
                return this.algorithmField;
            }
            set
            {
                this.algorithmField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    [System.Xml.Serialization.XmlRootAttribute("RSAKeyValue", Namespace = "http://www.w3.org/2000/09/xmldsig#", IsNullable = false)]
    public partial class RSAKeyValueType
    {

        private byte[] modulusField;

        private byte[] exponentField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "base64Binary")]
        public byte[] Modulus
        {
            get
            {
                return this.modulusField;
            }
            set
            {
                this.modulusField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "base64Binary")]
        public byte[] Exponent
        {
            get
            {
                return this.exponentField;
            }
            set
            {
                this.exponentField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    [System.Xml.Serialization.XmlRootAttribute("DSAKeyValue", Namespace = "http://www.w3.org/2000/09/xmldsig#", IsNullable = false)]
    public partial class DSAKeyValueType
    {

        private byte[] pField;

        private byte[] qField;

        private byte[] gField;

        private byte[] yField;

        private byte[] jField;

        private byte[] seedField;

        private byte[] pgenCounterField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "base64Binary")]
        public byte[] P
        {
            get
            {
                return this.pField;
            }
            set
            {
                this.pField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "base64Binary")]
        public byte[] Q
        {
            get
            {
                return this.qField;
            }
            set
            {
                this.qField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "base64Binary")]
        public byte[] G
        {
            get
            {
                return this.gField;
            }
            set
            {
                this.gField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "base64Binary")]
        public byte[] Y
        {
            get
            {
                return this.yField;
            }
            set
            {
                this.yField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "base64Binary")]
        public byte[] J
        {
            get
            {
                return this.jField;
            }
            set
            {
                this.jField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "base64Binary")]
        public byte[] Seed
        {
            get
            {
                return this.seedField;
            }
            set
            {
                this.seedField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "base64Binary")]
        public byte[] PgenCounter
        {
            get
            {
                return this.pgenCounterField;
            }
            set
            {
                this.pgenCounterField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    [System.Xml.Serialization.XmlRootAttribute("KeyValue", Namespace = "http://www.w3.org/2000/09/xmldsig#", IsNullable = false)]
    public partial class KeyValueType
    {

        private object itemField;

        private string[] textField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        [System.Xml.Serialization.XmlElementAttribute("DSAKeyValue", typeof(DSAKeyValueType))]
        [System.Xml.Serialization.XmlElementAttribute("RSAKeyValue", typeof(RSAKeyValueType))]
        public object Item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string[] Text
        {
            get
            {
                return this.textField;
            }
            set
            {
                this.textField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    [System.Xml.Serialization.XmlRootAttribute("KeyInfo", Namespace = "http://www.w3.org/2000/09/xmldsig#", IsNullable = false)]
    public partial class KeyInfoType
    {

        private object[] itemsField;

        private ItemsChoiceType2[] itemsElementNameField;

        private string[] textField;

        private string idField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        [System.Xml.Serialization.XmlElementAttribute("KeyName", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("KeyValue", typeof(KeyValueType))]
        [System.Xml.Serialization.XmlElementAttribute("MgmtData", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("PGPData", typeof(PGPDataType))]
        [System.Xml.Serialization.XmlElementAttribute("RetrievalMethod", typeof(RetrievalMethodType))]
        [System.Xml.Serialization.XmlElementAttribute("SPKIData", typeof(SPKIDataType))]
        [System.Xml.Serialization.XmlElementAttribute("X509Data", typeof(X509DataType))]
        [System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")]
        public object[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ItemsElementName")]
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public ItemsChoiceType2[] ItemsElementName
        {
            get
            {
                return this.itemsElementNameField;
            }
            set
            {
                this.itemsElementNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string[] Text
        {
            get
            {
                return this.textField;
            }
            set
            {
                this.textField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "ID")]
        public string Id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.w3.org/2000/09/xmldsig#", IncludeInSchema = false)]
    public enum ItemsChoiceType2
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("##any:")]
        Item,

        /// <remarks/>
        KeyName,

        /// <remarks/>
        KeyValue,

        /// <remarks/>
        MgmtData,

        /// <remarks/>
        PGPData,

        /// <remarks/>
        RetrievalMethod,

        /// <remarks/>
        SPKIData,

        /// <remarks/>
        X509Data,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    [System.Xml.Serialization.XmlRootAttribute("SignatureValue", Namespace = "http://www.w3.org/2000/09/xmldsig#", IsNullable = false)]
    public partial class SignatureValueType
    {

        private string idField;

        private byte[] valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "ID")]
        public string Id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute(DataType = "base64Binary")]
        public byte[] Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    [System.Xml.Serialization.XmlRootAttribute("DigestMethod", Namespace = "http://www.w3.org/2000/09/xmldsig#", IsNullable = false)]
    public partial class DigestMethodType
    {

        private System.Xml.XmlNode[] anyField;

        private string algorithmField;

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        public System.Xml.XmlNode[] Any
        {
            get
            {
                return this.anyField;
            }
            set
            {
                this.anyField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "anyURI")]
        public string Algorithm
        {
            get
            {
                return this.algorithmField;
            }
            set
            {
                this.algorithmField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    [System.Xml.Serialization.XmlRootAttribute("Reference", Namespace = "http://www.w3.org/2000/09/xmldsig#", IsNullable = false)]
    public partial class ReferenceType
    {

        private TransformType[] transformsField;

        private DigestMethodType digestMethodField;

        private byte[] digestValueField;

        private string idField;

        private string uRIField;

        private string typeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Transform", IsNullable = false)]
        public TransformType[] Transforms
        {
            get
            {
                return this.transformsField;
            }
            set
            {
                this.transformsField = value;
            }
        }

        /// <remarks/>
        public DigestMethodType DigestMethod
        {
            get
            {
                return this.digestMethodField;
            }
            set
            {
                this.digestMethodField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "base64Binary")]
        public byte[] DigestValue
        {
            get
            {
                return this.digestValueField;
            }
            set
            {
                this.digestValueField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "ID")]
        public string Id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "anyURI")]
        public string URI
        {
            get
            {
                return this.uRIField;
            }
            set
            {
                this.uRIField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "anyURI")]
        public string Type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    [System.Xml.Serialization.XmlRootAttribute("SignatureMethod", Namespace = "http://www.w3.org/2000/09/xmldsig#", IsNullable = false)]
    public partial class SignatureMethodType
    {

        private string hMACOutputLengthField;

        private System.Xml.XmlNode[] anyField;

        private string algorithmField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
        public string HMACOutputLength
        {
            get
            {
                return this.hMACOutputLengthField;
            }
            set
            {
                this.hMACOutputLengthField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        public System.Xml.XmlNode[] Any
        {
            get
            {
                return this.anyField;
            }
            set
            {
                this.anyField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "anyURI")]
        public string Algorithm
        {
            get
            {
                return this.algorithmField;
            }
            set
            {
                this.algorithmField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    [System.Xml.Serialization.XmlRootAttribute("CanonicalizationMethod", Namespace = "http://www.w3.org/2000/09/xmldsig#", IsNullable = false)]
    public partial class CanonicalizationMethodType
    {

        private System.Xml.XmlNode[] anyField;

        private string algorithmField;

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        public System.Xml.XmlNode[] Any
        {
            get
            {
                return this.anyField;
            }
            set
            {
                this.anyField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "anyURI")]
        public string Algorithm
        {
            get
            {
                return this.algorithmField;
            }
            set
            {
                this.algorithmField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    [System.Xml.Serialization.XmlRootAttribute("SignedInfo", Namespace = "http://www.w3.org/2000/09/xmldsig#", IsNullable = false)]
    public partial class SignedInfoType
    {

        private CanonicalizationMethodType canonicalizationMethodField;

        private SignatureMethodType signatureMethodField;

        private ReferenceType[] referenceField;

        private string idField;

        /// <remarks/>
        public CanonicalizationMethodType CanonicalizationMethod
        {
            get
            {
                return this.canonicalizationMethodField;
            }
            set
            {
                this.canonicalizationMethodField = value;
            }
        }

        /// <remarks/>
        public SignatureMethodType SignatureMethod
        {
            get
            {
                return this.signatureMethodField;
            }
            set
            {
                this.signatureMethodField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Reference")]
        public ReferenceType[] Reference
        {
            get
            {
                return this.referenceField;
            }
            set
            {
                this.referenceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "ID")]
        public string Id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    [System.Xml.Serialization.XmlRootAttribute("Signature", Namespace = "http://www.w3.org/2000/09/xmldsig#", IsNullable = false)]
    public partial class SignatureType
    {

        private SignedInfoType signedInfoField;

        private SignatureValueType signatureValueField;

        private KeyInfoType keyInfoField;

        private ObjectType[] objectField;

        private string idField;

        /// <remarks/>
        public SignedInfoType SignedInfo
        {
            get
            {
                return this.signedInfoField;
            }
            set
            {
                this.signedInfoField = value;
            }
        }

        /// <remarks/>
        public SignatureValueType SignatureValue
        {
            get
            {
                return this.signatureValueField;
            }
            set
            {
                this.signatureValueField = value;
            }
        }

        /// <remarks/>
        public KeyInfoType KeyInfo
        {
            get
            {
                return this.keyInfoField;
            }
            set
            {
                this.keyInfoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Object")]
        public ObjectType[] Object
        {
            get
            {
                return this.objectField;
            }
            set
            {
                this.objectField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "ID")]
        public string Id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public partial class AttachmentType
    {

        private AttachmentCompressionAlgorithmType attachmentCompressionAlgorithmField;

        private bool attachmentCompressionAlgorithmFieldSpecified;

        private AttachmentFormatType attachmentFormatField;

        private AttachmentEncodingType attachmentEncodingField;

        private bool attachmentEncodingFieldSpecified;

        private string attachmentDescriptionField;

        private string attachmentDataField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public AttachmentCompressionAlgorithmType AttachmentCompressionAlgorithm
        {
            get
            {
                return this.attachmentCompressionAlgorithmField;
            }
            set
            {
                this.attachmentCompressionAlgorithmField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool AttachmentCompressionAlgorithmSpecified
        {
            get
            {
                return this.attachmentCompressionAlgorithmFieldSpecified;
            }
            set
            {
                this.attachmentCompressionAlgorithmFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public AttachmentFormatType AttachmentFormat
        {
            get
            {
                return this.attachmentFormatField;
            }
            set
            {
                this.attachmentFormatField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public AttachmentEncodingType AttachmentEncoding
        {
            get
            {
                return this.attachmentEncodingField;
            }
            set
            {
                this.attachmentEncodingField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool AttachmentEncodingSpecified
        {
            get
            {
                return this.attachmentEncodingFieldSpecified;
            }
            set
            {
                this.attachmentEncodingFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string AttachmentDescription
        {
            get
            {
                return this.attachmentDescriptionField;
            }
            set
            {
                this.attachmentDescriptionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string AttachmentData
        {
            get
            {
                return this.attachmentDataField;
            }
            set
            {
                this.attachmentDataField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public enum AttachmentCompressionAlgorithmType
    {

        /// <remarks/>
        ZIP,

        /// <remarks/>
        GZIP,

        /// <remarks/>
        NONE,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public enum AttachmentFormatType
    {

        /// <remarks/>
        xml,

        /// <remarks/>
        doc,

        /// <remarks/>
        gif,

        /// <remarks/>
        rtf,

        /// <remarks/>
        pdf,

        /// <remarks/>
        xls,

        /// <remarks/>
        jpg,

        /// <remarks/>
        bmp,

        /// <remarks/>
        tiff,

        /// <remarks/>
        html,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public enum AttachmentEncodingType
    {

        /// <remarks/>
        BASE64,

        /// <remarks/>
        BER,

        /// <remarks/>
        DER,

        /// <remarks/>
        NONE,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public partial class AdditionalDataType
    {

        private string relatedInvoiceField;

        private AttachmentType[] relatedDocumentsField;

        private string invoiceAdditionalInformationField;

        private ExtensionsType extensionsField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string RelatedInvoice
        {
            get
            {
                return this.relatedInvoiceField;
            }
            set
            {
                this.relatedInvoiceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("Attachment", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
        public AttachmentType[] RelatedDocuments
        {
            get
            {
                return this.relatedDocumentsField;
            }
            set
            {
                this.relatedDocumentsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string InvoiceAdditionalInformation
        {
            get
            {
                return this.invoiceAdditionalInformationField;
            }
            set
            {
                this.invoiceAdditionalInformationField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public ExtensionsType Extensions
        {
            get
            {
                return this.extensionsField;
            }
            set
            {
                this.extensionsField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public partial class ExtensionsType
    {

        private System.Xml.XmlElement[] anyField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        public System.Xml.XmlElement[] Any
        {
            get
            {
                return this.anyField;
            }
            set
            {
                this.anyField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public partial class SpecialTaxableEventType
    {

        private SpecialTaxableEventCodeType specialTaxableEventCodeField;

        private string specialTaxableEventReasonField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public SpecialTaxableEventCodeType SpecialTaxableEventCode
        {
            get
            {
                return this.specialTaxableEventCodeField;
            }
            set
            {
                this.specialTaxableEventCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string SpecialTaxableEventReason
        {
            get
            {
                return this.specialTaxableEventReasonField;
            }
            set
            {
                this.specialTaxableEventReasonField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public enum SpecialTaxableEventCodeType
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("01")]
        Item01,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("02")]
        Item02,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public partial class DeliveryNoteType
    {

        private string deliveryNoteNumberField;

        private System.DateTime deliveryNoteDateField;

        private bool deliveryNoteDateFieldSpecified;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string DeliveryNoteNumber
        {
            get
            {
                return this.deliveryNoteNumberField;
            }
            set
            {
                this.deliveryNoteNumberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date")]
        public System.DateTime DeliveryNoteDate
        {
            get
            {
                return this.deliveryNoteDateField;
            }
            set
            {
                this.deliveryNoteDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool DeliveryNoteDateSpecified
        {
            get
            {
                return this.deliveryNoteDateFieldSpecified;
            }
            set
            {
                this.deliveryNoteDateFieldSpecified = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public partial class InvoiceLineType
    {

        private string issuerContractReferenceField;

        private System.DateTime issuerContractDateField;

        private bool issuerContractDateFieldSpecified;

        private string issuerTransactionReferenceField;

        private System.DateTime issuerTransactionDateField;

        private bool issuerTransactionDateFieldSpecified;

        private string receiverContractReferenceField;

        private System.DateTime receiverContractDateField;

        private bool receiverContractDateFieldSpecified;

        private string receiverTransactionReferenceField;

        private System.DateTime receiverTransactionDateField;

        private bool receiverTransactionDateFieldSpecified;

        private string fileReferenceField;

        private System.DateTime fileDateField;

        private bool fileDateFieldSpecified;

        private double sequenceNumberField;

        private bool sequenceNumberFieldSpecified;

        private DeliveryNoteType[] deliveryNotesReferencesField;

        private string itemDescriptionField;

        private double quantityField;

        private UnitOfMeasureType unitOfMeasureField;

        private bool unitOfMeasureFieldSpecified;

        private double unitPriceWithoutTaxField;

        private double totalCostField;

        private DiscountType[] discountsAndRebatesField;

        private ChargeType[] chargesField;

        private double grossAmountField;

        private TaxType[] taxesWithheldField;

        private InvoiceLineTypeTax[] taxesOutputsField;

        private PeriodDates lineItemPeriodField;

        private System.DateTime transactionDateField;

        private bool transactionDateFieldSpecified;

        private string additionalLineItemInformationField;

        private SpecialTaxableEventType specialTaxableEventField;

        private string articleCodeField;

        private ExtensionsType extensionsField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string IssuerContractReference
        {
            get
            {
                return this.issuerContractReferenceField;
            }
            set
            {
                this.issuerContractReferenceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date")]
        public System.DateTime IssuerContractDate
        {
            get
            {
                return this.issuerContractDateField;
            }
            set
            {
                this.issuerContractDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool IssuerContractDateSpecified
        {
            get
            {
                return this.issuerContractDateFieldSpecified;
            }
            set
            {
                this.issuerContractDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string IssuerTransactionReference
        {
            get
            {
                return this.issuerTransactionReferenceField;
            }
            set
            {
                this.issuerTransactionReferenceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date")]
        public System.DateTime IssuerTransactionDate
        {
            get
            {
                return this.issuerTransactionDateField;
            }
            set
            {
                this.issuerTransactionDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool IssuerTransactionDateSpecified
        {
            get
            {
                return this.issuerTransactionDateFieldSpecified;
            }
            set
            {
                this.issuerTransactionDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ReceiverContractReference
        {
            get
            {
                return this.receiverContractReferenceField;
            }
            set
            {
                this.receiverContractReferenceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date")]
        public System.DateTime ReceiverContractDate
        {
            get
            {
                return this.receiverContractDateField;
            }
            set
            {
                this.receiverContractDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ReceiverContractDateSpecified
        {
            get
            {
                return this.receiverContractDateFieldSpecified;
            }
            set
            {
                this.receiverContractDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ReceiverTransactionReference
        {
            get
            {
                return this.receiverTransactionReferenceField;
            }
            set
            {
                this.receiverTransactionReferenceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date")]
        public System.DateTime ReceiverTransactionDate
        {
            get
            {
                return this.receiverTransactionDateField;
            }
            set
            {
                this.receiverTransactionDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ReceiverTransactionDateSpecified
        {
            get
            {
                return this.receiverTransactionDateFieldSpecified;
            }
            set
            {
                this.receiverTransactionDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string FileReference
        {
            get
            {
                return this.fileReferenceField;
            }
            set
            {
                this.fileReferenceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date")]
        public System.DateTime FileDate
        {
            get
            {
                return this.fileDateField;
            }
            set
            {
                this.fileDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool FileDateSpecified
        {
            get
            {
                return this.fileDateFieldSpecified;
            }
            set
            {
                this.fileDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public double SequenceNumber
        {
            get
            {
                return this.sequenceNumberField;
            }
            set
            {
                this.sequenceNumberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool SequenceNumberSpecified
        {
            get
            {
                return this.sequenceNumberFieldSpecified;
            }
            set
            {
                this.sequenceNumberFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("DeliveryNote", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
        public DeliveryNoteType[] DeliveryNotesReferences
        {
            get
            {
                return this.deliveryNotesReferencesField;
            }
            set
            {
                this.deliveryNotesReferencesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ItemDescription
        {
            get
            {
                return this.itemDescriptionField;
            }
            set
            {
                this.itemDescriptionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public double Quantity
        {
            get
            {
                return this.quantityField;
            }
            set
            {
                this.quantityField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public UnitOfMeasureType UnitOfMeasure
        {
            get
            {
                return this.unitOfMeasureField;
            }
            set
            {
                this.unitOfMeasureField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool UnitOfMeasureSpecified
        {
            get
            {
                return this.unitOfMeasureFieldSpecified;
            }
            set
            {
                this.unitOfMeasureFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public double UnitPriceWithoutTax
        {
            get
            {
                return this.unitPriceWithoutTaxField;
            }
            set
            {
                this.unitPriceWithoutTaxField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public double TotalCost
        {
            get
            {
                return this.totalCostField;
            }
            set
            {
                this.totalCostField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("Discount", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
        public DiscountType[] DiscountsAndRebates
        {
            get
            {
                return this.discountsAndRebatesField;
            }
            set
            {
                this.discountsAndRebatesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("Charge", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
        public ChargeType[] Charges
        {
            get
            {
                return this.chargesField;
            }
            set
            {
                this.chargesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public double GrossAmount
        {
            get
            {
                return this.grossAmountField;
            }
            set
            {
                this.grossAmountField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("Tax", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
        public TaxType[] TaxesWithheld
        {
            get
            {
                return this.taxesWithheldField;
            }
            set
            {
                this.taxesWithheldField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("Tax", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
        public InvoiceLineTypeTax[] TaxesOutputs
        {
            get
            {
                return this.taxesOutputsField;
            }
            set
            {
                this.taxesOutputsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public PeriodDates LineItemPeriod
        {
            get
            {
                return this.lineItemPeriodField;
            }
            set
            {
                this.lineItemPeriodField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date")]
        public System.DateTime TransactionDate
        {
            get
            {
                return this.transactionDateField;
            }
            set
            {
                this.transactionDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool TransactionDateSpecified
        {
            get
            {
                return this.transactionDateFieldSpecified;
            }
            set
            {
                this.transactionDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string AdditionalLineItemInformation
        {
            get
            {
                return this.additionalLineItemInformationField;
            }
            set
            {
                this.additionalLineItemInformationField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public SpecialTaxableEventType SpecialTaxableEvent
        {
            get
            {
                return this.specialTaxableEventField;
            }
            set
            {
                this.specialTaxableEventField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ArticleCode
        {
            get
            {
                return this.articleCodeField;
            }
            set
            {
                this.articleCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public ExtensionsType Extensions
        {
            get
            {
                return this.extensionsField;
            }
            set
            {
                this.extensionsField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public enum UnitOfMeasureType
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("01")]
        Item01,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("02")]
        Item02,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("03")]
        Item03,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("04")]
        Item04,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("05")]
        Item05,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("06")]
        Item06,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("07")]
        Item07,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("08")]
        Item08,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("09")]
        Item09,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("10")]
        Item10,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("11")]
        Item11,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("12")]
        Item12,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("13")]
        Item13,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("14")]
        Item14,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("15")]
        Item15,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("16")]
        Item16,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("17")]
        Item17,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("18")]
        Item18,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("19")]
        Item19,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("20")]
        Item20,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("21")]
        Item21,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("22")]
        Item22,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("23")]
        Item23,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("24")]
        Item24,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("25")]
        Item25,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("26")]
        Item26,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("27")]
        Item27,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("28")]
        Item28,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("29")]
        Item29,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("30")]
        Item30,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("31")]
        Item31,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("32")]
        Item32,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("33")]
        Item33,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("34")]
        Item34,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("35")]
        Item35,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("36")]
        Item36,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public partial class DiscountType
    {

        private string discountReasonField;

        private double discountRateField;

        private bool discountRateFieldSpecified;

        private double discountAmountField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string DiscountReason
        {
            get
            {
                return this.discountReasonField;
            }
            set
            {
                this.discountReasonField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public double DiscountRate
        {
            get
            {
                return this.discountRateField;
            }
            set
            {
                this.discountRateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool DiscountRateSpecified
        {
            get
            {
                return this.discountRateFieldSpecified;
            }
            set
            {
                this.discountRateFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public double DiscountAmount
        {
            get
            {
                return this.discountAmountField;
            }
            set
            {
                this.discountAmountField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public partial class ChargeType
    {

        private string chargeReasonField;

        private double chargeRateField;

        private bool chargeRateFieldSpecified;

        private double chargeAmountField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ChargeReason
        {
            get
            {
                return this.chargeReasonField;
            }
            set
            {
                this.chargeReasonField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public double ChargeRate
        {
            get
            {
                return this.chargeRateField;
            }
            set
            {
                this.chargeRateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ChargeRateSpecified
        {
            get
            {
                return this.chargeRateFieldSpecified;
            }
            set
            {
                this.chargeRateFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public double ChargeAmount
        {
            get
            {
                return this.chargeAmountField;
            }
            set
            {
                this.chargeAmountField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public partial class TaxType
    {

        private TaxTypeCodeType taxTypeCodeField;

        private double taxRateField;

        private AmountType taxableBaseField;

        private AmountType taxAmountField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public TaxTypeCodeType TaxTypeCode
        {
            get
            {
                return this.taxTypeCodeField;
            }
            set
            {
                this.taxTypeCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public double TaxRate
        {
            get
            {
                return this.taxRateField;
            }
            set
            {
                this.taxRateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public AmountType TaxableBase
        {
            get
            {
                return this.taxableBaseField;
            }
            set
            {
                this.taxableBaseField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public AmountType TaxAmount
        {
            get
            {
                return this.taxAmountField;
            }
            set
            {
                this.taxAmountField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public enum TaxTypeCodeType
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("01")]
        Item01,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("02")]
        Item02,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("03")]
        Item03,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("04")]
        Item04,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("05")]
        Item05,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("06")]
        Item06,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("07")]
        Item07,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("08")]
        Item08,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("09")]
        Item09,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("10")]
        Item10,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("11")]
        Item11,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("12")]
        Item12,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("13")]
        Item13,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("14")]
        Item14,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("15")]
        Item15,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("16")]
        Item16,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("17")]
        Item17,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("18")]
        Item18,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("19")]
        Item19,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("20")]
        Item20,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("21")]
        Item21,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("22")]
        Item22,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("23")]
        Item23,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("24")]
        Item24,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("25")]
        Item25,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("26")]
        Item26,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("27")]
        Item27,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("28")]
        Item28,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("29")]
        Item29,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public partial class AmountType
    {

        private double totalAmountField;

        private double equivalentInEurosField;

        private bool equivalentInEurosFieldSpecified;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public double TotalAmount
        {
            get
            {
                return this.totalAmountField;
            }
            set
            {
                this.totalAmountField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public double EquivalentInEuros
        {
            get
            {
                return this.equivalentInEurosField;
            }
            set
            {
                this.equivalentInEurosField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool EquivalentInEurosSpecified
        {
            get
            {
                return this.equivalentInEurosFieldSpecified;
            }
            set
            {
                this.equivalentInEurosFieldSpecified = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public partial class InvoiceLineTypeTax : TaxOutputType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public partial class TaxOutputType
    {

        private TaxTypeCodeType taxTypeCodeField;

        private double taxRateField;

        private AmountType taxableBaseField;

        private AmountType taxAmountField;

        private AmountType specialTaxableBaseField;

        private AmountType specialTaxAmountField;

        private double equivalenceSurchargeField;

        private bool equivalenceSurchargeFieldSpecified;

        private AmountType equivalenceSurchargeAmountField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public TaxTypeCodeType TaxTypeCode
        {
            get
            {
                return this.taxTypeCodeField;
            }
            set
            {
                this.taxTypeCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public double TaxRate
        {
            get
            {
                return this.taxRateField;
            }
            set
            {
                this.taxRateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public AmountType TaxableBase
        {
            get
            {
                return this.taxableBaseField;
            }
            set
            {
                this.taxableBaseField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public AmountType TaxAmount
        {
            get
            {
                return this.taxAmountField;
            }
            set
            {
                this.taxAmountField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public AmountType SpecialTaxableBase
        {
            get
            {
                return this.specialTaxableBaseField;
            }
            set
            {
                this.specialTaxableBaseField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public AmountType SpecialTaxAmount
        {
            get
            {
                return this.specialTaxAmountField;
            }
            set
            {
                this.specialTaxAmountField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public double EquivalenceSurcharge
        {
            get
            {
                return this.equivalenceSurchargeField;
            }
            set
            {
                this.equivalenceSurchargeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool EquivalenceSurchargeSpecified
        {
            get
            {
                return this.equivalenceSurchargeFieldSpecified;
            }
            set
            {
                this.equivalenceSurchargeFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public AmountType EquivalenceSurchargeAmount
        {
            get
            {
                return this.equivalenceSurchargeAmountField;
            }
            set
            {
                this.equivalenceSurchargeAmountField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public partial class PeriodDates
    {

        private System.DateTime startDateField;

        private System.DateTime endDateField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date")]
        public System.DateTime StartDate
        {
            get
            {
                return this.startDateField;
            }
            set
            {
                this.startDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date")]
        public System.DateTime EndDate
        {
            get
            {
                return this.endDateField;
            }
            set
            {
                this.endDateField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public partial class PaymentInKindType
    {

        private string paymentInKindReasonField;

        private double paymentInKindAmountField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string PaymentInKindReason
        {
            get
            {
                return this.paymentInKindReasonField;
            }
            set
            {
                this.paymentInKindReasonField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public double PaymentInKindAmount
        {
            get
            {
                return this.paymentInKindAmountField;
            }
            set
            {
                this.paymentInKindAmountField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public partial class AmountsWithheldType
    {

        private string withholdingReasonField;

        private double withholdingRateField;

        private bool withholdingRateFieldSpecified;

        private double withholdingAmountField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string WithholdingReason
        {
            get
            {
                return this.withholdingReasonField;
            }
            set
            {
                this.withholdingReasonField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public double WithholdingRate
        {
            get
            {
                return this.withholdingRateField;
            }
            set
            {
                this.withholdingRateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool WithholdingRateSpecified
        {
            get
            {
                return this.withholdingRateFieldSpecified;
            }
            set
            {
                this.withholdingRateFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public double WithholdingAmount
        {
            get
            {
                return this.withholdingAmountField;
            }
            set
            {
                this.withholdingAmountField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public partial class ReimbursableExpensesType
    {

        private TaxIdentificationType reimbursableExpensesSellerPartyField;

        private TaxIdentificationType reimbursableExpensesBuyerPartyField;

        private System.DateTime issueDateField;

        private bool issueDateFieldSpecified;

        private string invoiceNumberField;

        private string invoiceSeriesCodeField;

        private double reimbursableExpensesAmountField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public TaxIdentificationType ReimbursableExpensesSellerParty
        {
            get
            {
                return this.reimbursableExpensesSellerPartyField;
            }
            set
            {
                this.reimbursableExpensesSellerPartyField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public TaxIdentificationType ReimbursableExpensesBuyerParty
        {
            get
            {
                return this.reimbursableExpensesBuyerPartyField;
            }
            set
            {
                this.reimbursableExpensesBuyerPartyField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date")]
        public System.DateTime IssueDate
        {
            get
            {
                return this.issueDateField;
            }
            set
            {
                this.issueDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool IssueDateSpecified
        {
            get
            {
                return this.issueDateFieldSpecified;
            }
            set
            {
                this.issueDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string InvoiceNumber
        {
            get
            {
                return this.invoiceNumberField;
            }
            set
            {
                this.invoiceNumberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string InvoiceSeriesCode
        {
            get
            {
                return this.invoiceSeriesCodeField;
            }
            set
            {
                this.invoiceSeriesCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public double ReimbursableExpensesAmount
        {
            get
            {
                return this.reimbursableExpensesAmountField;
            }
            set
            {
                this.reimbursableExpensesAmountField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public partial class PaymentOnAccountType
    {

        private System.DateTime paymentOnAccountDateField;

        private double paymentOnAccountAmountField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date")]
        public System.DateTime PaymentOnAccountDate
        {
            get
            {
                return this.paymentOnAccountDateField;
            }
            set
            {
                this.paymentOnAccountDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public double PaymentOnAccountAmount
        {
            get
            {
                return this.paymentOnAccountAmountField;
            }
            set
            {
                this.paymentOnAccountAmountField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public partial class SubsidyType
    {

        private string subsidyDescriptionField;

        private double subsidyRateField;

        private bool subsidyRateFieldSpecified;

        private double subsidyAmountField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string SubsidyDescription
        {
            get
            {
                return this.subsidyDescriptionField;
            }
            set
            {
                this.subsidyDescriptionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public double SubsidyRate
        {
            get
            {
                return this.subsidyRateField;
            }
            set
            {
                this.subsidyRateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool SubsidyRateSpecified
        {
            get
            {
                return this.subsidyRateFieldSpecified;
            }
            set
            {
                this.subsidyRateFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public double SubsidyAmount
        {
            get
            {
                return this.subsidyAmountField;
            }
            set
            {
                this.subsidyAmountField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public partial class InvoiceTotalsType
    {

        private double totalGrossAmountField;

        private DiscountType[] generalDiscountsField;

        private ChargeType[] generalSurchargesField;

        private double totalGeneralDiscountsField;

        private bool totalGeneralDiscountsFieldSpecified;

        private double totalGeneralSurchargesField;

        private bool totalGeneralSurchargesFieldSpecified;

        private double totalGrossAmountBeforeTaxesField;

        private double totalTaxOutputsField;

        private double totalTaxesWithheldField;

        private double invoiceTotalField;

        private SubsidyType[] subsidiesField;

        private PaymentOnAccountType[] paymentsOnAccountField;

        private ReimbursableExpensesType[] reimbursableExpensesField;

        private double totalFinancialExpensesField;

        private bool totalFinancialExpensesFieldSpecified;

        private double totalOutstandingAmountField;

        private double totalPaymentsOnAccountField;

        private bool totalPaymentsOnAccountFieldSpecified;

        private AmountsWithheldType amountsWithheldField;

        private double totalExecutableAmountField;

        private double totalReimbursableExpensesField;

        private bool totalReimbursableExpensesFieldSpecified;

        private PaymentInKindType paymentInKindField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public double TotalGrossAmount
        {
            get
            {
                return this.totalGrossAmountField;
            }
            set
            {
                this.totalGrossAmountField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("Discount", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
        public DiscountType[] GeneralDiscounts
        {
            get
            {
                return this.generalDiscountsField;
            }
            set
            {
                this.generalDiscountsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("Charge", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
        public ChargeType[] GeneralSurcharges
        {
            get
            {
                return this.generalSurchargesField;
            }
            set
            {
                this.generalSurchargesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public double TotalGeneralDiscounts
        {
            get
            {
                return this.totalGeneralDiscountsField;
            }
            set
            {
                this.totalGeneralDiscountsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool TotalGeneralDiscountsSpecified
        {
            get
            {
                return this.totalGeneralDiscountsFieldSpecified;
            }
            set
            {
                this.totalGeneralDiscountsFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public double TotalGeneralSurcharges
        {
            get
            {
                return this.totalGeneralSurchargesField;
            }
            set
            {
                this.totalGeneralSurchargesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool TotalGeneralSurchargesSpecified
        {
            get
            {
                return this.totalGeneralSurchargesFieldSpecified;
            }
            set
            {
                this.totalGeneralSurchargesFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public double TotalGrossAmountBeforeTaxes
        {
            get
            {
                return this.totalGrossAmountBeforeTaxesField;
            }
            set
            {
                this.totalGrossAmountBeforeTaxesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public double TotalTaxOutputs
        {
            get
            {
                return this.totalTaxOutputsField;
            }
            set
            {
                this.totalTaxOutputsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public double TotalTaxesWithheld
        {
            get
            {
                return this.totalTaxesWithheldField;
            }
            set
            {
                this.totalTaxesWithheldField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public double InvoiceTotal
        {
            get
            {
                return this.invoiceTotalField;
            }
            set
            {
                this.invoiceTotalField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("Subsidy", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
        public SubsidyType[] Subsidies
        {
            get
            {
                return this.subsidiesField;
            }
            set
            {
                this.subsidiesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("PaymentOnAccount", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
        public PaymentOnAccountType[] PaymentsOnAccount
        {
            get
            {
                return this.paymentsOnAccountField;
            }
            set
            {
                this.paymentsOnAccountField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("ReimbursableExpenses", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
        public ReimbursableExpensesType[] ReimbursableExpenses
        {
            get
            {
                return this.reimbursableExpensesField;
            }
            set
            {
                this.reimbursableExpensesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public double TotalFinancialExpenses
        {
            get
            {
                return this.totalFinancialExpensesField;
            }
            set
            {
                this.totalFinancialExpensesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool TotalFinancialExpensesSpecified
        {
            get
            {
                return this.totalFinancialExpensesFieldSpecified;
            }
            set
            {
                this.totalFinancialExpensesFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public double TotalOutstandingAmount
        {
            get
            {
                return this.totalOutstandingAmountField;
            }
            set
            {
                this.totalOutstandingAmountField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public double TotalPaymentsOnAccount
        {
            get
            {
                return this.totalPaymentsOnAccountField;
            }
            set
            {
                this.totalPaymentsOnAccountField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool TotalPaymentsOnAccountSpecified
        {
            get
            {
                return this.totalPaymentsOnAccountFieldSpecified;
            }
            set
            {
                this.totalPaymentsOnAccountFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public AmountsWithheldType AmountsWithheld
        {
            get
            {
                return this.amountsWithheldField;
            }
            set
            {
                this.amountsWithheldField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public double TotalExecutableAmount
        {
            get
            {
                return this.totalExecutableAmountField;
            }
            set
            {
                this.totalExecutableAmountField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public double TotalReimbursableExpenses
        {
            get
            {
                return this.totalReimbursableExpensesField;
            }
            set
            {
                this.totalReimbursableExpensesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool TotalReimbursableExpensesSpecified
        {
            get
            {
                return this.totalReimbursableExpensesFieldSpecified;
            }
            set
            {
                this.totalReimbursableExpensesFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public PaymentInKindType PaymentInKind
        {
            get
            {
                return this.paymentInKindField;
            }
            set
            {
                this.paymentInKindField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public partial class ExchangeRateDetailsType
    {

        private double exchangeRateField;

        private System.DateTime exchangeRateDateField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public double ExchangeRate
        {
            get
            {
                return this.exchangeRateField;
            }
            set
            {
                this.exchangeRateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date")]
        public System.DateTime ExchangeRateDate
        {
            get
            {
                return this.exchangeRateDateField;
            }
            set
            {
                this.exchangeRateDateField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public partial class PlaceOfIssueType
    {

        private string postCodeField;

        private string placeOfIssueDescriptionField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string PostCode
        {
            get
            {
                return this.postCodeField;
            }
            set
            {
                this.postCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string PlaceOfIssueDescription
        {
            get
            {
                return this.placeOfIssueDescriptionField;
            }
            set
            {
                this.placeOfIssueDescriptionField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public partial class InvoiceIssueDataType
    {

        private System.DateTime issueDateField;

        private System.DateTime operationDateField;

        private bool operationDateFieldSpecified;

        private PlaceOfIssueType placeOfIssueField;

        private PeriodDates invoicingPeriodField;

        private CurrencyCodeType invoiceCurrencyCodeField;

        private ExchangeRateDetailsType exchangeRateDetailsField;

        private CurrencyCodeType taxCurrencyCodeField;

        private LanguageCodeType languageNameField;

        private string invoiceDescriptionField;

        private string receiverTransactionReferenceField;

        private string fileReferenceField;

        private string receiverContractReferenceField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date")]
        public System.DateTime IssueDate
        {
            get
            {
                return this.issueDateField;
            }
            set
            {
                this.issueDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date")]
        public System.DateTime OperationDate
        {
            get
            {
                return this.operationDateField;
            }
            set
            {
                this.operationDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool OperationDateSpecified
        {
            get
            {
                return this.operationDateFieldSpecified;
            }
            set
            {
                this.operationDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public PlaceOfIssueType PlaceOfIssue
        {
            get
            {
                return this.placeOfIssueField;
            }
            set
            {
                this.placeOfIssueField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public PeriodDates InvoicingPeriod
        {
            get
            {
                return this.invoicingPeriodField;
            }
            set
            {
                this.invoicingPeriodField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public CurrencyCodeType InvoiceCurrencyCode
        {
            get
            {
                return this.invoiceCurrencyCodeField;
            }
            set
            {
                this.invoiceCurrencyCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public ExchangeRateDetailsType ExchangeRateDetails
        {
            get
            {
                return this.exchangeRateDetailsField;
            }
            set
            {
                this.exchangeRateDetailsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public CurrencyCodeType TaxCurrencyCode
        {
            get
            {
                return this.taxCurrencyCodeField;
            }
            set
            {
                this.taxCurrencyCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public LanguageCodeType LanguageName
        {
            get
            {
                return this.languageNameField;
            }
            set
            {
                this.languageNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string InvoiceDescription
        {
            get
            {
                return this.invoiceDescriptionField;
            }
            set
            {
                this.invoiceDescriptionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ReceiverTransactionReference
        {
            get
            {
                return this.receiverTransactionReferenceField;
            }
            set
            {
                this.receiverTransactionReferenceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string FileReference
        {
            get
            {
                return this.fileReferenceField;
            }
            set
            {
                this.fileReferenceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ReceiverContractReference
        {
            get
            {
                return this.receiverContractReferenceField;
            }
            set
            {
                this.receiverContractReferenceField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public enum CurrencyCodeType
    {

        /// <remarks/>
        AFN,

        /// <remarks/>
        ALL,

        /// <remarks/>
        AMD,

        /// <remarks/>
        ANG,

        /// <remarks/>
        AOA,

        /// <remarks/>
        ARS,

        /// <remarks/>
        AUD,

        /// <remarks/>
        AWG,

        /// <remarks/>
        AZN,

        /// <remarks/>
        BAD,

        /// <remarks/>
        BBD,

        /// <remarks/>
        BDT,

        /// <remarks/>
        BGN,

        /// <remarks/>
        BHD,

        /// <remarks/>
        BIF,

        /// <remarks/>
        BMD,

        /// <remarks/>
        BND,

        /// <remarks/>
        BOB,

        /// <remarks/>
        BRL,

        /// <remarks/>
        BRR,

        /// <remarks/>
        BSD,

        /// <remarks/>
        BWP,

        /// <remarks/>
        BYR,

        /// <remarks/>
        BZD,

        /// <remarks/>
        CAD,

        /// <remarks/>
        CDF,

        /// <remarks/>
        CDP,

        /// <remarks/>
        CHF,

        /// <remarks/>
        CLP,

        /// <remarks/>
        CNY,

        /// <remarks/>
        COP,

        /// <remarks/>
        CRC,

        /// <remarks/>
        CUP,

        /// <remarks/>
        CVE,

        /// <remarks/>
        CZK,

        /// <remarks/>
        DJF,

        /// <remarks/>
        DKK,

        /// <remarks/>
        DOP,

        /// <remarks/>
        DRP,

        /// <remarks/>
        DZD,

        /// <remarks/>
        EEK,

        /// <remarks/>
        EGP,

        /// <remarks/>
        ESP,

        /// <remarks/>
        ETB,

        /// <remarks/>
        EUR,

        /// <remarks/>
        FJD,

        /// <remarks/>
        FKP,

        /// <remarks/>
        GBP,

        /// <remarks/>
        GEK,

        /// <remarks/>
        GHC,

        /// <remarks/>
        GIP,

        /// <remarks/>
        GMD,

        /// <remarks/>
        GNF,

        /// <remarks/>
        GTQ,

        /// <remarks/>
        GWP,

        /// <remarks/>
        GYD,

        /// <remarks/>
        HKD,

        /// <remarks/>
        HNL,

        /// <remarks/>
        HRK,

        /// <remarks/>
        HTG,

        /// <remarks/>
        HUF,

        /// <remarks/>
        IDR,

        /// <remarks/>
        ILS,

        /// <remarks/>
        INR,

        /// <remarks/>
        IQD,

        /// <remarks/>
        IRR,

        /// <remarks/>
        ISK,

        /// <remarks/>
        JMD,

        /// <remarks/>
        JOD,

        /// <remarks/>
        JPY,

        /// <remarks/>
        KES,

        /// <remarks/>
        KGS,

        /// <remarks/>
        KHR,

        /// <remarks/>
        KMF,

        /// <remarks/>
        KPW,

        /// <remarks/>
        KRW,

        /// <remarks/>
        KWD,

        /// <remarks/>
        KYD,

        /// <remarks/>
        KZT,

        /// <remarks/>
        LAK,

        /// <remarks/>
        LBP,

        /// <remarks/>
        LKR,

        /// <remarks/>
        LRD,

        /// <remarks/>
        LSL,

        /// <remarks/>
        LTL,

        /// <remarks/>
        LVL,

        /// <remarks/>
        LYD,

        /// <remarks/>
        MAD,

        /// <remarks/>
        MDL,

        /// <remarks/>
        MGF,

        /// <remarks/>
        MNC,

        /// <remarks/>
        MNT,

        /// <remarks/>
        MOP,

        /// <remarks/>
        MRO,

        /// <remarks/>
        MUR,

        /// <remarks/>
        MVR,

        /// <remarks/>
        MWK,

        /// <remarks/>
        MXN,

        /// <remarks/>
        MYR,

        /// <remarks/>
        MZM,

        /// <remarks/>
        NGN,

        /// <remarks/>
        NIC,

        /// <remarks/>
        NIO,

        /// <remarks/>
        NIS,

        /// <remarks/>
        NOK,

        /// <remarks/>
        NPR,

        /// <remarks/>
        NZD,

        /// <remarks/>
        OMR,

        /// <remarks/>
        PAB,

        /// <remarks/>
        PEI,

        /// <remarks/>
        PEN,

        /// <remarks/>
        PES,

        /// <remarks/>
        PGK,

        /// <remarks/>
        PHP,

        /// <remarks/>
        PKR,

        /// <remarks/>
        PLN,

        /// <remarks/>
        PYG,

        /// <remarks/>
        QAR,

        /// <remarks/>
        RMB,

        /// <remarks/>
        RON,

        /// <remarks/>
        RUB,

        /// <remarks/>
        RWF,

        /// <remarks/>
        SAR,

        /// <remarks/>
        SBD,

        /// <remarks/>
        SCR,

        /// <remarks/>
        SDP,

        /// <remarks/>
        SEK,

        /// <remarks/>
        SGD,

        /// <remarks/>
        SHP,

        /// <remarks/>
        SKK,

        /// <remarks/>
        SLL,

        /// <remarks/>
        SOL,

        /// <remarks/>
        SOS,

        /// <remarks/>
        SRD,

        /// <remarks/>
        STD,

        /// <remarks/>
        SVC,

        /// <remarks/>
        SYP,

        /// <remarks/>
        SZL,

        /// <remarks/>
        THB,

        /// <remarks/>
        TJS,

        /// <remarks/>
        TMM,

        /// <remarks/>
        TND,

        /// <remarks/>
        TOP,

        /// <remarks/>
        TPE,

        /// <remarks/>
        TRY,

        /// <remarks/>
        TTD,

        /// <remarks/>
        TWD,

        /// <remarks/>
        TZS,

        /// <remarks/>
        UAH,

        /// <remarks/>
        UGS,

        /// <remarks/>
        USD,

        /// <remarks/>
        UYP,

        /// <remarks/>
        UYU,

        /// <remarks/>
        VEF,

        /// <remarks/>
        VND,

        /// <remarks/>
        VUV,

        /// <remarks/>
        WST,

        /// <remarks/>
        XAF,

        /// <remarks/>
        XCD,

        /// <remarks/>
        XOF,

        /// <remarks/>
        YER,

        /// <remarks/>
        ZAR,

        /// <remarks/>
        ZMK,

        /// <remarks/>
        ZWD,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public enum LanguageCodeType
    {

        /// <remarks/>
        ar,

        /// <remarks/>
        be,

        /// <remarks/>
        bg,

        /// <remarks/>
        ca,

        /// <remarks/>
        cs,

        /// <remarks/>
        da,

        /// <remarks/>
        de,

        /// <remarks/>
        el,

        /// <remarks/>
        en,

        /// <remarks/>
        es,

        /// <remarks/>
        et,

        /// <remarks/>
        eu,

        /// <remarks/>
        fi,

        /// <remarks/>
        fr,

        /// <remarks/>
        ga,

        /// <remarks/>
        gl,

        /// <remarks/>
        hr,

        /// <remarks/>
        hu,

        /// <remarks/>
        @is,

        /// <remarks/>
        it,

        /// <remarks/>
        lv,

        /// <remarks/>
        lt,

        /// <remarks/>
        mk,

        /// <remarks/>
        mt,

        /// <remarks/>
        nl,

        /// <remarks/>
        no,

        /// <remarks/>
        pl,

        /// <remarks/>
        pt,

        /// <remarks/>
        ro,

        /// <remarks/>
        ru,

        /// <remarks/>
        sk,

        /// <remarks/>
        sl,

        /// <remarks/>
        sq,

        /// <remarks/>
        sr,

        /// <remarks/>
        sv,

        /// <remarks/>
        tr,

        /// <remarks/>
        uk,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public partial class CorrectiveType
    {

        private string invoiceNumberField;

        private string invoiceSeriesCodeField;

        private ReasonCodeType reasonCodeField;

        private ReasonDescriptionType reasonDescriptionField;

        private PeriodDates taxPeriodField;

        private CorrectionMethodType correctionMethodField;

        private CorrectionMethodDescriptionType correctionMethodDescriptionField;

        private string additionalReasonDescriptionField;

        private System.DateTime invoiceIssueDateField;

        private bool invoiceIssueDateFieldSpecified;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string InvoiceNumber
        {
            get
            {
                return this.invoiceNumberField;
            }
            set
            {
                this.invoiceNumberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string InvoiceSeriesCode
        {
            get
            {
                return this.invoiceSeriesCodeField;
            }
            set
            {
                this.invoiceSeriesCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public ReasonCodeType ReasonCode
        {
            get
            {
                return this.reasonCodeField;
            }
            set
            {
                this.reasonCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public ReasonDescriptionType ReasonDescription
        {
            get
            {
                return this.reasonDescriptionField;
            }
            set
            {
                this.reasonDescriptionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public PeriodDates TaxPeriod
        {
            get
            {
                return this.taxPeriodField;
            }
            set
            {
                this.taxPeriodField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public CorrectionMethodType CorrectionMethod
        {
            get
            {
                return this.correctionMethodField;
            }
            set
            {
                this.correctionMethodField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public CorrectionMethodDescriptionType CorrectionMethodDescription
        {
            get
            {
                return this.correctionMethodDescriptionField;
            }
            set
            {
                this.correctionMethodDescriptionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string AdditionalReasonDescription
        {
            get
            {
                return this.additionalReasonDescriptionField;
            }
            set
            {
                this.additionalReasonDescriptionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date")]
        public System.DateTime InvoiceIssueDate
        {
            get
            {
                return this.invoiceIssueDateField;
            }
            set
            {
                this.invoiceIssueDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool InvoiceIssueDateSpecified
        {
            get
            {
                return this.invoiceIssueDateFieldSpecified;
            }
            set
            {
                this.invoiceIssueDateFieldSpecified = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public enum ReasonCodeType
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("01")]
        Item01,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("02")]
        Item02,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("03")]
        Item03,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("04")]
        Item04,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("05")]
        Item05,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("06")]
        Item06,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("07")]
        Item07,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("08")]
        Item08,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("09")]
        Item09,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("10")]
        Item10,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("11")]
        Item11,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("12")]
        Item12,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("13")]
        Item13,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("14")]
        Item14,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("15")]
        Item15,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("16")]
        Item16,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("80")]
        Item80,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("81")]
        Item81,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("82")]
        Item82,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("83")]
        Item83,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("84")]
        Item84,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("85")]
        Item85,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public enum ReasonDescriptionType
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("Número de la factura")]
        Númerodelafactura,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("Serie de la factura")]
        Seriedelafactura,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("Fecha expedición")]
        Fechaexpedición,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("Nombre y apellidos/Razón Social-Emisor")]
        NombreyapellidosRazónSocialEmisor,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("Nombre y apellidos/Razón Social-Receptor")]
        NombreyapellidosRazónSocialReceptor,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("Identificación fiscal Emisor/obligado")]
        IdentificaciónfiscalEmisorobligado,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("Identificación fiscal Receptor")]
        IdentificaciónfiscalReceptor,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("Domicilio Emisor/Obligado")]
        DomicilioEmisorObligado,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("Domicilio Receptor")]
        DomicilioReceptor,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("Detalle Operación")]
        DetalleOperación,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("Porcentaje impositivo a aplicar")]
        Porcentajeimpositivoaaplicar,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("Cuota tributaria a aplicar")]
        Cuotatributariaaaplicar,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("Fecha/Periodo a aplicar")]
        FechaPeriodoaaplicar,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("Clase de factura")]
        Clasedefactura,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("Literales legales")]
        Literaleslegales,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("Base imponible")]
        Baseimponible,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("Cálculo de cuotas repercutidas")]
        Cálculodecuotasrepercutidas,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("Cálculo de cuotas retenidas")]
        Cálculodecuotasretenidas,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("Base imponible modificada por devolución de envases / embalajes")]
        Baseimponiblemodificadapordevolucióndeenvasesembalajes,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("Base imponible modificada por descuentos y bonificaciones")]
        Baseimponiblemodificadapordescuentosybonificaciones,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("Base imponible modificada por resolución firme, judicial o administrativa")]
        Baseimponiblemodificadaporresoluciónfirmejudicialoadministrativa,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("Base imponible modificada cuotas repercutidas no satisfechas. Auto de declaración" +
            " de concurso")]
        BaseimponiblemodificadacuotasrepercutidasnosatisfechasAutodedeclaracióndeconcurso,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public enum CorrectionMethodType
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("01")]
        Item01,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("02")]
        Item02,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("03")]
        Item03,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("04")]
        Item04,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public enum CorrectionMethodDescriptionType
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("Rectificación íntegra")]
        Rectificacióníntegra,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("Rectificación por diferencias")]
        Rectificaciónpordiferencias,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("Rectificación por descuento por volumen de operaciones durante un periodo")]
        Rectificaciónpordescuentoporvolumendeoperacionesduranteunperiodo,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("Autorizadas por la Agencia Tributaria")]
        AutorizadasporlaAgenciaTributaria,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public partial class InvoiceHeaderType
    {

        private string invoiceNumberField;

        private string invoiceSeriesCodeField;

        private InvoiceDocumentTypeType invoiceDocumentTypeField;

        private InvoiceClassType invoiceClassField;

        private CorrectiveType correctiveField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string InvoiceNumber
        {
            get
            {
                return this.invoiceNumberField;
            }
            set
            {
                this.invoiceNumberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string InvoiceSeriesCode
        {
            get
            {
                return this.invoiceSeriesCodeField;
            }
            set
            {
                this.invoiceSeriesCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public InvoiceDocumentTypeType InvoiceDocumentType
        {
            get
            {
                return this.invoiceDocumentTypeField;
            }
            set
            {
                this.invoiceDocumentTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public InvoiceClassType InvoiceClass
        {
            get
            {
                return this.invoiceClassField;
            }
            set
            {
                this.invoiceClassField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public CorrectiveType Corrective
        {
            get
            {
                return this.correctiveField;
            }
            set
            {
                this.correctiveField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public enum InvoiceDocumentTypeType
    {

        /// <remarks/>
        FC,

        /// <remarks/>
        FA,

        /// <remarks/>
        AF,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public enum InvoiceClassType
    {

        /// <remarks/>
        OO,

        /// <remarks/>
        OR,

        /// <remarks/>
        OC,

        /// <remarks/>
        CO,

        /// <remarks/>
        CR,

        /// <remarks/>
        CC,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public partial class InvoiceType
    {

        private InvoiceHeaderType invoiceHeaderField;

        private InvoiceIssueDataType invoiceIssueDataField;

        private TaxOutputType[] taxesOutputsField;

        private TaxType[] taxesWithheldField;

        private InvoiceTotalsType invoiceTotalsField;

        private InvoiceLineType[] itemsField;

        private InstallmentType[] paymentDetailsField;

        private string[] legalLiteralsField;

        private AdditionalDataType additionalDataField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public InvoiceHeaderType InvoiceHeader
        {
            get
            {
                return this.invoiceHeaderField;
            }
            set
            {
                this.invoiceHeaderField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public InvoiceIssueDataType InvoiceIssueData
        {
            get
            {
                return this.invoiceIssueDataField;
            }
            set
            {
                this.invoiceIssueDataField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("Tax", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
        public TaxOutputType[] TaxesOutputs
        {
            get
            {
                return this.taxesOutputsField;
            }
            set
            {
                this.taxesOutputsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("Tax", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
        public TaxType[] TaxesWithheld
        {
            get
            {
                return this.taxesWithheldField;
            }
            set
            {
                this.taxesWithheldField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public InvoiceTotalsType InvoiceTotals
        {
            get
            {
                return this.invoiceTotalsField;
            }
            set
            {
                this.invoiceTotalsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("InvoiceLine", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
        public InvoiceLineType[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("Installment", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
        public InstallmentType[] PaymentDetails
        {
            get
            {
                return this.paymentDetailsField;
            }
            set
            {
                this.paymentDetailsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("LegalReference", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
        public string[] LegalLiterals
        {
            get
            {
                return this.legalLiteralsField;
            }
            set
            {
                this.legalLiteralsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public AdditionalDataType AdditionalData
        {
            get
            {
                return this.additionalDataField;
            }
            set
            {
                this.additionalDataField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public partial class InstallmentType
    {

        private System.DateTime installmentDueDateField;

        private double installmentAmountField;

        private PaymentMeansType paymentMeansField;

        private AccountType accountToBeCreditedField;

        private string paymentReconciliationReferenceField;

        private AccountType accountToBeDebitedField;

        private string collectionAdditionalInformationField;

        private string regulatoryReportingDataField;

        private string debitReconciliationReferenceField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date")]
        public System.DateTime InstallmentDueDate
        {
            get
            {
                return this.installmentDueDateField;
            }
            set
            {
                this.installmentDueDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public double InstallmentAmount
        {
            get
            {
                return this.installmentAmountField;
            }
            set
            {
                this.installmentAmountField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public PaymentMeansType PaymentMeans
        {
            get
            {
                return this.paymentMeansField;
            }
            set
            {
                this.paymentMeansField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public AccountType AccountToBeCredited
        {
            get
            {
                return this.accountToBeCreditedField;
            }
            set
            {
                this.accountToBeCreditedField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string PaymentReconciliationReference
        {
            get
            {
                return this.paymentReconciliationReferenceField;
            }
            set
            {
                this.paymentReconciliationReferenceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public AccountType AccountToBeDebited
        {
            get
            {
                return this.accountToBeDebitedField;
            }
            set
            {
                this.accountToBeDebitedField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string CollectionAdditionalInformation
        {
            get
            {
                return this.collectionAdditionalInformationField;
            }
            set
            {
                this.collectionAdditionalInformationField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string RegulatoryReportingData
        {
            get
            {
                return this.regulatoryReportingDataField;
            }
            set
            {
                this.regulatoryReportingDataField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string DebitReconciliationReference
        {
            get
            {
                return this.debitReconciliationReferenceField;
            }
            set
            {
                this.debitReconciliationReferenceField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public enum PaymentMeansType
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("01")]
        Item01,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("02")]
        Item02,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("03")]
        Item03,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("04")]
        Item04,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("05")]
        Item05,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("06")]
        Item06,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("07")]
        Item07,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("08")]
        Item08,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("09")]
        Item09,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("10")]
        Item10,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("11")]
        Item11,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("12")]
        Item12,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("13")]
        Item13,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("14")]
        Item14,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("15")]
        Item15,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("16")]
        Item16,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("17")]
        Item17,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("18")]
        Item18,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("19")]
        Item19,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public partial class AccountType
    {

        private string itemField;

        private ItemChoiceType itemElementNameField;

        private string bankCodeField;

        private string branchCodeField;

        private object item1Field;

        private string bICField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("AccountNumber", typeof(string), Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlElementAttribute("IBAN", typeof(string), Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemElementName")]
        public string Item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public ItemChoiceType ItemElementName
        {
            get
            {
                return this.itemElementNameField;
            }
            set
            {
                this.itemElementNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string BankCode
        {
            get
            {
                return this.bankCodeField;
            }
            set
            {
                this.bankCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string BranchCode
        {
            get
            {
                return this.branchCodeField;
            }
            set
            {
                this.branchCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("BranchInSpainAddress", typeof(AddressType), Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlElementAttribute("OverseasBranchAddress", typeof(OverseasAddressType), Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public object Item1
        {
            get
            {
                return this.item1Field;
            }
            set
            {
                this.item1Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string BIC
        {
            get
            {
                return this.bICField;
            }
            set
            {
                this.bICField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml", IncludeInSchema = false)]
    public enum ItemChoiceType
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute(":AccountNumber")]
        AccountNumber,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute(":IBAN")]
        IBAN,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public partial class AddressType
    {

        private string addressField;

        private string postCodeField;

        private string townField;

        private string provinceField;

        private CountryType countryCodeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Address
        {
            get
            {
                return this.addressField;
            }
            set
            {
                this.addressField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string PostCode
        {
            get
            {
                return this.postCodeField;
            }
            set
            {
                this.postCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Town
        {
            get
            {
                return this.townField;
            }
            set
            {
                this.townField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Province
        {
            get
            {
                return this.provinceField;
            }
            set
            {
                this.provinceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public CountryType CountryCode
        {
            get
            {
                return this.countryCodeField;
            }
            set
            {
                this.countryCodeField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public enum CountryType
    {

        /// <remarks/>
        AFG,

        /// <remarks/>
        ALB,

        /// <remarks/>
        DZA,

        /// <remarks/>
        ASM,

        /// <remarks/>
        AND,

        /// <remarks/>
        AGO,

        /// <remarks/>
        AIA,

        /// <remarks/>
        ATG,

        /// <remarks/>
        ARG,

        /// <remarks/>
        ARM,

        /// <remarks/>
        ABW,

        /// <remarks/>
        AUS,

        /// <remarks/>
        AUT,

        /// <remarks/>
        AZE,

        /// <remarks/>
        BHS,

        /// <remarks/>
        BHR,

        /// <remarks/>
        BGD,

        /// <remarks/>
        BRB,

        /// <remarks/>
        BLR,

        /// <remarks/>
        BEL,

        /// <remarks/>
        BLZ,

        /// <remarks/>
        BEN,

        /// <remarks/>
        BMU,

        /// <remarks/>
        BTN,

        /// <remarks/>
        BOL,

        /// <remarks/>
        BIH,

        /// <remarks/>
        BWA,

        /// <remarks/>
        BRA,

        /// <remarks/>
        BRN,

        /// <remarks/>
        BGR,

        /// <remarks/>
        BFA,

        /// <remarks/>
        BDI,

        /// <remarks/>
        KHM,

        /// <remarks/>
        CMR,

        /// <remarks/>
        CAN,

        /// <remarks/>
        CPV,

        /// <remarks/>
        CYM,

        /// <remarks/>
        CAF,

        /// <remarks/>
        TCD,

        /// <remarks/>
        CHL,

        /// <remarks/>
        CHN,

        /// <remarks/>
        COD,

        /// <remarks/>
        COL,

        /// <remarks/>
        COM,

        /// <remarks/>
        COG,

        /// <remarks/>
        COK,

        /// <remarks/>
        CRI,

        /// <remarks/>
        CIV,

        /// <remarks/>
        HRV,

        /// <remarks/>
        CUB,

        /// <remarks/>
        CYP,

        /// <remarks/>
        CZE,

        /// <remarks/>
        DNK,

        /// <remarks/>
        DJI,

        /// <remarks/>
        DMA,

        /// <remarks/>
        DOM,

        /// <remarks/>
        ECU,

        /// <remarks/>
        EGY,

        /// <remarks/>
        SLV,

        /// <remarks/>
        GNQ,

        /// <remarks/>
        ERI,

        /// <remarks/>
        EST,

        /// <remarks/>
        ETH,

        /// <remarks/>
        FLK,

        /// <remarks/>
        FRO,

        /// <remarks/>
        FJI,

        /// <remarks/>
        FIN,

        /// <remarks/>
        FRA,

        /// <remarks/>
        GUF,

        /// <remarks/>
        PYF,

        /// <remarks/>
        GAB,

        /// <remarks/>
        GMB,

        /// <remarks/>
        GEO,

        /// <remarks/>
        GGY,

        /// <remarks/>
        DEU,

        /// <remarks/>
        GHA,

        /// <remarks/>
        GIB,

        /// <remarks/>
        GRC,

        /// <remarks/>
        GRL,

        /// <remarks/>
        GRD,

        /// <remarks/>
        GLP,

        /// <remarks/>
        GUM,

        /// <remarks/>
        GTM,

        /// <remarks/>
        GIN,

        /// <remarks/>
        GNB,

        /// <remarks/>
        GUY,

        /// <remarks/>
        HTI,

        /// <remarks/>
        HND,

        /// <remarks/>
        HKG,

        /// <remarks/>
        HUN,

        /// <remarks/>
        ISL,

        /// <remarks/>
        IND,

        /// <remarks/>
        IDN,

        /// <remarks/>
        IMN,

        /// <remarks/>
        IRN,

        /// <remarks/>
        IRQ,

        /// <remarks/>
        IRL,

        /// <remarks/>
        ISR,

        /// <remarks/>
        ITA,

        /// <remarks/>
        JAM,

        /// <remarks/>
        JEY,

        /// <remarks/>
        JPN,

        /// <remarks/>
        JOR,

        /// <remarks/>
        KAZ,

        /// <remarks/>
        KEN,

        /// <remarks/>
        KIR,

        /// <remarks/>
        PRK,

        /// <remarks/>
        KOR,

        /// <remarks/>
        KWT,

        /// <remarks/>
        KGZ,

        /// <remarks/>
        LAO,

        /// <remarks/>
        LVA,

        /// <remarks/>
        LBN,

        /// <remarks/>
        LSO,

        /// <remarks/>
        LBR,

        /// <remarks/>
        LBY,

        /// <remarks/>
        LIE,

        /// <remarks/>
        LTU,

        /// <remarks/>
        LUX,

        /// <remarks/>
        MAC,

        /// <remarks/>
        MKD,

        /// <remarks/>
        MDG,

        /// <remarks/>
        MWI,

        /// <remarks/>
        MYS,

        /// <remarks/>
        MDV,

        /// <remarks/>
        MLI,

        /// <remarks/>
        MLT,

        /// <remarks/>
        MHL,

        /// <remarks/>
        MTQ,

        /// <remarks/>
        MRT,

        /// <remarks/>
        MUS,

        /// <remarks/>
        MYT,

        /// <remarks/>
        MEX,

        /// <remarks/>
        FSM,

        /// <remarks/>
        MDA,

        /// <remarks/>
        MCO,

        /// <remarks/>
        MNE,

        /// <remarks/>
        MNG,

        /// <remarks/>
        MSR,

        /// <remarks/>
        MAR,

        /// <remarks/>
        MOZ,

        /// <remarks/>
        MMR,

        /// <remarks/>
        NAM,

        /// <remarks/>
        NRU,

        /// <remarks/>
        NPL,

        /// <remarks/>
        NLD,

        /// <remarks/>
        ANT,

        /// <remarks/>
        NCL,

        /// <remarks/>
        NZL,

        /// <remarks/>
        NIC,

        /// <remarks/>
        NER,

        /// <remarks/>
        NGA,

        /// <remarks/>
        NIU,

        /// <remarks/>
        NFK,

        /// <remarks/>
        MNP,

        /// <remarks/>
        NOR,

        /// <remarks/>
        OMN,

        /// <remarks/>
        PAK,

        /// <remarks/>
        PLW,

        /// <remarks/>
        PAN,

        /// <remarks/>
        PNG,

        /// <remarks/>
        PRY,

        /// <remarks/>
        PSE,

        /// <remarks/>
        PER,

        /// <remarks/>
        PHL,

        /// <remarks/>
        PCN,

        /// <remarks/>
        POL,

        /// <remarks/>
        PRT,

        /// <remarks/>
        PRI,

        /// <remarks/>
        QAT,

        /// <remarks/>
        REU,

        /// <remarks/>
        ROU,

        /// <remarks/>
        RUS,

        /// <remarks/>
        RWA,

        /// <remarks/>
        KNA,

        /// <remarks/>
        LCA,

        /// <remarks/>
        VCT,

        /// <remarks/>
        WSM,

        /// <remarks/>
        SMR,

        /// <remarks/>
        STP,

        /// <remarks/>
        SAU,

        /// <remarks/>
        SEN,

        /// <remarks/>
        SRB,

        /// <remarks/>
        SYC,

        /// <remarks/>
        SLE,

        /// <remarks/>
        SGP,

        /// <remarks/>
        SVK,

        /// <remarks/>
        SVN,

        /// <remarks/>
        SLB,

        /// <remarks/>
        SOM,

        /// <remarks/>
        ZAF,

        /// <remarks/>
        ESP,

        /// <remarks/>
        LKA,

        /// <remarks/>
        SHN,

        /// <remarks/>
        SPM,

        /// <remarks/>
        SDN,

        /// <remarks/>
        SUR,

        /// <remarks/>
        SJM,

        /// <remarks/>
        SWZ,

        /// <remarks/>
        SWE,

        /// <remarks/>
        CHE,

        /// <remarks/>
        SYR,

        /// <remarks/>
        TWN,

        /// <remarks/>
        TJK,

        /// <remarks/>
        TZA,

        /// <remarks/>
        THA,

        /// <remarks/>
        TGO,

        /// <remarks/>
        TKL,

        /// <remarks/>
        TON,

        /// <remarks/>
        TTO,

        /// <remarks/>
        TUN,

        /// <remarks/>
        TUR,

        /// <remarks/>
        TKM,

        /// <remarks/>
        TLS,

        /// <remarks/>
        TCA,

        /// <remarks/>
        TUV,

        /// <remarks/>
        UGA,

        /// <remarks/>
        UKR,

        /// <remarks/>
        ARE,

        /// <remarks/>
        GBR,

        /// <remarks/>
        USA,

        /// <remarks/>
        URY,

        /// <remarks/>
        UZB,

        /// <remarks/>
        VUT,

        /// <remarks/>
        VAT,

        /// <remarks/>
        VEN,

        /// <remarks/>
        VNM,

        /// <remarks/>
        VGB,

        /// <remarks/>
        VIR,

        /// <remarks/>
        WLF,

        /// <remarks/>
        ESH,

        /// <remarks/>
        YEM,

        /// <remarks/>
        ZAR,

        /// <remarks/>
        ZMB,

        /// <remarks/>
        ZWE,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public partial class OverseasAddressType
    {

        private string addressField;

        private string postCodeAndTownField;

        private string provinceField;

        private CountryType countryCodeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Address
        {
            get
            {
                return this.addressField;
            }
            set
            {
                this.addressField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string PostCodeAndTown
        {
            get
            {
                return this.postCodeAndTownField;
            }
            set
            {
                this.postCodeAndTownField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Province
        {
            get
            {
                return this.provinceField;
            }
            set
            {
                this.provinceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public CountryType CountryCode
        {
            get
            {
                return this.countryCodeField;
            }
            set
            {
                this.countryCodeField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public partial class AdministrativeCentreType
    {

        private string centreCodeField;

        private RoleTypeCodeType roleTypeCodeField;

        private bool roleTypeCodeFieldSpecified;

        private string nameField;

        private string firstSurnameField;

        private string secondSurnameField;

        private object itemField;

        private ContactDetailsType contactDetailsField;

        private string physicalGLNField;

        private string logicalOperationalPointField;

        private string centreDescriptionField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string CentreCode
        {
            get
            {
                return this.centreCodeField;
            }
            set
            {
                this.centreCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public RoleTypeCodeType RoleTypeCode
        {
            get
            {
                return this.roleTypeCodeField;
            }
            set
            {
                this.roleTypeCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool RoleTypeCodeSpecified
        {
            get
            {
                return this.roleTypeCodeFieldSpecified;
            }
            set
            {
                this.roleTypeCodeFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string FirstSurname
        {
            get
            {
                return this.firstSurnameField;
            }
            set
            {
                this.firstSurnameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string SecondSurname
        {
            get
            {
                return this.secondSurnameField;
            }
            set
            {
                this.secondSurnameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("AddressInSpain", typeof(AddressType), Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlElementAttribute("OverseasAddress", typeof(OverseasAddressType), Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public object Item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public ContactDetailsType ContactDetails
        {
            get
            {
                return this.contactDetailsField;
            }
            set
            {
                this.contactDetailsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string PhysicalGLN
        {
            get
            {
                return this.physicalGLNField;
            }
            set
            {
                this.physicalGLNField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string LogicalOperationalPoint
        {
            get
            {
                return this.logicalOperationalPointField;
            }
            set
            {
                this.logicalOperationalPointField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string CentreDescription
        {
            get
            {
                return this.centreDescriptionField;
            }
            set
            {
                this.centreDescriptionField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public enum RoleTypeCodeType
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("01")]
        Item01,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("02")]
        Item02,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("03")]
        Item03,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("04")]
        Item04,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("05")]
        Item05,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("06")]
        Item06,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("07")]
        Item07,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("08")]
        Item08,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("09")]
        Item09,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public partial class ContactDetailsType
    {

        private string telephoneField;

        private string teleFaxField;

        private string webAddressField;

        private string electronicMailField;

        private string contactPersonsField;

        private string cnoCnaeField;

        private string iNETownCodeField;

        private string additionalContactDetailsField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Telephone
        {
            get
            {
                return this.telephoneField;
            }
            set
            {
                this.telephoneField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string TeleFax
        {
            get
            {
                return this.teleFaxField;
            }
            set
            {
                this.teleFaxField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string WebAddress
        {
            get
            {
                return this.webAddressField;
            }
            set
            {
                this.webAddressField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ElectronicMail
        {
            get
            {
                return this.electronicMailField;
            }
            set
            {
                this.electronicMailField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ContactPersons
        {
            get
            {
                return this.contactPersonsField;
            }
            set
            {
                this.contactPersonsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string CnoCnae
        {
            get
            {
                return this.cnoCnaeField;
            }
            set
            {
                this.cnoCnaeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string INETownCode
        {
            get
            {
                return this.iNETownCodeField;
            }
            set
            {
                this.iNETownCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string AdditionalContactDetails
        {
            get
            {
                return this.additionalContactDetailsField;
            }
            set
            {
                this.additionalContactDetailsField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public partial class BusinessType
    {

        private TaxIdentificationType taxIdentificationField;

        private string partyIdentificationField;

        private AdministrativeCentreType[] administrativeCentresField;

        private object itemField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public TaxIdentificationType TaxIdentification
        {
            get
            {
                return this.taxIdentificationField;
            }
            set
            {
                this.taxIdentificationField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string PartyIdentification
        {
            get
            {
                return this.partyIdentificationField;
            }
            set
            {
                this.partyIdentificationField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("AdministrativeCentre", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
        public AdministrativeCentreType[] AdministrativeCentres
        {
            get
            {
                return this.administrativeCentresField;
            }
            set
            {
                this.administrativeCentresField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Individual", typeof(IndividualType), Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlElementAttribute("LegalEntity", typeof(LegalEntityType), Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public object Item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public partial class IndividualType
    {

        private string nameField;

        private string firstSurnameField;

        private string secondSurnameField;

        private object itemField;

        private ContactDetailsType contactDetailsField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string FirstSurname
        {
            get
            {
                return this.firstSurnameField;
            }
            set
            {
                this.firstSurnameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string SecondSurname
        {
            get
            {
                return this.secondSurnameField;
            }
            set
            {
                this.secondSurnameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("AddressInSpain", typeof(AddressType), Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlElementAttribute("OverseasAddress", typeof(OverseasAddressType), Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public object Item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public ContactDetailsType ContactDetails
        {
            get
            {
                return this.contactDetailsField;
            }
            set
            {
                this.contactDetailsField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public partial class LegalEntityType
    {

        private string corporateNameField;

        private string tradeNameField;

        private RegistrationDataType registrationDataField;

        private object itemField;

        private ContactDetailsType contactDetailsField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string CorporateName
        {
            get
            {
                return this.corporateNameField;
            }
            set
            {
                this.corporateNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string TradeName
        {
            get
            {
                return this.tradeNameField;
            }
            set
            {
                this.tradeNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public RegistrationDataType RegistrationData
        {
            get
            {
                return this.registrationDataField;
            }
            set
            {
                this.registrationDataField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("AddressInSpain", typeof(AddressType), Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlElementAttribute("OverseasAddress", typeof(OverseasAddressType), Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public object Item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public ContactDetailsType ContactDetails
        {
            get
            {
                return this.contactDetailsField;
            }
            set
            {
                this.contactDetailsField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public partial class RegistrationDataType
    {

        private string bookField;

        private string registerOfCompaniesLocationField;

        private string sheetField;

        private string folioField;

        private string sectionField;

        private string volumeField;

        private string additionalRegistrationDataField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Book
        {
            get
            {
                return this.bookField;
            }
            set
            {
                this.bookField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string RegisterOfCompaniesLocation
        {
            get
            {
                return this.registerOfCompaniesLocationField;
            }
            set
            {
                this.registerOfCompaniesLocationField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Sheet
        {
            get
            {
                return this.sheetField;
            }
            set
            {
                this.sheetField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Folio
        {
            get
            {
                return this.folioField;
            }
            set
            {
                this.folioField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Section
        {
            get
            {
                return this.sectionField;
            }
            set
            {
                this.sectionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Volume
        {
            get
            {
                return this.volumeField;
            }
            set
            {
                this.volumeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string AdditionalRegistrationData
        {
            get
            {
                return this.additionalRegistrationDataField;
            }
            set
            {
                this.additionalRegistrationDataField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public partial class PartiesType
    {

        private BusinessType sellerPartyField;

        private BusinessType buyerPartyField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public BusinessType SellerParty
        {
            get
            {
                return this.sellerPartyField;
            }
            set
            {
                this.sellerPartyField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public BusinessType BuyerParty
        {
            get
            {
                return this.buyerPartyField;
            }
            set
            {
                this.buyerPartyField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public partial class RepositoryType
    {

        private string repositoryNameField;

        private string uRLField;

        private string referenceField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string RepositoryName
        {
            get
            {
                return this.repositoryNameField;
            }
            set
            {
                this.repositoryNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string URL
        {
            get
            {
                return this.uRLField;
            }
            set
            {
                this.uRLField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Reference
        {
            get
            {
                return this.referenceField;
            }
            set
            {
                this.referenceField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public partial class FactoringAssignmentDocumentType
    {

        private string documentCharacterField;

        private string representationIdentityField;

        private string documentTypeField;

        private RepositoryType repositoryField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string DocumentCharacter
        {
            get
            {
                return this.documentCharacterField;
            }
            set
            {
                this.documentCharacterField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string RepresentationIdentity
        {
            get
            {
                return this.representationIdentityField;
            }
            set
            {
                this.representationIdentityField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string DocumentType
        {
            get
            {
                return this.documentTypeField;
            }
            set
            {
                this.documentTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public RepositoryType Repository
        {
            get
            {
                return this.repositoryField;
            }
            set
            {
                this.repositoryField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public partial class AssigneeType
    {

        private TaxIdentificationType taxIdentificationField;

        private object itemField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public TaxIdentificationType TaxIdentification
        {
            get
            {
                return this.taxIdentificationField;
            }
            set
            {
                this.taxIdentificationField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Individual", typeof(IndividualType), Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlElementAttribute("LegalEntity", typeof(LegalEntityType), Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public object Item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public partial class FactoringAssignmentDataType
    {

        private AssigneeType assigneeField;

        private InstallmentType[] paymentDetailsField;

        private string factoringAssignmentClausesField;

        private FactoringAssignmentDocumentType[] factoringAssignmentDocumentField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public AssigneeType Assignee
        {
            get
            {
                return this.assigneeField;
            }
            set
            {
                this.assigneeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("Installment", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
        public InstallmentType[] PaymentDetails
        {
            get
            {
                return this.paymentDetailsField;
            }
            set
            {
                this.paymentDetailsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string FactoringAssignmentClauses
        {
            get
            {
                return this.factoringAssignmentClausesField;
            }
            set
            {
                this.factoringAssignmentClausesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("FactoringAssignmentDocument", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public FactoringAssignmentDocumentType[] FactoringAssignmentDocument
        {
            get
            {
                return this.factoringAssignmentDocumentField;
            }
            set
            {
                this.factoringAssignmentDocumentField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml")]
    public partial class BatchType
    {

        private string batchIdentifierField;

        private long invoicesCountField;

        private AmountType totalInvoicesAmountField;

        private AmountType totalOutstandingAmountField;

        private AmountType totalExecutableAmountField;

        private CurrencyCodeType invoiceCurrencyCodeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string BatchIdentifier
        {
            get
            {
                return this.batchIdentifierField;
            }
            set
            {
                this.batchIdentifierField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public long InvoicesCount
        {
            get
            {
                return this.invoicesCountField;
            }
            set
            {
                this.invoicesCountField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public AmountType TotalInvoicesAmount
        {
            get
            {
                return this.totalInvoicesAmountField;
            }
            set
            {
                this.totalInvoicesAmountField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public AmountType TotalOutstandingAmount
        {
            get
            {
                return this.totalOutstandingAmountField;
            }
            set
            {
                this.totalOutstandingAmountField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public AmountType TotalExecutableAmount
        {
            get
            {
                return this.totalExecutableAmountField;
            }
            set
            {
                this.totalExecutableAmountField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public CurrencyCodeType InvoiceCurrencyCode
        {
            get
            {
                return this.invoiceCurrencyCodeField;
            }
            set
            {
                this.invoiceCurrencyCodeField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    [System.Xml.Serialization.XmlRootAttribute("Transforms", Namespace = "http://www.w3.org/2000/09/xmldsig#", IsNullable = false)]
    public partial class TransformsType
    {

        private TransformType[] transformField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Transform")]
        public TransformType[] Transform
        {
            get
            {
                return this.transformField;
            }
            set
            {
                this.transformField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    [System.Xml.Serialization.XmlRootAttribute("Manifest", Namespace = "http://www.w3.org/2000/09/xmldsig#", IsNullable = false)]
    public partial class ManifestType
    {

        private ReferenceType[] referenceField;

        private string idField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Reference")]
        public ReferenceType[] Reference
        {
            get
            {
                return this.referenceField;
            }
            set
            {
                this.referenceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "ID")]
        public string Id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    [System.Xml.Serialization.XmlRootAttribute("SignatureProperties", Namespace = "http://www.w3.org/2000/09/xmldsig#", IsNullable = false)]
    public partial class SignaturePropertiesType
    {

        private SignaturePropertyType[] signaturePropertyField;

        private string idField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("SignatureProperty")]
        public SignaturePropertyType[] SignatureProperty
        {
            get
            {
                return this.signaturePropertyField;
            }
            set
            {
                this.signaturePropertyField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "ID")]
        public string Id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    [System.Xml.Serialization.XmlRootAttribute("SignatureProperty", Namespace = "http://www.w3.org/2000/09/xmldsig#", IsNullable = false)]
    public partial class SignaturePropertyType
    {

        private System.Xml.XmlElement[] itemsField;

        private string[] textField;

        private string targetField;

        private string idField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        public System.Xml.XmlElement[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string[] Text
        {
            get
            {
                return this.textField;
            }
            set
            {
                this.textField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "anyURI")]
        public string Target
        {
            get
            {
                return this.targetField;
            }
            set
            {
                this.targetField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "ID")]
        public string Id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }
    }
}
