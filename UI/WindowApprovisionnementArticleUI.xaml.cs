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
    /// Interaction logic for Window4.xaml
    /// </summary>
    public partial class WindowApprovisionnementArticleUI : Window
    {
        ApprovisionnementArticleBL approvisionnementArticleBL;
        CreerModifierArticleBL creerModifierArticleBL;
        CreerModifierMagasinBL creerModifierMagasinBL;

        String articleChoisi; //sera utile pour la génération des états
        String magasinChoisi; //sera utile pour la génération des états

        int annee;

        StockerBE ancienObjet;

        private int etat;

        // Définition d'une liste 'ListeApprovisionnements' observable de 'Stock'
        public ObservableCollection<StockerBE> ListeApprovisionnements { get; set; }

        // Fonction permettant de remplir le DataGrid avec les informations de la base de données
        // @param : - listObjet : la liste des objets à afficher dans le DataGrid
        public void RemplirDataGrid(List<StockerBE> listObjet)
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

            ListeApprovisionnements.Clear();

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

                StockerBE stock = new StockerBE();
                stock.numero = Convert.ToInt16(vNumero);
                stock.codeMagasin = vcodeMagasin;
                stock.codeArticle = vcodeArticle;
                stock.stockDebut = Convert.ToInt16(vStockDebut);
                stock.quantiteAchetee = Convert.ToInt16(vQuantiteAchetee);
                stock.quantiteVendue = Convert.ToInt16(vQuantiteVendue);
                stock.dateOperation = vDateOperation;
                stock.dateOperationString = vDateOperationString;
                stock.annee = vAnnee;
                stock.puArticle = Convert.ToInt16(vPuarticle);
                stock.stockRestant = Convert.ToInt16(vStockRestant);

                ListeApprovisionnements.Add(stock);

            }
        }

        
        public WindowApprovisionnementArticleUI()
        {
            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";
            Thread.CurrentThread.CurrentCulture = ci;

            InitializeComponent();

            approvisionnementArticleBL = new ApprovisionnementArticleBL();

            articleChoisi = "<Tous les Articles>";
            magasinChoisi = "<Tous les Magasins>";

            // A mettre pour que le binding avec le DataGrid fonctionne !
            grdListeApprovisionnement.DataContext = this;

            etat = 0;

            ancienObjet = new StockerBE();

            // Initialisation de la collection, qui va s'afficher dans la DataGrid :
            ListeApprovisionnements = new ObservableCollection<StockerBE>();
            List<StockerBE> LStockBE = approvisionnementArticleBL.listerToutesLesStock();
            // on met la liste "LStockBE" dans le DataGrid
            RemplirDataGrid(LStockBE);

            // ------------------- Chargement de la liste des codes de Cycle dans le comboBox de la fenêtre 
            //(utile pour le filtre)
            creerModifierArticleBL = new CreerModifierArticleBL();
            List<ArticleBE> LArticleBE = creerModifierArticleBL.listerTousLesArticle();
            cmbArticle.ItemsSource = approvisionnementArticleBL.getListCodeArticle2(LArticleBE);

            cmbFilterArticle.ItemsSource = approvisionnementArticleBL.getListCodeArticle(LArticleBE);

            txtAnnee.Text = Convert.ToString(approvisionnementArticleBL.getAnneeEnCours());

           annee = approvisionnementArticleBL.getAnneeEnCours();
           txtAnnee.Text = Convert.ToString(annee);
           txtAnneeScolaire.Text = (annee - 1).ToString();
          

            // ------------------- Chargement de la liste des codes de Magasin dans le comboBox de la fenêtre 
            //(utile pour le filtre)
            creerModifierMagasinBL = new CreerModifierMagasinBL();
            List<MagasinBE> LMagasinBE = creerModifierMagasinBL.listerToutesLesMagasin();
            cmbMagasin.ItemsSource = approvisionnementArticleBL.getListCodeMagasin2(LMagasinBE);

            cmbFilterMagasin.ItemsSource = approvisionnementArticleBL.getListCodeMagasin(LMagasinBE);

        }

        private void cmdFermer_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cmdValider_Click(object sender, RoutedEventArgs e)
        {
            if ((cmbArticle.Text != null && cmbMagasin.Text != null && txtQuantite.Text != null && txtPrixUnitaire.Text != null && txtAnneeScolaire.Text != null)
                && (cmbArticle.Text != "" && cmbMagasin.Text != "" && txtQuantite.Text != "" && txtPrixUnitaire.Text != "" && txtAnneeScolaire.Text != ""))
            { // si tous les champs sont non vides
                StockerBE stock = new StockerBE();

                stock.annee = Convert.ToInt16(txtAnnee.Text);

                //on teste si il ya aucun enregistrement dans la table stocker
                if (approvisionnementArticleBL.tableStokerIsEmpty())
                {
                    //alors c'est le premier enregistrement
                    //stock.numero = Convert.ToInt16(vNumero);
                    stock.codeMagasin = cmbMagasin.Text;
                    stock.codeArticle = cmbArticle.Text;
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
                    StockerBE dernierStocker = approvisionnementArticleBL.dernierEnregistrementStocker(cmbArticle.Text, cmbMagasin.Text);

                    if (dernierStocker != null)
                    {
                        //le stock de début est égales au stock de début du dernier enregistrement du même article
                        //on ajoute ajute la quantité acheté au dernier stock restant
                        //stock.numero = Convert.ToInt16(vNumero);
                        stock.codeMagasin = cmbMagasin.Text;
                        stock.codeArticle = cmbArticle.Text;

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
                    else {
                        //le stock de début est égale à 0
                        //le stock restant est égale à la quantité achetée
                        
                        //alors c'est le premier enregistrement
                        //stock.numero = Convert.ToInt16(vNumero);
                        stock.codeMagasin = cmbMagasin.Text;
                        stock.codeArticle = cmbArticle.Text;
                        stock.stockDebut = 0;
                        stock.quantiteAchetee = Convert.ToInt16(txtQuantite.Text);
                        stock.quantiteVendue = 0;
                        //DatePicker dpk = new DatePicker();
                        //dpk.Text = Convert.ToString(System.DateTime.Today.Date);
                        stock.dateOperation = System.DateTime.Today.Date;
                        //stock.dateOperationString = System.DateTime.Today.ToShortDateString;
                        stock.annee = Convert.ToInt32(txtAnnee.Text);
                        stock.puArticle = Convert.ToInt32(txtPrixUnitaire.Text);
                        stock.stockRestant = Convert.ToInt32(txtQuantite.Text);
                    }

                }


                if (etat == 1)
                {
                    // on retire dans l'ancien objet
                    StockerBE newStock = new StockerBE();

                    //stock.numero = Convert.ToInt16(vNumero);
                    newStock.codeMagasin = stock.codeMagasin;
                    newStock.codeArticle = stock.codeArticle;
                    newStock.stockDebut = stock.stockDebut;
                    newStock.quantiteAchetee = stock.quantiteAchetee;
                    newStock.quantiteVendue = stock.quantiteVendue;
                    //DatePicker dpk = new DatePicker();
                    //dpk.Text = Convert.ToString(System.DateTime.Today.Date);
                    newStock.dateOperation = stock.dateOperation;
                    //stock.dateOperationString = System.DateTime.Today.ToShortDateString;
                    newStock.annee = stock.annee;
                    newStock.puArticle = stock.puArticle;
                    newStock.stockRestant = stock.stockRestant;

                    if (stock.quantiteAchetee > ancienObjet.quantiteAchetee)
                    { // il a augmenté la quantité acheté
                        newStock.quantiteAchetee = ancienObjet.quantiteAchetee + (stock.quantiteAchetee - ancienObjet.quantiteAchetee);
                        newStock.stockRestant = ancienObjet.stockRestant + (stock.quantiteAchetee - ancienObjet.quantiteAchetee);
                        
                    }
                    else
                    { //si on a diminué la quantité
                        newStock.quantiteAchetee = ancienObjet.quantiteAchetee - (ancienObjet.quantiteAchetee - stock.quantiteAchetee);
                        newStock.stockRestant = ancienObjet.stockRestant - (ancienObjet.quantiteAchetee - stock.quantiteAchetee);

                        }

                    approvisionnementArticleBL.modifierStock(ancienObjet, newStock);
                    /*List<StockerBE> LStockerBE = approvisionnementArticleBL.listerToutesLesStock();
                    // on met la liste "LStockerBE" dans le DataGrid
                    RemplirDataGrid(LStockerBE);*/

                    // ------------------- Chargement de la liste des codes de série dans le comboBox de la fenêtre 
                    //(utile pour le filtre)
                    // cmbFilterCode.ItemsSource = getListCodeSerie(LSerieBE);

                    // ------------------- Chargement de la liste des noms de série dans le comboBox de la fenêtre 
                    //(utile pour le filtre)
                    //cmbFilterNom.ItemsSource = getListNomSerie(LSerieBE);

                    cmbArticle.Text = null;
                    cmbMagasin.Text = null;
                    txtQuantite.Text = "";
                    txtPrixUnitaire.Text = "";

                    etat = 0;
                }
                else
                {
                    approvisionnementArticleBL.creerStock(stock);

                    

                }

                cmbArticle.Text = "";
                cmbMagasin.Text = "";
                txtQuantite.Text = "";
                txtPrixUnitaire.Text = "";
                
                annee = approvisionnementArticleBL.getAnneeEnCours();
                txtAnnee.Text = Convert.ToString(annee);
                txtAnneeScolaire.Text = (annee - 1).ToString();
                //dpkDatePeremption.Text = "";

                etat = 0;

                //on liste tous les stocks
                List<StockerBE> LStockerBE = approvisionnementArticleBL.listerToutesLesStock();
                RemplirDataGrid(LStockerBE);

            }
            else MessageBox.Show("Tous les champs marqués par un Astérix '(*)' doivent être remplis !");
            

        }

        private void cmbFilterArticle_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // on filtre la liste affiché dans le DataGrid en fonction du choix de l'utilisateur
            articleChoisi = Convert.ToString(cmbFilterArticle.SelectedItem);

            List<StockerBE> LStockBE;
            if (cmbFilterMagasin.Text != null && cmbFilterMagasin.Text != "")
            {
                
                magasinChoisi = cmbFilterMagasin.Text;

                if (cmbFilterArticle.SelectedItem.Equals("<Tous les Articles>"))
                {
                    if (cmbFilterMagasin.SelectedItem.Equals("<Tous les Magasins>"))
                        LStockBE = approvisionnementArticleBL.listerToutesLesStock();
                    else
                        LStockBE = approvisionnementArticleBL.listerStockSuivantCritere("codemagasin = '" + cmbFilterMagasin.SelectedItem + "'");
                }
                else
                    if (cmbFilterMagasin.SelectedItem.Equals("<Tous les Magasins>"))
                        LStockBE = approvisionnementArticleBL.listerStockSuivantCritere("codearticle = '" + cmbFilterArticle.SelectedItem + "'");
                    else
                        LStockBE = approvisionnementArticleBL.listerStockSuivantCritere("codearticle = '" + cmbFilterArticle.SelectedItem + "' AND codemagasin = '" + cmbFilterMagasin.SelectedItem + "'");
            }
            else
                if (cmbFilterArticle.SelectedItem.Equals("<Tous les Articles>"))
                    LStockBE = approvisionnementArticleBL.listerToutesLesStock();
                else
                    LStockBE = approvisionnementArticleBL.listerStockSuivantCritere("codearticle = '" + cmbFilterArticle.SelectedItem + "'");

            //on rafraichir le DataGrid
            RemplirDataGrid(LStockBE);
        }

        private void cmbFilterMagasin_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            magasinChoisi = Convert.ToString(cmbFilterMagasin.SelectedItem);

            // on filtre la liste affiché dans le DataGrid en fonction du choix de l'utilisateur
            List<StockerBE> LStockBE;
            if (cmbFilterArticle.Text != null && cmbFilterArticle.Text != "")
            {
                articleChoisi = cmbFilterArticle.Text;

                if (cmbFilterMagasin.SelectedItem.Equals("<Tous les Magasins>"))
                {
                    if (cmbFilterArticle.SelectedItem.Equals("<Tous les Articles>"))
                        LStockBE = approvisionnementArticleBL.listerToutesLesStock();
                    else
                        LStockBE = approvisionnementArticleBL.listerStockSuivantCritere("codearticle = '" + cmbFilterArticle.SelectedItem + "'");
                }
                else
                    if (cmbFilterArticle.SelectedItem.Equals("<Tous les Articles>"))
                        LStockBE = approvisionnementArticleBL.listerStockSuivantCritere("codemagasin = '" + cmbFilterMagasin.SelectedItem + "'");
                    else
                        LStockBE = approvisionnementArticleBL.listerStockSuivantCritere("codearticle = '" + cmbFilterArticle.SelectedItem + "' AND codemagasin = '" + cmbFilterMagasin.SelectedItem + "'");
            }
            else
                if (cmbFilterMagasin.SelectedItem.Equals("<Tous les Magasins>"))
                    LStockBE = approvisionnementArticleBL.listerToutesLesStock();
                else
                    LStockBE = approvisionnementArticleBL.listerStockSuivantCritere("codemagasin = '" + cmbFilterMagasin.SelectedItem + "'");

            //on rafraichir le DataGrid
            RemplirDataGrid(LStockBE);
        }

        private void cmdAnnuler_Click(object sender, RoutedEventArgs e)
        {
            cmbArticle.Text = "";
            cmbMagasin.Text = "";
            txtQuantite.Text = "";
            txtPrixUnitaire.Text = "";

            annee = approvisionnementArticleBL.getAnneeEnCours();
            txtAnnee.Text = Convert.ToString(annee);
            txtAnneeScolaire.Text = (annee - 1).ToString();

            //dpkDatePeremption.Text = "";
           
            etat = 0;
        }

        private void grdListeApprovisionnement_KeyUp(object sender, KeyEventArgs e)
        {//NB: la supression d'un approvisionnement ne se fait que si cet article n'a pas encor été vendu
            if (e.Key == Key.Delete)
            {
                if (grdListeApprovisionnement.SelectedIndex != -1)
                {
                    if (grdListeApprovisionnement.SelectedIndex == ListeApprovisionnements.Count - 1)
                    {
                        if (approvisionnementArticleBL.ADejaEteVendu(ListeApprovisionnements.ElementAt(grdListeApprovisionnement.SelectedIndex).codeArticle))
                        {
                            MessageBox.Show("Désolé, vous ne pouvez pas supprimer cet approvisionnement car des articles de ce type ont deja été vendu !");
                        }
                        else
                        {
                            if (MessageBox.Show("Voulez-vous vraiment le supprimer ? ", "School : Confimation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                            {

                                if (approvisionnementArticleBL.supprinerStock(ListeApprovisionnements.ElementAt(grdListeApprovisionnement.SelectedIndex)))
                                    ListeApprovisionnements.RemoveAt(grdListeApprovisionnement.SelectedIndex);
                                grdListeApprovisionnement.ItemsSource = ListeApprovisionnements;

                                // ------------------- Chargement de la liste des codes de série dans le comboBox de la fenêtre 
                                //(utile pour le filtre)
                                List<MagasinBE> LMagasinBE = creerModifierMagasinBL.listerToutesLesMagasin();
                                cmbFilterMagasin.ItemsSource = approvisionnementArticleBL.getListCodeMagasin(LMagasinBE);

                                // ------------------- Chargement de la liste des noms de série dans le comboBox de la fenêtre 
                                //(utile pour le filtre)
                                List<ArticleBE> LArticleBE = creerModifierArticleBL.listerTousLesArticle();
                                cmbFilterArticle.ItemsSource = approvisionnementArticleBL.getListCodeArticle(LArticleBE);
                            }
                        }
                    }
                    else MessageBox.Show("Impossible de supprimer cet élément ! \n \n Vous ne pouvez supprimer que le dernier élément de la liste !");

                    grdListeApprovisionnement.UnselectAll();
                }
            }
        }

        private void grdListeApprovisionnement_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (grdListeApprovisionnement.SelectedIndex != -1)
            {
                if (grdListeApprovisionnement.SelectedIndex == ListeApprovisionnements.Count - 1)
                {
                    etat = 1;
                    ancienObjet = new StockerBE();

                    ancienObjet = approvisionnementArticleBL.rechercherStock(ListeApprovisionnements.ElementAt(grdListeApprovisionnement.SelectedIndex));
                    if (ancienObjet != null)
                    {
                        // on charge les informations dans le formulaire
                        cmbArticle.Text = ancienObjet.codeArticle;
                        cmbMagasin.Text = ancienObjet.codeMagasin;
                        txtQuantite.Text = Convert.ToString(ancienObjet.quantiteAchetee);
                        txtAnnee.Text = Convert.ToString(ancienObjet.annee);

                        annee = ancienObjet.annee;
                        txtAnnee.Text = Convert.ToString(annee);
                        txtAnneeScolaire.Text = (annee - 1).ToString();

                        txtPrixUnitaire.Text = Convert.ToString(ancienObjet.puArticle);
                    }

                }
                else MessageBox.Show("Impossible de modifier cet élément ! \n \n Vous ne pouvez modifier que le dernier élément de la liste !");

                grdListeApprovisionnement.UnselectAll();

            }
        }

        private void txtQuantite_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Utilitaires.IsTextAllowed(e.Text);
        }      

        private void txtPrixUnitaire_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Utilitaires.IsTextAllowed(e.Text);
        }

        private void cmdImprimer_Click(object sender, RoutedEventArgs e)
        {
            CreerEtat etat = new CreerEtat("Approvisionnement -" + DateTime.Today.ToShortDateString(), "Liste des Approvisionnements \n Date : "+System.DateTime.Today.ToShortDateString());
            etat.obtenirEtat(grdListeApprovisionnement);
        }

        private void cmdEtatDuSTosk_Click(object sender, RoutedEventArgs e)
        {
            WindowGetEtatStockArticleUI getEtatStockArticleUI = new WindowGetEtatStockArticleUI();
            getEtatStockArticleUI.ShowDialog();
        }

        private void txtAnnee_PreviewTextInput(object sender, TextCompositionEventArgs e)
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

        private void txtAnneeScolaire_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Utilitaires.IsTextAllowed(e.Text);
        }

    }
}
