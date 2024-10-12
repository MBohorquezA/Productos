using Productos.BLL.ServiceRepository.Interfaces;
using Productos.DAL.DataContext;
using Productos.DAL.Repository;
using Productos.Models.Entity;
using Vuelos.BLL.ServiceRepository;
using Vuelos.DAL.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddScoped<DbproductosContext>();

builder.Services.AddScoped<IGenericRepository<Product>, ProductRepository>();

builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
