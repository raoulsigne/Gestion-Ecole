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

using System.Globalization;
using System.Threading;

using Ecole.BusinessLogic;
using Ecole.BusinessEntity;
using Ecole.ClasseConception;
using Ecole.Utilitaire;

namespace Ecole.UI
{
    /// <summary>
    /// Interaction logic for WindowProfilAcademiqueUI.xaml
    /// </summary>
    public partial class WindowProfilAcademiqueUI : Window
    {
        GenererprofilAcademiqueBL genererprofilAcademiqueBL = new GenererprofilAcademiqueBL();
        int annee;
        //********************************** DEBUT des éléments utiles pour la barre de progréssion

        //Create a Delegate that matches 
        //the Signature of the ProgressBar's SetValue method
        private delegate void UpdateProgressBarDelegate(
                System.Windows.DependencyProperty dp, Object value);

        //********************************** Fin des éléments utiles pour la barre de progréssion

        public WindowProfilAcademiqueUI()
        {
            InitializeComponent();

            List<ClasseBE> LClasse = genererprofilAcademiqueBL.listerToutesLesClasses();

            cmbClasse.ItemsSource = genererprofilAcademiqueBL.getListCodeClasse(LClasse);

            //on cache la barre de progression
            ProgressBar1.Visibility = System.Windows.Visibility.Hidden;

            ParametresBE param = genererprofilAcademiqueBL.getParametres();
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

            //on grise le champs Année pour ne pas donner la possibilité de le modifier
            txtAnnee.IsEnabled = false;
        }

        private void txtMatricule_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                EleveBE eleve = new EleveBE();
                eleve.matricule = txtMatricule.Text;

                EleveBE eleve2 = genererprofilAcademiqueBL.rechercherEleve(eleve);


                if (eleve2 != null)
                {
                    lblInfoEleve.Content = "[Nom :" + eleve2.nom + "]";
                    //imageEleve.Source = new BitmapImage(new Uri("../Images/"+ eleve2.photo+".jpg"));
                    if (eleve2.photo != "")
                    {
                        try
                        {
                            imageEleve.Source = new BitmapImage(new Uri(ConnexionUI.DOSSIER_PHOTO + eleve2.photo));
                        }
                        catch (Exception) { imageEleve.Source = null; }
                    }
                    else imageEleve.Source = null;

                }
                else
                {
                    lblInfoEleve.Content = "";
                    imageEleve.Source = null;

                    MessageBox.Show("il y a aucun élève avec ce matricule dans le système !");
                }
            }
        }

        private void txtMatricule_GotFocus(object sender, RoutedEventArgs e)
        {
                //EleveBE eleve = new EleveBE();
                //eleve.matricule = txtMatricule.Text;

                //EleveBE eleve2 = genererprofilAcademiqueBL.rechercherEleve(eleve);


                //if (eleve2 != null)
                //{
                //    lblInfoEleve.Content = "[Nom :" + eleve2.nom + "]";
                //    //imageEleve.Source = new BitmapImage(new Uri("../Images/"+ eleve2.photo+".jpg"));
                //    if (eleve2.photo != "")
                //        imageEleve.Source = new BitmapImage(new Uri("E:\\annuler2.jpg"));
                //    else imageEleve.Source = null;

                //}
                //else
                //{
                //    lblInfoEleve.Content = "";
                //    imageEleve.Source = null;

                //    MessageBox.Show("il y a aucun élève avec ce matricule dans le système !");
                //}
        }

        private void txtMatricule_LostFocus(object sender, RoutedEventArgs e)
        {
             //EleveBE eleve = new EleveBE();
             //   eleve.matricule = txtMatricule.Text;

             //   EleveBE eleve2 = genererprofilAcademiqueBL.rechercherEleve(eleve);


             //   if (eleve2 != null)
             //   {
             //       lblInfoEleve.Content = "[Nom :" + eleve2.nom + "]";
             //       //imageEleve.Source = new BitmapImage(new Uri("../Images/"+ eleve2.photo+".jpg"));
             //       if (eleve2.photo != "")
             //           imageEleve.Source = new BitmapImage(new Uri("E:\\annuler2.jpg"));
             //       else imageEleve.Source = null;

             //   }
             //   else
             //   {
             //       lblInfoEleve.Content = "";
             //       imageEleve.Source = null;

             //       MessageBox.Show("il y a aucun élève avec ce matricule dans le système !");
             //   }
        }

        private void cmdAnnuler_Click(object sender, RoutedEventArgs e)
        {
            txtMatricule.Text = "";
        }

        private void cmdImprimer_Click(object sender, RoutedEventArgs e)
        {
            if (txtMatricule.Text != null && txtMatricule.Text != "")
            {
                genererprofilAcademiqueBL.genererProfilAcademiqueDunEleve(txtMatricule.Text);
            }
            else {
                MessageBox.Show("vous devez saisir le matricule d'un élève", "School Brain : Alerte !");
            }
        }

        private void cmdAnnuler2_Click(object sender, RoutedEventArgs e)
        {
            cmbClasse.Text = null;

            ParametresBE param = genererprofilAcademiqueBL.getParametres();
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
        }

        private void cmdImprimer2_Click(object sender, RoutedEventArgs e)
        {
            if (cmbClasse.Text != null && cmbClasse.Text != "" && txtAnneeScolaire.Text != null && txtAnneeScolaire.Text != "")
            {
                if (cmbClasse.Text.Equals("<Toutes les classes>"))
                {
                    List<ClasseBE> LClasse = genererprofilAcademiqueBL.listerToutesLesClasses();

                    if (LClasse != null && LClasse.Count != 0) {

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

                        for(int i=0; i<LClasse.Count; i++){
                        //on charge la liste des élèves de la classe considéré
                            List<EleveBE> LEleve = genererprofilAcademiqueBL.ListeDesElevesDuneClasse(LClasse.ElementAt(i).codeClasse, Convert.ToInt16(txtAnnee.Text));
                            if (LEleve != null && LEleve.Count != 0)
                            {
                                for (int j = 0; j < LEleve.Count; j++)
                                {
                                    genererprofilAcademiqueBL.genererProfilAcademiqueDunEleve(LEleve.ElementAt(j).matricule);
                                    
                                    value += 1;

                                    Dispatcher.Invoke(updatePbDelegate,
                                        System.Windows.Threading.DispatcherPriority.Background,
                                        new object[] { ProgressBar.ValueProperty, value });
                                }
                            }
                        }
                    }
                }
                else
                {
                    //on charge la liste des élèves de la classe considéré
                    List<EleveBE> LEleve = genererprofilAcademiqueBL.ListeDesElevesDuneClasse(cmbClasse.Text, Convert.ToInt16(txtAnnee.Text));
                    if (LEleve != null && LEleve.Count != 0)
                    {
                        //Configure the ProgressBar
                        ProgressBar1.Minimum = 0;
                        ProgressBar1.Maximum = LEleve.Count;
                        ProgressBar1.Value = 0;

                        //Stores the value of the ProgressBar
                        double value = 0;

                        //Create a new instance of our ProgressBar Delegate that points
                        // to the ProgressBar's SetValue method.
                        UpdateProgressBarDelegate updatePbDelegate =
                            new UpdateProgressBarDelegate(ProgressBar1.SetValue);


                        //on affiche la barre de progression
                        ProgressBar1.Visibility = System.Windows.Visibility.Visible;

                        for (int j = 0; j < LEleve.Count; j++)
                        {
                            genererprofilAcademiqueBL.genererProfilAcademiqueDunEleve(LEleve.ElementAt(j).matricule);

                            value += 1;

                            Dispatcher.Invoke(updatePbDelegate,
                                System.Windows.Threading.DispatcherPriority.Background,
                                new object[] { ProgressBar.ValueProperty, value });

                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("vous devez choisir la classe et l'année", "School Brain : Alerte !");
            }
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
