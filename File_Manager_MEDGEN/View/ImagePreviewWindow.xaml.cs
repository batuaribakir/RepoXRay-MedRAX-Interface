using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using Microsoft.Win32;
using PdfSharp.Drawing.Layout;
using Newtonsoft.Json;
using File_Manager_MEDGEN.Utils;

namespace File_Manager_MEDGEN.View
{
    /// <summary>
    /// Interaction logic for ImagePreviewWindow.xaml
    /// </summary>
    public partial class ImagePreviewWindow : Window
    {
        private string _imagePath;

        public ImagePreviewWindow(string imagePath)
        {
            InitializeComponent();
            _imagePath = imagePath;
            PreviewImage.Source = new BitmapImage(new Uri(_imagePath));
        }

        // Closing the window when the Close button is pressed
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        // Creating the report section when the Create Report button is clicked
        private void GenerateReport_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // ONNX model path
                string modelPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Models", "yolo11n.onnx");

                // ONNX model classifier
                var classifier = new ONNXClassifier(modelPath);

                // Classify image
                string result = classifier.ClassifyImage(_imagePath);

                // Show report
                ReportPanel.Visibility = Visibility.Visible;  // Make the right report panel visible
                ReportContent.Text = result;  // Add prediction result to report
                ReportDateTime.Text = DateTime.Now.ToString("dd MMMM yyyy - HH:mm");  // Add date and time
                SaveAsPDFButton.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SaveAsPDF_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "PDF file (*.pdf)|*.pdf",
                FileName = "MedicalReport.pdf"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                string pdfPath = saveFileDialog.FileName;

                PdfDocument document = new PdfDocument();
                document.Info.Title = "Medical Report";
                PdfPage page = document.AddPage();
                XGraphics gfx = XGraphics.FromPdfPage(page);

                XFont titleFont = new XFont("Arial Bold", 18);
                XFont contentFont = new XFont("Arial", 12);

                // Header
                gfx.DrawString("Medical Report", titleFont, XBrushes.Black, new XPoint(40, 60));

                // Report content
                XTextFormatter tf = new XTextFormatter(gfx);
                XRect contentRect = new XRect(40, 100, page.Width - 80, page.Height - 150);

                tf.Alignment = XParagraphAlignment.Left;
                tf.DrawString(ReportContent.Text, contentFont, XBrushes.Black, contentRect);

                // Date and time
                gfx.DrawString(ReportDateTime.Text, contentFont, XBrushes.Gray, new XPoint(page.Width - 120, page.Height - 40));

                // Save the file
                document.Save(pdfPath);
                MessageBox.Show("Saved as PDF", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }

}
