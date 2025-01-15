using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using elsaeedTea.data.Entities;

namespace elsaeedTea.repository.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity> GetByIdAsync(int id);
        Task<IReadOnlyList<TEntity>> GetAllAsync();
        Task AddAsync (TEntity entity);
        void Update (TEntity entity);
        void Delete(TEntity entity);



        Task<ElsaeedTeaProduct> GetTeaByIdAsync(int id);
        Task<IReadOnlyList<ElsaeedTeaProduct>> GetAllTeaAsync();



        Task<IReadOnlyList<CartItem>> GetAllCartsAsync();
        Task<CartItem> GetCartByIdAsync(int id);



    }
}
