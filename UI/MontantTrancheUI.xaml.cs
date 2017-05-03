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
using Ecole.BusinessLogic;
using Ecole.BusinessEntity;
using System.Globalization;
using System.Threading;
using System.Text.RegularExpressions;
using Ecole.Utilitaire;

namespace Ecole.UI
{
    /// <summary>
    /// Interaction logic for MontantTrancheUI.xaml
    /// </summary>
    public partial class MontantTrancheUI : Window
    {
        static string INSCRIPTION = "ins";
        GestionMontantTrancheBL montantTrancheBL;
        List<MontantTrancheBE> montantTranches;
        MontantTrancheBE ancien_mt;
        List<string> categories;
        List<string> prestations;
        List<string> tranches;
        int annee;
        bool doubleclick;
        string categorie, prestation, tranche;
        double montant;
        DateTime date;

        public MontantTrancheUI()
        {
            InitializeComponent();
            dpiDelaiPaiement.SelectedDate = DateTime.Today;
            dpiDelaiPaiement.IsTodayHighlighted = true;
            dpiDelaiPaiement.Text = DateTime.Today.Date.ToShortDateString();
            
            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "yyyy-MM-dd";
            Thread.CurrentThread.CurrentCulture = ci;

            ancien_mt = new MontantTrancheBE();
            categories = new List<string>();
            prestations = new List<string>();
            tranches = new List<string>();
            doubleclick = false;
            montantTrancheBL = new GestionMontantTrancheBL();
            montantTranches = new List<MontantTrancheBE>();
            
            annee = montantTrancheBL.anneeEnCours();
            categories = montantTrancheBL.listerValeurColonneCategorieEleve("codecateleve");
            prestations = montantTrancheBL.listerValeurColonnePrestation("codeprestation");
            prestations.Remove(INSCRIPTION);
            tranches = montantTrancheBL.listerValeurColonneTranche("codetranche");
            montantTranches = montantTrancheBL.listerSuivantCritereMontantTranche("annee = " + "'" + annee+"'");

            txtAnnee.Text = " / " + annee;
            txtAnneeScolaire.Text = (annee - 1).ToString();
            cmbCategorie.ItemsSource = categories;
            cmbCategorie.SelectedIndex = 0;
            cmbPrestation.ItemsSource = prestations;
            cmbPrestation.SelectedIndex = 0;
            cmbTranche.ItemsSource = tranches;
            cmbTranche.SelectedIndex = 0;
            grdTranches.DataContext = this;
            grdTranches.ItemsSource = montantTranches;
        }

        private void grdTranches_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                if (MessageBox.Show("Voulez vous supprimer?", "School : Confirmation", System.Windows.MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    if (grdTranches.SelectedIndex != -1)
                    {
                        MontantTrancheBE mt = new MontantTrancheBE();
                        mt = montantTranches.ElementAt(grdTranches.SelectedIndex);
                        montantTrancheBL.supprimerMontantTranche(mt);
                        montantTranches.Remove(mt);
                        grdTranches.ItemsSource = montantTranches;
                        grdTranches.Items.Refresh();
                    }
                    else
                        MessageBox.Show("Aucune ligne sélectionnée", "School brain:Alerte");
                }
            }
        }

        private void grdTranches_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (grdTranches.SelectedIndex >= 0)
            {
                ancien_mt = montantTranches.ElementAt(grdTranches.SelectedIndex);
                txtMontant.Text = Convert.ToString(ancien_mt.montant);
                cmbCategorie.SelectedIndex = cmbCategorie.Items.IndexOf(ancien_mt.codeCatEleve);
                cmbPrestation.SelectedIndex = cmbPrestation.Items.IndexOf(ancien_mt.codePrestation);
                cmbTranche.SelectedIndex = cmbTranche.Items.IndexOf(ancien_mt.codeTranche);
                txtAnneeScolaire.Text = (ancien_mt.annee-1).ToString();
                dpiDelaiPaiement.Text = ancien_mt.delai.Date.ToShortDateString();
                dpiDelaiPaiement.SelectedDate = ancien_mt.delai.Date;
                doubleclick = true;
                grdTranches.UnselectAll();
            }
        }

        private void cmdValider_Click(object sender, RoutedEventArgs e)
        {
            if (validerFormulaire())
            {
                categorie = cmbCategorie.SelectedValue.ToString();
                prestation = cmbPrestation.SelectedValue.ToString();
                tranche = cmbTranche.SelectedValue.ToString();
                montant = Convert.ToDouble(txtMontant.Text);
                date = dpiDelaiPaiement.SelectedDate.Value.Date;
                MontantTrancheBE m = new MontantTrancheBE(tranche, categorie, prestation, montant, annee, date);

                if (doubleclick)//c'est une modification
                {
                    //suppression de l'ancienne valeur
                    montantTrancheBL.supprimerMontantTranche(ancien_mt);
                    montantTranches.Remove(ancien_mt);
                    grdTranches.Items.Refresh();
                    doubleclick = false;
                }

                if (montantTrancheBL.rechercherMontantTranche(m) == null)
                {
                    if (montantTrancheBL.enregistrerMontantTranche(m))
                    {
                        MessageBox.Show("Montant de la tranche enregistré avec succès","School brain : Confirmation");
                        cmbTranche.Text = "";
                        txtMontant.Text = "";
                        montantTranches = montantTrancheBL.listerSuivantCritereMontantTranche("annee = " + "'" + annee + "'");
                        grdTranches.ItemsSource = montantTranches;
                    }
                    else
                        MessageBox.Show("Montant de la tranche non enregistré", "School brain : Alerte");
                }
                else
                    MessageBox.Show("Element deja enregistrer, pour les modifications double cliquer sur la ligne correspondante", "School brain : Alerte");
            }
            else
                MessageBox.Show("Formulaire non valide, renseigner tous les champs", "School brain : Alerte");
        }

        private void cmdFermer_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private bool validerFormulaire()
        {
            bool b = true;

            if (cmbCategorie.SelectedValue == null || cmbPrestation.SelectedValue == null || cmbTranche.SelectedValue == null || txtAnneeScolaire.Text == null || 
                dpiDelaiPaiement.SelectedDate == null || txtMontant.Text == null)
                b = false;
            else
                if (cmbCategorie.SelectedValue.ToString() == "" || cmbPrestation.SelectedValue.ToString() == "" || cmbTranche.SelectedValue.ToString() == "" || txtAnneeScolaire.Text == "" ||
                    txtMontant.Text.ToString() == "")
                    b = false;

            return b;
        }

        private void txtMontant_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Utilitaires.IsTextAllowed(e.Text);
        }

        private void cmdAnnuler_Click(object sender, RoutedEventArgs e)
        {
            cmbCategorie.SelectedIndex = 0;
            cmbPrestation.SelectedIndex = 0;
            cmbTranche.SelectedIndex = 0;
            txtMontant.Text = "";
            grdTranches.UnselectAll();
            doubleclick = false;
        }

        private void cmdImprimer_Click(object sender, RoutedEventArgs e)
        {
            CreerEtat etat = new CreerEtat("MontantTranches-" + DateTime.Today.ToShortDateString(), "Liste des montants des prestations");
            montantTrancheBL.journaliser("Impression de la liste des trances des prestations " + annee);
            etat.obtenirEtat(grdTranches);
        }

        private void txtAnneeScolaire_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                annee = Convert.ToInt32(txtAnneeScolaire.Text)+1;
                txtAnnee.Text = " / " + annee.ToString();
                montantTranches = montantTrancheBL.listerSuivantCritereMontantTranche("annee = " + "'" + annee + "'");
                grdTranches.ItemsSource = montantTranches;
                grdTranches.Items.Refresh();
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
