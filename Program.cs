using buildxact_supplies.Models;
using buildxact_supplies.Utilites;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SuppliesPriceLister
{
    class Program
    {
        static void Main(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            // Read settings
            string jsonFilePath = configuration["filePath:jsonPath"];
            string csvFilePath = configuration["filePath:csvPath"];
            float exchangeRate = float.Parse(configuration["audUsdExchangeRate"]);
            
            ReadCSV readCSV = new ReadCSV();
            // Your solution begins here
            List<SuppliesViewModel> CSVSupplies = readCSV.ReadCsvData(csvFilePath);

            ReadJson readJson = new ReadJson();
            List<SuppliesViewModel> JsonSupplies = readJson.ReadJsonData(jsonFilePath,exchangeRate);

            List<SuppliesViewModel> combinedSupplies = CSVSupplies.Concat(JsonSupplies).OrderByDescending(x => x.Price).ToList();

            foreach(var supply in combinedSupplies)
            {
                Console.WriteLine(supply.Id + ", " + supply.Name + ", " + supply.Price);
            }

        }
    }
}
