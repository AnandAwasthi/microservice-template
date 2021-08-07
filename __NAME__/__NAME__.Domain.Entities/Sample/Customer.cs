using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace __NAME__.Domain.Entities.Sample
{
    public class Customer : Entity
    {
        public Guid CustomerId { get; set; }
        public string Name { get; set; }
    }
}
