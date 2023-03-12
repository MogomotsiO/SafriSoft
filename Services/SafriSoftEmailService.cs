using Microsoft.Exchange.WebServices.Data;
using Microsoft.Identity.Client;
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
        private string AppId { get; set; } = "5dc46e65-4d26-4f96-8c48-7a6aac24fa95";
        private string ClientSecret { get; set; } = "2G08Q~QSSxbS2zQvY5lVen2m9aMYJpj-BewbObfS";
        private string TenantId { get; set; } = "299c2b4e-392b-4167-970d-c43fea846549";

        public ExchangeService CreateEWSClient()
        {
            var cca = ConfidentialClientApplicationBuilder
                .Create(AppId)
                .WithClientSecret(ClientSecret)
                .WithTenantId(TenantId)
                .Build();

            var ewsScopes = new string[] { "https://outlook.office365.com/.default" };

            try
            {
                var authResult = cca.AcquireTokenForClient(ewsScopes)
                    .ExecuteAsync();

                // Configure the ExchangeService with the access token
                var ewsClient = new ExchangeService();
                ewsClient.Url = new Uri("https://outlook.office365.com/EWS/Exchange.asmx");
                ewsClient.Credentials = new OAuthCredentials(authResult.GetAwaiter().GetResult().AccessToken);
                ewsClient.ImpersonatedUserId =
                    new ImpersonatedUserId(ConnectingIdType.SmtpAddress, "support@safrisoft.com");

                //Include x-anchormailbox header
                ewsClient.HttpHeaders.Add("X-AnchorMailbox", "support@safrisoft.com");

                return ewsClient;
            }
            catch(Exception ex)
            {
                return new ExchangeService();
            }
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
    }
}