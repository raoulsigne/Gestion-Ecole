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

namespace Ecole.UI
{
    /// <summary>
    /// Interaction logic for ReunionEleveUC.xaml
    /// </summary>
    public partial class ReunionEleveUC : UserControl
    {
        GestionNotificationBL notificationBL;

        public string concerne { get; set; }
        public string code { get; set; }
        public string message { get; set; }
        public bool modeSMS { get; set; }
        public bool modeEmail { get; set; }

        public const string MIXTE = "Mixte";
        public const string NIVEAU = "Niveau";
        public const string CYCLE = "Cycle";
        public const string SERIE = "Serie";
        public const string CLASSE = "Classe";

        int nbcaracteres;
        List<string> concernes;
        List<string> codes;

        public ReunionEleveUC()
        {
            InitializeComponent();
            notificationBL = new GestionNotificationBL();
            //chkEmail.IsChecked = true;
            //chkSMS.IsChecked = true;
            nbcaracteres = 0;
            concernes = new List<string>();
            codes = new List<string>();
            concernes.Add(CLASSE);
            concernes.Add(SERIE);
            concernes.Add(NIVEAU);
            concernes.Add(CYCLE);
            concernes.Add(MIXTE);
            cmbConcerne.ItemsSource = concernes;
            lblStatut.Content = GestionNotificationBL.INFORMATIONS_NON_VALIDEES;
        }

        private void cmbConcerne_DropDownClosed(object sender, EventArgs e)
        {
            cmbChoix.IsEnabled = true;

            switch (cmbConcerne.Text)
            {
                case CLASSE:
                    codes = notificationBL.listerValeurColonneClasse("codeclasse");
                    cmbChoix.ItemsSource = codes;
                    cmbChoix.Items.Refresh();
                    break;
                case SERIE:
                    codes = notificationBL.listerValeurColonneSerie("codeserie");
                    cmbChoix.ItemsSource = codes;
                    cmbChoix.Items.Refresh();
                    break;
                case CYCLE:
                    codes = notificationBL.listerValeurColonneCycle("codecycle");
                    cmbChoix.ItemsSource = codes;
                    cmbChoix.Items.Refresh();
                    break;
                case NIVEAU:
                    codes = notificationBL.listerValeurColonneNiveau("codeniveau");
                    cmbChoix.ItemsSource = codes;
                    cmbChoix.Items.Refresh();
                    break;
                case MIXTE:
                    cmbChoix.Text = "";
                    cmbChoix.IsEnabled = false;
                    break;
                default:
                    break;
            }
        }

        private void cmbChoix_DropDownClosed(object sender, EventArgs e)
        {
            code = cmbChoix.Text;
        }

        private void cmdOK_Click(object sender, RoutedEventArgs e)
        {
            if (txtMessage.Text != "" & cmbConcerne.Text != "")
            {
                lblStatut.Content = GestionNotificationBL.INFORMATIONS_VALIDEES;
                concerne = cmbConcerne.Text;
                code = cmbChoix.Text;
                message = txtMessage.Text;
                //modeSMS = (bool)chkSMS.IsChecked;
                modeEmail = false;
            }
            else
                MessageBox.Show("School brain:Alerte", "Certains champs sont vides", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void txtHeure_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Ecole.UI.Utilitaires.IsTextAllowed(e.Text);
        }

        private void txtMinute_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Ecole.UI.Utilitaires.IsTextAllowed(e.Text);
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
