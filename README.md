# box-azure-functions-cognitive-csharp sample

This sample app shows several use cases for using Azure Functions, Cosmos DB, and Microsoft Cognitive Services with Box:

* **Trigger external systems.**  Shows how to connect a Box webhook to an Azure Function securely. The messages are secured with a message signature that is validated in the Azure Function.
* **Extend Box with external processing.**  When an image file is uploaded to Box, use image analysis from Microsoft Cognitive Services Computer Vision API with Image-processing algorithms to smartly identify, caption and moderate your pictures. Distill actionable information from images and add that as metadata to the file in Box.
* **Build analytics.**  Record events that happen in Box in Microsoft's globally distributed Azure Cosmos DB for analytics.

This sample gives the step-by-step instructions.

#### Prerequisites
1. [Box developer account](https://developer.box.com/)
2. [Microsoft Visual Studio 2017](https://www.visualstudio.com/downloads/)
3. [Azure Functions](https://azure.microsoft.com/en-us/services/functions/)
4. [Azure Cosmos DB](https://docs.microsoft.com/en-us/azure/cosmos-db/introduction)
5. [Microsoft Cognitive Services Computer Vision API](https://azure.microsoft.com/en-us/services/cognitive-services/computer-vision/)

#### Step 1. Create a Box application
#### Step 2. Create a Computer Vision API
#### Step 3. Create a Azure Cosmos DB
#### Step 4. Create a Azure Function
#### Step 5. Test