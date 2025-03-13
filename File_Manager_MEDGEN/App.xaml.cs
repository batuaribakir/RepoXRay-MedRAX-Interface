using System.Configuration;
using System.Data;
using System.IO;
using System.Windows;
using File_Manager_MEDGEN.Data;
using File_Manager_MEDGEN.View;

namespace File_Manager_MEDGEN
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected void ApplicationStart(object sender, StartupEventArgs e)
        {
            // Create database automatically
            InitializeDatabase();

            // Show login screen
            ShowLoginView();
        }

        private void InitializeDatabase()
        {
            var dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ReportsDb.sqlite");
            Console.WriteLine($"The database file was created at: {dbPath}");

            using (var db = new ReportsDbContext())
            {
                db.Database.EnsureCreated(); // Creates database and tables automatically
            }
        }

        private void ShowLoginView()
        {
            var loginView = new LoginView();
            loginView.Show();

            loginView.IsVisibleChanged += (s, ev) =>
            {
                if (loginView.IsVisible == false && loginView.IsLoaded)
                {
                    var mainView = new MainWindow();
                    mainView.Show();

                    mainView.Closed += (sender, args) =>
                    {
                        ShowLoginView();
                    };
                }
            };
        }
    }
}