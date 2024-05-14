using MEDIQUICK.BL;
using Google.Api.Gax.Grpc;
using Google.Cloud.AIPlatform.V1;
using System.Text;
using Google.Api.Gax.Grpc;
using System.Threading.Tasks;
//using static Google.Cloud.AIPlatform.V1.PredictionService;

using System.Threading.Tasks; //Used for delaying

public class Gemini
{
    public async Task<string> GenerateContent(string fileContent)
    {
        string projectId = "crested-plexus-422813-c6";  //defined
        string location = "me-west1";                   //defined
        string publisher = "google";
        string model = "gemini-1.5-pro-preview-0409";   //defined

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
                        new Part { Text = promptContent.promptText },

                        //new Part { FileData = new() { MimeType = "image/png", FileUri = "gs://generativeai-downloads/images/scones.jpg" } }
                    }

                }
            }

            
        };

        var myResponse = "";

        // Make the request, returning a streaming response
        //using PredictionServiceClient.StreamGenerateContentStream response = predictionServiceClient.StreamGenerateContent(generateContentRequest);

        StringBuilder fullText = new();
        // Read streaming responses from server until complete
        //AsyncResponseStream<GenerateContentResponse> responseStream = response.GetResponseStream();
        // System.Threading.Thread.Sleep(10000);

        //int counter = 0;
        try
        {
            GenerateContentResponse response = await predictionServiceClient.GenerateContentAsync(generateContentRequest);

            string responseText = response.Candidates[0].Content.Parts[0].Text;
            Console.WriteLine(responseText);

            return responseText;


            //while (await responseStream.MoveNextAsync())
            //{
            //    GenerateContentResponse responseItem = responseStream.Current;
            //    await Console.Out.WriteLineAsync(responseItem?.Candidates[0].Content.Parts[0].Text);
            //    myResponse += responseItem?.Candidates[0].Content.Parts[0].Text;
            //    System.Threading.Thread.Sleep(1000);
            //}
        }
        catch (Exception e)
        {
            return e.Message;
        }
        finally
        {
            await Console.Out.WriteLineAsync(myResponse);

        }

        await Console.Out.WriteLineAsync(myResponse);
        return fullText.ToString();
    }
}