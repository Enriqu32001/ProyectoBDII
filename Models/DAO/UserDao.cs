using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using ProyectoLogin.Models.DAO.Cache;
using System.Windows;

namespace ProyectoLogin.Models.DAO
{
	class UserDao : ConnectionToSql
	{
		

		public byte Login(string user, string pass)
		{
			using(var connection = GetConnection())
			{
				try
				{
					connection.Open();

					using (var command = new SqlCommand())
					{
						command.Connection = connection;
						command.CommandText = "EXEC ObtenerTablaUsuariovalido @user, @pass ";
						command.Parameters.AddWithValue("@user", user);
						command.Parameters.AddWithValue("@pass", pass);
						command.CommandType = CommandType.Text;
						SqlDataReader reader = command.ExecuteReader();

						if (reader.HasRows)
						{
							while (reader.Read())
							{
								UserLoginCache.user.UserID = reader.GetInt32(0);
								UserLoginCache.user.FirstName = reader.GetString(1);
								UserLoginCache.user.LastName = reader.GetString(2);
								UserLoginCache.user.TypeUser = reader.GetString(3);
								UserLoginCache.user.Email = reader.GetString(4);
							}
							return 1;
						}
						else
						{
							return 0;
						}


					}

				}
				catch(Exception e)
				{
					if (MessageBox.Show(e.Message + " ¿Reintentar Conexion?", "ERROR", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
					{
						Login(user, pass);
					}
					return 2;
				}
				finally
				{
					connection.Close();
				}

				
			}
			
		}
	

		public void UserPrivileges()
		{
			if (UserLoginCache.user.TypeUser ==  TypeUsers.Administrator.ToString())
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
