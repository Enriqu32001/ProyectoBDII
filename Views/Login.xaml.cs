using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ProyectoLogin.Controllers;

namespace ProyectoLogin.Recursos.Views
{
	/// <summary>
	/// Lógica de interacción para Login.xaml
	/// </summary>
	/// 
	public partial class Login : Window
	{
		private bool vand = true;

		//Se usa en el metodo para poder arrastrar el form
		[DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
		private extern static void ReleaseCapture();

		[DllImport("user32.DLL", EntryPoint = "SendMessage")]
		private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

		//Hasta aqui

		public Login()
		{
			InitializeComponent();
		}

		private void btnIniSen_Click(object sender, RoutedEventArgs e)
		{

			if (!string.IsNullOrEmpty(txbUsuario.Text))
			{
				if (!string.IsNullOrEmpty(txbContra.Password))
				{
					UserController user = new UserController();
					var validLogin = user.LoginUser(txbUsuario.Text, txbContra.Password);
					if( validLogin == 1)
					{
						MainWindow mainWindow = new MainWindow();
						mainWindow.Show();
						mainWindow.Closed += Logout;
						mainWindow.Closed += txbContra_LostFocus;
						mainWindow.Closed += txbUsuario_LostFocus;
						this.Hide();
					}
					else if (validLogin == 0)
					{
						msgError("Usuario o contraseña incorrectos.\n Intentelo de nuevo.");
						
						txbContra.Clear();
						txbUsuario.Focusable = true;
					}
					else if (validLogin == 2)
					{
						msgError("Error en la conexion a la base de datos.");
						txbContra.Clear();
						txbUsuario.Focusable = true;
					}
				}
				else
				{
					msgError("Por favor ingrese su Contraseña primero. ");
				}
			}
			else
			{
				msgError("Por favor ingrese su Usuario primero. ");
			}			

			if (vand)
			{
				AnimationLoad("ErrorPasswordUser");
				vand = false;
			}
			else
			{
				AnimationLoad("ErrorPasswordUser1");
				vand = true;
			}


		}

		private void btnSalir_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		private void txbUsuario_GotFocus(object sender, RoutedEventArgs e)
		{
			AnimationLoad("Agrandar_1");
			txbUsuario.BorderBrush = Brushes.Black;
		}

		private void txbUsuario_LostFocus(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(txbUsuario.Text))
			{
				AnimationLoad("Encoger_1");
				txbUsuario.BorderBrush = new SolidColorBrush(Color.FromArgb(91, 91, 91, 0));
			}
		}

		private void txbContra_GotFocus(object sender, RoutedEventArgs e)
		{
			AnimationLoad("Agrandar_2");
			txbContra.BorderBrush = Brushes.Black;
		}

		private void txbContra_LostFocus(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(txbContra.Password))
			{
				VisualStateManager.GoToElementState(GridLogin, "Encoger_2", true);
				txbContra.BorderBrush = new SolidColorBrush(Color.FromArgb(91, 91, 91, 0));
			}
		}

		private void Logout(object sender , EventArgs e)
		{
			txbUsuario.Clear();
			txbContra.Clear();
			txtMensaje.Visibility = Visibility.Hidden;
			this.Show();
			txbUsuario.Focusable = true;

		}

		private void AnimationLoad(string name)
		{
			VisualStateManager.GoToElementState(GridLogin, name, true);
		}

		private void msgError(string msg)
		{
			txtMensaje.Text = msg;
			txtMensaje.Visibility = Visibility.Visible;
			
		}

		private void TitleGrid_MouseMove(object sender, MouseEventArgs e)
		{
			ReleaseCapture();
			SendMessage(new WindowInteropHelper(this).Handle, 0x112, 0xf012, 0);
		}
	}
}
