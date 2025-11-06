using Tests.Bytz.Collections.Dispatch.Examples.Entities.Customers;
using Tests.Bytz.Collections.Dispatch.Examples.Repositories.Contracts;
using Tests.Bytz.Collections.Dispatch.Examples.Rules.Functions;
using Tests.Bytz.Collections.Dispatch.Examples.Services.Contracts;

namespace Tests.Bytz.Collections.Dispatch.Examples.Services;

public class CustomerService
(
    IOrdersRepository ordersRepository
)
: ICustomerService
{
    public void CalculateOrderDiscount
    (
        Customer customer,
        Order order
    )
    {
        IEnumerable<Order> orders = ordersRepository.ReadOrders(customer.CustomerId);

        //  run rules, if no matches, default to no discount.
        order.Discount = new SimpleDiscountRules().Call(customer, orders, (_, _) => 0.0m);
    }
}
