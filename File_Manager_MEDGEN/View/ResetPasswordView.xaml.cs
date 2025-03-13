using File_Manager_MEDGEN.Model;
using File_Manager_MEDGEN.Repositories;
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
using System.Windows.Shapes;


namespace File_Manager_MEDGEN.View
{
    public partial class ResetPasswordView : Window
    {
        private IUserRepository userRepository;

        public ResetPasswordView()
        {
            InitializeComponent();
            userRepository = new UserRepository();
        }

        private void ResetPassword_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text;
            string newPassword = txtNewPassword.Password;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(newPassword))
            {
                txtResult.Text = "Username and password cannot be empty.";
                return;
            }

            bool result = userRepository.ResetPassword(username, newPassword);

            if (result)
            {
                txtResult.Text = "Password reset successfully!";
            }
            else
            {
                txtResult.Text = "Username not found!";
            }
        }
    }
}