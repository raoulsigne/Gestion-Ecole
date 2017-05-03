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
using Ecole.ClasseConception;
using Ecole.Utilitaire;

namespace Ecole.UI
{
    /// <summary>
    /// Interaction logic for RecapitulatifTrimestrielUI.xaml
    /// </summary>
    public partial class RecapitulatifTrimestrielUI : Window
    {
        GestionRecapitulatifTrimestrielBL trimestreBL;
        List<EleveBE> eleves;
        List<LigneRecapitulatif> recapitulatif;
        List<string> classes;
        List<string> trimestres;
        int annee;
        double moyenne;

        public RecapitulatifTrimestrielUI()
        {
            trimestreBL = new GestionRecapitulatifTrimestrielBL();
            classes = new List<string>();
            trimestres = new List<string>();
            eleves = new List<EleveBE>();
            recapitulatif = new List<LigneRecapitulatif>();
            moyenne = 0;

            InitializeComponent();

            classes = trimestreBL.listerValeurColonneClasse("codeclasse");
            trimestres = trimestreBL.listerValeurColonneTrimestre("codetrimestre");
            cmbClasse.ItemsSource = classes;
            cmbClasse.SelectedIndex = 0;
            cmbTrimestre.ItemsSource = trimestres;
            annee = trimestreBL.anneeEnCours();
            txtAnnee.Text = " / " + annee;
            txtAnneeScolaire.Text = (annee - 1).ToString();
        }

        private void cmdOK_Click(object sender, RoutedEventArgs e)
        {
            if (validerFormulaire())
            {
                ClasseBE classe = new ClasseBE();
                classe.codeClasse = cmbClasse.Text;
                classe = trimestreBL.rechercherClasse(classe);
                TrimestreBE trimestre = new TrimestreBE();
                trimestre.codetrimestre = cmbTrimestre.Text;
                trimestre = trimestreBL.rechercherTrimestre(trimestre);
                string nomprof = "";
                List<string> codematieres = new List<string>();
                List<string> codegroupes = new List<string>();

                eleves.Clear();
                recapitulatif.Clear();
                LigneRecapitulatif ligne;
                eleves = trimestreBL.listeEleveDuneClasse(classe, annee);
                foreach (EleveBE eleve in eleves)
                {
                    ligne = new LigneRecapitulatif();
                    ligne = trimestreBL.recapitulatifTrimestrielEleve(eleve, classe.codeClasse, trimestre.codetrimestre, annee);
                    recapitulatif.Add(ligne);
                }

                codematieres = trimestreBL.listeCodeMatiereDuneClasse(classe.codeClasse, annee);
                codegroupes = trimestreBL.listeCodeGroupeDuneClasse(classe.codeClasse, annee);

                nomprof = trimestreBL.obtenirProfTitulaire(classe.codeClasse, annee);
                CreerEtat etat = new CreerEtat("recapitulatifTrimestriel-" + classe.codeClasse + "-" + trimestre.codetrimestre, "Récapitulatif des notes du : " + trimestre.nomtrimestre);
                trimestreBL.journaliser("Impression du recapitulatif trimestriel de "+classe.codeClasse+" du "+trimestre.codetrimestre);

                // on tri la liste suivant le nom croissant avant d'imprimer
                List<LigneRecapitulatif> tampon = recapitulatif.OrderBy(o => o.nom).ToList();
                recapitulatif.Clear();
                foreach (LigneRecapitulatif l in tampon)
                    recapitulatif.Add(l);

                double moyenne = trimestreBL.obtenirMoyenneClasse(cmbClasse.Text, cmbTrimestre.Text, annee);
                etat.recapitulatifNotes(recapitulatif, classe, nomprof, codematieres, codegroupes, annee, moyenne);
            }
        }

        private void cmdAnnuler_Click(object sender, RoutedEventArgs e)
        {
            cmbClasse.SelectedIndex = 0;
            cmbTrimestre.Text = "";
        }

        private void cmdFermer_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private bool validerFormulaire()
        {
            if (cmbClasse.SelectedValue != null && cmbTrimestre.SelectedValue != null && txtAnneeScolaire.Text != "")
                return true;
            else
                return false;
        }

        private void textBox1_TextChanged(object sender, TextChangedEventArgs e)
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
