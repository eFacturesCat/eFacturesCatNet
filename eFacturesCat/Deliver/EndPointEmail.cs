using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Net.Mime;
using System.IO;

using eFacturesCat.Transform;

namespace eFacturesCat.Deliver
{
    /// <summary>
    /// Example Class to send eInvoices by email
    /// </summary>
    public class EndPointEmail:EndPoint
    {
        SmtpClient SmtpServer { set; get; }
        MailMessage mail;

        /// <summary>
        /// Constructor using an auth SSL smtp server
        /// </summary>
        /// <param name="hostname"></param>
        /// <param name="port"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public EndPointEmail(String hostname, int port, String username, String password)
        {            
            SmtpServer = new SmtpClient(hostname);
            SmtpServer.Port = port;
            SmtpServer.Credentials = new System.Net.NetworkCredential(username, password);
            SmtpServer.EnableSsl = true;
        }

        /// <summary>
        /// Create Message for sending mail
        /// </summary>
        /// <param name="from">from address</param>
        /// <param name="to">to address</param>
        /// <param name="subject">subject of the message</param>
        /// <param name="body">body of the message</param>
        public void createMessage(String from, String to, String subject, String body)
        {
            mail = new MailMessage();
            mail.From = new MailAddress(from);
            mail.To.Add(to);
            mail.Subject = subject;
            mail.Body = body;
        }

        /// <summary>
        /// Method overrides EndPoint abstrac.
        /// Attach de XMLInvoice to email and send message
        /// </summary>
        /// <param name="xmlInvoice">XmlInvoice to send</param>
        /// <returns>Results</returns>
        public override DeliverResponse deliverInvoice(XMLInvoice xmlInvoice)
        {
            DeliverResponse dr = new DeliverResponse();
            try
            {
                Attachment att = new Attachment(xmlInvoice.xmlInputStream.BaseStream, "Invoice.xml");
                mail.Attachments.Add(att);
                SmtpServer.Send(mail);
            }
            catch (Exception ex)
            {
                dr.setError(DeliverResponse.ConnectError, ex.Message);                
            }
            return dr;            
        }

        /// <summary>
        /// Method overrides EndPoint abstrac.
        /// Attach de XMLInvoice to email and send message
        /// </summary>
        /// <param name="xmlInvoice">XmlInvoice to send</param>
        /// <param name="invoiceType">Invoice type</param>
        /// <param name="invoiceVersion">Invoice version</param>
        /// <returns>Results</returns>
        public override DeliverResponse deliverInvoice(XMLInvoice xmlInvoice, String invoiceType, String invoiceVersion)
        {
            DeliverResponse dr = new DeliverResponse();
            try
            {
                Attachment att = new Attachment(xmlInvoice.xmlInputStream.BaseStream, "Invoice.xml");
                mail.Attachments.Add(att);
                SmtpServer.Send(mail);
            }
            catch (Exception ex)
            {
                dr.setError(DeliverResponse.ConnectError, ex.Message);
            }
            return dr;
        }

        /// <summary>
        /// Method that overrides close session to EndPoint
        /// </summary>
        public override void close()
        {
            SmtpServer = null;
        }
    }
}
