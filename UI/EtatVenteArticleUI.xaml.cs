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
    /// Interaction logic for EtatVenteArticleUI.xaml
    /// </summary>
    public partial class EtatVenteArticleUI : Window
    {
        private decimal totalEntree;
        private decimal totalSortie;
        private decimal totalSolde;
        private GestionCaisseBL caisseBL;
        private List<AcheterBE> acheters;
        private List<LigneEtat> lignes;
        private static string CODE_MIXTE = "Tous";
        decimal entree, sortie, soldetotal;

        public EtatVenteArticleUI()
        {
            totalEntree = 0;
            totalSolde = 0;
            totalSortie = 0;

            acheters = new List<AcheterBE>();
            lignes = new List<LigneEtat>();
            InitializeComponent();
            List<String> listOps = new List<string>();
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

            listOps = caisseBL.listerValeursColonneTypeOperation("codetypeop");
            listOps.Add(CODE_MIXTE);
            acheters = caisseBL.listerSuivantCritereAcheters("dateachat = " + "'" + DateTime.Today.Date.ToShortDateString() + "'");

            lignes = creerDataSource(acheters);
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
            CreerEtat etat = new CreerEtat("caisse-" + DateTime.Today.ToShortDateString(), "Bilan de vente des articles");
            etat.etatCaisse(grdEtatCaisse, datedebut, datefin, totalEntree, totalSortie, totalSolde, 0);
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
            List<AcheterBE> acheters_avant = new List<AcheterBE>();
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
                acheters = new List<AcheterBE>();
                acheters = caisseBL.listerSuivantCritereAcheters("dateachat between " + "'" + datedebut + "' AND " + "'" + datefin + "'");

                lignes = creerDataSource(acheters);


                //calcul du montant des entrees et des sorties
                montantSorties = 0;
                montantEntrees = 0;
                if (acheters != null)
                    foreach (AcheterBE a in acheters)
                        montantEntrees += a.montant;

                //calcul du solde avant la date de debut
                acheters_avant = caisseBL.listerSuivantCritereAcheters("dateachat < " + "'" + datedebut + "'");
                solde = 0;
                if (acheters_avant != null)
                    foreach (AcheterBE a in acheters_avant)
                        solde += a.montant;
                acheters_avant = caisseBL.listerTousAcheter();
                soldetotal = 0;
                if (acheters_avant != null)
                    foreach (AcheterBE a in acheters_avant)
                        soldetotal += a.montant;

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

        private List<LigneEtat> creerDataSource(List<AcheterBE> liste2)
        {
            List<LigneEtat> listes = new List<LigneEtat>();

            if (liste2 != null & liste2.Count > 0)
            {
                foreach (AcheterBE a in liste2)
                {
                    listes.Add(new LigneEtat("Entree", "Vente " + caisseBL.obtenirNomSetArticle(a.codesetarticle), a.datAchat.ToShortDateString(), a.montant, caisseBL.obtenirNomEleve(a.matricule)));
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
