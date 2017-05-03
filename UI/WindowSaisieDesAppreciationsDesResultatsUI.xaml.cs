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
    /// Interaction logic for WindowSaisieDesAppreciationsDesResultatsUI.xaml
    /// </summary>
    public partial class WindowSaisieDesAppreciationsDesResultatsUI : Window
    {
          public SaisieAppreciationResultatBL saisieAppreciationResultatBL;

          public  List<LigneSaisieAppreciationResultat> ListLigneSaisieAppreciation;

          int annee;

          public WindowSaisieDesAppreciationsDesResultatsUI()
        {
            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";
            Thread.CurrentThread.CurrentCulture = ci;

            InitializeComponent();

            saisieAppreciationResultatBL = new SaisieAppreciationResultatBL();
            ListLigneSaisieAppreciation = new List<LigneSaisieAppreciationResultat>();

            //on charge les périodes dans le comboBox
            String[] periode = { "Séquence", "Trimestre", "Année" };
            cmbPeriode.ItemsSource = periode;

            List<ClasseBE> LClasse = saisieAppreciationResultatBL.listerToutesLesClasses();
            cmbClasse.ItemsSource = saisieAppreciationResultatBL.getListCodeClasse(LClasse);


            annee = saisieAppreciationResultatBL.getAnneeEnCours();

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
                    List<ClasseBE> LClasse = saisieAppreciationResultatBL.listerToutesLesClasses();
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
                                    List<SequenceBE> LSequence = saisieAppreciationResultatBL.listerToutesLesSequences();
                                    ListLigneSaisieAppreciation = new List<LigneSaisieAppreciationResultat>();
                                    for (int j = 0; j < LClasse.Count; j++)
                                    {
                                        for (int i = 0; i < LSequence.Count; i++)
                                        {
                                            List<ResultatBE> ListResultatSequentiels = new List<ResultatBE>();
                                            ListResultatSequentiels = saisieAppreciationResultatBL.listerResultatsSequentielleDesElevesDuneClasse(LClasse.ElementAt(j).codeClasse, LSequence.ElementAt(i).codeseq, Convert.ToInt16(txtAnnee.Text));
                                            //on recherche et on liste les résultats

                                            if (ListResultatSequentiels != null && ListResultatSequentiels.Count != 0)
                                            {
                                                for (int k = 0; k < ListResultatSequentiels.Count; k++)
                                                {
                                                    LigneSaisieAppreciationResultat ligneSaisieAppreciationResultat = new LigneSaisieAppreciationResultat();

                                                    EleveBE eleve = new EleveBE();
                                                    eleve.matricule = ListResultatSequentiels.ElementAt(k).matricule;
                                                    eleve = saisieAppreciationResultatBL.rechercherEleve(eleve);

                                                    if(eleve != null){
                                                        ligneSaisieAppreciationResultat.nomEleve = eleve.nom;
                                                        ligneSaisieAppreciationResultat.matricule = ListResultatSequentiels.ElementAt(k).matricule;
                                                        ligneSaisieAppreciationResultat.Periode = ListResultatSequentiels.ElementAt(k).codeseq;
                                                        ligneSaisieAppreciationResultat.annee = ListResultatSequentiels.ElementAt(k).annee;
                                                       // ligneSaisieAppreciationResultat.codeMatiere = ListResultatSequentiels.ElementAt(k).codeMat;
                                                        ligneSaisieAppreciationResultat.moyenne = ListResultatSequentiels.ElementAt(k).moyenne;
                                                        ligneSaisieAppreciationResultat.mention = ListResultatSequentiels.ElementAt(k).mention;
                                                        ligneSaisieAppreciationResultat.rang = ListResultatSequentiels.ElementAt(k).rang;
                                                        ligneSaisieAppreciationResultat.moyenneClasse = ListResultatSequentiels.ElementAt(k).moyenneclasse;
                                                        //ligneSaisieAppreciationResultat.moyenneMin = ListResultatSequentiels.ElementAt(k).moyenneMin;
                                                        //ligneSaisieAppreciationResultat.moyenneMax = ListResultatSequentiels.ElementAt(k).moyenneMax;
                                                        ligneSaisieAppreciationResultat.decision = ListResultatSequentiels.ElementAt(k).decision;
                                                        
                                                        ligneSaisieAppreciationResultat.appreciation = ListResultatSequentiels.ElementAt(k).appreciation;

                                                        ligneSaisieAppreciationResultat.eleve = eleve;

                                                    }

                                                    ListLigneSaisieAppreciation.Add(ligneSaisieAppreciationResultat);

                                                }
                                            }

                                        }
                                    }

                                    //MessageBox.Show("Opération Terminée !! ");
                                    grdListeDesResultats.ItemsSource = ListLigneSaisieAppreciation;
                                }
                                else
                                {

                                    //--------------------- Action pour une Séquence particulière
                                    ListLigneSaisieAppreciation = new List<LigneSaisieAppreciationResultat>();

                                    for (int j = 0; j < LClasse.Count; j++)
                                    {
                                        List<ResultatBE> ListResultatsSequentielles = new List<ResultatBE>();
                                        ListResultatsSequentielles = saisieAppreciationResultatBL.listerResultatsSequentielleDesElevesDuneClasse(LClasse.ElementAt(j).codeClasse, cmbChoixPeriode.Text, Convert.ToInt16(txtAnnee.Text));
                                        //on recherche et on liste les résultats

                                        if (ListResultatsSequentielles != null && ListResultatsSequentielles.Count != 0)
                                        {
                                            for (int k = 0; k < ListResultatsSequentielles.Count; k++)
                                            {
                                                LigneSaisieAppreciationResultat ligneSaisieAppreciationResultat = new LigneSaisieAppreciationResultat();

                                                EleveBE eleve = new EleveBE();
                                                eleve.matricule = ListResultatsSequentielles.ElementAt(k).matricule;
                                                eleve = saisieAppreciationResultatBL.rechercherEleve(eleve);

                                                if (eleve != null)
                                                {
                                                    ligneSaisieAppreciationResultat.nomEleve = eleve.nom;
                                                    ligneSaisieAppreciationResultat.matricule = ListResultatsSequentielles.ElementAt(k).matricule;
                                                    ligneSaisieAppreciationResultat.Periode = ListResultatsSequentielles.ElementAt(k).codeseq;
                                                    ligneSaisieAppreciationResultat.annee = ListResultatsSequentielles.ElementAt(k).annee;
                                                    //ligneSaisieAppreciationResultat.codeMatiere = ListResultatsSequentielles.ElementAt(k).codeMat;
                                                    ligneSaisieAppreciationResultat.moyenne = ListResultatsSequentielles.ElementAt(k).moyenne;
                                                    ligneSaisieAppreciationResultat.mention = ListResultatsSequentielles.ElementAt(k).mention;
                                                    ligneSaisieAppreciationResultat.rang = ListResultatsSequentielles.ElementAt(k).rang;
                                                    ligneSaisieAppreciationResultat.moyenneClasse = ListResultatsSequentielles.ElementAt(k).moyenneclasse;
                                                    //ligneSaisieAppreciationResultat.moyenneMin = ListResultatsSequentielles.ElementAt(k).moyennein;
                                                    //ligneSaisieAppreciationResultat.moyenneMax = ListResultatsSequentielles.ElementAt(k).moyenneMax;
                                                    ligneSaisieAppreciationResultat.decision = ListResultatsSequentielles.ElementAt(k).decision;
                                                    
                                                    ligneSaisieAppreciationResultat.appreciation = ListResultatsSequentielles.ElementAt(k).appreciation;

                                                    ligneSaisieAppreciationResultat.eleve = eleve;

                                                }

                                                ListLigneSaisieAppreciation.Add(ligneSaisieAppreciationResultat);

                                            }
                                        }
                                        
                                    }

                                    //MessageBox.Show("Opération Terminée !! ");
                                    grdListeDesResultats.ItemsSource = ListLigneSaisieAppreciation;
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
                                    List<TrimestreBE> LTrimestre = saisieAppreciationResultatBL.listerTousLesTrimestres();
                                   
                                    ListLigneSaisieAppreciation = new List<LigneSaisieAppreciationResultat>();

                                    for (int j = 0; j < LClasse.Count; j++)
                                    {
                                        for (int i = 0; i < LTrimestre.Count; i++)
                                        {

                                            List<ResultatTrimestrielBE> ListResultatsTrimestrielles = new List<ResultatTrimestrielBE>();
                                            ListResultatsTrimestrielles = saisieAppreciationResultatBL.listerResultatsTrimestriellesDesElevesDuneClasse(LClasse.ElementAt(j).codeClasse, LTrimestre.ElementAt(i).codetrimestre, Convert.ToInt16(txtAnnee.Text));
                                            //on recherche et on liste les résultats

                                            if (ListResultatsTrimestrielles != null && ListResultatsTrimestrielles.Count != 0)
                                            {
                                                for (int k = 0; k < ListResultatsTrimestrielles.Count; k++)
                                                {
                                                    LigneSaisieAppreciationResultat ligneSaisieAppreciationResultat = new LigneSaisieAppreciationResultat();

                                                    EleveBE eleve = new EleveBE();
                                                    eleve.matricule = ListResultatsTrimestrielles.ElementAt(k).matricule;
                                                    eleve = saisieAppreciationResultatBL.rechercherEleve(eleve);

                                                    if (eleve != null)
                                                    {
                                                        ligneSaisieAppreciationResultat.nomEleve = eleve.nom;
                                                        ligneSaisieAppreciationResultat.matricule = ListResultatsTrimestrielles.ElementAt(k).matricule;
                                                        ligneSaisieAppreciationResultat.Periode = ListResultatsTrimestrielles.ElementAt(k).codeTrimestre;
                                                        ligneSaisieAppreciationResultat.annee = ListResultatsTrimestrielles.ElementAt(k).annee;
                                                        //ligneSaisieAppreciationResultat.codeMatiere = ListResultatsTrimestrielles.ElementAt(k).codeMat;
                                                        ligneSaisieAppreciationResultat.moyenne = ListResultatsTrimestrielles.ElementAt(k).moyenne;
                                                        ligneSaisieAppreciationResultat.mention = ListResultatsTrimestrielles.ElementAt(k).mention;
                                                        ligneSaisieAppreciationResultat.rang = ListResultatsTrimestrielles.ElementAt(k).rang;
                                                        ligneSaisieAppreciationResultat.moyenneClasse = ListResultatsTrimestrielles.ElementAt(k).moyenneclasse;
                                                        //ligneSaisieAppreciationResultat.moyenneMin = ListResultatsTrimestrielles.ElementAt(k).moyenneMin;
                                                        //ligneSaisieAppreciationResultat.moyenneMax = ListResultatsTrimestrielles.ElementAt(k).moyenneMax;
                                                        ligneSaisieAppreciationResultat.decision = ListResultatsTrimestrielles.ElementAt(k).decision;
                                                        
                                                        ligneSaisieAppreciationResultat.appreciation = ListResultatsTrimestrielles.ElementAt(k).appreciation;

                                                        ligneSaisieAppreciationResultat.eleve = eleve;

                                                    }

                                                    ListLigneSaisieAppreciation.Add(ligneSaisieAppreciationResultat);

                                                }
                                            }
                                            
                                        }
                                    }

                                    //MessageBox.Show("Opération Terminée !! ");
                                    grdListeDesResultats.ItemsSource = ListLigneSaisieAppreciation;
                                }
                                else
                                {

                                    //--------------------- Action pour un trimestre particulière
                                    ListLigneSaisieAppreciation = new List<LigneSaisieAppreciationResultat>();

                                    for (int j = 0; j < LClasse.Count; j++)
                                    {
                                        List<ResultatTrimestrielBE> ListResultatsTrimestriels = new List<ResultatTrimestrielBE>();
                                        ListResultatsTrimestriels = saisieAppreciationResultatBL.listerResultatsTrimestriellesDesElevesDuneClasse(LClasse.ElementAt(j).codeClasse, cmbChoixPeriode.Text, Convert.ToInt16(txtAnnee.Text));
                                        //on recherche et on liste les résultats

                                        if (ListResultatsTrimestriels != null && ListResultatsTrimestriels.Count != 0)
                                        {
                                            for (int k = 0; k < ListResultatsTrimestriels.Count; k++)
                                            {
                                                LigneSaisieAppreciationResultat ligneSaisieAppreciationResultat = new LigneSaisieAppreciationResultat();

                                                EleveBE eleve = new EleveBE();
                                                eleve.matricule = ListResultatsTrimestriels.ElementAt(k).matricule;
                                                eleve = saisieAppreciationResultatBL.rechercherEleve(eleve);

                                                if (eleve != null)
                                                {
                                                    ligneSaisieAppreciationResultat.nomEleve = eleve.nom;
                                                    ligneSaisieAppreciationResultat.matricule = ListResultatsTrimestriels.ElementAt(k).matricule;
                                                    ligneSaisieAppreciationResultat.Periode = ListResultatsTrimestriels.ElementAt(k).codeTrimestre;
                                                    ligneSaisieAppreciationResultat.annee = ListResultatsTrimestriels.ElementAt(k).annee;
                                                    //ligneSaisieAppreciationResultat.codeMatiere = ListResultatsTrimestriels.ElementAt(k).codeMat;
                                                    ligneSaisieAppreciationResultat.moyenne = ListResultatsTrimestriels.ElementAt(k).moyenne;
                                                    ligneSaisieAppreciationResultat.mention = ListResultatsTrimestriels.ElementAt(k).mention;
                                                    ligneSaisieAppreciationResultat.rang = ListResultatsTrimestriels.ElementAt(k).rang;
                                                    ligneSaisieAppreciationResultat.moyenneClasse = ListResultatsTrimestriels.ElementAt(k).moyenneclasse;
                                                    //ligneSaisieAppreciationResultat.moyenneMin = ListResultatsTrimestriels.ElementAt(k).moyenneMin;
                                                    //ligneSaisieAppreciationResultat.moyenneMax = ListResultatsTrimestriels.ElementAt(k).moyenneMax;
                                                    ligneSaisieAppreciationResultat.decision = ListResultatsTrimestriels.ElementAt(k).decision;
                                                    
                                                    ligneSaisieAppreciationResultat.appreciation = ListResultatsTrimestriels.ElementAt(k).appreciation;

                                                    ligneSaisieAppreciationResultat.eleve = eleve;

                                                }

                                                ListLigneSaisieAppreciation.Add(ligneSaisieAppreciationResultat);

                                            }
                                        }

                                        
                                    }

                                    //MessageBox.Show("Opération Terminée !!");
                                    grdListeDesResultats.ItemsSource = ListLigneSaisieAppreciation;
                                }
                            }
                            else MessageBox.Show("Vous devez choisir un Trimestre !");
                        }
                        else
                        {
                            // traitement pour une année

                            //--------------------- Action pour une Séquence particulière
                            ListLigneSaisieAppreciation = new List<LigneSaisieAppreciationResultat>();

                            for (int j = 0; j < LClasse.Count; j++)
                            {
                                List<ResultatAnnuelBE> ListResultatsAnnuels = new List<ResultatAnnuelBE>();
                                ListResultatsAnnuels = saisieAppreciationResultatBL.listerResultatsAnnuellesDesElevesDuneClasse(LClasse.ElementAt(j).codeClasse, Convert.ToInt16(txtAnnee.Text));
                                //on recherche et on liste les résultats

                                if (ListResultatsAnnuels != null && ListResultatsAnnuels.Count != 0)
                                {
                                    for (int k = 0; k < ListResultatsAnnuels.Count; k++)
                                    {
                                        LigneSaisieAppreciationResultat ligneSaisieAppreciationResultat = new LigneSaisieAppreciationResultat();

                                        EleveBE eleve = new EleveBE();
                                        eleve.matricule = ListResultatsAnnuels.ElementAt(k).matricule;
                                        eleve = saisieAppreciationResultatBL.rechercherEleve(eleve);

                                        if (eleve != null)
                                        {
                                            ligneSaisieAppreciationResultat.nomEleve = eleve.nom;
                                            ligneSaisieAppreciationResultat.matricule = ListResultatsAnnuels.ElementAt(k).matricule;
                                            ligneSaisieAppreciationResultat.Periode = "-";
                                            ligneSaisieAppreciationResultat.annee = ListResultatsAnnuels.ElementAt(k).annee;
                                            //ligneSaisieAppreciationResultat.codeMatiere = ListResultatsAnnuels.ElementAt(k).codeMat;
                                            ligneSaisieAppreciationResultat.moyenne = ListResultatsAnnuels.ElementAt(k).moyenne;
                                            ligneSaisieAppreciationResultat.mention = ListResultatsAnnuels.ElementAt(k).mention;
                                            ligneSaisieAppreciationResultat.rang = ListResultatsAnnuels.ElementAt(k).rang;
                                            ligneSaisieAppreciationResultat.moyenneClasse = ListResultatsAnnuels.ElementAt(k).moyenneclasse;
                                            //ligneSaisieAppreciationResultat.moyenneMin = ListResultatsAnnuels.ElementAt(k).moyenneMin;
                                            //ligneSaisieAppreciationResultat.moyenneMax = ListResultatsAnnuels.ElementAt(k).moyenneMax;
                                            ligneSaisieAppreciationResultat.decision = ListResultatsAnnuels.ElementAt(k).decision;
                                            
                                            ligneSaisieAppreciationResultat.appreciation = ListResultatsAnnuels.ElementAt(k).appreciation;

                                            ligneSaisieAppreciationResultat.eleve = eleve;

                                        }

                                        ListLigneSaisieAppreciation.Add(ligneSaisieAppreciationResultat);

                                    }
                                }
                                
                            }

                            //MessageBox.Show("Opération Terminée !!");
                            grdListeDesResultats.ItemsSource = ListLigneSaisieAppreciation;

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
                                List<SequenceBE> LSequence = saisieAppreciationResultatBL.listerToutesLesSequences();

                                ListLigneSaisieAppreciation = new List<LigneSaisieAppreciationResultat>();

                                for (int i = 0; i < LSequence.Count; i++)
                                {
                                    List<ResultatBE> ListResultatsSequentiels = new List<ResultatBE>();
                                    ListResultatsSequentiels = saisieAppreciationResultatBL.listerResultatsSequentielleDesElevesDuneClasse(codeClasse, LSequence.ElementAt(i).codeseq, Convert.ToInt16(txtAnnee.Text));
                                    //on recherche et on liste les résultats

                                    if (ListResultatsSequentiels != null && ListResultatsSequentiels.Count != 0)
                                    {
                                        for (int k = 0; k < ListResultatsSequentiels.Count; k++)
                                        {
                                            LigneSaisieAppreciationResultat ligneSaisieAppreciationResultat = new LigneSaisieAppreciationResultat();

                                            EleveBE eleve = new EleveBE();
                                            eleve.matricule = ListResultatsSequentiels.ElementAt(k).matricule;
                                            eleve = saisieAppreciationResultatBL.rechercherEleve(eleve);

                                            if (eleve != null)
                                            {
                                                ligneSaisieAppreciationResultat.nomEleve = eleve.nom;
                                                ligneSaisieAppreciationResultat.matricule = ListResultatsSequentiels.ElementAt(k).matricule;
                                                ligneSaisieAppreciationResultat.Periode = ListResultatsSequentiels.ElementAt(k).codeseq;
                                                ligneSaisieAppreciationResultat.annee = ListResultatsSequentiels.ElementAt(k).annee;
                                                //ligneSaisieAppreciationResultat.codeMatiere = ListResultatsSequentiels.ElementAt(k).codeMat;
                                                ligneSaisieAppreciationResultat.moyenne = ListResultatsSequentiels.ElementAt(k).moyenne;
                                                ligneSaisieAppreciationResultat.mention = ListResultatsSequentiels.ElementAt(k).mention;
                                                ligneSaisieAppreciationResultat.rang = ListResultatsSequentiels.ElementAt(k).rang;
                                                ligneSaisieAppreciationResultat.moyenneClasse = ListResultatsSequentiels.ElementAt(k).moyenneclasse;
                                                //ligneSaisieAppreciationResultat.moyenneMin = ListResultatsSequentiels.ElementAt(k).moyenneMin;
                                                //ligneSaisieAppreciationResultat.moyenneMax = ListResultatsSequentiels.ElementAt(k).moyenneMax;
                                                ligneSaisieAppreciationResultat.decision = ListResultatsSequentiels.ElementAt(k).decision;
                                                
                                                ligneSaisieAppreciationResultat.appreciation = ListResultatsSequentiels.ElementAt(k).appreciation;

                                                ligneSaisieAppreciationResultat.eleve = eleve;

                                            }

                                            ListLigneSaisieAppreciation.Add(ligneSaisieAppreciationResultat);

                                        }
                                    }

                                }

                                //MessageBox.Show("Opération Terminée !! ");
                                grdListeDesResultats.ItemsSource = ListLigneSaisieAppreciation;

                            }
                            else
                            {

                                //--------------------- Action pour une Séquence particulière
                                ListLigneSaisieAppreciation = new List<LigneSaisieAppreciationResultat>();

                                List<ResultatBE> ListResultatsSequentiels = new List<ResultatBE>();
                                ListResultatsSequentiels = saisieAppreciationResultatBL.listerResultatsSequentielleDesElevesDuneClasse(codeClasse, cmbChoixPeriode.Text, Convert.ToInt16(txtAnnee.Text));
                                //on recherche et on liste les résultats

                                if (ListResultatsSequentiels != null && ListResultatsSequentiels.Count != 0)
                                {
                                    for (int k = 0; k < ListResultatsSequentiels.Count; k++)
                                    {
                                        LigneSaisieAppreciationResultat ligneSaisieAppreciationResultats = new LigneSaisieAppreciationResultat();

                                        EleveBE eleve = new EleveBE();
                                        eleve.matricule = ListResultatsSequentiels.ElementAt(k).matricule;
                                        eleve = saisieAppreciationResultatBL.rechercherEleve(eleve);

                                        if (eleve != null)
                                        {
                                            ligneSaisieAppreciationResultats.nomEleve = eleve.nom;
                                            ligneSaisieAppreciationResultats.matricule = ListResultatsSequentiels.ElementAt(k).matricule;
                                            ligneSaisieAppreciationResultats.Periode = ListResultatsSequentiels.ElementAt(k).codeseq;
                                            ligneSaisieAppreciationResultats.annee = ListResultatsSequentiels.ElementAt(k).annee;
                                            //ligneSaisieAppreciationResultats.codeMatiere = ListResultatsSequentiels.ElementAt(k).codeMat;
                                            ligneSaisieAppreciationResultats.moyenne = ListResultatsSequentiels.ElementAt(k).moyenne;
                                            ligneSaisieAppreciationResultats.mention = ListResultatsSequentiels.ElementAt(k).mention;
                                            ligneSaisieAppreciationResultats.rang = ListResultatsSequentiels.ElementAt(k).rang;
                                            ligneSaisieAppreciationResultats.moyenneClasse = ListResultatsSequentiels.ElementAt(k).moyenneclasse;
                                            //ligneSaisieAppreciationResultats.moyenneMin = ListResultatsSequentiels.ElementAt(k).moyenneMin;
                                            //ligneSaisieAppreciationResultats.moyenneMax = ListResultatsSequentiels.ElementAt(k).moyenneMax;
                                            ligneSaisieAppreciationResultats.decision = ListResultatsSequentiels.ElementAt(k).decision;
                                            
                                            ligneSaisieAppreciationResultats.appreciation = ListResultatsSequentiels.ElementAt(k).appreciation;

                                            ligneSaisieAppreciationResultats.eleve = eleve;

                                        }

                                        ListLigneSaisieAppreciation.Add(ligneSaisieAppreciationResultats);

                                    }

                                    
                                }

                                //MessageBox.Show("Opération Terminée !! ");
                                grdListeDesResultats.ItemsSource = ListLigneSaisieAppreciation;
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
                                List<TrimestreBE> LTrimestre = saisieAppreciationResultatBL.listerTousLesTrimestres();
                                ListLigneSaisieAppreciation = new List<LigneSaisieAppreciationResultat>();

                                for (int i = 0; i < LTrimestre.Count; i++)
                                {
                                    List<ResultatTrimestrielBE> ListResultatsTrimestriels = new List<ResultatTrimestrielBE>();
                                    ListResultatsTrimestriels = saisieAppreciationResultatBL.listerResultatsTrimestriellesDesElevesDuneClasse(cmbClasse.Text, LTrimestre.ElementAt(i).codetrimestre, Convert.ToInt16(txtAnnee.Text));
                                    //on recherche et on liste les résultats

                                    if (ListResultatsTrimestriels != null && ListResultatsTrimestriels.Count != 0)
                                    {
                                        for (int k = 0; k < ListResultatsTrimestriels.Count; k++)
                                        {
                                            LigneSaisieAppreciationResultat ligneSaisieAppreciationResultat = new LigneSaisieAppreciationResultat();

                                            EleveBE eleve = new EleveBE();
                                            eleve.matricule = ListResultatsTrimestriels.ElementAt(k).matricule;
                                            eleve = saisieAppreciationResultatBL.rechercherEleve(eleve);

                                            if (eleve != null)
                                            {
                                                ligneSaisieAppreciationResultat.nomEleve = eleve.nom;
                                                ligneSaisieAppreciationResultat.matricule = ListResultatsTrimestriels.ElementAt(k).matricule;
                                                ligneSaisieAppreciationResultat.Periode = ListResultatsTrimestriels.ElementAt(k).codeTrimestre;
                                                ligneSaisieAppreciationResultat.annee = ListResultatsTrimestriels.ElementAt(k).annee;
                                                //ligneSaisieAppreciationResultat.codeMatiere = ListResultatsTrimestriels.ElementAt(k).codeMat;
                                                ligneSaisieAppreciationResultat.moyenne = ListResultatsTrimestriels.ElementAt(k).moyenne;
                                                ligneSaisieAppreciationResultat.mention = ListResultatsTrimestriels.ElementAt(k).mention;
                                                ligneSaisieAppreciationResultat.rang = ListResultatsTrimestriels.ElementAt(k).rang;
                                                ligneSaisieAppreciationResultat.moyenneClasse = ListResultatsTrimestriels.ElementAt(k).moyenneclasse;
                                                //ligneSaisieAppreciationResultat.moyenneMin = ListResultatsTrimestriels.ElementAt(k).moyenneMin;
                                                //ligneSaisieAppreciationResultat.moyenneMax = ListResultatsTrimestriels.ElementAt(k).moyenneMax;
                                                ligneSaisieAppreciationResultat.decision = ListResultatsTrimestriels.ElementAt(k).decision;
                                                
                                                ligneSaisieAppreciationResultat.appreciation = ListResultatsTrimestriels.ElementAt(k).appreciation;

                                                ligneSaisieAppreciationResultat.eleve = eleve;

                                            }

                                            ListLigneSaisieAppreciation.Add(ligneSaisieAppreciationResultat);

                                        }
                                    }
                                    
                                }

                                //MessageBox.Show("Opération Terminée !! ");

                            }
                            else
                            {

                                //--------------------- Action pour un trimestre particulière
                                ListLigneSaisieAppreciation = new List<LigneSaisieAppreciationResultat>();

                                List<ResultatTrimestrielBE> ListResultatsTrimestriels = new List<ResultatTrimestrielBE>();
                                ListResultatsTrimestriels = saisieAppreciationResultatBL.listerResultatsTrimestriellesDesElevesDuneClasse(cmbClasse.Text, cmbChoixPeriode.Text, Convert.ToInt16(txtAnnee.Text));
                                //on recherche et on liste les résultats

                                if (ListResultatsTrimestriels != null && ListResultatsTrimestriels.Count != 0)
                                {
                                    for (int k = 0; k < ListResultatsTrimestriels.Count; k++)
                                    {
                                        LigneSaisieAppreciationResultat ligneSaisieAppreciationResultat = new LigneSaisieAppreciationResultat();

                                        EleveBE eleve = new EleveBE();
                                        eleve.matricule = ListResultatsTrimestriels.ElementAt(k).matricule;
                                        eleve = saisieAppreciationResultatBL.rechercherEleve(eleve);

                                        if (eleve != null)
                                        {
                                            ligneSaisieAppreciationResultat.nomEleve = eleve.nom;
                                            ligneSaisieAppreciationResultat.matricule = ListResultatsTrimestriels.ElementAt(k).matricule;
                                            ligneSaisieAppreciationResultat.Periode = ListResultatsTrimestriels.ElementAt(k).codeTrimestre;
                                            ligneSaisieAppreciationResultat.annee = ListResultatsTrimestriels.ElementAt(k).annee;
                                            //ligneSaisieAppreciationResultat.codeMatiere = ListResultatsTrimestriels.ElementAt(k).codeMat;
                                            ligneSaisieAppreciationResultat.moyenne = ListResultatsTrimestriels.ElementAt(k).moyenne;
                                            ligneSaisieAppreciationResultat.mention = ListResultatsTrimestriels.ElementAt(k).mention;
                                            ligneSaisieAppreciationResultat.rang = ListResultatsTrimestriels.ElementAt(k).rang;
                                            ligneSaisieAppreciationResultat.moyenneClasse = ListResultatsTrimestriels.ElementAt(k).moyenneclasse;
                                            //ligneSaisieAppreciationResultat.moyenneMin = ListResultatsTrimestriels.ElementAt(k).moyenneMin;
                                            //ligneSaisieAppreciationResultat.moyenneMax = ListResultatsTrimestriels.ElementAt(k).moyenneMax;
                                            ligneSaisieAppreciationResultat.decision = ListResultatsTrimestriels.ElementAt(k).decision;
                                            
                                            ligneSaisieAppreciationResultat.appreciation = ListResultatsTrimestriels.ElementAt(k).appreciation;

                                            ligneSaisieAppreciationResultat.eleve = eleve;

                                        }

                                        ListLigneSaisieAppreciation.Add(ligneSaisieAppreciationResultat);

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
                        ListLigneSaisieAppreciation = new List<LigneSaisieAppreciationResultat>();

                        List<ResultatAnnuelBE> ListResultatsAnnuels = new List<ResultatAnnuelBE>();
                        ListResultatsAnnuels = saisieAppreciationResultatBL.listerResultatsAnnuellesDesElevesDuneClasse(codeClasse, Convert.ToInt16(txtAnnee.Text));
                        //on recherche et on liste les résultats

                        if (ListResultatsAnnuels != null && ListResultatsAnnuels.Count != 0)
                        {
                            for (int k = 0; k < ListResultatsAnnuels.Count; k++)
                            {
                                LigneSaisieAppreciationResultat ligneSaisieAppreciationResultat = new LigneSaisieAppreciationResultat();

                                EleveBE eleve = new EleveBE();
                                eleve.matricule = ListResultatsAnnuels.ElementAt(k).matricule;
                                eleve = saisieAppreciationResultatBL.rechercherEleve(eleve);

                                if (eleve != null)
                                {
                                    ligneSaisieAppreciationResultat.nomEleve = eleve.nom;
                                    ligneSaisieAppreciationResultat.matricule = ListResultatsAnnuels.ElementAt(k).matricule;
                                    ligneSaisieAppreciationResultat.Periode = "-";
                                    ligneSaisieAppreciationResultat.annee = ListResultatsAnnuels.ElementAt(k).annee;
                                    //ligneSaisieAppreciationResultat.codeMatiere = ListResultatsAnnuels.ElementAt(k).codeMat;
                                    ligneSaisieAppreciationResultat.moyenne = ListResultatsAnnuels.ElementAt(k).moyenne;
                                    ligneSaisieAppreciationResultat.mention = ListResultatsAnnuels.ElementAt(k).mention;
                                    ligneSaisieAppreciationResultat.rang = ListResultatsAnnuels.ElementAt(k).rang;
                                    ligneSaisieAppreciationResultat.moyenneClasse = ListResultatsAnnuels.ElementAt(k).moyenneclasse;
                                    //ligneSaisieAppreciationResultat.moyenneMin = ListResultatsAnnuels.ElementAt(k).moyenneMin;
                                    //ligneSaisieAppreciationResultat.moyenneMax = ListResultatsAnnuels.ElementAt(k).moyenneMax;
                                    ligneSaisieAppreciationResultat.decision = ListResultatsAnnuels.ElementAt(k).decision;

                                    ligneSaisieAppreciationResultat.appreciation = ListResultatsAnnuels.ElementAt(k).appreciation;

                                    ligneSaisieAppreciationResultat.eleve = eleve;

                                }

                                ListLigneSaisieAppreciation.Add(ligneSaisieAppreciationResultat);

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

            annee = saisieAppreciationResultatBL.getAnneeEnCours();

            txtAnnee.Text = Convert.ToString(annee);
            txtAnneeScolaire.Text = (annee - 1).ToString();

            lblChoixPeriode.Content = "";
            lblChoixPeriode.Visibility = System.Windows.Visibility.Hidden;
            cmbChoixPeriode.Visibility = System.Windows.Visibility.Hidden;

            grdListeDesResultats.ItemsSource = null;
        }

        private void cmbPeriode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbPeriode.SelectedItem != null)
            {
                if (cmbPeriode.SelectedItem.Equals("Séquence"))
                {
                    //on a choisi recalculer le résultat d'un élève pour une séquence
                    List<SequenceBE> LSequence = saisieAppreciationResultatBL.listerToutesLesSequences();
                    // ------------------- Chargement de la liste des codes de séquence dans le comboBox de la fenêtre 
                    cmbChoixPeriode.ItemsSource = saisieAppreciationResultatBL.getListCodeSequence2(LSequence);

                    lblChoixPeriode.Content = "Séquence";
                    lblChoixPeriode.Visibility = System.Windows.Visibility.Visible;
                    cmbChoixPeriode.Visibility = System.Windows.Visibility.Visible;

                }
                else if (cmbPeriode.SelectedItem.Equals("Trimestre"))
                {
                    //on a choisi recalculer le résultat d'un élève pour un Trimestre
                    List<TrimestreBE> LTrimestre = saisieAppreciationResultatBL.listerTousLesTrimestres();
                    // ------------------- Chargement de la liste des codes de Trimestre dans le comboBox de la fenêtre 
                    cmbChoixPeriode.ItemsSource = saisieAppreciationResultatBL.getListCodeTrimestre2(LTrimestre);

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
            int APPRECIATION = 9;
            int MAX_ROW = ListLigneSaisieAppreciation.Count;
            var grid = sender as DataGrid;
            DataGridRow row = new DataGridRow();
            string codeclasse = cmbClasse.SelectedValue.ToString();
            int annee = Convert.ToInt32(txtAnnee.Text);
            string periode = cmbPeriode.Text;
            string choixPeriode = cmbChoixPeriode.Text;
            LigneSaisieAppreciationResultat ligne = new LigneSaisieAppreciationResultat();
            string matricule, appreciation;

            if (e.Key == Key.Return)
            {
                e.Handled = true;

                int row_index = grdListeDesResultats.Items.IndexOf(grdListeDesResultats.CurrentItem);
                int col_index = grdListeDesResultats.CurrentColumn.DisplayIndex;
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
                        saisieAppreciationResultatBL.enregistrerAppreciationResultat(matricule, periode, choixPeriode, annee, appreciation);
                        //******************

                        row_index = (row_index + 1) % MAX_ROW;
                        SelectCellByIndex(grdListeDesResultats, row_index, APPRECIATION);
                    }
                    catch (Exception)
                    {
                        row_index = (row_index + 1) % MAX_ROW;
                        SelectCellByIndex(grdListeDesResultats, row_index, APPRECIATION);
                    }
                }
                else
                    MessageBox.Show("cellule non modifiable");
            }
        }

        private void cmbImprimer_Click(object sender, RoutedEventArgs e)
        {
            CreerEtat etat = new CreerEtat("Saisie Appréciation resultats - " + cmbPeriode.Text + " - "+ cmbChoixPeriode.Text +" -" + DateTime.Today.ToShortDateString(), "état des appréciations");
            etat.obtenirEtatAppreciationResultat(grdListeDesResultats);
        
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
