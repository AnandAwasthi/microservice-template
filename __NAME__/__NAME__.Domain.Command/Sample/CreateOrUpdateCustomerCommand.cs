
using System;
using __NAME__.CommandProcessor.Command;

namespace __NAME__.Domain.Command
{
    public class CreateOrUpdateCustomerCommand : ICommand
    {
        public Guid CustomerId { get; set; }
        public string Name { get; set; }
    }
}
