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
using System.Xml;
using Ecole.BusinessLogic;
using Ecole.Utilitaire;
using Ecole.BusinessEntity;
using Ecole.BusinessLogic;
using System.Net;

namespace Ecole.UI
{
    /// <summary>
    /// Interaction logic for Connexion.xaml
    /// </summary>
    public partial class ConnexionUI : Window
    {
        GestionConnexionBL connexionBL;
        public static UtilisateurBE utilisateur { get; set; }
        
        public static string DOSSIER_PHOTO = "";
        public static string DOSSIER_ETATS = "";
        public static string DOSSIER_IMAGES = "";
        public static string DOSSIER_BULLETINS = "";
        public static string DOSSIER_EQUIPE = "";

        public ConnexionUI()
        {

            InitializeComponent();

            connexionBL = new GestionConnexionBL();
            txtLogin.Focus();

            ParametresBE parametre = new ParametresBE();
            CreerModifierParametresBL creerModifierParametresBL = new CreerModifierParametresBL();

            parametre = creerModifierParametresBL.getParametre();

            if (parametre != null)
            {
                DOSSIER_PHOTO = @"" + parametre.REPERTOIRE_PHOTO + "Photos\\";
                DOSSIER_ETATS = @"" + parametre.REPERTOIRE_PHOTO + "Etats\\";
                DOSSIER_IMAGES = @"" + parametre.REPERTOIRE_PHOTO + "Images\\";
                DOSSIER_BULLETINS = @"" + parametre.REPERTOIRE_PHOTO + "Bulletins\\";
                DOSSIER_EQUIPE = @"" + parametre.REPERTOIRE_PHOTO + "Equipe\\";
            }

        }

        private void cmdAnnuler_Click(object sender, RoutedEventArgs e)
        {
            txtLogin.Text = "";
            pwdPassword.Password = "";
        }

        private void cmdConnexion_Click(object sender, RoutedEventArgs e)
        {
            if (txtLogin.Text != "" && pwdPassword.Password != "")
            {
                string login = txtLogin.Text;
                string password = pwdPassword.Password;

                ConnexionUI.utilisateur = connexionBL.connect(login, password);
                if (ConnexionUI.utilisateur != null)
                {
                    connexionBL.journaliser("Connexion au système");
                    List<ParametresBE> listesParametres = new List<ParametresBE>();
                    listesParametres = connexionBL.listerTousLesParametres();
                    this.Close();

                    Boolean quitterAppli = false;
                    while (listesParametres == null || listesParametres.Count == 0)
                    {
                        WindowAddEditParametresUI parametreWindow = new WindowAddEditParametresUI();
                        parametreWindow.ShowDialog();
                        listesParametres = connexionBL.listerTousLesParametres();
                        if (listesParametres == null || listesParametres.Count == 0)
                        {
                            if (MessageBox.Show("Vous devez entrer les parametres de l'etablissement ! \n \n \n Voulez-vous continuer ? ", "School : Confimation", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.No)
                            {
                                //********************************** instruction pour fermer l'application ***************
                                quitterAppli = true;

                                break;
                            }
                            
                        }
                    }

                    if (quitterAppli) {
                        this.Close();
                        Application.Current.Shutdown();
                        //Application.Current.Exit();

                    }

                    MainWindow fenetre = new MainWindow();
                    fenetre.Show();
                }
                else
                    MessageBox.Show("Mot de passe ou Login incorrect", "School brain:Alerte");
            }
        }

        private void pwdPassword_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
                cmdConnexion_Click(sender, e);
        }

        private void txtLogin_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                if (txtLogin.Text != "")
                    pwdPassword.Focus();
            }
        }
    }
}
