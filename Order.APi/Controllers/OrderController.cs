using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.OpenApi.Models;
using Order.APi.Dtos;
using Order.APi.Hubs;
using Order.Core.Entities;
using Order.Repository.Repositories.OrderRepository;

namespace Order.APi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ordersController : ControllerBase
    {
        private readonly IOrderRepository _order;
        private readonly IMapper _mapper;
        private readonly IHubContext<OrderHub> _hubcontext;

        public ordersController(IOrderRepository order,IMapper mapper, IHubContext<OrderHub> hubcontext)
        {
            _order = order;
            _mapper = mapper;
            _hubcontext = hubcontext;
        }


        [HttpPost]

        public async Task<IActionResult> CreateOrder(OrderDto order)
        {
            var mapOrder= _mapper.Map<Orders>(order);

            var neworder=await _order.CreateOrderAsync(mapOrder);
            if(neworder != null)
            {

                return Ok(order);
            }

            return BadRequest();
            
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DisplayOrders()
        {
            var orders=_order.GetAllOrdersAsync();
            var maporder=_mapper.Map<Orders>(orders);
            if(maporder != null)
            {
                return Ok(orders);
            }
            return NotFound();    
        }


        [HttpGet("{orderId}")]
        
        public async Task<IActionResult>GetOrderdetails(int orderId)
        {
            var order=await _order.GetOrderByIdAsync(orderId);

            if(order != null)
            {
                var maporder = _mapper.Map<OrderDto>(order);
                return Ok(maporder);
            }
             

            return NotFound();
        }



        [HttpGet("{orderId}/status")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> UpdateStatus(int orderid,string status)
        {

            var order = _order.UpdateOrderAsync(orderid, status);
            if(order != null )
            {
                await _hubcontext.Clients.All.SendAsync("UpdateStatus", orderid, status);
                return Ok(new { message = "order has been Updated", order });
            }
            return NotFound();
        }



    }
}
