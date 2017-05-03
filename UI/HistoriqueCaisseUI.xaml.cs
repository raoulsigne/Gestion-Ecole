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
using System.Globalization;
using System.Threading;
using Ecole.Utilitaire;

namespace Ecole.UI
{
    /// <summary>
    /// Interaction logic for HistoriqueCaisse.xaml
    /// </summary>
    public partial class HistoriqueCaisseUI : Window
    {
        private GestionHistoriqueCaisseBL caisseBL;
        private List<RealiserBE> realisers;
        private List<AcheterBE> acheters;
        private List<PayerBE> payers;
        private List<LigneHistorique> lignes;
        decimal entree, sortie, soldetotal;

        public HistoriqueCaisseUI()
        {
            realisers = new List<RealiserBE>();
            acheters = new List<AcheterBE>();
            payers = new List<PayerBE>();
            lignes = new List<LigneHistorique>();
            List<String> listOps = new List<string>();
            caisseBL = new GestionHistoriqueCaisseBL();
            entree = 0;
            sortie = 0;
            soldetotal = 0;

            InitializeComponent();
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
            acheters = caisseBL.listerSuivantCritereAcheters("dateachat = " + "'" + DateTime.Today.Date.ToShortDateString() + "'");
            payers = caisseBL.listerSuivantCriterePayers("datepaiement = " + "'" + DateTime.Today.Date.ToShortDateString() + "'");

            lignes = creerDataSource(realisers, acheters, payers);
            changementMotif();

            grdEtatCaisse.ItemsSource = lignes;
            grdEtatCaisse.Items.Refresh();
            
            txtEntreePeriode.IsEnabled = false;
            txtSortiePeriode.IsEnabled = false;
            txtTotalEntree.IsEnabled = false;
            txtSoldePeriode.IsEnabled = false;
            txtTotalSolde.IsEnabled = false;
            txtTotalSortie.IsEnabled = false;
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
            entree = 0;
            sortie = 0;
            soldetotal = 0;
            List<RealiserBE> listes = new List<RealiserBE>();
            List<AcheterBE> acheters_avant = new List<AcheterBE>();
            List<PayerBE> payers_avant = new List<PayerBE>();
            lignes = new List<LigneHistorique>();
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
                acheters = new List<AcheterBE>();
                payers = new List<PayerBE>();

                foreach (RealiserBE r in listes)
                    realisers.Add(r);
                acheters = caisseBL.listerSuivantCritereAcheters("dateachat between " + "'" + datedebut + "' AND " + "'" + datefin + "'");
                payers = caisseBL.listerSuivantCriterePayers("datepaiement between " + "'" + datedebut + "' AND " + "'" + datefin + "'");

                lignes = creerDataSource(realisers, acheters, payers);

                //calcul du montant des entrees et des sorties
                montantSorties = 0;
                montantEntrees = 0;
                if (listes != null)
                {
                    foreach (RealiserBE r in listes)
                    {
                        if (r.codeop == "entree")
                            montantEntrees += r.montant;
                        else
                            montantSorties += r.montant;
                    }
                }
                if (acheters != null)
                {
                    foreach (AcheterBE a in acheters)
                        montantEntrees += a.montant;
                }
                if (payers != null)
                    foreach (PayerBE p in payers)
                        montantEntrees += (decimal)p.montant;

                listes = caisseBL.listerTousRealiser();
                acheters_avant = caisseBL.listerTousAcheter();
                payers_avant = caisseBL.listerTousPayer();
                soldetotal = 0;
                if (listes != null)
                    foreach (RealiserBE r in listes)
                    {
                        if (r.codeop == "entree")
                            soldetotal += r.montant;
                        else
                            soldetotal -= r.montant;
                    }
                if (acheters_avant != null)
                    foreach (AcheterBE a in acheters_avant)
                        soldetotal += a.montant;
                if (payers_avant != null)
                    foreach (PayerBE p in payers_avant)
                        soldetotal += (decimal)p.montant;

                txtEntreePeriode.Text = montantEntrees.ToString("0,0", elGR);
                txtSortiePeriode.Text = montantSorties.ToString("0,0", elGR);
                txtSoldePeriode.Text = (montantEntrees - montantSorties).ToString("0,0", elGR);

                string motif = txtMotif.Text;
                List<LigneHistorique> tampon = new List<LigneHistorique>();
                foreach (LigneHistorique l in lignes)
                {
                    if (motif == "" || l.motif.ToUpper().Contains(motif.ToUpper()))
                        tampon.Add(l);
                }

                lignes = new List<LigneHistorique>();
                foreach (LigneHistorique l in tampon)
                {
                    lignes.Add(l);
                    try
                    {
                        entree += Convert.ToDecimal(l.entree);
                        l.entree = Convert.ToDouble(l.entree).ToString("0,0", elGR);
                    }
                    catch (Exception) { }
                    try
                    {
                        sortie += Convert.ToDecimal(l.sortie);
                        l.sortie = Convert.ToDouble(l.sortie).ToString("0,0", elGR);
                    }
                    catch (Exception) { }
                }

                grdEtatCaisse.ItemsSource = lignes;
                grdEtatCaisse.Items.Refresh();

                txtTotalEntree.Text = Convert.ToString(entree.ToString("0,0", elGR));
                txtTotalSolde.Text = Convert.ToString((entree - sortie).ToString("0,0", elGR));
                txtTotalSortie.Text = Convert.ToString(sortie.ToString("0,0", elGR));
            }
            else
            {
                MessageBox.Show("La date de debut doit être plus petite ou égale à la date de fin");
                lignes = new List<LigneHistorique>();
                grdEtatCaisse.ItemsSource = lignes;
                grdEtatCaisse.Items.Refresh();
            }
        }

        private List<LigneHistorique> creerDataSource(List<RealiserBE> liste1, List<AcheterBE> liste2, List<PayerBE> liste3)
        {
            List<LigneHistorique> listes = new List<LigneHistorique>();
            int numero = 1;
            if (liste1 != null & liste1.Count > 0)
            {
                foreach (RealiserBE r in liste1)
                {
                    if (r.codeop == "entree")
                        listes.Add(new LigneHistorique(numero++, r.motif, r.dateop.ToShortDateString(), Convert.ToInt32(r.montant), 0, r.concerne));
                    else
                        listes.Add(new LigneHistorique(numero++, r.motif, r.dateop.ToShortDateString(), 0, Convert.ToInt32(r.montant), r.concerne));
                }
            }
            if (liste2 != null & liste2.Count > 0)
            {
                foreach (AcheterBE a in liste2)
                {
                    listes.Add(new LigneHistorique(numero++, "Vente " + caisseBL.obtenirNomSetArticle(a.codesetarticle), a.datAchat.ToShortDateString(), Convert.ToInt32(a.montant), 0, caisseBL.obtenirNomEleve(a.matricule)));
                }
            }
            if (liste3 != null & liste3.Count > 0)
            {
                foreach (PayerBE p in liste3)
                {
                    listes.Add(new LigneHistorique(numero++, "Paiement " + caisseBL.obtenirNomTranche(p.codeTranche) + " - " + caisseBL.obtenirNomPrestation(p.codePrestation), p.datePaiement.ToShortDateString(), Convert.ToInt32(p.montant), 0, caisseBL.obtenirNomEleve(p.matricule)));
                }
            }

            return listes;
        }

        private void txtMotif_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                changementMotif();
            }
            catch (Exception) 
            {
                MessageBox.Show("erreur dans changement motif","school brain:alerte",MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }

        private void changementMotif()
        {
            CultureInfo elGR = CultureInfo.CreateSpecificCulture("el-GR");
            List<LigneHistorique> tampon = new List<LigneHistorique>();
            string motif = txtMotif.Text;
            
            //on charge d'abord la grille et puis on tri sur les elements affiches
            chargerDataGrid();
            entree = 0;
            sortie = 0;
            foreach (LigneHistorique l in lignes)
            {
                if (motif == "" || l.motif.ToUpper().Contains(motif.ToUpper()))
                    tampon.Add(l);
            }

            lignes = new List<LigneHistorique>();
            int i = 1;
            foreach (LigneHistorique l in tampon)
            {
                l.numero = i++;
                lignes.Add(l);
                try
                {
                    Convert.ToDecimal(l.entree);
                    entree += l.montant;
                }
                catch (Exception) 
                {
                    sortie += l.montant;
                }
            }

            grdEtatCaisse.ItemsSource = lignes;
            grdEtatCaisse.Items.Refresh();
            txtTotalEntree.Text = Convert.ToString(entree.ToString("0,0", elGR));
            txtTotalSolde.Text = Convert.ToString((entree - sortie).ToString("0,0", elGR));
            txtTotalSortie.Text = Convert.ToString(sortie.ToString("0,0", elGR));
        }

        private void imprimer_Click(object sender, RoutedEventArgs e)
        {
            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";
            Thread.CurrentThread.CurrentCulture = ci;
            string datefin = dpiDateFin.SelectedDate.Value.Date.ToShortDateString();
            string datedebut = dpiDateDebut.SelectedDate.Value.Date.ToShortDateString();

            CreerEtat etat = new CreerEtat("caisse-" + DateTime.Today.ToShortDateString(), "Historique des opérations de la caisse");
            etat.etatCaisse(grdEtatCaisse, datedebut, datefin, entree, sortie, (entree - sortie), 2);
        }

        private void fermer_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
