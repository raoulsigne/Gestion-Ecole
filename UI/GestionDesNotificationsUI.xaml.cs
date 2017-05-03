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
using Ecole.BusinessLogic;
using Ecole.BusinessEntity;
using System.Threading.Tasks;
using System.Threading;
using System.ComponentModel;
using Ecole.Utilitaire;

namespace Ecole.UI
{
    /// <summary>
    /// Interaction logic for GestionDesNotificationsUI.xaml
    /// </summary>
    public partial class GestionDesNotificationsUI : Window
    {
        public const string CONVOCATION_PARENT = "PARENT";
        public const string CONVOCATION_PERSONNEL = "PERSONNEL";
        public const string ENVOI_RESULTAT = "RESULTAT";
        public const string REUNION = "REUNION";
        public const string REUNION_ELEVE = "REUNION_ELEVE";
        public const string PERSONNALISE = "PERSONNALISE";
        public static string NUMERO = "NUMERO";
        public static string EMAIL = "EMAIL";

        GestionNotificationBL notificationBL;
        string action;
        ConvocationParentUC convocationParent;
        ConvocationProfUC convocationPersonnel;
        EnvoyerResultatUC envoiResultat;
        ReunionUC reunion;
        ReunionEleveUC reunionEleve;
        EnvoiPersonnaliseUC envoirPersonnalise;
        RapportNotificationUC rapportUC;
        ParametresBE parametre;

        int nb = 0;
        int nbreussi = 0;
        List<Adresse> echecs;

        public GestionDesNotificationsUI()
        {
            InitializeComponent();
            action = "";
            notificationBL = new GestionNotificationBL();
            convocationParent = new ConvocationParentUC();
            convocationPersonnel = new ConvocationProfUC();
            envoiResultat = new EnvoyerResultatUC();
            reunion = new ReunionUC();
            reunionEleve = new ReunionEleveUC();
            parametre = notificationBL.getParametre();
        }

        private void cmdconvocationParent_Click(object sender, RoutedEventArgs e)
        {
            convocationParent = new ConvocationParentUC();
            panelForm.Children.Clear();
            panelForm.Children.Add(convocationParent);
            action = CONVOCATION_PARENT;
        }

        private void cmdconvocationEnseignant_Click(object sender, RoutedEventArgs e)
        {
            convocationPersonnel = new ConvocationProfUC();
            panelForm.Children.Clear();
            panelForm.Children.Add(convocationPersonnel);
            action = CONVOCATION_PERSONNEL;
        }

        private void cmdEnvoiResultat_Click(object sender, RoutedEventArgs e)
        {
            envoiResultat = new EnvoyerResultatUC();
            panelForm.Children.Clear();
            panelForm.Children.Add(envoiResultat);
            action = ENVOI_RESULTAT;
        }

        private void cmdReunion_Click(object sender, RoutedEventArgs e)
        {
            reunion = new ReunionUC();
            panelForm.Children.Clear();
            panelForm.Children.Add(reunion);
            action = REUNION;
        }

        private void cmbReunionEleve_Click(object sender, RoutedEventArgs e)
        {
            reunionEleve = new ReunionEleveUC();
            panelForm.Children.Clear();
            panelForm.Children.Add(reunionEleve);
            action = REUNION_ELEVE;
        }

        private void cmdPersonnalise_Click(object sender, RoutedEventArgs e)
        {
            envoirPersonnalise = new EnvoiPersonnaliseUC();
            panelForm.Children.Clear();
            panelForm.Children.Add(envoirPersonnalise);
            action = PERSONNALISE;
        }

        private void cmdFermer_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdEnvoyer_Click(object sender, RoutedEventArgs ev)
        {
            bool b = true;

            switch (action)
            {
                case CONVOCATION_PARENT:
                    if (convocationParent.lblStatut.Content.ToString() != GestionNotificationBL.INFORMATIONS_VALIDEES)
                        b = false;
                    break;
                case CONVOCATION_PERSONNEL:
                    if (convocationPersonnel.lblStatut.Content.ToString() != GestionNotificationBL.INFORMATIONS_VALIDEES)
                        b = false;
                    break;
                case REUNION:
                    if (reunion.lblStatut.Content.ToString() != GestionNotificationBL.INFORMATIONS_VALIDEES)
                        b = false;
                    break;
                case REUNION_ELEVE:
                    if (reunionEleve.lblStatut.Content.ToString() != GestionNotificationBL.INFORMATIONS_VALIDEES)
                        b = false;
                    break;
                case ENVOI_RESULTAT:
                    if (envoiResultat.lblStatut.Content.ToString() != GestionNotificationBL.INFORMATIONS_VALIDEES)
                        b = false;
                    break;
                default:
                    break;
            }

            if (!Tools.HasConnection())
            {
                MessageBox.Show("Vous avez besoin de connexion internet pour envoyer les messages");
                b = false;
            }
            else if (b == false)
                MessageBox.Show("Veuillez après avoir renseigné les informations cliquer sur VALIDER avant de cliquer sur ENVOYER", "School brain:Information",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            else
            {
                BackgroundWorker bwr = new BackgroundWorker();
                ProgressDialogResult result = ProgressDialog.Execute(this, "Loading data...", (bw, we) =>
                {
                    var thread = new Thread(() =>
                    {
                        actionToExcecuteOnClick();
                    });
                    thread.Start();
                    while (thread.IsAlive) Console.WriteLine("running");
                    Console.WriteLine("end");
                });

                rapportUC = new RapportNotificationUC(nb, nbreussi, echecs);
                panelForm.Children.Clear();
                panelForm.Children.Add(rapportUC);
            }
        }

        private void actionToExcecuteOnClick()
        {
            int annee = notificationBL.anneeEnCours();
            List<EleveBE> eleves = new List<EleveBE>();
            nb = 0;
            nbreussi = 0;
            echecs = new List<Adresse>();
            Dictionary<string, string> mappage = new Dictionary<string, string>();
            Dictionary<string, string> mappageNumero = new Dictionary<string, string>();

            switch (action)
            {
                case CONVOCATION_PARENT:

                    EleveBE eleve = new EleveBE();
                    eleve.matricule = convocationParent.matricule;
                    eleve = notificationBL.rechercherEleve(eleve);
                    nb = 1;
                    nbreussi = 0;

                    string[] adresse = new string[2] { eleve.matricule, eleve.telParent };
                    Dictionary<string, int> result = new Dictionary<string, int>();

                    if (Tools.format_number(eleve.telParent) == "")
                        echecs.Add(new Adresse(eleve.telParent, eleve.nom));
                    else
                    {
                        nbreussi++;
                        result = notificationBL.envoiSMS(adresse, convocationParent.message);
                    }

                    break;

                case CONVOCATION_PERSONNEL:
                    EnseignantBE prof = new EnseignantBE();
                    prof.codeProf = convocationPersonnel.matricule;
                    prof = notificationBL.rechercherEnseignant(prof);
                    nb = 1;
                    nbreussi = 0;

                    adresse = new string[2] { prof.codeProf, prof.tel };
                    result = new Dictionary<string, int>();

                    if (Tools.format_number(prof.tel) == "")
                        echecs.Add(new Adresse(prof.tel, prof.nomProf));
                    else
                    {
                        nbreussi++;
                        result = notificationBL.envoiSMS(adresse, convocationPersonnel.message);
                    }

                    break;

                case ENVOI_RESULTAT:
                    List<string> sequences = notificationBL.listerValeurColonneSequence("codeseq");
                    List<string> trimestres = notificationBL.listerValeurColonneTrimestre("codetrimestre");
                    List<string[]> destinataireSMS = new List<string[]>();
                    mappage = new Dictionary<string, string>();
                    mappageNumero = new Dictionary<string, string>();

                    if (envoiResultat.classe == EnvoyerResultatUC.TOUTE)
                        eleves = notificationBL.listeEleveDuneAnnee(annee);
                    else
                        eleves = notificationBL.listeEleveDuneClasse(envoiResultat.classe, annee);
                    string message = "";
                    if (eleves != null)
                    {
                        nb = eleves.Count;
                        nbreussi = 0;

                        if (sequences.Contains(envoiResultat.periode))
                        {
                            ResultatBE resultat;
                            foreach (EleveBE e in eleves)
                            {
                                resultat = new ResultatBE();
                                resultat = notificationBL.resultatSequentielleEleve(e.matricule, annee, envoiResultat.periode);

                                if (Tools.format_number(e.telParent) == "")
                                    echecs.Add(new Adresse(e.telParent, e.nom));
                                else
                                {
                                    nbreussi++;
                                    message = "L'élève " + e.nom + " a obtenu la note de " + resultat.moyenne
                                    + " pour le compte de la Séquence " + envoiResultat.periode
                                    + " Rang " + resultat.rang
                                    + " Moyenne générale " + resultat.moyenneclasse;

                                    destinataireSMS.Add(new string[3] { e.matricule, e.telParent, message });
                                    mappage.Add(e.matricule, e.nom);
                                    mappageNumero.Add(e.matricule, e.telParent);   
                                }
                            }
                        }

                        if (trimestres.Contains(envoiResultat.periode))
                        {
                            ResultatTrimestrielBE resultat;
                            foreach (EleveBE e in eleves)
                            {
                                resultat = new ResultatTrimestrielBE();
                                resultat = notificationBL.resultatTrimestrielEleve(e.matricule, annee, envoiResultat.periode);

                                if (Tools.format_number(e.telParent) == "")
                                    echecs.Add(new Adresse(e.telParent, e.nom));
                                else
                                {
                                    nbreussi++;
                                    message = "L'élève " + e.nom + " a obtenu la note de " + resultat.moyenne
                                        + " pour le compte du Trimestre " + envoiResultat.periode
                                        + " Rang " + resultat.rang 
                                        + " Moyenne générale " + resultat.moyenneclasse;

                                    destinataireSMS.Add(new string[3] { e.matricule, e.telParent, message });
                                    mappage.Add(e.matricule, e.nom);
                                    mappageNumero.Add(e.matricule, e.telParent);
                                }
                            }
                        }

                        if (EnvoyerResultatUC.ANNUEL == envoiResultat.periode)
                        {
                            ResultatAnnuelBE resultat;
                            foreach (EleveBE e in eleves)
                            {
                                resultat = new ResultatAnnuelBE();
                                resultat = notificationBL.resultatAnnuelDunEleve(e.matricule, annee);

                                if (Tools.format_number(e.telParent) == "")
                                    echecs.Add(new Adresse(e.telParent, e.nom));
                                else
                                {
                                    nbreussi++;
                                    message = "L'élève " + e.nom + " a obtenu la note annuelle de " + resultat.moyenne
                                        + " pour le compte de l'année académique : " + (annee - 1) + "/" + annee
                                        + " Rang " + resultat.rang
                                        + " Moyenne générale " + resultat.moyenneclasse;

                                    destinataireSMS.Add(new string[3] { e.matricule, e.telParent, message });
                                    mappage.Add(e.matricule, e.nom);
                                    mappageNumero.Add(e.matricule, e.telParent);
                                }
                            }
                        }
                    }

                    result = new Dictionary<string, int>();
                    result = notificationBL.envoiSMS(destinataireSMS);
                    //if (result != null)
                    //{
                    //    foreach (KeyValuePair<string, string> element in mappage)
                    //    {
                    //        if (result[element.Key] != 0)
                    //            echecs.Add(new Adresse(mappageNumero[element.Key], element.Value));
                    //    }
                    //    nbreussi = nb - echecs.Count;
                    //}
                    //else
                    //{
                    //    foreach (KeyValuePair<string, string> element in mappage)
                    //    {
                    //        echecs.Add(new Adresse(mappageNumero[element.Key], element.Value));
                    //    }
                    //}

                    break;

                case REUNION:
                    destinataireSMS = new List<string[]>();
                    mappage = new Dictionary<string, string>();
                    mappageNumero = new Dictionary<string, string>();
                    nbreussi = 0;
                    nb = 0;

                    if (ReunionUC.ENSEIGNANT == reunion.concerne)
                    {
                        List<EnseignantBE> liste = new List<EnseignantBE>();
                        liste = notificationBL.listerToutEnseignants();
                        if (liste != null)
                        {
                            nb = liste.Count;
                            foreach (EnseignantBE e in liste)
                            {
                                if (Tools.format_number(e.tel) == "")
                                    echecs.Add(new Adresse(e.tel, e.nomProf));
                                else
                                {
                                    nbreussi++;
                                    destinataireSMS.Add(new string[2] { e.codeProf, e.tel });
                                    mappage.Add(e.codeProf, e.nomProf);
                                    mappageNumero.Add(e.codeProf, e.tel);
                                }
                            }
                        }
                    }
                    else if (ReunionUC.PARENT == reunion.concerne)
                    {
                        List<EleveBE> list = new List<EleveBE>();
                        list = notificationBL.listeEleveDuneAnnee(annee);
                        if (list != null)
                        {
                            nb = list.Count;
                            foreach (EleveBE e in list)
                            {
                                if (Tools.format_number(e.telParent) == "")
                                    echecs.Add(new Adresse(e.telParent, e.nom));
                                else
                                {
                                    nbreussi++;
                                    destinataireSMS.Add(new string[2] { e.matricule, e.telParent });
                                    mappage.Add(e.matricule, e.nom);
                                    mappageNumero.Add(e.matricule, e.telParent);
                                }
                            }
                        }
                    }
                    else if (ReunionUC.MIXTE == reunion.concerne)
                    {
                        List<EnseignantBE> liste = new List<EnseignantBE>();
                        liste = notificationBL.listerToutEnseignants();
                        if (liste != null)
                        {
                            nb = liste.Count;
                            foreach (EnseignantBE e in liste)
                            {
                                if (Tools.format_number(e.tel) == "")
                                    echecs.Add(new Adresse(e.tel, e.nomProf));
                                else
                                {
                                    nbreussi++;
                                    destinataireSMS.Add(new string[2] { e.codeProf, e.tel });
                                    mappage.Add(e.codeProf, e.nomProf);
                                    mappageNumero.Add(e.codeProf, e.tel);
                                }
                            }
                        }
                        List<EleveBE> list = new List<EleveBE>();
                        list = notificationBL.listeEleveDuneAnnee(annee);
                        if (list != null)
                        {
                            nb += list.Count;
                            foreach (EleveBE e in list)
                            {
                                if (Tools.format_number(e.telParent) == "")
                                    echecs.Add(new Adresse(e.telParent, e.nom));
                                else
                                {
                                    nbreussi++;
                                    destinataireSMS.Add(new string[2] { e.matricule, e.telParent });
                                    mappage.Add(e.matricule, e.nom);
                                    mappageNumero.Add(e.matricule, e.telParent);
                                }
                            }
                        }
                    }

                    message = reunion.message;
                    result = new Dictionary<string, int>();
                    result = notificationBL.envoiSMS(destinataireSMS, message);

                    //if (result != null)
                    //{
                    //    foreach (KeyValuePair<string, string> element in mappage)
                    //    {
                    //        if (result[element.Key] != 0)
                    //            echecs.Add(new Adresse(mappageNumero[element.Key], element.Value));
                    //    }
                    //    nbreussi = nb - echecs.Count;
                    //}
                    //else
                    //    foreach (KeyValuePair<string, string> element in mappage)
                    //    {
                    //        echecs.Add(new Adresse(mappageNumero[element.Key], element.Value));
                    //    }

                    break;

                case REUNION_ELEVE:
                    destinataireSMS = new List<string[]>();
                    eleves = new List<EleveBE>();
                    mappage = new Dictionary<string, string>();
                    mappageNumero = new Dictionary<string, string>();
                    nbreussi = 0;

                    if (ReunionEleveUC.SERIE == reunionEleve.concerne)
                    {
                        eleves = notificationBL.listeEleveDuneSerie(reunionEleve.code, annee);
                        if (eleves != null)
                            foreach (EleveBE e in eleves)
                            {
                                if (Tools.format_number(e.telParent) == "")
                                    echecs.Add(new Adresse(e.telParent, e.nom));
                                else
                                {
                                    nbreussi++;
                                    destinataireSMS.Add(new string[2] { e.matricule, e.telParent });
                                    mappage.Add(e.matricule, e.nom);
                                    mappageNumero.Add(e.matricule, e.telParent);
                                }
                            }
                    }
                    else if (ReunionEleveUC.CYCLE == reunionEleve.concerne)
                    {
                        eleves = notificationBL.listeEleveDunCycle(reunionEleve.code, annee);
                        if (eleves != null)
                            foreach (EleveBE e in eleves)
                            {
                                if (Tools.format_number(e.telParent) == "")
                                    echecs.Add(new Adresse(e.telParent, e.nom));
                                else
                                {
                                    nbreussi++;
                                    destinataireSMS.Add(new string[2] { e.matricule, e.telParent });
                                    mappage.Add(e.matricule, e.nom);
                                    mappageNumero.Add(e.matricule, e.telParent);
                                }
                            }
                    }
                    else if (ReunionEleveUC.NIVEAU == reunionEleve.concerne)
                    {
                        eleves = notificationBL.listeEleveDunNiveau(reunionEleve.code, annee);
                        if (eleves != null)
                            foreach (EleveBE e in eleves)
                            {
                                if (Tools.format_number(e.telParent) == "")
                                    echecs.Add(new Adresse(e.telParent, e.nom));
                                else
                                {
                                    nbreussi++;
                                    destinataireSMS.Add(new string[2] { e.matricule, e.telParent });
                                    mappage.Add(e.matricule, e.nom);
                                    mappageNumero.Add(e.matricule, e.telParent);
                                }
                            }
                    }
                    else if (ReunionEleveUC.CLASSE == reunionEleve.concerne)
                    {
                        eleves = notificationBL.listeEleveDuneClasse(reunionEleve.code, annee);
                        if (eleves != null)
                            foreach (EleveBE e in eleves)
                            {
                                if (Tools.format_number(e.telParent) == "")
                                    echecs.Add(new Adresse(e.telParent, e.nom));
                                else
                                {
                                    nbreussi++;
                                    destinataireSMS.Add(new string[2] { e.matricule, e.telParent });
                                    mappage.Add(e.matricule, e.nom);
                                    mappageNumero.Add(e.matricule, e.telParent);
                                }
                            }
                    }
                    else if (ReunionEleveUC.MIXTE == reunionEleve.concerne)
                    {
                        eleves = notificationBL.listeEleveDuneAnnee(annee);
                        if (eleves != null)
                            foreach (EleveBE e in eleves)
                            {
                                if (Tools.format_number(e.telParent) == "")
                                    echecs.Add(new Adresse(e.telParent, e.nom));
                                else
                                {
                                    nbreussi++;
                                    destinataireSMS.Add(new string[2] { e.matricule, e.telParent });
                                    mappage.Add(e.matricule, e.nom);
                                    mappageNumero.Add(e.matricule, e.telParent);
                                }
                            }
                    }

                    nb = eleves.Count;
                    result = new Dictionary<string, int>();
                    result = notificationBL.envoiSMS(destinataireSMS, reunionEleve.message);
                    
                    //if (result != null)
                    //{
                    //    foreach (KeyValuePair<string, string> element in mappage)
                    //    {
                    //        if (result[element.Key] != 0)
                    //            echecs.Add(new Adresse(mappageNumero[element.Key], element.Value));
                    //    }
                    //    nbreussi = nb - echecs.Count;
                    //}
                    //else
                    //    foreach (KeyValuePair<string, string> element in mappage)
                    //    {
                    //        echecs.Add(new Adresse(mappageNumero[element.Key], element.Value));
                    //    }

                    break;

                default:
                    break;
            }
        }
    }

    public partial class Adresse
    {
        public string numero { get; set; }
        public string nom { get; set; }

        public Adresse(string numero, string nom)
        {
            this.numero = numero;
            this.nom = nom;
        }
    }
}
