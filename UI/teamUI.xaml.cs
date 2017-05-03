using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Ecole.UI
{
    /// <summary>
    /// Interaction logic for teamUI.xaml
    /// </summary>
    public partial class teamUI : Window
    {
        public teamUI()
        {
            InitializeComponent();

            try
            {
                image1.Source = new BitmapImage(new Uri(ConnexionUI.DOSSIER_EQUIPE + "\\Monthe.JPG"));
                image3.Source = new BitmapImage(new Uri(ConnexionUI.DOSSIER_EQUIPE + "\\Signe.JPG"));
                image4.Source = new BitmapImage(new Uri(ConnexionUI.DOSSIER_EQUIPE + "\\Yuya.JPG"));
            }
            catch (Exception) 
            { 
            }

        }
    }
}
