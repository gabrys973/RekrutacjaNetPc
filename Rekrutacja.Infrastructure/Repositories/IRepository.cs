using Rekrutacja.Domain;

namespace Rekrutacja.Infrastructure.Repositories;

public interface IRepository<TEntity> where TEntity : Entity
{
    IQueryable<TEntity> GetAll();

    Task<TEntity?> GetByIdAsync(int id);
}