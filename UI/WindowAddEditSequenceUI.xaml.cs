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
    /// Interaction logic for WindowAddEditSequenceUI.xaml
    /// </summary>
    public partial class WindowAddEditSequenceUI : Window
    {
        CreerModifierSequenceBL creerModifierSequenceBL;
        private int etat; // idique si nous sommes en création (0) ou en modification (1)

        private SequenceBE ancienObjet; //garde l'ancien état de l'objet, qui sera utilisé pour la modification


        // Définition d'une liste 'ListeSequences' observable de 'Sequence'
        public ObservableCollection<SequenceBE> ListeSequences { get; set; }

        // Fonction permettant de remplir le DataGrid avec les informations de la base de données
        // @param : - listObjet : la liste des objets à afficher dans le DataGrid
        public void RemplirDataGrid(List<SequenceBE> listObjet)
        {
            // Ajout de données dans la DataTable :
            var table = new DataTable();

            table.Columns.Add(new DataColumn("codeseq", typeof(string)));
            table.Columns.Add(new DataColumn("nomseq", typeof(string)));
            table.Columns.Add(new DataColumn("codetrimestre", typeof(string)));

            if (listObjet != null)
            {

                for (int i = 0; i < listObjet.Count; i++)
                {
                    DataRow dr = table.NewRow();
                    dr["codeseq"] = listObjet.ElementAt(i).codeseq;
                    dr["nomseq"] = listObjet.ElementAt(i).nomseq;
                    dr["codetrimestre"] = listObjet.ElementAt(i).codetrimestre;
                    table.Rows.Add(dr);
                }
            }

            string vCode = "";
            string vNom = "";
            string vCodeTrimestre = "";

            ListeSequences.Clear();

            //Personnes_Table = LoadDataTable();

            foreach (DataRow row in table.Rows)
            {
                vCode = Convert.ToString(row["codeseq"]);
                vNom = Convert.ToString(row["nomseq"]);
                vCodeTrimestre = Convert.ToString(row["codetrimestre"]);

                ListeSequences.Add(new SequenceBE(vCode, vNom, vCodeTrimestre));

            }
        }

        public WindowAddEditSequenceUI()
        {
            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";
            Thread.CurrentThread.CurrentCulture = ci;

            InitializeComponent();

            creerModifierSequenceBL = new CreerModifierSequenceBL();

            etat = 0;

            ancienObjet = new SequenceBE();

            // A mettre pour que le binding avec le DataGrid fonctionne !
            grdListeSequence.DataContext = this;

            // Initialisation de la collection, qui va s'afficher dans la DataGrid :
            ListeSequences = new ObservableCollection<SequenceBE>();

            //on charge la liste des codes des trimestres
            List<TrimestreBE> LTrimestreBE = creerModifierSequenceBL.listerTousLesTrimestres();
            // on met la liste "LTrimestreBE" dans le DataGrid
            cmbTrimestre.ItemsSource = creerModifierSequenceBL.getListCodeTrimestre2(LTrimestreBE);
            cmbFilterTrimestre.ItemsSource = creerModifierSequenceBL.getListCodeTrimestre(LTrimestreBE);

            List<SequenceBE> LSequenceBE = creerModifierSequenceBL.listerToutesLesSequences();
            // on met la liste "LSequenceBE" dans le DataGrid
            RemplirDataGrid(LSequenceBE);

            // ------------------- Chargement de la liste des codes de Sequence dans le comboBox de la fenêtre 
            //(utile pour le filtre)
            cmbFilterCode.ItemsSource = creerModifierSequenceBL.getListCodeSequence(LSequenceBE);

            // ------------------- Chargement de la liste des noms de Sequence dans le comboBox de la fenêtre 
            //(utile pour le filtre)
            cmbFilterNom.ItemsSource = creerModifierSequenceBL.getListNomSequence(LSequenceBE);
        }

        private void cmdFermer_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cmdValider_Click(object sender, RoutedEventArgs e)
        {
            if ((txtCode.Text != null && txtNom.Text != null && cmbTrimestre.Text != null) && (txtCode.Text != "" && txtNom.Text != "" && cmbTrimestre.Text != ""))
            { // si tous les champs sont non vides

                SequenceBE sequence = new SequenceBE();
                sequence.codeseq = txtCode.Text;
                sequence.nomseq = txtNom.Text;
                sequence.codetrimestre = cmbTrimestre.Text;

                if (etat == 1)
                {
                    creerModifierSequenceBL.modifierSequence(ancienObjet, sequence);
                    List<SequenceBE> LSequenceBE = creerModifierSequenceBL.listerToutesLesSequences();
                    // on met la liste "LSequenceBE" dans le DataGrid
                    RemplirDataGrid(LSequenceBE);

                    // ------------------- Chargement de la liste des codes de Cycle dans le comboBox de la fenêtre 
                    //(utile pour le filtre)
                    cmbFilterCode.ItemsSource = creerModifierSequenceBL.getListCodeSequence(LSequenceBE);

                    // ------------------- Chargement de la liste des noms de Niveau dans le comboBox de la fenêtre 
                    //(utile pour le filtre)
                    cmbFilterNom.ItemsSource = creerModifierSequenceBL.getListNomSequence(LSequenceBE);

                    txtCode.Text = "";
                    txtNom.Text = "";
                    cmbTrimestre.Text = null;
                    etat = 0;
                }
                else if (creerModifierSequenceBL.rechercherSequence(new SequenceBE(txtCode.Text, txtNom.Text, cmbTrimestre.Text)) == null)
                { // si une Sequence possédant le même code n'existe pas deja dans la BD

                    if (creerModifierSequenceBL.creerSequence(txtCode.Text, txtNom.Text, cmbTrimestre.Text))
                    { // si l'nregistrement a réussi

                        MessageBox.Show("Enregistrement Séquence [" + txtCode.Text + ", " + txtNom.Text + "] " + " : Opération réussie");
                        txtCode.Text = "";
                        txtNom.Text = "";
                        cmbTrimestre.Text = null;

                        List<SequenceBE> LSequenceBE = creerModifierSequenceBL.listerToutesLesSequences();
                        //on rafraichir le DataGrid
                        RemplirDataGrid(LSequenceBE);
                        // ------------------- Chargement de la liste des codes de Séquence dans le comboBox de la fenêtre 
                        //(utile pour le filtre)
                        cmbFilterCode.ItemsSource = creerModifierSequenceBL.getListCodeSequence(LSequenceBE);

                        // ------------------- Chargement de la liste des noms de Séquence dans le comboBox de la fenêtre 
                        //(utile pour le filtre)
                        cmbFilterNom.ItemsSource = creerModifierSequenceBL.getListNomSequence(LSequenceBE);
                    }
                    else MessageBox.Show("Echec enregistrement : une erreure est survenue !");
                }
                else MessageBox.Show("Une Séquence ayant le code \"" + txtCode.Text + "\" existe deja dans le système \n \n Veuillez changer de code SVP !");
            }
            else MessageBox.Show("Tous les champs doivent pas être remplis !");
        }

        private void cmbFilterCode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // on filtre la liste affiché dans le DataGrid en fonction du choix de l'utilisateur
            List<SequenceBE> LSequenceBE;
            if (cmbFilterNom.Text != null && cmbFilterNom.Text != "")
            {
                if (cmbFilterCode.SelectedItem.Equals("<Tous les Codes>"))
                {
                    if (cmbFilterNom.SelectedItem.Equals("<Tous les Noms>"))
                        LSequenceBE = creerModifierSequenceBL.listerToutesLesSequences();
                    else
                        LSequenceBE = creerModifierSequenceBL.listerSequenceSuivantCritere("nomseq = '" + cmbFilterNom.SelectedItem + "'");
                }
                else
                    if (cmbFilterNom.SelectedItem.Equals("<Tous les Noms>"))
                        LSequenceBE = creerModifierSequenceBL.listerSequenceSuivantCritere("codeseq = '" + cmbFilterCode.SelectedItem + "'");
                    else
                        LSequenceBE = creerModifierSequenceBL.listerSequenceSuivantCritere("codeseq = '" + cmbFilterCode.SelectedItem + "' AND nomseq = '" + cmbFilterNom.SelectedItem + "'");
            }
            else
                if (cmbFilterCode.SelectedItem.Equals("<Tous les Codes>"))
                    LSequenceBE = creerModifierSequenceBL.listerToutesLesSequences();
                else
                    LSequenceBE = creerModifierSequenceBL.listerSequenceSuivantCritere("codeseq = '" + cmbFilterCode.SelectedItem + "'");

            //on rafraichir le DataGrid
            RemplirDataGrid(LSequenceBE);
        }

        private void cmbFilterNom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // on filtre la liste affiché dans le DataGrid en fonction du choix de l'utilisateur
            List<SequenceBE> LSequenceBE;
            if (cmbFilterCode.Text != null && cmbFilterCode.Text != "")
            {
                if (cmbFilterNom.SelectedItem.Equals("<Tous les Noms>"))
                {
                    if (cmbFilterCode.SelectedItem.Equals("<Tous les Codes>"))
                        LSequenceBE = creerModifierSequenceBL.listerToutesLesSequences();
                    else
                        LSequenceBE = creerModifierSequenceBL.listerSequenceSuivantCritere("codeseq = '" + cmbFilterCode.SelectedItem + "'");
                }
                else
                    if (cmbFilterCode.SelectedItem.Equals("<Tous les Codes>"))
                        LSequenceBE = creerModifierSequenceBL.listerSequenceSuivantCritere("nomseq = '" + cmbFilterNom.SelectedItem + "'");
                    else
                        LSequenceBE = creerModifierSequenceBL.listerSequenceSuivantCritere("codeseq = '" + cmbFilterCode.SelectedItem + "' AND nomseq = '" + cmbFilterNom.SelectedItem + "'");
            }
            else
                if (cmbFilterNom.SelectedItem.Equals("<Tous les Noms>"))
                    LSequenceBE = creerModifierSequenceBL.listerToutesLesSequences();
                else
                    LSequenceBE = creerModifierSequenceBL.listerSequenceSuivantCritere("nomseq = '" + cmbFilterNom.SelectedItem + "'");

            //on rafraichir le DataGrid
            RemplirDataGrid(LSequenceBE);
        }

        private void cmbFilterTrimestre_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // on filtre la liste affiché dans le DataGrid en fonction du choix de l'utilisateur
            List<SequenceBE> LSequenceBE;
            if (cmbFilterTrimestre.Text != null && cmbFilterTrimestre.Text != "")
            {
                if (cmbFilterTrimestre.SelectedItem.Equals("<Tous les Codes>"))
                        LSequenceBE = creerModifierSequenceBL.listerToutesLesSequences();
                 else
                        LSequenceBE = creerModifierSequenceBL.listerSequenceSuivantCritere("codetrimestre = '" + cmbFilterTrimestre.SelectedItem + "'");
            }
            else if (cmbFilterTrimestre.Text != null) {
                if (cmbFilterTrimestre.SelectedItem.Equals("<Tous les Codes>"))
                    LSequenceBE = creerModifierSequenceBL.listerToutesLesSequences();
                else
                    LSequenceBE = creerModifierSequenceBL.listerSequenceSuivantCritere("codetrimestre = '" + cmbFilterTrimestre.SelectedItem + "'");
            }
            else
                LSequenceBE = creerModifierSequenceBL.listerToutesLesSequences();

            //on rafraichir le DataGrid
            RemplirDataGrid(LSequenceBE);
        }


        private void cmdAnnuler_Click(object sender, RoutedEventArgs e)
        {
            txtCode.Text = "";
            txtNom.Text = "";
            cmbTrimestre.Text = null;
            etat = 0;
        }

        private void grdListeSequence_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                if (grdListeSequence.SelectedIndex != -1)
                {
                    if (MessageBox.Show("Voulez-vous vraiment le supprimer ? ", "School : Confimation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        if (creerModifierSequenceBL.supprinerSequence(ListeSequences.ElementAt(grdListeSequence.SelectedIndex)))
                            ListeSequences.RemoveAt(grdListeSequence.SelectedIndex);

                        grdListeSequence.ItemsSource = ListeSequences;

                        // ------------------- Chargement de la liste des codes de Sequence dans le comboBox de la fenêtre 
                        //(utile pour le filtre)
                        List<SequenceBE> LSequenceBE = creerModifierSequenceBL.listerToutesLesSequences();
                        cmbFilterCode.ItemsSource = creerModifierSequenceBL.getListCodeSequence(LSequenceBE);

                        // ------------------- Chargement de la liste des noms de Cycle dans le comboBox de la fenêtre 
                        //(utile pour le filtre)
                        cmbFilterNom.ItemsSource = creerModifierSequenceBL.getListNomSequence(LSequenceBE);
                    }

                    grdListeSequence.UnselectAll();
                }
            }
        }

        private void grdListeSequence_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (grdListeSequence.SelectedIndex != -1)
            {
                etat = 1;
                SequenceBE sequence = new SequenceBE();

                sequence = creerModifierSequenceBL.rechercherSequence(ListeSequences.ElementAt(grdListeSequence.SelectedIndex));
                if (sequence != null)
                {
                    // on charge les informations dans le formulaire
                    txtCode.Text = sequence.codeseq;
                    txtNom.Text = sequence.nomseq;
                    cmbTrimestre.Text = sequence.codetrimestre;

                    ancienObjet = sequence;
                }

                grdListeSequence.UnselectAll();
            }
        }

        private void cmdImprimer_Click(object sender, RoutedEventArgs e)
        {
            CreerEtat etat = new CreerEtat("Séquences -" + DateTime.Today.ToShortDateString(), "Liste des Séquences ");

            etat.obtenirEtat(grdListeSequence);
        }

    }
}
