﻿using System;
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
    /// Interaction logic for HelperWindow.xaml
    /// </summary>
    public partial class HelperWindow : Window
    {
        public HelperWindow()
        {
            InitializeComponent();
        }
        public void DisplayImage(string path) 
        {
            Uri imageUri = new Uri(path, UriKind.RelativeOrAbsolute);
            Image.Source = new BitmapImage(imageUri);
        }
    }
}
