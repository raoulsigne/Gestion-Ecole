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

namespace Ecole.UI
{
    /// <summary>
    /// Interaction logic for StatComparatifResultatClasseUI.xaml
    /// </summary>
    public partial class StatComparatifResultatClasseUI : Window
    {
        int annee;
        string periode;
        GestionStatistiqueBL statistiqueBL;
        List<string> sequences;
        List<string> examens;
        List<KeyValuePair<string, int>> listeSource;
        public static string ANNUEL = "Annuel";
        public static string TRIMESTRIEL = "Trimestriel";
        public static string SEQUENTIEL = "Sequentiel";

        public StatComparatifResultatClasseUI()
        {
            List<string> tampon = new List<string>();
            statistiqueBL = new GestionStatistiqueBL();
            InitializeComponent();

            examens = new List<string>();
            sequences = new List<string>();
            listeSource = new List<KeyValuePair<string, int>>();
            annee = statistiqueBL.anneeEnCours();
            txtAnnee.Text = " / " + annee;
            txtAnneeScolaire.Text = (annee - 1).ToString();

            sequences = statistiqueBL.listerValeurColonneSequence("codeseq");
            foreach (string seq in sequences)
                examens.Add(seq);
            tampon = statistiqueBL.listerValeurColonneTrimestre("codetrimestre");
            foreach (string trim in tampon)
                examens.Add(trim);
            examens.Add(ANNUEL);
            cmbExamen.ItemsSource = examens;
        }

        private void cmdOK_Click(object sender, RoutedEventArgs e)
        {
            if (cmbExamen.Text != null && txtAnneeScolaire.Text != null)
            {
                if (cmbExamen.Text != "" && txtAnneeScolaire.Text != "")
                {
                    //traitement
                    cmdOK.IsEnabled = false;
                    string examen = cmbExamen.Text;
                    if (examen == ANNUEL)
                    {
                        //comparaison annuelle
                        listeSource = statistiqueBL.effectifValidationResultatAnnuelClasse(annee);
                        periode = "ANNUEL";
                    }
                    else
                    {
                        if (!sequences.Contains(examen))
                        {
                            //comparaison trimestrielle
                            listeSource = statistiqueBL.effectifValidationResultatTrimestrielClasse(annee, examen);
                            periode = "Trimestre "+examen;
                        }
                        else
                        {
                            //comparaision sequentielle
                            listeSource = statistiqueBL.effectifValidationResultatSequentielClasse(annee, examen);
                            periode = "Sequence " + examen;
                        }
                    }

                    matieres.DataContext = listeSource;
                    matieresLine.DataContext = listeSource;
                }
                else
                    MessageBox.Show("Remplir tous les champs avant de valider","School brain:Alerte",MessageBoxButton.OK,MessageBoxImage.Information);
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
            if (cmbExamen.Text != null && txtAnneeScolaire.Text != null)
            {
                CreerEtat etat = new CreerEtat("Resultats-classe" + cmbExamen.Text, "Comparaison du travail des élèves d'une classe");
                ClasseBE classe = new ClasseBE();

                etat.exportGraphesToPDF(gridChartColumn, gridChartLine, null, annee, periode, null, listeSource);
            }
        }
    }
}
