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
using Ecole.DataAccess;
using System.Collections.ObjectModel;
using System.Data;
using Ecole.BusinessLogic;
using System.Globalization;
using System.Threading;
using System.Text.RegularExpressions;
using Ecole.UI;
using Ecole.Utilitaire;

namespace Ecole.UI
{
    /// <summary>
    /// Interaction logic for ops_caisse.xaml
    /// </summary>
    public partial class Ops_caisse : Window
    {
        private string numero;
        private String stype = "";
        //private String soperation = "";
        private String smotif = "";
        private String concerne = "";
        private string motif = "";
        private decimal smontant;
        private DateTime sdate;
        //private List<OperationBE> operations;
        private List<TypeoperationBE> types;
        private List<RealiserBE> realisers;
        private GestionCaisseBL caisseBL;
        //chaine qui precise l'action à entreprendre quand on click sur le bouton Valider
        private string typeValidation;
        RealiserBE objet_realiser;
        String date;

        // Définition d'une liste 'ListeSeries' observable de 'Série'
        
        public Ops_caisse()
        {
            InitializeComponent();
            dpiDateOperation.IsTodayHighlighted = true;
            dpiDateOperation.SelectedDate = DateTime.Today;
            dpiDateOperation.IsTodayHighlighted = true;
            dpiDateOperation2.SelectedDate = DateTime.Today;
            dpiDateOperation2.IsTodayHighlighted = true;
            
            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "yyyy-MM-dd";
            Thread.CurrentThread.CurrentCulture = ci;

            objet_realiser = new RealiserBE();
            string op = "%";
            if(cmbOperation2.SelectedValue != null)
                op = cmbOperation2.SelectedValue.ToString();
            date = DateTime.Today.Date.ToShortDateString();
            motif = txtMotif2.Text;
            string critere = "dateop = " + "'" + date + "' AND codeop LIKE " + "'" + op + "' AND motif LIKE " + "'" + motif + "%'";
            //MessageBox.Show(critere);
            List<String> listTypes = new List<string>();
            //operations = new List<OperationBE>();
            types = new List<TypeoperationBE>();
            realisers = new List<RealiserBE>();
            caisseBL = new GestionCaisseBL();

            types = caisseBL.listerTypeOperation();
            listTypes = caisseBL.listerValeursColonneTypeOperation("codetypeop");
            cmbTypeOperation.ItemsSource = listTypes;
            cmbTypeOperation.SelectedIndex = 0;
            
            List<string> liste = new List<string>();
            liste = caisseBL.listerValeursColonneTypeOperation("codetypeop");
            if (liste != null)
            {
                liste.Add("Tout");
                cmbOperation2.ItemsSource = liste;
            }

            grdListe.DataContext = this;
            realisers = caisseBL.listerSuivantCritereRealisers(critere);
            grdListe.ItemsSource = realisers;

            //initialisation du type pour l'enregistrement
            typeValidation = "enregistrer";
        }

        private void annuler_Click(object sender, RoutedEventArgs e)
        {
            txtMontant.Clear();
            txtMotif.Clear();
            txtConcerne.Clear();
            //cmbOperation.Text = "";
            dpiDateOperation.IsTodayHighlighted = true;
            dpiDateOperation2.IsTodayHighlighted = true;
            cmbTypeOperation.IsEnabled = true;
            //cmbOperation.IsEnabled = true;
            grdListe.UnselectAll();
            typeValidation = "enregistrer";
        }

        private void valider_Click(object sender, RoutedEventArgs e)
        {
            if (validerFormulaire())
            {
                string typeop = "";
                //c'est un nouvel enregistrement
                if (typeValidation == "enregistrer")
                {
                    stype = cmbTypeOperation.SelectedValue.ToString();
                    //soperation = cmbOperation.SelectedValue.ToString();
                    smotif = txtMotif.Text;
                    if (stype == "entree")
                    {
                        smotif = "Encaissement - " + smotif;
                        typeop = "Bon d'Encaissement";
                    }
                    else{
                        smotif = "Decaissement - " + smotif;
                        typeop = "Bon de Decaissement";
                    }

                    smontant = Convert.ToDecimal(txtMontant.Text);
                    sdate = (DateTime)dpiDateOperation.SelectedDate;
                    concerne = txtConcerne.Text;
                    numero = caisseBL.getNumeroSuivant();
                    RealiserBE realiser = new RealiserBE(stype, Ecole.UI.ConnexionUI.utilisateur.login, numero, smotif, smontant, sdate, concerne);
                    //MessageBox.Show(numero);
                    //enregistrement dans la BD
                    if (caisseBL.enregistrerRealiser(realiser))
                    {
                        CreerEtat etat = new CreerEtat("bordorau-"+DateTime.Today.ToShortDateString(),typeop+" N° "+realiser.numeroop);
                        etat.bordoreauOperation(realiser);

                    }
                    else
                        MessageBox.Show("Enregistrement échoué");

                }
                else //c'est une modification
                {
                    RealiserBE realiser = new RealiserBE();
                    realiser = caisseBL.rechercherByNumeroRealiser(objet_realiser);
                    realiser.motif = txtMotif.Text;
                    realiser.montant = Convert.ToDecimal(txtMontant.Text);
                    realiser.dateop = (DateTime)dpiDateOperation.SelectedDate;
                    realiser.concerne = txtConcerne.Text;

                    //modification dans la BD
                    if (caisseBL.modifierRealiser(realiser))
                    {
                        CreerEtat etat = new CreerEtat("bordorau-" + DateTime.Today.ToShortDateString(), "Bon d' "+realiser.motif.Split('-')[0]+" N° "+realiser.numeroop);
                        etat.bordoreauOperation(realiser);
                    }
                    else
                        MessageBox.Show("Mise à jour échouée");

                    cmbTypeOperation.IsEnabled = true;
                    //cmbOperation.IsEnabled = true;
                    typeValidation = "enregistrer";

                }
                
                //recuperation des valeurs des conditions pour la recherche des objets realisers
                string op = "%";
                if (cmbOperation2.SelectedValue != null)
                    op = cmbOperation2.SelectedValue.ToString();
                //if (dpiDateOperation2.SelectedDate != null)
                //    date = dpiDateOperation2.SelectedDate.Value.Date.ToShortDateString();
                motif = txtMotif2.Text;
                string critere = "dateop = " + "'" + date + "' AND codeop LIKE " + "'" + op + "' AND motif LIKE " + "'%" + motif + "%'";
                //MessageBox.Show(critere);
                realisers = caisseBL.listerSuivantCritereRealisers(critere);
                grdListe.ItemsSource = realisers;
                grdListe.Items.Refresh();

                txtMontant.Clear();
                txtMotif.Clear();
                txtConcerne.Clear();
                cmbTypeOperation.Text = "";
                //cmbOperation.Text = "";
                dpiDateOperation.IsTodayHighlighted = true;
                dpiDateOperation2.IsTodayHighlighted = true;
            }
            else
                MessageBox.Show("Renseigner tous les champs","School brain : Message d'alerte");
        }

        /*
         * Fenetre qui gere les etats pour les impressions 
        */
        private void etat_Click(object sender, RoutedEventArgs e)
        {
            Etat etat = new Etat();
            etat.ShowDialog();
        }

        private void cmbTypeOperation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //rechercher les operations dont le type est dans typeOperation
            //List<String> listOps = new List<string>();
            if (cmbTypeOperation.SelectedValue != null)
            {
                stype = cmbTypeOperation.SelectedValue.ToString();
                //listOps = caisseBL.listerValeursColonneOperationParType(stype);
                //cmbOperation.ItemsSource = listOps;
            }
        }

        private void dpiDateOperation2_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
                ci.DateTimeFormat.ShortDatePattern = "yyyy-MM-dd";
                Thread.CurrentThread.CurrentCulture = ci;

                //mettre a jour la datagrid
                date = DateTime.Today.Date.ToShortDateString();
                string op = "%";
                if (cmbOperation2.SelectedValue != null)
                    if (cmbOperation2.SelectedValue.ToString() != "Tout")
                        op = cmbOperation2.SelectedValue.ToString();
                date = dpiDateOperation2.SelectedDate.Value.Date.ToShortDateString();
                //MessageBox.Show(date);
                motif = txtMotif2.Text;
                string critere = "dateop = " + "'" + date + "' AND codeop LIKE " + "'" + op + "' AND motif LIKE " + "'%" + motif + "%'";
                realisers = new List<RealiserBE>();
                realisers = caisseBL.listerSuivantCritereRealisers(critere);
                grdListe.ItemsSource = realisers;
                grdListe.Items.Refresh();
            }
            catch (Exception) { }
        }

        private void cmbOperation2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //mettre a jour la datagrid
            string date = DateTime.Today.Date.ToShortDateString();
            string op = "%";
            if (cmbOperation2.SelectedValue != null)
                if(cmbOperation2.SelectedValue.ToString() != "Tout")
                    op = cmbOperation2.SelectedValue.ToString();
            date = dpiDateOperation2.SelectedDate.Value.Date.ToShortDateString();
            motif = txtMotif2.Text;
            string critere = "dateop = " + "'" + date + "' AND codeop LIKE " + "'" + op + "' AND motif LIKE " + "'%" + motif + "%'";

            realisers = new List<RealiserBE>();
            realisers = caisseBL.listerSuivantCritereRealisers(critere);
            grdListe.ItemsSource = realisers;
            grdListe.Items.Refresh();
        }

        private void txtMotif2_LostFocus(object sender, RoutedEventArgs e)
        {
            //mettre a jour la datagrid
            //recuperation des valeurs des conditions pour la recherche des objets realisers
            string date = DateTime.Today.Date.ToShortDateString();
            string op = "%";
            if (cmbOperation2.SelectedValue != null)
                if (cmbOperation2.SelectedValue.ToString() != "Tout")
                    op = cmbOperation2.SelectedValue.ToString();
            if (dpiDateOperation2.SelectedDate != null)
                date = dpiDateOperation2.SelectedDate.Value.Date.ToShortDateString();
            motif = txtMotif2.Text;
            string critere = "dateop = " + "'" + date + "' AND codeop LIKE " + "'" + op + "' AND motif LIKE " + "'%" + motif + "%'";

            realisers = caisseBL.listerSuivantCritereRealisers(critere);
            grdListe.ItemsSource = realisers;
            grdListe.Items.Refresh();
        }

        //private void btnUpdate_Click(object sender, RoutedEventArgs e)
        //{
            
        //}

        //private void btnDelete_Click(object sender, RoutedEventArgs e)
        //{
        //    MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Etes vous sure?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
        //    if (messageBoxResult == MessageBoxResult.Yes)
        //    {
                
        //    }
        //    else
        //    {
        //        MessageBox.Show("operation aborded");
        //    }
        //}

        private void grdListe_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (grdListe.SelectedIndex >= 0)
            {
                objet_realiser = realisers.ElementAt(grdListe.SelectedIndex);
                txtMotif.Text = objet_realiser.motif;
                txtMontant.Text = objet_realiser.montant.ToString();
                txtConcerne.Text = objet_realiser.concerne;
                dpiDateOperation.SelectedDate = objet_realiser.dateop.Date;
                numero = objet_realiser.numeroop;
                //cmbOperation.SelectedIndex = cmbOperation.Items.IndexOf(objet_realiser.codeop);
                cmbTypeOperation.IsEnabled = false;
                //cmbOperation.IsEnabled = false;

                typeValidation = "update";
                grdListe.UnselectAll();
            }
        }

        private void grdListe_KeyUp(object sender, KeyEventArgs e)
        {
            String //recuperation des valeurs des conditions pour la recherche des objets realisers
            date = DateTime.Today.Date.ToShortDateString();
            string op = "%";
            if (cmbOperation2.SelectedValue != null)
                if (cmbOperation2.SelectedValue.ToString() != "Tout")
                    op = cmbOperation2.SelectedValue.ToString();
            if (dpiDateOperation2.SelectedDate != null)
                date = dpiDateOperation2.SelectedDate.Value.Date.ToShortDateString();
            motif = txtMotif2.Text;
            string critere = "dateop = " + "'" + date + "' AND codeop LIKE " + "'" + op + "' AND motif LIKE " + "'%" + motif + "%'";

            if (e.Key == Key.Delete)
            {

                if (MessageBox.Show("Voulez vous supprimer?", "School : Confirmation", System.Windows.MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    if (grdListe.SelectedIndex != -1)
                    {
                        RealiserBE realiser = new RealiserBE();
                        realiser = realisers.ElementAt(grdListe.SelectedIndex);
                        caisseBL.supprimerRealiser(realiser);
                        realisers.Remove(realiser);
                        grdListe.ItemsSource = realisers;
                        grdListe.Items.Refresh();
                    }
                    else
                        MessageBox.Show("Aucune ligne sélectionnée", "School brain:Alerte");
                } 
            }
        }

        private void txtMotif2_KeyUp(object sender, KeyEventArgs e)
        {
            //mettre a jour la datagrid
            //recuperation des valeurs des conditions pour la recherche des objets realisers
            string date = DateTime.Today.Date.ToShortDateString();
            string op = "%";
            if (cmbOperation2.SelectedValue != null)
                if (cmbOperation2.SelectedValue.ToString() != "Tout")
                    op = cmbOperation2.SelectedValue.ToString();
            if (dpiDateOperation2.SelectedDate != null)
                date = dpiDateOperation2.SelectedDate.Value.Date.ToShortDateString();
            motif = txtMotif2.Text;
            string critere = "dateop = " + "'" + date + "' AND codeop LIKE " + "'" + op + "' AND motif LIKE " + "'%" + motif + "%'";

            realisers = new List<RealiserBE>();
            realisers = caisseBL.listerSuivantCritereRealisers(critere);
            grdListe.ItemsSource = realisers;
        }

        private bool validerFormulaire()
        {
            bool b = true;

            if (typeValidation == "enregistrer")
            {
                if (cmbTypeOperation.SelectedValue == null || txtMotif.Text == null || txtMontant.Text == null ||
                    txtConcerne.Text == null || dpiDateOperation.SelectedDate == null)
                    b = false;
                else if (cmbTypeOperation.SelectedValue.ToString() == "" || txtMotif.Text.ToString() == "" || txtMontant.Text.ToString() == "" ||
                    txtConcerne.Text.ToString() == "")
                    b = false;
            }

            return b;
        }

        private void txtMontant_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Utilitaires.IsTextAllowed(e.Text);
        }

        private void cmdFermer_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
