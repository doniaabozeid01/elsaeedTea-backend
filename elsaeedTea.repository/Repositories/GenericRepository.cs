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
            return await _context.Set<ElsaeedTeaProduct>().Include(x => x.Images).FirstOrDefaultAsync(x => x.Id==id);
        }
    }
}
