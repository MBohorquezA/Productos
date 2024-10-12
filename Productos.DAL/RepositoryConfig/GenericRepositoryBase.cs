using Productos.DAL.Repository;

namespace Productos.DAL.RepositoryConfig
{
    public abstract class GenericRepositoryBase<TEntityModel> : IGenericRepository<TEntityModel> where TEntityModel : class
    {
        public virtual Task<int> Insert(TEntityModel modelo)
        {
            return Task.FromResult(0);
        }

        public virtual Task<bool> Update(TEntityModel modelo)
        {
            return Task.FromResult(true);
        }

        public virtual Task<bool> Delete(int id)
        {
            return Task.FromResult(true);
        }

        public virtual Task<TEntityModel?> GetId(int id)
        {
            return Task.FromResult(default(TEntityModel));
        }

        public virtual Task<IQueryable<TEntityModel>> GetAll()
        {
            return Task.FromResult(Enumerable.Empty<TEntityModel>().AsQueryable());
        }
    }
}
