using buildxact_supplies.Handler;
using buildxact_supplies.Models.View;
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
            SuppliesListHandler suppliesListHandler = new SuppliesListHandler();

            List<SuppliesViewModel> combinedSupplies = suppliesListHandler.CombineSuppliesData();

            foreach(var supply in combinedSupplies)
            {
                Console.WriteLine(supply.Id + ", " + supply.Name + ", " + supply.Price);
            }

        }
    }
}
