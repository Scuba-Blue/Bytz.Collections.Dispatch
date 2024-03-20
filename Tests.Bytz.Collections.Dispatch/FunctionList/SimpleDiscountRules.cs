using Bytz.Collections.Dispatch.Functions;
using Tests.Bytz.Collections.Dispatch.Entities;

namespace Tests.Bytz.Collections.Dispatch.FunctionList;

public class SimpleDiscountRules
: FunctionList<Customer, IEnumerable<Order>, decimal>
{
    public override void OnRegister()
    {
        Add((c, o) => o.Any(o => o.OrderedOn > DateTime.Now.AddYears(-1) && o.SubTotal > 50000m), (_, _) => 0.18m);
        Add((c, o) => c.CustomerType == "Individual" && o.Any(o => o.OrderedOn > DateTime.Now.AddMonths(-6) && o.SubTotal > 500m), (c, o) => 0.10m);
    }
}