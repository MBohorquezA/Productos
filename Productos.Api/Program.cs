using Microsoft.EntityFrameworkCore;
using Productos.BLL.ServiceRepository.Interfaces;
using Productos.DAL.DataContext;
using Productos.DAL.Repository;
using Productos.Models.Entity;
using Vuelos.BLL.ServiceRepository;
using Vuelos.DAL.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<DbproductosContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("cadenaSQL"));
});

builder.Services.AddScoped<DbproductosContext>();

builder.Services.AddScoped<IGenericRepository<Product>, ProductRepository>();

builder.Services.AddScoped<IProductService, ProductService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
