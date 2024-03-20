using Tests.Bytz.Collections.Dispatch.Entities;
using Tests.Bytz.Collections.Dispatch.FunctionList;
using Tests.Bytz.Collections.Dispatch.Repositories.Contracts;
using Tests.Bytz.Collections.Dispatch.Services.Contracts;

namespace Tests.Bytz.Collections.Dispatch.Services;

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
