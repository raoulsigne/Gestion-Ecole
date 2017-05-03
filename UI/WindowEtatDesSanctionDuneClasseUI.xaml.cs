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

namespace Ecole.UI
{
    /// <summary>
    /// Interaction logic for WindowEtatDesSanctionDuneClasseUI.xaml
    /// </summary>
    public partial class WindowEtatDesSanctionDuneClasseUI : Window
    {
        GenererEtatDesSanctionsDuneClasseBL genererEtatDesSanctionsDuneClasseBL;

        int annee;

        //********************************** DEBUT des éléments utiles pour la barre de progréssion

        //Create a Delegate that matches 
        //the Signature of the ProgressBar's SetValue method
        private delegate void UpdateProgressBarDelegate(
                System.Windows.DependencyProperty dp, Object value);

        //********************************** Fin des éléments utiles pour la barre de progréssion

        public WindowEtatDesSanctionDuneClasseUI()
        {

            InitializeComponent();

            genererEtatDesSanctionsDuneClasseBL = new GenererEtatDesSanctionsDuneClasseBL();

            //on charge les périodes dans le comboBox
            String[] periode = { "Séquence", "Trimestre", "Année" };
            cmbPeriode.ItemsSource = periode;

            List<ClasseBE> LClasse = genererEtatDesSanctionsDuneClasseBL.listerToutesLesClasses();
            cmbClasse.ItemsSource = genererEtatDesSanctionsDuneClasseBL.getListCodeClasse2(LClasse);

            annee = genererEtatDesSanctionsDuneClasseBL.getAnneeEnCours();
            txtAnnee.Text = Convert.ToString(annee);
            txtAnneeScolaire.Text = (annee - 1).ToString();
            

            lblChoixPeriode.Visibility = System.Windows.Visibility.Hidden;
            cmbChoixPeriode.Visibility = System.Windows.Visibility.Hidden;

            //on cache la barre de progression
            ProgressBar1.Visibility = System.Windows.Visibility.Hidden;

        }

        private void cmdValider_Click(object sender, RoutedEventArgs e)
        {
            //on vérifit si tous les champs ont été corectement rempli
            if ((cmbClasse.Text != null && cmbPeriode.Text != null && txtAnneeScolaire.Text != null) &&
                (cmbClasse.Text != "" && cmbPeriode.Text != "" && txtAnneeScolaire.Text != ""))
            {

                //la classe choisi
                String codeClasse = cmbClasse.Text;

                if (cmbClasse.Text.Equals("<Toutes Les Classes>"))
                {
                    //on liste toutes les Classes 
                    List<ClasseBE> LClasse = genererEtatDesSanctionsDuneClasseBL.listerToutesLesClasses();
                    if (LClasse != null && LClasse.Count != 0)
                    {

                        if (cmbPeriode.Text.Equals("Séquence"))
                        {
                            if (cmbChoixPeriode.Text != null && cmbChoixPeriode.Text != "")
                            {
                                // traitement pour une Séquence

                                //si on a choisi <Toutes les Séquences>
                                if (cmbChoixPeriode.Text.Equals("<Toutes Les Séquences>"))
                                {

                                    //--------------------------- Action pour toutes les séquences d'une matière particulière

                                    //on liste toutes les Séquences
                                    List<SequenceBE> LSequence = genererEtatDesSanctionsDuneClasseBL.listerToutesLesSequences();

                                    //Configure the ProgressBar
                                    ProgressBar1.Minimum = 0;
                                    ProgressBar1.Maximum = LSequence.Count;
                                    ProgressBar1.Value = 0;

                                    //Stores the value of the ProgressBar
                                    double value = 0;

                                    //Create a new instance of our ProgressBar Delegate that points
                                    // to the ProgressBar's SetValue method.
                                    UpdateProgressBarDelegate updatePbDelegate =
                                        new UpdateProgressBarDelegate(ProgressBar1.SetValue);


                                    //on affiche la barre de progression
                                    ProgressBar1.Visibility = System.Windows.Visibility.Visible;

                                    for (int j = 0; j < LClasse.Count; j++)
                                    {
                                        for (int i = 0; i < LSequence.Count; i++)
                                        {
                                            //génération de l'état des sanctions séquentielles
                                            genererEtatDesSanctionsDuneClasseBL.genererEtatSanctionSequentielleDuneClasse(LClasse.ElementAt(j).codeClasse, LSequence.ElementAt(i).codeseq, Convert.ToInt16(txtAnnee.Text));

                                            value += 1;

                                            Dispatcher.Invoke(updatePbDelegate,
                                                System.Windows.Threading.DispatcherPriority.Background,
                                                new object[] { ProgressBar.ValueProperty, value });

                                        }
                                    }

                                    MessageBox.Show("Opération Terminée !! ");

                                    //on cache la barre de progression
                                    ProgressBar1.Visibility = System.Windows.Visibility.Hidden;

                                    //txtMatricule.Text = "";
                                    //cmbPeriode.Text = null;
                                    //cmbChoixPeriode.Text = null;
                                    //txtAnnee.Text = Convert.ToString(genererResultatsDunEleveBL.getAnneeEnCours());

                                    //lblInfoEleve.Content = "";

                                    //etat = 0;

                                    ////lblChoixPeriode.Content = "";
                                    ////lblChoixPeriode.Visibility = System.Windows.Visibility.Hidden;
                                    ////cmbChoixPeriode.Visibility = System.Windows.Visibility.Hidden;

                                }
                                else
                                {

                                    //--------------------- Action pour une Séquence particulière

                                    //Configure the ProgressBar
                                    ProgressBar1.Minimum = 0;
                                    ProgressBar1.Maximum = 1;
                                    ProgressBar1.Value = 0;

                                    //Stores the value of the ProgressBar
                                    double value = 0;

                                    //Create a new instance of our ProgressBar Delegate that points
                                    // to the ProgressBar's SetValue method.
                                    UpdateProgressBarDelegate updatePbDelegate =
                                        new UpdateProgressBarDelegate(ProgressBar1.SetValue);


                                    //on affiche la barre de progression
                                    ProgressBar1.Visibility = System.Windows.Visibility.Visible;

                                    for (int j = 0; j < LClasse.Count; j++)
                                    {
                                        //génération de l'état des sanctions séquentielles
                                        genererEtatDesSanctionsDuneClasseBL.genererEtatSanctionSequentielleDuneClasse(LClasse.ElementAt(j).codeClasse, cmbChoixPeriode.Text, Convert.ToInt16(txtAnnee.Text));

                                        value += 1;


                                        Dispatcher.Invoke(updatePbDelegate,
                                            System.Windows.Threading.DispatcherPriority.Background,
                                            new object[] { ProgressBar.ValueProperty, value });

                                    }

                                    MessageBox.Show("Opération Terminée !! ");

                                    //on cache la barre de progression
                                    ProgressBar1.Visibility = System.Windows.Visibility.Hidden;

                                    //txtMatricule.Text = "";
                                    //cmbPeriode.Text = null;
                                    //cmbChoixPeriode.Text = null;
                                    //txtAnnee.Text = Convert.ToString(genererResultatsDunEleveBL.getAnneeEnCours());

                                    //lblInfoEleve.Content = "";

                                    //etat = 0;

                                    ////lblChoixPeriode.Content = "";
                                    ////lblChoixPeriode.Visibility = System.Windows.Visibility.Hidden;
                                    ////cmbChoixPeriode.Visibility = System.Windows.Visibility.Hidden;
                                }

                            }
                            else MessageBox.Show("Vous devez choisir une Séquence !");
                        }
                        else if (cmbPeriode.Text.Equals("Trimestre"))
                        {
                            if (cmbChoixPeriode.Text != null && cmbChoixPeriode.Text != "")
                            {
                                // traitement pour un Trimestre
                                //si on a choisi <Toutes les Séquences>
                                if (cmbChoixPeriode.Text.Equals("<Tous Les Trimestres>"))
                                {

                                    //--------------------------- Action pour toutes les Trimestres d'une matière particulière

                                    //on liste toutes les Trimestres
                                    List<TrimestreBE> LTrimestre = genererEtatDesSanctionsDuneClasseBL.listerTousLesTrimestres();

                                    //Configure the ProgressBar
                                    ProgressBar1.Minimum = 0;
                                    ProgressBar1.Maximum = LTrimestre.Count;
                                    ProgressBar1.Value = 0;

                                    //Stores the value of the ProgressBar
                                    double value = 0;

                                    //Create a new instance of our ProgressBar Delegate that points
                                    // to the ProgressBar's SetValue method.
                                    UpdateProgressBarDelegate updatePbDelegate =
                                        new UpdateProgressBarDelegate(ProgressBar1.SetValue);


                                    //on affiche la barre de progression
                                    ProgressBar1.Visibility = System.Windows.Visibility.Visible;

                                    for (int j = 0; j < LClasse.Count; j++)
                                    {
                                        for (int i = 0; i < LTrimestre.Count; i++)
                                        {
                                            //génération de l'état des sanctions
                                            genererEtatDesSanctionsDuneClasseBL.genererEtatSanctionTrimestrielleDuneClasse(LClasse.ElementAt(j).codeClasse, LTrimestre.ElementAt(i).codetrimestre, Convert.ToInt16(txtAnnee.Text));

                                            value += 1;

                                            Dispatcher.Invoke(updatePbDelegate,
                                                System.Windows.Threading.DispatcherPriority.Background,
                                                new object[] { ProgressBar.ValueProperty, value });

                                        }
                                    }

                                    MessageBox.Show("Opération Terminée !! ");

                                    //on cache la barre de progression
                                    ProgressBar1.Visibility = System.Windows.Visibility.Hidden;

                                    //txtMatricule.Text = "";
                                    //cmbPeriode.Text = null;
                                    //cmbChoixPeriode.Text = null;
                                    //txtAnnee.Text = Convert.ToString(genererResultatsDunEleveBL.getAnneeEnCours());

                                    //lblInfoEleve.Content = "";

                                    //etat = 0;

                                    ////lblChoixPeriode.Content = "";
                                    ////lblChoixPeriode.Visibility = System.Windows.Visibility.Hidden;
                                    ////cmbChoixPeriode.Visibility = System.Windows.Visibility.Hidden;

                                }
                                else
                                {

                                    //--------------------- Action pour un trimestre particulière

                                    //Configure the ProgressBar
                                    ProgressBar1.Minimum = 0;
                                    ProgressBar1.Maximum = 1;
                                    ProgressBar1.Value = 0;

                                    //Stores the value of the ProgressBar
                                    double value = 0;

                                    //Create a new instance of our ProgressBar Delegate that points
                                    // to the ProgressBar's SetValue method.
                                    UpdateProgressBarDelegate updatePbDelegate =
                                        new UpdateProgressBarDelegate(ProgressBar1.SetValue);


                                    //on affiche la barre de progression
                                    ProgressBar1.Visibility = System.Windows.Visibility.Visible;

                                    for (int j = 0; j < LClasse.Count; j++)
                                    {
                                        //génération de l'état des sanctions
                                        genererEtatDesSanctionsDuneClasseBL.genererEtatSanctionTrimestrielleDuneClasse(LClasse.ElementAt(j).codeClasse, cmbChoixPeriode.Text, Convert.ToInt16(txtAnnee.Text));

                                        value += 1;

                                        Dispatcher.Invoke(updatePbDelegate,
                                            System.Windows.Threading.DispatcherPriority.Background,
                                            new object[] { ProgressBar.ValueProperty, value });
                                    }

                                    MessageBox.Show("Opération Terminée !!");

                                    //on cache la barre de progression
                                    ProgressBar1.Visibility = System.Windows.Visibility.Hidden;

                                    //txtMatricule.Text = "";
                                    //cmbPeriode.Text = null;
                                    //cmbChoixPeriode.Text = null;
                                    //txtAnnee.Text = Convert.ToString(genererResultatsDunEleveBL.getAnneeEnCours());

                                    //lblInfoEleve.Content = "";

                                    //etat = 0;

                                    ////lblChoixPeriode.Content = "";
                                    ////lblChoixPeriode.Visibility = System.Windows.Visibility.Hidden;
                                    ////cmbChoixPeriode.Visibility = System.Windows.Visibility.Hidden;
                                }
                            }
                            else MessageBox.Show("Vous devez choisir un Trimestre !");
                        }
                        else
                        {
                            // traitement pour une année

                            //--------------------- Action pour une Séquence particulière

                            //Configure the ProgressBar
                            ProgressBar1.Minimum = 0;
                            ProgressBar1.Maximum = 1;
                            ProgressBar1.Value = 0;

                            //Stores the value of the ProgressBar
                            double value = 0;

                            //Create a new instance of our ProgressBar Delegate that points
                            // to the ProgressBar's SetValue method.
                            UpdateProgressBarDelegate updatePbDelegate =
                                new UpdateProgressBarDelegate(ProgressBar1.SetValue);


                            //on affiche la barre de progression
                            ProgressBar1.Visibility = System.Windows.Visibility.Visible;

                            for (int j = 0; j < LClasse.Count; j++)
                            {
                                //génération de l'état des sanctions
                                genererEtatDesSanctionsDuneClasseBL.genererEtatSanctionAnnuelleDuneClasse(LClasse.ElementAt(j).codeClasse, Convert.ToInt16(txtAnnee.Text));

                                value += 1;

                                Dispatcher.Invoke(updatePbDelegate,
                                    System.Windows.Threading.DispatcherPriority.Background,
                                    new object[] { ProgressBar.ValueProperty, value });
                            }

                            MessageBox.Show("Opération Terminée !!");

                            //on cache la barre de progression
                            ProgressBar1.Visibility = System.Windows.Visibility.Hidden;

                            //txtMatricule.Text = "";
                            //cmbPeriode.Text = null;
                            //cmbChoixPeriode.Text = null;
                            //txtAnnee.Text = Convert.ToString(genererResultatsDunEleveBL.getAnneeEnCours());

                            //lblInfoEleve.Content = "";

                            //etat = 0;

                            ////lblChoixPeriode.Content = "";
                            ////lblChoixPeriode.Visibility = System.Windows.Visibility.Hidden;
                            ////cmbChoixPeriode.Visibility = System.Windows.Visibility.Hidden;
                        }

                       
                    }
                }
                else { //cas où on a choisi une classe particulière
                    if (cmbPeriode.Text.Equals("Séquence"))
                    {
                        if (cmbChoixPeriode.Text != null && cmbChoixPeriode.Text != "")
                        {
                            // traitement pour une Séquence

                            //si on a choisi <Toutes les Séquences>
                            if (cmbChoixPeriode.Text.Equals("<Toutes Les Séquences>"))
                            {

                                //--------------------------- Action pour toutes les séquences d'une matière particulière

                                //on liste toutes les Séquences
                                List<SequenceBE> LSequence = genererEtatDesSanctionsDuneClasseBL.listerToutesLesSequences();

                                //Configure the ProgressBar
                                ProgressBar1.Minimum = 0;
                                ProgressBar1.Maximum = LSequence.Count;
                                ProgressBar1.Value = 0;

                                //Stores the value of the ProgressBar
                                double value = 0;

                                //Create a new instance of our ProgressBar Delegate that points
                                // to the ProgressBar's SetValue method.
                                UpdateProgressBarDelegate updatePbDelegate =
                                    new UpdateProgressBarDelegate(ProgressBar1.SetValue);


                                //on affiche la barre de progression
                                ProgressBar1.Visibility = System.Windows.Visibility.Visible;

                                for (int i = 0; i < LSequence.Count; i++)
                                {
                                    //génération de l'état des sanctions séquentielles
                                    genererEtatDesSanctionsDuneClasseBL.genererEtatSanctionSequentielleDuneClasse(codeClasse, LSequence.ElementAt(i).codeseq, Convert.ToInt16(txtAnnee.Text));

                                    value += 1;

                                    Dispatcher.Invoke(updatePbDelegate,
                                        System.Windows.Threading.DispatcherPriority.Background,
                                        new object[] { ProgressBar.ValueProperty, value });

                                }

                                MessageBox.Show("Opération Terminée !! ");

                                //on cache la barre de progression
                                ProgressBar1.Visibility = System.Windows.Visibility.Hidden;

                                //txtMatricule.Text = "";
                                //cmbPeriode.Text = null;
                                //cmbChoixPeriode.Text = null;
                                //txtAnnee.Text = Convert.ToString(genererResultatsDunEleveBL.getAnneeEnCours());

                                //lblInfoEleve.Content = "";

                                //etat = 0;

                                ////lblChoixPeriode.Content = "";
                                ////lblChoixPeriode.Visibility = System.Windows.Visibility.Hidden;
                                ////cmbChoixPeriode.Visibility = System.Windows.Visibility.Hidden;

                            }
                            else
                            {

                                //--------------------- Action pour une Séquence particulière

                                //Configure the ProgressBar
                                ProgressBar1.Minimum = 0;
                                ProgressBar1.Maximum = 1;
                                ProgressBar1.Value = 0;

                                //Stores the value of the ProgressBar
                                double value = 0;

                                //Create a new instance of our ProgressBar Delegate that points
                                // to the ProgressBar's SetValue method.
                                UpdateProgressBarDelegate updatePbDelegate =
                                    new UpdateProgressBarDelegate(ProgressBar1.SetValue);


                                //on affiche la barre de progression
                                ProgressBar1.Visibility = System.Windows.Visibility.Visible;

                                //génération de l'état des sanctions séquentielles
                                genererEtatDesSanctionsDuneClasseBL.genererEtatSanctionSequentielleDuneClasse(codeClasse, cmbChoixPeriode.Text, Convert.ToInt16(txtAnnee.Text));

                                value += 1;

                                Dispatcher.Invoke(updatePbDelegate,
                                    System.Windows.Threading.DispatcherPriority.Background,
                                    new object[] { ProgressBar.ValueProperty, value });


                                MessageBox.Show("Opération Terminée !! ");

                                //on cache la barre de progression
                                ProgressBar1.Visibility = System.Windows.Visibility.Hidden;

                                //txtMatricule.Text = "";
                                //cmbPeriode.Text = null;
                                //cmbChoixPeriode.Text = null;
                                //txtAnnee.Text = Convert.ToString(genererResultatsDunEleveBL.getAnneeEnCours());

                                //lblInfoEleve.Content = "";

                                //etat = 0;

                                ////lblChoixPeriode.Content = "";
                                ////lblChoixPeriode.Visibility = System.Windows.Visibility.Hidden;
                                ////cmbChoixPeriode.Visibility = System.Windows.Visibility.Hidden;
                            }

                        }
                        else MessageBox.Show("Vous devez choisir une Séquence !");
                    }
                    else if (cmbPeriode.Text.Equals("Trimestre"))
                    {
                        if (cmbChoixPeriode.Text != null && cmbChoixPeriode.Text != "")
                        {
                            // traitement pour un Trimestre
                            //si on a choisi <Toutes les Séquences>
                            if (cmbChoixPeriode.Text.Equals("<Tous Les Trimestres>"))
                            {

                                //--------------------------- Action pour toutes les Trimestres d'une matière particulière

                                //on liste toutes les Trimestres
                                List<TrimestreBE> LTrimestre = genererEtatDesSanctionsDuneClasseBL.listerTousLesTrimestres();

                                //Configure the ProgressBar
                                ProgressBar1.Minimum = 0;
                                ProgressBar1.Maximum = LTrimestre.Count;
                                ProgressBar1.Value = 0;

                                //Stores the value of the ProgressBar
                                double value = 0;

                                //Create a new instance of our ProgressBar Delegate that points
                                // to the ProgressBar's SetValue method.
                                UpdateProgressBarDelegate updatePbDelegate =
                                    new UpdateProgressBarDelegate(ProgressBar1.SetValue);


                                //on affiche la barre de progression
                                ProgressBar1.Visibility = System.Windows.Visibility.Visible;

                                for (int i = 0; i < LTrimestre.Count; i++)
                                {
                                    //génération de l'état des sanctions
                                    genererEtatDesSanctionsDuneClasseBL.genererEtatSanctionTrimestrielleDuneClasse(codeClasse, LTrimestre.ElementAt(i).codetrimestre, Convert.ToInt16(txtAnnee.Text));

                                    value += 1;

                                    Dispatcher.Invoke(updatePbDelegate,
                                        System.Windows.Threading.DispatcherPriority.Background,
                                        new object[] { ProgressBar.ValueProperty, value });

                                }

                                MessageBox.Show("Opération Terminée !! ");

                                //on cache la barre de progression
                                ProgressBar1.Visibility = System.Windows.Visibility.Hidden;

                                //txtMatricule.Text = "";
                                //cmbPeriode.Text = null;
                                //cmbChoixPeriode.Text = null;
                                //txtAnnee.Text = Convert.ToString(genererResultatsDunEleveBL.getAnneeEnCours());

                                //lblInfoEleve.Content = "";

                                //etat = 0;

                                ////lblChoixPeriode.Content = "";
                                ////lblChoixPeriode.Visibility = System.Windows.Visibility.Hidden;
                                ////cmbChoixPeriode.Visibility = System.Windows.Visibility.Hidden;

                            }
                            else
                            {

                                //--------------------- Action pour un trimestre particulière

                                //Configure the ProgressBar
                                ProgressBar1.Minimum = 0;
                                ProgressBar1.Maximum = 1;
                                ProgressBar1.Value = 0;

                                //Stores the value of the ProgressBar
                                double value = 0;

                                //Create a new instance of our ProgressBar Delegate that points
                                // to the ProgressBar's SetValue method.
                                UpdateProgressBarDelegate updatePbDelegate =
                                    new UpdateProgressBarDelegate(ProgressBar1.SetValue);


                                //on affiche la barre de progression
                                ProgressBar1.Visibility = System.Windows.Visibility.Visible;

                                //génération de l'état des sanctions
                                genererEtatDesSanctionsDuneClasseBL.genererEtatSanctionTrimestrielleDuneClasse(codeClasse, cmbChoixPeriode.Text, Convert.ToInt16(txtAnnee.Text));

                                value += 1;

                                Dispatcher.Invoke(updatePbDelegate,
                                    System.Windows.Threading.DispatcherPriority.Background,
                                    new object[] { ProgressBar.ValueProperty, value });

                                MessageBox.Show("Opération Terminée !!");

                                //on cache la barre de progression
                                ProgressBar1.Visibility = System.Windows.Visibility.Hidden;

                                //txtMatricule.Text = "";
                                //cmbPeriode.Text = null;
                                //cmbChoixPeriode.Text = null;
                                //txtAnnee.Text = Convert.ToString(genererResultatsDunEleveBL.getAnneeEnCours());

                                //lblInfoEleve.Content = "";

                                //etat = 0;

                                ////lblChoixPeriode.Content = "";
                                ////lblChoixPeriode.Visibility = System.Windows.Visibility.Hidden;
                                ////cmbChoixPeriode.Visibility = System.Windows.Visibility.Hidden;
                            }
                        }
                        else MessageBox.Show("Vous devez choisir un Trimestre !");
                    }
                    else
                    {
                        // traitement pour une année

                        //--------------------- Action pour une Séquence particulière

                        //Configure the ProgressBar
                        ProgressBar1.Minimum = 0;
                        ProgressBar1.Maximum = 1;
                        ProgressBar1.Value = 0;

                        //Stores the value of the ProgressBar
                        double value = 0;

                        //Create a new instance of our ProgressBar Delegate that points
                        // to the ProgressBar's SetValue method.
                        UpdateProgressBarDelegate updatePbDelegate =
                            new UpdateProgressBarDelegate(ProgressBar1.SetValue);


                        //on affiche la barre de progression
                        ProgressBar1.Visibility = System.Windows.Visibility.Visible;

                        //génération de l'état des sanctions
                        genererEtatDesSanctionsDuneClasseBL.genererEtatSanctionAnnuelleDuneClasse(codeClasse, Convert.ToInt16(txtAnnee.Text));

                        value += 1;

                        Dispatcher.Invoke(updatePbDelegate,
                            System.Windows.Threading.DispatcherPriority.Background,
                            new object[] { ProgressBar.ValueProperty, value });

                        MessageBox.Show("Opération Terminée !!");

                        //on cache la barre de progression
                        ProgressBar1.Visibility = System.Windows.Visibility.Hidden;

                        //txtMatricule.Text = "";
                        //cmbPeriode.Text = null;
                        //cmbChoixPeriode.Text = null;
                        //txtAnnee.Text = Convert.ToString(genererResultatsDunEleveBL.getAnneeEnCours());

                        //lblInfoEleve.Content = "";

                        //etat = 0;

                        ////lblChoixPeriode.Content = "";
                        ////lblChoixPeriode.Visibility = System.Windows.Visibility.Hidden;
                        ////cmbChoixPeriode.Visibility = System.Windows.Visibility.Hidden;
                    }
                }

            }
            else MessageBox.Show("Tous les champs doivent êtres remplis !! ");
        }

        private void cmdAnnuler_Click(object sender, RoutedEventArgs e)
        {
            cmbClasse.Text = null;
            cmbPeriode.Text = null;
            cmbChoixPeriode.Text = null;
           
            annee = genererEtatDesSanctionsDuneClasseBL.getAnneeEnCours();
            txtAnnee.Text = Convert.ToString(annee);
            txtAnneeScolaire.Text = (annee - 1).ToString();

            lblChoixPeriode.Content = "";
            lblChoixPeriode.Visibility = System.Windows.Visibility.Hidden;
            cmbChoixPeriode.Visibility = System.Windows.Visibility.Hidden;
        }

        private void cmbPeriode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbPeriode.SelectedItem != null)
            {
                if (cmbPeriode.SelectedItem.Equals("Séquence"))
                {
                    //on a choisi recalculer le résultat d'un élève pour une séquence
                    List<SequenceBE> LSequence = genererEtatDesSanctionsDuneClasseBL.listerToutesLesSequences();
                    // ------------------- Chargement de la liste des codes de séquence dans le comboBox de la fenêtre 
                    cmbChoixPeriode.ItemsSource = genererEtatDesSanctionsDuneClasseBL.getListCodeSequence2(LSequence);

                    lblChoixPeriode.Content = "Séquence";
                    lblChoixPeriode.Visibility = System.Windows.Visibility.Visible;
                    cmbChoixPeriode.Visibility = System.Windows.Visibility.Visible;

                }
                else if (cmbPeriode.SelectedItem.Equals("Trimestre"))
                {
                    //on a choisi recalculer le résultat d'un élève pour un Trimestre
                    List<TrimestreBE> LTrimestre = genererEtatDesSanctionsDuneClasseBL.listerTousLesTrimestres();
                    // ------------------- Chargement de la liste des codes de Trimestre dans le comboBox de la fenêtre 
                    cmbChoixPeriode.ItemsSource = genererEtatDesSanctionsDuneClasseBL.getListCodeTrimestre2(LTrimestre);

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

        private void cmdFermer_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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

    }
}
