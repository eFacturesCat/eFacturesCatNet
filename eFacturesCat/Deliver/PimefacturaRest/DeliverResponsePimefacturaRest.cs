using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Net;

namespace eFacturesCat.Deliver.PimefacturaRest
{
    class DeliverResponsePimefacturaRest: DeliverResponse
    {
        public void setResponse(HttpResponseMessage aResponse)
        {
            String aResponseMessage = aResponse.Content.ReadAsStringAsync().Result;

            if (aResponse.IsSuccessStatusCode)
                setCorrect("correct", "invoice succesful sent to PimeFacturaRest");
            else if (aResponse.StatusCode == HttpStatusCode.Forbidden)
                setError(AuthenticationFailure, aResponseMessage);
            else if (aResponse.StatusCode == HttpStatusCode.BadRequest)
                setError(MalformedRequest, aResponseMessage);
            else if (aResponse.StatusCode == HttpStatusCode.InternalServerError)
                setError(WrongInvoice, aResponseMessage);
            else if (aResponse.StatusCode == HttpStatusCode.ServiceUnavailable)
                setError(ServiceUnavaliable, aResponseMessage);
            else
                setError(UnknownError, aResponseMessage);                
        }

        public void setResponse(String sErrorMessage)
        {
            setError(UnknownError, sErrorMessage);                
        }
    }
}
