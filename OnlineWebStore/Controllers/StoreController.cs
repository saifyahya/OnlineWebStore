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
    public class StoreController: ControllerBase
    {
        private IStoreService storeService;

        public StoreController(IStoreService _storeService)
        {
            storeService = _storeService;
        }

        [HttpPost("stores")]
        [Authorize(Roles = "Manager")]
        public IActionResult addStore(StoreDto storeDto)
        {
            storeService.addStore(storeDto);
            return StatusCode((int)HttpStatusCode.OK, new { message = "Store Added Successfully", status = "success" });
        }

        [HttpGet("stores/{storeId}")]
        [Authorize(Roles = "Manager,Employee")]
        public IActionResult getStore(int storeId)
        {
         StoreDto store=   storeService.getStore(storeId);
            return Ok(store);
        }

        [HttpGet("stores")]
        [Authorize(Roles = "Manager")]
        public IActionResult getStores( )
        {
          List<StoreDto> stores=  storeService.getStores();
            return Ok(stores);
        }

    }
}
