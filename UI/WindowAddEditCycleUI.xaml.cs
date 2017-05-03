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
    /// Interaction logic for Window4.xaml
    /// </summary>
    public partial class WindowAddEditCycleUI : Window
    {
        CreerModifierCycleBL creerModifierCycleBL;
        private int etat; // idique si nous sommes en création (0) ou en modification (1)

        private CycleBE ancienObjet; //garde l'ancien état de l'objet, qui sera utilisé pour la modification


        // Définition d'une liste 'ListeCycles' observable de 'Cycle'
        public ObservableCollection<CycleBE> ListeCycles { get; set; }

        // Fonction permettant de remplir le DataGrid avec les informations de la base de données
        // @param : - listObjet : la liste des objets à afficher dans le DataGrid
        public void RemplirDataGrid(List<CycleBE> listObjet)
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
                    dr["code"] = listObjet.ElementAt(i).codeCycle;
                    dr["nom"] = listObjet.ElementAt(i).nomCycle;
                    table.Rows.Add(dr);
                }
            }

            string vCode = "";
            string vNom = "";

            ListeCycles.Clear();

            //Personnes_Table = LoadDataTable();

            foreach (DataRow row in table.Rows)
            {
                vCode = Convert.ToString(row["code"]);
                vNom = Convert.ToString(row["nom"]);

                ListeCycles.Add(new CycleBE(vCode, vNom));

            }
        }


        public WindowAddEditCycleUI()
        {
            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";
            Thread.CurrentThread.CurrentCulture = ci;

            InitializeComponent();

            creerModifierCycleBL = new CreerModifierCycleBL();

            etat = 0;

            ancienObjet = new CycleBE();

            // A mettre pour que le binding avec le DataGrid fonctionne !
            grdListeCycle.DataContext = this;

            // Initialisation de la collection, qui va s'afficher dans la DataGrid :
            ListeCycles = new ObservableCollection<CycleBE>();
            List<CycleBE> LCycleBE = creerModifierCycleBL.listerToutesLesCycle();
            // on met la liste "LSerieBE" dans le DataGrid
            RemplirDataGrid(LCycleBE);

            // ------------------- Chargement de la liste des codes de Cycle dans le comboBox de la fenêtre 
            //(utile pour le filtre)
            cmbFilterCode.ItemsSource = creerModifierCycleBL.getListCodeCycle(LCycleBE);

            // ------------------- Chargement de la liste des noms de Cycle dans le comboBox de la fenêtre 
            //(utile pour le filtre)
            cmbFilterNom.ItemsSource = creerModifierCycleBL.getListNomCycle(LCycleBE);
        }

        private void cmdFermer_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cmdValider_Click(object sender, RoutedEventArgs e)
        {
            if ((txtCode.Text != null && txtNom.Text != null) && (txtCode.Text != "" && txtNom.Text != ""))
            { // si tous les champs sont non vides

                CycleBE cycle = new CycleBE();
                cycle.codeCycle = txtCode.Text;
                cycle.nomCycle = txtNom.Text;

                if (etat == 1)
                {
                    creerModifierCycleBL.modifierCycle(ancienObjet, cycle);
                    List<CycleBE> LCycleBE = creerModifierCycleBL.listerToutesLesCycle();
                    // on met la liste "LCycleBE" dans le DataGrid
                    RemplirDataGrid(LCycleBE);

                    // ------------------- Chargement de la liste des codes de Cycle dans le comboBox de la fenêtre 
                    //(utile pour le filtre)
                    cmbFilterCode.ItemsSource = creerModifierCycleBL.getListCodeCycle(LCycleBE);

                    // ------------------- Chargement de la liste des noms de Niveau dans le comboBox de la fenêtre 
                    //(utile pour le filtre)
                    cmbFilterNom.ItemsSource = creerModifierCycleBL.getListNomCycle(LCycleBE);

                    txtCode.Text = "";
                    txtNom.Text = "";
                    etat = 0;
                }
                else if (creerModifierCycleBL.rechercherCycle(new CycleBE(txtCode.Text, txtNom.Text)) == null)
                { // si un cycle possédant le même code n'existe pas deja dans la BD

                    if (creerModifierCycleBL.creerCycle(txtCode.Text, txtNom.Text))
                    { // si l'nregistrement a réussi

                        MessageBox.Show("Opération réussie");
                        txtCode.Text = "";
                        txtNom.Text = "";

                        List<CycleBE> LCycleBE = creerModifierCycleBL.listerToutesLesCycle();
                        //on rafraichir le DataGrid
                        RemplirDataGrid(LCycleBE);
                        // ------------------- Chargement de la liste des codes de série dans le comboBox de la fenêtre 
                        //(utile pour le filtre)
                        cmbFilterCode.ItemsSource = creerModifierCycleBL.getListCodeCycle(LCycleBE);

                        // ------------------- Chargement de la liste des noms de série dans le comboBox de la fenêtre 
                        //(utile pour le filtre)
                        cmbFilterNom.ItemsSource = creerModifierCycleBL.getListNomCycle(LCycleBE);
                    }
                    else MessageBox.Show("Echec enregistrement : une erreure est survenue !");
                }
                else MessageBox.Show("Un Cycle ayant le code \"" + txtCode.Text + "\" existe deja dans le système \n \n Veuillez changer de code SVP !");
            }
            else MessageBox.Show("Tous les champs doivent pas être remplis !");
        }

        private void cmbFilterCode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // on filtre la liste affiché dans le DataGrid en fonction du choix de l'utilisateur
            List<CycleBE> LCycleBE;
            if (cmbFilterNom.Text != null && cmbFilterNom.Text != "")
            {
                if (cmbFilterCode.SelectedItem.Equals("<Tous les Codes>"))
                {
                    if (cmbFilterNom.SelectedItem.Equals("<Tous les Noms>"))
                        LCycleBE = creerModifierCycleBL.listerToutesLesCycle();
                    else
                        LCycleBE = creerModifierCycleBL.listerCycleSuivantCritere("nomcycle = '" + cmbFilterNom.SelectedItem + "'");
                }
                else
                    if (cmbFilterNom.SelectedItem.Equals("<Tous les Noms>"))
                        LCycleBE = creerModifierCycleBL.listerCycleSuivantCritere("codecycle = '" + cmbFilterCode.SelectedItem + "'");
                    else
                        LCycleBE = creerModifierCycleBL.listerCycleSuivantCritere("codecycle = '" + cmbFilterCode.SelectedItem + "' AND nomcycle = '" + cmbFilterNom.SelectedItem + "'");
            }
            else
                if (cmbFilterCode.SelectedItem.Equals("<Tous les Codes>"))
                    LCycleBE = creerModifierCycleBL.listerToutesLesCycle();
                else
                    LCycleBE = creerModifierCycleBL.listerCycleSuivantCritere("codecycle = '" + cmbFilterCode.SelectedItem + "'");

            //on rafraichir le DataGrid
            RemplirDataGrid(LCycleBE);
        }

        private void cmbFilterNom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // on filtre la liste affiché dans le DataGrid en fonction du choix de l'utilisateur
            List<CycleBE> LCycleBE;
            if (cmbFilterCode.Text != null && cmbFilterCode.Text != "")
            {
                if (cmbFilterNom.SelectedItem.Equals("<Tous les Noms>"))
                {
                    if (cmbFilterCode.SelectedItem.Equals("<Tous les Codes>"))
                        LCycleBE = creerModifierCycleBL.listerToutesLesCycle();
                    else
                        LCycleBE = creerModifierCycleBL.listerCycleSuivantCritere("codecycle = '" + cmbFilterCode.SelectedItem + "'");
                }
                else
                    if (cmbFilterCode.SelectedItem.Equals("<Tous les Codes>"))
                        LCycleBE = creerModifierCycleBL.listerCycleSuivantCritere("nomcycle = '" + cmbFilterNom.SelectedItem + "'");
                    else
                        LCycleBE = creerModifierCycleBL.listerCycleSuivantCritere("codecycle = '" + cmbFilterCode.SelectedItem + "' AND nomcycle = '" + cmbFilterNom.SelectedItem + "'");
            }
            else
                if (cmbFilterNom.SelectedItem.Equals("<Tous les Noms>"))
                    LCycleBE = creerModifierCycleBL.listerToutesLesCycle();
                else
                    LCycleBE = creerModifierCycleBL.listerCycleSuivantCritere("nomcycle = '" + cmbFilterNom.SelectedItem + "'");

            //on rafraichir le DataGrid
            RemplirDataGrid(LCycleBE);
        }

        private void cmdAnnuler_Click(object sender, RoutedEventArgs e)
        {
            txtCode.Text = "";
            txtNom.Text = "";
            etat = 0;
        }

        private void grdListeCycle_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                if (grdListeCycle.SelectedIndex != -1)
                {
                    if (MessageBox.Show("Voulez-vous vraiment le supprimer ? ", "School : Confimation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        if (creerModifierCycleBL.supprinerCycle(ListeCycles.ElementAt(grdListeCycle.SelectedIndex)))
                            ListeCycles.RemoveAt(grdListeCycle.SelectedIndex);

                        grdListeCycle.ItemsSource = ListeCycles;

                        // ------------------- Chargement de la liste des codes de Cycle dans le comboBox de la fenêtre 
                        //(utile pour le filtre)
                        List<CycleBE> LCycleBE = creerModifierCycleBL.listerToutesLesCycle();
                        cmbFilterCode.ItemsSource = creerModifierCycleBL.getListCodeCycle(LCycleBE);

                        // ------------------- Chargement de la liste des noms de Cycle dans le comboBox de la fenêtre 
                        //(utile pour le filtre)
                        cmbFilterNom.ItemsSource = creerModifierCycleBL.getListNomCycle(LCycleBE);

                    }

                    grdListeCycle.UnselectAll();
                }
            }
        }

        private void grdListeCycle_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (grdListeCycle.SelectedIndex != -1)
            {
                etat = 1;
                CycleBE cycle = new CycleBE();

                cycle = creerModifierCycleBL.rechercherCycle(ListeCycles.ElementAt(grdListeCycle.SelectedIndex));
                if (cycle != null)
                {
                    // on charge les informations dans le formulaire
                    txtCode.Text = cycle.codeCycle;
                    txtNom.Text = cycle.nomCycle;

                    ancienObjet = cycle;
                }

                grdListeCycle.UnselectAll();
            }
        }

        private void cmdImprimer_Click(object sender, RoutedEventArgs e)
        {
            CreerEtat etat = new CreerEtat("Cycle -" + DateTime.Today.ToShortDateString(), "Liste des Cycles");
            etat.obtenirEtat(grdListeCycle);
        }

    }
}
