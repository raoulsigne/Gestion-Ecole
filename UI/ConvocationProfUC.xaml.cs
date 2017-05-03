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
using Ecole.BusinessLogic;
using Ecole.BusinessEntity;

namespace Ecole.UI
{
    /// <summary>
    /// Interaction logic for ConvocationProfUC.xaml
    /// </summary>
    public partial class ConvocationProfUC : UserControl
    {
        GestionNotificationBL notificationBL;

        public string matricule { get; set; }
        public string nom { get; set; }
        public string message { get; set; }
        public bool modeSMS { get; set; }
        public bool modeEmail { get; set; }
        int nbcaracteres;
        List<string> enseignants;
        EnseignantBE enseignant;
        int annee;

        public ConvocationProfUC()
        {
            InitializeComponent();

            notificationBL = new GestionNotificationBL();
            //chkEmail.IsChecked = true;
            nbcaracteres = 0;
            //chkSMS.IsChecked = true;
            annee = notificationBL.anneeEnCours();
            enseignant = new EnseignantBE();
            enseignants = new List<string>();
            List<EnseignantBE> professeurs = notificationBL.listerToutEnseignants();
            if (professeurs != null)
                foreach (EnseignantBE e in professeurs)
                    enseignants.Add(e.codeProf + " - " + e.nomProf);
            cmbEnseignant.ItemsSource = enseignants;
            cmbEnseignant.Items.Refresh();
            lblStatut.Content = GestionNotificationBL.INFORMATIONS_NON_VALIDEES;
        }

        private void cmdOK_Click(object sender, RoutedEventArgs e)
        {
            if (txtMatricule.Text != "" & txtMessage.Text != "")
            {
                lblStatut.Content = GestionNotificationBL.INFORMATIONS_VALIDEES;
                matricule = txtMatricule.Text;
                nom = cmbEnseignant.Text.Split('-')[1].Trim();
                //modeSMS = (bool)chkSMS.IsChecked;
                modeEmail = false;
                message = txtMessage.Text;
            }
            else
                MessageBox.Show("School brain:Alerte", "Certains champs sont vides", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void cmbEnseignant_DropDownClosed(object sender, EventArgs e)
        {
            if (cmbEnseignant.Text != null && cmbEnseignant.Text != "")
            {
                string nommat = cmbEnseignant.Text;
                txtMatricule.Text = nommat.Split('-')[0].Trim();
                enseignant.codeProf = nommat.Split('-')[0].Trim();
                enseignant = notificationBL.rechercherEnseignant(enseignant);
            }
        }

        public override string ToString()
        {
            return "Matricule = " + matricule + " nom = " + nom + " message=" + message;
        }

        private void txtMessage_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Back & e.Key != Key.Delete)
                nbcaracteres++;
            else
                nbcaracteres--;

            txtNombreCaracteres.Text = (nbcaracteres % ConvocationParentUC.LONGUEUR_SMS).ToString();
            txtNombreSMS.Text = (nbcaracteres / ConvocationParentUC.LONGUEUR_SMS).ToString();
        }
    }
}
