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
using System.Globalization;
using System.Threading;

namespace Ecole.UI
{
    /// <summary>
    /// Interaction logic for EleveDuneClasseUI.xaml
    /// </summary>
    public partial class EleveDuneClasseUI : Window
    {
        ClasseBE classe;
        List<EleveBE> eleves;
        List<InscrireBE> inscrits;
        GestionEleveDuneClasseBL eleveBL;
        List<LigneEleve> lignes, lignesMere;
        List<string> classes;
        int annee;

        public EleveDuneClasseUI()
        {
            classe = new ClasseBE();
            eleves = new List<EleveBE>();
            eleveBL = new GestionEleveDuneClasseBL();
            lignes = new List<LigneEleve>();
            lignesMere = new List<LigneEleve>();
            classes = new List<string>();
            inscrits = new List<InscrireBE>();
            
            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";
            Thread.CurrentThread.CurrentCulture = ci;

            InitializeComponent();

            classes = eleveBL.listerValeursColonneClasse("codeclasse");
            cmbClasse.ItemsSource = classes;
            annee = eleveBL.anneeEnCours();
            txtAnnee.Text = " / " + annee;
            txtAnneeScolaire.Text = (annee - 1).ToString();
        }


        private void cmdImprimer_Click(object sender, RoutedEventArgs e)
        {
            string codeclasse = cmbClasse.SelectedValue.ToString();
            int COLONNE_NOM = 2;
            CreerEtat etat = new CreerEtat("Liste-Classe-" + annee, "Liste des élèves");
            List<string> headers = new List<string>();
            for (int j = 0; j < grdListe.Columns.Count; j++)
            {
                if (j != COLONNE_NOM)
                    headers.Add(grdListe.Columns[j].Header.ToString().ToUpper());
                else
                    headers.Add("Nom".ToUpper());
            }
            eleveBL.journaliser("Impression de la liste des élèves de " + codeclasse);
            etat.etatListeEleve(lignes, headers, classe, annee.ToString());
        }

        private void cmdFermer_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cmbClasse_DropDownClosed(object sender, EventArgs e)
        {
            if (cmbClasse.SelectedValue != null && txtAnnee.Text != "")
            {
                inscrits = new List<InscrireBE>();
                string codeclasse = cmbClasse.SelectedValue.ToString();
                classe.codeClasse = codeclasse;
                classe = eleveBL.rechercherClasse(classe);
                try
                {
                    EleveBE eleve = new EleveBE();
                    inscrits = eleveBL.listerSuivantCritereInscrire(codeclasse, annee);
                    lignes.Clear();
                    lignesMere.Clear();
                    if (inscrits != null)
                    {
                        int numero = 1;
                        foreach (InscrireBE i in inscrits)
                        {
                            eleve.matricule = i.matricule;
                            eleve = eleveBL.rechercherEleve(eleve);
                            LigneEleve l = new LigneEleve(numero++,eleve.matricule, eleve.nom, eleve.dateNaissance.ToShortDateString(), eleve.telephone, eleve.telParent, eleve.adresse);
                            lignes.Add(l);
                        }
                    }
                    //tri de la liste
                    List<LigneEleve> liste = lignes.OrderBy(o => o.nom).ToList();
                    lignes.Clear();
                    int j = 1;
                    foreach (LigneEleve l in liste)
                    {
                        l.numero = j++;
                        lignes.Add(l);
                        lignesMere.Add(l);
                    }

                    grdListe.ItemsSource = lignes;
                    grdListe.Items.Refresh();
                }
                catch (Exception)
                {
                    MessageBox.Show("Format de champ Année academique non valide", "school brain:Alerte");
                }
            }
        }

        private void NameSearchBox_KeyUp(object sender, KeyEventArgs e)
        {
            TextBox txt = sender as TextBox;
            List<LigneEleve> liste = new List<LigneEleve>();
            lignes.Clear();
            foreach (LigneEleve l in lignesMere)
                lignes.Add(l);
            if (txt.Text != "")
            {
                liste = lignes.FindAll(c => c.nom.ToUpper().Contains(txt.Text.ToUpper()));
                lignes.Clear();
                int i = 1;
                foreach (LigneEleve l in liste)
                {
                    l.numero = i++;
                    lignes.Add(l);
                }
            }
            grdListe.ItemsSource = lignes;
            grdListe.Items.Refresh();
        }

        private void txtAnneeScolaire_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                annee = Convert.ToInt32(txtAnneeScolaire.Text) + 1;
                txtAnnee.Text = " / " + annee.ToString();
                cmbClasse_DropDownClosed(sender, e);

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
