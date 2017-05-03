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
    /// Interaction logic for WindowAddEditTypeEvaluationUI.xaml
    /// </summary>
    public partial class WindowAddEditTypeEvaluationUI : Window
    {
        CreerModifierTypeEvaluationBL creerModifierTypeEvaluationBL ;
        private int etat; // indique si nous sommes en création (0) ou en modification (1)

        private TypeevaluationBE ancienObjet; //garde l'ancien état de l'objet, qui sera utilisé pour la modification

        // Définition d'une liste 'ListeTypeEvaluations' observable de 'TypeEvaluation'
        public ObservableCollection<TypeevaluationBE> ListeTypeEvaluations { get; set; }

        // Fonction permettant de remplir le DataGrid avec les informations de la base de données
        // @param : - listObjet : la liste des objets à afficher dans le DataGrid
        public void RemplirDataGrid(List<TypeevaluationBE> listObjet)
        {
            // Ajout de données dans la DataTable :
            var table = new DataTable();

            table.Columns.Add(new DataColumn("codeevaluation", typeof(string)));
            table.Columns.Add(new DataColumn("nomeval", typeof(string)));

            if (listObjet != null)
            {

                for (int i = 0; i < listObjet.Count; i++)
                {
                    DataRow dr = table.NewRow();
                    dr["codeevaluation"] = listObjet.ElementAt(i).codeevaluation;
                    dr["nomeval"] = listObjet.ElementAt(i).nomeval;

                    table.Rows.Add(dr);
                }
            }

            string vCode = "";
            string vNom = "";

            ListeTypeEvaluations.Clear();

            foreach (DataRow row in table.Rows)
            {
                vCode = Convert.ToString(row["codeevaluation"]);
                vNom = Convert.ToString(row["nomeval"]);

                ListeTypeEvaluations.Add(new TypeevaluationBE(vCode, vNom));

            }

            grdListeTypeEvaluation.ItemsSource = ListeTypeEvaluations;
        }

        public WindowAddEditTypeEvaluationUI()
        {
            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";
            Thread.CurrentThread.CurrentCulture = ci;

            InitializeComponent();

            creerModifierTypeEvaluationBL = new CreerModifierTypeEvaluationBL();

            etat = 0;

            ancienObjet = new TypeevaluationBE();

            // A mettre pour que le binding avec le DataGrid fonctionne !
            grdListeTypeEvaluation.DataContext = this;

            // Initialisation de la collection, qui va s'afficher dans la DataGrid :
            ListeTypeEvaluations = new ObservableCollection<TypeevaluationBE>();
            List<TypeevaluationBE> LTypeevaluationBE = creerModifierTypeEvaluationBL.listerTousLesTypeEvaluations();
            // on met la liste "TypeevaluationBE" dans le DataGrid
            RemplirDataGrid(LTypeevaluationBE);
        }

        private void cmdEnregistrer_Click(object sender, RoutedEventArgs e)
        {
            if ((txtCode.Text != null && txtNom.Text != null)
                && (txtCode.Text != "" && txtNom.Text != ""))
            {
                TypeevaluationBE typeEvaluation = new TypeevaluationBE();
                typeEvaluation.codeevaluation = txtCode.Text;
                typeEvaluation.nomeval = txtNom.Text;

                if (etat == 1)
                {
                    creerModifierTypeEvaluationBL.modifierTypeEvaluation(ancienObjet, typeEvaluation);
                    List<TypeevaluationBE> LTypeevaluationBE = creerModifierTypeEvaluationBL.listerTousLesTypeEvaluations();
                    // on met la liste "LTypeevaluationBE" dans le DataGrid
                    RemplirDataGrid(LTypeevaluationBE);

                    txtCode.Text = "";
                    txtNom.Text = "";
                   
                    etat = 0;
                }
                else if (creerModifierTypeEvaluationBL.rechercherTypeEvaluation(typeEvaluation) == null)
                {
                    if (creerModifierTypeEvaluationBL.creerTypeEvaluation(txtCode.Text, txtNom.Text))
                    {
                        MessageBox.Show("Enregistrement Type Evaluation [" + txtCode.Text + ", " + txtNom.Text +", ] : Opération réussie");
                        txtCode.Text = "";
                        txtNom.Text = "";

                        // Initialisation de la collection, qui va s'afficher dans la DataGrid :
                        List<TypeevaluationBE> LTypeevaluationBE = creerModifierTypeEvaluationBL.listerTousLesTypeEvaluations();
                        // on met la liste "LTypeevaluationBE" dans le DataGrid
                        RemplirDataGrid(LTypeevaluationBE);

                    }
                    else MessageBox.Show("Echec enregistrement : une erreure est survenue !");
                }
                else MessageBox.Show("Une Matière ayant le code \"" + txtCode.Text + "\" existe deja dans le système \n \n Veuillez changer de code SVP !");
            }
            else MessageBox.Show("Tous les champs doivent êtres renseignés !");
        }

        private void cmdAnnuler_Click(object sender, RoutedEventArgs e)
        {
            txtCode.Text = "";
            txtNom.Text = "";
           
            etat = 0;
        }

        private void grdListeTypeEvaluation_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                if (grdListeTypeEvaluation.SelectedIndex != -1)
                {
                    if (MessageBox.Show("Voulez-vous vraiment le supprimer ? ", "School : Confimation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        if (creerModifierTypeEvaluationBL.supprinerTypeEvaluation(ListeTypeEvaluations.ElementAt(grdListeTypeEvaluation.SelectedIndex)))
                            ListeTypeEvaluations.RemoveAt(grdListeTypeEvaluation.SelectedIndex);
                        grdListeTypeEvaluation.ItemsSource = ListeTypeEvaluations;

                    }

                    grdListeTypeEvaluation.UnselectAll();
                }

            }
        }

        private void grdListeTypeEvaluation_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (grdListeTypeEvaluation.SelectedIndex != -1)
            {
                etat = 1;
                TypeevaluationBE Typeevaluation = new TypeevaluationBE();

                Typeevaluation = creerModifierTypeEvaluationBL.rechercherTypeEvaluation(ListeTypeEvaluations.ElementAt(grdListeTypeEvaluation.SelectedIndex));
                if (Typeevaluation != null)
                {
                    // on charge les informations dans le formulaire
                    txtCode.Text = Typeevaluation.codeevaluation;
                    txtNom.Text = Typeevaluation.nomeval;

                    ancienObjet = Typeevaluation;
                }

                grdListeTypeEvaluation.UnselectAll();
            }
        }

        private void cmdFermer_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cmdImprimer_Click(object sender, RoutedEventArgs e)
        {
            CreerEtat etat = new CreerEtat("Type Evaluation -" + DateTime.Today.ToShortDateString(), "Liste des Types d'évaluations");
            etat.obtenirEtat(grdListeTypeEvaluation);
        }
    }
}
