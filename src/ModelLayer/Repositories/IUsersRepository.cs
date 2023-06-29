using ModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Repositories
{
    public interface IUsersRepository
    {
        Task SaveAllUsers(IEnumerable<UserModel> users);
        Task UpdateUser(UserModel user);
        int GetUsersCount();
    }
}
