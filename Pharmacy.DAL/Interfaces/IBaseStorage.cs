namespace Pharmacy.DAL.Interfaces;

public interface IBaseStorage<T>
{
  Task Add(T item);
  Task Delete(T item);
  Task<T> Get(Guid id);
  Task<T> Update(T item);
  IQueryable<T> GetAll();
}