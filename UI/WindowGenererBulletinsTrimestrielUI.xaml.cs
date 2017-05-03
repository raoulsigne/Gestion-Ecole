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
    /// Interaction logic for WindowGenererBulletinsTrimestrielUI.xaml
    /// </summary>
    public partial class WindowGenererBulletinsTrimestrielUI : Window
    {
        GenererBulletinsTrimestrielBL genererBulletinsTrimestrielBL;

        int annee;
        //********************************** DEBUT des éléments utiles pour la barre de progréssion

        //Create a Delegate that matches 
        //the Signature of the ProgressBar's SetValue method
        private delegate void UpdateProgressBarDelegate(
                System.Windows.DependencyProperty dp, Object value);

        //********************************** Fin des éléments utiles pour la barre de progréssion

        public WindowGenererBulletinsTrimestrielUI()
        {
            InitializeComponent();

            genererBulletinsTrimestrielBL = new GenererBulletinsTrimestrielBL();

            List<ClasseBE> LClasse = genererBulletinsTrimestrielBL.listerToutesLesClasses();
            // ------------------- Chargement de la liste des codes de Classe dans le comboBox de la fenêtre 
            //(utile pour le filtre)
            cmbClasse.ItemsSource = genererBulletinsTrimestrielBL.getListCodeClasse(LClasse);

            List<TrimestreBE> LTrimestre = genererBulletinsTrimestrielBL.listerTousLesTrimestres();
            // ------------------- Chargement de la liste des codes de Trimestre dans le comboBox de la fenêtre 
            //(utile pour le filtre)
            cmbTrimestre.ItemsSource = genererBulletinsTrimestrielBL.getListCodeTrimestre2(LTrimestre);

            annee = genererBulletinsTrimestrielBL.getAnneeEnCours();

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
            cmbTrimestre.Text = null;

            annee = genererBulletinsTrimestrielBL.getAnneeEnCours();

            txtAnnee.Text = Convert.ToString(annee);
            txtAnneeScolaire.Text = (annee - 1).ToString();
        }

        private void cmdValider_Click(object sender, RoutedEventArgs e)
        {
            //on vérifit si tous les champs ont été corectement rempli
            if ((cmbClasse.Text != null && txtAnneeScolaire.Text != null && cmbTrimestre.Text != null) && (cmbClasse.Text != "" && txtAnneeScolaire.Text != "" && cmbTrimestre.Text != ""))
            {
                //si on a choisi <Toutes les classes>
                if (cmbClasse.Text.Equals("<Toutes Les Classes>"))
                {
                    if (cmbTrimestre.Text.Equals("<Tous Les Trimestres>"))
                    {
                        //--------------------------- Action pour toutes les classes et tous les trimestres

                        //on liste toutes les classes
                        List<ClasseBE> LClasse = genererBulletinsTrimestrielBL.listerToutesLesClasses();

                        List<TrimestreBE> LTrimestre = genererBulletinsTrimestrielBL.listerTousLesTrimestres();

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
                            List<EleveBE> LEleve = genererBulletinsTrimestrielBL.listeEleves(LClasse.ElementAt(i), Convert.ToInt16(txtAnnee.Text));
                            for (int j = 0; j < LEleve.Count; j++)
                            {
                                for (int k = 0; k < LTrimestre.Count; k++)
                                {
                                    // ***********calcul des moyennes                            
                                    //remplissage de la table "moyennes"

                                    genererBulletinsTrimestrielBL.genererBulletinTrimestrielDunEleve(LEleve.ElementAt(j).matricule, Convert.ToInt16(txtAnnee.Text),
                                        LClasse.ElementAt(i).codeClasse, LTrimestre.ElementAt(k).codetrimestre, LEleve.ElementAt(j).photo);


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
                        List<ClasseBE> LClasse = genererBulletinsTrimestrielBL.listerToutesLesClasses();

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
                            List<EleveBE> LEleve = genererBulletinsTrimestrielBL.listeEleves(LClasse.ElementAt(i), Convert.ToInt16(txtAnnee.Text));
                            for (int j = 0; j < LEleve.Count; j++)
                            {
                                // ***********calcul des moyennes                            
                                //remplissage de la table "moyennes"

                                genererBulletinsTrimestrielBL.genererBulletinTrimestrielDunEleve(LEleve.ElementAt(j).matricule, Convert.ToInt16(txtAnnee.Text),
                                    LClasse.ElementAt(i).codeClasse, cmbTrimestre.Text, LEleve.ElementAt(j).photo);


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
                    if (cmbTrimestre.Text.Equals("<Tous Les Trimestres>"))
                    {
                        //--------------------- Action pour une classe particulière et tous les trimestres
                        List<TrimestreBE> LTrimestre = genererBulletinsTrimestrielBL.listerTousLesTrimestres();

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

                        ClasseBE classe = new ClasseBE();
                        classe.codeClasse = cmbClasse.Text;
                        classe = genererBulletinsTrimestrielBL.rechercherClasse(classe);
                        //on liste les élèves de la classe
                        List<EleveBE> LEleve = genererBulletinsTrimestrielBL.listeEleves(classe, Convert.ToInt16(txtAnnee.Text));
                        for (int j = 0; j < LEleve.Count; j++)
                        {
                            for (int k = 0; k < LTrimestre.Count; k++)
                            {
                                // ***********calcul des moyennes                            
                                //remplissage de la table "moyennes"

                                genererBulletinsTrimestrielBL.genererBulletinTrimestrielDunEleve(LEleve.ElementAt(j).matricule, Convert.ToInt16(txtAnnee.Text), classe.codeClasse, 
                                    LTrimestre.ElementAt(k).codetrimestre, LEleve.ElementAt(j).photo);


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
                        classe = genererBulletinsTrimestrielBL.rechercherClasse(classe);

                        //on liste les élèves de la classe
                        List<EleveBE> LEleve = genererBulletinsTrimestrielBL.listeEleves(classe, Convert.ToInt16(txtAnnee.Text));
                        for (int j = 0; j < LEleve.Count; j++)
                        {

                            // ***********calcul des moyennes                            
                            //remplissage de la table "moyennes"

                            genererBulletinsTrimestrielBL.genererBulletinTrimestrielDunEleve(LEleve.ElementAt(j).matricule, Convert.ToInt16(txtAnnee.Text), cmbClasse.Text,
                                cmbTrimestre.Text, LEleve.ElementAt(j).photo);


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
