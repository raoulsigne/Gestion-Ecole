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
using Ecole.BusinessEntity;
using Ecole.BusinessLogic;

namespace Ecole.UI
{
    /// <summary>
    /// Interaction logic for EnvoyerResultatUC.xaml
    /// </summary>
    public partial class EnvoyerResultatUC : UserControl
    {
        GestionNotificationBL notificationBL;

        public string periode { get; set; }
        public string classe { get; set; }
        public string message { get; set; }
        public bool modeSMS { get; set; }
        public bool modeEmail { get; set; }

        public const string TOUTE = "Toutes";
        public const string ANNUEL = "Annuelle";

        int annee;
        List<string> classes;
        List<string> periodes;

        public EnvoyerResultatUC()
        {
            InitializeComponent();
            notificationBL = new GestionNotificationBL();
            //chkEmail.IsChecked = true;
            //chkSMS.IsChecked = true;
            classes = new List<string>();
            classes = notificationBL.listerValeurColonneClasse("codeclasse");
            classes.Add(TOUTE);
            cmbClasse.ItemsSource = classes;
            annee = notificationBL.anneeEnCours();
            txtAnnee.Text = " / " + annee;
            txtAnneeScolaire.Text = (annee - 1).ToString();
            List<string> tampon = new List<string>();
            periodes = new List<string>();
            tampon = notificationBL.listerValeurColonneSequence("codeseq");
            if (tampon != null)
                foreach (string s in tampon)
                    periodes.Add(s);
            tampon = notificationBL.listerValeurColonneTrimestre("codetrimestre");
            if (tampon != null)
                foreach (string s in tampon)
                    periodes.Add(s);
            periodes.Add(ANNUEL);
            cmbPeriode.ItemsSource = periodes;
            txtMessage.IsEnabled = false;
            lblStatut.Content = GestionNotificationBL.INFORMATIONS_NON_VALIDEES;
        }

        private void cmdOK_Click(object sender, RoutedEventArgs e)
        {
            if (cmbClasse.Text != "" & cmbPeriode.Text != "")
            {
                lblStatut.Content = GestionNotificationBL.INFORMATIONS_VALIDEES;
                periode = cmbPeriode.Text;
                classe = cmbClasse.Text;
                message = "";
                //modeSMS = (bool)chkSMS.IsChecked;
                modeEmail = false;
            }
            else
                MessageBox.Show("School brain:Alerte", "Certains champs sont vides", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void txtAnneeScolaire_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                annee = Convert.ToInt32(txtAnneeScolaire.Text) + 1;
                txtAnnee.Text = " / " + annee.ToString();
            }
            catch (Exception)
            {
                MessageBox.Show("L'annee doit etre un nombre positif", "School brain:Alerte");
            }
        }

        private void txtAnneeScolaire_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Ecole.UI.Utilitaires.IsTextAllowed(e.Text);
        }

        public override string ToString()
        {
            return "Periode = " + periode + " classe = " + classe + " message = " + message;
        }
    }
}
