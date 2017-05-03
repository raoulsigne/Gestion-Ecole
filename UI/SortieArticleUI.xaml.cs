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
using System.Collections.ObjectModel;
using System.Data;

namespace Ecole.UI
{
    /// <summary>
    /// Interaction logic for SortieArticleUI.xaml
    /// </summary>
    public partial class SortieArticleUI : Window
    {
        private static string MESSAGE_MATRICULE_ERRONE = "Ce matricule n'existe pas";
        private static string TOUT = "Tous";
        private int annee;
        private string login = Ecole.UI.ConnexionUI.utilisateur.login;
        private List<AcheterBE> acheters;
        SetarticleBE setarticle;
        AcheterBE ancien_acheter;
        EleveBE eleve;
        List<string> eleves;
        List<string> classes;
        List<string> achats_setarticle;
        List<StockerBE> stockers;
        List<ArticleBE> LArticleBE;
        private GestionArticleBL articleBL;
        public ObservableCollection<ArticleBE> ListeArticles1 { get; set; }
        public ObservableCollection<ArticleQTBE> ListeArticles2 { get; set; }
        public string typeOperation;
        public static string ENREGISTRER = "enregistrer";
        public static string MODIFIER = "modifier";

        public SortieArticleUI()
        {
            InitializeComponent();
            dpiDateOp.SelectedDate = DateTime.Today;
            dpiDateOp.IsTodayHighlighted = true;
            dpiDateOp.Text = DateTime.Now.ToString();
            eleves = new List<string>();
            classes = new List<string>();
            achats_setarticle = new List<string>();
            articleBL = new GestionArticleBL();

            stockers = new List<StockerBE>();
            ListeArticles1 = new ObservableCollection<ArticleBE>();
            ListeArticles2 = new ObservableCollection<ArticleQTBE>();
            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "yyyy-MM-dd";
            Thread.CurrentThread.CurrentCulture = ci;

            ancien_acheter = new AcheterBE();
            classes = articleBL.listerValeurColonneClasse("codeclasse");
            cmbClasse.ItemsSource = classes;
            cmbClasse.SelectedIndex = 0;

            setarticle = new SetarticleBE();
            eleve = new EleveBE();
            annee = articleBL.anneeEnCours();
            txtAnnee.Text = " / " + annee;
            txtAnneeScolaire.Text = (annee - 1).ToString();
            chargerListeEleves();
            LArticleBE = articleBL.listerTousLesArticle();
            RemplirDataGrid1(LArticleBE);
            typeOperation = ENREGISTRER;
            radioEnregistrement.IsChecked = true;
        }

        public void RemplirDataGrid1(List<ArticleBE> listObjet)
        {
            var table = new DataTable();

            table.Columns.Add(new DataColumn("code", typeof(string)));
            table.Columns.Add(new DataColumn("designation", typeof(string)));

            if (listObjet != null)
            {
                for (int i = 0; i < listObjet.Count; i++)
                {
                    DataRow dr = table.NewRow();
                    dr["code"] = listObjet.ElementAt(i).codeArticle;
                    dr["designation"] = listObjet.ElementAt(i).designation;
                    table.Rows.Add(dr);
                }
            }

            string vCode = "";
            string vDesignation = "";

            ListeArticles1.Clear();

            foreach (DataRow row in table.Rows)
            {
                vCode = Convert.ToString(row["code"]);
                vDesignation = Convert.ToString(row["designation"]);
                ArticleBE article = new ArticleBE();
                article.codeArticle = vCode;
                article.designation = vDesignation;
                ListeArticles1.Add(article);
            }

            grdListeArticle1.ItemsSource = ListeArticles1;
            grdListeArticle1.Items.Refresh();
        }

        public void RemplirDataGrid2(List<ArticleQTBE> listObjet)
        {
            var table = new DataTable();

            table.Columns.Add(new DataColumn("code", typeof(string)));
            table.Columns.Add(new DataColumn("quantite", typeof(int)));

            if (listObjet != null)
            {
                for (int i = 0; i < listObjet.Count; i++)
                {
                    DataRow dr = table.NewRow();
                    dr["code"] = listObjet.ElementAt(i).codeArticle;
                    dr["categorie"] = listObjet.ElementAt(i).codeCatArticle;
                    dr["designation"] = listObjet.ElementAt(i).designation;
                    dr["quantite"] = listObjet.ElementAt(i).designation;
                    table.Rows.Add(dr);
                }
            }

            string vCode = "";
            string vCategorie = "";
            string vDesignation = "";
            int vQute = 0;

            ListeArticles2.Clear();
            foreach (DataRow row in table.Rows)
            {
                vCode = Convert.ToString(row["code"]);
                vCategorie = Convert.ToString(row["categorie"]);
                vDesignation = Convert.ToString(row["designation"]);
                vQute = Convert.ToInt16(row["quantite"]);
                ListeArticles2.Add(new ArticleQTBE(vCode, vCategorie, vDesignation, vQute));
            }

            grdListeArticle2.ItemsSource = ListeArticles2;
            grdListeArticle2.Items.Refresh();
        }

        private void txtMatricule_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                nouveauMatricule();
            }
        }
        private void cmbClasse_DropDownClosed(object sender, EventArgs e)
        {
            if (cmbClasse.Text != null && cmbClasse.Text != "")
            {
                chargerListeEleves();
            }
        }

        private void chargerListeEleves()
        {
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

        private void cmbEleve_DropDownClosed(object sender, EventArgs e)
        {
            if (cmbEleve.Text != null && cmbEleve.Text != "")
            {
                string nommat = cmbEleve.Text;
                txtMatricule.Text = nommat.Split('-')[0].Trim();

                acheters = new List<AcheterBE>();
                achats_setarticle = new List<string>();
                acheters = articleBL.listerSuivantCritereAcheters(" matricule = " + "'" + txtMatricule.Text + "' AND annee = " + "'" + annee + "'");
                if (acheters != null && acheters.Count > 0)
                {
                    foreach (AcheterBE a in acheters)
                        achats_setarticle.Add(a.codesetarticle);
                    cmbArticle.ItemsSource = achats_setarticle;
                    cmbArticle.Items.Refresh();
                    cmbArticle.SelectedIndex = 0;

                    setarticle = new SetarticleBE();
                    setarticle.codesetarticle = achats_setarticle.ElementAt(0);
                    setarticle = articleBL.rechercherSetArticle(setarticle);
                    lblNomSetArticle.Content = setarticle.nomsetarticle;
                    lblPrix.Content = setarticle.montant.ToString();
                    txtQuantite.Text = acheters.ElementAt(0).quantite.ToString();

                    lignesStockerDuneVente();
                }
                else
                {
                    achats_setarticle = new List<string>();
                    cmbArticle.Text = "";
                    cmbArticle.ItemsSource = achats_setarticle;
                    cmbArticle.Items.Refresh();
                    lblNomSetArticle.Content = "";
                    lblPrix.Content = "";
                    txtQuantite.Text = "";
                    ListeArticles2.Clear();
                    grdListeArticle2.ItemsSource = ListeArticles2;
                    RemplirDataGrid1(LArticleBE);
                }
            }
        }

        private void cmbArticle_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbArticle.SelectedValue != null && cmbArticle.Text != "")
            {
                setarticle = new SetarticleBE();
                setarticle.codesetarticle = cmbArticle.SelectedValue.ToString();
                setarticle = articleBL.rechercherSetArticle(setarticle);
                lblNomSetArticle.Content = setarticle.nomsetarticle;
                lblPrix.Content = setarticle.montant.ToString();
                AcheterBE a = acheters.ElementAt(achats_setarticle.IndexOf(cmbArticle.Text));
                txtQuantite.Text = a.quantite.ToString();

                lignesStockerDuneVente();
            }
        }

        private void cmbArticle_DropDownClosed(object sender, EventArgs e)
        {
            if (cmbArticle.SelectedValue != null && cmbArticle.Text != "")
            {
                setarticle = new SetarticleBE();
                setarticle.codesetarticle = cmbArticle.SelectedValue.ToString();
                setarticle = articleBL.rechercherSetArticle(setarticle);
                lblNomSetArticle.Content = setarticle.nomsetarticle;
                lblPrix.Content = setarticle.montant.ToString();
                AcheterBE a = acheters.ElementAt(achats_setarticle.IndexOf(cmbArticle.Text));
                txtQuantite.Text = a.quantite.ToString();

                lignesStockerDuneVente();
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

        private void cmdSimpleFlecheDroite_Click(object sender, RoutedEventArgs e)
        {
            if (grdListeArticle1.SelectedIndex != -1)
            {
                ArticleQTBE articleQt = new ArticleQTBE();
                ArticleBE article = ListeArticles1.ElementAt(grdListeArticle1.SelectedIndex);
                articleQt.codeArticle = article.codeArticle;
                articleQt.codeCatArticle = article.codeCatArticle;
                articleQt.designation = article.designation;
                articleQt.quantite = 1;

                ListeArticles2.Add(articleQt);

                ListeArticles1.Remove(ListeArticles1.ElementAt(grdListeArticle1.SelectedIndex));
                grdListeArticle1.ItemsSource = ListeArticles1;
                grdListeArticle2.ItemsSource = ListeArticles2;
            }
        }

        private void cmdSimpleFlecheGauche_Click(object sender, RoutedEventArgs e)
        {
            if (grdListeArticle2.SelectedIndex != -1)
            {
                try
                {
                    ArticleQTBE articleQt = ListeArticles2.ElementAt(grdListeArticle2.SelectedIndex);
                    ArticleBE article = new ArticleBE();

                    article.codeArticle = articleQt.codeArticle;
                    article.codeCatArticle = articleQt.codeCatArticle;
                    article.designation = articleQt.designation;

                    ListeArticles2.Remove(ListeArticles2.ElementAt(grdListeArticle2.SelectedIndex));
                    ListeArticles1.Add(article);
                    grdListeArticle1.ItemsSource = ListeArticles1;
                    grdListeArticle2.ItemsSource = ListeArticles2;
                    
                    if (typeOperation == MODIFIER) 
                    {
                        int numerovente = articleBL.rechercherNumeroAchat(acheters.ElementAt(cmbArticle.SelectedIndex));
                        articleBL.incrementerStock(articleQt.codeArticle, articleQt.quantite, annee, dpiDateOp.SelectedDate.Value, numerovente);
                    }
                }
                catch (Exception exp) { Console.WriteLine(exp.Message); }
            }
        }

        private void cmdDoubleFlecheDroite_Click(object sender, RoutedEventArgs e)
        {
            if (ListeArticles1.Count != 0)
            {
                for (int i = 0; i < ListeArticles1.Count; i++)
                {

                    ArticleQTBE articleQt = new ArticleQTBE();
                    ArticleBE article = ListeArticles1.ElementAt(i);
                    articleQt.codeArticle = article.codeArticle;
                    articleQt.codeCatArticle = article.codeCatArticle;
                    articleQt.designation = article.designation;
                    articleQt.quantite = 1;


                    ListeArticles2.Add(articleQt);
                }
                ListeArticles1.Clear();
                //RemplirDataGrid1(LArticleBE);
                grdListeArticle1.ItemsSource = ListeArticles1;
                grdListeArticle2.ItemsSource = ListeArticles2;
            }

        }

        private void cmdDoubleFlecheGauche_Click(object sender, RoutedEventArgs e)
        {
            if (ListeArticles2.Count != 0)
            {
                int numerovente = articleBL.rechercherNumeroAchat(acheters.ElementAt(cmbArticle.SelectedIndex));
                for (int i = 0; i < ListeArticles2.Count; i++)
                {
                    ArticleQTBE articleQt = ListeArticles2.ElementAt(i);
                    ArticleBE article = new ArticleBE();

                    article.codeArticle = articleQt.codeArticle;
                    article.codeCatArticle = articleQt.codeCatArticle;
                    article.designation = articleQt.designation;

                    ListeArticles1.Add(article);

                    if (typeOperation == MODIFIER)
                        articleBL.incrementerStock(articleQt.codeArticle, articleQt.quantite, annee, dpiDateOp.SelectedDate.Value, numerovente);
                }
                ListeArticles2.Clear();
                //RemplirDataGrid1(LArticleBE);
                grdListeArticle1.ItemsSource = ListeArticles1;
                grdListeArticle2.ItemsSource = ListeArticles2;
            }
        }

        private void cmdQuitter_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cmdValider_Click(object sender, RoutedEventArgs e)
        {
            if (validerFormulaire())
            {
                if (grdListeArticle2.ItemsSource != null)
                {
                    int nombre = Convert.ToInt32(txtQuantite.Text);
                    int numerovente = articleBL.rechercherNumeroAchat(acheters.ElementAt(cmbArticle.SelectedIndex));
                    foreach (var row in grdListeArticle2.ItemsSource)
                    {
                        ArticleQTBE articleQt = (ArticleQTBE)row;
                        articleBL.decrementerStock(articleQt.codeArticle, articleQt.quantite * nombre, annee, dpiDateOp.SelectedDate.Value, numerovente);
                    }

                    ListeArticles2.Clear();
                    grdListeArticle2.ItemsSource = ListeArticles2;
                    RemplirDataGrid1(LArticleBE);
                    cmbArticle.Text = "";
                    txtMatricule.Text = "";
                    cmbEleve.Text = "";
                    lblNomSetArticle.Content = "";
                    lblPrix.Content = "";
                    txtQuantite.Text = "";
                    typeOperation = ENREGISTRER;
                    radioEnregistrement.IsChecked = true;
                    MessageBox.Show("Opération réalisée avec succès", "school brain:Infos", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                    MessageBox.Show("Choisissez d'abord les articles avant de valider", "school brain:Infos", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void radioEnregistrement_Checked(object sender, RoutedEventArgs e)
        {
            cmdSimpleFlecheGauche.IsEnabled = false;
            cmdDoubleFlecheGauche.IsEnabled = false;
            typeOperation = ENREGISTRER;

            RemplirDataGrid1(LArticleBE);
            ListeArticles2.Clear();
            grdListeArticle2.ItemsSource = ListeArticles2;
            cmbArticle.Text = "";
            txtMatricule.Text = "";
            cmbEleve.Text = "";
            lblNomSetArticle.Content = "";
            lblPrix.Content = "";
            txtQuantite.Text = "";
        }

        private void radioModification_Checked(object sender, RoutedEventArgs e)
        {
            cmdSimpleFlecheGauche.IsEnabled = true;
            cmdDoubleFlecheGauche.IsEnabled = true;
            txtQuantite.IsEnabled = false;
            typeOperation = MODIFIER;

            RemplirDataGrid1(LArticleBE);
            ListeArticles2.Clear();
            grdListeArticle2.ItemsSource = ListeArticles2;
            cmbArticle.Text = "";
            txtMatricule.Text = "";
            cmbEleve.Text = "";
            lblNomSetArticle.Content = "";
            lblPrix.Content = "";
            txtQuantite.Text = "";
        }

        private bool validerFormulaire()
        {
            if (txtMatricule.Text != null && cmbArticle.Text != null && txtQuantite.Text != null)
                if (txtMatricule.Text != "" && cmbArticle.Text != "" && txtQuantite.Text != "")
                    return true;
                else
                    return false;
            else
                return false;
        }

        private void nouveauMatricule()
        {
            //recherche de sa classe
            acheters = new List<AcheterBE>();
            achats_setarticle = new List<string>();
            cmbClasse.Text = "";
            InscrireBE inscrire = new InscrireBE();
            inscrire.matricule = txtMatricule.Text;
            inscrire.annee = annee;
            inscrire = articleBL.rechercherInscrire(inscrire);
            if (inscrire != null)
            {
                cmbClasse.Text = inscrire.codeClasse;
            }
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

            eleve = new EleveBE();
            eleve.matricule = txtMatricule.Text.ToString();
            eleve = articleBL.rechercherEleve(eleve);
            if (eleve != null)
            {
                cmbEleve.Text = eleve.matricule + " - " + eleve.nom;
                acheters = articleBL.listerSuivantCritereAcheters(" matricule = " + "'" + eleve.matricule + "' AND annee = " + "'" + annee + "'");
                if (acheters != null && acheters.Count > 0)
                {
                    foreach (AcheterBE a in acheters)
                        achats_setarticle.Add(a.codesetarticle);
                    cmbArticle.ItemsSource = achats_setarticle;
                    cmbArticle.SelectedIndex = 0;
                    cmbArticle.Items.Refresh();
                }
            }
            else
            {
                MessageBox.Show(MESSAGE_MATRICULE_ERRONE, "School brain:alerte", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                acheters = new List<AcheterBE>();
                achats_setarticle = new List<string>();
                cmbArticle.ItemsSource = achats_setarticle;
            }
        }

        private void lignesStockerDuneVente()
        {
            stockers = new List<StockerBE>();
            List<ArticleBE> articles = new List<ArticleBE>();

            ListeArticles2.Clear();
            int numero = articleBL.rechercherNumeroAchat(acheters.ElementAt(cmbArticle.SelectedIndex));
            stockers = articleBL.rechercherLigneStocker(numero);
            ArticleBE article;
            ArticleQTBE a;
            articles = articleBL.listerTousLesArticle();
            foreach (StockerBE s in stockers)
            {
                a = new ArticleQTBE();
                article = new ArticleBE();
                article.codeArticle = s.codeArticle;
                article = articleBL.rechercherArticle(article);
                a.codeArticle = article.codeArticle;
                a.designation = article.designation;
                a.codeCatArticle = article.codeCatArticle;
                a.quantite = s.quantiteVendue;

                ListeArticles2.Add(a);
                article = articles.Find(c => c.codeArticle.Equals(article.codeArticle));
                articles.Remove(article);
            }

            ListeArticles1.Clear();
            foreach (ArticleBE art in articles)
                ListeArticles1.Add(art);
            grdListeArticle2.ItemsSource = ListeArticles2;
            grdListeArticle2.Items.Refresh();
            grdListeArticle1.ItemsSource = ListeArticles1;
            grdListeArticle1.Items.Refresh();
        }

    }
}
