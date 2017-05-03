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

using System.Globalization;
using System.Threading;

using Ecole.BusinessEntity;
using Ecole.BusinessLogic;
using Ecole.Utilitaire;

namespace Ecole.UI
{
    /// <summary>
    /// Interaction logic for WindowAddEditCategorieArticle.xaml
    /// </summary>
    public partial class WindowAddEditCategorieArticleUI : Window
    {
        private CreerModifierCategorieArticleBL creerModifierCategorieArticleBL;
        private int etat; // idique si nous sommes en création (0) ou en modification (1)

        private CategorieArticleBE ancienObjet; // garde l'ancien objet de Catégorie Article (utile pour la mise à jour)

        // Définition d'une liste 'ListeCategorieArticle' observable de 'Série'
        public ObservableCollection<CategorieArticleBE> ListeCategorieArticle { get; set; }

        // Fonction permettant de remplir le DataGrid avec les informations de la base de données
        // @param : - listObjet : la liste des objets à afficher dans le DataGrid
        public void RemplirDataGrid(List<CategorieArticleBE> listObjet)
        {
            // Ajout de données dans la DataTable :
            var table = new DataTable();

            table.Columns.Add(new DataColumn("code", typeof(string)));
            table.Columns.Add(new DataColumn("nom", typeof(string)));

            if (listObjet != null)
            {

                for (int i = 0; i < listObjet.Count; i++)
                {
                    DataRow dr = table.NewRow();
                    dr["code"] = listObjet.ElementAt(i).codeCatArticle;
                    dr["nom"] = listObjet.ElementAt(i).nomCatArticle;
                    table.Rows.Add(dr);
                }
            }

            string vCode = "";
            string vNom = "";

            ListeCategorieArticle.Clear();

            //Personnes_Table = LoadDataTable();

            foreach (DataRow row in table.Rows)
            {
                vCode = Convert.ToString(row["code"]);
                vNom = Convert.ToString(row["nom"]);

                ListeCategorieArticle.Add(new CategorieArticleBE(vCode, vNom));

            }
        }

        
        public WindowAddEditCategorieArticleUI()
        {
            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";
            Thread.CurrentThread.CurrentCulture = ci;

            InitializeComponent();

            creerModifierCategorieArticleBL = new CreerModifierCategorieArticleBL();

            etat = 0;

            ancienObjet = new CategorieArticleBE();

            // A mettre pour que le binding avec le DataGrid fonctionne !
            grdCategorieArticle.DataContext = this;

            // Initialisation de la collection, qui va s'afficher dans la DataGrid :
            ListeCategorieArticle = new ObservableCollection<CategorieArticleBE>();
            List<CategorieArticleBE> LCategorieArticleBE = creerModifierCategorieArticleBL.listerTousLesCategorieArticle();
            // on met la liste "LSerieBE" dans le DataGrid
            RemplirDataGrid(LCategorieArticleBE);

            // ------------------- Chargement de la liste des codes de Niveau dans le comboBox de la fenêtre 
            //(utile pour le filtre)
            cmbFilterCode.ItemsSource = creerModifierCategorieArticleBL.getListCodeCategorieArticle(LCategorieArticleBE);

            // ------------------- Chargement de la liste des noms de Niveau dans le comboBox de la fenêtre 
            //(utile pour le filtre)
            cmbFilterNom.ItemsSource = creerModifierCategorieArticleBL.getListNomCategorieArticle(LCategorieArticleBE);
        }

        private void cmdFermer_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cmdValider_Click(object sender, RoutedEventArgs e)
        {
            if ((txtCode.Text != null && txtNom.Text != null) && (txtCode.Text != "" && txtNom.Text != ""))
            { // si tous les champs sont non vides

                CategorieArticleBE categorieArticle = new CategorieArticleBE();
                categorieArticle.codeCatArticle = txtCode.Text;
                categorieArticle.nomCatArticle = txtNom.Text;

                if (etat == 1)
                {
                    creerModifierCategorieArticleBL.modifierCategorieArticle(ancienObjet, categorieArticle);
                    List<CategorieArticleBE> LCategorieArticleBE = creerModifierCategorieArticleBL.listerTousLesCategorieArticle();
                    // on met la liste "LCategorieArticleBE" dans le DataGrid
                    RemplirDataGrid(LCategorieArticleBE);

                    // ------------------- Chargement de la liste des codes de Catégorie Article dans le comboBox de la fenêtre 
                    //(utile pour le filtre)
                    cmbFilterCode.ItemsSource = creerModifierCategorieArticleBL.getListCodeCategorieArticle(LCategorieArticleBE);

                    // ------------------- Chargement de la liste des noms de série dans le comboBox de la fenêtre 
                    //(utile pour le filtre)
                    cmbFilterNom.ItemsSource = creerModifierCategorieArticleBL.getListNomCategorieArticle(LCategorieArticleBE);

                    txtCode.Text = "";
                    txtNom.Text = "";

                    etat = 0;
                }
                else if (creerModifierCategorieArticleBL.rechercherCategorieArticle(new CategorieArticleBE(txtCode.Text, txtNom.Text)) == null)
                { // si une catégorie d'article possédant le même code n'existe pas deja dans la BD

                    if (creerModifierCategorieArticleBL.creerCategorieArticle(txtCode.Text, txtNom.Text))
                    { // si l'nregistrement a réussi

                        MessageBox.Show("Opération réussie");
                        txtCode.Text = "";
                        txtNom.Text = "";

                        // Initialisation de la collection, qui va s'afficher dans la DataGrid :
                        List<CategorieArticleBE> LCategorieArticleBE = creerModifierCategorieArticleBL.listerTousLesCategorieArticle();

                        // on met la liste "LCategorieArticleBE" dans le DataGrid
                        RemplirDataGrid(LCategorieArticleBE);

                        // ------------------- Chargement de la liste des codes de Catégorie Article dans le comboBox de la fenêtre 
                        //(utile pour le filtre)
                        cmbFilterCode.ItemsSource = creerModifierCategorieArticleBL.getListCodeCategorieArticle(LCategorieArticleBE);

                        // ------------------- Chargement de la liste des noms de série dans le comboBox de la fenêtre 
                        //(utile pour le filtre)
                        cmbFilterNom.ItemsSource = creerModifierCategorieArticleBL.getListNomCategorieArticle(LCategorieArticleBE);
                    }
                    else MessageBox.Show("Echec enregistrement : une erreure est survenue !");
                }
                else MessageBox.Show("Une Catégorie d'article ayant le code \"" + txtCode.Text + "\" existe deja dans le système \n \n Veuillez changer de code SVP !");
            }
            else MessageBox.Show("Tous les champs doivent pas être remplis !");
        }

        private void cmbFilterCode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // on filtre la liste affiché dans le DataGrid en fonction du choix de l'utilisateur
            List<CategorieArticleBE> LCategorieArticleBE;
            if (cmbFilterNom.Text != null && cmbFilterNom.Text != "")
            {
                if (cmbFilterCode.SelectedItem.Equals("<Tous les Codes>"))
                {
                    if (cmbFilterNom.SelectedItem.Equals("<Tous les Noms>"))
                        LCategorieArticleBE = creerModifierCategorieArticleBL.listerTousLesCategorieArticle();
                    else
                        LCategorieArticleBE = creerModifierCategorieArticleBL.listerCategorieArticleSuivantCritere("nomcatarticle = '" + cmbFilterNom.SelectedItem + "'");
                }
                else
                    if (cmbFilterNom.SelectedItem.Equals("<Tous les Noms>"))
                        LCategorieArticleBE = creerModifierCategorieArticleBL.listerCategorieArticleSuivantCritere("codecatarticle = '" + cmbFilterCode.SelectedItem + "'");
                    else
                        LCategorieArticleBE = creerModifierCategorieArticleBL.listerCategorieArticleSuivantCritere("codecatarticle = '" + cmbFilterCode.SelectedItem + "' AND nomcatarticle = '" + cmbFilterNom.SelectedItem + "'");
            }
            else
                if (cmbFilterCode.SelectedItem.Equals("<Tous les Codes>"))
                    LCategorieArticleBE = creerModifierCategorieArticleBL.listerTousLesCategorieArticle();
                else
                    LCategorieArticleBE = creerModifierCategorieArticleBL.listerCategorieArticleSuivantCritere("codecatarticle = '" + cmbFilterCode.SelectedItem + "'");

            //on rafraichir le DataGrid
            RemplirDataGrid(LCategorieArticleBE);
        }

        private void cmbFilterNom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // on filtre la liste affiché dans le DataGrid en fonction du choix de l'utilisateur
            List<CategorieArticleBE> LCategorieArticleBE;
            if (cmbFilterCode.Text != null && cmbFilterCode.Text != "")
            {
                if (cmbFilterNom.SelectedItem.Equals("<Tous les Noms>"))
                {
                    if (cmbFilterCode.SelectedItem.Equals("<Tous les Codes>"))
                        LCategorieArticleBE = creerModifierCategorieArticleBL.listerTousLesCategorieArticle();
                    else
                        LCategorieArticleBE = creerModifierCategorieArticleBL.listerCategorieArticleSuivantCritere("codecatarticle = '" + cmbFilterCode.SelectedItem + "'");
                }
                else
                    if (cmbFilterCode.SelectedItem.Equals("<Tous les Codes>"))
                        LCategorieArticleBE = creerModifierCategorieArticleBL.listerCategorieArticleSuivantCritere("nomcatarticle = '" + cmbFilterNom.SelectedItem + "'");
                    else
                        LCategorieArticleBE = creerModifierCategorieArticleBL.listerCategorieArticleSuivantCritere("codecatarticle = '" + cmbFilterCode.SelectedItem + "' AND nomcatarticle = '" + cmbFilterNom.SelectedItem + "'");
            }
            else
                if (cmbFilterNom.SelectedItem.Equals("<Tous les Noms>"))
                    LCategorieArticleBE = creerModifierCategorieArticleBL.listerTousLesCategorieArticle();
                else
                    LCategorieArticleBE = creerModifierCategorieArticleBL.listerCategorieArticleSuivantCritere("nomcatarticle = '" + cmbFilterNom.SelectedItem + "'");

            //on rafraichir le DataGrid
            RemplirDataGrid(LCategorieArticleBE);
        }

        private void cmdAnnuler_Click(object sender, RoutedEventArgs e)
        {
            txtCode.Text = "";
            txtNom.Text = "";
            etat = 0;
        }

        private void grdCategorieArticle_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                if (grdCategorieArticle.SelectedIndex != -1)
                {
                    if (MessageBox.Show("Voulez-vous vraiment le supprimer ? ", "School : Confimation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        if (creerModifierCategorieArticleBL.supprinerCategorieArticle(ListeCategorieArticle.ElementAt(grdCategorieArticle.SelectedIndex)))
                            ListeCategorieArticle.RemoveAt(grdCategorieArticle.SelectedIndex);
                        grdCategorieArticle.ItemsSource = ListeCategorieArticle;

                        // ------------------- Chargement de la liste des codes de Catégorie article dans le comboBox de la fenêtre 
                        //(utile pour le filtre)
                        List<CategorieArticleBE> LCategorieArticleBE = creerModifierCategorieArticleBL.listerTousLesCategorieArticle();
                        cmbFilterCode.ItemsSource = creerModifierCategorieArticleBL.getListCodeCategorieArticle(LCategorieArticleBE);

                        // ------------------- Chargement de la liste des noms de  Catégorie article dans le comboBox de la fenêtre 
                        //(utile pour le filtre)
                        cmbFilterNom.ItemsSource = creerModifierCategorieArticleBL.getListNomCategorieArticle(LCategorieArticleBE);

                    }

                    grdCategorieArticle.UnselectAll();
                }
            }
        }

        private void grdCategorieArticle_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (grdCategorieArticle.SelectedIndex != -1)
            {
                etat = 1;
                CategorieArticleBE categorieArticle = new CategorieArticleBE();

                categorieArticle = creerModifierCategorieArticleBL.rechercherCategorieArticle(ListeCategorieArticle.ElementAt(grdCategorieArticle.SelectedIndex));
                if (categorieArticle != null)
                {
                    // on charge les informations dans le formulaire
                    txtCode.Text = categorieArticle.codeCatArticle;
                    txtNom.Text = categorieArticle.nomCatArticle;

                    ancienObjet = categorieArticle;
                }

                grdCategorieArticle.UnselectAll();
            }
        }

        private void cmdImprimer_Click(object sender, RoutedEventArgs e)
        {
            CreerEtat etat = new CreerEtat("Catégorie Article-" + DateTime.Today.ToShortDateString(), "Liste des Catégories d'Articles");
            etat.obtenirEtat(grdCategorieArticle);
        }
    }
}
