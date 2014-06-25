using System;
using System.IO;
using System.Configuration;
using eFacturesCat;
using eFacturesCat.Commons;
using eFacturesCat.Transform;
using eFacturesCat.Secure;
using eFacturesCat.Deliver;
using SamplesCommon;


namespace EmailDeliver
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

            // Get the smtp configuration
            string smtpServerHost = ConfigurationManager.AppSettings["smtpServerHost"];
            int smtpServerPort = Convert.ToInt32(ConfigurationManager.AppSettings["smtpServerPort"]);
            string smtpServerUser = ConfigurationManager.AppSettings["smtpServerUser"];
            string smtpServerPassword = ConfigurationManager.AppSettings["smtpServerPassword"];

            // Create Session with the EndPoint to SMTP Service 
            s = new Session(new EndPointEmail(smtpServerHost, smtpServerPort, smtpServerUser, smtpServerPassword));

            // Get Files to be processed
            string[] invoicesPath = Directory.GetFiles(Tools.baseDir + "signed", "*.xml");

            // Process each file
            foreach (string fileName in invoicesPath)
            {
                // Load unsigned invoice
                Console.WriteLine("Processing " + fileName + "...");
                SecuredInvoice sFe32 = new SecuredFacturae3_2(fileName);

                sFe32.xmlInvoiceSecured.deserialize(); // Deserialize to get email address
                ((EndPointEmail)s.sessionEndPoint).createMessage(ConfigurationManager.AppSettings["fromAddress"], sFe32.xmlInvoiceSecured.getBuyerEmail(), "Enviament de factura", "Trobarà adjunta la factura enviada");

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

            // Close Session
            s.close();

            Console.WriteLine("");
            Console.WriteLine("Finished. Press <ENTER> to exit.");
            Console.ReadLine();
        }
    }
}
