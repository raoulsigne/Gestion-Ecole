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
    /// Interaction logic for RecapitulatifSequentielUI.xaml
    /// </summary>
    public partial class RecapitulatifSequentielUI : Window
    {
        GestionRecapitulatifSequentielBL sequenceBL;
        GestionDisciplineBL disciplineBL;
        List<EleveBE> eleves;
        List<LigneRecapitulatif> recapitulatif;
        List<LigneRecapSeq> recapitulatif_new;
        List<string> classes;
        List<string> sequences;
        int annee;

        public RecapitulatifSequentielUI()
        {
            sequenceBL = new GestionRecapitulatifSequentielBL();
            disciplineBL = new GestionDisciplineBL();
            classes = new List<string>();
            sequences = new List<string>();
            eleves = new List<EleveBE>();
            recapitulatif = new List<LigneRecapitulatif>();
            recapitulatif_new = new List<LigneRecapSeq>();

            InitializeComponent();

            classes = sequenceBL.listerValeurColonneClasse("codeclasse");
            sequences = sequenceBL.listerValeurColonneSequence("codeseq");
            cmbClasse.ItemsSource = classes;
            cmbClasse.SelectedIndex = 0;
            cmbSequence.ItemsSource = sequences;
            annee = sequenceBL.anneeEnCours();
            txtAnnee.Text = " / " + annee;
            txtAnneeScolaire.Text = (annee - 1).ToString();
        }

        private void cmdOK_Click_New(object sender, RoutedEventArgs e)
        {
            if (validerFormulaire())
            {
                ClasseBE classe = new ClasseBE();
                classe.codeClasse = cmbClasse.Text;
                classe = sequenceBL.rechercherClasse(classe);
                SequenceBE sequence = new SequenceBE();
                sequence.codeseq = cmbSequence.Text;
                sequence = sequenceBL.rechercherSequence(sequence);
                string nomprof = "";
                List<string> codematieres = new List<string>();
                List<string> codesanctions = new List<string>();

                eleves.Clear();
                recapitulatif_new.Clear();
                LigneRecapSeq ligne;
                eleves = sequenceBL.listeEleveDuneClasse(classe, annee);
                foreach (EleveBE eleve in eleves)
                {
                    ligne = new LigneRecapSeq();
                    ligne = sequenceBL.recapitulatifSequentielEleve_new(eleve, classe.codeClasse, sequence.codeseq, annee);
                    recapitulatif_new.Add(ligne);
                }

                nomprof = sequenceBL.obtenirProfTitulaire(classe.codeClasse, annee);
                codematieres = sequenceBL.listeCodeMatiereDuneClasse(classe.codeClasse, annee);
                codesanctions = disciplineBL.listerCodeDiscipline();

                sequenceBL.journaliser("Impression du recapitulatif sequentiel de " + classe.codeClasse + " de la " + sequence.codeseq);

                // on tri la liste suivant le nom croissant avant d'imprimer
                List<LigneRecapSeq> tampon = recapitulatif_new.OrderBy(o => o.nom).ToList();
                recapitulatif_new.Clear();
                foreach (LigneRecapSeq l in tampon)
                    recapitulatif_new.Add(l);

                double moyenne = sequenceBL.obtenirMoyenneClasse(cmbClasse.Text, cmbSequence.Text, annee);
                StatistiqueClasseBL statistiqueClasseBL = new StatistiqueClasseBL();
                StatistiqueClasseBE stat = statistiqueClasseBL.getStatistiqueDuneSequence(cmbClasse.Text, annee, cmbSequence.Text);

                CreerEtat etat = new CreerEtat("recapitulatifNoteSequentiel-" + classe.codeClasse + "-" + sequence.codeseq, "Récapitulatif des notes de la " + sequence.nomseq);
                etat.recapitulatifNotes_new(recapitulatif_new, classe, stat, nomprof, codematieres, codesanctions, annee, moyenne);
            }
        }

        private void cmdAnnuler_Click(object sender, RoutedEventArgs e)
        {
            cmbClasse.SelectedIndex = 0;
            cmbSequence.Text = "";
        }

        private void cmdFermer_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private bool validerFormulaire()
        {
            if (cmbClasse.SelectedValue != null && cmbSequence.SelectedValue != null && txtAnneeScolaire.Text != "")
                return true;
            else
                return false;
        }

        private void txtAnneeSolaire_TextChanged(object sender, TextChangedEventArgs e)
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
