using Tests.Bytz.Collections.Dispatch.Entities;

namespace Tests.Bytz.Collections.Dispatch.Services.Contracts;

public interface ICustomerService
{
    void CalculateOrderDiscount
    (
        Customer customer,
        Order order
    );
}
