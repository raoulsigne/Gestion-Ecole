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
using Ecole.Utilitaire;
using Ecole.BusinessEntity;
using Ecole.ClasseConception;

namespace Ecole.UI
{
    /// <summary>
    /// Interaction logic for BilanSequentielUI.xaml
    /// </summary>
    public partial class BilanSequentielUI : Window
    {
        int annee;
        string periode;
        GestionStatistiqueBL statistiqueBL;
        List<string> classes;
        List<string> sequences;
        List<EleveBE> eleves;
        List<string> examens;
        List<KeyValuePair<string, int>> listeSource;
        public static string ANNUEL = "Annuel";
        public static string TRIMESTRIEL = "Trimestriel";
        public static string SEQUENTIEL = "Sequentiel";
        GestionRecapitulatifSequentielBL sequenceBL;
        GestionDisciplineBL disciplineBL;
        List<LigneRecapSeq> recapitulatif_new;

        public BilanSequentielUI()
        {
            sequenceBL = new GestionRecapitulatifSequentielBL();
            disciplineBL = new GestionDisciplineBL();
            eleves = new List<EleveBE>();
            recapitulatif_new = new List<LigneRecapSeq>();

            List<string> tampon = new List<string>();
            classes = new List<string>();
            statistiqueBL = new GestionStatistiqueBL();
            InitializeComponent();

            examens = new List<string>();
            sequences = new List<string>();
            classes = new List<string>();
            listeSource = new List<KeyValuePair<string, int>>();
            classes = statistiqueBL.listerValeurColonneClasse("codeclasse");
            cmbClasse.ItemsSource = classes;
            cmbClasse.SelectedIndex = 0;
            cmbClasse.Items.Refresh();
            annee = statistiqueBL.anneeEnCours();
            txtAnnee.Text = " / " + annee;
            txtAnneeScolaire.Text = (annee - 1).ToString();

            sequences = statistiqueBL.listerValeurColonneSequence("codeseq");
            foreach (string seq in sequences)
                examens.Add(seq);
            cmbExamen.ItemsSource = examens;
        }

        private void cmdOK_Click(object sender, RoutedEventArgs e)
        {
            if (cmbClasse.Text != null && txtAnneeScolaire.Text != null)
            {
                if (cmbClasse.Text != "" && txtAnneeScolaire.Text != "")
                {
                    //traitement
                    string codeclasse = cmbClasse.Text;
                    string examen = cmbExamen.Text;
                    listeSource.Clear();
                    cmdOK.IsEnabled = false;

                    //comparaision sequentielle
                    listeSource = statistiqueBL.effectifValidationMatiereSequentielClasse(codeclasse, annee, examen);
                    periode = "Sequence " + examen;

                    matieres.DataContext = listeSource;
                    matieresLine.DataContext = listeSource;
                }
                else
                    MessageBox.Show("Remplir tous les champs avant de valider", "School brain:Alerte", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            else
                MessageBox.Show("Remplir tous les champs avant de valider", "School brain:Alerte", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void cmdAnnuler_Click(object sender, RoutedEventArgs e)
        {
            listeSource = new List<KeyValuePair<string, int>>();
            matieres.DataContext = listeSource;
            matieresLine.DataContext = listeSource;

            columnChart.Series.Clear();
            lineChart.Series.Clear();
            columnChart.Series.Add(matieres);
            lineChart.Series.Add(matieresLine);
            cmdOK.IsEnabled = true;
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

        private void cmdImprimer_Click(object sender, RoutedEventArgs e)
        {
            if (cmbClasse.Text != null && txtAnneeScolaire.Text != null)
            {
                CreerEtat etat = new CreerEtat("Resultats des matieres d'une classe", "Comparaison du travail des élèves par matière");
                ClasseBE classe = new ClasseBE();
                classe.codeClasse = cmbClasse.Text;
                classe = statistiqueBL.rechercherClasse(classe);

                Dictionary<string, string> stat1 = new Dictionary<string, string>();
                stat1 = statistiqueBL.syntheseClasse(cmbClasse.Text, annee, cmbExamen.Text);

                etat.exportGraphesToPDF(gridChartColumn, gridChartLine, classe, annee, periode, stat1, listeSource);

                SequenceBE sequence = new SequenceBE();
                sequence.codeseq = cmbExamen.Text;
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

                double moyenne = sequenceBL.obtenirMoyenneClasse(cmbClasse.Text, cmbExamen.Text, annee);
                StatistiqueClasseBL statistiqueClasseBL = new StatistiqueClasseBL();
                StatistiqueClasseBE stat = statistiqueClasseBL.getStatistiqueDuneSequence(cmbClasse.Text, annee, cmbExamen.Text);
                Synthese synthese = sequenceBL.obtenirSyntheseSequentielle(cmbClasse.Text, cmbExamen.Text, annee);

                etat = new CreerEtat("recapitulatifNoteSequentiel-" + classe.codeClasse + "-" + sequence.codeseq, "Récapitulatif des notes de la " + sequence.nomseq);
                etat.recapitulatifNotes_new(recapitulatif_new, classe, stat, nomprof, codematieres, codesanctions, annee, moyenne);

                etat = new CreerEtat("recapitulatifMoyenneSequentiel-" + classe.codeClasse + "-" + sequence.codeseq, "Récapitulatif des moyennes de la " + sequence.nomseq);
                etat.recapitulatifMoyenne(recapitulatif_new, classe, stat, nomprof, codesanctions, annee, moyenne);

                etat = new CreerEtat("recapitulatifClasseSequentiel-" + classe.codeClasse + "-" + sequence.codeseq, "Bilan de la " + sequence.nomseq);
                etat.synthese_resultat_sequentiel(classe, cmbExamen.Text, synthese);

                GenererEtatDesSanctionsDuneClasseBL genererEtatDesSanctionsDuneClasseBL = new GenererEtatDesSanctionsDuneClasseBL();
                genererEtatDesSanctionsDuneClasseBL.genererEtatSanctionSequentielleDuneClasse(classe.codeClasse, sequence.codeseq, annee);

                etat = new CreerEtat("statistique-" + classe.codeClasse + "-" + sequence.codeseq, "Statistiques sur la " + sequence.nomseq);
                etat.etatPourcentageCumuleDeNotesParSequence(classe, sequence.codeseq, annee);

                etat = new CreerEtat("ConseilClasse-" + classe.codeClasse + "-" + sequence.codeseq, "Conseil de Classe de la " + sequence.nomseq);
                etat.etatConseilDeClasse(classe, sequence.codeseq, annee);
            }
        }
    }
}