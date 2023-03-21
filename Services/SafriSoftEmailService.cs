using Microsoft.Exchange.WebServices.Data;
using Microsoft.Identity.Client;
using SafriSoftv1._3.Models;
using SafriSoftv1._3.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SafriSoftv1._3.Services
{
    public class SafriSoftEmailService
    {
        private string AppId { get; set; } 
        private string ClientSecret { get; set; }
        private string TenantId { get; set; } 

        public ExchangeService CreateEWSClient()
        {
            //var cca = ConfidentialClientApplicationBuilder
            //    .Create("5dc46e65-4d26-4f96-8c48-7a6aac24fa95")
            //    .WithClientSecret("2G08Q~QSSxbS2zQvY5lVen2m9aMYJpj-BewbObfS")
            //    .WithTenantId("299c2b4e-392b-4167-970d-c43fea846549")
            //    .Build();

            //var ewsScopes = new string[] { "https://outlook.office365.com/.default" };
            var ewsClient = new ExchangeService();

            var pcaOptions = new PublicClientApplicationOptions
            {
                ClientId = "5dc46e65-4d26-4f96-8c48-7a6aac24fa95",
                TenantId = "299c2b4e-392b-4167-970d-c43fea846549"
            };

            var pca = PublicClientApplicationBuilder
                .CreateWithApplicationOptions(pcaOptions).Build();

            // The permission scope required for EWS access
            var ewsScopes = new string[] { "https://outlook.office365.com/EWS.AccessAsUser.All" };


            try
            {
                // Make the interactive token request
                var authResult = pca.AcquireTokenInteractive(ewsScopes).ExecuteAsync();

                // Configure the ExchangeService with the access token
                ewsClient.Url = new Uri("https://outlook.office365.com/EWS/Exchange.asmx");
                ewsClient.Credentials = new OAuthCredentials(authResult.GetAwaiter().GetResult().AccessToken);
            }
            catch(Exception ex)
            {
                throw;
            }

            return ewsClient;
        }

        public Dictionary<bool,string> SendEWSEmail(ExchangeService ews, string subject, string body, string[] toRecipients, string[] ccReceipients)
        {
            var emailText = BuildEmailHeader();
            emailText.Append("<font style='text-align: left;color:#595a5c'>" + body + "<br/><br/>");
            emailText.Append("<font style='text-align: left;color:#595a5c'>Regards,<br/>");
            emailText.Append("<font style='text-align: left;color:#17a2b8'>SafriSoft.");

            EmailMessage message = new EmailMessage(ews);
            message.Subject = subject;
            message.Body = emailText.ToString();

            try
            {
                Dictionary<bool, string> result = new Dictionary<bool, string>();

                foreach (EmailAddress emailAddresses in ccReceipients)
                {
                    message.CcRecipients.Add(emailAddresses.Address.ToString());
                }

                foreach (EmailAddress emailAddresses in toRecipients)
                {
                    message.ToRecipients.Add(emailAddresses.Address.ToString());
                }

                message.SendAndSaveCopy();
                result.Add(true, "Email has been sent successfully");
                return result;
            }
            catch (Exception ex)
            {
                Dictionary<bool, string> result = new Dictionary<bool, string>();
                result.Add(false, ex.Message);
                return result;
            }            
        }

        public StringBuilder BuildEmailHeader()
        {
            var header = new StringBuilder();

            header.Append("<h1 style='color:#17a2b8;'>SafriSoft.</h1>");
            header.Append("<font style='color:#595a5c;text-align: left;'>Dear Client<br/><br/>");

            return header;
        }

        public bool SaveEmail(string subject, string body, string fromAddress, string[] toAddress, string[] toCcAddress)
        {
            SafriSoftDbContext SafriSoft = new SafriSoftDbContext();

            var email = new Email();

            bool success = false;

            try
            {
                email.Subject = subject;
                email.Body = body;
                email.FromAddress = fromAddress;
                email.ToAddress = string.Join(";", toAddress);
                email.CcAddress = string.Join(";", toCcAddress);
                email.EmailStatus = "Process";

                SafriSoft.Emails.Add(email);
                SafriSoft.SaveChanges();

                success = true;
            }
            catch (Exception ex)
            {
                success = false;
            }

            return success;
        }
    }
}