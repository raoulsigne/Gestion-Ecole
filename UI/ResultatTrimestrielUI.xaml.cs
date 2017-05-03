﻿using System;
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
    public partial class ResultatTrimestrielUI : Window
    {
        GenererBulletinsTrimestrielBL bulletinsTrimestrielBL;
        CreerModifierClasseBL classeBL;

        //********************************** DEBUT des éléments utiles pour la barre de progréssion

        //Create a Delegate that matches 
        //the Signature of the ProgressBar's SetValue method
        private delegate void UpdateProgressBarDelegate(
                System.Windows.DependencyProperty dp, Object value);

        //********************************** Fin des éléments utiles pour la barre de progréssion

        public ResultatTrimestrielUI()
        {
            InitializeComponent();
            bulletinsTrimestrielBL = new GenererBulletinsTrimestrielBL();
            classeBL = new CreerModifierClasseBL();

            List<ClasseBE> LClasse = bulletinsTrimestrielBL.listerToutesLesClasses();
            // ---- Chargement de la liste des Classe
            cmbClasse.ItemsSource = bulletinsTrimestrielBL.getListCodeClasse(LClasse);

            List<TrimestreBE> LTrimestre = bulletinsTrimestrielBL.listerTousLesTrimestres();
            // ---- Chargement de la liste des Sequences
            cmbTrimestre.ItemsSource = bulletinsTrimestrielBL.getListCodeTrimestre2(LTrimestre);

            txtAnnee.Text = Convert.ToString(bulletinsTrimestrielBL.getAnneeEnCours());
            txtAnneeScolaire.Text = (Convert.ToInt32(txtAnnee.Text) - 1).ToString() + "/" + txtAnnee.Text.ToString();


            //SauvegarderMenuDansPrivilege();
            //afficherEleveDansListBox(cmbClasse.Text, txtAnnee.Text);
            listBoxEleve.SelectionMode = SelectionMode.Multiple;

            //on cache la barre de progression
            ProgressBar1.Visibility = System.Windows.Visibility.Hidden;
        }

        //---------Afficher les éléments du menu de la fenetre principale dans le listBox-----------

        public void afficherEleveDansListBox(string codeClasse, string annee)
        {
            List<ElementListeEleve> listElementBox = new List<ElementListeEleve>();
            List<ClasseBE> listClasse = new List<ClasseBE>();
            List<EleveBE> listeEleve = new List<EleveBE>();
            ClasseBE classeBE = new ClasseBE();
            try
            {
                //avoir l'entité classe qui correspond au code sélectionné
                listClasse = classeBL.listerClasseSuivantCritere("codeclasse='" + codeClasse + "'");
                if (listClasse.Count != 0)
                    classeBE = listClasse.ElementAt(0);

                listeEleve = bulletinsTrimestrielBL.listeEleves(classeBE, Convert.ToInt32(txtAnnee.Text));


                foreach (EleveBE unEleve in listeEleve)
                {

                    listElementBox.Add(new ElementListeEleve()
                    {
                        matricule = unEleve.matricule.ToString(),
                        nom = unEleve.nom.ToString(),
                        chaineAffichee = unEleve.matricule.ToString().ToUpper() + "==" + unEleve.nom.ToString().ToUpper()
                       //newTextElement = unEleve.nom.ToString().ToUpper() + "==" + unEleve.matricule.ToString().ToUpper() 

                    });
                }

                //trier la liste avant d'envoyer à létat
                if (listElementBox.Count != 0)
                {
                    List<ElementListeEleve> newList = listElementBox.OrderBy(o => o.nom).ToList();
                    listElementBox.Clear();
                    foreach (ElementListeEleve elt in newList)
                    {
                        listElementBox.Add(elt);
                    }
                    listBoxEleve.ItemsSource = listElementBox;
                }


            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        //-------------------Imprimer les relevés des élèves sélectionnés-------------------
        public void imprimerBulletinTrimestriel(int annee, string classe, string sequence)
        {

            //on vérifit si tous les champs ont été corectement rempli
            if ((cmbClasse.Text != null && txtAnnee.Text != null && cmbTrimestre.Text != null) && (cmbClasse.Text != "" && txtAnnee.Text != "" && cmbTrimestre.Text != ""))
            {
                
                    if (cmbTrimestre.Text.Equals("<Tous Les Trimestres>"))
                    {
                        //--------------------- Action pour une classe particulière et tous les trimestres
                        List<TrimestreBE> LTrimestre = bulletinsTrimestrielBL.listerTousLesTrimestres();

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

                        for (int i = 0; i < listBoxEleve.Items.Count; i++)
                        {
                            for (int k = 0; k < LTrimestre.Count; k++)
                            {
                                ListBoxItem item = listBoxEleve.ItemContainerGenerator.ContainerFromIndex(i) as ListBoxItem;

                                if (item != null)
                                {
                                    CheckBox myCheckBox = FindVisualChild<CheckBox>(item) as CheckBox;
                                    if (myCheckBox.IsChecked.Value)
                                    {
                                        //bulletinsTrimestrielBL.genererBulletinTrimestrielDunEleve((listBoxEleve.Items[i] as ElementListeEleve).matricule, annee, classe, sequence);
                                        string matricule = (listBoxEleve.Items[i] as ElementListeEleve).matricule;
                                        EleveBE eleve = new EleveBE();
                                        eleve = bulletinsTrimestrielBL.rechercherEleve(eleve);
                                        bulletinsTrimestrielBL.genererBulletinTrimestrielDunEleve(matricule, Convert.ToInt16(txtAnnee.Text), classe, 
                                            LTrimestre.ElementAt(k).codetrimestre, eleve.photo);

                                        value += 1;

                                        Dispatcher.Invoke(updatePbDelegate,
                                            System.Windows.Threading.DispatcherPriority.Background,
                                            new object[] { ProgressBar.ValueProperty, value });
                                    }

                                }
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

                        for (int i = 0; i < listBoxEleve.Items.Count; i++)
                        {
                                ListBoxItem item = listBoxEleve.ItemContainerGenerator.ContainerFromIndex(i) as ListBoxItem;

                                if (item != null)
                                {
                                    CheckBox myCheckBox = FindVisualChild<CheckBox>(item) as CheckBox;
                                    if (myCheckBox.IsChecked.Value)
                                    {
                                        //bulletinsTrimestrielBL.genererBulletinTrimestrielDunEleve((listBoxEleve.Items[i] as ElementListeEleve).matricule, annee, classe, sequence);
                                        string matricule = (listBoxEleve.Items[i] as ElementListeEleve).matricule;
                                        EleveBE eleve = new EleveBE();
                                        eleve = bulletinsTrimestrielBL.rechercherEleve(eleve);
                                        bulletinsTrimestrielBL.genererBulletinTrimestrielDunEleve(matricule, Convert.ToInt16(txtAnnee.Text), classe, 
                                            cmbTrimestre.Text, eleve.photo);

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

            }
            else MessageBox.Show("Tous les champs doivent êtres remplis !! ");

            
        }

        //------fin cocher les menus du role----------------------------

        //---------décocher tous les élèves-------------------------

        private void decocherTousLesMenu()
        {
            for (int i = 0; i < listBoxEleve.Items.Count; i++)
            {
                ListBoxItem item = listBoxEleve.ItemContainerGenerator.ContainerFromIndex(i) as ListBoxItem;

                if (item != null)
                {
                    CheckBox myCheckBox = FindVisualChild<CheckBox>(item) as CheckBox;
                    myCheckBox.IsChecked = false;

                }
            }
        }

        //-----fin decocher tous les menus------------------------


        //---cocher tous les menus------------------------------

        private void cocherTousLesMenu()
        {
            for (int i = 0; i < listBoxEleve.Items.Count; i++)
            {
                ListBoxItem item = listBoxEleve.ItemContainerGenerator.ContainerFromIndex(i) as ListBoxItem;

                if (item != null)
                {
                    CheckBox myCheckBox = FindVisualChild<CheckBox>(item) as CheckBox;
                    myCheckBox.IsChecked = true;

                }
            }
        }

        //-----fin cocher tous les menus------------------------

        //---parcourir l'abre visuel------------------------------

        public static childItem FindVisualChild<childItem>(DependencyObject obj) where childItem : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is childItem)
                    return (childItem)child;
                else
                {
                    childItem childOfChild = FindVisualChild<childItem>(child);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }
            return null;
        }

        private void listBoxEleve_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            for (int i = 0; i < listBoxEleve.Items.Count; i++)
            {
                ListBoxItem item = listBoxEleve.ItemContainerGenerator.ContainerFromIndex(i) as ListBoxItem;

                if (item != null)
                {
                    CheckBox myCheckBox = FindVisualChild<CheckBox>(item) as CheckBox;

                    if (myCheckBox.IsChecked == true)
                    {
                        listBoxEleve.SelectedItems.Add(item);

                    }

                }
            }
        }

        private void chBxEleve_Checked(object sender, RoutedEventArgs e)
        {

            var cb = sender as CheckBox;
            var item = cb.DataContext;
            listBoxEleve.SelectedItems.Add(item);


        }

        private void chBxEleve_Unchecked(object sender, RoutedEventArgs e)
        {
            var cb = sender as CheckBox;
            var item = cb.DataContext;
            listBoxEleve.SelectedItems.Remove(item);

        }

        //private void cmbRole_DropDownClosed(object sender, EventArgs e)
        //{
        //    txtDescription.Text = gestionGroupeBL.getDescriptionRole(cmbRole.Text.ToString());
        //}

        private void chBxTousCocher_Checked(object sender, RoutedEventArgs e)
        {
            cocherTousLesMenu();
        }

        private void chBxTousCocher_Unchecked(object sender, RoutedEventArgs e)
        {
            decocherTousLesMenu();
        }

        private void btnFerm_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void txtAnnee_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtAnnee.Text != "")
                txtAnneeScolaire.Text = (Convert.ToInt32(txtAnnee.Text) - 1).ToString() + "/" + txtAnnee.Text.ToString();
        }

        private void cmbClasse_DropDownClosed(object sender, EventArgs e)
        {
            listBoxEleve.ItemsSource = null;
            afficherEleveDansListBox(cmbClasse.Text, txtAnnee.Text);
            cocherTousLesMenu();
        }

        private void btnImprimer_Click(object sender, RoutedEventArgs e)
        {
            imprimerBulletinTrimestriel(Convert.ToInt32(txtAnnee.Text.ToString()), cmbClasse.Text.ToString(), cmbTrimestre.Text.ToString());
        }

        private void txtAnnee_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Utilitaires.IsTextAllowed(e.Text);
        }

        //-----------fin arbre visuel---------------------------------

    }


    //public class ElementListeEleve
    //{
    //    public string matricule { get; set; }
    //    public String nom { get; set; }
    //    public string chaineAffichee { get; set; }
    //}

   
}
