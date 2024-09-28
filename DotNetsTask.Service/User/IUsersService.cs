
using DotNetsTask.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DotNetsTask.Services
{
    public interface IUsersService
    {
        User GetUserByEmailOrUserName(string userName);

        User GetUserByEmail(string email);

        User GetUserById(long id);

        Task UpdateUser(User user);
		Task SaveUser(User user);


	}
}
