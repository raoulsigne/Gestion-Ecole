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
using Ecole.ClasseMetiers;
using Ecole.BusinessLogic;
using Ecole.BusinessEntity;
using Ecole.Utilitaire;
using System.Globalization;
using System.Threading;

namespace Ecole.UI
{
    /// <summary>
    /// Interaction logic for ProgrammeClasseUI.xaml
    /// </summary>
    public partial class ProgrammeClasseUI : Window
    {
        GestionProgrammeBL programmeBL;
        List<ProgrammerBE> programmes;
        List<LigneProgramme> lignes;
        List<string> classes;
        int annee;

        public ProgrammeClasseUI()
        {
            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";
            Thread.CurrentThread.CurrentCulture = ci;
            
            programmeBL = new GestionProgrammeBL();
            lignes = new List<LigneProgramme>();
            programmes = new List<ProgrammerBE>();
            classes = new List<string>();

            InitializeComponent();

            classes = programmeBL.listerValeurColonneClasse("codeclasse");
            annee = programmeBL.anneeEnCours();
            txtAnnee.Text = " / " + annee;
            txtAnneeScolaire.Text = (annee - 1).ToString();
            radioAvecProf.IsChecked = true;
            cmbClasse.ItemsSource = classes;
            grdProgrammes.DataContext = this;
            grdProgrammes.ItemsSource = lignes;
        }

        private void AfficherProgramme()
        {
            LigneProgramme ligne;
            MatiereBE matiere;
            EnseignantBE enseignant;
            programmes = new List<ProgrammerBE>();
            lignes = new List<LigneProgramme>();

            if (validerFormulaire() != false)
            {
                programmes = programmeBL.listerSuivantCritereProgrammer("codeclasse = " + "'" + cmbClasse.SelectedValue.ToString() + "' AND annee = " + "'" + annee + "'");
                if (programmes != null)
                {
                    foreach (ProgrammerBE p in programmes)
                    {
                        matiere = new MatiereBE();
                        matiere.codeMat = p.codematiere;
                        matiere = programmeBL.rechercherMatiere(matiere);
                        enseignant = new EnseignantBE();
                        enseignant.codeProf = p.codeprof;
                        enseignant = programmeBL.rechercherEnseignant(enseignant);
                        ligne = new LigneProgramme(matiere.nomMat, p.coef, enseignant.nomProf);
                        lignes.Add(ligne);
                    }
                    if (radioAvecProf.IsChecked == true)
                        grdProgrammes.Columns[2].Visibility = Visibility.Visible;
                    else if (radioSansProf.IsChecked == true)
                        grdProgrammes.Columns[2].Visibility = Visibility.Hidden;

                    grdProgrammes.ItemsSource = lignes;
                    grdProgrammes.Items.Refresh();
                }
            }
        }

        private void cmdImprimer_Click(object sender, RoutedEventArgs e)
        {
            if (cmbClasse.SelectedValue != null && txtAnneeScolaire.Text != null)
            {
                string codeclasse = cmbClasse.SelectedValue.ToString();
                ClasseBE classe = new ClasseBE();
                classe.codeClasse = codeclasse;
                classe = programmeBL.rechercherClasse(classe);
                CreerEtat creerEtat = new CreerEtat("programme scolaire" + classe.codeClasse + DateTime.Today.ToShortDateString(), "Programme Scolaire ");
                programmeBL.journaliser("Impression du programme de " + classe.codeClasse + " en " + annee);
                creerEtat.etatProgrammeClasse(grdProgrammes, classe.codeClasse + " - " + classe.nomClasse, annee);
            }
            else
                MessageBox.Show("Veuillez renseigner tous les champs du formulaire","School brain : alerte", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void cmdFermer_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private bool validerFormulaire()
        {
            if (txtAnneeScolaire.Text != null & cmbClasse.SelectedValue != null)
                if (txtAnneeScolaire.Text != "" & cmbClasse.SelectedValue.ToString() != "")
                    return true;
                else
                    return false;
            else
                return false;
        }

        private void radioSansProf_Click(object sender, RoutedEventArgs e)
        {
            AfficherProgramme();
        }

        private void radioAvecProf_Click(object sender, RoutedEventArgs e)
        {
            AfficherProgramme();
        }

        private void cmbClasse_DropDownClosed(object sender, EventArgs e)
        {
            AfficherProgramme();
        }

        private void txtAnneeScolaire_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                annee = Convert.ToInt32(txtAnneeScolaire.Text) + 1;
                txtAnnee.Text = " / " + annee.ToString();
                AfficherProgramme();
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

    }
}
