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
using Ecole.DataAccess;
using Ecole.BusinessLogic;
using System.Globalization;
using System.Threading;
using Ecole.ClasseConception;
using Ecole.Utilitaire;

namespace Ecole.UI
{
    
    public partial class JournalCaisseUI : Window
    {
        private GestionJournalBL journalBL;
        private List<LigneEtatJournal> journal;
        private List<LigneEtatJournal> lignesJournal;
       
        List<String> listLogin;
        List<String> listNom;
        private string CODE_MIXTE = "Tous";
        
       //private DateTime datedebut, datefin;

        public JournalCaisseUI() 
        {
            lignesJournal = new List<LigneEtatJournal>();
            journal = new List<LigneEtatJournal>();
            
            InitializeComponent();

            journalBL = new GestionJournalBL();
            listLogin = new List<String>();
            listNom = new List<String>();


             //DateTimePicker1.Format = DateTimePickerFormat.Custom;
             //DateTimePicker1.CustomFormat = "dd-MM-yyyy";

            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "yyyy-MM-dd";
            Thread.CurrentThread.CurrentCulture = ci;
            
            //charger la liste des login
            listLogin = journalBL.listerValeurColonne("login");
            listLogin.Add(CODE_MIXTE);

            //charger liste des noms user
            listNom = journalBL.listerValeurColonne("nom");
            listNom.Add(CODE_MIXTE);
            
            cmbLogin.ItemsSource = listLogin;
            cmbLogin.SelectedIndex = cmbLogin.Items.IndexOf(CODE_MIXTE);



            lignesJournal = journalBL.listerSuivantCritere(" AND action LIKE  " + "'%vente%' AND date LIKE " + "'" + DateTime.Today.Date.ToString("yyyy-MM-dd") + "%'");
           
            //changementMotif();

            grdJournal.ItemsSource = lignesJournal;
            grdJournal.Items.Refresh();

            dpiDateDebut.IsTodayHighlighted = true;
            dpiDateDebut.SelectedDate = DateTime.Today;
            dpiDateDebut.Text = DateTime.Today.ToShortDateString();

            dpiDateFin.IsTodayHighlighted = true;
            dpiDateFin.SelectedDate = DateTime.Today;
            dpiDateFin.Text = DateTime.Today.ToShortDateString();
                        
        }

        //private void imprimer_Click(object sender, RoutedEventArgs e)
        //{
        //    CreerEtat etat = new CreerEtat("caisse-"+DateTime.Today.ToShortDateString(),"Liste des operations de la caisse");
        //    etat.etatCaisse(grdEtatCaisse, dpiDateDebut.Text, dpiDateFin.Text, soldetotal);
        //}

        private void fermer_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /* fonction qui s'execute quand la date de fin est changee 
         *
         */
        private void dpiDateFin_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            chargerDataGrid();
        }

        /* fonction qui s'execute quand la date de debut est changee 
         *
         */
        private void dpiDateDebut_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            chargerDataGrid();
        }

        /*
         * fonction qui s'execute quand la valeur de la liste deroulante est changee
         */
        private void cmbLogin_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<LigneEtatJournal> tampon = new List<LigneEtatJournal>();
            string filtre = txtFiltre.Text;

            //if (cmbLogin.SelectedValue.ToString() != CODE_MIXTE && cmbLogin.Text != "")
            lblNom.Content = listNom.ElementAt(cmbLogin.SelectedIndex).ToString(); 

            //on charge d'abord la grille et puis on tri sur les elements affiches
            chargerDataGrid();

            if (lignesJournal != null)
            {
                foreach (LigneEtatJournal l in lignesJournal)
                {
                    if (cmbLogin.SelectedValue.ToString() == CODE_MIXTE || l.login.ToUpper().Contains(cmbLogin.SelectedValue.ToString().ToUpper()))
                        tampon.Add(l);
                }

                lignesJournal = new List<LigneEtatJournal>();
                foreach (LigneEtatJournal l in tampon)
                {
                    lignesJournal.Add(l);
                }
            }
            grdJournal.ItemsSource = lignesJournal;
            grdJournal.Items.Refresh();
            
        }

        /**
         * fonction qui recupere les conditions pour alimenter la datagrid
         */ 
        private void chargerDataGrid()
        {
            List<LigneEtatJournal> journal = new List<LigneEtatJournal>();
            lignesJournal = new List<LigneEtatJournal>();

            string datedebut = DateTime.Today.ToShortDateString();
            string datefin = DateTime.Today.ToShortDateString();
            
            string code = "%";

            //string datefin = DateTime.Today.Date.ToShortDateString();
            //string datedebut = DateTime.Today.Date.ToShortDateString();
            //string code = "%";



            if (dpiDateDebut.SelectedDate != null)
                datedebut = dpiDateDebut.SelectedDate.Value.Date.ToShortDateString();
            if (dpiDateFin.SelectedDate != null)
                datefin = dpiDateFin.SelectedDate.Value.Date.ToShortDateString();
            if (cmbLogin.SelectedValue != null)
                code = cmbLogin.SelectedValue.ToString();
           // OperationBE operation = new OperationBE();

            if (Convert.ToDateTime(datefin).CompareTo(Convert.ToDateTime(datedebut)) >= 0)
            {
                //changer format avant d'insérer
                //if (dpiDateDebut.SelectedDate != null)
                //    datedebut = dpiDateDebut.SelectedDate.Value.Date.ToString("dd/MM/yyyy");
                //if (dpiDateFin.SelectedDate != null)
                //    datefin = dpiDateFin.SelectedDate.Value.Date.ToString("dd/MM/yyyy");
                //insérer
                lignesJournal = journalBL.listerSuivantCritere("AND action LIKE  " + "'%vente%' AND date between " + "'" + Convert.ToDateTime(datedebut).ToString("yyyy-MM-dd") + "' AND " + "'" + Convert.ToDateTime(datefin).ToString("yyyy-MM-dd") + "'");

                if (lignesJournal != null)
                    foreach (LigneEtatJournal ligne in lignesJournal)
                        journal.Add(ligne);

                // lignesJournal = creerDataSource(realisers,acheters,payers);
                grdJournal.ItemsSource = lignesJournal;
                grdJournal.Items.Refresh();

                string filtre = txtFiltre.Text;

                List<LigneEtatJournal> tampon = new List<LigneEtatJournal>();
                if (lignesJournal != null)
                {
                    foreach (LigneEtatJournal l in lignesJournal)
                    {
                        if (filtre == "" || l.action.ToUpper().Contains(filtre.ToUpper()))
                            tampon.Add(l);
                    }

                    lignesJournal = new List<LigneEtatJournal>();
                    foreach (LigneEtatJournal l in tampon)
                    {
                        lignesJournal.Add(l);
                    }
                }

            }
            else
            {
                MessageBox.Show("La date de debut doit être plus petite ou égale à la date de fin");
                grdJournal.ItemsSource = null;
                grdJournal.Items.Refresh();

            }
        }



       private void txtMotif_KeyUp(object sender, KeyEventArgs e)
        {
            changementMotif();
        }


        private void changementMotif()
        {
            List<LigneEtatJournal> tampon = new List<LigneEtatJournal>();
            string filtre = txtFiltre.Text;
           

            //on charge d'abord la grille et puis on tri sur les elements affiches
            chargerDataGrid();
            if (lignesJournal != null)
            {
                foreach (LigneEtatJournal l in lignesJournal)
                {
                    if (filtre == "" || l.action.ToUpper().Contains(filtre.ToUpper()))
                        tampon.Add(l);
                }

                lignesJournal = new List<LigneEtatJournal>();
                foreach (LigneEtatJournal l in tampon)
                {
                    lignesJournal.Add(l);

                }
            }

            grdJournal.ItemsSource = lignesJournal;
            grdJournal.Items.Refresh();
            
        }

        private void cmdImprimer_Click(object sender, RoutedEventArgs e)
        {
            if (grdJournal.Items.Count == 0)
                
                MessageBox.Show("Aucun contenu n'est disponible pour impréssion", "School Brain", MessageBoxButton.OK, MessageBoxImage.Error);
                          
            else
            {
                CreerEtat etat = new CreerEtat("Journal-" + DateTime.Today.ToString("dd-MM-yyyy"), "Journal des opérations éffectuées sur le système ");
                etat.etatJournal(grdJournal, cmbLogin.Text.ToString(), lblNom.Content.ToString(), Convert.ToDateTime(dpiDateDebut.Text).ToString("dd-MM-yyyy"), Convert.ToDateTime(dpiDateFin.Text).ToString("dd-MM-yyyy"));
                JournalBE ligneJournal = new JournalBE("ecole", "Impréssion du journal des opérations", DateTime.Today.Date.ToString("yyyy-MM-dd"), DateTime.Now.ToLongTimeString());
                journalBL.ajouterJournal(ligneJournal);
            }
        }

        

    }
}
