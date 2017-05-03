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
using Ecole.BusinessLogic;
using Ecole.ClasseConception;
using Ecole.Utilitaire;

namespace Ecole.UI
{
    /// <summary>
    /// Interaction logic for FicheDePresenceUI.xaml
    /// </summary>
    public partial class FicheDeReleveDeNotes2UI : Window
    {
        ClasseBE classe;
        List<InscrireBE> listInscrits;
        GestionEleveDuneClasseBL eleveBL;
        List<string> listClasses;
        
        
        List<LigneFicheReleveNote> listLigneFiche;

        MatiereBE matiere;
       // List<MatiereBE> listMatiere;
        EvaluerBE evaluation;
        List<string[]> listEvaluation;
        SaisieNotesSansAnonymatBL noteBL;
        DefinirEvaluationMatiereBL evaluationBL;
        public FicheDeReleveDeNotes2UI()
        {
            classe = new ClasseBE();
            matiere = new MatiereBE();
            eleveBL = new GestionEleveDuneClasseBL();
            listClasses = new List<string>();
            listInscrits = new List<InscrireBE>();
            listLigneFiche = new List<LigneFicheReleveNote>();

            evaluation = new EvaluerBE();
            listEvaluation = new List<string[]>();
            noteBL = new SaisieNotesSansAnonymatBL();
            evaluationBL = new DefinirEvaluationMatiereBL();

            InitializeComponent();
            //Obtenir la liste des classes et les ajouter au comboBox des classes
            listClasses = eleveBL.listerValeursColonneClasse("codeclasse");
            cmbClasse.ItemsSource = listClasses;

            txtAnnee.Text = eleveBL.anneeEnCours().ToString();
            txtAnneeScolaire.Text = ((Convert.ToInt32(txtAnnee.Text.ToString())) - 1).ToString()+ "/" + txtAnnee.Text;

        }


        private void cmdImprimer_Click(object sender, RoutedEventArgs e)
        {
            List<ProgrammerBE> listProgramme = new List<ProgrammerBE>();
            string codeclasse = cmbClasse.SelectedValue.ToString(); // le code de la classe selectionnée
            List<string> lisMatieres = new List<string>();
             int numero = 1;
            if (cmbClasse.SelectedValue != null && txtAnnee.Text != "" )
            {
                listInscrits = new List<InscrireBE>();
                codeclasse = cmbClasse.SelectedValue.ToString();
                classe.codeClasse = codeclasse;
                classe = eleveBL.rechercherClasse(classe);

                listLigneFiche = new List<LigneFicheReleveNote>();
                
                try
                {
                    int annee = Convert.ToInt32(txtAnnee.Text);
                    EleveBE eleve = new EleveBE();
                    listInscrits = eleveBL.listerSuivantCritereInscrire(codeclasse, annee);

                    if (listInscrits != null)
                    {

                        foreach (InscrireBE i in listInscrits)
                        {
                            eleve.matricule = i.matricule;
                            eleve = eleveBL.rechercherEleve(eleve);
                            LigneFicheReleveNote LigneFicheReleveNote = new LigneFicheReleveNote(numero++, eleve.nom, eleve.sexe.ElementAt(0).ToString(), listEvaluation);
                            listLigneFiche.Add(LigneFicheReleveNote);
                        }
                    }

                    CreerEtat etat = new CreerEtat("Releve de note- " + cmbClasse.Text + "_" + txtAnnee.Text, "Relevé de Notes");
                    List<string> headers = new List<string>();
                    headers.Add("Num "); headers.Add("Noms et Prénoms"); headers.Add("Sexe ");

                    List<TrimestreBE> listeTrimestre = noteBL.listerTousLesTrimestres();

                    for (int i = 0; i < listeTrimestre.Count; i++) { 
                        //on recherche les séquences du trimestre
                        List<SequenceBE> listSequence = noteBL.listerSequenceSuivantCritere("codeTrimestre = '"+listeTrimestre.ElementAt(i).codetrimestre+"'");
                        if (listSequence != null) {
                            for (int j = 0; j < listSequence.Count; j++) {
                                headers.Add(listSequence.ElementAt(j).codeseq);
                            }

                            headers.Add(listeTrimestre.ElementAt(i).codetrimestre);
                        }
                    }

                    //trier la liste avant d'envoyer à létat
                    if (listLigneFiche != null)
                    {
                        List<LigneFicheReleveNote> newList = listLigneFiche.OrderBy(o => o.nom).ToList();
                        listLigneFiche.Clear();
                        numero = 1;
                        foreach (LigneFicheReleveNote ligne in newList)
                        {
                            ligne.numero = numero++;
                            listLigneFiche.Add(ligne);
                        }

                    }


                    etat.etatFicheDeReleveNoteSimplifiee(listLigneFiche, headers, classe, txtAnnee.Text);
                   

                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                }
            }
            
         }

        private void cmdFermer_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        private void txtAnnee_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            if (txtAnnee.Text!="")
                txtAnneeScolaire.Text = ((Convert.ToInt32(txtAnnee.Text.ToString())) - 1).ToString() + "/" + txtAnnee.Text;
        }

        private void txtAnnee_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Utilitaires.IsTextAllowed(e.Text);
        }

        private void cmbClasse_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //charger la liste des matière de la classe pour l'année choisi
            ClasseBE classe = new ClasseBE();
            classe.codeClasse = cmbClasse.SelectedValue.ToString();
            
            List<MatiereBE> listMatieres = noteBL.ListeMatiereDuneClasse(classe, Convert.ToInt16(txtAnnee.Text));
            List<String> listCodeMatieres = noteBL.getListCodeMatiere2(listMatieres);
        }

          
   
    }

    //--------------------Classe conception LigneFichePresence-----------------------

    
}