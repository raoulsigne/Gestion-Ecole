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
    /// Interaction logic for Etape2CreationClasse.xaml
    /// </summary>
    public partial class Etape2CreationClasse : UserControl
    {
        AssistanceCreationClasseBL assistanceBL; 

        public string choix { get; set; }
        public string codeniveau { get; set; }
        public string nomniveau { get; set; }
        public int niveau { get; set; }

        List<string> listeDeChoix;

        public Etape2CreationClasse()
        {
            InitializeComponent();
            txtCode.IsEnabled = false;
            txtNom.IsEnabled = false;
            txtNiveau.IsEnabled = false;
            assistanceBL = new AssistanceCreationClasseBL();
            listeDeChoix = new List<string>();
            listeDeChoix = assistanceBL.listerValeurColonneNiveau("codeniveau");
            listeDeChoix.Add(AssistanceCreationClasseBL.CHOIX_NOUVEAU);
            cmbChoisir.ItemsSource = listeDeChoix;
        }

        public Etape2CreationClasse(string code, string nom, int niveau)
        {
            InitializeComponent();
            listeDeChoix = new List<string>();
            assistanceBL = new AssistanceCreationClasseBL();

            listeDeChoix = assistanceBL.listerValeurColonneNiveau("codeniveau");
            listeDeChoix.Add(AssistanceCreationClasseBL.CHOIX_NOUVEAU); 
            cmbChoisir.ItemsSource = listeDeChoix;
            txtCode.Text = code;
            txtNom.Text = nom;
            txtNiveau.Text = niveau.ToString();
            this.codeniveau = code;
            this.nomniveau = nom;
            this.niveau = niveau;

            if (!listeDeChoix.Contains(code))
            {
                cmbChoisir.Text = AssistanceCreationClasseBL.CHOIX_NOUVEAU;
                txtCode.IsEnabled = true;
                txtNom.IsEnabled = true;
                txtNiveau.IsEnabled = true;
            }
            else
            {
                cmbChoisir.Text = code;
                txtCode.IsEnabled = false;
                txtNom.IsEnabled = false;
                txtNiveau.IsEnabled = false;
            }
        }

        private void cmbChoisir_DropDownClosed(object sender, EventArgs e)
        {
            if (cmbChoisir.Text == AssistanceCreationClasseBL.CHOIX_NOUVEAU)
            {
                txtCode.IsEnabled = true;
                txtNom.IsEnabled = true;
                txtNiveau.IsEnabled = true;
                txtCode.Text = "";
                txtNom.Text = "";
                txtNiveau.Text = "";
            }
            else
            {
                codeniveau = cmbChoisir.Text;
                NiveauBE niveau = new NiveauBE();
                niveau.codeNiveau = codeniveau;
                niveau = assistanceBL.rechercherNiveau(niveau);
                if (niveau != null)
                {
                    txtCode.Text = niveau.codeNiveau;
                    txtNom.Text = niveau.nomNiveau;
                    txtNiveau.Text = niveau.niveau.ToString();
                    nomniveau = niveau.nomNiveau;
                    this.niveau = niveau.niveau;
                    txtCode.IsEnabled = false;
                    txtNom.IsEnabled = false;
                    txtNiveau.IsEnabled = false;
                }
            }
        }

        private void txtCode_LostFocus(object sender, RoutedEventArgs e)
        {
            codeniveau = txtCode.Text;
        }

        private void txtNom_LostFocus(object sender, RoutedEventArgs e)
        {
            nomniveau = txtNom.Text;
        }

        private void txtNiveau_LostFocus(object sender, RoutedEventArgs e)
        {
            niveau = Convert.ToInt32(txtNiveau.Text.ToString());
        }

        private void txtNiveau_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Utilitaires.IsTextAllowed(e.Text);
        }
    }
}
