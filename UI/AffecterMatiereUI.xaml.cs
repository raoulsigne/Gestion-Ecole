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
using System.Text.RegularExpressions;
using Ecole.ClasseConception;

namespace Ecole.UI
{
    /// <summary>
    /// Interaction logic for AffecterMatiereUI.xaml
    /// </summary>
    public partial class AffecterMatiereUI : Window
    {
        GestionAffectationMatiereBL affecterMatiereBL;
        List<LigneProgrammer> lignes;
        List<EnseignantBE> enseignants;
        List<string> classes;
        List<string> matieres;
        List<string> professeurs;
        List<string> annees;
        List<string> groupes;
        List<ProgrammerBE> programmers;
        ProgrammerBE programmer;
        LigneProgrammer old_ligne;
        //si on doubleclick,, il faut d'abord valider la ligne chargee avant de doubleclicker encore, on utilise un etat doubleclick pour verifier cela
        bool doubleclick,modification;
        int annee;

        public AffecterMatiereUI()
        {
            affecterMatiereBL = new GestionAffectationMatiereBL();
            lignes = new List<LigneProgrammer>();
            enseignants = new List<EnseignantBE>();
            classes = new List<string>();
            matieres = new List<string>();
            professeurs = new List<string>();
            groupes = new List<string>();
            annees = new List<string>() { "2011", "2012", "2013", "2014", "2015", "2016", "2017", "2018", "2019", "2020" };
            doubleclick = true;
            modification = false;
            programmers = new List<ProgrammerBE>();
            programmer = new ProgrammerBE();
            old_ligne = new LigneProgrammer();

            InitializeComponent();

            lblAnneeAncien.IsEnabled = false;
            cmbAnneeAncien.IsEnabled = false;
            classes = affecterMatiereBL.listerValeurColonneClasse("codeclasse");
            enseignants = affecterMatiereBL.listerToutEnseignants();
            if (enseignants != null)
                foreach (EnseignantBE e in enseignants)
                    professeurs.Add(e.codeProf + " - " + e.nomProf);
            else
                MessageBox.Show("shcool brain:Information","vous devez d'abord enregistrer les enseignants");
            groupes = affecterMatiereBL.listerValeurColonneGroupe("codegroupe");

            annee = affecterMatiereBL.anneeEnCours();
            txtAnnee.Text = " / " + annee;
            txtAnneeScolaire.Text = (annee - 1).ToString();
            cmbAnneeAncien.ItemsSource = annees;
            cmbClasse.ItemsSource = classes;
            cmbClasseModele.ItemsSource = classes;
            cmbEnseignant.ItemsSource = professeurs;
            cmbMatiere.ItemsSource = matieres;
            cmbGroupe.ItemsSource = groupes;
            //programmers = affecterMatiereBL.listerToutProgrammer();
            grdProgrammers.DataContext = this;
            grdProgrammers.ItemsSource = lignes;
        }

        private void cmbClasse_DropDownClosed(object sender, EventArgs e)
        {
            matieres.Clear();
            lignes.Clear();
            cmbMatiere.ItemsSource = matieres;
            cmbMatiere.Items.Refresh();
            if (checkChargerAncien.IsChecked == true)
                chargerAncienProgramme();
            else
            {
                if (cmbClasse.SelectedValue != null & txtAnneeScolaire.Text != null)
                {
                    if (cmbClasse.SelectedValue.ToString() != "" & txtAnneeScolaire.Text != "")
                    {
                        programmers = new List<ProgrammerBE>();
                        programmers = affecterMatiereBL.listerSuivantCritereProgrammer("codeclasse = " + "'" + cmbClasse.SelectedValue.ToString() + "' AND ANNEE = " + "'" + annee + "'");
                        foreach (ProgrammerBE p in programmers)
                            lignes.Add(new LigneProgrammer(p.codematiere, p.coef, p.codegroupe, p.codeprof, enseignants.Find(en => en.codeProf.Contains(p.codeprof)).nomProf));
                        grdProgrammers.ItemsSource = lignes;
                        grdProgrammers.Items.Refresh();

                        List<string> codematieres = new List<string>();
                        codematieres = affecterMatiereBL.listerValeurColonneMatiere("codemat");
                        if (programmers != null && programmers.Count > 0)
                        {
                            foreach (ProgrammerBE p in programmers)
                                if (codematieres.Contains(p.codematiere))
                                    codematieres.Remove(p.codematiere);
                            foreach (string ch in codematieres)
                                matieres.Add(ch);
                            cmbMatiere.ItemsSource = matieres;
                        }
                        else
                        {
                            foreach (string ch in codematieres)
                                matieres.Add(ch);
                            cmbMatiere.ItemsSource = matieres;
                        }
                        cmbMatiere.Items.Refresh();
                    }
                }
            }
        }

        private void cmbAnnee_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cmbClasse_DropDownClosed(sender, e);
        }

        private void cmbAnneeAncien_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            chargerAncienProgramme();
        }

        private void chargerAncienProgramme()
        {
            lignes.Clear();
            if (cmbClasse.SelectedValue != null & txtAnneeScolaire.Text != null & cmbAnneeAncien.SelectedValue != null)
            {
                if (cmbClasse.SelectedValue.ToString() != "" & txtAnneeScolaire.Text != "" & cmbAnneeAncien.SelectedValue.ToString() != "")
                {
                    programmers = new List<ProgrammerBE>();
                    programmers = affecterMatiereBL.listerSuivantCritereProgrammer("codeclasse = " + "'" + cmbClasse.SelectedValue.ToString() + "' AND ANNEE = " + "'" + cmbAnneeAncien.SelectedValue.ToString() + "'");
                    foreach (ProgrammerBE p in programmers)
                        lignes.Add(new LigneProgrammer(p.codematiere, p.coef, p.codegroupe, p.codeprof, enseignants.Find(en => en.codeProf.Contains(p.codeprof)).nomProf));
                    grdProgrammers.ItemsSource = lignes;
                    grdProgrammers.Items.Refresh();
                }
            }
        }

        private void checkChargerAncien_Checked(object sender, RoutedEventArgs e)
        {
            lblAnneeAncien.IsEnabled = true;
            cmbAnneeAncien.IsEnabled = true;
            chargerAncienProgramme();
            //cmbAnneeAncien.IsEnabled = false;
            //cmbAnnee.IsEnabled = false;
            //cmbClasse.IsEnabled = false;
        }

        private void checkChargerAncien_Unchecked(object sender, RoutedEventArgs e)
        {
            lblAnneeAncien.IsEnabled = false;
            cmbAnneeAncien.Text = "";
            cmbAnneeAncien.IsEnabled = false;
            programmers = new List<ProgrammerBE>();
            lignes = new List<LigneProgrammer>();
            cmbClasse_DropDownClosed(sender, e);
        }

        private void cmdAnuler_Click(object sender, RoutedEventArgs e)
        {
            cmbEnseignant.Text = "";
            cmbMatiere.Text = "";
            txtCoeficient.Text = "";
            cmbAnneeAncien.Text = "";
            cmbAnneeAncien.IsEnabled = true;
            txtAnneeScolaire.IsEnabled = true;
            cmbClasse.IsEnabled = true;
            cmbClasse.Text = "";
            cmbClasseModele.Text = "";
            checkChargerAncien.IsChecked = false;
            programmers = new List<ProgrammerBE>();
            lignes = new List<LigneProgrammer>();
            grdProgrammers.ItemsSource = lignes;
            grdProgrammers.Items.Refresh();
        }

        private void cmdAjouter_Click(object sender, RoutedEventArgs e)
        {
            ProgrammerBE nouveau_p = new ProgrammerBE();
            LigneProgrammer l = new LigneProgrammer();
            if (validerFormulaire() != false)
            {
                string codeprof = cmbEnseignant.SelectedValue.ToString().Split('-')[0].Trim();
                nouveau_p = new ProgrammerBE(cmbClasse.SelectedValue.ToString(), cmbMatiere.SelectedValue.ToString(), codeprof,
                    Convert.ToInt32(txtCoeficient.Text), annee, cmbGroupe.SelectedValue.ToString());
                l = new LigneProgrammer(cmbMatiere.SelectedValue.ToString(), Convert.ToInt32(txtCoeficient.Text), cmbGroupe.SelectedValue.ToString(),
                    codeprof , enseignants.Find(en => en.codeProf.Contains(codeprof)).nomProf);

                //si c'est une modification d'une ligne du tableau
                if (doubleclick)
                {
                    programmers.Remove(programmer);
                    lignes.Remove(old_ligne);
                }

                //mise a jour du tableau
                programmers.Add(nouveau_p);
                lignes.Add(l);
                grdProgrammers.ItemsSource = lignes;
                grdProgrammers.Items.Refresh();

                //mise a jour de la liste des matieres
                matieres.RemoveAt(cmbMatiere.SelectedIndex);
                cmbMatiere.ItemsSource = matieres;
                cmbMatiere.Items.Refresh();

                //initialisation des champs
                cmbEnseignant.Text = "";
                cmbMatiere.Text = "";
                txtCoeficient.Text = "";
                cmbGroupe.Text = "";
                doubleclick = false;
            }
            else
                MessageBox.Show("Formulaire non valide, veuillez remplir tous les champs", "School brain : alerte",MessageBoxButton.OK,MessageBoxImage.Exclamation);
        }

        private void cmdValider_Click(object sender, RoutedEventArgs e)
        {
            bool b = true;
            string annee_ancien = "", annee_encours = "";
            if (cmbAnneeAncien.SelectedValue != null)
                annee_ancien = cmbAnneeAncien.SelectedValue.ToString();
            annee_encours = txtAnneeScolaire.Text;
            ProgrammerBE program = new ProgrammerBE();

            if (programmers.Count > 0)
            {
                foreach (ProgrammerBE p in programmers)
                {
                    p.annee = annee;
                    b = b & affecterMatiereBL.enregistrerProgrammer(p);
                }
                if (b == true)
                    MessageBox.Show("Affectation réussi","School brain : alerte", MessageBoxButton.OK, MessageBoxImage.Information);
                else
                    MessageBox.Show("Affectation échouée", "School brain : alerte", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                modification = false;

                programmers = new List<ProgrammerBE>();
                lignes = new List<LigneProgrammer>();
                grdProgrammers.ItemsSource = lignes;
                if (checkChargerAncien.IsChecked == true)
                {
                    cmbAnneeAncien.IsEnabled = true;
                    txtAnneeScolaire.IsEnabled = true;
                    cmbClasse.IsEnabled = true;
                    checkChargerAncien.IsChecked = false;
                }
                cmbClasse.Text = "";
            }
            else
                MessageBox.Show("Liste encore vide, veuillez affecter les matieres aux enseignants","School brain : Message alerte",MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }

        private void cmdFermer_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void grdProgrammers_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (grdProgrammers.SelectedIndex >= 0)
            {
                programmer = programmers.ElementAt(grdProgrammers.SelectedIndex);
                old_ligne = lignes.ElementAt(grdProgrammers.SelectedIndex);

                txtCoeficient.Text = Convert.ToString(old_ligne.coeficient);
                cmbEnseignant.SelectedIndex = cmbEnseignant.Items.IndexOf(old_ligne.codeprof+" - "+old_ligne.nomprof);
                cmbGroupe.SelectedIndex = cmbGroupe.Items.IndexOf(old_ligne.codegroupe);
                //positionnement du drapeau 
                doubleclick = true;

                //verifier si la liste des matieres n'est pas vide, dans ce cas il faut ajouter a la liste
                if (matieres.Contains(old_ligne.matiere))
                    cmbMatiere.SelectedIndex = cmbMatiere.Items.IndexOf(old_ligne.matiere);
                else
                {
                    if (modification == false)
                        matieres.Add(old_ligne.matiere);
                    else
                    {
                        if (matieres.Count > 0)
                            matieres.Remove(matieres.ElementAt(0));
                        matieres.Add(old_ligne.matiere);
                    }
                    cmbMatiere.ItemsSource = matieres;
                    cmbMatiere.Items.Refresh();
                    cmbMatiere.Text = (old_ligne.matiere);
                    modification = true;
                }
            }
        }

        private void grdProgrammers_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                if (MessageBox.Show("Voulez vous supprimer?", "School : Confirmation", System.Windows.MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    if (grdProgrammers.SelectedIndex != -1)
                    {
                        ProgrammerBE p = new ProgrammerBE();
                        LigneProgrammer l = new LigneProgrammer();
                        p = programmers.ElementAt(grdProgrammers.SelectedIndex);
                        l = lignes.ElementAt(grdProgrammers.SelectedIndex);
                        programmers.Remove(p);
                        lignes.Remove(l);
                        affecterMatiereBL.supprimerProgrammer(p);
                        grdProgrammers.ItemsSource = lignes;
                        grdProgrammers.Items.Refresh();
                        if (!matieres.Contains(l.matiere))
                            matieres.Add(p.codematiere);
                        cmbMatiere.ItemsSource = matieres;
                        cmbMatiere.Items.Refresh();
                    }
                    else
                        MessageBox.Show("Aucune ligne sélectionnée", "School brain : alerte", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
        }

        private bool validerFormulaire()
        {
            bool b = true;
            if (cmbClasse.SelectedValue == null || txtAnneeScolaire.Text == null || cmbMatiere.SelectedValue == null || cmbEnseignant.SelectedValue == null || cmbGroupe.SelectedValue == null || txtCoeficient.Text == null)
                b = false;
            else if (cmbClasse.SelectedValue.ToString() == "" || txtAnneeScolaire.Text == "" || cmbMatiere.SelectedValue.ToString() == "" || cmbEnseignant.SelectedValue.ToString() == "" || cmbGroupe.SelectedValue.ToString() == "" || txtCoeficient.Text == "")
                b = false;
            return b;
        }

        
        private void txtCoeficient_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Utilitaires.IsTextAllowed(e.Text);
        }

        private void txtAnneeScolaire_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                annee = Convert.ToInt32(txtAnneeScolaire.Text) + 1;
                txtAnnee.Text = " / " + annee.ToString();

            }
            catch (Exception)
            {
                MessageBox.Show("L'annee doit etre un nombre positif", "School brain : alerte", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void txtAnneeScolaire_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Utilitaires.IsTextAllowed(e.Text);
        }

        private void cmbClasseModele_DropDownClosed(object sender, EventArgs e)
        {
            lignes.Clear();
            if (cmbClasseModele.SelectedValue != null & txtAnneeScolaire.Text != null)
            {
                if (cmbClasseModele.SelectedValue.ToString() != "" & txtAnneeScolaire.Text != "")
                {
                    List<ProgrammerBE> listes = new List<ProgrammerBE>();
                    programmers = new List<ProgrammerBE>();
                    listes = affecterMatiereBL.listerSuivantCritereProgrammer("codeclasse = " + "'" + cmbClasseModele.SelectedValue.ToString() + "' AND ANNEE = " + "'" + annee + "'");
                    foreach(ProgrammerBE p in listes)
                        programmers.Add(new ProgrammerBE(cmbClasse.Text, p.codematiere, p.codeprof, p.coef, annee, p.codegroupe));
                    foreach (ProgrammerBE p in programmers)
                        lignes.Add(new LigneProgrammer(p.codematiere, p.coef, p.codegroupe, p.codeprof, enseignants.Find(en => en.codeProf.Contains(p.codeprof)).nomProf));
                    grdProgrammers.ItemsSource = lignes;
                    grdProgrammers.Items.Refresh();
                }
            }
        }


    }
}
