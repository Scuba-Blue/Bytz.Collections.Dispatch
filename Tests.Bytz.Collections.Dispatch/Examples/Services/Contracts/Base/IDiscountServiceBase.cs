using Tests.Bytz.Collections.Dispatch.Examples.Entities.Customers;

namespace Tests.Bytz.Collections.Dispatch.Examples.Services.Contracts.Base;

public interface IDiscountServiceBase
{
    decimal CalculateDiscount(Customer customer, IEnumerable<Order> orders);
}