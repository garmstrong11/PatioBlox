namespace PatioBlox2016.Abstract
{
  using System;
  using System.Collections.Generic;
  using System.Linq.Expressions;
  using System.Threading.Tasks;

  public interface IRepository<T> where T : class
	{
		ICollection<T> GetAll();
		Task<ICollection<T>> GetAllAsync();

		Maybe<T> Get(int id);
		Task<Maybe<T>> GetAsync(int id);

		Maybe<T> Find(Expression<Func<T, bool>> match);
		Task<Maybe<T>> FindAsync(Expression<Func<T, bool>> match);

		ICollection<T> FindAll(Expression<Func<T, bool>> match);
		Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> match);

		T Add(T t);
		Task<T> AddAsync(T t);

		Maybe<T> Update(T updated,int key);
		Task<Maybe<T>> UpdateAsync(T updated, int key);

		void Delete(T t);
		Task<int> DeleteAsync(T t);

		int Count();
		Task<int> CountAsync();
	}
}