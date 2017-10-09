using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Box.Samples.AzureFunctions.Cognitive
{
    internal class ConfigSettings
    {
        public static string AzureComputerVisionApiKey => GetSetting("AzureComputerVisionApiKey");
        public static string CosmosDbEndpointUrl => GetSetting("CosmosDbEndpointUrl");
        public static string CosmosDbPrimaryKey => GetSetting("CosmosDbPrimaryKey");
        public static string CosmosDbDatabaseId => GetSetting("CosmosDbDatabaseId");
        public static string CosmosDbDatabaseCollection => GetSetting("CosmosDbDatabaseCollection");
        public static string BoxClientId => GetSetting("BoxClientId");
        public static string BoxClientSecret => GetSetting("BoxClientSecret");
        public static string BoxEnterpriseId => GetSetting("BoxEnterpriseId");
        public static string BoxJwtPrivateKey => GetSetting("BoxJwtPrivateKey");
        public static string BoxJwtPrivateKeyPassword => GetSetting("BoxJwtPrivateKeyPassword");
        public static string BoxJwtPublicKeyId => GetSetting("BoxJwtPublicKeyId");
        public static string BoxMetadataScope => GetSetting("BoxMetadataScope");
        public static string BoxMetadataTemplate => GetSetting("BoxMetadataTemplate");
        public static string CurrentDirectory => IsAzureEnvironment ? Path.Combine(GetSetting("HOME"), @"site\wwwroot") : Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"../../../"));
        public static string BoxConfigFileName => GetSetting("BoxConfigFileName");
        public static bool IsAzureEnvironment => !string.IsNullOrEmpty(GetSetting("WEBSITE_INSTANCE_ID"));

        private static string GetSetting(string settingKey)
        {
            return string.IsNullOrEmpty(settingKey) ? null : Environment.GetEnvironmentVariable(settingKey);
        }
    }
}
