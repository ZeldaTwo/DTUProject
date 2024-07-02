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
            ImagesPanel.Children.Clear();

            foreach (string imagePath in imagePaths)
            {
                Image image = new Image
                {
                    Source = new BitmapImage(new Uri(imagePath, UriKind.RelativeOrAbsolute)),
                    Stretch = System.Windows.Media.Stretch.Uniform,
                    Margin = new Thickness(10),
                    Width = 200,  // Set a default width
                    Height = 200  // Set a default height
                };

                ImagesPanel.Children.Add(image);
            }
        }
    }
}
