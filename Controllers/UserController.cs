using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoLogin.Models.DAO;
using ProyectoLogin.Models.DAO.Cache;

namespace ProyectoLogin.Controllers
{
	class UserController
	{
		UserDao userDao = new UserDao();

		public byte LoginUser(string user, string pass)
		{
			return userDao.Login(user, pass);
		}

		public void UserPrivileges()
		{
			//Security and Permissions
			if (UserLoginCache.user.TypeUser == TypeUsers.Administrator.ToString())
			{
				
			}
			else if (UserLoginCache.user.TypeUser == TypeUsers.Manager.ToString())
			{

			}
			else if (UserLoginCache.user.TypeUser == TypeUsers.User.ToString())
			{

			}
		}
	}
}
