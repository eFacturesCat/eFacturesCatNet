using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using eFacturesCat.Deliver;

namespace eFacturesCat.Deliver.Pimefactura
{
    /// <summary>
    /// Deliver Response for Pimefactura Service
    /// </summary>
    public class DeliverResponsePimefactura:DeliverResponse
    {
        private static String PF_SUCCESS = "0";
        private static String PF_NULLFACTURASNODE = "NullFacturasNode";        
        private static String PF_NULLFACTURANODE = "NullFacturaNode";
        private static String PF_NULLBASE64NODE = "NullBase64Node";
        private static String PF_NULLPARAM1 = "NullParam1";
        private static String PF_NULLAKNODE = "NullAkNode";
        private static String PF_INCORRECTSCHEMA = "IncorrectSchema";
        private static String PF_INCORRECTSEMANTIC = "IncorrectSemantic";
        private static String PF_AKWRONGVALIDATION = "AKWrongValidation";
        private static String PF_INVOICEALREADYEXIST ="InvoiceAlreadyExist";

        /// <summary>
        /// Parse Pimefactura String response and set properties for DeliverResponse Object
        /// </summary>
        /// <param name="pfResponse">Pimefactura String Response</param>
        public void setResponse(String pfResponse)
        {
            if (pfResponse == PF_SUCCESS) setCorrect("correct", "invoice succesful sent to PimeFactura");
            else
                if ((pfResponse == PF_NULLAKNODE) || (pfResponse == PF_NULLBASE64NODE) ||
                    (pfResponse == PF_NULLFACTURANODE) || (pfResponse == PF_NULLFACTURASNODE) || (pfResponse == PF_NULLPARAM1))
                    setError(MalformedRequest, pfResponse);
                else
                    if ((pfResponse == PF_INCORRECTSCHEMA) || (pfResponse == PF_INCORRECTSEMANTIC) || (pfResponse == PF_INVOICEALREADYEXIST))
                        setError(WrongInvoice, pfResponse);
                    else if (pfResponse == PF_AKWRONGVALIDATION) setError(AuthenticationFailure, pfResponse);
                    else setError(UnknownError, pfResponse);
        }

    }

}
