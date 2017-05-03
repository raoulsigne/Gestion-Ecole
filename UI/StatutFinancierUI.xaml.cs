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
using Ecole.ClasseConception;
using Ecole.BusinessLogic;
using Ecole.BusinessEntity;
using System.Globalization;
using Ecole.Utilitaire;
using System.Threading;

namespace Ecole.UI
{
    /// <summary>
    /// Interaction logic for StatutFinancierUI.xaml
    /// </summary>
    public partial class StatutFinancierUI : Window
    {
        private static string TRANCHE_INCOMPLET = "Incomplet";
        //public static string INSCRIPTION = "ins";
        private List<LignePrestation> lignes;
        private GestionPrestationBL prestationBL;
        private List<MontantTrancheBE> montanttranches;
        private List<PayerBE> payers;
        private double totalAPayer;
        private double totalVerse;
        private double totalRemise;
        private double resteAPayer;
        private string matricule;
        //decimal fraisPrestation;
        private int annee;
        EleveBE eleve;
        private string categorie;
        private decimal fraisInscription;
        private List<string> eleves;
        private List<string> classes;

        public StatutFinancierUI()
        {
            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";
            Thread.CurrentThread.CurrentCulture = ci;

            InitializeComponent();

            prestationBL = new GestionPrestationBL();
            montanttranches = new List<MontantTrancheBE>();
            payers = new List<PayerBE>();
            lignes = new List<LignePrestation>();
            eleves = new List<string>();
            classes = new List<string>();
            eleve = new EleveBE();
            fraisInscription = 0;
            //fraisPrestation = 0;
            totalAPayer = 0;

            classes = prestationBL.listerValeursColonneClasse("codeclasse");
            cmbClasse.ItemsSource = classes;
            annee = prestationBL.AnneeEnCours();
            txtAnnee.Text = " / " + annee.ToString();
            txtAnneeScolaire.Text = (annee - 1).ToString();
            txtTotal.IsEnabled = false;
            txtTotalVerse.IsEnabled = false;
            txtResteAPayer.IsEnabled = false;
        }

        private List<LignePrestation> chargerGrid()
        {
            AppartenirBE app = new AppartenirBE();
            MontantTrancheBE mt = new MontantTrancheBE();
            List<LignePrestation> liste = new List<LignePrestation>();
            List<MontantTrancheBE> alltranches = new List<MontantTrancheBE>();
            totalAPayer = 0;
            totalVerse = 0;
            totalRemise = 0;
            CultureInfo elGR = CultureInfo.CreateSpecificCulture("el-GR");

            alltranches = prestationBL.listerSuivantCritereMontanttranches("codecateleve = " + "'" + categorie + "' and annee = " + "'" + annee + "'");
            foreach (MontantTrancheBE m in alltranches)
            {
                totalAPayer += m.montant;
            }
            totalAPayer += (double)fraisInscription;

            payers = prestationBL.listerSuivantCriterePayer("matricule = " + "'" + matricule + "' and annee =" + "'" + annee + "'");
            foreach (PayerBE payer in payers)
            {
                //if (payer.codePrestation == INSCRIPTION)
                //{
                //    liste.Add(new LignePrestation(payer.codePrestation, payer.codeTranche, (decimal)payer.montant, payer.observation,
                //        fraisInscription - Convert.ToDecimal(payer.montant) - payer.remise, payer.remise, payer.datePaiement.ToShortDateString()));
                //}
                //else
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
                }
                totalRemise += Convert.ToDouble(payer.remise);
                totalVerse += payer.montant;
                totalAPayer -= Convert.ToDouble(payer.remise);
            }

            resteAPayer = totalAPayer - totalVerse;

            txtTotal.Text = totalAPayer.ToString("0,0", elGR);
            txtTotalVerse.Text = totalVerse.ToString("0,0", elGR);
            txtResteAPayer.Text = (totalAPayer - totalVerse).ToString("0,0", elGR);
            txtRemise.Text = totalRemise.ToString("0,0", elGR);

            //txtReste.Text = "0";

            return liste;
        }

        private void txtMatricule_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                statutEleve();
            }
        }

        private void statutEleve()
        {
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
                //lblNom.Content = eleve.nom + " - Catégorie : " + c.nomCatEleve;
                payers = prestationBL.listerSuivantCriterePayer("matricule = " + "'" + eleve.matricule + "' and annee =" + "'" + annee + "'");
                //fraisInscription = prestationBL.obtenirFraisInscription(eleve);
                lignes = new List<LignePrestation>();
                lignes = chargerGrid();
                grdStatus.ItemsSource = lignes;
                grdStatus.Items.Refresh();
            }
            else
                MessageBox.Show("L'élève n'existe pas");
        }

        private void cmdAnnuler_Click(object sender, RoutedEventArgs e)
        {
            txtMatricule.Text = "";
            lignes = new List<LignePrestation>();
            cmbEleve.Text = "";
            grdStatus.ItemsSource = lignes;
            grdStatus.Items.Refresh();
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
                creerEtat.statutFinancier(grdStatus, eleve, categorie + " - " + cat.nomCatEleve, totalAPayer, totalVerse, resteAPayer, totalRemise, prestations);
            }
        }

        private void cmdValider_new_Click(object sender, RoutedEventArgs e)
        {
            statutEleve();
        }

        private void cmdQuitter_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void txtAnnee_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtAnnee.Text != "" && txtMatricule.Text != "")
            {
                statutEleve();
            }
        }

        private void txtAnneeScolaire_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                annee = Convert.ToInt32(txtAnneeScolaire.Text) + 1;
                txtAnnee.Text = " / " + annee.ToString();

            }catch(Exception)
            {
                MessageBox.Show("L'annee doit etre un nombre positif","School brain:Alerte");
            }
        }

        private void txtAnneeScolaire_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Utilitaires.IsTextAllowed(e.Text);
        }

        private void cmbClasse_DropDownClosed(object sender, EventArgs e)
        {
            if (cmbClasse.Text != null && cmbClasse.Text != "")
            {
                //recherche des eleves inscrits  dans cette classe pour charger le combobox des eleves
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
