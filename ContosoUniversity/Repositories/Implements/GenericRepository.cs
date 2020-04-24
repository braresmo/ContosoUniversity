using ContosoUniversity.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoUniversity.Repositories.Implements
{
    public class GenericRepository<Tentity> : IGenericRepository<Tentity> where Tentity : class
    {

        private readonly SchoolContext _context;

        public GenericRepository(SchoolContext context)
        {
            _context = context;
        }

        public async Task Delete(int id)
        {
            var entity = await GetById(id);

            if (entity == null) 
                throw new System.Exception("The entity is null");
            

            _context.Set<Tentity>().Remove(entity);
            await _context.SaveChangesAsync();
            
        }

        public async Task<List<Tentity>> GetAll()
        {
            return await _context.Set<Tentity>().ToListAsync();
        }

        public async Task<Tentity> GetById(int id)
        {
            return await  _context.Set<Tentity>().FindAsync(id);
        }

        public async Task<Tentity> Insert(Tentity entity)
        {
            await _context.Set<Tentity>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Tentity> Update(Tentity entity)
        {
             _context.Set<Tentity>().Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
