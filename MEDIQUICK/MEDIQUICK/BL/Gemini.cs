using MEDIQUICK.BL;
using Google.Api.Gax.Grpc;
using Google.Cloud.AIPlatform.V1;
using System.Text;
public class Gemini
{
    public async Task<string> GenerateContent(string promptToGemini)
    {
        string projectId = "crested-plexus-422813-c6";  //modified
        string location = "me-west1";                   //modified
        string publisher = "google";                      
        string model = "gemini-1.5-pro-preview-0409";   //modified

        // Create client
        var predictionServiceClient = new PredictionServiceClientBuilder
        {
            Endpoint = $"{location}-aiplatform.googleapis.com"
        }.Build();

        // Initialize content request
        var generateContentRequest = new GenerateContentRequest
        {
            Model = $"projects/{projectId}/locations/{location}/publishers/{publisher}/models/{model}",
            GenerationConfig = new GenerationConfig
            {
                Temperature = 0.4f,
                TopP = 1,
                TopK = 32,
                MaxOutputTokens = 2048 * 4
            },
            Contents =
            {
                new Content
                {
                    Role = "USER",
                    Parts =
                    {
                        new Part { Text = promptContent.promptText }
                    }
                }
            }
        };

        try
        {
            //Getting the response from google API asynchronously
            GenerateContentResponse response = await predictionServiceClient.GenerateContentAsync(generateContentRequest);
            string responseText = response.Candidates[0].Content.Parts[0].Text;
            return responseText;
        }
        catch (Exception e)
        {
            return e.Message;
        }
    }
}