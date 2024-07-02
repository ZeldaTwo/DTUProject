using ControlzEx.Theming;
using MahApps.Metro.Controls;
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


    public partial class WorkingWindow : MetroWindow
    {
        private FilledInfos FilledInfos;

        public WorkingWindow()
        {
            InitializeComponent();
            FilledInfos = new FilledInfos("","");
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
            if (outsidecheckbx.IsChecked == true) 
            {
                outsidecheckbx.IsChecked = false;
            }
            FilledInfos.vitrageoutside = false;
        }

        private void outisdecheckbx_Checked(object sender, RoutedEventArgs e)
        {
            if(insidecheckbx.IsChecked == true) 
            {
                insidecheckbx.IsChecked = false;
            }
            FilledInfos.vitrageoutside = true;
        }

        //Cancel Functions
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

            //Cancel checkBox vertical / incliné 
            Verticalcheckbx.IsEnabled = true;
            Inclinedcheckbx.IsEnabled = true;
            Verticalcheckbx.IsChecked=false;
            Inclinedcheckbx.IsChecked=false;    

        }
        // End Cancel Functions

        private void Francecheckbx_Checked(object sender, RoutedEventArgs e)
        {
            if (OutreMercheckbx.IsChecked == true) 
            {
                OutreMercheckbx.IsChecked = false;
                RegionOutreMercbx.Visibility = Visibility.Hidden;
                RegionOutreMercbx.SelectedIndex = -1;
            }
            Windlbl.Visibility = Visibility.Visible;
            Regioncbx.Visibility = Visibility.Visible;
            HelpButton.Visibility = Visibility.Visible;
        }

        private void OutreMercheckbx_Checked(object sender, RoutedEventArgs e)
        {
            if (Francecheckbx.IsChecked == true)
            {
                Regioncbx.SelectedIndex = -1;
                Francecheckbx.IsChecked = false;
                Regioncbx.Visibility = Visibility.Hidden;
                HelpButton.Visibility = Visibility.Hidden;
            }
            Windlbl.Visibility= Visibility.Visible;
            RegionOutreMercbx.Visibility= Visibility.Visible;
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
            if (Heightcbx.SelectedIndex == 5) 
            {
                MessageBox.Show("Veuillez renseigner la pression à la main, lorsque nous sommes à plus de 100m, la pression doit être étudiée en soufflerie.");
            }
            else 
            {
                FilledInfos.IndexHeight = Heightcbx.SelectedIndex;
            }
        }

        private void Calculatepressure_Click(object sender, RoutedEventArgs e)
        {
            FilledInfos.CalculatePressure();
            MessageBox.Show("La pression de vent est :" + FilledInfos.Pressure);
        }

        private void Inclinedcheckbx_Checked(object sender, RoutedEventArgs e)
        {
            if(Verticalcheckbx.IsChecked == true) 
            {
                Verticalcheckbx.IsChecked = false;
            }
            FilledInfos.inclined = true;
        }

        private void Verticalcheckbx_Checked(object sender, RoutedEventArgs e)
        {
            if (Inclinedcheckbx.IsChecked == true) 
            {
                Inclinedcheckbx.IsChecked=false;
            }
            FilledInfos.inclined = false;
        }
    }
}
