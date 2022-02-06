//api key = vmyG6+5nEIIFSkMLHZB5dVaRM4Ye5TeotiEybm2fqQMxWrCe+5AvptP5u7W9DioziZnD8bEDZ9bdchK9oH7EaA==

// This code requires the Nuget package Microsoft.AspNet.WebApi.Client to be installed.
// Instructions for doing this in Visual Studio:
// Tools -> Nuget Package Manager -> Package Manager Console
// Install-Package Microsoft.AspNet.WebApi.Client

using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;


/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;*/

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

        public static string result = "525";

        private static Form1 form;
        PredictionModel(Form1 form)
        {
            PredictionModel.form = form;
        }

        public static void setInputValues (string[,] input)
        {
            inputValues = input;
        }

        public static void startPrediction()
        {
            Console.WriteLine("TEST TEST TEST TEST TEST TEST TEST TEST TEST TEST TEST TEST TEST TEST TEST ");
            _ = InvokeRequestResponseService();
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
                const string apiKey = "vmyG6+5nEIIFSkMLHZB5dVaRM4Ye5TeotiEybm2fqQMxWrCe+5AvptP5u7W9DioziZnD8bEDZ9bdchK9oH7EaA=="; // Replace this with the API key for the web service
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

                client.BaseAddress = new Uri("https://ussouthcentral.services.azureml.net/workspaces/62e0fb2b58ed44b9a41c96e13c547c99/services/44230f48c5a84075a78c27071ea4a064/execute?api-version=2.0&details=true");

                // WARNING: The 'await' statement below can result in a deadlock if you are calling this code from the UI thread of an ASP.Net application.
                // One way to address this would be to call ConfigureAwait(false) so that the execution does not attempt to resume on the original context.
                // For instance, replace code such as:
                //      result = await DoSomeTask()
                // with the following:
                //      result = await DoSomeTask().ConfigureAwait(false)

                
                HttpResponseMessage response = await client.PostAsJsonAsync("", scoreRequest);


                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Result: {0}", result);
                    form.setPredictionText(result);
                }
                else
                {
                    Console.WriteLine(string.Format("The request failed with status code: {0}", response.StatusCode));

                    // Print the headers - they include the requert ID and the timestamp, which are useful for debugging the failure
                    Console.WriteLine(response.Headers.ToString());

                    string responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(responseContent);
                }
            }
        }
    }
}
