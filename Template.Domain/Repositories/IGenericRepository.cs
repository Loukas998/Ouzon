
namespace Template.Domain.Repositories;

public interface IGenericRepository<T> where T : class
{
	Task<IEnumerable<T>> GetAllAsync();
	Task<T?> FindByIdAsync(int id);
	Task<T> AddAsync(T entity);
	Task DeleteAsync(T entity);
	Task SaveChangesAsync();

}
