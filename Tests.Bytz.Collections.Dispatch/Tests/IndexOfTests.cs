using Tests.Bytz.Collections.Dispatch.Examples.Rules.Functions;
using Tests.Bytz.Collections.Dispatch.Tests.Basis;

namespace Tests.Bytz.Collections.Dispatch.Tests;

public class IndexOfTests
: DispatchTestBase<SimpleDiscountRules>
{
    protected override SimpleDiscountRules Rule => [];

    [Fact]
    public void IndexOf_Assert_Any_Order_In_Last_Year_Over_50000m_That_Gets_18_Percent_Discount()
    {
        Assert.Equal(0, this.Rule.IndexOf(new(), [new() { OrderedOn = DateTime.Now.AddMonths(-10), SubTotal = 50001m }]));
    }

    [Fact]
    public void IndexOf_Individual_With_Any_Order_In_Last_Six_Months_With_An_Order_Over_500m()
    {
        Assert.Equal(1, this.Rule.IndexOf(new() { CustomerType = "Individual" }, [new() { OrderedOn = DateTime.Now.AddMonths(-3), SubTotal = 501m }]));
    }

    //   a test without a specific match will throw an exception.
    [Fact]
    public void IndexOf_NoMatch_Throws_InvalidOperationException()
    {
        Assert.Throws<InvalidOperationException>
        (
            () => this.Rule.IndexOf(new() { CustomerType = "BlahBlahBlah" }, [])
        );
    }
}