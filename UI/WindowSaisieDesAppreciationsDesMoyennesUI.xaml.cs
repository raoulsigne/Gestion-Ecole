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
using Ecole.ClasseConception;
using System.Windows.Controls.Primitives;
using Ecole.Utilitaire;
using System.Globalization;
using System.Threading;

namespace Ecole.UI
{
    /// <summary>
    /// Interaction logic for WindowSaisieDesAppreciationsDesMoyennesUI.xaml
    /// </summary>
    public partial class WindowSaisieDesAppreciationsDesMoyennesUI : Window
    {
        public SaisieAppreciationMoyenneBL saisieAppreciationMoyenneBL;

        int annee;

        List<LigneSaisieAppreciationMoyenne> ListLigneSaisieAppreciation;
        public WindowSaisieDesAppreciationsDesMoyennesUI()
        {
            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";
            Thread.CurrentThread.CurrentCulture = ci;

            InitializeComponent();

            saisieAppreciationMoyenneBL = new SaisieAppreciationMoyenneBL();
            ListLigneSaisieAppreciation = new List<LigneSaisieAppreciationMoyenne>();

            //on charge les périodes dans le comboBox
            String[] periode = { "Séquence", "Trimestre", "Année" };
            cmbPeriode.ItemsSource = periode;

            List<ClasseBE> LClasse = saisieAppreciationMoyenneBL.listerToutesLesClasses();
            cmbClasse.ItemsSource = saisieAppreciationMoyenneBL.getListCodeClasse(LClasse);

            annee = saisieAppreciationMoyenneBL.getAnneeEnCours();

            txtAnnee.Text = Convert.ToString(annee);
            txtAnneeScolaire.Text = (annee - 1).ToString();

            lblChoixPeriode.Visibility = System.Windows.Visibility.Hidden;
            cmbChoixPeriode.Visibility = System.Windows.Visibility.Hidden;

        }

        private void cmdValider_Click(object sender, RoutedEventArgs e)
        {
            //on vérifit si tous les champs ont été corectement rempli
            if ((cmbClasse.Text != null && cmbPeriode.Text != null && txtAnneeScolaire.Text != null) &&
                (cmbClasse.Text != "" && cmbPeriode.Text != "" && txtAnneeScolaire.Text != ""))
            {

                //la classe choisi
                String codeClasse = cmbClasse.Text;

                if (cmbClasse.Text.Equals("<Toutes Les Classes>"))
                {
                    //on liste toutes les Classes 
                    List<ClasseBE> LClasse = saisieAppreciationMoyenneBL.listerToutesLesClasses();
                    if (LClasse != null && LClasse.Count != 0)
                    {

                        if (cmbPeriode.Text.Equals("Séquence"))
                        {
                            if (cmbChoixPeriode.Text != null && cmbChoixPeriode.Text != "")
                            {
                                // traitement pour une Séquence

                                //si on a choisi <Toutes les Séquences>
                                if (cmbChoixPeriode.Text.Equals("<Toutes Les Séquences>"))
                                {

                                    //--------------------------- Action pour toutes les séquences d'une matière particulière

                                    //on liste toutes les Séquences
                                    List<SequenceBE> LSequence = saisieAppreciationMoyenneBL.listerToutesLesSequences();
                                    ListLigneSaisieAppreciation = new List<LigneSaisieAppreciationMoyenne>();
                                    for (int j = 0; j < LClasse.Count; j++)
                                    {
                                        for (int i = 0; i < LSequence.Count; i++)
                                        {
                                            List<MoyennesBE> ListMoyennesSequentielles = new List<MoyennesBE>();
                                            ListMoyennesSequentielles = saisieAppreciationMoyenneBL.listerMoyennesSequentielleDesElevesDuneClasse(LClasse.ElementAt(j).codeClasse, cmbMatiere.Text, LSequence.ElementAt(i).codeseq, Convert.ToInt16(txtAnnee.Text));
                                            //on recherche et on liste les résultats

                                            if (ListMoyennesSequentielles != null && ListMoyennesSequentielles.Count != 0) {
                                                for (int k = 0; k < ListMoyennesSequentielles.Count; k++) {
                                                    LigneSaisieAppreciationMoyenne ligneSaisieAppreciationMoyenne = new LigneSaisieAppreciationMoyenne();

                                                    EleveBE eleve = new EleveBE();
                                                    eleve.matricule = ListMoyennesSequentielles.ElementAt(k).matricule;
                                                    eleve = saisieAppreciationMoyenneBL.rechercherEleve(eleve);

                                                    if(eleve != null){
                                                        ligneSaisieAppreciationMoyenne.nomEleve = eleve.nom;
                                                        ligneSaisieAppreciationMoyenne.matricule = ListMoyennesSequentielles.ElementAt(k).matricule;
                                                        ligneSaisieAppreciationMoyenne.Periode = ListMoyennesSequentielles.ElementAt(k).codeSeq;
                                                        ligneSaisieAppreciationMoyenne.annee = ListMoyennesSequentielles.ElementAt(k).annee;
                                                        ligneSaisieAppreciationMoyenne.codeMatiere = ListMoyennesSequentielles.ElementAt(k).codeMat;
                                                        ligneSaisieAppreciationMoyenne.moyenne = ListMoyennesSequentielles.ElementAt(k).moyenne;
                                                        ligneSaisieAppreciationMoyenne.mention = ListMoyennesSequentielles.ElementAt(k).mention;
                                                        ligneSaisieAppreciationMoyenne.rang = ListMoyennesSequentielles.ElementAt(k).rang;
                                                        ligneSaisieAppreciationMoyenne.moyenneClasse = ListMoyennesSequentielles.ElementAt(k).moyenneClasse;
                                                        ligneSaisieAppreciationMoyenne.moyenneMin = ListMoyennesSequentielles.ElementAt(k).moyenneMin;
                                                        ligneSaisieAppreciationMoyenne.moyenneMax = ListMoyennesSequentielles.ElementAt(k).moyenneMax;
                                                        ligneSaisieAppreciationMoyenne.appreciation = ListMoyennesSequentielles.ElementAt(k).appreciation;

                                                        ligneSaisieAppreciationMoyenne.eleve = eleve;

                                                        MatiereBE matiere = new MatiereBE();
                                                        matiere.codeMat = ListMoyennesSequentielles.ElementAt(k).codeMat;
                                                        matiere = saisieAppreciationMoyenneBL.rechercherMatiere(matiere);

                                                        ligneSaisieAppreciationMoyenne.matiere = matiere;
                                                    }

                                                    ListLigneSaisieAppreciation.Add(ligneSaisieAppreciationMoyenne);

                                                }
                                            }

                                        }
                                    }

                                    //MessageBox.Show("Opération Terminée !! ");
                                    grdListeDesMoyennes.ItemsSource = ListLigneSaisieAppreciation;
                                }
                                else
                                {

                                    //--------------------- Action pour une Séquence particulière
                                    ListLigneSaisieAppreciation = new List<LigneSaisieAppreciationMoyenne>();

                                    for (int j = 0; j < LClasse.Count; j++)
                                    {
                                        List<MoyennesBE> ListMoyennesSequentielles = new List<MoyennesBE>();
                                        ListMoyennesSequentielles = saisieAppreciationMoyenneBL.listerMoyennesSequentielleDesElevesDuneClasse(LClasse.ElementAt(j).codeClasse, cmbMatiere.Text, cmbChoixPeriode.Text, Convert.ToInt16(txtAnnee.Text));
                                        //on recherche et on liste les résultats

                                        if (ListMoyennesSequentielles != null && ListMoyennesSequentielles.Count != 0)
                                        {
                                            for (int k = 0; k < ListMoyennesSequentielles.Count; k++)
                                            {
                                                LigneSaisieAppreciationMoyenne ligneSaisieAppreciationMoyenne = new LigneSaisieAppreciationMoyenne();

                                                EleveBE eleve = new EleveBE();
                                                eleve.matricule = ListMoyennesSequentielles.ElementAt(k).matricule;
                                                eleve = saisieAppreciationMoyenneBL.rechercherEleve(eleve);

                                                if (eleve != null)
                                                {
                                                    ligneSaisieAppreciationMoyenne.nomEleve = eleve.nom;
                                                    ligneSaisieAppreciationMoyenne.matricule = ListMoyennesSequentielles.ElementAt(k).matricule;
                                                    ligneSaisieAppreciationMoyenne.Periode = ListMoyennesSequentielles.ElementAt(k).codeSeq;
                                                    ligneSaisieAppreciationMoyenne.annee = ListMoyennesSequentielles.ElementAt(k).annee;
                                                    ligneSaisieAppreciationMoyenne.codeMatiere = ListMoyennesSequentielles.ElementAt(k).codeMat;
                                                    ligneSaisieAppreciationMoyenne.moyenne = ListMoyennesSequentielles.ElementAt(k).moyenne;
                                                    ligneSaisieAppreciationMoyenne.mention = ListMoyennesSequentielles.ElementAt(k).mention;
                                                    ligneSaisieAppreciationMoyenne.rang = ListMoyennesSequentielles.ElementAt(k).rang;
                                                    ligneSaisieAppreciationMoyenne.moyenneClasse = ListMoyennesSequentielles.ElementAt(k).moyenneClasse;
                                                    ligneSaisieAppreciationMoyenne.moyenneMin = ListMoyennesSequentielles.ElementAt(k).moyenneMin;
                                                    ligneSaisieAppreciationMoyenne.moyenneMax = ListMoyennesSequentielles.ElementAt(k).moyenneMax;
                                                    ligneSaisieAppreciationMoyenne.appreciation = ListMoyennesSequentielles.ElementAt(k).appreciation;

                                                    ligneSaisieAppreciationMoyenne.eleve = eleve;

                                                    MatiereBE matiere = new MatiereBE();
                                                    matiere.codeMat = ListMoyennesSequentielles.ElementAt(k).codeMat;
                                                    matiere = saisieAppreciationMoyenneBL.rechercherMatiere(matiere);

                                                    ligneSaisieAppreciationMoyenne.matiere = matiere;
                                                }

                                                ListLigneSaisieAppreciation.Add(ligneSaisieAppreciationMoyenne);

                                            }
                                        }
                                        
                                    }

                                    //MessageBox.Show("Opération Terminée !! ");
                                    grdListeDesMoyennes.ItemsSource = ListLigneSaisieAppreciation;
                                }

                            }
                            else MessageBox.Show("Vous devez choisir une Séquence !");
                        }
                        else if (cmbPeriode.Text.Equals("Trimestre"))
                        {
                            if (cmbChoixPeriode.Text != null && cmbChoixPeriode.Text != "")
                            {
                                // traitement pour un Trimestre
                                //si on a choisi <Toutes les Séquences>
                                if (cmbChoixPeriode.Text.Equals("<Tous Les Trimestres>"))
                                {

                                    //--------------------------- Action pour toutes les Trimestres d'une matière particulière

                                    //on liste toutes les Trimestres
                                    List<TrimestreBE> LTrimestre = saisieAppreciationMoyenneBL.listerTousLesTrimestres();
                                   
                                    ListLigneSaisieAppreciation = new List<LigneSaisieAppreciationMoyenne>();

                                    for (int j = 0; j < LClasse.Count; j++)
                                    {
                                        for (int i = 0; i < LTrimestre.Count; i++)
                                        {

                                            List<MoyennesTrimestrielsBE> ListMoyennesTrimestrielles = new List<MoyennesTrimestrielsBE>();
                                            ListMoyennesTrimestrielles = saisieAppreciationMoyenneBL.listerMoyennesTrimestriellesDesElevesDuneClasse(LClasse.ElementAt(j).codeClasse, cmbMatiere.Text, LTrimestre.ElementAt(i).codetrimestre, Convert.ToInt16(txtAnnee.Text));
                                            //on recherche et on liste les résultats

                                            if (ListMoyennesTrimestrielles != null && ListMoyennesTrimestrielles.Count != 0)
                                            {
                                                for (int k = 0; k < ListMoyennesTrimestrielles.Count; k++)
                                                {
                                                    LigneSaisieAppreciationMoyenne ligneSaisieAppreciationMoyenne = new LigneSaisieAppreciationMoyenne();

                                                    EleveBE eleve = new EleveBE();
                                                    eleve.matricule = ListMoyennesTrimestrielles.ElementAt(k).matricule;
                                                    eleve = saisieAppreciationMoyenneBL.rechercherEleve(eleve);

                                                    if (eleve != null)
                                                    {
                                                        ligneSaisieAppreciationMoyenne.nomEleve = eleve.nom;
                                                        ligneSaisieAppreciationMoyenne.matricule = ListMoyennesTrimestrielles.ElementAt(k).matricule;
                                                        ligneSaisieAppreciationMoyenne.Periode = ListMoyennesTrimestrielles.ElementAt(k).codeTrimestre;
                                                        ligneSaisieAppreciationMoyenne.annee = ListMoyennesTrimestrielles.ElementAt(k).annee;
                                                        ligneSaisieAppreciationMoyenne.codeMatiere = ListMoyennesTrimestrielles.ElementAt(k).codeMat;
                                                        ligneSaisieAppreciationMoyenne.moyenne = ListMoyennesTrimestrielles.ElementAt(k).moyenne;
                                                        ligneSaisieAppreciationMoyenne.mention = ListMoyennesTrimestrielles.ElementAt(k).mention;
                                                        ligneSaisieAppreciationMoyenne.rang = ListMoyennesTrimestrielles.ElementAt(k).rang;
                                                        ligneSaisieAppreciationMoyenne.moyenneClasse = ListMoyennesTrimestrielles.ElementAt(k).moyenneClasse;
                                                        ligneSaisieAppreciationMoyenne.moyenneMin = ListMoyennesTrimestrielles.ElementAt(k).moyenneMin;
                                                        ligneSaisieAppreciationMoyenne.moyenneMax = ListMoyennesTrimestrielles.ElementAt(k).moyenneMax;
                                                        ligneSaisieAppreciationMoyenne.appreciation = ListMoyennesTrimestrielles.ElementAt(k).appreciation;

                                                        ligneSaisieAppreciationMoyenne.eleve = eleve;

                                                        MatiereBE matiere = new MatiereBE();
                                                        matiere.codeMat = ListMoyennesTrimestrielles.ElementAt(k).codeMat;
                                                        matiere = saisieAppreciationMoyenneBL.rechercherMatiere(matiere);

                                                        ligneSaisieAppreciationMoyenne.matiere = matiere;
                                                    }

                                                    ListLigneSaisieAppreciation.Add(ligneSaisieAppreciationMoyenne);

                                                }
                                            }
                                            
                                        }
                                    }

                                    //MessageBox.Show("Opération Terminée !! ");
                                    grdListeDesMoyennes.ItemsSource = ListLigneSaisieAppreciation;
                                }
                                else
                                {

                                    //--------------------- Action pour un trimestre particulière
                                    ListLigneSaisieAppreciation = new List<LigneSaisieAppreciationMoyenne>();

                                    for (int j = 0; j < LClasse.Count; j++)
                                    {
                                        List<MoyennesTrimestrielsBE> ListMoyennesTrimestrielles = new List<MoyennesTrimestrielsBE>();
                                        ListMoyennesTrimestrielles = saisieAppreciationMoyenneBL.listerMoyennesTrimestriellesDesElevesDuneClasse(LClasse.ElementAt(j).codeClasse, cmbMatiere.Text, cmbChoixPeriode.Text, Convert.ToInt16(txtAnnee.Text));
                                        //on recherche et on liste les résultats

                                        if (ListMoyennesTrimestrielles != null && ListMoyennesTrimestrielles.Count != 0)
                                        {
                                            for (int k = 0; k < ListMoyennesTrimestrielles.Count; k++)
                                            {
                                                LigneSaisieAppreciationMoyenne ligneSaisieAppreciationMoyenne = new LigneSaisieAppreciationMoyenne();

                                                EleveBE eleve = new EleveBE();
                                                eleve.matricule = ListMoyennesTrimestrielles.ElementAt(k).matricule;
                                                eleve = saisieAppreciationMoyenneBL.rechercherEleve(eleve);

                                                if (eleve != null)
                                                {
                                                    ligneSaisieAppreciationMoyenne.nomEleve = eleve.nom;
                                                    ligneSaisieAppreciationMoyenne.matricule = ListMoyennesTrimestrielles.ElementAt(k).matricule;
                                                    ligneSaisieAppreciationMoyenne.Periode = ListMoyennesTrimestrielles.ElementAt(k).codeTrimestre;
                                                    ligneSaisieAppreciationMoyenne.annee = ListMoyennesTrimestrielles.ElementAt(k).annee;
                                                    ligneSaisieAppreciationMoyenne.codeMatiere = ListMoyennesTrimestrielles.ElementAt(k).codeMat;
                                                    ligneSaisieAppreciationMoyenne.moyenne = ListMoyennesTrimestrielles.ElementAt(k).moyenne;
                                                    ligneSaisieAppreciationMoyenne.mention = ListMoyennesTrimestrielles.ElementAt(k).mention;
                                                    ligneSaisieAppreciationMoyenne.rang = ListMoyennesTrimestrielles.ElementAt(k).rang;
                                                    ligneSaisieAppreciationMoyenne.moyenneClasse = ListMoyennesTrimestrielles.ElementAt(k).moyenneClasse;
                                                    ligneSaisieAppreciationMoyenne.moyenneMin = ListMoyennesTrimestrielles.ElementAt(k).moyenneMin;
                                                    ligneSaisieAppreciationMoyenne.moyenneMax = ListMoyennesTrimestrielles.ElementAt(k).moyenneMax;
                                                    ligneSaisieAppreciationMoyenne.appreciation = ListMoyennesTrimestrielles.ElementAt(k).appreciation;

                                                    ligneSaisieAppreciationMoyenne.eleve = eleve;

                                                    MatiereBE matiere = new MatiereBE();
                                                    matiere.codeMat = ListMoyennesTrimestrielles.ElementAt(k).codeMat;
                                                    matiere = saisieAppreciationMoyenneBL.rechercherMatiere(matiere);

                                                    ligneSaisieAppreciationMoyenne.matiere = matiere;
                                                }

                                                ListLigneSaisieAppreciation.Add(ligneSaisieAppreciationMoyenne);

                                            }
                                        }

                                        
                                    }

                                    //MessageBox.Show("Opération Terminée !!");
                                    grdListeDesMoyennes.ItemsSource = ListLigneSaisieAppreciation;
                                }
                            }
                            else MessageBox.Show("Vous devez choisir un Trimestre !");
                        }
                        else
                        {
                            // traitement pour une année

                            //--------------------- Action pour une Séquence particulière
                            ListLigneSaisieAppreciation = new List<LigneSaisieAppreciationMoyenne>();

                            for (int j = 0; j < LClasse.Count; j++)
                            {
                                List<MoyennesAnnuellesBE> ListMoyennesAnnuelles = new List<MoyennesAnnuellesBE>();
                                ListMoyennesAnnuelles = saisieAppreciationMoyenneBL.listerMoyennesAnnuellesDesElevesDuneClasse(LClasse.ElementAt(j).codeClasse, cmbMatiere.Text, Convert.ToInt16(txtAnnee.Text));
                                //on recherche et on liste les résultats

                                if (ListMoyennesAnnuelles != null && ListMoyennesAnnuelles.Count != 0)
                                {
                                    for (int k = 0; k < ListMoyennesAnnuelles.Count; k++)
                                    {
                                        LigneSaisieAppreciationMoyenne ligneSaisieAppreciationMoyenne = new LigneSaisieAppreciationMoyenne();

                                        EleveBE eleve = new EleveBE();
                                        eleve.matricule = ListMoyennesAnnuelles.ElementAt(k).matricule;
                                        eleve = saisieAppreciationMoyenneBL.rechercherEleve(eleve);

                                        if (eleve != null)
                                        {
                                            ligneSaisieAppreciationMoyenne.nomEleve = eleve.nom;
                                            ligneSaisieAppreciationMoyenne.matricule = ListMoyennesAnnuelles.ElementAt(k).matricule;
                                            ligneSaisieAppreciationMoyenne.Periode = "-";
                                            ligneSaisieAppreciationMoyenne.annee = ListMoyennesAnnuelles.ElementAt(k).annee;
                                            ligneSaisieAppreciationMoyenne.codeMatiere = ListMoyennesAnnuelles.ElementAt(k).codeMat;
                                            ligneSaisieAppreciationMoyenne.moyenne = ListMoyennesAnnuelles.ElementAt(k).moyenne;
                                            ligneSaisieAppreciationMoyenne.mention = ListMoyennesAnnuelles.ElementAt(k).mention;
                                            ligneSaisieAppreciationMoyenne.rang = ListMoyennesAnnuelles.ElementAt(k).rang;
                                            ligneSaisieAppreciationMoyenne.moyenneClasse = ListMoyennesAnnuelles.ElementAt(k).moyenneClasse;
                                            ligneSaisieAppreciationMoyenne.moyenneMin = ListMoyennesAnnuelles.ElementAt(k).moyenneMin;
                                            ligneSaisieAppreciationMoyenne.moyenneMax = ListMoyennesAnnuelles.ElementAt(k).moyenneMax;
                                            ligneSaisieAppreciationMoyenne.appreciation = ListMoyennesAnnuelles.ElementAt(k).appreciation;

                                            ligneSaisieAppreciationMoyenne.eleve = eleve;

                                            MatiereBE matiere = new MatiereBE();
                                            matiere.codeMat = ListMoyennesAnnuelles.ElementAt(k).codeMat;
                                            matiere = saisieAppreciationMoyenneBL.rechercherMatiere(matiere);

                                            ligneSaisieAppreciationMoyenne.matiere = matiere;
                                        }

                                        ListLigneSaisieAppreciation.Add(ligneSaisieAppreciationMoyenne);

                                    }
                                }
                                
                            }

                            //MessageBox.Show("Opération Terminée !!");
                            grdListeDesMoyennes.ItemsSource = ListLigneSaisieAppreciation;

                        }


                    }
                }
                else
                { //cas où on a choisi une classe particulière
                    if (cmbPeriode.Text.Equals("Séquence"))
                    {
                        if (cmbChoixPeriode.Text != null && cmbChoixPeriode.Text != "")
                        {
                            // traitement pour une Séquence

                            //si on a choisi <Toutes les Séquences>
                            if (cmbChoixPeriode.Text.Equals("<Toutes Les Séquences>"))
                            {

                                //--------------------------- Action pour toutes les séquences d'une matière particulière

                                //on liste toutes les Séquences
                                List<SequenceBE> LSequence = saisieAppreciationMoyenneBL.listerToutesLesSequences();

                                ListLigneSaisieAppreciation = new List<LigneSaisieAppreciationMoyenne>();

                                for (int i = 0; i < LSequence.Count; i++)
                                {
                                    List<MoyennesBE> ListMoyennesSequentielles = new List<MoyennesBE>();
                                    ListMoyennesSequentielles = saisieAppreciationMoyenneBL.listerMoyennesSequentielleDesElevesDuneClasse(codeClasse, cmbMatiere.Text, LSequence.ElementAt(i).codeseq, Convert.ToInt16(txtAnnee.Text));
                                    //on recherche et on liste les résultats

                                    if (ListMoyennesSequentielles != null && ListMoyennesSequentielles.Count != 0)
                                    {
                                        for (int k = 0; k < ListMoyennesSequentielles.Count; k++)
                                        {
                                            LigneSaisieAppreciationMoyenne ligneSaisieAppreciationMoyenne = new LigneSaisieAppreciationMoyenne();

                                            EleveBE eleve = new EleveBE();
                                            eleve.matricule = ListMoyennesSequentielles.ElementAt(k).matricule;
                                            eleve = saisieAppreciationMoyenneBL.rechercherEleve(eleve);

                                            if (eleve != null)
                                            {
                                                ligneSaisieAppreciationMoyenne.nomEleve = eleve.nom;
                                                ligneSaisieAppreciationMoyenne.matricule = ListMoyennesSequentielles.ElementAt(k).matricule;
                                                ligneSaisieAppreciationMoyenne.Periode = ListMoyennesSequentielles.ElementAt(k).codeSeq;
                                                ligneSaisieAppreciationMoyenne.annee = ListMoyennesSequentielles.ElementAt(k).annee;
                                                ligneSaisieAppreciationMoyenne.codeMatiere = ListMoyennesSequentielles.ElementAt(k).codeMat;
                                                ligneSaisieAppreciationMoyenne.moyenne = ListMoyennesSequentielles.ElementAt(k).moyenne;
                                                ligneSaisieAppreciationMoyenne.mention = ListMoyennesSequentielles.ElementAt(k).mention;
                                                ligneSaisieAppreciationMoyenne.rang = ListMoyennesSequentielles.ElementAt(k).rang;
                                                ligneSaisieAppreciationMoyenne.moyenneClasse = ListMoyennesSequentielles.ElementAt(k).moyenneClasse;
                                                ligneSaisieAppreciationMoyenne.moyenneMin = ListMoyennesSequentielles.ElementAt(k).moyenneMin;
                                                ligneSaisieAppreciationMoyenne.moyenneMax = ListMoyennesSequentielles.ElementAt(k).moyenneMax;
                                                ligneSaisieAppreciationMoyenne.appreciation = ListMoyennesSequentielles.ElementAt(k).appreciation;

                                                ligneSaisieAppreciationMoyenne.eleve = eleve;

                                                MatiereBE matiere = new MatiereBE();
                                                matiere.codeMat = ListMoyennesSequentielles.ElementAt(k).codeMat;
                                                matiere = saisieAppreciationMoyenneBL.rechercherMatiere(matiere);

                                                ligneSaisieAppreciationMoyenne.matiere = matiere;
                                            }

                                            ListLigneSaisieAppreciation.Add(ligneSaisieAppreciationMoyenne);

                                        }
                                    }

                                }

                                //MessageBox.Show("Opération Terminée !! ");
                                grdListeDesMoyennes.ItemsSource = ListLigneSaisieAppreciation;

                            }
                            else
                            {

                                //--------------------- Action pour une Séquence particulière
                                ListLigneSaisieAppreciation = new List<LigneSaisieAppreciationMoyenne>();

                                List<MoyennesBE> ListMoyennesSequentielles = new List<MoyennesBE>();
                                ListMoyennesSequentielles = saisieAppreciationMoyenneBL.listerMoyennesSequentielleDesElevesDuneClasse(codeClasse, cmbMatiere.Text, cmbChoixPeriode.Text, Convert.ToInt16(txtAnnee.Text));
                                //on recherche et on liste les résultats

                                if (ListMoyennesSequentielles != null && ListMoyennesSequentielles.Count != 0)
                                {
                                    for (int k = 0; k < ListMoyennesSequentielles.Count; k++)
                                    {
                                        LigneSaisieAppreciationMoyenne ligneSaisieAppreciationMoyenne = new LigneSaisieAppreciationMoyenne();

                                        EleveBE eleve = new EleveBE();
                                        eleve.matricule = ListMoyennesSequentielles.ElementAt(k).matricule;
                                        eleve = saisieAppreciationMoyenneBL.rechercherEleve(eleve);

                                        if (eleve != null)
                                        {
                                            ligneSaisieAppreciationMoyenne.nomEleve = eleve.nom;
                                            ligneSaisieAppreciationMoyenne.matricule = ListMoyennesSequentielles.ElementAt(k).matricule;
                                            ligneSaisieAppreciationMoyenne.Periode = ListMoyennesSequentielles.ElementAt(k).codeSeq;
                                            ligneSaisieAppreciationMoyenne.annee = ListMoyennesSequentielles.ElementAt(k).annee;
                                            ligneSaisieAppreciationMoyenne.codeMatiere = ListMoyennesSequentielles.ElementAt(k).codeMat;
                                            ligneSaisieAppreciationMoyenne.moyenne = ListMoyennesSequentielles.ElementAt(k).moyenne;
                                            ligneSaisieAppreciationMoyenne.mention = ListMoyennesSequentielles.ElementAt(k).mention;
                                            ligneSaisieAppreciationMoyenne.rang = ListMoyennesSequentielles.ElementAt(k).rang;
                                            ligneSaisieAppreciationMoyenne.moyenneClasse = ListMoyennesSequentielles.ElementAt(k).moyenneClasse;
                                            ligneSaisieAppreciationMoyenne.moyenneMin = ListMoyennesSequentielles.ElementAt(k).moyenneMin;
                                            ligneSaisieAppreciationMoyenne.moyenneMax = ListMoyennesSequentielles.ElementAt(k).moyenneMax;
                                            ligneSaisieAppreciationMoyenne.appreciation = ListMoyennesSequentielles.ElementAt(k).appreciation;

                                            ligneSaisieAppreciationMoyenne.eleve = eleve;

                                            MatiereBE matiere = new MatiereBE();
                                            matiere.codeMat = ListMoyennesSequentielles.ElementAt(k).codeMat;
                                            matiere = saisieAppreciationMoyenneBL.rechercherMatiere(matiere);

                                            ligneSaisieAppreciationMoyenne.matiere = matiere;
                                        }

                                        ListLigneSaisieAppreciation.Add(ligneSaisieAppreciationMoyenne);

                                    }

                                    
                                }

                                //MessageBox.Show("Opération Terminée !! ");
                                grdListeDesMoyennes.ItemsSource = ListLigneSaisieAppreciation;
                            }

                        }
                        else MessageBox.Show("Vous devez choisir une Séquence !");
                    }
                    else if (cmbPeriode.Text.Equals("Trimestre"))
                    {
                        if (cmbChoixPeriode.Text != null && cmbChoixPeriode.Text != "")
                        {
                            // traitement pour un Trimestre
                            //si on a choisi <Toutes les Séquences>
                            if (cmbChoixPeriode.Text.Equals("<Tous Les Trimestres>"))
                            {

                                //--------------------------- Action pour toutes les Trimestres d'une matière particulière

                                //on liste toutes les Trimestres
                                List<TrimestreBE> LTrimestre = saisieAppreciationMoyenneBL.listerTousLesTrimestres();
                                ListLigneSaisieAppreciation = new List<LigneSaisieAppreciationMoyenne>();

                                for (int i = 0; i < LTrimestre.Count; i++)
                                {
                                    List<MoyennesTrimestrielsBE> ListMoyennesTrimestrielles = new List<MoyennesTrimestrielsBE>();
                                    ListMoyennesTrimestrielles = saisieAppreciationMoyenneBL.listerMoyennesTrimestriellesDesElevesDuneClasse(cmbClasse.Text, cmbMatiere.Text, LTrimestre.ElementAt(i).codetrimestre, Convert.ToInt16(txtAnnee.Text));
                                    //on recherche et on liste les résultats

                                    if (ListMoyennesTrimestrielles != null && ListMoyennesTrimestrielles.Count != 0)
                                    {
                                        for (int k = 0; k < ListMoyennesTrimestrielles.Count; k++)
                                        {
                                            LigneSaisieAppreciationMoyenne ligneSaisieAppreciationMoyenne = new LigneSaisieAppreciationMoyenne();

                                            EleveBE eleve = new EleveBE();
                                            eleve.matricule = ListMoyennesTrimestrielles.ElementAt(k).matricule;
                                            eleve = saisieAppreciationMoyenneBL.rechercherEleve(eleve);

                                            if (eleve != null)
                                            {
                                                ligneSaisieAppreciationMoyenne.nomEleve = eleve.nom;
                                                ligneSaisieAppreciationMoyenne.matricule = ListMoyennesTrimestrielles.ElementAt(k).matricule;
                                                ligneSaisieAppreciationMoyenne.Periode = ListMoyennesTrimestrielles.ElementAt(k).codeTrimestre;
                                                ligneSaisieAppreciationMoyenne.annee = ListMoyennesTrimestrielles.ElementAt(k).annee;
                                                ligneSaisieAppreciationMoyenne.codeMatiere = ListMoyennesTrimestrielles.ElementAt(k).codeMat;
                                                ligneSaisieAppreciationMoyenne.moyenne = ListMoyennesTrimestrielles.ElementAt(k).moyenne;
                                                ligneSaisieAppreciationMoyenne.mention = ListMoyennesTrimestrielles.ElementAt(k).mention;
                                                ligneSaisieAppreciationMoyenne.rang = ListMoyennesTrimestrielles.ElementAt(k).rang;
                                                ligneSaisieAppreciationMoyenne.moyenneClasse = ListMoyennesTrimestrielles.ElementAt(k).moyenneClasse;
                                                ligneSaisieAppreciationMoyenne.moyenneMin = ListMoyennesTrimestrielles.ElementAt(k).moyenneMin;
                                                ligneSaisieAppreciationMoyenne.moyenneMax = ListMoyennesTrimestrielles.ElementAt(k).moyenneMax;
                                                ligneSaisieAppreciationMoyenne.appreciation = ListMoyennesTrimestrielles.ElementAt(k).appreciation;

                                                ligneSaisieAppreciationMoyenne.eleve = eleve;

                                                MatiereBE matiere = new MatiereBE();
                                                matiere.codeMat = ListMoyennesTrimestrielles.ElementAt(k).codeMat;
                                                matiere = saisieAppreciationMoyenneBL.rechercherMatiere(matiere);

                                                ligneSaisieAppreciationMoyenne.matiere = matiere;
                                            }

                                            ListLigneSaisieAppreciation.Add(ligneSaisieAppreciationMoyenne);

                                        }
                                    }
                                    
                                }

                               // MessageBox.Show("Opération Terminée !! ");

                            }
                            else
                            {

                                //--------------------- Action pour un trimestre particulière
                                ListLigneSaisieAppreciation = new List<LigneSaisieAppreciationMoyenne>();

                                List<MoyennesTrimestrielsBE> ListMoyennesTrimestrielles = new List<MoyennesTrimestrielsBE>();
                                ListMoyennesTrimestrielles = saisieAppreciationMoyenneBL.listerMoyennesTrimestriellesDesElevesDuneClasse(cmbClasse.Text, cmbMatiere.Text, cmbChoixPeriode.Text, Convert.ToInt16(txtAnnee.Text));
                                //on recherche et on liste les résultats

                                if (ListMoyennesTrimestrielles != null && ListMoyennesTrimestrielles.Count != 0)
                                {
                                    for (int k = 0; k < ListMoyennesTrimestrielles.Count; k++)
                                    {
                                        LigneSaisieAppreciationMoyenne ligneSaisieAppreciationMoyenne = new LigneSaisieAppreciationMoyenne();

                                        EleveBE eleve = new EleveBE();
                                        eleve.matricule = ListMoyennesTrimestrielles.ElementAt(k).matricule;
                                        eleve = saisieAppreciationMoyenneBL.rechercherEleve(eleve);

                                        if (eleve != null)
                                        {
                                            ligneSaisieAppreciationMoyenne.nomEleve = eleve.nom;
                                            ligneSaisieAppreciationMoyenne.matricule = ListMoyennesTrimestrielles.ElementAt(k).matricule;
                                            ligneSaisieAppreciationMoyenne.Periode = ListMoyennesTrimestrielles.ElementAt(k).codeTrimestre;
                                            ligneSaisieAppreciationMoyenne.annee = ListMoyennesTrimestrielles.ElementAt(k).annee;
                                            ligneSaisieAppreciationMoyenne.codeMatiere = ListMoyennesTrimestrielles.ElementAt(k).codeMat;
                                            ligneSaisieAppreciationMoyenne.moyenne = ListMoyennesTrimestrielles.ElementAt(k).moyenne;
                                            ligneSaisieAppreciationMoyenne.mention = ListMoyennesTrimestrielles.ElementAt(k).mention;
                                            ligneSaisieAppreciationMoyenne.rang = ListMoyennesTrimestrielles.ElementAt(k).rang;
                                            ligneSaisieAppreciationMoyenne.moyenneClasse = ListMoyennesTrimestrielles.ElementAt(k).moyenneClasse;
                                            ligneSaisieAppreciationMoyenne.moyenneMin = ListMoyennesTrimestrielles.ElementAt(k).moyenneMin;
                                            ligneSaisieAppreciationMoyenne.moyenneMax = ListMoyennesTrimestrielles.ElementAt(k).moyenneMax;
                                            ligneSaisieAppreciationMoyenne.appreciation = ListMoyennesTrimestrielles.ElementAt(k).appreciation;

                                            ligneSaisieAppreciationMoyenne.eleve = eleve;

                                            MatiereBE matiere = new MatiereBE();
                                            matiere.codeMat = ListMoyennesTrimestrielles.ElementAt(k).codeMat;
                                            matiere = saisieAppreciationMoyenneBL.rechercherMatiere(matiere);

                                            ligneSaisieAppreciationMoyenne.matiere = matiere;
                                        }

                                        ListLigneSaisieAppreciation.Add(ligneSaisieAppreciationMoyenne);

                                    }
                                }

                                
                                //MessageBox.Show("Opération Terminée !!");

                            }
                        }
                        else MessageBox.Show("Vous devez choisir un Trimestre !");
                    }
                    else
                    {
                        // traitement pour une année

                        //--------------------- Action pour une Séquence particulière
                        ListLigneSaisieAppreciation = new List<LigneSaisieAppreciationMoyenne>();

                        List<MoyennesAnnuellesBE> ListMoyennesAnnuelles = new List<MoyennesAnnuellesBE>();
                        ListMoyennesAnnuelles = saisieAppreciationMoyenneBL.listerMoyennesAnnuellesDesElevesDuneClasse(codeClasse, cmbMatiere.Text, Convert.ToInt16(txtAnnee.Text));
                        //on recherche et on liste les résultats

                        if (ListMoyennesAnnuelles != null && ListMoyennesAnnuelles.Count != 0)
                        {
                            for (int k = 0; k < ListMoyennesAnnuelles.Count; k++)
                            {
                                LigneSaisieAppreciationMoyenne ligneSaisieAppreciationMoyenne = new LigneSaisieAppreciationMoyenne();

                                EleveBE eleve = new EleveBE();
                                eleve.matricule = ListMoyennesAnnuelles.ElementAt(k).matricule;
                                eleve = saisieAppreciationMoyenneBL.rechercherEleve(eleve);

                                if (eleve != null)
                                {
                                    ligneSaisieAppreciationMoyenne.nomEleve = eleve.nom;
                                    ligneSaisieAppreciationMoyenne.matricule = ListMoyennesAnnuelles.ElementAt(k).matricule;
                                    ligneSaisieAppreciationMoyenne.Periode = "-";
                                    ligneSaisieAppreciationMoyenne.annee = ListMoyennesAnnuelles.ElementAt(k).annee;
                                    ligneSaisieAppreciationMoyenne.codeMatiere = ListMoyennesAnnuelles.ElementAt(k).codeMat;
                                    ligneSaisieAppreciationMoyenne.moyenne = ListMoyennesAnnuelles.ElementAt(k).moyenne;
                                    ligneSaisieAppreciationMoyenne.mention = ListMoyennesAnnuelles.ElementAt(k).mention;
                                    ligneSaisieAppreciationMoyenne.rang = ListMoyennesAnnuelles.ElementAt(k).rang;
                                    ligneSaisieAppreciationMoyenne.moyenneClasse = ListMoyennesAnnuelles.ElementAt(k).moyenneClasse;
                                    ligneSaisieAppreciationMoyenne.moyenneMin = ListMoyennesAnnuelles.ElementAt(k).moyenneMin;
                                    ligneSaisieAppreciationMoyenne.moyenneMax = ListMoyennesAnnuelles.ElementAt(k).moyenneMax;
                                    ligneSaisieAppreciationMoyenne.appreciation = ListMoyennesAnnuelles.ElementAt(k).appreciation;

                                    ligneSaisieAppreciationMoyenne.eleve = eleve;

                                    MatiereBE matiere = new MatiereBE();
                                    matiere.codeMat = ListMoyennesAnnuelles.ElementAt(k).codeMat;
                                    matiere = saisieAppreciationMoyenneBL.rechercherMatiere(matiere);

                                    ligneSaisieAppreciationMoyenne.matiere = matiere;
                                }

                                ListLigneSaisieAppreciation.Add(ligneSaisieAppreciationMoyenne);

                            }
                        }
                        
                        //MessageBox.Show("Opération Terminée !!");

                    }
                }

            }
            else MessageBox.Show("Tous les champs doivent êtres remplis !! ");
        }

        private void cmdAnnuler_Click(object sender, RoutedEventArgs e)
        {
            cmbClasse.Text = null;
            cmbPeriode.Text = null;
            cmbChoixPeriode.Text = null;

            annee = saisieAppreciationMoyenneBL.getAnneeEnCours();

            txtAnnee.Text = Convert.ToString(annee);
            txtAnneeScolaire.Text = (annee - 1).ToString();

            lblChoixPeriode.Content = "";
            lblChoixPeriode.Visibility = System.Windows.Visibility.Hidden;
            cmbChoixPeriode.Visibility = System.Windows.Visibility.Hidden;
            cmbMatiere.Text = null;

            grdListeDesMoyennes.ItemsSource = null;
        }

        private void cmbPeriode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbPeriode.SelectedItem != null)
            {
                if (cmbPeriode.SelectedItem.Equals("Séquence"))
                {
                    //on a choisi recalculer le résultat d'un élève pour une séquence
                    List<SequenceBE> LSequence = saisieAppreciationMoyenneBL.listerToutesLesSequences();
                    // ------------------- Chargement de la liste des codes de séquence dans le comboBox de la fenêtre 
                    cmbChoixPeriode.ItemsSource = saisieAppreciationMoyenneBL.getListCodeSequence2(LSequence);

                    lblChoixPeriode.Content = "Séquence";
                    lblChoixPeriode.Visibility = System.Windows.Visibility.Visible;
                    cmbChoixPeriode.Visibility = System.Windows.Visibility.Visible;

                }
                else if (cmbPeriode.SelectedItem.Equals("Trimestre"))
                {
                    //on a choisi recalculer le résultat d'un élève pour un Trimestre
                    List<TrimestreBE> LTrimestre = saisieAppreciationMoyenneBL.listerTousLesTrimestres();
                    // ------------------- Chargement de la liste des codes de Trimestre dans le comboBox de la fenêtre 
                    cmbChoixPeriode.ItemsSource = saisieAppreciationMoyenneBL.getListCodeTrimestre2(LTrimestre);

                    lblChoixPeriode.Content = "Trimestre";
                    lblChoixPeriode.Visibility = System.Windows.Visibility.Visible;
                    cmbChoixPeriode.Visibility = System.Windows.Visibility.Visible;
                }
                else if (cmbPeriode.SelectedItem.Equals("Année"))
                {
                    lblChoixPeriode.Visibility = System.Windows.Visibility.Hidden;
                    cmbChoixPeriode.Visibility = System.Windows.Visibility.Hidden;
                }
            }
        }

        private void cmdFermer_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cmbClasse_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //on liste les matières programmées dans la classe
            ClasseBE classe = new ClasseBE();
            classe.codeClasse = Convert.ToString(cmbClasse.SelectedItem);

            classe = saisieAppreciationMoyenneBL.rechercherClasse(classe);
            List<MatiereBE> ListMatiere = null;
            if (classe != null) { 
                ListMatiere = saisieAppreciationMoyenneBL.listerLesMatieresDuneClasse(classe, Convert.ToInt16(txtAnnee.Text));

            }

            cmbMatiere.ItemsSource = saisieAppreciationMoyenneBL.getListCodeMatiere(ListMatiere);
        }

        private void txtAnnee_KeyUp(object sender, KeyEventArgs e)
        {
            //on liste les matières programmées dans la classe
            ClasseBE classe = new ClasseBE();
            classe.codeClasse = Convert.ToString(cmbClasse.SelectedItem);

            classe = saisieAppreciationMoyenneBL.rechercherClasse(classe);
            List<MatiereBE> ListMatiere = null;
            if (classe != null)
            {
                ListMatiere = saisieAppreciationMoyenneBL.listerLesMatieresDuneClasse(classe, Convert.ToInt16(txtAnnee.Text));

            }

            cmbMatiere.ItemsSource = saisieAppreciationMoyenneBL.getListCodeMatiere(ListMatiere);
        }

        public static void SelectCellByIndex(DataGrid dataGrid, int rowIndex, int columnIndex)
        {
            if (!dataGrid.SelectionUnit.Equals(DataGridSelectionUnit.Cell))
                MessageBox.Show("The SelectionUnit of the DataGrid must be set to Cell.");
            else
            {
                if (rowIndex < 0 || rowIndex > (dataGrid.Items.Count - 1))
                    MessageBox.Show(rowIndex + " is an invalid row index.");
                else
                {
                    if (columnIndex < 0 || columnIndex > (dataGrid.Columns.Count - 1))
                        MessageBox.Show(columnIndex + " is an invalid row index.");
                    else
                    {
                        dataGrid.SelectedCells.Clear();

                        object item = dataGrid.Items[rowIndex]; //=Product X
                        DataGridRow row = dataGrid.ItemContainerGenerator.ContainerFromIndex(rowIndex) as DataGridRow;
                        if (row == null)
                        {
                            dataGrid.ScrollIntoView(item);
                            row = dataGrid.ItemContainerGenerator.ContainerFromIndex(rowIndex) as DataGridRow;
                        }
                        if (row != null)
                        {
                            DataGridCell cell = GetCell(dataGrid, row, columnIndex);
                            if (cell != null)
                            {
                                DataGridCellInfo dataGridCellInfo = new DataGridCellInfo(cell);
                                dataGrid.SelectedCells.Add(dataGridCellInfo);
                                cell.Focus();
                            }
                        }
                    }
                }
            }
        }

        public static T FindVisualChild<T>(DependencyObject obj) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is T)
                    return (T)child;
                else
                {
                    T childOfChild = FindVisualChild<T>(child);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }
            return null;
        }

        public static DataGridCell GetCell(DataGrid dataGrid, DataGridRow rowContainer, int column)
        {
            if (rowContainer != null)
            {
                DataGridCellsPresenter presenter = FindVisualChild<DataGridCellsPresenter>(rowContainer);
                if (presenter == null)
                {
                    /* if the row has been virtualized away, call its ApplyTemplate() method
                     * to build its visual tree in order for the DataGridCellsPresenter
                     * and the DataGridCells to be created */
                    rowContainer.ApplyTemplate();
                    presenter = FindVisualChild<DataGridCellsPresenter>(rowContainer);
                }
                if (presenter != null)
                {
                    DataGridCell cell = presenter.ItemContainerGenerator.ContainerFromIndex(column) as DataGridCell;
                    if (cell == null)
                    {
                        /* bring the column into view
                         * in case it has been virtualized away */
                        dataGrid.ScrollIntoView(rowContainer, dataGrid.Columns[column]);
                        cell = presenter.ItemContainerGenerator.ContainerFromIndex(column) as DataGridCell;
                    }
                    return cell;
                }
            }
            return null;
        }

        private void grdListeDesMoyennes_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            int APPRECIATION = 11;
            int MAX_ROW = ListLigneSaisieAppreciation.Count;
            var grid = sender as DataGrid;
            DataGridRow row = new DataGridRow();
            string codeclasse = cmbClasse.SelectedValue.ToString();
            int annee = Convert.ToInt32(txtAnnee.Text);
            string codeMatiere = cmbMatiere.Text;
            string periode = cmbPeriode.Text;
            string choixPeriode = cmbChoixPeriode.Text;
            LigneSaisieAppreciationMoyenne ligne = new LigneSaisieAppreciationMoyenne();
            string matricule, appreciation;

            if (e.Key == Key.Return)
            {
                e.Handled = true;

                int row_index = grdListeDesMoyennes.Items.IndexOf(grdListeDesMoyennes.CurrentItem);
                int col_index = grdListeDesMoyennes.CurrentColumn.DisplayIndex;
                row = (DataGridRow)grid.ItemContainerGenerator.ContainerFromIndex(row_index);
                if (col_index == APPRECIATION)
                {
                    DataGridCellInfo cell = grid.SelectedCells[0];
                    TextBox textBox;
                    try
                    {
                        textBox = (TextBox)cell.Column.GetCellContent(cell.Item);

                        ligne = ListLigneSaisieAppreciation.ElementAt(row_index);
                        appreciation = textBox.Text;
                        matricule = ligne.matricule;
                        //********************
                        saisieAppreciationMoyenneBL.enregistrerAppreciationMoyenne(matricule, periode, choixPeriode, codeMatiere, annee, appreciation);
                        //******************

                        row_index = (row_index + 1) % MAX_ROW;
                        SelectCellByIndex(grdListeDesMoyennes, row_index, APPRECIATION);
                    }
                    catch (Exception)
                    {
                        row_index = (row_index + 1) % MAX_ROW;
                        SelectCellByIndex(grdListeDesMoyennes, row_index, APPRECIATION);
                    }
                }
                else
                    MessageBox.Show("cellule non modifiable");
            }
        }

        private void cmbImprimer_Click(object sender, RoutedEventArgs e)
        {
            CreerEtat etat = new CreerEtat("Saisie Appréciation moyennes - " + cmbPeriode.Text + " - " + cmbChoixPeriode.Text + " -" + DateTime.Today.ToShortDateString(), "état des appréciations");
            etat.obtenirEtatAppreciationMoyenne(grdListeDesMoyennes);
        
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

        private void txtAnneeScolaire_KeyUp(object sender, KeyEventArgs e)
        {
            //on liste les matières programmées dans la classe
            ClasseBE classe = new ClasseBE();
            classe.codeClasse = Convert.ToString(cmbClasse.SelectedItem);

            classe = saisieAppreciationMoyenneBL.rechercherClasse(classe);
            List<MatiereBE> ListMatiere = null;
            if (classe != null)
            {
                ListMatiere = saisieAppreciationMoyenneBL.listerLesMatieresDuneClasse(classe, Convert.ToInt16(txtAnnee.Text));

            }

            cmbMatiere.ItemsSource = saisieAppreciationMoyenneBL.getListCodeMatiere(ListMatiere);
        }

    }
}
