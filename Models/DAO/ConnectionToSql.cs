using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;


namespace ProyectoLogin.Models.DAO
{
	public abstract class ConnectionToSql
	{
		private readonly string connectionString;
		private string MachineName;

		public ConnectionToSql()
		{
			MachineName = Environment.MachineName;
			connectionString = "Data Source="+MachineName+ ";Initial Catalog=Farmacia;Integrated Security=True";
			
		}

		protected SqlConnection GetConnection()
		{
			return new SqlConnection(connectionString);
		}
	}
}
