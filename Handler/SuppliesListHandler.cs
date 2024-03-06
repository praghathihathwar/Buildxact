using buildxact_supplies.Models.View;
using buildxact_supplies.Utilites;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace buildxact_supplies.Handler
{
    public class SuppliesListHandler : ISuppliesListHandler
    {
        public List<SuppliesViewModel> CombineSuppliesData()
        {
            IConfiguration configuration = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
               .Build();

            string solutionDirectory = AppDomain.CurrentDomain.BaseDirectory;

            int indexOfBin = solutionDirectory.IndexOf("\\bin", StringComparison.OrdinalIgnoreCase);

            // Return the substring up to and including the project path (excluding '\bin'), or the original base directory if '\bin' is not found
            string folderPath = indexOfBin != -1 ? solutionDirectory.Substring(0, indexOfBin) : solutionDirectory;


            // Read settings
            string jsonFilePath = configuration["filePath:jsonPath"];
            jsonFilePath = Path.Combine(folderPath, jsonFilePath);
            string csvFilePath = configuration["filePath:csvPath"];
            csvFilePath = Path.Combine(folderPath, csvFilePath);

            float exchangeRate = float.Parse(configuration["audUsdExchangeRate"]);

            ReadCSV readCSV = new ReadCSV();
            // Your solution begins here
            List<SuppliesViewModel> CSVSupplies = readCSV.ReadCsvData(csvFilePath);

            ReadJson readJson = new ReadJson();
            List<SuppliesViewModel> JsonSupplies = readJson.ReadJsonData(jsonFilePath, exchangeRate);

            List<SuppliesViewModel> combinedSupplies = CSVSupplies.Concat(JsonSupplies).OrderByDescending(x => x.Price).ToList();

            return combinedSupplies;
        }
    }
}
