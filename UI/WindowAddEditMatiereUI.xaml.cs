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
    /// Interaction logic for WindowAddEditMatiereUI.xaml
    /// </summary>
    public partial class WindowAddEditMatiereUI : Window
    {
        CreerModifierMatiereBL creerModifierMatiereBL;
        private int etat; // indique si nous sommes en création (0) ou en modification (1)
        int annee;
        ParametresBE param; // les paramètres de l'application

        private MatiereBE ancienObjet; //garde l'ancien état de l'objet, qui sera utilisé pour la modification


        // Définition d'une liste 'Listematieres' observable de 'Matières'
        public ObservableCollection<MatiereBE> ListeMatieres { get; set; }

        // Fonction permettant de remplir le DataGrid avec les informations de la base de données
        // @param : - listObjet : la liste des objets à afficher dans le DataGrid
        public void RemplirDataGrid(List<MatiereBE> listObjet)
        {
            // Ajout de données dans la DataTable :
            var table = new DataTable();

            table.Columns.Add(new DataColumn("codeMat", typeof(string)));
            table.Columns.Add(new DataColumn("nomMat", typeof(string)));
            table.Columns.Add(new DataColumn("nameMat", typeof(string)));
            table.Columns.Add(new DataColumn("annee", typeof(string)));

            if (listObjet != null)
            {

                for (int i = 0; i < listObjet.Count; i++)
                {
                    DataRow dr = table.NewRow();
                    dr["codeMat"] = listObjet.ElementAt(i).codeMat;
                    dr["nomMat"] = listObjet.ElementAt(i).nomMat;
                    dr["nameMat"] = Convert.ToString(listObjet.ElementAt(i).nameMat);
                    dr["annee"] = Convert.ToString(listObjet.ElementAt(i).annee);

                    table.Rows.Add(dr);
                }
            }

            string vCodeMat = "";
            string vNomMat = "";
            string vNameMat = "";
            int vAnnee = 0;

            ListeMatieres.Clear();

            //Personnes_Table = LoadDataTable();

            foreach (DataRow row in table.Rows)
            {
                vCodeMat = Convert.ToString(row["codeMat"]);
                vNomMat = Convert.ToString(row["nomMat"]);
                vNameMat = Convert.ToString(row["nameMat"]);
                vAnnee = Convert.ToInt16(row["annee"]);

                ListeMatieres.Add(new MatiereBE(vCodeMat, vNomMat, vNameMat, Convert.ToInt16(vAnnee)));

            }

            grdListeMatiere.ItemsSource = ListeMatieres;
        }


        public WindowAddEditMatiereUI()
        {
            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";
            Thread.CurrentThread.CurrentCulture = ci;

            InitializeComponent();

            creerModifierMatiereBL = new CreerModifierMatiereBL();

            param = new ParametresBE();
            param = creerModifierMatiereBL.getParametres();

            etat = 0;

            ancienObjet = new MatiereBE();

            // A mettre pour que le binding avec le DataGrid fonctionne !
            grdListeMatiere.DataContext = this;

            // Initialisation de la collection, qui va s'afficher dans la DataGrid :
            ListeMatieres = new ObservableCollection<MatiereBE>();

            List<MatiereBE> LMatiereBE = new List<MatiereBE>();
            //if(param != null)
            //     LMatiereBE = creerModifierMatiereBL.listerMatieresSuivantCritere("annee = '"+param.annee+"'");

            LMatiereBE = creerModifierMatiereBL.listerToutesLesMatieres();

                // on met la liste "LMatiereBE" dans le DataGrid
            RemplirDataGrid(LMatiereBE);

            param = creerModifierMatiereBL.getParametres();
            if (param != null)
            {
                annee = param.annee;

                txtAnnee.Text = Convert.ToString(param.annee);
                txtAnneeScolaire.Text = (param.annee - 1).ToString();
            }
            else
            {
                txtAnnee.Text = "";
                txtAnneeScolaire.Text = "";
            }


        }

        private void cmdEnregistrer_Click(object sender, RoutedEventArgs e)
        {
            if ((txtCode.Text != null && txtNom.Text != null && txtAnneeScolaire.Text != null)
                && (txtCode.Text != "" && txtNom.Text != "" && txtAnneeScolaire.Text != ""))
            {
                MatiereBE matiere = new MatiereBE();
                matiere.codeMat = txtCode.Text;
                matiere.nomMat = txtNom.Text;

                if (txtName.Text == null || txtName.Text == "")
                    matiere.nameMat = "";
                else matiere.nameMat = txtName.Text;

                matiere.annee = Convert.ToInt16(txtAnnee.Text);

                if (etat == 1)
                {
                    creerModifierMatiereBL.modifierMatiere(ancienObjet, matiere);
                    List<MatiereBE> LMatiereBE = creerModifierMatiereBL.listerToutesLesMatieres();
                    // on met la liste "LMatiereBE" dans le DataGrid
                    RemplirDataGrid(LMatiereBE);

                    txtCode.Text = "";
                    txtNom.Text = "";
                    txtName.Text = "";
                    //txtAnneeScolaire.Text = "";

                    param = creerModifierMatiereBL.getParametres();
                    if (param != null)
                    {
                        annee = param.annee;
                        txtAnnee.Text = Convert.ToString(param.annee);
                        txtAnneeScolaire.Text = (param.annee - 1).ToString();
                    }
                    else
                    {
                        txtAnnee.Text = "";
                        txtAnneeScolaire.Text = "";

                    }

                    etat = 0;
                }
                else if (creerModifierMatiereBL.rechercherMatiere(matiere) == null)
                {
                    if (creerModifierMatiereBL.creerMatiere(txtCode.Text, txtNom.Text, txtName.Text, Convert.ToInt16(txtAnnee.Text)))
                    {
                        MessageBox.Show("Enregistrement Matière [" + txtCode.Text + ", " + txtNom.Text + ", " + txtName.Text + ", "+txtAnnee.Text+" ] " + " : Opération réussie");
                        txtCode.Text = "";
                        txtNom.Text = "";
                        txtName.Text = "";
                        //txtAnneeScolaire.Text = "";
                        //txtAnnee.Text = "";

                        param = creerModifierMatiereBL.getParametres();
                        if (param != null)
                        {
                            txtAnnee.Text = Convert.ToString(param.annee);
                            txtAnneeScolaire.Text = (param.annee - 1).ToString();
                        }
                        else
                        {
                            txtAnnee.Text = "";
                            txtAnneeScolaire.Text = "";

                        }

                        // Initialisation de la collection, qui va s'afficher dans la DataGrid :
                        List<MatiereBE> LMatiereBE = creerModifierMatiereBL.listerToutesLesMatieres();
                        // on met la liste "LMatiereBE" dans le DataGrid
                        RemplirDataGrid(LMatiereBE);

                    }
                    else MessageBox.Show("Echec enregistrement : une erreure est survenue !");
                }
                else MessageBox.Show("Une Matière ayant le code \"" + txtCode.Text + "\" existe deja dans le système \n \n Veuillez changer de code SVP !");
            }
            else MessageBox.Show("Tous les champs marqués par Astérix '(*)' doivent êtres renseignés !");
        }

        private void cmdAnnuler_Click(object sender, RoutedEventArgs e)
        {
            txtCode.Text = "";
            txtNom.Text = "";
            txtName.Text = "";
            //txtAnneeScolaire.Text = "";
            //txtAnnee.Text = "";

            param = creerModifierMatiereBL.getParametres();
            if (param != null)
            {
                txtAnnee.Text = Convert.ToString(param.annee);
                txtAnneeScolaire.Text = (param.annee - 1).ToString();
            }
            else
            {
                txtAnnee.Text = "";
                txtAnneeScolaire.Text = "";

            }

            etat = 0;

        }

        private void grdListeMatiere_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                if (grdListeMatiere.SelectedIndex != -1)
                {
                    if (MessageBox.Show("Voulez-vous vraiment le supprimer ? ", "School : Confimation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        if (creerModifierMatiereBL.supprinerMatiere(ListeMatieres.ElementAt(grdListeMatiere.SelectedIndex)))
                            ListeMatieres.RemoveAt(grdListeMatiere.SelectedIndex);
                        grdListeMatiere.ItemsSource = ListeMatieres;

                    }

                    grdListeMatiere.UnselectAll();
                }

            }
        }

        private void grdListeMatiere_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (grdListeMatiere.SelectedIndex != -1)
            {
                etat = 1;
                MatiereBE matiere = new MatiereBE();

                matiere = creerModifierMatiereBL.rechercherMatiere(ListeMatieres.ElementAt(grdListeMatiere.SelectedIndex));
                // on charge les informations dans le formulaire
                txtCode.Text = matiere.codeMat;
                txtNom.Text = matiere.nomMat;
                txtName.Text = matiere.nameMat;

                txtAnnee.Text = Convert.ToString(matiere.annee);
                txtAnneeScolaire.Text = Convert.ToString(matiere.annee - 1);
                annee = Convert.ToInt32(txtAnneeScolaire.Text) + 1;

                ancienObjet = matiere;

                grdListeMatiere.UnselectAll();
            }
        }

        private void txtAnnee_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Utilitaires.IsTextAllowed(e.Text);
        }

        private void cmdFermer_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cmdImprimer_Click(object sender, RoutedEventArgs e)
        {
            CreerEtat etat = new CreerEtat("Liste Matières -" + DateTime.Today.ToShortDateString(), "Liste des Matières");
            etat.obtenirEtat(grdListeMatiere);
        }

        private void txtAnnee_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) {
                List<MatiereBE> LMatiereBE = new List<MatiereBE>();
               
                LMatiereBE = creerModifierMatiereBL.listerMatieresSuivantCritere("annee = '" + txtAnnee.Text + "'");

                // on met la liste "LMatiereBE" dans le DataGrid
                RemplirDataGrid(LMatiereBE);
            }
        }

        private void txtAnneeScolaire_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                annee = Convert.ToInt32(txtAnneeScolaire.Text) + 1;
                txtAnnee.Text = annee.ToString();

            }
            catch (Exception)
            {
                MessageBox.Show("L'annee doit etre un nombre positif", "School brain:Alerte");
            }
        }

    }
}
