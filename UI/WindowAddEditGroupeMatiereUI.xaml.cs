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
    /// Interaction logic for WindowAddEditGroupeMatiereUI.xaml
    /// </summary>
    public partial class WindowAddEditGroupeMatiereUI : Window
    {
        private CreerModifierGroupeMatiereBL creerModifierGroupeMatiereBL;
        private int etat; // indique si nous sommes en création (0) ou en modification (1)

        private GroupeMatiereBE ancienObjet; //garde l'ancien état de l'objet, qui sera utilisé pour la modification

        String codeChoisi; //sera utile pour la génération des états
        String nomChoisi; //sera utile pour la génération des états

        // Définition d'une liste 'ListeGroupeMatieres' observable de 'GroupeMatiere'
        public ObservableCollection<GroupeMatiereBE> ListeGroupeMatieres { get; set; }

        // Fonction permettant de remplir le DataGrid avec les informations de la base de données
        // @param : - listObjet : la liste des objets à afficher dans le DataGrid
        public void RemplirDataGrid(List<GroupeMatiereBE> listObjet)
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
                    dr["code"] = listObjet.ElementAt(i).codegroupe;
                    dr["nom"] = listObjet.ElementAt(i).nomgroupe;
                    table.Rows.Add(dr);
                }
            }

            string vCode = "";
            string vNom = "";

            ListeGroupeMatieres.Clear();

            //Personnes_Table = LoadDataTable();

            foreach (DataRow row in table.Rows)
            {
                vCode = Convert.ToString(row["code"]);
                vNom = Convert.ToString(row["nom"]);

                ListeGroupeMatieres.Add(new GroupeMatiereBE(vCode, vNom));

            }
        }


        public WindowAddEditGroupeMatiereUI()
        {
            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";
            Thread.CurrentThread.CurrentCulture = ci;

            InitializeComponent();

            creerModifierGroupeMatiereBL = new CreerModifierGroupeMatiereBL();

            codeChoisi = "<Tous les Codes>";
            nomChoisi = "<Tous les Noms>";

            etat = 0;

            ancienObjet = new GroupeMatiereBE();

            // A mettre pour que le binding avec le DataGrid fonctionne !
            grdListeGroupeMatiere.DataContext = this;

            // Initialisation de la collection, qui va s'afficher dans la DataGrid :
            ListeGroupeMatieres = new ObservableCollection<GroupeMatiereBE>();
            List<GroupeMatiereBE> LGroupeMatiereBE = creerModifierGroupeMatiereBL.listerTousLesGroupeMatieres();
            // on met la liste "LGroupeMatiereBE" dans le DataGrid
            RemplirDataGrid(LGroupeMatiereBE);

            // ------------------- Chargement de la liste des codes de série dans le comboBox de la fenêtre 
            //(utile pour le filtre)
            cmbFilterCode.ItemsSource = creerModifierGroupeMatiereBL.getListCodeGroupeMatiere(LGroupeMatiereBE);

            // ------------------- Chargement de la liste des noms de série dans le comboBox de la fenêtre 
            //(utile pour le filtre)
            cmbFilterNom.ItemsSource = creerModifierGroupeMatiereBL.getListNomGroupeMatiere(LGroupeMatiereBE);
        }

        private void cmdFermer_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cmdValider_Click(object sender, RoutedEventArgs e)
        {
            if ((txtCode.Text != null && txtNom.Text != null) && (txtCode.Text != "" && txtNom.Text != ""))
            {
                GroupeMatiereBE groupeMatiere = new GroupeMatiereBE();
                groupeMatiere.codegroupe = txtCode.Text;
                groupeMatiere.nomgroupe = txtNom.Text;
                if (etat == 1)
                {
                    creerModifierGroupeMatiereBL.modifierGroupeMatiere(ancienObjet, groupeMatiere);
                    List<GroupeMatiereBE> LGroupeMatiereBE = creerModifierGroupeMatiereBL.listerTousLesGroupeMatieres();
                    // on met la liste "LGroupeMatiereBE" dans le DataGrid
                    RemplirDataGrid(LGroupeMatiereBE);

                    // ------------------- Chargement de la liste des codes de série dans le comboBox de la fenêtre 
                    //(utile pour le filtre)
                    cmbFilterCode.ItemsSource = creerModifierGroupeMatiereBL.getListCodeGroupeMatiere(LGroupeMatiereBE);

                    // ------------------- Chargement de la liste des noms de série dans le comboBox de la fenêtre 
                    //(utile pour le filtre)
                    cmbFilterNom.ItemsSource = creerModifierGroupeMatiereBL.getListNomGroupeMatiere(LGroupeMatiereBE);

                    txtCode.Text = "";
                    txtNom.Text = "";

                    etat = 0;
                }
                else if (creerModifierGroupeMatiereBL.rechercherGroupeMatiere(groupeMatiere) == null)
                {

                    if (creerModifierGroupeMatiereBL.creerGroupeMatiere(txtCode.Text, txtNom.Text))
                    {
                        MessageBox.Show("Opération réussie");
                        //on rafraichit les champs du formulaire
                        txtCode.Text = "";
                        txtNom.Text = "";

                        List<GroupeMatiereBE> LGroupeMatiereBE = creerModifierGroupeMatiereBL.listerTousLesGroupeMatieres();
                        //on rafraichir le DataGrid
                        RemplirDataGrid(LGroupeMatiereBE);
                        // ------------------- Chargement de la liste des codes de série dans le comboBox de la fenêtre 
                        //(utile pour le filtre)
                        cmbFilterCode.ItemsSource = creerModifierGroupeMatiereBL.getListCodeGroupeMatiere(LGroupeMatiereBE);

                        // ------------------- Chargement de la liste des noms de série dans le comboBox de la fenêtre 
                        //(utile pour le filtre)
                        cmbFilterNom.ItemsSource = creerModifierGroupeMatiereBL.getListNomGroupeMatiere(LGroupeMatiereBE);
                    }
                    else MessageBox.Show("Echec enregistrement : une erreure est survenue !");
                }
                else MessageBox.Show("Un Groupe Matière ayant le code \"" + txtCode.Text + "\" existe deja dans le système \n \n Veuillez changer de code SVP !");
            }
            else MessageBox.Show("Tous les champs doivent pas être remplis !");
        }

        private void cmbFilterCode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            codeChoisi = Convert.ToString(cmbFilterCode.SelectedItem);
            
            // on filtre la liste affiché dans le DataGrid en fonction du choix de l'utilisateur
            List<GroupeMatiereBE> LGroupeMatiereBE;
            if (cmbFilterNom.Text != null && cmbFilterNom.Text != "")
            {
                nomChoisi = cmbFilterNom.Text;

                if (cmbFilterCode.SelectedItem.Equals("<Tous les Codes>"))
                {
                    if (cmbFilterNom.SelectedItem.Equals("<Tous les Noms>"))
                        LGroupeMatiereBE = creerModifierGroupeMatiereBL.listerTousLesGroupeMatieres();
                    else
                        LGroupeMatiereBE = creerModifierGroupeMatiereBL.listerGroupeMatiereSuivantCritere("nomgroupe = '" + cmbFilterNom.SelectedItem + "'");
                }
                else
                    if (cmbFilterNom.SelectedItem.Equals("<Tous les Noms>"))
                        LGroupeMatiereBE = creerModifierGroupeMatiereBL.listerGroupeMatiereSuivantCritere("codegroupe = '" + cmbFilterCode.SelectedItem + "'");
                    else
                        LGroupeMatiereBE = creerModifierGroupeMatiereBL.listerGroupeMatiereSuivantCritere("codegroupe = '" + cmbFilterCode.SelectedItem + "' AND nomgroupe = '" + cmbFilterNom.SelectedItem + "'");
            }
            else
                if (cmbFilterCode.SelectedItem.Equals("<Tous les Codes>"))
                    LGroupeMatiereBE = creerModifierGroupeMatiereBL.listerTousLesGroupeMatieres();
                else
                    LGroupeMatiereBE = creerModifierGroupeMatiereBL.listerGroupeMatiereSuivantCritere("codegroupe = '" + cmbFilterCode.SelectedItem + "'");

            //on rafraichir le DataGrid
            RemplirDataGrid(LGroupeMatiereBE);
        }

        private void cmbFilterNom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            nomChoisi = Convert.ToString(cmbFilterNom.SelectedItem);

            // on filtre la liste affiché dans le DataGrid en fonction du choix de l'utilisateur
            List<GroupeMatiereBE> LGroupeMatiereBE;
            if (cmbFilterCode.Text != null && cmbFilterCode.Text != "")
            {
                codeChoisi = cmbFilterCode.Text;

                if (cmbFilterNom.SelectedItem.Equals("<Tous les Noms>"))
                {
                    if (cmbFilterCode.SelectedItem.Equals("<Tous les Codes>"))
                        LGroupeMatiereBE = creerModifierGroupeMatiereBL.listerTousLesGroupeMatieres();
                    else
                        LGroupeMatiereBE = creerModifierGroupeMatiereBL.listerGroupeMatiereSuivantCritere("codegroupe = '" + cmbFilterCode.SelectedItem + "'");
                }
                else
                    if (cmbFilterCode.SelectedItem.Equals("<Tous les Codes>"))
                        LGroupeMatiereBE = creerModifierGroupeMatiereBL.listerGroupeMatiereSuivantCritere("nomgroupe = '" + cmbFilterNom.SelectedItem + "'");
                    else
                        LGroupeMatiereBE = creerModifierGroupeMatiereBL.listerGroupeMatiereSuivantCritere("codegroupe = '" + cmbFilterCode.SelectedItem + "' AND nomgroupe = '" + cmbFilterNom.SelectedItem + "'");
            }
            else
                if (cmbFilterNom.SelectedItem.Equals("<Tous les Noms>"))
                    LGroupeMatiereBE = creerModifierGroupeMatiereBL.listerTousLesGroupeMatieres();
                else
                    LGroupeMatiereBE = creerModifierGroupeMatiereBL.listerGroupeMatiereSuivantCritere("nomgroupe = '" + cmbFilterNom.SelectedItem + "'");

            //on rafraichir le DataGrid
            RemplirDataGrid(LGroupeMatiereBE);
        }

        private void grdListeGroupeMatiere_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                if (grdListeGroupeMatiere.SelectedIndex != -1)
                {
                    if (MessageBox.Show("Voulez-vous vraiment le supprimer ? ", "School : Confimation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {

                        if (creerModifierGroupeMatiereBL.supprinerGroupeMatiere(ListeGroupeMatieres.ElementAt(grdListeGroupeMatiere.SelectedIndex)))
                            ListeGroupeMatieres.RemoveAt(grdListeGroupeMatiere.SelectedIndex);
                        grdListeGroupeMatiere.ItemsSource = ListeGroupeMatieres;

                        // ------------------- Chargement de la liste des codes de série dans le comboBox de la fenêtre 
                        //(utile pour le filtre)
                        List<GroupeMatiereBE> LGroupeMatiereBE = creerModifierGroupeMatiereBL.listerTousLesGroupeMatieres();
                        cmbFilterCode.ItemsSource = creerModifierGroupeMatiereBL.getListCodeGroupeMatiere(LGroupeMatiereBE);

                        // ------------------- Chargement de la liste des noms de série dans le comboBox de la fenêtre 
                        //(utile pour le filtre)
                        cmbFilterNom.ItemsSource = creerModifierGroupeMatiereBL.getListNomGroupeMatiere(LGroupeMatiereBE);

                    }

                    grdListeGroupeMatiere.UnselectAll();
                }
            }
        }

        private void grdListeGroupeMatiere_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (grdListeGroupeMatiere.SelectedIndex != -1)
            {
                etat = 1;
                GroupeMatiereBE groupeMatiere = new GroupeMatiereBE();

                groupeMatiere = creerModifierGroupeMatiereBL.rechercherGroupeMatiere(ListeGroupeMatieres.ElementAt(grdListeGroupeMatiere.SelectedIndex));
                if (groupeMatiere != null)
                {
                    // on charge les informations dans le formulaire
                    txtCode.Text = groupeMatiere.codegroupe;
                    txtNom.Text = groupeMatiere.nomgroupe;

                    ancienObjet = groupeMatiere;
                }

                grdListeGroupeMatiere.UnselectAll();
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
            CreerEtat etat ;

            //if(codeChoisi.Equals("<Tous les Codes>") && nomChoisi.Equals("<Tous les Noms>"))
            //    etat = new CreerEtat("Groupe Matières -" + DateTime.Today.ToShortDateString(), "Liste des Groupes de Matières");
            //else
            //    etat = new CreerEtat("Groupe Matières -" + DateTime.Today.ToShortDateString(), "Liste des Groupes de Matières, Code : "+codeChoisi+" / Nom : "+nomChoisi);

            etat = new CreerEtat("Groupe Matières -" + DateTime.Today.ToShortDateString(), "Liste des Groupes de Matières");

            etat.obtenirEtat(grdListeGroupeMatiere);
        }
    }
}
