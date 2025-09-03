﻿using Microsoft.EntityFrameworkCore;
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
        var property = entity.GetType().GetProperty("IsDeleted");
        if (property != null && property.PropertyType == typeof(bool))
        {
            property.SetValue(entity, true);
            await dbContext.SaveChangesAsync();
        }

        else
        {
            throw new InvalidOperationException($"Entity {typeof(T).Name} Does not Have IsDeleted as a property");
        }
    }

    public async Task<T?> FindByIdAsync(int id)
    {
        return await dbContext.Set<T>().FindAsync(id);
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await dbContext.Set<T>().ToListAsync();
    }
    public async Task<IEnumerable<T>> GetPagedResponseAsync(int pageNumber, int pageSize)
    {
        return await dbContext.Set<T>().Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }
    public async Task UpdateAsync(T entity)
    {
        dbContext.Set<T>().Update(entity);
        await SaveChangesAsync();
    }
    public async Task SaveChangesAsync()
    {
        await dbContext.SaveChangesAsync();
    }

}
