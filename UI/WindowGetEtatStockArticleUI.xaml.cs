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

using System.Globalization;
using System.Threading;

using Ecole.BusinessEntity;
using Ecole.BusinessLogic;
using Ecole.Utilitaire;

namespace Ecole.UI
{
    /// <summary>
    /// Interaction logic for WindowGetEtatStockArticle.xaml
    /// </summary>
    public partial class WindowGetEtatStockArticleUI : Window
    {
        ConsulterEtatStockBL consulterEtatStockBL;

        string codeCatArticleChoisi; //sera utile pour la génération des états
        string codeArticleChoisi; //sera utile pour la génération des états

        // Définition d'une liste 'ListeStocks' observable de 'Stock'
        public ObservableCollection<EtatStockBE> ListeEtatStocks { get; set; }

        // Fonction permettant de remplir le DataGrid avec les informations de la base de données
        // @param : - listObjet : la liste des objets à afficher dans le DataGrid
        public void RemplirDataGrid(List<EtatStockBE> listObjet)
        {
            // Ajout de données dans la DataTable :
            var table = new DataTable();

            table.Columns.Add(new DataColumn("numero", typeof(string)));
            table.Columns.Add(new DataColumn("codeArticle", typeof(string)));
            table.Columns.Add(new DataColumn("codeMagasin", typeof(string)));
            table.Columns.Add(new DataColumn("stockDebut", typeof(string)));
            table.Columns.Add(new DataColumn("quantiteAchetee", typeof(string)));
            table.Columns.Add(new DataColumn("quantiteVendue", typeof(string)));
            table.Columns.Add(new DataColumn("dateOperation", typeof(DateTime)));
            table.Columns.Add(new DataColumn("dateOperationString", typeof(string)));
            table.Columns.Add(new DataColumn("annee", typeof(string)));
            table.Columns.Add(new DataColumn("puArticle", typeof(string)));
            table.Columns.Add(new DataColumn("stockRestant", typeof(string)));

            table.Columns.Add(new DataColumn("designationArticle", typeof(string)));

            if (listObjet != null)
            {

                for (int i = 0; i < listObjet.Count; i++)
                {
                    DataRow dr = table.NewRow();
                    dr["numero"] = listObjet.ElementAt(i).numero;
                    dr["codeArticle"] = listObjet.ElementAt(i).codeArticle;
                    dr["codeMagasin"] = listObjet.ElementAt(i).codeMagasin;
                    dr["stockDebut"] = listObjet.ElementAt(i).stockDebut;
                    dr["quantiteAchetee"] = listObjet.ElementAt(i).quantiteAchetee;
                    dr["quantiteVendue"] = listObjet.ElementAt(i).quantiteVendue;
                    dr["dateOperation"] = listObjet.ElementAt(i).dateOperation;
                    dr["dateOperationString"] = listObjet.ElementAt(i).dateOperation.ToShortDateString();
                    dr["annee"] = listObjet.ElementAt(i).annee;
                    dr["puArticle"] = listObjet.ElementAt(i).puArticle;
                    dr["stockRestant"] = listObjet.ElementAt(i).stockRestant;

                    /*ArticleBE article = new ArticleBE();
                    article.codeArticle = listObjet.ElementAt(i).codeArticle;
                    article = consulterEtatStockBL.rechercherArticle(article);
                    dr["designationArticle"] = article.designation;*/
                    dr["designationArticle"] = listObjet.ElementAt(i).designationArticle;

                    table.Rows.Add(dr);
                }
            }

            string vNumero = "";
            string vcodeArticle = "";
            string vcodeMagasin = "";
            int vStockDebut = 0;
            int vQuantiteAchetee = 0;
            int vQuantiteVendue = 0;
            DateTime vDateOperation = new DateTime();
            string vDateOperationString = "";
            int vAnnee = 0;
            int vPuarticle = 0;
            int vStockRestant = 0;

            string vDesignationArticle = "";

            ListeEtatStocks.Clear();

            //Personnes_Table = LoadDataTable();

            foreach (DataRow row in table.Rows)
            {
                vNumero = Convert.ToString(row["numero"]);
                vcodeArticle = Convert.ToString(row["codeArticle"]);
                vcodeMagasin = Convert.ToString(row["codeMagasin"]);
                vStockDebut = Convert.ToInt16(row["stockDebut"]);
                vQuantiteAchetee = Convert.ToInt16(row["quantiteAchetee"]);
                vQuantiteVendue = Convert.ToInt16(row["quantiteVendue"]);
                vDateOperation = Convert.ToDateTime(row["dateOperation"]);
                vDateOperationString = Convert.ToString(row["dateOperationString"]);
                vAnnee = Convert.ToInt16(row["annee"]);
                vPuarticle = Convert.ToInt16(row["puArticle"]);
                vStockRestant = Convert.ToInt16(row["stockRestant"]);
                vDesignationArticle = Convert.ToString(row["designationArticle"]);

                EtatStockBE etatStock = new EtatStockBE();
                etatStock.numero = Convert.ToInt16(vNumero);
                etatStock.codeMagasin = vcodeMagasin;
                etatStock.codeArticle = vcodeArticle;
                etatStock.stockDebut = Convert.ToInt16(vStockDebut);
                etatStock.quantiteAchetee = Convert.ToInt16(vQuantiteAchetee);
                etatStock.quantiteVendue = Convert.ToInt16(vQuantiteVendue);
                etatStock.dateOperation = vDateOperation;
                etatStock.dateOperationString = vDateOperationString;
                etatStock.annee = vAnnee;
                etatStock.puArticle = Convert.ToInt16(vPuarticle);
                etatStock.stockRestant = Convert.ToInt16(vStockRestant);

                etatStock.designationArticle = vDesignationArticle;

                ListeEtatStocks.Add(etatStock);

            }
        }


        public WindowGetEtatStockArticleUI()
        {
            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";
            Thread.CurrentThread.CurrentCulture = ci;

            InitializeComponent();

            consulterEtatStockBL = new ConsulterEtatStockBL();

            codeArticleChoisi = "<Tous les Articles>";
            codeCatArticleChoisi = "<Toutes les Catégories>";

            List<CategorieArticleBE> LCatArticle = consulterEtatStockBL.listerToutesLesCategoriesArticles();
            cmbCategorieArticle.ItemsSource = consulterEtatStockBL.getListCodeCategorieArticle(LCatArticle);

            // A mettre pour que le binding avec le DataGrid fonctionne !
            GrdListEtatStock.DataContext = this;

            // Initialisation de la collection, qui va s'afficher dans la DataGrid :
            ListeEtatStocks = new ObservableCollection<EtatStockBE>();

            //on liste tous les EtatStockBE
            List<EtatStockBE> LEtatStockBE = consulterEtatStockBL.getEtatStock();

            ////pour chaque article du stock on recherche la quantité vendu
            //List<EtatStockBE> LEtatStockBE = new List<EtatStockBE>();

            // on met la liste "LStockBE" dans le DataGrid
            //RemplirDataGrid(LStockBE);
            GrdListEtatStock.ItemsSource = LEtatStockBE;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cmdAnnuler_Click(object sender, RoutedEventArgs e)
        {
            cmbCategorieArticle.Text = null;
            cmbArticle.Text = null;
            cmbArticle.ItemsSource = null;

            GrdListEtatStock.ItemsSource = null;

            codeArticleChoisi = "";
            codeCatArticleChoisi = "";
        }


        private void cmbCategorieArticle_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // on filtre la liste affiché dans le DataGrid en fonction du choix de l'utilisateur
            String codeCatArticle = Convert.ToString(cmbCategorieArticle.SelectedItem);
            List<ArticleBE> LArticleBE;
            if (codeCatArticle.Equals("<Toutes Les Catégories>"))
                LArticleBE = consulterEtatStockBL.listerTousLesArticles();
            else
                LArticleBE = consulterEtatStockBL.listerArticleSuivantCritere("codecatarticle = '" + codeCatArticle + "'");

            cmbArticle.ItemsSource = consulterEtatStockBL.getListCodeArticle(LArticleBE);

            ////on rafraichir le DataGrid
            //RemplirDataGrid(LSerieBE);
        }

        private void cmdValider_Click(object sender, RoutedEventArgs e)
        {
            if ((cmbArticle.Text != null && cmbCategorieArticle.Text != null) && (cmbArticle.Text != "" && cmbArticle.Text != ""))
            { // si tous les champs sont non vides

                codeCatArticleChoisi = cmbCategorieArticle.Text;
                codeArticleChoisi = cmbArticle.Text;

                List<EtatStockBE> LEtatStockBE = new List<EtatStockBE>();

                if (cmbCategorieArticle.Text.Equals("<Toutes Les Catégories>") && cmbArticle.Text.Equals("<Tous Les Articles>"))
                {
                    //on liste tous les Stocks
                    LEtatStockBE = consulterEtatStockBL.getEtatStock();

                }
                else if (cmbCategorieArticle.Text.Equals("<Toutes Les Catégories>"))
                {
                    // on liste les Stocks de l'article 
                    LEtatStockBE = consulterEtatStockBL.etatStockSuivantCodeArticle(cmbArticle.Text);

                }
                else if (cmbArticle.Text.Equals("<Tous Les Articles>"))
                {
                    // on liste les articles de la catégorie 
                    List<ArticleBE> LArticles = consulterEtatStockBL.listerArticleSuivantCritere("codecatarticle = '" + cmbCategorieArticle.Text + "'");
                    //on liste les stock des articles obtenus
                    if (LArticles != null)
                    {
                        for (int i = 0; i < LArticles.Count; i++)
                        {
                            List<EtatStockBE> list = consulterEtatStockBL.etatStockSuivantCodeArticle(LArticles.ElementAt(i).codeArticle);

                            for (int j = 0; j < list.Count; j++)
                            {
                                LEtatStockBE.Add(list.ElementAt(j));
                            }

                        }

                    }
                    //LStockBE = consulterEtatStockBL.listerStocksSuivantCritere("codemagasin = '" + cmbArticle.Text + "'");
                }
                else
                {
                    // on liste les Stocks de l'article 
                    LEtatStockBE = consulterEtatStockBL.etatStockSuivantCodeArticle(cmbArticle.Text);

                }


                // on met la liste "LStockBE" dans le DataGrid
                //RemplirDataGrid(LStockBE);
                GrdListEtatStock.ItemsSource = LEtatStockBE;

                cmbCategorieArticle.Text = null;
                cmbArticle.Text = null;
                cmbArticle.ItemsSource = null;
            }
            else MessageBox.Show("Tous les champs doivent êtres remplis !");
        }

        private void cmbArticle_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //// on filtre la liste affiché dans le DataGrid en fonction du choix de l'utilisateur
            //String codeArticle = Convert.ToString(cmbArticle.SelectedItem);
            //List<StockerBE> LStockerBE;
            //List<EtatStockBE> LEtatStockBE = new List<EtatStockBE>();
            //if (codeArticle.Equals("<Tous Les Articles>"))
            //    LStockerBE = consulterEtatStockBL.listerTousLesStocks();
            //else
            //    LStockerBE = consulterEtatStockBL.listerStocksSuivantCritere("codearticle = '" + codeArticle + "'");

            //for (int i = 0; i < LStockerBE.Count; i++) {
            //    EtatStockBE etatStock = new EtatStockBE();
            //    etatStock.codearticle = LStockerBE.ElementAt(i).codearticle;
            //    etatStock.codemagasin = LStockerBE.ElementAt(i).codemagasin;
            //    etatStock.quantiteachetee = LStockerBE.ElementAt(i).quantiteachetee;
            //    etatStock.dateachat = LStockerBE.ElementAt(i).dateachat;
            //    etatStock.datefin = LStockerBE.ElementAt(i).datefin;
            //    etatStock.puarticle = LStockerBE.ElementAt(i).puarticle;
            //    etatStock.dateperemtion = LStockerBE.ElementAt(i).dateperemtion;
            //    //etatStock.quantitevendu = Convert.ToString(consulterEtatStockBL.GetQuantiteVenduArticle(etatStock.codearticle, Convert.ToInt16(txtAnnee.Text)));
            //    //etatStock.quantiterestante = Convert.ToString(Convert.ToInt16(etatStock.quantiteachetee) - Convert.ToInt16(etatStock.quantitevendu));
            //    etatStock.quantiterestante = LStockerBE.ElementAt(i).stock;
            //    etatStock.quantitevendu = LStockerBE.ElementAt(i).quantitecumulee - etatStock.quantiterestante;
            //    etatStock.quantitecumulee = LStockerBE.ElementAt(i).quantitecumulee;

            //    LEtatStockBE.Add(etatStock);
            //}

            //GrdListEtatStock.ItemsSource = null;
            //GrdListEtatStock.ItemsSource = LEtatStockBE;

            //////on rafraichir le DataGrid
            ////RemplirDataGrid(LSerieBE);
        }

        private void cmdFermer_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cmdImprimer_Click(object sender, RoutedEventArgs e)
        {
            CreerEtat etat = new CreerEtat("Etat Stock -" + DateTime.Today.ToShortDateString(), "Etat des stocks \n Date : " + Convert.ToString(System.DateTime.Today.ToShortDateString()));
            etat.obtenirEtatModePaysage(GrdListEtatStock);
        }

        private void txtFilterCodeArticle_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                List<EtatStockBE> LEtatStockBE = new List<EtatStockBE>();
                if(txtFilterCodeArticle.Text.Equals(""))
                    LEtatStockBE = consulterEtatStockBL.getEtatStock();
                else
                    LEtatStockBE = consulterEtatStockBL.etatStockSuivantCodeArticle(txtFilterCodeArticle.Text);

                // on met la liste "LStockBE" dans le DataGrid
                //RemplirDataGrid(LStockBE);
                GrdListEtatStock.ItemsSource = LEtatStockBE;

            }

        }



        private void datePickerFilterDateAchat_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                List<EtatStockBE> LEtatStockBE = new List<EtatStockBE>();
                LEtatStockBE = consulterEtatStockBL.etatStockSuivantDateOperation(Convert.ToDateTime(datePickerFilterDateAchat.Text));

                // on met la liste "LStockBE" dans le DataGrid
                //RemplirDataGrid(LStockBE);
                GrdListEtatStock.ItemsSource = LEtatStockBE;

            }
        }

        private void datePickerFilterDateAchat_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (datePickerFilterDateAchat.SelectedDate != null && ! datePickerFilterDateAchat.SelectedDate.Equals(""))
            {
                List<EtatStockBE> LEtatStockBE = new List<EtatStockBE>();
                LEtatStockBE = consulterEtatStockBL.etatStockSuivantDateOperation(Convert.ToDateTime(datePickerFilterDateAchat.SelectedDate));

                // on met la liste "LStockBE" dans le DataGrid
                //RemplirDataGrid(LStockBE);
                GrdListEtatStock.ItemsSource = LEtatStockBE;
            }

            

        }
    }
}
