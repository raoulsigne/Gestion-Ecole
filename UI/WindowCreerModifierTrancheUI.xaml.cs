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
    /// Interaction logic for WindowCreerModifierTrancheUI.xaml
    /// </summary>
    public partial class WindowCreerModifierTrancheUI : Window
    {
        CreerModifierTrancheBL creerModifierTrancheBL;
        private int etat; // indique si nous sommes en création (0) ou en modification (1)
         
        private TrancheBE ancienneTranche;
        // Définition d'une liste 'ListeTranches' observable de 'Tranche'
        public ObservableCollection<TrancheBE> ListeTranches { get; set; }

        // Fonction permettant de remplir le DataGrid avec les informations de la base de données
        // @param : - listObjet : la liste des objets à afficher dans le DataGrid
        public void RemplirDataGrid(List<TrancheBE> listObjet)
        {
            // Ajout de données dans la DataTable :
            var table = new DataTable();

            table.Columns.Add(new DataColumn("codetranche", typeof(string)));
            table.Columns.Add(new DataColumn("nomtranche", typeof(string)));

            if (listObjet != null)
            {

                for (int i = 0; i < listObjet.Count; i++)
                {
                    DataRow dr = table.NewRow();
                    dr["codetranche"] = listObjet.ElementAt(i).codetranche;
                    dr["nomtranche"] = listObjet.ElementAt(i).nomtranche;
                    table.Rows.Add(dr);
                }
            }

            string vCode = "";
            string vNom = "";

            ListeTranches.Clear();

            //Personnes_Table = LoadDataTable();

            foreach (DataRow row in table.Rows)
            {
                vCode = Convert.ToString(row["codetranche"]);
                vNom = Convert.ToString(row["nomtranche"]);

                ListeTranches.Add(new TrancheBE(vCode, vNom));

            }
        }

        public WindowCreerModifierTrancheUI()
        {
            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";
            Thread.CurrentThread.CurrentCulture = ci;

            InitializeComponent();

            creerModifierTrancheBL = new CreerModifierTrancheBL();
            ancienneTranche = new TrancheBE();

            etat = 0;

            // A mettre pour que le binding avec le DataGrid fonctionne !
            grdListeTranche.DataContext = this;

            // Initialisation de la collection, qui va s'afficher dans la DataGrid :
            ListeTranches = new ObservableCollection<TrancheBE>();
            List<TrancheBE> LTrancheBE = creerModifierTrancheBL.listerToutesLesTranche();
            // on met la liste "LSerieBE" dans le DataGrid
            RemplirDataGrid(LTrancheBE);

            radioManuelle.IsChecked = true;
        }

        private void cmdValider_Click(object sender, RoutedEventArgs e)
        {
            TrancheBE tranche = new TrancheBE();

            if (etat == 1)
            {       if((txtCodeTranche.Text != null && txtNom.Text != null) && (txtCodeTranche.Text != "" && txtNom.Text != "")){
                        tranche.codetranche = txtCodeTranche.Text;
                        tranche.nomtranche = txtNom.Text;

                        if (creerModifierTrancheBL.rechercherTranche(tranche) == null)
                        {
                            creerModifierTrancheBL.modifierTranche(ancienneTranche, tranche);
                            // on met à jour le DataGrid
                            List<TrancheBE> LTranche = creerModifierTrancheBL.listerToutesLesTranche();
                            RemplirDataGrid(LTranche);

                            txtNbreTranche.Text = "";
                            txtCodeTranche.Text = "";
                            txtNom.Text = "";
                            radioAutomatique.IsChecked = false;
                            radioManuelle.IsChecked = true;

                            //on remet les champs "code" et "nom" éditable
                            txtCodeTranche.IsReadOnly = false;
                            txtNom.IsReadOnly = false;
                            txtNbreTranche.IsEnabled = false;

                            etat = 0;
                        }
                        else MessageBox.Show("Il existe deja une tranche avec ce code dans le système !");

                    }else MessageBox.Show("Les champs 'Code' et 'Nom' doivent êtres renseignés !");
                    
             }
            else if (radioAutomatique.IsChecked == true) { 
                // si on a coché sur "automatique"
                if (txtNbreTranche.Text != null && txtNbreTranche.Text != "") {
                    
                    for (int i = 0; i < Convert.ToInt16(txtNbreTranche.Text); i++) {
                        tranche.codetranche = "T" + (i+1);
                        tranche.nomtranche = "Tranche" + (i + 1);

                        if (creerModifierTrancheBL.rechercherTranche(tranche) == null)
                        {
                            creerModifierTrancheBL.creerTranche(tranche.codetranche, tranche.nomtranche);
                            //MessageBox.Show(" Echec Enregistrement Tranche [ code : " + tranche.codetranche + ", nom : " + tranche.nomtranche + " ]");
                        }
                        else MessageBox.Show("Il existe deja une tranche avec le code '"+ tranche.codetranche +"' dans le système !");
                    }
                    // on met à jour le DataGrid
                    List<TrancheBE> LTranche = creerModifierTrancheBL.listerToutesLesTranche();
                    RemplirDataGrid(LTranche);

                    txtNbreTranche.Text = "";
                    txtCodeTranche.Text = "";
                    txtNom.Text = "";
                    radioAutomatique.IsChecked = false;
                    radioManuelle.IsChecked = true;

                    //on remet les champs "code" et "nom" éditable
                    txtCodeTranche.IsReadOnly = false;
                    txtNom.IsReadOnly = false;
                    txtNbreTranche.IsEnabled = false;
                }
                else
                    MessageBox.Show("Vous devez indiquer le nombre de tranches !");
            }
            else if (radioManuelle.IsChecked == true) { 
                // si on a coché sur "manuelle"
                    if((txtCodeTranche.Text != null && txtNom.Text != null) && (txtCodeTranche.Text !="" && txtNom.Text != "")){
                        tranche.codetranche = txtCodeTranche.Text;
                        tranche.nomtranche = txtNom.Text;

                        if (creerModifierTrancheBL.rechercherTranche(tranche) == null)
                        {
                            creerModifierTrancheBL.creerTranche(tranche.codetranche, tranche.nomtranche);

                            // on met à jour le DataGrid
                            List<TrancheBE> LTranche = creerModifierTrancheBL.listerToutesLesTranche();
                            RemplirDataGrid(LTranche);

                            txtNbreTranche.Text = "";
                            txtCodeTranche.Text = "";
                            txtNom.Text = "";
                            radioAutomatique.IsChecked = false;
                            radioManuelle.IsChecked = true;

                            //on remet les champs "code" et "nom" éditable
                            txtCodeTranche.IsReadOnly = false;
                            txtNom.IsReadOnly = false;
                            txtNbreTranche.IsEnabled = true;
                        }
                        else MessageBox.Show("Il existe deja une tranche avec le code '" + tranche.codetranche + "' dans le système !");
                    }
                    else MessageBox.Show("Les champs 'Code' et 'Nom' doivent êtres renseignés ! ");
            }


        }

        private void cmdAnnuler_Click(object sender, RoutedEventArgs e)
        {
            txtNbreTranche.Text = "";
            txtCodeTranche.Text = "";
            txtNom.Text = "";
            radioAutomatique.IsChecked = false;
            radioManuelle.IsChecked = true;

            //on remet les champs "code" et "nom" éditable
            txtCodeTranche.IsReadOnly = false;
            txtNom.IsReadOnly = false;
            txtNbreTranche.IsEnabled = false;

            etat = 0;
        }

        private void grdListeTranche_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                if (MessageBox.Show("Voulez-vous vraiment le supprimer ? ", "School : Confimation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    if (creerModifierTrancheBL.supprinerTranche(ListeTranches.ElementAt(grdListeTranche.SelectedIndex)))
                        ListeTranches.RemoveAt(grdListeTranche.SelectedIndex);
                    grdListeTranche.ItemsSource = ListeTranches;

                }

                grdListeTranche.UnselectAll();
            }
        }

        private void grdListeTranche_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (grdListeTranche.SelectedIndex != -1)
            {
                etat = 1;
                TrancheBE tranche = new TrancheBE();

                tranche = creerModifierTrancheBL.rechercherTranche(ListeTranches.ElementAt(grdListeTranche.SelectedIndex));
                if (tranche != null)
                {
                    // on charge les informations dans le formulaire
                    txtCodeTranche.Text = tranche.codetranche;
                    txtNom.Text = tranche.nomtranche;

                    ancienneTranche.codetranche = tranche.codetranche;
                    ancienneTranche.nomtranche = tranche.nomtranche;

                    //on remet les champs "code" et "nom" éditable
                    txtCodeTranche.IsReadOnly = false;
                    txtNom.IsReadOnly = false;
                    txtNbreTranche.IsEnabled = false;
                }

                grdListeTranche.UnselectAll();
            }
        }

        private void radioAutomatique_Checked(object sender, RoutedEventArgs e)
        {
            //on vide tous les champs texte
            txtNbreTranche.Text = "";
            txtCodeTranche.Text = "";
            txtNom.Text = "";
            // on grise les champs "code" et "nom"
            txtCodeTranche.IsReadOnly = true;
            txtCodeTranche.IsEnabled = false;

            txtNom.IsReadOnly = true;
            txtNom.IsEnabled = false;

            txtNbreTranche.IsEnabled = true;

            txtNbreTranche.Visibility = System.Windows.Visibility.Visible;
            lblNbreTranche.Visibility = System.Windows.Visibility.Visible;

            txtCodeTranche.Visibility = System.Windows.Visibility.Hidden;
            lblCodeTrance.Visibility = System.Windows.Visibility.Hidden;
            txtNom.Visibility = System.Windows.Visibility.Hidden;
            lblNom.Visibility = System.Windows.Visibility.Hidden;
        }

        private void radioManuelle_Checked(object sender, RoutedEventArgs e)
        {
            //on vide tous les champs texte
            txtNbreTranche.Text = "";
            txtCodeTranche.Text = "";
            txtNom.Text = "";

            //on grise le champ "nombre de tranche"
            txtNbreTranche.IsEnabled = false;

            txtCodeTranche.IsReadOnly = false;
            txtCodeTranche.IsEnabled = true;

            txtNom.IsReadOnly = false;
            txtNom.IsEnabled = true;

            txtNbreTranche.Visibility = System.Windows.Visibility.Hidden;
            lblNbreTranche.Visibility = System.Windows.Visibility.Hidden;

            txtCodeTranche.Visibility = System.Windows.Visibility.Visible;
            lblCodeTrance.Visibility = System.Windows.Visibility.Visible;
            txtNom.Visibility = System.Windows.Visibility.Visible;
            lblNom.Visibility = System.Windows.Visibility.Visible;
        }

        private void txtNbreTranche_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Utilitaires.IsTextAllowed(e.Text);
        }

        private void cmbFermer_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cmdImprimer_Click(object sender, RoutedEventArgs e)
        {
            CreerEtat etat = new CreerEtat("Tranches -" + DateTime.Today.ToShortDateString(), "Liste des Tranches");
            etat.obtenirEtat(grdListeTranche);
        }
    }
}
