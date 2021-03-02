using Microsoft.EntityFrameworkCore;
using SignalR_Chat.Data.Context;
using System.Collections.Generic;
using System.Linq;

namespace SignalR_Chat.Services.Application.BaseService
{
    public class BaseService<TEntity> : IBaseService<TEntity>
        where TEntity : class
    {
        protected readonly DbSet<TEntity> _db;

        public BaseService(ChatContext context)
        {
            _db = context.Set<TEntity>();
        }

        public TEntity Add(TEntity entity)
        {
            _db.Add(entity);
            return entity;
        }

        public virtual IEnumerable<TEntity> GetAll() => _db.ToList();

        public TEntity GetById(int id) => _db.Find(id);
        
    }
}
