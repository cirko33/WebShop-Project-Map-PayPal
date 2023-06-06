using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineStoreApp.DTOs;
using OnlineStoreApp.Exceptions;
using OnlineStoreApp.Interfaces.IServices;
using System.Text;

namespace OnlineStoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuyerController : ControllerBase
    {
        IBuyerService _buyerService;

        public BuyerController(IBuyerService buyerService)
        {
            _buyerService = buyerService;
        }

        [Authorize(Roles = "Buyer")]
        [HttpGet("products")]
        public async Task<IActionResult> GetProductsAsync() 
        {
            var products = await _buyerService.GetProducts();
            return Ok(products);
        }

        [Authorize(Roles = "Buyer")]
        [HttpGet("orders")]
        public async Task<IActionResult> GetMyOrders()
        {
            if (!int.TryParse(User.Claims.First(c => c.Type == "Id").Value, out int id))
                throw new BadRequestException("Bad ID. Logout and login.");
            var orders  = await _buyerService.GetMyOrders(id);
            return Ok(orders);
        }

        [Authorize(Roles = "Buyer")]
        [HttpPost("order")]
        public async Task<IActionResult> CreateOrder(CreateOrderDTO  createOrder)
        {
            if (!int.TryParse(User.Claims.First(c => c.Type == "Id").Value, out int id))
                throw new BadRequestException("Bad ID. Logout and login.");

            await _buyerService.CreateOrder(createOrder, id);
            return Ok();
        }

        [Authorize(Roles = "Buyer")]
        [HttpPost("cancel-order/{id}")]
        public async Task<IActionResult> CancelOrder(int id)
        {
            if (!int.TryParse(User.Claims.First(c => c.Type == "Id").Value, out int userId))
                throw new BadRequestException("Bad ID. Logout and login.");

            await _buyerService.CancelOrder(userId, id);
            return Ok();
        }
    }
}
