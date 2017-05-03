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
using System.Collections.ObjectModel;
using System.Data;

using Ecole.BusinessLogic;
using Ecole.BusinessEntity;
using Ecole.Utilitaire;
using System.Globalization;
using System.Threading;

namespace Ecole.UI
{
    /// <summary>
    /// Interaction logic for WindowAddEditArticleUI.xaml
    /// </summary>
    public partial class WindowAddEditArticleUI : Window
    {
        private CreerModifierArticleBL creerModifierArticleBL;
        private CreerModifierCategorieArticleBL creerModifierCategorieArticleBL;
        ApprovisionnementArticleBL approvisionnementArticleBL;
        CreerModifierMagasinBL creerModifierMagasinBL;

        ArticleBE ancienArticle; // sera utilisé pour la mise à jour

        private int etat; // idique si nous sommes en création (0) ou en modification (1)

        // Définition d'une liste 'ListeListeArticlesCycles' observable de 'Article'
        public ObservableCollection<ArticleBE> ListeArticles { get; set; }

        // Fonction permettant de remplir le DataGrid avec les informations de la base de données
        // @param : - listObjet : la liste des objets à afficher dans le DataGrid
        public void RemplirDataGrid(List<ArticleBE> listObjet)
        {
            // Ajout de données dans la DataTable :
            var table = new DataTable();

            table.Columns.Add(new DataColumn("codeArticle", typeof(string)));
            table.Columns.Add(new DataColumn("codeCategorie", typeof(string)));
            table.Columns.Add(new DataColumn("designation", typeof(string)));

            table.Columns.Add(new DataColumn("quantiteSaisie", typeof(string)));
            table.Columns.Add(new DataColumn("PuArticle", typeof(string)));

            if (listObjet != null)
            {

                for (int i = 0; i < listObjet.Count; i++)
                {
                    DataRow dr = table.NewRow();
                    dr["codeArticle"] = listObjet.ElementAt(i).codeArticle;
                    dr["codeCategorie"] = listObjet.ElementAt(i).codeCatArticle;
                    dr["designation"] = listObjet.ElementAt(i).designation;

                    dr["quantiteSaisie"] = listObjet.ElementAt(i).quantiteSaisie;
                    dr["PuArticle"] = listObjet.ElementAt(i).PuArticle;

                    table.Rows.Add(dr);
                }
            }

            string vCodeArtcicle = "";
            string vCategorie = "";
            string vDesignation = "";

            ListeArticles.Clear();

            //Personnes_Table = LoadDataTable();

            foreach (DataRow row in table.Rows)
            {
                vCodeArtcicle = Convert.ToString(row["codeArticle"]);
                vCategorie = Convert.ToString(row["codeCategorie"]);
                vDesignation = Convert.ToString(row["designation"]);

                ArticleBE article = new ArticleBE(vCodeArtcicle, vCategorie, vDesignation);
                article.quantiteSaisie = Convert.ToString(row["quantiteSaisie"]);
                article.PuArticle = Convert.ToString(row["PuArticle"]);

                ListeArticles.Add(article);

            }
        }

        // retourne la liste des codes de Magasin deja enregistré (pour le filtre)
        public List<string> getListCodeMagasin(List<MagasinBE> listMagasin)
        {
            List<string> listeCodeMagasin = new List<string>();

            listeCodeMagasin = new List<string>();
            if (listMagasin != null)
            {
                for (int i = 0; i < listMagasin.Count; i++)
                {
                    listeCodeMagasin.Add(listMagasin.ElementAt(i).codeMagasin);
                }
                //listeCodeMagasin.Add("Tous");
                return listeCodeMagasin;
            }
            else return null;
        }

        // retourne la liste des codes des Article deja enregistré (pour le filtre)
        public List<string> getListCodeArticle(List<ArticleBE> listArticle)
        {
            List<string> listeCodeArticle = new List<string>();

            listeCodeArticle = new List<string>();
            listeCodeArticle.Add("<Tous les Articles>");
            if (listArticle != null)
            {
                for (int i = 0; i < listArticle.Count; i++)
                {
                    listeCodeArticle.Add(listArticle.ElementAt(i).codeArticle);
                }
                //listeCodeArticle.Add("Tous");
                return listeCodeArticle;
            }
            else return null;
        }

        // retourne la liste des codes des Catégorie d'article deja enregistré (pour le filtre)
        public List<string> getListCodeCatArticle1(List<CategorieArticleBE> listCatArticle)
        {
            List<string> listeCodeCatArticle = new List<string>();

            listeCodeCatArticle = new List<string>();
            if (listCatArticle != null)
            {
                for (int i = 0; i < listCatArticle.Count; i++)
                {
                    listeCodeCatArticle.Add(listCatArticle.ElementAt(i).codeCatArticle);
                }
                //listeCodeCatArticle.Add("Tous");
                return listeCodeCatArticle;
            }
            else return null;
        }

        // retourne la liste des codes des Catégorie d'article deja enregistré (pour le filtre)
        public List<string> getListCodeCatArticle2(List<CategorieArticleBE> listCatArticle)
        {
            List<string> listeCodeCatArticle = new List<string>();

            listeCodeCatArticle = new List<string>();
            listeCodeCatArticle.Add("<Toutes les Catégories>");

            if (listCatArticle != null)
            {
                for (int i = 0; i < listCatArticle.Count; i++)
                {
                    listeCodeCatArticle.Add(listCatArticle.ElementAt(i).codeCatArticle);
                }
                //listeCodeCatArticle.Add("Tous");
                return listeCodeCatArticle;
            }
            else return null;
        }

        public WindowAddEditArticleUI()
        {
            InitializeComponent();

            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";
            Thread.CurrentThread.CurrentCulture = ci;

            creerModifierArticleBL = new CreerModifierArticleBL();
            creerModifierCategorieArticleBL = new CreerModifierCategorieArticleBL();
            approvisionnementArticleBL = new ApprovisionnementArticleBL();
            creerModifierMagasinBL = new CreerModifierMagasinBL();

            ancienArticle = new ArticleBE();

            etat = 0;

            // A mettre pour que le binding avec le DataGrid fonctionne !
            grdListeArticle.DataContext = this;

            // Initialisation de la collection, qui va s'afficher dans la DataGrid :
            ListeArticles = new ObservableCollection<ArticleBE>();
            List<ArticleBE> LArticleBE = creerModifierArticleBL.listerTousLesArticle();
            // on met la liste "LArticleBE" dans le DataGrid
            RemplirDataGrid(LArticleBE);

            // ------------------- Chargement de la liste des codes d'Article dans le comboBox de la fenêtre 
            //(utile pour le filtre)
            cmbFilterCodeArticle.ItemsSource = getListCodeArticle(LArticleBE);
            // ------------------- Chargement de la liste des codes de Cycle dans le comboBox de la fenêtre 
            //(utile pour le filtre)
            List<CategorieArticleBE> LCatArticleBE = creerModifierCategorieArticleBL.listerTousLesCategorieArticle();
            cmbCategorieArticle.ItemsSource = getListCodeCatArticle1(LCatArticleBE);
            cmbFilterCategorieArticle.ItemsSource = getListCodeCatArticle2(LCatArticleBE);

            // ------------------- Chargement de la liste des codes de Magasin dans le comboBox de la fenêtre 
            //(utile pour le filtre)
            creerModifierMagasinBL = new CreerModifierMagasinBL();
            List<MagasinBE> LMagasinBE = creerModifierMagasinBL.listerToutesLesMagasin();
            cmbMagasin.ItemsSource = getListCodeMagasin(LMagasinBE);

            txtAnnee.Text = Convert.ToString(approvisionnementArticleBL.getAnneeEnCours());

        }

        private void cmdFermer_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cmdValider_Click(object sender, RoutedEventArgs e)
        {
            if ((cmbMagasin.Text != "" || txtQuantite.Text != "" || txtPrixUnitaire.Text != ""))
            {

                if ((txtCodeArticle.Text != null && txtDesignation.Text != null && cmbCategorieArticle.Text != null && cmbMagasin.Text != null && txtQuantite.Text != null && txtPrixUnitaire.Text != null && txtAnnee.Text != null)
                && (txtCodeArticle.Text != "" && txtDesignation.Text != "" && cmbCategorieArticle.Text != "" && cmbMagasin.Text != "" && txtQuantite.Text != "" && txtPrixUnitaire.Text != "" && txtAnnee.Text != null))
                {
                    ArticleBE article = new ArticleBE();
                    article.codeArticle = txtCodeArticle.Text;
                    article.codeCatArticle = cmbCategorieArticle.Text;
                    article.designation = txtDesignation.Text;

                    //********************** DEBUT utile pour l'aafichage dans le datagrid
                    article.quantiteSaisie = txtQuantite.Text;
                    article.PuArticle = txtPrixUnitaire.Text;
                    //********************** FIN utile pour l'aafichage dans le datagrid


                    StockerBE stock = new StockerBE();
                    stock.annee = Convert.ToInt16(txtAnnee.Text);

                    //on teste si il ya aucun enregistrement dans la table stocker
                    if (approvisionnementArticleBL.tableStokerIsEmpty())
                    {
                        //alors c'est le premier enregistrement
                        //stock.numero = Convert.ToInt16(vNumero);
                        stock.codeMagasin = cmbMagasin.Text;
                        stock.codeArticle = txtCodeArticle.Text;
                        stock.stockDebut = 0;
                        stock.quantiteAchetee = Convert.ToInt16(txtQuantite.Text);
                        stock.quantiteVendue = 0;
                        //DatePicker dpk = new DatePicker();
                        //dpk.Text = Convert.ToString(System.DateTime.Today.Date);
                        stock.dateOperation = System.DateTime.Today.Date;
                        //stock.dateOperationString = System.DateTime.Today.ToShortDateString;
                        stock.annee = Convert.ToInt16(txtAnnee.Text);
                        stock.puArticle = Convert.ToInt16(txtPrixUnitaire.Text);
                        stock.stockRestant = Convert.ToInt16(txtQuantite.Text);
                    }
                    else
                    {
                        //ce n'est pas le premier enregistrement
                        //alors on recherche le dernier enregistrement
                        StockerBE dernierStocker = approvisionnementArticleBL.dernierEnregistrementStocker(txtCodeArticle.Text, cmbMagasin.Text);

                        if (dernierStocker != null)
                        {
                            //stock.numero = Convert.ToInt16(vNumero);
                            stock.codeMagasin = cmbMagasin.Text;
                            stock.codeArticle = txtCodeArticle.Text;

                            if (dernierStocker.annee != stock.annee)
                                stock.stockDebut = Convert.ToInt16(dernierStocker.stockRestant);
                            else
                                stock.stockDebut = Convert.ToInt16(dernierStocker.stockDebut);

                            stock.quantiteAchetee = Convert.ToInt16(txtQuantite.Text);
                            stock.quantiteVendue = 0;
                            stock.dateOperation = System.DateTime.Today.Date;
                            //stock.dateOperationString = System.DateTime.Today.ToShortDateString;
                            stock.annee = Convert.ToInt16(txtAnnee.Text);
                            stock.puArticle = Convert.ToInt16(txtPrixUnitaire.Text);
                            stock.stockRestant = Convert.ToInt16(dernierStocker.stockRestant + Convert.ToInt16(txtQuantite.Text));
                        }
                        else
                        {
                            //le stock de début est égale à 0
                            //le stock restant est égale à la quantité achetée

                            //alors c'est le premier enregistrement
                            //stock.numero = Convert.ToInt16(vNumero);
                            stock.codeMagasin = cmbMagasin.Text;
                            stock.codeArticle = txtCodeArticle.Text;
                            stock.stockDebut = 0;
                            stock.quantiteAchetee = Convert.ToInt16(txtQuantite.Text);
                            stock.quantiteVendue = 0;
                            //DatePicker dpk = new DatePicker();
                            //dpk.Text = Convert.ToString(System.DateTime.Today.Date);
                            stock.dateOperation = System.DateTime.Today.Date;
                            //stock.dateOperationString = System.DateTime.Today.ToShortDateString;
                            stock.annee = Convert.ToInt16(txtAnnee.Text);
                            stock.puArticle = Convert.ToInt16(txtPrixUnitaire.Text);
                            stock.stockRestant = Convert.ToInt16(txtQuantite.Text);
                        }

                    }


                    if (etat == 1)
                    {
                        creerModifierArticleBL.modifierArticle(ancienArticle, article);
                        List<ArticleBE> LArticleBE = creerModifierArticleBL.listerTousLesArticle();
                       
                        // on met la liste "LArticleBE" dans le DataGrid
                        RemplirDataGrid(LArticleBE);

                        // ------------------- Chargement de la liste des codes de Article dans le comboBox de la fenêtre 
                        //(utile pour le filtre)
                        cmbFilterCodeArticle.ItemsSource = getListCodeArticle(LArticleBE);

                        txtCodeArticle.Text = "";
                        cmbMagasin.Text = "";
                        txtQuantite.Text = "";
                        //dpkDateAchat.Text = "";
                        txtPrixUnitaire.Text = "";
                        //dpkDatePeremption.Text = "";
                        cmbCategorieArticle.Text = "";
                        txtDesignation.Text = "";
                        txtAnnee.Text = Convert.ToString(approvisionnementArticleBL.getAnneeEnCours());
                        etat = 0;
                    }
                    else if (creerModifierArticleBL.rechercherArticle(new ArticleBE(txtCodeArticle.Text, cmbCategorieArticle.Text, txtDesignation.Text)) == null)
                    {
                        if (creerModifierArticleBL.creerArticle(txtCodeArticle.Text, cmbCategorieArticle.Text, txtDesignation.Text))
                        {
                            MessageBox.Show("Enregistrement Article réussie");
                            txtCodeArticle.Text = "";
                            txtDesignation.Text = "";
                            cmbCategorieArticle.Text = "";

                            List<ArticleBE> LArticleBE = creerModifierArticleBL.listerTousLesArticle();

                            ListeArticles.Add(article);

                            List<ArticleBE> LArticleBE2 = new List<ArticleBE>(); ;
                            for (int i = 0; i < ListeArticles.Count; i++)
                            {
                                LArticleBE2.Add(ListeArticles.ElementAt(i));
                            }
                                // on met la liste "LArticleBE" dans le DataGrid
                            RemplirDataGrid(LArticleBE2);

                            // ------------------- Chargement de la liste des codes d'Article dans le comboBox de la fenêtre 
                            //(utile pour le filtre)
                            cmbFilterCodeArticle.ItemsSource = getListCodeArticle(LArticleBE);
                            // ------------------- Chargement de la liste des codes de Cycle dans le comboBox de la fenêtre 
                            //(utile pour le filtre)
                            List<CategorieArticleBE> LCatArticleBE = creerModifierCategorieArticleBL.listerTousLesCategorieArticle();
                            cmbCategorieArticle.ItemsSource = getListCodeCatArticle1(LCatArticleBE);
                            cmbFilterCategorieArticle.ItemsSource = getListCodeCatArticle2(LCatArticleBE);

                            //-------------- on enregistre le stock de l'article
                            if (approvisionnementArticleBL.creerStock(stock)) {
                                MessageBox.Show("Enregistrement Stock réussie");
                            }
                            else MessageBox.Show("Echec Enregistrement du Stock! ");

                            txtCodeArticle.Text = "";
                            cmbMagasin.Text = "";
                            txtQuantite.Text = "";
                            //dpkDateAchat.Text = "";
                            txtPrixUnitaire.Text = "";
                            //dpkDatePeremption.Text = "";
                            cmbCategorieArticle.Text = "";
                            txtDesignation.Text = "";
                            txtAnnee.Text = Convert.ToString(approvisionnementArticleBL.getAnneeEnCours());
                        }
                        else MessageBox.Show("Echec enregistrement de l'article : une erreure est survenue !");
                    }
                    else MessageBox.Show("Un Aticle ayant le code \"" + txtCodeArticle.Text + "\" existe deja dans le système \n \n Veuillez changer de code SVP !");

                    //**************
                }
                else MessageBox.Show("Tous les champs marqués par un astérix (*) doivent pas être remplis !");
            }
            else if ((txtCodeArticle.Text != null && txtDesignation.Text != null && cmbCategorieArticle.Text != null)
                && (txtCodeArticle.Text != "" && txtDesignation.Text != "" && cmbCategorieArticle.Text != ""))
            {
                ArticleBE article = new ArticleBE();
                article.codeArticle = txtCodeArticle.Text;
                article.codeCatArticle = cmbCategorieArticle.Text;
                article.designation = txtDesignation.Text;

                if (etat == 1)
                {
                    creerModifierArticleBL.modifierArticle(ancienArticle, article);
                    List<ArticleBE> LArticleBE = creerModifierArticleBL.listerTousLesArticle();
                    // on met la liste "LArticleBE" dans le DataGrid
                    RemplirDataGrid(LArticleBE);

                    // ------------------- Chargement de la liste des codes de Article dans le comboBox de la fenêtre 
                    //(utile pour le filtre)
                    cmbFilterCodeArticle.ItemsSource = getListCodeArticle(LArticleBE);

                    txtCodeArticle.Text = "";
                    txtDesignation.Text = "";
                    cmbCategorieArticle.Text = "";
                    etat = 0;
                }
                else if (creerModifierArticleBL.rechercherArticle(new ArticleBE(txtCodeArticle.Text, cmbCategorieArticle.Text, txtDesignation.Text)) == null)
                {
                    if (creerModifierArticleBL.creerArticle(txtCodeArticle.Text, cmbCategorieArticle.Text, txtDesignation.Text))
                    {
                        MessageBox.Show("Enregistrement Article Opération réussie");
                        txtCodeArticle.Text = "";
                        txtDesignation.Text = "";
                        cmbCategorieArticle.Text = "";

                        List<ArticleBE> LArticleBE = creerModifierArticleBL.listerTousLesArticle();
                        // on met la liste "LArticleBE" dans le DataGrid
                        RemplirDataGrid(LArticleBE);

                        // ------------------- Chargement de la liste des codes d'Article dans le comboBox de la fenêtre 
                        //(utile pour le filtre)
                        cmbFilterCodeArticle.ItemsSource = getListCodeArticle(LArticleBE);
                        // ------------------- Chargement de la liste des codes de Cycle dans le comboBox de la fenêtre 
                        //(utile pour le filtre)
                        List<CategorieArticleBE> LCatArticleBE = creerModifierCategorieArticleBL.listerTousLesCategorieArticle();
                        cmbCategorieArticle.ItemsSource = getListCodeCatArticle1(LCatArticleBE);
                        cmbFilterCategorieArticle.ItemsSource = getListCodeCatArticle2(LCatArticleBE);
                    }
                    else MessageBox.Show("Echec enregistrement : une erreure est survenue !");
                }
                else MessageBox.Show("Un Aticle ayant le code \"" + txtCodeArticle.Text + "\" existe deja dans le système \n \n Veuillez changer de code SVP !");

            }
            else MessageBox.Show("Les champs \"Code Article\", \"Catégorie\" et \"Désignation\" doivent êtres remplis !");

           
        }

        private void cmbFilterCodeArticle_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // on filtre la liste affiché dans le DataGrid en fonction du choix de l'utilisateur
            List<ArticleBE> LArticleBE;
            if (cmbFilterCategorieArticle.Text != null && cmbFilterCategorieArticle.Text != "")
            {
                if (cmbFilterCodeArticle.SelectedItem.Equals("<Tous les Articles>"))
                {
                    if (cmbFilterCategorieArticle.SelectedItem.Equals("<Toutes les Catégories>"))
                        LArticleBE = creerModifierArticleBL.listerTousLesArticle();
                    else
                        LArticleBE = creerModifierArticleBL.listerArticleSuivantCritere("codecatarticle = '" + cmbFilterCategorieArticle.SelectedItem + "'");
                }
                else
                    if (cmbFilterCategorieArticle.SelectedItem.Equals("<Toutes les Catégories>"))
                        LArticleBE = creerModifierArticleBL.listerArticleSuivantCritere("codearticle = '" + cmbFilterCodeArticle.SelectedItem + "'");
                    else
                        LArticleBE = creerModifierArticleBL.listerArticleSuivantCritere("codearticle = '" + cmbFilterCodeArticle.SelectedItem + "' AND codecatarticle = '" + cmbFilterCategorieArticle.SelectedItem + "'");
            }
            else
                if (cmbFilterCodeArticle.SelectedItem.Equals("<Tous les Articles>"))
                    LArticleBE = creerModifierArticleBL.listerTousLesArticle();
                else
                    LArticleBE = creerModifierArticleBL.listerArticleSuivantCritere("codearticle = '" + cmbFilterCodeArticle.SelectedItem + "'");

            //on rafraichir le DataGrid
            RemplirDataGrid(LArticleBE);
        }

        private void cmbFilterCategorieArticle_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // on filtre la liste affiché dans le DataGrid en fonction du choix de l'utilisateur
            List<ArticleBE> LArticleBE;
            if (cmbFilterCodeArticle.Text != null && cmbFilterCodeArticle.Text != "")
            {
                if (cmbFilterCategorieArticle.SelectedItem.Equals("<Toutes les Catégories>"))
                {
                    if (cmbFilterCodeArticle.SelectedItem.Equals("<Tous les Articles>"))
                        LArticleBE = creerModifierArticleBL.listerTousLesArticle();
                    else
                        LArticleBE = creerModifierArticleBL.listerArticleSuivantCritere("codearticle = '" + cmbFilterCodeArticle.SelectedItem + "'");
                }
                else
                    if (cmbFilterCodeArticle.SelectedItem.Equals("<Tous les Articles>"))
                        LArticleBE = creerModifierArticleBL.listerArticleSuivantCritere("codecatarticle = '" + cmbFilterCategorieArticle.SelectedItem + "'");
                    else
                        LArticleBE = creerModifierArticleBL.listerArticleSuivantCritere("codearticle = '" + cmbFilterCodeArticle.SelectedItem + "' AND codecatarticle = '" + cmbFilterCategorieArticle.SelectedItem + "'");
            }
            else
                if (cmbFilterCategorieArticle.SelectedItem.Equals("<Toutes les Catégories>"))
                    LArticleBE = creerModifierArticleBL.listerTousLesArticle();
                else
                    LArticleBE = creerModifierArticleBL.listerArticleSuivantCritere("codecatarticle = '" + cmbFilterCategorieArticle.SelectedItem + "'");

            //on rafraichir le DataGrid
            RemplirDataGrid(LArticleBE);
        }

        private void cmdAnnuler_Click(object sender, RoutedEventArgs e)
        {
            txtCodeArticle.Text = "";
            cmbMagasin.Text = "";
            txtQuantite.Text = "";
            //dpkDateAchat.Text = "";
            txtPrixUnitaire.Text = "";
            //dpkDatePeremption.Text = "";
            cmbCategorieArticle.Text = "";
            txtDesignation.Text = "";
            txtAnnee.Text = Convert.ToString(approvisionnementArticleBL.getAnneeEnCours());

            etat = 0;
        }

        private void grdListeArticle_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                if (grdListeArticle.SelectedIndex != -1)
                {
                    if (MessageBox.Show("Voulez-vous vraiment le supprimer ? ", "School : Confimation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        if (creerModifierArticleBL.supprinerArticle(ListeArticles.ElementAt(grdListeArticle.SelectedIndex)))
                            ListeArticles.RemoveAt(grdListeArticle.SelectedIndex);
                        grdListeArticle.ItemsSource = ListeArticles;

                        // ------------------- Chargement de la liste des codes d'Article dans le comboBox de la fenêtre 
                        //(utile pour le filtre)
                        List<ArticleBE> LArticleBE = creerModifierArticleBL.listerTousLesArticle();
                        cmbFilterCodeArticle.ItemsSource = getListCodeArticle(LArticleBE);

                    }

                    grdListeArticle.UnselectAll();
                }
            }
        }

        private void grdListeArticle_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            etat = 1;
            ArticleBE article = new ArticleBE();
            article = creerModifierArticleBL.rechercherArticle(ListeArticles.ElementAt(grdListeArticle.SelectedIndex));
            // on charge les informations dans le formulaire
            txtCodeArticle.Text = article.codeArticle;
            txtDesignation.Text = article.designation;
            cmbCategorieArticle.Text = article.codeCatArticle;

            ancienArticle = article;

            grdListeArticle.UnselectAll();
        }

        private void txtPrixUnitaire_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Utilitaires.IsTextAllowed(e.Text);
        }

        private void txtQuantite_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Utilitaires.IsTextAllowed(e.Text);
        }

        private void cmdEtatDuSTosk_Click(object sender, RoutedEventArgs e)
        {
            WindowGetEtatStockArticleUI getEtatStockArticleUI = new WindowGetEtatStockArticleUI();
            getEtatStockArticleUI.ShowDialog();
        }

        private void cmdImprimer_Click(object sender, RoutedEventArgs e)
        {
            CreerEtat etat = new CreerEtat("Liste des articles -" + DateTime.Today.ToShortDateString(), "Liste des Articles");
            etat.etatListeDesArticles(ListeArticles);
        }

        private void txtAnnee_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Utilitaires.IsTextAllowed(e.Text);
        }

    }
}
