using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.ProjectOxford.Vision.Contract;

namespace Box.Samples.AzureFunctions.Cognitive
{
    public static class FileEnricher
    {
        [FunctionName("FileEnricher")]
        public static async Task<object> Run([HttpTrigger(WebHookType = "genericJson")]HttpRequestMessage req, TraceWriter log)
        {
            log.Info($"Box Webhook was triggered!");

            string jsonContent = await req.Content.ReadAsStringAsync();
            BoxEvent eventData = JsonConvert.DeserializeObject<BoxEvent>(jsonContent);

            //Validate webhook event payload
            var message = ValidateContent(eventData, log);

            if (!string.IsNullOrEmpty(message))
                return req.CreateErrorResponse(HttpStatusCode.BadRequest, message);

            //save webhook event and payload in Azure Cosmos DB
            await SaveEvent(eventData, jsonContent);

            //Configure and create Box object
            var box = new Box(eventData.CreatedBy.Id, log);

            //Get file from Box and analyze image with Cognitive Vision API to generate metadata
            var vision = new Vision(ConfigSettings.AzureComputerVisionApiKey);
            var analysisResult = await vision.Analyze(await box.GetFileAsync(eventData));
            log.Info($"Image analysis: {JsonConvert.SerializeObject(analysisResult) ?? "results empty"}");

            //Add metadats to file in Box
            var metadata = analysisResult.Tags.ToDictionary<Tag, string, object>(tag => tag.Name, tag => tag.Confidence.ToString(CultureInfo.InvariantCulture));
            var result = await box.AddMetaData(eventData.Source.Id, metadata);
            log.Info($"Add metadata results: {JsonConvert.SerializeObject(result)}");

            return req.CreateResponse(HttpStatusCode.OK, $"Successsfully analyzed and added metadata to file { eventData.Source.Name }");
        }

        private static string ValidateContent(BoxEvent eventData, TraceWriter log)
        {

            var message = new StringWriter();
            message.Write($"Webhook = {eventData.Webhook.Id}");

            // The event trigger: FILE.UPLOADED
            if (!eventData.Trigger.Equals("FILE.UPLOADED"))
            {
                return "statusCode: 400, body: 'FILE.UPLOADED event is expected' }";
            }

            message.Write($"trigger = {eventData.Trigger}");

            // The source that triggered the event: a file, folder, etc.
            if (eventData.Source == null)
            {
                return "statusCode: 400, body: 'Source is not present in the webhook event'";
            }

            message.Write($"source = {eventData.Source.Type}");
            message.Write($"id = {eventData.Source.Id}");
            message.Write($"name = {eventData.Source.Name ?? "unknown"}");

            //log the request content details
            log.Info($"Box event: { message} ");

            return null;
        }

        private static async Task SaveEvent(BoxEvent eventData, string payload)
        {
            var endpointUrl = ConfigSettings.CosmosDbEndpointUrl;
            var primaryKey = ConfigSettings.CosmosDbPrimaryKey;
            var databaseId = ConfigSettings.CosmosDbDatabaseId;
            var databaseCollection = ConfigSettings.CosmosDbDatabaseCollection;

            var client = new DocumentClient(new Uri(endpointUrl), primaryKey);
            await client.CreateDatabaseIfNotExistsAsync(new Database { Id = databaseId });
            var eventCollection = new DocumentCollection { Id = databaseCollection };
            eventCollection.PartitionKey.Paths.Add("/userId");

            await client.CreateDocumentCollectionIfNotExistsAsync(
                UriFactory.CreateDatabaseUri(databaseId),
                eventCollection,
                new RequestOptions { OfferThroughput = 2500 });

            await client.CreateDocumentAsync(
                UriFactory.CreateDocumentCollectionUri(databaseId, databaseCollection),
                new EventData
                {
                    FileId = eventData.Source.Id,
                    FileName = eventData.Source.Name,
                    FileEtag = eventData.Source.Etag,
                    FileSize = eventData.Source.Size,
                    UserId = eventData.CreatedBy.Id,
                    CreatedAt = eventData.Source.CreatedAt,
                    EventId = eventData.Id,
                    Data = payload
                });
        }
    }
}
