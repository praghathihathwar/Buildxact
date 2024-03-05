using buildxact_supplies.Models;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Formats.Asn1;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace buildxact_supplies.Utilites
{
    public class ReadCSV
    {
        public List<SuppliesViewModel>  ReadCsvData(string filePath)
        {
            try
            {
                List<SuppliesViewModel> csvSuppliesViewModel = new List<SuppliesViewModel>();
                using (var reader = new StreamReader(filePath))
                using (var csv = new CsvReader(reader,CultureInfo.InvariantCulture))
                {
                    csv.Context.RegisterClassMap<CSVDataMap>(); // Map CSV columns to properties in your class
                    List<CSVDataModel> records = csv.GetRecords<CSVDataModel>().ToList();

                    foreach(var record in records)
                    {
                        csvSuppliesViewModel.Add(new SuppliesViewModel
                        {
                            Id = record.Id,
                            Name = record.Description,
                            Price = (float)Math.Round(record.Price,2)
                        });
                    }
                    return csvSuppliesViewModel;
                    
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., file not found, parsing errors)
                Console.WriteLine($"Error reading CSV file: {ex.Message}");
                throw; // Or rethrow a specific exception if needed
            }
        }
    }
}
