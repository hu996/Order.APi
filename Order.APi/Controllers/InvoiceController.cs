using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Order.APi.Dtos;
using Order.Repository.Repositories.InvoiceRepository;

namespace Order.APi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceRepo _invc;
        private readonly IMapper _mapper;

        public InvoiceController(IInvoiceRepo incv, IMapper mapper)
        {
            _invc = incv;
        }


        [HttpGet]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> GetAllinvoices()
        {
            var ii=await _invc.GetIAllnvoidAsync();

            if(ii.Any())
            {
                var mapInvoice = _mapper.Map<InvoiceDto>(ii);
                return Ok(mapInvoice);
            }
            else
            {
                return NotFound("No Invoices Found");
            }
        }


        [HttpGet("invoiceId")]
        public async Task<IActionResult> GetinvoicesByID(int id)
        {
            var allinvoices = _invc.GetInvoidByIdAsync(id);

            return allinvoices  == null ? NotFound():Ok(_mapper.Map<InvoiceDto>(allinvoices));

            
        }

    }
}
