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
    /// Interaction logic for WorkingWindow.xaml
    /// </summary>
    /// 


    public partial class WorkingWindow : Window
    {
        private FilledInfos FilledInfos;

        public WorkingWindow()
        {
            InitializeComponent();
            FilledInfos = new FilledInfos("");
        }

        private void HelpButton_Click(object sender, RoutedEventArgs e)
        {
            HelperWindow helperWindow = new HelperWindow();
            helperWindow.DisplayImage("Images/RegiondeVent.png");
            helperWindow.Show();
        }

        private void HelpButton2_Click(object sender, RoutedEventArgs e)
        {
            HelperWindowMultiImages imagesWindow = new HelperWindowMultiImages();

            // Chemins relatifs aux images dans le dossier du projet
            string[] imagePaths = {
                "Images/R0.png",
                "Images/R2.png",
                "Images/R3a.png",
                "Images/R3b.png",
                "Images/R4.png"
            };

            imagesWindow.SetImages(imagePaths);
            imagesWindow.Show();
        }

        private void insidecheckbx_Checked(object sender, RoutedEventArgs e)
        {
            if (insidecheckbx.IsChecked == true) 
            {
                insidecheckbx.IsEnabled = false;
                outsidecheckbx.IsEnabled = false;
                FilledInfos.vitrageoutside = false;
            }
        }

        private void outisdecheckbx_Checked(object sender, RoutedEventArgs e)
        {
            if(outsidecheckbx.IsChecked == true) 
            {
                outsidecheckbx.IsEnabled = false;
                insidecheckbx.IsEnabled = false; 
            }
        }

        private void CancelCheckingInsideOutside() 
        {
            outsidecheckbx.IsChecked = false;
            insidecheckbx.IsChecked= false;
            outsidecheckbx.IsEnabled = true;
            insidecheckbx.IsEnabled = true;
            FilledInfos.vitrageoutside = true;
        }

        private void CancelcheckboxFranceOutreMer() 
        {
            Francecheckbx.IsChecked = false;
            OutreMercheckbx.IsChecked = false;
            Francecheckbx.IsEnabled = true;
            OutreMercheckbx.IsEnabled= true;
            FilledInfos.inFrance = true; 
        }

        private void CancelcbxFranceOutreMer() 
        {
            Windlbl.Visibility = Visibility.Hidden;
            HelpButton.Visibility = Visibility.Hidden;
            Regioncbx.Text = null;
            RegionOutreMercbx.Text = null;
            Regioncbx.Visibility = Visibility.Hidden;
            RegionOutreMercbx.Visibility = Visibility.Hidden;
            FilledInfos.IndexRegion = -1;
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            CancelCheckingInsideOutside();
            CancelcheckboxFranceOutreMer();
            CancelcbxFranceOutreMer();
        }

        private void Francecheckbx_Checked(object sender, RoutedEventArgs e)
        {
            if (!(bool)insidecheckbx.IsChecked && !(bool)outsidecheckbx.IsChecked) 
            {
                MessageBox.Show("Cocher les cases dans le bon ordre (de haut en bas)");
                Francecheckbx.IsChecked = false;
            }
            else 
            {
                Francecheckbx.IsEnabled = false;
                OutreMercheckbx.IsEnabled = false;
                Windlbl.Visibility = Visibility.Visible;
                Regioncbx.Visibility = Visibility.Visible;
                HelpButton.Visibility = Visibility.Visible;
            }
        }

        private void OutreMercheckbx_Checked(object sender, RoutedEventArgs e)
        {
            if (!(bool)insidecheckbx.IsChecked && !(bool)outsidecheckbx.IsChecked)
            {
                MessageBox.Show("Cocher les cases dans le bon ordre (de haut en bas)");
                OutreMercheckbx.IsChecked = false;
            }
            else 
            {
                Francecheckbx.IsEnabled = false;
                OutreMercheckbx.IsEnabled = false;
                Windlbl.Visibility = Visibility.Visible;
                RegionOutreMercbx.Visibility = Visibility.Visible;
                FilledInfos.inFrance = false;
            }
        }

        private void Regioncbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FilledInfos.IndexRegion = Regioncbx.SelectedIndex;
        }

        private void RegionOutreMercbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FilledInfos.IndexRegion = RegionOutreMercbx.SelectedIndex;
        }

        private void FieldTypecbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FilledInfos.IndexFieldType = FieldTypecbx.SelectedIndex;
        }

        private void Heightcbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FilledInfos.IndexHeight = Heightcbx.SelectedIndex;
        }

        private void Calculatepressure_Click(object sender, RoutedEventArgs e)
        {
            FilledInfos.CalculatePressure();
            MessageBox.Show("The Pressure is :" + FilledInfos.Pressure);
        }        
    }
}
