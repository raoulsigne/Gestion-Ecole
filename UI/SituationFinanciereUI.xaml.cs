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
    public partial class SituationFinanciereUI : Window
    {
        ClasseBE classe;
        List<InscrireBE> listInscrits;
        GestionEleveDuneClasseBL eleveBL;
        List<string> classes;
        List<LigneFicheDiscipline> listLigneFiche;

        DisciplineBE discipline;
        List<DisciplineBE> listDiscipline;
        GestionDisciplineBL disciplineBL;

        public SituationFinanciereUI()
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
            int numero = 1;
            List<EleveBE> listEleve = new List<EleveBE>();

            if (cmbClasse.SelectedValue != null && txtAnnee.Text != "")
            {
                listInscrits = new List<InscrireBE>();
                string codeclasse = cmbClasse.SelectedValue.ToString();
               // classe.codeClasse = codeclasse;
               // classe = eleveBL.rechercherClasse(classe);
               // listDiscipline = disciplineBL.listerToutDiscipline();
               // listLigneFiche = new List<LigneFicheDiscipline>();
                
                try
                {
                    int annee = Convert.ToInt32(txtAnnee.Text);
                    
                    listInscrits = eleveBL.listerSuivantCritereInscrire(codeclasse, annee);

                    if (listInscrits != null)
                    {
                        numero = 1;
                        foreach (InscrireBE i in listInscrits)
                        {
                            EleveBE eleve = new EleveBE();
                            eleve.matricule = i.matricule;
                            eleve = eleveBL.rechercherEleve(eleve);

                            listEleve.Add(eleve);
                        }
                    }

                   CreerEtat etat = new CreerEtat("Situation Finance- " + cmbClasse.Text + "_" + txtAnnee.Text, "Situation Financière des élèves");

                   //trier la liste avant d'envoyer à létat
                   if (listEleve != null)
                   {
                       List<EleveBE> newList = listEleve.OrderBy(o => o.nom).ToList();
                       listEleve.Clear();
                       numero = 1;
                       foreach (EleveBE ligne in newList)
                       {
                          listEleve.Add(ligne);
                       }

                   }
                   etat.etatSituationFinanciereDuneClasse(listEleve, codeclasse, Convert.ToInt32(txtAnnee.Text.ToString()));
                   

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