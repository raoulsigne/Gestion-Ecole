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
using System.Windows.Controls.Primitives;
using Ecole.ClasseConception;
using Ecole.BusinessEntity;
using Ecole.BusinessLogic;
using System.Globalization;
using System.Threading;
using Ecole.Utilitaire;

namespace Ecole.UI
{
    /// <summary>
    /// Interaction logic for JuryUI.xaml
    /// </summary>
    public partial class JuryUI : Window
    {
        List<string> classes;
        List<string> jurys;
        List<string> sequences;
        List<LigneJury> lignes;
        GestionJuryBL juryBL;
        List<InscrireBE> inscrires;
        public static string ANNUEL = "Annuel";
        public static string TRIMESTRIEL = "Trimestriel";
        public static string SEQUENTIEL = "Sequentiel";
        int annee;

        public JuryUI()
        {
            List<string> tampon = new List<string>();
            sequences = new List<string>();
            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";
            Thread.CurrentThread.CurrentCulture = ci;

            InitializeComponent();

            juryBL = new GestionJuryBL();
            classes = new List<string>();
            jurys = new List<string>();
            classes = juryBL.listerValeurColonneClasse("codeclasse");
            cmbClasse.ItemsSource = classes;
            cmbClasse.SelectedIndex = 0;
            sequences = juryBL.listerValeurColonneSequence("codeseq");
            foreach (string seq in sequences)
                jurys.Add(seq);
            tampon = juryBL.listerValeurColonneTrimestre("codetrimestre");
            foreach (string trim in tampon)
                jurys.Add(trim);
            jurys.Add(ANNUEL);
            cmbJury.ItemsSource = jurys;
            cmbJury.SelectedIndex = 0;
            annee = juryBL.anneeEnCours();
            txtAnnee.Text = " / " + annee;
            txtAnneeScolaire.Text = (annee - 1).ToString();
        }

        private void cmdOK_Click(object sender, RoutedEventArgs e)
        {
            if (validerFormulaire())
            {
                cmdOK.IsEnabled = false;

                string codeclasse = cmbClasse.SelectedValue.ToString();
                string jury = cmbJury.SelectedValue.ToString();
                int i = 1;
                EleveBE eleve = new EleveBE();
                List<string> matriculetampon = new List<string>();
                lignes = new List<LigneJury>();

                if (cmbJury.SelectedValue.ToString() == ANNUEL)
                {
                    List<ResultatAnnuelBE> resultats = new List<ResultatAnnuelBE>();
                    resultats = juryBL.listerSuivantCritereResultatAnnuel(codeclasse, annee);
                    if (resultats != null)
                    {
                        foreach (ResultatAnnuelBE r in resultats)
                        {
                            eleve.matricule = r.matricule;
                            eleve = juryBL.rechercherEleve(eleve);
                            lignes.Add(new LigneJury(i++, eleve.nom, r.matricule, r.decision));
                            matriculetampon.Add(r.matricule.ToUpper());
                        }
                        List<LigneJury> liste = lignes.OrderBy(o => o.nom).ToList();
                        lignes.Clear();
                        i = 1;
                        foreach (LigneJury l in liste)
                        {
                            l.numero = i++;
                            lignes.Add(l);
                        }
                    }
                }
                else
                {
                    if (!sequences.Contains(cmbJury.SelectedValue.ToString()))
                    {
                        List<ResultatTrimestrielBE> resultats = new List<ResultatTrimestrielBE>();
                        resultats = juryBL.listerSuivantCritereResultatTrimestriel(codeclasse, cmbJury.SelectedValue.ToString(), annee);
                        if (resultats != null)
                        {
                            foreach (ResultatTrimestrielBE r in resultats)
                            {
                                eleve.matricule = r.matricule;
                                eleve = juryBL.rechercherEleve(eleve);
                                lignes.Add(new LigneJury(i++, eleve.nom, r.matricule, r.decision));
                                matriculetampon.Add(r.matricule.ToUpper());
                            }
                            List<LigneJury> liste = lignes.OrderBy(o => o.nom).ToList();
                            lignes.Clear();
                            i = 1;
                            foreach (LigneJury l in liste)
                            {
                                l.numero = i++;
                                lignes.Add(l);
                            }
                        }
                    }
                    else
                    {
                        List<ResultatBE> resultats = new List<ResultatBE>();
                        resultats = juryBL.listerSuivantCritereResultatSequentiel(codeclasse, cmbJury.SelectedValue.ToString(), annee);
                        if (resultats != null)
                        {
                            foreach (ResultatBE r in resultats)
                            {
                                eleve.matricule = r.matricule;
                                eleve = juryBL.rechercherEleve(eleve);
                                lignes.Add(new LigneJury(i++, eleve.nom, r.matricule, r.remarque));
                                matriculetampon.Add(r.matricule.ToUpper());
                            }
                            List<LigneJury> liste = lignes.OrderBy(o => o.nom).ToList();
                            lignes.Clear();
                            i = 1;
                            foreach (LigneJury l in liste)
                            {
                                l.numero = i++;
                                lignes.Add(l);
                            }
                        }
                    }
                }

                inscrires = juryBL.listerSuivantCritereInscrire("codeclasse = " + "'" + codeclasse + "' and annee = " + "'" + annee + "'");
                if (inscrires != null)
                {
                    foreach (InscrireBE inscrire in inscrires)
                    {
                        if (!matriculetampon.Contains(inscrire.matricule.ToUpper()))
                        {
                            eleve.matricule = inscrire.matricule;
                            eleve = juryBL.rechercherEleve(eleve);
                            lignes.Add(new LigneJury(i++, eleve.nom, eleve.matricule, ""));
                        }
                    }
                    List<LigneJury> liste = lignes.OrderBy(o => o.nom).ToList();
                    lignes.Clear();
                    i = 1;
                    foreach (LigneJury l in liste)
                    {
                        l.numero = i++;
                        lignes.Add(l);
                    }
                }

                grdListe.ItemsSource = lignes;
                grdListe.Items.Refresh();
            }
            else
                MessageBox.Show("Veuillez remplir tous les champs");
        }

        private void cmdAnnuler_Click(object sender, RoutedEventArgs e)
        {
            cmdOK.IsEnabled = true;
            cmbClasse.SelectedIndex = 0;
            cmbJury.Text = "";
            lignes = new List<LigneJury>();
            grdListe.ItemsSource = lignes;
            grdListe.Items.Refresh();
        }

        private void cmdImprimer_Click(object sender, RoutedEventArgs e)
        {
            string classe = cmbClasse.SelectedValue.ToString();
            string jury = cmbJury.SelectedValue.ToString();
            string s;
            s = jury;
            if(jury != ANNUEL)
                s = "Trimestre " + cmbJury.SelectedValue.ToString();
            CreerEtat creerEtat = new CreerEtat("conseil_classe_"+classe+"_" + DateTime.Today.ToShortDateString(), "Conseil de classe");
            juryBL.journaliser("Impression de la liste des remarques du juury de " + classe);
            creerEtat.etatConseilClasse(grdListe, classe, s, annee);
        }

        private void cmdFermer_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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
            int COLUMN_DECISION = 3;
            int MAX_ROW = lignes.Count;
            var grid = sender as DataGrid;
            DataGridRow row = new DataGridRow();
            string codeclasse = cmbClasse.SelectedValue.ToString();
            string jury = cmbJury.SelectedValue.ToString();
            LigneJury ligne = new LigneJury();
            string matricule, remarque;

            if (e.Key == Key.Return)
            {
                e.Handled = true;

                int row_index = grdListe.Items.IndexOf(grdListe.CurrentItem);
                int col_index = grdListe.CurrentColumn.DisplayIndex;
                row = (DataGridRow)grid.ItemContainerGenerator.ContainerFromIndex(row_index);
                if (col_index == COLUMN_DECISION)
                {
                    DataGridCellInfo cell = grid.SelectedCells[0];
                    TextBox textBox;
                    try
                    {
                        textBox = (TextBox)cell.Column.GetCellContent(cell.Item);

                        ligne = lignes.ElementAt(row_index);
                        remarque = textBox.Text;
                        matricule = ligne.matricule;
                        juryBL.enregistrerRemarque(matricule, annee, remarque, jury);
                        row_index = (row_index + 1) % MAX_ROW;
                        SelectCellByIndex(grdListe, row_index, COLUMN_DECISION);
                    }
                    catch (Exception) 
                    {
                        row_index = (row_index + 1) % MAX_ROW;
                        SelectCellByIndex(grdListe, row_index, COLUMN_DECISION);
                    }
                }
                else
                    MessageBox.Show("cellule non modifiable");
            }
        }

        private bool validerFormulaire()
        {
            bool b = true;

            if (cmbClasse.SelectedValue == null || cmbJury.SelectedValue == null || txtAnneeScolaire.Text == null)
                b = false;
            else if (cmbClasse.SelectedValue.ToString() == "" || cmbJury.SelectedValue.ToString() == "" || txtAnneeScolaire.Text.ToString() == "")
                b = false;

            return b;
        }

        private void txtAnnee_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Utilitaires.IsTextAllowed(e.Text);
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
    }
}
