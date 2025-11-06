using Bytz.Collections.Dispatch.Functions;
using Tests.Bytz.Collections.Dispatch.Examples.Entities.Customers;
using Tests.Bytz.Collections.Dispatch.Examples.Services.Contracts;

namespace Tests.Bytz.Collections.Dispatch.Examples.Rules.Functions;

public class CustomerDiscountRules
(
    IIndividualDiscountService individualDiscountService,
    IBusinessDiscountService businessDiscountService
)
: FunctionList<Customer, IEnumerable<Order>, decimal>
{
    public override void OnRegister()
    {
        // stupid examples
        Add((c, o) => o.Any(o => o.OrderedOn > DateTime.Now.AddYears(-1) && o.SubTotal > 50000m), (_, _) => 0.18m);
        Add((c, o) => c.CustomerType == "Individual" && o.Any(o => o.OrderedOn > DateTime.Now.AddMonths(-6) && o.SubTotal > 500m), (c, o) => individualDiscountService.CalculateDiscount(c, o));
        Add((c, o) => c.CustomerType == "Business" && o.Any(o => o.OrderedOn > DateTime.Now.AddYears(-1) && o.SubTotal > 20000m), (c, o) => businessDiscountService.CalculateDiscount(c, o));
        Add((c, o) => c.CustomerType == "Charity" && o.Any(o => o.OrderedOn > DateTime.Now.AddMonths(-4)), (_, _) => 0.16m);
    }

    /*
     *  *** the above defines rules that mimic the following block
     *  
     *  decimal discount = 0.0m;

        //  a bit contrived, but it suffices

        if (orders.Any(o => o.OrderedOn > DateTime.Now.AddYears(-1) && o.SubTotal > 50000m))
        {
            discount = 0.18m;
        }
        else if (customer.CustomerType == "Individual" && orders.Any(o => o.OrderedOn > DateTime.Now.AddMonths(-6) && o.SubTotal > 500m))
        {
            discount = _individualDiscountService.CalculateDiscount(customer, orders);
        }
        else if (customer.CustomerType == "Business" && orders.Any(o => o.OrderedOn > DateTime.Now.AddYears(-1) && o.SubTotal > 20000m))
        {
            discount = _businessDiscountService.CalculateDiscount(customer, orders);
        }
        else if (customer.CustomerType == "Charity" && orders.Any(o => o.OrderedOn > DateTime.Now.AddMonths(-4)))
        {
            discount = 0.16m;
        }

        return discount;
     */
}