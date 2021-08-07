
using System;
using System.Linq;
using __NAME__.Domain.Entities.Sample;

namespace __NAME__.Domain.Query
{
    public interface IQuery
    {
        IQueryable<Customer> GetCustomers();
        Customer GetCustomerById(Guid Id);
    }
}
