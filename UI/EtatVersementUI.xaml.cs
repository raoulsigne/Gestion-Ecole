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
using Ecole.ClasseConception;
using System.Globalization;
using System.Threading;
using Ecole.Utilitaire;

namespace Ecole.UI
{
    /// <summary>
    /// Interaction logic for EtatPrestationUI.xaml
    /// </summary>
    public partial class EtatVersementUI : Window
    {
        private GestionCaisseBL caisseBL;
        private List<LigneVersement> payers;
        decimal entree;

        public EtatVersementUI()
        {
            payers = new List<LigneVersement>();
            InitializeComponent();
            caisseBL = new GestionCaisseBL();
            dpiDateFin.IsTodayHighlighted = true;
            dpiDateFin.SelectedDate = DateTime.Today;
            dpiDateFin.Text = DateTime.Now.ToString();
            dpiDateDebut.SelectedDate = DateTime.Today;
            dpiDateDebut.IsTodayHighlighted = true;
            dpiDateDebut.Text = DateTime.Now.ToString();
            
            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "yyyy-MM-dd";
            Thread.CurrentThread.CurrentCulture = ci;

            entree = 0;
            payers = caisseBL.listerSuivantCriterePayers_versement("datepaiement = " + "'" + DateTime.Today.Date.ToShortDateString() + "'");

            changementMotif();
            
            foreach (LigneVersement l in payers)
            {
                l.nom = caisseBL.obtenirNomEleve(l.matricule);
                l.classe = caisseBL.obtenirClasse(l.matricule);
            }

            grdEtatCaisse.ItemsSource = payers;
            grdEtatCaisse.Items.Refresh();
            txtTotalEntree.IsEnabled = false;
        }

        private void imprimer_Click(object sender, RoutedEventArgs e)
        {
            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";
            Thread.CurrentThread.CurrentCulture = ci;

            string datefin = dpiDateFin.SelectedDate.Value.Date.ToShortDateString();
            string datedebut = dpiDateDebut.SelectedDate.Value.Date.ToShortDateString();

            CreerEtat etat = new CreerEtat("caisse-" + DateTime.Today.ToShortDateString(), "Bilan du versement des prestation");
            etat.etatVersement(grdEtatCaisse, datedebut, datefin, entree);
        }

        private void fermer_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /* fonction qui s'execute quand la date de fin est changee 
         *
         */
        private void dpiDateFin_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            chargerDataGrid();
        }

        /* fonction qui s'execute quand la date de debut est changee 
         *
         */
        private void dpiDateDebut_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            chargerDataGrid();
        }

        
        /**
         * fonction qui recupere les conditions pour alimenter la datagrid
         */
        private void chargerDataGrid()
        {
            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "yyyy-MM-dd";
            Thread.CurrentThread.CurrentCulture = ci;
            CultureInfo elGR = CultureInfo.CreateSpecificCulture("el-GR");

            string datefin = DateTime.Today.Date.ToShortDateString();
            string datedebut = DateTime.Today.Date.ToShortDateString();
            if (dpiDateFin.SelectedDate != null)
                datefin = dpiDateFin.SelectedDate.Value.Date.ToShortDateString();
            if (dpiDateDebut.SelectedDate != null)
                datedebut = dpiDateDebut.SelectedDate.Value.Date.ToShortDateString();
            OperationBE operation = new OperationBE();

            entree = 0;

            if (datefin.CompareTo(datedebut) >= 0)
            {
                payers = new List<LigneVersement>();
                payers = caisseBL.listerSuivantCriterePayers_versement("datepaiement between " + "'" + datedebut + "' AND " + "'" + datefin + "'");

                //calcul du montant des entrees et des sorties
                if (payers != null)
                    foreach (LigneVersement p in payers)
                        entree += (decimal)p.montant;

                string motif = txtMotif.Text;

                foreach (LigneVersement l in payers)
                {
                    l.nom = caisseBL.obtenirNomEleve(l.matricule);
                    try
                    {
                        l.classe = caisseBL.obtenirClasse(l.matricule);
                    }
                    catch (Exception)
                    {
                        l.classe = " - ";
                    }
                }
                grdEtatCaisse.ItemsSource = payers;
                grdEtatCaisse.Items.Refresh();

                txtTotalEntree.Text = entree.ToString("0,0", elGR);
            }
            else
            {
                MessageBox.Show("La date de debut doit être plus petite ou égale à la date de fin");
                payers = new List<LigneVersement>();
                grdEtatCaisse.ItemsSource = payers;
                grdEtatCaisse.Items.Refresh();
            }
        }

        private void txtMotif_KeyUp(object sender, KeyEventArgs e)
        {
            changementMotif();
        }

        private void changementMotif()
        {
            CultureInfo elGR = CultureInfo.CreateSpecificCulture("el-GR");
            List<LigneVersement> tampon = new List<LigneVersement>();
            string motif = txtMotif.Text;

            //on charge d'abord la grille et puis on tri sur les elements affiches
            chargerDataGrid();
            entree = 0;
            foreach (LigneVersement l in payers)
            {
                l.nom = caisseBL.obtenirNomEleve(l.matricule);
                l.classe = caisseBL.obtenirClasse(l.matricule);
            }
            foreach (LigneVersement l in payers)
            {
                if (motif == "" || l.nom.ToUpper().Contains(motif.ToUpper()))
                    tampon.Add(l);
            }

            payers = new List<LigneVersement>();
            foreach (LigneVersement l in tampon)
            {
                payers.Add(l);
                entree += Convert.ToDecimal(l.montant);
            }

            grdEtatCaisse.ItemsSource = payers;
            grdEtatCaisse.Items.Refresh();
            txtTotalEntree.Text = entree.ToString("0,0", elGR);
        }

    }
}
