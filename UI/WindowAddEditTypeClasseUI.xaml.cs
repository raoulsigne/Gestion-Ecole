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
    /// Interaction logic for WindowAddEditTypeClasse.xaml
    /// </summary>
    public partial class WindowAddEditTypeClasseUI : Window
    {
        CreerModifierTypeClasseBL creerModifierTypeClasseBL;
        private int etat; // idique si nous sommes en création (0) ou en modification (1)

        

        private TypeclasseBE ancienObjet; //garde l'ancien état de l'objet, qui sera utilisé pour la modification


        // Définition d'une liste 'ListeTypeClasse' observable de 'Série'
        public ObservableCollection<TypeclasseBE> ListeTypeClasse { get; set; }

        // Fonction permettant de remplir le DataGrid avec les informations de la base de données
        // @param : - listObjet : la liste des objets à afficher dans le DataGrid
        public void RemplirDataGrid(List<TypeclasseBE> listObjet)
        {
            // Ajout de données dans la DataTable :
            var table = new DataTable();

            table.Columns.Add(new DataColumn("code", typeof(string)));
            table.Columns.Add(new DataColumn("nom", typeof(string)));
            table.Columns.Add(new DataColumn("frais", typeof(string)));

            if (listObjet != null)
            {

                for (int i = 0; i < listObjet.Count; i++)
                {
                    DataRow dr = table.NewRow();
                    dr["code"] = listObjet.ElementAt(i).codetypeclasse;
                    dr["nom"] = listObjet.ElementAt(i).nomtypeclasse;
                    dr["frais"] = Convert.ToString(listObjet.ElementAt(i).fraisinscription);
                    table.Rows.Add(dr);
                }
            }

            string vCode = "";
            string vNom = "";
            decimal vFraisInscription;

            ListeTypeClasse.Clear();

            //Personnes_Table = LoadDataTable();

            foreach (DataRow row in table.Rows)
            {
                vCode = Convert.ToString(row["code"]);
                vNom = Convert.ToString(row["nom"]);
                vFraisInscription = Convert.ToDecimal(row["frais"]);

                ListeTypeClasse.Add(new TypeclasseBE(vCode, vNom, vFraisInscription));

            }
        }


        public WindowAddEditTypeClasseUI()
        {
            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";
            Thread.CurrentThread.CurrentCulture = ci;


            InitializeComponent();

            creerModifierTypeClasseBL = new CreerModifierTypeClasseBL();

            etat = 0;

            ancienObjet = new TypeclasseBE();

            // A mettre pour que le binding avec le DataGrid fonctionne !
            grdListeTypeClasse.DataContext = this;

            // Initialisation de la collection, qui va s'afficher dans la DataGrid :
            ListeTypeClasse = new ObservableCollection<TypeclasseBE>();
            List<TypeclasseBE> LtypeClasseBE = creerModifierTypeClasseBL.listerTousLesTypeClasse();
            // on met la liste "LSerieBE" dans le DataGrid
            RemplirDataGrid(LtypeClasseBE);

            // ------------------- Chargement de la liste des codes de série dans le comboBox de la fenêtre 
            //(utile pour le filtre)
            cmbFilterCode.ItemsSource = creerModifierTypeClasseBL.getListCodeTypeClasse(LtypeClasseBE);

            // ------------------- Chargement de la liste des noms de série dans le comboBox de la fenêtre 
            //(utile pour le filtre)
            cmbFilterNom.ItemsSource = creerModifierTypeClasseBL.getListNomTypeClasse(LtypeClasseBE);
        }

        private void cmdFermer_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cmdValider_Click(object sender, RoutedEventArgs e)
        {
            if ((txtCode.Text != null && txtNom.Text != null && txtFraisInscription.Text != null) && (txtCode.Text != "" && txtNom.Text != "" && txtFraisInscription.Text != ""))
            {
                TypeclasseBE typeClasse = new TypeclasseBE();
                typeClasse.codetypeclasse = txtCode.Text;
                typeClasse.nomtypeclasse = txtNom.Text;
                typeClasse.fraisinscription = Convert.ToInt32(txtFraisInscription.Text);

                if (etat == 1)
                {
                    creerModifierTypeClasseBL.modifierTypeClasse(ancienObjet, typeClasse);
                    List<TypeclasseBE> LTypeClasseBE = creerModifierTypeClasseBL.listerTousLesTypeClasse();
                    // on met la liste "LSerieBE" dans le DataGrid
                    RemplirDataGrid(LTypeClasseBE);

                    // ------------------- Chargement de la liste des codes de série dans le comboBox de la fenêtre 
                    //(utile pour le filtre)
                    cmbFilterCode.ItemsSource = creerModifierTypeClasseBL.getListCodeTypeClasse(LTypeClasseBE);

                    // ------------------- Chargement de la liste des noms de série dans le comboBox de la fenêtre 
                    //(utile pour le filtre)
                    cmbFilterNom.ItemsSource = creerModifierTypeClasseBL.getListNomTypeClasse(LTypeClasseBE);

                    txtCode.Text = "";
                    txtNom.Text = "";
                    txtFraisInscription.Text = "";

                    etat = 0;
                }
                else if (creerModifierTypeClasseBL.rechercherTypeClasse(typeClasse) == null)
                {
                    if (creerModifierTypeClasseBL.creerTypeClasse(txtCode.Text, txtNom.Text, Convert.ToInt32(txtFraisInscription.Text)))
                    {
                        MessageBox.Show("Enregistrement Type de Classe [" + txtCode.Text + ", " + txtNom.Text + ", "+ txtFraisInscription.Text +" ] " + " : Opération réussie");
                        txtCode.Text = "";
                        txtNom.Text = "";
                        txtFraisInscription.Text = "";

                        List<TypeclasseBE> LTypeclasseBE = creerModifierTypeClasseBL.listerTousLesTypeClasse();
                        //on rafraichir le DataGrid
                        RemplirDataGrid(LTypeclasseBE);
                        // ------------------- Chargement de la liste des codes de Type de classe dans le comboBox de la fenêtre 
                        //(utile pour le filtre)
                        cmbFilterCode.ItemsSource = creerModifierTypeClasseBL.getListCodeTypeClasse(LTypeclasseBE);

                        // ------------------- Chargement de la liste des noms de Type de classe dans le comboBox de la fenêtre 
                        //(utile pour le filtre)
                        cmbFilterNom.ItemsSource = creerModifierTypeClasseBL.getListNomTypeClasse(LTypeclasseBE);
                    }
                    else MessageBox.Show("Echec enregistrement : une erreure est survenue !");
                }
                else MessageBox.Show("Un Type de Classe ayant le code \"" + txtCode.Text + "\" existe deja dans le système \n \n Veuillez changer de code SVP !");
            }
            else MessageBox.Show("Tous les champs doivent pas être remplis !");
        }

        private void cmbFilterCode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // on filtre la liste affiché dans le DataGrid en fonction du choix de l'utilisateur
            List<TypeclasseBE> LTypeclasseBE;
            if (cmbFilterNom.Text != null && cmbFilterNom.Text != "")
            {
                if (cmbFilterCode.SelectedItem.Equals("<Tous les Codes>"))
                {
                    if (cmbFilterNom.SelectedItem.Equals("<Tous les Noms>"))
                        LTypeclasseBE = creerModifierTypeClasseBL.listerTousLesTypeClasse();
                    else
                        LTypeclasseBE = creerModifierTypeClasseBL.listerTypeClasseSuivantCritere("nomtypeclasse = '" + cmbFilterNom.SelectedItem + "'");
                }
                else
                    if (cmbFilterNom.SelectedItem.Equals("<Tous les Noms>"))
                        LTypeclasseBE = creerModifierTypeClasseBL.listerTypeClasseSuivantCritere("codetypeclasse = '" + cmbFilterCode.SelectedItem + "'");
                    else
                        LTypeclasseBE = creerModifierTypeClasseBL.listerTypeClasseSuivantCritere("codetypeclasse = '" + cmbFilterCode.SelectedItem + "' AND nomtypeclasse = '" + cmbFilterNom.SelectedItem + "'");
            }
            else
                if (cmbFilterCode.SelectedItem.Equals("<Tous les Codes>"))
                    LTypeclasseBE = creerModifierTypeClasseBL.listerTousLesTypeClasse();
                else
                    LTypeclasseBE = creerModifierTypeClasseBL.listerTypeClasseSuivantCritere("codetypeclasse = '" + cmbFilterCode.SelectedItem + "'");

            //on rafraichir le DataGrid
            RemplirDataGrid(LTypeclasseBE);
        }

        private void cmbFilterNom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // on filtre la liste affiché dans le DataGrid en fonction du choix de l'utilisateur
            List<TypeclasseBE> LTypeclasseBE;
            if (cmbFilterCode.Text != null && cmbFilterCode.Text != "")
            {
                if (cmbFilterNom.SelectedItem.Equals("<Tous les Noms>"))
                {
                    if (cmbFilterCode.SelectedItem.Equals("<Tous les Codes>"))
                        LTypeclasseBE = creerModifierTypeClasseBL.listerTousLesTypeClasse();
                    else
                        LTypeclasseBE = creerModifierTypeClasseBL.listerTypeClasseSuivantCritere("codetypeclasse = '" + cmbFilterCode.SelectedItem + "'");
                }
                else
                    if (cmbFilterCode.SelectedItem.Equals("<Tous les Codes>"))
                        LTypeclasseBE = creerModifierTypeClasseBL.listerTypeClasseSuivantCritere("nomtypeclasse = '" + cmbFilterNom.SelectedItem + "'");
                    else
                        LTypeclasseBE = creerModifierTypeClasseBL.listerTypeClasseSuivantCritere("codetypeclasse = '" + cmbFilterCode.SelectedItem + "' AND nomtypeclasse = '" + cmbFilterNom.SelectedItem + "'");
            }
            else
                if (cmbFilterNom.SelectedItem.Equals("<Tous les Noms>"))
                    LTypeclasseBE = creerModifierTypeClasseBL.listerTousLesTypeClasse();
                else
                    LTypeclasseBE = creerModifierTypeClasseBL.listerTypeClasseSuivantCritere("nomtypeclasse = '" + cmbFilterNom.SelectedItem + "'");

            //on rafraichir le DataGrid
            RemplirDataGrid(LTypeclasseBE);
        }

        private void cmdAnnuler_Click(object sender, RoutedEventArgs e)
        {
            txtCode.Text = "";
            txtNom.Text = "";
            txtFraisInscription.Text = "";
            etat = 0;
        }

        private void grdListeNiveau_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                if (grdListeTypeClasse.SelectedIndex != -1)
                {
                    if (MessageBox.Show("Voulez-vous vraiment le supprimer ? ", "School : Confimation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        if (creerModifierTypeClasseBL.supprinerTypeClasse(ListeTypeClasse.ElementAt(grdListeTypeClasse.SelectedIndex)))
                            ListeTypeClasse.RemoveAt(grdListeTypeClasse.SelectedIndex);
                        grdListeTypeClasse.ItemsSource = ListeTypeClasse;

                        // ------------------- Chargement de la liste des codes de série dans le comboBox de la fenêtre 
                        //(utile pour le filtre)
                        List<TypeclasseBE> LTypeClasseBE = creerModifierTypeClasseBL.listerTousLesTypeClasse();
                        cmbFilterCode.ItemsSource = creerModifierTypeClasseBL.getListCodeTypeClasse(LTypeClasseBE);

                        // ------------------- Chargement de la liste des noms de série dans le comboBox de la fenêtre 
                        //(utile pour le filtre)
                        cmbFilterNom.ItemsSource = creerModifierTypeClasseBL.getListNomTypeClasse(LTypeClasseBE);
                    }

                    grdListeTypeClasse.UnselectAll();
                }
            }
        }

        private void grdListeNiveau_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (grdListeTypeClasse.SelectedIndex != -1)
            {
                etat = 1;
                TypeclasseBE typeClasse = new TypeclasseBE();

                typeClasse = creerModifierTypeClasseBL.rechercherTypeClasse(ListeTypeClasse.ElementAt(grdListeTypeClasse.SelectedIndex));
                if (typeClasse != null)
                {
                    // on charge les informations dans le formulaire
                    txtCode.Text = typeClasse.codetypeclasse;
                    txtNom.Text = typeClasse.nomtypeclasse;
                    txtFraisInscription.Text = Convert.ToString(typeClasse.fraisinscription).Split(',')[0];

                    ancienObjet = typeClasse;
                }

                grdListeTypeClasse.UnselectAll();

            }
        }

        private void txtFraisInscription_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Utilitaires.IsTextAllowed(e.Text);
        }

        private void cmdImprimer_Click(object sender, RoutedEventArgs e)
        {
            CreerEtat etat = new CreerEtat("Types Classes -" + DateTime.Today.ToShortDateString(), "Liste des Types de Classe");
            etat.obtenirEtat(grdListeTypeClasse);
        }
    }
}
