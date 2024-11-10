using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineWebStore.Dto;
using OnlineWebStore.service;

namespace OnlineWebStore.Controllers
{
    [Route("api/[controller]")]
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
            return Ok("Store Saved");
        }

        [HttpGet("stores/{storeId}")]
        [Authorize(Roles = "Manager,Employee")]
        public IActionResult getStore(int storeId)
        {
            storeService.getStore(storeId);
            return Ok("Store Saved");
        }

        [HttpGet("stores")]
        [Authorize(Roles = "Manager")]
        public IActionResult getStores( )
        {
            storeService.getStores();
            return Ok("Store Saved");
        }

    }
}
