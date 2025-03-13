using Microsoft.Win32;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.IO;
using System.Data.SqlClient;
using File_Manager_MEDGEN.View;
using File_Manager_MEDGEN.Data;

namespace File_Manager_MEDGEN.CControls
{
    /// <summary>
    /// Interaction logic for ReportsView.xaml
    /// </summary>
    public partial class ReportsView : UserControl
    {
        public ReportsView()
        {
            InitializeComponent();
            LoadFilesFromDatabase();
        }
        private void UploadFile_Click(object sender, RoutedEventArgs e)
        {
            // Open file dialog to select a file
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpg)|*.png;*.jpg|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                // Load selected file into recent files section
                AddRecentFile(openFileDialog.FileName);
                SaveFilePathToDatabase(openFileDialog.FileName);
            }
        }

        private void LoadFilesFromDatabase()
        {
            using (var db = new ReportsDbContext())
            {
                var files = db.UploadedFiles.ToList();
                foreach (var file in files)
                {
                    AddRecentFile(file.FilePath);
                }
            }
        }

        // Add file to recent files with icon and name
        private void AddRecentFile(string filePath)
        {
            // Create stack panel for file preview
            StackPanel filePreviewPanel = new StackPanel
            {
                Orientation = Orientation.Vertical,
                Margin = new Thickness(10)
            };

            // Create image preview
            Image imagePreview = new Image
            {
                Width = 100,
                Height = 100,
                Source = new BitmapImage(new Uri(filePath)),
                Stretch = System.Windows.Media.Stretch.UniformToFill
            };
            filePreviewPanel.Children.Add(imagePreview);

            // Create text block for file name
            TextBlock fileNameText = new TextBlock
            {
                Text = Path.GetFileName(filePath),
                HorizontalAlignment = HorizontalAlignment.Center
            };
            filePreviewPanel.Children.Add(fileNameText);

            // Set double-click event to open the file in custom image viewer
            filePreviewPanel.MouseLeftButtonDown += (s, e) =>
            {
                if (e.ClickCount == 2)
                {
                    // Open the file in your custom WPF image viewer
                    ImagePreviewWindow viewer = new ImagePreviewWindow(filePath);
                    viewer.Show();  // Open the image viewer window
                }
            };

            // Add file preview to RecentFilesPanel
            RecentFilesPanel.Children.Add(filePreviewPanel);
        }


        private void SaveFilePathToDatabase(string filePath)
        {
            using (var db = new ReportsDbContext())
            {
                var uploadedFile = new UploadedFile { FilePath = filePath };
                db.UploadedFiles.Add(uploadedFile);
                db.SaveChanges();
            }
        }


    }
}
