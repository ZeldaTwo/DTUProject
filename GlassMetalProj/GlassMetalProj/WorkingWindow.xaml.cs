using ControlzEx.Theming;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
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
        private MathsHelper mathsHelper;

        private double a = 0;
        private double b = 0;
        private double c = 0;
        private double d = 0;

        int IndexInfeuilette = 1;
        public WorkingWindow()
        {
            InitializeComponent();
            FilledInfos = new FilledInfos("","");
            mathsHelper = new MathsHelper(FilledInfos);

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
            e1txtBox.IsEnabled = false;
            RapportlsurLtxtbx.IsEnabled = false;
            Surfacetxtbx.IsEnabled = false;
            eRtxtbox.IsEnabled = false;
            eFtxtbox.IsEnabled = false;

            //Disappearing Label and CheckBox for "Grandcôté or petit côté"
            BordLibrelbl.Visibility = Visibility.Hidden;
            petitcotelcheckbx.Visibility = Visibility.Hidden;
            grandcotecheckbx.Visibility = Visibility.Hidden;

            //Disappearing Label and CheckBox for "Maitiens au milieu du grand côté or Doubles maintiens équidistants
            PositionDuMaitienlbl.Visibility = Visibility.Hidden;
            MaintienMiddlecheckbx.Visibility = Visibility.Hidden;
            DoubleMaintienscheckbx.Visibility = Visibility.Hidden;

            //Disappapearing Labels and CheckBox for glazingtype
            Monolithiqueslbl.Visibility = Visibility.Hidden;
            GlazingTypemonolithiquescombobx.Visibility = Visibility.Hidden;
            epaisseurmonolithiquetxtbx.Visibility = Visibility.Hidden;

            //Disappearing ListBox for feuilleté
            feuilletelbx.Visibility = Visibility.Hidden;
            GlazingTypeFeuilleté1.Visibility = Visibility.Hidden;
            GlazingTypeFeuilleté2.Visibility = Visibility.Hidden;
            GlazingTypeFeuilleté3.Visibility = Visibility.Hidden;
            GlazingTypeFeuilleté4.Visibility = Visibility.Hidden;

            //Disappearing textbox for feuilleté and thickness
            epaisseurfeuilleté1txtbx.Visibility = Visibility.Hidden;
            epaisseurfeuilleté2txtbx.Visibility = Visibility.Hidden;
            epaisseurfeuilleté3txtbx.Visibility = Visibility.Hidden;
            epaisseurfeuilleté4txtbx.Visibility = Visibility.Hidden;

            //Disappearing combobox for isolant double faces and triple faces
            isolant2facescombobx.Visibility = Visibility.Hidden;
            isolant3facescombobx.Visibility = Visibility.Hidden;

            //Disabling the button to calculate ER AND EF
            thicknessCalculatebtn.IsEnabled = false; 

            //Disappearing buttons for isolant double and triple faces. 
            NextButton.Visibility = Visibility.Hidden;
            BackButton.Visibility = Visibility.Hidden;


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
                Heightcbx.SelectedIndex = -1;
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
                                    if (value > FilledInfos.L && FilledInfos.L != 0)
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
                                    if (value < FilledInfos.l && FilledInfos.l != 0)
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
                                    if (Dimensioncbx.SelectedIndex == 3)
                                    {
                                        if (c != 0 && value < c)
                                            MessageBox.Show("a ne peut pas être inférieur à c");
                                        else
                                        {
                                            a = value;
                                            textBox.IsEnabled = false;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        a = value;
                                        textBox.IsEnabled = false;
                                    }
                                    break;
                                case "btxtbx":
                                    b = value;
                                    textBox.IsEnabled = false;
                                    break;
                                case "ctxtbx":
                                    if (Dimensioncbx.SelectedIndex == 3)
                                    {
                                        if (a != 0 && value > a)
                                            MessageBox.Show("c ne peut pas être supérieur à a");
                                        else
                                        {
                                            c = value;
                                            textBox.IsEnabled = false;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        c = value;
                                        textBox.IsEnabled = false;
                                    }
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
                    else
                        MessageBox.Show("Veuillez entrer une valeur valide (entre 0 exclu et plus)");
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

                    RapportlsurLtxtbx.Text = Math.Round(FilledInfos.l / FilledInfos.L, 2).ToString();
                    Surfacetxtbx.Text = Math.Round((FilledInfos.L * FilledInfos.l), 3) + " m²";
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
                    RapportlsurLtxtbx.Text = Math.Round(FilledInfos.l / FilledInfos.L, 2).ToString();
                    Surfacetxtbx.Text = Math.Round((FilledInfos.L * FilledInfos.l), 3) + " m²";

                }

            }
            if (Dimensioncbx.SelectedIndex == 3) 
            {
                if (a == 0 || b == 0 || c == 0)
                    MessageBox.Show("Veuillez renseigner les longueurs a, b et c." + " (Après avoir entré une valeur, appuyez sur entrée)");
                else 
                {
                    FilledInfos.CalculateDimensions(3, a, b, c, d);
                    Lcalcultxtbx.Text = FilledInfos.L.ToString();
                    lcalcultxtbx.Text = FilledInfos.l.ToString();
                    RapportlsurLtxtbx.Text = Math.Round(FilledInfos.l / FilledInfos.L, 2).ToString();
                    Surfacetxtbx.Text = Math.Round((FilledInfos.L * FilledInfos.l), 3) + " m²";

                }

            }
            if (Dimensioncbx.SelectedIndex == 4) 
            {
                if (a == 0 || b == 0 || c == 0 || d == 0)
                    MessageBox.Show("Veuillez renseigner les longueurs a, b, c et d." + " (Après avoir entré une valeur, appuyez sur entrée)");
                else 
                {
                    FilledInfos.CalculateDimensions(4, a, b, c, d);
                    Lcalcultxtbx.Text = FilledInfos.L.ToString();
                    lcalcultxtbx.Text = FilledInfos.l.ToString();
                    RapportlsurLtxtbx.Text = Math.Round(FilledInfos.l / FilledInfos.L, 2).ToString();
                    Surfacetxtbx.Text = Math.Round((FilledInfos.L * FilledInfos.l), 3) + " m²";
                }
            }
            if (Dimensioncbx.SelectedIndex == 5) 
            {
                if (a == 0)
                    MessageBox.Show("Veuillez renseigner la longueur a" + " (Après avoir entré une valeur, appuyez sur entrée)");
                else 
                {
                    FilledInfos.CalculateDimensions(5, a, b, c, d);
                    Lcalcultxtbx.Text = FilledInfos.L.ToString();
                    lcalcultxtbx.Text = FilledInfos.l.ToString();
                    RapportlsurLtxtbx.Text = Math.Round(FilledInfos.l / FilledInfos.L, 2).ToString();
                    Surfacetxtbx.Text = Math.Round((FilledInfos.L * FilledInfos.l), 3) + " m²";
                }

            }
            if (Dimensioncbx.SelectedIndex == 6)
            {
                if (b == 0 || c == 0)
                    MessageBox.Show("Veuillez renseigner les longueurs b et c." + " (Après avoir entré une valeur, appuyez sur entrée)");
                else 
                {
                    FilledInfos.CalculateDimensions(6, a, b, c, d);
                    Lcalcultxtbx.Text = FilledInfos.L.ToString();
                    lcalcultxtbx.Text = FilledInfos.l.ToString();
                    RapportlsurLtxtbx.Text = Math.Round(FilledInfos.l / FilledInfos.L,2).ToString();

                    Surfacetxtbx.Text = Math.Round((FilledInfos.L * FilledInfos.l), 3) + " m²";
                }

            }

        }

        private void Avalanche_checkbx_Checked(object sender, RoutedEventArgs e)
        {
            FilledInfos.Avalanche = true;
        }

        private void Avalanche_checkbx_Unchecked(object sender, RoutedEventArgs e)
        {
            FilledInfos.Avalanche = false;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            ComboBoxItem selected = GlazingSupport.SelectedItem as ComboBoxItem;
            CheckBox checkBox = sender as CheckBox;
            if (checkBox.Name == "petitcotelcheckbx") 
            {
                if (grandcotecheckbx.IsChecked == true)
                    grandcotecheckbx.IsChecked = false;

                mathsHelper.e1Calculatation(selected.Content.ToString(), -1, -1);

                if (GlazingSupport.SelectedIndex != 3)
                    e1txtBox.Text = mathsHelper.e1.ToString();

                //Update if changing the grand cote or petit cote CheckBox
                else if (GlazingSupport.SelectedIndex == 3 && (MaintienMiddlecheckbx.IsChecked == true)) 
                {
                    mathsHelper.e1Calculatation(selected.Content.ToString(), 0, -1);
                    e1txtBox.Text = mathsHelper.e1.ToString();
                }
                else if (GlazingSupport.SelectedIndex == 3 && DoubleMaintienscheckbx.IsChecked == true) 
                {
                    mathsHelper.e1Calculatation(selected.Content.ToString(), -1, -1);
                    e1txtBox.Text = mathsHelper.e1.ToString();
                }
            }
            if (checkBox.Name == "grandcotecheckbx") 
            {
                if (petitcotelcheckbx.IsChecked== true)
                    petitcotelcheckbx.IsChecked= false;

                mathsHelper.e1Calculatation(selected.Content.ToString(), 0, -1);

                if (GlazingSupport.SelectedIndex != 3)
                    e1txtBox.Text = mathsHelper.e1.ToString();

                //Update if changing the grand cote or petit cote checkbox
                else if (GlazingSupport.SelectedIndex == 3 && (MaintienMiddlecheckbx.IsChecked == true))
                {
                    mathsHelper.e1Calculatation(selected.Content.ToString(), 0, 0);
                    e1txtBox.Text = mathsHelper.e1.ToString();
                }
                else if (GlazingSupport.SelectedIndex == 3 && DoubleMaintienscheckbx.IsChecked == true)
                {
                    mathsHelper.e1Calculatation(selected.Content.ToString(), -1, 0);
                    e1txtBox.Text = mathsHelper.e1.ToString();
                }
            }
            if (checkBox.Name == "MaintienMiddlecheckbx") 
            {
                //Visual and Not doable issues
                if (DoubleMaintienscheckbx.IsChecked == true)
                    DoubleMaintienscheckbx.IsChecked = false;
                if (petitcotelcheckbx.IsChecked == false && grandcotecheckbx.IsChecked == false)
                {
                    MessageBox.Show("Veuillez d'abord cocher une des cases ci-dessus");
                    checkBox.IsChecked = false;
                }
                else 
                {
                    if (petitcotelcheckbx.IsChecked == true)
                        mathsHelper.e1Calculatation(selected.Content.ToString(), 0, -1);
                    else if (grandcotecheckbx.IsChecked == true)
                        mathsHelper.e1Calculatation(selected.Content.ToString(), 0, 0);
                    e1txtBox.Text = mathsHelper.e1.ToString();
                }
            }
            if (checkBox.Name == "DoubleMaintienscheckbx") 
            {
                //Visual and Not doable issues
                if (MaintienMiddlecheckbx.IsChecked == true)
                    MaintienMiddlecheckbx.IsChecked = false;
                if (petitcotelcheckbx.IsChecked == false && grandcotecheckbx.IsChecked == false)
                {
                    MessageBox.Show("Veuillez d'abord cocher une des cases ci-dessus");
                    checkBox.IsChecked = false;
                }
                else 
                {
                    if (petitcotelcheckbx.IsChecked == true)
                        mathsHelper.e1Calculatation(selected.Content.ToString(), -1, -1);
                    else if (grandcotecheckbx.IsChecked == true)
                        mathsHelper.e1Calculatation(selected.Content.ToString(), -1, 0);
                    e1txtBox.Text = mathsHelper.e1.ToString();
                }


            }
        }

        private void GlazingSupport_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem selectedstring = GlazingSupport.SelectedItem as ComboBoxItem;

            if (GlazingSupport.SelectedIndex != 0) 
            {
                //Visual things
                BordLibrelbl.Visibility = Visibility.Visible;
                petitcotelcheckbx.Visibility = Visibility.Visible;
                grandcotecheckbx.Visibility= Visibility.Visible;
                petitcotelcheckbx.IsChecked = false;
                grandcotecheckbx.IsChecked = false ;
                e1txtBox.Text = string.Empty;

                MaintienMiddlecheckbx.Visibility = Visibility.Hidden;
                DoubleMaintienscheckbx.Visibility = Visibility.Hidden;
                PositionDuMaitienlbl.Visibility = Visibility.Hidden;
                DoubleMaintienscheckbx.IsChecked = false;
                MaintienMiddlecheckbx.IsChecked = false;

                if (GlazingSupport.SelectedIndex == 3) 
                {
                    MaintienMiddlecheckbx.Visibility = Visibility.Visible;
                    DoubleMaintienscheckbx.Visibility = Visibility.Visible;
                    PositionDuMaitienlbl.Visibility = Visibility.Visible;
                }
            }
            else 
            {
                //Visual things
                BordLibrelbl.Visibility = Visibility.Hidden;
                petitcotelcheckbx.Visibility = Visibility.Hidden;
                grandcotecheckbx.Visibility = Visibility.Hidden;
                petitcotelcheckbx.IsChecked = false;
                grandcotecheckbx.IsChecked = false;

                MaintienMiddlecheckbx.Visibility = Visibility.Hidden;
                DoubleMaintienscheckbx.Visibility = Visibility.Hidden;
                PositionDuMaitienlbl.Visibility = Visibility.Hidden;
                DoubleMaintienscheckbx.IsChecked = false;
                MaintienMiddlecheckbx.IsChecked = false;

                mathsHelper.e1Calculatation(selectedstring.Content.ToString(), -1, -1);
                e1txtBox.Text = mathsHelper.e1.ToString();
            }
        }

        private void Preview_Text_Input(object sender, TextCompositionEventArgs e)
        {
            if (e.Text == ".")
            {
                // Annule l'entrée de texte par défaut
                e.Handled = true;

                // Insère une virgule à la position du curseur
                TextBox textBox = sender as TextBox;
                int caretIndex = textBox.CaretIndex;
                textBox.Text = textBox.Text.Insert(caretIndex, ",");
                textBox.CaretIndex = caretIndex + 1; // Déplace le curseur après la virgule
            }
        }

        private void GlazingTypecombobx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Hide everyting at the beginning
            Monolithiqueslbl.Visibility = Visibility.Hidden;
            GlazingTypemonolithiquescombobx.Visibility = Visibility.Hidden;
            GlazingTypemonolithiquescombobx.SelectedIndex = -1;
            epaisseurmonolithiquetxtbx.Visibility = Visibility.Hidden;
            epaisseurmonolithiquetxtbx.Text = string.Empty;

            feuilletelbx.Visibility = Visibility.Hidden;
            GlazingTypeFeuilleté1.Visibility = Visibility.Hidden;
            GlazingTypeFeuilleté2.Visibility = Visibility.Hidden;
            GlazingTypeFeuilleté3.Visibility = Visibility.Hidden;
            GlazingTypeFeuilleté4.Visibility = Visibility.Hidden;

            epaisseurfeuilleté1txtbx.Visibility = Visibility.Hidden;
            epaisseurfeuilleté2txtbx.Visibility = Visibility.Hidden;
            epaisseurfeuilleté3txtbx.Visibility = Visibility.Hidden;
            epaisseurfeuilleté4txtbx.Visibility = Visibility.Hidden;

            isolant2facescombobx.Visibility = Visibility.Hidden;
            isolant2facescombobx.SelectedIndex = -1;

            isolant3facescombobx.Visibility = Visibility.Hidden;
            isolant3facescombobx.SelectedIndex = -1;

            NextButton.Visibility = Visibility.Hidden;
            BackButton.Visibility = Visibility.Hidden;

            thicknessCalculatebtn.IsEnabled = false;

            IndexInfeuilette = 1;
            NextButton.IsEnabled = true;

            //Reset everying in FilledInfos
            FilledInfos.FeuilleteIndex = -1;
            FilledInfos.FeuilleteIndex2 = -1;
            FilledInfos.GlazingTypeFromLayersIndexes = new int[4];
            FilledInfos.GlazingTypeFromLayersIndexes2 = new int[4];
            FilledInfos.GlazingTypeMonolithiqueIndex = -1;
            FilledInfos.GlazingTypeMonolithiqueIndex2 = -1;
            FilledInfos.thicknessformonolithiques = 0;
            FilledInfos.thicknessformonolithiques2 = 0;
            FilledInfos.thicknessfromeachLayerfeuillete = new double[4];
            FilledInfos.thicknessfromeachLayerfeuillete2 = new double[4];


            if (GlazingTypecombobx.SelectedIndex == 0) 
            {
                Monolithiqueslbl.Visibility = Visibility.Visible;
                GlazingTypemonolithiquescombobx.Visibility = Visibility.Visible;
                epaisseurmonolithiquetxtbx.Visibility = Visibility.Visible;
                thicknessCalculatebtn.IsEnabled = true;
                
            }
            if (GlazingTypecombobx.SelectedIndex == 1) 
            {
                feuilletelbx.Visibility = Visibility.Visible;
                thicknessCalculatebtn.IsEnabled = true;
            }
            if (GlazingTypecombobx.SelectedIndex == 2) 
            {
                isolant2facescombobx.Visibility = Visibility.Visible;
            }
            if (GlazingTypecombobx.SelectedIndex == 3)
                isolant3facescombobx.Visibility = Visibility.Visible;

        }

        private void feuilletelbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Visual Aspects
            GlazingTypeFeuilleté1.SelectedIndex = -1;
            GlazingTypeFeuilleté2.SelectedIndex = -1;
            GlazingTypeFeuilleté3.SelectedIndex = -1;
            GlazingTypeFeuilleté4.SelectedIndex = -1;
            epaisseurfeuilleté1txtbx.Text = string.Empty;
            epaisseurfeuilleté2txtbx.Text = string.Empty;
            epaisseurfeuilleté3txtbx.Text = string.Empty;
            epaisseurfeuilleté4txtbx.Text = string.Empty;
            epaisseurfeuilleté1txtbx.Visibility = Visibility.Visible;
            epaisseurfeuilleté2txtbx.Visibility = Visibility.Visible;
            epaisseurfeuilleté3txtbx.Visibility = Visibility.Hidden;
            epaisseurfeuilleté4txtbx.Visibility = Visibility.Hidden;
            GlazingTypeFeuilleté1.Visibility = Visibility.Visible;
            GlazingTypeFeuilleté2.Visibility = Visibility.Visible;
            GlazingTypeFeuilleté3.Visibility = Visibility.Hidden;
            GlazingTypeFeuilleté4.Visibility = Visibility.Hidden;

            int i = feuilletelbx.SelectedIndex;
            switch (i) 
            {
                case 1: case 4:
                    GlazingTypeFeuilleté3.Visibility = Visibility.Visible;

                    epaisseurfeuilleté3txtbx.Visibility = Visibility.Visible;
                    break;
                case 2:
                    GlazingTypeFeuilleté3.Visibility = Visibility.Visible;
                    GlazingTypeFeuilleté4.Visibility = Visibility.Visible;

                    epaisseurfeuilleté3txtbx.Visibility = Visibility.Visible;
                    epaisseurfeuilleté4txtbx.Visibility = Visibility.Visible;
                    break;
            }
        }

        private void isolant2facescombobx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BackButton.Visibility != Visibility.Visible && NextButton.Visibility != Visibility.Visible) 
            {
                BackButton.Visibility = Visibility.Visible;
                NextButton.Visibility = Visibility.Visible;
            }
            isolant2facescombobx.Visibility = Visibility.Hidden;

            //For the first item being displayed
            switch (isolant2facescombobx.SelectedIndex) 
            {
                case 0: case 1:
                    Monolithiqueslbl.Visibility = Visibility.Visible;
                    GlazingTypemonolithiquescombobx.Visibility = Visibility.Visible;
                    epaisseurmonolithiquetxtbx.Visibility = Visibility.Visible;
                    break;

                case 2: 
                    feuilletelbx.Visibility = Visibility.Visible;
                    break;
            }
           
        }

        private void thicknessCalculatebtn_Click(object sender, RoutedEventArgs e)
        {
            // It means the last used is the monolithiques
            if (epaisseurmonolithiquetxtbx.Visibility == Visibility.Visible) 
            {
                double thickness;
                if (!Double.TryParse(epaisseurmonolithiquetxtbx.Text, out thickness)) 
                {
                    MessageBox.Show("Veuillez rentrer une valeur correcte");
                }
                else if (thickness == 0)
                    MessageBox.Show("L'épaisseur ne peut pas être nulle");
                else if (GlazingTypemonolithiquescombobx.SelectedIndex == -1)
                    MessageBox.Show("Veuillez saisir un type de glace");
                else 
                {
                    switch (GlazingTypecombobx.SelectedIndex) 
                    {
                        //Case it's only 1 Monolithique
                        case 0:
                            mathsHelper.eRCalculation(0, thickness, null, GlazingTypemonolithiquescombobx.SelectedIndex, null, null);
                            mathsHelper.eFCalculation(0, thickness, null, null);
                            break;
                        //Case where we have 2 Monolithiques
                        case 2:
                            mathsHelper.eRCalculation(2, thickness, null,GlazingTypemonolithiquescombobx.SelectedIndex,null, null);
                            mathsHelper.eFCalculation(2, thickness, null, null);
                            break;
                        //Case where we have 3 Monolithiques
                        case 3:
                            mathsHelper.eRCalculation(3, thickness, null, GlazingTypemonolithiquescombobx.SelectedIndex, null, null);
                            mathsHelper.eFCalculation(3, thickness, null, null);
                            break;
                    }
                    if (!mathsHelper.eRisValid()) 
                    {
                        MessageBox.Show("L'épaisseur de résistance ne vérifie pas la condition : eR ≥ e1 * c \n"
                            + "Veuillez modifier vos épaisseurs ou bien vos types de vitrages pour avoir un résultat correct");
                    }
                    else 
                    {
                        eRtxtbox.Text = mathsHelper.eR.ToString();
                        eFtxtbox.Text = mathsHelper.eF.ToString();
                    }
                }
            }
            //It means the last used is the feuillete
            else if (epaisseurfeuilleté1txtbx.Visibility == Visibility.Visible) 
            {
                double[] thicknesses = new double[4];
                //Issues prevention
                if (GlazingTypeFeuilleté1.SelectedIndex == -1 || GlazingTypeFeuilleté2.SelectedIndex == -1 || (GlazingTypeFeuilleté3.SelectedIndex == -1 && GlazingTypeFeuilleté3.Visibility == Visibility.Visible) || (GlazingTypeFeuilleté4.SelectedIndex == -1 && GlazingTypeFeuilleté4.Visibility == Visibility.Visible))
                    MessageBox.Show("Veuillez choisir le type du vitrage monolithique pour chaque couche");
                //Issues prevention for thickness filled infos
                else if (!Double.TryParse(epaisseurfeuilleté1txtbx.Text, out thicknesses[0]) || !Double.TryParse(epaisseurfeuilleté2txtbx.Text, out thicknesses[1]))
                {
                    MessageBox.Show("Veuillez vérifier les informations saisies pour les épaisseurs");
                }
                else if (thicknesses[0] == 0 || thicknesses[1] == 0)
                    MessageBox.Show("Les épaisseurs ne peuvent pas être nulles");

                else if ((epaisseurfeuilleté3txtbx.Visibility == Visibility.Visible && !Double.TryParse(epaisseurfeuilleté3txtbx.Text, out thicknesses[2])))
                {
                    MessageBox.Show("Veuillez vérifier les informations saisies pour les épaisseurs");
                }
                else if ((epaisseurfeuilleté4txtbx.Visibility == Visibility.Visible && !Double.TryParse(epaisseurfeuilleté4txtbx.Text, out thicknesses[3])))
                {
                    MessageBox.Show("Veuillez vérifier les informations saisies pour les épaisseurs");
                }
                else if ((epaisseurfeuilleté3txtbx.Visibility == Visibility.Visible && thicknesses[2] == 0) || (epaisseurfeuilleté4txtbx.Visibility == Visibility.Visible && thicknesses[3] == 0))
                    MessageBox.Show("les épaisseurs ne peuvent pas être nulles");
                else 
                {
                    int[] indexesForGlazingMono = new int[4];
                    int[] indexesEpsilon2 = new int[3]; 
                    indexesForGlazingMono[0] = GlazingTypeFeuilleté1.SelectedIndex;
                    indexesForGlazingMono[1] = GlazingTypeFeuilleté2.SelectedIndex;
                    indexesForGlazingMono[2] = GlazingTypeFeuilleté3.SelectedIndex;
                    indexesForGlazingMono[3] = GlazingTypeFeuilleté4.SelectedIndex;
                    switch (GlazingTypecombobx.SelectedIndex) 
                    {
                        //It's 1 Feuilleté
                        case 1:
                            indexesEpsilon2[0] = feuilletelbx.SelectedIndex;
                            mathsHelper.eRCalculation(1, 0, thicknesses, -1, indexesForGlazingMono,indexesEpsilon2);
                            mathsHelper.eFCalculation(1, 0, thicknesses, indexesEpsilon2);
                            break;
                        //It's 1 monolithique + 1 feuilleté or it's double feuilleté
                        case 2:
                            if (isolant2facescombobx.SelectedIndex == 1)
                                indexesEpsilon2[0] = feuilletelbx.SelectedIndex;
                            else 
                            {
                                indexesEpsilon2[0] = FilledInfos.FeuilleteIndex;
                                indexesEpsilon2[1] = feuilletelbx.SelectedIndex;
                            }
                                mathsHelper.eRCalculation(2, 0, thicknesses, -1, indexesForGlazingMono, indexesEpsilon2);
                                mathsHelper.eFCalculation(2, 0, thicknesses, indexesEpsilon2);
                            break;
                        //it's 2 monolithiques + 1 feuilleté, or 1 monolithique
                        case 3:
                            if (isolant3facescombobx.SelectedIndex == 1)
                                indexesEpsilon2[0] = feuilletelbx.SelectedIndex;
                            else if (isolant3facescombobx.SelectedIndex == 2) 
                            {
                                indexesEpsilon2[0] = FilledInfos.FeuilleteIndex2;
                                indexesEpsilon2[1] = feuilletelbx.SelectedIndex;
                            }
                            else if (isolant3facescombobx.SelectedIndex == 3) 
                            {
                                indexesEpsilon2[0] = FilledInfos.FeuilleteIndex;
                                indexesEpsilon2[1] = FilledInfos.FeuilleteIndex2;
                                indexesEpsilon2[2] = feuilletelbx.SelectedIndex;
                            }

                            mathsHelper.eRCalculation(3, 0, thicknesses, -1, indexesForGlazingMono, indexesEpsilon2);
                            mathsHelper.eFCalculation(3, 0, thicknesses, indexesEpsilon2);
                            break;
                    }
                    if (!mathsHelper.eRisValid())
                    {
                        MessageBox.Show("L'épaisseur de résistance ne vérifie pas la condition : eR ≥ e1 * c \n"
                            + "Veuillez modifier vos épaisseurs ou bien vos types de vitrages pour avoir un résultat correct");
                    }
                    else
                    {
                        eRtxtbox.Text = mathsHelper.eR.ToString();
                        eFtxtbox.Text = mathsHelper.eF.ToString();
                    }
                }

            }
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            IndexInfeuilette++;

            //Managing with saving the infos that we wrote (Need to do before the step after)
            if (IndexInfeuilette == 2)
            {
                //Isolant 2 faces
                //We display things depending on the type of glazing we have on the layer
                if (isolant2facescombobx.SelectedIndex == 0)
                {
                    //Issues Prevention
                    if (!Double.TryParse(epaisseurmonolithiquetxtbx.Text, out FilledInfos.thicknessformonolithiques))
                    {
                        MessageBox.Show("Veuillez entrer une valeur correcte");
                        IndexInfeuilette--;
                    }
                    else if (FilledInfos.thicknessformonolithiques == 0) 
                    {
                        MessageBox.Show("L'épaisseur ne peut pas être nulle");
                        IndexInfeuilette--;
                    }
                    else if (GlazingTypemonolithiquescombobx.SelectedIndex == -1)
                    {
                        MessageBox.Show("Veuillez choisir le type du vitrage monolithique");
                        IndexInfeuilette--;
                    }
                    //Else we go to the next step 
                    else
                    {
                        FilledInfos.GlazingTypeMonolithiqueIndex = GlazingTypemonolithiquescombobx.SelectedIndex;
                        GlazingTypemonolithiquescombobx.SelectedIndex = -1;
                        epaisseurmonolithiquetxtbx.Text = string.Empty;
                    }
                }

                else if (isolant2facescombobx.SelectedIndex == 1)
                {
                    //Issues Prevention
                    if (!Double.TryParse(epaisseurmonolithiquetxtbx.Text, out FilledInfos.thicknessformonolithiques))
                    {
                        MessageBox.Show("Veuillez entrer une valeur correcte");
                        IndexInfeuilette--;
                    }
                    else if (FilledInfos.thicknessformonolithiques == 0) 
                    {
                        MessageBox.Show("L'épaisseur ne peut pas être nulle");
                        IndexInfeuilette--;
                    }
                    else if (GlazingTypemonolithiquescombobx.SelectedIndex == -1)
                    {
                        MessageBox.Show("Veuillez choisir le type du vitrage monolithique");
                        IndexInfeuilette--;
                    }
                    else
                    {
                        epaisseurmonolithiquetxtbx.Text = string.Empty;
                        epaisseurmonolithiquetxtbx.Visibility = Visibility.Hidden;
                        Monolithiqueslbl.Visibility = Visibility.Hidden;
                        FilledInfos.GlazingTypeMonolithiqueIndex = GlazingTypemonolithiquescombobx.SelectedIndex;
                        GlazingTypemonolithiquescombobx.Visibility = Visibility.Hidden;
                        feuilletelbx.Visibility = Visibility.Visible;
                    }

                }
                else if (isolant2facescombobx.SelectedIndex == 2)
                {
                    //Issues prevention
                    if (GlazingTypeFeuilleté1.SelectedIndex == -1 || GlazingTypeFeuilleté2.SelectedIndex == -1 || (GlazingTypeFeuilleté3.SelectedIndex == -1 && GlazingTypeFeuilleté3.Visibility == Visibility.Visible) || (GlazingTypeFeuilleté4.SelectedIndex == -1 && GlazingTypeFeuilleté4.Visibility == Visibility.Visible))
                    {
                        MessageBox.Show("Veuillez choisir le type du vitrage monolithique pour chaque couche");
                        IndexInfeuilette--;
                    }
                    //Issues prevention for thickness filled infos
                    else if (!Double.TryParse(epaisseurfeuilleté1txtbx.Text, out FilledInfos.thicknessfromeachLayerfeuillete[0]) || !Double.TryParse(epaisseurfeuilleté2txtbx.Text, out FilledInfos.thicknessfromeachLayerfeuillete[1]))
                    {
                        MessageBox.Show("Veuillez vérifier les informations saisies pour les épaisseurs");
                        IndexInfeuilette--;
                    }
                    else if (FilledInfos.thicknessfromeachLayerfeuillete[0] == 0 || FilledInfos.thicknessfromeachLayerfeuillete[1] == 0) 
                    {
                        MessageBox.Show("Les épaisseurs ne peuvent pas être nulles");
                        IndexInfeuilette--;
                    }
                    else if ((epaisseurfeuilleté3txtbx.Visibility == Visibility.Visible && !Double.TryParse(epaisseurfeuilleté3txtbx.Text, out FilledInfos.thicknessfromeachLayerfeuillete[2])))
                    {
                        MessageBox.Show("Veuillez vérifier les informations saisies pour les épaisseurs");
                        IndexInfeuilette--;
                    }
                    else if ((epaisseurfeuilleté4txtbx.Visibility == Visibility.Visible && !Double.TryParse(epaisseurfeuilleté4txtbx.Text, out FilledInfos.thicknessfromeachLayerfeuillete[3])))
                    {
                        MessageBox.Show("Veuillez vérifier les informations saisies pour les épaisseurs");
                        IndexInfeuilette--;
                    }
                    else if ((epaisseurfeuilleté3txtbx.Visibility == Visibility.Visible && FilledInfos.thicknessfromeachLayerfeuillete[2] == 0) || (epaisseurfeuilleté4txtbx.Visibility == Visibility.Visible && FilledInfos.thicknessfromeachLayerfeuillete[3] == 0)) 
                    {
                        MessageBox.Show("Les épaisseurs ne peuvent pas être nulles");
                        IndexInfeuilette--;
                    }
                    else
                    {
                        FilledInfos.FeuilleteIndex = feuilletelbx.SelectedIndex;
                        FilledInfos.GlazingTypeFromLayersIndexes[0] = GlazingTypeFeuilleté1.SelectedIndex;
                        FilledInfos.GlazingTypeFromLayersIndexes[1] = GlazingTypeFeuilleté2.SelectedIndex;
                        FilledInfos.GlazingTypeFromLayersIndexes[2] = GlazingTypeFeuilleté3.SelectedIndex;
                        FilledInfos.GlazingTypeFromLayersIndexes[3] = GlazingTypeFeuilleté4.SelectedIndex;

                        feuilletelbx.SelectedIndex = -1;
                        epaisseurfeuilleté1txtbx.Visibility = Visibility.Hidden;
                        epaisseurfeuilleté2txtbx.Visibility = Visibility.Hidden;
                        GlazingTypeFeuilleté1.Visibility = Visibility.Hidden;
                        GlazingTypeFeuilleté2.Visibility = Visibility.Hidden;
                    }

                }

                //Isolant 3 faces
                else if (isolant3facescombobx.SelectedIndex == 0 || isolant3facescombobx.SelectedIndex == 1)
                {
                    //Issues Prevention
                    if (!Double.TryParse(epaisseurmonolithiquetxtbx.Text, out FilledInfos.thicknessformonolithiques))
                    {
                        MessageBox.Show("Veuillez entrer une valeur correcte");
                        IndexInfeuilette--;
                    }
                    else if (FilledInfos.thicknessformonolithiques == 0) 
                    {
                        MessageBox.Show("L'épaisseur ne peut pas être nulle");
                        IndexInfeuilette--;
                    }
                    else if (GlazingTypemonolithiquescombobx.SelectedIndex == -1)
                    {
                        MessageBox.Show("Veuillez choisir le type du vitrage monolithique");
                        IndexInfeuilette--;
                    }
                    //Else we go to the next step 
                    else
                    {
                        FilledInfos.GlazingTypeMonolithiqueIndex = GlazingTypemonolithiquescombobx.SelectedIndex;
                        if (FilledInfos.GlazingTypeMonolithiqueIndex2 != -1)
                        {
                            GlazingTypemonolithiquescombobx.SelectedIndex = FilledInfos.GlazingTypeMonolithiqueIndex2;
                            epaisseurmonolithiquetxtbx.Text = FilledInfos.thicknessformonolithiques2.ToString();
                        }
                        else
                        {
                            GlazingTypemonolithiquescombobx.SelectedIndex = -1;
                            epaisseurmonolithiquetxtbx.Text = string.Empty;
                        }
                    }
                }
                else if (isolant3facescombobx.SelectedIndex == 2)
                {
                    //Issues Prevention
                    if (!Double.TryParse(epaisseurmonolithiquetxtbx.Text, out FilledInfos.thicknessformonolithiques))
                    {
                        MessageBox.Show("Veuillez entrer une valeur correcte");
                        IndexInfeuilette--;
                    }
                    else if (GlazingTypemonolithiquescombobx.SelectedIndex == -1)
                    {
                        MessageBox.Show("Veuillez choisir le type du vitrage monolithique");
                        IndexInfeuilette--;
                    }
                    else if (FilledInfos.thicknessformonolithiques == 0) 
                    {
                        MessageBox.Show("L'épaisseur ne peut pas être nulle");
                        IndexInfeuilette--;
                    }
                    else
                    {
                        epaisseurmonolithiquetxtbx.Text = string.Empty;
                        epaisseurmonolithiquetxtbx.Visibility = Visibility.Hidden;
                        Monolithiqueslbl.Visibility = Visibility.Hidden;
                        FilledInfos.GlazingTypeMonolithiqueIndex = GlazingTypemonolithiquescombobx.SelectedIndex;
                        GlazingTypemonolithiquescombobx.Visibility = Visibility.Hidden;
                        feuilletelbx.Visibility = Visibility.Visible;

                        //It means that we had already done it before, so we redisplay the infos 
                        if (FilledInfos.FeuilleteIndex2 != -1) 
                        {
                            feuilletelbx.SelectedIndex = FilledInfos.FeuilleteIndex2;
                            GlazingTypeFeuilleté1.SelectedIndex = FilledInfos.GlazingTypeFromLayersIndexes2[0];
                            GlazingTypeFeuilleté2.SelectedIndex = FilledInfos.GlazingTypeFromLayersIndexes2[1];
                            epaisseurfeuilleté1txtbx.Text = FilledInfos.thicknessfromeachLayerfeuillete2[0].ToString();
                            epaisseurfeuilleté2txtbx.Text = FilledInfos.thicknessfromeachLayerfeuillete2[1].ToString();
                            if (GlazingTypeFeuilleté3.Visibility == Visibility.Visible) 
                            {
                                GlazingTypeFeuilleté3.SelectedIndex = FilledInfos.GlazingTypeFromLayersIndexes2[2];
                                epaisseurfeuilleté3txtbx.Text = FilledInfos.thicknessfromeachLayerfeuillete2[2].ToString();

                            }
                            if (GlazingTypeFeuilleté4.Visibility == Visibility.Visible) 
                            {
                                GlazingTypeFeuilleté4.SelectedIndex = FilledInfos.GlazingTypeFromLayersIndexes2[3];
                                epaisseurfeuilleté4txtbx.Text = FilledInfos.thicknessfromeachLayerfeuillete2[3].ToString();
                            }

                        }
                    }
                }
                else if (isolant3facescombobx.SelectedIndex == 3)
                {
                    //Issues prevention
                    if (GlazingTypeFeuilleté1.SelectedIndex == -1 || GlazingTypeFeuilleté2.SelectedIndex == -1 || (GlazingTypeFeuilleté3.SelectedIndex == -1 && GlazingTypeFeuilleté3.Visibility == Visibility.Visible) || (GlazingTypeFeuilleté4.SelectedIndex == -1 && GlazingTypeFeuilleté4.Visibility == Visibility.Visible))
                    {
                        MessageBox.Show("Veuillez choisir le type du vitrage monolithique pour chaque couche");
                        IndexInfeuilette--;
                    }
                    //Issues prevention for thickness filled infos
                    else if (!Double.TryParse(epaisseurfeuilleté1txtbx.Text, out FilledInfos.thicknessfromeachLayerfeuillete[0]) || !Double.TryParse(epaisseurfeuilleté2txtbx.Text, out FilledInfos.thicknessfromeachLayerfeuillete[1]))
                    {
                        MessageBox.Show("Veuillez vérifier les informations saisies pour les épaisseurs");
                        IndexInfeuilette--;
                    }
                    else if (FilledInfos.thicknessfromeachLayerfeuillete[0] == 0 || FilledInfos.thicknessfromeachLayerfeuillete[1] == 0) 
                    {
                        MessageBox.Show("Les épaisseurs ne peuvent pas être nulles");
                        IndexInfeuilette--;
                    }

                    else if ((epaisseurfeuilleté3txtbx.Visibility == Visibility.Visible && !Double.TryParse(epaisseurfeuilleté3txtbx.Text, out FilledInfos.thicknessfromeachLayerfeuillete[2])))
                    {
                        MessageBox.Show("Veuillez vérifier les informations saisies pour les épaisseurs");
                        IndexInfeuilette--;
                    }
                    else if ((epaisseurfeuilleté4txtbx.Visibility == Visibility.Visible && !Double.TryParse(epaisseurfeuilleté4txtbx.Text, out FilledInfos.thicknessfromeachLayerfeuillete[3])))
                    {
                        MessageBox.Show("Veuillez vérifier les informations saisies pour les épaisseurs");
                        IndexInfeuilette--;
                    }
                    else if ((epaisseurfeuilleté3txtbx.Visibility == Visibility.Visible && FilledInfos.thicknessfromeachLayerfeuillete[2] == 0) || (epaisseurfeuilleté4txtbx.Visibility == Visibility.Visible && FilledInfos.thicknessfromeachLayerfeuillete[3] == 0)) 
                    {
                        MessageBox.Show("Les épaisseurs ne peuvent pas être nulles");
                        IndexInfeuilette--;
                    }
                    else
                    {
                        FilledInfos.FeuilleteIndex = feuilletelbx.SelectedIndex;
                        FilledInfos.GlazingTypeFromLayersIndexes[0] = GlazingTypeFeuilleté1.SelectedIndex;
                        FilledInfos.GlazingTypeFromLayersIndexes[1] = GlazingTypeFeuilleté2.SelectedIndex;
                        FilledInfos.GlazingTypeFromLayersIndexes[2] = GlazingTypeFeuilleté3.SelectedIndex;
                        FilledInfos.GlazingTypeFromLayersIndexes[3] = GlazingTypeFeuilleté4.SelectedIndex;

                        if (FilledInfos.FeuilleteIndex2 != -1) 
                        {
                            feuilletelbx.SelectedIndex = FilledInfos.FeuilleteIndex2;
                            GlazingTypeFeuilleté1.SelectedIndex = FilledInfos.GlazingTypeFromLayersIndexes2[0];
                            GlazingTypeFeuilleté2.SelectedIndex = FilledInfos.GlazingTypeFromLayersIndexes2[1];
                            epaisseurfeuilleté1txtbx.Text = FilledInfos.thicknessfromeachLayerfeuillete2[0].ToString();
                            epaisseurfeuilleté2txtbx.Text = FilledInfos.thicknessfromeachLayerfeuillete2[1].ToString();
                            if (GlazingTypeFeuilleté3.Visibility == Visibility.Visible)
                            {
                                GlazingTypeFeuilleté3.SelectedIndex = FilledInfos.GlazingTypeFromLayersIndexes2[2];
                                epaisseurfeuilleté3txtbx.Text = FilledInfos.thicknessfromeachLayerfeuillete2[2].ToString();

                            }
                            if (GlazingTypeFeuilleté4.Visibility == Visibility.Visible)
                            {
                                GlazingTypeFeuilleté4.SelectedIndex = FilledInfos.GlazingTypeFromLayersIndexes2[3];
                                epaisseurfeuilleté4txtbx.Text = FilledInfos.thicknessfromeachLayerfeuillete2[3].ToString();
                            }
                        }
                        else 
                        {
                            feuilletelbx.SelectedIndex = -1;
                            epaisseurfeuilleté1txtbx.Visibility = Visibility.Hidden;
                            epaisseurfeuilleté2txtbx.Visibility = Visibility.Hidden;
                            GlazingTypeFeuilleté1.Visibility = Visibility.Hidden;
                            GlazingTypeFeuilleté2.Visibility = Visibility.Hidden;
                        }   
                    }
                }
            }
            else if (IndexInfeuilette == 3)
            {
                if (isolant3facescombobx.SelectedIndex == 0)
                {
                    //Issues Prevention
                    if (!Double.TryParse(epaisseurmonolithiquetxtbx.Text, out FilledInfos.thicknessformonolithiques2))
                    {
                        MessageBox.Show("Veuillez entrer une valeur correcte");
                        IndexInfeuilette--;
                    }

                    else if (GlazingTypemonolithiquescombobx.SelectedIndex == -1)
                    {
                        MessageBox.Show("Veuillez choisir le type du vitrage monolithique");
                        IndexInfeuilette--;
                    }
                    if (FilledInfos.thicknessformonolithiques2 == 0) 
                    {
                        MessageBox.Show("L'épaisseur ne peut pas être nulle");
                        IndexInfeuilette--;
                    }
                    //Else we go to the next step 
                    else
                    {
                        FilledInfos.GlazingTypeMonolithiqueIndex2 = GlazingTypemonolithiquescombobx.SelectedIndex;
                        GlazingTypemonolithiquescombobx.SelectedIndex = -1;
                        epaisseurmonolithiquetxtbx.Text = string.Empty;
                    }
                }
                else if (isolant3facescombobx.SelectedIndex == 1)
                {
                    //Issues Prevention
                    if (!Double.TryParse(epaisseurmonolithiquetxtbx.Text, out FilledInfos.thicknessformonolithiques2))
                    {
                        MessageBox.Show("Veuillez entrer une valeur correcte");
                        IndexInfeuilette--;
                    }
                    if (FilledInfos.thicknessformonolithiques2 == 0)
                    {
                        MessageBox.Show("L'épaisseur ne peut pas être nulle");
                        IndexInfeuilette--;
                    }    
                    else if (GlazingTypemonolithiquescombobx.SelectedIndex == -1)
                    {
                        MessageBox.Show("Veuillez choisir le type du vitrage monolithique");
                        IndexInfeuilette--;
                    }
                    else
                    {
                        epaisseurmonolithiquetxtbx.Text = string.Empty;
                        epaisseurmonolithiquetxtbx.Visibility = Visibility.Hidden;
                        Monolithiqueslbl.Visibility = Visibility.Hidden;
                        GlazingTypemonolithiquescombobx.Visibility = Visibility.Hidden;
                        FilledInfos.GlazingTypeMonolithiqueIndex2 = GlazingTypemonolithiquescombobx.SelectedIndex;
                        feuilletelbx.Visibility = Visibility.Visible;
                    }
                }
                else if (isolant3facescombobx.SelectedIndex == 2 || isolant3facescombobx.SelectedIndex == 3) 
                {
                    //Issues prevention
                    if (GlazingTypeFeuilleté1.SelectedIndex == -1 || GlazingTypeFeuilleté2.SelectedIndex == -1 || (GlazingTypeFeuilleté3.SelectedIndex == -1 && GlazingTypeFeuilleté3.Visibility == Visibility.Visible) || (GlazingTypeFeuilleté4.SelectedIndex == -1 && GlazingTypeFeuilleté4.Visibility == Visibility.Visible))
                    {
                        MessageBox.Show("Veuillez choisir le type du vitrage monolithique pour chaque couche");
                        IndexInfeuilette--;
                    }
                    //Issues prevention for thickness filled infos
                    else if (!Double.TryParse(epaisseurfeuilleté1txtbx.Text, out FilledInfos.thicknessfromeachLayerfeuillete2[0]) || !Double.TryParse(epaisseurfeuilleté2txtbx.Text, out FilledInfos.thicknessfromeachLayerfeuillete2[1]))
                    {
                        MessageBox.Show("Veuillez vérifier les informations saisies pour les épaisseurs");
                        IndexInfeuilette--;
                    }
                    else if (FilledInfos.thicknessfromeachLayerfeuillete2[0] == 0 || FilledInfos.thicknessfromeachLayerfeuillete2[1] == 0)
                    {
                        MessageBox.Show("Les épaisseurs ne peuvent pas être nulles");
                        IndexInfeuilette--;
                    }
                    else if ((epaisseurfeuilleté3txtbx.Visibility == Visibility.Visible && !Double.TryParse(epaisseurfeuilleté3txtbx.Text, out FilledInfos.thicknessfromeachLayerfeuillete2[2])))
                    {
                        MessageBox.Show("Veuillez vérifier les informations saisies pour les épaisseurs");
                        IndexInfeuilette--;
                    }
                    else if ((epaisseurfeuilleté4txtbx.Visibility == Visibility.Visible && !Double.TryParse(epaisseurfeuilleté4txtbx.Text, out FilledInfos.thicknessfromeachLayerfeuillete2[3])))
                    {
                        MessageBox.Show("Veuillez vérifier les informations saisies pour les épaisseurs");
                        IndexInfeuilette--;
                    }
                    else if ((epaisseurfeuilleté3txtbx.Visibility == Visibility.Visible && FilledInfos.thicknessfromeachLayerfeuillete2[2] == 0) || (epaisseurfeuilleté4txtbx.Visibility == Visibility.Visible && FilledInfos.thicknessfromeachLayerfeuillete2[3] == 0))
                    {
                        MessageBox.Show("Les épaisseurs ne peuvent pas être nulles");
                        IndexInfeuilette--;
                    }
                    else
                    {
                        FilledInfos.FeuilleteIndex2 = feuilletelbx.SelectedIndex;
                        FilledInfos.GlazingTypeFromLayersIndexes2[0] = GlazingTypeFeuilleté1.SelectedIndex;
                        FilledInfos.GlazingTypeFromLayersIndexes2[1] = GlazingTypeFeuilleté2.SelectedIndex;
                        FilledInfos.GlazingTypeFromLayersIndexes2[2] = GlazingTypeFeuilleté3.SelectedIndex;
                        FilledInfos.GlazingTypeFromLayersIndexes2[3] = GlazingTypeFeuilleté4.SelectedIndex;

                        feuilletelbx.SelectedIndex = -1;
                        epaisseurfeuilleté1txtbx.Visibility = Visibility.Hidden;
                        epaisseurfeuilleté2txtbx.Visibility = Visibility.Hidden;
                        GlazingTypeFeuilleté1.Visibility = Visibility.Hidden;
                        GlazingTypeFeuilleté2.Visibility = Visibility.Hidden;
                    }
                }
            }

            //We enable the button, depending if we need 2 or 3 layers of glazing 
            if (GlazingTypecombobx.SelectedIndex == 2) 
            {
                if (IndexInfeuilette == 2) 
                {
                    thicknessCalculatebtn.IsEnabled = true;
                    NextButton.IsEnabled = false;
                }
            }
            else if (GlazingTypecombobx.SelectedIndex == 3) 
            {
                if (IndexInfeuilette == 3) 
                {
                    NextButton.IsEnabled = false;
                    thicknessCalculatebtn.IsEnabled = true;
                }
            }
            
             
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            IndexInfeuilette--;

            thicknessCalculatebtn.IsEnabled = false;

            if (IndexInfeuilette < 0)
                IndexInfeuilette = 0;

            //We change the type of our glazing (double feuilleté, monolithique + feuilleté...)
            if (IndexInfeuilette == 0)
            {
                BackButton.Visibility = Visibility.Hidden;
                NextButton.Visibility = Visibility.Hidden;

                //Reset everying in FilledInfos
                FilledInfos.FeuilleteIndex = -1;
                FilledInfos.FeuilleteIndex2 = -1;
                FilledInfos.GlazingTypeFromLayersIndexes = new int[4];
                FilledInfos.GlazingTypeFromLayersIndexes2 = new int[4];
                FilledInfos.GlazingTypeMonolithiqueIndex = -1;
                FilledInfos.GlazingTypeMonolithiqueIndex2 = -1;
                FilledInfos.thicknessformonolithiques = 0;
                FilledInfos.thicknessformonolithiques2 = 0;
                FilledInfos.thicknessfromeachLayerfeuillete = new double[4];
                FilledInfos.thicknessfromeachLayerfeuillete2 = new double[4];

                if (GlazingTypecombobx.SelectedIndex == 2)
                    isolant2facescombobx.Visibility = Visibility.Visible;
                else if (GlazingTypecombobx.SelectedIndex == 3)
                    isolant3facescombobx.Visibility = Visibility.Visible;

                Monolithiqueslbl.Visibility = Visibility.Hidden;
                epaisseurmonolithiquetxtbx.Visibility = Visibility.Hidden;
                epaisseurmonolithiquetxtbx.Text = string.Empty;
                GlazingTypemonolithiquescombobx.SelectedIndex = -1;
                GlazingTypemonolithiquescombobx.Visibility = Visibility.Hidden;

                feuilletelbx.SelectedIndex = -1;
                feuilletelbx.Visibility = Visibility.Hidden;

                epaisseurfeuilleté1txtbx.Visibility = Visibility.Hidden;
                epaisseurfeuilleté2txtbx.Visibility = Visibility.Hidden;
                GlazingTypeFeuilleté1.Visibility = Visibility.Hidden;
                GlazingTypeFeuilleté2.Visibility = Visibility.Hidden;

                IndexInfeuilette = 1;

            }

            //Isolant 2 faces
            else if (GlazingTypecombobx.SelectedIndex == 2) 
            {
                if (IndexInfeuilette == 1 )
                {
                    NextButton.IsEnabled = true;
                    switch (isolant2facescombobx.SelectedIndex)
                    {
                        case 0:
                            //We need to get Back our infos and redisplay our things
                            epaisseurmonolithiquetxtbx.Text = FilledInfos.thicknessformonolithiques.ToString();
                            GlazingTypemonolithiquescombobx.SelectedIndex = FilledInfos.GlazingTypeMonolithiqueIndex;
                            break;
                        case 1:
                            Monolithiqueslbl.Visibility = Visibility.Visible;
                            epaisseurmonolithiquetxtbx.Visibility = Visibility.Visible;
                            GlazingTypemonolithiquescombobx.Visibility = Visibility.Visible;
                            epaisseurmonolithiquetxtbx.Text = FilledInfos.thicknessformonolithiques.ToString();
                            GlazingTypemonolithiquescombobx.SelectedIndex = FilledInfos.GlazingTypeMonolithiqueIndex;

                            //Putting the SelectedIndex = -1 call the selection changed method of the feuilletelbx
                            //which makes almost all the job of hiding
                            feuilletelbx.SelectedIndex = -1;
                            epaisseurfeuilleté1txtbx.Visibility = Visibility.Hidden;
                            epaisseurfeuilleté2txtbx.Visibility = Visibility.Hidden;
                            GlazingTypeFeuilleté1.Visibility = Visibility.Hidden;
                            GlazingTypeFeuilleté2.Visibility = Visibility.Hidden;
                            feuilletelbx.Visibility = Visibility.Hidden;
                            break;
                        case 2:
                            feuilletelbx.SelectedIndex = FilledInfos.FeuilleteIndex;
                            GlazingTypeFeuilleté1.SelectedIndex = FilledInfos.GlazingTypeFromLayersIndexes[0];
                            GlazingTypeFeuilleté2.SelectedIndex = FilledInfos.GlazingTypeFromLayersIndexes[1];
                            epaisseurfeuilleté1txtbx.Text = FilledInfos.thicknessfromeachLayerfeuillete[0].ToString();
                            epaisseurfeuilleté2txtbx.Text = FilledInfos.thicknessfromeachLayerfeuillete[1].ToString();
                            if (epaisseurfeuilleté3txtbx.Visibility == Visibility.Visible)
                            {
                                GlazingTypeFeuilleté3.SelectedIndex = FilledInfos.GlazingTypeFromLayersIndexes[2];
                                epaisseurfeuilleté3txtbx.Text = FilledInfos.thicknessfromeachLayerfeuillete[2].ToString();
                            }

                            if (epaisseurfeuilleté4txtbx.Visibility == Visibility.Visible)
                            {
                                GlazingTypeFeuilleté4.SelectedIndex = FilledInfos.GlazingTypeFromLayersIndexes[3];
                                epaisseurfeuilleté4txtbx.Text = FilledInfos.thicknessfromeachLayerfeuillete[3].ToString();
                            }
                            break;
                    }
                }
            }

            else if (GlazingTypecombobx.SelectedIndex == 3) 
            {
                if (IndexInfeuilette == 1) 
                {
                    switch (isolant3facescombobx.SelectedIndex) 
                    {
                        case 0: case 1:
                            //We need to get Back our infos and redisplay our things
                            epaisseurmonolithiquetxtbx.Text = FilledInfos.thicknessformonolithiques.ToString();
                            GlazingTypemonolithiquescombobx.SelectedIndex = FilledInfos.GlazingTypeMonolithiqueIndex;
                            break;
                        case 2:
                            Monolithiqueslbl.Visibility = Visibility.Visible;
                            epaisseurmonolithiquetxtbx.Visibility = Visibility.Visible;
                            GlazingTypemonolithiquescombobx.Visibility = Visibility.Visible;
                            epaisseurmonolithiquetxtbx.Text = FilledInfos.thicknessformonolithiques.ToString();
                            GlazingTypemonolithiquescombobx.SelectedIndex = FilledInfos.GlazingTypeMonolithiqueIndex;

                            //Putting the SelectedIndex = -1 call the selection changed method of the feuilletelbx
                            //which makes almost all the job of hiding
                            feuilletelbx.SelectedIndex = -1;
                            epaisseurfeuilleté1txtbx.Visibility = Visibility.Hidden;
                            epaisseurfeuilleté2txtbx.Visibility = Visibility.Hidden;
                            GlazingTypeFeuilleté1.Visibility = Visibility.Hidden;
                            GlazingTypeFeuilleté2.Visibility = Visibility.Hidden;
                            feuilletelbx.Visibility = Visibility.Hidden;
                            break;
                        case 3:
                            feuilletelbx.SelectedIndex = FilledInfos.FeuilleteIndex;
                            GlazingTypeFeuilleté1.SelectedIndex = FilledInfos.GlazingTypeFromLayersIndexes[0];
                            GlazingTypeFeuilleté2.SelectedIndex = FilledInfos.GlazingTypeFromLayersIndexes[1];
                            epaisseurfeuilleté1txtbx.Text = FilledInfos.thicknessfromeachLayerfeuillete[0].ToString();
                            epaisseurfeuilleté2txtbx.Text = FilledInfos.thicknessfromeachLayerfeuillete[1].ToString();
                            if (epaisseurfeuilleté3txtbx.Visibility == Visibility.Visible)
                            {
                                GlazingTypeFeuilleté3.SelectedIndex = FilledInfos.GlazingTypeFromLayersIndexes[2];
                                epaisseurfeuilleté3txtbx.Text = FilledInfos.thicknessfromeachLayerfeuillete[2].ToString();
                            }

                            if (epaisseurfeuilleté4txtbx.Visibility == Visibility.Visible)
                            {
                                GlazingTypeFeuilleté4.SelectedIndex = FilledInfos.GlazingTypeFromLayersIndexes[3];
                                epaisseurfeuilleté4txtbx.Text = FilledInfos.thicknessfromeachLayerfeuillete[3].ToString();
                            }
                            break;

                    }
                }

                if (IndexInfeuilette == 2) 
                {
                    NextButton.IsEnabled = true;

                    switch (isolant3facescombobx.SelectedIndex)
                    {
                        case 0:
                            //We need to get Back our infos and redisplay our things
                            epaisseurmonolithiquetxtbx.Text = FilledInfos.thicknessformonolithiques2.ToString();
                            GlazingTypemonolithiquescombobx.SelectedIndex = FilledInfos.GlazingTypeMonolithiqueIndex2;
                            break;
                        case 1:
                            Monolithiqueslbl.Visibility = Visibility.Visible;
                            epaisseurmonolithiquetxtbx.Visibility = Visibility.Visible;
                            GlazingTypemonolithiquescombobx.Visibility = Visibility.Visible;
                            epaisseurmonolithiquetxtbx.Text = FilledInfos.thicknessformonolithiques2.ToString();
                            GlazingTypemonolithiquescombobx.SelectedIndex = FilledInfos.GlazingTypeMonolithiqueIndex2;

                            //Putting the SelectedIndex = -1 call the selection changed method of the feuilletelbx
                            //which makes almost all the job of hiding
                            feuilletelbx.SelectedIndex = -1;
                            epaisseurfeuilleté1txtbx.Visibility = Visibility.Hidden;
                            epaisseurfeuilleté2txtbx.Visibility = Visibility.Hidden;
                            GlazingTypeFeuilleté1.Visibility = Visibility.Hidden;
                            GlazingTypeFeuilleté2.Visibility = Visibility.Hidden;
                            feuilletelbx.Visibility = Visibility.Hidden;
                            break;
                        case 2: case 3:
                            feuilletelbx.SelectedIndex = FilledInfos.FeuilleteIndex2;
                            GlazingTypeFeuilleté1.SelectedIndex = FilledInfos.GlazingTypeFromLayersIndexes2[0];
                            GlazingTypeFeuilleté2.SelectedIndex = FilledInfos.GlazingTypeFromLayersIndexes2[1];
                            epaisseurfeuilleté1txtbx.Text = FilledInfos.thicknessfromeachLayerfeuillete2[0].ToString();
                            epaisseurfeuilleté2txtbx.Text = FilledInfos.thicknessfromeachLayerfeuillete2[1].ToString();
                            if (epaisseurfeuilleté3txtbx.Visibility == Visibility.Visible)
                            {
                                GlazingTypeFeuilleté3.SelectedIndex = FilledInfos.GlazingTypeFromLayersIndexes2[2];
                                epaisseurfeuilleté3txtbx.Text = FilledInfos.thicknessfromeachLayerfeuillete2[2].ToString();
                            }

                            if (epaisseurfeuilleté4txtbx.Visibility == Visibility.Visible)
                            {
                                GlazingTypeFeuilleté4.SelectedIndex = FilledInfos.GlazingTypeFromLayersIndexes2[3];
                                epaisseurfeuilleté4txtbx.Text = FilledInfos.thicknessfromeachLayerfeuillete2[3].ToString();
                            }
                            break;
                    }
                }
            }
            
        }

        private void isolant3facescombobx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BackButton.Visibility != Visibility.Visible && NextButton.Visibility != Visibility.Visible)
            {
                BackButton.Visibility = Visibility.Visible;
                NextButton.Visibility = Visibility.Visible;
            }
            isolant3facescombobx.Visibility = Visibility.Hidden;

            switch (isolant3facescombobx.SelectedIndex) 
            {
                case 0: case 1: case 2:
                    Monolithiqueslbl.Visibility = Visibility.Visible;
                    GlazingTypemonolithiquescombobx.Visibility = Visibility.Visible;
                    epaisseurmonolithiquetxtbx.Visibility = Visibility.Visible;
                    break;
                case 3:
                    feuilletelbx.Visibility = Visibility.Visible;
                    break;

            }
        }
    }
}
