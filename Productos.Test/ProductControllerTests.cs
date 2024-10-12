using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Productos.Api.Controllers;
using Productos.BLL.ServiceRepository.Interfaces;
using Productos.Models.Entity;
using Productos.WebApi.Models.ViewModels;
using Microsoft.AspNetCore.Http;

public class ProductControllerTests
{
    private readonly Mock<IProductService> _productServiceMock;
    private readonly Mock<ILogger<ProductController>> _loggerMock;
    private readonly ProductController _controller;

    public ProductControllerTests()
    {
        _productServiceMock = new Mock<IProductService>();
        _loggerMock = new Mock<ILogger<ProductController>>();
        _controller = new ProductController(_productServiceMock.Object, _loggerMock.Object);
    }

    [Fact]
    public async Task GetAll_ReturnsOk_WithProductList()
    {
        var products = new List<Product>
    {
        new Product { Id = 1, Name = "Product 1", Description = "Description 1", Price = 10.0, Stock = 5 },
        new Product { Id = 2, Name = "Product 2", Description = "Description 2", Price = 20.0, Stock = 10 }
    }.AsQueryable();

        _productServiceMock.Setup(service => service.GetAll())
            .ReturnsAsync(products);

        var result = await _controller.GetAll();

        var objectResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(StatusCodes.Status200OK, objectResult.StatusCode);
        var returnValue = Assert.IsType<List<VMProduct>>(objectResult.Value);
        Assert.Equal(2, returnValue.Count);
    }

    [Fact]
    public async Task GetId_ReturnsOk_WithProduct()
    {
        var product = new Product { Id = 1, Name = "Product 1", Description = "Description 1", Price = 10.0, Stock = 5 };

        _productServiceMock.Setup(service => service.GetId(1))
            .ReturnsAsync(product);

        var result = await _controller.GetId(1);

        var objectResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(StatusCodes.Status200OK, objectResult.StatusCode);
        var returnValue = Assert.IsType<Product>(objectResult.Value);
        Assert.Equal(1, returnValue.Id);
    }

    [Fact]
    public async Task GetId_ReturnsNotFound_WhenProductDoesNotExist()
    {
        _productServiceMock.Setup(service => service.GetId(1))
            .ReturnsAsync((Product)null);

        var result = await _controller.GetId(1);

        var objectResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(StatusCodes.Status404NotFound, objectResult.StatusCode);
    }

    [Fact]
    public async Task Insert_ReturnsCreated_WhenProductIsCreated()
    {
        var model = new VMProduct { Name = "Product 1", Description = "Description 1", Price = 10.0, Stock = 5 };

        _productServiceMock.Setup(service => service.Insert(It.IsAny<Product>())).ReturnsAsync(1);

        var result = await _controller.Insert(model);

        var objectResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(StatusCodes.Status201Created, objectResult.StatusCode);
        var returnValue = Assert.IsType<Dictionary<string, int>>(objectResult.Value);
        Assert.Equal(1, returnValue["id"]);
    }

    [Fact]
    public async Task Insert_ReturnsBadRequest_WhenModelIsInvalid()
    {
        _controller.ModelState.AddModelError("Name", "Required");

        var model = new VMProduct { Name = "", Description = "Description", Price = 10.0, Stock = 5 };

        var result = await _controller.Insert(model);

        Assert.IsType<BadRequestObjectResult>(result);
    }
}