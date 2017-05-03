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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Ecole.UI;
using Ecole.BusinessEntity;
using Ecole.DataAccess;
using Ecole.BusinessLogic;
using Ecole.Utilitaire;

namespace Ecole
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<MenuItem> maListe = new List<MenuItem>();
        GestionGroupePrivilegeBL gestionGroupePrivilegeBL;
        String role;
        List<String> listPrivilege = new List<string>();
        DemarrageBL demarreur = new DemarrageBL();
        
        public MainWindow()
        {
            InitializeComponent();

            ParametresDA parametresDA = new ParametresDA();
            ParametresBE parametresBE = parametresDA.getParametre();

            Console.WriteLine(DateTime.Today.Hour.ToString("hh:mm:ss"));
            role = ConnexionUI.utilisateur.role;
            gestionGroupePrivilegeBL = new GestionGroupePrivilegeBL();
            lblSociete.Content = demarreur.lireValeurClefSection("appSettings", "societe").ToString().ToUpper();

            //charger le statutbar
            Label user = new Label();
            user.Content = "             Utilisateur connecté : " + Ecole.UI.ConnexionUI.utilisateur.login + " => " + Ecole.UI.ConnexionUI.utilisateur.nom.ToUpper();
            barEtat.Items.Add(user);

            Label espace = new Label();
            espace.Content = "                                                   ";
            barEtat.Items.Add(espace);

            Label jour = new Label();
            jour.Content = DateTime.Today.ToLongDateString();
            barEtat.Items.Add(jour);

            Label espace1 = new Label();
            espace1.Content = "                                                                                                         ";
            barEtat.Items.Add(espace1);

            Label heure = new Label();
            heure.Content = DateTime.Now.ToLongTimeString();
           // barEtat.Items.Add(heure);
             
        }

        public List<MenuItem> parcourMenu(Menu menu)
        {
            maListe.Clear();
            //for (int i = 0; i < menu.Items.Count; i++)
            foreach (MenuItem item in menu.Items)
            {

                if (item != null)
                {

                    maListe.Add(item);
                    parcourSousMenu(item);

                }
            }

            return maListe;

        }

        //----parourir un sous menus-----------------------------------------------------------
        public void parcourSousMenu(MenuItem sousMenu)
        {

            if (!(sousMenu is Separator))
            {
                foreach (MenuItem item in sousMenu.Items)
                {

                    if (item != null)
                    {
                        maListe.Add(item);
                        parcourSousMenu(item);

                    }
                }
            }

        }

        //--------obtenir la liste des sous menus------------------
        public List<MenuItem> listerFilsMenu(MenuItem sousMenu)
        {
            //List<MenuItem> maListe = new List<MenuItem>();
            //for (int i = 0; i < menu.Items.Count; i++)
            maListe.Clear();
            foreach (MenuItem item in sousMenu.Items)
            {

                if (item != null)
                {

                    maListe.Add(item);
                    parcourSousMenu(item);

                }
            }

            return maListe;

        }

        //-----------------------parcour suppression----------------------------

        public void activerLesMenus(Menu menu, List<String> listPrivilege)
        {
            int j = 0;
            for (j = 0; j < listPrivilege.Count; j++)
            {

                for (int i = 0; i < menu.Items.Count; i++)
                // foreach (MenuItem item in menu.Items)
                {
                    //MenuItem item = new MenuItem();
                    MenuItem item = menu.Items[i] as MenuItem;
                    // item = menu.Items[i] as MenuItem;
                    if (item != null)
                    {

                        //   if (item.Header.ToString() == listPrivilege.ElementAt(j))
                        if (item.Header.ToString() == listPrivilege.ElementAt(j))
                        {
                            //menuTest.Items.Remove(item);
                            // item.Visibility = Visibility.Hidden;
                            item.IsEnabled = true;
                            break;
                        }
                        else
                        {
                            List<String> listPrivilege1 = new List<string>();
                            listPrivilege1.Add(listPrivilege.ElementAt(j));
                            activerSousMenus(item, listPrivilege1);
                        }
                    }
                }

            }
        }

        //----parourir un sous menus-----------------------------------------------------------
        public void activerSousMenus(MenuItem sousMenu, List<String> listPrivilege)
        {
            int j = 0;
            for (j = 0; j < listPrivilege.Count; j++)
            {
                if (!(sousMenu is Separator))
                {
                    for (int i = 0; i < sousMenu.Items.Count; i++)
                    //foreach (MenuItem item in sousMenu.Items)
                    {

                        MenuItem item = sousMenu.Items[i] as MenuItem;
                        if (item != null)
                        {
                            //if (item.Header.ToString() == listPrivilege.ElementAt(j))
                            if (item.Header.ToString() == listPrivilege.ElementAt(j))
                            {
                                //menuTest.Items.Remove(item);
                                // item.Visibility = Visibility.Hidden;
                                item.IsEnabled = true;
                                break;
                            }
                            else
                            {
                                List<String> listPrivilege1 = new List<string>();
                                listPrivilege1.Add(listPrivilege.ElementAt(j));
                                activerSousMenus(item, listPrivilege1);
                            }

                        }
                    }
                }

            }

        }


        //----------------désactiver tous les menus----------------------------------------

        //----------------parcour menu--------------------------------------------

        public void desactiverTousLesMenus(Menu menu)
        {
            //for (int i = 0; i < menu.Items.Count; i++)
            foreach (MenuItem item in menu.Items)
            {

                if (item != null)
                {

                    item.IsEnabled = false;
                    desactiverSousMenus(item);

                }
            }

        }


        //----parourir un sous menus-----------------------------------------------------------
        public void desactiverSousMenus(MenuItem sousMenu)
        {

            if (!(sousMenu is Separator))
            {
                foreach (MenuItem item in sousMenu.Items)
                {

                    if (item != null)
                    {
                        item.IsEnabled = false;
                        desactiverSousMenus(item);

                    }
                }
            }

        }

        //--------obtenir la liste des sous menus------------------




        //-------------------------------------------------------------------------------
        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            desactiverTousLesMenus(menuTest);
            listPrivilege = gestionGroupePrivilegeBL.listerPrivilegeDunRole(role);
            activerLesMenus(menuTest, listPrivilege);
        }

        private void smnuQuitter_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Voulez vous vraiment quitter?", "School : Confirmation", System.Windows.MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }

        private void cmdDeconnexion_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Voulez vous vraiment vous déconnecter?", "School : Confirmation", System.Windows.MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                ConnexionUI fenetreConnexion = new ConnexionUI();
                fenetreConnexion.Show();
                this.Close();
            }
        }

        private void caisse_Click(object sender, RoutedEventArgs e)
        {
            Ops_caisse caisse = new Ops_caisse();
            caisse.ShowDialog();
        }

        private void cmdVenteArticle_Click(object sender, RoutedEventArgs e)
        {
            VenteArticleUI article = new VenteArticleUI();
            article.ShowDialog();
        }

        private void cmdAnnulerVenteArticle(object sender, RoutedEventArgs e)
        {
            AnnulationVenteUI fenetre = new AnnulationVenteUI();
            fenetre.ShowDialog();
        }

        private void cmdPayerPrestation_Click(object sender, RoutedEventArgs e)
        {
            Payer_Prestation prestation = new Payer_Prestation();
            prestation.ShowDialog();
        }

        private void cmdSortirArticle(object sender, RoutedEventArgs e)
        {
            SortieArticleUI fenetre = new SortieArticleUI();
            fenetre.ShowDialog();
        }

        private void statutFinancier(object sender, RoutedEventArgs e)
        {
            StatutFinancierUI prestation = new StatutFinancierUI();
            prestation.ShowDialog();
        }

        public void cmdAttribuerPrivilire_click(object sender, RoutedEventArgs e)
        {
            listeRole role = new listeRole();
            role.ShowDialog();
        }

        private void cmdCaisse_Click(object sender, RoutedEventArgs e)
        {
            Etat etat = new Etat();
            etat.ShowDialog();
        }

        private void cmdEtatVenteArticle(object sender, RoutedEventArgs e)
        {
            EtatVenteArticleUI etat = new EtatVenteArticleUI();
            etat.ShowDialog();
        }

        private void cmdEtatPrestation(object sender, RoutedEventArgs e)
        {
            EtatPrestationUI etat = new EtatPrestationUI();
            etat.ShowDialog();
        }

        private void cmdEtatVersement(object sender, RoutedEventArgs e)
        {
            EtatVersementUI etat = new EtatVersementUI();
            etat.ShowDialog();
        }

        private void cmdEtatOperation(object sender, RoutedEventArgs e)
        {
            EtatOperationUI etat = new EtatOperationUI();
            etat.ShowDialog();
        }

        private void cmdEtatOperation_Prestation(object sender, RoutedEventArgs e)
        {
            Etat_Operation_PrestationUI etat = new Etat_Operation_PrestationUI();
            etat.ShowDialog();
        }
        
        private void cmdHistoriqueCaisse_Click(object sender, RoutedEventArgs e)
        {
            HistoriqueCaisseUI etat = new HistoriqueCaisseUI();
            etat.ShowDialog();
        }

        private void cmdPays_Click(object sender, RoutedEventArgs e)
        {
            PaysUI pays = new PaysUI();
            pays.ShowDialog();
        }

        private void cmdRegion_Click(object sender, RoutedEventArgs e)
        {
            RegionUI region = new RegionUI();
            region.ShowDialog();
        }

        private void cmdDepartement_Click(object sender, RoutedEventArgs e)
        {
            DepartementUI dept = new DepartementUI();
            dept.ShowDialog();
        }
        
        private void cmdEleve_Click(object sender, RoutedEventArgs e)
        {
            EleveUI eleve = new EleveUI();
            eleve.ShowDialog();
        }

        private void cmdChangementClasse(object sender, RoutedEventArgs e)
        {
            ChangementClasseUI window = new ChangementClasseUI();
            window.ShowDialog();
        }


        private void cmdMontantTranche_Click(object sender, RoutedEventArgs e)
        {
            MontantTrancheUI m = new MontantTrancheUI();
            m.ShowDialog();
        }

        private void cmdAffecterMatiere_Click(object sender, RoutedEventArgs e)
        {
            AffecterMatiereUI a = new AffecterMatiereUI();
            a.ShowDialog();
        }

        private void cmdProgrammeClasse_Click(object sender, RoutedEventArgs e)
        {
            ProgrammeClasseUI p = new ProgrammeClasseUI();
            p.ShowDialog();
        }

        private void cmdAnonymat_Click(object sender, RoutedEventArgs e)
        {
            SaisieAnonymatUI saisieAnonymat = new SaisieAnonymatUI();
            saisieAnonymat.ShowDialog();
        }

        private void cmdNotesAnonymes_Click(object sender, RoutedEventArgs e)
        {
            SaisieNotesAvecAnonymatsUI notesanonymes = new SaisieNotesAvecAnonymatsUI();
            notesanonymes.ShowDialog();
        }

        private void cmdNotesSansAnonymat_Click(object sender, RoutedEventArgs e)
        {
            SaisieNotesSansAnonymatsUI notessimples = new SaisieNotesSansAnonymatsUI();
            notessimples.ShowDialog();
        }

        private void cmdGroupeutilisateur_Click(object sender, RoutedEventArgs e)
        {
            GroupeUtilisateursUI groupeUI = new GroupeUtilisateursUI();
            groupeUI.ShowDialog();
        }

        private void cmdDiscipline_Click(object sender, RoutedEventArgs e)
        {
            DisciplineUI discipline = new DisciplineUI();
            discipline.ShowDialog();
        }

        private void cmdSanctionClasse_Click(object sender, RoutedEventArgs e)
        {
            SaisirSanctionsClasseUI sanctionner = new SaisirSanctionsClasseUI();
            sanctionner.ShowDialog();
        }

        private void cmdSanctionnereleve_Click(object sender, RoutedEventArgs e)
        {
            SaisirSanctionEleveUI sanctionner = new SaisirSanctionEleveUI();
            sanctionner.ShowDialog();
        }

        private void cmdUtilisateur_Click(object sender, RoutedEventArgs e)
        {
            UtilisateursUI user = new UtilisateursUI();
            user.ShowDialog();
        }

        private void cmdModifierPassword_Click(object sender, RoutedEventArgs e)
        {
            ModifierPasswordUI passwordModificationUI = new ModifierPasswordUI();
            passwordModificationUI.ShowDialog();
        }

        private void cmdConseil_Click(object sender, RoutedEventArgs e)
        {
            JuryUI jury = new JuryUI();
            jury.ShowDialog();
        }

        /*************************** UI de FRANCIS ******************************************/
        private void cmdCycle_Click(object sender, RoutedEventArgs e)
        {
            WindowAddEditCycleUI AddEditCycleUI = new WindowAddEditCycleUI();
            AddEditCycleUI.ShowDialog();
        }

        private void cmdListeEleveDuneClasse_Click(object sender, RoutedEventArgs e)
        {
            //EleveDuneClasseUI fenetre = new EleveDuneClasseUI();
            WindowListeEleveParClasseUI fenetre = new UI.WindowListeEleveParClasseUI();
            fenetre.ShowDialog();
        }

        private void cmdNiveau_Click(object sender, RoutedEventArgs e)
        {
            WindowAddEditNiveauUI AddEditNiveauUI = new WindowAddEditNiveauUI();
            AddEditNiveauUI.ShowDialog();
        }

        private void cmdSerie_Click(object sender, RoutedEventArgs e)
        {
            WindowAddEDitSerieUI AddEditSerieUI = new WindowAddEDitSerieUI();
            AddEditSerieUI.ShowDialog();
        }

        private void cmdClasse_Click(object sender, RoutedEventArgs e)
        {
            WindowAddEditClasseUI AddEditClasseUI = new WindowAddEditClasseUI();
            AddEditClasseUI.ShowDialog();
        }

        private void cmdMagasin_Click(object sender, RoutedEventArgs e)
        {
            WindowAddEditMagasinUI AddEditMagasinUI = new WindowAddEditMagasinUI();
            AddEditMagasinUI.ShowDialog();
        }

        private void cmdEnregistrementArticle_Click(object sender, RoutedEventArgs e)
        {
            WindowAddEditArticleUI AddEditArticle = new WindowAddEditArticleUI();
            AddEditArticle.ShowDialog();
        }

        private void cmdApprovisionnement_Click(object sender, RoutedEventArgs e)
        {
            WindowApprovisionnementArticleUI approvisionnementArticleUI = new WindowApprovisionnementArticleUI();
            approvisionnementArticleUI.ShowDialog();
        }

        private void cmdCategorieArticle_Click(object sender, RoutedEventArgs e)
        {
            WindowAddEditCategorieArticleUI addEditCategorieArticleUI = new WindowAddEditCategorieArticleUI();
            addEditCategorieArticleUI.ShowDialog();
        }

        private void cmdAddSetArticle_Click(object sender, RoutedEventArgs e)
        {
            WindowAddSetArticleUI addSetArticleUI = new WindowAddSetArticleUI();
            addSetArticleUI.ShowDialog();
        }

        private void cmdEditSetArticle_Click(object sender, RoutedEventArgs e)
        {
            WindowEditSetArticleUI editSetArticleUI = new WindowEditSetArticleUI();
            editSetArticleUI.ShowDialog();
        }

        private void cmdEtatStock_Click(object sender, RoutedEventArgs e)
        {
            WindowGetEtatStockArticleUI getEtatStockArticleUI = new WindowGetEtatStockArticleUI();
            getEtatStockArticleUI.ShowDialog();
        }

        private void cmdTypeClasse_Click(object sender, RoutedEventArgs e)
        {
            WindowAddEditTypeClasseUI addEditTypeClasse = new WindowAddEditTypeClasseUI();
            addEditTypeClasse.ShowDialog();
        }

        private void cmdListeEleveParClasse_Click(object sender, RoutedEventArgs e)
        {
            WindowGetNbreEleveParClasseUI getNbreEleveParClasse = new WindowGetNbreEleveParClasseUI();
            getNbreEleveParClasse.ShowDialog();
        }

        private void cmdEnseignants_Click(object sender, RoutedEventArgs e)
        {
            WindowAddEditEnseignantUI windowAddEditEnseignantUI = new WindowAddEditEnseignantUI();
            windowAddEditEnseignantUI.ShowDialog();
        }

        private void cmdMatieres_Click(object sender, RoutedEventArgs e)
        {
            WindowAddEditMatiereUI windowAddEditMatiereUI = new WindowAddEditMatiereUI();
            windowAddEditMatiereUI.ShowDialog();
        }

        private void cmdTypeEvaluation_Click(object sender, RoutedEventArgs e)
        {
            WindowAddEditTypeEvaluationUI windowAddEditTypeEvaluationUI = new WindowAddEditTypeEvaluationUI();
            windowAddEditTypeEvaluationUI.ShowDialog();
        }

        private void cmdTitularisation_Click(object sender, RoutedEventArgs e)
        {
            WindowTitularisationEnseignantUI windowTitularisationEnseignantUI = new WindowTitularisationEnseignantUI();
            windowTitularisationEnseignantUI.ShowDialog();
        }

        private void cmdDefinirEvaluation_Click(object sender, RoutedEventArgs e)
        {
            WindowDefinirEvaluationMatiereUI windowDefinirEvaluationMatiereUI = new WindowDefinirEvaluationMatiereUI();
            windowDefinirEvaluationMatiereUI.ShowDialog();
        }

        private void cmdCategorieEleve_Click(object sender, RoutedEventArgs e)
        {
            WindowAddEditCategorieEleveUI windowAddEditCategorieEleveUI = new WindowAddEditCategorieEleveUI();
            windowAddEditCategorieEleveUI.ShowDialog();
        }

        private void cmdPrestation_Click(object sender, RoutedEventArgs e)
        {
            WindowAddEditPrestationUI windowAddEditPrestationUI = new WindowAddEditPrestationUI();
            windowAddEditPrestationUI.ShowDialog();
        }

        private void cmdInscription_Click(object sender, RoutedEventArgs e)
        {
            WindowInscriptionEleveUI windowInscriptionEleveUI = new WindowInscriptionEleveUI();
            windowInscriptionEleveUI.ShowDialog();

        }

        private void cmdTranche_Click(object sender, RoutedEventArgs e)
        {
            WindowCreerModifierTrancheUI windowCreerModifierTrancheUI = new WindowCreerModifierTrancheUI();
            windowCreerModifierTrancheUI.ShowDialog();
        }

        private void cmdGroupeMatiere_Click(object sender, RoutedEventArgs e)
        {
            WindowAddEditGroupeMatiereUI windowAddEditGroupeMatiereUI = new WindowAddEditGroupeMatiereUI();
            windowAddEditGroupeMatiereUI.ShowDialog();
        }

        private void cmdTrimestre_Click(object sender, RoutedEventArgs e)
        {
            WindowAddEditTrimestreUI windowAddEditTrimestreUI = new WindowAddEditTrimestreUI();
            windowAddEditTrimestreUI.ShowDialog();
        }

        private void cmdSequence_Click(object sender, RoutedEventArgs e)
        {
            WindowAddEditSequenceUI windowAddEditSequenceUI = new WindowAddEditSequenceUI();
            windowAddEditSequenceUI.ShowDialog();
        }

        private void cmdParametre_Click(object sender, RoutedEventArgs e)
        {
            WindowAddEditParametresUI windowAddEditParametresUI = new WindowAddEditParametresUI();
            windowAddEditParametresUI.ShowDialog();
        }

        private void cmdResultatsSequentiels_Click(object sender, RoutedEventArgs e)
        {
            WindowGenererResultatsSequentielsUI windowGenererResultatsSequentielsUI = new WindowGenererResultatsSequentielsUI();
            windowGenererResultatsSequentielsUI.ShowDialog();
        }

        private void cmdTrimestriels_Click(object sender, RoutedEventArgs e)
        {
            WindowGenererResultatsTrimestrielsUI windowGenererResultatsTrimestrielsUI = new WindowGenererResultatsTrimestrielsUI();
            windowGenererResultatsTrimestrielsUI.ShowDialog();
        }

        private void cmdResultatsAnnuels_Click(object sender, RoutedEventArgs e)
        {
            WindowGenererResultatsAnnuelsUI windowGenererResultatsAnnuelsUI = new WindowGenererResultatsAnnuelsUI();
            windowGenererResultatsAnnuelsUI.ShowDialog();
        }

        private void cmdRecalculResultatEleve_Click(object sender, RoutedEventArgs e)
        {
            RecalculerResultatDunEleveUI recalculerResultatDunEleveUI = new RecalculerResultatDunEleveUI();
            recalculerResultatDunEleveUI.ShowDialog();
        }

        private void cmdInscription2_Click(object sender, RoutedEventArgs e)
        {
            WindowInscriptionEleve2UI windowInscriptionEleve2UI = new WindowInscriptionEleve2UI();
            windowInscriptionEleve2UI.ShowDialog();
        }

        private void cmdMention_Click(object sender, RoutedEventArgs e)
        {
            WindowDefinirMentionUI windowDefinirMentionUI = new WindowDefinirMentionUI();
            windowDefinirMentionUI.ShowDialog();
        }

        private void cmdMoyennesAnnuelles_Click(object sender, RoutedEventArgs e)
        {
            MoyennesAnnuellesUI moyenneAnnuelle = new MoyennesAnnuellesUI();
            moyenneAnnuelle.ShowDialog();
        }

        private void cmdListeEleveUneClasse_Click(object sender, RoutedEventArgs e)
        {
            EleveDuneClasseUI eleveDuneClasse = new EleveDuneClasseUI();
            eleveDuneClasse.ShowDialog();
        }

        /*public void cmdCertificatScolariteEleve(object sender, RoutedEventArgs e)
        {
            CertificatScolariteUI certificat = new CertificatScolariteUI();
            certificat.ShowDialog();
        }*/

        public void cmdCertificatScolariteClasse(object sender, RoutedEventArgs e)
        {
            CertificatScolariteUI certificat = new CertificatScolariteUI();
            certificat.ShowDialog();
        }

        private void cmdEtatSanctionClasse_Click(object sender, RoutedEventArgs e)
        {
            WindowEtatDesSanctionDuneClasseUI windowEtatDesSanctionDuneClasseUI = new WindowEtatDesSanctionDuneClasseUI();
            windowEtatDesSanctionDuneClasseUI.ShowDialog();
        }

        private void cmdRecapSequentiel_Click(object sender, RoutedEventArgs e)
        {
            RecapitulatifSequentielUI recap = new RecapitulatifSequentielUI();
            recap.ShowDialog();
        }

        private void cmdRecapTrimestriel_Click(object sender, RoutedEventArgs e)
        {
            RecapitulatifTrimestrielUI recap = new RecapitulatifTrimestrielUI();
            recap.ShowDialog();
        }

        private void cmdRecapAnnuel_Click(object sender, RoutedEventArgs e)
        {
            RecapitulatifAnnuelUI recap = new RecapitulatifAnnuelUI();
            recap.ShowDialog();
        }

        private void cmdBulletinTrimestriel_Click(object sender, RoutedEventArgs e)
        {
            BulletinTrimestrielUI bulletinsTrimestrielUI  = new BulletinTrimestrielUI();
            bulletinsTrimestrielUI.ShowDialog();
        }

        private void cmdBulletinAnnuel_Click(object sender, RoutedEventArgs e)
        {
            BulletinAnnuelUI windowGenererBulletinsAnnuelUI = new BulletinAnnuelUI();
            windowGenererBulletinsAnnuelUI.ShowDialog();
        }

        private void cmdBulletinSequentiel_Click(object sender, RoutedEventArgs e)
        {
            BulletinSequentielUI windowGenererBulletinsSequentielUI = new BulletinSequentielUI();
            windowGenererBulletinsSequentielUI.ShowDialog();
        }

        private void cmdJournalisation(object sender, RoutedEventArgs e)
        {
            JournalUI journal = new JournalUI();
            journal.ShowDialog();
        }

        private void cmdFicheReportNotes(object sender, RoutedEventArgs e)
        {
            FicheDeReleveDeNotesUI reportNote = new FicheDeReleveDeNotesUI();
            reportNote.ShowDialog();
        }

        private void cmdFicheReportNotes2(object sender, RoutedEventArgs e)
        {
            FicheDeReleveDeNotes2UI reportNote = new FicheDeReleveDeNotes2UI();
            reportNote.ShowDialog();
        }

        private void cmdFichePresence(object sender, RoutedEventArgs e)
        {
            FicheDePresenceUI presence = new FicheDePresenceUI();
            presence.ShowDialog();
        }

        private void cmbEditInfosDemarrage_Click(object sender, RoutedEventArgs e)
        {
            WindowEditInfosDemarrageUI windowEditInfosDemarrageUI = new WindowEditInfosDemarrageUI();
            windowEditInfosDemarrageUI.ShowDialog();
        }

        private void cmdFicheDiscipline(object sender, RoutedEventArgs e)
        {
            FicheDeDisciplineUI discipline = new FicheDeDisciplineUI();
            discipline.ShowDialog();
        }

        private void cmdMoyenneSequentielle_Click(object sender, RoutedEventArgs e)
        {
            MoyenneSequentielleUI etatMoyenne = new MoyenneSequentielleUI();
            etatMoyenne.ShowDialog();
        }

        private void cmdMoyenneTrimestrielle_Click(object sender, RoutedEventArgs e)
        {
            MoyenneTrimestrielleUI etatMoyenne = new MoyenneTrimestrielleUI();
            etatMoyenne.ShowDialog();
        }

        private void cmdProfilScolaire_Click(object sender, RoutedEventArgs e)
        {
            ProfilScolaireUI profil = new ProfilScolaireUI();
            profil.ShowDialog();
        }

        private void cmdBilanSequentiel_Click(object sender, RoutedEventArgs e)
        {
            BilanSequentielUI window = new BilanSequentielUI();
            window.ShowDialog();
        }

        private void cmdSituationFinanciereClasse_Click(object sender, RoutedEventArgs e)
        {
            InsolvablesUI insolvable = new InsolvablesUI();
            insolvable.ShowDialog(); 
        }

        private void cmbAppreciationMoyenne_Click(object sender, RoutedEventArgs e)
        {
            WindowSaisieDesAppreciationsDesMoyennesUI windowSaisieDesAppreciationsDesMoyennesUI = new WindowSaisieDesAppreciationsDesMoyennesUI();
            windowSaisieDesAppreciationsDesMoyennesUI.ShowDialog();
        }

        private void cmbAppreciationResultat_Click(object sender, RoutedEventArgs e)
        {
            WindowSaisieDesAppreciationsDesResultatsUI windowSaisieDesAppreciationsDesResultatsUI = new WindowSaisieDesAppreciationsDesResultatsUI();
            windowSaisieDesAppreciationsDesResultatsUI.ShowDialog();
        }

        private void cmdComparaisonMatiere(object sender, RoutedEventArgs e)
        {
            StatComparatifMatiereClasseUI etat = new StatComparatifMatiereClasseUI();
            etat.ShowDialog();
        }

        private void cmdComparaisonMatierePeriodique(object sender, RoutedEventArgs e)
        {
            StatVariationNiveauParMatiereUI etat = new StatVariationNiveauParMatiereUI();
            etat.ShowDialog();
        }

        private void cmdComparaisonResultatClasse(object sender, RoutedEventArgs e)
        {
            StatComparatifResultatClasseUI etat = new StatComparatifResultatClasseUI();
            etat.ShowDialog();
        }

        private void cmdEquipe(object sender, RoutedEventArgs e)
        {
            teamUI etat = new teamUI();
            etat.ShowDialog();
        }

        private void cmdComparaisonResultatNiveau(object sender, RoutedEventArgs e)
        {
            StatComparaisonResultatNiveauUI etat = new StatComparaisonResultatNiveauUI();
            etat.ShowDialog();
        }

        private void cmdProgressionClasse(object sender, RoutedEventArgs e)
        {
            StatProgressionClasseUI etat = new StatProgressionClasseUI();
            etat.ShowDialog();
        }

        private void cmdGenererStatClasse(object sender, RoutedEventArgs e)
        {
            WindowGenererStatistiqueDuneClasseUI etat = new WindowGenererStatistiqueDuneClasseUI();
            etat.ShowDialog();
        }

        private void cmdGenererStatNiveau(object sender, RoutedEventArgs e)
        {
            WindowGenererStatistiqueDunNiveauUI etat = new WindowGenererStatistiqueDunNiveauUI();
            etat.ShowDialog();
        }

        private void cmdBilanFinancier(object sender, RoutedEventArgs e)
        {
            BilanFinancierUI etat = new BilanFinancierUI();
            etat.ShowDialog();
        }

        private void cmdNotificationSMS_Email(object sender, RoutedEventArgs e)
        {
            GestionDesNotificationsUI fenetre = new GestionDesNotificationsUI();
            fenetre.ShowDialog();
        }

        private void cmdConfigureNotificationSMS_Email(object sender, RoutedEventArgs e)
        {
            ConfigurerServeurNotificationUI fenetre = new ConfigurerServeurNotificationUI();
            fenetre.ShowDialog();
        }
        
        private void cmdAssistanceCreationClasse(object sender, RoutedEventArgs e)
        {
            AssistanceCreationClasseUI fenetre = new AssistanceCreationClasseUI();
            fenetre.ShowDialog();
        }

        private void smnuJournalCaisse_Click(object sender, RoutedEventArgs e)
        {
            JournalCaisseUI journalwindow = new JournalCaisseUI();
            journalwindow.ShowDialog();
        }

        //private void btnFrancais_Checked(object sender, RoutedEventArgs e)
        //{
        //    //btnAnglais.IsChecked = false;
        //    ChoixLangue.fichierLangue = "francais.xml";
        //    ParametresDA parametresDA = new ParametresDA();
        //    ParametresBE parametresBE = parametresDA.getParametre();
        //    parametresBE.FICHIER_LANGUE = ChoixLangue.fichierLangue;
        //    parametresDA.modifier(parametresBE);
        //}

        //private void btnAnglais_Checked(object sender, RoutedEventArgs e)
        //{
        //    //btnFrancais.IsChecked = false;
        //    ChoixLangue.fichierLangue = "anglais.xml";
        //    ParametresDA parametresDA = new ParametresDA();
        //    ParametresBE parametresBE = parametresDA.getParametre();
        //    parametresBE.FICHIER_LANGUE = ChoixLangue.fichierLangue;
        //    parametresDA.modifier(parametresBE);
        //}
       /************************** FIN ***********************************************************************************************/
    }
}
