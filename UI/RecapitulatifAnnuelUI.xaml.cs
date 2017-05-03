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
using Ecole.BusinessLogic;
using Ecole.BusinessEntity;
using Ecole.Utilitaire;
using Ecole.ClasseConception;

namespace Ecole.UI
{
    /// <summary>
    /// Interaction logic for RecapitulatifAnnuelUI.xaml
    /// </summary>
    public partial class RecapitulatifAnnuelUI : Window
    {
        GestionRecapitulatifAnnuelBL annuelBL;
        List<EleveBE> eleves;
        List<LigneRecapitulatif> recapitulatif;
        List<string> classes;
        int annee;
        double moyenne;

        public RecapitulatifAnnuelUI()
        {
            annuelBL = new GestionRecapitulatifAnnuelBL();
            classes = new List<string>();
            eleves = new List<EleveBE>();
            recapitulatif = new List<LigneRecapitulatif>();
            moyenne = 0;

            InitializeComponent();

            classes = annuelBL.listerValeurColonneClasse("codeclasse");
            cmbClasse.ItemsSource = classes;
            cmbClasse.SelectedIndex = 0;
            annee = annuelBL.anneeEnCours();
            txtAnnee.Text = " / " + annee;
            txtAnneeScolaire.Text = (annee - 1).ToString();
        }

        private void cmdOK_Click(object sender, RoutedEventArgs e)
        {
            if (validerFormulaire())
            {
                ClasseBE classe = new ClasseBE();
                classe.codeClasse = cmbClasse.Text;
                classe = annuelBL.rechercherClasse(classe);
                string nomprof = "";
                List<string> codematieres = new List<string>();
                List<string> codegroupes = new List<string>();

                eleves.Clear();
                recapitulatif.Clear();
                LigneRecapitulatif ligne;
                eleves = annuelBL.listeEleveDuneClasse(classe, annee);
                foreach (EleveBE eleve in eleves)
                {
                    ligne = new LigneRecapitulatif();
                    ligne = annuelBL.recapitulatifAnnuelEleve(eleve, classe.codeClasse, annee);
                    recapitulatif.Add(ligne);
                }

                codematieres = annuelBL.listeCodeMatiereDuneClasse(classe.codeClasse, annee);
                codegroupes = annuelBL.listeCodeGroupeDuneClasse(classe.codeClasse, annee);

                nomprof = annuelBL.obtenirProfTitulaire(classe.codeClasse, annee);
                CreerEtat etat = new CreerEtat("recapitulatifAnnuel-" + classe.codeClasse + "-" + annee, "Récapitulatif des notes annuelles");
                annuelBL.journaliser("Impression du recapitulatif annuel de " + classe.codeClasse + " de " + annee);

                // on tri la liste suivant le nom croissant avant d'imprimer
                List<LigneRecapitulatif> tampon = recapitulatif.OrderBy(o => o.nom).ToList();
                recapitulatif.Clear();
                foreach (LigneRecapitulatif l in tampon)
                    recapitulatif.Add(l);

                double moyenne = annuelBL.obtenirMoyenneClasse(cmbClasse.Text, annee);
                etat.recapitulatifNotes(recapitulatif, classe, nomprof, codematieres, codegroupes, annee, moyenne);
            }
        }

        private void cmdAnnuler_Click(object sender, RoutedEventArgs e)
        {
            cmbClasse.SelectedIndex = 0;
        }

        private void cmdFermer_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private bool validerFormulaire()
        {
            if (cmbClasse.SelectedValue != null && txtAnnee.Text != "")
                return true;
            else
                return false;
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
            e.Handled = !Utilitaires.IsTextAllowed(e.Text);
        }
    }
}
