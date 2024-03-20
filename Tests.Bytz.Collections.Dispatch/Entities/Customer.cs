using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Bytz.Collections.Dispatch.Entities;

public class Customer
{
    public int CustomerId { get; set; }

    public string CustomerType { get; set; }

    public string CustomerName { get; set; }

    public DateTime? BirthDate { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public bool IsActive { get; set; }

    public bool IsGoldStarMember(IEnumerable<Order> orders)
    {
        // stupid
        return orders.Any(o => o.OrderedOn > DateTime.Now.AddMonths(-3) && o.SubTotal > 100000m);
    }
}