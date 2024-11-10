using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineWebStore.Dto;
using OnlineWebStore.service;
using System.Net;

namespace OnlineWebStore.Controllers
{
    [Route("api/")]
    [ApiController]
    [Authorize(Roles = "Manager,Employee")]
    public class OrderController : ControllerBase
    {
        IOrderService orderService;
        public OrderController(IOrderService _orderSservice) { 
        orderService = _orderSservice;
        }

        [HttpPost("orders")]
        [Authorize(Roles = "Employee")]
        public IActionResult addOrder(OrderDto orderDto)
        {
           orderService.addStoreOrder(orderDto);
           return StatusCode((int)HttpStatusCode.OK, new { message = "Order Added Successsfuly", status = "success" });
        }

        [HttpPut("orders")]
        [Authorize(Roles = "Employee")]
        public IActionResult editOrderStatus(string orderCode, string status)
        {
          bool isUpdated = orderService.editOrderStatus(orderCode,status);
            if (isUpdated) {
                return StatusCode((int)HttpStatusCode.OK, new { message = "Order Updated Successsfuly", status = "success" });
            }
            return BadRequest("Error Updating Order");
        }

        [HttpDelete("orders")]
        [Authorize(Roles = "Employee")]
        public IActionResult deleteOrder(string orderCode)
        {
            bool isDeleted = orderService.deleteOrder(orderCode);
            if (isDeleted)
            {
             return StatusCode((int)HttpStatusCode.OK, new { message = "Order Deleted Successsfuly", status = "success" });            }
            return BadRequest("Error Deleting Order");
        }

        [HttpGet("orders/{storeId}")]
        [Authorize(Roles = "Employee,Manager")]
        public IActionResult getStoreOrders(int storeId)
        {
            return Ok(orderService.getStoreOrders(storeId));
        }


        [HttpGet("orders")]
        [Authorize(Roles = "Manager")]
        public IActionResult getAllOrders()
        {
            return Ok(orderService.getAllStoresOrders());
        }
    }
}
