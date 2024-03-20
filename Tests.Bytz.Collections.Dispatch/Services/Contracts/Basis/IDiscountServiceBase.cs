using Tests.Bytz.Collections.Dispatch.Entities;

namespace Tests.Bytz.Collections.Dispatch.Services.Contracts.Basis;

public interface IDiscountServiceBase
{
    decimal CalculateDiscount(Customer customer, IEnumerable<Order> orders);
}