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
using Ecole.BusinessLogic; 
using System.Globalization;
using System.Threading;
using Ecole.ClasseConception;
using Ecole.Utilitaire;

namespace Ecole.UI 
{
    /// <summary>
    /// Interaction logic for Etat.xaml
    /// </summary>
    public partial class InsolvablesUI : Window
    {
       
        private GestionCaisseBL caisseBL;
        private List<RealiserBE> realisers;
        private List<AcheterBE> acheters;
        private List<PayerBE> payers;
        private List<LigneEtat> lignes;
        private static string CODE_MIXTE = "TOUS";
        decimal entree, sortie, soldetotal;

        ClasseBE classe;
        List<string> listClasses;
        List<LigneInsolvable> listLigneInsolvable;
        List<InscrireBE> listInscrits;
        GestionEleveDuneClasseBL eleveBL;
        private GestionJournalBL journalBL;
        //private DateTime datedebut, datefin;

        public InsolvablesUI()
        {
            classe = new ClasseBE();
            listClasses = new List<string>();
            listInscrits = new List<InscrireBE>();
            listLigneInsolvable = new List<LigneInsolvable>();
            eleveBL = new GestionEleveDuneClasseBL();
            journalBL = new GestionJournalBL();

            InitializeComponent();
            //Obtenir la liste des classes et les ajouter au comboBox des classes
            listClasses = eleveBL.listerValeursColonneClasse("codeclasse");
            cmbClasse.ItemsSource = listClasses;

            txtAnnee.Text = eleveBL.anneeEnCours().ToString();
            txtAnneeScolaire.Text = ((Convert.ToInt32(txtAnnee.Text.ToString())) - 1).ToString() + "/" + txtAnnee.Text;

            //Charger le comboBox des observations
            List<String> listObservations = new List<string>();
            listObservations.Add("OK");
            listObservations.Add("INSOLVABLE");
            listObservations.Add(CODE_MIXTE);
            cmbObservation.ItemsSource = listObservations;
            cmbObservation.SelectedIndex = cmbObservation.Items.IndexOf(CODE_MIXTE);

            //mettre la date d'aujourdui
            dpiDate.SelectedDate = DateTime.Today;
            dpiDate.Text = DateTime.Today.ToShortDateString();

            //charger la grille

            caisseBL = new GestionCaisseBL();
            
            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "yyyy-MM-dd";
            Thread.CurrentThread.CurrentCulture = ci;

            
           // cmbOperation.ItemsSource = listOps;
           // cmbOperation.SelectedIndex = cmbOperation.Items.IndexOf(CODE_MIXTE);
            realisers = caisseBL.listerSuivantCritereRealisers("dateop = "+"'"+ DateTime.Today.Date.ToShortDateString() + "'");
            acheters = caisseBL.listerSuivantCritereAcheters("dateachat = " + "'" + DateTime.Today.Date.ToShortDateString() + "'");
            payers = caisseBL.listerSuivantCriterePayers("datepaiement = " + "'" + DateTime.Today.Date.ToShortDateString() + "'");

            lignes = creerDataSource(realisers, acheters, payers);
           // changementMotif();
            //grdEtatCaisse.ItemsSource = lignes;
            //grdEtatCaisse.Items.Refresh();
            //dpiDateFin.IsTodayHighlighted = true;
            //dpiDateFin.SelectedDate = DateTime.Today;
            //dpiDateFin.Text = DateTime.Now.ToString();
            //dpiDateDebut.SelectedDate = DateTime.Today;
            //dpiDateDebut.IsTodayHighlighted = true;
            //dpiDateDebut.Text = DateTime.Now.ToString();
            //txtEntreePeriode.IsEnabled = false;
            //txtSortiePeriode.IsEnabled = false;
            //txtTotalEntree.IsEnabled = false;
            //txtSoldeTotal.IsEnabled = false;
            //txtAncienSolde.IsEnabled = false;
            //txtSoldePeriode.IsEnabled = false;
            //txtSoldeTotal.IsEnabled = false;
            //txtTotalSolde.IsEnabled = false;
            //txtTotalSortie.IsEnabled = false;
            
        }

        private void imprimer_Click(object sender, RoutedEventArgs e)
        {
            //CreerEtat etat = new CreerEtat("caisse-"+DateTime.Today.ToShortDateString(),"Liste des operations de la caisse");
            //etat.etatCaisse(grdEtatCaisse, dpiDateDebut.Text, dpiDateFin.Text, Convert.ToDecimal(txtTotalSolde.Text));
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

        /*
         * fonction qui s'execute quand la valeur de la liste deroulante est changee
         */
        private void cmbType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<LigneEtat> tampon = new List<LigneEtat>();
          //  string motif = txtMotif.Text;
            entree = 0;
            sortie = 0;

            //on charge d'abord la grille et puis on tri sur les elements affiches
            chargerDataGrid();
            foreach (LigneEtat l in lignes)
            {
                //if (cmbOperation.SelectedValue.ToString() == CODE_MIXTE || l.type.ToUpper().Contains(cmbOperation.SelectedValue.ToString().ToUpper()))
                //    tampon.Add(l);
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

            //grdEtatCaisse.ItemsSource = lignes;
            //grdEtatCaisse.Items.Refresh();
            //txtTotalEntree.Text = Convert.ToString(entree);
            //txtTotalSolde.Text = Convert.ToString(entree - sortie);
            //txtTotalSortie.Text = Convert.ToString(sortie);
        }

        /**
         * fonction qui recupere les conditions pour alimenter la datagrid
         */ 
        private void chargerDataGrid()
        {
            GestionEleveBL eleveBL = new GestionEleveBL();
           // List<LigneInsolvable> listLigneInsolvable=new List<LigneInsolvable>();

            int annee;
            string classe;
            string date;
           
            if ((txtAnnee.Text != "") && (cmbClasse.Text.ToString() != "") && (dpiDate.Text.ToString() != ""))
            {
                annee = Convert.ToInt32(txtAnnee.Text);
                classe = cmbClasse.Text.ToString();
                date = dpiDate.SelectedDate.Value.Date.ToShortDateString();
                listLigneInsolvable = eleveBL.getListeInsolvable(classe, annee, date);
                //grdInsolvable.ItemsSource = listLigneInsolvable;
                //grdInsolvable.Items.Refresh();


                List<LigneInsolvable> tampon = new List<LigneInsolvable>();
                int numero = 0;
                
                if (listLigneInsolvable != null)
                {
                    foreach (LigneInsolvable l in listLigneInsolvable)
                    {
                        if (cmbObservation.SelectedValue.ToString() == CODE_MIXTE || l.observation.ToUpper().Contains(cmbObservation.SelectedValue.ToString().ToUpper()))
                        {
                            numero = numero + 1;
                            l.numero = numero;
                            tampon.Add(l);
                        }
                    }

                    listLigneInsolvable = new List<LigneInsolvable>();
                    foreach (LigneInsolvable l in tampon)
                    {
                        listLigneInsolvable.Add(l);
                    }
                }
                grdInsolvable.ItemsSource = listLigneInsolvable;
                grdInsolvable.Items.Refresh();



            }
           
            //List<RealiserBE> listes = new List<RealiserBE>();
            //List<AcheterBE> acheters_avant = new List<AcheterBE>();
            //List<PayerBE> payers_avant = new List<PayerBE>();
            //lignes = new List<LigneEtat>();
            //decimal montantEntrees, montantSorties, solde;

            //if (dpiDateFin.SelectedDate != null)
            //    datefin = dpiDateFin.SelectedDate.Value.Date.ToShortDateString();
            //if (dpiDateDebut.SelectedDate != null)
            //    datedebut = dpiDateDebut.SelectedDate.Value.Date.ToShortDateString();
            //if (cmbOperation.SelectedValue != null)
            //    code = cmbOperation.SelectedValue.ToString();
           // OperationBE operation = new OperationBE();
                
            //if (datefin.CompareTo(datedebut) >= 0)
            //{
            //    listes = caisseBL.listerSuivantCritereRealisers("dateop between " + "'" + datedebut + "' AND " + "'" + datefin + "'");
            //    realisers = new List<RealiserBE>();
            //    acheters = new List<AcheterBE>();
            //    payers = new List<PayerBE>();

            //    //if (code == CODE_MIXTE)
            //    //{
            //    //    foreach (RealiserBE r in listes)
            //    //        realisers.Add(r);
            //    //}
            //    //else
            //    //{
            //    //    foreach (RealiserBE r in listes)
            //    //    {
            //    //        operation.codeOp = r.codeop;
            //    //        operation = caisseBL.rechercherOperation(operation);
            //    //        if (operation.codeTypeOp == code)
            //    //            realisers.Add(r);
            //    //    }
            //    //}
            //    //if (code != "sortie")
            //    //{
            //    //    acheters = caisseBL.listerSuivantCritereAcheters("dateachat between " + "'" + datedebut + "' AND " + "'" + datefin + "'");
            //    //    payers = caisseBL.listerSuivantCriterePayers("datepaiement between " + "'" + datedebut + "' AND " + "'" + datefin + "'");
            //    //}

            //    foreach (RealiserBE r in listes)
            //        realisers.Add(r);
            //    acheters = caisseBL.listerSuivantCritereAcheters("dateachat between " + "'" + datedebut + "' AND " + "'" + datefin + "'");
            //    payers = caisseBL.listerSuivantCriterePayers("datepaiement between " + "'" + datedebut + "' AND " + "'" + datefin + "'");
                
            //    lignes = creerDataSource(realisers,acheters,payers);
            //    //grdEtatCaisse.ItemsSource = lignes;
            //    //grdEtatCaisse.Items.Refresh();
            
            //    //calcul du montant des entrees et des sorties
            //    montantSorties = 0;
            //    montantEntrees = 0;
            //    if(listes != null)
            //        foreach (RealiserBE r in listes)
            //        {
            //            //operation.codeOp = r.codeop;
            //            //operation = caisseBL.rechercherOperation(operation);
            //            if (r.codeop == "entree")
            //                montantEntrees += r.montant;
            //            else
            //                montantSorties += r.montant;
            //        }
            //    if (acheters != null)
            //        foreach (AcheterBE a in acheters)
            //            montantEntrees += a.montant;
            //    if (payers != null)
            //        foreach (PayerBE p in payers)
            //            montantEntrees += (decimal)p.montant;

            //    //calcul du solde avant la date de debut
            //    listes = caisseBL.listerSuivantCritereRealisers("dateop < " + "'" + datedebut + "'");
            //    acheters_avant = caisseBL.listerSuivantCritereAcheters("dateachat < " + "'" + datedebut + "'");
            //    payers_avant = caisseBL.listerSuivantCriterePayers("datepaiement < " + "'" + datedebut + "'");
            //    //operation = new OperationBE();
            //    solde = 0;
            //    if (listes != null)
            //        foreach (RealiserBE r in listes)
            //        {
            //            //operation.codeOp = r.codeop;
            //            //operation = caisseBL.rechercherOperation(operation);
            //            if (r.codeop == "entree")
            //                solde += r.montant;
            //            else
            //                solde -= r.montant;
            //        }
            //    if (acheters_avant != null)
            //        foreach (AcheterBE a in acheters_avant)
            //            solde += a.montant;
            //    if (payers_avant != null)
            //        foreach (PayerBE p in payers_avant)
            //            solde += (decimal)p.montant;


            //    listes = caisseBL.listerTousRealiser();
            //    acheters_avant = caisseBL.listerTousAcheter();
            //    payers_avant = caisseBL.listerTousPayer();
            //    //operation = new OperationBE();
            //    soldetotal = 0;
            //    if (listes != null)
            //        foreach (RealiserBE r in listes)
            //        {
            //            //operation.codeOp = r.codeop;
            //            //operation = caisseBL.rechercherOperation(operation);
            //            if (r.codeop == "entree")
            //                soldetotal += r.montant;
            //            else
            //                soldetotal -= r.montant;
            //        }
            //    if (acheters_avant != null)
            //        foreach (AcheterBE a in acheters_avant)
            //            soldetotal += a.montant;
            //    if (payers_avant != null)
            //        foreach (PayerBE p in payers_avant)
            //            soldetotal += (decimal)p.montant;

            //    //txtEntreePeriode.Text = Convert.ToString(montantEntrees); Console.WriteLine(montantEntrees.ToString("#,#", CultureInfo.InvariantCulture));
            //    //txtSortiePeriode.Text = Convert.ToString(montantSorties); Console.WriteLine(montantSorties.ToString("#,#", CultureInfo.InvariantCulture));
            //    //txtSoldePeriode.Text = Convert.ToString(montantEntrees - montantSorties);
            //    //txtAncienSolde.Text = Convert.ToString(solde);
            //    //txtSoldeTotal.Text = Convert.ToString(soldetotal);

            //    //string motif = txtMotif.Text;
            //    decimal entree = 0, sortie = 0;
            //    List<LigneEtat> tampon = new List<LigneEtat>();
            //    foreach (LigneEtat l in lignes)
            //    {
            //        //if (motif == "" || l.motif.ToUpper().Contains(motif.ToUpper()))
            //        //    tampon.Add(l);
            //    }

            //    lignes = new List<LigneEtat>();
            //    foreach (LigneEtat l in tampon)
            //    {
            //        lignes.Add(l);
            //        if (l.type.ToLower() == "entree")
            //            entree += l.montant;
            //        else
            //            sortie += l.montant;
            //    }

            //    //txtTotalEntree.Text = Convert.ToString(entree);
            //    //txtTotalSolde.Text = Convert.ToString(entree - sortie);
            //    //txtTotalSortie.Text = Convert.ToString(sortie);
            //}
            //else
            //    MessageBox.Show("La date de debut doit être plus petite ou égale à la date de fin");
        }

        private List<LigneEtat> creerDataSource(List<RealiserBE> liste1,List<AcheterBE> liste2, List<PayerBE> liste3)
        {
            List<LigneEtat> listes = new List<LigneEtat>();

            if (liste1 != null & liste1.Count > 0)
            {
                foreach (RealiserBE r in liste1)
                {
                    listes.Add(new LigneEtat(r.codeop, r.motif, r.dateop.ToShortDateString(), (decimal)r.montant, r.concerne));
                }
            }
            if (liste2 != null & liste2.Count > 0)
            {
                foreach (AcheterBE a in liste2)
                {
                    listes.Add(new LigneEtat("Entree", "Vente " + caisseBL.obtenirNomSetArticle(a.codesetarticle), a.datAchat.ToShortDateString(), a.montant, caisseBL.obtenirNomEleve(a.matricule)));
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
            List<LigneEtat> tampon = new List<LigneEtat>();
           // string motif = txtMotif.Text;
            decimal entree = 0, sortie = 0;

            //on charge d'abord la grille et puis on tri sur les elements affiches
            chargerDataGrid();
            foreach (LigneEtat l in lignes)
            {
                //if (motif == "" || l.motif.ToUpper().Contains(motif.ToUpper()))
                //    tampon.Add(l);
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

            //grdEtatCaisse.ItemsSource = lignes;
            //grdEtatCaisse.Items.Refresh();
            //txtTotalEntree.Text = Convert.ToString(entree);
            //txtTotalSolde.Text = Convert.ToString(entree - sortie);
            //txtTotalSortie.Text = Convert.ToString(sortie);
        }

        private void txtAnnee_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtAnnee.Text != "")
                txtAnneeScolaire.Text = ((Convert.ToInt32(txtAnnee.Text.ToString())) - 1).ToString() + "/" + txtAnnee.Text;
            chargerDataGrid();
        }

        private void cmdFermer_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cmbClasse_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //on charge la grille
            //chargerDataGrid();
        }

        private void dpiDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            chargerDataGrid();
        }

        private void cmbObservation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           // chargerDataGrid();
        }

        private void cmdAfficher_Click(object sender, RoutedEventArgs e)
        {
            chargerDataGrid();
        }

        private void cmbClasse_DropDownClosed(object sender, EventArgs e)
        {
            chargerDataGrid();
        }

        private void cmbObservation_DropDownClosed(object sender, EventArgs e)
        {
            chargerDataGrid();
        }

        private void cmdImprimer_Click(object sender, RoutedEventArgs e)
        {
            if (grdInsolvable.Items.Count == 0)

                MessageBox.Show("Aucun contenu n'est disponible pour impréssion", "School Brain", MessageBoxButton.OK, MessageBoxImage.Error);

            else
            {
                CreerEtat etat = new CreerEtat("Insolvable" + DateTime.Today.ToString("dd-MM-yyyy"), "Situation financière des élèves au ");
                etat.etatInsolvable(grdInsolvable, cmbClasse.Text.ToString(), Convert.ToInt32(txtAnnee.Text.ToString()), Convert.ToDateTime(dpiDate.Text).ToString("dd-MM-yyyy"),cmbObservation.Text.ToString());
                JournalBE ligneJournal = new JournalBE("ecole", "Impréssion de la liste des insolvables de la " + cmbClasse.Text.ToString(), DateTime.Today.Date.ToString("yyyy-MM-dd"), DateTime.Now.ToLongTimeString());
                journalBL.ajouterJournal(ligneJournal);
            }
        }

        private void txtAnnee_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Utilitaires.IsTextAllowed(e.Text);
        }

             

    }
}
