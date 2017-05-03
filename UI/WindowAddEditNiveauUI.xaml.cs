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
    /// Interaction logic for WindowAddEditNiveau.xaml
    /// </summary>
    public partial class WindowAddEditNiveauUI : Window
    {
        CreerModifierNiveauBL creerModifierNiveauBL;
        private int etat; // idique si nous sommes en création (0) ou en modification (1)

        private NiveauBE ancienObjet; //garde l'ancien état de l'objet, qui sera utilisé pour la modification


        // Définition d'une liste 'ListeNiveaux' observable de 'Série'
        public ObservableCollection<NiveauBE> ListeNiveaux { get; set; }

        // Fonction permettant de remplir le DataGrid avec les informations de la base de données
        // @param : - listObjet : la liste des objets à afficher dans le DataGrid
        public void RemplirDataGrid(List<NiveauBE> listObjet) {
            // Ajout de données dans la DataTable :
            var table = new DataTable();

            table.Columns.Add(new DataColumn("code", typeof(string)));
            table.Columns.Add(new DataColumn("nom", typeof(string)));
            table.Columns.Add(new DataColumn("niveau", typeof(string)));

            if (listObjet != null)
            {

                for (int i = 0; i < listObjet.Count; i++)
                {
                    DataRow dr = table.NewRow();
                    dr["code"] = listObjet.ElementAt(i).codeNiveau;
                    dr["nom"] = listObjet.ElementAt(i).nomNiveau;
                    dr["niveau"] = Convert.ToString(listObjet.ElementAt(i).niveau);
                    table.Rows.Add(dr);
                }
            }

            string vCode = "";
            string vNom = "";
            string vNiveau = "";

            ListeNiveaux.Clear();

            //Personnes_Table = LoadDataTable();

            foreach (DataRow row in table.Rows)
            {
                vCode = Convert.ToString(row["code"]);
                vNom = Convert.ToString(row["nom"]);
                vNiveau = Convert.ToString(row["niveau"]);

                ListeNiveaux.Add(new NiveauBE(vCode, vNom, Convert.ToInt16(vNiveau)));

            }
        }


        public WindowAddEditNiveauUI()
        {
            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";
            Thread.CurrentThread.CurrentCulture = ci;

            InitializeComponent();

            creerModifierNiveauBL = new CreerModifierNiveauBL();

            etat = 0;

            ancienObjet = new NiveauBE();

            // A mettre pour que le binding avec le DataGrid fonctionne !
            grdListeNiveau.DataContext = this;

            // Initialisation de la collection, qui va s'afficher dans la DataGrid :
            ListeNiveaux = new ObservableCollection<NiveauBE>();
            List<NiveauBE> LNiveauBE = creerModifierNiveauBL.listerTousLesNiveaux();
            // on met la liste "LSerieBE" dans le DataGrid
            RemplirDataGrid(LNiveauBE);

            // ------------------- Chargement de la liste des codes de Niveau dans le comboBox de la fenêtre 
            //(utile pour le filtre)
            cmbFilterCode.ItemsSource = creerModifierNiveauBL.getListCodeNiveau(LNiveauBE);

            // ------------------- Chargement de la liste des noms de Niveau dans le comboBox de la fenêtre 
            //(utile pour le filtre)
            cmbFilterNom.ItemsSource = creerModifierNiveauBL.getListNomNiveau(LNiveauBE);
        }

        private void cmdFermer_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cmdValider_Click(object sender, RoutedEventArgs e)
        {
            if ((txtCode.Text != null && txtNom.Text != null && txtNiveau.Text != null) && (txtCode.Text != "" && txtNom.Text != "" && txtNiveau.Text != ""))
            {
                NiveauBE niveau = new NiveauBE();
                niveau.codeNiveau = txtCode.Text;
                niveau.nomNiveau = txtNom.Text;
                niveau.niveau = Convert.ToInt16(txtNiveau.Text);

                if (etat == 1)
                {
                    creerModifierNiveauBL.modifierNiveau(ancienObjet, niveau);
                    List<NiveauBE> LNiveauBE = creerModifierNiveauBL.listerTousLesNiveaux();
                    // on met la liste "LNiveauBE" dans le DataGrid
                    RemplirDataGrid(LNiveauBE);

                    // ------------------- Chargement de la liste des codes de Niveau dans le comboBox de la fenêtre 
                    //(utile pour le filtre)
                    cmbFilterCode.ItemsSource = creerModifierNiveauBL.getListCodeNiveau(LNiveauBE);

                    // ------------------- Chargement de la liste des noms de Niveau dans le comboBox de la fenêtre 
                    //(utile pour le filtre)
                    cmbFilterNom.ItemsSource = creerModifierNiveauBL.getListNomNiveau(LNiveauBE);

                    txtCode.Text = "";
                    txtNom.Text = "";
                    txtNiveau.Text = "";
                    etat = 0;
                }
                else if (creerModifierNiveauBL.rechercherNiveau(niveau) == null)
                {
                    if (creerModifierNiveauBL.creerNiveau(txtCode.Text, txtNom.Text, Convert.ToInt16(txtNiveau.Text)))
                    {
                        MessageBox.Show("Enregistrement Niveau [" + txtCode.Text + ", " + txtNom.Text + ", " + txtNiveau.Text + "] " + " : Opération réussie");
                        txtCode.Text = "";
                        txtNom.Text = "";
                        txtNiveau.Text = "";

                        // Initialisation de la collection, qui va s'afficher dans la DataGrid :
                        List<NiveauBE> LNiveauBE = creerModifierNiveauBL.listerTousLesNiveaux();
                        // on met la liste "LSerieBE" dans le DataGrid
                        RemplirDataGrid(LNiveauBE);

                        // ------------------- Chargement de la liste des codes de série dans le comboBox de la fenêtre 
                        //(utile pour le filtre)
                        cmbFilterCode.ItemsSource = creerModifierNiveauBL.getListCodeNiveau(LNiveauBE);

                        // ------------------- Chargement de la liste des noms de série dans le comboBox de la fenêtre 
                        //(utile pour le filtre)
                        cmbFilterNom.ItemsSource = creerModifierNiveauBL.getListNomNiveau(LNiveauBE);
                    }
                    else MessageBox.Show("Echec enregistrement : une erreure est survenue !");
                }
                else MessageBox.Show("Un Niveau ayant le code \"" + txtCode.Text + "\" existe deja dans le système \n \n Veuillez changer de code SVP !");
            }
            else MessageBox.Show("Tous les champs doivent êtres remplis !");
        }

        private void cmbFilterCode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // on filtre la liste affiché dans le DataGrid en fonction du choix de l'utilisateur
            List<NiveauBE> LNiveauBE;
            if (cmbFilterNom.Text != null && cmbFilterNom.Text != "")
            {
                if (cmbFilterCode.SelectedItem.Equals("<Tous les Codes>"))
                {
                    if (cmbFilterNom.SelectedItem.Equals("<Tous les Noms>"))
                        LNiveauBE = creerModifierNiveauBL.listerTousLesNiveaux();
                    else
                        LNiveauBE = creerModifierNiveauBL.listerNiveauSuivantCritere("nomniveau = '" + cmbFilterNom.SelectedItem + "'");
                }
                else
                    if (cmbFilterNom.SelectedItem.Equals("<Tous les Noms>"))
                        LNiveauBE = creerModifierNiveauBL.listerNiveauSuivantCritere("codeniveau = '" + cmbFilterCode.SelectedItem + "'");
                    else
                        LNiveauBE = creerModifierNiveauBL.listerNiveauSuivantCritere("codeniveau = '" + cmbFilterCode.SelectedItem + "' AND nomniveau = '" + cmbFilterNom.SelectedItem + "'");
            }
            else
                if (cmbFilterCode.SelectedItem.Equals("<Tous les Codes>"))
                    LNiveauBE = creerModifierNiveauBL.listerTousLesNiveaux();
                else
                    LNiveauBE = creerModifierNiveauBL.listerNiveauSuivantCritere("codeserie = '" + cmbFilterCode.SelectedItem + "'");

            //on rafraichir le DataGrid
            RemplirDataGrid(LNiveauBE);
        }

        private void cmbFilterNom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // on filtre la liste affiché dans le DataGrid en fonction du choix de l'utilisateur
            List<NiveauBE> LNiveauBE;
            if (cmbFilterCode.Text != null && cmbFilterCode.Text != "")
            {
                if (cmbFilterNom.SelectedItem.Equals("<Tous les Noms>"))
                {
                    if (cmbFilterCode.SelectedItem.Equals("<Tous les Codes>"))
                        LNiveauBE = creerModifierNiveauBL.listerTousLesNiveaux();
                    else
                        LNiveauBE = creerModifierNiveauBL.listerNiveauSuivantCritere("codeniveau = '" + cmbFilterCode.SelectedItem + "'");
                }
                else
                    if (cmbFilterCode.SelectedItem.Equals("<Tous les Codes>"))
                        LNiveauBE = creerModifierNiveauBL.listerNiveauSuivantCritere("nomniveau = '" + cmbFilterNom.SelectedItem + "'");
                    else
                        LNiveauBE = creerModifierNiveauBL.listerNiveauSuivantCritere("codeniveau = '" + cmbFilterCode.SelectedItem + "' AND nomniveau = '" + cmbFilterNom.SelectedItem + "'");
            }
            else
                if (cmbFilterNom.SelectedItem.Equals("<Tous les Noms>"))
                    LNiveauBE = creerModifierNiveauBL.listerTousLesNiveaux();
                else
                    LNiveauBE = creerModifierNiveauBL.listerNiveauSuivantCritere("nomniveau = '" + cmbFilterNom.SelectedItem + "'");

            //on rafraichir le DataGrid
            RemplirDataGrid(LNiveauBE);
        }

        private void cmdAnnuler_Click(object sender, RoutedEventArgs e)
        {
            txtCode.Text = "";
            txtNom.Text = "";
            txtNiveau.Text = "";
            etat = 0;
        }

        private void grdListeNiveau_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                if (grdListeNiveau.SelectedIndex != -1)
                {
                    if (MessageBox.Show("Voulez-vous vraiment le supprimer ? ", "School : Confimation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        if (creerModifierNiveauBL.supprinerNiveau(ListeNiveaux.ElementAt(grdListeNiveau.SelectedIndex)))
                            ListeNiveaux.RemoveAt(grdListeNiveau.SelectedIndex);
                        grdListeNiveau.ItemsSource = ListeNiveaux;

                        // ------------------- Chargement de la liste des codes de Niveau dans le comboBox de la fenêtre 
                        //(utile pour le filtre)
                        List<NiveauBE> LNiveauBE = creerModifierNiveauBL.listerTousLesNiveaux();
                        cmbFilterCode.ItemsSource = creerModifierNiveauBL.getListCodeNiveau(LNiveauBE);

                        // ------------------- Chargement de la liste des noms de Niveau dans le comboBox de la fenêtre 
                        //(utile pour le filtre)
                        cmbFilterNom.ItemsSource = creerModifierNiveauBL.getListNomNiveau(LNiveauBE);
                    }

                    grdListeNiveau.UnselectAll();
                }

            }
        }

        private void grdListeNiveau_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (grdListeNiveau.SelectedIndex != -1)
            {
                etat = 1;
                NiveauBE niveau = new NiveauBE();

                niveau = creerModifierNiveauBL.rechercherNiveau(ListeNiveaux.ElementAt(grdListeNiveau.SelectedIndex));
                // on charge les informations dans le formulaire
                txtCode.Text = niveau.codeNiveau;
                txtNom.Text = niveau.nomNiveau;
                txtNiveau.Text = Convert.ToString(niveau.niveau);

                ancienObjet = niveau;

                grdListeNiveau.UnselectAll();
            }
        }

        private void txtNiveau_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Utilitaires.IsTextAllowed(e.Text);
        }

        private void cmdImprimer_Click(object sender, RoutedEventArgs e)
        {
            CreerEtat etat = new CreerEtat("Liste des Niveaux -" + DateTime.Today.ToShortDateString(), "Liste des Niveaux");
            etat.obtenirEtat(grdListeNiveau);
        }
    }
}
