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
    /// Interaction logic for MoyennesAnnuelles.xaml
    /// </summary>
    public partial class MoyennesAnnuellesUI : Window
    {
        static string TOUT = "Toutes";
        List<LigneEtatMoyenne> lignes;
        List<LigneEtatMoyenne> lignesMere; //utile pour eviter les acces a la BD chaque fois qu'on modifie le critere de recherche nom
        List<string> classes;
        GestionMoyennesAnnuellesBL moyenneBL;
        int annee;
        double moyenne;

        public MoyennesAnnuellesUI()
        {
            lignes = new List<LigneEtatMoyenne>();
            lignesMere = new List<LigneEtatMoyenne>();
            classes = new List<string>();
            moyenneBL = new GestionMoyennesAnnuellesBL();
            moyenne = 0;

            InitializeComponent();
            classes = moyenneBL.listerValeurColonneClasse("codeclasse");
            classes.Add(TOUT);
            cmbClasse.ItemsSource = classes;
            annee = moyenneBL.anneeEnCours();
            txtAnnee.Text = " / " + annee;
            txtAnneeScolaire.Text = (annee - 1).ToString();
            grdListe.DataContext = this;
        }

        private void cmdImprimer_Click(object sender, RoutedEventArgs e)
        {
            int COLONNE_NOM = 1;
            ClasseBE classe = new ClasseBE();
            string codeclasse = cmbClasse.SelectedValue.ToString();
            if (codeclasse == TOUT)
            {
                codeclasse = "%";
                classe = null;
            }
            else
            {
                classe.codeClasse = codeclasse;
                classe = moyenneBL.rechercherClasse(classe);
            }

            CreerEtat etat = new CreerEtat("moyenne-annuelle-" + annee, "Liste des moyennes annuelles");
            List<string> headers = new List<string>();
            for (int j = 0; j < grdListe.Columns.Count; j++)
            {
                if (j != COLONNE_NOM)
                    headers.Add(grdListe.Columns[j].Header.ToString().ToUpper());
                else
                    headers.Add("Nom".ToUpper());
            }

            moyenneBL.journaliser("Impression de la liste des moyennes annuelles");
            etat.etatMoyennes(lignes, headers, classe, annee.ToString(), null, null, moyenne);
        }

        private void cmdFermer_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cmbClasse_DropDownClosed(object sender, EventArgs e)
        {
            if (cmbClasse.SelectedValue != null && txtAnneeScolaire.Text != "")
            {
                List<ResultatAnnuelBE> resultats = new List<ResultatAnnuelBE>();
                string codeclasse = cmbClasse.SelectedValue.ToString();
                if (codeclasse == TOUT)
                    codeclasse = "%";
                try
                {
                    EleveBE eleve = new EleveBE();
                    resultats = moyenneBL.listerSuivantCritereResultatAnnuel(codeclasse, annee);
                    lignes.Clear();
                    lignesMere.Clear();
                    if (resultats != null && resultats.Count > 0)
                    {
                        foreach (ResultatAnnuelBE r in resultats)
                        {
                            eleve.matricule = r.matricule;
                            eleve = moyenneBL.rechercherEleve(eleve);
                            LigneEtatMoyenne l = new LigneEtatMoyenne(eleve.matricule, eleve.nom, r.moyenne, r.coef, r.point, r.rang, r.mention, r.remarque);
                            lignes.Add(l);
                            lignesMere.Add(l);
                        }
                        moyenne = resultats.ElementAt(0).moyenneclasse;
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

        private void radioOrdreMerite_Click(object sender, RoutedEventArgs e)
        {
            if (lignes != null)
            {
                List<LigneEtatMoyenne> liste = lignes.OrderByDescending(o => o.note).ToList();
                lignes.Clear();
                foreach (LigneEtatMoyenne l in liste)
                    lignes.Add(l);
                grdListe.ItemsSource = lignes;
                grdListe.Items.Refresh();
            }
        }

        private void NameSearchButton_Click(object sender, RoutedEventArgs e)
        {
        }

        private void radioOrdreAlphabetique_Click(object sender, RoutedEventArgs e)
        {
            if (lignes != null)
            {
                List<LigneEtatMoyenne> liste = lignes.OrderBy(o => o.nom).ToList();
                lignes.Clear();
                foreach (LigneEtatMoyenne l in liste)
                    lignes.Add(l);
                grdListe.ItemsSource = lignes;
                grdListe.Items.Refresh();
            }
        }

        private void NameSearchBox_KeyUp(object sender, KeyEventArgs e)
        {
            TextBox txt = sender as TextBox;
            List<LigneEtatMoyenne> liste = new List<LigneEtatMoyenne>();
            lignes.Clear();
            foreach (LigneEtatMoyenne l in lignesMere)
                lignes.Add(l);
            if (txt.Text != "")
            {
                liste = lignes.FindAll(c => c.nom.ToUpper().Contains(txt.Text.ToUpper()));
                lignes.Clear();
                foreach (LigneEtatMoyenne l in liste)
                    lignes.Add(l);
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
