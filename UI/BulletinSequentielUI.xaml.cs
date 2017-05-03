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
using iTextSharp.text;
using Ecole.Utilitaire;
using iTextSharp.text.pdf;

namespace Ecole.UI
{
    public partial class BulletinSequentielUI : Window
    {
        GenererBulletinsSequentielBL bulletinsSequentielBL;
        CreerModifierClasseBL classeBL;

        //********************************** DEBUT des éléments utiles pour la barre de progréssion

        //Create a Delegate that matches 
        //the Signature of the ProgressBar's SetValue method
        private delegate void UpdateProgressBarDelegate(
                System.Windows.DependencyProperty dp, Object value);

        //********************************** Fin des éléments utiles pour la barre de progréssion

        public BulletinSequentielUI()
        {
            InitializeComponent();
            bulletinsSequentielBL = new GenererBulletinsSequentielBL();
            classeBL = new CreerModifierClasseBL();

            List<ClasseBE> LClasse = bulletinsSequentielBL.listerToutesLesClasses();
            // ---- Chargement de la liste des Classe
            cmbClasse.ItemsSource = bulletinsSequentielBL.getListCodeClasse(LClasse);

            List<SequenceBE> LSequence = bulletinsSequentielBL.listerToutesLesSequences();
            // ---- Chargement de la liste des Sequences
            cmbSequence.ItemsSource = bulletinsSequentielBL.getListCodeSequence2(LSequence);

            txtAnnee.Text = Convert.ToString(bulletinsSequentielBL.getAnneeEnCours());
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

                listeEleve = bulletinsSequentielBL.listeEleves(classeBE, Convert.ToInt32(txtAnnee.Text));


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
        public void imprimerBulletinSequentiel(int annee, string classe, string sequence)
        {

            Document doc = new Document(iTextSharp.text.PageSize.A4, 5, 5, 5, 25);
            CreerEtat etat = new CreerEtat();
            etat.docname = ConnexionUI.DOSSIER_BULLETINS + annee + "-" + classe + "-" + sequence + ".pdf";
            etat.title = "BULLETIN DE NOTES DE LA SEQUENCE " + sequence;
            PdfWriter writer = PdfWriter.GetInstance(doc, new System.IO.FileStream(etat.docname, System.IO.FileMode.Create));
            Ecole.DataAccess.ParametresDA parametreDA = new Ecole.DataAccess.ParametresDA();
            ParametresBE parametre = parametreDA.rechercherDerniereValeur();
            writer.PageEvent = new PDFFooter1(parametre.nomEcole);
            doc.Open();

            //on vérifit si tous les champs ont été corectement rempli
            if ((cmbClasse.Text != null && txtAnnee.Text != null && cmbSequence.Text != null) && (cmbClasse.Text != "" && txtAnnee.Text != "" && cmbSequence.Text != ""))
            {
                //si on a choisi <Toutes les classes>
                    if (cmbSequence.Text.Equals("<Toutes Les Séquences>"))
                    {
                        //--------------------- Action pour une classe particulière et toutes les Séquences
                        List<SequenceBE> LSequence = bulletinsSequentielBL.listerToutesLesSequences();

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

                        for (int i = 0; i < listBoxEleve.Items.Count; i++)
                        {
                            for (int k = 0; k < LSequence.Count; k++)
                            {
                                ListBoxItem item = listBoxEleve.ItemContainerGenerator.ContainerFromIndex(i) as ListBoxItem;

                                if (item != null)
                                {
                                    CheckBox myCheckBox = FindVisualChild<CheckBox>(item) as CheckBox;
                                    if (myCheckBox.IsChecked.Value)
                                    {
                                        // bulletinsSequentielBL.genererBulletinSequentielDunEleve((listBoxEleve.Items[i] as ElementListeEleve).matricule, annee, classe, sequence);
                                        string matricule = (listBoxEleve.Items[i] as ElementListeEleve).matricule;
                                        EleveBE eleve = new EleveBE();
                                        eleve.matricule = matricule;
                                        eleve = bulletinsSequentielBL.rechercherEleve(eleve);
                                        bulletinsSequentielBL.genererBulletinSequentielDunEleve(doc, etat, writer, (listBoxEleve.Items[i] as ElementListeEleve).matricule, Convert.ToInt16(txtAnnee.Text), 
                                            classe, LSequence.ElementAt(k).codeseq, eleve.photo);

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

                        //ClasseBE classe = new ClasseBE();
                        //classe.codeClasse = cmbClasse.Text;
                        //classe = bulletinsSequentielBL.rechercherClasse(classe);

                        //on liste les élèves de la classe
                        for (int i = 0; i < listBoxEleve.Items.Count; i++)
                        {
                                ListBoxItem item = listBoxEleve.ItemContainerGenerator.ContainerFromIndex(i) as ListBoxItem;

                                if (item != null)
                                {
                                    CheckBox myCheckBox = FindVisualChild<CheckBox>(item) as CheckBox;
                                    if (myCheckBox.IsChecked.Value)
                                    {
                                        // bulletinsSequentielBL.genererBulletinSequentielDunEleve((listBoxEleve.Items[i] as ElementListeEleve).matricule, annee, classe, sequence);
                                        string matricule = (listBoxEleve.Items[i] as ElementListeEleve).matricule;
                                        EleveBE eleve = new EleveBE();
                                        eleve.matricule = matricule;
                                        eleve = bulletinsSequentielBL.rechercherEleve(eleve);
                                        bulletinsSequentielBL.genererBulletinSequentielDunEleve(doc, etat, writer, matricule, Convert.ToInt16(txtAnnee.Text), classe, cmbSequence.Text, eleve.photo);

                                        value += 1;

                                        Dispatcher.Invoke(updatePbDelegate,
                                            System.Windows.Threading.DispatcherPriority.Background,
                                            new object[] { ProgressBar.ValueProperty, value });
                                    }

                                }
                        }

                        //calcul des résultats
                        //remplissage de la table "resultats"

                        //genererResultatsTrimestrielsBL.calculerResultatsTrimestriels(cmbClasse.Text, cmbTrimestre.Text, Convert.ToInt16(txtAnnee.Text));

                        MessageBox.Show("Opération Terminée !! ");

                        //on cache la barre de progression
                        ProgressBar1.Visibility = System.Windows.Visibility.Hidden;
                    }


            }
            else MessageBox.Show("Tous les champs doivent êtres remplis !! ");

            try
            {
                doc.Close();
            }
            catch (Exception) { }
            System.Diagnostics.Process.Start(System.IO.Path.GetFullPath(etat.docname));
            //etat.SendToPrinter(System.IO.Path.GetFullPath(etat.docname));
        }


        //---------lister tous les menus de la fenetre principale-----------

        //public void listerMenu(Menu menu)
        //{
        //    int i = 0;
        //    int j;
        //    List<MenuItem> listeMenu = new List<MenuItem>();
        //    listeMenu = frmApp.parcourMenu(menu);
        //    foreach (MenuItem menuEnCour in listeMenu)
        //    {

        //        MessageBox.Show("Titre menu = " + menuEnCour.Header.ToString() + " || Nom= " + menuEnCour.Name.ToString());
        //        i = i + 1;

        //    }

        //}

        //---------formater le nom du menu a afficher dans la liste-----------

        //public String formaterNomMenuAffiche(MenuItem eltMenu)
        //{

        //    int i, niveauMenu;
        //    String tabulation = "..........";
        //    String newNomMenu;

        //    newNomMenu = eltMenu.Header.ToString();
        //    niveauMenu = hierarchieMenu(eltMenu.Name.ToString()); //recupérer la hierarchie du menu en cours
        //    for(i=0; i<niveauMenu; i++)
        //    {
        //        newNomMenu = tabulation + newNomMenu;
        //    }


        //    return newNomMenu;
        //}

        //---------retrouver le nom du menu à partir de son text affiché dans le listBox avec des (.........)---

        //public String deformaterNomMenuAffiche(String nomFormate)
        //{

        //    int i, position = 0;
        //    String nomDeformate;
        //    for (i = 0; i < nomFormate.Length; i++)
        //    {
        //        if (nomFormate.Substring(i, 1).Equals("."))
        //            position = position + 1;
        //    }


        //    return nomFormate.Substring(position);
        //}


        //---récupérer les chekbox cochés--------------MOI-----------------------
        //private void btnEnregistrer_Click(object sender, RoutedEventArgs e)
        //{

        //    gestionGroupePrivilegeBL.supprimerTousLesPrivilegeDunRole(listBoxEleve.Text.ToString());
        //    EnregistrerLesPrivilegeDuRole(listBoxEleve.Text.ToString());

        //    MessageBox.Show("Enregistrement éffectué avec succès","School Brain");
        //    cmbClasse.Text = "";
        //    txtAnneeScolaire.Text = "";
        //    decocherTousLesMenu();
        //    chBxTousCocher.IsChecked = false;

        //}

        //---cocher un chekbox sélectionné------------------------------

        // private void btnSelectionner_Click(object sender, RoutedEventArgs e)
        //{
        //    for (int i = 0; i < lbRole.Items.Count; i++)
        //    {
        //        ListBoxItem item = lbRole.ItemContainerGenerator.ContainerFromIndex(i) as ListBoxItem;

        //        if (item != null)
        //        {
        //            CheckBox myCheckBox = FindVisualChild<CheckBox>(item) as CheckBox;

        //            if (myCheckBox.Content.ToString() == "Edition")
        //            {
        //                myCheckBox.IsChecked = true;

        //            }

        //        }
        //    }
        //}

        
        //----------Cocher les menus d'un role--------------------------------
        //private void cocherLesPrivilegeDunRole(String role)
        //{
        //    List<String> listPrivilege = new List<string>();
        //    listPrivilege = gestionGroupePrivilegeBL.listerPrivilegeDunRole(role);

        //    String t, d;

        //    for (int j = 0; j < listPrivilege.Count; j++)
        //    {

        //        for (int i = 0; i < lbRole.Items.Count; i++)
        //        {
        //            ListBoxItem item = lbRole.ItemContainerGenerator.ContainerFromIndex(i) as ListBoxItem;

        //            if (item != null)
        //            {
        //                CheckBox myCheckBox = FindVisualChild<CheckBox>(item) as CheckBox;

        //                t=deformaterNomMenuAffiche(myCheckBox.Content.ToString());
        //                d = listPrivilege.ElementAt(j);

        //                if (deformaterNomMenuAffiche(myCheckBox.Content.ToString()) == listPrivilege.ElementAt(j))
        //                {
        //                    myCheckBox.IsChecked = true;



        //                }
        //                //if (myCheckBox.Content.ToString() == listPrivilege.ElementAt(j))
        //                //{
        //                //    myCheckBox.IsChecked = true;

        //                //}

        //            }
        //        }
        //    }
        //}

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
            {
                txtAnneeScolaire.Text = (Convert.ToInt32(txtAnnee.Text) - 1).ToString() + "/" + txtAnnee.Text.ToString();

                cmbClasse_DropDownClosed(sender, e);
            }
        }

        private void cmbClasse_DropDownClosed(object sender, EventArgs e)
        {
            listBoxEleve.ItemsSource = null;
            afficherEleveDansListBox(cmbClasse.Text, txtAnnee.Text);
            cocherTousLesMenu();
        }

        private void btnImprimer_Click(object sender, RoutedEventArgs e)
        {
            imprimerBulletinSequentiel(Convert.ToInt32(txtAnnee.Text.ToString()), cmbClasse.Text.ToString(), cmbSequence.Text.ToString());
        }

        //-----------fin arbre visuel---------------------------------

    }


    public class ElementListeEleve
    {
        public string matricule { get; set; }
        public String nom { get; set; }
        public string chaineAffichee { get; set; }
    }

   
}

