// This code requires the Nuget package Microsoft.AspNet.WebApi.Client to be installed.
// Instructions for doing this in Visual Studio:
// Tools -> Nuget Package Manager -> Package Manager Console
// Install-Package Microsoft.AspNet.WebApi.Client

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace RUAP_Project
{
    public class StringTable
    {
        public string[] ColumnNames { get; set; }
        public string[,] Values { get; set; }
    }
    class PredictionModel
    {
        private static string[,] inputValues;

        public static string result = "455334";

        public static void setInputValues (string[,] input)
        {
            inputValues = input;
        }

        public static void startPrediction()
        {
            _ = InvokeRequestResponseService();
        }

        public string getResult()
        {
            return result;
        }

        public static double parseCalculatedValue(string value)
        {
            String[] arr = value.Split('.');
            if(arr.Length == 1)
            {
                return double.Parse(arr[0])*100;
            }
            else
            {
                return double.Parse(string.Join(",", arr))*100;
            }
        }

        static async Task InvokeRequestResponseService()
        {
            using (var client = new HttpClient())
            {
                var scoreRequest = new
                {

                    Inputs = new Dictionary<string, StringTable>() {
                        {
                            "input1",
                            new StringTable()
                            {
                                ColumnNames = new string[] {"age", "sex", "cp", "trtbps", "chol", "fbs", "restecg", "thalachh", "exng", "oldpeak", "slp", "caa", "thall", "output"},
                                Values = inputValues
                            }
                        },
                    },
                    GlobalParameters = new Dictionary<string, string>()
                    {
                    }
                };
                const string apiKey = "YZ+9f3KyE4cQXWReyiDzRnN8RcslFTFty/u1KPk8izZVvJRyOtc3BdRicdUDOlZVSBKQxJKoP31j3e4ppoRG/A==";
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

                client.BaseAddress = new Uri("https://ussouthcentral.services.azureml.net/workspaces/62e0fb2b58ed44b9a41c96e13c547c99/services/a5c112d4d3e64c1a99c0e248211bf640/execute?api-version=2.0&details=true");

                result = "Please, press the button again";
                HttpResponseMessage response = await client.PostAsJsonAsync("", scoreRequest).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsStringAsync();
                    Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(result);


                    double value = double.Parse(myDeserializedClass.Results.output1.value.Values[0][14]);
                    //double probability = PredictionModel.parseCalculatedValue(myDeserializedClass.Results.output1.value.Values[0][15]);
                    //probability = Math.Round(probability, 2);

                    result = "You have been classified to have " + (value == 1 ? "HIGHER chances of heart attack " : "LOWER chances of heart attack ") + "\n";
                }
                else
                {
                    Console.WriteLine(string.Format("The request failed with status code: {0}", response.StatusCode));

                    Console.WriteLine(response.Headers.ToString());

                    string responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(responseContent);
                }
            }
        }
    }
}
