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
    public partial class FicheDePresenceUI : Window
    {
        ClasseBE classe;
        List<InscrireBE> listInscrits;
        GestionEleveDuneClasseBL eleveBL;
        List<string> classes;
        List<LigneFichePresence> listLigneFiche;

        public FicheDePresenceUI()
        {
            classe = new ClasseBE();
            eleveBL = new GestionEleveDuneClasseBL();
            classes = new List<string>();
            listInscrits = new List<InscrireBE>();
            listLigneFiche = new List<LigneFichePresence>();

            InitializeComponent();
            //Obtenir la liste des classes et les ajouter au comboBox des classes
            classes = eleveBL.listerValeursColonneClasse("codeclasse");
            cmbClasse.ItemsSource = classes;

            txtAnnee.Text = eleveBL.anneeEnCours().ToString();
            txtAnneeScolaire.Text = ((Convert.ToInt32(txtAnnee.Text.ToString())) -1).ToString() + "/" + txtAnnee.Text;
        }


               
        private void cmdImprimer_Click(object sender, RoutedEventArgs e)
        {
            int numero = 1;
            if (cmbClasse.SelectedValue != null && txtAnnee.Text != "")
            {
                
                listInscrits = new List<InscrireBE>();
                string codeclasse = cmbClasse.SelectedValue.ToString();
                classe.codeClasse = codeclasse;
                classe = eleveBL.rechercherClasse(classe);
               // listLigneFiche = new List<LigneFichePresence>();
                listLigneFiche.Clear();
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
                            LigneFichePresence ligne = new LigneFichePresence(numero++, eleve.nom, eleve.matricule);
                            listLigneFiche.Add(ligne);
                        }
                    }

                    if (radioJour.IsChecked == true)
                    {
                        CreerEtat etat = new CreerEtat("fiche presence- " + cmbClasse.Text + "_" + txtAnnee.Text, "Fiche de présences");
                        List<string> headers = new List<string>();
                        headers.Add("Num "); headers.Add("Noms et Prénoms"); headers.Add("Matricule ");
                        headers.Add("___h___\n___h___"); headers.Add("___h___\n___h___");
                        headers.Add("___h___\n___h___"); headers.Add("___h___\n___h___");
                        headers.Add("___h___\n___h___"); headers.Add("___h___\n___h___");
                        headers.Add("___h___\n___h___"); 
                        headers.Add("Total\nJournée");
                        //trier la liste avant d'envoyer à létat
                        if (listLigneFiche != null)
                        {
                            List<LigneFichePresence> newList = listLigneFiche.OrderBy(o => o.nom).ToList();
                            listLigneFiche.Clear();
                            numero = 1;
                            foreach (LigneFichePresence ligne in newList)
                            {
                                ligne.numero = numero++;
                                listLigneFiche.Add(ligne);
                            }

                        }
                        
                        etat.etatFicheDePresence(listLigneFiche, headers, classe, txtAnnee.Text, "jour");
                    }

                    if (radioHebdo.IsChecked == true)
                    {
                        CreerEtat etat = new CreerEtat("fiche presence- " + cmbClasse.Text + "_" + txtAnnee.Text, "Fiche de présences");
                        List<string> headers = new List<string>();
                        headers.Add("Num "); headers.Add("Noms et Prénoms"); headers.Add("Matricule ");
                        headers.Add("Lundi       "); headers.Add("Mardi ");
                        headers.Add("Mercredi  "); headers.Add("Jeudi ");
                        headers.Add("Vendredi  "); headers.Add("Samedi ");
                        headers.Add("Total ");
                        
                         //trier la liste avant d'envoyer à létat
                        if (listLigneFiche != null)
                    {
                        List<LigneFichePresence> newList = listLigneFiche.OrderBy(o => o.nom).ToList();
                        listLigneFiche.Clear();
                        numero = 1;
                        foreach (LigneFichePresence ligne in newList)
                        {
                            ligne.numero = numero++;
                            listLigneFiche.Add(ligne);
                        }

                    }

                        etat.etatFicheDePresence(listLigneFiche, headers, classe, txtAnnee.Text, "hebdo");
                    }

                }
                catch (Exception)
                {
                    MessageBox.Show("Format de champ Année academique non valide", "school brain:Alerte");
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