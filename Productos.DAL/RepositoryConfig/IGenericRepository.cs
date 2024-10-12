namespace Productos.DAL.Repository
{
    public interface IGenericRepository<TEntityModel> where TEntityModel : class
    {
        Task<int> Insert(TEntityModel modelo);
        Task<bool> Update(TEntityModel modelo);
        Task<bool> Delete(int id);
        Task<TEntityModel> GetId(int id);
        Task<IQueryable<TEntityModel>> GetAll();
    }
}
