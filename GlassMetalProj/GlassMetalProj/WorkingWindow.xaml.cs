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
        }

        private void CancelcbxFranceOutreMer() 
        {
            Windlbl.Visibility = Visibility.Hidden;
            HelpButton.Visibility = Visibility.Hidden;
            Regioncbx.Text = null;
            RegionOutreMercbx.Text = null;
            Regioncbx.Visibility = Visibility.Hidden;
            RegionOutreMercbx.Visibility = Visibility.Hidden;
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            CancelCheckingInsideOutside();
            CancelcheckboxFranceOutreMer();
            CancelcbxFranceOutreMer();
        }

        private void Francecheckbx_Checked(object sender, RoutedEventArgs e)
        {
            Francecheckbx.IsEnabled = false;
            OutreMercheckbx.IsEnabled = false;
            Windlbl.Visibility = Visibility.Visible;
            Regioncbx.Visibility = Visibility.Visible;
            HelpButton.Visibility = Visibility.Visible;

        }

        private void OutreMercheckbx_Checked(object sender, RoutedEventArgs e)
        {
            Francecheckbx.IsEnabled = false;
            OutreMercheckbx.IsEnabled = false;
            Windlbl.Visibility= Visibility.Visible;
            RegionOutreMercbx.Visibility = Visibility.Visible;
        }
    }
}
