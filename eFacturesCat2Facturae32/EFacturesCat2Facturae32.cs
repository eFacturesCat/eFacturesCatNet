using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.XPath;
using System.IO;
using System.Xml.Xsl;
using System.Xml;
using System.Reflection;

namespace eFacturesCat.Transform
{
    public static class EFacturesCat2Facturae32
    {
        private static String xsltResoureName = "eFacturesCat2Facturae32.Resources.eFacturesCatUBL2_1Facturae3_2.xsl";
        public static StreamReader TransformEFacturesCat2Facturae32(StreamReader fromStream)
        {
            XmlReader myFromXmlDoc = XmlReader.Create(fromStream);

            XslCompiledTransform myXslTrans = new XslCompiledTransform();

            //Permit document() function in XSLT
            XsltSettings xsltSettings = new XsltSettings(true,false);

            // embedded XSLT (includes and imports also embeddeds)
            Assembly assembly = Assembly.GetAssembly(typeof(EFacturesCat2Facturae32));
            EmbeddedResourceResolver resolver = new EmbeddedResourceResolver(assembly);

            // Load XSLT
            myXslTrans.Load(xsltResoureName, xsltSettings, resolver);

            //create the output stream
            MemoryStream myWriter = new MemoryStream();
            
            XmlTextWriter writer = new XmlTextWriter(myWriter, Encoding.UTF8);
            writer.Formatting = Formatting.Indented;
            writer.Indentation = 4;
            writer.WriteStartDocument();
            myXslTrans.Transform(myFromXmlDoc, null, writer, resolver);
            myWriter.Position = 0;
            return new StreamReader(myWriter);
        }
    }

    public class EmbeddedResourceResolver : XmlUrlResolver
    {
        private Assembly _assembly;

        public EmbeddedResourceResolver()
        {
            _assembly = Assembly.GetExecutingAssembly();
        }

        public EmbeddedResourceResolver(Assembly assembly)
        { _assembly = assembly; }

        public override object GetEntity(Uri absoluteUri, string role, Type ofObjectToReturn)
        {
            return _assembly.GetManifestResourceStream(absoluteUri.Segments[absoluteUri.Segments.Length - 1]);
        }
    }
}
