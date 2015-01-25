using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;

namespace eFacturesCat.Commons
{
    /// <summary>
    /// Constants class for eFacturesCat API
    /// </summary>
    public class Constants
    {
        public const String facturae31policy = "facturae31";
        public const String XAdES_EPES_Enveloped = "XAdES-EPES-Enveloped";
        public const String XAdES_XL_Enveloped = "XAdES-XL-Enveloped";
        public const String prod_environment = "prod";
        public const String prepro_environment = "prepro";
        public const string ROLE = "supplier";

        // Invoice Formats Supported
        public const string facturae32_EPES = "facturae32_EPES";

        //REST
        public const string REST_PROD_URIBASE = "http://www.pimefactura.com/";
        public const string REST_PREPRO_URIBASE = "http://localhost:80/";
        //public const string REST_PREPRO_URIBASE = "http://new.pimefactura.com/";
        public const string REST_INVOICETYPE = "facturae";
        public const string REST_INVOICETYPE_VERSION = "3.2";
        public const string REST_CERTID_DUMMY = "dummy";
        public const string REST_CERTPASS_DUMMY = "dummy";
        public const string REST_AUTORIZATION_PASS = "2d52737b3e94b99b0f6f5a58c4a42f81";
        public const string REST_PATH_UPLOADINVOICE = "uploadinvoice/";
        public const string REST_RESULT_OK = "0";

        
    }
}
