using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Box.Samples.AzureFunctions.Cognitive
{
    public partial class BoxEvent
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("additional_info")]
        public object[] AdditionalInfo { get; set; }

        [JsonProperty("created_by")]
        public EdBy CreatedBy { get; set; }

        [JsonProperty("trigger")]
        public string Trigger { get; set; }

        [JsonProperty("source")]
        public Source Source { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("webhook")]
        public Webhook Webhook { get; set; }
    }

    public class EdBy
    {
        [JsonProperty("login")]
        public string Login { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }

    public class Source
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("created_by")]
        public EdBy CreatedBy { get; set; }

        [JsonProperty("content_modified_at")]
        public string ContentModifiedAt { get; set; }

        [JsonProperty("content_created_at")]
        public string ContentCreatedAt { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("etag")]
        public string Etag { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("file_version")]
        public FileVersion FileVersion { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("purged_at")]
        public object PurgedAt { get; set; }

        [JsonProperty("modified_at")]
        public string ModifiedAt { get; set; }

        [JsonProperty("item_status")]
        public string ItemStatus { get; set; }

        [JsonProperty("modified_by")]
        public EdBy ModifiedBy { get; set; }

        [JsonProperty("parent")]
        public Parent Parent { get; set; }

        [JsonProperty("owned_by")]
        public EdBy OwnedBy { get; set; }

        [JsonProperty("path_collection")]
        public PathCollection PathCollection { get; set; }

        [JsonProperty("sha1")]
        public string Sha1 { get; set; }

        [JsonProperty("size")]
        public long Size { get; set; }

        [JsonProperty("sequence_id")]
        public string SequenceId { get; set; }

        [JsonProperty("shared_link")]
        public object SharedLink { get; set; }

        [JsonProperty("trashed_at")]
        public object TrashedAt { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }

    public class FileVersion
    {
        [JsonProperty("sha1")]
        public string Sha1 { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }

    public class Parent
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("sequence_id")]
        public string SequenceId { get; set; }

        [JsonProperty("etag")]
        public string Etag { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }

    public class PathCollection
    {
        [JsonProperty("entries")]
        public Entry[] Entries { get; set; }

        [JsonProperty("total_count")]
        public long TotalCount { get; set; }
    }

    public class Entry
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("sequence_id")]
        public string SequenceId { get; set; }

        [JsonProperty("etag")]
        public string Etag { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }

    public class Webhook
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }

    public partial class BoxEvent
    {
        public static BoxEvent FromJson(string json)
        {
            return JsonConvert.DeserializeObject<BoxEvent>(json, Converter.Settings);
        }
    }

    public static class Serialize
    {
        public static string ToJson(this BoxEvent self)
        {
            return JsonConvert.SerializeObject(self, Converter.Settings);
        }
    }

    public class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
        };
    }
}
