using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;

namespace eFacturesCat.Commons
{
    /// <summary>
    /// Class for Response to Methods
    /// </summary>
    /// <author>@santicasas</author>
    public class Response
    {
        public static int Correct = 0;
        public static int Warning = 1;
        public static int Error = 2;

        public static String CertificateError = "Certificate Error";
        public static String SigningError = "Signing Error";
        public static String ConnectError = "Connection Error";
        public static String MalformedRequest = "Malformed Request";
        public static String WrongInvoice = "Wrong Invoice Document";
        public static String AuthenticationFailure = "Authentication Failure";
        public static String ServiceUnavaliable = "Service Unavaliable";
        public static String UnknownError = "Unknown Error";

        public int result { get; protected set; }
        public String description { get; protected set; }
        public String longDescription { get; protected set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public Response()
        {
            setCorrect("correct", "Correct Execution");
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="res"></param>
        /// <param name="desc"></param>
        /// <param name="longDesc"></param>
        public Response(int res, string desc, string longDesc)
        {
            result = res;
            description = desc;
            longDescription = longDesc;
        }

        /// <summary>
        /// Set "correct" response
        /// </summary>
        /// <param name="desc">Description</param>
        /// <param name="longDesc">Long Description</param>
        public void setCorrect(String desc, String longDesc)
        {
            result = Response.Correct;
            description = desc;
            longDescription = longDesc;
        }

        /// <summary>
        /// Set "warning" response
        /// </summary>
        /// <param name="desc">Description</param>
        /// <param name="longDesc">Long description</param>
        public void setWarning(String desc, String longDesc)
        {
            result = Response.Warning;
            description = desc;
            longDescription = longDesc;
        }

        /// <summary>
        /// Set "error" response
        /// </summary>
        /// <param name="desc">Description</param>
        /// <param name="longDesc">Loing description</param>
        public void setError(String desc, String longDesc)
        {
            result = Response.Error;
            description = desc;
            longDescription = longDesc;
        }
    }
}
