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

//Add MySql Library
using MySql.Data.MySqlClient;

using System.Globalization;
using System.Threading;

using Ecole.BusinessEntity;
using Ecole.BusinessLogic;
using Ecole.Utilitaire;

namespace Ecole.UI
{
    /// <summary>
    /// Interaction logic for WindowAddEditClasse.xaml
    /// </summary>
    public partial class WindowAddEditClasseUI : Window
    {
        private CreerModifierClasseBL creerModifierClasseBL;
        private CreerModifierCycleBL creerModifierCycleBL;
        private CreerModifierTypeClasseBL creerModifierTypeClasseBL;
        private CreerModifierNiveauBL creerModifierNiveauBL;
        private CreerModifierSerieBL creerModifierSerieBL;

        private int etat; // idique si nous sommes en création (0) ou en modification (1)

        private ClasseBE ancienObjet; //garde l'ancien état de l'objet, qui sera utilisé pour la modification

        // Définition d'une liste 'ListeClasse' observable de 'Classe'
        public ObservableCollection<ClasseBE> ListeClasses { get; set; }

        // Fonction permettant de remplir le DataGrid avec les informations de la base de données
        // @param : - listObjet : la liste des objets à afficher dans le DataGrid
        public void RemplirDataGrid(List<ClasseBE> listObjet)
        {
            // Ajout de données dans la DataTable :
            var table = new DataTable();

            table.Columns.Add(new DataColumn("code", typeof(string)));
            table.Columns.Add(new DataColumn("nom", typeof(string)));
            table.Columns.Add(new DataColumn("cycle", typeof(string)));
            table.Columns.Add(new DataColumn("type", typeof(string)));
            table.Columns.Add(new DataColumn("niveau", typeof(string)));
            table.Columns.Add(new DataColumn("serie", typeof(string)));

            if (listObjet != null)
            {

                for (int i = 0; i < listObjet.Count; i++)
                {
                    DataRow dr = table.NewRow();
                    dr["code"] = listObjet.ElementAt(i).codeClasse;
                    dr["nom"] = listObjet.ElementAt(i).nomClasse;
                    dr["cycle"] = listObjet.ElementAt(i).codeCycle;
                    dr["type"] = listObjet.ElementAt(i).codeTypeClasse;
                    dr["niveau"] = listObjet.ElementAt(i).codeNiveau;
                    dr["serie"] = listObjet.ElementAt(i).codeSerie;
                    table.Rows.Add(dr);
                }
            }

            string vCode = "";
            string vNom = "";
            string vCycle = "";
            string vType = "";
            string vNiveau = "";
            string vSerie = "";

            ListeClasses.Clear();

            //Personnes_Table = LoadDataTable();

            foreach (DataRow row in table.Rows)
            {
                vCode = Convert.ToString(row["code"]);
                vNom = Convert.ToString(row["nom"]);
                vCycle = Convert.ToString(row["cycle"]);
                vType = Convert.ToString(row["type"]);
                vNiveau = Convert.ToString(row["niveau"]);
                vSerie = Convert.ToString(row["serie"]);

                ListeClasses.Add(new ClasseBE(vCode, vCycle, vType, vSerie, vNiveau, vNom));

            }
        }


        public WindowAddEditClasseUI()
        {
            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";
            Thread.CurrentThread.CurrentCulture = ci;

            InitializeComponent();

            creerModifierClasseBL = new CreerModifierClasseBL();
            creerModifierCycleBL = new CreerModifierCycleBL();
            creerModifierTypeClasseBL = new CreerModifierTypeClasseBL();
            creerModifierNiveauBL = new CreerModifierNiveauBL();
            creerModifierSerieBL = new CreerModifierSerieBL();

            // A mettre pour que le binding avec le DataGrid fonctionne !
            grdListeClasse.DataContext = this;

            etat = 0;

            ancienObjet = new ClasseBE();

            // Initialisation de la collection, qui va s'afficher dans la DataGrid :
            ListeClasses = new ObservableCollection<ClasseBE>();
            List<ClasseBE> LClasseBE = creerModifierClasseBL.listerTousLesClasse();
            // on met la liste "LClasseBE" dans le DataGrid
            RemplirDataGrid(LClasseBE);

            // ------------------- Chargement de la liste des cycles dans le comboBox de la fenêtre
            // uitle pour le filtre
            cmbFilterCode.ItemsSource = creerModifierClasseBL.getListCodeClasse(LClasseBE);

            // ------------------- Chargement de la liste des cycles dans le comboBox de la fenêtre
            List<CycleBE> listeCycle = creerModifierCycleBL.listerToutesLesCycle();

            cmbCycle.ItemsSource = creerModifierClasseBL.getListCodeCycle2(listeCycle);

            cmbFilterCycle.ItemsSource = creerModifierClasseBL.getListCodeCycle(listeCycle);

            // ------------------- Chargement de la liste des types de Classe dans le comboBox de la fenêtre
            List<TypeclasseBE> listeTypeclasse = creerModifierTypeClasseBL.listerTousLesTypeClasse();

            cmbType.ItemsSource = creerModifierClasseBL.getListCodeTypeClasse2(listeTypeclasse);

            cmbFilterType.ItemsSource = creerModifierClasseBL.getListCodeTypeClasse(listeTypeclasse);

            // ------------------- Chargement de la liste des Niveaux dans le comboBox de la fenêtre
            List<NiveauBE> listeNiveau = creerModifierNiveauBL.listerTousLesNiveaux();
            cmbNiveau.ItemsSource = creerModifierClasseBL.getListCodeNiveau2(listeNiveau);

            cmbFilterNiveau.ItemsSource = creerModifierClasseBL.getListCodeNiveau(listeNiveau);

            // ------------------- Chargement de la liste des Série dans le comboBox de la fenêtre

            List<SerieBE> listeSerie = creerModifierSerieBL.listerToutesLesSeries();
            cmbSerie.ItemsSource = creerModifierClasseBL.getListCodeSerie2(listeSerie);

            cmbFilterSerie.ItemsSource = creerModifierClasseBL.getListCodeSerie(listeSerie);

        }

        private void cmdFermer_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cmdValider_Click(object sender, RoutedEventArgs e)
        {
            if ((txtCode.Text != null && cmbType.Text != null && cmbNiveau.Text != null && cmbCycle.Text != null && txtNom.Text != null)
                && (txtCode.Text != "" && cmbType.Text != "" && cmbNiveau.Text != "" && cmbCycle.Text != "" && txtNom.Text != ""))
            { // si tous les champs sont non vides

                ClasseBE classe = new ClasseBE();
                classe.codeClasse = txtCode.Text;
                classe.codeCycle = cmbCycle.Text;
                classe.codeNiveau = cmbNiveau.Text;
                if (cmbSerie.Text == "")
                    classe.codeSerie = null;
                else classe.codeSerie = cmbSerie.Text;
                classe.codeTypeClasse = cmbType.Text;
                classe.nomClasse = txtNom.Text;

                if (etat == 1)
                {
                    creerModifierClasseBL.modifierClasse(ancienObjet, classe);
                    List<ClasseBE> LClasseBE = creerModifierClasseBL.listerTousLesClasse();
                    // on met la liste "LSerieBE" dans le DataGrid
                    RemplirDataGrid(LClasseBE);

                    // ------------------- Chargement de la liste des codes de Classe dans le comboBox de la fenêtre 
                    //(utile pour le filtre)
                    cmbFilterCode.ItemsSource = creerModifierClasseBL.getListCodeClasse(LClasseBE);

                    txtCode.Text = "";
                    cmbCycle.Text = null;
                    cmbNiveau.Text = null; ;
                    cmbSerie.Text = null;
                    cmbType.Text = null;
                    txtNom.Text = "";

                    etat = 0;
                }
                else if (creerModifierClasseBL.rechercherClasse(new ClasseBE(classe.codeClasse, classe.codeCycle, classe.codeTypeClasse, classe.codeSerie, classe.codeNiveau, classe.nomClasse)) == null)
                { // si une Classe possédant le même code n'existe pas deja dans la BD

                    if (creerModifierClasseBL.creerClasse(classe.codeClasse, classe.codeCycle, classe.codeTypeClasse, classe.codeSerie, classe.codeNiveau, classe.nomClasse))
                    { // si l'nregistrement a réussi

                        MessageBox.Show("Opération réussie");
                        txtCode.Text = "";
                        cmbCycle.Text = null;
                        cmbType.Text = null;
                        cmbSerie.Text = null;
                        cmbNiveau.Text = null;
                        txtNom.Text = "";

                        List<ClasseBE> LClasseBE = creerModifierClasseBL.listerTousLesClasse();
                        //on rafraichir le DataGrid
                        RemplirDataGrid(LClasseBE);
                        // ------------------- Chargement de la liste des codes de série dans le comboBox de la fenêtre 
                        //(utile pour le filtre)
                        cmbFilterCode.ItemsSource = creerModifierClasseBL.getListCodeClasse(LClasseBE);
                        cmbFilterCode.ItemsSource = cmbFilterCode.ItemsSource;

                        List<CycleBE> LCycleBE = creerModifierCycleBL.listerToutesLesCycle();
                        // ------------------- Chargement de la liste des codes de Cycle dans le comboBox de la fenêtre 
                        //(utile pour le filtre)
                        cmbCycle.ItemsSource = creerModifierClasseBL.getListCodeCycle2(LCycleBE);

                        cmbFilterCycle.ItemsSource = creerModifierClasseBL.getListCodeCycle(LCycleBE);

                        List<TypeclasseBE> LTypeClasseBE = creerModifierTypeClasseBL.listerTousLesTypeClasse();
                        // ------------------- Chargement de la liste des codes de Type de classe dans le comboBox de la fenêtre 
                        //(utile pour le filtre)
                        cmbType.ItemsSource = creerModifierClasseBL.getListCodeTypeClasse2(LTypeClasseBE);

                        cmbFilterType.ItemsSource = creerModifierClasseBL.getListCodeTypeClasse(LTypeClasseBE);

                        List<NiveauBE> LTypeNiveauBE = creerModifierNiveauBL.listerTousLesNiveaux();
                        // ------------------- Chargement de la liste des Niveaux dans le comboBox de la fenêtre 
                        //(utile pour le filtre)
                        cmbNiveau.ItemsSource = creerModifierClasseBL.getListCodeNiveau2(LTypeNiveauBE);

                        cmbFilterNiveau.ItemsSource = creerModifierClasseBL.getListCodeNiveau(LTypeNiveauBE);

                        List<SerieBE> LTypeSerieBE = creerModifierSerieBL.listerToutesLesSeries();
                        // ------------------- Chargement de la liste des Série dans le comboBox de la fenêtre 
                        //(utile pour le filtre)
                        cmbSerie.ItemsSource = creerModifierClasseBL.getListCodeSerie2(LTypeSerieBE);

                        cmbFilterSerie.ItemsSource = creerModifierClasseBL.getListCodeSerie(LTypeSerieBE);
                    }
                    else MessageBox.Show("Echec enregistrement : une erreure est survenue !");
                }
                else MessageBox.Show("Une Classe ayant le code \"" + txtCode.Text + "\" existe deja dans le système \n \n Veuillez changer de code SVP !");
            }
            else MessageBox.Show("Tous les champs marqués par un astérix (*) doivent pas être remplis !");
        }

        private void cmbFilterCode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // on filtre la liste affiché dans le DataGrid en fonction du choix de l'utilisateur
            List<ClasseBE> LClasseBE;
            if (cmbFilterCode.SelectedItem.Equals("<Toutes les Classes>"))
            {
                LClasseBE = creerModifierClasseBL.listerTousLesClasse();
            }
            else
                LClasseBE = creerModifierClasseBL.listerClasseSuivantCritere("codeclasse = '" + cmbFilterCode.SelectedItem + "'");

            grdListeClasse.ItemsSource = LClasseBE;
        }

        private void cmbFilterCycle_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // on filtre la liste affiché dans le DataGrid en fonction du choix de l'utilisateur
            List<ClasseBE> LClasseBE;
            if (cmbFilterCycle.SelectedItem.Equals("<Tous les Cycles>"))
            {
                LClasseBE = creerModifierClasseBL.listerTousLesClasse();
            }
            else
                LClasseBE = creerModifierClasseBL.listerClasseSuivantCritere("codecycle = '" + cmbFilterCycle.SelectedItem + "'");

            grdListeClasse.ItemsSource = LClasseBE;
        }

        private void cmbFilterType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // on filtre la liste affiché dans le DataGrid en fonction du choix de l'utilisateur
            List<ClasseBE> LClasseBE;
            if (cmbFilterType.SelectedItem.Equals("<Tous les Types>"))
            {
                LClasseBE = creerModifierClasseBL.listerTousLesClasse();
            }
            else
                LClasseBE = creerModifierClasseBL.listerClasseSuivantCritere("codetypeclasse = '" + cmbFilterType.SelectedItem + "'");

            grdListeClasse.ItemsSource = LClasseBE;
        }

        private void cmbFilterNiveau_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // on filtre la liste affiché dans le DataGrid en fonction du choix de l'utilisateur
            List<ClasseBE> LClasseBE;
            if (cmbFilterNiveau.SelectedItem.Equals("<Tous les Niveaux>"))
            {
                LClasseBE = creerModifierClasseBL.listerTousLesClasse();
            }
            else
                LClasseBE = creerModifierClasseBL.listerClasseSuivantCritere("codeniveau = '" + cmbFilterNiveau.SelectedItem + "'");
            
            grdListeClasse.ItemsSource = LClasseBE;

        }

        private void cmbFilterSerie_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // on filtre la liste affiché dans le DataGrid en fonction du choix de l'utilisateur
            List<ClasseBE> LClasseBE;
            if (cmbFilterSerie.SelectedItem.Equals("<Toutes les Series>"))
            {
                LClasseBE = creerModifierClasseBL.listerTousLesClasse();
            }
            else
                LClasseBE = creerModifierClasseBL.listerClasseSuivantCritere("codeserie = '" + cmbFilterSerie.SelectedItem + "'");

            grdListeClasse.ItemsSource = LClasseBE;
        }

        private void cmdAnnuler_Click(object sender, RoutedEventArgs e)
        {
            txtCode.Text = "";
            cmbCycle.Text = null;
            cmbType.Text = null;
            cmbNiveau.Text = null;
            cmbSerie.Text = null;
            txtNom.Text = "";

            etat = 0;
        }

        private void grdListeClasse_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                if (grdListeClasse.SelectedIndex != -1)
                {
                    if (MessageBox.Show("Voulez-vous vraiment le supprimer ? ", "School : Confimation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        if (creerModifierClasseBL.supprinerClasse(ListeClasses.ElementAt(grdListeClasse.SelectedIndex)))
                            ListeClasses.RemoveAt(grdListeClasse.SelectedIndex);
                        grdListeClasse.ItemsSource = ListeClasses;

                        // ------------------- Chargement de la liste des codes de Classe dans le comboBox de la fenêtre 
                        //(utile pour le filtre)
                        List<ClasseBE> LClasseBE = creerModifierClasseBL.listerTousLesClasse();
                        cmbFilterCode.ItemsSource = creerModifierClasseBL.getListCodeClasse(LClasseBE);

                    }

                    grdListeClasse.UnselectAll();
                }
            }
        }

        private void grdListeClasse_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (grdListeClasse.SelectedIndex != -1)
            {
                etat = 1;
                ClasseBE classe = new ClasseBE();

                classe = creerModifierClasseBL.rechercherClasse(ListeClasses.ElementAt(grdListeClasse.SelectedIndex));
                if (classe != null)
                {
                    // on charge les informations dans le formulaire
                    txtCode.Text = classe.codeClasse;
                    txtNom.Text = classe.nomClasse;
                    cmbCycle.Text = classe.codeCycle;
                    cmbType.Text = classe.codeTypeClasse;
                    cmbNiveau.Text = classe.codeNiveau;
                    cmbSerie.Text = classe.codeSerie;

                    ancienObjet = classe;
                }

                grdListeClasse.UnselectAll();
            }
        }

        private void cmdImprimer_Click(object sender, RoutedEventArgs e)
        {
            CreerEtat etat = new CreerEtat("Liste Classe -" + DateTime.Today.ToShortDateString(), "Liste des Classes");
            etat.obtenirEtat(grdListeClasse);
        }
    }
}
