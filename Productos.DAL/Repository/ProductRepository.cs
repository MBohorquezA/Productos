using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Productos.DAL.DataContext;
using Productos.DAL.RepositoryConfig;
using Productos.Models.Entity;

namespace Vuelos.DAL.Repository
{
    public class ProductRepository : GenericRepositoryBase<Product>
    {
        private readonly DbproductosContext _dbcontext;

        public ProductRepository(DbproductosContext context)
        {
            _dbcontext = context;
        }
        public override async Task<int> Insert(Product model)
        {
            var nameParam = new SqlParameter("@Name", model.Name);
            var descriptionParam = new SqlParameter("@Description", model.Description);
            var priceParam = new SqlParameter("@Price", model.Price);
            var stockParam = new SqlParameter("@Stock", model.Stock);

            var idParam = new SqlParameter("@ProductId", SqlDbType.Int) { Direction = ParameterDirection.Output };

            await _dbcontext.Database.ExecuteSqlRawAsync(
                "EXEC dbo.InsertProduct @Name, @Description, @Price, @Stock, @ProductId OUTPUT",
                nameParam, descriptionParam, priceParam, stockParam, idParam);

            return (int)idParam.Value;
        }

        public override async Task<Product?> GetId(int id)
        {
            var prodIdParam = new SqlParameter("@Id", id);
            var sql = "EXEC GetProductById @Id";

            var products = await _dbcontext.Products
                .FromSqlRaw(sql, prodIdParam)
                .ToListAsync();

            return products.FirstOrDefault();
        }

        public override async Task<IQueryable<Product>?> GetAll()
        {
            var products = await _dbcontext.Products
            .FromSqlRaw("EXEC GetAllProducts")
            .ToListAsync();

            return products.AsQueryable();
        }
    }
}
