using Tests.Bytz.Collections.Dispatch.Entities;
using Tests.Bytz.Collections.Dispatch.FunctionList;
using Tests.Bytz.Collections.Dispatch.Tests.Basis;

namespace Tests.Bytz.Collections.Dispatch.Tests;

public class CallTests
: DispatchTestBase<SimpleDiscountRules>
{
    protected override SimpleDiscountRules Rule => [];

    [Fact]
    public void Call_Assert_Any_Order_In_Last_Year_Over_50000m_That_Gets_18_Percent_Discount()
    {
        Assert.Equal(0.18m, this.Rule.Call(new(), [new() { OrderedOn = DateTime.Now.AddMonths(-10), SubTotal = 50001m }]));
    }

    [Fact]
    public void Call_Individual_With_Any_Order_In_Last_Six_Months_With_An_Order_Over_500m()
    {
        Assert.Equal(0.10m, this.Rule.Call(new() { CustomerType = "Individual" }, [new() { OrderedOn = DateTime.Now.AddMonths(-3), SubTotal = 501m }]));
    }

    [Fact]
    public void Call_NoMatch_Throws_InvalidOperationException()
    {
        Assert.Throws<InvalidOperationException>
        (
            () => this.Rule.Call(new() { CustomerType = "BlahBlahBlah" }, [])
        );
    }

    [Fact]
    public void Call_Individual_With_No_Match_Returns_Default()
    {
        Assert.Equal
        (
            0.0m, 
            this.Rule.Call
            (
                new() { CustomerType = "BlahBlahBlah" }, [],
                (_, _) => 0.0m));
    }
}
