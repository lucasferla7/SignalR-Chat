using System.Collections.Generic;

namespace SignalR_Chat.Services.Application.BaseService
{
    public interface IBaseService<TEntity> where TEntity : class
    {
        TEntity Add(TEntity viewModel);
        TEntity GetById(int id);
        IEnumerable<TEntity> GetAll();

    }
}
