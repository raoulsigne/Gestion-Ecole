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
    /// Interaction logic for WindowAddEDitSerie.xaml
    /// </summary>
    public partial class WindowAddEDitSerieUI : Window
    {
        private CreerModifierSerieBL creerModifierSerieBL;
        private int etat; // idique si nous sommes en création (0) ou en modification (1)

        private SerieBE ancienObjet; //garde l'ancien état de l'objet, qui sera utilisé pour la modification


        // Définition d'une liste 'ListeSeries' observable de 'Série'
        public ObservableCollection<SerieBE> ListeSeries { get; set; }

        // Fonction permettant de remplir le DataGrid avec les informations de la base de données
        // @param : - listObjet : la liste des objets à afficher dans le DataGrid
        public void RemplirDataGrid(List<SerieBE> listObjet)
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
                    dr["code"] = listObjet.ElementAt(i).codeserie;
                    dr["nom"] = listObjet.ElementAt(i).nomserie;
                    table.Rows.Add(dr);
                }
            }

            string vCode = "";
            string vNom = "";

            ListeSeries.Clear();

            //Personnes_Table = LoadDataTable();

            foreach (DataRow row in table.Rows)
            {
                vCode = Convert.ToString(row["code"]);
                vNom = Convert.ToString(row["nom"]);

                ListeSeries.Add(new SerieBE(vCode, vNom));

            }
        }

        public WindowAddEDitSerieUI()
        {
            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";
            Thread.CurrentThread.CurrentCulture = ci;

            InitializeComponent();

            creerModifierSerieBL = new CreerModifierSerieBL();

            etat = 0;

            ancienObjet = new SerieBE();

            // A mettre pour que le binding avec le DataGrid fonctionne !
            grdListeSerie.DataContext = this;

            // Initialisation de la collection, qui va s'afficher dans la DataGrid :
            ListeSeries = new ObservableCollection<SerieBE>();
            List<SerieBE> LSerieBE = creerModifierSerieBL.listerToutesLesSeries();
            // on met la liste "LSerieBE" dans le DataGrid
            RemplirDataGrid(LSerieBE);

            // ------------------- Chargement de la liste des codes de série dans le comboBox de la fenêtre 
            //(utile pour le filtre)
            cmbFilterCode.ItemsSource = creerModifierSerieBL.getListCodeSerie(LSerieBE);

            // ------------------- Chargement de la liste des noms de série dans le comboBox de la fenêtre 
            //(utile pour le filtre)
            cmbFilterNom.ItemsSource = creerModifierSerieBL.getListNomSerie(LSerieBE);
        }


        private void cmdFermer_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cmdValider_Click(object sender, RoutedEventArgs e)
        {
            if ((txtCode.Text != null && txtNom.Text != null) && (txtCode.Text != "" && txtNom.Text != ""))
            {
                SerieBE serie = new SerieBE();
                serie.codeserie = txtCode.Text;
                serie.nomserie = txtNom.Text;
                if (etat == 1)
                {
                    creerModifierSerieBL.modifierSerie(ancienObjet, serie);
                    List<SerieBE> LSerieBE = creerModifierSerieBL.listerToutesLesSeries();
                    // on met la liste "LSerieBE" dans le DataGrid
                    RemplirDataGrid(LSerieBE);

                    // ------------------- Chargement de la liste des codes de série dans le comboBox de la fenêtre 
                    //(utile pour le filtre)
                    cmbFilterCode.ItemsSource = creerModifierSerieBL.getListCodeSerie(LSerieBE);

                    // ------------------- Chargement de la liste des noms de série dans le comboBox de la fenêtre 
                    //(utile pour le filtre)
                    cmbFilterNom.ItemsSource = creerModifierSerieBL.getListNomSerie(LSerieBE);

                    txtCode.Text = "";
                    txtNom.Text = "";

                    etat = 0;
                }
                else if (creerModifierSerieBL.rechercherSerie(serie) == null)
                {

                    if (creerModifierSerieBL.creerSerie(txtCode.Text, txtNom.Text))
                    {
                        MessageBox.Show("Enregistrement Série [" + txtCode.Text + ", " + txtNom.Text + "] " + " : Opération réussie");
                        //on rafraichit les champs du formulaire
                        txtCode.Text = "";
                        txtNom.Text = "";

                        List<SerieBE> LSerieBE = creerModifierSerieBL.listerToutesLesSeries();
                        //on rafraichir le DataGrid
                        RemplirDataGrid(LSerieBE);
                        // ------------------- Chargement de la liste des codes de série dans le comboBox de la fenêtre 
                        //(utile pour le filtre)
                        cmbFilterCode.ItemsSource = creerModifierSerieBL.getListCodeSerie(LSerieBE);

                        // ------------------- Chargement de la liste des noms de série dans le comboBox de la fenêtre 
                        //(utile pour le filtre)
                        cmbFilterNom.ItemsSource = creerModifierSerieBL.getListNomSerie(LSerieBE);
                    }
                    else MessageBox.Show("Echec enregistrement : une erreure est survenue !");
                }
                else MessageBox.Show("Une Série ayant le code \"" + txtCode.Text + "\" existe deja dans le système \n \n Veuillez changer de code SVP !");
            }
            else MessageBox.Show("Tous les champs doivent pas être remplis !");
        }

        private void cmbFilterCode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // on filtre la liste affiché dans le DataGrid en fonction du choix de l'utilisateur
            List<SerieBE> LSerieBE;
            if (cmbFilterNom.Text != null && cmbFilterNom.Text != "")
            {
                if (cmbFilterCode.SelectedItem.Equals("<Tous les Codes>"))
                {
                    if (cmbFilterNom.SelectedItem.Equals("<Tous les Noms>"))
                        LSerieBE = creerModifierSerieBL.listerToutesLesSeries();
                    else
                        LSerieBE = creerModifierSerieBL.listerSerieSuivantCritere("nomserie = '" + cmbFilterNom.SelectedItem + "'");
                }
                else
                    if (cmbFilterNom.SelectedItem.Equals("<Tous les Noms>"))
                        LSerieBE = creerModifierSerieBL.listerSerieSuivantCritere("codeserie = '" + cmbFilterCode.SelectedItem + "'");
                    else
                        LSerieBE = creerModifierSerieBL.listerSerieSuivantCritere("codeserie = '" + cmbFilterCode.SelectedItem + "' AND nomserie = '" + cmbFilterNom.SelectedItem + "'");
            }
            else
                if (cmbFilterCode.SelectedItem.Equals("<Tous les Codes>"))
                    LSerieBE = creerModifierSerieBL.listerToutesLesSeries();
                else
                    LSerieBE = creerModifierSerieBL.listerSerieSuivantCritere("codeserie = '" + cmbFilterCode.SelectedItem + "'");

            //on rafraichir le DataGrid
            RemplirDataGrid(LSerieBE);
        }

        private void cmbFilterNom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // on filtre la liste affiché dans le DataGrid en fonction du choix de l'utilisateur
            List<SerieBE> LSerieBE;
            if (cmbFilterCode.Text != null && cmbFilterCode.Text != "")
            {
                if (cmbFilterNom.SelectedItem.Equals("<Tous les Noms>"))
                {
                    if (cmbFilterCode.SelectedItem.Equals("<Tous les Codes>"))
                        LSerieBE = creerModifierSerieBL.listerToutesLesSeries();
                    else
                        LSerieBE = creerModifierSerieBL.listerSerieSuivantCritere("codeserie = '" + cmbFilterCode.SelectedItem + "'");
                }
                else
                    if (cmbFilterCode.SelectedItem.Equals("<Tous les Codes>"))
                        LSerieBE = creerModifierSerieBL.listerSerieSuivantCritere("nomserie = '" + cmbFilterNom.SelectedItem + "'");
                    else
                        LSerieBE = creerModifierSerieBL.listerSerieSuivantCritere("codeserie = '" + cmbFilterCode.SelectedItem + "' AND nomserie = '" + cmbFilterNom.SelectedItem + "'");
            }
            else
                if (cmbFilterNom.SelectedItem.Equals("<Tous les Noms>"))
                    LSerieBE = creerModifierSerieBL.listerToutesLesSeries();
                else
                    LSerieBE = creerModifierSerieBL.listerSerieSuivantCritere("nomserie = '" + cmbFilterNom.SelectedItem + "'");

            //on rafraichir le DataGrid
            RemplirDataGrid(LSerieBE);
        }



        private void grdListeSerie_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                if (grdListeSerie.SelectedIndex != -1)
                {
                    if (MessageBox.Show("Voulez-vous vraiment le supprimer ? ", "School : Confimation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {

                        if (creerModifierSerieBL.supprinerSerie(ListeSeries.ElementAt(grdListeSerie.SelectedIndex)))
                            ListeSeries.RemoveAt(grdListeSerie.SelectedIndex);
                        grdListeSerie.ItemsSource = ListeSeries;

                        // ------------------- Chargement de la liste des codes de série dans le comboBox de la fenêtre 
                        //(utile pour le filtre)
                        List<SerieBE> LSerieBE = creerModifierSerieBL.listerToutesLesSeries();
                        cmbFilterCode.ItemsSource = creerModifierSerieBL.getListCodeSerie(LSerieBE);

                        // ------------------- Chargement de la liste des noms de série dans le comboBox de la fenêtre 
                        //(utile pour le filtre)
                        cmbFilterNom.ItemsSource = creerModifierSerieBL.getListNomSerie(LSerieBE);
                    }

                    grdListeSerie.UnselectAll();
                }

            }
        }

        private void grdListeSerie_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (grdListeSerie.SelectedIndex != -1)
            {
                etat = 1;
                SerieBE serie = new SerieBE();

                serie = creerModifierSerieBL.rechercherSerie(ListeSeries.ElementAt(grdListeSerie.SelectedIndex));
                if (serie != null)
                {
                    // on charge les informations dans le formulaire
                    txtCode.Text = serie.codeserie;
                    txtNom.Text = serie.nomserie;

                    ancienObjet = serie;
                }

                grdListeSerie.UnselectAll();

            }

        }

        private void cmdAnnuler_Click(object sender, RoutedEventArgs e)
        {
            txtCode.Text = "";
            txtNom.Text = "";
            etat = 0;
        }

        private void cmdImprimer_Click(object sender, RoutedEventArgs e)
        {
            CreerEtat etat = new CreerEtat("série -" + DateTime.Today.ToShortDateString(), "Liste des Séries");
            etat.obtenirEtat(grdListeSerie);
        }


    }
}
