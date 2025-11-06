using Tests.Bytz.Collections.Dispatch.Examples.Entities.Customers;

namespace Tests.Bytz.Collections.Dispatch.Examples.Repositories.Contracts;

public interface IOrdersRepository
{
    IEnumerable<Order> ReadOrders(int customerId);
}