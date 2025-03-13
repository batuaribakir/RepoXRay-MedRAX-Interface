using File_Manager_MEDGEN.CControls;
using File_Manager_MEDGEN.View;
using File_Manager_MEDGEN.ViewModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Effects;

namespace File_Manager_MEDGEN
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnMaximize_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
            {
                this.WindowState = WindowState.Maximized;
            }
            else
            {
                this.WindowState = WindowState.Normal;
            }
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void HomeRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            // Assuming your MainWindow ViewModel is set as DataContext
            var viewModel = DataContext as MainWindowViewModel;
            if (viewModel != null)
            {
                viewModel.CurrentView = new HomeViewModel();
            }
        }
        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            var loginView = new File_Manager_MEDGEN.View.LoginView();
        
            var loginViewModel = loginView.DataContext as File_Manager_MEDGEN.ViewModel.LoginViewModel;

            if (loginViewModel != null)
            {
                loginViewModel.ClearLoginCredentials();
            }

            loginView.Show();

            this.Close();
        }
    }
}