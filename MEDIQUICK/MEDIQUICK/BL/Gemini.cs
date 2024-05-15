using MEDIQUICK.BL;
using Google.Api.Gax.Grpc;
using Google.Cloud.AIPlatform.V1;
using System.Text;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System.Net;
using Newtonsoft.Json.Linq;

public class Gemini
{
    DBServices dbs = new DBServices();

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
            string new_content = responseText.Replace("json", "");
            string last_content = new_content.Replace("```", "");

            // ניתוח JSON למערך
            JArray json_data = JsonConvert.DeserializeObject<JArray>(last_content);
 
            // עקוב אחר כל פריט במערך
            foreach (JObject item in json_data)
            {
                string jsonString = item.ToString();

                Question tmpQuestion = JsonConvert.DeserializeObject<Question>(jsonString);
                tmpQuestion.Creator = 9999;

                dbs.InsertQuestion(tmpQuestion);
            }

         



            return responseText;
        }
        catch (Exception e)
        {
            return e.Message;
        }
    }
}




