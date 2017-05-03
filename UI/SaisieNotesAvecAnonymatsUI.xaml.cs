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
using Ecole.ClasseConception;
using Ecole.BusinessEntity;
using Ecole.BusinessLogic;
using System.Windows.Controls.Primitives;
using Ecole.Utilitaire;
using System.Globalization;
using System.Threading;

namespace Ecole.UI
{
    /// <summary>
    /// Interaction logic for SaisieNotesAvecAnonymatsUI.xaml
    /// </summary>
    public partial class SaisieNotesAvecAnonymatsUI : Window
    {
        List<string> classes;
        List<string> matieres;
        List<string> evaluations;
        List<string> baremes;
        List<string> sequences;
        List<NotesBE> notes;
        List<LigneNote> lignes;
        List<InscrireBE> inscrires;
        SaisieNotesAvecAnonymatBL noteBL;
        ClasseBE classe;
        MatiereBE matiere;
        int annee;

        public SaisieNotesAvecAnonymatsUI()
        {
            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";
            Thread.CurrentThread.CurrentCulture = ci;
            
            InitializeComponent();

            classe = new ClasseBE();
            matiere = new MatiereBE();
            noteBL = new SaisieNotesAvecAnonymatBL();
            classes = new List<string>();
            matieres = new List<string>();
            evaluations = new List<string>();
            sequences = new List<string>();
            lignes = new List<LigneNote>();
            baremes = new List<string> { "10", "20", "30", "40", "50", "60", "70", "80", "90", "100"};
            notes = new List<NotesBE>();
            inscrires = new List<InscrireBE>();

            annee = noteBL.anneeEnCours();
            txtAnnee.Text = " / " + annee;
            txtAnneeScolaire.Text = (annee - 1).ToString(); 
            classes = noteBL.listerValeurColonneClasse("codeclasse");
            cmbClasse.ItemsSource = classes;
            sequences = noteBL.listerValeurColonneSequence("codeseq");
            cmbSequence.ItemsSource = sequences;
            evaluations = noteBL.listerValeurColonneTypeEvaluation("codeevaluation");
            cmbEvaluation.ItemsSource = evaluations;
            cmbBareme.ItemsSource = baremes;
            cmbBareme.SelectedIndex = 1;
        }

        private void cmbClasse_DropDownClosed(object sender, EventArgs e)
        {
            matieres.Clear();
            cmbMatiere.Items.Refresh();
            if (cmbClasse.SelectedValue != null && cmbClasse.SelectedValue.ToString() != "")
            {
                List<ProgrammerBE> listes = new List<ProgrammerBE>();
                string codeclasse = cmbClasse.SelectedValue.ToString();
                if (txtAnnee.Text != null && txtAnnee.Text != "")
                {
                    listes = noteBL.listerSuivantCritereProgramer("codeclasse = " + "'" + codeclasse + "' and annee = " + "'" + annee + "'");
                    foreach (ProgrammerBE prog in listes)
                        matieres.Add(prog.codematiere);
                    cmbMatiere.ItemsSource = matieres;
                    cmbMatiere.Items.Refresh();
                }
                classe.codeClasse = codeclasse;
                classe = noteBL.rechercherClasse(classe);
            }
        }

        private void cmbSequence_DropDownClosed(object sender, EventArgs e)
        {
            chargerEvaluer();
        }

        private void cmdOK_Click(object sender, RoutedEventArgs e)
        {
            if (validerFormulaire())
            {
                cmdOK.IsEnabled = false;

                string codeclasse = cmbClasse.SelectedValue.ToString();
                string codematiere = cmbMatiere.SelectedValue.ToString();
                string codeeval = cmbEvaluation.SelectedValue.ToString();
                string codeseq = cmbSequence.SelectedValue.ToString();
                int bareme = Convert.ToInt32(cmbBareme.SelectedValue.ToString());
                int i = 0;
                EleveBE eleve = new EleveBE();
                List<string> matriculetampon = new List<string>();

                notes = noteBL.listerSuivantCritereNotes("codemat = " + "'" + codematiere + "' and codeseq = " + "'" + codeseq + "' and codeevaluation = " + "'" + codeeval
                    + "' and annee = " + "'" + annee + "' and matricule in (SELECT matricule from inscrire where codeclasse = " + "'" + codeclasse + "' and annee = " + "'" + annee + "' )");

                if (notes != null)
                {
                    for (i = 0; i < notes.Count; i++)
                    {
                        eleve.matricule = notes.ElementAt(i).matricule;
                        eleve = noteBL.rechercherEleve(eleve);
                        matriculetampon.Add(notes.ElementAt(i).matricule.ToUpper());
                        if (notes.ElementAt(i).note != -1)
                            lignes.Add(new LigneNote(i + 1, eleve.nom, eleve.matricule, notes.ElementAt(i).anonymat, (decimal)notes.ElementAt(i).note * bareme / 20));
                        else
                            lignes.Add(new LigneNote(i + 1, eleve.nom, eleve.matricule, notes.ElementAt(i).anonymat, -1));

                    }
                }

                if (notes != null)
                    i = notes.Count + 1;
                else
                    i = 1;

                inscrires = noteBL.listerSuivantCritereInscrire("codeclasse = " + "'" + codeclasse + "' and annee = " + "'" + annee + "'");
                if (inscrires != null)
                {
                    foreach (InscrireBE inscrire in inscrires)
                    {
                        eleve.matricule = inscrire.matricule;
                        eleve = noteBL.rechercherEleve(eleve);
                        if (!matriculetampon.Contains(inscrire.matricule.ToUpper()))
                            lignes.Add(new LigneNote(i++, eleve.nom, eleve.matricule, ""));
                    }
                }

                List<LigneNote> liste = lignes.OrderBy(o => o.matricule).ToList();
                lignes.Clear();
                i = 1;
                foreach (LigneNote l in liste)
                {
                    l.numero = i++;
                    lignes.Add(l);
                }
                grdListe.ItemsSource = lignes;
                grdListe.Items.Refresh();
            }
            else MessageBox.Show("Veuillez renseigner tous les champs");
        }

        private void cmdAnnuler_Click(object sender, RoutedEventArgs e)
        {
            cmdOK.IsEnabled = true;
            //cmbClasse.Text = "";
            //cmbMatiere.Text = "";
            //matieres.Clear();
            //cmbEvaluation.Text = "";
            //cmbSequence.Text = "";
            //cmbBareme.Text = "";
            lignes = new List<LigneNote>();
            grdListe.ItemsSource = lignes;
            grdListe.Items.Refresh();
        }

        private void cmdImprimer_Click(object sender, RoutedEventArgs e)
        {
            if (validerFormulaire())
            {
                string sequence = cmbSequence.SelectedValue.ToString();
                string evaluation = cmbEvaluation.SelectedValue.ToString();
                int bareme = Convert.ToInt32(cmbBareme.SelectedValue.ToString());

                CreerEtat creerEtat = new CreerEtat("Notes anonymees" + DateTime.Today.ToShortDateString(), "Liste des Notes");
                noteBL.journaliser("Impression des notes avec anonymats de la " + sequence + " de la " + classe.nomClasse + " de " + evaluation);
                creerEtat.etatNotes(grdListe, classe.codeClasse + " - " + classe.nomClasse, matiere.codeMat + " - " + matiere.nomMat, sequence, evaluation, annee, bareme, 2);
            }
        }

        private void cmdFermer_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private bool validerFormulaire()
        {
            bool b = true;

            if (cmbClasse.SelectedValue == null || cmbEvaluation.SelectedValue == null || cmbMatiere.SelectedValue == null || cmbSequence.SelectedValue == null || txtAnnee.Text == null 
                || cmbBareme.SelectedValue == null)
                b = false;
            else if (cmbClasse.SelectedValue.ToString() == "" || cmbEvaluation.SelectedValue.ToString() == "" || cmbMatiere.SelectedValue.ToString() == "" ||
                cmbSequence.SelectedValue.ToString() == "" || txtAnnee.Text.ToString() == "" || cmbBareme.SelectedValue.ToString() == "")
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

                        object item = dataGrid.Items[rowIndex]; //=Product X
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
            int COLUMN_NOTE = 2;
            int MAX_ROW = lignes.Count;
            var grid = sender as DataGrid;
            DataGridRow row = new DataGridRow();
            string codeclasse = cmbClasse.SelectedValue.ToString();
            string codematiere = cmbMatiere.SelectedValue.ToString();
            string codeeval = cmbEvaluation.SelectedValue.ToString();
            string codeseq = cmbSequence.SelectedValue.ToString();
            int bareme = Convert.ToInt32(cmbBareme.SelectedValue.ToString());
            double moyenne = 1;
            LigneNote ligne = new LigneNote();
            
            NotesBE note = new NotesBE("", codematiere, codeseq, codeeval, 0, annee, "");
            note.annee = annee;

            if (e.Key == Key.Return)
            {
                e.Handled = true;

                int row_index = grdListe.Items.IndexOf(grdListe.CurrentItem);
                int col_index = grdListe.CurrentColumn.DisplayIndex;
                row = (DataGridRow)grid.ItemContainerGenerator.ContainerFromIndex(row_index);
                if (col_index == COLUMN_NOTE)
                {
                    DataGridCellInfo cell = grid.SelectedCells[0];
                    TextBox textBox;
                    try
                    {
                        textBox = (TextBox)cell.Column.GetCellContent(cell.Item);
                        ligne = lignes.ElementAt(row_index);
                        note.anonymat = ligne.anonymat;
                        note.matricule = ligne.matricule;
                        try
                        {
                            moyenne = Convert.ToDouble(textBox.Text);
                            if (moyenne <= bareme && moyenne >= 0)
                            {
                                note.note = (moyenne * 20) / bareme;
                                noteBL.enregistrerAnonymat(note);
                                row_index = (row_index + 1) % MAX_ROW;
                                SelectCellByIndex(grdListe, row_index, COLUMN_NOTE);
                            }
                            else
                            {
                                MessageBox.Show("La note doit être positive et inférieure à " + bareme);
                                textBox.Text = "";
                            }
                        }
                        catch (FormatException)
                        {
                            if (textBox.Text.ToUpper() == "A")
                            {
                                note.note = -1;
                                noteBL.enregistrerAnonymat(note);
                                row_index = (row_index + 1) % MAX_ROW;
                                SelectCellByIndex(grdListe, row_index, COLUMN_NOTE);
                            }
                            else
                            {
                                MessageBox.Show("Format incorrect, saisissez A pour absent ou un nombre pour sa  note", "School brain:Alerte");
                                textBox.Text = "";
                            }
                        }
                    }
                    catch (Exception)
                    {
                        row_index = (row_index + 1) % MAX_ROW;
                        SelectCellByIndex(grdListe, row_index, COLUMN_NOTE);
                    }
                }
                else
                    MessageBox.Show("cellule non modifiable");
            }
        }

        private void txtAnnee_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                cmbClasse_DropDownClosed(sender,e);
            }
        }

        private void cmbMatiere_DropDownClosed(object sender, EventArgs e)
        {
            if (cmbMatiere.SelectedValue != null)
            {
                matiere.codeMat = cmbMatiere.SelectedValue.ToString();
                matiere = noteBL.rechercherMatiere(matiere);
            }
            chargerEvaluer();
        }

        private void chargerEvaluer()
        {
            evaluations.Clear();
            if (cmbClasse.SelectedValue != null && cmbMatiere.SelectedValue != null && cmbSequence.SelectedValue != null && txtAnnee.Text != "")
            {
                evaluations = noteBL.listerEvaluation(cmbClasse.SelectedValue.ToString(), cmbMatiere.SelectedValue.ToString(), cmbSequence.SelectedValue.ToString(), annee);
                cmbEvaluation.ItemsSource = evaluations;
                cmbEvaluation.Items.Refresh();
            }
        }

        private void cmbBareme_DropDownClosed(object sender, EventArgs e)
        {
            if (cmbBareme.SelectedValue != null)
                grdNotes.Header = "Notes /" + cmbBareme.SelectedValue.ToString();
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
