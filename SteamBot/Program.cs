using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading;

namespace SteamBot
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> badGamesList = new List<string>();

            var gamesResponse = new Games();
            Task.Run(async () =>
            {
                HttpClient client = new HttpClient();
                string queryString = "http://api.steampowered.com/ISteamApps/GetAppList/v2";
                var gamesResult = await client.GetAsync(queryString);
                gamesResponse = JsonConvert.DeserializeObject<Games>(await gamesResult.Content.ReadAsStringAsync());

                int iterator = 0;
                foreach (Apps apps in gamesResponse.applist.apps)
                {
                    string queryString2 = String.Format("http://store.steampowered.com/api/appdetails/?appids={0}", apps.appid);
                    var gamesListResult = await client.GetAsync(queryString2);
                    if (!gamesListResult.IsSuccessStatusCode)
                    {
                        iterator++;
                    }

                    while (!gamesListResult.IsSuccessStatusCode)
                    {
                        Thread.Sleep(10000);
                        Console.WriteLine("Slept for 10 seconds");
                        gamesListResult = await client.GetAsync(queryString2);
                    }

                    var GameListResultString = await gamesListResult.Content.ReadAsStringAsync();
                    var gamesListResponse = (JObject)JsonConvert.DeserializeObject(GameListResultString);

                    try { 
                        Console.WriteLine(gamesListResponse[apps.appid]["data"]["name"]);
                    }
                    catch
                    {
                        badGamesList.Add(apps.appid.ToString());
                    }
                    Thread.Sleep(1500);
                }
            }).GetAwaiter().GetResult();


            Console.ReadLine();
        }
    }
}
