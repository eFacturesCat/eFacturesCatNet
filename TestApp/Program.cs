using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using eFacturesCat;
using eFacturesCat.Generate;
using eFacturesCat.Commons;
using eFacturesCat.Transform;
using eFacturesCat.Secure;
using eFacturesCat.Deliver;
using eFacturesCat.Deliver.Pimefactura;

using System.Security.Cryptography.X509Certificates;
using System.Xml.XPath;
using System.Xml.Xsl;
using System.Xml;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // For encodings
            Encoding iso = Encoding.GetEncoding("ISO-8859-1");
            Encoding utf8 = Encoding.UTF8;

            // *** Testing schemas
            /*
            string[] invoicesPath = Directory.GetFiles("C:\\tmp\\psd\\signed", "*.xml");
            foreach (string fileName in invoicesPath)
            {
                Facturae_3_2 fe32 = new Facturae_3_2(fileName);
                // Validate schema
                fe32.deserialize();
                if (fe32.isValidXml)
                    Console.WriteLine("Good!");
                else
                    Console.WriteLine("Wrong!");
            }

            // ***
            */


            //Invoice inv = new Invoice(TestConstants.eFacturesCatFileName);
            
            /*
            // Instance Invoice
            Invoice inv = new Invoice("EUR", "es", 6, 2, false); // Euros, español, 6 decimals on lines, 2 decimals on totals, not CreditNote
            //Invoice inv = new Invoice("EUR", "es", 6, 2, true); // Euros, español, 6 decimals on lines, 2 decimals on totals, CreditNote
            // If creditNote setCorrective is Mandatory
            //inv.setCorrectiveNode("FIND INVOICE", ReasonCodeType.Item03, ReasonDescriptionType.Fechaexpedición, new DateTime(2012, 1, 1), new DateTime(2012, 12, 31), CorrectionMethodType.Item01, CorrectionMethodDescriptionType.Rectificacióníntegra);

            // Set Invoice Header (SeriesCode, InvoiceNumber, IssueDate)
            inv.setInvoiceHeader("SC", DateTime.Now.ToString("yyyyMMddHHmmss"), new DateTime(2013,04,12));
            //inv.setInvoiceHeader("2012", "0401", new DateTime(2012, 04, 27)); //**VALIDATEDID**

            // Set Seller Party
            //PartyType sellerParty = inv.setSellerParty("ESA00000000", "Seller Entity Inc", "Small Street", "Barcelona", "08034", "BARCELONA","ES", null, "0000000");
            PartyType sellerParty = inv.setSellerPersonParty("ESA00000000", "John", "Smith", "Small Street", "Barcelona", "08034", "BARCELONA", "ES", null, "0000000");
            //PartyType sellerParty = inv.setSellerParty("ESB65750721", "VALIDATED ID, S.L.", "Pardal 6", "Terrassa", "08224", "BARCELONA", "ES", "invoices@validatedid.com", null); //**VALIDATEDID**

            // Set Buyer Party
            PartyType buyerParty = inv.setBuyerParty("ES12345678Z", "Buyer Entity Inc", "Big Street", "Barcelona", "08034", "BARCELONA", "ES", "me@sntc.eu", "Desc Centro Entrega", "CustomerID-1");
            buyerParty.PostalAddress = new AddressType();
            inv.createAddress(buyerParty.PostalAddress, "Postal Adress", "Postal Town", "08000", "BARCELONA", "ES");
            
            //PartyType buyerParty = inv.setBuyerParty("ESB61127122", "GERSOFT HISPANIA S.L.", "Pablo Iglesias 63 1o 3a", "MATARO", "08303", "BARCELONA", "ES", "hidalgo_rafa@hotmail.com", null, null); //**VALIDATEDID**
           
            //Before lines define global discount
            inv.setGlobalDiscount(2, "Descuento de pronto pago");
            // Add Lines
            //InvoiceLineType il1 = inv.addInvoiceLine("First description", 10, 115.50, "S", 0, "More info for first description");
            //InvoiceLineType il1 = inv.addInvoiceLine("First description", 20, 15.08, "S", 18, "More info for first description");
            InvoiceLineType il1 = inv.addInvoiceLine("First description", 175, 7.25, "S", 21, "More info for first description");
            //UnitsOfMessure form facturae3 codes (direct map)
            il1.InvoicedQuantity.unitCode = "03";
            il1.InvoicedQuantity.unitCodeListID = "ES:facturae3";
            //Albaran
            il1.Delivery = new DeliveryType[1];
            il1.Delivery[0] = new DeliveryType();
            il1.Delivery[0].ID = new IDType();
            il1.Delivery[0].ID.Value = "NumAlbaran";
            il1.Delivery[0].ActualDeliveryDate = new ActualDeliveryDateType();
            il1.Delivery[0].ActualDeliveryDate.Value = DateTime.Now;
            //Pedido
            il1.DocumentReference = new DocumentReferenceType[1];
            il1.DocumentReference[0] = new DocumentReferenceType();
            il1.DocumentReference[0].ID = new IDType();
            il1.DocumentReference[0].ID.Value = "NumPedido";
            InvoiceLineType il2 = inv.addInvoiceLine("2th description", 175, 7.25, "S", 21, null);
            //InvoiceLineType il2 = inv.addInvoiceLine("Second description", 260, 7.01, "S", 18, null);
            //InvoiceLineType il3 = inv.addInvoiceLine("Third description", 100, 6.48, "S", 18, null);
            //inv.addInvoiceLine("Line", 100, 18.19, "S", 18, null);
            //inv.addInvoiceLine("Line", 50, 18.19, "S", 18, null);
            //inv.addInvoiceLine("Line", 300, 6.67, "S", 18, null);
            //inv.addInvoiceLine("Line", 300, 14.45, "S", 18, null);
            //inv.addInvoiceLine("Line", 100, 14.45, "S", 18, null);
            //inv.addInvoiceLine("Line", 200, 14.45, "S", 18, null);
            //inv.addInvoiceLine("Line", 510, 6.52, "S", 18, null);
            //TODO: **** ENCODINGS ****
            //InvoiceLineType il1 = inv.addInvoiceLine("Primer pago consultoria solucion digitalizacion certificada", 1, 3240, "S", 18, null); //**VALIDATEDID**
            inv.closeLines();
            inv.calculateTotals();

            // PaymentsMeans and Terms
            inv.setPaymentMeans("31", "1234-1234-12-123456789", false, "desc forma de pago");

            //inv.setPaymentMeans("31", "0081-0016-13-0001944604", false); //**VALIDATEDID**

            inv.addPaymentTerm(true, 100, new DateTime(2012, 04, 27), null);

            inv.closePaymentTerms();

            //inv.setAdditionalInvoiceInfo("More info about this invoice");
            inv.addAdditionalInvoiceInfo("legal:NotaLegal");
            inv.addAdditionalInvoiceInfo("additional:AdditionalInfo p.e. Delivery Terms");

            // Get the xmlStreamReader for invoice;
            //String xmlEFacturesCat = inv.getStreamReader().ReadToEnd();
            //Console.WriteLine(xmlEFacturesCat);
            

            //Transform
            //load the Xml doc
            //StreamReader reader = EFacturesCat2Facturae32.TransformEFacturesCat2Facturae32(new StreamReader(TestConstants.eFacturesCatFileName));
            StreamReader reader = EFacturesCat2Facturae32.TransformEFacturesCat2Facturae32(inv.getStreamReader());
            //String xmlFe32 = reader.ReadToEnd();
            //Console.WriteLine(xmlFe32);
            
            //Try to sing and send
            X509Certificate2 cert = new X509Certificate2(TestConstants.pkcs12_fileName, TestConstants.pkcs12_password);
            //X509Certificate2 cert = CertUtils.selectCertificateFromWindowsStore("Certificats disponibles", "Seleccioni un certificat");
            SecuredFacturae3_2 sFe32 = new SecuredFacturae3_2(new Facturae_3_2(reader));
            //SecuredFacturae3_2 sFe32 = new SecuredFacturae3_2(new Facturae_3_2(TestConstants.fileName));
            sFe32.secureInvoice(cert, Constants.XAdES_EPES_Enveloped);
            //sFe32.saveInvoiceSigned(TestConstants.fileNameSigned);

            //String ValidatedID_ak = "5941fcefd748bffc7b2a7b8238ae9094"; //**VALIDATEDID**
            //EndPointPimefactura epp = new EndPointPimefactura(ValidatedID_ak, TestConstants.environment);
            //EndPointPimefactura epp = new EndPointPimefactura(ValidatedID_ak, Constants.prod_environment); //**VALIDATEDID*
            EndPointPimefactura epp = new EndPointPimefactura(TestConstants.AK_test, TestConstants.environment);
            //XMLInvoice invoice = new Facturae_3_2(TestConstants.fileNameSigned);
            //invoice.deserialize();
            DeliverInvoice di = new DeliverInvoice(sFe32.xmlInvoiceSecured, epp);
            DeliverResponse dr = di.deliverInvoice();
            Console.WriteLine("Result = " + dr.result + " " + dr.description + " " + dr.longDescription);


            Console.ReadLine();
           */
            
            // Test sign Invoice
            SecuredFacturae3_2 sFe32 = null;
            X509Certificate2 cert = new X509Certificate2(TestConstants.pkcs12_fileName, TestConstants.pkcs12_password);
            //X509Certificate2 cert = CertUtils.selectCertificateFromWindowsStore("Certificats disponibles", "Seleccioni un certificat");
            sFe32 = new SecuredFacturae3_2(new Facturae_3_2(TestConstants.fileName));
            sFe32.secureInvoice(cert, Constants.XAdES_EPES_Enveloped);
            sFe32.saveInvoiceSigned(TestConstants.fileNameSigned);
            Console.WriteLine("Invoice Signed");

            // Test send Invoice
            /*
            //EndPointPimefactura epp = new EndPointPimefactura(TestConstants.AK_test, TestConstants.environment);
            EndPointEmail epp = new EndPointEmail("smtp.gmail.com", 587, "efacturescat@santicasas.net","pimefactura");

            Session efSession = new Session(epp);

            XMLInvoice invoice = new Facturae_3_2(TestConstants.fileNameSigned);
            invoice.deserialize();
            epp.createMessage("efacturescat@santicasas.net", invoice.getBuyerEmail(), "Enviament de factura", "Trobarà adjunta la factura enviada");

            DeliverInvoice di = new DeliverInvoice(invoice, epp);
            DeliverResponse dr = di.deliverInvoice();
            Console.WriteLine("Result = " + dr.result + " " + dr.description + " " + dr.longDescription);
            */

            Console.ReadLine();
           


        }
    }
}
