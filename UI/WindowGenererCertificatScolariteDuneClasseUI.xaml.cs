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
using Ecole.ClasseConception;
using Ecole.Utilitaire;

namespace Ecole.UI
{
    /// <summary>
    /// Interaction logic for WindowGenererCertificatScolariteUI.xaml
    /// </summary>
    public partial class WindowGenererCertificatScolariteDuneClasseUI : Window
    {
        CreerCertificatScolariteBL creerCertificatScolariteBL;

        int annee;

        //Create a Delegate that matches 
        //the Signature of the ProgressBar's SetValue method
        private delegate void UpdateProgressBarDelegate(
                System.Windows.DependencyProperty dp, Object value);

        //********************************** Fin des éléments utiles pour la barre de progréssion

        public WindowGenererCertificatScolariteDuneClasseUI()
        {
            InitializeComponent();

            creerCertificatScolariteBL = new CreerCertificatScolariteBL();

            //on charge la liste des classes
            List<ClasseBE> LClasse = creerCertificatScolariteBL.listerToutesLesClasses();
            cmbClasse.ItemsSource = creerCertificatScolariteBL.getListCodeClasse2(LClasse);

            //txtAnnee.Text = Convert.ToString(System.DateTime.Today.Year);
            ParametresBE param = creerCertificatScolariteBL.getParametres();
            if (param != null)
            {
                annee = param.annee;
                txtAnnee.Text = Convert.ToString(param.annee);
                txtAnneeScolaire.Text = (param.annee - 1).ToString();
            }
            else
            {
                txtAnnee.Text = "";
                txtAnneeScolaire.Text = "";

            }

            //on cache la barre de progression
            ProgressBar1.Visibility = System.Windows.Visibility.Hidden;

        }

        private void txtAnnee_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Utilitaires.IsTextAllowed(e.Text);
        }


        private void cmdAnnuler_Click(object sender, RoutedEventArgs e)
        {
            ParametresBE param = creerCertificatScolariteBL.getParametres();
            if (param != null)
            {
                annee = param.annee;
                txtAnnee.Text = Convert.ToString(param.annee);
                txtAnneeScolaire.Text = (param.annee - 1).ToString();
            }
            else
            {
                txtAnnee.Text = "";
                txtAnneeScolaire.Text = "";

            }

            cmbClasse.Text = null;

        }

        private void cmdValider_Click(object sender, RoutedEventArgs e)
        {
            if ((txtAnneeScolaire.Text != "" && txtAnneeScolaire.Text != null) && (cmbClasse.Text != null && cmbClasse.Text != ""))
            {
                if (cmbClasse.Text.Equals("<Toutes les classes>"))
                {


                    List<ClasseBE> LClasse = creerCertificatScolariteBL.listerToutesLesClasses();

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

                    if (LClasse != null && LClasse.Count != 0)
                    {
                        for (int i = 0; i < LClasse.Count; i++)
                        {
                            //on charge la liste des inscriptions de la classe pour l'année choisi
                            List<InscrireBE> LInscrire = creerCertificatScolariteBL.listeDesEffectifsDuneClassePourUneAnnee(LClasse.ElementAt(i).codeClasse, txtAnnee.Text);

                            if (LInscrire != null && LInscrire.Count != 0)
                            {
                                for (int j = 0; j < LInscrire.Count; j++)
                                {
                                    //on recherhe l'élève
                                    EleveBE eleve = new EleveBE();
                                    eleve.matricule = LInscrire.ElementAt(j).matricule;
                                    eleve = creerCertificatScolariteBL.rechercherEleve(eleve);

                                    //on recherche la classe de l'élève
                                    ClasseBE classe = new ClasseBE();
                                    classe.codeClasse = LInscrire.ElementAt(j).codeClasse;

                                    classe = creerCertificatScolariteBL.rechercherClasse(classe);

                                    ParametresBE parametre = creerCertificatScolariteBL.getParametres();

                                    creerCertificatScolariteBL.etatCertificatScolarite(Convert.ToInt16(txtAnnee.Text), eleve, classe, LInscrire.ElementAt(j), parametre);

                                    value += 1;

                                    Dispatcher.Invoke(updatePbDelegate,
                                        System.Windows.Threading.DispatcherPriority.Background,
                                        new object[] { ProgressBar.ValueProperty, value });

                                }
                            }

                        }

                        //on cache la barre de progression
                        ProgressBar1.Visibility = System.Windows.Visibility.Hidden;

                    }

                }
                else {
                        //on charge la liste des inscriptions de la classe pour l'année choisi
                        List<InscrireBE> LInscrire = creerCertificatScolariteBL.listeDesEffectifsDuneClassePourUneAnnee(cmbClasse.Text, txtAnnee.Text);

                        //Configure the ProgressBar
                        ProgressBar1.Minimum = 0;
                        ProgressBar1.Maximum = LInscrire.Count;
                        ProgressBar1.Value = 0;

                        //Stores the value of the ProgressBar
                        double value = 0;

                        //Create a new instance of our ProgressBar Delegate that points
                        // to the ProgressBar's SetValue method.
                        UpdateProgressBarDelegate updatePbDelegate =
                            new UpdateProgressBarDelegate(ProgressBar1.SetValue);


                        //on affiche la barre de progression
                        ProgressBar1.Visibility = System.Windows.Visibility.Visible;

                        if (LInscrire != null && LInscrire.Count != 0)
                        {
                            for (int j = 0; j < LInscrire.Count; j++)
                            {
                                //on recherhe l'élève
                                EleveBE eleve = new EleveBE();
                                eleve.matricule = LInscrire.ElementAt(j).matricule;
                                eleve = creerCertificatScolariteBL.rechercherEleve(eleve);

                                //on recherche la classe de l'élève
                                ClasseBE classe = new ClasseBE();
                                classe.codeClasse = LInscrire.ElementAt(j).codeClasse;

                                classe = creerCertificatScolariteBL.rechercherClasse(classe);

                                ParametresBE parametre = creerCertificatScolariteBL.getParametres();

                                creerCertificatScolariteBL.etatCertificatScolarite(Convert.ToInt16(txtAnnee.Text), eleve, classe, LInscrire.ElementAt(j), parametre);

                                value += 1;

                                Dispatcher.Invoke(updatePbDelegate,
                                    System.Windows.Threading.DispatcherPriority.Background,
                                    new object[] { ProgressBar.ValueProperty, value });

                            }

                            //on cache la barre de progression
                            ProgressBar1.Visibility = System.Windows.Visibility.Hidden;

                        }

                }
                
            }
            else
            {
                MessageBox.Show("Tous les champs doivent êtres remplis ! ", "School Brain alerte");
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
