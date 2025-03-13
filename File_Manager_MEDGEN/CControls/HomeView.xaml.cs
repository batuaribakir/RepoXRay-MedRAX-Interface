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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace File_Manager_MEDGEN.CControls
{
    /// <summary>
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class HomeView : UserControl
    {
        public HomeView()
        {
            InitializeComponent();
        }

        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            var button = sender as Button;
            var WidthAnimation = new DoubleAnimation() { To = 175, Duration = TimeSpan.FromSeconds(0.3) };
            var HeightAnimation = new DoubleAnimation() { To = 190, Duration = TimeSpan.FromSeconds(0.3) };

            button.BeginAnimation(Button.WidthProperty, WidthAnimation);
            button.BeginAnimation(Button.HeightProperty, HeightAnimation);
        }

        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {
            var button = sender as Button;
            var WidthAnimation = new DoubleAnimation() { To = 165, Duration = TimeSpan.FromSeconds(0.3) };
            var HeightAnimation = new DoubleAnimation() { To = 180, Duration = TimeSpan.FromSeconds(0.3) };

            button.BeginAnimation(Button.WidthProperty, WidthAnimation);
            button.BeginAnimation(Button.HeightProperty, HeightAnimation);
        }
    }
}