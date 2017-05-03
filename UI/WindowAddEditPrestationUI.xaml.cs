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
    /// Interaction logic for WindowAddEditPrestationUI.xaml
    /// </summary>
    public partial class WindowAddEditPrestationUI : Window
    {
        CreerModifierPrestationBL creerModifierPrestationBL;
        private int etat; // idique si nous sommes en création (0) ou en modification (1)

        private PrestationBE ancienObjet; //garde l'ancien état de l'objet, qui sera utilisé pour la modification


        // Définition d'une liste 'ListePrestations' observable de 'Prestation'
        public ObservableCollection<PrestationBE> ListePrestations { get; set; }

        // Fonction permettant de remplir le DataGrid avec les informations de la base de données
        // @param : - listObjet : la liste des objets à afficher dans le DataGrid
        public void RemplirDataGrid(List<PrestationBE> listObjet)
        {
            // Ajout de données dans la DataTable :
            var table = new DataTable();

            table.Columns.Add(new DataColumn("codePrestation", typeof(string)));
            table.Columns.Add(new DataColumn("nomPrestation", typeof(string)));
            table.Columns.Add(new DataColumn("priorite", typeof(string)));

            if (listObjet != null)
            {

                for (int i = 0; i < listObjet.Count; i++)
                {
                    DataRow dr = table.NewRow();
                    dr["codePrestation"] = listObjet.ElementAt(i).codePrestation;
                    dr["nomPrestation"] = listObjet.ElementAt(i).nomPrestation;
                    dr["priorite"] = listObjet.ElementAt(i).priorite;

                    table.Rows.Add(dr);
                }
            }

            string vCode = "";
            string vNom = "";
            int vPriorite = 0;

            ListePrestations.Clear();

            //Personnes_Table = LoadDataTable();

            foreach (DataRow row in table.Rows)
            {
                vCode = Convert.ToString(row["codePrestation"]);
                vNom = Convert.ToString(row["nomPrestation"]);
                vPriorite = Convert.ToInt16(row["priorite"]);

                ListePrestations.Add(new PrestationBE(vCode, vNom, vPriorite));

            }
        }

        public WindowAddEditPrestationUI()
        {
            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";
            Thread.CurrentThread.CurrentCulture = ci;

            InitializeComponent();

            creerModifierPrestationBL = new CreerModifierPrestationBL();

            etat = 0;

            ancienObjet = new PrestationBE();

            // A mettre pour que le binding avec le DataGrid fonctionne !
            grdListePrestation.DataContext = this;

            // Initialisation de la collection, qui va s'afficher dans la DataGrid :
            ListePrestations = new ObservableCollection<PrestationBE>();
            List<PrestationBE> LPrestationBE = creerModifierPrestationBL.listerToutesLesPrestation();
            // on met la liste "LPrestationBE" dans le DataGrid
            RemplirDataGrid(LPrestationBE);
        }

        private void cmdEnregistrer_Click(object sender, RoutedEventArgs e)
        {
            if ((txtCode.Text != null && txtNom.Text != null && txtPriorite.Text != null) && (txtCode.Text != "" && txtNom.Text != "" && txtPriorite.Text != ""))
            { // si tous les champs sont non vides

                PrestationBE prestation = new PrestationBE();
                prestation.codePrestation = txtCode.Text;
                prestation.nomPrestation = txtNom.Text;
                prestation.priorite = Convert.ToInt16(txtPriorite.Text);

                if (etat == 1)
                {
                    creerModifierPrestationBL.modifierPrestation(ancienObjet, prestation);
                    List<PrestationBE> LPrestationBE = creerModifierPrestationBL.listerToutesLesPrestation();
                    // on met la liste "LPrestationBE" dans le DataGrid
                    RemplirDataGrid(LPrestationBE);

                    txtCode.Text = "";
                    txtNom.Text = "";
                    txtPriorite.Text = "";
                    etat = 0;
                }
                else if (creerModifierPrestationBL.rechercherPrestation(new PrestationBE(txtCode.Text, txtNom.Text, Convert.ToInt16(txtPriorite.Text))) == null)
                { // si un cycle possédant le même code n'existe pas deja dans la BD

                    if (creerModifierPrestationBL.creerPrestation(txtCode.Text, txtNom.Text, Convert.ToInt16(txtPriorite.Text)))
                    { // si l'nregistrement a réussi

                        MessageBox.Show("Enregistrement Prestation [" + txtCode.Text + ", " + txtNom.Text + "] " + " : Opération réussie");
                        txtCode.Text = "";
                        txtNom.Text = "";
                        txtPriorite.Text = "";

                        List<PrestationBE> LPrestationBE = creerModifierPrestationBL.listerToutesLesPrestation();
                        //on rafraichir le DataGrid
                        RemplirDataGrid(LPrestationBE);

                    }
                    else MessageBox.Show("Echec enregistrement : une erreure est survenue !");
                }
                else MessageBox.Show("Une Prestation ayant le code \"" + txtCode.Text + "\" existe deja dans le système \n \n Veuillez changer de code SVP !");
            }
            else MessageBox.Show("Tous les champs doivent pas être remplis !");
        }

        private void grdListePrestation_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                if (grdListePrestation.SelectedIndex != -1)
                {
                    if (MessageBox.Show("Voulez-vous vraiment le supprimer ? ", "School : Confimation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        if (creerModifierPrestationBL.supprinerPrestation(ListePrestations.ElementAt(grdListePrestation.SelectedIndex)))
                            ListePrestations.RemoveAt(grdListePrestation.SelectedIndex);

                        grdListePrestation.ItemsSource = ListePrestations;

                    }

                    grdListePrestation.UnselectAll();
                }
            }
        }

        private void grdListePrestation_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (grdListePrestation.SelectedIndex != -1)
            {
                etat = 1;
                PrestationBE prestationBE = new PrestationBE();

                prestationBE = creerModifierPrestationBL.rechercherPrestation(ListePrestations.ElementAt(grdListePrestation.SelectedIndex));
                if (prestationBE != null)
                {
                    // on charge les informations dans le formulaire
                    txtCode.Text = prestationBE.codePrestation;
                    txtNom.Text = prestationBE.nomPrestation;
                    txtPriorite.Text = Convert.ToString(prestationBE.priorite);

                    ancienObjet = prestationBE;
                }

                grdListePrestation.UnselectAll();
            }
        }

        private void cmdAnnuler_Click(object sender, RoutedEventArgs e)
        {
            txtCode.Text = "";
            txtNom.Text = "";
            txtPriorite.Text = "";
            etat = 0;
        }

        private void cmdFermer_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void txtPriorite_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Utilitaires.IsTextAllowed(e.Text);
        }

        private void cmdImprimer_Click(object sender, RoutedEventArgs e)
        {
            CreerEtat etat = new CreerEtat("Prestations -" + DateTime.Today.ToShortDateString(), "Liste des Prestations");
            etat.obtenirEtat(grdListePrestation);
        }
    }
}
