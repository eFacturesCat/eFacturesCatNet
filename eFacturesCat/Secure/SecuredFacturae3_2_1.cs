using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using eFacturesCat.Transform;
using eFacturesCat.Commons;
using System.Security.Cryptography.X509Certificates;

namespace eFacturesCat.Secure
{
    /// <summary>
    /// Spanish facturae 3.2 secured (signed invoice)
    /// </summary>
    public class SecuredFacturae3_2_1:SecuredInvoice
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="xmlInvoice">The facturae 3.2 invoice to be signed</param>
        public SecuredFacturae3_2_1(GlobalInvoice xmlInvoice) : base(xmlInvoice) { }

        /// <summary>
        /// Constructor from secured filename
        /// </summary>
        /// <param name="fileName">The fileName of secured invoice</param>
        public SecuredFacturae3_2_1(String fileName)
            : base(fileName)
        {
            this.xmlInvoiceSecured = new GlobalInvoice(fileName);
        }	    

        /// <summary>
        /// Secure (sign with XAdES-EPES) the invoice
        /// </summary>
        /// <param name="cert">Certificate (with private key) for signing</param>
        /// <param name="format">Signing format (Only Constants.XAdES_EPES_Enveloped) implemented.</param>
        public void secureInvoice(X509Certificate2 cert, String format)
        {
		    if (format==Constants.XAdES_EPES_Enveloped)
		    {
			    this.signInvoiceXadesEpesEnveloped(cert, Constants.facturae31policy);
		    } else {
			    throw new EFacturesException("Signature Format " + format + " not implemented yet.");
		    }			
	    }
    }
}
