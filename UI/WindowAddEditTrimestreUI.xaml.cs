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
    /// Interaction logic for WindowAddEditTrimestreUI.xaml
    /// </summary>
    public partial class WindowAddEditTrimestreUI : Window
    {
        CreerModifierTrimestreBL creerModifierTrimestreBL;
        private int etat; // idique si nous sommes en création (0) ou en modification (1)

        private TrimestreBE ancienObjet; //garde l'ancien état de l'objet, qui sera utilisé pour la modification

        // Définition d'une liste 'ListeTrimestres' observable de 'Trimestre'
        public ObservableCollection<TrimestreBE> ListeTrimestres { get; set; }

        // Fonction permettant de remplir le DataGrid avec les informations de la base de données
        // @param : - listObjet : la liste des objets à afficher dans le DataGrid
        public void RemplirDataGrid(List<TrimestreBE> listObjet)
        {
            // Ajout de données dans la DataTable :
            var table = new DataTable();

            table.Columns.Add(new DataColumn("codetrimestre", typeof(string)));
            table.Columns.Add(new DataColumn("nomtrimestre", typeof(string)));

            if (listObjet != null)
            {

                for (int i = 0; i < listObjet.Count; i++)
                {
                    DataRow dr = table.NewRow();
                    dr["codetrimestre"] = listObjet.ElementAt(i).codetrimestre;
                    dr["nomtrimestre"] = listObjet.ElementAt(i).nomtrimestre;
                    table.Rows.Add(dr);
                }
            }

            string vCode = "";
            string vNom = "";

            ListeTrimestres.Clear();

            //Personnes_Table = LoadDataTable();

            foreach (DataRow row in table.Rows)
            {
                vCode = Convert.ToString(row["codetrimestre"]);
                vNom = Convert.ToString(row["nomtrimestre"]);

                ListeTrimestres.Add(new TrimestreBE(vCode, vNom));

            }
        }
        public WindowAddEditTrimestreUI()
        {
            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";
            Thread.CurrentThread.CurrentCulture = ci;

            InitializeComponent();
            creerModifierTrimestreBL = new CreerModifierTrimestreBL();

            etat = 0;

            ancienObjet = new TrimestreBE();

            // A mettre pour que le binding avec le DataGrid fonctionne !
            grdListeTrimestre.DataContext = this;

            // Initialisation de la collection, qui va s'afficher dans la DataGrid :
            ListeTrimestres = new ObservableCollection<TrimestreBE>();
            List<TrimestreBE> LTrimestreBE = creerModifierTrimestreBL.listerTousLesTrimestres();
            // on met la liste "LTrimestreBE" dans le DataGrid
            RemplirDataGrid(LTrimestreBE);

            // ------------------- Chargement de la liste des codes de Trimestre dans le comboBox de la fenêtre 
            //(utile pour le filtre)
            cmbFilterCode.ItemsSource = creerModifierTrimestreBL.getListCodeTrimestre(LTrimestreBE);

            // ------------------- Chargement de la liste des noms de Trimestre dans le comboBox de la fenêtre 
            //(utile pour le filtre)
            cmbFilterNom.ItemsSource = creerModifierTrimestreBL.getListNomTrimestre(LTrimestreBE);
        }

        private void cmdFermer_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cmdValider_Click(object sender, RoutedEventArgs e)
        {
            if ((txtCode.Text != null && txtNom.Text != null) && (txtCode.Text != "" && txtNom.Text != ""))
            { // si tous les champs sont non vides

                TrimestreBE trimestre = new TrimestreBE();
                trimestre.codetrimestre = txtCode.Text;
                trimestre.nomtrimestre = txtNom.Text;

                if (etat == 1)
                {
                    creerModifierTrimestreBL.modifierTrimestre(ancienObjet, trimestre);
                    List<TrimestreBE> LTrimestreBE = creerModifierTrimestreBL.listerTousLesTrimestres();
                    // on met la liste "LTrimestreBE" dans le DataGrid
                    RemplirDataGrid(LTrimestreBE);

                    // ------------------- Chargement de la liste des codes de Trimestre dans le comboBox de la fenêtre 
                    //(utile pour le filtre)
                    cmbFilterCode.ItemsSource = creerModifierTrimestreBL.getListCodeTrimestre(LTrimestreBE);

                    // ------------------- Chargement de la liste des noms de Trimestre dans le comboBox de la fenêtre 
                    //(utile pour le filtre)
                    cmbFilterNom.ItemsSource = creerModifierTrimestreBL.getListNomTrimestre(LTrimestreBE);

                    txtCode.Text = "";
                    txtNom.Text = "";
                    etat = 0;
                }
                else if (creerModifierTrimestreBL.rechercherTrimestre(new TrimestreBE(txtCode.Text, txtNom.Text)) == null)
                { // si un TRimestre possédant le même code n'existe pas deja dans la BD

                    if (creerModifierTrimestreBL.creerTrimestre(txtCode.Text, txtNom.Text))
                    { // si l'nregistrement a réussi

                        MessageBox.Show("Enregistrement Trimestre [" + txtCode.Text + ", " + txtNom.Text + "] " + " : Opération réussie");
                        txtCode.Text = "";
                        txtNom.Text = "";

                        List<TrimestreBE> LTrimestreBE = creerModifierTrimestreBL.listerTousLesTrimestres();
                        //on rafraichir le DataGrid
                        RemplirDataGrid(LTrimestreBE);
                        // ------------------- Chargement de la liste des codes de trimestre dans le comboBox de la fenêtre 
                        //(utile pour le filtre)
                        cmbFilterCode.ItemsSource = creerModifierTrimestreBL.getListCodeTrimestre(LTrimestreBE);

                        // ------------------- Chargement de la liste des noms de trimestre dans le comboBox de la fenêtre 
                        //(utile pour le filtre)
                        cmbFilterNom.ItemsSource = creerModifierTrimestreBL.getListNomTrimestre(LTrimestreBE);
                    }
                    else MessageBox.Show("Echec enregistrement : une erreure est survenue !");
                }
                else MessageBox.Show("Un Trimestre ayant le code \"" + txtCode.Text + "\" existe deja dans le système \n \n Veuillez changer de code SVP !");
            }
            else MessageBox.Show("Tous les champs doivent pas être remplis !");
        }

        private void cmbFilterCode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // on filtre la liste affiché dans le DataGrid en fonction du choix de l'utilisateur
            List<TrimestreBE> LTrimestreBE;
            if (cmbFilterNom.Text != null && cmbFilterNom.Text != "")
            {
                if (cmbFilterCode.SelectedItem.Equals("<Tous les Codes>"))
                {
                    if (cmbFilterNom.SelectedItem.Equals("<Tous les Noms>"))
                        LTrimestreBE = creerModifierTrimestreBL.listerTousLesTrimestres();
                    else
                        LTrimestreBE = creerModifierTrimestreBL.listerTrimestreSuivantCritere("nomtrimestre = '" + cmbFilterNom.SelectedItem + "'");
                }
                else
                    if (cmbFilterNom.SelectedItem.Equals("<Tous les Noms>"))
                        LTrimestreBE = creerModifierTrimestreBL.listerTrimestreSuivantCritere("codetrimestre = '" + cmbFilterCode.SelectedItem + "'");
                    else
                        LTrimestreBE = creerModifierTrimestreBL.listerTrimestreSuivantCritere("codetrimestre = '" + cmbFilterCode.SelectedItem + "' AND nomtrimestre = '" + cmbFilterNom.SelectedItem + "'");
            }
            else
                if (cmbFilterCode.SelectedItem.Equals("<Tous les Codes>"))
                    LTrimestreBE = creerModifierTrimestreBL.listerTousLesTrimestres();
                else
                    LTrimestreBE = creerModifierTrimestreBL.listerTrimestreSuivantCritere("codetrimestre = '" + cmbFilterCode.SelectedItem + "'");

            //on rafraichir le DataGrid
            RemplirDataGrid(LTrimestreBE);
        }

        private void cmbFilterNom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // on filtre la liste affiché dans le DataGrid en fonction du choix de l'utilisateur
            List<TrimestreBE> LTrimestreBE;
            if (cmbFilterCode.Text != null && cmbFilterCode.Text != "")
            {
                if (cmbFilterNom.SelectedItem.Equals("<Tous les Noms>"))
                {
                    if (cmbFilterCode.SelectedItem.Equals("<Tous les Codes>"))
                        LTrimestreBE = creerModifierTrimestreBL.listerTousLesTrimestres();
                    else
                        LTrimestreBE = creerModifierTrimestreBL.listerTrimestreSuivantCritere("codetrimestre = '" + cmbFilterCode.SelectedItem + "'");
                }
                else
                    if (cmbFilterCode.SelectedItem.Equals("<Tous les Codes>"))
                        LTrimestreBE = creerModifierTrimestreBL.listerTrimestreSuivantCritere("nomtrimestre = '" + cmbFilterNom.SelectedItem + "'");
                    else
                        LTrimestreBE = creerModifierTrimestreBL.listerTrimestreSuivantCritere("codetrimestre = '" + cmbFilterCode.SelectedItem + "' AND nomtrimestre = '" + cmbFilterNom.SelectedItem + "'");
            }
            else
                if (cmbFilterNom.SelectedItem.Equals("<Tous les Noms>"))
                    LTrimestreBE = creerModifierTrimestreBL.listerTousLesTrimestres();
                else
                    LTrimestreBE = creerModifierTrimestreBL.listerTrimestreSuivantCritere("nomtrimestre = '" + cmbFilterNom.SelectedItem + "'");

            //on rafraichir le DataGrid
            RemplirDataGrid(LTrimestreBE);
        }

        private void cmdAnnuler_Click(object sender, RoutedEventArgs e)
        {
            txtCode.Text = "";
            txtNom.Text = "";
            etat = 0;
        }

        private void grdListeTrimestre_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                if (grdListeTrimestre.SelectedIndex != -1)
                {
                    if (MessageBox.Show("Voulez-vous vraiment le supprimer ? ", "School : Confimation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        if (creerModifierTrimestreBL.supprinertrimestre(ListeTrimestres.ElementAt(grdListeTrimestre.SelectedIndex)))
                            ListeTrimestres.RemoveAt(grdListeTrimestre.SelectedIndex);

                        grdListeTrimestre.ItemsSource = ListeTrimestres;

                        // ------------------- Chargement de la liste des codes de Trimestre dans le comboBox de la fenêtre 
                        //(utile pour le filtre)
                        List<TrimestreBE> LTrimestreBE = creerModifierTrimestreBL.listerTousLesTrimestres();
                        cmbFilterCode.ItemsSource = creerModifierTrimestreBL.getListCodeTrimestre(LTrimestreBE);

                        // ------------------- Chargement de la liste des noms de Trimestre dans le comboBox de la fenêtre 
                        //(utile pour le filtre)
                        cmbFilterNom.ItemsSource = creerModifierTrimestreBL.getListNomTrimestre(LTrimestreBE);
                    }

                    grdListeTrimestre.UnselectAll();
                }

            }
        }

        private void grdListeTrimestre_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (grdListeTrimestre.SelectedIndex != -1)
            {
                etat = 1;
                TrimestreBE trimestre = new TrimestreBE();

                trimestre = creerModifierTrimestreBL.rechercherTrimestre(ListeTrimestres.ElementAt(grdListeTrimestre.SelectedIndex));
                if (trimestre != null)
                {
                    // on charge les informations dans le formulaire
                    txtCode.Text = trimestre.codetrimestre;
                    txtNom.Text = trimestre.nomtrimestre;

                    ancienObjet = trimestre;
                }

                grdListeTrimestre.UnselectAll();
            }
        }

        private void cmdImprimer_Click(object sender, RoutedEventArgs e)
        {
            CreerEtat etat = new CreerEtat("Trimestres -" + DateTime.Today.ToShortDateString(), "Liste des Trimestres ");

            etat.obtenirEtat(grdListeTrimestre);
        }


    }
}
