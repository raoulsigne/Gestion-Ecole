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
using Ecole.Utilitaire;
using System.Globalization;
using System.Threading;

namespace Ecole.UI
{
    /// <summary>
    /// Interaction logic for DisciplineUI.xaml
    /// </summary>
    public partial class DisciplineUI : Window
    {
        GestionDisciplineBL disciplineBL;
        DisciplineBE old_discipline;
        List<DisciplineBE> disciplines;
        List<string> variable;
        static string TYPE_ENREGISTRER = "enregistrer";
        static string TYPE_MODIFIER = "modifier";
        string typeValidation;

        public DisciplineUI()
        {
            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";
            Thread.CurrentThread.CurrentCulture = ci;
            
            InitializeComponent();

            disciplineBL = new GestionDisciplineBL();
            old_discipline = new DisciplineBE();
            typeValidation = TYPE_ENREGISTRER;
            disciplines = new List<DisciplineBE>();
            variable = new List<string>() {"Heure", "Jour", "Mois" };

            disciplines = disciplineBL.listerToutDiscipline();
            grdListe.ItemsSource = disciplines;
            cmbVariable.ItemsSource = variable;
            cmbVariable.SelectedIndex = 0;
        }

        private void cmdEnregistrer_Click(object sender, RoutedEventArgs e)
        {
            if (validerFormulaire())
            {
                DisciplineBE discipline = new DisciplineBE();
                discipline.codeSanction = txtCodeSanction.Text;
                discipline.nomSanction = txtNomSanction.Text;
                discipline.variable = cmbVariable.SelectedValue.ToString();
                discipline.unite = cmbVariable.SelectedValue.ToString().ElementAt(0).ToString();
                discipline.priorite = Convert.ToInt16(txtPriorite.Text);

                if (typeValidation == TYPE_ENREGISTRER)
                {
                    if (disciplineBL.enregistrerDiscpline(discipline))
                        MessageBox.Show("Enregistrement effectué", "School brain : Alerte",MessageBoxButton.OK,MessageBoxImage.Information);
                    else
                        MessageBox.Show("Enregistrement échoué", "School brain : Alerte", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                else
                {
                    if (disciplineBL.modifierDiscipline(old_discipline, discipline))
                    {
                        MessageBox.Show("Mise à jour effectuée", "School brain : Alerte", MessageBoxButton.OK, MessageBoxImage.Information);
                        //disciplines.Remove(old_discipline);
                    }
                    else
                        MessageBox.Show("Mise à jour échouée", "School brain : Alerte", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                disciplines.Clear();
                disciplines = disciplineBL.listerToutDiscipline();
                grdListe.ItemsSource = disciplines;
                grdListe.Items.Refresh();
                txtCodeSanction.Clear();
                txtNomSanction.Clear();
                txtPriorite.Clear();
                typeValidation = TYPE_ENREGISTRER;
            }
            else
                MessageBox.Show("il y'a des champs vides, remplir tous les champs du formulaire", "School brain : alerte", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }

        private bool validerFormulaire()
        {
            if (txtCodeSanction.Text == "" || txtNomSanction.Text == "" || cmbVariable.SelectedValue == null || txtPriorite.Text == "")
                return false;
            else
                return true;
        }

        private void cmdAnnuler_Click(object sender, RoutedEventArgs e)
        {
            txtCodeSanction.Clear();
            txtNomSanction.Clear();
            txtPriorite.Clear();
            cmbVariable.SelectedIndex = 0;
            typeValidation = TYPE_ENREGISTRER;
            grdListe.UnselectAll();
        }

        private void cmdFermer_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cmdImprimer_Click(object sender, RoutedEventArgs e)
        {
            CreerEtat etat = new CreerEtat("Discipline-" + DateTime.Today.ToShortDateString(), "Liste des disciplines");
            disciplineBL.journaliser("Impression de la liste de sanctions");
            etat.obtenirEtat(grdListe);
        }

        private void grdListe_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                if (MessageBox.Show("Voulez vous supprimer?", "School : Confirmation", System.Windows.MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    if (grdListe.SelectedIndex != -1)
                    {
                        DisciplineBE discipline = new DisciplineBE();
                        discipline = disciplines.ElementAt(grdListe.SelectedIndex);
                        disciplineBL.supprimerDiscipline(discipline);
                        disciplines.Remove(discipline);
                        grdListe.ItemsSource = disciplines;
                        grdListe.Items.Refresh();
                        txtCodeSanction.Clear();
                        txtNomSanction.Clear();
                        txtPriorite.Clear();
                        typeValidation = TYPE_ENREGISTRER;
                    }
                    else
                        MessageBox.Show("Aucune ligne sélectionnée", "School brain:Alerte", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
        }

        private void grdListe_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (grdListe.SelectedIndex >= 0)
            {
                old_discipline = disciplines.ElementAt(grdListe.SelectedIndex);
                txtCodeSanction.Text = old_discipline.codeSanction;
                txtNomSanction.Text = old_discipline.nomSanction;
                txtPriorite.Text = Convert.ToString(old_discipline.priorite);
                cmbVariable.SelectedIndex = cmbVariable.Items.IndexOf(old_discipline.variable);
                typeValidation = TYPE_MODIFIER;
                grdListe.UnselectAll();
            }
        }

        private void txtPriorite_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Utilitaires.IsTextAllowed(e.Text);
        }
    }
}
