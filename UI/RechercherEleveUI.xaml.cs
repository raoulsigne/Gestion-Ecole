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
using Ecole.BusinessEntity;
using Ecole.BusinessLogic;

namespace Ecole.UI
{
    /// <summary>
    /// Interaction logic for RechercherEleveUI.xaml
    /// </summary>
    public partial class RechercherEleveUI : Window
    {
        List<string> eleves;
        List<string> classes;
        int annee;
        GestionEleveBL eleveBL;

        public string matricule { get; set; }

        public RechercherEleveUI()
        {
            InitializeComponent();
            eleves = new List<string>();
            eleveBL = new GestionEleveBL();
            annee = eleveBL.AnneeEnCours();
            txtAnnee.Text = " / " + annee;
            txtAnneeScolaire.Text = (annee - 1).ToString();
            classes = new List<string>();
            classes = eleveBL.listerValeurColonneClasse("codeclasse");
            cmbClasse.ItemsSource = classes;
        }

        private void cmbClasse_DropDownClosed(object sender, EventArgs e)
        {
            if (cmbClasse.Text != null && cmbClasse.Text != "")
            {
                //recherche des eleves inscrits  dans cette classe pour charger le combobox des eleves
                eleves = new List<string>();
                string codeclasse = cmbClasse.Text;
                List<EleveBE> listeleves = new List<EleveBE>();
                listeleves = eleveBL.listerElevesDuneClasse(codeclasse, annee);
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
            }
        }

        private void txtAnneeScolaire_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                annee = Convert.ToInt32(txtAnneeScolaire.Text) + 1;
                txtAnnee.Text = " / " + annee.ToString();
                if (cmbClasse.Text != null && cmbClasse.Text != "")
                {
                    //recherche des eleves inscrits  dans cette classe pour charger le combobox des eleves
                    eleves = new List<string>();
                    string codeclasse = cmbClasse.Text;
                    List<EleveBE> listeleves = new List<EleveBE>();
                    listeleves = eleveBL.listerElevesDuneClasse(codeclasse, annee);
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
            catch (Exception)
            {
                MessageBox.Show("L'annee doit etre un nombre positif", "School brain:Alerte");
            }
        }

        private void txtAnneeScolaire_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Ecole.UI.Utilitaires.IsTextAllowed(e.Text);
        }

        private void cmdValider_Click(object sender, RoutedEventArgs e)
        {
            matricule = txtMatricule.Text;
            this.Close();
        }
    }
}
