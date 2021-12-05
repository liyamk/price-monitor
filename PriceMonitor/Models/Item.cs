namespace PriceMonitor.Models
{
    using Newtonsoft.Json;

    public class Item
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("sku")]
        public int Sku { get; set; }

        [JsonProperty("targetPrice")]
        public double TargetPrice { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("price")]
        public double? Price { get; set; }

        [JsonProperty("targetAchieved")]
        public bool TargetAchieved { get; set; }

        [JsonProperty("createdDate")]
        public string CreatedDate { get; set; }

        [JsonProperty("modifiedDate")]
        public string ModifiedDate { get; set; }

        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string DocumentId { get; set; }
    }
}
