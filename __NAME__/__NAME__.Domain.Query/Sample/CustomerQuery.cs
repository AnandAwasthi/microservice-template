
using System;
using System.Linq;
using MongoDB.Driver;
using __NAME__.Domain.Entities.Sample;
using __NAME__.Mongo.DatabaseFactory;

namespace __NAME__.Domain.Query.ApiManagment
{
    public class CustomerQuery : IQuery

    {
        private readonly IDbContext _dbContext;
        public CustomerQuery(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<Customer> GetCustomers()
        {
            var customerCollection = _dbContext.GetCollection<Customer>("SampleCustomer").AsQueryable();

            var customerDetail = from arc in customerCollection
                                 select new Customer { CustomerId = arc.CustomerId, Name = arc.Name };
            return customerDetail;
        }
        public Customer GetCustomerById(Guid Id)
        {
            var customers = GetCustomers();
            var customerDetail = from arc in customers
                                 where arc.Name == "Sample Customer"
                                 select new Customer { CustomerId = arc.CustomerId, Name = arc.Name};
            return customerDetail.SingleOrDefault();
        }
    }
}
