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
    /// Interaction logic for Etat_Operation_PrestationUI.xaml
    /// </summary>
    public partial class Etat_Operation_PrestationUI : Window
    {
        private decimal totalEntree;
        private decimal totalSortie;
        private decimal totalSolde;
        private GestionCaisseBL caisseBL;
        private List<RealiserBE> realisers;
        private List<PayerBE> payers;
        private List<LigneEtat> lignes;
        decimal entree, sortie, soldetotal;

        public Etat_Operation_PrestationUI()
        {
            totalEntree = 0;
            totalSolde = 0;
            totalSortie = 0;

            realisers = new List<RealiserBE>();
            //acheters = new List<AcheterBE>();
            payers = new List<PayerBE>();
            lignes = new List<LigneEtat>();
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

            realisers = caisseBL.listerSuivantCritereRealisers("dateop = " + "'" + DateTime.Today.Date.ToShortDateString() + "'");
            payers = caisseBL.listerSuivantCriterePayers("datepaiement = " + "'" + DateTime.Today.Date.ToShortDateString() + "'");

            lignes = creerDataSource(realisers, payers);
            changementMotif();
            grdEtatCaisse.ItemsSource = lignes;
            grdEtatCaisse.Items.Refresh();
            txtEntreePeriode.IsEnabled = false;
            txtSortiePeriode.IsEnabled = false;
            txtTotalEntree.IsEnabled = false;
            txtSoldeTotal.IsEnabled = false;
            txtAncienSolde.IsEnabled = false;
            txtSoldePeriode.IsEnabled = false;
            txtSoldeTotal.IsEnabled = false;
            txtTotalSolde.IsEnabled = false;
            txtTotalSortie.IsEnabled = false;

        }

        private void imprimer_Click(object sender, RoutedEventArgs e)
        {
            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";
            Thread.CurrentThread.CurrentCulture = ci;
            string datefin = dpiDateFin.SelectedDate.Value.Date.ToShortDateString();
            string datedebut = dpiDateDebut.SelectedDate.Value.Date.ToShortDateString();

            CreerEtat etat = new CreerEtat("caisse-" + DateTime.Today.ToShortDateString(), "Liste des operations de la caisse");
            //etat.etatCaisse(grdEtatCaisse, dpiDateDebut.Text, dpiDateFin.Text, Convert.ToDecimal(txtTotalEntree.Text), Convert.ToDecimal(txtTotalSortie.Text), Convert.ToDecimal(txtTotalSolde.Text), 1);
            etat.etatCaisse(grdEtatCaisse, datedebut, datefin, totalEntree, totalSortie, totalSolde, 1);
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
            List<RealiserBE> listes = new List<RealiserBE>();
            List<PayerBE> payers_avant = new List<PayerBE>();
            lignes = new List<LigneEtat>();
            decimal montantEntrees, montantSorties, solde;

            string datefin = DateTime.Today.Date.ToShortDateString();
            string datedebut = DateTime.Today.Date.ToShortDateString();
            if (dpiDateFin.SelectedDate != null)
                datefin = dpiDateFin.SelectedDate.Value.Date.ToShortDateString();
            if (dpiDateDebut.SelectedDate != null)
                datedebut = dpiDateDebut.SelectedDate.Value.Date.ToShortDateString();
            OperationBE operation = new OperationBE();

            if (datefin.CompareTo(datedebut) >= 0)
            {
                listes = caisseBL.listerSuivantCritereRealisers("dateop between " + "'" + datedebut + "' AND " + "'" + datefin + "'");
                realisers = new List<RealiserBE>();
                payers = new List<PayerBE>();

                foreach (RealiserBE r in listes)
                    realisers.Add(r);
                payers = caisseBL.listerSuivantCriterePayers("datepaiement between " + "'" + datedebut + "' AND " + "'" + datefin + "'");

                lignes = creerDataSource(realisers, payers);


                //calcul du montant des entrees et des sorties
                montantSorties = 0;
                montantEntrees = 0;
                if (listes != null)
                    foreach (RealiserBE r in listes)
                    {
                        //operation.codeOp = r.codeop;
                        //operation = caisseBL.rechercherOperation(operation);
                        if (r.codeop == "entree")
                            montantEntrees += r.montant;
                        else
                            montantSorties += r.montant;
                    }
                if (payers != null)
                    foreach (PayerBE p in payers)
                        montantEntrees += (decimal)p.montant;

                //calcul du solde avant la date de debut
                listes = caisseBL.listerSuivantCritereRealisers("dateop < " + "'" + datedebut + "'");
                payers_avant = caisseBL.listerSuivantCriterePayers("datepaiement < " + "'" + datedebut + "'");
                //operation = new OperationBE();
                solde = 0;
                if (listes != null)
                    foreach (RealiserBE r in listes)
                    {
                        //operation.codeOp = r.codeop;
                        //operation = caisseBL.rechercherOperation(operation);
                        if (r.codeop == "entree")
                            solde += r.montant;
                        else
                            solde -= r.montant;
                    }
                if (payers_avant != null)
                    foreach (PayerBE p in payers_avant)
                        solde += (decimal)p.montant;


                listes = caisseBL.listerTousRealiser();
                payers_avant = caisseBL.listerTousPayer();
                //operation = new OperationBE();
                soldetotal = 0;
                if (listes != null)
                    foreach (RealiserBE r in listes)
                    {
                        //operation.codeOp = r.codeop;
                        //operation = caisseBL.rechercherOperation(operation);
                        if (r.codeop == "entree")
                            soldetotal += r.montant;
                        else
                            soldetotal -= r.montant;
                    }
                if (payers_avant != null)
                    foreach (PayerBE p in payers_avant)
                        soldetotal += (decimal)p.montant;

                txtEntreePeriode.Text = montantEntrees.ToString("0,0", elGR);
                txtSortiePeriode.Text = montantSorties.ToString("0,0", elGR);
                txtSoldePeriode.Text = (montantEntrees - montantSorties).ToString("0,0", elGR);
                txtAncienSolde.Text = solde.ToString("0,0", elGR);
                txtSoldeTotal.Text = soldetotal.ToString("0,0", elGR);

                string motif = txtMotif.Text;
                decimal entree = 0, sortie = 0;
                List<LigneEtat> tampon = new List<LigneEtat>();
                foreach (LigneEtat l in lignes)
                {
                    if (motif == "" || l.motif.ToUpper().Contains(motif.ToUpper()))
                        tampon.Add(l);
                }

                lignes = new List<LigneEtat>();
                foreach (LigneEtat l in tampon)
                {
                    lignes.Add(l);
                    if (l.type.ToLower() == "entree")
                        entree += l.montant;
                    else
                        sortie += l.montant;
                }

                grdEtatCaisse.ItemsSource = lignes;
                grdEtatCaisse.Items.Refresh();

                txtTotalEntree.Text = entree.ToString("0,0", elGR);
                txtTotalSolde.Text = (entree - sortie).ToString("0,0", elGR);
                txtTotalSortie.Text = sortie.ToString("0,0", elGR);

                totalEntree = entree;
                totalSortie = sortie;
                totalSolde = (entree - sortie);
            }
            else
            {
                MessageBox.Show("La date de debut doit être plus petite ou égale à la date de fin");
                lignes = new List<LigneEtat>();
                grdEtatCaisse.ItemsSource = lignes;
                grdEtatCaisse.Items.Refresh();
            }
        }

        private List<LigneEtat> creerDataSource(List<RealiserBE> liste1, List<PayerBE> liste3)
        {
            List<LigneEtat> listes = new List<LigneEtat>();

            if (liste1 != null & liste1.Count > 0)
            {
                foreach (RealiserBE r in liste1)
                {
                    listes.Add(new LigneEtat(r.codeop, r.motif, r.dateop.ToShortDateString(), (decimal)r.montant, r.concerne));
                }
            }
            if (liste3 != null & liste3.Count > 0)
            {
                foreach (PayerBE p in liste3)
                {
                    listes.Add(new LigneEtat("Entree", "Paiement " + caisseBL.obtenirNomTranche(p.codeTranche) + " - " + caisseBL.obtenirNomPrestation(p.codePrestation), p.datePaiement.ToShortDateString(), (decimal)p.montant, caisseBL.obtenirNomEleve(p.matricule)));
                }
            }

            return listes;
        }

        private void txtMotif_KeyUp(object sender, KeyEventArgs e)
        {
            changementMotif();
        }

        private void changementMotif()
        {
            CultureInfo elGR = CultureInfo.CreateSpecificCulture("el-GR");
            List<LigneEtat> tampon = new List<LigneEtat>();
            string motif = txtMotif.Text;
            decimal entree = 0, sortie = 0;

            //on charge d'abord la grille et puis on tri sur les elements affiches
            chargerDataGrid();
            foreach (LigneEtat l in lignes)
            {
                if (motif == "" || l.motif.ToUpper().Contains(motif.ToUpper()))
                    tampon.Add(l);
            }
            lignes = new List<LigneEtat>();
            foreach (LigneEtat l in tampon)
            {
                lignes.Add(l);
                if (l.type.ToLower() == "entree")
                    entree += l.montant;
                else
                    sortie += l.montant;
            }

            grdEtatCaisse.ItemsSource = lignes;
            grdEtatCaisse.Items.Refresh();
            txtTotalEntree.Text = entree.ToString("0,0", elGR);
            txtTotalSolde.Text = (entree - sortie).ToString("0,0", elGR);
            txtTotalSortie.Text = sortie.ToString("0,0", elGR);
        }

    }
}
