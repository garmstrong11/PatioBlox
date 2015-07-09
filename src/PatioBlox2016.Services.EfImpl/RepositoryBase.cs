namespace PatioBlox2016.Services.EfImpl
{
	using System;
	using System.Collections.Generic;
	using System.Data.Entity;
	using System.Linq;
	using System.Linq.Expressions;
	using System.Threading.Tasks;
	using Concrete;
	using Contracts;

	public class RepositoryBase<T> : IRepository<T> where T : class
	{
		    protected DbContext Context;
 
        public RepositoryBase(DbContext context)
        {
            Context = context;
        }
 
        public ICollection<T> GetAll()
        {
            return Context.Set<T>().ToList();
        }
 
        public async Task<ICollection<T>> GetAllAsync()
        {
            return await Context.Set<T>().ToListAsync();
        }
 
        public Maybe<T> Get(int id)
        {
          var found = Context.Set<T>().Find(id);
	        return found == null ? new Maybe<T>() : new Maybe<T>(found);
        }
 
        public async Task<Maybe<T>> GetAsync(int id)
        {
          var found = await Context.Set<T>().FindAsync(id);
					return found == null ? new Maybe<T>() : new Maybe<T>(found);
        }
 
        public Maybe<T> Find(Expression<Func<T, bool>> match)
        {
	        var found = Context.Set<T>().SingleOrDefault(match);
  				return found == null ? new Maybe<T>() : new Maybe<T>(found);
        }
 
        public async Task<Maybe<T>> FindAsync(Expression<Func<T, bool>> match)
        {
          var found = await Context.Set<T>().SingleOrDefaultAsync(match);
	        return found == null ? new Maybe<T>() : new Maybe<T>(found);
        }
 
        public ICollection<T> FindAll(Expression<Func<T, bool>> match)
        {
            return Context.Set<T>().Where(match).ToList();
        }
 
        public async Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> match)
        {
            return await Context.Set<T>().Where(match).ToListAsync();
        }
 
        public T Add(T t)
        {
            Context.Set<T>().Add(t);
            Context.SaveChanges();
            return t;
        }
 
        public async Task<T> AddAsync(T t)
        {
            Context.Set<T>().Add(t);
            await Context.SaveChangesAsync();
            return t;
        }
 
        public Maybe<T> Update(T updated,int key)
        {
          // If arg is null, return an empty Maybe<T>:
					if (updated == null) return new Maybe<T>();
 
          var existing = Context.Set<T>().Find(key);
					// If arg is not found in data source, return an empty Maybe<T>:
	        if (existing == null) return new Maybe<T>();

					// If arg is found in data source, update existing's values with arg's,
					// and return a Maybe<T> with the updated existing as the sole element.
	        Context.Entry(existing).CurrentValues.SetValues(updated);
	        Context.SaveChanges();

	        return new Maybe<T>(existing);
        }
 
        public async Task<Maybe<T>> UpdateAsync(T updated, int key)
        {
	        if (updated == null) return new Maybe<T>();
 
          var existing = await Context.Set<T>().FindAsync(key);
	        if (existing == null) return new Maybe<T>();

	        Context.Entry(existing).CurrentValues.SetValues(updated);
	        await Context.SaveChangesAsync();

	        return new Maybe<T> (existing);
        }
 
        public void Delete(T t)
        {
	        Context.Set<T>().Remove(t);
          Context.SaveChanges();
        }
 
        public async Task<int> DeleteAsync(T t)
        {
            Context.Set<T>().Remove(t);
            return await Context.SaveChangesAsync();
        }
 
        public int Count()
        {
            return Context.Set<T>().Count();
        }
 
        public async Task<int> CountAsync()
        {
            return await Context.Set<T>().CountAsync();
        }
 
	}
}