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
using System.Globalization;
using System.Threading;
using Ecole.Utilitaire;

namespace Ecole.UI
{
    /// <summary>
    /// Interaction logic for SaisirSanctionEleve.xaml
    /// </summary>
    public partial class SaisirSanctionEleveUI : Window
    {
        GestionSanctionEleveBL sanctionEleveBL;
        List<SanctionnerBE> sanctionners;
        SanctionnerBE ancien_sanction;
        List<string> sanctions;
        List<string> sequences;
        List<DisciplineBE> disciplines;
        static string TYPE_ENREGISTRER = "enregistrer";
        static string TYPE_MODIFIER = "modifier";
        string etat;
        string typeValidation;
        int annee;
        EleveBE eleve;
        List<string> eleves;
        List<string> classes;

        public SaisirSanctionEleveUI()
        {
            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";
            Thread.CurrentThread.CurrentCulture = ci;
            
            InitializeComponent();
            sanctionEleveBL = new GestionSanctionEleveBL();
            sanctionners = new List<SanctionnerBE>();
            ancien_sanction = new SanctionnerBE();
            sanctions = new List<string>();
            sequences = new List<string>();
            disciplines = new List<DisciplineBE>();
            eleves = new List<string>();
            classes = new List<string>();
            eleve = new EleveBE();
            List<string> etats = new List<string>() { "NON JUSTIFIEE", "JUSTIFIEE"};
            cmbEtat.ItemsSource = etats;
            disciplines = sanctionEleveBL.listerToutDiscipline();
            if (disciplines != null)
                foreach (DisciplineBE d in disciplines)
                {
                    sanctions.Add(d.codeSanction + " - " + d.nomSanction);
                }
            sequences = sanctionEleveBL.listerValeurColonneSequence("codeseq");
            cmbSanction.ItemsSource = sanctions;
            cmbSanction.SelectedIndex = 0;
            cmbSequence.ItemsSource = sequences;
            cmbEtat.SelectedIndex = 1;
            annee = sanctionEleveBL.anneeEnCours();
            txtAnnee.Text = " / " + annee;
            txtAnneeScolaire.Text = (annee - 1).ToString(); 
            typeValidation = TYPE_ENREGISTRER;

            classes = sanctionEleveBL.listerValeurColonneClasse("codeclasse");
            cmbClasse.ItemsSource = classes;

        }

        private void txtMatricule_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                //recherche de sa classe
                string matricule = txtMatricule.Text;
                cmbClasse.Text = "";
                InscrireBE inscrire = new InscrireBE();
                inscrire.matricule = txtMatricule.Text;
                inscrire.annee = annee;
                inscrire = sanctionEleveBL.rechercherInscrire(inscrire);
                if (inscrire != null)
                {
                    cmbClasse.Text = inscrire.codeClasse;
                }
                eleves = new List<string>();
                string codeclasse = cmbClasse.Text;
                List<EleveBE> listeleves = new List<EleveBE>();
                listeleves = sanctionEleveBL.listerElevesDuneClasse(codeclasse, annee);
                if (listeleves != null)
                {
                    foreach (EleveBE el in listeleves)
                    {
                        eleves.Add(el.matricule + " - " + el.nom);
                    }
                }
                cmbEleve.ItemsSource = eleves;

                eleve = new EleveBE();
                eleve.matricule = txtMatricule.Text.ToString();
                eleve = sanctionEleveBL.rechercherEleve(eleve);
                if (eleve != null)
                {
                    cmbEleve.Text = eleve.matricule + " - " + eleve.nom;
                    sanctionners = sanctionEleveBL.listerSuivantCritereSanctionner("matricule =" + "'" + matricule + "' and annee =" + annee);
                    grdListe.ItemsSource = sanctionners;
                    grdListe.Items.Refresh();
                }
                else
                    MessageBox.Show("L'élève de matricule "+matricule+" n'est pas encore inscrit", "School brain:alerte", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void cmdEnregistrer_Click(object sender, RoutedEventArgs e)
        {
            if (validerFormulaire())
            {
                string matricule = txtMatricule.Text;
                string sanction = cmbSanction.Text.Split('-')[0].Trim();
                string sequence = cmbSequence.SelectedValue.ToString();
                int quantite = Convert.ToInt32(txtQuantite.Text);
                try
                {
                    etat = cmbEtat.SelectedValue.ToString();
                }
                catch (Exception) { etat = "NON JUSTIFIEE"; }
                DateTime date = DateTime.Today;

                SanctionnerBE sanctionner = new SanctionnerBE(sanction, matricule, annee, quantite, date, sequence,etat);
                if (typeValidation == TYPE_ENREGISTRER)
                {
                    if (sanctionEleveBL.enregistrerSanctionner(sanctionner))
                    {
                        MessageBox.Show("Enregistrement réussi", "School brain:Alerte");
                        sanctionners.Add(sanctionner);
                    }
                    else
                        MessageBox.Show("Enregistrement échoué", "School brain:Alerte");
                }
                else
                {
                    if (sanctionEleveBL.modifierSanctionner(ancien_sanction, sanctionner))
                    {
                        MessageBox.Show("Mise à jour effectuée", "School brain : Alerte");
                        sanctionners.Add(sanctionner);
                        sanctionners.Remove(ancien_sanction);
                    }
                    else
                        MessageBox.Show("Mise à jour échouée", "School brain : Alerte");
                }
                grdListe.Items.Refresh();
                txtQuantite.Text = "";

                cmbEleve.Text = "";
                txtMatricule.Text = "";
                cmbSanction.IsEnabled = true;
                cmbSequence.IsEnabled = true;
                cmbEtat.IsEnabled = true;
                typeValidation = TYPE_ENREGISTRER;
            }
            else
                MessageBox.Show("il y'a des champs vides, remplir tous les champs du formulaire", "School brain : alerte");
        }

        
        private void cmdAnnuler_Click(object sender, RoutedEventArgs e)
        {
            eleves.Clear();
            txtQuantite.Text = "";
            cmbSanction.SelectedIndex = 0;
            cmbSequence.Text = "";
            cmbEtat.SelectedIndex = 1;
            cmbClasse.Text = "";
            cmbEleve.Text = "";
            cmbSanction.IsEnabled = true;
            cmbSequence.IsEnabled = true;
            cmbEtat.IsEnabled = true;
            typeValidation = TYPE_ENREGISTRER;
        }

        private void cmdFermer_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cmdImprimer_Click(object sender, RoutedEventArgs e)
        {
            if (grdListe.ItemsSource != null)
            {
                string matricule = txtMatricule.Text;
                EleveBE eleve = new EleveBE();
                eleve.matricule = matricule;
                eleve = sanctionEleveBL.rechercherEleve(eleve);
                CreerEtat creerEtat = new CreerEtat("sanction-" + txtMatricule.Text + "" + DateTime.Today.ToShortDateString(), "historique des sanctions d'un élève");
                sanctionEleveBL.journaliser("Impression de l'historique des sanctions de "+eleve.nom);
                creerEtat.etatSanctionEleve(grdListe, eleve, annee);
            }
        }

        private bool validerFormulaire()
        {
            if (txtMatricule.Text == "" || cmbSequence.SelectedValue == null || cmbSanction.SelectedValue == null || txtQuantite.Text == "" || txtAnnee.Text == "")
                return false;
            else
                return true;
        }

        private void grdListe_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (grdListe.SelectedIndex >= 0)
            {
                DisciplineBE d = new DisciplineBE ();
                ancien_sanction = sanctionners.ElementAt(grdListe.SelectedIndex);
                d.codeSanction = ancien_sanction.codesanction;
                d = sanctionEleveBL.rechercherDiscipline(d);
                cmbSanction.SelectedIndex = cmbSanction.Items.IndexOf(d.codeSanction +" - "+d.nomSanction);
                cmbSequence.SelectedIndex = cmbSequence.Items.IndexOf(ancien_sanction.sequence);
                cmbEtat.SelectedIndex = cmbEtat.Items.IndexOf(ancien_sanction.etat);
                Console.WriteLine(cmbSequence.Items.IndexOf(ancien_sanction.etat)+" "+ancien_sanction.etat);
                etat = ancien_sanction.etat;
                txtQuantite.Text = ancien_sanction.quantité.ToString();

                cmbSanction.IsEnabled = false;
                cmbSequence.IsEnabled = false;
                cmbEtat.IsEnabled = false;
                typeValidation = TYPE_MODIFIER;
            }
        }

        private void grdListe_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                if (MessageBox.Show("Voulez vous supprimer?", "School : Confirmation", System.Windows.MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    if (grdListe.SelectedIndex != -1)
                    {
                        SanctionnerBE sanction = new SanctionnerBE();
                        sanction = sanctionners.ElementAt(grdListe.SelectedIndex);
                        sanctionEleveBL.supprimerSanctionner(sanction);
                        sanctionners.Remove(sanction);
                        grdListe.ItemsSource = sanctionners;
                        grdListe.Items.Refresh();
                        cmbSanction.Text = "";
                        cmbSequence.Text = "";
                        typeValidation = TYPE_ENREGISTRER;
                        cmbEtat.SelectedIndex = cmbEtat.Items.IndexOf("NON JUSTIFIEE");
                    }
                    else
                        MessageBox.Show("Aucune ligne sélectionnée", "School brain:Alerte");
                }
            }
        }

        private void txtQuantite_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Utilitaires.IsTextAllowed(e.Text);
        }

        private void cmbEtat_DropDownClosed(object sender, EventArgs e)
        {
            if (cmbEtat.SelectedValue != null && cmbEtat.SelectedValue.ToString() != "")
                etat = cmbEtat.SelectedValue.ToString();
        }

        private void cmbClasse_DropDownClosed(object sender, EventArgs e)
        {
            if (cmbClasse.Text != null && cmbClasse.Text != "")
            {
                //recherche des eleves inscrits  dans cette classe pour charger le combobox des eleves
                eleves = new List<string>();
                string codeclasse = cmbClasse.Text;
                List<EleveBE> listeleves = new List<EleveBE>();
                listeleves = sanctionEleveBL.listerElevesDuneClasse(codeclasse, annee);
                if (listeleves != null)
                {
                    foreach (EleveBE el in listeleves)
                    {
                        eleves.Add(el.matricule + " - " + el.nom);
                    }
                }
                cmbEleve.ItemsSource = eleves;
                txtMatricule.Text = "";
            }
        }

        private void cmbEleve_DropDownClosed(object sender, EventArgs e)
        {
            if (cmbEleve.Text != null && cmbEleve.Text != "")
            {
                string nommat = cmbEleve.Text;
                txtMatricule.Text = nommat.Split('-')[0].Trim();
                eleve.matricule = nommat.Split('-')[0].Trim();
                eleve = sanctionEleveBL.rechercherEleve(eleve);
                sanctionners = sanctionEleveBL.listerSuivantCritereSanctionner("matricule =" + "'" + eleve.matricule + "' and annee =" + annee);
                grdListe.ItemsSource = sanctionners;
                grdListe.Items.Refresh();
            }
        }

        private void txtAnneeScolaire_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                annee = Convert.ToInt32(txtAnneeScolaire.Text) + 1;
                txtAnnee.Text = " / " + annee;
            }
            catch (Exception)
            {
                MessageBox.Show("Le champ annee doit etre un nombre positif","School brain:Alerte");
            }
        }

        private void txtAnneeScolaire_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Utilitaires.IsTextAllowed(e.Text);
        }
    }
}
