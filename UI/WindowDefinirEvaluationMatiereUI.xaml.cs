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
    /// Interaction logic for WindowDefinirEvaluationMatiereUI.xaml
    /// </summary>
    public partial class WindowDefinirEvaluationMatiereUI : Window
    {
        DefinirEvaluationMatiereBL definirEvaluationMatiereBL;

        List<String> LCodeTypeEvaluation;
        private int etat; // idique si nous sommes en création (0) ou en modification (1)

        private int etatGrid; // mémorise dans quel état se trouvait la liste (0 pour vide et 1 si elle contenait des infos)

        private int indexAmodifier; // garde l'index de l'élément à modifier

        int annee;

        // Définition d'une liste 'ListeTitulaires' observable de 'Titulaire des classes'
        public ObservableCollection<EvaluerBE> ListeEvaluationMatieres { get; set; }

        //on défini une liste (pour pouvoir controler après que la somme des pourcentages est égale à 100)
        List<EvaluerBE> LEvaluer;

        // Fonction permettant de remplir le DataGrid avec les informations de la base de données
        // @param : - listObjet : la liste des objets à afficher dans le DataGrid
        public void RemplirDataGrid(List<EvaluerBE> listObjet)
        {
            // Ajout de données dans la DataTable :
            var table = new DataTable();

            table.Columns.Add(new DataColumn("codeEvaluation", typeof(string)));
            table.Columns.Add(new DataColumn("codeMat", typeof(string)));
            table.Columns.Add(new DataColumn("codeClasse", typeof(string)));
            table.Columns.Add(new DataColumn("poids", typeof(int)));
            table.Columns.Add(new DataColumn("annee", typeof(int)));
            table.Columns.Add(new DataColumn("codeSequence", typeof(string)));

            table.Columns.Add(new DataColumn("classe", typeof(ClasseBE)));
            table.Columns.Add(new DataColumn("typeEvaluation", typeof(TypeevaluationBE)));
            table.Columns.Add(new DataColumn("matiere", typeof(MatiereBE)));

            if (listObjet != null)
            {

                for (int i = 0; i < listObjet.Count; i++)
                {
                    DataRow dr = table.NewRow();
                    dr["codeEvaluation"] = listObjet.ElementAt(i).codeEvaluation;
                    dr["codeMat"] = listObjet.ElementAt(i).codeMat;
                    dr["codeClasse"] = listObjet.ElementAt(i).codeClasse;
                    dr["poids"] = listObjet.ElementAt(i).poids;
                    dr["annee"] = listObjet.ElementAt(i).annee;
                    dr["codeSequence"] = listObjet.ElementAt(i).codeSequence;

                    dr["classe"] = listObjet.ElementAt(i).classe;
                    dr["typeEvaluation"] = listObjet.ElementAt(i).typeEvaluation;
                    dr["matiere"] = listObjet.ElementAt(i).matiere;

                    table.Rows.Add(dr);
                }
            }

            string vCodeEvaluation = "";
            string vCodeMat = "";
            string vCodeClasse = "";
            int vPoids;
            int annee = Convert.ToInt16(System.DateTime.Today.Year);
            string codeSequence = "";
            ClasseBE classe = new ClasseBE();
            TypeevaluationBE typeEvaluation = new TypeevaluationBE();
            MatiereBE matiere = new MatiereBE();

            ListeEvaluationMatieres.Clear();

            foreach (DataRow row in table.Rows)
            {
                vCodeEvaluation = Convert.ToString(row["codeEvaluation"]);
                vCodeMat = Convert.ToString(row["codeMat"]);
                vCodeClasse = Convert.ToString(row["codeClasse"]);
                vPoids = Convert.ToInt16(row["poids"]);
                annee = Convert.ToInt16(row["annee"]);
                codeSequence = Convert.ToString(row["codeSequence"]);

                classe = (ClasseBE)row["classe"];
                typeEvaluation = (TypeevaluationBE)row["typeEvaluation"];
                matiere = (MatiereBE)row["matiere"];

                EvaluerBE evaluer = new EvaluerBE();
                evaluer.codeEvaluation = vCodeEvaluation;
                evaluer.codeMat = vCodeMat;
                evaluer.codeClasse = vCodeClasse;
                evaluer.poids = vPoids;
                evaluer.annee = annee;
                evaluer.codeSequence = codeSequence;

                evaluer.classe = classe;
                evaluer.typeEvaluation = typeEvaluation;
                evaluer.matiere = matiere;

                ListeEvaluationMatieres.Add(evaluer);

            }

            grdListeEvaluationMatiere.ItemsSource = ListeEvaluationMatieres;
        }

        public WindowDefinirEvaluationMatiereUI()
        {
            InitializeComponent();

            definirEvaluationMatiereBL = new DefinirEvaluationMatiereBL();
            LCodeTypeEvaluation = new List<String>();

            LEvaluer = new List<EvaluerBE>();

            etat = 0;

            etatGrid = 0;
            // A mettre pour que le binding avec le DataGrid fonctionne !
            grdListeEvaluationMatiere.DataContext = this;

            // par défaut l'année est l'année courante
            //txtAnnee.Text = Convert.ToString(System.DateTime.Today.Year);
            ParametresBE param = definirEvaluationMatiereBL.getParametres();
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
            ListeEvaluationMatieres = new ObservableCollection<EvaluerBE>();

            //oldEvaluer = new EvaluerBE();

            // ------------------- Chargement de la liste des codes de classe dans le comboBox de la fenêtre 
            //(utile pour le filtre)
            List<ClasseBE> LClasse = definirEvaluationMatiereBL.listerTousLesClasseOrderByNiveau();
            cmbClasse.ItemsSource = definirEvaluationMatiereBL.getListCodeClasse(LClasse);

            List<SequenceBE> LSequence = definirEvaluationMatiereBL.listerToutesLesSequences();
            cmbSequence.ItemsSource = definirEvaluationMatiereBL.getListCodeSequence2(LSequence);
        }

        private void cmbClasse_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (txtAnnee.Text != null && txtAnnee.Text != "")
            {
                if (cmbClasse.SelectedItem != null && cmbClasse.SelectedItem != "")
                {
                    ClasseBE classe = new ClasseBE();
                    classe.codeClasse = Convert.ToString(cmbClasse.SelectedItem);

                    // on charge les matières de la classe sélectionnée dans le comboBox
                    List<MatiereBE> LMatiere = definirEvaluationMatiereBL.ListeMatiereDuneClasse(classe, Convert.ToInt16(txtAnnee.Text));

                    List<String> LCodeMatiere =  definirEvaluationMatiereBL.getListCodeMatiere(LMatiere);
                    cmbMatiere.ItemsSource = definirEvaluationMatiereBL.getListCodeMatiere2(LMatiere);
                    
                }
            }

        }

        private void cmdAjouter_Click(object sender, RoutedEventArgs e)
        {
            if ((cmbClasse.Text != null && txtAnnee.Text != null && cmbMatiere.Text != null && cmbEvaluation.Text != null && txtPourcentage.Text != null && cmbSequence.Text != null)
                && (cmbClasse.Text != "" && txtAnnee.Text != "" && cmbMatiere.Text != "" && cmbEvaluation.Text != "" && txtPourcentage.Text != "" && cmbSequence.Text != ""))
            {

                EvaluerBE evaluer = new EvaluerBE();
                evaluer.codeEvaluation = cmbEvaluation.Text;
                evaluer.codeMat = cmbMatiere.Text;
                evaluer.codeClasse = cmbClasse.Text;
                evaluer.poids = Convert.ToInt16(txtPourcentage.Text);
                evaluer.annee = Convert.ToInt16(txtAnnee.Text);
                evaluer.codeSequence = cmbSequence.Text;

                if (etat == 1)
                {
                    //on modifit l'élément dans la liste 
                    LEvaluer.ElementAt(indexAmodifier).codeEvaluation = evaluer.codeEvaluation;
                    LEvaluer.ElementAt(indexAmodifier).codeClasse = evaluer.codeClasse;
                    LEvaluer.ElementAt(indexAmodifier).codeMat = evaluer.codeMat;
                    LEvaluer.ElementAt(indexAmodifier).poids = evaluer.poids;
                    LEvaluer.ElementAt(indexAmodifier).annee = evaluer.annee;
                    LEvaluer.ElementAt(indexAmodifier).codeSequence = evaluer.codeSequence;

                    ////on charge les évaluations de la matière pour la classe
                    //List<EvaluerBE> LEvaluerBE = definirEvaluationMatiereBL.listerEvaluersSuivantCritere("codemat = '" + evaluer.codeMat + "' AND codeclasse = '"+evaluer.codeClasse+"' AND annee = '"+txtAnnee.Text+"'");

                    // on met la liste "LEvaluerBE" dans le DataGrid
                    RemplirDataGrid(LEvaluer);

                    //txtAnnee.Text = Convert.ToString(System.DateTime.Today.Year);
                    //cmbClasse.Text = null;
                    //cmbMatiere.Text = null;
                    cmbEvaluation.Text = null;
                    txtPourcentage.Text = "";

                    etat = 0;
                }
                else
                { // si une évaluation de ce type n'a pas encor été définie pour cette classe dans cette année
                    LEvaluer.Add(evaluer);

                    RemplirDataGrid(LEvaluer);

                    //on retire le code d'évaluation de la liste
                    LCodeTypeEvaluation.RemoveAt(cmbEvaluation.SelectedIndex);
                    cmbEvaluation.Text = null;
                    cmbEvaluation.ItemsSource = null;
                    cmbEvaluation.ItemsSource = LCodeTypeEvaluation;
                    txtPourcentage.Text = "";

                }

            }
            else MessageBox.Show("Tous les champs doivent pas être remplis !");
        }

        private void cmdValider_Click(object sender, RoutedEventArgs e)
        {
            if (LEvaluer == null || LEvaluer.Count == 0)
            {
                if (cmbMatiere.Text.Equals("<Toutes les matières>"))
                {

                    if (cmbMatiere.Text.Equals("<Toutes les séquences>"))
                    {

                        ClasseBE classe = new ClasseBE();
                        classe.codeClasse = cmbClasse.Text;
                        classe = definirEvaluationMatiereBL.rechercherClasse(classe);
                        // on charge les matières de la classe sélectionnée dans le comboBox
                        List<MatiereBE> LMatiere = definirEvaluationMatiereBL.ListeMatiereDuneClasse(classe, Convert.ToInt16(txtAnnee.Text));

                        if (LMatiere != null && LMatiere.Count != 0)
                        {
                            List<SequenceBE> LSequence = definirEvaluationMatiereBL.listerToutesLesSequences();
                            if (LSequence != null && LSequence.Count != 0)
                            {

                                bool t = true;
                                for (int i = 0; i < LMatiere.Count; i++)
                                {
                                    for (int j = 0; j < LSequence.Count; j++)
                                    {
                                        if (definirEvaluationMatiereBL.supprinerEvaluerSuivantCritere("codemat = '" + LMatiere.ElementAt(i).codeMat + "' AND codeclasse = '" + cmbClasse.SelectedItem + "' AND annee = '" + txtAnnee.Text + "' AND codeseq = '" + LSequence.ElementAt(j).codeseq + "'"))
                                        {

                                            if (etatGrid == 0)
                                                MessageBox.Show("Vous devez Définir des types d'évaluation avant de valider  !");
                                            else
                                            {
                                                MessageBox.Show("Types d'évaluation Enregistré !");
                                                cmbClasse.Text = null;
                                                ParametresBE param = definirEvaluationMatiereBL.getParametres();
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

                                                etatGrid = 0;
                                            }
                                        }
                                        else t = false;
                                    }
                                }

                                if (t == false)
                                    MessageBox.Show("Echec. Une erreur est survenu l'ors de l'enregistrement !");
                            }

                        }

                    }
                    else
                    {

                        ClasseBE classe = new ClasseBE();
                        classe.codeClasse = cmbClasse.Text;
                        classe = definirEvaluationMatiereBL.rechercherClasse(classe);
                        // on charge les matières de la classe sélectionnée dans le comboBox
                        List<MatiereBE> LMatiere = definirEvaluationMatiereBL.ListeMatiereDuneClasse(classe, Convert.ToInt16(txtAnnee.Text));

                        if (LMatiere != null && LMatiere.Count != 0)
                        {

                            bool t = true;
                            for (int i = 0; i < LMatiere.Count; i++)
                            {
                                if (definirEvaluationMatiereBL.supprinerEvaluerSuivantCritere("codemat = '" + LMatiere.ElementAt(i).codeMat + "' AND codeclasse = '" + cmbClasse.SelectedItem + "' AND annee = '" + txtAnnee.Text + "' AND codeseq = '" + cmbSequence.SelectedItem + "'"))
                                {

                                    if (etatGrid == 0)
                                        MessageBox.Show("Vous devez Définir des types d'évaluation avant de valider  !");
                                    else
                                    {
                                        MessageBox.Show("Types d'évaluation Enregistré !");
                                        cmbClasse.Text = null;
                                        ParametresBE param = definirEvaluationMatiereBL.getParametres();
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

                                        etatGrid = 0;
                                    }
                                }
                                else t = false;

                                if (t == false)
                                    MessageBox.Show("Echec. Une erreur est survenu l'ors de l'enregistrement !");
                            }

                        }
                    }
                }
            }

            // on vérifit si la somme des pourcentage est égales à 100
            if (LEvaluer != null)
            {
                if (LEvaluer.Count != 0)
                {
                    int somme = 0;
                    for (int i = 0; i < LEvaluer.Count; i++)
                    {
                        somme += LEvaluer.ElementAt(i).poids;
                    }

                    if (somme != 100)
                        MessageBox.Show("la somme des pourcentages doit être égale à '100'");
                    else
                    {
                        if (cmbSequence.Text.Equals("<Toutes les Séquences>"))
                        {
                            List<SequenceBE> LSequence = definirEvaluationMatiereBL.listerToutesLesSequences();

                            if (cmbMatiere.Text.Equals("<Toutes les matières>"))
                            {
                                ClasseBE classe = new ClasseBE();
                                classe.codeClasse = cmbClasse.Text;
                                classe = definirEvaluationMatiereBL.rechercherClasse(classe);

                                //on liste toutes les matières programmées dans la classe
                                List<MatiereBE> LMatiere = definirEvaluationMatiereBL.ListeMatiereDuneClasse(classe, Convert.ToInt16(txtAnnee.Text));

                                if(LMatiere != null && LMatiere.Count != 0){
                                    Boolean t = true;
                                    for(int k=0; k<LMatiere.Count; k++){
                                        for (int i = 0; i < LSequence.Count; i++)
                                        {
                                            //on supprime tos les types d'évaluation qui ont deja été enregistré pour la matière, la classe et l'année choisi
                                            definirEvaluationMatiereBL.supprinerEvaluerSuivantCritere("codemat = '" + LMatiere.ElementAt(k).codeMat + "' AND codeclasse = '" + cmbClasse.SelectedItem + "' AND annee = '" + LEvaluer.ElementAt(0).annee + "' AND codeseq = '" + LSequence.ElementAt(i).codeseq + "'");

                                            for (int j = 0; j < LEvaluer.Count; j++)
                                            {
                                                if (!definirEvaluationMatiereBL.creerEvaluer(LEvaluer.ElementAt(j).codeEvaluation, LMatiere.ElementAt(k).codeMat, LEvaluer.ElementAt(j).codeClasse, LEvaluer.ElementAt(j).poids, LEvaluer.ElementAt(j).annee, LSequence.ElementAt(i).codeseq))
                                                  t = false;
                                             }

                                         }
                                    }

                                    if (t == true)
                                     {
                                         //tous les éléments ont bien été enregistrés
                                         MessageBox.Show("Types d'évaluation Enregistré !");
                                      }
                                     else MessageBox.Show("Echec. Une erreur est survenu l'ors de l'enregistrement !");

                                }

                            }else{

                                Boolean t = true;

                                for (int i = 0; i < LSequence.Count; i++)
                                {
                                    //on supprime tos les types d'évaluation qui ont deja été enregistré pour la matière, la classe et l'année choisi
                                    definirEvaluationMatiereBL.supprinerEvaluerSuivantCritere("codemat = '" + cmbMatiere.SelectedItem + "' AND codeclasse = '" + cmbClasse.SelectedItem + "' AND annee = '" + LEvaluer.ElementAt(0).annee + "' AND codeseq = '" + LSequence.ElementAt(i).codeseq + "'");

                                    for (int j = 0; j < LEvaluer.Count; j++)
                                    {
                                        if (!definirEvaluationMatiereBL.creerEvaluer(LEvaluer.ElementAt(j).codeEvaluation, LEvaluer.ElementAt(j).codeMat, LEvaluer.ElementAt(j).codeClasse, LEvaluer.ElementAt(j).poids, LEvaluer.ElementAt(j).annee, LSequence.ElementAt(i).codeseq))
                                            t = false;
                                    }

                                }

                                if (t == true)
                                {
                                    //tous les éléments ont bien été enregistrés
                                    MessageBox.Show("Types d'évaluation Enregistré !");
                                }
                                else MessageBox.Show("Echec. Une erreur est survenu l'ors de l'enregistrement !");
                            }
                        }
                        else
                        {
                            if (cmbMatiere.Text.Equals("<Toutes les matières>"))
                            {
                                ClasseBE classe = new ClasseBE();
                                classe.codeClasse = cmbClasse.Text;
                                classe = definirEvaluationMatiereBL.rechercherClasse(classe);

                                //on liste toutes les matières programmées dans la classe
                                List<MatiereBE> LMatiere = definirEvaluationMatiereBL.ListeMatiereDuneClasse(classe, Convert.ToInt16(txtAnnee.Text));

                                if (LMatiere != null && LMatiere.Count != 0)
                                {
                                    Boolean t = true;

                                    for (int j = 0; j < LMatiere.Count; j++)
                                    {
                                        //on supprime tos les types d'évaluation qui ont deja été enregistré pour la matière, la classe et l'année choisi
                                        definirEvaluationMatiereBL.supprinerEvaluerSuivantCritere("codemat = '" + LMatiere.ElementAt(j).codeMat + "' AND codeclasse = '" + cmbClasse.SelectedItem + "' AND annee = '" + LEvaluer.ElementAt(0).annee + "' AND codeseq = '" + cmbSequence.SelectedItem + "'");
                                        for (int i = 0; i < LEvaluer.Count; i++)
                                        {

                                            if (!definirEvaluationMatiereBL.creerEvaluer(LEvaluer.ElementAt(i).codeEvaluation, LMatiere.ElementAt(j).codeMat, LEvaluer.ElementAt(i).codeClasse, LEvaluer.ElementAt(i).poids, LEvaluer.ElementAt(i).annee, LEvaluer.ElementAt(i).codeSequence))
                                                t = false;
                                        }
                                    }

                                    if (t == true)
                                    {
                                        //tous les éléments ont bien été enregistrés
                                        MessageBox.Show("Types d'évaluation Enregistré !");
                                    }
                                    else MessageBox.Show("Echec. Une erreur est survenu l'ors de l'enregistrement !");
                                }

                            }
                            else
                            {

                                //on supprime tos les types d'évaluation qui ont deja été enregistré pour la matière, la classe et l'année choisi
                                definirEvaluationMatiereBL.supprinerEvaluerSuivantCritere("codemat = '" + cmbMatiere.SelectedItem + "' AND codeclasse = '" + cmbClasse.SelectedItem + "' AND annee = '" + LEvaluer.ElementAt(0).annee + "' AND codeseq = '" + cmbSequence.SelectedItem + "'");
                                Boolean t = true;
                                for (int i = 0; i < LEvaluer.Count; i++)
                                {
                                    if (!definirEvaluationMatiereBL.creerEvaluer(LEvaluer.ElementAt(i).codeEvaluation, LEvaluer.ElementAt(i).codeMat, LEvaluer.ElementAt(i).codeClasse, LEvaluer.ElementAt(i).poids, LEvaluer.ElementAt(i).annee, LEvaluer.ElementAt(i).codeSequence))
                                        t = false;
                                }

                                if (t == true)
                                {
                                    //tous les éléments ont bien été enregistrés
                                    MessageBox.Show("Types d'évaluation Enregistré !");
                                }
                                else MessageBox.Show("Echec. Une erreur est survenu l'ors de l'enregistrement !");
                            }
                        }

                        //on rafraichir le DataGrid
                        LEvaluer.Clear();
                        RemplirDataGrid(LEvaluer);

                        // on rafraichi les champs du formulaire
                        //txtAnnee.Text = Convert.ToString(System.DateTime.Today.Year);
                        ParametresBE param = definirEvaluationMatiereBL.getParametres();
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
                        cmbMatiere.Text = null;
                        cmbMatiere.ItemsSource = null;

                        cmbSequence.Text = null;
                        cmbEvaluation.Text = null;
                        txtPourcentage.Text = "";

                    }
                }


            }

        }

        private void grdListeEvaluationMatiere_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                if (MessageBox.Show("Voulez-vous vraiment le supprimer ? ", "School : Confimation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    //on récupère le code de l'évaluation choisit
                    String codeEvaluation = ((EvaluerBE)grdListeEvaluationMatiere.SelectedItem).codeEvaluation;

                    //on ajoute le code dans le comboBox
                    LCodeTypeEvaluation.Add(codeEvaluation);
                    cmbEvaluation.ItemsSource = null;
                    cmbEvaluation.ItemsSource = LCodeTypeEvaluation;
                    //on retire de la liste
                    LEvaluer.RemoveAt(grdListeEvaluationMatiere.SelectedIndex);
                    grdListeEvaluationMatiere.ItemsSource = null;
                    grdListeEvaluationMatiere.ItemsSource = LEvaluer;

                    cmbEvaluation.Text = null;
                    txtPourcentage.Text = "";
                    etat = 0;

                }
            }
        }

        private void grdListeEvaluationMatiere_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (grdListeEvaluationMatiere.SelectedIndex != -1)
            {
                etat = 1;
                EvaluerBE evaluer = new EvaluerBE();

                evaluer = LEvaluer.ElementAt(grdListeEvaluationMatiere.SelectedIndex);

                // on charge les informations dans le formulaire
                cmbClasse.Text = evaluer.codeClasse;

                ParametresBE param = definirEvaluationMatiereBL.getParametres();
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

                //cmbMatiere.Text = evaluer.codeMat;
                //cmbSequence.Text = evaluer.codeSequence;
                /*MessageBox.Show("code : " + evaluer.codeEvaluation);
                LCodeTypeEvaluation.Add(evaluer.codeEvaluation);
                cmbEvaluation.ItemsSource = null;
                cmbEvaluation.ItemsSource = LCodeTypeEvaluation;*/
                cmbEvaluation.Text = evaluer.codeEvaluation;
                txtPourcentage.Text = Convert.ToString(evaluer.poids);

                //on garde l'index de l'élément à modifier
                indexAmodifier = grdListeEvaluationMatiere.SelectedIndex;
            }
        }

        private void cmdAnnuler_Click(object sender, RoutedEventArgs e)
        {
            //on rafraichir le DataGrid
            LEvaluer.Clear();
            RemplirDataGrid(LEvaluer);

            // on rafraichi les champs du formulaire
            //txtAnnee.Text = Convert.ToString(System.DateTime.Today.Year);
            ParametresBE param = definirEvaluationMatiereBL.getParametres();
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
            cmbMatiere.Text = null;
            cmbMatiere.ItemsSource = null;

            cmbSequence.Text = null;

            cmbEvaluation.Text = null;
            txtPourcentage.Text = "";

            etat = 0;

        }

        private void cmbMatiere_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbSequence.Text != null && cmbSequence.Text != "")
            {
                //on charge la liste des codes des types d"évaluations deja enregistrés
                List<TypeevaluationBE> LTypeEvaluation = definirEvaluationMatiereBL.listerTousLesTypesEvaluations();
                LCodeTypeEvaluation.Clear();
                LCodeTypeEvaluation = definirEvaluationMatiereBL.getListCodeTypeEvaluation(LTypeEvaluation);
                cmbEvaluation.ItemsSource = null;
                cmbEvaluation.ItemsSource = LCodeTypeEvaluation;

                //********** on charge dans le dataGrid toutes les types évaluations de la matière choisi, pour la classe et l'année choisi
                LEvaluer.Clear();
                LEvaluer = definirEvaluationMatiereBL.listerEvaluersSuivantCritere("codemat = '" + cmbMatiere.SelectedItem + "' AND codeclasse = '" + cmbClasse.Text + "' AND annee = '" + txtAnnee.Text + "' AND codeseq = '" + cmbSequence.Text + "'");
                if (LEvaluer == null || LEvaluer.Count == 0)
                    etatGrid = 0; //pour dire que la liste initialement est vide
                else
                    etatGrid = 1; // la liste contient des infos

                //on remplit dans le datagrid de codeEvaluation les codes des évaluations qui n'avaient pas été renseigné
                List<String> LCodeEval = new List<String>();
                for (int i = 0; i < LCodeTypeEvaluation.Count; i++)
                {
                    bool trouve = false;
                    for (int j = 0; j < LEvaluer.Count; j++)
                    {
                        if (LEvaluer.ElementAt(j).codeEvaluation == LCodeTypeEvaluation.ElementAt(i))
                        {
                            trouve = true;
                            break;
                        }
                    }

                    if (trouve == false)
                        LCodeEval.Add(LCodeTypeEvaluation.ElementAt(i));
                }

                RemplirDataGrid(LEvaluer);

                //on ajoute le code dans le comboBox
                if (LEvaluer != null && LEvaluer.Count != 0)
                {
                    // LCodeTypeEvaluation.Clear();
                    cmbEvaluation.ItemsSource = null;
                    cmbEvaluation.ItemsSource = LCodeEval;
                }
            }
        }

        private void txtAnnee_PreviewTextInput(object sender, TextCompositionEventArgs e)
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

                    // on charge les matières de la classe sélectionnée dans le comboBox
                    List<MatiereBE> LMatiere = definirEvaluationMatiereBL.ListeMatiereDuneClasse(classe, Convert.ToInt16(txtAnnee.Text));

                    cmbMatiere.ItemsSource = definirEvaluationMatiereBL.getListCodeMatiere2(LMatiere);
                }
            }
        }

        private void txtPourcentage_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

            e.Handled = !Utilitaires.IsTextAllowed(e.Text);
        }

        private void cmbSequence_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbMatiere.Text != null && cmbMatiere.Text != "")
            {
                List<String> LCodeEval = new List<String>();


                //on charge la liste des codes des types d"évaluations deja enregistrés
                List<TypeevaluationBE> LTypeEvaluation = definirEvaluationMatiereBL.listerTousLesTypesEvaluations();
                LCodeTypeEvaluation.Clear();
                LCodeTypeEvaluation = definirEvaluationMatiereBL.getListCodeTypeEvaluation(LTypeEvaluation);
                cmbEvaluation.ItemsSource = null;
                cmbEvaluation.ItemsSource = LCodeTypeEvaluation;

                if (cmbMatiere.Text.Equals("<Toutes les matières>"))
                {
                    ClasseBE classe = new ClasseBE();
                    classe.codeClasse = cmbClasse.Text;
                    classe = definirEvaluationMatiereBL.rechercherClasse(classe);
                    // on charge les matières de la classe sélectionnée dans le comboBox
                    List<MatiereBE> LMatiere = definirEvaluationMatiereBL.ListeMatiereDuneClasse(classe, Convert.ToInt16(txtAnnee.Text));

                    if (LMatiere != null && LMatiere.Count != 0)
                    {
                        //********** on charge dans le dataGrid toutes les types évaluations d'une des matières de la classe choisi, pour la classe et l'année choisi
                        LEvaluer.Clear();

                        if (cmbSequence.SelectedItem.Equals("<Toutes les Séquences>"))
                        {
                            List<SequenceBE> LSequence = definirEvaluationMatiereBL.listerToutesLesSequences();
                            if (LSequence != null && LSequence.Count != 0) {
                                LEvaluer = definirEvaluationMatiereBL.listerEvaluersSuivantCritere("codemat = '" + LMatiere.ElementAt(0).codeMat + "' AND codeclasse = '" + cmbClasse.Text + "' AND annee = '" + txtAnnee.Text + "' AND codeseq = '" + LSequence.ElementAt(0).codeseq + "'");

                            }

                        }
                        else LEvaluer = definirEvaluationMatiereBL.listerEvaluersSuivantCritere("codemat = '" + LMatiere.ElementAt(0).codeMat + "' AND codeclasse = '" + cmbClasse.Text + "' AND annee = '" + txtAnnee.Text + "' AND codeseq = '" + cmbSequence.SelectedItem + "'");
                        
                        if (LEvaluer == null || LEvaluer.Count == 0)
                            etatGrid = 0; //pour dire que la liste initialement est vide
                        else
                            etatGrid = 1; // la liste contient des infos

                        //on remplit dans le datagrid de codeEvaluation les codes des évaluations qui n'avaient pas été renseigné

                        for (int i = 0; i < LCodeTypeEvaluation.Count; i++)
                        {
                            bool trouve = false;
                            for (int j = 0; j < LEvaluer.Count; j++)
                            {
                                if (LEvaluer.ElementAt(j).codeEvaluation == LCodeTypeEvaluation.ElementAt(i))
                                {
                                    trouve = true;
                                    break;
                                }
                            }

                            if (trouve == false)
                                LCodeEval.Add(LCodeTypeEvaluation.ElementAt(i));
                        }

                        RemplirDataGrid(LEvaluer);

                        //on ajoute le code dans le comboBox
                        if (LEvaluer != null && LEvaluer.Count != 0)
                        {
                            //LCodeTypeEvaluation.Clear();
                            cmbEvaluation.ItemsSource = null;
                            cmbEvaluation.ItemsSource = LCodeEval;
                            LCodeTypeEvaluation.Clear();
                            LCodeTypeEvaluation = LCodeEval;
                        }
                    }

                }
                else
                {
                    //********** on charge dans le dataGrid toutes les types évaluations de la matière choisi, pour la classe et l'année choisi
                    LEvaluer.Clear();

                    if (cmbSequence.Text.Equals("<Toutes les Séquences>"))
                    {
                        List<SequenceBE> LSequence = definirEvaluationMatiereBL.listerToutesLesSequences();
                        if (LSequence != null && LSequence.Count != 0)
                        {
                            LEvaluer = definirEvaluationMatiereBL.listerEvaluersSuivantCritere("codemat = '" + cmbMatiere.Text + "' AND codeclasse = '" + cmbClasse.Text + "' AND annee = '" + txtAnnee.Text + "' AND codeseq = '" + LSequence.ElementAt(0).codeseq + "'");

                        }

                    }
                    else
                        LEvaluer = definirEvaluationMatiereBL.listerEvaluersSuivantCritere("codemat = '" + cmbMatiere.Text + "' AND codeclasse = '" + cmbClasse.Text + "' AND annee = '" + txtAnnee.Text + "' AND codeseq = '" + cmbSequence.SelectedItem + "'");
                    
                    if (LEvaluer == null || LEvaluer.Count == 0)
                        etatGrid = 0; //pour dire que la liste initialement est vide
                    else
                        etatGrid = 1; // la liste contient des infos

                    //on remplit dans le datagrid de codeEvaluation les codes des évaluations qui n'avaient pas été renseigné

                    for (int i = 0; i < LCodeTypeEvaluation.Count; i++)
                    {
                        bool trouve = false;
                        for (int j = 0; j < LEvaluer.Count; j++)
                        {
                            if (LEvaluer.ElementAt(j).codeEvaluation == LCodeTypeEvaluation.ElementAt(i))
                            {
                                trouve = true;
                                break;
                            }
                        }

                        if (trouve == false)
                            LCodeEval.Add(LCodeTypeEvaluation.ElementAt(i));
                    }

                    RemplirDataGrid(LEvaluer);

                    //on ajoute le code dans le comboBox
                    if (LEvaluer != null && LEvaluer.Count != 0)
                    {
                        //LCodeTypeEvaluation.Clear();
                        cmbEvaluation.ItemsSource = null;
                        cmbEvaluation.ItemsSource = LCodeEval;
                        LCodeTypeEvaluation.Clear();
                        LCodeTypeEvaluation = LCodeEval;
                    }

                }

            }
        }

        private void cmdFermer_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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
