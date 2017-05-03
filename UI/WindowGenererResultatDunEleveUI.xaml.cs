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
    /// Interaction logic for WindowGenererResultatDunEleveUI.xaml
    /// </summary>
    public partial class WindowGenererResultatDunEleveUI : Window
    {
        GenererResultatsDunEleveBL genererResultatsDunEleveBL;

        private MoyennesBE ancienObjet; //garde l'ancien état de l'objet, qui sera utilisé pour la modification

        int annee;

        // Définition d'une liste 'ListeMoyennes' observable de 'Moyenne'
        public ObservableCollection<MoyennesBE> ListeMoyennes { get; set; }

        //********************************** DEBUT des éléments utiles pour la barre de progréssion

        //Create a Delegate that matches 
        //the Signature of the ProgressBar's SetValue method
        private delegate void UpdateProgressBarDelegate(
                System.Windows.DependencyProperty dp, Object value);

        //********************************** Fin des éléments utiles pour la barre de progréssion

        public WindowGenererResultatDunEleveUI()
        {
            InitializeComponent();

            genererResultatsDunEleveBL = new GenererResultatsDunEleveBL();

            ancienObjet = new MoyennesBE();

            // A mettre pour que le binding avec le DataGrid fonctionne !

            //on charge les périodes dans le comboBox
            String[] periode = {"Séquence", "Trimestre", "Année"};
            cmbPeriode.ItemsSource = periode;

            annee = genererResultatsDunEleveBL.getAnneeEnCours();
            txtAnnee.Text = Convert.ToString(annee);
            txtAnneeScolaire.Text = (annee - 1).ToString();

            lblChoixPeriode.Visibility = System.Windows.Visibility.Hidden;
            cmbChoixPeriode.Visibility = System.Windows.Visibility.Hidden;

            //on cache la barre de progression
            ProgressBar1.Visibility = System.Windows.Visibility.Hidden;
        }

        private void cmdFermer_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cmdAnnuler_Click(object sender, RoutedEventArgs e)
        {
            txtMatricule.Text = Convert.ToString(genererResultatsDunEleveBL.getAnneeEnCours());

            cmbPeriode.Text = null;
            cmbChoixPeriode.Text = null;

            annee = genererResultatsDunEleveBL.getAnneeEnCours();
            txtAnnee.Text = Convert.ToString(annee);
            txtAnneeScolaire.Text = (annee - 1).ToString();

            lblInfoEleve.Content = "";


            lblChoixPeriode.Content = "";
            lblChoixPeriode.Visibility = System.Windows.Visibility.Hidden;
            cmbChoixPeriode.Visibility = System.Windows.Visibility.Hidden;
        }

        private void txtMatricule_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (txtAnnee.Text != null && txtAnnee.Text != "")
                {
                    EleveBE eleve = new EleveBE();
                    eleve.matricule = txtMatricule.Text;

                    EleveBE eleve2 = genererResultatsDunEleveBL.rechercherEleve(eleve);
                    
                    if (eleve2 != null)
                    {
                        //la classe de l'élève
                        String codeClasse = genererResultatsDunEleveBL.getClasseEleve(eleve2.matricule, Convert.ToInt16(txtAnnee.Text));
                    
                        lblInfoEleve.Content = "[ Nom : " + eleve2.nom + ", Sexe : " + eleve2.sexe + ", Classe : " + codeClasse + "]";
                        

                        //*************** ensuite on charge dans le comboBox la liste des matières de la classe pour cette année


                        ClasseBE classe = new ClasseBE();
                        classe.codeClasse = codeClasse;
                        classe = genererResultatsDunEleveBL.rechercherClasse(classe);

                        List<MatiereBE> LMatiereBE = genererResultatsDunEleveBL.ListeMatiereDuneClasse(classe, Convert.ToInt16(txtAnnee.Text));

                    }
                    else
                    {
                        lblInfoEleve.Content = "";

                    }
                }
            }


        }

        private void cmdValider_Click(object sender, RoutedEventArgs e)
        {
            //on vérifit si tous les champs ont été corectement rempli
            if ((txtMatricule.Text != null && cmbPeriode.Text != null && txtAnneeScolaire.Text != null) &&
                (txtMatricule.Text != "" && cmbPeriode.Text != "" && txtAnneeScolaire.Text != ""))
            {

                //la classe de l'élève
                String codeClasse = genererResultatsDunEleveBL.getClasseEleve(txtMatricule.Text, Convert.ToInt16(txtAnnee.Text));


                if (cmbPeriode.Text.Equals("Séquence")) {
                    if (cmbChoixPeriode.Text != null && cmbChoixPeriode.Text != "") { 
                        // traitement pour une Séquence

                        //si on a choisi <Toutes les Séquences>
                        if (cmbChoixPeriode.Text.Equals("<Toutes Les Séquences>"))
                        {

                            //--------------------------- Action pour toutes les séquences d'une matière particulière

                            //on liste toutes les Séquences
                            List<SequenceBE> LSequence = genererResultatsDunEleveBL.listerToutesLesSequences();

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
                                //remplissage de la table "moyennes"
                                genererResultatsDunEleveBL.calculerMoyenneSequentielleDunEleve(txtMatricule.Text, codeClasse, LSequence.ElementAt(i).codeseq, Convert.ToInt16(txtAnnee.Text));

                                //calcul des résultats
                                //remplissage de la table "resultats"
                                genererResultatsDunEleveBL.calculerResultatsSequentielDunEleve(txtMatricule.Text, codeClasse, LSequence.ElementAt(i).codeseq, Convert.ToInt16(txtAnnee.Text));

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

                            //remplissage de la table "moyennes"
                            genererResultatsDunEleveBL.calculerMoyenneSequentielleDunEleve(txtMatricule.Text, codeClasse, cmbChoixPeriode.Text, Convert.ToInt16(txtAnnee.Text));

                            value += 1;

                            Dispatcher.Invoke(updatePbDelegate,
                                System.Windows.Threading.DispatcherPriority.Background,
                                new object[] { ProgressBar.ValueProperty, value });

                            //calcul des résultats
                            //remplissage de la table "resultats"
                            genererResultatsDunEleveBL.calculerResultatsSequentielDunEleve(txtMatricule.Text, codeClasse, cmbChoixPeriode.Text, Convert.ToInt16(txtAnnee.Text));

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
                            List<TrimestreBE> LTrimestre = genererResultatsDunEleveBL.listerTousLesTrimestres();

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
                                //remplissage de la table "moyennesTrimestrielles"
                                genererResultatsDunEleveBL.calculerMoyenneTrimestrielleDunEleve(txtMatricule.Text, codeClasse, LTrimestre.ElementAt(i).codetrimestre, Convert.ToInt16(txtAnnee.Text));

                                //calcul des résultats
                                //remplissage de la table "resultatsTrimestriel"
                                genererResultatsDunEleveBL.calculerResultatsTrimestrielDunEleve(txtMatricule.Text, codeClasse, LTrimestre.ElementAt(i).codetrimestre, Convert.ToInt16(txtAnnee.Text));

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

                            //remplissage de la table "moyennesTrimestrielles"
                            genererResultatsDunEleveBL.calculerMoyenneTrimestrielleDunEleve(txtMatricule.Text, codeClasse, cmbChoixPeriode.Text, Convert.ToInt16(txtAnnee.Text));

                            value += 1;

                            Dispatcher.Invoke(updatePbDelegate,
                                System.Windows.Threading.DispatcherPriority.Background,
                                new object[] { ProgressBar.ValueProperty, value });

                            //calcul des résultats
                            //remplissage de la table "resultatsTrimestriel"
                            genererResultatsDunEleveBL.calculerResultatsTrimestrielDunEleve(txtMatricule.Text, codeClasse, cmbChoixPeriode.Text, Convert.ToInt16(txtAnnee.Text));

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
                else { 
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

                    //remplissage de la table "moyennesAnnuelles"
                    genererResultatsDunEleveBL.calculerMoyenneAnnuelleDunEleve(txtMatricule.Text, codeClasse, Convert.ToInt16(txtAnnee.Text));

                    value += 1;

                    Dispatcher.Invoke(updatePbDelegate,
                        System.Windows.Threading.DispatcherPriority.Background,
                        new object[] { ProgressBar.ValueProperty, value });

                    //calcul des résultats
                    //remplissage de la table "resultatsAnnuel"
                    genererResultatsDunEleveBL.calculerResultatsAnnuelDunEleve(txtMatricule.Text, codeClasse, Convert.ToInt16(txtAnnee.Text));

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
            else MessageBox.Show("Tous les champs doivent êtres remplis !! ");
        }

        private void cmbPeriode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbPeriode.SelectedItem != null) {
                if (cmbPeriode.SelectedItem.Equals("Séquence")) {
                    //on a choisi recalculer le résultat d'un élève pour une séquence
                    List<SequenceBE> LSequence = genererResultatsDunEleveBL.listerToutesLesSequences();
                    // ------------------- Chargement de la liste des codes de séquence dans le comboBox de la fenêtre 
                    cmbChoixPeriode.ItemsSource = genererResultatsDunEleveBL.getListCodeSequence2(LSequence);

                    lblChoixPeriode.Content = "Séquence";
                    lblChoixPeriode.Visibility = System.Windows.Visibility.Visible;
                    cmbChoixPeriode.Visibility = System.Windows.Visibility.Visible;

                }
                else if (cmbPeriode.SelectedItem.Equals("Trimestre")) {
                    //on a choisi recalculer le résultat d'un élève pour un Trimestre
                    List<TrimestreBE> LTrimestre = genererResultatsDunEleveBL.listerTousLesTrimestres();
                    // ------------------- Chargement de la liste des codes de Trimestre dans le comboBox de la fenêtre 
                    cmbChoixPeriode.ItemsSource = genererResultatsDunEleveBL.getListCodeTrimestre2(LTrimestre);

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

    }
}
