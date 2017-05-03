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

using Ecole.BusinessLogic;
using Ecole.BusinessEntity;
using Ecole.Utilitaire;

namespace Ecole.UI
{
    /// <summary>
    /// Interaction logic for WindowAddEditEffectifUI.xaml
    /// </summary>
    public partial class WindowListeEleveParClasseUI : Window
    {
        ListerEffectifClasseBL listerEffectifClasseBL;
        String codeClasseRecherche; // le code de la classe qui a permis de générer la liste
        int anneeRecherche; // l'annee sur lequel on a fait la recherche

        String classeChoisi; // servira dans la création des états

        int annee;
        List<EleveBE> LEleveBE;

        // Définition d'une liste 'ListeEleves' observable de 'Eleves'
        public ObservableCollection<EleveBE> ListeEleves { get; set; }

        // Fonction permettant de remplir le DataGrid avec les informations de la base de données
        // @param : - listObjet : la liste des objets à afficher dans le DataGrid
        public void RemplirDataGrid(List<EleveBE> listObjet)
        {
            // Ajout de données dans la DataTable :
            var table = new DataTable();

            table.Columns.Add(new DataColumn("numero", typeof(int))); //utile pour le data datagrid

            table.Columns.Add(new DataColumn("matricule", typeof(string)));
            table.Columns.Add(new DataColumn("nom", typeof(string)));

            table.Columns.Add(new DataColumn("categorie", typeof(string)));

            table.Columns.Add(new DataColumn("sexe", typeof(string)));
            table.Columns.Add(new DataColumn("etat", typeof(string)));
            table.Columns.Add(new DataColumn("dateNaissanceString", typeof(string)));
            table.Columns.Add(new DataColumn("telephone", typeof(string)));
            table.Columns.Add(new DataColumn("nompere", typeof(string)));
            table.Columns.Add(new DataColumn("telParent", typeof(string)));
            table.Columns.Add(new DataColumn("email", typeof(string)));
            table.Columns.Add(new DataColumn("adresse", typeof(string)));

            if (listObjet != null)
            {

                for (int i = 0; i < listObjet.Count; i++)
                {
                    DataRow dr = table.NewRow();
                    dr["numero"] = i+1; //utile pour le data datagrid

                    dr["matricule"] = listObjet.ElementAt(i).matricule;
                    dr["nom"] = listObjet.ElementAt(i).nom;

                    dr["categorie"] = listObjet.ElementAt(i).categorie;

                    if (listObjet.ElementAt(i).sexe != null && listObjet.ElementAt(i).sexe.Count() != 0)
                    {
                        dr["sexe"] = listObjet.ElementAt(i).sexe.ElementAt(0).ToString().ToUpper();
                    }
                    else
                    {
                        dr["sexe"] = listObjet.ElementAt(i).sexe;
                    }

                    dr["etat"] = listObjet.ElementAt(i).etat;
                    
                    dr["dateNaissanceString"] = listObjet.ElementAt(i).dateNaissance;
                    dr["telephone"] = listObjet.ElementAt(i).telephone;
                    dr["nompere"] = listObjet.ElementAt(i).nomPere; 
                    dr["telParent"] = listObjet.ElementAt(i).telParent;
                    dr["email"] = listObjet.ElementAt(i).email;
                    dr["adresse"] = listObjet.ElementAt(i).adresse;
                    table.Rows.Add(dr);
                }
            }

            string vMatrcule = "";
            string vNom = "";
            string vSexe = "";
            string vEtat = "";
            DateTime vDateNaissance = new DateTime();
            string vTelephone = "";
            string vNomPere = "";
            string vTelParent = "";
            string vEmail = "";
            string vAdresse = "";

            string vCategorie = "";

            ListeEleves.Clear();

            //Personnes_Table = LoadDataTable();

            int index = 1; // compte le nombre d'élèves

            foreach (DataRow row in table.Rows)
            {
                vMatrcule = Convert.ToString(row["matricule"]);
                vNom = Convert.ToString(row["nom"]);
                vSexe = Convert.ToString(row["sexe"]);
                vEtat = Convert.ToString(row["etat"]);
                vDateNaissance = Convert.ToDateTime(row["dateNaissanceString"]);
                vTelephone = Convert.ToString(row["telephone"]);
                vNomPere = Convert.ToString(row["nompere"]);
                vTelParent = Convert.ToString(row["telParent"]);
                vEmail = Convert.ToString(row["email"]);
                vAdresse = Convert.ToString(row["adresse"]);

                vCategorie = Convert.ToString(row["categorie"]);

                EleveBE eleve = new EleveBE();
                eleve.matricule = vMatrcule;
                eleve.nom = vNom;
                eleve.sexe = vSexe;
                eleve.etat = vEtat;
                eleve.dateNaissance = vDateNaissance;
                eleve.dateNaissanceString = vDateNaissance.ToShortDateString();
                eleve.telephone = vTelephone;
                eleve.nomPere = vNomPere;
                eleve.telParent = vTelParent;
                eleve.email = vEmail;
                eleve.adresse = vAdresse;

                eleve.categorie = vCategorie;

                ListeEleves.Add(eleve);

                eleve.numero = index;
                index++;

            }
        }
  

        public WindowListeEleveParClasseUI()
        {
            // pour le formatage de la date
            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "yyyy-MM-dd";
            Thread.CurrentThread.CurrentCulture = ci;
            // fin pour le formatage de la date

            InitializeComponent();
            listerEffectifClasseBL = new ListerEffectifClasseBL();

            classeChoisi = "";

            // chargement de la liste des codes des classes dans le comboBox
            List<ClasseBE> LClasseBE = listerEffectifClasseBL.listerToutesLesClasses();
            cmbClasse.ItemsSource = listerEffectifClasseBL.getListCodeClasse(LClasseBE);

            // A mettre pour que le binding avec le DataGrid fonctionne !
            grdListeEffectif.DataContext = this;

            // Initialisation de la collection, qui va s'afficher dans la DataGrid :
            ListeEleves  = new ObservableCollection<EleveBE>();
            LEleveBE = new List<EleveBE>();

            //List<EleveBE> LEleveBE = null;
            //// on met la liste "LSerieBE" dans le DataGrid
            //RemplirDataGrid(LEleveBE);     

            ParametresBE param = listerEffectifClasseBL.getParametres();
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

        private void cmdFermer_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void combBoxClasse_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void cmdValider_Click(object sender, RoutedEventArgs e)
        { //on vide le dataGrid
            ListeEleves.Clear();
            LEleveBE.Clear();
           
            // recupération du champs "annee" saisit
            if ((cmbClasse.Text != null && txtAnneeScolaire.Text != null) && (cmbClasse.Text != "" && txtAnneeScolaire.Text != ""))
            {
                classeChoisi = cmbClasse.Text;

                List<InscrireBE> listInscrireBE;
                // si il a choit "<Toutes les Classes>
                if (cmbClasse.Text.Equals("<Toutes les Classes>"))
                {
                    listInscrireBE = listerEffectifClasseBL.listeDesEffectifsDeToutesLesClassePourUneAnnee(Convert.ToString(txtAnnee.Text));
                }
                else {
                    // on récupère la liste des inscrits pour la classe et l'année choisit
                    listInscrireBE = listerEffectifClasseBL.listeDesEffectifsDuneClassePourUneAnnee(cmbClasse.Text, Convert.ToString(txtAnnee.Text));
                
                }
                if (listInscrireBE != null) {
                    //si la liste n'est pas vide  
                    // on recherche les informations sur les élèves inscrits (qui seront affiché dans le gridData)
                    EleveBE eleveBE = new EleveBE();

                    LEleveBE = new List<EleveBE>();
                    for (int i = 0; i < listInscrireBE.Count; i++) {

                        //MessageBox.Show("[ " + listInscrireBE.ElementAt(i).codeClasse + ", " + listInscrireBE.ElementAt(i).matricule + ", " + listInscrireBE.ElementAt(i).annee+ " ]");
                        EleveBE eleve = new EleveBE();
                        eleve.matricule = listInscrireBE.ElementAt(i).matricule;
                        eleveBE = listerEffectifClasseBL.rechercherEleve(eleve);

                        if (eleveBE != null)
                        {
                            //on recherche la catégorie de l'élève
                            AppartenirBE appartenir = new AppartenirBE();
                            List<AppartenirBE> LAppartenir = listerEffectifClasseBL.ListerAppartenirSuivantCritere("matricule = '"+ eleveBE.matricule +"' AND annee = '"+ txtAnnee.Text +"'");
                            
                            if(LAppartenir != null && LAppartenir.Count != 0){
                                eleveBE.categorie = LAppartenir.ElementAt(0).codeCatEleve;
                            }

                            LEleveBE.Add(eleveBE);
                        }
                    }

                    //tri de la liste
                    List<EleveBE> liste = LEleveBE.OrderBy(o => o.nom).ToList();
                    LEleveBE.Clear();
                    int j = 1;
                    foreach (EleveBE eleve in liste)
                    {
                        LEleveBE.Add(eleve);
                    }

                    //on rafraichir le DataGrid
                    RemplirDataGrid(LEleveBE);
                    lblTotal.Content = LEleveBE.Count.ToString();

                    codeClasseRecherche = cmbClasse.Text;
                    anneeRecherche = Convert.ToInt16(txtAnnee.Text);
                    //txtAnnee.Text = "";
                    ParametresBE param = listerEffectifClasseBL.getParametres();
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

                    cmbClasse.Text = "";
                   
                }
            }
            else MessageBox.Show("Tous les champs doivent pas être remplis !");
        }

        private void cmdAnnuler_Click(object sender, RoutedEventArgs e)
        {
            cmbClasse.Text = null;
            //txtAnnee.Text = "";
            ParametresBE param = listerEffectifClasseBL.getParametres();
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

            classeChoisi = "";
        }

        private void grdListeEffectif_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                if (grdListeEffectif.SelectedIndex != -1)
                {
                    if (MessageBox.Show("Voulez-vous vraiment le supprimer ? ", "School : Confimation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        String matriculeEleve = ListeEleves.ElementAt(grdListeEffectif.SelectedIndex).matricule;

                        if (listerEffectifClasseBL.retireUnEleveDuneClasse(this.codeClasseRecherche, matriculeEleve, this.anneeRecherche))
                            ListeEleves.RemoveAt(grdListeEffectif.SelectedIndex);

                        grdListeEffectif.ItemsSource = ListeEleves;

                    }

                    grdListeEffectif.UnselectAll();
                }

            }
        }

        private void txtAnnee_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Utilitaires.IsTextAllowed(e.Text);
        }

        private void cmdImprimer_Click(object sender, RoutedEventArgs e)
        {
            CreerEtat etat;

            //if (classeChoisi.Equals("<Toutes les Classes>"))
            //{
            //    etat = new CreerEtat("Liste Elèves -" + DateTime.Today.ToShortDateString(), "Liste de tous les élèves de l'établissement ");

            //}
            //else
            //    etat = new CreerEtat("Liste Elèves "+classeChoisi+" -" + DateTime.Today.ToShortDateString(), "Liste des Elèves de la "+classeChoisi);

            etat = new CreerEtat("Liste Elèves -" + DateTime.Today.ToShortDateString(), "Liste des élèves");

            ClasseBE classe = new ClasseBE();
            classe.codeClasse = classeChoisi;
            classe = listerEffectifClasseBL.rechercherClasse(classe);
            etat.etatListeEleveDuneClasse_new(ListeEleves, classe, anneeRecherche);
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

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            ExportToExcel<InfosEleve> report = new ExportToExcel<InfosEleve>();
            List<InfosEleve> liste = new List<InfosEleve>();
            if (ListeEleves != null)
            {
                int i = 1;
                foreach (EleveBE eleve in LEleveBE)
                {
                    liste.Add(new InfosEleve(i++, eleve.matricule, eleve.nom, eleve.nomPere, eleve.telParent, eleve.dateNaissanceString + "   à   " + eleve.lieuNaissance, eleve.sexe, eleve.etat));
                }
            }
            report.dataToPrint = liste;
            report.GenerateReport();
        }
        
    }

    public class InfosEleve
    {
        public int numero { get; set; }
        public String matricule { get; set; }
        public String nom { get; set; }
        public String nompere { get; set; }
        public String telparent { get; set; }
        public String datenaissance { get; set; }
        public String sexe { get; set; }
        public String etat { get; set; }

        public InfosEleve(int numero, String matricule, String nom, String nompere, String telparent, String datenaissance, String sexe, String etat)
        {
            this.numero = numero;
            this.matricule = matricule;
            this.nom = nom;
            this.nompere = nompere;
            this.telparent = telparent;
            this.datenaissance = datenaissance;
            this.sexe = sexe;
            this.etat = etat;
        }
    }
}
