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
using System.Globalization;
using System.Threading;

namespace Ecole.UI
{
    /// <summary>
    /// Interaction logic for AnnulationVenteUI.xaml
    /// </summary>
    public partial class AnnulationVenteUI : Window
    {
        private static string MESSAGE_MATRICULE_ERRONE = "Ce matricule n'existe pas";
        private static string TOUT = "Tous";
        private string article;
        private string matricule;
        private int annee;
        private int quantite;
        private string login = Ecole.UI.ConnexionUI.utilisateur.login;
        private List<AcheterBE> acheters;
        EleveBE eleve;
        List<string> eleves;
        List<string> classes;
        private GestionArticleBL articleBL;


        public AnnulationVenteUI()
        {
            InitializeComponent();
            eleves = new List<string>();
            classes = new List<string>();
            articleBL = new GestionArticleBL();

            dpiDate.SelectedDate = DateTime.Today;
            dpiDate.IsTodayHighlighted = true;
            dpiDate.Text = DateTime.Now.ToString();

            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "yyyy-MM-dd";
            Thread.CurrentThread.CurrentCulture = ci;
            
            classes = articleBL.listerValeurColonneClasse("codeclasse");
            cmbClasse.ItemsSource = classes;
            eleve = new EleveBE();
            annee = articleBL.anneeEnCours();
            
            acheters = articleBL.listerSuivantCritereAcheters("annee = " + "'" + annee + "'");
            txtAnnee.Text = " / " + annee;
            txtAnneeScolaire.Text = (annee - 1).ToString();
            grdListePaiement.ItemsSource = acheters;
        }

        private void grdListePaiement_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                if (MessageBox.Show("Voulez vous supprimer?", "School : Confirmation", System.Windows.MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    if (grdListePaiement.SelectedIndex != -1)
                    {
                        AcheterBE acheter = new AcheterBE();
                        SetarticleBE setarticle = new SetarticleBE();
                        acheter = acheters.ElementAt(grdListePaiement.SelectedIndex);
                        setarticle.codesetarticle = acheter.codesetarticle;
                        setarticle = articleBL.rechercherSetArticle(setarticle);
                        articleBL.incrementerStock(setarticle, acheter.quantite, annee);
                        acheters.Remove(acheter);
                        articleBL.supprimerAcheter(acheter);
                        grdListePaiement.ItemsSource = acheters;
                        grdListePaiement.Items.Refresh();
                    }
                    else
                        MessageBox.Show("Aucune ligne sélectionnée", "School brain:Alerte");
                }
            }
        }

        private void txtMatricule_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                //recherche de sa classe
                cmbClasse.Text = "";
                InscrireBE inscrire = new InscrireBE();
                inscrire.matricule = txtMatricule.Text;
                inscrire.annee = annee;
                inscrire = articleBL.rechercherInscrire(inscrire);
                if (inscrire != null)
                {
                    cmbClasse.Text = inscrire.codeClasse;
                }
                eleves = new List<string>();
                string codeclasse = cmbClasse.Text;
                List<EleveBE> listeleves = new List<EleveBE>();
                listeleves = articleBL.listerElevesDuneClasse(codeclasse, annee);
                if (listeleves != null)
                {
                    foreach (EleveBE el in listeleves)
                    {
                        eleves.Add(el.matricule + " - " + el.nom);
                    }
                }
                cmbEleve.ItemsSource = eleves;

                eleve = new EleveBE();
                eleve.matricule = txtMatricule.Text.ToString();
                eleve = articleBL.rechercherEleve(eleve);
                if (eleve != null)
                    cmbEleve.Text = eleve.matricule + " - " + eleve.nom;
                else
                    MessageBox.Show(MESSAGE_MATRICULE_ERRONE, "School brain:alerte", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
        private void cmbClasse_DropDownClosed(object sender, EventArgs e)
        {
            if (cmbClasse.Text != null && cmbClasse.Text != "")
            {
                //recherche des eleves inscrits  dans cette classe pour charger le combobox des eleves
                eleves = new List<string>();
                string codeclasse = cmbClasse.Text;
                List<EleveBE> listeleves = new List<EleveBE>();
                listeleves = articleBL.listerElevesDuneClasse(codeclasse, annee);
                if (listeleves != null)
                {
                    foreach (EleveBE el in listeleves)
                    {
                        eleves.Add(el.matricule + " - " + el.nom);
                    }
                }
                cmbEleve.ItemsSource = eleves;
                txtMatricule.Text = "";
            }
        }

        private void cmbEleve_DropDownClosed(object sender, EventArgs e)
        {
            if (cmbEleve.Text != null && cmbEleve.Text != "")
            {
                string nommat = cmbEleve.Text;
                txtMatricule.Text = nommat.Split('-')[0].Trim();
                eleve.matricule = nommat.Split('-')[0].Trim();
                eleve = articleBL.rechercherEleve(eleve);
                chargerDataGrid(nommat.Split('-')[0].Trim());
            }
        }

        private void txtAnneeScolaire_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                annee = Convert.ToInt32(txtAnneeScolaire.Text) + 1;
                txtAnnee.Text = " / " + annee.ToString();
                rechargerGrid();
            }
            catch (Exception)
            {
                MessageBox.Show("L'annee doit etre un nombre positif", "School brain:Alerte");
            }
        }

        private void txtAnneeScolaire_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Ecole.UI.Utilitaires.IsTextAllowed(e.Text);
        }

        private void txtMatricule2_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                chargerDataGrid();
            }
        }

        private void dpiDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            chargerDataGrid();
        }

        private void btnInitialiser_Click(object sender, RoutedEventArgs e)
        {
            rechargerGrid();
        }

        private void cmdQuitter_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void chargerDataGrid()
        {
            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "yyyy-MM-dd";
            Thread.CurrentThread.CurrentCulture = ci;

            string date = DateTime.Today.Date.ToShortDateString();
            string mat = "%";

            if (dpiDate.SelectedDate != null)
                date = dpiDate.SelectedDate.Value.Date.ToShortDateString();
            if (txtMatricule2.Text != "")
                mat = txtMatricule2.Text;

            acheters = new List<AcheterBE>();
            acheters = articleBL.listerSuivantCritereAcheter("dateachat = " + "'" + date + "' AND matricule LIKE " + "'%" + mat + "%'");
            grdListePaiement.ItemsSource = acheters;
            grdListePaiement.Items.Refresh();
        }

        private void chargerDataGrid(string matricle)
        {
            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "yyyy-MM-dd";
            Thread.CurrentThread.CurrentCulture = ci;

            acheters = new List<AcheterBE>();
            acheters = articleBL.listerSuivantCritereAcheter(" matricule LIKE " + "'%" + matricle + "%'");
            grdListePaiement.ItemsSource = acheters;
            grdListePaiement.Items.Refresh();
        }

        private void rechargerGrid()
        {
            txtMatricule2.Text = "";
            txtMatricule.Text = "";
            cmbEleve.Text = "";
            dpiDate.Text = "";
            dpiDate.SelectedDate = null;
            acheters = articleBL.listerSuivantCritereAcheters("annee = " + "'" + annee + "'");
            grdListePaiement.ItemsSource = acheters;
            grdListePaiement.Items.Refresh();
        }
    }
}
