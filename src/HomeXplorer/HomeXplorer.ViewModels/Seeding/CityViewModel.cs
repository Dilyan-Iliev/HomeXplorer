namespace HomeXplorer.ViewModels.Seeding
{
    using System.Text.Json.Serialization;

    public class CityViewModel
    {
        [JsonPropertyName("Name")]
        public string Name { get; set; } = null!;

        [JsonPropertyName("CountryId")]
        public int CountryId { get; set; }
    }
}
