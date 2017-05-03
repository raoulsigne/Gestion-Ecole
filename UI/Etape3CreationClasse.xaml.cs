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
    /// Interaction logic for Etape3CreationClasse.xaml
    /// </summary>
    public partial class Etape3CreationClasse : UserControl
    {
        AssistanceCreationClasseBL assistanceBL; 

        public string choix { get; set; }
        public string codetype { get; set; }
        public string nomtype { get; set; }
        public decimal fraisInscription { get; set; }

        List<string> listeDeChoix;

        public Etape3CreationClasse()
        {
            InitializeComponent();
            txtCode.IsEnabled = false;
            txtNom.IsEnabled = false;
            txtFraisInscription.IsEnabled = false;
            assistanceBL = new AssistanceCreationClasseBL();
            listeDeChoix = new List<string>();
            listeDeChoix = assistanceBL.listerValeurColonneTypeclasse("codetypeclasse");
            listeDeChoix.Add(AssistanceCreationClasseBL.CHOIX_NOUVEAU);
            cmbChoisir.ItemsSource = listeDeChoix;
        }

        public Etape3CreationClasse(string code, string nom, decimal frais)
        {
            InitializeComponent();
            listeDeChoix = new List<string>();
            assistanceBL = new AssistanceCreationClasseBL();

            listeDeChoix = assistanceBL.listerValeurColonneTypeclasse("codetypeclasse");
            listeDeChoix.Add(AssistanceCreationClasseBL.CHOIX_NOUVEAU); 
            cmbChoisir.ItemsSource = listeDeChoix;
            txtCode.Text = code;
            txtNom.Text = nom;
            txtFraisInscription.Text = frais.ToString();

            this.codetype = code;
            this.nomtype = nom;
            this.fraisInscription = frais;

            if (!listeDeChoix.Contains(code))
            {
                cmbChoisir.Text = AssistanceCreationClasseBL.CHOIX_NOUVEAU;
                txtCode.IsEnabled = true;
                txtNom.IsEnabled = true;
                txtFraisInscription.IsEnabled = true;
            }
            else
            {
                cmbChoisir.Text = code;
                txtCode.IsEnabled = false;
                txtNom.IsEnabled = false;
                txtFraisInscription.IsEnabled = false;
            }
        }

        private void cmbChoisir_DropDownClosed(object sender, EventArgs e)
        {
            if (cmbChoisir.Text == AssistanceCreationClasseBL.CHOIX_NOUVEAU)
            {
                txtCode.IsEnabled = true;
                txtNom.IsEnabled = true;
                txtFraisInscription.IsEnabled = true;
                txtCode.Text = "";
                txtNom.Text = "";
                txtFraisInscription.Text = "";
            }
            else
            {
                codetype = cmbChoisir.Text;
                TypeclasseBE type = new TypeclasseBE();
                type.codetypeclasse = codetype;
                type = assistanceBL.rechercherTypeClasse(type);
                if (type != null)
                {
                    txtCode.Text = type.codetypeclasse;
                    txtNom.Text = type.nomtypeclasse;
                    txtFraisInscription.Text = type.fraisinscription.ToString();
                    nomtype = type.nomtypeclasse;
                    fraisInscription = type.fraisinscription;
                    txtCode.IsEnabled = false;
                    txtNom.IsEnabled = false;
                    txtFraisInscription.IsEnabled = false;
                }
            }
        }

        private void txtCode_LostFocus(object sender, RoutedEventArgs e)
        {
            codetype = txtCode.Text;
        }
        private void txtNom_LostFocus(object sender, RoutedEventArgs e)
        {
            nomtype = txtNom.Text;
        }
        private void txtFraisInscription_LostFocus(object sender, RoutedEventArgs e)
        {
            fraisInscription = Convert.ToDecimal(txtFraisInscription.Text.ToString());
        }
    }
}
