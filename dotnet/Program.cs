using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Json;

using dotenv.net;
using System.IO;
using System.Text.Json;

namespace dotnet
{
    //Request payload
    public class Review {
        public string review {get; set;}
    }

    class Program
    {
        private static readonly HttpClient client = new HttpClient();

        static async Task Main(string[] args)
        {
            DotEnv.Config(true, "../common/.env");
            String predictionServer = Environment.GetEnvironmentVariable("PREDICTION_SERVER");
            String deploymentId = Environment.GetEnvironmentVariable("DEPLOYMENT_ID");
            String predictionEndpoint = $"{predictionServer}/predApi/v1.0/deployments/{deploymentId}/predictions";

            String apiKey = Environment.GetEnvironmentVariable("API_KEY");
            String datarobotKey = Environment.GetEnvironmentVariable("DATAROBOT_KEY");

            await makePrediction(predictionEndpoint, apiKey, datarobotKey);
        }

        private static async Task makePrediction(String predictionEndpoint, String apiKey, String datarobotKey) 
        {   
            var fs = File.OpenRead("../common/payload_to_predict.json");

            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
            client.DefaultRequestHeaders.Add("datarobot-key", datarobotKey);

            var payload = await JsonSerializer.DeserializeAsync<Review[]>(fs);
            var content = JsonContent.Create<Review[]>(payload);

            var predictionResponse = await client.PostAsync(predictionEndpoint, content);
            
            if(predictionResponse.IsSuccessStatusCode){
                Console.WriteLine("Success");
                var predictionString = await predictionResponse.Content.ReadAsStringAsync();
                Console.WriteLine(predictionString);
            }

            else {
                Console.WriteLine(predictionResponse.StatusCode.ToString());
            }
        }
    }
}
