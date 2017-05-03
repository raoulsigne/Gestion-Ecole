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
using Ecole.ClasseConception;
using Ecole.Utilitaire;

namespace Ecole.UI
{
    /// <summary>
    /// Interaction logic for WindowInscriptionEleveUI.xaml
    /// </summary>
    public partial class WindowInscriptionEleveUI : Window
    {
        CreerModifierInscriptionClasseBL creerModifierInscriptionClasseBL;
        CreerModifierCategorieEleveBL creerModifierCategorieEleveBL;
        List<string> eleves;
        List<string> classes;
        InscrireBE oldInscrire;
        AppartenirBE oldAppartenir;

        EleveBE eleveChoisi;
        private int etat; // idique si nous sommes en création (0) ou en modification (1)

        int annee;

        // Définition d'une liste 'ListeInscriptionClasse' observable de 'Inscrire'
        public ObservableCollection<InscrireBE> ListeInscriptionClasse { get; set; }

        // Définition d'une liste 'Moyennes de l'élève' observable de 'MoyennesScolaireEleve'
        public ObservableCollection<MoyennesScolaireEleveBE> ListeMoyennesScolaireEleve { get; set; }

        // Définition d'une liste 'Résultats de l'élève' observable de 'ResultatsScolaireEleve'
        public ObservableCollection<ResultatsScolaireEleveBE> ListeResultatsScolaireEleve { get; set; }

        // Fonction permettant de remplir le DataGrid avec les informations de la base de données
        // @param : - listObjet : la liste des objets à afficher dans le DataGrid
        public void RemplirDataGrid(List<InscrireBE> listObjet)
        {
            // Ajout de données dans la DataTable :
            var table = new DataTable();

            table.Columns.Add(new DataColumn("codeClasse", typeof(string)));
            table.Columns.Add(new DataColumn("matricule", typeof(string)));
            table.Columns.Add(new DataColumn("annee", typeof(string)));
            table.Columns.Add(new DataColumn("eleve", typeof(EleveBE)));
            table.Columns.Add(new DataColumn("categorieEleve", typeof(CategorieEleveBE)));

            if (listObjet != null)
            {

                for (int i = 0; i < listObjet.Count; i++)
                {
                    DataRow dr = table.NewRow();
                    dr["codeClasse"] = listObjet.ElementAt(i).codeClasse;
                    dr["matricule"] = listObjet.ElementAt(i).matricule;
                    dr["annee"] = listObjet.ElementAt(i).annee;
                    dr["eleve"] = listObjet.ElementAt(i).eleve;
                    dr["categorieEleve"] = listObjet.ElementAt(i).categorieEleve;
                    table.Rows.Add(dr);
                }
            }

            string vcodeClasse = "";
            string vmatricule = "";
            string vannee = "";
            EleveBE veleve = new EleveBE();
            CategorieEleveBE vcategorieEleve = new CategorieEleveBE();

            ListeInscriptionClasse.Clear();

            //Personnes_Table = LoadDataTable();

            foreach (DataRow row in table.Rows)
            {
                vcodeClasse = Convert.ToString(row["codeClasse"]);
                vmatricule = Convert.ToString(row["matricule"]);
                vannee = Convert.ToString(row["annee"]);
                veleve = (EleveBE)row["eleve"];
                vcategorieEleve = (CategorieEleveBE)row["categorieEleve"];

                InscrireBE inscrire = new InscrireBE();
                inscrire.codeClasse = vcodeClasse;
                inscrire.annee = Convert.ToInt16(vannee);
                inscrire.matricule = vmatricule;

                inscrire.eleve = veleve;
                inscrire.categorieEleve = vcategorieEleve;

                ListeInscriptionClasse.Add(inscrire);

            }
        }

        public WindowInscriptionEleveUI()
        {

            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";
            Thread.CurrentThread.CurrentCulture = ci;

            InitializeComponent();

            creerModifierInscriptionClasseBL = new CreerModifierInscriptionClasseBL();
            creerModifierCategorieEleveBL = new CreerModifierCategorieEleveBL();

            eleves = new List<string>();
            classes = new List<string>();
            classes = creerModifierInscriptionClasseBL.listerValeurColonneClasse("codeclasse");
            cmbClasse1.ItemsSource = classes;
            eleveChoisi = null;

            etat = 0;

            oldInscrire = new InscrireBE();
            oldAppartenir = new AppartenirBE();

            //txtAnnee.Text = Convert.ToString(System.DateTime.Today.Year);
            ParametresBE param = creerModifierInscriptionClasseBL.getParametres();
            if (param != null)
            {
                annee = param.annee;

                txtAnnee.Text = Convert.ToString(param.annee);
                txtAnneeScolaire.Text = (param.annee - 1).ToString();

                txtAnneeMoyennesScolaire.Text = Convert.ToString(param.annee);
                txtAnneeScolaireMoyennes.Text = (param.annee - 1).ToString();

                txtAnneeResultatsScolaire.Text = Convert.ToString(param.annee);
                txtAnneeScolaireResultats.Text = (param.annee - 1).ToString();

                //***********

            }
            else
            {
                txtAnnee.Text = "";
                txtAnneeScolaire.Text = "";

                txtAnneeMoyennesScolaire.Text = "";
                txtAnneeScolaireMoyennes.Text = "";

                txtAnneeResultatsScolaire.Text = "";
                txtAnneeScolaireResultats.Text = "";

            }

            // A mettre pour que le binding avec le DataGrid fonctionne !
            grdListeInscriptionClasse.DataContext = this;

            //on charge les éléments des combobox
            //List<ClasseBE> LClasse = creerModifierInscriptionClasseBL.listerToutesLesClasses();
            //cmbClasse.ItemsSource = creerModifierInscriptionClasseBL.getListCodeClasse(LClasse);

            List<CategorieEleveBE> LCatEleve = creerModifierInscriptionClasseBL.listerToutesLesCategoriesELeves();
            cmbCategorieEleve.ItemsSource = creerModifierInscriptionClasseBL.getListCodeCategorieEleve(LCatEleve);

            // Initialisation de la collection, qui va s'afficher dans la DataGrid :
            ListeInscriptionClasse = new ObservableCollection<InscrireBE>();

            ////on charge la liste des codes de séquence dans le combobox
            //List<SequenceBE> LSequence = creerModifierInscriptionClasseBL.listerToutesLesSequences();
            //cmbPeriodeSequenceMoyennesScolaire.ItemsSource = creerModifierInscriptionClasseBL.getListCodeSequence(LSequence);
            //cmbPeriodeSequenceResultatsScolaire.ItemsSource = creerModifierInscriptionClasseBL.getListCodeSequence(LSequence);

            //on charge la liste des codes de Trimestre dans le combobox
            List<TrimestreBE> LTrimestre = creerModifierInscriptionClasseBL.listerTousLesTrimestres();
            cmbPeriodeTrimestreMoyennesScolaire.ItemsSource = creerModifierInscriptionClasseBL.getListCodeTrimestre(LTrimestre);
            cmbPeriodeTrimestreResultatsScolaire.ItemsSource = creerModifierInscriptionClasseBL.getListCodeTrimestre(LTrimestre);
           
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void cmdEnregistrer_Click(object sender, RoutedEventArgs e)
        {
            if ((txtMatricule.Text != null && cmbClasse.Text != null && cmbCategorieEleve.Text != null && txtAnneeScolaire.Text != null)
                && (txtMatricule.Text != "" && cmbClasse.Text != "" && cmbCategorieEleve.Text != "" && txtAnneeScolaire.Text != ""))
            { // si tous les champs sont non vides

                InscrireBE inscrire = new InscrireBE();
                inscrire.matricule = txtMatricule.Text;
                inscrire.codeClasse = cmbClasse.Text;
                inscrire.annee = Convert.ToInt16(txtAnnee.Text);
                CategorieEleveBE catEleve = new CategorieEleveBE();
                catEleve.codeCatEleve = cmbCategorieEleve.Text;
                inscrire.categorieEleve = creerModifierCategorieEleveBL.rechercherCategorieEleve(catEleve);
                EleveBE eleve = new EleveBE();
                eleve.matricule = txtMatricule.Text;
                inscrire.eleve = creerModifierInscriptionClasseBL.rechercherEleve(eleve);

                if (inscrire.eleve == null) {
                    MessageBox.Show("Cet élève n'existe pas dans la Base de donnée !");
                }
                else if (etat == 1)
                {
                    if (MessageBox.Show("Voulez-vous modifier l'inscription de cet élève ? Attention : Tous les informations scolaire et financier de l'élève seront supprimé si vous répondez pas l'affirmatif !!. ", "School Brain : Confimation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {

                        //on supprime l'élève
                        creerModifierInscriptionClasseBL.supprimerEleve(inscrire.eleve);
                        creerModifierInscriptionClasseBL.enregistrerEleve(inscrire.eleve);

                        //creerModifierInscriptionClasseBL.modifierInscrire(oldInscrire, inscrire);
                        creerModifierInscriptionClasseBL.creerInscrire(inscrire.codeClasse, inscrire.matricule, inscrire.annee);

                        List<InscrireBE> LInscrireBE = creerModifierInscriptionClasseBL.listerInscrireSuivantCritere("matricule = '" + txtMatricule.Text + "' AND annee = '" + txtAnnee.Text + "'");
                        AppartenirBE appartenir = new AppartenirBE();

                        appartenir.matricule = inscrire.matricule;
                        appartenir.annee = inscrire.annee;
                        appartenir.categorieeleve = inscrire.categorieEleve;
                        appartenir.codeCatEleve = inscrire.categorieEleve.codeCatEleve;

                        //creerModifierInscriptionClasseBL.modifierAppartenir(oldAppartenir, appartenir);
                        creerModifierInscriptionClasseBL.creerAppartenir(appartenir.codeCatEleve, appartenir.matricule, Convert.ToInt16(appartenir.annee));

                        CreerEtat fiche = new CreerEtat("Inscription - " + txtMatricule.Text + " - " + DateTime.Today.ToShortDateString(), "Fiche d'inscription");
                        fiche.EtatFicheInscription(inscrire.eleve, cmbClasse.Text, inscrire.categorieEleve.nomCatEleve);

                        if (LInscrireBE != null && LInscrireBE.Count != 0)
                        {
                            // on met la liste "LInscrireBE" dans le DataGrid
                            for (int i = 0; i < LInscrireBE.Count; i++)
                            {
                                EleveBE elv = new EleveBE();
                                elv.matricule = LInscrireBE.ElementAt(i).matricule;
                                LInscrireBE.ElementAt(i).eleve = creerModifierInscriptionClasseBL.rechercherEleve(elv);

                                AppartenirBE app = new AppartenirBE();
                                app = creerModifierInscriptionClasseBL.rechercherAppartenirSuivantCritere("matricule = '" + LInscrireBE.ElementAt(i).matricule + "' AND annee = '" + LInscrireBE.ElementAt(i).annee + "'");
                                //appartenir.annee = LInscrireBE.ElementAt(i).annee;
                                //appartenir.matricule = LInscrireBE.ElementAt(i).matricule;
                                //appartenir.

                                CategorieEleveBE catElv = new CategorieEleveBE();

                                catElv.codeCatEleve = app.codeCatEleve;

                                LInscrireBE.ElementAt(i).categorieEleve = creerModifierInscriptionClasseBL.rechercherCategorieEleve(catElv);

                            }

                        }

                        LInscrireBE = creerModifierInscriptionClasseBL.listerInscrireSuivantCritere("matricule = '" + txtMatricule.Text + "' AND annee = '" + txtAnnee.Text + "'");

                        if (LInscrireBE != null)
                        {
                            ListeInscriptionClasse.Clear();

                            for (int i = 0; i < LInscrireBE.Count; i++)
                            {
                                eleve = new EleveBE();
                                eleve.matricule = LInscrireBE.ElementAt(i).matricule;
                                LInscrireBE.ElementAt(i).eleve = creerModifierInscriptionClasseBL.rechercherEleve(eleve);

                                appartenir = new AppartenirBE();

                                appartenir = creerModifierInscriptionClasseBL.rechercherAppartenirSuivantCritere("matricule = '" + LInscrireBE.ElementAt(i).matricule + "' AND annee = '" + LInscrireBE.ElementAt(i).annee + "'");
                                //appartenir.annee = LInscrireBE.ElementAt(i).annee;
                                //appartenir.matricule = LInscrireBE.ElementAt(i).matricule;
                                //appartenir.

                                if (appartenir != null)
                                {
                                    catEleve = new CategorieEleveBE();

                                    catEleve.codeCatEleve = appartenir.codeCatEleve;

                                    LInscrireBE.ElementAt(i).categorieEleve = creerModifierInscriptionClasseBL.rechercherCategorieEleve(catEleve);
                                }

                                ListeInscriptionClasse.Add(LInscrireBE.ElementAt(i));
                            }
                        }

                        grdListeInscriptionClasse.ItemsSource = null;
                        grdListeInscriptionClasse.ItemsSource = LInscrireBE;
                        //RemplirDataGrid(LInscrireBE);

                        //txtMatricule.Text = "";
                        cmbClasse.Text = null;
                        cmbCategorieEleve.Text = null;
                        //lblInfoEleve.Content = "";
                        //txtAnnee.Text = Convert.ToString(System.DateTime.Today.Year);
                        ParametresBE param = creerModifierInscriptionClasseBL.getParametres();
                        if (param != null)
                        {
                            annee = param.annee;

                            txtAnnee.Text = Convert.ToString(param.annee);
                            txtAnneeScolaire.Text = (param.annee - 1).ToString();

                            txtAnneeMoyennesScolaire.Text = Convert.ToString(param.annee);
                            txtAnneeScolaireMoyennes.Text = (param.annee - 1).ToString();

                            txtAnneeResultatsScolaire.Text = Convert.ToString(param.annee);
                            txtAnneeScolaireResultats.Text = (param.annee - 1).ToString();

                            //***********

                        }
                        else
                        {
                            txtAnnee.Text = "";
                            txtAnneeScolaire.Text = "";

                            txtAnneeMoyennesScolaire.Text = "";
                            txtAnneeScolaireMoyennes.Text = "";

                            txtAnneeResultatsScolaire.Text = "";
                            txtAnneeScolaireResultats.Text = "";

                        }

                        //imageEleve.Source = null;

                        etat = 0;

                    }

                }
                else if (creerModifierInscriptionClasseBL.rechercherInscrire(inscrire) == null)
                { // si une incription de ce type n'existe pas deja dans la BD
                    if (creerModifierInscriptionClasseBL.rechercherInscrireSuivantCritere("matricule = '" + inscrire.matricule + "' AND annee = '" + inscrire.annee + "'") == null)
                    {
                        if (creerModifierInscriptionClasseBL.creerInscrire(cmbClasse.Text, txtMatricule.Text, Convert.ToInt16(txtAnnee.Text)))
                        { // si l'nregistrement a réussi

                            MessageBox.Show("Opération réussie");

                            //******************* on génère la fiche d'inscription de l'élève
                            //***************** DEBUT génération de la fiche d'inscription

                            EleveBE el = new EleveBE();
                            el.matricule = txtMatricule.Text;
                            el = creerModifierInscriptionClasseBL.rechercherEleve(el);

                            CategorieEleveBE c = new CategorieEleveBE();
                            c.codeCatEleve = cmbCategorieEleve.Text;
                            c = creerModifierCategorieEleveBL.rechercherCategorieEleve(c);

                            CreerEtat fiche = new CreerEtat("Inscription - "+txtMatricule.Text+" - " + DateTime.Today.ToShortDateString(), "Fiche d'inscription");
                            fiche.EtatFicheInscription(el, cmbClasse.Text, c.nomCatEleve);

                            //**************** FIN génération de la fiche d'inscription

                            txtMatricule.Text = "";
                            cmbClasse.Text = null;
                            cmbCategorieEleve.Text = null;
                            //txtAnnee.Text = Convert.ToString(System.DateTime.Today.Year);
                            ParametresBE param = creerModifierInscriptionClasseBL.getParametres();
                            if (param != null)
                            {
                                annee = param.annee;

                                txtAnnee.Text = Convert.ToString(param.annee);
                                txtAnneeScolaire.Text = (param.annee - 1).ToString();

                                txtAnneeMoyennesScolaire.Text = Convert.ToString(param.annee);
                                txtAnneeScolaireMoyennes.Text = (param.annee - 1).ToString();

                                txtAnneeResultatsScolaire.Text = Convert.ToString(param.annee);
                                txtAnneeScolaireResultats.Text = (param.annee - 1).ToString();

                                //***********

                            }
                            else
                            {
                                txtAnnee.Text = "";
                                txtAnneeScolaire.Text = "";

                                txtAnneeMoyennesScolaire.Text = "";
                                txtAnneeScolaireMoyennes.Text = "";

                                txtAnneeResultatsScolaire.Text = "";
                                txtAnneeScolaireResultats.Text = "";

                            }

                            imageEleve.Source = null;
                            lblInfoEleve.Content = "";

                            AppartenirBE appartenir = new AppartenirBE();
                            appartenir.annee = inscrire.annee;
                            appartenir.codeCatEleve = inscrire.categorieEleve.codeCatEleve;
                            appartenir.matricule = inscrire.matricule;

                            if (creerModifierInscriptionClasseBL.rechercherAppartenir(appartenir) == null)
                            {
                                if (! creerModifierInscriptionClasseBL.creerAppartenir(appartenir.codeCatEleve, appartenir.matricule, (int)appartenir.annee))
                                {
                                    MessageBox.Show("ERREUR : la catégorie de l'élève n'a pa pu être enregistrée !");
                                }

                            }
                            else MessageBox.Show("Cet élève a deja été enregistré dans cette catégorie pour l'année choisi !"); ;

                            List<InscrireBE> LInscrireBE = creerModifierInscriptionClasseBL.listerInscrireSuivantCritere("matricule = '" + txtMatricule.Text + "' AND annee = '" + txtAnnee.Text + "'");

                            if (LInscrireBE != null && LInscrireBE.Count != 0)
                            {
                                //on rafraichir le DataGrid
                                for (int i = 0; i < LInscrireBE.Count; i++)
                                {
                                    EleveBE elv = new EleveBE();
                                    elv.matricule = LInscrireBE.ElementAt(i).matricule;
                                    LInscrireBE.ElementAt(i).eleve = creerModifierInscriptionClasseBL.rechercherEleve(elv);

                                    AppartenirBE app = new AppartenirBE();
                                    app = creerModifierInscriptionClasseBL.rechercherAppartenirSuivantCritere("matricule = '" + LInscrireBE.ElementAt(i).matricule + "' AND annee = '" + LInscrireBE.ElementAt(i).annee + "'");
                                    //appartenir.annee = LInscrireBE.ElementAt(i).annee;
                                    //appartenir.matricule = LInscrireBE.ElementAt(i).matricule;
                                    //appartenir.
                                    if (app != null)
                                    {
                                        CategorieEleveBE catElv = new CategorieEleveBE();

                                        catElv.codeCatEleve = app.codeCatEleve;

                                        LInscrireBE.ElementAt(i).categorieEleve = creerModifierInscriptionClasseBL.rechercherCategorieEleve(catElv);
                                    }
                                }
                            }

                            RemplirDataGrid(LInscrireBE);


                        }
                        else MessageBox.Show("Echec enregistrement : une erreure est survenue !");
                    }
                    else MessageBox.Show("Cette élève est deja inscrit dans une classe pour cette année !");
                }
                else MessageBox.Show("Une Inscription de ce type existe deja dans le système \n \n Veuillez changer de code SVP !");
            }
            else MessageBox.Show("Tous les champs doivent pas être remplis !");
        }

        private void cmdAnnuler_Click(object sender, RoutedEventArgs e)
        {
            txtMatricule.Text = "";
            cmbEleve.Text = "";
            cmbClasse.Text = null;
            cmbCategorieEleve.Text = null;
            lblInfoEleve.Content = "";
            //txtAnnee.Text = "";
            ParametresBE param = creerModifierInscriptionClasseBL.getParametres();
            if (param != null)
            {
                annee = param.annee;

                txtAnnee.Text = Convert.ToString(param.annee);
                txtAnneeScolaire.Text = (param.annee - 1).ToString();

                txtAnneeMoyennesScolaire.Text = Convert.ToString(param.annee);
                txtAnneeScolaireMoyennes.Text = (param.annee - 1).ToString();

                txtAnneeResultatsScolaire.Text = Convert.ToString(param.annee);
                txtAnneeScolaireResultats.Text = (param.annee - 1).ToString();

                //***********

            }
            else
            {
                txtAnnee.Text = "";
                txtAnneeScolaire.Text = "";

                txtAnneeMoyennesScolaire.Text = "";
                txtAnneeScolaireMoyennes.Text = "";

                txtAnneeResultatsScolaire.Text = "";
                txtAnneeScolaireResultats.Text = "";

            }

            imageEleve.Source = null;

            grdListeInscriptionClasse.ItemsSource = null;

            etat = 0;

            eleveChoisi = null;
        }

        private void grdListeInscriptionClasse_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                if (grdListeInscriptionClasse.SelectedIndex != -1)
                {
                    if (MessageBox.Show("Voulez-vous vraiment supprimer cette inscription ? ", "School Brain : Confimation", MessageBoxButton.YesNo,MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        if (creerModifierInscriptionClasseBL.getParametres().annee == ListeInscriptionClasse.ElementAt(grdListeInscriptionClasse.SelectedIndex).annee)
                        {
                            AppartenirBE appartenir = new AppartenirBE();
                            appartenir.codeCatEleve = ListeInscriptionClasse.ElementAt(grdListeInscriptionClasse.SelectedIndex).categorieEleve.codeCatEleve;
                            appartenir.annee = ListeInscriptionClasse.ElementAt(grdListeInscriptionClasse.SelectedIndex).annee;
                            appartenir.matricule = ListeInscriptionClasse.ElementAt(grdListeInscriptionClasse.SelectedIndex).matricule;

                            if (creerModifierInscriptionClasseBL.supprinerAppartenir(appartenir))
                            {
                                if (creerModifierInscriptionClasseBL.supprinerInscrire(ListeInscriptionClasse.ElementAt(grdListeInscriptionClasse.SelectedIndex)))
                                    ListeInscriptionClasse.RemoveAt(grdListeInscriptionClasse.SelectedIndex);
                            }

                            grdListeInscriptionClasse.ItemsSource = ListeInscriptionClasse;

                        }
                        else
                        {
                            MessageBox.Show("Vous ne pouvez supprimer qu'une inscription de l'annee en cours ? ", "School Brain : Erreur de donnees", MessageBoxButton.OK, MessageBoxImage.Stop);
                        }
                        grdListeInscriptionClasse.UnselectAll();
                    }
                }

                grdListeInscriptionClasse.UnselectAll();
            }
        }

        private void grdListeInscriptionClasse_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (grdListeInscriptionClasse.SelectedIndex != -1)
            {
               
                oldInscrire = new InscrireBE();
                //oldInscrire = creerModifierInscriptionClasseBL.rechercherInscrire(ListeInscriptionClasse.ElementAt(grdListeInscriptionClasse.SelectedIndex));
                oldInscrire = ListeInscriptionClasse.ElementAt(grdListeInscriptionClasse.SelectedIndex);

                if (oldInscrire.annee == creerModifierInscriptionClasseBL.getParametres().annee)
                {
                    etat = 1;

                    oldAppartenir = new AppartenirBE();
                    oldAppartenir.codeCatEleve = ListeInscriptionClasse.ElementAt(grdListeInscriptionClasse.SelectedIndex).categorieEleve.codeCatEleve;
                    oldAppartenir.matricule = ListeInscriptionClasse.ElementAt(grdListeInscriptionClasse.SelectedIndex).matricule;
                    oldAppartenir.annee = ListeInscriptionClasse.ElementAt(grdListeInscriptionClasse.SelectedIndex).annee;


                    // on charge les informations dans le formulaire
                    txtMatricule.Text = oldInscrire.matricule;
                    cmbClasse.Text = oldInscrire.codeClasse;
                    cmbCategorieEleve.Text = oldInscrire.categorieEleve.codeCatEleve;

                    txtAnnee.Text = Convert.ToString(oldInscrire.annee);
                    txtAnneeScolaire.Text = Convert.ToString(oldInscrire.annee - 1);
                }
                else MessageBox.Show("Impossible de modifier cette inscription. \n \n Ce n'est pas une inscription de l'année en cours");

                grdListeInscriptionClasse.UnselectAll();
            }
        }

        private void txtMatricule_TextChanged(object sender, TextChangedEventArgs e)
        {
            //EleveBE eleve = new EleveBE();
            //eleve.matricule = txtMatricule.Text;

            //EleveBE eleve2 = creerModifierInscriptionClasseBL.rechercherEleve(eleve);

            //if (eleve2 != null)
            //{
            //    lblInfoEleve.Content = "[ Matricule : " + eleve2.matricule + ", Nom :" + eleve2.nom + ", Sexe :" + eleve2.sexe + ", Né le :" + eleve2.dateNaissance + ", Tel : " + eleve2.telephone + " ]";
            //    //imageEleve.Source = new BitmapImage(new Uri("../Images/"+ eleve2.photo+".jpg"));
            //    if (eleve2.photo != "")
            //        imageEleve.Source = new BitmapImage(new Uri("E:\\annuler2.jpg"));
            //    else imageEleve.Source = null;

            //    //*************** ensuite on charge dans le datagrid toutes les inscriptions de l'élève

            //    List<InscrireBE> LInscrireBE = creerModifierInscriptionClasseBL.listerInscrireSuivantCritere("matricule = '"+eleve2.matricule+"' AND annee = '" + txtAnnee.Text + "'");

            //    if (LInscrireBE != null)
            //    {
            //        for (int i = 0; i < LInscrireBE.Count; i++)
            //        {
            //            //EleveBE eleve = new EleveBE();
            //            //eleve.matricule = LInscrireBE.ElementAt(i).matricule;
            //            LInscrireBE.ElementAt(i).eleve = creerModifierInscriptionClasseBL.rechercherEleve(eleve2);

            //            AppartenirBE appartenir = new AppartenirBE();

            //            appartenir = creerModifierInscriptionClasseBL.rechercherAppartenirSuivantCritere("matricule = '" + LInscrireBE.ElementAt(i).matricule + "' AND annee = '" + LInscrireBE.ElementAt(i).annee + "'");
            //            //appartenir.annee = LInscrireBE.ElementAt(i).annee;
            //            //appartenir.matricule = LInscrireBE.ElementAt(i).matricule;
            //            //appartenir.

            //            if (appartenir != null)
            //            {
            //                CategorieEleveBE catEleve = new CategorieEleveBE();

            //                catEleve.codeCatEleve = appartenir.codeCatEleve;

            //                LInscrireBE.ElementAt(i).categorieEleve = creerModifierInscriptionClasseBL.rechercherCategorieEleve(catEleve);
            //            }
            //        }
            //    }
            //    // on met la liste "LInscrireBE" dans le DataGrid
            //    grdListeInscriptionClasse.ItemsSource = null;
            //    grdListeInscriptionClasse.ItemsSource = LInscrireBE;
            //    //RemplirDataGrid(LInscrireBE);


            //}
            //else
            //{
            //    lblInfoEleve.Content = "";
            //    imageEleve.Source = null;

            //    grdListeInscriptionClasse.ItemsSource = null;
            //}
        }

        private void txtMatricule_LostFocus(object sender, RoutedEventArgs e)
        {
            EleveBE eleve = new EleveBE();
            eleve.matricule = txtMatricule.Text;

            EleveBE eleve2 = creerModifierInscriptionClasseBL.rechercherEleve(eleve);

            eleveChoisi = eleve2;

            if (eleve2 != null)
            {
                lblInfoEleve.Content = "[Nom :" + eleve2.nom + "]";
                //imageEleve.Source = new BitmapImage(new Uri("../Images/"+ eleve2.photo+".jpg"));
                if (eleve2.photo != "")
                {
                    try
                    {
                        imageEleve.Source = new BitmapImage(new Uri(ConnexionUI.DOSSIER_PHOTO + eleve2.photo));
                        //imageEleve.Source = new BitmapImage(new Uri("..\\..\\Photos\\"+eleve2.photo));
                    }
                    catch (Exception) { imageEleve.Source = null; }
                }
                else imageEleve.Source = null;

                //*************** ensuite on charge dans le datagrid toutes les inscriptions de l'élève

                List<InscrireBE> LInscrireBE = creerModifierInscriptionClasseBL.listerInscrireSuivantCritere("matricule = '" + eleve2.matricule + "'");

                if (LInscrireBE != null)
                {
                    ListeInscriptionClasse.Clear();

                    for (int i = 0; i < LInscrireBE.Count; i++)
                    {
                        //EleveBE eleve = new EleveBE();
                        //eleve.matricule = LInscrireBE.ElementAt(i).matricule;
                        LInscrireBE.ElementAt(i).eleve = creerModifierInscriptionClasseBL.rechercherEleve(eleve2);

                        AppartenirBE appartenir = new AppartenirBE();

                        appartenir = creerModifierInscriptionClasseBL.rechercherAppartenirSuivantCritere("matricule = '" + LInscrireBE.ElementAt(i).matricule + "' AND annee = '" + LInscrireBE.ElementAt(i).annee + "'");
                        //appartenir.annee = LInscrireBE.ElementAt(i).annee;
                        //appartenir.matricule = LInscrireBE.ElementAt(i).matricule;
                        //appartenir.

                        if (appartenir != null)
                        {
                            CategorieEleveBE catEleve = new CategorieEleveBE();

                            catEleve.codeCatEleve = appartenir.codeCatEleve;

                            LInscrireBE.ElementAt(i).categorieEleve = creerModifierInscriptionClasseBL.rechercherCategorieEleve(catEleve);
                        }

                        ListeInscriptionClasse.Add(LInscrireBE.ElementAt(i));
                    }
                }
                // on met la liste "LInscrireBE" dans le DataGrid
                grdListeInscriptionClasse.ItemsSource = null;
                grdListeInscriptionClasse.ItemsSource = LInscrireBE;

                //on vide aussi les dataGrid qui sont dans les autres onglets
                grdMoyennesScolaire.ItemsSource = null;
                grdResultatsScolaire.ItemsSource = null;

                //on reset les comboBox qui sont dans les autres onglets
                cmbPeriodeTrimestreMoyennesScolaire.Text = null;
                cmbPeriodeSequenceMoyennesScolaire.Text = null;

                cmbPeriodeTrimestreResultatsScolaire.Text = null;
                cmbPeriodeSequenceResultatsScolaire.Text = null;

                //RemplirDataGrid(LInscrireBE);

                //on recherche si l'élève possède un résultat annuel. si c'est le cas dans le comboBox de classe
                //on ne charge que la classe à laquelle l'élève peut s'inscrire (en fonction de son résultat annuel précédent)
                ResultatAnnuelBE resultatAnnuel = creerModifierInscriptionClasseBL.RechercherResultatsAnnuelsDunEleve(txtMatricule.Text);
                if (resultatAnnuel != null)
                {
                    //on recherche la nouvelle classe de l'élève

                    int newNiveau = resultatAnnuel.newNiveau;
                    if (resultatAnnuel.newNiveau == 8)
                        newNiveau = 7;
                    List<String> LCodeClasse = creerModifierInscriptionClasseBL.getCodeClasseByNiveau(newNiveau);

                    cmbClasse.ItemsSource = null;
                    cmbClasse.ItemsSource = LCodeClasse;
                }
                else
                {

                    List<ClasseBE> LClasse = creerModifierInscriptionClasseBL.listerToutesLesClasses();
                    cmbClasse.ItemsSource = null;
                    cmbClasse.ItemsSource = creerModifierInscriptionClasseBL.getListCodeClasse(LClasse);
                }

            }
            else
            {
                lblInfoEleve.Content = "";
                imageEleve.Source = null;

                List<ClasseBE> LClasse = creerModifierInscriptionClasseBL.listerToutesLesClasses();
                cmbClasse.ItemsSource = null;
                cmbClasse.ItemsSource = creerModifierInscriptionClasseBL.getListCodeClasse(LClasse);

                grdListeInscriptionClasse.ItemsSource = null;

                //on vide aussi les dataGrid qui sont dans les autres onglets
                grdMoyennesScolaire.ItemsSource = null;
                grdResultatsScolaire.ItemsSource = null;

                //on reset les comboBox qui sont dans les autres onglets
                cmbPeriodeTrimestreMoyennesScolaire.Text = null;
                cmbPeriodeSequenceMoyennesScolaire.Text = null;

                cmbPeriodeTrimestreResultatsScolaire.Text = null;
                cmbPeriodeSequenceResultatsScolaire.Text = null;
            }
        }

        private void txtAnnee_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Utilitaires.IsTextAllowed(e.Text);
        }

        private void txtAnneeMoyennesScolaire_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Utilitaires.IsTextAllowed(e.Text);
        }

        private void txtAnneeResultatsScolaire_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Utilitaires.IsTextAllowed(e.Text);
        }

        private void cmdFermerInscrire_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cmdFermerMoyennesScolaire_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cmdFermerResultatsScolaire_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void txtMatricule_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                try
                {
                    EleveBE eleve = new EleveBE();
                    eleve.matricule = txtMatricule.Text;

                    EleveBE eleve2 = creerModifierInscriptionClasseBL.rechercherEleve(eleve);

                    eleveChoisi = eleve2;

                    if (eleve2 != null)
                    {
                        lblInfoEleve.Content = "[Nom :" + eleve2.nom + "]";
                        //imageEleve.Source = new BitmapImage(new Uri("../Images/"+ eleve2.photo+".jpg"));
                        if (eleve2.photo != "")
                        {
                            try
                            {
                                imageEleve.Source = new BitmapImage(new Uri(ConnexionUI.DOSSIER_PHOTO + eleve2.photo));
                            }
                            catch (Exception) { imageEleve.Source = null; }
                        }
                        else imageEleve.Source = null;

                        //*************** ensuite on charge dans le datagrid toutes les inscriptions de l'élève

                        List<InscrireBE> LInscrireBE = creerModifierInscriptionClasseBL.listerInscrireSuivantCritere("matricule = '" + eleve2.matricule + "'");

                        if (LInscrireBE != null)
                        {
                            ListeInscriptionClasse.Clear();

                            for (int i = 0; i < LInscrireBE.Count; i++)
                            {
                                //EleveBE eleve = new EleveBE();
                                //eleve.matricule = LInscrireBE.ElementAt(i).matricule;
                                LInscrireBE.ElementAt(i).eleve = creerModifierInscriptionClasseBL.rechercherEleve(eleve2);

                                AppartenirBE appartenir = new AppartenirBE();

                                appartenir = creerModifierInscriptionClasseBL.rechercherAppartenirSuivantCritere("matricule = '" + LInscrireBE.ElementAt(i).matricule + "' AND annee = '" + LInscrireBE.ElementAt(i).annee + "'");
                                //appartenir.annee = LInscrireBE.ElementAt(i).annee;
                                //appartenir.matricule = LInscrireBE.ElementAt(i).matricule;
                                //appartenir.

                                if (appartenir != null)
                                {
                                    CategorieEleveBE catEleve = new CategorieEleveBE();

                                    catEleve.codeCatEleve = appartenir.codeCatEleve;

                                    LInscrireBE.ElementAt(i).categorieEleve = creerModifierInscriptionClasseBL.rechercherCategorieEleve(catEleve);
                                }

                                ListeInscriptionClasse.Add(LInscrireBE.ElementAt(i));
                            }
                        }
                        // on met la liste "LInscrireBE" dans le DataGrid
                        grdListeInscriptionClasse.ItemsSource = null;
                        grdListeInscriptionClasse.ItemsSource = LInscrireBE;
                        //RemplirDataGrid(LInscrireBE);

                        //on vide aussi les dataGrid qui sont dans les autres onglets
                        grdMoyennesScolaire.ItemsSource = null;
                        grdResultatsScolaire.ItemsSource = null;

                        //on reset les comboBox qui sont dans les autres onglets
                        cmbPeriodeTrimestreMoyennesScolaire.Text = null;
                        cmbPeriodeSequenceMoyennesScolaire.Text = null;

                        cmbPeriodeTrimestreResultatsScolaire.Text = null;
                        cmbPeriodeSequenceResultatsScolaire.Text = null;


                        //on recherche si l'élève possède un résultat annuel. si c'est le cas dans le comboBox de classe
                        //on ne charge que la classe à laquelle l'élève peut s'inscrire (en fonction de son résultat annuel précédent)
                        ResultatAnnuelBE resultatAnnuel = creerModifierInscriptionClasseBL.RechercherResultatsAnnuelsDunEleve(txtMatricule.Text);
                        if (resultatAnnuel != null)
                        {
                            //on recherche la nouvelle classe de l'élève
                            int newNiveau = resultatAnnuel.newNiveau;
                            List<String> LCodeClasse = new List<string>();
                            if (resultatAnnuel.newNiveau == 6 || resultatAnnuel.newNiveau == 7)
                            {
                                List<String> LCodeClasse1 = creerModifierInscriptionClasseBL.getCodeClasseByNiveau(newNiveau);
                                List<String> LCodeClasse2 = creerModifierInscriptionClasseBL.getCodeClasseByNiveau(newNiveau - 1);
                                if (LCodeClasse1 != null && LCodeClasse1.Count != 0)
                                {
                                    for (int i = 0; i < LCodeClasse1.Count; i++) {
                                        LCodeClasse.Add(LCodeClasse1.ElementAt(i));
                                    }
                                }

                                if (LCodeClasse2 != null && LCodeClasse2.Count != 0)
                                {
                                    for (int i = 0; i < LCodeClasse2.Count; i++)
                                    {
                                        LCodeClasse.Add(LCodeClasse2.ElementAt(i));
                                    }
                                }

                            }
                            else
                            {
                                LCodeClasse = creerModifierInscriptionClasseBL.getCodeClasseByNiveau(newNiveau);
                            }

                            cmbClasse.ItemsSource = null;
                            cmbClasse.ItemsSource = LCodeClasse;
                        }
                        else
                        {

                            List<ClasseBE> LClasse = creerModifierInscriptionClasseBL.listerToutesLesClasses();
                            cmbClasse.ItemsSource = null;
                            cmbClasse.ItemsSource = creerModifierInscriptionClasseBL.getListCodeClasse(LClasse);
                        }

                    }
                    else
                    {
                        lblInfoEleve.Content = "";
                        imageEleve.Source = null;

                        List<ClasseBE> LClasse = creerModifierInscriptionClasseBL.listerToutesLesClasses();
                        cmbClasse.ItemsSource = null;
                        cmbClasse.ItemsSource = creerModifierInscriptionClasseBL.getListCodeClasse(LClasse);

                        grdListeInscriptionClasse.ItemsSource = null;

                        //on vide aussi les dataGrid qui sont dans les autres onglets
                        grdMoyennesScolaire.ItemsSource = null;
                        grdResultatsScolaire.ItemsSource = null;

                        //on reset les comboBox qui sont dans les autres onglets
                        cmbPeriodeTrimestreMoyennesScolaire.Text = null;
                        cmbPeriodeSequenceMoyennesScolaire.Text = null;

                        cmbPeriodeTrimestreResultatsScolaire.Text = null;
                        cmbPeriodeSequenceResultatsScolaire.Text = null;
                    }
                }

                catch (Exception err)
                {
                    MessageBox.Show("Une erreur est survenue \n" + err.Message, "School Brain : Erreur de données", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        

        private void cmbPeriodeSequenceMoyennesScolaire_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbPeriodeSequenceMoyennesScolaire.SelectedItem != null && cmbPeriodeSequenceMoyennesScolaire.SelectedItem != "")
            {
                if ((txtAnneeMoyennesScolaire.Text == null || txtAnneeMoyennesScolaire.Text == ""))
                {
                    MessageBox.Show("vous devez renseigner le champs texte");
                    //cmbPeriodeSequenceMoyennesScolaire.Text = null;
                }
                else if (txtMatricule.Text == "" || txtAnneeMoyennesScolaire.Text == "")
                {
                    MessageBox.Show("vous devez renseigner le matricule de l'élève dans l'onglet 'Inscription' !! '");
                    //cmbPeriodeSequenceMoyennesScolaire.Text = null;
                }
                else
                {
                    List<MoyennesScolaireEleveBE> LMoyennesScolaireEleve = new List<MoyennesScolaireEleveBE>();

                    //on recherche les moyennes séquentielles de l'élève
                    //if (cmbPeriodeSequenceMoyennesScolaire.SelectedItem.Equals("<Toutes Les Séquences>"))
                    //{
                    //    List<SequenceBE> LSequence = creerModifierInscriptionClasseBL.listerToutesLesSequences();

                    //    if (LSequence != null && LSequence.Count != 0)
                    //    {
                    //        for (int i = 0; i < LSequence.Count; i++)
                    //        {
                    //            List<MoyennesBE> LMoyennes = creerModifierInscriptionClasseBL.moyennesSequentiellesEleve(txtMatricule.Text, Convert.ToInt16(txtAnneeMoyennesScolaire.Text), LSequence.ElementAt(i).codeseq);

                    //            if (LMoyennes != null && LMoyennes.Count != 0)
                    //            {
                    //                for (int j = 0; j < LMoyennes.Count; j++)
                    //                {
                    //                    MoyennesScolaireEleveBE moyenneScolaireEleve = new MoyennesScolaireEleveBE();
                    //                    moyenneScolaireEleve.matricule = LMoyennes.ElementAt(j).matricule;
                    //                    moyenneScolaireEleve.codeMatiere = LMoyennes.ElementAt(j).codeMat;
                    //                    moyenneScolaireEleve.periode = LMoyennes.ElementAt(j).codeSeq;
                    //                    moyenneScolaireEleve.annee = LMoyennes.ElementAt(j).annee;
                    //                    moyenneScolaireEleve.moyenne = LMoyennes.ElementAt(j).moyenne;
                    //                    moyenneScolaireEleve.rang = LMoyennes.ElementAt(j).rang;
                    //                    moyenneScolaireEleve.mention = LMoyennes.ElementAt(j).mention;

                    //                    LMoyennesScolaireEleve.Add(moyenneScolaireEleve);
                    //                }
                    //            }
                    //        }


                    //    }
                    //}
                    //else
                   // {
                        
                        List<MoyennesBE> LMoyennes = creerModifierInscriptionClasseBL.moyennesSequentiellesEleve(txtMatricule.Text, Convert.ToInt16(txtAnneeMoyennesScolaire.Text), Convert.ToString(cmbPeriodeSequenceMoyennesScolaire.SelectedItem));

                        if (LMoyennes != null && LMoyennes.Count != 0)
                        {
                            for (int j = 0; j < LMoyennes.Count; j++)
                            {
                                MoyennesScolaireEleveBE moyenneScolaireEleve = new MoyennesScolaireEleveBE();
                                moyenneScolaireEleve.matricule = LMoyennes.ElementAt(j).matricule;
                                moyenneScolaireEleve.codeMatiere = LMoyennes.ElementAt(j).codeMat;
                                moyenneScolaireEleve.periode = LMoyennes.ElementAt(j).codeSeq;
                                moyenneScolaireEleve.annee = LMoyennes.ElementAt(j).annee;
                                moyenneScolaireEleve.moyenne = LMoyennes.ElementAt(j).moyenne;
                                moyenneScolaireEleve.rang = LMoyennes.ElementAt(j).rang;
                                moyenneScolaireEleve.mention = LMoyennes.ElementAt(j).mention;

                                LMoyennesScolaireEleve.Add(moyenneScolaireEleve);
                            }
                        }
                    //}

                    grdMoyennesScolaire.ItemsSource = null;
                    grdMoyennesScolaire.ItemsSource = LMoyennesScolaireEleve;

                }
            }
        }

        private void cmbPeriodeTrimestreMoyennesScolaire_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbPeriodeTrimestreMoyennesScolaire.SelectedItem != null && cmbPeriodeTrimestreMoyennesScolaire.SelectedItem != "")
            {
                if ((txtAnneeMoyennesScolaire.Text == null || txtAnneeMoyennesScolaire.Text == ""))
                {
                    MessageBox.Show("vous devez renseigner le champs texte");
                    //cmbPeriodeTrimestreMoyennesScolaire.Text = null;
                }
                else if (txtMatricule.Text == "" || txtAnneeMoyennesScolaire.Text == "")
                {
                    MessageBox.Show("vous devez renseigner le matricule de l'élève dans l'onglet 'Inscription' !! '");
                    //cmbPeriodeTrimestreMoyennesScolaire.Text = null;
                }
                else
                {

                    //on charge la liste des séquences faisant partie du trimestre choisi dans le combobox
                    List<SequenceBE> LSequence = creerModifierInscriptionClasseBL.listerSequencesSuivantCritere("codeTrimestre = '" + cmbPeriodeTrimestreMoyennesScolaire.SelectedItem + "'");
                    cmbPeriodeSequenceMoyennesScolaire.ItemsSource = creerModifierInscriptionClasseBL.getListCodeSequence(LSequence);


                    List<MoyennesScolaireEleveBE> LMoyennesScolaireEleve = new List<MoyennesScolaireEleveBE>();

                    //on recherche les moyennes séquentielles de l'élève
                    if (cmbPeriodeTrimestreMoyennesScolaire.SelectedItem.Equals("<Tous Les Trimestres>"))
                    {
                        List<TrimestreBE> LTrimestre = creerModifierInscriptionClasseBL.listerTousLesTrimestres();

                        if (LTrimestre != null && LTrimestre.Count != 0)
                        {
                            for (int i = 0; i < LTrimestre.Count; i++)
                            {
                                List<MoyennesTrimestrielsBE> LMoyennesLTrimestrielle = creerModifierInscriptionClasseBL.moyennesTrimestriellesEleve(txtMatricule.Text, Convert.ToInt16(txtAnneeMoyennesScolaire.Text), LTrimestre.ElementAt(i).codetrimestre);

                                if (LMoyennesLTrimestrielle != null && LMoyennesLTrimestrielle.Count != 0)
                                {
                                    for (int j = 0; j < LMoyennesLTrimestrielle.Count; j++)
                                    {
                                        MoyennesScolaireEleveBE moyenneScolaireEleve = new MoyennesScolaireEleveBE();
                                        moyenneScolaireEleve.matricule = LMoyennesLTrimestrielle.ElementAt(j).matricule;
                                        moyenneScolaireEleve.codeMatiere = LMoyennesLTrimestrielle.ElementAt(j).codeMat;
                                        moyenneScolaireEleve.periode = LMoyennesLTrimestrielle.ElementAt(j).codeTrimestre;
                                        moyenneScolaireEleve.annee = LMoyennesLTrimestrielle.ElementAt(j).annee;
                                        moyenneScolaireEleve.moyenne = LMoyennesLTrimestrielle.ElementAt(j).moyenne;
                                        moyenneScolaireEleve.rang = LMoyennesLTrimestrielle.ElementAt(j).rang;
                                        moyenneScolaireEleve.mention = LMoyennesLTrimestrielle.ElementAt(j).mention;

                                        LMoyennesScolaireEleve.Add(moyenneScolaireEleve);
                                    }
                                }
                            }


                        }
                    }
                    else
                    {
                        List<MoyennesTrimestrielsBE> LMoyennesLTrimestrielle = creerModifierInscriptionClasseBL.moyennesTrimestriellesEleve(txtMatricule.Text, Convert.ToInt16(txtAnneeMoyennesScolaire.Text), Convert.ToString(cmbPeriodeTrimestreMoyennesScolaire.SelectedItem));

                        if (LMoyennesLTrimestrielle != null && LMoyennesLTrimestrielle.Count != 0)
                        {
                            for (int j = 0; j < LMoyennesLTrimestrielle.Count; j++)
                            {
                                MoyennesScolaireEleveBE moyenneScolaireEleve = new MoyennesScolaireEleveBE();
                                moyenneScolaireEleve.matricule = LMoyennesLTrimestrielle.ElementAt(j).matricule;
                                moyenneScolaireEleve.codeMatiere = LMoyennesLTrimestrielle.ElementAt(j).codeMat;
                                moyenneScolaireEleve.periode = LMoyennesLTrimestrielle.ElementAt(j).codeTrimestre;
                                moyenneScolaireEleve.annee = LMoyennesLTrimestrielle.ElementAt(j).annee;
                                moyenneScolaireEleve.moyenne = LMoyennesLTrimestrielle.ElementAt(j).moyenne;
                                moyenneScolaireEleve.rang = LMoyennesLTrimestrielle.ElementAt(j).rang;
                                moyenneScolaireEleve.mention = LMoyennesLTrimestrielle.ElementAt(j).mention;

                                LMoyennesScolaireEleve.Add(moyenneScolaireEleve);
                            }
                        }
                    }

                    grdMoyennesScolaire.ItemsSource = null;
                    grdMoyennesScolaire.ItemsSource = LMoyennesScolaireEleve;

                }
            }
        }

        private void cmbPeriodeSequenceResultatsScolaire_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbPeriodeSequenceResultatsScolaire.SelectedItem != null && cmbPeriodeSequenceResultatsScolaire.SelectedItem != "")
            {
                if ((txtAnneeResultatsScolaire.Text == null || txtAnneeResultatsScolaire.Text == ""))
                {
                    MessageBox.Show("vous devez renseigner le champs texte 'Année' ");
                    //cmbPeriodeSequenceResultatsScolaire.Text = null;
                }
                else if (txtMatricule.Text == null || txtMatricule.Text == "")
                {
                    MessageBox.Show("vous devez renseigner le matricule de l'élève dans l'onglet 'Inscription' !! '");
                    //cmbPeriodeSequenceResultatsScolaire.Text = null;
                }
                else
                {
                    List<ResultatsScolaireEleveBE> LResultatsScolaireEleve = new List<ResultatsScolaireEleveBE>();

                    //on recherche les moyennes séquentielles de l'élève
                    //if (cmbPeriodeSequenceResultatsScolaire.SelectedItem.Equals("<Toutes Les Séquences>"))
                    //{
                    //    List<SequenceBE> LSequence = creerModifierInscriptionClasseBL.listerToutesLesSequences();

                    //    if (LSequence != null && LSequence.Count != 0)
                    //    {
                    //        for (int i = 0; i < LSequence.Count; i++)
                    //        {
                    //            List<ResultatBE> LResultatSequentiels = creerModifierInscriptionClasseBL.resultatsSequentiellesEleve(txtMatricule.Text, Convert.ToInt16(txtAnneeResultatsScolaire.Text), LSequence.ElementAt(i).codeseq);

                    //            if (LResultatSequentiels != null && LResultatSequentiels.Count != 0)
                    //            {
                    //                for (int j = 0; j < LResultatSequentiels.Count; j++)
                    //                {
                    //                    ResultatsScolaireEleveBE resultatScolaireEleveBE = new ResultatsScolaireEleveBE();
                    //                    resultatScolaireEleveBE.matricule = LResultatSequentiels.ElementAt(j).matricule;

                    //                    if (creerModifierInscriptionClasseBL.getClasseEleve(LResultatSequentiels.ElementAt(j).matricule, Convert.ToInt16(txtAnneeResultatsScolaire.Text)) != null)
                    //                        resultatScolaireEleveBE.codeClasse = creerModifierInscriptionClasseBL.getClasseEleve(LResultatSequentiels.ElementAt(j).matricule, Convert.ToInt16(txtAnneeResultatsScolaire.Text));
                    //                    else resultatScolaireEleveBE.codeClasse = "";

                    //                    resultatScolaireEleveBE.periode = LResultatSequentiels.ElementAt(j).codeseq;
                    //                    resultatScolaireEleveBE.annee = LResultatSequentiels.ElementAt(j).annee;
                    //                    resultatScolaireEleveBE.moyenne = LResultatSequentiels.ElementAt(j).moyenne;
                    //                    resultatScolaireEleveBE.rang = LResultatSequentiels.ElementAt(j).rang;
                    //                    resultatScolaireEleveBE.mention = LResultatSequentiels.ElementAt(j).mention;
                    //                    resultatScolaireEleveBE.decision = LResultatSequentiels.ElementAt(j).decision;

                    //                    LResultatsScolaireEleve.Add(resultatScolaireEleveBE);
                    //                }
                    //            }
                    //        }


                    //    }
                    //}
                    //else
                    //{
                        List<ResultatBE> LResultatSequentiels = creerModifierInscriptionClasseBL.resultatsSequentiellesEleve(txtMatricule.Text, Convert.ToInt16(txtAnneeResultatsScolaire.Text), Convert.ToString(cmbPeriodeSequenceResultatsScolaire.SelectedItem));

                        if (LResultatSequentiels != null && LResultatSequentiels.Count != 0)
                        {
                            for (int j = 0; j < LResultatSequentiels.Count; j++)
                            {
                                ResultatsScolaireEleveBE resultatScolaireEleveBE = new ResultatsScolaireEleveBE();
                                resultatScolaireEleveBE.matricule = LResultatSequentiels.ElementAt(j).matricule;

                                if (creerModifierInscriptionClasseBL.getClasseEleve(LResultatSequentiels.ElementAt(j).matricule, Convert.ToInt16(txtAnneeResultatsScolaire.Text)) != null)
                                    resultatScolaireEleveBE.codeClasse = creerModifierInscriptionClasseBL.getClasseEleve(LResultatSequentiels.ElementAt(j).matricule, Convert.ToInt16(txtAnneeResultatsScolaire.Text));
                                else resultatScolaireEleveBE.codeClasse = "";

                                resultatScolaireEleveBE.periode = LResultatSequentiels.ElementAt(j).codeseq;
                                resultatScolaireEleveBE.annee = LResultatSequentiels.ElementAt(j).annee;
                                resultatScolaireEleveBE.moyenne = LResultatSequentiels.ElementAt(j).moyenne;
                                resultatScolaireEleveBE.rang = LResultatSequentiels.ElementAt(j).rang;
                                resultatScolaireEleveBE.mention = LResultatSequentiels.ElementAt(j).mention;
                                resultatScolaireEleveBE.decision = LResultatSequentiels.ElementAt(j).decision;

                                LResultatsScolaireEleve.Add(resultatScolaireEleveBE);
                            }
                        }
                   // }

                    grdResultatsScolaire.ItemsSource = null;
                    grdResultatsScolaire.ItemsSource = LResultatsScolaireEleve;

                }
            }
        }

        private void cmbPeriodeTrimestreResultatsScolaire_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbPeriodeTrimestreResultatsScolaire.SelectedItem != null && cmbPeriodeTrimestreResultatsScolaire.SelectedItem != "")
            {
                if ((txtAnneeResultatsScolaire.Text == null || txtAnneeResultatsScolaire.Text == ""))
                {
                    MessageBox.Show("vous devez renseigner le champs texte");
                    //cmbPeriodeTrimestreResultatsScolaire.Text = null;
                }
                else if (txtMatricule.Text == null || txtMatricule.Text == "")
                {
                    MessageBox.Show("vous devez renseigner le matricule de l'élève dans l'onglet 'Inscription' !! '");
                    //cmbPeriodeTrimestreResultatsScolaire.Text = null;
                }
                else
                {
                    //on charge la liste des séquences faisant partie du trimestre choisi dans le combobox
                    List<SequenceBE> LSequence = creerModifierInscriptionClasseBL.listerSequencesSuivantCritere("codeTrimestre = '" + cmbPeriodeTrimestreResultatsScolaire.SelectedItem + "'");
                    cmbPeriodeSequenceResultatsScolaire.ItemsSource = creerModifierInscriptionClasseBL.getListCodeSequence(LSequence);


                    List<ResultatsScolaireEleveBE> LResultatsScolaireEleve = new List<ResultatsScolaireEleveBE>();

                    //on recherche les moyennes séquentielles de l'élève
                    if (cmbPeriodeTrimestreResultatsScolaire.SelectedItem.Equals("<Tous Les Trimestres>"))
                    {
                        List<TrimestreBE> LTrimestre = creerModifierInscriptionClasseBL.listerTousLesTrimestres();

                        if (LTrimestre != null && LTrimestre.Count != 0)
                        {
                            for (int i = 0; i < LTrimestre.Count; i++)
                            {
                                List<ResultatTrimestrielBE> LResultatTrimestriels = creerModifierInscriptionClasseBL.resultatsTrimestrielsEleve(txtMatricule.Text, Convert.ToInt16(txtAnneeResultatsScolaire.Text), LTrimestre.ElementAt(i).codetrimestre);

                                if (LResultatTrimestriels != null && LResultatTrimestriels.Count != 0)
                                {
                                    for (int j = 0; j < LResultatTrimestriels.Count; j++)
                                    {
                                        ResultatsScolaireEleveBE resultatScolaireEleveBE = new ResultatsScolaireEleveBE();
                                        resultatScolaireEleveBE.matricule = LResultatTrimestriels.ElementAt(j).matricule;

                                        if (creerModifierInscriptionClasseBL.getClasseEleve(LResultatTrimestriels.ElementAt(j).matricule, Convert.ToInt16(txtAnneeResultatsScolaire.Text)) != null)
                                            resultatScolaireEleveBE.codeClasse = creerModifierInscriptionClasseBL.getClasseEleve(LResultatTrimestriels.ElementAt(j).matricule, Convert.ToInt16(txtAnneeResultatsScolaire.Text));
                                        else resultatScolaireEleveBE.codeClasse = "";

                                        resultatScolaireEleveBE.periode = LResultatTrimestriels.ElementAt(j).codeTrimestre;
                                        resultatScolaireEleveBE.annee = LResultatTrimestriels.ElementAt(j).annee;
                                        resultatScolaireEleveBE.moyenne = LResultatTrimestriels.ElementAt(j).moyenne;
                                        resultatScolaireEleveBE.rang = LResultatTrimestriels.ElementAt(j).rang;
                                        resultatScolaireEleveBE.mention = LResultatTrimestriels.ElementAt(j).mention;
                                        resultatScolaireEleveBE.decision = LResultatTrimestriels.ElementAt(j).decision;

                                        LResultatsScolaireEleve.Add(resultatScolaireEleveBE);
                                    }
                                }
                            }


                        }
                    }
                    else
                    {
                        List<ResultatTrimestrielBE> LResultatTrimestriel = creerModifierInscriptionClasseBL.resultatsTrimestrielsEleve(txtMatricule.Text, Convert.ToInt16(txtAnneeResultatsScolaire.Text), Convert.ToString(cmbPeriodeTrimestreResultatsScolaire.SelectedItem));

                        if (LResultatTrimestriel != null && LResultatTrimestriel.Count != 0)
                        {
                            for (int j = 0; j < LResultatTrimestriel.Count; j++)
                            {
                                ResultatsScolaireEleveBE resultatScolaireEleveBE = new ResultatsScolaireEleveBE();
                                resultatScolaireEleveBE.matricule = LResultatTrimestriel.ElementAt(j).matricule;

                                if (creerModifierInscriptionClasseBL.getClasseEleve(LResultatTrimestriel.ElementAt(j).matricule, Convert.ToInt16(txtAnneeResultatsScolaire.Text)) != null)
                                    resultatScolaireEleveBE.codeClasse = creerModifierInscriptionClasseBL.getClasseEleve(LResultatTrimestriel.ElementAt(j).matricule, Convert.ToInt16(txtAnneeResultatsScolaire.Text));
                                else resultatScolaireEleveBE.codeClasse = "";

                                resultatScolaireEleveBE.periode = LResultatTrimestriel.ElementAt(j).codeTrimestre;
                                resultatScolaireEleveBE.annee = LResultatTrimestriel.ElementAt(j).annee;
                                resultatScolaireEleveBE.moyenne = LResultatTrimestriel.ElementAt(j).moyenne;
                                resultatScolaireEleveBE.rang = LResultatTrimestriel.ElementAt(j).rang;
                                resultatScolaireEleveBE.mention = LResultatTrimestriel.ElementAt(j).mention;
                                resultatScolaireEleveBE.decision = LResultatTrimestriel.ElementAt(j).decision;

                                LResultatsScolaireEleve.Add(resultatScolaireEleveBE);
                            }
                        }
                    }

                    grdResultatsScolaire.ItemsSource = null;
                    grdResultatsScolaire.ItemsSource = LResultatsScolaireEleve;

                }
            }
        }

        private void txtMatricule_GotFocus(object sender, RoutedEventArgs e)
        {
            cmbClasse.ItemsSource = null;
        }

        private void cmdImprimerInscrire_Click(object sender, RoutedEventArgs e)
        {
            if (eleveChoisi != null)
            {
                CreerEtat fiche = new CreerEtat("List_inscriptions -" + eleveChoisi.matricule, "Les des inscriptions de " + eleveChoisi.nom);
                fiche.obtenirEtat(grdListeInscriptionClasse);
            }
        }

        private void cmdImprimerMoyennesScolaire_Click(object sender, RoutedEventArgs e)
        {
            //***************** DEBUT génération de la fiche des moyennes scolaire de l'élève
            if (txtMatricule.Text != null && txtMatricule.Text != "")
            {
                //EleveBE el = new EleveBE();
                //el.matricule = txtMatricule.Text;
                //el = creerModifierInscriptionClasseBL.rechercherEleve(el);

                CreerEtat fiche = new CreerEtat("Moyennes - " + eleveChoisi.matricule, "Moyennes Scolaires de " + eleveChoisi.nom);
                fiche.obtenirEtat(grdMoyennesScolaire);
            }
            else MessageBox.Show("Vous devez d'abor saisir le matricule de l'élève dans l'onglet 'Inscription' !");

            //**************** FIN génération de la fiche moyennes scolaire de l'élève
        }

        private void cmdImprimerResultatsScolaire_Click(object sender, RoutedEventArgs e)
        {
            //***************** DEBUT génération de la fiche des résultats scolaire de l'élève
            if (txtMatricule.Text != null && txtMatricule.Text != "")
            {
                //EleveBE el = new EleveBE();
                //el.matricule = txtMatricule.Text;
                //el = creerModifierInscriptionClasseBL.rechercherEleve(el);

                CreerEtat fiche = new CreerEtat("Résultats - " + eleveChoisi.matricule, "Résultats Scolaires de " + eleveChoisi.nom);
                fiche.obtenirEtat(grdResultatsScolaire);
            }
            else MessageBox.Show("Vous devez d'abor saisir le matricule de l'élève dans l'onglet 'Inscription' !");

            //**************** FIN génération de la fiche des résultats scolaire de l'élève
        }

        private void cmdProfilAcademique_Click(object sender, RoutedEventArgs e)
        {
            if (eleveChoisi != null)
            {
                CreerEtat fiche = new CreerEtat("Profil_Académique -" + eleveChoisi.matricule, "Profil academique de " + eleveChoisi.nom);
                List<MoyennesBE> LMoyennes = creerModifierInscriptionClasseBL.listeMoyennesEleve(eleveChoisi.matricule);
                if (LMoyennes != null)
                    fiche.etatProfilAcadémique(LMoyennes);
                else MessageBox.Show("Cette élève n'a aucune moyenne dans le système !");
            }
            else MessageBox.Show("Aucun élève choisit ! ", "School Brain : Alerte");
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

        private void txtAnneeScolaireMoyennes_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                annee = Convert.ToInt32(txtAnneeScolaireMoyennes.Text) + 1;
                txtAnneeMoyennesScolaire.Text = annee.ToString();

                cmbPeriodeSequenceMoyennesScolaire.Text = "";
                cmbPeriodeTrimestreMoyennesScolaire.Text = "";

            }
            catch (Exception)
            {
                MessageBox.Show("L'annee doit etre un nombre positif", "School brain:Alerte");
            }
        }

        private void txtAnneeScolaireResultats_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                annee = Convert.ToInt32(txtAnneeScolaireResultats.Text) + 1;
                txtAnneeResultatsScolaire.Text = annee.ToString();

                cmbPeriodeSequenceResultatsScolaire.Text = "";
                cmbPeriodeTrimestreResultatsScolaire.Text = "";

            }
            catch (Exception)
            {
                MessageBox.Show("L'annee doit etre un nombre positif", "School brain:Alerte");
            }
        }

        //------------------RAOUL---------------------------------

        private void txtAnneeScolaire_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Utilitaires.IsTextAllowed(e.Text);
        }

        private void txtAnneeScolaireMoyennes_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Utilitaires.IsTextAllowed(e.Text);
        }

        private void txtAnneeScolaireResultats_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Utilitaires.IsTextAllowed(e.Text);
        }

        private void cmbClasse_DropDownClosed(object sender, EventArgs e)
        {
            if (cmbClasse1.Text != null && cmbClasse1.Text != "")
            {
                //recherche des eleves inscrits  dans cette classe pour charger le combobox des eleves
                eleves = new List<string>();
                string codeclasse = cmbClasse1.Text;
                List<EleveBE> listeleves = new List<EleveBE>();
                listeleves = creerModifierInscriptionClasseBL.listerElevesDuneClasse(codeclasse, annee);
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
                
                try
                {
                    EleveBE eleve = new EleveBE();
                    eleve.matricule = txtMatricule.Text;

                    EleveBE eleve2 = creerModifierInscriptionClasseBL.rechercherEleve(eleve);

                    eleveChoisi = eleve2;

                    if (eleve2 != null)
                    {
                        lblInfoEleve.Content = "[Nom :" + eleve2.nom + "]";
                        //imageEleve.Source = new BitmapImage(new Uri("../Images/"+ eleve2.photo+".jpg"));
                        if (eleve2.photo != "")
                        {
                            try
                            {
                                imageEleve.Source = new BitmapImage(new Uri(ConnexionUI.DOSSIER_PHOTO + eleve2.photo));
                            }
                            catch (Exception) { imageEleve.Source = null; }
                        }
                        else imageEleve.Source = null;

                        //*************** ensuite on charge dans le datagrid toutes les inscriptions de l'élève

                        List<InscrireBE> LInscrireBE = creerModifierInscriptionClasseBL.listerInscrireSuivantCritere("matricule = '" + eleve2.matricule + "'");

                        if (LInscrireBE != null)
                        {
                            ListeInscriptionClasse.Clear();

                            for (int i = 0; i < LInscrireBE.Count; i++)
                            {
                                //EleveBE eleve = new EleveBE();
                                //eleve.matricule = LInscrireBE.ElementAt(i).matricule;
                                LInscrireBE.ElementAt(i).eleve = creerModifierInscriptionClasseBL.rechercherEleve(eleve2);

                                AppartenirBE appartenir = new AppartenirBE();

                                appartenir = creerModifierInscriptionClasseBL.rechercherAppartenirSuivantCritere("matricule = '" + LInscrireBE.ElementAt(i).matricule + "' AND annee = '" + LInscrireBE.ElementAt(i).annee + "'");
                                //appartenir.annee = LInscrireBE.ElementAt(i).annee;
                                //appartenir.matricule = LInscrireBE.ElementAt(i).matricule;
                                //appartenir.

                                if (appartenir != null)
                                {
                                    CategorieEleveBE catEleve = new CategorieEleveBE();

                                    catEleve.codeCatEleve = appartenir.codeCatEleve;

                                    LInscrireBE.ElementAt(i).categorieEleve = creerModifierInscriptionClasseBL.rechercherCategorieEleve(catEleve);
                                }

                                ListeInscriptionClasse.Add(LInscrireBE.ElementAt(i));
                            }
                        }
                        // on met la liste "LInscrireBE" dans le DataGrid
                        grdListeInscriptionClasse.ItemsSource = null;
                        grdListeInscriptionClasse.ItemsSource = LInscrireBE;
                        //RemplirDataGrid(LInscrireBE);

                        //on vide aussi les dataGrid qui sont dans les autres onglets
                        grdMoyennesScolaire.ItemsSource = null;
                        grdResultatsScolaire.ItemsSource = null;

                        //on reset les comboBox qui sont dans les autres onglets
                        cmbPeriodeTrimestreMoyennesScolaire.Text = null;
                        cmbPeriodeSequenceMoyennesScolaire.Text = null;

                        cmbPeriodeTrimestreResultatsScolaire.Text = null;
                        cmbPeriodeSequenceResultatsScolaire.Text = null;


                        //on recherche si l'élève possède un résultat annuel. si c'est le cas dans le comboBox de classe
                        //on ne charge que la classe à laquelle l'élève peut s'inscrire (en fonction de son résultat annuel précédent)
                        ResultatAnnuelBE resultatAnnuel = creerModifierInscriptionClasseBL.RechercherResultatsAnnuelsDunEleve(txtMatricule.Text);
                        if (resultatAnnuel != null)
                        {
                            //on recherche la nouvelle classe de l'élève
                            int newNiveau = resultatAnnuel.newNiveau;
                            List<String> LCodeClasse = new List<string>();
                            if (resultatAnnuel.newNiveau == 6 || resultatAnnuel.newNiveau == 7)
                            {
                                List<String> LCodeClasse1 = creerModifierInscriptionClasseBL.getCodeClasseByNiveau(newNiveau);
                                List<String> LCodeClasse2 = creerModifierInscriptionClasseBL.getCodeClasseByNiveau(newNiveau - 1);
                                if (LCodeClasse1 != null && LCodeClasse1.Count != 0)
                                {
                                    for (int i = 0; i < LCodeClasse1.Count; i++) {
                                        LCodeClasse.Add(LCodeClasse1.ElementAt(i));
                                    }
                                }

                                if (LCodeClasse2 != null && LCodeClasse2.Count != 0)
                                {
                                    for (int i = 0; i < LCodeClasse2.Count; i++)
                                    {
                                        LCodeClasse.Add(LCodeClasse2.ElementAt(i));
                                    }
                                }

                            }
                            else
                            {
                                LCodeClasse = creerModifierInscriptionClasseBL.getCodeClasseByNiveau(newNiveau);
                            }

                            cmbClasse.ItemsSource = null;
                            cmbClasse.ItemsSource = LCodeClasse;
                        }
                        else
                        {

                            List<ClasseBE> LClasse = creerModifierInscriptionClasseBL.listerToutesLesClasses();
                            cmbClasse.ItemsSource = null;
                            cmbClasse.ItemsSource = creerModifierInscriptionClasseBL.getListCodeClasse(LClasse);
                        }

                    }
                    else
                    {
                        lblInfoEleve.Content = "";
                        imageEleve.Source = null;

                        List<ClasseBE> LClasse = creerModifierInscriptionClasseBL.listerToutesLesClasses();
                        cmbClasse.ItemsSource = null;
                        cmbClasse.ItemsSource = creerModifierInscriptionClasseBL.getListCodeClasse(LClasse);

                        grdListeInscriptionClasse.ItemsSource = null;

                        //on vide aussi les dataGrid qui sont dans les autres onglets
                        grdMoyennesScolaire.ItemsSource = null;
                        grdResultatsScolaire.ItemsSource = null;

                        //on reset les comboBox qui sont dans les autres onglets
                        cmbPeriodeTrimestreMoyennesScolaire.Text = null;
                        cmbPeriodeSequenceMoyennesScolaire.Text = null;

                        cmbPeriodeTrimestreResultatsScolaire.Text = null;
                        cmbPeriodeSequenceResultatsScolaire.Text = null;
                    }
                }

                catch (Exception err)
                {
                    MessageBox.Show("Une erreur est survenue \n" + err.Message, "School Brain : Erreur de données", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        //----------------------FIN RAOUL---------------------------------

    }
}
