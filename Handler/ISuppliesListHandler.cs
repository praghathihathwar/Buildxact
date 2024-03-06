using buildxact_supplies.Models.View;
using System.Collections.Generic;

namespace buildxact_supplies.Handler
{
    public interface ISuppliesListHandler
    {
        List<SuppliesViewModel> CombineSuppliesData();
    }
}
