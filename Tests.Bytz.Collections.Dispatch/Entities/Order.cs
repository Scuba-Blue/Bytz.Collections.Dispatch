namespace Tests.Bytz.Collections.Dispatch.Entities;

public class Order
{
    public int OrderId { get; set; }

    public int CustomerId { get; set; }

    public DateTime OrderedOn { get; set; }

    public decimal SubTotal { get; set; }

    public decimal Discount { get; set; }

    public decimal OrderTotal { get; set; }
}