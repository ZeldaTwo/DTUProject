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

namespace GlassMetalProj
{
    /// <summary>
    /// Interaction logic for HelperWindowMultiImages.xaml
    /// </summary>
    public partial class HelperWindowMultiImages : Window
    {
        public HelperWindowMultiImages()
        {
            InitializeComponent();
        }

        public void SetImages(string[] imagePaths)
        {
            if (imagePaths.Length != 5)
                throw new ArgumentException("There must be exactly 5 image paths.");

            Image1.Source = new BitmapImage(new Uri(imagePaths[0], UriKind.RelativeOrAbsolute));
            Image2.Source = new BitmapImage(new Uri(imagePaths[1], UriKind.RelativeOrAbsolute));
            Image3.Source = new BitmapImage(new Uri(imagePaths[2], UriKind.RelativeOrAbsolute));
            Image4.Source = new BitmapImage(new Uri(imagePaths[3], UriKind.RelativeOrAbsolute));
            Image5.Source = new BitmapImage(new Uri(imagePaths[4], UriKind.RelativeOrAbsolute));
        }
    }
}
