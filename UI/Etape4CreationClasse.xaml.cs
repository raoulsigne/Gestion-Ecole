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
    /// Interaction logic for Etape4CreationClasse.xaml
    /// </summary>
    public partial class Etape4CreationClasse : UserControl
    {
        AssistanceCreationClasseBL assistanceBL; 

        public string choix { get; set; }
        public string codeserie { get; set; }
        public string nomserie { get; set; }

        List<string> listeDeChoix;

        public Etape4CreationClasse()
        {
            InitializeComponent();
            txtCode.IsEnabled = false;
            txtNom.IsEnabled = false;
            assistanceBL = new AssistanceCreationClasseBL();
            listeDeChoix = new List<string>();
            listeDeChoix = assistanceBL.listerValeurColonneSerie("codeserie");
            listeDeChoix.Add(AssistanceCreationClasseBL.CHOIX_NOUVEAU);
            cmbChoisir.ItemsSource = listeDeChoix;
        }

        public Etape4CreationClasse(string code, string nom)
        {
            InitializeComponent();
            listeDeChoix = new List<string>();
            assistanceBL = new AssistanceCreationClasseBL();

            listeDeChoix = assistanceBL.listerValeurColonneSerie("codeserie");
            listeDeChoix.Add(AssistanceCreationClasseBL.CHOIX_NOUVEAU); 
            cmbChoisir.ItemsSource = listeDeChoix;
            txtCode.Text = code;
            txtNom.Text = nom;

            this.codeserie = code;
            this.nomserie = nom;

            if (!listeDeChoix.Contains(code))
            {
                cmbChoisir.Text = AssistanceCreationClasseBL.CHOIX_NOUVEAU;
                txtCode.IsEnabled = true;
                txtNom.IsEnabled = true;
            }
            else
            {
                cmbChoisir.Text = code;
                txtCode.IsEnabled = false;
                txtNom.IsEnabled = false;
            }
        }

        private void cmbChoisir_DropDownClosed(object sender, EventArgs e)
        {
            if (cmbChoisir.Text == AssistanceCreationClasseBL.CHOIX_NOUVEAU)
            {
                txtCode.IsEnabled = true;
                txtNom.IsEnabled = true;

                txtCode.Text = "";
                txtNom.Text = "";
            }
            else
            {
                codeserie = cmbChoisir.Text;
                SerieBE serie = new SerieBE();
                serie.codeserie = codeserie;
                serie = assistanceBL.rechercherSerie(serie);
                if (serie != null)
                {
                    txtCode.Text = serie.codeserie;
                    txtNom.Text = serie.nomserie;
                    nomserie = serie.nomserie;
                    txtCode.IsEnabled = false;
                    txtNom.IsEnabled = false;
                }
            }
        }

        private void txtCode_LostFocus(object sender, RoutedEventArgs e)
        {
            codeserie = txtCode.Text;
        }

        private void txtNom_LostFocus(object sender, RoutedEventArgs e)
        {
            nomserie = txtNom.Text;
        }
    }
}
