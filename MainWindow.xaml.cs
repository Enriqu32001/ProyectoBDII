using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ProyectoLogin.Models.DAO;
using ProyectoLogin.Models.DAO.Cache;
using ProyectoLogin.Views.Pages;
using System.Runtime.InteropServices;
using System.Windows.Interop;

namespace ProyectoLogin
{
	
	public partial class MainWindow : Window
	{
		private bool vand = true;
		private bool vand2 = true;

		//Se usa en el metodo para poder arrastrar el form
		[DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
		private extern static void ReleaseCapture();

		[DllImport("user32.DLL", EntryPoint = "SendMessage")]
		private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

		//Hasta aqui

		public MainWindow()
		{
			InitializeComponent();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			if (vand)
			{
				gridPrinc.Width = 100;
				vand = false;
			}
			else
			{
				gridPrinc.Width = 150;
				vand = true;
			}
		}

		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
			if(MessageBox.Show("¿Estas seguro de cerrar sesion? ","¡Cuidado!",
				MessageBoxButton.YesNo,MessageBoxImage.Warning) == MessageBoxResult.Yes)
			{
				this.Close();
			}
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			LoadUserData();

			//Manage permissions

			if(UserLoginCache.user.TypeUser == TypeUsers.Administrator.ToString())
			{
				txbTypeUser1.Text = TypeUsers.Administrator.ToString();
			}
			else if (UserLoginCache.user.TypeUser == TypeUsers.Manager.ToString())
			{
				txbTypeUser1.Text = TypeUsers.Manager.ToString();
			}
			else if (UserLoginCache.user.TypeUser == TypeUsers.User.ToString())
			{
				txbTypeUser1.Text = TypeUsers.User.ToString();
			}

			//MainFrame.NavigationService.Navigate(new Test());
			
		}

		private void LoadUserData()
		{
			txbName.Text = UserLoginCache.user.FirstName+ " "+UserLoginCache.user.LastName+".";
			txbTypeUser.Text = UserLoginCache.user.TypeUser + ".";
			txbEmail.Text = UserLoginCache.user.Email;
		}

		private void PanelTitleBar_MouseMove(object sender, MouseEventArgs e)
		{
			ReleaseCapture();
			SendMessage(new WindowInteropHelper(this).Handle, 0x112, 0xf012, 0);
		}

		private void btnMinimized_Click(object sender, RoutedEventArgs e)
		{
			this.WindowState = WindowState.Minimized;
		}

		private void btnMaxMin_Click(object sender, RoutedEventArgs e)
		{
			if (vand2)
			{
				this.WindowState = WindowState.Maximized;
				if (this.WindowState == WindowState.Maximized)
				{
					imgbtnMaxMin.Source = obtImagen("pack://application:,,,/images/normal_screen_64px.png");
				}
				vand2 = false;
			}
			else
			{
				this.WindowState = WindowState.Normal;
				if (this.WindowState == WindowState.Normal)
				{
					imgbtnMaxMin.Source = obtImagen("pack://application:,,,/images/full_screen_64px.png");
				}
				vand2 = true;
			}
		}

		private void btnClose_Click(object sender, RoutedEventArgs e)
		{
			if (MessageBox.Show("¿Estas seguro de salir? ", "¡Cuidado!",
				MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
			{
				this.Close();
			}
		}

		private ImageSource obtImagen(string direccion)
		{
			Image img = new Image(); 

			BitmapImage bi = new BitmapImage();
			bi.BeginInit();
			bi.UriSource = new System.Uri(direccion);
			bi.EndInit();

			img.Source = bi;
			ImageSource imgsource = bi;

			ImageBrush ib = new ImageBrush();
			ib.ImageSource = bi;
			
			return imgsource;
		}
	}

}
