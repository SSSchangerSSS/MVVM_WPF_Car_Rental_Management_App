using Microsoft.EntityFrameworkCore;
using Models.DbContexts;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Models.Services.Crud
{
    /// <summary>
    /// <see cref="Gcrud{T}"/> class implements <see cref="IGcrud{T}"/> interface.
    /// </summary>
    public class Gcrud<T> : IGcrud<T> where T : class, IIdentifier
    {
        private readonly CarRentalDbContextFactory _contextFactory;

        ///<summary>
        /// Initializes a new instance of the <see cref="Gcrud{T}"/> class.
        /// </summary>
        /// <param name="contextFactory">DbContext factory for <see cref="CarRentalDbContext"/></param>
        public Gcrud(CarRentalDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        /// <inheritdoc />
        public async Task<bool> Add(T entity)
        {
            try
            {
                using (CarRentalDbContext context = _contextFactory.CreateDbContext())
                {
                    var dbset = context.Set<T>();
                    dbset.Add(entity);
                    await context.SaveChangesAsync();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <inheritdoc />
        public async Task<IEnumerable<T>> GetAll()
        {
            using (CarRentalDbContext context = _contextFactory.CreateDbContext())
            {
                var dbset = context.Set<T>();
                return await dbset.ToListAsync();
            }
        }
        /// <inheritdoc />
        public async Task<T> GetRowByID(Guid id)
        {
            using (CarRentalDbContext context = _contextFactory.CreateDbContext())
            {
                var dbset = context.Set<T>();
                return await dbset.FirstOrDefaultAsync(e => e.Id == id);
            }
        }
        /// <inheritdoc />
        public async Task<bool> Delete(Guid ID)
        {
            try
            {
                using (CarRentalDbContext context = _contextFactory.CreateDbContext())
                {
                    var dbset = context.Set<T>();
                    T entity = dbset.Find(ID);
                    dbset.Remove(entity);
                    await context.SaveChangesAsync();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
        /// <inheritdoc />
        public async Task<bool> Delete(T entity)
        {
            try
            {
                using (CarRentalDbContext context = _contextFactory.CreateDbContext())
                {
                    var dbset = context.Set<T>();
                    dbset.Remove(entity);
                    await context.SaveChangesAsync();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
        /// <inheritdoc />
        public async Task<bool> Update(Guid id, T entity)
        {
            try
            {
                using (CarRentalDbContext context = _contextFactory.CreateDbContext())
                {
                    entity.Id = id;
                    var dbset = context.Set<T>();
                    dbset.Update(entity);
                    await context.SaveChangesAsync();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
