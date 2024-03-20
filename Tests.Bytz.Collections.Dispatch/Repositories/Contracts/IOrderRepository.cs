using Tests.Bytz.Collections.Dispatch.Entities;

namespace Tests.Bytz.Collections.Dispatch.Repositories.Contracts;

public interface IOrdersRepository
{
    IEnumerable<Order> ReadOrders(int customerId);
}