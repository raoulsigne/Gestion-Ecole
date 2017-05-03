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
using Ecole.BusinessEntity;
using Ecole.BusinessLogic;
using Ecole.ClasseConception;
using System.Windows.Controls.Primitives;
using Ecole.Utilitaire;
using System.Globalization;
using System.Threading;

namespace Ecole.UI
{
    /// <summary>
    /// Interaction logic for SaisieAnonymatUI.xaml
    /// </summary>
    public partial class SaisieAnonymatUI : Window
    {
        List<string> classes;
        List<string> matieres;
        List<string> evaluations;
        List<string> sequences;
        List<NotesBE> notes;
        List<InscrireBE> inscrires;
        List<LigneSaisieAnonymat> lignes;
        SaisieAnonymatBL anonymatBL;
        List<string> anonymatsSaisies;
        List<string> matriculeSaisies;
        bool deplacement;
        int annee;

        public SaisieAnonymatUI()
        {
            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";
            Thread.CurrentThread.CurrentCulture = ci;

            InitializeComponent();

            anonymatBL = new SaisieAnonymatBL();
            classes = new List<string>();
            matieres = new List<string>();
            evaluations = new List<string>();
            sequences = new List<string>();
            lignes = new List<LigneSaisieAnonymat>();
            notes = new List<NotesBE>();
            inscrires = new List<InscrireBE>();
            anonymatsSaisies = new List<string>();
            matriculeSaisies = new List<string>();
            deplacement = true;

            annee = anonymatBL.anneeEnCours();
            txtAnnee.Text = " / " + annee;
            txtAnneeScolaire.Text = (annee - 1).ToString(); 
            classes = anonymatBL.listerValeurColonneClasse("codeclasse");
            cmbClasse.ItemsSource = classes;
            sequences = anonymatBL.listerValeurColonneSequence("codeseq");
            cmbSequence.ItemsSource = sequences;
            grdListe.DataContext = this;
            grdListe.ItemsSource = lignes;
        }

        private void cmbClasse_DropDownClosed(object sender, EventArgs e)
        {
            if (cmbClasse.SelectedValue != null)
            {
                List<ProgrammerBE> listes = new List<ProgrammerBE>();
                matieres = new List<string>();
                string codeclasse = cmbClasse.SelectedValue.ToString();
                listes = anonymatBL.listerSuivantCritereProgrammer("codeclasse = " + "'" + codeclasse + "' and annee = " + "'" + annee + "'");
                foreach (ProgrammerBE p in listes)
                    matieres.Add(p.codematiere);
                cmbMatiere.ItemsSource = matieres;
                cmbMatiere.Items.Refresh();
            }
        }


        private void txtAnnee_KeyUp(object sender, KeyEventArgs e)
        {
            if (cmbClasse.SelectedValue != null)
            {
                List<ProgrammerBE> listes = new List<ProgrammerBE>();
                matieres = new List<string>();
                string codeclasse = cmbClasse.SelectedValue.ToString();
                listes = anonymatBL.listerSuivantCritereProgrammer("codeclasse = " + "'" + codeclasse + "' and annee = " + "'" + annee + "'");
                foreach (ProgrammerBE p in listes)
                    matieres.Add(p.codematiere);
                cmbMatiere.ItemsSource = matieres;
                cmbMatiere.Items.Refresh();
            }
        }

        private void cmdOK_Click(object sender, RoutedEventArgs e)
        {
            cmdOK.IsEnabled = false;

            string codeclasse = cmbClasse.SelectedValue.ToString();
            string codematiere = cmbMatiere.SelectedValue.ToString();
            string codeeval = cmbEvaluation.SelectedValue.ToString();
            string codeseq = cmbSequence.SelectedValue.ToString();
            int i = 1;
            EleveBE eleve = new EleveBE();
            notes = new List<NotesBE>();
            lignes = new List<LigneSaisieAnonymat>();

            notes = anonymatBL.listerSuivantCritereNotes("codemat = " + "'" + codematiere + "' and codeseq = " + "'" + codeseq + "' and codeevaluation = " + "'" + codeeval
                + "' and annee = " + "'" + annee + "' and matricule in (SELECT matricule from inscrire where codeclasse = " + "'" + codeclasse + "' and annee = " + "'" + annee + "' )");
            if (notes != null)
            {
                foreach (NotesBE n in notes)
                {
                    eleve.matricule = n.matricule;
                    eleve = anonymatBL.rechercherEleve(eleve);
                    lignes.Add(new LigneSaisieAnonymat(i++, eleve.nom, n.matricule, n.anonymat));
                    anonymatsSaisies.Add(n.anonymat);
                    matriculeSaisies.Add(n.matricule.ToUpper());
                }
            }

            inscrires = anonymatBL.listerSuivantCritereInscrire("codeclasse = " + "'" + codeclasse + "' and annee = " + "'" + annee + "'");
            if (inscrires != null)
                foreach (InscrireBE inscrire in inscrires)
                {
                    if (!matriculeSaisies.Contains(inscrire.matricule.ToUpper()))
                        lignes.Add(new LigneSaisieAnonymat(i++, "", "", ""));
                }

            List<LigneSaisieAnonymat> liste = lignes.OrderBy(o => o.nom).ToList();
            lignes.Clear();
            i = 1;
            foreach (LigneSaisieAnonymat l in liste)
            {
                l.numero = i++;
                lignes.Add(l);
            }
            grdListe.ItemsSource = lignes;
            grdListe.Items.Refresh();
        }

        private void cmdAnnuler_Click(object sender, RoutedEventArgs e)
        {
            //cmbClasse.Text = "";
            //matieres = new List<string>();
            //cmbMatiere.ItemsSource = matieres;
            //cmbEvaluation.Text = "";
            //cmbSequence.Text = "";
            lignes = new List<LigneSaisieAnonymat>();
            anonymatsSaisies = new List<string>();
            matriculeSaisies = new List<string>();
            grdListe.ItemsSource = lignes;
            cmdOK.IsEnabled = true;
        }

        private void cmdImprimer_Click(object sender, RoutedEventArgs e)
        {
            if (validerFormulaire())
            {
                string classe = cmbClasse.SelectedValue.ToString();
                string matiere = cmbMatiere.SelectedValue.ToString();
                string sequence = cmbSequence.SelectedValue.ToString();
                string evaluation = cmbEvaluation.SelectedValue.ToString();
                grdListe.ItemsSource = lignes;
                grdListe.Items.Refresh();
                List<string> headers = new List<string>();
                for (int j = 0; j < grdListe.Columns.Count; j++)
                    headers.Add(grdListe.Columns[j].Header.ToString().ToUpper());

                CreerEtat creerEtat = new CreerEtat("anonymat" + DateTime.Today.ToShortDateString(), "Liste des anonymats");
                anonymatBL.journaliser("Impression de la liste des anonymats de la " + classe + " pour la " + sequence + " de " + evaluation);
                creerEtat.etatAnonymat(lignes, headers, classe, matiere, sequence, evaluation, annee);
            }
        }

        private void cmdFermer_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private bool validerFormulaire()
        {
            bool b = true;

            if (cmbClasse.SelectedValue == null || cmbEvaluation.SelectedValue == null || cmbMatiere.SelectedValue == null || cmbSequence.SelectedValue == null || txtAnnee.Text == null)
                b = false;
            else if (cmbClasse.SelectedValue.ToString() == "" || cmbEvaluation.SelectedValue.ToString() == "" || cmbMatiere.SelectedValue.ToString() == "" ||
                cmbSequence.SelectedValue.ToString() == "" || txtAnnee.Text.ToString() == "")
                b = false;

            return b;
        }

        public static void SelectCellByIndex(DataGrid dataGrid, int rowIndex, int columnIndex)
        {
            if (!dataGrid.SelectionUnit.Equals(DataGridSelectionUnit.Cell))
                MessageBox.Show("The SelectionUnit of the DataGrid must be set to Cell.");
            else
            {
                if (rowIndex < 0 || rowIndex > (dataGrid.Items.Count - 1))
                    MessageBox.Show(rowIndex + " is an invalid row index.");
                else
                {
                    if (columnIndex < 0 || columnIndex > (dataGrid.Columns.Count - 1))
                        MessageBox.Show(columnIndex + " is an invalid row index.");
                    else
                    {
                        dataGrid.SelectedCells.Clear();

                        object item = dataGrid.Items[rowIndex]; //=ligne X
                        DataGridRow row = dataGrid.ItemContainerGenerator.ContainerFromIndex(rowIndex) as DataGridRow;
                        if (row == null)
                        {
                            dataGrid.ScrollIntoView(item);
                            row = dataGrid.ItemContainerGenerator.ContainerFromIndex(rowIndex) as DataGridRow;
                        }
                        if (row != null)
                        {
                            DataGridCell cell = GetCell(dataGrid, row, columnIndex);
                            if (cell != null)
                            {
                                DataGridCellInfo dataGridCellInfo = new DataGridCellInfo(cell);
                                dataGrid.SelectedCells.Add(dataGridCellInfo);
                                cell.Focus();
                            }
                        }
                    }
                }
            }
        }

        public static T FindVisualChild<T>(DependencyObject obj) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is T)
                    return (T)child;
                else
                {
                    T childOfChild = FindVisualChild<T>(child);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }
            return null;
        }

        public static DataGridCell GetCell(DataGrid dataGrid, DataGridRow rowContainer, int column)
        {
            if (rowContainer != null)
            {
                DataGridCellsPresenter presenter = FindVisualChild<DataGridCellsPresenter>(rowContainer);
                if (presenter == null)
                {
                    /* if the row has been virtualized away, call its ApplyTemplate() method
                     * to build its visual tree in order for the DataGridCellsPresenter
                     * and the DataGridCells to be created */
                    rowContainer.ApplyTemplate();
                    presenter = FindVisualChild<DataGridCellsPresenter>(rowContainer);
                }
                if (presenter != null)
                {
                    DataGridCell cell = presenter.ItemContainerGenerator.ContainerFromIndex(column) as DataGridCell;
                    if (cell == null)
                    {
                        /* bring the column into view
                         * in case it has been virtualized away */
                        dataGrid.ScrollIntoView(rowContainer, dataGrid.Columns[column]);
                        cell = presenter.ItemContainerGenerator.ContainerFromIndex(column) as DataGridCell;
                    }
                    return cell;
                }
            }
            return null;
        }

        private void grdListe_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            int COLUMN_NOM = 1;
            int COLUMN_MATRICULE = 2;
            int COLUMN_ANONYMAT = 3;
            int MAX_ROW = lignes.Count;
            string codeclasse = cmbClasse.SelectedValue.ToString();
            string codematiere = cmbMatiere.SelectedValue.ToString();
            string codeeval = cmbEvaluation.SelectedValue.ToString();
            string codeseq = cmbSequence.SelectedValue.ToString();

            var grid = sender as DataGrid;
            DataGridRow row = new DataGridRow();
            bool deplacement = false;

            if (grid != null)
            {
                //row = grid.SelectedItem as DataGridRow;

                if (e.Key == Key.Return)
                {
                    e.Handled = true;
                    EleveBE eleve = new EleveBE();
                    InscrireBE inscrire = new InscrireBE();
                    int rowIndex = grdListe.Items.IndexOf(grdListe.CurrentItem);
                    int columnIndex = grdListe.CurrentColumn.DisplayIndex;
                    row = (DataGridRow)grid.ItemContainerGenerator.ContainerFromIndex(rowIndex);
                    LigneSaisieAnonymat ligne = new LigneSaisieAnonymat();

                    NotesBE note_old = new NotesBE();
                    NotesBE note = new NotesBE("", codematiere, codeseq, codeeval, 0, annee, "");
                    note.annee = annee;
                    inscrire.annee = annee;
                    inscrire.codeClasse = codeclasse;
                    ligne = lignes.ElementAt(rowIndex);
                    if (columnIndex == COLUMN_MATRICULE)
                    {
                        TextBox textBox;
                        DataGridCellInfo cell = grid.SelectedCells[0];
                        try
                        {
                            textBox = (TextBox)cell.Column.GetCellContent(cell.Item);
                            string matriculeSaisie = textBox.Text;
                            inscrire.matricule = matriculeSaisie;
                            inscrire = anonymatBL.rechercherInscrire(inscrire);
                            eleve.matricule = matriculeSaisie;
                            eleve = anonymatBL.rechercherEleve(eleve);
                            if (inscrire != null)
                            {
                                if (!matriculeSaisies.Contains(inscrire.matricule.ToUpper()))
                                {
                                    if (inscrire != null)
                                    {
                                        deplacement = true;
                                        GetCell(grdListe, row, COLUMN_NOM).Content = eleve.nom;
                                        lignes.ElementAt(rowIndex).nom = eleve.nom;
                                        matriculeSaisies.Add(inscrire.matricule.ToUpper());
                                    }
                                    else
                                    {
                                        deplacement = false;
                                        ((TextBox)cell.Column.GetCellContent(cell.Item)).Text = "";
                                        MessageBox.Show("Cet élève n'est pas inscrit en " + codeclasse + " en " + annee);
                                    }
                                }
                                else
                                {
                                    deplacement = false;
                                    ((TextBox)cell.Column.GetCellContent(cell.Item)).Text = "";
                                    MessageBox.Show("Ce matricule est déjà saisi " + inscrire.matricule);
                                }
                            }
                            else
                            {
                                deplacement = false;
                                ((TextBox)cell.Column.GetCellContent(cell.Item)).Text = "";
                                MessageBox.Show("L'élève de matricule -" + matriculeSaisie + "- n'est pas inscrit en -" + codeclasse);
                            }
                        }
                        catch (Exception)
                        {
                            deplacement = true;
                        }
                    }
                    else if (columnIndex == COLUMN_ANONYMAT)
                    {
                        string matricule = lignes.ElementAt(rowIndex).matricule;

                        inscrire.matricule = matricule;
                        inscrire = anonymatBL.rechercherInscrire(inscrire);
                        if (inscrire == null)
                        {
                            deplacement = false;
                            MessageBox.Show("Cet élève n'est pas inscrit en " + codeclasse + " en " + annee);
                            DataGridCellInfo cell = grid.SelectedCells[0];
                            ((TextBox)cell.Column.GetCellContent(cell.Item)).Text = "";
                        }
                        else
                        {
                            TextBox textBox;
                            DataGridCellInfo cell = grid.SelectedCells[0];
                            try
                            {
                                textBox = (TextBox)cell.Column.GetCellContent(cell.Item);
                                note.matricule = matricule;
                                note.anonymat = textBox.Text;
                                try
                                {
                                    anonymatsSaisies[rowIndex] = "";
                                }
                                catch (Exception) { }

                                if (anonymatsSaisies.Contains(note.anonymat))
                                {
                                    MessageBox.Show("L'anonymat " + note.anonymat + " a déjà été saisie");
                                    deplacement = false;
                                    ((TextBox)cell.Column.GetCellContent(cell.Item)).Text = "";
                                }
                                else
                                {
                                    deplacement = true;
                                    note_old = anonymatBL.rechercherNote(note);
                                    if (note_old != null && note_old.note != 0)
                                        note.note = note_old.note;
                                    anonymatBL.enregistrerAnonymat(note);
                                    try
                                    {
                                        anonymatsSaisies[rowIndex] = note.anonymat;
                                    }
                                    catch (Exception)
                                    {
                                        anonymatsSaisies.Add(note.anonymat);
                                    }

                                }
                            }
                            catch (Exception)
                            {
                                deplacement = true;
                            }
                        }
                    }


                    if (deplacement)
                    {
                        if (columnIndex == COLUMN_MATRICULE)
                        {
                            SelectCellByIndex(grdListe, rowIndex, COLUMN_ANONYMAT);
                        }
                        else
                            if (columnIndex == COLUMN_ANONYMAT)
                            {
                                rowIndex = (rowIndex + 1) % MAX_ROW;
                                SelectCellByIndex(grdListe, rowIndex, COLUMN_MATRICULE);
                            }
                            else
                            {
                                MessageBox.Show("cellule non modifiable");
                            }
                    }
                }
            }
        }

        private void cmbMatiere_DropDownClosed(object sender, EventArgs e)
        {
            chargerEvaluation();
        }

        private void cmbSequence_DropDownClosed(object sender, EventArgs e)
        {
            chargerEvaluation();
        }

        private void chargerEvaluation()
        {
            evaluations.Clear();
            if (cmbClasse.SelectedValue != null && cmbMatiere.SelectedValue != null && cmbSequence.SelectedValue != null && txtAnnee.Text != "")
            {
                evaluations = anonymatBL.listerEvaluation(cmbClasse.SelectedValue.ToString(), cmbMatiere.SelectedValue.ToString(), cmbSequence.SelectedValue.ToString(), annee);
                cmbEvaluation.ItemsSource = evaluations;
                cmbEvaluation.Items.Refresh();
            }
        }

        private void txtAnneeScolaire_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                annee = Convert.ToInt32(txtAnneeScolaire.Text) + 1;
                txtAnnee.Text = " / " + annee.ToString();

            }
            catch (Exception)
            {
                MessageBox.Show("L'annee doit etre un nombre positif", "School brain:Alerte");
            }
        }

        private void txtAnneeScolaire_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Utilitaires.IsTextAllowed(e.Text);
        }
    }
}
