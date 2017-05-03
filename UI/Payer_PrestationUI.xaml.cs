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
using System.Text.RegularExpressions;
using Ecole.UI;
using Ecole.Utilitaire;
using System.Globalization;
using System.Threading;

namespace Ecole.UI
{
    /// <summary>
    /// Interaction logic for Payer_Prestation.xaml
    /// </summary>
    public partial class Payer_Prestation : Window
    {
        private static string PRESTATION_DEPOT = "Depot";
        private static string TRANCHE_COMPLET = "Complet";
        private static string TRANCHE_INCOMPLET = "Incomplet";
        //public static string INSCRIPTION = "ins";
        private const string BOURSE = "Montant";
        private const string REMISE = "Pourcentage";
        private double totalAPayer;
        private double totalVerse;
        private double resteAPayer;
        private string matricule;
        private string tranche;
        private string login;
        private string categorie;
        private int annee;
        private double montant;
        private PayerBE payer;
        private List<LignePrestation> lignes;
        private GestionPrestationBL prestationBL;
        private List<MontantTrancheBE> montanttranches;
        private List<PayerBE> payers;
        private List<string> listprestation;
        private List<string> listtranches;
        private List<string> classes;
        private List<string> eleves;
        private List<string> choixDispense;
        //private decimal fraisInscription;
        EleveBE eleve;
        decimal fraisPrestation;
        PrestationBE prestation;

        public Payer_Prestation()
        {
            InitializeComponent();
            dpiDateOp.SelectedDate = DateTime.Today;
            dpiDateOp.IsTodayHighlighted = true;
            dpiDateOp.Text = DateTime.Now.ToString();

            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";
            Thread.CurrentThread.CurrentCulture = ci;

            prestationBL = new GestionPrestationBL();
            montanttranches = new List<MontantTrancheBE>();
            listprestation = new List<string>();
            listtranches = new List<string>();
            choixDispense = new List<string>(){BOURSE,REMISE};
            classes = new List<string>();
            eleves = new List<string>();
            payers = new List<PayerBE>();
            lignes = new List<LignePrestation>();
            eleve = new EleveBE();
            payer = new PayerBE();
            prestation = new PrestationBE();
           // fraisInscription = 0;
            fraisPrestation = 0;
            totalAPayer = 0;

            classes = prestationBL.listerValeursColonneClasse("codeclasse");
            cmbClasse.ItemsSource = classes;
            cmbDispense.ItemsSource = choixDispense;
            annee = prestationBL.AnneeEnCours();
            listtranches = prestationBL.listerValeursColonneTranche("codetranche");
            cmbTranche.ItemsSource = listtranches;
            txtAnnee.Text = " / " + annee;
            txtAnneeScolaire.Text = (annee - 1).ToString();
            txtTotal.IsEnabled = false;
            txtTotalVerse.IsEnabled = false;
            txtResteAPayer.IsEnabled = false;
            txtReste.IsEnabled = false;
            txtRemise.IsEnabled = false;
            cmbDispense.IsEnabled = false;
            txtMontant.IsEnabled = false;
        }

        private void txtAnneeScolaire_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                annee = Convert.ToInt32(txtAnneeScolaire.Text) + 1;
                txtAnnee.Text = " / " + annee.ToString();
                if (txtMatricule.Text != "")
                    statutEleve();
            }
            catch (Exception)
            {
                MessageBox.Show("L'annee doit etre un nombre positif", "School brain:Alerte");
            }
        }

        private void cmbPrestation_DropDownClosed(object sender, EventArgs e)
        {
            prestation = new PrestationBE();
            txtMontant.Text = "";
            lblNomPrestation.Content = "";
            if (cmbPrestation.SelectedValue != null && cmbPrestation.SelectedValue.ToString() != "" && txtAnneeScolaire.Text != null)
            {
                fraisPrestation = prestationBL.obtenirMontantPrestation(cmbPrestation.SelectedValue.ToString(), categorie, annee);
                if (cmbPrestation.SelectedValue.ToString() != PRESTATION_DEPOT)
                {
                    prestation.codePrestation = cmbPrestation.SelectedValue.ToString();
                    prestation = prestationBL.rechercherPrestation(prestation);
                    lblNomPrestation.Content = prestation.nomPrestation;
                    txtMontant.Text = fraisPrestation.ToString();
                    txtMontant.IsEnabled = false;
                    cmbTranche.IsEnabled = true;
                    checkRemise.IsEnabled = true;
                }
                else
                {
                    cmbTranche.Text = "";
                    cmbTranche.IsEnabled = false;
                    //checkRemise.IsEnabled = false;
                    txtMontant.IsEnabled = true;
                }
            }
        }

        private void cmbTranche_DropDownClosed(object sender, EventArgs e)
        {
            montantTrancheBE();
        }

        private void cmdValider_new_Click(object sender, RoutedEventArgs e)
        {
                if (cmbPrestation.SelectedValue == null || cmbPrestation.Text == "")
                {
                    if (checkRemise.IsChecked == true)
                    {
                        switch (cmbDispense.Text)
                        {
                            case REMISE:
                                MessageBox.Show("La réduction s'effectue sur une prestation, choisissez d'abord la prestation","school brain:Alerte",MessageBoxButton.OK,MessageBoxImage.Information);
                                break;
                            case BOURSE:
                                gestionDispense();
                                break;
                        }
                    }
                }
                else
                {
                    if (txtMontant.Text != "")
                    {
                        string codeprestation = cmbPrestation.SelectedValue.ToString();
                        double remise = 0;
                        List<MontantTrancheBE> tranches = new List<MontantTrancheBE>();
                        montant = Convert.ToDouble(txtMontant.Text);
                        matricule = txtMatricule.Text.ToString();
                        codeprestation = cmbPrestation.SelectedValue.ToString();
                        login = Ecole.UI.ConnexionUI.utilisateur.login;
                        tranches = prestationBL.listerSuivantCritereMontanttranches("codeprestation = " + "'" + codeprestation + "' and codecateleve = " + "'" + categorie + "' and annee = " + "'" + annee + "'");
                        PayerBE payerBE;
                        double frais;
                        CreerEtat etat = null;
                        TrancheBE trancheBE;
                        switch (codeprestation)
                        {
                            #region gestion de cas d'un depot
                            case "Depot":
                                if (checkRemise.IsChecked == true)
                                {
                                    if (cmbDispense.Text == BOURSE)
                                    {
                                        gestionDispense();
                                    }
                                }

                                // on recherche dans la bd à la fois les tranches non complètement payées et les tranches qui n'ont pas encore commencé à être payées
                                bool b = true;
                                List<PayerBE> listes = prestationBL.listerTrancheNonPayees(categorie, matricule, annee);
                                MontantTrancheBE t = new MontantTrancheBE();
                                string numerorecu = "";

                                if (listes != null)
                                {
                                    PayerBE p = new PayerBE();

                                    foreach (PayerBE pa in listes)
                                    {
                                        if (b == true)
                                        {
                                            t.codeCatEleve = categorie;
                                            t.codePrestation = pa.codePrestation;
                                            t.codeTranche = pa.codeTranche;
                                            t.annee = pa.annee;
                                            t = prestationBL.rechercherMontantTranche(t);
                                            if (montant >= (t.montant - pa.montant - Convert.ToDouble(pa.remise)))
                                            {
                                                payerBE = new PayerBE(pa.matricule, login, pa.codePrestation, pa.codeTranche, t.montant - Convert.ToDouble(pa.remise),
                                                    dpiDateOp.SelectedDate.Value, annee, TRANCHE_COMPLET, pa.remise);
                                                p = new PayerBE(pa.matricule, login, pa.codePrestation, pa.codeTranche, t.montant - pa.montant - Convert.ToDouble(pa.remise),
                                                    dpiDateOp.SelectedDate.Value, annee, TRANCHE_COMPLET, pa.remise);
                                                if (prestationBL.modifierPayer(payerBE, p))
                                                {
                                                    payers.Add(payerBE);
                                                    numerorecu = prestationBL.rechercherNumeroPayer(payerBE);
                                                }
                                                montant -= (t.montant - pa.montant - Convert.ToDouble(pa.remise));
                                            }
                                            else
                                            {
                                                if (montant > 0)
                                                {
                                                    payerBE = new PayerBE(pa.matricule, login, pa.codePrestation, pa.codeTranche, (pa.montant + montant), dpiDateOp.SelectedDate.Value, annee, TRANCHE_INCOMPLET, pa.remise);
                                                    p = new PayerBE(pa.matricule, login, pa.codePrestation, pa.codeTranche, montant, dpiDateOp.SelectedDate.Value, annee, TRANCHE_INCOMPLET, pa.remise);
                                                
                                                    if (prestationBL.modifierPayer(payerBE, p))
                                                    {
                                                        payers.Add(payerBE);
                                                        if (numerorecu == "")
                                                            numerorecu = prestationBL.rechercherNumeroPayer(payerBE);
                                                    }
                                                }
                                                montant = 0;
                                                b = false;
                                            }
                                        }
                                    }

                                    //enregistrement du versement
                                    //prestationBL.enregistrer_versement(matricule, "Opération de dépot d'argent pour les frais de : " + cmbPrestation.Text, (Convert.ToDouble(txtMontant.Text) - montant ),
                                    //    dpiDateOp.SelectedDate.Value, annee);

                                }
                                lignes = chargerGrid();
                                etat = new CreerEtat("Paiement_prestation" + matricule + DateTime.Today.ToShortDateString(), "Reçu de versement N° " + numerorecu + "\nDate " + dpiDateOp.SelectedDate.Value);
                                etat.factureDepot(Convert.ToDouble(txtMontant.Text), montant, totalAPayer, totalVerse, resteAPayer, eleve, cmbClasse.Text);
                                initialiser();
                                break;
                            #endregion

                            #region gestion d'un paiement normal d'une tranche
                            default:
                                if (checkRemise.IsChecked == true)
                                {
                                    switch (cmbDispense.Text)
                                    {
                                        case REMISE:
                                            gestionRemise();
                                            break;
                                        case BOURSE:
                                            gestionDispense();
                                            break;
                                    }
                                }
                                else
                                {
                                    if (cmbTranche.SelectedValue != null)
                                    {
                                        payerBE = new PayerBE(matricule, login, codeprestation, tranche, montant, dpiDateOp.SelectedDate.Value, annee, TRANCHE_COMPLET, 0);
                                        if (prestationBL.rechercherPayer(payerBE) == null)
                                        {
                                            if (prestationBL.ajouterAcheterPayer(payerBE))
                                            {
                                                trancheBE = new TrancheBE();
                                                trancheBE.codetranche = payerBE.codeTranche;
                                                trancheBE = prestationBL.rechercherTranche(trancheBE);
                                                etat = new CreerEtat("Paiement_prestation" + matricule + DateTime.Today.ToShortDateString(), "Reçu de paiement N° " + prestationBL.rechercherNumeroPayer(payerBE) + "\nDate " + dpiDateOp.SelectedDate.Value);
                                                etat.facturePrestation(payerBE, eleve, cmbClasse.Text, prestation.nomPrestation, 0, trancheBE.nomtranche);
                                                montant -= montant;

                                                //enregistrement du versement
                                                //prestationBL.enregistrer_versement(matricule, "Opération de dépot d'argent pour les frais de : " + cmbPrestation.Text, Convert.ToDouble(txtMontant.Text),
                                                //    dpiDateOp.SelectedDate.Value, annee);

                                            }
                                            else
                                                MessageBox.Show("Enregistrement echoue");
                                        }
                                        else
                                            MessageBox.Show("Cette tranche a déjà été payée pour cet étudiant, pour modifier cliquer sur la ligne correspondante dans la grille du bas");
                                    }
                                    else
                                    {
                                        double m = montant;
                                        numerorecu = "";
                                        foreach (MontantTrancheBE mt in tranches)
                                        {
                                            payerBE = new PayerBE(matricule, Ecole.UI.ConnexionUI.utilisateur.login, codeprestation, mt.codeTranche, mt.montant, dpiDateOp.SelectedDate.Value, annee, TRANCHE_COMPLET, Convert.ToDecimal(remise));
                                            if (prestationBL.ajouterAcheterPayer(payerBE))
                                            {
                                                if (numerorecu == "")
                                                    numerorecu = prestationBL.rechercherNumeroPayer(payerBE);
                                                montant -= mt.montant;
                                            }
                                            else
                                                MessageBox.Show("Enregistrement echoue");
                                        }

                                        //enregistrement du versement
                                        //prestationBL.enregistrer_versement(matricule, "Opération de dépot d'argent pour les frais de : " + cmbPrestation.Text, Convert.ToDouble(txtMontant.Text),
                                        //    dpiDateOp.SelectedDate.Value, annee);

                                        etat = new CreerEtat("Paiement_prestation" + matricule + DateTime.Today.ToShortDateString(), "Reçu de paiement N° " + numerorecu + "\nDate " + dpiDateOp.SelectedDate.Value);
                                        etat.facturePrestation(prestation, eleve, cmbClasse.Text, m, remise);
                                    }
                                }
                                initialiser();
                                break;
                            #endregion
                        }
                    }else
                        MessageBox.Show("Veuillez renseigner le montant!", "school brain:Alerte", MessageBoxButton.OK, MessageBoxImage.Information);
                }

            //enregistrement du versement
                prestationBL.enregistrer_versement(matricule, "Opération de dépot d'argent pour les frais de : " + cmbPrestation.Text, (Convert.ToDouble(txtMontant.Text) - montant),
                    dpiDateOp.SelectedDate.Value, annee);

            txtReste.Text = montant.ToString();
            lignes.Clear();
            lignes = chargerGrid();
            grdStatus.ItemsSource = lignes;
            grdStatus.Items.Refresh();

            txtMontant.Clear();
            cmbTranche.Text = "";
        }

        private void gestionDispense()
        {
            double montantDispense = Convert.ToDouble(txtRemise.Text);
            matricule = txtMatricule.Text.ToString();
            login = Ecole.UI.ConnexionUI.utilisateur.login;
            PayerBE payerBE;

            bool b = true;
            // on recherche dans la bd à la fois les tranches non complètement payées et les tranches qui n'ont pas encore commencé à être payées
            List<PayerBE> listes = prestationBL.listerTrancheNonPayees(categorie, matricule, annee);
            MontantTrancheBE t = new MontantTrancheBE();
            if (listes != null)
            {
                PayerBE p = new PayerBE();

                foreach (PayerBE pa in listes)
                {
                    if (b == true)
                    {
                        t.codeCatEleve = categorie;
                        t.codePrestation = pa.codePrestation;
                        t.codeTranche = pa.codeTranche;
                        t.annee = pa.annee;
                        t = prestationBL.rechercherMontantTranche(t);
                        if (montantDispense >= (t.montant - pa.montant - Convert.ToDouble(pa.remise)))
                        {
                            payerBE = new PayerBE(pa.matricule, login, pa.codePrestation, pa.codeTranche, pa.montant,
                                dpiDateOp.SelectedDate.Value, annee, TRANCHE_COMPLET, Convert.ToDecimal(t.montant - pa.montant));
                            p = new PayerBE(pa.matricule, login, pa.codePrestation, pa.codeTranche, 0,
                                dpiDateOp.SelectedDate.Value, annee, TRANCHE_COMPLET, Convert.ToDecimal(t.montant - pa.montant - (double)pa.remise));
                            if (prestationBL.modifierPayer(payerBE, p))
                            {
                                payers.Add(payerBE);
                            }
                            montantDispense -= (t.montant - pa.montant - Convert.ToDouble(pa.remise));
                        }
                        else
                        {
                            if (montantDispense > 0)
                            {
                                payerBE = new PayerBE(pa.matricule, login, pa.codePrestation, pa.codeTranche, pa.montant, dpiDateOp.SelectedDate.Value, annee, TRANCHE_INCOMPLET, pa.remise + Convert.ToDecimal(montantDispense));
                                p = new PayerBE(pa.matricule, login, pa.codePrestation, pa.codeTranche, 0, dpiDateOp.SelectedDate.Value, annee, TRANCHE_INCOMPLET, Convert.ToDecimal(montantDispense));
                                if (prestationBL.modifierPayer(payerBE, p))
                                {
                                    payers.Add(payerBE);
                                }
                            }
                            montantDispense = 0;
                            b = false;
                        }
                    }
                }
            }
        }

        private void gestionRemise()
        {
            decimal pourcentageRemise = Convert.ToDecimal(txtRemise.Text);
            string codeprestation = cmbPrestation.SelectedValue.ToString();
            double remise = 0;
            List<MontantTrancheBE> tranches = new List<MontantTrancheBE>();
            montant = Convert.ToDouble(txtMontant.Text);
            matricule = txtMatricule.Text.ToString();
            codeprestation = cmbPrestation.SelectedValue.ToString();
            login = Ecole.UI.ConnexionUI.utilisateur.login;
            tranches = prestationBL.listerSuivantCritereMontanttranches("codeprestation = " + "'" + codeprestation + "' and codecateleve = " + "'" + categorie + "' and annee = " + "'" + annee + "'");
            PayerBE payerBE;
            double frais;
            CreerEtat etat = null;
            TrancheBE trancheBE;

            if (cmbTranche.SelectedValue != null)
            {
                foreach (MontantTrancheBE mt in tranches)
                {
                    if (mt.codeTranche != tranche)
                    {
                        remise = (mt.montant * Convert.ToDouble(pourcentageRemise)) / 100;
                        payerBE = new PayerBE(matricule, Ecole.UI.ConnexionUI.utilisateur.login, codeprestation, mt.codeTranche, 0, dpiDateOp.SelectedDate.Value, annee, TRANCHE_INCOMPLET, Convert.ToDecimal(remise));
                        if (prestationBL.ajouterAcheterPayer(payerBE))
                        {
                        }
                        else
                            MessageBox.Show("Enregistrement echoue");
                    }
                    else
                    {
                        remise = (montant * Convert.ToDouble(pourcentageRemise)) / 100;
                        frais = montant - remise;
                        payerBE = new PayerBE(matricule, login, codeprestation, tranche, frais, dpiDateOp.SelectedDate.Value, annee, TRANCHE_COMPLET, Convert.ToDecimal(remise));
                        if (prestationBL.rechercherPayer(payerBE) == null)
                        {
                            if (prestationBL.ajouterAcheterPayer(payerBE))
                            {
                                trancheBE = new TrancheBE();
                                trancheBE.codetranche = payerBE.codeTranche;
                                trancheBE = prestationBL.rechercherTranche(trancheBE);
                                etat = new CreerEtat("Paiement_prestation" + matricule + DateTime.Today.ToShortDateString(), "Reçu de paiement N° " + prestationBL.rechercherNumeroPayer(payerBE) + "\nDate " + dpiDateOp.SelectedDate.Value);
                                etat.facturePrestation(payerBE, eleve, cmbClasse.Text, prestation.nomPrestation, remise, trancheBE.nomtranche);
                                montant -= frais;
                            }
                            else
                                MessageBox.Show("Enregistrement echoue");
                        }
                        else
                            MessageBox.Show("Cette tranche a déjà été payée pour cet étudiant, faire un dépot pour compléter le paiement si elle est incomplète");
                    }
                }
            }
            else
            {
                double montantPrestation = montant;
                foreach (MontantTrancheBE mt in tranches)
                {
                    remise = (mt.montant * Convert.ToDouble(pourcentageRemise)) / 100;
                    payerBE = new PayerBE(matricule, Ecole.UI.ConnexionUI.utilisateur.login, codeprestation, mt.codeTranche, mt.montant - remise, dpiDateOp.SelectedDate.Value, annee, TRANCHE_COMPLET, Convert.ToDecimal(remise));
                    if (prestationBL.ajouterAcheterPayer(payerBE))
                    {
                        montant -= mt.montant - remise;
                    }
                    else
                        MessageBox.Show("Enregistrement echoue");
                }
                remise = (montantPrestation * Convert.ToDouble(pourcentageRemise)) / 100;
                etat = new CreerEtat("Paiement_prestation" + matricule + DateTime.Today.ToShortDateString(), "Reçu de paiement " + "\nDate " + dpiDateOp.SelectedDate.Value);
                etat.facturePrestation(prestation, eleve, cmbClasse.Text, montantPrestation, remise);
            }
        }

        private void cmdAnnuler_Click(object sender, RoutedEventArgs e)
        {
            viderLeFormulaire();
        }

        private void viderLeFormulaire()
        {
            txtMatricule.Clear();
            cmbEleve.Text = "";
            lblNom.Content = "";
            txtMontant.Clear();
            cmbPrestation.Text = "";
            fraisPrestation = 0;
            cmbTranche.Text = "";
            lblNomPrestation.Content = "";
            payers.Clear();
            grdStatus.ItemsSource = payers;
            grdStatus.Items.Refresh();
            txtTotal.Text = "";
            txtTotalVerse.Text = "";
            txtResteAPayer.Text = "";
            txtReste.Text = "";
            txtRemise.Text = "";
            cmbDispense.Text = "";
            checkRemise.IsChecked = false;
            cmbTranche.IsEnabled = true;
            listprestation.Clear();
            cmbPrestation.ItemsSource = listprestation;
            cmbPrestation.Items.Refresh();
            txtMontant.IsEnabled = false;
        }
        private void cmdQuitter_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void grdStatus_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {

                if (MessageBox.Show("Voulez vous supprimer?", "School : Confirmation", System.Windows.MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    if (grdStatus.SelectedIndex != -1)
                    {
                        PayerBE payer = new PayerBE();
                        payer = payers.ElementAt(grdStatus.SelectedIndex);
                        payers.Remove(payer);
                        prestationBL.supprimerPayer(payer);
                        lignes = chargerGrid();
                        grdStatus.ItemsSource = lignes;
                        grdStatus.Items.Refresh();
                    }
                    else
                        MessageBox.Show("Aucune ligne sélectionnée", "School brain:Alerte");
                }
            }
        }

        private void cmbImprimer_Click(object sender, RoutedEventArgs e)
        {
            if (lignes != null)
            {
                List<PrestationBE> prestations = new List<PrestationBE>();
                prestations = prestationBL.listerTousPrestation();
                CategorieEleveBE cat = new CategorieEleveBE();
                cat.codeCatEleve = categorie;
                cat = prestationBL.rechercherCategorieEleve(cat);
                CreerEtat creerEtat = new CreerEtat("statut financier" + DateTime.Today.ToShortDateString(), "Situation financière de l'élève");
                prestationBL.journaliser("Impression de l'état financier de " + eleve.matricule);
                creerEtat.statutFinancier(grdStatus, eleve, categorie + " - " + cat.nomCatEleve, totalAPayer, totalVerse, resteAPayer, prestations);
            }
        }

        private bool validerFormulaire()
        {
            bool b = true;

            if (txtMatricule.Text == null || cmbPrestation.SelectedValue == null || cmbTranche.SelectedValue == null)
                b = false;
            else
                if (txtMatricule.Text.ToString() == "" || cmbPrestation.SelectedValue.ToString() == "" || cmbTranche.SelectedValue.ToString() == "")
                    b = false;
                else
                    b = true;
            return b;
        }

        private void montantTrancheBE()
        {
            if (cmbTranche.SelectedValue != null & cmbPrestation.SelectedValue != null & txtMatricule.Text != null)
            {
                tranche = cmbTranche.SelectedValue.ToString();
                MontantTrancheBE mt = new MontantTrancheBE();
                mt.annee = annee;
                mt.codeCatEleve = categorie;
                mt.codePrestation = cmbPrestation.SelectedValue.ToString();
                mt.codeTranche = cmbTranche.SelectedValue.ToString();

                mt = prestationBL.rechercherMontantTranche(mt);
                if (mt != null)
                {
                    txtMontant.Text = Convert.ToString(mt.montant);
                    txtMontant.IsEnabled = false;
                }
                else
                {
                    MessageBox.Show("Cette tranche n'est pas definie pour cette categorie");
                    txtMontant.Text = "";
                    cmbTranche.Text = "";
                }
            }
            else
                txtMontant.IsEnabled = true;
        }

        private List<LignePrestation> chargerGrid()
        {
            AppartenirBE app = new AppartenirBE();
            MontantTrancheBE mt = new MontantTrancheBE();
            List<LignePrestation> liste = new List<LignePrestation>();
            List<MontantTrancheBE> alltranches = new List<MontantTrancheBE>();
            totalAPayer = 0;
            totalVerse = 0;
            CultureInfo elGR = CultureInfo.CreateSpecificCulture("el-GR");

            alltranches = prestationBL.listerSuivantCritereMontanttranches("codecateleve = " + "'" + categorie + "' and annee = " + "'" + annee + "'");
            foreach (MontantTrancheBE m in alltranches)
            {
                totalAPayer += m.montant;
            }
            //totalAPayer += (double)fraisInscription;

            payers = prestationBL.listerSuivantCriterePayer("matricule = " + "'" + matricule + "' and annee =" + "'" + annee + "'");
            foreach (PayerBE payer in payers)
            {
                app = new AppartenirBE();
                app.matricule = payer.matricule;
                app.annee = payer.annee;
                app = prestationBL.rechercherCategorie(app);

                mt = new MontantTrancheBE();
                mt.annee = payer.annee;
                mt.codeCatEleve = app.codeCatEleve;
                mt.codePrestation = payer.codePrestation;
                mt.codeTranche = payer.codeTranche;
                mt = prestationBL.rechercherMontantTranche(mt);

                if (payer.observation == TRANCHE_INCOMPLET)
                    liste.Add(new LignePrestation(payer.codePrestation, payer.codeTranche, (decimal)payer.montant, payer.observation, (decimal)(mt.montant - payer.montant - Convert.ToDouble(payer.remise)),
                        payer.remise, payer.datePaiement.ToShortDateString()));
                else
                    liste.Add(new LignePrestation(payer.codePrestation, payer.codeTranche, (decimal)payer.montant, payer.observation, 0,
                        payer.remise, payer.datePaiement.ToShortDateString()));
                totalVerse += payer.montant;
                totalAPayer -= Convert.ToDouble(payer.remise);
            }

            resteAPayer = totalAPayer - totalVerse;

            txtTotal.Text = totalAPayer.ToString("0,0", elGR);
            txtTotalVerse.Text = totalVerse.ToString("0,0", elGR);
            txtResteAPayer.Text = (totalAPayer - totalVerse).ToString("0,0", elGR);

            //txtReste.Text = "0";

            return liste;
        }

        private void txtMontant_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Utilitaires.IsTextAllowed(e.Text);
        }

        private void checkRemise_Click(object sender, RoutedEventArgs e)
        {
            if (checkRemise.IsChecked == true)
            {
                txtRemise.IsEnabled = true;
                cmbDispense.IsEnabled = true;
            }
            else
            {
                txtRemise.IsEnabled = false;
                cmbDispense.IsEnabled = false;
            }
            txtRemise.Clear();
        }

        private void initialiser()
        {
            cmbPrestation.Text = "";
            checkRemise.IsChecked = false;
            txtRemise.Text = "";
            cmbDispense.Text = "";
            cmbTranche.Text = "";
        }

        private void statutEleve()
        {
            txtReste.Text = "";
            CategorieEleveBE c = new CategorieEleveBE();

            eleve = new EleveBE();
            eleve.matricule = txtMatricule.Text;
            eleve = prestationBL.rechercherEleve(eleve);
            if (eleve != null)
            {
                matricule = eleve.matricule;
                //recherche de sa classe
                InscrireBE inscrire = new InscrireBE();
                inscrire.matricule = eleve.matricule;
                inscrire.annee = annee;
                inscrire = prestationBL.rechercherInscrire(inscrire);
                if (inscrire != null)
                {
                    cmbClasse.Text = inscrire.codeClasse;
                }

                //chargement de ses camarades dans la liste des eleves
                eleves = new List<string>();
                string codeclasse = cmbClasse.Text;
                List<EleveBE> listeleves = new List<EleveBE>();
                listeleves = prestationBL.listerElevesDuneClasse(codeclasse, annee);
                if (listeleves != null)
                {
                    foreach (EleveBE el in listeleves)
                    {
                        eleves.Add(el.matricule + " - " + el.nom);
                    }
                }
                cmbEleve.ItemsSource = eleves;
                cmbEleve.Items.Refresh();
                cmbEleve.Text = eleve.matricule + " - " + eleve.nom;

                //recherche de sa categorie dans la table appartenir
                AppartenirBE appartenir = new AppartenirBE();
                appartenir.matricule = txtMatricule.Text;
                appartenir.annee = prestationBL.AnneeEnCours();
                appartenir = prestationBL.rechercherCategorie(appartenir);
                categorie = appartenir.codeCatEleve;
                c.codeCatEleve = categorie;
                c = prestationBL.rechercherCategorieEleve(c);
                lblNom.Content = "Catégorie : "+ c.codeCatEleve + " " + c.nomCatEleve;
                cmbEleve.Text = eleve.matricule +" - "+ eleve.nom;

                payers = prestationBL.listerSuivantCriterePayer("matricule = " + "'" + eleve.matricule + "' and annee =" + "'" + annee + "'");
                //fraisInscription = prestationBL.obtenirFraisInscription(eleve);
                lignes = new List<LignePrestation>();
                lignes = chargerGrid();
                grdStatus.ItemsSource = lignes;
                grdStatus.Items.Refresh();
                listprestation.Clear();
                List<MontantTrancheBE> mtranches = new List<MontantTrancheBE>();
                mtranches = prestationBL.listerSuivantCritereMontanttranches("codecateleve = " + "'" + categorie + "' and annee =" + "'" + annee + "' ");
                if (mtranches != null)
                    foreach (MontantTrancheBE mt in mtranches)
                        if (!listprestation.Contains(mt.codePrestation))
                            listprestation.Add(mt.codePrestation);
                listprestation.Add(PRESTATION_DEPOT);
                cmbPrestation.ItemsSource = listprestation;
                cmbPrestation.Items.Refresh();
                montantTrancheBE();
            }
            else
                MessageBox.Show("L'élève n'existe pas");
        }

        private void txtAnneeScolaire_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Utilitaires.IsTextAllowed(e.Text);
        }

        private void txtMatricule_KeyUp(object sender, KeyEventArgs e)
        {
            txtReste.Text = "";
            CategorieEleveBE c = new CategorieEleveBE();

            if (e.Key == Key.Return)
            {
                statutEleve();
            }
        }

        private void txtMatricule_LostFocus(object sender, RoutedEventArgs e)
        {
            statutEleve();
        }

        private void cmbClasse_DropDownClosed(object sender, EventArgs e)
        {
            if (cmbClasse.Text != null && cmbClasse.Text != "")
            {
                //recherche des eleves inscrits  dans cette classe pour charger le combobox des eleves
                viderLeFormulaire();
                eleves = new List<string>();
                string codeclasse = cmbClasse.Text;
                List<EleveBE> listeleves = new List<EleveBE>();
                listeleves = prestationBL.listerElevesDuneClasse(codeclasse, annee);
                if (listeleves != null)
                {
                    foreach (EleveBE el in listeleves)
                    {
                        eleves.Add(el.matricule + " - " + el.nom);
                    }
                }
                cmbEleve.ItemsSource = eleves;
            }
        }

        private void cmbEleve_DropDownClosed(object sender, EventArgs e)
        {
            if (cmbEleve.Text != null && cmbEleve.Text != "")
            {
                string nommat = cmbEleve.Text;
                txtMatricule.Text = nommat.Split('-')[0].Trim();
                statutEleve();
            }
        }

    }
}
