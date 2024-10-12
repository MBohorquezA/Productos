using Productos.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Productos.BLL.ServiceRepository.Interfaces
{
    public interface IProductService
    {
        Task<int> Insert(Product model);
        Task<Product> GetId(int id);
        Task<IQueryable<Product>> GetAll();
    }
}
