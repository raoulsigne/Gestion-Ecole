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
    public partial class ResultatSequentielUI : Window
    {
        GenererResultatsDunEleveBL genererResultatsDunEleveBL;
        CreerModifierClasseBL classeBL;

        //********************************** DEBUT des éléments utiles pour la barre de progréssion

        //Create a Delegate that matches 
        //the Signature of the ProgressBar's SetValue method
        private delegate void UpdateProgressBarDelegate(
                System.Windows.DependencyProperty dp, Object value);

        //********************************** Fin des éléments utiles pour la barre de progréssion

        public ResultatSequentielUI()
        {
            InitializeComponent();
            genererResultatsDunEleveBL = new GenererResultatsDunEleveBL();
            classeBL = new CreerModifierClasseBL();

            List<ClasseBE> LClasse = genererResultatsDunEleveBL.listerToutesLesClasses();
            // ---- Chargement de la liste des Classe
            cmbClasse.ItemsSource = genererResultatsDunEleveBL.getListCodeClasse(LClasse);

            txtAnnee.Text = Convert.ToString(genererResultatsDunEleveBL.getAnneeEnCours());
            txtAnneeScolaire.Text = (Convert.ToInt32(txtAnnee.Text) - 1).ToString() + "/" + txtAnnee.Text.ToString();

            //on charge les périodes dans le comboBox
            String[] periode = { "Séquence", "Trimestre", "Année" };
            cmbPeriode.ItemsSource = periode;

            lblChoixPeriode.Visibility = System.Windows.Visibility.Hidden;
            cmbChoixPeriode.Visibility = System.Windows.Visibility.Hidden;

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

                listeEleve = genererResultatsDunEleveBL.listeEleves(classeBE, Convert.ToInt32(txtAnnee.Text));


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
        public void imprimerResultatDunEleve(int annee, string classe)
        {


            //on vérifit si tous les champs ont été corectement rempli
            if ((cmbClasse.Text != null && txtAnnee.Text != null && cmbPeriode.Text != null) && (cmbClasse.Text != "" && txtAnnee.Text != "" && cmbPeriode.Text != ""))
            {

                //la classe de l'élève
                //String codeClasse = genererResultatsDunEleveBL.getClasseEleve(txtMatricule.Text, Convert.ToInt16(txtAnnee.Text));


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
                                for (int j = 0; j < listBoxEleve.Items.Count; j++)
                                {
                                    
                                        ListBoxItem item = listBoxEleve.ItemContainerGenerator.ContainerFromIndex(j) as ListBoxItem;

                                        if (item != null)
                                        {
                                            CheckBox myCheckBox = FindVisualChild<CheckBox>(item) as CheckBox;
                                            if (myCheckBox.IsChecked.Value)
                                            {
                                                //remplissage de la table "moyennes"
                                                genererResultatsDunEleveBL.calculerMoyenneSequentielleDunEleve((listBoxEleve.Items[j] as ElementListeEleve).matricule, cmbClasse.Text, LSequence.ElementAt(i).codeseq, Convert.ToInt16(txtAnnee.Text));

                                                //calcul des résultats
                                                //remplissage de la table "resultats"
                                                genererResultatsDunEleveBL.calculerResultatsSequentielDunEleve((listBoxEleve.Items[j] as ElementListeEleve).matricule, cmbClasse.Text, LSequence.ElementAt(i).codeseq, Convert.ToInt16(txtAnnee.Text));

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
                            ProgressBar1.Maximum = listBoxEleve.Items.Count;
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
                                    ListBoxItem item = listBoxEleve.ItemContainerGenerator.ContainerFromIndex(i) as ListBoxItem;

                                    if (item != null)
                                    {
                                        CheckBox myCheckBox = FindVisualChild<CheckBox>(item) as CheckBox;
                                        if (myCheckBox.IsChecked.Value)
                                        {
                                            
                                            //remplissage de la table "moyennes"
                                            genererResultatsDunEleveBL.calculerMoyenneSequentielleDunEleve((listBoxEleve.Items[i] as ElementListeEleve).matricule, cmbClasse.Text, cmbChoixPeriode.Text, Convert.ToInt16(txtAnnee.Text));

                                            //calcul des résultats
                                            //remplissage de la table "resultats"
                                            genererResultatsDunEleveBL.calculerResultatsSequentielDunEleve((listBoxEleve.Items[i] as ElementListeEleve).matricule, cmbClasse.Text, cmbChoixPeriode.Text, Convert.ToInt16(txtAnnee.Text));
                                           
                                        }

                                    }

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
                            List<TrimestreBE> LTrimestre = genererResultatsDunEleveBL.listerTousLesTrimestres();

                            //Configure the ProgressBar
                            ProgressBar1.Minimum = 0;
                            ProgressBar1.Maximum = listBoxEleve.Items.Count;
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
                                            //remplissage de la table "moyennesTrimestrielles"
                                            genererResultatsDunEleveBL.calculerMoyenneTrimestrielleDunEleve((listBoxEleve.Items[i] as ElementListeEleve).matricule, cmbClasse.Text, LTrimestre.ElementAt(k).codetrimestre, Convert.ToInt16(txtAnnee.Text));

                                            //calcul des résultats
                                            //remplissage de la table "resultatsTrimestriel"
                                            genererResultatsDunEleveBL.calculerResultatsTrimestrielDunEleve((listBoxEleve.Items[i] as ElementListeEleve).matricule, cmbClasse.Text, LTrimestre.ElementAt(k).codetrimestre, Convert.ToInt16(txtAnnee.Text));


                                            
                                        }

                                    }
                                }

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
                            ProgressBar1.Maximum = listBoxEleve.Items.Count;
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
                                    ListBoxItem item = listBoxEleve.ItemContainerGenerator.ContainerFromIndex(i) as ListBoxItem;

                                    if (item != null)
                                    {
                                        CheckBox myCheckBox = FindVisualChild<CheckBox>(item) as CheckBox;
                                        if (myCheckBox.IsChecked.Value)
                                        {
                                           //remplissage de la table "moyennesTrimestrielles"
                                            genererResultatsDunEleveBL.calculerMoyenneTrimestrielleDunEleve((listBoxEleve.Items[i] as ElementListeEleve).matricule, cmbClasse.Text, cmbChoixPeriode.Text, Convert.ToInt16(txtAnnee.Text));

                                            
                                            //calcul des résultats
                                            //remplissage de la table "resultatsTrimestriel"
                                            genererResultatsDunEleveBL.calculerResultatsTrimestrielDunEleve((listBoxEleve.Items[i] as ElementListeEleve).matricule, cmbClasse.Text, cmbChoixPeriode.Text, Convert.ToInt16(txtAnnee.Text));


                                        }

                                    
                                }

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
                    ProgressBar1.Maximum = listBoxEleve.Items.Count;
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
                            ListBoxItem item = listBoxEleve.ItemContainerGenerator.ContainerFromIndex(i) as ListBoxItem;

                            if (item != null)
                            {
                                CheckBox myCheckBox = FindVisualChild<CheckBox>(item) as CheckBox;
                                if (myCheckBox.IsChecked.Value)
                                {
                                    //remplissage de la table "moyennesAnnuelles"
                                    genererResultatsDunEleveBL.calculerMoyenneAnnuelleDunEleve((listBoxEleve.Items[i] as ElementListeEleve).matricule, cmbClasse.Text, Convert.ToInt16(txtAnnee.Text));


                                    //calcul des résultats
                                    //remplissage de la table "resultatsAnnuel"
                                    genererResultatsDunEleveBL.calculerResultatsAnnuelDunEleve((listBoxEleve.Items[i] as ElementListeEleve).matricule, cmbClasse.Text, Convert.ToInt16(txtAnnee.Text));


                                   


                                }

                            }

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
            else MessageBox.Show("Tous les champs doivent êtres remplis !! ");

        }


        

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


        //---cocher tous les élèves------------------------------

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
            imprimerResultatDunEleve(Convert.ToInt32(txtAnnee.Text.ToString()), cmbClasse.Text.ToString());
        }

        //-----------fin arbre visuel---------------------------------

        private void cmbPeriode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbPeriode.SelectedItem != null)
            {
                if (cmbPeriode.SelectedItem.Equals("Séquence"))
                {
                    //on a choisi recalculer le résultat d'un élève pour une séquence
                    List<SequenceBE> LSequence = genererResultatsDunEleveBL.listerToutesLesSequences();
                    // ------------------- Chargement de la liste des codes de séquence dans le comboBox de la fenêtre 
                    cmbChoixPeriode.ItemsSource = genererResultatsDunEleveBL.getListCodeSequence2(LSequence);

                    lblChoixPeriode.Content = "Séquence";
                    lblChoixPeriode.Visibility = System.Windows.Visibility.Visible;
                    cmbChoixPeriode.Visibility = System.Windows.Visibility.Visible;

                }
                else if (cmbPeriode.SelectedItem.Equals("Trimestre"))
                {
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

        private void txtAnnee_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Utilitaires.IsTextAllowed(e.Text);
        }

    }


    //public class ElementListeEleve
    //{
    //    public string matricule { get; set; }
    //    public String nom { get; set; }
    //    public string chaineAffichee { get; set; }
    //}

   
}

