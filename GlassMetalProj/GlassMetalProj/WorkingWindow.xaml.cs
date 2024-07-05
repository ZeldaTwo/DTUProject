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

        private double a = 0;
        private double b = 0;
        private double c = 0;
        private double d = 0;

        public WorkingWindow()
        {
            InitializeComponent();
            FilledInfos = new FilledInfos("","");

            //Disappearing textbox and Labels for dimensions.
            atxtbx.Visibility = Visibility.Hidden;
            btxtbx.Visibility = Visibility.Hidden;
            ctxtbx.Visibility = Visibility.Hidden;
            dtxtbx.Visibility = Visibility.Hidden;
            ltxtbx.Visibility = Visibility.Hidden;
            Ltxtbx.Visibility = Visibility.Hidden;

            albl.Visibility = Visibility.Hidden;
            blbl.Visibility = Visibility.Hidden;
            clbl.Visibility = Visibility.Hidden;
            dlbl.Visibility = Visibility.Hidden;
            lLabel.Visibility = Visibility.Hidden;
            LLabel.Visibility = Visibility.Hidden;

            lcalcultxtbx.IsEnabled = false;
            Lcalcultxtbx.IsEnabled = false;
        }

        private void HelpButton_Click(object sender, RoutedEventArgs e)
        {
            HelperWindow helperWindow = new HelperWindow();
            Button button = sender as Button;
            switch (button.Name) 
            {
                case "HelpButtonRegion":
                    helperWindow.DisplayImage("Images/RegiondeVent.png");
                    helperWindow.Show();
                    break;
                case "HelpButtonDimension":
                    helperWindow.DisplayImage("Images/shapes.png");
                    helperWindow.Show();
                    break;


            }
            
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
            HelpButtonRegion.Visibility = Visibility.Hidden;
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
            HelpButtonRegion.Visibility = Visibility.Visible;
        }

        private void OutreMercheckbx_Checked(object sender, RoutedEventArgs e)
        {
            if (Francecheckbx.IsChecked == true)
            {
                Regioncbx.SelectedIndex = -1;
                Francecheckbx.IsChecked = false;
                Regioncbx.Visibility = Visibility.Hidden;
                HelpButtonRegion.Visibility = Visibility.Hidden;
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
            if (!CheckEverythingIsChecked())
                MessageBox.Show("Remplissez toutes les informations!");
            else 
            {
                FilledInfos.CalculatePressure();
                Pressuretxtbx.Text = FilledInfos.Pressure + "";
            }
            
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

        private void Francecheckbx_Unchecked(object sender, RoutedEventArgs e)
        {
            Windlbl.Visibility = Visibility.Hidden;
            Regioncbx.Visibility = Visibility.Hidden;
            HelpButtonRegion.Visibility = Visibility.Hidden;
        }

        private void OutreMercheckbx_Unchecked(object sender, RoutedEventArgs e)
        {
            Windlbl.Visibility = Visibility.Hidden;
            RegionOutreMercbx.Visibility = Visibility.Hidden;
        }

        private void Below9mcheckbx_Checked(object sender, RoutedEventArgs e)
        {
            FilledInfos.c = 0.9;
        }

        private void Below9mcheckbx_Unchecked(object sender, RoutedEventArgs e)
        {
            FilledInfos.c = 1.0;
        }
        private bool CheckEverythingIsChecked()
        {
            if (insidecheckbx.IsChecked == true || outsidecheckbx.IsChecked == true)
            
            {
                if (Francecheckbx.IsChecked == true || OutreMercheckbx.IsChecked == true)
                
                {
                    if (Heightcbx.SelectedIndex != -1) 
                    {
                        if (Regioncbx.SelectedIndex != -1 || RegionOutreMercbx.SelectedIndex != -1) 
                        {
                            if (Inclinedcheckbx.IsChecked == true || Verticalcheckbx.IsChecked == true)
                            
                            {
                                if (FieldTypecbx.SelectedIndex != -1)
                                    return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        private void Dimensioncbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Dimensioncbx.SelectedIndex == 0) 
            {
                FilledInfos.L = 0;
                FilledInfos.l = 0;

                lLabel.Visibility = Visibility.Visible;
                LLabel.Visibility = Visibility.Visible;
                ltxtbx.Visibility = Visibility.Visible;
                Ltxtbx.Visibility = Visibility.Visible;

                albl.Visibility = Visibility.Hidden;
                blbl.Visibility = Visibility.Hidden;
                clbl.Visibility = Visibility.Hidden;
                dlbl.Visibility = Visibility.Hidden;

                atxtbx.Visibility = Visibility.Hidden;
                btxtbx.Visibility = Visibility.Hidden;
                ctxtbx.Visibility = Visibility.Hidden;
                dtxtbx.Visibility = Visibility.Hidden;
               

                ltxtbx.Text = string.Empty;
                Ltxtbx.Text = string.Empty;
                atxtbx.Text = string.Empty;
                btxtbx.Text = string.Empty;
                ctxtbx.Text = string.Empty;
                dtxtbx.Text = string.Empty;
                lcalcultxtbx.Text = string.Empty;
                Lcalcultxtbx.Text = string.Empty;

                ltxtbx.IsEnabled = true;
                Ltxtbx.IsEnabled = true;
                atxtbx.IsEnabled = true;
                btxtbx.IsEnabled = true;
                ctxtbx.IsEnabled = true;
                dtxtbx.IsEnabled = true;
            }

            if (Dimensioncbx.SelectedIndex == 1 || Dimensioncbx.SelectedIndex == 2) 
            {
                //Visible
                albl.Visibility = Visibility.Visible;
                blbl.Visibility = Visibility.Visible;

                atxtbx.Visibility = Visibility.Visible;
                btxtbx.Visibility = Visibility.Visible;

                //Hidden
                clbl.Visibility = Visibility.Hidden;
                dlbl.Visibility = Visibility.Hidden;

                atxtbx.Text = string.Empty;
                btxtbx.Text = string.Empty;

                ctxtbx.Text = string.Empty;
                ctxtbx.Visibility = Visibility.Hidden;
                dtxtbx.Text = string.Empty;
                dtxtbx.Visibility = Visibility.Hidden;

                lLabel.Visibility = Visibility.Hidden;
                LLabel.Visibility = Visibility.Hidden;
                ltxtbx.Visibility = Visibility.Hidden;
                Ltxtbx.Visibility = Visibility.Hidden;

                ltxtbx.Text = string.Empty;
                Ltxtbx.Text = string.Empty;
                lcalcultxtbx.Text = string.Empty;
                Lcalcultxtbx.Text = string.Empty;

                ltxtbx.IsEnabled = true;
                Ltxtbx.IsEnabled = true;
                atxtbx.IsEnabled = true;
                btxtbx.IsEnabled = true;
                ctxtbx.IsEnabled = true;
                dtxtbx.IsEnabled = true;
            }
            if (Dimensioncbx.SelectedIndex == 3) 
            {
                //Visible
                albl.Visibility = Visibility.Visible;
                blbl.Visibility = Visibility.Visible;
                clbl.Visibility = Visibility.Visible;

                atxtbx.Visibility= Visibility.Visible;
                btxtbx.Visibility= Visibility.Visible;
                ctxtbx.Visibility= Visibility.Visible;

                //Hidden
                dlbl.Visibility = Visibility.Hidden;
                dtxtbx.Visibility= Visibility.Hidden;

                dtxtbx.Text= string.Empty;
                dtxtbx.Visibility= Visibility.Hidden;

                lLabel.Visibility = Visibility.Hidden;
                LLabel.Visibility = Visibility.Hidden;
                ltxtbx.Visibility = Visibility.Hidden;
                Ltxtbx.Visibility = Visibility.Hidden;

                ltxtbx.Text = string.Empty;
                Ltxtbx.Text = string.Empty;
                atxtbx.Text= string.Empty;
                btxtbx.Text = string.Empty;
                ctxtbx.Text= string.Empty;
                lcalcultxtbx.Text = string.Empty;
                Lcalcultxtbx.Text = string.Empty;

                ltxtbx.IsEnabled = true;
                Ltxtbx.IsEnabled = true;
                atxtbx.IsEnabled = true;
                btxtbx.IsEnabled = true;
                ctxtbx.IsEnabled = true;
                dtxtbx.IsEnabled = true;
            }
            if (Dimensioncbx.SelectedIndex == 4) 
            {
                albl.Visibility= Visibility.Visible;
                blbl.Visibility= Visibility.Visible;
                clbl.Visibility= Visibility.Visible;
                dlbl.Visibility= Visibility.Visible;

                atxtbx.Visibility= Visibility.Visible;
                btxtbx.Visibility =Visibility.Visible;
                ctxtbx.Visibility= Visibility.Visible;
                dtxtbx.Visibility= Visibility.Visible;

                lLabel.Visibility = Visibility.Hidden;
                LLabel.Visibility = Visibility.Hidden;
                ltxtbx.Visibility = Visibility.Hidden;
                Ltxtbx.Visibility = Visibility.Hidden;

                ltxtbx.Text = string.Empty;
                Ltxtbx.Text = string.Empty;
                atxtbx.Text = string.Empty;
                btxtbx.Text = string.Empty;
                ctxtbx.Text= string.Empty;
                dtxtbx.Text = string.Empty;
                lcalcultxtbx.Text = string.Empty;
                Lcalcultxtbx.Text = string.Empty;

                ltxtbx.IsEnabled = true;
                Ltxtbx.IsEnabled = true;
                atxtbx.IsEnabled = true;
                btxtbx.IsEnabled = true;
                ctxtbx.IsEnabled = true;
                dtxtbx.IsEnabled = true;
            }
            if (Dimensioncbx.SelectedIndex == 5) 
            {
                albl.Visibility= Visibility.Visible;
                atxtbx.Visibility = Visibility.Visible;

                blbl.Visibility = Visibility.Hidden;
                clbl.Visibility = Visibility.Hidden;
                dlbl.Visibility = Visibility.Hidden;

                btxtbx.Visibility =Visibility.Hidden;
                ctxtbx.Visibility= Visibility.Hidden;
                dtxtbx.Visibility= Visibility.Hidden;

                lLabel.Visibility = Visibility.Hidden;
                LLabel.Visibility = Visibility.Hidden;
                ltxtbx.Visibility = Visibility.Hidden;
                Ltxtbx.Visibility = Visibility.Hidden;

                ltxtbx.Text = string.Empty;
                Ltxtbx.Text = string.Empty;
                atxtbx.Text = string.Empty;
                btxtbx.Text= string.Empty;
                ctxtbx.Text = string.Empty;
                dtxtbx.Text= string.Empty;
                lcalcultxtbx.Text = string.Empty;
                Lcalcultxtbx.Text = string.Empty;

                ltxtbx.IsEnabled = true;
                Ltxtbx.IsEnabled = true;
                atxtbx.IsEnabled = true;
                btxtbx.IsEnabled = true;
                ctxtbx.IsEnabled = true;
                dtxtbx.IsEnabled = true;
            }
            if (Dimensioncbx.SelectedIndex == 6) 
            {
                blbl.Visibility= Visibility.Visible;
                clbl.Visibility=Visibility.Visible;

                btxtbx.Visibility = Visibility.Visible;
                ctxtbx.Visibility= Visibility.Visible;

                albl.Visibility = Visibility.Hidden;
                dlbl.Visibility= Visibility.Hidden;

                atxtbx.Visibility= Visibility.Hidden;
                dtxtbx.Visibility = Visibility.Hidden;

                lLabel.Visibility = Visibility.Hidden;
                LLabel.Visibility = Visibility.Hidden;
                ltxtbx.Visibility = Visibility.Hidden;
                Ltxtbx.Visibility = Visibility.Hidden;

                ltxtbx.Text = string.Empty;
                Ltxtbx.Text = string.Empty;
                atxtbx.Text = string.Empty;
                btxtbx.Text = string.Empty;
                ctxtbx.Text = string.Empty;
                dtxtbx.Text = string.Empty;
                lcalcultxtbx.Text = string.Empty;
                Lcalcultxtbx.Text = string.Empty;

                ltxtbx.IsEnabled = true;
                Ltxtbx.IsEnabled = true;
                atxtbx.IsEnabled = true;
                btxtbx.IsEnabled = true;
                ctxtbx.IsEnabled = true;
                dtxtbx.IsEnabled = true;
            }
        }

        private void ValueEntered_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) 
            {
                TextBox textBox = sender as TextBox;
                if (textBox != null)
                {
                    double value = -1;
                    if (double.TryParse(textBox.Text, out value))
                    {
                        if (value <= 0)
                            MessageBox.Show("La valeur ne peut pas être inférieure ou égale à 0");
                        else
                        {
                            switch (textBox.Name)
                            {
                                case "ltxtbx":
                                    if (value > FilledInfos.L) 
                                    {
                                        MessageBox.Show("l ne peut pas être plus grand que L");
                                        ltxtbx.Text = string.Empty;
                                    }
                                    else 
                                    {
                                        FilledInfos.l = value;
                                        textBox.IsEnabled = false;
                                    }
                                    break;
                                case "Ltxtbx":
                                    if (value < FilledInfos.l) 
                                    {
                                        MessageBox.Show("L ne peut pas être plus petit que l");
                                        Ltxtbx.Text = string.Empty;
                                    }
                                    else 
                                    {
                                        textBox.IsEnabled = false;
                                        FilledInfos.L = value;
                                    }
                                    break;
                                case "atxtbx":
                                    a = value;
                                    textBox.IsEnabled = false;
                                    break;
                                case "btxtbx":
                                    b = value;
                                    textBox.IsEnabled = false;
                                    break;
                                case "ctxtbx":
                                    c = value;
                                    textBox.IsEnabled = false;
                                    break;
                                case "dtxtbx":
                                    d = value;
                                    textBox.IsEnabled = false;
                                    break;
                                default:
                                    throw new Exception("wrong text box");
                            }
                        }
                    }
                }
            }
            
        }

        private void CalculateDimensions_Click(object sender, RoutedEventArgs e)
        {
            if (Dimensioncbx.SelectedIndex == -1)
                MessageBox.Show("Veuillez sélectionner une forme");
            if (Dimensioncbx.SelectedIndex == 0) 
            {
                if (FilledInfos.l == 0 || FilledInfos.L == 0)
                    MessageBox.Show("Veuillez renseigner les longueurs L et l" + " (Après avoir entré une valeur, appuyez sur entrée)");
                else 
                {
                    Lcalcultxtbx.Text = FilledInfos.L.ToString();
                    lcalcultxtbx.Text = FilledInfos.l.ToString();
                }
            }
                
            if (Dimensioncbx.SelectedIndex == 1 || Dimensioncbx.SelectedIndex == 2)
            {
                if (a == 0 || b == 0)
                    MessageBox.Show("Veuillez renseigner les longueurs a et b." + " (Après avoir entré une valeur, appuyez sur entrée)");
                else 
                {
                    FilledInfos.CalculateDimensions(Dimensioncbx.SelectedIndex, a, b, c, d);
                    Lcalcultxtbx.Text = FilledInfos.L.ToString();
                    lcalcultxtbx.Text = FilledInfos.l.ToString();
                }
                
            }
            if (Dimensioncbx.SelectedIndex == 3) 
            {
                if (a == 0 || b == 0 || c == 0)
                    MessageBox.Show("Veuillez renseigner les longueurs a, b et c." + " (Après avoir entré une valeur, appuyez sur entrée)");
                else 
                {
                    FilledInfos.CalculateDimensions(3, a, b, c, d);
                    Ltxtbx.Text = FilledInfos.L.ToString();
                    ltxtbx.Text = FilledInfos.l.ToString();
                }
                
            }
            if (Dimensioncbx.SelectedIndex == 4) 
            {
                if (a == 0 || b == 0 || c == 0 || d == 0)
                    MessageBox.Show("Veuillez renseigner les longueurs a, b, c et d." + " (Après avoir entré une valeur, appuyez sur entrée)");
                else 
                {
                    FilledInfos.CalculateDimensions(4, a, b, c, d);
                    Ltxtbx.Text = FilledInfos.L.ToString();
                    ltxtbx.Text = FilledInfos.l.ToString();
                }
            }
            if (Dimensioncbx.SelectedIndex == 5) 
            {
                if (a == 0)
                    MessageBox.Show("Veuillez renseigner la longueur a" + " (Après avoir entré une valeur, appuyez sur entrée)");
                else 
                {
                    FilledInfos.CalculateDimensions(5, a, b, c, d);
                    Ltxtbx.Text = FilledInfos.L.ToString();
                    ltxtbx.Text = FilledInfos.l.ToString();
                }
                
            }
            if (Dimensioncbx.SelectedIndex == 6)
            {
                if (b == 0 || c == 0)
                    MessageBox.Show("Veuillez renseigner les longueurs b et c." + " (Après avoir entré une valeur, appuyez sur entrée)");
                else 
                {
                    FilledInfos.CalculateDimensions(6, a, b, c, d);
                    Ltxtbx.Text = FilledInfos.L.ToString();
                    ltxtbx.Text = FilledInfos.l.ToString();
                }
                
            }

        }
    }
}
