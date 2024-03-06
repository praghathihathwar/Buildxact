using buildxact_supplies.Models.Data;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace buildxact_supplies.Mapper
{
    public class CSVDataMap : ClassMap<CSVDataModel>
    {
        public CSVDataMap()
        {
            Map(csv => csv.Id).Index(0);
            Map(csv => csv.Description).Index(1);
            Map(csv => csv.Unit).Index(2);
            Map(csv => csv.Price).Index(3);
        }
    }
}
