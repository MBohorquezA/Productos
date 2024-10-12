using Microsoft.AspNetCore.Mvc;

using Productos.BLL.ServiceRepository.Interfaces;
using Productos.Models.Entity;
using Productos.WebApi.Models.ViewModels;

namespace Productos.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;

        public ProductController(IProductService productService, ILogger<ProductController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                IQueryable<Product> queryProductSQL = await _productService.GetAll();

                if (queryProductSQL == null)
                {
                    _logger.LogWarning("No se encontraron productos.");
                    return StatusCode(StatusCodes.Status404NotFound, "No se encontraron productos.");
                }

                List<VMProduct> lista = queryProductSQL.Select(c => new VMProduct()
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    Price = c.Price,
                    Stock = c.Stock
                }).ToList();

                return StatusCode(StatusCodes.Status200OK, lista);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los productos.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener los productos.");
            }
        }

        [HttpGet("GetId")]
        public async Task<IActionResult> GetId(int id)
        {
            try
            {
                Product queryProductSQL = await _productService.GetId(id);

                if (queryProductSQL == null)
                {
                    _logger.LogWarning($"Producto con ID {id} no encontrado.");
                    return StatusCode(StatusCodes.Status404NotFound, $"Producto con ID {id} no encontrado.");
                }

                return StatusCode(StatusCodes.Status200OK, queryProductSQL);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener el producto con ID: {id}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al obtener el producto con ID {id}.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Insert([FromBody]VMProduct model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogInformation("Error al validar el modelo");
                    return BadRequest(ModelState);
                }
                Product product = new Product()
                {
                    Name = model.Name,
                    Description = model.Description,
                    Price = model.Price,
                    Stock = model.Stock
                };
                int idResponse = await _productService.Insert(product);
                var response = new Dictionary<string, int> { { "id", idResponse } };

                return StatusCode(StatusCodes.Status201Created, response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear producto.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al crear producto.");
            }
        }

    }
}
