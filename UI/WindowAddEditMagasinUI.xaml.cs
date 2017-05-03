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

using Ecole.BusinessLogic;
using Ecole.BusinessEntity;
using Ecole.Utilitaire;

namespace Ecole.UI
{
    /// <summary>
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class WindowAddEditMagasinUI : Window
    {
         CreerModifierMagasinBL creerModifierMagasinBL;
         private int etat; // idique si nous sommes en création (0) ou en modification (1)

         private MagasinBE ancienObjet; //garde l'ancien état de l'objet, qui sera utilisé pour la modification


        // Définition d'une liste 'ListeMagasins' observable de 'Magasin'
         public ObservableCollection<MagasinBE> ListeMagasins { get; set; }

        // Fonction permettant de remplir le DataGrid avec les informations de la base de données
        // @param : - listObjet : la liste des objets à afficher dans le DataGrid
        public void RemplirDataGrid(List<MagasinBE> listObjet)
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
                    dr["code"] = listObjet.ElementAt(i).codeMagasin;
                    dr["nom"] = listObjet.ElementAt(i).nomMagasin;
                    table.Rows.Add(dr);
                }
            }

            string vCode = "";
            string vNom = "";

            ListeMagasins.Clear();

            //Personnes_Table = LoadDataTable();

            foreach (DataRow row in table.Rows)
            {
                vCode = Convert.ToString(row["code"]);
                vNom = Convert.ToString(row["nom"]);

                ListeMagasins.Add(new MagasinBE(vCode, vNom));

            }
        }

        public WindowAddEditMagasinUI()
        {
            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";
            Thread.CurrentThread.CurrentCulture = ci;

            InitializeComponent();

            creerModifierMagasinBL = new CreerModifierMagasinBL();

            etat = 0;

            ancienObjet = new MagasinBE();

            // A mettre pour que le binding avec le DataGrid fonctionne !
            grdListeMagasin.DataContext = this;

            // Initialisation de la collection, qui va s'afficher dans la DataGrid :
            ListeMagasins = new ObservableCollection<MagasinBE>();
            List<MagasinBE> LMagasinBE = creerModifierMagasinBL.listerToutesLesMagasin();
            // on met la liste "LSerieBE" dans le DataGrid
            RemplirDataGrid(LMagasinBE);

            // ------------------- Chargement de la liste des codes de Cycle dans le comboBox de la fenêtre 
            //(utile pour le filtre)
            cmbFilterCode.ItemsSource = creerModifierMagasinBL.getListCodeMagasin(LMagasinBE);

            // ------------------- Chargement de la liste des noms de Cycle dans le comboBox de la fenêtre 
            //(utile pour le filtre)
            cmbFilterNom.ItemsSource = creerModifierMagasinBL.getListNomMagasin(LMagasinBE);
        }

        private void cmdFermer_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cmdValider_Click(object sender, RoutedEventArgs e)
        {
            if ((txtCode.Text != null && txtNom.Text != null) && (txtCode.Text != "" && txtNom.Text != ""))
            { // si tous les champs sont non vides

                MagasinBE magasin = new MagasinBE();
                magasin.codeMagasin = txtCode.Text;
                magasin.nomMagasin = txtNom.Text;

                if (etat == 1)
                {
                    creerModifierMagasinBL.modifierMagasin(ancienObjet, magasin);
                    List<MagasinBE> LMagasinBE = creerModifierMagasinBL.listerToutesLesMagasin();
                    // on met la liste "LMagasinBE" dans le DataGrid
                    RemplirDataGrid(LMagasinBE);

                    // ------------------- Chargement de la liste des codes de Magasin dans le comboBox de la fenêtre 
                    //(utile pour le filtre)
                    cmbFilterCode.ItemsSource = creerModifierMagasinBL.getListCodeMagasin(LMagasinBE);

                    // ------------------- Chargement de la liste des noms de Niveau dans le comboBox de la fenêtre 
                    //(utile pour le filtre)
                    cmbFilterNom.ItemsSource = creerModifierMagasinBL.getListNomMagasin(LMagasinBE);

                    txtCode.Text = "";
                    txtNom.Text = "";
                    etat = 0;
                }
                else if (creerModifierMagasinBL.rechercherMagasin(new MagasinBE(txtCode.Text, txtNom.Text)) == null)
                { // si un magasin possédant le même code n'existe pas deja dans la BD

                    if (creerModifierMagasinBL.creerMagasin(txtCode.Text, txtNom.Text))
                    { // si l'nregistrement a réussi

                        MessageBox.Show("Opération réussie");
                        txtCode.Text = "";
                        txtNom.Text = "";

                        List<MagasinBE> LMagasinBE = creerModifierMagasinBL.listerToutesLesMagasin();
                        //on rafraichir le DataGrid
                        RemplirDataGrid(LMagasinBE);
                        // ------------------- Chargement de la liste des codes de Magasin dans le comboBox de la fenêtre 
                        //(utile pour le filtre)
                        cmbFilterCode.ItemsSource = creerModifierMagasinBL.getListCodeMagasin(LMagasinBE);

                        // ------------------- Chargement de la liste des noms de Magasin dans le comboBox de la fenêtre 
                        //(utile pour le filtre)
                        cmbFilterNom.ItemsSource = creerModifierMagasinBL.getListNomMagasin(LMagasinBE);
                    }
                    else MessageBox.Show("Echec enregistrement : une erreure est survenue !");
                }
                else MessageBox.Show("Un Magasin ayant le code \"" + txtCode.Text + "\" existe deja dans le système \n \n Veuillez changer de code SVP !");
            }
            else MessageBox.Show("Tous les champs doivent pas être remplis !");
        }

        private void cmbFilterCode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // on filtre la liste affiché dans le DataGrid en fonction du choix de l'utilisateur
            List<MagasinBE> LMagasinBE;
            if (cmbFilterNom.Text != null && cmbFilterNom.Text != "")
            {
                if (cmbFilterCode.SelectedItem.Equals("<Tous les Codes>"))
                {
                    if (cmbFilterNom.SelectedItem.Equals("<Tous les Noms>"))
                        LMagasinBE = creerModifierMagasinBL.listerToutesLesMagasin();
                    else
                        LMagasinBE = creerModifierMagasinBL.listerMagasinSuivantCritere("nommagasin = '" + cmbFilterNom.SelectedItem + "'");
                }
                else
                    if (cmbFilterNom.SelectedItem.Equals("<Tous les Noms>"))
                        LMagasinBE = creerModifierMagasinBL.listerMagasinSuivantCritere("codemagasin = '" + cmbFilterCode.SelectedItem + "'");
                    else
                        LMagasinBE = creerModifierMagasinBL.listerMagasinSuivantCritere("codemagasin = '" + cmbFilterCode.SelectedItem + "' AND nommagasin = '" + cmbFilterNom.SelectedItem + "'");
            }
            else
                if (cmbFilterCode.SelectedItem.Equals("<Tous les Codes>"))
                    LMagasinBE = creerModifierMagasinBL.listerToutesLesMagasin();
                else
                    LMagasinBE = creerModifierMagasinBL.listerMagasinSuivantCritere("codemagasin = '" + cmbFilterCode.SelectedItem + "'");

            //on rafraichir le DataGrid
            RemplirDataGrid(LMagasinBE);
        }

        private void cmbFilterNom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // on filtre la liste affiché dans le DataGrid en fonction du choix de l'utilisateur
            List<MagasinBE> LMagasinBE;
            if (cmbFilterCode.Text != null && cmbFilterCode.Text != "")
            {
                if (cmbFilterNom.SelectedItem.Equals("<Tous les Noms>"))
                {
                    if (cmbFilterCode.SelectedItem.Equals("<Tous les Codes>"))
                        LMagasinBE = creerModifierMagasinBL.listerToutesLesMagasin();
                    else
                        LMagasinBE = creerModifierMagasinBL.listerMagasinSuivantCritere("codemagasin = '" + cmbFilterCode.SelectedItem + "'");
                }
                else
                    if (cmbFilterCode.SelectedItem.Equals("<Tous les Codes>"))
                        LMagasinBE = creerModifierMagasinBL.listerMagasinSuivantCritere("nommagasin = '" + cmbFilterNom.SelectedItem + "'");
                    else
                        LMagasinBE = creerModifierMagasinBL.listerMagasinSuivantCritere("codemagasin = '" + cmbFilterCode.SelectedItem + "' AND nommagasin = '" + cmbFilterNom.SelectedItem + "'");
            }
            else
                if (cmbFilterNom.SelectedItem.Equals("<Tous les Noms>"))
                    LMagasinBE = creerModifierMagasinBL.listerToutesLesMagasin();
                else
                    LMagasinBE = creerModifierMagasinBL.listerMagasinSuivantCritere("nommagasin = '" + cmbFilterNom.SelectedItem + "'");

            //on rafraichir le DataGrid
            RemplirDataGrid(LMagasinBE);
        }

        private void cmdAnnuler_Click(object sender, RoutedEventArgs e)
        {
            txtCode.Text = "";
            txtNom.Text = "";
            etat = 0;
        }

        private void grdListeMagasin_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                if (grdListeMagasin.SelectedIndex != -1)
                {
                    if (MessageBox.Show("Voulez-vous vraiment le supprimer ? ", "School : Confimation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        if (creerModifierMagasinBL.supprinerMagasin(ListeMagasins.ElementAt(grdListeMagasin.SelectedIndex)))
                            ListeMagasins.RemoveAt(grdListeMagasin.SelectedIndex);
                        grdListeMagasin.ItemsSource = ListeMagasins;

                        // ------------------- Chargement de la liste des codes de Magasin dans le comboBox de la fenêtre 
                        //(utile pour le filtre)
                        List<MagasinBE> LMagasinBE = creerModifierMagasinBL.listerToutesLesMagasin();
                        cmbFilterCode.ItemsSource = creerModifierMagasinBL.getListCodeMagasin(LMagasinBE);

                        // ------------------- Chargement de la liste des noms de Magasin dans le comboBox de la fenêtre 
                        //(utile pour le filtre)
                        cmbFilterNom.ItemsSource = creerModifierMagasinBL.getListNomMagasin(LMagasinBE);
                    }

                    grdListeMagasin.UnselectAll();
                }
            }
        }

        private void grdListeMagasin_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (grdListeMagasin.SelectedIndex != -1)
            {
                etat = 1;
                MagasinBE magasin = new MagasinBE();

                magasin = creerModifierMagasinBL.rechercherMagasin(ListeMagasins.ElementAt(grdListeMagasin.SelectedIndex));
                // on charge les informations dans le formulaire
                txtCode.Text = magasin.codeMagasin;
                txtNom.Text = magasin.nomMagasin;

                ancienObjet = magasin;

                grdListeMagasin.UnselectAll();
            }
        }

        private void cmdImprimer_Click(object sender, RoutedEventArgs e)
        {
            CreerEtat etat = new CreerEtat("Magasin -" + DateTime.Today.ToShortDateString(), "Liste des Magasins");
            etat.obtenirEtat(grdListeMagasin);
        }
    }
}
