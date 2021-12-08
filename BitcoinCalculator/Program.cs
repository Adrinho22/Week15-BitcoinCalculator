using System;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace BitcoinCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            BitcoinRate currentBitcoin = GetRates();
            //Console.WriteLine($"current rate: {currentBitcoin.bpi.USD.code} {currentBitcoin.bpi.USD.rate_float}");
            //Console.WriteLine($"{currentBitcoin.disclaimer}");
            Console.WriteLine("Mitu bitcoini teil on");
            float userbitcoin = float.Parse(Console.ReadLine());
            Console.WriteLine("'EUR'/'GBP'/'USD'");                        
            string usercurrency = Console.ReadLine();
            float currentRate = 0;
            
            if(usercurrency == "EUR")
            {
                currentRate = currentBitcoin.bpi.EUR.rate_float;               
            }
            else if (usercurrency == "GBP")
            {
                currentRate = currentBitcoin.bpi.GBP.rate_float;
            }
            else if(usercurrency == "USD")
            {
                currentRate = currentBitcoin.bpi.USD.rate_float;
            }

            float result = currentRate * userbitcoin;
            Console.WriteLine($"Sinu bitcoini väärtus on {usercurrency} {result}");


        }
        public static BitcoinRate GetRates()
        {
            string url = "https://api.coindesk.com/v1/bpi/currentprice.json";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";

            var webResponse = request.GetResponse();
            var webStream = webResponse.GetResponseStream();

            BitcoinRate bitCoinData;

            using(var responseReader = new StreamReader(webStream))
            {
                var response = responseReader.ReadToEnd();
                bitCoinData = JsonConvert.DeserializeObject<BitcoinRate>(response);
            }
            return bitCoinData;
        }
                          
    }
}
