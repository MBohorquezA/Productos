using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Productos.BLL.ServiceRepository.Interfaces;
using Productos.DAL.Repository;
using Productos.Models.Entity;

namespace Vuelos.BLL.ServiceRepository
{
    public class ProductService : IProductService
    {
        private readonly IGenericRepository<Product> _productRepo;

        public ProductService(IGenericRepository<Product> productRepo)
        {
            _productRepo = productRepo;
        }

        public async Task<int> Insert(Product model)
        {
            return await _productRepo.Insert(model);
        }

        public async Task<Product> GetId(int id)
        {
            return await _productRepo.GetId(id);
        }

        public async Task<IQueryable<Product>> GetAll()
        {
            return await _productRepo.GetAll();
        }
    }
}
