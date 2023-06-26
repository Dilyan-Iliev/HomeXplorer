namespace HomeXplorer.Config.Google
{
    using System.Text.Json.Serialization;

    public class GoogleCaptchaResponse
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("score")]
        public double Score { get; set; }
    }
}
