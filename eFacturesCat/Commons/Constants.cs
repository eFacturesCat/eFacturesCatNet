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
        public const string REST_PROD_URIBASE = "https://www.pimefactura.com/";
        //public const string REST_PREPRO_URIBASE = "http://localhost:80/";
        public const string REST_PREPRO_URIBASE = "http://new.pimefactura.com/";
        public const string INVOICE_TYPE_FACTURAE = "facturae";
        public const string INVOICE_TYPE_UBL = "UBL";
        public const string FACTURAE_VERSION_3_2 = "3.2";
        public const string FACTURAE_VERSION_3_2_1 = "3.2.1";
        public const string FACTURAE_VERSION_3_2_2 = "3.2.2";
        public const string UBL_VERSION_2_1 = "2.1";
        public const string REST_CERTID_DUMMY = "dummy";
        public const string REST_CERTPASS_DUMMY = "dummy";
        public const string REST_AUTORIZATION_PASS = "2d52737b3e94b99b0f6f5a58c4a42f81";
        public const string REST_PATH_UPLOADINVOICE = "uploadinvoice/";
        public const string REST_RESULT_OK = "0";
        public const string NAMESPACE_FACTURAE_3_2 = "http://www.facturae.es/Facturae/2009/v3.2/Facturae";
        public const string NAMESPACE_FACTURAE_3_2_1 = "http://www.facturae.es/Facturae/2014/v3.2.1/Facturae";
        public const string NAMESPACE_FACTURAE_3_2_2 = "http://www.facturae.gob.es/formato/Versiones/Facturaev3_2_2.xml";
        public const string NAMESPACE_UBL_2_1 = "urn:oasis:names:specification:ubl:schema:xsd:Invoice-2";

        
    }
}
