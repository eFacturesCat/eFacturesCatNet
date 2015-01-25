using System;
using System.Collections.Generic;
using System.Text;

namespace eFacturesCat.Deliver.PimefacturaRest
{
    public class UploadSignatureDTO
    {
        public string invoicetype { get; set; }
        public string invoicetypeversion { get; set; }
        public string invoiceb64 { get; set; }
        public string certificateid { get; set; }
        public string certificatepassword { get; set; }
        public string channeloutid { get; set; }       
    }
}
