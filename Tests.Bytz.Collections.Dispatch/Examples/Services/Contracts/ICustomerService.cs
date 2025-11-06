using Tests.Bytz.Collections.Dispatch.Examples.Entities.Customers;

namespace Tests.Bytz.Collections.Dispatch.Examples.Services.Contracts;

public interface ICustomerService
{
    void CalculateOrderDiscount
    (
        Customer customer,
        Order order
    );
}
