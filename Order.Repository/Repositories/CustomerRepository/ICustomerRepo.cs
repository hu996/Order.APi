using Order.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Repository.Repositories.CustomerRepository
{
    public interface ICustomerRepo
    {
        public Task<Customer>CreateAsync(Customer entity);

        public Task<IEnumerable<Customer>> GeTAsync(int Id);
    }
}
