using Microsoft.EntityFrameworkCore;
using Template.Domain.Repositories;
using Template.Infrastructure.Persistence;

namespace Template.Infrastructure.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
	protected TemplateDbContext dbContext;
	public GenericRepository(TemplateDbContext dbContext)
	{
		this.dbContext = dbContext;
	}
	public async Task<T> AddAsync(T entity)
	{
		dbContext.Set<T>().Add(entity);
		await dbContext.SaveChangesAsync();
		return entity;
	}

	public async Task DeleteAsync(T entity)
	{
		dbContext.Remove(entity);
		await dbContext.SaveChangesAsync();
	}

	public async Task<T?> FindByIdAsync(int id)
	{
		return await dbContext.Set<T>().FindAsync(id);
	}

	public async Task<IEnumerable<T>> GetAllAsync()
	{
		return await dbContext.Set<T>().ToListAsync();
	}

	public async Task SaveChangesAsync()
	{
		await dbContext.SaveChangesAsync();
	}
}
