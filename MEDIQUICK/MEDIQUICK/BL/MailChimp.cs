using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MEDIQUICK.BL
{
    public class MailChimp
    {
        private readonly string _apiKey;
        private readonly string _apiUrl = "https://usX.api.mailchimp.com/3.0/campaigns";

        public MailChimp(string apiKey)
        {
            _apiKey = apiKey;
        }

        public async Task SendVerificationEmail(string email, string verificationLink)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);

                var content = new
                {
                    recipients = new { to = new[] { new { email_address = email } } },
                    type = "html",
                    content = $"<a href='{verificationLink}'>Click here to verify your email</a>",
                    subject = "Verify your email"
                };

                var jsonContent = JsonConvert.SerializeObject(content);
                var response = await client.PostAsync(_apiUrl, new StringContent(jsonContent, Encoding.UTF8, "application/json"));

                response.EnsureSuccessStatusCode();
            }
        }
    }
}
