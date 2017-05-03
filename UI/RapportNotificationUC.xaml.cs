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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Ecole.Utilitaire;

namespace Ecole.UI
{
    /// <summary>
    /// Interaction logic for RapportNotificationUC.xaml
    /// </summary>
    public partial class RapportNotificationUC : UserControl
    {
        List<Adresse> liste;

        public RapportNotificationUC()
        {
            InitializeComponent();
            liste = new List<Adresse>();
        }

        public RapportNotificationUC(int nb, int nbreussi, List<Adresse> echecs)
        {
            InitializeComponent();
            if (echecs != null)
            {
                txtNombre.Text = nbreussi + " / " + nb;
                grdEchecs.ItemsSource = echecs;
                grdEchecs.Items.Refresh();
                liste = echecs;
            }
            else
                txtNombre.Text = "Aucun message envoyé; veuillez contacter votre fournisseur";
        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            ExportToExcel<Adresse> report = new ExportToExcel<Adresse>();
            report.dataToPrint = liste;
            report.GenerateReport();
        }
    }
}
