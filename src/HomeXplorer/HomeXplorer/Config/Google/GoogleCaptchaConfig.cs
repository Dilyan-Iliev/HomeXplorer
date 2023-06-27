namespace HomeXplorer.Config.Google
{
    using Microsoft.Extensions.Options;
    using Newtonsoft.Json;
    using System.Net;

    public class GoogleCaptchaConfig
    {
        private readonly IOptionsMonitor<GoogleCaptchaSettings> config;

        public GoogleCaptchaConfig(IOptionsMonitor<GoogleCaptchaSettings> config)
        {
            this.config = config;
        }

        public async Task<bool> VerifyToken(string token)
        {
            try
            {
                var url = $"https://www.google.com/recaptcha/api/siteverify?secret={config.CurrentValue.SecretKey}&response={token}";

                using (var client = new HttpClient())
                {
                    var httpResult = await client.GetAsync(url);

                    if (httpResult.StatusCode != HttpStatusCode.OK)
                    {
                        return false;
                    }

                    var responseString = await httpResult.Content.ReadAsStringAsync();

                    var googleResult = JsonConvert.DeserializeObject<GoogleCaptchaResponse>(responseString);

                    return googleResult.Success && googleResult.Score >= 0.5;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
