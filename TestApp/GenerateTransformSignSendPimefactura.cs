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
    class GenerateTransformSignSendPimefactura
    {
        static void Main(string[] args)
        {
            String BuyerEmail = "me@sntc.eu";
            String PimefacturaActivationKey = TestConstants.AK_test;
            String PimefacturaEnvironment = TestConstants.environment;
            String P12FileName = TestConstants.pkcs12_fileName;
            String P12PassWord = TestConstants.pkcs12_password;
             
            DateTime invoiceDate = DateTime.Now.Date;
            
            
            // Instance Invoice
            Invoice inv = new Invoice("EUR", "es", 6, 2, false); // Euros, español, 6 decimals on lines, 2 decimals on totals, not CreditNote

            // Set Invoice Header (SeriesCode, InvoiceNumber, IssueDate)
            inv.setInvoiceHeader("SC", DateTime.Now.ToString("yyyyMMddHHmmss"), invoiceDate);

            // Set Seller Party
            PartyType sellerParty = inv.setSellerParty("ESA00000000", "Seller Entity Inc", "Small Street", "Barcelona", "08034", "BARCELONA","ES", null, "0000000");

            // Set Buyer Party
            PartyType buyerParty = inv.setBuyerParty("ES12345678Z", "Buyer Entity Inc", "Big Street", "Barcelona", "08034", "BARCELONA", "ES", BuyerEmail, "Desc Centro Entrega", "CustomerID-1");
            buyerParty.PostalAddress = new AddressType();
            inv.createAddress(buyerParty.PostalAddress, "Postal Adress", "Postal Town", "08000", "BARCELONA", "ES");
                       
            //Before lines define global discount
            //inv.setGlobalDiscount(2, "Descuento de pronto pago");

            // Add Lines
            InvoiceLineType il1 = inv.addInvoiceLine("First description", 25, 11.14, "S", 21, "More info for first description");
            //Optionals
                //UnitsOfMessure form facturae3 codes (direct map)
                il1.InvoicedQuantity.unitCode = "03";
                il1.InvoicedQuantity.unitCodeListID = "ES:facturae3";
                //DeliveryNote
                il1.Delivery = new DeliveryType[1];
                il1.Delivery[0] = new DeliveryType();
                il1.Delivery[0].ID = new IDType();
                il1.Delivery[0].ID.Value = "NumAlbaran";
                il1.Delivery[0].ActualDeliveryDate = new ActualDeliveryDateType();
                il1.Delivery[0].ActualDeliveryDate.Value = DateTime.Now;
                //Order
                il1.DocumentReference = new DocumentReferenceType[1];
                il1.DocumentReference[0] = new DocumentReferenceType();
                il1.DocumentReference[0].ID = new IDType();
                il1.DocumentReference[0].ID.Value = "NumPedido";

            InvoiceLineType il2 = inv.addInvoiceLine("Second description", 200, 4.87, "S", 21, null);
            //InvoiceLineType il3 = inv.addInvoiceLine("Third description", 100, 6.48, "S", 21, null);
            inv.closeLines();
            inv.calculateTotals();

            //If you want to set totals you can
            //inv.inv.TaxTotal[0].TaxAmount.Value = 263.03m;
            //inv.inv.TaxTotal[0].TaxSubtotal[0].TaxAmount.Value = 263.03m;
            //inv.inv.LegalMonetaryTotal.TaxInclusiveAmount.Value = 1515.53m;
            //inv.inv.LegalMonetaryTotal.PayableAmount.Value = 1515.53m;

            // PaymentsMeans and Terms
            inv.setPaymentMeans("31", "1234-1234-12-123456789", false, "desc forma de pago");

            inv.addPaymentTerm(true, 100, invoiceDate.AddDays(30), null);

            inv.closePaymentTerms();


            inv.addAdditionalInvoiceInfo("legal:NotaLegal");
            inv.addAdditionalInvoiceInfo("additional:AdditionalInfo p.e. Delivery Terms");
            

            //Transform
            //load the Xml doc
            StreamReader reader = EFacturesCat2Facturae32.TransformEFacturesCat2Facturae32(inv.getStreamReader());
            
            //Try to sing and send
            X509Certificate2 cert = new X509Certificate2(P12FileName, P12PassWord);
            SecuredFacturae3_2 sFe32 = new SecuredFacturae3_2(new Facturae_3_2(reader));
            sFe32.secureInvoice(cert, Constants.XAdES_EPES_Enveloped);
            //sFe32.saveInvoiceSigned(TestConstants.fileNameSigned);

            EndPointPimefactura epp = new EndPointPimefactura(PimefacturaActivationKey, PimefacturaEnvironment);

            DeliverInvoice di = new DeliverInvoice(sFe32.xmlInvoiceSecured, epp);
            DeliverResponse dr = di.deliverInvoice();
            Console.WriteLine("Result = " + dr.result + " " + dr.description + " " + dr.longDescription);


            Console.ReadLine();

        }
    }
}
