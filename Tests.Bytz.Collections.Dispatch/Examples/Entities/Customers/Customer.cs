namespace Tests.Bytz.Collections.Dispatch.Examples.Entities.Customers;

public class Customer
{
    public int CustomerId { get; set; }

    public string CustomerType { get; set; }

    public string CustomerName { get; set; }

    public DateTime? BirthDate { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public bool IsActive { get; set; }
}
