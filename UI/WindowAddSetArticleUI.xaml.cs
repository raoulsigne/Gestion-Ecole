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

using Ecole.BusinessEntity;
using Ecole.BusinessLogic;

namespace Ecole.UI
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class WindowAddSetArticleUI : Window
    {
        CreerSetArticleBL creerSetArticleBL;
        CreerModifierArticleBL creerModifierArticleBL;

        int annee;

        // Définition d'une liste 'ListeArticles' observable de 'Article'
        public ObservableCollection<ArticleBE> ListeArticles1 { get; set; }
        public ObservableCollection<ArticleQTBE> ListeArticles2 { get; set; }

        // Fonction permettant de remplir le DataGrid avec les informations de la base de données
        // @param : - listObjet : la liste des objets à afficher dans le DataGrid
        public void RemplirDataGrid1(List<ArticleBE> listObjet)
        {
            // Ajout de données dans la DataTable :
            var table = new DataTable();

            table.Columns.Add(new DataColumn("code", typeof(string)));
            table.Columns.Add(new DataColumn("designation", typeof(string)));

            if (listObjet != null)
            {

                for (int i = 0; i < listObjet.Count; i++)
                {
                    DataRow dr = table.NewRow();
                    dr["code"] = listObjet.ElementAt(i).codeArticle;
                    //dr["categorie"] = listObjet.ElementAt(i).codeCatArticle;
                    dr["designation"] = listObjet.ElementAt(i).designation;
                    table.Rows.Add(dr);
                }
            }

            string vCode = "";
            //string vCategorie = "";
            string vDesignation = "";

            ListeArticles1.Clear();

            //Personnes_Table = LoadDataTable();

            foreach (DataRow row in table.Rows)
            {
                vCode = Convert.ToString(row["code"]);
                //vCategorie = Convert.ToString(row["categorie"]);
                vDesignation = Convert.ToString(row["designation"]);

                ArticleBE article = new ArticleBE();
                article.codeArticle = vCode;
                article.designation = vDesignation;
                ListeArticles1.Add(article);

            }
        }

        public void RemplirDataGrid2(List<ArticleQTBE> listObjet)
        {
            // Ajout de données dans la DataTable :
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

            //Personnes_Table = LoadDataTable();

            foreach (DataRow row in table.Rows)
            {
                vCode = Convert.ToString(row["code"]);
                vCategorie = Convert.ToString(row["categorie"]);
                vDesignation = Convert.ToString(row["designation"]);
                vQute = Convert.ToInt16(row["quantite"]);

                ListeArticles2.Add(new ArticleQTBE(vCode, vCategorie, vDesignation, vQute));

            }
        }

        public WindowAddSetArticleUI()
        {
            InitializeComponent();

            creerSetArticleBL = new CreerSetArticleBL();
            creerModifierArticleBL = new CreerModifierArticleBL();

            // A mettre pour que le binding avec le DataGrid fonctionne !
            grdListeArticle1.DataContext = this;
            grdListeArticle2.DataContext = this;

            // Initialisation de la collection, qui va s'afficher dans la DataGrid :
            ListeArticles1 = new ObservableCollection<ArticleBE>();
            ListeArticles2 = new ObservableCollection<ArticleQTBE>();
            List<ArticleBE> LArticleBE = creerModifierArticleBL.listerTousLesArticle();
            // on met la liste "LSerieBE" dans le DataGrid
            RemplirDataGrid1(LArticleBE);
            //grdListeArticle1.ItemsSource = LArticleBE;

            ParametresBE param = creerSetArticleBL.getParametres();
            if (param != null)
            {
                annee = param.annee;

                txtAnnee.Text = Convert.ToString(param.annee);
                txtAnneeScolaire.Text = (param.annee - 1).ToString();
            }
            else
            {
                txtAnnee.Text = "";
                txtAnneeScolaire.Text = "";
            }

        }

        private void cmdAnnuler_Click(object sender, RoutedEventArgs e)
        {
            //txtAnnee.Text = "";
            ParametresBE param = creerSetArticleBL.getParametres();
            if (param != null)
            {
                annee = param.annee;

                txtAnnee.Text = Convert.ToString(param.annee);
                txtAnneeScolaire.Text = (param.annee - 1).ToString();
            }
            else
            {
                txtAnnee.Text = "";
                txtAnneeScolaire.Text = "";
            }

            txtCodeSet.Text = "";
            txtMontant.Text = "";
            txtNomSet.Text = "";

            List<ArticleBE> listArticle = creerModifierArticleBL.listerTousLesArticle();
            RemplirDataGrid1(listArticle);

            ListeArticles2.Clear();
            grdListeArticle2.ItemsSource = ListeArticles2;
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
                //RemplirDataGrid1(LArticleBE);
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
                    //RemplirDataGrid1(LArticleBE);
                    grdListeArticle1.ItemsSource = ListeArticles1;
                    grdListeArticle2.ItemsSource = ListeArticles2;
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

        private void cmdDoubleFlecheGau_Click(object sender, RoutedEventArgs e)
        {
            if (ListeArticles2.Count != 0)
            {
                for (int i = 0; i < ListeArticles2.Count; i++)
                {
                    ArticleQTBE articleQt = ListeArticles2.ElementAt(i);
                    ArticleBE article = new ArticleBE();

                    article.codeArticle = articleQt.codeArticle;
                    article.codeCatArticle = articleQt.codeCatArticle;
                    article.designation = articleQt.designation;

                    ListeArticles1.Add(article);
                }
                ListeArticles2.Clear();
                //RemplirDataGrid1(LArticleBE);
                grdListeArticle1.ItemsSource = ListeArticles1;
                grdListeArticle2.ItemsSource = ListeArticles2;
            }
        }

        private void cmdValider_Click(object sender, RoutedEventArgs e)
        {
                    //MessageBox.Show("code : " + articleQt.codeArticle+" Quantite : "+articleQt.quantite);
            if ((txtCodeSet.Text != null && txtNomSet.Text != null && txtMontant.Text != null && txtAnneeScolaire.Text != null) && (txtCodeSet.Text != "" && txtNomSet.Text != "" && txtMontant.Text != "" && txtAnneeScolaire.Text != ""))
                    {

                        SetarticleBE setArticle = new SetarticleBE();
                        setArticle.codesetarticle = Convert.ToString(txtCodeSet.Text);
                        setArticle.nomsetarticle = Convert.ToString(txtNomSet.Text);
                        setArticle.annee = Convert.ToInt16(txtAnnee.Text);
                        setArticle.montant = Convert.ToDecimal(txtMontant.Text);

                        // on recherche si un set ayant le même code n'existe pas deja
                        if (creerSetArticleBL.rechercherSetArticle(setArticle) != null)
                            MessageBox.Show("Un set Article de code '" + setArticle.codesetarticle + "' existe deja dans le système \n \n Veuillez changer le code SVP !");
                        else
                        { //on vérifit que toutes les quantités saisis sont des entiers et qu'ils sont positif
                            bool trouve = false;
                            foreach (var row in grdListeArticle2.ItemsSource)
                            {
                                ArticleQTBE articleQt = (ArticleQTBE)row;
                                // maintenant on enregistre la composition des articles dans le set
                                ComposerBE composer = new ComposerBE();
                                composer.codeArticle = articleQt.codeArticle;
                                composer.annee = Convert.ToInt16(txtAnnee.Text);
                                composer.codeSetArticle = txtCodeSet.Text;
                                composer.quantite = articleQt.quantite;

                                // on enregistre les informations dans la table composer
                                if (composer.quantite < 0)
                                {
                                    trouve = true;
                                    break;
                                }

                            }

                            if (trouve == true) {
                                MessageBox.Show("Les quantités ne peuvent pas êtres des valeurs négatives !");
                            }
                            else if (creerSetArticleBL.creerSetArticle(setArticle))
                              { // le set Article est enregistré
                                MessageBox.Show("Enregistrement Set Article [code set : " + setArticle.codesetarticle + ", annee : " + setArticle.annee + ", nom set : " + setArticle.nomsetarticle + ", Montant : " + setArticle.montant + "] réussit !");

                                bool verif = true; // vérifi si tous les enregistrements ont été effectuées
                                foreach (var row in grdListeArticle2.ItemsSource)
                                {
                                    ArticleQTBE articleQt = (ArticleQTBE)row;
                                    // maintenant on enregistre la composition des articles dans le set
                                    ComposerBE composer = new ComposerBE();
                                    composer.codeArticle = articleQt.codeArticle;
                                    composer.annee = Convert.ToInt16(txtAnnee.Text);
                                    composer.codeSetArticle = txtCodeSet.Text;
                                    composer.quantite = articleQt.quantite;

                                    // on enregistre les informations dans la table composer
                                    if (!creerSetArticleBL.creerCompositionSetArticle(composer))
                                        verif = false;

                                }

                                if (verif == true)
                                {
                                    MessageBox.Show("Enregistrement de la composition du set réussit !");

                                    // on reinitialise les champs du formulaire
                                    //txtAnnee.Text = "";
                                    ParametresBE param = creerSetArticleBL.getParametres();
                                    if (param != null)
                                    {
                                        annee = param.annee;

                                        txtAnnee.Text = Convert.ToString(param.annee);
                                        txtAnneeScolaire.Text = (param.annee - 1).ToString();
                                    }
                                    else
                                    {
                                        txtAnnee.Text = "";
                                        txtAnneeScolaire.Text = "";
                                    }

                                    txtCodeSet.Text = "";
                                    txtMontant.Text = "";
                                    txtNomSet.Text = "";

                                    List<ArticleBE> listArticle = creerModifierArticleBL.listerTousLesArticle();
                                    RemplirDataGrid1(listArticle);

                                    ListeArticles2.Clear();
                                    grdListeArticle2.ItemsSource = ListeArticles2;
                                }
                                else
                                {
                                    MessageBox.Show("Echec Enregistrement de la composition du set !");

                                }

                            }
                            else
                                MessageBox.Show("Echec l'ors de l'enregistrement du set Article");
                        }
                    }
                    else
                        MessageBox.Show("tous les champs doivent êtres remplis !");

                
        }

        private void txtAnnee_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Utilitaires.IsTextAllowed(e.Text);
        }

        private void txtAnneeScolaire_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Utilitaires.IsTextAllowed(e.Text);
        }

        private void txtMontant_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Utilitaires.IsTextAllowed(e.Text);
        }

        private void txtAnneeScolaire_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                annee = Convert.ToInt32(txtAnneeScolaire.Text) + 1;
                txtAnnee.Text = annee.ToString();

            }
            catch (Exception)
            {
                MessageBox.Show("L'annee doit etre un nombre positif", "School brain:Alerte");
            }
        }


    }
}
