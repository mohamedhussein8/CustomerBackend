using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class CustomerWithAddressSpecs : BaseSpecification<Customer>
    {
        public CustomerWithAddressSpecs()
        {
            AddInclude(c => c.Addresses);
        }

        public CustomerWithAddressSpecs(Expression<Func<Customer, bool>> criteria) : base(criteria)
        {
            AddInclude(c => c.Addresses);
        }
    }
}
