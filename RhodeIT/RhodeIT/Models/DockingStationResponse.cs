using Newtonsoft.Json;

namespace RhodeIT.Models
{
    public class DockingStationResponse
    {
        [JsonProperty("msg")]
        public string Message { get; set; }
        [JsonProperty("tReceipt")]
        public string TransactionReceipt { get; set; }
        [JsonProperty("error")]
        public bool Error { get; set; }
    }
}
