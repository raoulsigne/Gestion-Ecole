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
    /// Interaction logic for WindowTitularisationEnseignantUI.xaml
    /// </summary>
    public partial class WindowTitularisationEnseignantUI : Window
    {
        TitularisationEnseignantBL titularisationEnseignantBL;
        private int etat; // idique si nous sommes en création (0) ou en modification (1)

        // Définition d'une liste 'ListeTitulaires' observable de 'Titulaire des classes'
        public ObservableCollection<DirigerBE> ListeTitulaires { get; set; }
        List<EnseignantBE> LEnseignant;
        int annee;
        DirigerBE oldDiriger;

        // Fonction permettant de remplir le DataGrid avec les informations de la base de données
        // @param : - listObjet : la liste des objets à afficher dans le DataGrid
        public void RemplirDataGrid(List<DirigerBE> listObjet)
        {
            // Ajout de données dans la DataTable :
            var table = new DataTable();

            table.Columns.Add(new DataColumn("codeClasse", typeof(string)));
            table.Columns.Add(new DataColumn("codeProf", typeof(string)));
            table.Columns.Add(new DataColumn("annee", typeof(string)));
            table.Columns.Add(new DataColumn("enseignant", typeof(EnseignantBE)));

            if (listObjet != null)
            {

                for (int i = 0; i < listObjet.Count; i++)
                {
                    DataRow dr = table.NewRow();
                    dr["codeClasse"] = listObjet.ElementAt(i).codeClasse;
                    dr["codeProf"] = listObjet.ElementAt(i).codeProf;
                    dr["annee"] = listObjet.ElementAt(i).annee;
                    dr["enseignant"] = listObjet.ElementAt(i).enseignant;

                    table.Rows.Add(dr);
                }
            }

            string vCodeClasse = "";
            string vCodeProf = "";
            int annee = Convert.ToInt16(System.DateTime.Today.Year);
            EnseignantBE enseignant = new EnseignantBE();

            ListeTitulaires.Clear();

            foreach (DataRow row in table.Rows)
            {
                vCodeClasse = Convert.ToString(row["codeClasse"]);
                vCodeProf = Convert.ToString(row["codeProf"]);
                annee = Convert.ToInt16(row["annee"]);
                enseignant = (EnseignantBE)row["enseignant"];
                DirigerBE diriger = new DirigerBE();
                diriger.codeClasse = vCodeClasse;
                diriger.codeProf = vCodeProf;
                diriger.annee = annee;
                diriger.enseignant = enseignant;

                ListeTitulaires.Add(diriger);

            }

            grdListeTitulaire.ItemsSource = ListeTitulaires;
        }
    

        public WindowTitularisationEnseignantUI()
        {
            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";
            Thread.CurrentThread.CurrentCulture = ci;

            InitializeComponent();

            titularisationEnseignantBL = new TitularisationEnseignantBL();

            etat = 0;

            // A mettre pour que le binding avec le DataGrid fonctionne !
            grdListeTitulaire.DataContext = this;

            //txtAnnee.Text = Convert.ToString(System.DateTime.Today.Year);
            ParametresBE param = titularisationEnseignantBL.getParametres();
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

            // Initialisation de la collection, qui va s'afficher dans la DataGrid :
            ListeTitulaires = new ObservableCollection<DirigerBE>();
            LEnseignant = new List<EnseignantBE>();
            oldDiriger = new DirigerBE();

            List<DirigerBE> LDirigerBE = new List<DirigerBE>();
            if(param != null)
                LDirigerBE = titularisationEnseignantBL.listerTitularisationsSuivantCritere("annee = '" + param.annee+ "'");
            else 
                LDirigerBE = titularisationEnseignantBL.listerTitularisationsSuivantCritere("annee = '"+Convert.ToInt16(System.DateTime.Today.Year)+"'");

            // on charge les infos sur les enseignants            
            for (int i = 0; i < LDirigerBE.Count; i++ ){
                EnseignantBE enseignant = new EnseignantBE();
                enseignant.codeProf = LDirigerBE.ElementAt(i).codeProf;
                LDirigerBE.ElementAt(i).enseignant = titularisationEnseignantBL.rechercherEnseignant(enseignant);
            }
                // on met la liste "LSerieBE" dans le DataGrid
           
            RemplirDataGrid(LDirigerBE);

            //grdListeTitulaire.ItemsSource = LDirigerBE;

            // ------------------- Chargement de la liste des codes de classe dans le comboBox de la fenêtre 
            //(utile pour le filtre)
            List<ClasseBE> LClasse = titularisationEnseignantBL.listerToutesLesClasses();
            cmbClasse.ItemsSource = titularisationEnseignantBL.getListCodeClasse(LClasse);

        }

        private void cmbClasse_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (txtAnnee.Text != null && txtAnnee.Text != "")
            {
                if (cmbClasse.SelectedItem != null && cmbClasse.SelectedItem != "")
                {
                    ClasseBE classe = new ClasseBE();
                    classe.codeClasse = Convert.ToString(cmbClasse.SelectedItem);
                    //on charge la liste des enseignants de la classe choisit
                    LEnseignant = titularisationEnseignantBL.getListEnseignants(classe, Convert.ToInt16(txtAnnee.Text));

                    cmbTitulaire.ItemsSource = titularisationEnseignantBL.getListNomEnseignant(LEnseignant);
                }
            }
        }

        private void cmdEnregistrer_Click(object sender, RoutedEventArgs e)
        {
            if ((cmbClasse.Text != null && txtAnneeScolaire.Text != null && cmbTitulaire.Text != null)
                && (cmbClasse.Text != "" && txtAnneeScolaire.Text != "" && cmbTitulaire.Text != ""))
            {
                DirigerBE diriger = new DirigerBE();
                diriger.codeClasse = cmbClasse.Text;
                diriger.annee = Convert.ToInt16(txtAnnee.Text);
                diriger.codeProf = LEnseignant.ElementAt(cmbTitulaire.SelectedIndex).codeProf;
                // on charge les infos sur l'enseignant
                EnseignantBE enseignant = new EnseignantBE();
                enseignant.codeProf = diriger.codeProf;
                diriger.enseignant = titularisationEnseignantBL.rechercherEnseignant(enseignant);

                if (etat == 1)
                {
                    titularisationEnseignantBL.modifierTitularisation(oldDiriger, diriger);
                   

                    //txtAnnee.Text = Convert.ToString(System.DateTime.Today.Year); 
                    ParametresBE param = titularisationEnseignantBL.getParametres();
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

                    cmbClasse.Text = null;
                    cmbTitulaire.Text = null;
                    cmbTitulaire.ItemsSource = null;

                    etat = 0;

                    List<DirigerBE> LDirigerBE = new List<DirigerBE>();
                    if(param != null)
                        LDirigerBE = titularisationEnseignantBL.listerTitularisationsSuivantCritere("annee = '" + param.annee + "'");
                    else
                        LDirigerBE = titularisationEnseignantBL.listerTitularisationsSuivantCritere("annee = '" + Convert.ToInt16(System.DateTime.Today.Year) + "'");

                    // on met la liste "LSerieBE" dans le DataGrid
                    // on charge les infos sur les enseignants            
                    for (int i = 0; i < LDirigerBE.Count; i++)
                    {
                        enseignant = new EnseignantBE();
                        enseignant.codeProf = LDirigerBE.ElementAt(i).codeProf;
                        LDirigerBE.ElementAt(i).enseignant = titularisationEnseignantBL.rechercherEnseignant(enseignant);
                    }
                    RemplirDataGrid(LDirigerBE);
                    //grdListeTitulaire.ItemsSource = LDirigerBE;
                }
                else if (titularisationEnseignantBL.listerTitularisationsSuivantCritere("codeclasse = '" + cmbClasse.Text + "' AND annee = '" + txtAnnee.Text + "'").Count == 0)
                { // si la classe en question n'a pas encore de titulaire
                    if (titularisationEnseignantBL.rechercherTitularisation(diriger) == null)
                    { // si un enseignant n'a pas encor été désigné titulaire pour cette classe dans cette année

                        if (titularisationEnseignantBL.creerTitularisation(cmbClasse.Text, LEnseignant.ElementAt(cmbTitulaire.SelectedIndex).codeProf, Convert.ToInt16(txtAnnee.Text)))
                            {
                                MessageBox.Show("Opération réussie");


                                List<DirigerBE> LDirigerBE = titularisationEnseignantBL.listerTitularisationsSuivantCritere("annee = '" + txtAnnee.Text + "'");
                                // on charge les infos sur les enseignants            
                                for (int i = 0; i < LDirigerBE.Count; i++)
                                {
                                    enseignant = new EnseignantBE();
                                    enseignant.codeProf = LDirigerBE.ElementAt(i).codeProf;
                                    LDirigerBE.ElementAt(i).enseignant = titularisationEnseignantBL.rechercherEnseignant(enseignant);
                                }
                                //on rafraichir le DataGrid
                                RemplirDataGrid(LDirigerBE);
                                //grdListeTitulaire.ItemsSource = LDirigerBE;

                                //on rafraichit les champs du formulaire
                                //txtAnnee.Text = Convert.ToString(System.DateTime.Today.Year);
                                ParametresBE param = titularisationEnseignantBL.getParametres();
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

                                cmbClasse.Text = null;
                                cmbTitulaire.Text = null;
                                cmbTitulaire.ItemsSource = null;

                            }
                            else MessageBox.Show("Echec enregistrement : une erreure est survenue !");
                       
                    }
                    else MessageBox.Show("Une Titularisation de ce type existe deja dans le système \n \n Veuillez modifier les informations SVP !");
            
                }else
                    MessageBox.Show("Cette Classe a deja un titulaire pour l'année choisit !");
            }
            else MessageBox.Show("Tous les champs doivent pas être remplis !");
        }

        private void cmdAnnuler_Click(object sender, RoutedEventArgs e)
        {
            //on rafraichit les champs du formulaire
            //txtAnnee.Text = Convert.ToString(System.DateTime.Today.Year);
            ParametresBE param = titularisationEnseignantBL.getParametres();
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

            cmbClasse.Text = null;
            cmbTitulaire.Text = null;
            cmbTitulaire.ItemsSource = null;

            etat = 0;
        }

        private void grdListeTitulaire_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                if (MessageBox.Show("Voulez-vous vraiment le supprimer ? ", "School : Confimation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {

                    if (titularisationEnseignantBL.supprinerTitularisation(ListeTitulaires.ElementAt(grdListeTitulaire.SelectedIndex)))
                        ListeTitulaires.RemoveAt(grdListeTitulaire.SelectedIndex);
                    grdListeTitulaire.ItemsSource = ListeTitulaires;

                }

                grdListeTitulaire.UnselectAll();

            }
        }

        private void grdListeTitulaire_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (grdListeTitulaire.SelectedIndex != -1)
            {
                etat = 1;
                DirigerBE diriger = new DirigerBE();
                diriger = titularisationEnseignantBL.rechercherTitularisation(ListeTitulaires.ElementAt(grdListeTitulaire.SelectedIndex));

                // on charge les informations dans le formulaire
                cmbClasse.Text = diriger.codeClasse;

                txtAnnee.Text = Convert.ToString(diriger.annee);
                txtAnneeScolaire.Text = Convert.ToString(diriger.annee - 1);

                //on cherche les infos ur l'enseignant
                EnseignantBE enseignant = new EnseignantBE();
                enseignant.codeProf = ListeTitulaires.ElementAt(grdListeTitulaire.SelectedIndex).codeProf;
                enseignant = titularisationEnseignantBL.rechercherEnseignant(enseignant);
                cmbTitulaire.Text = enseignant.codeProf + " - " + enseignant.nomProf;
                oldDiriger = diriger;

                grdListeTitulaire.UnselectAll();
            }
        }

        private void txtAnnee_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Utilitaires.IsTextAllowed(e.Text);

           
        }

        private void txtAnneeScolaire_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Utilitaires.IsTextAllowed(e.Text);


        }

        private void txtAnnee_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtAnnee.Text != null && txtAnnee.Text != "")
            {
                if (cmbClasse.Text != null && cmbClasse.Text != "")
                {
                    ClasseBE classe = new ClasseBE();
                    classe.codeClasse = Convert.ToString(cmbClasse.SelectedItem);
                    //on charge la liste des enseignants de la classe choisit
                    LEnseignant = titularisationEnseignantBL.getListEnseignants(classe, Convert.ToInt16(txtAnnee.Text));

                    cmbTitulaire.ItemsSource = titularisationEnseignantBL.getListNomEnseignant(LEnseignant);
                }
            }
        }

        private void cmdFermer_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cmdImprimer_Click(object sender, RoutedEventArgs e)
        {
            CreerEtat etat = new CreerEtat("Liste Titularisation -" + DateTime.Today.ToShortDateString(), "Liste des Titularisations : Année "+txtAnnee.Text);
            etat.obtenirEtat(grdListeTitulaire);
        }

        private void txtAnnee_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) {
                List<DirigerBE> LDirigerBE = new List<DirigerBE>();
                
                LDirigerBE = titularisationEnseignantBL.listerTitularisationsSuivantCritere("annee = '" + txtAnnee.Text + "'");

                // on charge les infos sur les enseignants            
                for (int i = 0; i < LDirigerBE.Count; i++)
                {
                    EnseignantBE enseignant = new EnseignantBE();
                    enseignant.codeProf = LDirigerBE.ElementAt(i).codeProf;
                    LDirigerBE.ElementAt(i).enseignant = titularisationEnseignantBL.rechercherEnseignant(enseignant);
                }
                // on met la liste "LSerieBE" dans le DataGrid
                RemplirDataGrid(LDirigerBE);
                //grdListeTitulaire.ItemsSource = LDirigerBE;

            }
        }

        private void txtAnneeScolaire_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                List<DirigerBE> LDirigerBE = new List<DirigerBE>();

                LDirigerBE = titularisationEnseignantBL.listerTitularisationsSuivantCritere("annee = '" + txtAnnee.Text + "'");

                // on charge les infos sur les enseignants            
                for (int i = 0; i < LDirigerBE.Count; i++)
                {
                    EnseignantBE enseignant = new EnseignantBE();
                    enseignant.codeProf = LDirigerBE.ElementAt(i).codeProf;
                    LDirigerBE.ElementAt(i).enseignant = titularisationEnseignantBL.rechercherEnseignant(enseignant);
                }
                // on met la liste "LSerieBE" dans le DataGrid
                RemplirDataGrid(LDirigerBE);
                //grdListeTitulaire.ItemsSource = LDirigerBE;

            }
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

            if (txtAnnee.Text != null && txtAnnee.Text != "")
            {
                if (cmbClasse.Text != null && cmbClasse.Text != "")
                {
                    ClasseBE classe = new ClasseBE();
                    classe.codeClasse = Convert.ToString(cmbClasse.SelectedItem);
                    //on charge la liste des enseignants de la classe choisit
                    LEnseignant = titularisationEnseignantBL.getListEnseignants(classe, Convert.ToInt16(txtAnnee.Text));

                    cmbTitulaire.ItemsSource = titularisationEnseignantBL.getListNomEnseignant(LEnseignant);
                }
            }
        }

       
    }
}
