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

namespace Ecole.UI
{
    /// <summary>
    /// Interaction logic for WindowGenererBulletinsSequentielUI.xaml
    /// </summary>
    public partial class WindowGenererBulletinsSequentielUI : Window
    {
        GenererBulletinsSequentielBL genererBulletinsSequentielBL;

        int annee;

        //********************************** DEBUT des éléments utiles pour la barre de progréssion

        //Create a Delegate that matches 
        //the Signature of the ProgressBar's SetValue method
        private delegate void UpdateProgressBarDelegate(
                System.Windows.DependencyProperty dp, Object value);

        //********************************** Fin des éléments utiles pour la barre de progréssion

        public WindowGenererBulletinsSequentielUI()
        {
            InitializeComponent();

            genererBulletinsSequentielBL = new GenererBulletinsSequentielBL();

            List<ClasseBE> LClasse = genererBulletinsSequentielBL.listerToutesLesClasses();
            // ------------------- Chargement de la liste des codes de Classe dans le comboBox de la fenêtre 
            //(utile pour le filtre)
            cmbClasse.ItemsSource = genererBulletinsSequentielBL.getListCodeClasse(LClasse);

            List<SequenceBE> LSequence = genererBulletinsSequentielBL.listerToutesLesSequences();
            // ------------------- Chargement de la liste des codes de Sequence dans le comboBox de la fenêtre 
            //(utile pour le filtre)
            cmbSequence.ItemsSource = genererBulletinsSequentielBL.getListCodeSequence2(LSequence);

            annee = genererBulletinsSequentielBL.getAnneeEnCours();

            txtAnnee.Text = Convert.ToString(annee);
            txtAnneeScolaire.Text = (annee - 1).ToString();

            //on cache la barre de progression
            ProgressBar1.Visibility = System.Windows.Visibility.Hidden;
        }

        private void cmdFermer_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cmdAnnuler_Click(object sender, RoutedEventArgs e)
        {
            cmbClasse.Text = null;
            cmbSequence.Text = null;

            annee = genererBulletinsSequentielBL.getAnneeEnCours();

            txtAnnee.Text = Convert.ToString(annee);
            txtAnneeScolaire.Text = (annee - 1).ToString();
        }

        private void cmdValider_Click(object sender, RoutedEventArgs e)
        {
            //on vérifit si tous les champs ont été corectement rempli
            if ((cmbClasse.Text != null && txtAnneeScolaire.Text != null && cmbSequence.Text != null) && (cmbClasse.Text != "" && txtAnneeScolaire.Text != "" && cmbSequence.Text != ""))
            {
                //si on a choisi <Toutes les classes>
                if (cmbClasse.Text.Equals("<Toutes Les Classes>"))
                {
                    if (cmbSequence.Text.Equals("<Toutes Les Séquences>"))
                    {
                        //--------------------------- Action pour toutes les classes et toutes les séquences

                        //on liste toutes les classes
                        List<ClasseBE> LClasse = genererBulletinsSequentielBL.listerToutesLesClasses();

                        List<SequenceBE> LSequence = genererBulletinsSequentielBL.listerToutesLesSequences();

                        //Configure the ProgressBar
                        ProgressBar1.Minimum = 0;
                        ProgressBar1.Maximum = LClasse.Count;
                        ProgressBar1.Value = 0;

                        //Stores the value of the ProgressBar
                        double value = 0;

                        //Create a new instance of our ProgressBar Delegate that points
                        // to the ProgressBar's SetValue method.
                        UpdateProgressBarDelegate updatePbDelegate =
                            new UpdateProgressBarDelegate(ProgressBar1.SetValue);


                        //on affiche la barre de progression
                        ProgressBar1.Visibility = System.Windows.Visibility.Visible;

                        for (int i = 0; i < LClasse.Count; i++)
                        {
                            //on liste les élèves de la classe
                            List<EleveBE> LEleve = genererBulletinsSequentielBL.listeEleves(LClasse.ElementAt(i), Convert.ToInt16(txtAnnee.Text));
                            for (int j = 0; j < LEleve.Count; j++)
                            {
                                for (int k = 0; k < LSequence.Count; k++)
                                {
                                    // ***********calcul des moyennes                            
                                    //remplissage de la table "moyennes"

                                    genererBulletinsSequentielBL.genererBulletinSequentielDunEleve(LEleve.ElementAt(j).matricule, Convert.ToInt16(txtAnnee.Text), 
                                        LClasse.ElementAt(i).codeClasse, LSequence.ElementAt(k).codeseq, LEleve.ElementAt(j).photo);


                                    value += 1;

                                    Dispatcher.Invoke(updatePbDelegate,
                                        System.Windows.Threading.DispatcherPriority.Background,
                                        new object[] { ProgressBar.ValueProperty, value });
                                }
                            }
                        }

                        MessageBox.Show("Opération Terminée !! ");

                        //on cache la barre de progression
                        ProgressBar1.Visibility = System.Windows.Visibility.Hidden;
                    }
                    else
                    {
                        //--------------------------- Action pour toutes les classes et un trimestre particulier

                        //on liste toutes les classes
                        List<ClasseBE> LClasse = genererBulletinsSequentielBL.listerToutesLesClasses();

                        //Configure the ProgressBar
                        ProgressBar1.Minimum = 0;
                        ProgressBar1.Maximum = LClasse.Count;
                        ProgressBar1.Value = 0;

                        //Stores the value of the ProgressBar
                        double value = 0;

                        //Create a new instance of our ProgressBar Delegate that points
                        // to the ProgressBar's SetValue method.
                        UpdateProgressBarDelegate updatePbDelegate =
                            new UpdateProgressBarDelegate(ProgressBar1.SetValue);


                        //on affiche la barre de progression
                        ProgressBar1.Visibility = System.Windows.Visibility.Visible;

                        for (int i = 0; i < LClasse.Count; i++)
                        {
                            //on liste les élèves de la classe
                            List<EleveBE> LEleve = genererBulletinsSequentielBL.listeEleves(LClasse.ElementAt(i), Convert.ToInt16(txtAnnee.Text));
                            for (int j = 0; j < LEleve.Count; j++)
                            {
                                // ***********calcul des moyennes                            
                                //remplissage de la table "moyennes"

                                genererBulletinsSequentielBL.genererBulletinSequentielDunEleve(LEleve.ElementAt(j).matricule, Convert.ToInt16(txtAnnee.Text),
                                    LClasse.ElementAt(i).codeClasse, cmbSequence.Text, LEleve.ElementAt(j).photo);


                                value += 1;

                                Dispatcher.Invoke(updatePbDelegate,
                                    System.Windows.Threading.DispatcherPriority.Background,
                                    new object[] { ProgressBar.ValueProperty, value });
                            }
                        }

                        MessageBox.Show("Opération Terminée !! ");

                        //on cache la barre de progression
                        ProgressBar1.Visibility = System.Windows.Visibility.Hidden;
                    }
                }
                else
                {
                    if (cmbSequence.Text.Equals("<Toutes Les Séquences>"))
                    {
                        //--------------------- Action pour une classe particulière et toutes les Séquences
                        List<SequenceBE> LSequence = genererBulletinsSequentielBL.listerToutesLesSequences();

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

                        ClasseBE classe = new ClasseBE();
                        classe.codeClasse = cmbClasse.Text;
                        classe = genererBulletinsSequentielBL.rechercherClasse(classe);
                        //on liste les élèves de la classe
                        List<EleveBE> LEleve = genererBulletinsSequentielBL.listeEleves(classe, Convert.ToInt16(txtAnnee.Text));
                        for (int j = 0; j < LEleve.Count; j++)
                        {
                            for (int k = 0; k < LSequence.Count; k++)
                            {
                                // ***********calcul des moyennes                            
                                //remplissage de la table "moyennes"

                                genererBulletinsSequentielBL.genererBulletinSequentielDunEleve(LEleve.ElementAt(j).matricule, Convert.ToInt16(txtAnnee.Text), classe.codeClasse, 
                                    LSequence.ElementAt(k).codeseq, LEleve.ElementAt(j).photo);


                                value += 1;

                                Dispatcher.Invoke(updatePbDelegate,
                                    System.Windows.Threading.DispatcherPriority.Background,
                                    new object[] { ProgressBar.ValueProperty, value });
                            }
                        }

                        MessageBox.Show("Opération Terminée !! ");

                        //on cache la barre de progression
                        ProgressBar1.Visibility = System.Windows.Visibility.Hidden;
                    }
                    else
                    {

                        //--------------------- Action pour une classe particulière et un trimestre particulier

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

                        // ***********calcul des moyennes                            
                        //remplissage de la table "moyennes"

                        ClasseBE classe = new ClasseBE();
                        classe.codeClasse = cmbClasse.Text;
                        classe = genererBulletinsSequentielBL.rechercherClasse(classe);

                        //on liste les élèves de la classe
                        List<EleveBE> LEleve = genererBulletinsSequentielBL.listeEleves(classe, Convert.ToInt16(txtAnnee.Text));
                        for (int j = 0; j < LEleve.Count; j++)
                        {

                            // ***********calcul des moyennes                            
                            //remplissage de la table "moyennes"

                            genererBulletinsSequentielBL.genererBulletinSequentielDunEleve(LEleve.ElementAt(j).matricule, Convert.ToInt16(txtAnnee.Text), cmbClasse.Text, 
                                cmbSequence.Text, LEleve.ElementAt(j).photo);


                            value += 1;

                            Dispatcher.Invoke(updatePbDelegate,
                                System.Windows.Threading.DispatcherPriority.Background,
                                new object[] { ProgressBar.ValueProperty, value });
                        }

                        //calcul des résultats
                        //remplissage de la table "resultats"

                        //genererResultatsTrimestrielsBL.calculerResultatsTrimestriels(cmbClasse.Text, cmbTrimestre.Text, Convert.ToInt16(txtAnnee.Text));

                        MessageBox.Show("Opération Terminée !! ");

                        //on cache la barre de progression
                        ProgressBar1.Visibility = System.Windows.Visibility.Hidden;
                    }

                }


            }
            else MessageBox.Show("Tous les champs doivent êtres remplis !! ");
        }

        private void txtAnnee_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Utilitaires.IsTextAllowed(e.Text);
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
