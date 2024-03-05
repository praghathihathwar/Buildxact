using buildxact_supplies.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace buildxact_supplies.Utilites
{
    public class ReadJson
    {
        public List<SuppliesViewModel> ReadJsonData(string filePath, float exchangeRate)
        {
            try
            {
                List<SuppliesViewModel> jsonSuppliesViewModel = new List<SuppliesViewModel>();
                using StreamReader reader = new(filePath);
                var json = reader.ReadToEnd();
                JSONDataModel convertedJson = JsonConvert.DeserializeObject<JSONDataModel>(json);

                foreach (var partner in convertedJson.Partners)
                {
                    foreach (var supply in partner.Supplies)
                    {
                        jsonSuppliesViewModel.Add(new SuppliesViewModel
                        {
                            Id = supply.Id,
                            Name = supply.Description,
                            Price = (float)Math.Round((supply.PriceInCents / 100) / exchangeRate,2)

                        });
                    }
                }

                return jsonSuppliesViewModel;
            }
            catch(Exception ex)
            {
                // Handle exceptions (e.g., file not found, parsing errors)
                Console.WriteLine($"Error reading Json file: {ex.Message}");
                throw; // Or rethrow a specific exception if needed
            }

        }
    }
}
