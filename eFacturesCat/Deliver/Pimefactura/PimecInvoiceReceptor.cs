using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Serialization;
using System.Xml;

namespace eFacturesCat.Deliver.Pimefactura
{
    /// WebSerivce Client for "pimefactura"
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name = "PimecInvoiceReceptorSoap11Binding", Namespace = "http://Service.Reception.Invoice.Pimec")]
    internal partial class PimecInvoiceReceptor : System.Web.Services.Protocols.SoapHttpClientProtocol
    {

        private System.Threading.SendOrPostCallback summitInvoiceOperationCompleted;

        /// <remarks/>
        //public PimecInvoiceReceptor()
        //{
        //    // by default, prepro
        //    this.Url = "http://new.pimefactura.com/axis2/services/PimecInvoiceReceptor.PimecInvoiceRecepto" +
        //        "HttpSoap11Endpoint/";
        //}

        /// <remarks/>
        public PimecInvoiceReceptor(string urlService)
        {
            this.Url = urlService +
                "HttpSoap12Endpoint/";
        }

        /// <remarks/>
        public event summitInvoiceCompletedEventHandler summitInvoiceCompleted;

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:summitInvoice", RequestNamespace = "http://Service.Reception.Invoice.Pimec", ResponseNamespace = "http://Service.Reception.Invoice.Pimec", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("return", IsNullable = true)]
        public string summitInvoice([System.Xml.Serialization.XmlElementAttribute(IsNullable = true)] XmlElement omRequest)
        {
            object[] results = this.Invoke("summitInvoice", new object[] {
                    omRequest});
            return ((string)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginsummitInvoice(XmlElement omRequest, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("summitInvoice", new XmlElement[] {
                    omRequest}, callback, asyncState);
        }

        /// <remarks/>
        public string EndsummitInvoice(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }

        /// <remarks/>
        public void summitInvoiceAsync(XmlElement omRequest)
        {
            this.summitInvoiceAsync(omRequest, null);
        }

        /// <remarks/>
        public void summitInvoiceAsync(XmlElement omRequest, object userState)
        {
            if ((this.summitInvoiceOperationCompleted == null))
            {
                this.summitInvoiceOperationCompleted = new System.Threading.SendOrPostCallback(this.OnsummitInvoiceOperationCompleted);
            }
            this.InvokeAsync("summitInvoice", new object[] {
                    omRequest}, this.summitInvoiceOperationCompleted, userState);
        }

        private void OnsummitInvoiceOperationCompleted(object arg)
        {
            if ((this.summitInvoiceCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.summitInvoiceCompleted(this, new summitInvoiceCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        public new void CancelAsync(object userState)
        {
            base.CancelAsync(userState);
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
    public delegate void summitInvoiceCompletedEventHandler(object sender, summitInvoiceCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class summitInvoiceCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal summitInvoiceCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
            base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public string Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }

}
