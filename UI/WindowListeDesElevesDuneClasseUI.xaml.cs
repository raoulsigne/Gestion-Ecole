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
    /// Interaction logic for WindowListeDesElevesDuneClasseUI.xaml
    /// </summary>
    public partial class WindowListeDesElevesDuneClasseUI : Window
    {
        ListerEffectifClasseBL listerEffectifClasseBL;

        private String codeClasse; // contient le code de la classe des elèves affichés
        private int annee; // contient l'année de la classe des elèves affichés

        // Définition d'une liste 'ListeEleves' observable de 'Eleves'
        public ObservableCollection<EleveBE> ListeEleves { get; set; }

        public WindowListeDesElevesDuneClasseUI(String codeClasse, int annee)
        {
            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";
            Thread.CurrentThread.CurrentCulture = ci;

            InitializeComponent();

            this.Title = this.Title +" : "+ codeClasse;

            listerEffectifClasseBL = new ListerEffectifClasseBL();

            this.codeClasse = codeClasse;
            this.annee = annee;

           // A mettre pour que le binding avec le DataGrid fonctionne !
            grdListeEleves.DataContext = this;

            // Initialisation de la collection, qui va s'afficher dans la DataGrid :
            ListeEleves = new ObservableCollection<EleveBE>();
            //List<EleveBE> LEleveBE = null;
            //// on met la liste "LSerieBE" dans le DataGrid
            //RemplirDataGrid(LEleveBE);  

            List<InscrireBE> LInscrire = listerEffectifClasseBL.listeDesEffectifsDuneClassePourUneAnnee(codeClasse, Convert.ToString(annee));
            List<EleveBE> LEleve = new List<EleveBE>();
            if (LInscrire != null)
            {
                for (int i = 0; i < LInscrire.Count; i++)
                {
                    EleveBE eleve = new EleveBE();
                    eleve.matricule = LInscrire.ElementAt(i).matricule;
                    //LEleve.Add(listerEffectifClasseBL.rechercherEleve(eleve));
                    eleve = listerEffectifClasseBL.rechercherEleve(eleve);

                    eleve.numero = i + 1;

                    if (eleve.sexe != null && eleve.sexe.Count() != 0)
                    {
                        eleve.sexe = eleve.sexe.ElementAt(0).ToString().ToUpper();
                    }

                    //on recherche la catégorie de l'élève
                    AppartenirBE appartenir = new AppartenirBE();
                    List<AppartenirBE> LAppartenir = listerEffectifClasseBL.ListerAppartenirSuivantCritere("matricule = '" + eleve.matricule + "' AND annee = '" + annee + "'");

                    if (LAppartenir != null && LAppartenir.Count != 0)
                    {
                        eleve.categorie = LAppartenir.ElementAt(0).codeCatEleve;
                    }

                    ListeEleves.Add(eleve);
                }
            }

            grdListeEleves.ItemsSource = ListeEleves;
            lblTotal.Content = ListeEleves.Count.ToString();
        }

        private void cmdFermer_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void grdListeEleves_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                if (MessageBox.Show("Voulez-vous vraiment le supprimer ? ", "School : Confimation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    String matriculeEleve = ListeEleves.ElementAt(grdListeEleves.SelectedIndex).matricule;
                    
                    if(listerEffectifClasseBL.retireUnEleveDuneClasse(this.codeClasse, matriculeEleve, this.annee))
                        ListeEleves.RemoveAt(grdListeEleves.SelectedIndex);
        
                    grdListeEleves.ItemsSource = ListeEleves;
                    lblTotal.Content = ListeEleves.Count.ToString();
                }

                grdListeEleves.UnselectAll();
            }
        }

        private void cmdImprimer_Click(object sender, RoutedEventArgs e)
        {
            //CreerEtat etat = new CreerEtat("Liste Elèves "+codeClasse+" -" + DateTime.Today.ToShortDateString(), "Liste des Elèves de la "+codeClasse);
            CreerEtat etat = new CreerEtat("Liste Elèves " + codeClasse + " -" + DateTime.Today.ToShortDateString(), "Liste des Elèves");
            
            ClasseBE classe = new ClasseBE();
            classe.codeClasse = codeClasse;
            classe = listerEffectifClasseBL.rechercherClasse(classe);
            etat.etatListeEleveDuneClasse(ListeEleves, classe, annee);
        }
    }
}
