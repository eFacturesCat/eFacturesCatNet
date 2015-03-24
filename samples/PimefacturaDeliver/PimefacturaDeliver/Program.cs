using System;
using System.IO;
using System.Configuration;
using eFacturesCat;
using eFacturesCat.Commons;
using eFacturesCat.Transform;
using eFacturesCat.Secure;
using eFacturesCat.Deliver.Pimefactura;
using SamplesCommon;

/*
 * DEPRECATED - Uses the old SOAP web service. Use new REST web service instead. 
 */


namespace PimefacturaDeliver
{
    class Program
    {
        
        Session s;

        public static void Main(string[] args)
        {
            Program p = new Program();
            p.execute();
        }
        private void execute()
        {
            // Get baseDir from app configuration. All folders (inbox, processed, not_processed, signed) depends on it
            Tools.baseDir = ConfigurationManager.AppSettings["baseDir"];
            Console.WriteLine(Tools.baseDir);

            // Get the authentication key for the issuer to deliver to pimefactura service
            string ak = ConfigurationManager.AppSettings["ak"];

            // Create the EndPoint to Pimefactura Service (prepro environment)
            EndPointPimefactura epp = new EndPointPimefactura(ak, Constants.prepro_environment);
            // EndPointPimefactura epp = new EndPointPimefactura(ak, Constants.prod_environment);

            // Create Session
            s = new Session(epp);

            // Get Files to be processed
            string[] invoicesPath = Directory.GetFiles(Tools.baseDir + "signed", "*.xml");

            // Process each file
            foreach (string fileName in invoicesPath)
            {
                // Load unsigned invoice
                Console.WriteLine("Processing " + fileName + "...");
                SecuredInvoice sFe32 = new SecuredFacturae3_2(fileName);

                // Check Syntax
                sFe32.xmlInvoiceSecured.deserialize();
                if (sFe32.xmlInvoiceSecured.isValidXml)
                {
                    // Session Deliver
                    Response dr = s.Deliver(Constants.facturae32_EPES, sFe32);

                    // Process Response
                    if (dr.result == Response.Error)
                    {
                        Console.WriteLine("ERROR: " + dr.description + "-" + dr.longDescription);
                        Tools.InvoiceNotProcessed(fileName);
                    }
                    else
                    {
                        if (dr.result == Response.Warning)
                            Console.WriteLine("WARNING: " + dr.description + "-" + dr.longDescription);
                        Tools.InvoiceProcessed(fileName);
                    }
                }
                else
                {
                    Console.WriteLine("ERROR: FacturaeSyntaxError - " + sFe32.xmlInvoiceSecured.xmlErrorStr);
                    Tools.InvoiceNotProcessed(fileName);
                }
            }

            // Close Session
            s.close();

            Console.WriteLine("");
            Console.WriteLine("Finished. Press <ENTER> to exit.");
            Console.ReadLine();
        }
    }
}
