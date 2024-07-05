using Microsoft.EntityFrameworkCore;
using Rekrutacja.Domain;
using Rekrutacja.Infrastructure.DataAccess;

namespace Rekrutacja.Infrastructure.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
{
    protected readonly ApplicationDbContext _context;
    private DbSet<TEntity> _dbSet;

    public Repository(ApplicationDbContext context)
    {
        _context = context;
    }

    protected DbSet<TEntity> Entities
    {
        get
        {
            if(_dbSet == null)
                _dbSet = _context.Set<TEntity>();

            return _dbSet;
        }
    }

    public IQueryable<TEntity> GetAll()
    {
        return Entities.AsQueryable<TEntity>();
    }

    public async Task<TEntity?> GetByIdAsync(int id)
    {
        return await Entities.FirstOrDefaultAsync(x => x.Id == id);
    }
}