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
using Ecole.Utilitaire;

namespace Ecole.UI
{
    /// <summary>
    /// Interaction logic for SetArticle.xaml
    /// </summary>
    /// 
    public partial class VenteArticleUI : Window
    {
        private static string MESSAGE_MATRICULE_ERRONE = "Ce matricule n'existe pas";
        private static string TOUT = "Tous";
        private string article;
        private string matricule;
        private int annee;
        private int quantite;
        private string login = Ecole.UI.ConnexionUI.utilisateur.login;
        private List<AcheterBE> acheters;
        SetarticleBE setarticle;
        AcheterBE ancien_acheter;
        EleveBE eleve;
        List<string> eleves;
        List<string> classes;
        List<String> listSet;
        List<String> listSet2;
        private GestionArticleBL articleBL;

        //chaine qui precise l'action à entreprendre quand on click sur le bouton Valider
        private string typeValidation;

        public VenteArticleUI()
        {
            InitializeComponent();
            listSet = new List<string>();
            listSet2 = new List<string>();
            eleves = new List<string>();
            classes = new List<string>();
            articleBL = new GestionArticleBL();

            dpiDate.SelectedDate = DateTime.Today;
            dpiDate.IsTodayHighlighted = true;
            dpiDate.Text = DateTime.Now.ToString();
            dpiDateOp.SelectedDate = DateTime.Today;
            dpiDateOp.IsTodayHighlighted = true;
            dpiDateOp.Text = DateTime.Now.ToString();

            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "yyyy-MM-dd";
            Thread.CurrentThread.CurrentCulture = ci;

            ancien_acheter = new AcheterBE();

            classes = articleBL.listerValeurColonneClasse("codeclasse");
            cmbClasse.ItemsSource = classes;

            typeValidation = "enregistrer";
            acheters = articleBL.listerSuivantCritereAcheters("dateachat = " + "'" + DateTime.Today.Date.ToShortDateString() + "'");
            setarticle = new SetarticleBE();
            eleve = new EleveBE();
            listSet = articleBL.listerValeursColonneSetArticle("codesetarticle");
            listSet2 = articleBL.listerValeursColonneSetArticle("codesetarticle");
            listSet2.Add(TOUT);
            cmbArticle.ItemsSource = listSet;
            cmbArticle2.ItemsSource = listSet2;
            annee = articleBL.anneeEnCours();
            txtAnnee.Text = " / " + annee;
            txtAnneeScolaire.Text = (annee - 1).ToString();
            grdListePaiement.ItemsSource = acheters;
        }

        private void cmbArticle_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CultureInfo elGR = CultureInfo.CreateSpecificCulture("el-GR");

            if (cmbArticle.SelectedValue != null)
            {
                setarticle = new SetarticleBE();
                setarticle.codesetarticle = cmbArticle.SelectedValue.ToString();
                setarticle = articleBL.rechercherSetArticle(setarticle);
                lblMontant.Content = setarticle.montant.ToString("0,0", elGR);
            }
        }

        private void txtMatricule2_LostFocus(object sender, RoutedEventArgs e)
        {
            chargerDataGrid();
        }

        private void txtMatricule2_TextChanged(object sender, TextChangedEventArgs e)
        {
            chargerDataGrid();
        }

        private void cmbArticle2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            chargerDataGrid();
        }

        private void dpiDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            chargerDataGrid();
        }

        private void cmdValider_Click(object sender, RoutedEventArgs e)
        {
            if (validerFormulaire())
            {
                article = cmbArticle.Text.ToString();
                matricule = txtMatricule.Text.ToString();
                quantite = Convert.ToInt32(txtQuantite.Text);
                AcheterBE acheter = new AcheterBE(article, matricule, login, annee, dpiDateOp.SelectedDate.Value, setarticle.montant, quantite);

                if (typeValidation == "enregistrer")
                {
                    if (eleve != null)
                    {
                        if (articleBL.ajouterAcheter(acheter))
                        {
                            //articleBL.decrementerStock(setarticle, quantite, annee);
                            acheters.Add(acheter);
                            grdListePaiement.Items.Refresh();
                            CreerEtat etat = new CreerEtat("achat_article" + matricule, "Facture de vente d'article N° " + articleBL.rechercherNumeroAcheter(acheter));
                            etat.factureAchatArticle(acheter, eleve, setarticle);
                        }
                        else
                            MessageBox.Show("Enregistrement non réussi");
                    }
                    else
                        MessageBox.Show("Changer la valeur du matricule !!!!", "School brain : alerte");
                }
                else
                {
                    //ajout des informations concernant la suppression
                    acheters.Remove(ancien_acheter);
                    grdListePaiement.ItemsSource = acheters;
                    grdListePaiement.Items.Refresh();
                    //articleBL.incrementerStock(setarticle, quantite, annee);
                    articleBL.supprimerAcheter(ancien_acheter);

                    //modification dans la BD
                    if (articleBL.ajouterAcheter(acheter))
                    {
                        acheters.Add(acheter);
                        grdListePaiement.Items.Refresh();
                        CreerEtat etat = new CreerEtat("achat_article" + matricule, "Facture de vente d'article N° " + articleBL.rechercherNumeroAcheter(acheter));
                        etat.factureAchatArticle(acheter, eleve, setarticle);
                    }
                    else
                        MessageBox.Show("Mise à jour échouée");

                    typeValidation = "enregistrer";
                }

                txtMatricule.Clear();
                cmbEleve.Text = "";
                cmbArticle.Text = "";
                lblMontant.Content = "";
                txtQuantite.Clear();
            }
            else
                MessageBox.Show("Formulaire non valider, entrer toutes les informations", "School brain : Alerte");
        }

        private void cmdAnnuler_Click(object sender, RoutedEventArgs e)
        {
            txtMatricule.Clear();
            cmbEleve.Text = "";
            lblMontant.Content = "";
            typeValidation = "enregistrer";
            txtQuantite.Clear();
        }

        private void cmdQuitter_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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

        private void chargerDataGrid()
        {
            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "yyyy-MM-dd";
            Thread.CurrentThread.CurrentCulture = ci;

            string date = DateTime.Today.Date.ToShortDateString();
            string mat = "%";
            string code = "%";

            if (dpiDate.SelectedDate != null)
                date = dpiDate.SelectedDate.Value.Date.ToShortDateString();
            if (txtMatricule2.Text != "")
                mat = txtMatricule2.Text;
            if (cmbArticle2.SelectedValue != null)
                if (cmbArticle2.SelectedValue.ToString() != TOUT)
                    code = cmbArticle2.SelectedValue.ToString();

            acheters = new List<AcheterBE>();
            acheters = articleBL.listerSuivantCritereAcheter("dateachat = " + "'" + date + "' AND matricule LIKE " + "'%" + mat + "%' AND codesetarticle LIKE " + "'" + code + "'");
            grdListePaiement.ItemsSource = acheters;
            grdListePaiement.Items.Refresh();
        }

        private bool validerFormulaire()
        {
            bool b = true;

            if (txtAnneeScolaire.Text == null || cmbArticle.SelectedValue == null || txtMatricule.Text == null || txtQuantite.Text == null)
                b = false;
            else if (txtAnneeScolaire.Text == "" || cmbArticle.SelectedValue.ToString() == "" || txtMatricule.Text == "" || txtQuantite.Text == "")
                b = false;

            return b;
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
            }
        }

        private void txtAnneeScolaire_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                annee = Convert.ToInt32(txtAnneeScolaire.Text) + 1;
                txtAnnee.Text = " / " + annee.ToString();

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

        private void cmdNouveauSet_Click(object sender, RoutedEventArgs e)
        {
            NouveauSetArticleUI dialog = new NouveauSetArticleUI();
            dialog.ShowDialog();
            try
            {
                string code = dialog.code;
                string designation = dialog.designation;
                decimal montant = dialog.montant;
                if (code == "" & designation == "" & montant == 0)
                {
                }
                else if (code == "" || designation == "" || montant == 0)
                {
                    MessageBox.Show("Un set ne peut pas avoir les champs vides", "School brain:Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    SetarticleBE set = new SetarticleBE(code, annee, designation, montant);
                    if (articleBL.ajouterSetArticle(set))
                    {
                        listSet.Add(code);
                        cmbArticle.ItemsSource = listSet;
                        cmbArticle.SelectedIndex = listSet.IndexOf(code);
                        cmbArticle.Items.Refresh();
                        CultureInfo elGR = CultureInfo.CreateSpecificCulture("el-GR");
                        lblMontant.Content = montant.ToString("0,0", elGR);
                        MessageBox.Show("Nouveau set créé", "School brain:Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                        MessageBox.Show("Nouveau set non enregistré, il doit être déjà créé dans le système", "School brain:Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception) { }
        }

    }
}
