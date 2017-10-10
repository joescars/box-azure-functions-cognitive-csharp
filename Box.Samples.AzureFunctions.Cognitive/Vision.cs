using System.IO;
using System.Threading.Tasks;
using Microsoft.ProjectOxford.Vision;
using Microsoft.ProjectOxford.Vision.Contract;

namespace Box.Samples.AzureFunctions.Cognitive
{
    public class Vision
    {
        private readonly VisionServiceClient visionServiceClient;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="visionApiKey"></param>
        public Vision(string visionApiKey)
        {
            visionServiceClient = new VisionServiceClient(visionApiKey);
        }

        /// <summary>
        /// Analyze stream input with Microsoft Cognitive Compupter Vision API and returns the result
        /// </summary>
        /// <param name="stream">image file stream</param>
        /// <returns>Congnitive Computer Vision API analysis results</returns>
        public async Task<AnalysisResult> Analyze(Stream stream)
        {
            var features = new[] { VisualFeature.Faces, VisualFeature.Tags, VisualFeature.ImageType, VisualFeature.Description, VisualFeature.Adult };
            return await visionServiceClient.AnalyzeImageAsync(stream, features);
        }
    }
}
