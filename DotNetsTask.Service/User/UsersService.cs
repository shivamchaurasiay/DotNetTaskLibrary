
using DotNetsTask.Data.Models;
using DotNetsTask.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace DotNetsTask.Services

{
    public class UsersService : IUsersService
    {
      
        IRepository<User> repoUsers;

        
        public UsersService(IRepository<User> _repoUsers)
        {
            repoUsers = _repoUsers;
        }


        public User GetUserByEmail(string email)
        {
            return repoUsers.Query().Filter(x => x.Email.Equals(email)).Get().FirstOrDefault();
        }


        public User GetUserByEmailOrUserName(string userName)
        {
            return repoUsers.Query().Filter(x => x.Email.Equals(userName) || x.Name.Equals(userName)).Get().FirstOrDefault();
        }

        public User GetUserById(long id)
        {
            return repoUsers.FindById(id);
        }

        public async Task UpdateUser(User user)
        {
            await repoUsers.UpdateAsync(user);
        }
		public async Task SaveUser(User user)
		{
			await repoUsers.InsertAsync(user);
		}



	}
}
