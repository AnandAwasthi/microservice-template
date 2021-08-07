
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using __NAME__.CommandProcessor.Dispatcher;
using __NAME__.Domain.Query;
using __NAME__.Domain.Command;
using __NAME__.Domain.Entities.Sample;

namespace __NAME__.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SampleCustomerController : ControllerBase
    {

        private readonly ICommandBus _commandBus;
        private readonly IQuery _query;
        public SampleCustomerController(ICommandBus commandBus, IQuery query)
        {
            this._commandBus = commandBus;
            this._query = query;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<Customer> Get()
        {
            var customers = _query.GetCustomers();
            return customers.ToArray();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Customer Get(Guid id)
        {
            var customer = _query.GetCustomerById(id);
            return customer;
        }

        // POST api/values
        [HttpPost]
        public async Task Post([FromBody] Customer customer)
        {
            var customerCommand = new CreateOrUpdateCustomerCommand();
            customerCommand.CustomerId = customer.CustomerId;
            customerCommand.Name = customer.Name;
            await _commandBus.Submit(customerCommand);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task Put(Guid id, [FromBody] Customer customer)
        {
            var customerCommand = new CreateOrUpdateCustomerCommand();
            customerCommand.CustomerId = customer.CustomerId;
            customerCommand.Name = customer.Name;
            await _commandBus.Submit(customerCommand);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
