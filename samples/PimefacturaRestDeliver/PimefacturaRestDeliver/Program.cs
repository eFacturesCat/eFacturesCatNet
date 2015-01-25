using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eFacturesCat.Deliver.Pimefactura;
using eFacturesCat.Commons;
using System.Security.Cryptography.X509Certificates;
using eFacturesCat.Secure;
using eFacturesCat;
using System.IO;
using eFacturesCat.Transform;
using eFacturesCat.Deliver;
using eFacturesCat.Deliver.PimefacturaRest;

namespace PimefacturaRestDeliver
{
    class Program
    {
        //Ser parameters
        static String sBasePath = "c:/tmp/efacturescat/";
        static String sInvoiceFile = "myInvoice.xml";
        static String sCertFile = "CertAutonomoIvan.pfx";
        static String sCertPass = "****";
        static String ak = "73352031754535436142623350332036413142314c366944643849317231493075322032703863446f344c436e387232503441416e396f32453334413b393435422d315073632d65322d3b3a524d3a336541";                            

        static void Main(string[] args)
        {
            signAndDeliverRest();
            //DeliverRestCloudSignature();
            //signAndDeliverSOAP();
        }

        public static void signAndDeliverSOAP()
        {
            // Load Cert
            X509Certificate2 cert = new X509Certificate2(sBasePath + sCertFile, sCertPass);
            // Create the EndPoint
            EndPointPimefactura epp = new EndPointPimefactura(ak, Constants.prepro_environment);            
            // Create Session
            Session s = new Session(cert, epp);
            Facturae_3_2 fe32 = new Facturae_3_2(sBasePath + sInvoiceFile);
            SecuredInvoice sFe32;
            Response dr = s.signAndDeliver(fe32, Constants.facturae32_EPES, out sFe32);
            if (dr.result != DeliverResponse.Correct)
            {
                throw new Exception("Error processing invoice");
            }
            // Close Session
            s.close();
        }

        public static void signAndDeliverRest()
        {
            // Load Cert
            X509Certificate2 cert = new X509Certificate2(sBasePath + sCertFile, sCertPass);
            // Create the EndPoint
            EndPointPimefacturaRest epp = new EndPointPimefacturaRest(ak, EndPointPimefacturaRest.RestEnvironment.PREPRO);
            epp.setChannelOut("merda");
            // Create Session
            Session s = new Session(cert, epp);
            Facturae_3_2 fe32 = new Facturae_3_2(sBasePath + sInvoiceFile);
            SecuredInvoice sFe32;
            Response dr = s.signAndDeliver(fe32, Constants.facturae32_EPES, out sFe32);           
            if (dr.result != DeliverResponse.Correct)
            {
                throw new Exception("Error processing invoice");
            }
            // Close Session
            s.close();
        }

        public static void DeliverRestCloudSignature()
        {
            // Load Cert
            X509Certificate2 cert = new X509Certificate2(sBasePath + sCertFile, sCertPass);
            // Create the EndPoint
            EndPointPimefacturaRest epp = new EndPointPimefacturaRest(ak, EndPointPimefacturaRest.RestEnvironment.PREPRO);
            epp.setSigningCertificate("factura-sw", "189CBA9");
            // Create Session
            Session s = new Session(cert, epp);            
            SecuredInvoice sFe32 = new SecuredFacturae3_2(sBasePath + sInvoiceFile);
            Response dr = s.Deliver(Constants.facturae32_EPES, sFe32);
            if (dr.result != DeliverResponse.Correct)
            {
                throw new Exception("Error processing invoice");
            }
            // Close Session
            s.close();
        }
    }    
}
