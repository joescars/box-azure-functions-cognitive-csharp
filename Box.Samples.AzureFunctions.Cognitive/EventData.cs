using Newtonsoft.Json;

namespace Box.Samples.AzureFunctions.Cognitive
{
    public class EventData
    {
        [JsonProperty("fileId")]
        public string FileId;

        [JsonProperty("fileName")]
        public string FileName;

        [JsonProperty("fileEtag")]
        public string FileEtag;

        [JsonProperty("fileSize")]
        public long FileSize;

        [JsonProperty("createdAt")]
        public string CreatedAt;

        [JsonProperty("userId")]
        public string UserId;

        [JsonProperty("eventId")]
        public string EventId;

        [JsonProperty("eventData")]
        public string Data;
    }
}