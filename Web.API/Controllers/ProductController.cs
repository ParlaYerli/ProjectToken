using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.API.Domain.Responses;
using Web.API.Domain.Services;
using Web.API.Resources;
using Web.API.Extensions;
using Web.API.Domain.Model;

namespace Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;
        private readonly IMapper _mapper;
        public ProductController(IProductService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            ProductListResponse productListResponse = await _service.ListAsync();
            if (productListResponse.Success)
            {
                return Ok(productListResponse.ProductList);
            }
            else
            {
                return BadRequest(productListResponse.Message);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetFindById(int id)
        {
            ProductResponse productResponse = await _service.FindByIdAsync(id);
            if (productResponse.Success)
            {
                return Ok(productResponse.Product);
            }
            else
            {
                return BadRequest(productResponse.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddProducts(ProductResource productResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }
            else
            {
                Product product = _mapper.Map<ProductResource, Product>(productResource);
                var response = await _service.AddProduct(product);
                if (response.Success)
                {
                    return Ok(response.Product);
                }
                else
                {
                    return BadRequest(response.Message);
                }
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct(ProductResource productResource, int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }
            else
            {
                Product product = _mapper.Map<ProductResource, Product>(productResource);

                var response=await _service.UpdateProduct(product,id);
                if (response.Success)
                {
                    return Ok(response.Product);
                }
                else
                {
                    return BadRequest(response.Message);
                }
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> RemoveProduct(int id)
        {
            ProductResponse response = await _service.RemoveProduct(id);
            if (response.Success)
            {
                return Ok(response.Product);
            }
            else
            {
                return BadRequest(response.Message);
            }
        }
            
    }
}
