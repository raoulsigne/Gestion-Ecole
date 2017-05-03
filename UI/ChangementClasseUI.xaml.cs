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
    public partial class ChangementClasseUI : Window
    {
        GestionChangementBL changementBL;
        private List<string> classes, classes_possibles;
        int annee;
        List<EleveBE> listeleves;
        List<LigneChangeClasse> lignes;

        public ChangementClasseUI()
        {
            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";
            Thread.CurrentThread.CurrentCulture = ci;

            InitializeComponent();

            classes = new List<string>();
            classes_possibles = new List<string>();
            changementBL = new GestionChangementBL();
            classes = changementBL.listerValeursColonneClasse("codeclasse");
            cmbClasse.ItemsSource = classes;
            annee = changementBL.AnneeEnCours();
            txtAnnee.Text = " / " + annee;
            txtAnneeScolaire.Text = (annee - 1).ToString();
            listeleves = new List<EleveBE>();
            lignes = new List<LigneChangeClasse>();
            grdListe.DataContext = this;
            grdListe.ItemsSource = lignes;
        }

        private void cmbClasse_DropDownClosed(object sender, EventArgs e)
        {
            if (cmbClasse.Text != null && cmbClasse.Text != "")
            {
                //recherche des eleves inscrits  dans cette classe pour charger le combobox des eleves
                string codeclasse = cmbClasse.Text;
                string codeniveau = changementBL.obtenirNiveau(codeclasse);
                classes_possibles = changementBL.classeDunNiveau(codeniveau);
                classe.ItemsSource = classes_possibles;
                listeleves = new List<EleveBE>();
                lignes = new List<LigneChangeClasse>();
                listeleves = changementBL.listerElevesDuneClasse(codeclasse, annee);
                if (listeleves != null)
                {
                    int i = 1;
                    foreach (EleveBE el in listeleves)
                    {
                        lignes.Add(new LigneChangeClasse(i++, el.nom, el.matricule, codeclasse));
                    }
                }
            }
        }


        private void txtAnnee_KeyUp(object sender, KeyEventArgs e)
        {
            if (cmbClasse.Text != "")
            {
                //recherche des eleves inscrits  dans cette classe pour charger le combobox des eleves
                string codeclasse = cmbClasse.Text;
                listeleves = new List<EleveBE>();
                lignes = new List<LigneChangeClasse>();
                listeleves = changementBL.listerElevesDuneClasse(codeclasse, annee);
                if (listeleves != null)
                {
                    int i = 1;
                    foreach (EleveBE el in listeleves)
                    {
                        lignes.Add(new LigneChangeClasse(i++, el.nom, el.matricule, codeclasse));
                    }
                }
            }
        }

        private void cmdOK_Click(object sender, RoutedEventArgs e)
        {
            cmdOK.IsEnabled = false;

            //recherche des eleves inscrits  dans cette classe pour charger le combobox des eleves
            string codeclasse = cmbClasse.Text;
            listeleves = new List<EleveBE>();
            lignes = new List<LigneChangeClasse>();
            listeleves = changementBL.listerElevesDuneClasse(codeclasse, annee);
            listeleves = listeleves.OrderBy(o => o.nom).ToList();
            if (listeleves != null)
            {
                int i = 1;
                foreach (EleveBE el in listeleves)
                {
                    lignes.Add(new LigneChangeClasse(i++, el.nom, el.matricule, codeclasse));
                }
            }

            grdListe.ItemsSource = lignes;
            grdListe.Items.Refresh();
        }

        private void cmdAnnuler_Click(object sender, RoutedEventArgs e)
        {
            cmbClasse.Text = "";
            lignes = new List<LigneChangeClasse>();
            grdListe.DataContext = this;
            grdListe.ItemsSource = lignes;
            cmdOK.IsEnabled = true;
        }

        private void cmdImprimer_Click(object sender, RoutedEventArgs e)
        {
            string classe = cmbClasse.SelectedValue.ToString();
            grdListe.ItemsSource = lignes;
            grdListe.Items.Refresh();
            List<string> headers = new List<string>();
            for (int j = 0; j < grdListe.Columns.Count; j++)
                headers.Add(grdListe.Columns[j].Header.ToString().ToUpper());

            CreerEtat creerEtat = new CreerEtat("changement de classe" + DateTime.Today.ToShortDateString(), "Liste des changements de classes");
            creerEtat.etatChangement(lignes, headers, classe, annee);
        }

        private void cmdFermer_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private bool validerFormulaire()
        {
            bool b = true;

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
            int COLUMN_CLASSE = 3;
            int MAX_ROW = lignes.Count;
            var grid = sender as DataGrid;
            DataGridRow row = new DataGridRow();
            string codeclasse = cmbClasse.SelectedValue.ToString();
            LigneChangeClasse ligne = new LigneChangeClasse();

            InscrireBE inscrire = new InscrireBE();

            if (e.Key == Key.Return)
            {
                e.Handled = true;

                int row_index = grdListe.Items.IndexOf(grdListe.CurrentItem);
                int col_index = grdListe.CurrentColumn.DisplayIndex;
                row = (DataGridRow)grid.ItemContainerGenerator.ContainerFromIndex(row_index);
                if (col_index == COLUMN_CLASSE)
                {
                    DataGridCellInfo cell = grid.SelectedCells[0];
                    ComboBox combobox;
                    try
                    {
                        combobox = (ComboBox)cell.Column.GetCellContent(cell.Item);
                        ligne = lignes.ElementAt(row_index);
                        inscrire.codeClasse = combobox.Text;
                        inscrire.matricule = ligne.matricule;
                        inscrire.annee = annee;

                        //changement de la classe
                        changementBL.modifierInscrire(inscrire);
                        
                        row_index = (row_index + 1) % MAX_ROW;
                        SelectCellByIndex(grdListe, row_index, COLUMN_CLASSE);
                    }
                    catch (Exception)
                    {
                        row_index = (row_index + 1) % MAX_ROW;
                        SelectCellByIndex(grdListe, row_index, COLUMN_CLASSE);
                    }
                }
                else
                    MessageBox.Show("cellule non modifiable");
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
