using System;
using System.IO;
using System.Configuration;
using System.Security.Cryptography.X509Certificates;
using eFacturesCat;
using eFacturesCat.Commons;
using eFacturesCat.Transform;
using eFacturesCat.Secure;
using SamplesCommon;


namespace Facturae32Sign
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

            // Select Windows Store Certificate
            X509Certificate2 cert = null;
            cert = CertUtils.selectCertificateFromWindowsStore("Available signing certificates", "Select one");
            // You can also use a pkcs12 file certificate like
            // X509Certificate2 cert = new X509Certificate2(filename, password);		

            // if there are a certificate selected
            if (cert != null)
            {
                // Create Session
                s = new Session(cert);
                // Check Certificate (Optional)
                Response checkCert = s.checkCertificate(null);
                if (checkCert.result != Response.Error)
                {
                    if (checkCert.result == Response.Warning)
                        Console.WriteLine("WARNING: " + checkCert.description + "-" + checkCert.longDescription);
                    // Get Files to be processed
                    string[] invoicesPath = Directory.GetFiles(Tools.baseDir + "inbox", "*.xml");

                    // Process each file
                    foreach (string fileName in invoicesPath)
                    {
                        // Load unsigned invoice
                        Console.WriteLine("Processing " + fileName + "...");
                        Facturae_3_2 fe32 = new Facturae_3_2(fileName);

                        // Validate schema
                        fe32.deserialize();
                        if (fe32.isValidXml)
                        {
                            SecuredInvoice sFe32 = null;

                            // Session sign
                            Response dr = s.signInvoice(fe32, Constants.facturae32_EPES, out sFe32);

                            if (sFe32 != null)
                                sFe32.saveInvoiceSigned(Tools.getSignedFileName(fileName));

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
                            Console.WriteLine("ERROR XML doesn't validate schema: " + fe32.xmlErrorStr);
                            Tools.InvoiceNotProcessed(fileName);
                        }

                    }
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
