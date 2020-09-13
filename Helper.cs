using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RestSharp;
using RestSharp.Serialization.Json;
using RestSharp.Serializers.NewtonsoftJson;
using System;
using System.Net;

namespace BrewdogBeerTests
{
    public class Helper
    {
        public static string RemoveUnnecessaryString(string someString, int charsToBeRemoved = 0)
        {
            var count = someString.Length;
            return someString.Remove(0, count - charsToBeRemoved);
        }

        public static string ReturnMessage(int statusCode, RestResponse response, int msgType = 0)
        {
            if (msgType.Equals(0))
            {
                return string.Format("Status code is: {0},\n Response is: {1}.\n", statusCode, response.Content);
            }
            else
            {
                return string.Format("Error!\nStatus code is: {0},\n Response is: {1}.\n", statusCode, response.Content);
            }
        }

        public static void PrintResponse(string response)
        {
            Console.WriteLine(response);
        }

        public static string ReplaceCommaWithDot(string input)
        {
            // for some reason the double is being interpreted with comma (,) not with dot (.) so the following "workaround" had to be implemented. 
            return input.Replace(",", ".");
        }

        public static int ReturnHttpStatusCode(RestResponse response)
        {
            HttpStatusCode statusCode = response.StatusCode;
            int numericStatusCode = (int)statusCode;
            return numericStatusCode;
        }

        public static void SetSerialization(RestClient client, int selection = 0)
        {
            if (selection == 0)
            {
                JsonSerializerSettings DefaultSettings = new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    DefaultValueHandling = DefaultValueHandling.Include,
                    TypeNameHandling = TypeNameHandling.All,
                    NullValueHandling = NullValueHandling.Include,
                    Formatting = Formatting.None,
                    ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
                    FloatParseHandling = FloatParseHandling.Double,
                    FloatFormatHandling = FloatFormatHandling.DefaultValue,
                };
                client.UseNewtonsoftJson();
            }
            else if (selection == 1)
            {
                JsonDeserializer jsonDeserializer = new JsonDeserializer();
                client.AddHandler("application/json", () => jsonDeserializer);
            }
            else
            {
                Console.WriteLine("Serialization set to default.");
                selection = 0;
            }
        }
    }
}
