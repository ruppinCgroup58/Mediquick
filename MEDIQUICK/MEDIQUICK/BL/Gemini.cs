﻿using MEDIQUICK.BL;
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
                        new Part { Text = promptToGemini }
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


            // ניתוח JSON ל-JToken
            JToken json_data = JsonConvert.DeserializeObject<JToken>(last_content);

            // בדיקת סוג ה-JSON
            if (json_data is JArray jsonArray)
            {
                // אם ה-JSON הוא מערך, נעבור על כל אובייקט במערך
                foreach (JObject item in jsonArray)
                {
                    string jsonString = item.ToString();
                    Question tmpQuestion = JsonConvert.DeserializeObject<Question>(jsonString);
                    tmpQuestion.Creator = "Gemini";
                    dbs.InsertQuestion(tmpQuestion);
                }
            }
            else if (json_data is JObject jsonObject)
            {
                // אם ה-JSON הוא אובייקט בודד, נעבד אותו כאובייקט יחיד
                string jsonString = jsonObject.ToString();
                Question tmpQuestion = JsonConvert.DeserializeObject<Question>(jsonString);
                tmpQuestion.Creator = "Gemini";
                dbs.InsertQuestion(tmpQuestion);
            }
            else
            {
                // טיפול במקרה שבו הפורמט אינו מזוהה
                throw new InvalidOperationException("Unsupported JSON format");
            }

            return last_content;
        }
        catch (Exception e)
        {
            return e.Message;
        }
    }

    public async Task<string> GeminiForSimilarity(string promptToGemini)
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
                        new Part { Text = promptToGemini }
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


            // ניתוח JSON ל-JToken
            JToken json_data = JsonConvert.DeserializeObject<JToken>(last_content);

            // בדיקת סוג ה-JSON
            if (json_data is JArray jsonArray)
            {
                // אם ה-JSON הוא מערך, נעבור על כל אובייקט במערך
                foreach (JObject item in jsonArray)
                {
                    string jsonString = item.ToString();
                    Question tmpQuestion = JsonConvert.DeserializeObject<Question>(jsonString);
                    tmpQuestion.Creator = "Gemini";
                    dbs.InsertQuestion(tmpQuestion);
                }
            }
            else if (json_data is JObject jsonObject)
            {
                // אם ה-JSON הוא אובייקט בודד, נעבד אותו כאובייקט יחיד
                string jsonString = jsonObject.ToString();
                Question tmpQuestion = JsonConvert.DeserializeObject<Question>(jsonString);
                tmpQuestion.Creator = "Gemini";
                dbs.InsertQuestion(tmpQuestion);
            }
            else
            {
                // טיפול במקרה שבו הפורמט אינו מזוהה
                throw new InvalidOperationException("Unsupported JSON format");
            }

            return last_content;
        }
        catch (Exception e)
        {
            return e.Message;
        }
    }
}




