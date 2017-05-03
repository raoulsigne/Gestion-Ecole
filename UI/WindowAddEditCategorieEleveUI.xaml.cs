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
    /// Interaction logic for WindowAddEditCategorieEleveUI.xaml
    /// </summary>
    public partial class WindowAddEditCategorieEleveUI : Window
    {
        CreerModifierCategorieEleveBL creerModifierCategorieEleveBL;
        private int etat; // idique si nous sommes en création (0) ou en modification (1)

        private CategorieEleveBE ancienObjet; // garde l'ancien état de l'objet qui sera utilisé pour la modification

        // Définition d'une liste 'ListeCategorieEleves' observable de 'CategorieEleves'
        public ObservableCollection<CategorieEleveBE> ListeCategorieEleves { get; set; }

        // Fonction permettant de remplir le DataGrid avec les informations de la base de données
        // @param : - listObjet : la liste des objets à afficher dans le DataGrid
        public void RemplirDataGrid(List<CategorieEleveBE> listObjet)
        {
            // Ajout de données dans la DataTable :
            var table = new DataTable();

            table.Columns.Add(new DataColumn("codeCatEleve", typeof(string)));
            table.Columns.Add(new DataColumn("nomCatEleve", typeof(string)));

            if (listObjet != null)
            {

                for (int i = 0; i < listObjet.Count; i++)
                {
                    DataRow dr = table.NewRow();
                    dr["codeCatEleve"] = listObjet.ElementAt(i).codeCatEleve;
                    dr["nomCatEleve"] = listObjet.ElementAt(i).nomCatEleve;
                    table.Rows.Add(dr);
                }
            }

            string vCode = "";
            string vNom = "";

            ListeCategorieEleves.Clear();

            //Personnes_Table = LoadDataTable();

            foreach (DataRow row in table.Rows)
            {
                vCode = Convert.ToString(row["codeCatEleve"]);
                vNom = Convert.ToString(row["nomCatEleve"]);

                ListeCategorieEleves.Add(new CategorieEleveBE(vCode, vNom));

            }
        }

        public WindowAddEditCategorieEleveUI()
        {
            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";
            Thread.CurrentThread.CurrentCulture = ci;

            InitializeComponent();

            creerModifierCategorieEleveBL = new CreerModifierCategorieEleveBL();

            etat = 0;

            ancienObjet = new CategorieEleveBE();

            // A mettre pour que le binding avec le DataGrid fonctionne !
            grdListeCategorieEleve.DataContext = this;

            // Initialisation de la collection, qui va s'afficher dans la DataGrid :
            ListeCategorieEleves = new ObservableCollection<CategorieEleveBE>();
            List<CategorieEleveBE> LCategorieEleveBE = creerModifierCategorieEleveBL.listerToutesLesCategorieEleve();
            // on met la liste "LCategorieEleveBE" dans le DataGrid
            RemplirDataGrid(LCategorieEleveBE);

        }

        private void cmdEnregistrer_Click(object sender, RoutedEventArgs e)
        {
            if ((txtCode.Text != null && txtNom.Text != null) && (txtCode.Text != "" && txtNom.Text != ""))
            {
                CategorieEleveBE categorieEleve = new CategorieEleveBE();
                categorieEleve.codeCatEleve = txtCode.Text;
                categorieEleve.nomCatEleve = txtNom.Text;
                if (etat == 1)
                {
                    creerModifierCategorieEleveBL.modifierCategorieEleve(ancienObjet, categorieEleve);
                    List<CategorieEleveBE> LCategorieEleveBE = creerModifierCategorieEleveBL.listerToutesLesCategorieEleve();
                    // on met la liste "LGroupeMatiereBE" dans le DataGrid
                    RemplirDataGrid(LCategorieEleveBE);

                    txtCode.Text = "";
                    txtNom.Text = "";

                    etat = 0;
                }
                else if (creerModifierCategorieEleveBL.rechercherCategorieEleve(categorieEleve) == null)
                {

                    if (creerModifierCategorieEleveBL.creerCategorieEleve(txtCode.Text, txtNom.Text))
                    {
                        MessageBox.Show("Opération réussie");
                        //on rafraichit les champs du formulaire
                        txtCode.Text = "";
                        txtNom.Text = "";

                        List<CategorieEleveBE> LCategorieEleveBE = creerModifierCategorieEleveBL.listerToutesLesCategorieEleve();
                        //on rafraichir le DataGrid
                        RemplirDataGrid(LCategorieEleveBE);
                        
                    }
                    else MessageBox.Show("Echec enregistrement : une erreure est survenue !");
                }
                else MessageBox.Show("Une Catégorie d'élève ayant le code \"" + txtCode.Text + "\" existe deja dans le système \n \n Veuillez changer de code SVP !");
            }
            else MessageBox.Show("Tous les champs doivent pas être remplis !");
        }

        private void grdListeCategorieEleve_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                if (grdListeCategorieEleve.SelectedIndex != -1)
                {
                    if (MessageBox.Show("Voulez-vous vraiment le supprimer ? ", "School : Confimation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {

                        if (creerModifierCategorieEleveBL.supprinerCategorieEleve(ListeCategorieEleves.ElementAt(grdListeCategorieEleve.SelectedIndex)))
                            ListeCategorieEleves.RemoveAt(grdListeCategorieEleve.SelectedIndex);
                        grdListeCategorieEleve.ItemsSource = ListeCategorieEleves;

                    }

                    grdListeCategorieEleve.UnselectAll();
                }
            }
        }

        private void grdListeCategorieEleve_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (grdListeCategorieEleve.SelectedIndex != -1)
            {
                etat = 1;
                CategorieEleveBE categorieEleve = new CategorieEleveBE();

                categorieEleve = creerModifierCategorieEleveBL.rechercherCategorieEleve(ListeCategorieEleves.ElementAt(grdListeCategorieEleve.SelectedIndex));
                if (categorieEleve != null)
                {
                    // on charge les informations dans le formulaire
                    txtCode.Text = categorieEleve.codeCatEleve;
                    txtNom.Text = categorieEleve.nomCatEleve;

                    ancienObjet = categorieEleve;
                }

                grdListeCategorieEleve.UnselectAll();
            }
        }

        private void cmdAnnuler_Click(object sender, RoutedEventArgs e)
        {
            txtCode.Text = "";
            txtNom.Text = "";
            etat = 0;
        }

        private void cmdFermer_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cmdImprimer_Click(object sender, RoutedEventArgs e)
        {
            CreerEtat etat = new CreerEtat("Catégorie Elèves -" + DateTime.Today.ToShortDateString(), "Liste des Catégories d'élèves ");
            etat.obtenirEtat(grdListeCategorieEleve);
        }
    }
}
