﻿using System;
using System.Collections.Generic;
using System.Text;
using eFacturesCat.Commons;
using eFacturesCat.Transform;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;
using System.IO;

namespace eFacturesCat.Deliver.PimefacturaRest
{
    public class EndPointPimefacturaRest : EndPoint
    {
        String m_sAK;             
        String m_sCertId;
        String m_sCertPass;
        String m_sCannelOut;
        RestEnvironment m_sRestEnv;
        HttpClient m_aHttpClient;   

        public enum RestEnvironment
        {
            PREPRO,
            PROD
        };


       
        public EndPointPimefacturaRest(String AK, RestEnvironment aRestEnv)
        {
            m_sAK = AK;
            m_sRestEnv = aRestEnv;
            //m_aHttpClient = new HttpClient();
            
            ////Set baseURL
            //if (aRestEnv == RestEnvironment.PREPRO)
            //    m_aHttpClient.BaseAddress = new Uri(Constants.REST_PREPRO_URIBASE);                
            //else
            //    m_aHttpClient.BaseAddress = new Uri(Constants.REST_PROD_URIBASE);                
        }

        public void setSigningCertificate(String sCertId, String sCertPass)
        {
            m_sCertId = sCertId;
            m_sCertPass = sCertPass;
        }

        public void setChannelOut(String sChannelOut)
        {
            m_sCannelOut = sChannelOut;            
        }


        private UploadSignatureDTO buildUploadSignatureDTO(String sInvoiceB64, String sInvoiceType, String sInvoiceVersion)
        {
            UploadSignatureDTO aUploadSignatureDTO = new UploadSignatureDTO();
            aUploadSignatureDTO.invoiceb64 = sInvoiceB64;
            aUploadSignatureDTO.invoicetype = sInvoiceType;
            aUploadSignatureDTO.invoicetypeversion = sInvoiceVersion;
            aUploadSignatureDTO.outchannelid = m_sCannelOut;
            if (String.IsNullOrEmpty(m_sCertId))
                aUploadSignatureDTO.certificateid = Constants.REST_CERTID_DUMMY;
            else
                aUploadSignatureDTO.certificateid = m_sCertId;
            if (String.IsNullOrEmpty(m_sCertPass))
                aUploadSignatureDTO.certificatepassword = Constants.REST_CERTPASS_DUMMY;
            else
                aUploadSignatureDTO.certificatepassword = m_sCertPass;
            


            return aUploadSignatureDTO;
        }

        public override DeliverResponse deliverInvoice(XMLInvoice xmlInvoice, String sInvoiceType, String sInvoiceVersion)
        {
            DeliverResponsePimefacturaRest aDeliverResponse = new DeliverResponsePimefacturaRest();
            try
            {
                int retries = 5;
                do
                {
                    m_aHttpClient = new HttpClient();
                    UploadSignatureDTO aUploadSignatureDTO = buildUploadSignatureDTO(Convert.ToBase64String(xmlInvoice.toByteArray()), sInvoiceType, sInvoiceVersion);
                    //Set baseURL
                    if (m_sRestEnv == RestEnvironment.PREPRO)
                        m_aHttpClient.BaseAddress = new Uri(Constants.REST_PREPRO_URIBASE);
                    else
                        m_aHttpClient.BaseAddress = new Uri(Constants.REST_PROD_URIBASE);
                    m_aHttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(m_sAK + ":" + Constants.REST_AUTORIZATION_PASS)));
                    HttpResponseMessage response = m_aHttpClient.PutAsJsonAsync<UploadSignatureDTO>(Constants.REST_PATH_UPLOADINVOICE, aUploadSignatureDTO).Result;

                    aDeliverResponse.setResponse(response);
                    if (response.StatusCode != HttpStatusCode.ServiceUnavailable)
                    {
                        retries = 0;
                    }
                    else
                    {
                        retries--;
                        System.Threading.Thread.Sleep(1000);
                    }
                    m_aHttpClient.Dispose();
                } while (retries > 0);

                return aDeliverResponse;
                
            }
            catch (Exception ex)
            {
                aDeliverResponse.setResponse(ex.Message);

                return aDeliverResponse;
            }            
        }

        public override DeliverResponse deliverInvoice(XMLInvoice xmlInvoice)
        {
            DeliverResponsePimefacturaRest aDeliverResponse = new DeliverResponsePimefacturaRest();
            try
            {
                int retries = 5;
                do
                {
                    m_aHttpClient = new HttpClient();
                    UploadSignatureDTO aUploadSignatureDTO = buildUploadSignatureDTO(Convert.ToBase64String(xmlInvoice.toByteArray()), Constants.INVOICE_TYPE_FACTURAE, Constants.FACTURAE_VERSION_3_2);
                    //Set baseURL
                    if (m_sRestEnv == RestEnvironment.PREPRO)
                        m_aHttpClient.BaseAddress = new Uri(Constants.REST_PREPRO_URIBASE);
                    else
                        m_aHttpClient.BaseAddress = new Uri(Constants.REST_PROD_URIBASE);
                    m_aHttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(m_sAK + ":" + Constants.REST_AUTORIZATION_PASS)));
                    HttpResponseMessage response = m_aHttpClient.PutAsJsonAsync<UploadSignatureDTO>(Constants.REST_PATH_UPLOADINVOICE, aUploadSignatureDTO).Result;

                    aDeliverResponse.setResponse(response);
                    if (response.StatusCode != HttpStatusCode.ServiceUnavailable)
                    {
                        retries = 0;
                    }
                    else
                    {
                        retries--;
                        System.Threading.Thread.Sleep(1000);
                    }
                    m_aHttpClient.Dispose();
                } while (retries > 0);

                return aDeliverResponse;

            }
            catch (Exception ex)
            {
                aDeliverResponse.setResponse(ex.Message);

                return aDeliverResponse;
            }
        }

        private String manageResponse(HttpResponseMessage aResponse)
        {
            String sResponseResult = "";

            if (aResponse.IsSuccessStatusCode)
                sResponseResult = Constants.REST_RESULT_OK;

            return sResponseResult;
            
        }


        public override void close()
        {
            m_sAK = null;
            m_sCertId = null;
            m_sCertPass = null;
            m_aHttpClient = null;
            
        }


       
    }
}
