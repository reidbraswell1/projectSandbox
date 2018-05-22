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
        private static string token = "";
        private static string api = "https://api.ebay.com/buy/browse/v1/item_summary/search";
        private static string options = "?q=pear&limit=3";


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
                var itemsList = items.itemSummaries;
                foreach(var item in itemsList)
                {
                    Console.WriteLine(item.title);
                    Console.WriteLine(item.price.value);
                    Console.WriteLine(item.image.imageUrl);
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
        /* 
        private static string HttpGet(string URI)
        {
            WebClient client = new WebClient();

            // Add a user agent header in case the 
            // requested URI contains a query.

            client.Headers.Add("Authorization", "Bearer v^1.1#i^1#r^0#I^3#p^1#f^0#t^H4sIAAAAAAAAAOVXf2wTVRxf98vMORX5ISwYy4F/gN713V3v2p60oawjazK2QTvAocHr3Tt2rL2r917XVY0ZIw4k0YiDAIJkigkhwSgxRBETTIgKMQEWIVFMCAGiREM0BgViJr67ltFNws8hJPaf5r7v+77v8/l8f9w90F1ZNaO3ofd8jeu+0v5u0F3qcrHVoKqy4skHy0prK0pAkYOrv3tad3lP2ZmZSE4l09J8iNKmgaC7K5U0kOQYg1TGMiRTRjqSDDkFkYQVKRae2yhxDJDSlolNxUxS7mgkSPG+hBewIptQuYCmsCKxGpdjxs0g5ec1AIAsQ6/IspqmknWEMjBqICwbOEhxgPXTQKA5Ls6KEvBKXh8T4H1tlHsBtJBuGsSFAVTIgSs5e60irNeGKiMELUyCUKFoeE6sORyN1DfFZ3qKYoUKOsSwjDNo+FOdqUL3AjmZgdc+BjneUiyjKBAhyhPKnzA8qBS+DOYW4DtSJ7yi6NO8ssipgQQvJEZFyjmmlZLxtXHYFl2lNcdVggbWce56ihI1EsugggtPTSRENOK2/+Zl5KSu6dAKUvWzw8+EW1qoUGNzazQWaY3TC3XU3qgjTLfMj9C8KPoFoGoaLaoCL8uaUjgoH60g84iT6kxD1W3RkLvJxLMhQQ2HayNIQpE2xKnZaLbCGrYRFfv5hzRk2+yk5rOYwe2GnVeYIkK4ncfrZ2BoN8aWnshgOBRh5IIjUZCS02ldpUYuOrVYKJ8uFKTaMU5LHk82m2WyPGNaSz0cAKxn0dzGmNIOUzJFfO1ez/vr199A6w4VBZKdSJdwLk2wdJFaJQCMpVSI8/Mixxd0Hw4rNNL6L0MRZ8/wjhitDhH9ZMaoPiUg8CDAa/xodEioUKQeGwdMyDk6JVsdEKeTsgJphdRZJgUtXZV4QeN4vwZpVQxotDdAyjYhqCLNahACCBMJJeD/PzXKjZZ6TDHTsMVM6kpuVAp+1Iqdt9QW2cK5GEwmieFGq/6qJJFN8o7Ts3v9pijaMRAJIqd1xq5tRjFTHlMmQ802LXFQ3xZvnbwP76mkEoJ5prqaf5ExDl0GdSqMBZGZscg7nGm253rc7IAG6RJsmckktBawt6XE6E30uzTNr8pKSepExiX3GrObHJO3WNsyvousy3tci6/CnBU4HwAC8Ptui1udk9d47j8YWjeV2AYTYajegQ8Qz/DrUKjE+bE9rl2gx7WT3KiABzzBTgVTKstay8seqEU6howuawzSlxrkK9+CTAfMpWXdKq10LZ780fYlRRew/ufAxKErWFUZW110HwOTr6xUsA89WsP6gcBxrAi8Xl8bmHpltZydUD7u2PH3BqvSJ/S3Htlwckes//DEU32LQM2Qk8tVUUIqo2Tlqvjpjs9XDJy9//HOv7KsMZD948zP/N623dMbGq0fkEvcOW/xyr59uT8P1Eu51w+9MVBR3cu8c+nUsVkfjNn0/sYJs6oqxm84nBoYs4P//rGx67b1nn2qUogwmzaP6Tpo7Qm+NsW75aWLLxrLfvyQ+7ukb/Xe35J157pqP97vm/bQ2m/HDa5+tXEqv+V8cNK+2udPfuXvrNmy7pU9G6Sv6948N7imde3Wo0fW/PIdv2PX5umnf8cHWvfNylSPnfPrs52fHflm/7aajSe3bt89eEFdicf3ZQ6uSmWPPj0pekiVLmZ/Ot717oxzy5u/uNRQv9y3e4XadAFF1h+Iv/zCifELv2SYyNsPr//0k417LuXT9w+Ve+uAGg8AAA==");
            client.Headers.Add("Content-Type", "application/json");
            client.Headers.Add("X-EBAY-C-ENDUSERCTX", "affiliateCampaignId=<ePNCampaignId>,affiliateReferenceId=<referenceId>");

            Stream data = client.OpenRead(URI);
            StreamReader reader = new StreamReader(data);
            string s = reader.ReadToEnd();
            data.Close();
            reader.Close();

            return s;
        }
        */
        static void Main(string[] args)
        {
            //HttpGet(api + options);
            HttpGet(api + options);
        }
    }
}