using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using elsaeedTea.data.Context;
using elsaeedTea.data.Entities;
using elsaeedTea.repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace elsaeedTea.repository.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly ElsaeedTeaDbContext _context;

        public GenericRepository(ElsaeedTeaDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
        }

        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public async Task<IReadOnlyList<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task<TEntity> GetByIdAsync(string id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task<IReadOnlyList<OrderRequest>> GetOrdersByUserId(string id)
        {
            return await _context.Set<OrderRequest>().Where(x => x.UserId == id).ToListAsync();
        }

        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
        }

        public async Task<IReadOnlyList<ElsaeedTeaProduct>> GetAllTeaAsync()
        {
            return await _context.Set<ElsaeedTeaProduct>().Include(x => x.Images).ToListAsync();
        }

        public async Task<ElsaeedTeaProduct> GetTeaByIdAsync(int id)
        {
            return await _context.Set<ElsaeedTeaProduct>().Include(x => x.Images).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IReadOnlyList<CartItem>> GetAllCartsAsync()
        {
            return await _context.Set<CartItem>().Include(x => x.User).Include(x => x.ProductDetails).ToListAsync();
        }

        public async Task<CartItem> GetCartByIdAsync(int id)
        {
            return await _context.Set<CartItem>().Include(x => x.User).Include(x => x.ProductDetails).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IReadOnlyList<CartItem>> GetByUserIdAsync(string id)
        {
            return await _context.Set<CartItem>().Include(x => x.ProductDetails).Include(x => x.User).Where(x => x.UserId == id).ToListAsync();
        }


        public async Task<IReadOnlyList<ElsaeedTeaProduct>> GetDetailsByProductIdAsync(int id)
        {
            return await _context.Set<ElsaeedTeaProduct>().Include(x => x.Details).Where(x => x.Id == id).ToListAsync();
        }




    }
}
