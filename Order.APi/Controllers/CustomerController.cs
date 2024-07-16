using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Order.Core.Entities;
using Order.Repository.Repositories.CustomerRepository;

namespace Order.APi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepo _cst;
        private readonly IMapper _mapper;
        public CustomerController(ICustomerRepo cst,IMapper mapper)
        {
            _cst= cst;
            _mapper= mapper;    
        }

        [HttpPost]
        public async Task<IActionResult>CreateCustomer(CustomerDto customer)
        {

            Customer mapcustomer = _mapper.Map<Customer>(customer);
            var x = await _cst.CreateAsync(mapcustomer);
            return Ok(x);
        }

        [HttpGet]
        [Route("{customerId}/orders")]

        public IActionResult GetCustomer(int customerId)
        {

            var customer=  _cst.GeTAsync(customerId);
            if(customer is not  null)
            {
                var CustomerOrders= _mapper.Map<Customer>(customer);
                return Ok(CustomerOrders);
            }
            return NotFound("Not Found");
            
        }

    }
}
