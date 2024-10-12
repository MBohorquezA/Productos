using Microsoft.AspNetCore.Mvc;
using Productos.Api.Models;
using Productos.Api.Models.ViewModels;
using Productos.BLL.ServiceRepository.Interfaces;
using Productos.Models.Entity;
using System.Diagnostics;

namespace Productos.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IQueryable<Product> queryProductSQL = await _productService.GetAll();
            List<VMProduct> lista = queryProductSQL.Select(c => new VMProduct(){
                                                            Id = c.Id,
                                                            Name = c.Name,
                                                            Description = c.Description,
                                                            Price = c.Price,
                                                            Stock = c.Stock
                                                            }).ToList();
            return StatusCode(StatusCodes.Status200OK, lista);
        }

        [HttpGet]
        public async Task<IActionResult> GetId(int id)
        {
           Product queryProductSQL = await _productService.GetId(id);

           return StatusCode(StatusCodes.Status200OK, queryProductSQL);
        }

        [HttpPost]
        public async Task<IActionResult> Insert([FromBody]VMProduct model)
        {
            Product product = new Product()
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                Stock = model.Stock
            };
            int idResponse = await _productService.Insert(product);

            return StatusCode(StatusCodes.Status201Created, new { value = idResponse });
        }

    }
}
