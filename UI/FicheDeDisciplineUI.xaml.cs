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
    public partial class FicheDeDisciplineUI : Window
    {
        ClasseBE classe;
        List<InscrireBE> listInscrits;
        GestionEleveDuneClasseBL eleveBL;
        List<string> classes;
        List<LigneFicheDiscipline> listLigneFiche;

        DisciplineBE discipline;
        List<DisciplineBE> listDiscipline;
        GestionDisciplineBL disciplineBL;

        public FicheDeDisciplineUI()
        {
            classe = new ClasseBE();
            
            eleveBL = new GestionEleveDuneClasseBL();
            classes = new List<string>();
            listInscrits = new List<InscrireBE>();
            listLigneFiche = new List<LigneFicheDiscipline>();

            discipline = new DisciplineBE();
            listDiscipline = new List<DisciplineBE>();
            disciplineBL = new GestionDisciplineBL();

            InitializeComponent();
            //Obtenir la liste des classes et les ajouter au comboBox des classes
            classes = eleveBL.listerValeursColonneClasse("codeclasse");
            cmbClasse.ItemsSource = classes;

            txtAnnee.Text = eleveBL.anneeEnCours().ToString();
            txtAnneeScolaire.Text = ((Convert.ToInt32(txtAnnee.Text.ToString())) - 1).ToString() + "/" + txtAnnee.Text;
        }


               
        private void cmdImprimer_Click(object sender, RoutedEventArgs e)
        {
            string type="jour";
            int numero = 1;
            string titre;
            if (cmbClasse.SelectedValue != null && txtAnnee.Text != "")
            {
                listInscrits = new List<InscrireBE>();
                string codeclasse = cmbClasse.SelectedValue.ToString();
                classe.codeClasse = codeclasse;
                classe = eleveBL.rechercherClasse(classe);
                listDiscipline = disciplineBL.listerToutDiscipline();
                listLigneFiche = new List<LigneFicheDiscipline>();
                
                try
                {
                    int annee = Convert.ToInt32(txtAnnee.Text);
                    EleveBE eleve = new EleveBE();
                    listInscrits = eleveBL.listerSuivantCritereInscrire(codeclasse, annee);

                    if (listInscrits != null)
                    {
                        numero = 1;
                        foreach (InscrireBE i in listInscrits)
                        {
                            eleve.matricule = i.matricule;
                            eleve = eleveBL.rechercherEleve(eleve);
                            LigneFicheDiscipline ligneFiche = new LigneFicheDiscipline(numero++, eleve.nom, eleve.matricule, listDiscipline);
                            listLigneFiche.Add(ligneFiche);
                        }
                    }

                    // definir le titre-----------------------------
                    if (radioHebdo.IsChecked == true)
                      titre = "Fiche de discipline Hebdomadaire";
                    else
                      titre = "Fiche de discipline Journalière";


                    CreerEtat etat = new CreerEtat("fiche discipline- " + cmbClasse.Text + "_" + txtAnnee.Text, titre);
                    List<string> headers = new List<string>();
                    headers.Add("Num "); headers.Add("Noms et Prénoms"); headers.Add("Matricule ");

                    if (listDiscipline != null)
                    {
                        foreach (DisciplineBE d in listDiscipline)
                        {
                            headers.Add(d.codeSanction + "(" + d.unite + ")");
                        }
                    }

                    
                   if (radioHebdo.IsChecked == true)
                   {
                        
                      // headers.Add("Total");
                       type = "hebdo";
                   }

                   //trier la liste avant d'envoyer à létat
                   if (listLigneFiche != null)
                   {
                       List<LigneFicheDiscipline> newList = listLigneFiche.OrderBy(o => o.nom).ToList();
                       listLigneFiche.Clear();
                       numero = 1;
                       foreach (LigneFicheDiscipline ligne in newList)
                       {
                           ligne.numero = numero++;
                           listLigneFiche.Add(ligne);
                       }

                   }
                   etat.etatFicheDeDiscipline(listLigneFiche, headers, classe, txtAnnee.Text,type);
                   

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

          
   
    }

    //--------------------Classe conception LigneFichePresence-----------------------

    
}