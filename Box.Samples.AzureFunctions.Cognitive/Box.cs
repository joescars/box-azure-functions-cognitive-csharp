using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Box.V2;
using Box.V2.Config;
using Box.V2.Exceptions;
using Box.V2.JWTAuth;
using Microsoft.Azure.WebJobs.Host;

namespace Box.Samples.AzureFunctions.Cognitive
{
    public class Box
    {
        private readonly BoxClient userClient;
        private readonly TraceWriter traceWriter;

        public Box(string userId, TraceWriter log)
        {
            traceWriter = log;

            var boxJwt = new BoxJWTAuth(ConfigureBoxApi());
            var userToken = boxJwt.UserToken(userId); //valid for 60 minutes so should be cached and re-used
            userClient = boxJwt.UserClient(userToken, userId);
        }

        public async Task<Stream> GetFileAsync(BoxEvent eventData)
        {
            if (eventData == null)
                throw new ArgumentNullException(nameof(eventData));

            return await userClient.FilesManager.DownloadStreamAsync(eventData.Source.Id);
        }

        public async Task<Dictionary<string, object>> AddMetaData(string fileId, Dictionary<string, object> metadata)
        {
            var scope = ConfigSettings.BoxMetadataScope;
            var templateKey = ConfigSettings.BoxMetadataTemplate;

            //delete existing metadata of the file if same file is used in unit testing 
            try
            {
                var existingMetadata = await userClient.MetadataManager.GetFileMetadataAsync(fileId, scope, templateKey);
                //existing metadata found so delete before adding new metadata
                await userClient.MetadataManager.DeleteFileMetadataAsync(fileId, scope, templateKey);
            }
            catch (BoxException e)
            {
                Console.WriteLine(e);
                if (e.StatusCode != HttpStatusCode.NotFound)
                    throw;
            }

            return await userClient.MetadataManager.CreateFileMetadataAsync(fileId, metadata, scope, templateKey);
        }

        private IBoxConfig ConfigureBoxApi()
        {
            var configFilePath = Path.Combine(ConfigSettings.CurrentDirectory, ConfigSettings.BoxConfigFileName);
            traceWriter.Info($"Config file path: {configFilePath}");

            IBoxConfig boxConfig;
            using (var fs = new FileStream(configFilePath, FileMode.Open))
            {
                boxConfig = BoxConfig.CreateFromJsonFile(fs);
            }
            return boxConfig;
        }
    }
}
