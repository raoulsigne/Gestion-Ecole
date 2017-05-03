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

using System.Globalization;
using System.Threading;

using Ecole.BusinessLogic;
using Ecole.BusinessEntity;
using Ecole.Utilitaire;

namespace Ecole.UI
{
    /// <summary>
    /// Interaction logic for WindowGetNbreEleveParClasseUI.xaml
    /// </summary>
    public partial class WindowGetNbreEleveParClasseUI : Window
    {
        GetListeEleveParClasseBL getNbreEleveParClasseBL;
        int anneeRecherche; //contiendra l'année qui a été utilisée pour la recherche
        private String oldClasse;

        private string classeChoisi; // sera utile pour la génération des états

        int annee;

        // Définition d'une liste 'ListeEffectifClasses' observable des 'Effectifs des classes'
        public ObservableCollection<EffectifClasseBE> ListeEffectifClasses { get; set; }

        

        public WindowGetNbreEleveParClasseUI()
        {
            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";
            Thread.CurrentThread.CurrentCulture = ci;

            InitializeComponent();

            classeChoisi = "";

            getNbreEleveParClasseBL = new GetListeEleveParClasseBL();

            // chargement de la liste des codes des classes dans le comboBox
            List<ClasseBE> LClasseBE = getNbreEleveParClasseBL.listerToutesLesClasses();
            cmbClasse.ItemsSource = getNbreEleveParClasseBL.getListCodeClasse(LClasseBE);

            //txtAnnee.Text = DateTime.Today.Date.Year.;

            // A mettre pour que le binding avec le DataGrid fonctionne !
            grdListeEffectif.DataContext = this;

            // Initialisation de la collection, qui va s'afficher dans la DataGrid :
            ListeEffectifClasses = new ObservableCollection<EffectifClasseBE>();

            // Initialisation de la collection, qui va s'afficher dans la DataGrid :
            //ListeEleves = new ObservableCollection<EleveBE>();
            //List<EleveBE> LEleveBE = null;
            //// on met la liste "LSerieBE" dans le DataGrid
            //RemplirDataGrid(LEleveBE); 

            ParametresBE param = getNbreEleveParClasseBL.getParametres();
            if (param != null)
            {
                annee = param.annee;

                txtAnnee.Text = Convert.ToString(param.annee);
                txtAnneeScolaire.Text = (param.annee - 1).ToString();
            }
            else
            {
                txtAnnee.Text = "";
                txtAnneeScolaire.Text = "";
            }

        }

        private void cmdValider_Click(object sender, RoutedEventArgs e)
        {
            //on vide le dataGrid
            ListeEffectifClasses.Clear();

            // recupération du champs "annee" saisit
            if ((cmbClasse.Text != null && txtAnneeScolaire.Text != null) && (cmbClasse.Text != "" && txtAnneeScolaire.Text != ""))
            {
                classeChoisi = cmbClasse.Text;

                anneeRecherche = Convert.ToInt16(txtAnnee.Text);

                List<InscrireBE> listInscrireBE = new List<InscrireBE>();
                oldClasse = cmbClasse.Text;
                if (cmbClasse.Text.Equals("<Toutes Les Classes>"))
                {
                    // on récupère la liste des inscrits pour toutes les classes et pour l'année choisit
                    List<ClasseBE> LClasse = getNbreEleveParClasseBL.listerToutesLesClasses();
                    for (int i = 0; i < LClasse.Count; i++){
                        EffectifClasseBE effectifClasse = new EffectifClasseBE();
                        effectifClasse.codeClasse = LClasse.ElementAt(i).codeClasse;
                        listInscrireBE = getNbreEleveParClasseBL.listeDesEffectifsDuneClassePourUneAnnee(effectifClasse.codeClasse, txtAnnee.Text);
                        if(listInscrireBE != null)
                            effectifClasse.effectif = listInscrireBE.Count;
                        else
                            effectifClasse.effectif = 0;
                        ListeEffectifClasses.Add(effectifClasse);
                    }
                    
                }
                else{
                     // on récupère la liste des inscrits pour la classe et l'année choisit
                    listInscrireBE = getNbreEleveParClasseBL.listeDesEffectifsDuneClassePourUneAnnee(cmbClasse.Text.ToString(), txtAnnee.Text);
                    if (listInscrireBE != null)
                        ListeEffectifClasses.Add(new EffectifClasseBE(cmbClasse.Text, listInscrireBE.Count));
                    else 
                        ListeEffectifClasses.Add(new EffectifClasseBE(cmbClasse.Text, 0));

                }

                grdListeEffectif.ItemsSource = ListeEffectifClasses; 
               
                //on valcul le Total
                int compteur = 0;
                foreach (EffectifClasseBE classe in ListeEffectifClasses) {
                    compteur += classe.effectif;
                }

                lblTotal.Content = compteur;

            }
            else MessageBox.Show("Tous les champs doivent pas être remplis !");
        }

        private void cmdFermer_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void grdListeEffectif_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (grdListeEffectif.SelectedIndex != -1)
            {
                String classe = ListeEffectifClasses.ElementAt(grdListeEffectif.SelectedIndex).codeClasse;
                WindowListeDesElevesDuneClasseUI listeDesElevesDuneClasseUI = new WindowListeDesElevesDuneClasseUI(classe, anneeRecherche);
                listeDesElevesDuneClasseUI.ShowDialog();

                // on recharge la datagrid (au cas où un elève a été retiré d'une classe)
                //on vide le dataGrid
                ListeEffectifClasses.Clear();

                anneeRecherche = Convert.ToInt16(txtAnnee.Text);

                List<InscrireBE> listInscrireBE = new List<InscrireBE>();
                if (oldClasse.Equals("<Toutes Les Classes>"))
                {
                    // on récupère la liste des inscrits pour toutes les classes et pour l'année choisit
                    List<ClasseBE> LClasse = getNbreEleveParClasseBL.listerToutesLesClasses();
                    for (int i = 0; i < LClasse.Count; i++)
                    {
                        EffectifClasseBE effectifClasse = new EffectifClasseBE();
                        effectifClasse.codeClasse = LClasse.ElementAt(i).codeClasse;
                        listInscrireBE = getNbreEleveParClasseBL.listeDesEffectifsDuneClassePourUneAnnee(effectifClasse.codeClasse, txtAnnee.Text);
                        if (listInscrireBE != null)
                            effectifClasse.effectif = listInscrireBE.Count;
                        else
                            effectifClasse.effectif = 0;
                        ListeEffectifClasses.Add(effectifClasse);
                    }

                }
                else
                {
                    // on récupère la liste des inscrits pour la classe et l'année choisit
                    listInscrireBE = getNbreEleveParClasseBL.listeDesEffectifsDuneClassePourUneAnnee(cmbClasse.Text.ToString(), txtAnnee.Text);
                    if (listInscrireBE != null)
                        ListeEffectifClasses.Add(new EffectifClasseBE(cmbClasse.Text, listInscrireBE.Count));
                    else
                        ListeEffectifClasses.Add(new EffectifClasseBE(cmbClasse.Text, 0));

                }

                grdListeEffectif.ItemsSource = ListeEffectifClasses;

                grdListeEffectif.UnselectAll();
            }

           
        }

        private void cmdAnnuler_Click(object sender, RoutedEventArgs e)
        {
            //txtAnnee.Text = "";
            ParametresBE param = getNbreEleveParClasseBL.getParametres();
            if (param != null)
            {
                annee = param.annee;

                txtAnnee.Text = Convert.ToString(param.annee);
                txtAnneeScolaire.Text = (param.annee - 1).ToString();
            }
            else
            {
                txtAnnee.Text = "";
                txtAnneeScolaire.Text = "";
            }

            cmbClasse.Text = null;

        }

        private void txtAnnee_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Utilitaires.IsTextAllowed(e.Text);
        }

        private void cmdImprimer_Click(object sender, RoutedEventArgs e)
        {
            CreerEtat etat;
            if(classeChoisi.Equals("<Toutes Les Classes>"))
                   etat = new CreerEtat("effectifs  -" + DateTime.Today.ToShortDateString(), "Liste des éffectifs des classes");
            else
                etat = new CreerEtat("effectifs " + classeChoisi + " -" + DateTime.Today.ToShortDateString(), "Liste des éffectifs de la "+classeChoisi);

            etat.obtenirEtat(grdListeEffectif);
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
