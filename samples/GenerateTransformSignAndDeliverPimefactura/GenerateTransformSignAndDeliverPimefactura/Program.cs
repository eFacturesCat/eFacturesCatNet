using System;
using System.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Text;
using eFacturesCat;
using eFacturesCat.Generate;
using eFacturesCat.Commons;
using eFacturesCat.Transform;
using eFacturesCat.Secure;
using eFacturesCat.Deliver;
using eFacturesCat.Deliver.Pimefactura;
using System.Security.Cryptography.X509Certificates;
using SamplesCommon;

namespace GenerateTransformSignAndDeliverPimefactura
{
    /// <summary>
    /// Sample Class for Generate, Transform, Signing with XAdES-EPES Enveloped and deliver to Pimefactura service "facturae 3.2" invoices
    /// </summary>
    /// <remarks>eFactures.cat project</remarks>
    /// <author>@santicasas</author>
    class Program
    {
        Session s;

        static void Main(string[] args)
        {
            Program p = new Program();
            p.execute();
        }

        /// <summary>
        /// Excecute method from main
        /// </summary>
        private void execute()
        {
            // Get baseDir from app configuration. All folders (inbox, processed, not_processed, signed) depends on it
            Tools.baseDir = ConfigurationManager.AppSettings["baseDir"];

            // Get the authentication key for the issuer to deliver to pimefactura service
            string ak = ConfigurationManager.AppSettings["ak"];
            // Create the EndPoint to Pimefactura Service (prepro environment)
            EndPointPimefactura epp = new EndPointPimefactura(ak, Constants.prepro_environment);

            // Select Windows Store Certificate
            X509Certificate2 cert = null;
            cert = CertUtils.selectCertificateFromWindowsStore("Available signing certificates", "Select one");
            // You can also use a pkcs12 file certificate like
            // X509Certificate2 cert = new X509Certificate2(filename, password);

            // if there are a certificate selected
            if (cert != null)
            {
                // Create Session
                s = new Session(cert, epp);

                // Check Certificate (Optional)
                Response checkCert = s.checkCertificate(null);
                if (checkCert.result != Response.Error)
                {
                    if (checkCert.result == Response.Warning)
                        Console.WriteLine("WARNING: " + checkCert.description + "-" + checkCert.longDescription);

                    // ****
                    // Process Normal Invoice. The returned objects of methods could be modified from its basic form
                    // ****

                    Invoice inv = new Invoice("EUR", "es", 6, 2, false); // Euros, español, 6 decimals on lines, 2 decimals on totals, not CreditNote
                    // Set Invoice Header (SeriesCode, InvoiceNumber, CurrencyCode, IssueDate)
                    inv.setInvoiceHeader("SC", DateTime.Now.ToString("yyyyMMddHHmmss"), new DateTime(2012, 01, 12));
                    // Set SellerParty
                    PartyType sellerParty = inv.setSellerPersonParty("ESA00000000", "John", "Smith", "Small Street", "Barcelona", "08034", "BARCELONA", "ES", null, "0000000");
                    // Set Buyer Party
                    PartyType buyerParty = inv.setBuyerParty("ES12345678Z", "Buyer Entity Inc", "Big Street", "Barcelona", "08034", "BARCELONA", "ES", "me@sntc.eu", "0000000", "CustomerID-1");
                    // Set Buyer Party Delivery Address (**NEW**)
                    buyerParty.PostalAddress = new AddressType();
                    inv.createAddress(buyerParty.PostalAddress, "Postal Adress", "Postal Town", "08000", "BARCELONA", "ES");

                    //Before lines define global discount
                    inv.setGlobalDiscount(3, "Global Discount");

                    // Add Lines
                    InvoiceLineType il1 = inv.addInvoiceLine("First description", 10, 115.50, "S", 18, "More info for first description");

                    //UnitsOfMessure form facturae3 codes (direct map) (**NEW**)
                    il1.InvoicedQuantity.unitCode = "03";
                    il1.InvoicedQuantity.unitCodeListID = "ES:facturae3";
                    //DeliveryNote (**NEW**)
                    il1.Delivery = new DeliveryType[1];
                    il1.Delivery[0] = new DeliveryType();
                    il1.Delivery[0].ID = new IDType();
                    il1.Delivery[0].ID.Value = "NumAlbaran";
                    il1.Delivery[0].ActualDeliveryDate = new ActualDeliveryDateType();
                    il1.Delivery[0].ActualDeliveryDate.Value = DateTime.Now;
                    //Order (**NEW**)
                    il1.DocumentReference = new DocumentReferenceType[1];
                    il1.DocumentReference[0] = new DocumentReferenceType();
                    il1.DocumentReference[0].ID = new IDType();
                    il1.DocumentReference[0].ID.Value = "NumPedido";

                    InvoiceLineType il2 = inv.addInvoiceLine("Second description", 15, 95.50, "E", 0, null);
                    InvoiceLineType il3 = inv.addInvoiceLine("Third description", 1, 1195.50, "AA", 8, null);
                    inv.closeLines(); // Lines must be closed
                    // Calculate Totals
                    inv.calculateTotals();
                    // PaymentsMeans and Terms
                    inv.setPaymentMeans("31", "1234-1234-12-123456789", false, "PaymenMeans Description");
                    inv.addPaymentTerm(true, 100, new DateTime(2012, 04, 27), null);
                    inv.closePaymentTerms(); // PaymentTerms must be closed

                    //MoreInfo for Invoices (**NEW**)
                    inv.addAdditionalInvoiceInfo("legal:NotaLegal");
                    inv.addAdditionalInvoiceInfo("additional:AdditionalInfo p.e. Delivery Terms");

                    //Transform to facturae32
                    StreamReader reader = EFacturesCat2Facturae32.TransformEFacturesCat2Facturae32(inv.getStreamReader());
                    Console.WriteLine("Processing Normal Invoice...");
                    Facturae_3_2 fe32 = new Facturae_3_2(reader);

                    // Session sign and deliver
                    SecuredInvoice sFe32;
                    Response dr = s.signAndDeliver(fe32, Constants.facturae32_EPES, out sFe32);
                    // Process Response
                    if (dr.result == DeliverResponse.Error)
                    {
                        Console.WriteLine("ERROR: " + dr.description + "-" + dr.longDescription);
                    }
                    else
                    {
                        if (dr.result == DeliverResponse.Warning)
                            Console.WriteLine("WARNING: " + dr.description + "-" + dr.longDescription);
                    }
                    // ****
                    // End of Normal Invoice
                    // ****

                    // ****
                    // Process Credit Note Invoice. 
                    // ****

                    inv = new Invoice("EUR", "es", 6, 2, true); // Euros, español, 6 decimals on lines, 2 decimals on totals, CreditNote
                    // If creditNote setCorrective is Mandatory
                    inv.setCorrectiveNode("124332", ReasonCodeType.Item03, ReasonDescriptionType.Fechaexpedición, new DateTime(2012, 1, 1), new DateTime(2012, 12, 31), CorrectionMethodType.Item01, CorrectionMethodDescriptionType.Rectificacióníntegra);
                    // Set Invoice Header (SeriesCode, InvoiceNumber, CurrencyCode, IssueDate)
                    inv.setInvoiceHeader("SC", DateTime.Now.ToString("yyyyMMddHHmmss"), new DateTime(2012, 01, 12));
                    // Set SellerParty
                    sellerParty = inv.setSellerPersonParty("ESA00000000", "John", "Smith", "Small Street", "Barcelona", "08034", "BARCELONA", "ES", null, "0000000");
                    // Set Buyer Party
                    buyerParty = inv.setBuyerParty("ES12345678Z", "Buyer Entity Inc", "Big Street", "Barcelona", "08034", "BARCELONA", "ES", "me@sntc.eu", "0000000", "CustomerID-1");
                    //Before lines define global discount
                    inv.setGlobalDiscount(3, "Global Discount");
                    // Add Lines
                    il1 = inv.addInvoiceLine("First description", -10, 115.50, "S", 18, "More info for first description");
                    il2 = inv.addInvoiceLine("Second description", -15, 95.50, "S", 18, null);
                    il3 = inv.addInvoiceLine("Third description", -1, 1195.50, "AA", 8, null);
                    inv.closeLines(); // Lines must be closed
                    // Calculate Totals
                    inv.calculateTotals();
                    // PaymentsMeans and Terms
                    inv.setPaymentMeans("31", "1234-1234-12-123456789", false, "PaymenMeans Description");
                    inv.addPaymentTerm(true, 100, new DateTime(2012, 04, 27), null);
                    inv.closePaymentTerms(); // PaymentTerms must be closed

                    //Transform to facturae32
                    reader = EFacturesCat2Facturae32.TransformEFacturesCat2Facturae32(inv.getStreamReader());
                    Console.WriteLine("Processing CreditNote ...");
                    fe32 = new Facturae_3_2(reader);

                    // Session sign and deliver
                    sFe32 = null;
                    dr = s.signAndDeliver(fe32, Constants.facturae32_EPES, out sFe32);
                    // Process Response
                    if (dr.result == DeliverResponse.Error)
                    {
                        Console.WriteLine("ERROR: " + dr.description + "-" + dr.longDescription);
                    }
                    else
                    {
                        if (dr.result == DeliverResponse.Warning)
                            Console.WriteLine("WARNING: " + dr.description + "-" + dr.longDescription);
                    }
                    // ****
                    // End of Credit Note
                    // ****

                }
                else
                {
                    Console.WriteLine("ERROR: " + checkCert.description + "-" + checkCert.longDescription);
                }

                // Close Session
                s.close();
            }
            else
            {
                Console.WriteLine("No certificate selected");
            }
            Console.WriteLine("");
            Console.WriteLine("Finished. Press <ENTER> to exit.");
            Console.ReadLine();
        }
    }
}
