using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineWebStore.Dto;
using OnlineWebStore.entity;
using OnlineWebStore.service;
using System.Net;

namespace OnlineWebStore.Controllers
{

    [Route("api/")]
    [ApiController]
    [Authorize(Roles ="Manager,Employee")]
    public class ProductController : ControllerBase
    {
        private IProductService productService;
        public ProductController(IProductService _productService)
        {
            productService = _productService;
        }

        [HttpPost("products")]
        [Authorize(Roles = "Employee")]
        public IActionResult saveProduct(ProductDto product)
        {
          string productDode =  productService.addStoreProduct(product);
            return StatusCode((int)HttpStatusCode.OK, new { message = productDode, status = "success" });
        }

        [HttpPut("products/{productName}")]
        [Authorize(Roles = "Employee")]
        public IActionResult editProduct(ProductDto product, string productName)
        {
            productService.editStoreProduct(product, productName);
            return StatusCode((int)HttpStatusCode.OK, new { message = "Product Updated", status = "success" });
        }

        [HttpDelete("products/{productName}/{storeId}")]
        [Authorize(Roles = "Employee")]
        public IActionResult deleteProduct(string productName, int storeId)
        {
            productService.deleteStoreProduct(productName, storeId);
            return StatusCode((int)HttpStatusCode.OK, new { message = "Product Deleted", status = "success" });
        }

        [HttpGet("products/{productName}/{storeId}")]
        [Authorize(Roles = "Employee")]
        public IActionResult getProduct(string productName,int storeId)
        {
            return Ok(productService.getStoreProduct(productName, storeId));
        }

        [HttpGet("products/{storeId}")]
        [Authorize(Roles = "Employee,Manger")]
        public IActionResult getStoreProducts(int storeId)
        {
            return Ok(productService.getStoreProducts(storeId));
        }

        [HttpGet("products")]
        [Authorize(Roles = "Manger")]
        public IActionResult getAllProducts()
        {
            return Ok(productService.getAllProducts());
        }


    }
}
