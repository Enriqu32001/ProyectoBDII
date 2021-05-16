using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoLogin.Models.DIO
{
	public class User
	{
		public string LoginName { get; set; }
		public string Password { get; set; }
		public int UserID { set; get; }
		public string FirstName { set; get; }
		public string LastName { set; get; }
		public string TypeUser { set; get; }
		public string Email { set; get; }
	}
}
