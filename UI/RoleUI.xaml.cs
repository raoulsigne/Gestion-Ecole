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

namespace Ecole
{
    /// <summary>
    /// Interaction logic for listeRole.xaml
    /// </summary>
    public partial class listeRole : Window
    {
        int nbTabulation;
        MainWindow frmApp = new MainWindow();
        GestionGroupeBL gestionGroupeBL;
        GestionGroupePrivilegeBL gestionGroupePrivilegeBL;
        GestionPrivilegeBL gestionPrivilegeBL;
        


        public listeRole()
        {
            nbTabulation = 0;

            InitializeComponent();
            gestionGroupeBL = new GestionGroupeBL();
            gestionGroupePrivilegeBL = new GestionGroupePrivilegeBL();
            gestionPrivilegeBL = new GestionPrivilegeBL();

            SauvegarderMenuDansPrivilege();
            afficherMenuDansListBox();
            lbRole.SelectionMode = SelectionMode.Multiple;

            //on charge les roles dans le combobox
            List<GroupeBE> listGroupeBE = gestionGroupeBL.listerTousLesGroupe();
            List<string> listeDesRole = new List<string>();
            listeDesRole = gestionGroupeBL.ListerRoleGroupe(listGroupeBE);
            cmbRole.ItemsSource = listeDesRole;
            

            
        }

        //---------Afficher les éléments du menu de la fenetre principale dans le listBox-----------

        public void afficherMenuDansListBox()
        {
            int i = 0;
            int j;
            
            List<ElementMenu> items = new List<ElementMenu>();

            List<MenuItem> listeMenu = new List<MenuItem>();
            listeMenu = frmApp.parcourMenu(frmApp.menuTest);

            foreach (MenuItem menuEnCour in listeMenu)
            {

               items.Add(new ElementMenu() { nomElement = menuEnCour.Name.ToString(), 
                                             textElement =menuEnCour.Header.ToString(), 
                                             newTextElement=formaterNomMenuAffiche(menuEnCour),
                                             sonMenuItem=menuEnCour});
            }

            lbRole.ItemsSource = items;

           

        }

        //---------Sauvegarder les éléments du menu dans la table des privileges-----------

        public void SauvegarderMenuDansPrivilege()
        {
            int i = 0;
            int j;
            PrivilegeBE privilege; 
            List<ElementMenu> items = new List<ElementMenu>();

            List<MenuItem> listeMenu = new List<MenuItem>();
            listeMenu = frmApp.parcourMenu(frmApp.menuTest);
            //gestionPrivilegeBL.supprimerTousPrivileges();

            foreach (MenuItem menuEnCour in listeMenu)
            {

                privilege = new PrivilegeBE(menuEnCour.Name.ToString(), menuEnCour.Header.ToString());
                if (!gestionPrivilegeBL.rechercherPrivillege(privilege))
                    gestionPrivilegeBL.ajouterPrivilege(privilege);
                
                //items.Add(new ElementMenu()
                //{
                //    nomElement = menuEnCour.Name.ToString(),
                //    textElement = menuEnCour.Header.ToString(),
                //    newTextElement = formaterNomMenuAffiche(menuEnCour),
                //    sonMenuItem = menuEnCour
                //});
            }

           // lbRole.ItemsSource = items;



        }

        
        //---------lister tous les menus de la fenetre principale-----------

        public void listerMenu(Menu menu)
        {
            int i = 0;
            int j;
            List<MenuItem> listeMenu = new List<MenuItem>();
            listeMenu = frmApp.parcourMenu(menu);
            foreach (MenuItem menuEnCour in listeMenu)
            {

                MessageBox.Show("Titre menu = " + menuEnCour.Header.ToString() + " || Nom= " + menuEnCour.Name.ToString());
                i = i + 1;

            }

        }

        //---------formater le nom du menu a afficher dans la liste-----------

        public String formaterNomMenuAffiche(MenuItem eltMenu)
        {
           
            int i, niveauMenu;
            String tabulation = "..........";
            String newNomMenu;

            newNomMenu = eltMenu.Header.ToString();
            niveauMenu = hierarchieMenu(eltMenu.Name.ToString()); //recupérer la hierarchie du menu en cours
            for(i=0; i<niveauMenu; i++)
            {
                newNomMenu = tabulation + newNomMenu;
            }


            return newNomMenu;
        }

        //---------retrouver le nom du menu à partir de son text affiché dans le listBox avec des (.........)---

        public String deformaterNomMenuAffiche(String nomFormate)
        {

            int i,position=0;
            String nomDeformate;
            for (i = 0; i < nomFormate.Length; i++)
            {
                if (nomFormate.Substring(i,1).Equals("."))
                    position = position + 1;
            }


            return nomFormate.Substring(position);
        }

        //---------retourne le niveau hierarchique d'un élément du menu-----------

        public int hierarchieMenu(String nomMenu)
        {
            int i = 0;
            String lettre;
            Boolean continuer = true;

            while (continuer)
            {
                lettre = nomMenu.Substring(i, 1);
                if (lettre == "s")
                    i = i + 1;

                else
                    continuer = false;

            }
            return i;
        }
         



    //---récupérer les chekbox cochés--------------MOI-----------------------
       private void btnEnregistrer_Click(object sender, RoutedEventArgs e)
       {

           gestionGroupePrivilegeBL.supprimerTousLesPrivilegeDunRole(cmbRole.Text.ToString());
           EnregistrerLesPrivilegeDuRole(cmbRole.Text.ToString());

           MessageBox.Show("Enregistrement éffectué avec succès","School Brain");
           cmbRole.Text = "";
           txtDescription.Text = "";
           decocherTousLesMenu();
           chBxTousCocher.IsChecked = false;
           
       }

       //---cocher un chekbox sélectionné------------------------------
        
        private void btnFermer_Click(object sender, RoutedEventArgs e)
       {
           this.Close();
       }

       //-------------------Enregistrer les privileges du role---------------
        public void EnregistrerLesPrivilegeDuRole(String role)
        {
            GroupePrivilegeBE gp;
            for (int i = 0; i < lbRole.Items.Count; i++)
            {
                ListBoxItem item = lbRole.ItemContainerGenerator.ContainerFromIndex(i) as ListBoxItem;

                if (item != null)
                {
                    CheckBox myCheckBox = FindVisualChild<CheckBox>(item) as CheckBox;
                    if (myCheckBox.IsChecked.Value)
                    {
                        gp = new GroupePrivilegeBE((lbRole.Items[i] as ElementMenu).nomElement.ToString(), role, 2015);
                        if (gestionGroupePrivilegeBL.ajouterGroupePrivilege(gp))
                            continue;
                        
                       // MessageBox.Show("Code = " + (lbRole.Items[i] as ElementMenu).nomElement + " || Nom= " + (lbRole.Items[i] as ElementMenu).textElement);
                        
                    }

                }
            }
        }
        
        //----------Cocher les menus d'un role--------------------------------
        private void cocherLesPrivilegeDunRole(String role)
        {
            List<String> listPrivilege = new List<string>();
            listPrivilege = gestionGroupePrivilegeBL.listerPrivilegeDunRole(role);

            String t, d;

            for (int j = 0; j < listPrivilege.Count; j++)
            {
                
                for (int i = 0; i < lbRole.Items.Count; i++)
                {
                    ListBoxItem item = lbRole.ItemContainerGenerator.ContainerFromIndex(i) as ListBoxItem;
                    
                    if (item != null)
                    {
                        CheckBox myCheckBox = FindVisualChild<CheckBox>(item) as CheckBox;

                        t=deformaterNomMenuAffiche(myCheckBox.Content.ToString());
                        d = listPrivilege.ElementAt(j);

                        if (deformaterNomMenuAffiche(myCheckBox.Content.ToString()) == listPrivilege.ElementAt(j))
                        {
                            myCheckBox.IsChecked = true;
                            
                          
                            
                        }
                        //if (myCheckBox.Content.ToString() == listPrivilege.ElementAt(j))
                        //{
                        //    myCheckBox.IsChecked = true;

                        //}

                    }
                }
            }
        }

       //------fin cocher les menus du role----------------------------

        //---------décocher tous les menus-------------------------

       private void decocherTousLesMenu()
        {
            for (int i = 0; i < lbRole.Items.Count; i++)
            {
                ListBoxItem item = lbRole.ItemContainerGenerator.ContainerFromIndex(i) as ListBoxItem;

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
            for (int i = 0; i < lbRole.Items.Count; i++)
            {
                ListBoxItem item = lbRole.ItemContainerGenerator.ContainerFromIndex(i) as ListBoxItem;

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

       private void lbRole_SelectionChanged(object sender, SelectionChangedEventArgs e)
       {
           for (int i = 0; i < lbRole.Items.Count; i++)
           {
               ListBoxItem item = lbRole.ItemContainerGenerator.ContainerFromIndex(i) as ListBoxItem;

               if (item != null)
               {
                   CheckBox myCheckBox = FindVisualChild<CheckBox>(item) as CheckBox;

                   if ( myCheckBox.IsChecked == true)
                   {
                       lbRole.SelectedItems.Add(item);

                   }

               }
           }
       }


   

       private void chBxRole_Checked(object sender, RoutedEventArgs e)
       {

           var cb = sender as CheckBox;
           var item = cb.DataContext;
           lbRole.SelectedItems.Add(item);
        

       }

   private void chBxRole_Unchecked(object sender, RoutedEventArgs e)
   {
       var cb = sender as CheckBox;
       var item = cb.DataContext;
       lbRole.SelectedItems.Remove(item);
  
   }

   private void cmbRole_DropDownClosed(object sender, EventArgs e)
   {
       txtDescription.Text  = gestionGroupeBL.getDescriptionRole(cmbRole.Text.ToString());
       decocherTousLesMenu();
       chBxTousCocher.IsChecked = false;
       cocherLesPrivilegeDunRole(cmbRole.Text.ToString()); 
   }

   private void chBxTousCocher_Checked(object sender, RoutedEventArgs e)
   {
       cocherTousLesMenu();
   }

   private void chBxTousCocher_Unchecked(object sender, RoutedEventArgs e)
   {
       decocherTousLesMenu();
   }

  
       

//-----------fin arbre visuel---------------------------------

     }


  public class ElementMenu
    {
        public string nomElement { get; set; }
        public String textElement { get; set; }
        public String newTextElement { get; set; }
        public MenuItem sonMenuItem { get; set; }
       
    }

    public class TodoItem
    {
        public string nomRole { get; set; }
        public String codeRole { get; set; }

        //public TodoItem(string nom, string role)
        //{
        //    nomRole = nom;
        //    codeRole = role;
        //}
    }
}

