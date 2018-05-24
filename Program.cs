using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Request library
using System.Net;
using System.IO;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Json;
namespace projectSandbox
{
    class Program
    {
        private static string token;
        private static string api = "https://api.ebay.com/buy/browse/v1/item_summary/search";
        private static readonly string _apiKeyFileName = $"{Environment.CurrentDirectory}/apiKey";
        private static string HttpGet(string url)
        {
            try
            {
                WebRequest request = WebRequest.Create(url);

                request.Headers.Add("Authorization", $"Bearer {token}");
                request.Headers.Add("Content-Type", "application/json");
                request.Headers.Add("X-EBAY-C-ENDUSERCTX", "affiliateCampaignId=<ePNCampaignId>,affiliateReferenceId=<referenceId>");

                WebResponse response = request.GetResponse();

                Stream dataStream = response.GetResponseStream();

                StreamReader reader = new StreamReader(dataStream);

                var json = reader.ReadToEnd();
                var items = JsonConvert.DeserializeObject<RootObject>(json);
                if (items.total > 0)
                {
                    var itemsList = items.itemSummaries;
                    var cnt = 1;
                    foreach (var item in itemsList)
                    {
                        Console.WriteLine($"\n({cnt++}).    Item Title = {item.title}");
                        Console.WriteLine($"\t Item ID        = {item.itemId}");
                        Console.WriteLine($"\t Item Web Url   = {item.itemWebUrl}");
                        Console.WriteLine($"\t Item Price     = {item.price.value}");
                        Console.WriteLine($"\t Item Image URl = {item.image.imageUrl}");
                    }
                }
                else
                {
                    Console.WriteLine("No Items Found");
                }

                reader.Close();
                response.Close();

                return json;
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "Error: " + ex.Message;
            }
        }
        private static string GetAPIKey(String path)
        {
            var resp = "";
            do
            {
                Console.WriteLine("Do You Wish To Update The API Token");
                resp = Console.ReadLine();
            } while (!resp.ToLower().Equals("y") && !resp.ToLower().Equals("n"));
            if (resp.ToLower().Equals("y"))
            {
                do
                {
                    Console.WriteLine("Enter Text");
                    resp = Console.ReadLine();
                } while (String.IsNullOrEmpty(resp));
                File.WriteAllText(path, resp);
            }
            return (File.Exists(path)) ? File.ReadAllText(path) : null;
        }

        static void Main(string[] args)
        {
            token = GetAPIKey(_apiKeyFileName);
            if (String.IsNullOrEmpty(token))
            {
                Console.WriteLine("API KEY IS NULL EXITING");
                Environment.Exit(1);
            }
            //HttpGet(api + options);
            while (true)
            {
                var response = "";
                do
                {
                    Console.WriteLine("Enter Description ~ Exits");
                    response = Console.ReadLine();
                } while (String.IsNullOrEmpty(response));
                if (response.Equals("~"))
                {
                    break;
                }
                var options = $"?q={response}&limit=15";
                HttpGet(api + options);
            }
        }
    }
}