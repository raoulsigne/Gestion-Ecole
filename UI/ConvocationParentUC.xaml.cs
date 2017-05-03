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
    /// Interaction logic for ConvocationParentUC.xaml
    /// </summary>
    public partial class ConvocationParentUC : UserControl
    {
        public static int LONGUEUR_SMS = 150;
        GestionNotificationBL notificationBL;

        public string matricule { get; set; }
        public string nom { get; set; }
        public string message { get; set; }
        public bool modeSMS { get; set; }
        public bool modeEmail { get; set; }

        List<string> classes;
        List<string> eleves;
        EleveBE eleve;
        int annee;
        int nbcaracteres;

        public ConvocationParentUC()
        {
            InitializeComponent();
            classes = new List<string>();

            notificationBL = new GestionNotificationBL();
            //chkEmail.IsChecked = true;
            //chkSMS.IsChecked = true;
            nbcaracteres = 0;
            annee = notificationBL.anneeEnCours();
            eleve = new EleveBE();
            classes = notificationBL.listerValeurColonneClasse("codeclasse");
            cmbClasse.ItemsSource = classes;
            lblStatut.Content = GestionNotificationBL.INFORMATIONS_NON_VALIDEES;
        }

        private void cmdOK_Click(object sender, RoutedEventArgs e)
        {
            if (txtMatricule.Text != "" & txtMessage.Text != "")
            {
                lblStatut.Content = GestionNotificationBL.INFORMATIONS_VALIDEES;
                matricule = txtMatricule.Text;
                nom = cmbEleve.Text.Split('-')[1].Trim();
                //modeSMS = (bool)chkSMS.IsChecked;
                modeEmail = false;
                message = txtMessage.Text;
            }
            else
                MessageBox.Show("School brain:Alerte", "Certains champs sont vides", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void cmbClasse_DropDownClosed(object sender, EventArgs e)
        {
            if (cmbClasse.Text != null && cmbClasse.Text != "")
            {
                //recherche des eleves inscrits  dans cette classe pour charger le combobox des eleves
                eleves = new List<string>();
                string codeclasse = cmbClasse.Text;
                List<EleveBE> listeleves = new List<EleveBE>();
                listeleves = notificationBL.listerElevesDuneClasse(codeclasse, annee);
                if (listeleves != null)
                {
                    foreach (EleveBE el in listeleves)
                    {
                        eleves.Add(el.matricule + " - " + el.nom);
                    }
                }
                cmbEleve.ItemsSource = eleves;
                txtMatricule.Text = "";
            }
        }

        private void cmbEleve_DropDownClosed(object sender, EventArgs e)
        {
            if (cmbEleve.Text != null && cmbEleve.Text != "")
            {
                string nommat = cmbEleve.Text;
                txtMatricule.Text = nommat.Split('-')[0].Trim();
                eleve.matricule = nommat.Split('-')[0].Trim();
                eleve = notificationBL.rechercherEleve(eleve);
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
            else if (e.Key != Key.Back)
                nbcaracteres--;
            
            txtNombreCaracteres.Text = (nbcaracteres % LONGUEUR_SMS).ToString();
            txtNombreSMS.Text = (nbcaracteres / LONGUEUR_SMS).ToString();
        }
    }
}
