using H3Project.Data.Context;
using H3Project.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace H3Project.Data.Repository;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly CinemaDbContext _cinemaDbContext;
    private readonly DbSet<T> _dbSet;

    public Repository(CinemaDbContext cinemaDbContext)
    {
        _cinemaDbContext = cinemaDbContext;
        _dbSet = _cinemaDbContext.Set<T>();
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await _cinemaDbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        await _cinemaDbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);

        if (entity != null)
        {
            _dbSet.Remove(entity);
            await _cinemaDbContext.SaveChangesAsync();
        }
    }
}