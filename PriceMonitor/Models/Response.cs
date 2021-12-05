using Newtonsoft.Json;

namespace PriceMonitor.Models
{
    public class Response
    {
        [JsonProperty("responseCode")]
        public string ResponseCode { get; set; }

        [JsonProperty("responseMessage")]
        public string Message { get; set; }
    }
}
