using ProdutosApp.Domain.Interfaces.Repositories;
using ProdutosApp.Infra.SqlServer.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdutosApp.Infra.SqlServer.Repositories
{
    public abstract class BaseRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey>
        where TEntity : class
    {
        private readonly DataContext? _dataContext;

        protected BaseRepository(DataContext? dataContext)
        {
            _dataContext = dataContext;
        }

        public virtual void Add(TEntity entity)
        {
            _dataContext?.Add(entity);
        }

        public virtual void Update(TEntity entity)
        {
            _dataContext?.Update(entity);
        }

        public virtual void Delete(TEntity entity)
        {
            _dataContext?.Remove(entity);
        }

        public virtual List<TEntity> GetAll()
        {
            return _dataContext?.Set<TEntity>().ToList();
        }

        public virtual List<TEntity> GetAll(Func<TEntity, bool> where)
        {
            return _dataContext?.Set<TEntity>().Where(where).ToList();
        }

        public virtual TEntity? GetById(TKey id)
        {
            return _dataContext?.Set<TEntity>().Find(id);
        }

        public virtual TEntity? Get(Func<TEntity, bool> where)
        {
            return _dataContext?.Set<TEntity>().FirstOrDefault(where);
        }

        public void Dispose()
        {
            _dataContext?.Dispose();
        }
    }
}
