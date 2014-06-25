using System;
using System.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Text;
using eFacturesCat;
using eFacturesCat.Commons;
using eFacturesCat.Transform;
using eFacturesCat.Secure;
using eFacturesCat.Deliver;
using eFacturesCat.Deliver.Pimefactura;
using System.Security.Cryptography.X509Certificates;
using SamplesCommon;

namespace PimefacturaSignAndDeliver
{
    /// <summary>
    /// Sample Class for signing with XAdES-EPES Enveloped and deliver to Pimefactura service "facturae 3.2" invoices
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

                    // Get Files to be processed
                    string[] invoicesPath = Directory.GetFiles(Tools.baseDir + "inbox", "*.xml");

                    // Process each file
                    foreach (string fileName in invoicesPath)
                    {

                        // Load unsigned invoice
                        Console.WriteLine("Processing " + fileName + "...");
                        Facturae_3_2 fe32 = new Facturae_3_2(fileName);
                        SecuredInvoice sFe32;

                        // Session sign and deliver
                        Response dr = s.signAndDeliver(fe32, Constants.facturae32_EPES, out sFe32);

                        // Save Signed Invoice (is not necessary)                        
                        if ((ConfigurationManager.AppSettings["saveSignedInvoice"] == "1") && sFe32!=null)
                            sFe32.saveInvoiceSigned(Tools.getSignedFileName(fileName));
                       
                        // Process Response
                        if (dr.result == DeliverResponse.Error)
                        {
                            Console.WriteLine("ERROR: " + dr.description + "-" + dr.longDescription);
                            Tools.InvoiceNotProcessed(fileName);
                        }
                        else
                        {
                            if (dr.result == DeliverResponse.Warning)
                                Console.WriteLine("WARNING: " + dr.description + "-" + dr.longDescription);
                            Tools.InvoiceProcessed(fileName);
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
