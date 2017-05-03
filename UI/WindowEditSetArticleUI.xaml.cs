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
    /// Interaction logic for WindowEditSetArticle.xaml
    /// </summary>
    public partial class WindowEditSetArticleUI : Window
    {
        CreerSetArticleBL modifierSetArticleBL;
        CreerModifierArticleBL creerModifierArticleBL;

        // Définition d'une liste 'ListeArticles' observable de 'Article'
        public ObservableCollection<ArticleBE> ListeArticles1 { get; set; }
        public ObservableCollection<ArticleQTBE> ListeArticles2 { get; set; }

        int annee;

        // retourne la liste des codes des Set Article deja enregistré (pour le filtre)
        public List<string> getListCodeSetArticle(List<SetarticleBE> listSetArticle)
        {
            List<string> listeCodeSetArticle = new List<string>();

            listeCodeSetArticle = new List<string>();
            if (listSetArticle != null)
            {
                for (int i = 0; i < listSetArticle.Count; i++)
                {
                    listeCodeSetArticle.Add(listSetArticle.ElementAt(i).codesetarticle);
                }
                return listeCodeSetArticle;
            }
            else return null;
        }

        public WindowEditSetArticleUI()
        {
            InitializeComponent();

            modifierSetArticleBL = new CreerSetArticleBL();
            creerModifierArticleBL = new CreerModifierArticleBL();

            // A mettre pour que le binding avec le DataGrid fonctionne !
            grdListeArticle1.DataContext = this;
            grdListeArticle2.DataContext = this;

            // Initialisation de la collection, qui va s'afficher dans la DataGrid :
            ListeArticles1 = new ObservableCollection<ArticleBE>();
            ListeArticles2 = new ObservableCollection<ArticleQTBE>();

            List<SetarticleBE> LSetArticle = modifierSetArticleBL.listerTousLesSetArticle();
            //lister les codes des sets Articles
            cmbCodeSet.ItemsSource = getListCodeSetArticle(LSetArticle);

            ParametresBE param = modifierSetArticleBL.getParametres();
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
            cmbCodeSet.Text = null;
            //txtAnnee.Text = "";
            ParametresBE param = modifierSetArticleBL.getParametres();
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

            txtMontant.Text = "";
            txtNomSet.Text = "";
            ListeArticles1.Clear();
            ListeArticles2.Clear();
            grdListeArticle1.ItemsSource = ListeArticles1;
            grdListeArticle2.ItemsSource = ListeArticles2;

            List<SetarticleBE> LSetArticle = modifierSetArticleBL.listerTousLesSetArticle();
            //lister les codes des sets Articles
            cmbCodeSet.ItemsSource = getListCodeSetArticle(LSetArticle);
        }

        private void cmbCodeSet_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // on récupère le code du Set choisi
            String codeSetArticle = Convert.ToString(cmbCodeSet.SelectedItem);
            //on recherche le set dans la BD
            if (modifierSetArticleBL.listerSetArticleSuivantCritere("codesetarticle = '" + codeSetArticle + "'").Count != 0)
            {
                SetarticleBE setArticle = modifierSetArticleBL.listerSetArticleSuivantCritere("codesetarticle = '" + codeSetArticle + "'")[0];
                //on charge les informations du set dans le formulaire
                txtNomSet.Text = setArticle.nomsetarticle;
                txtAnnee.Text = Convert.ToString(setArticle.annee);
                txtMontant.Text = Convert.ToString(setArticle.montant).Split(',')[0];
            }

            //on recherche la composition du set Article
            List<ComposerBE> LCompser = modifierSetArticleBL.listerComposerSuivantCrietere("codesetarticle ='" + codeSetArticle + "'");
            //on charge les articles obtenu dans la datagrid2
            List<ArticleQTBE> LArticleQt = new List<ArticleQTBE>();
            ListeArticles2.Clear();
            for (int i = 0; i < LCompser.Count; i++) {
                ArticleQTBE articleQt = new ArticleQTBE();
                ComposerBE composer = LCompser.ElementAt(i);
                List<ArticleBE> listArticle = creerModifierArticleBL.listerArticleSuivantCritere("codearticle ='"+composer.codeArticle+"'");
                if(listArticle != null && listArticle.Count != 0){
                    ArticleBE article = listArticle.ElementAt(0);
                    articleQt.codeArticle = article.codeArticle;
                    articleQt.codeCatArticle = article.codeCatArticle;
                    articleQt.designation = article.designation;
                    articleQt.quantite = composer.quantite; 
                }
                

                ListeArticles2.Add(articleQt);
            }

            grdListeArticle2.ItemsSource = ListeArticles2;

            //on charge le reste d'article dans le datagrid1
            List<ArticleBE> LArticle = creerModifierArticleBL.listerTousLesArticle();
            ListeArticles1.Clear();
            for (int i = 0; i < LArticle.Count; i++) {
                if (modifierSetArticleBL.listerComposerSuivantCrietere("codearticle ='" + LArticle.ElementAt(i).codeArticle + "' AND codesetarticle ='" + codeSetArticle + "'") != null)
                {
                    if (modifierSetArticleBL.listerComposerSuivantCrietere("codearticle ='" + LArticle.ElementAt(i).codeArticle + "' AND codesetarticle ='" + codeSetArticle + "'").Count == 0)
                        ListeArticles1.Add(LArticle.ElementAt(i));
                }
            }

            grdListeArticle1.ItemsSource = ListeArticles1;
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
            if ((cmbCodeSet.Text != null && txtNomSet.Text != null && txtMontant.Text != null && txtAnneeScolaire.Text != null) && (cmbCodeSet.Text != "" && txtNomSet.Text != "" && txtMontant.Text != "" && txtAnneeScolaire.Text != ""))
            {

                SetarticleBE setArticle = new SetarticleBE();
                setArticle.codesetarticle = Convert.ToString(cmbCodeSet.Text);
                setArticle.nomsetarticle = Convert.ToString(txtNomSet.Text);
                setArticle.annee = Convert.ToInt16(txtAnnee.Text);
                setArticle.montant = Convert.ToDecimal(txtMontant.Text);

                //on vérifit que toutes les quantités saisis sont des entiers et qu'ils sont positif
                bool trouve = false;
                foreach (var row in grdListeArticle2.ItemsSource)
                {
                    ArticleQTBE articleQt = (ArticleQTBE)row;
                    // maintenant on enregistre la composition des articles dans le set
                    ComposerBE composer = new ComposerBE();
                    composer.codeArticle = articleQt.codeArticle;
                    composer.annee = Convert.ToInt16(txtAnnee.Text);
                    composer.codeSetArticle = cmbCodeSet.Text;
                    composer.quantite = articleQt.quantite;

                    // on enregistre les informations dans la table composer
                    if (composer.quantite < 0)
                    {
                        trouve = true;
                        break;
                    }

                }

                if (trouve == true)
                {
                    MessageBox.Show("Les quantités ne peuvent pas êtres des valeurs négatives !");
                }
                else if (modifierSetArticleBL.modifierSetArticle(setArticle))
                { // le set Article est enregistré
                    MessageBox.Show("Enregistrement Set Article [code set : " + setArticle.codesetarticle + ", annee : " + setArticle.annee + ", nom set : " + setArticle.nomsetarticle + ", Montant : " + setArticle.montant + "] réussit !");

                    //on supprime l'ancienne composition du set
                    List<ComposerBE> LComposer = modifierSetArticleBL.listerComposerSuivantCrietere("codesetarticle ='"+setArticle.codesetarticle+"'");
                    for (int i = 0; i < LComposer.Count; i++)
                        modifierSetArticleBL.supprimerComposer(LComposer.ElementAt(i));

                    bool verif = true; // vérifi si tous les enregistrements ont été effectuées
                    foreach (var row in grdListeArticle2.ItemsSource)
                    {
                        ArticleQTBE articleQt = (ArticleQTBE)row;
                        // maintenant on enregistre la composition des articles dans le set
                        ComposerBE composer = new ComposerBE();
                        composer.codeArticle = articleQt.codeArticle;
                        composer.annee = Convert.ToInt16(txtAnnee.Text);
                        composer.codeSetArticle = cmbCodeSet.Text;
                        composer.quantite = articleQt.quantite;

                        // on enregistre les informations dans la table composer
                        if (!modifierSetArticleBL.creerCompositionSetArticle(composer))
                            verif = false;

                    }

                    if (verif == true)
                    {
                        MessageBox.Show("Enregistrement de la composition du set réussit !");

                        cmbCodeSet.Text = null;
                        //txtAnnee.Text = "";
                        ParametresBE param = modifierSetArticleBL.getParametres();
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

                        txtMontant.Text = "";
                        txtNomSet.Text = "";
                        ListeArticles1.Clear();
                        ListeArticles2.Clear();
                        grdListeArticle1.ItemsSource = ListeArticles1;
                        grdListeArticle2.ItemsSource = ListeArticles2;

                        List<SetarticleBE> LSetArticle = modifierSetArticleBL.listerTousLesSetArticle();
                        //lister les codes des sets Articles
                        cmbCodeSet.ItemsSource = getListCodeSetArticle(LSetArticle);
                    }
                    else
                    {
                        MessageBox.Show("Echec mise à jour de la composition du set !");

                    }

                }
                else
                    MessageBox.Show("Echec l'ors de la mise à jour du set Article");
            }
            else
                MessageBox.Show("tous les champs doivent êtres remplis !");
        }

        private void txtAnnee_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Utilitaires.IsTextAllowed(e.Text);
        }

        private void txtMontant_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Utilitaires.IsTextAllowed(e.Text);
        }

        private void cmdSupprimer_Click(object sender, RoutedEventArgs e)
        {
            if (cmbCodeSet.Text != null && cmbCodeSet.Text != "")
            {
                if (MessageBox.Show("Voulez-vous vraiment le supprimer ? ", "School : Confimation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    SetarticleBE setArticle = new SetarticleBE();
                    setArticle.codesetarticle = cmbCodeSet.Text;
                    if (modifierSetArticleBL.supprinerSetArticle(setArticle))
                    {
                        MessageBox.Show("Set Article supprimé ! ");

                        //on met à jour le formulaire
                        cmbCodeSet.Text = null;
                        //txtAnnee.Text = "";
                        ParametresBE param = modifierSetArticleBL.getParametres();
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

                        txtMontant.Text = "";
                        txtNomSet.Text = "";
                        ListeArticles1.Clear();
                        ListeArticles2.Clear();
                        grdListeArticle1.ItemsSource = ListeArticles1;
                        grdListeArticle2.ItemsSource = ListeArticles2;

                        List<SetarticleBE> LSetArticle = modifierSetArticleBL.listerTousLesSetArticle();
                        //lister les codes des sets Articles
                        cmbCodeSet.ItemsSource = getListCodeSetArticle(LSetArticle);
                        
                        //fin de mise à jour du formulaire
                    }
                    else
                        MessageBox.Show("Echec de suppression du Set Article ! ");

                }
            }
            else MessageBox.Show("Vous devez dabor choisir le Set à supprimer !");
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

        private void txtAnneeScolaire_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Utilitaires.IsTextAllowed(e.Text);
        }

    }
}
