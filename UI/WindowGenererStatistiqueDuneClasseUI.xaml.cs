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

using System.Collections.ObjectModel;
using System.Data;

using Ecole.BusinessLogic;
using Ecole.BusinessEntity;
using Ecole.Utilitaire;

namespace Ecole.UI
{
    /// <summary>
    /// Interaction logic for WindowGenererResultatDunEleveUI.xaml
    /// </summary>
    public partial class WindowGenererStatistiqueDuneClasseUI : Window
    {
        StatistiqueClasseBL statistiqueClasseBL;

        string classeChoisi = "";
        string periodeChoisi = "";
        int anneeScolaireChoisi;

        int annee;

        // Définition d'une liste 'ListeMoyennes' observable de 'Moyenne'
        public ObservableCollection<StatistiqueClasseBE> StatistiqueClasse { get; set; }

        public WindowGenererStatistiqueDuneClasseUI()
        {
            InitializeComponent();

            statistiqueClasseBL = new StatistiqueClasseBL();

            // A mettre pour que le binding avec le DataGrid fonctionne !

            //on charge les périodes dans le comboBox
            String[] periode = { "Séquence", "Trimestre", "Année" };
            cmbPeriode.ItemsSource = periode;

            annee = statistiqueClasseBL.getAnneeEnCours();
            txtAnnee.Text = Convert.ToString(annee);
            txtAnneeScolaire.Text = (annee - 1).ToString();

            lblChoixPeriode.Visibility = System.Windows.Visibility.Hidden;
            cmbChoixPeriode.Visibility = System.Windows.Visibility.Hidden;

            List<ClasseBE> LClasse = statistiqueClasseBL.listerToutesLesClasses();
            cmbClasse.ItemsSource = statistiqueClasseBL.getListCodeClasse(LClasse);

        }

        private void cmdFermer_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cmdAnnuler_Click(object sender, RoutedEventArgs e)
        {
            cmbClasse.Text = null;

            cmbPeriode.Text = null;
            cmbChoixPeriode.Text = null;

            annee = statistiqueClasseBL.getAnneeEnCours();
            txtAnnee.Text = Convert.ToString(annee);
            txtAnneeScolaire.Text = (annee - 1).ToString();

            lblChoixPeriode.Content = "";
            lblChoixPeriode.Visibility = System.Windows.Visibility.Hidden;
            cmbChoixPeriode.Visibility = System.Windows.Visibility.Hidden;
        }

        private void cmdValider_Click(object sender, RoutedEventArgs e)
        {
            //on vérifit si tous les champs ont été corectement rempli
            if ((cmbClasse.Text != null && cmbPeriode.Text != null && txtAnneeScolaire.Text != null) &&
                (cmbClasse.Text != "" && cmbPeriode.Text != "" && txtAnneeScolaire.Text != ""))
            {
                classeChoisi = cmbClasse.Text;
                periodeChoisi = cmbChoixPeriode.Text;
                anneeScolaireChoisi = Convert.ToInt16(txtAnneeScolaire.Text);

               if (cmbPeriode.Text.Equals("Séquence"))
                {
                    if (cmbChoixPeriode.Text != null && cmbChoixPeriode.Text != "")
                    {
                        // traitement pour une Séquence

                            //--------------------- Action pour une Séquence particulière
                        List<StatistiqueClasseBE> LStatistique = new List<StatistiqueClasseBE>();

                        if (cmbClasse.Text.Equals("<Toutes Les Classes>"))
                        {
                            List<ClasseBE> ListClasse = statistiqueClasseBL.listerToutesLesClasses();
                            int effectifTotal = 0;
                            int nbAdmisTotal = 0;
                            int nbEchecTotal = 0;
                            for (int i = 0; i < ListClasse.Count; i++) {
                                StatistiqueClasseBE stat = statistiqueClasseBL.getStatistiqueDuneSequence(ListClasse.ElementAt(i).codeClasse, Convert.ToInt16(txtAnnee.Text), cmbChoixPeriode.Text);
                                if (stat != null)
                                {
                                    effectifTotal += stat.effectif;
                                    nbAdmisTotal += stat.nbAdmis;
                                    nbEchecTotal += stat.nbEchec;
                                    LStatistique.Add(stat);
                                }
                            }

                            string pourcentageTotalAdmis = Convert.ToString(Math.Round((Convert.ToDecimal(nbAdmisTotal) / Convert.ToDecimal(effectifTotal)) * 100, 2)) + "%";
                            string pourcentageTotalEchec = Convert.ToString(Math.Round((Convert.ToDecimal(nbEchecTotal) / Convert.ToDecimal(effectifTotal)) * 100, 2)) + "%";

                            StatistiqueClasseBE stat2 = new StatistiqueClasseBE("TOTAL", effectifTotal, nbAdmisTotal, pourcentageTotalAdmis, nbEchecTotal, pourcentageTotalEchec);

                            if (stat2 != null)
                                LStatistique.Add(stat2);

                        }
                        else
                        {
                            //génération des statisques
                            StatistiqueClasseBE stat = statistiqueClasseBL.getStatistiqueDuneSequence(cmbClasse.Text, Convert.ToInt16(txtAnnee.Text), cmbChoixPeriode.Text);
                            if (stat != null)
                                LStatistique.Add(stat);
                        }

                        
                        grdStatistiqueClasse.ItemsSource = LStatistique;
                    }
                    else MessageBox.Show("Vous devez choisir une Séquence !");
                }
                else if (cmbPeriode.Text.Equals("Trimestre"))
                {
                    if (cmbChoixPeriode.Text != null && cmbChoixPeriode.Text != "")
                    {

                        List<StatistiqueClasseBE> LStatistique = new List<StatistiqueClasseBE>();

                        if (cmbClasse.Text.Equals("<Toutes Les Classes>"))
                        {
                            List<ClasseBE> ListClasse = statistiqueClasseBL.listerToutesLesClasses();
                            int effectifTotal = 0;
                            int nbAdmisTotal = 0;
                            int nbEchecTotal = 0;
                            for (int i = 0; i < ListClasse.Count; i++)
                            {
                                StatistiqueClasseBE stat = statistiqueClasseBL.getStatistiqueDuneSequence(ListClasse.ElementAt(i).codeClasse, Convert.ToInt16(txtAnnee.Text), cmbChoixPeriode.Text);
                                if (stat != null)
                                {
                                    effectifTotal += stat.effectif;
                                    nbAdmisTotal += stat.nbAdmis;
                                    nbEchecTotal += stat.nbEchec;
                                    LStatistique.Add(stat);
                                }
                            }

                            string pourcentageTotalAdmis = Convert.ToString(Math.Round((Convert.ToDecimal(nbAdmisTotal) / Convert.ToDecimal(effectifTotal)) * 100, 2)) + "%";
                            string pourcentageTotalEchec = Convert.ToString(Math.Round((Convert.ToDecimal(nbEchecTotal) / Convert.ToDecimal(effectifTotal)) * 100, 2)) + "%";

                            StatistiqueClasseBE stat2 = new StatistiqueClasseBE("TOTAL", effectifTotal, nbAdmisTotal, pourcentageTotalAdmis, nbEchecTotal, pourcentageTotalEchec);

                            if (stat2 != null)
                                LStatistique.Add(stat2);

                        }
                        else
                        {
                            //génération des statisques
                            StatistiqueClasseBE stat = statistiqueClasseBL.getStatistiqueDuneSequence(cmbClasse.Text, Convert.ToInt16(txtAnnee.Text), cmbChoixPeriode.Text);
                            if (stat != null)
                                LStatistique.Add(stat);
                        }


                        grdStatistiqueClasse.ItemsSource = LStatistique;

                           
                    }
                    else MessageBox.Show("Vous devez choisir un Trimestre !");
                }
                else
                {
                    // traitement pour une année

                    List<StatistiqueClasseBE> LStatistique = new List<StatistiqueClasseBE>();

                    if (cmbClasse.Text.Equals("<Toutes Les Classes>"))
                    {
                        List<ClasseBE> ListClasse = statistiqueClasseBL.listerToutesLesClasses();
                        int effectifTotal = 0;
                        int nbAdmisTotal = 0;
                        int nbEchecTotal = 0;
                        for (int i = 0; i < ListClasse.Count; i++)
                        {
                            StatistiqueClasseBE stat = statistiqueClasseBL.getStatistiqueDuneSequence(ListClasse.ElementAt(i).codeClasse, Convert.ToInt16(txtAnnee.Text), cmbChoixPeriode.Text);
                            if (stat != null)
                            {
                                effectifTotal += stat.effectif;
                                nbAdmisTotal += stat.nbAdmis;
                                nbEchecTotal += stat.nbEchec;
                                LStatistique.Add(stat);
                            }
                        }

                        string pourcentageTotalAdmis = Convert.ToString(Math.Round((Convert.ToDecimal(nbAdmisTotal) / Convert.ToDecimal(effectifTotal)) * 100, 2)) + "%";
                        string pourcentageTotalEchec = Convert.ToString(Math.Round((Convert.ToDecimal(nbEchecTotal) / Convert.ToDecimal(effectifTotal)) * 100, 2)) + "%";

                        StatistiqueClasseBE stat2 = new StatistiqueClasseBE("TOTAL", effectifTotal, nbAdmisTotal, pourcentageTotalAdmis, nbEchecTotal, pourcentageTotalEchec);

                        if (stat2 != null)
                            LStatistique.Add(stat2);

                    }
                    else
                    {
                        //génération des statisques
                        StatistiqueClasseBE stat = statistiqueClasseBL.getStatistiqueDuneSequence(cmbClasse.Text, Convert.ToInt16(txtAnnee.Text), cmbChoixPeriode.Text);
                        if (stat != null)
                            LStatistique.Add(stat);
                    }


                    grdStatistiqueClasse.ItemsSource = LStatistique;

                }


            }
            else MessageBox.Show("Tous les champs doivent êtres remplis !! ");
        }

        private void cmbPeriode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbPeriode.SelectedItem != null)
            {
                if (cmbPeriode.SelectedItem.Equals("Séquence"))
                {
                    //on a choisi recalculer le résultat d'un élève pour une séquence
                    List<SequenceBE> LSequence = statistiqueClasseBL.listerToutesLesSequences();
                    // ------------------- Chargement de la liste des codes de séquence dans le comboBox de la fenêtre 
                    cmbChoixPeriode.ItemsSource = statistiqueClasseBL.getListCodeSequence(LSequence);

                    lblChoixPeriode.Content = "Séquence";
                    lblChoixPeriode.Visibility = System.Windows.Visibility.Visible;
                    cmbChoixPeriode.Visibility = System.Windows.Visibility.Visible;

                }
                else if (cmbPeriode.SelectedItem.Equals("Trimestre"))
                {
                    //on a choisi recalculer le résultat d'un élève pour un Trimestre
                    List<TrimestreBE> LTrimestre = statistiqueClasseBL.listerTousLesTrimestres();
                    // ------------------- Chargement de la liste des codes de Trimestre dans le comboBox de la fenêtre 
                    cmbChoixPeriode.ItemsSource = statistiqueClasseBL.getListCodeTrimestre(LTrimestre);

                    lblChoixPeriode.Content = "Trimestre";
                    lblChoixPeriode.Visibility = System.Windows.Visibility.Visible;
                    cmbChoixPeriode.Visibility = System.Windows.Visibility.Visible;
                }
                else if (cmbPeriode.SelectedItem.Equals("Année"))
                {
                    lblChoixPeriode.Visibility = System.Windows.Visibility.Hidden;
                    cmbChoixPeriode.Visibility = System.Windows.Visibility.Hidden;
                }
            }
        }

        private void txtAnneeScolaire_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                annee = Convert.ToInt32(txtAnneeScolaire.Text) + 1;
                txtAnnee.Text = annee.ToString();

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
            if (grdStatistiqueClasse != null)
            {
                string codeclasse = cmbClasse.Text;
                if(cmbClasse.Text == "<Toutes Les Classes>")
                    codeclasse = "toutes les Classes";
                CreerEtat etat = new CreerEtat("Statistique Classe -" + codeclasse, " Statistiques \n Classe : " + classeChoisi + " \n Période : " + periodeChoisi + " \n Année Scolaire : " + anneeScolaireChoisi + "/" + (anneeScolaireChoisi + 1));
                etat.obtenirEtat(grdStatistiqueClasse);
            }
        }

    }
}
