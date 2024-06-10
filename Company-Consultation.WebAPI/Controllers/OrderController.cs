using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.DTOs;
using Service.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace Company_Consultation.WebAPI.Controllers
{
    [ApiController]
    [Route("api/Order")]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("v1/processOrder")]
        [Authorize]
        [SwaggerResponse(StatusCodes.Status201Created, "Success", typeof(CnpjDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Incorrect Header Data", typeof(string))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Server Error", typeof(string))]
        public async Task<IActionResult> ProcessOrder([FromBody] CnpjDTO cnpj)
        {
            try
            {
                await _orderService.ProcessOrderAsync(cnpj.Cnpj);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("v1/getAllOrders")]
        [Authorize]
        [SwaggerResponse(StatusCodes.Status200OK, "Success", typeof(string))]
        [SwaggerResponse(StatusCodes.Status204NoContent, "Incorrect Header Data", typeof(string))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Server Error", typeof(string))]
        public async Task<IActionResult> GetAllOrders()
        {
            try
            {
                var orders = await _orderService.GetAllOrdersAsync();
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
