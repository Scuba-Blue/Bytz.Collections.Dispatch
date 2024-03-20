using Tests.Bytz.Collections.Dispatch.Entities;
using Tests.Bytz.Collections.Dispatch.FunctionList;
using Tests.Bytz.Collections.Dispatch.Tests.Basis;

namespace Tests.Bytz.Collections.Dispatch.Tests;

public class CustomerDiscountTests
: DispatchTestBase<CustomerDiscountRules>
{
    protected override CustomerDiscountRules Rule => new(null, null);


    [Fact]
    public void CustomerDiscount_Assert_Any_Order_In_Last_Year_Over_50000m()
    {
        Assert.Equal(0, this.Rule.IndexOf(new(), [new() { OrderedOn = DateTime.Now.AddMonths(-10), SubTotal = 50001m }]));
    }

    [Fact]
    public void CustomerDiscount_Assert_Individual_With_Any_Order_In_Last_Six_Months_With_An_Order_Over_500m()
    {
        Assert.Equal(1, this.Rule.IndexOf(new() { CustomerType = "Individual" }, [new() { OrderedOn = DateTime.Now.AddMonths(-3), SubTotal = 501m }]));
    }

    [Fact]
    public void CustomerDiscount_Assert_Business_With_Any_Order_In_Last_Year_With_An_Order_Over_20000m()
    {
        Assert.Equal(2, this.Rule.IndexOf(new() { CustomerType = "Business" }, [new() { OrderedOn = DateTime.Now.AddMonths(-6), SubTotal = 20001m }]));
    }

    [Fact]
    public void CustomerDiscount_Assert_Any_Charity_With_Order_In_Last_Four_Months()
    {
        Assert.Equal(3, this.Rule.IndexOf(new() { CustomerType = "Charity" }, [new() { OrderedOn = DateTime.Now.AddMonths(-2) }]));
    }

    [Fact]
    public void CustomerDiscount_Assert_NoMatch_Throws_InvalidOperationException()
    {
        //  throwing for the IndexOf is desirable.  with a CALL there is a default overload taking a default that would be used.
        Assert.Throws<InvalidOperationException>(() => this.Rule.IndexOf(new(), []));
    }
}
