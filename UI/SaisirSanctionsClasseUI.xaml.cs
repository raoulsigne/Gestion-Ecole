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
    /// Interaction logic for SaisirSanctionsClasseUI.xaml
    /// </summary>
    public partial class SaisirSanctionsClasseUI : Window
    {
        static string ETAT = "NON JUSTIFIEE";
        GestionSanctionClasseBL sanctionClasseBL;
        List<string> classes;
        List<string> sequences;
        List<string> sanctions;
        List<DisciplineBE> disciplines;
        List<InscrireBE> eleves;
        List<LigneSanction> lignes;
        static string NON_JUSTIFIEE = "NON JUSTIFIEE";
        int annee;

        public SaisirSanctionsClasseUI()
        {
            
            InitializeComponent();
            sanctionClasseBL = new GestionSanctionClasseBL();
            lignes = new List<LigneSanction>();
            classes = new List<string>();
            sequences = new List<string>();
            sanctions = new List<string>();
            eleves = new List<InscrireBE>();
            disciplines = new List<DisciplineBE>();
            dpiDateSanction.SelectedDate = DateTime.Today;
            dpiDateSanction.Text = DateTime.Today.ToShortDateString();

            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "yyyy-MM-dd";
            Thread.CurrentThread.CurrentCulture = ci;

            classes = sanctionClasseBL.listerValeurColonneClasse("codeclasse");
            sequences = sanctionClasseBL.listerValeurColonneSequence("codeseq");
            disciplines = sanctionClasseBL.listerToutDiscipline();
            if(disciplines != null)
                foreach (DisciplineBE d in disciplines)
                {
                    sanctions.Add(d.codeSanction +" - "+ d.nomSanction);
                }
            //sanctions = sanctionClasseBL.listerValeurColonneDiscipline("codesanction");
            cmbClasse.ItemsSource = classes;
            cmbSequence.ItemsSource = sequences;
            cmbSanction.ItemsSource = sanctions;
            
            cmbSanction.SelectedIndex = 0;
            DisciplineBE discipline = new DisciplineBE();
            discipline.codeSanction = cmbSanction.Text.Split('-')[0].Trim();
            discipline = sanctionClasseBL.rechercherDiscipline(discipline);
            if (discipline != null)
                txtVariable.Text = discipline.variable;

            txtVariable.IsEnabled = false;
            annee = sanctionClasseBL.anneeEnCours();
            txtAnnee.Text = " / " + annee.ToString();
            txtAnneeScolaire.Text = (annee - 1).ToString();
        }

        private void cmbClasse_DropDownClosed(object sender, EventArgs e)
        {
            //if (cmbSanction.SelectedValue != null && cmbSanction.SelectedValue.ToString() != "")
            //{
            //    DisciplineBE discipline = new DisciplineBE();
            //    discipline.codeSanction = cmbSanction.Text.Split('-')[0].Trim();
            //    discipline = sanctionClasseBL.rechercherDiscipline(discipline);
            //    if (discipline != null)
            //        txtVariable.Text = discipline.variable;
            //}
        }

        private void cmbSanction_DropDownClosed(object sender, EventArgs e)
        {
            if (cmbSanction.SelectedValue != null && cmbSanction.SelectedValue.ToString() != "")
            {
                DisciplineBE discipline = new DisciplineBE();
                //discipline.codeSanction = cmbSanction.SelectedValue.ToString();
                discipline.codeSanction = cmbSanction.Text.Split('-')[0].Trim();
                discipline = sanctionClasseBL.rechercherDiscipline(discipline);
                if (discipline != null)
                    txtVariable.Text = discipline.variable;
            }
        }

        private void cmdOK_Click(object sender, RoutedEventArgs e)
        {
            if (validerFormulaire())
            {
                string classe = cmbClasse.SelectedValue.ToString();
                string sequence = cmbSequence.SelectedValue.ToString();
                string sanction = cmbSanction.Text.Split('-')[0].Trim();
                string datesanction = dpiDateSanction.SelectedDate.Value.ToString("yyyy-MM-dd");
                lignes.Clear();
                int num = 1;
                EleveBE eleve;

                List<string> matriculeSaisies = new List<string>();
                List<SanctionnerBE> sanctionners = new List<SanctionnerBE>();
                sanctionners = sanctionClasseBL.listerSanctionnerClasse(classe, sanction, annee, sequence, datesanction);
                if (sanctionners != null)
                {
                    eleve = new EleveBE();
                    foreach (SanctionnerBE s in sanctionners)
                    {
                        eleve.matricule = s.matricule;
                        eleve = sanctionClasseBL.rechercherEleve(eleve);
                        lignes.Add(new LigneSanction(num++, eleve.nom, s.matricule, s.quantité, s.etat, s.datesanction));
                        matriculeSaisies.Add(s.matricule.ToUpper());
                    }
                }
                eleves = sanctionClasseBL.listerSuivantCritereInscrire("codeclasse = " + "'" + classe + "' and annee = " + "'" + annee + "'");
                if (eleves != null)
                {
                    eleve = new EleveBE();
                    foreach (InscrireBE i in eleves)
                    {
                        if (!matriculeSaisies.Contains(i.matricule.ToUpper()))
                        {
                            eleve.matricule = i.matricule;
                            eleve = sanctionClasseBL.rechercherEleve(eleve);
                            lignes.Add(new LigneSanction(num++, eleve.nom, eleve.matricule, 0, ETAT, DateTime.Today.Date));
                        }
                    }
                }

                List<LigneSanction> liste = lignes.OrderBy(o => o.nom).ToList();
                lignes.Clear();
                num = 1;
                foreach (LigneSanction l in liste)
                {
                    l.numero = num++;
                    lignes.Add(l);
                }
                grdListe.ItemsSource = lignes;
                grdListe.Items.Refresh();

                cmdHistorique.IsEnabled = false;
            }
            else
                MessageBox.Show("il y'a des champs vides, remplir tous les champs du formulaire", "School brain : alerte");
        }


        private void cmdHistorique_Click(object sender, RoutedEventArgs e)
        {
            if (cmbClasse.Text != "" && cmbSequence.Text != "" && cmbSanction.Text != "" && txtAnnee.Text != "")
            {
                string classe = cmbClasse.SelectedValue.ToString();
                string sequence = cmbSequence.SelectedValue.ToString();
                string sanction = cmbSanction.Text.Split('-')[0].Trim();
                lignes.Clear();
                int num = 1;
                EleveBE eleve;

                List<string> matriculeSaisies = new List<string>();
                List<SanctionnerBE> sanctionners = new List<SanctionnerBE>();
                sanctionners = sanctionClasseBL.listerSanctionnerClasse(classe, sanction, annee, sequence);
                if (sanctionners != null)
                {
                    eleve = new EleveBE();
                    foreach (SanctionnerBE s in sanctionners)
                    {
                        eleve.matricule = s.matricule;
                        eleve = sanctionClasseBL.rechercherEleve(eleve);
                        lignes.Add(new LigneSanction(num++, eleve.nom, s.matricule, s.quantité, s.etat, s.datesanction));
                        matriculeSaisies.Add(s.matricule.ToUpper());
                    }
                }
                List<LigneSanction> liste = lignes.OrderBy(o => o.nom).ToList();
                lignes.Clear();
                num = 1;
                foreach (LigneSanction l in liste)
                {
                    l.numero = num++;
                    lignes.Add(l);
                }
                grdListe.ItemsSource = lignes;
                grdListe.Items.Refresh();

                cmdOK.IsEnabled = false;
            }
            else
                MessageBox.Show("il y'a des champs vides, remplir tous les champs du formulaire", "School brain : alerte");
        }

        private bool validerFormulaire()
        {
            if (cmbClasse.SelectedValue == null || cmbSequence.SelectedValue == null || cmbSanction.SelectedValue == null || txtAnnee.Text == "")
                return false;
            else if (cmbClasse.SelectedValue.ToString() == "" || cmbSequence.SelectedValue.ToString() == "" || cmbSanction.SelectedValue.ToString() == "" || txtAnnee.Text == "")
                return false;
            else
                return true;
        }

        private void cmdAnnuler_Click(object sender, RoutedEventArgs e)
        {
            //cmbClasse.Text = "";
            //cmbSequence.Text = "";
            cmbSanction.Text = "";
            txtVariable.Text = "";
            lignes.Clear();
            cmdHistorique.IsEnabled = true;
            cmdOK.IsEnabled = true;
            grdListe.ItemsSource = lignes;
            grdListe.Items.Refresh();
        }

        #region fonction qui aide à la gestion des cellules du datagrid
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
        #endregion

        private void grdListe_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            int COLUMN_NOMBRE = 3;
            int MAX_ROW = lignes.Count;
            var grid = sender as DataGrid;
            DataGridRow row = new DataGridRow();

            if (e.Key == Key.Return)
            {
                e.Handled = true;

                string classe = cmbClasse.SelectedValue.ToString();
                string sequence = cmbSequence.SelectedValue.ToString();
                string sanction = cmbSanction.Text.Split('-')[0].Trim();
                LigneSanction ligne = new LigneSanction();
                int row_index = grdListe.Items.IndexOf(grdListe.CurrentItem);
                int col_index = grdListe.CurrentColumn.DisplayIndex;
                row = (DataGridRow)grid.ItemContainerGenerator.ContainerFromIndex(row_index);
                SanctionnerBE sanctionner = new SanctionnerBE();
                sanctionner.annee = annee;
                sanctionner.codesanction = sanction;
                sanctionner.datesanction = DateTime.Today;
                sanctionner.sequence = sequence;
                sanctionner.etat = NON_JUSTIFIEE;

                if (col_index == COLUMN_NOMBRE)
                {
                    DataGridCellInfo cell = grid.SelectedCells[0];
                    TextBox textBox;
                    try
                    {
                        textBox = (TextBox)cell.Column.GetCellContent(cell.Item);
                        ligne = lignes.ElementAt(row_index);
                        sanctionner.matricule = ligne.matricule;
                        try
                        {
                            sanctionner.quantité = Convert.ToInt32(textBox.Text);
                            sanctionClasseBL.enregistrerSanctionner(sanctionner);
                            row_index = (row_index + 1) % MAX_ROW;
                            SelectCellByIndex(grdListe, row_index, COLUMN_NOMBRE);
                        }
                        catch (FormatException)
                        {
                            MessageBox.Show("Le nombre doit être un entier");
                            textBox.Text = "";
                        }
                    }
                    catch
                    {
                        row_index = (row_index + 1) % MAX_ROW;
                        SelectCellByIndex(grdListe, row_index, COLUMN_NOMBRE);
                    }
                }
                else
                    MessageBox.Show("cellule non modifiable");
            }
        }

        private void cmdFermer_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cmdImprimer_Click(object sender, RoutedEventArgs e)
        {
            if (validerFormulaire())
            {
                string codeclasse = cmbClasse.SelectedValue.ToString();
                ClasseBE classe = new ClasseBE();
                classe.codeClasse = codeclasse;
                classe = sanctionClasseBL.rechercherClasse(classe);

                string codesequence = cmbSequence.SelectedValue.ToString();
                SequenceBE sequence = new SequenceBE();
                sequence.codeseq = codesequence;
                sequence = sanctionClasseBL.rechercherSequence(sequence);

                string codesanction = cmbSanction.Text.Split('-')[0].Trim();
                DisciplineBE sanction = new DisciplineBE();
                sanction.codeSanction = codesanction;
                sanction = sanctionClasseBL.rechercherDiscipline(sanction);


                string variable = txtVariable.Text;
                CreerEtat creerEtat = new CreerEtat("sanction-" + cmbClasse.SelectedValue.ToString() + "" + DateTime.Today.ToShortDateString(), "historique des sanctions d'une classe");
                sanctionClasseBL.journaliser("Impression de l'historique des sanctions de la "+classe.codeClasse);
                creerEtat.etatSanctionClasse(grdListe, classe.codeClasse + " - " + classe.nomClasse, sequence.codeseq + " - " + sequence.nomseq, sanction.codeSanction + " - " + sanction.nomSanction, variable);
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
