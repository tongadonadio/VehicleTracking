using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VehicleTracking.Repository;
using VehicleTracking.Services.Exceptions;
using VehicleTracking.Services.ServiceImplementations;
using VehicleTracking.Services.Services;
using VehicleTracking.Web.Models;

namespace VehicleTracking.WinApp.Management
{
    public partial class LoginForm : Form
    {

        private UserService userService;

        public LoginForm()
        {
            InitializeComponent();
            this.userService = new UserServiceImpl(new UserDAOImp());            
            this.loginBtn.FlatStyle = FlatStyle.Flat;
            this.loginBtn.FlatAppearance.BorderSize = 0;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            LoginDTO userToLogIn = this.createLogInDTO();
            try
            {
                UserLoggedDTO userLogged = this.userService.LogIn(userToLogIn);
                if (userLogged.Role == "Administrador")
                {
                    Main main = new Main(userLogged);
                    this.Hide();
                    main.ShowDialog();
                } else
                {
                   MessageBox.Show(
                   "El usuario no tiene suficientes permisos.",
                   "Error",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            } catch (UserOrPasswordNotFoundException)
            {
                MessageBox.Show(
                    "El nombre de usuario o contraseña no son correctos.", 
                    "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private LoginDTO createLogInDTO()
        {
            LoginDTO login = new LoginDTO();
            login.UserName = this.txtUserName.Text;
            login.Password = this.txtPassword.Text;
            return login;
        }

    }
}
