using Microsoft.AspNetCore.Http;
using SignalR_Chat.Data.Context;
using SignalR_Chat.Models;
using SignalR_Chat.Services.Application.BaseService;
using System.Collections.Generic;
using System.Linq;

namespace SignalR_Chat.Services.Application.UserServices
{
    public class UserService : BaseService<User>, IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(ChatContext context, IHttpContextAccessor httpContextAccessor) : base(context)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public override IEnumerable<User> GetAll()
        {
            List<User> users = base.GetAll().ToList();
            return RemoveCurrentUser(ref users);
        }

        private IEnumerable<User> RemoveCurrentUser(ref List<User> users)
        {
            int userId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirst(p => p.Type == "UserId").Value);
            User currentUser = users.First(p => p.Id == userId);
            users.Remove(currentUser);
            return users;
        }
    }
}
