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
using Ecole.Utilitaire;
using Ecole.BusinessEntity;
using Ecole.BusinessLogic;
using System.Windows.Controls.DataVisualization.Charting;

namespace Ecole.UI
{
    /// <summary>
    /// Interaction logic for StatVariationNiveauParMatiereUI.xaml
    /// </summary>
    public partial class StatVariationNiveauParMatiereUI : Window
    {
        int annee;
        string periode;
        GestionStatistiqueBL statistiqueBL;
        List<string> classes;
        List<string> sequences, trimestres;
        List<string> periodes;
        List<KeyValuePair<string, int>> listeSource;
        public static string TRIMESTRIEL = "Trimestrielle";
        public static string SEQUENTIEL = "Sequentielle";

        public StatVariationNiveauParMatiereUI()
        {
            trimestres = new List<string>();
            classes = new List<string>();
            statistiqueBL = new GestionStatistiqueBL();
            InitializeComponent();

            periodes = new List<string>();
            periodes.Add(SEQUENTIEL);
            periodes.Add(TRIMESTRIEL);
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
            trimestres = statistiqueBL.listerValeurColonneTrimestre("codetrimestre");
            cmbExamen.ItemsSource = periodes;
        }

        private void cmdOK_Click(object sender, RoutedEventArgs e)
        {
            if (cmbClasse.Text != null && txtAnneeScolaire.Text != null)
            {
                if (cmbClasse.Text != "" && txtAnneeScolaire.Text != "")
                {
                    //traitement
                    cmdOK.IsEnabled = false;
                    listeSource.Clear(); 
                    string codeclasse = cmbClasse.Text;
                    string p = cmbExamen.Text;
                    if (p == SEQUENTIEL)
                    {
                        //comparaison sequentielle
                        foreach (string seq in sequences)
                        {
                            listeSource = new List<KeyValuePair<string, int>>();
                            listeSource = statistiqueBL.effectifValidationMatiereSequentielClasse(codeclasse, annee, seq);
                           
                            LineSeries line = new LineSeries();
                            line.Title = " %Admis " + seq;
                            line.DependentValuePath = "Value";
                            line.IndependentValuePath = "Key";
                            line.ItemsSource = listeSource;
                            ColumnSeries column = new ColumnSeries();
                            column.Title = " %Admis " + seq;
                            column.DependentValuePath = "Value";
                            column.IndependentValuePath = "Key";
                            column.ItemsSource = listeSource;
                            
                            lineChart.Series.Add(line);
                            columnChart.Series.Add(column);
                        }
                    }
                    else
                    {
                        if (p == TRIMESTRIEL)
                        {
                            //comparaison trimestrielle
                            foreach (string trim in trimestres)
                            {
                                listeSource = new List<KeyValuePair<string, int>>();
                                listeSource = statistiqueBL.effectifValidationMatiereTrimestrielClasse(codeclasse, annee, trim);

                                LineSeries line = new LineSeries();
                                line.Title = " %Admis " + trim;
                                line.DependentValuePath = "Value";
                                line.IndependentValuePath = "Key";
                                line.ItemsSource = listeSource;
                                ColumnSeries column = new ColumnSeries();
                                column.Title = " %Admis " + trim;
                                column.DependentValuePath = "Value";
                                column.IndependentValuePath = "Key";
                                column.ItemsSource = listeSource;

                                lineChart.Series.Add(line);
                                columnChart.Series.Add(column);
                                periode = "Séquence " + trim;
                            }
                        }
                    }
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
            lineChart.Series.Clear();
            columnChart.Series.Clear();
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
                CreerEtat etat = new CreerEtat("Variation du niveau par matiere", "Comparaison du travail des élèves par matière");
                ClasseBE classe = new ClasseBE();
                classe.codeClasse = cmbClasse.Text;
                classe = statistiqueBL.rechercherClasse(classe);

                etat.exportGraphesToPDF(gridChartColumn, gridChartLine, classe, annee, cmbExamen.Text, null, listeSource);
            }
        }
    }
}
