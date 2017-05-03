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
    /// Interaction logic for ReunionUC.xaml
    /// </summary>
    public partial class ReunionUC : UserControl
    {

        public string concerne { get; set; }
        public string message { get; set; }
        public bool modeSMS { get; set; }
        public bool modeEmail { get; set; }

        public const string MIXTE = "Mixte";
        public const string ENSEIGNANT = "Enseignant";
        public const string PARENT = "Parent";

        int nbcaracteres;
        List<string> concernes;

        public ReunionUC()
        {
            InitializeComponent();

            //chkEmail.IsChecked = true;
            //chkSMS.IsChecked = true;
            nbcaracteres = 0;
            concernes = new List<string>();
            concernes.Add(ENSEIGNANT);
            concernes.Add(PARENT);
            concernes.Add(MIXTE);
            cmbConcerne.ItemsSource = concernes;
            cmbConcerne.SelectedIndex = 0;
            lblStatut.Content = GestionNotificationBL.INFORMATIONS_NON_VALIDEES;
        }

        private void cmdOK_Click(object sender, RoutedEventArgs e)
        {
            if (txtMessage.Text != "" & cmbConcerne.Text != "" )
            {
                lblStatut.Content = GestionNotificationBL.INFORMATIONS_VALIDEES;
                concerne = cmbConcerne.Text;
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
