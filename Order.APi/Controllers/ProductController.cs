using AutoMapper;
using AutoMapper.Configuration.Annotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Order.APi.Dtos;
using Order.Core.Entities;
using Order.Repository.Repositories.ProductRepository;

namespace Order.APi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepo _prdct;
        private readonly IMapper _mapper;

        public ProductController(IProductRepo _prdct,IMapper mapper)
        {
            _mapper=mapper;
        }


        [HttpPost]
        [Authorize("Admin")]
        public async Task<IActionResult> Createproduct(ProductDto product)
        {
            var mapproduct=_mapper.Map<Product>(product);

            var createProduct = await _prdct.createProductAsync(mapproduct);
            if(createProduct != null)
            {
                return Ok(new { Message = "Product Created Succefully", createProduct });
            }
            return BadRequest();
        }


        [HttpGet("productId")]

        public async Task<IActionResult> ProductDetails(int Id)
        {
            var prodct=await _prdct.GetProductByIdAsync(Id);
            if(prodct != null) 
            {
                var mapprct = _mapper.Map<ProductDto>(prodct);

                return  Ok(mapprct);
            }
            else
            {
                return NotFound();
            }
        }


        [HttpGet]
        public async Task<IActionResult> DisplayProducts()
        {
            var prdct = await _prdct.GetAllProductsAsync();

            return prdct == null ? NotFound($"No Products") : Ok(_mapper.Map<ProductDto>(prdct)); 
        }


        [HttpPost("{productId}")]
        [Authorize("Admin")]

        public async Task<IActionResult>UpdateProduct(ProductDto prodct)
        {
            var mapprdct=_mapper.Map<Product>(prodct);

            var prdct=await _prdct.UpdateProductAsync(mapprdct);
            return null;
        }

    }
}
