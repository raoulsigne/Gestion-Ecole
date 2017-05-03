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

using System.Security.Permissions;
using System.Security;

using System.Configuration;

using Ecole.DataAccess;

using Ecole.BusinessLogic;
using Ecole.BusinessEntity;
using System.IO;
using Ecole.Utilitaire;

namespace Ecole.UI
{
    /// <summary>
    /// Interaction logic for DemarrageUI.xaml
    /// </summary>
    public partial class DemarrageUI : Window
    {
        DemarrageBL demarrage;

        public static String login;
        public static String role;

        public bool ExisterUtilisateur; //variable indiquant si il n'existe pas encore d'utilisateur dans le système

        public DemarrageUI()
        {
            //GestionRepertoire.QshareFolder(@"E:\\jeux\\", "html5", "This is a Test Share");
            demarrage = new DemarrageBL();
            //File.SetAttributes(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile, FileAttributes.Encrypted);
            ////----------------------
           // MessageBox.Show(AppDomain.CurrentDomain.BaseDirectory);
           // String chaineCon = ConfigurationManager.ConnectionStrings["connexion"].ToString();
           // MessageBox.Show(chaineCon);
           // MessageBox.Show(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
          //---------------------------
            ExisterUtilisateur = false; //par défaut il ya aucun utilisateur dans le système

            //on lit dans le fichier de configuration pour savoir si c'est la premier fois qu'on lance l'application
            if (!demarrage.IsFisrtTime())
            {
                SplashScreen screen = new SplashScreen();
                screen.ShowDialog();
                this.Close();
            }
            else
                MessageBox.Show("Premier Démarrage de l'application, \n Veuillez configurer votre copie de School Brain","SCHOOL BRAIN : Démarrage",MessageBoxButton.OK,MessageBoxImage.Information);


            InitializeComponent();

            txtNomSociete.Focus();

            //on grise la partie du formulaire demandant les infos d'administration
            txtLogin.IsEnabled = false;
            pswdAdmin.IsEnabled = false;
            pswdAdminConfirmation.IsEnabled = false;


        }

        private void cmdValider_Click(object sender, RoutedEventArgs e)
        {
            if (ExisterUtilisateur)
            {
                //si un utilisateur existe deja dans le système

                if ((txtNomSociete.Text != null && txtAdresseServeur.Text != null && txtUtilisateurBD.Text != null && passwdBD.Password != null) &&
                    (txtNomSociete.Text != "" && txtAdresseServeur.Text != "" && txtUtilisateurBD.Text != ""))
                   // if ((txtNomSociete.Text != null && txtAdresseServeur.Text != null && txtUtilisateurBD.Text != null && passwdBD.Password != null) &&
                   //(txtNomSociete.Text != "" && txtAdresseServeur.Text != "" && txtUtilisateurBD.Text != "" && passwdBD.Password != ""))

                {

                    String nomSociete = txtNomSociete.Text;
                    String adresseServeur = txtAdresseServeur.Text;
                    String utilisateurBD = txtUtilisateurBD.Text;
                    String passwordBD = passwdBD.Password;

                    //String loginAdmin = txtLogin.Text;
                    //String passwordAdmin = pswdAdmin.Password;
                    //String passWordAdminConfirmation = pswdAdminConfirmation.Password;

                    //on teste la connexion avant d'écrire dans le fichier
                    //on teste la connexion à la BD

                    if (DemarrageBL.testConnexion(adresseServeur, utilisateurBD, passwordBD))
                    {
                        //on écrit le nom de la société et les paramètres du serveur dans le fichier de configuration app.Config
                      //  MessageBox.Show("Avant écriture Param BD");
                        if (demarrage.EcrireConfigurationConnexionBD(adresseServeur, utilisateurBD, passwordBD))
                        {
                         //   MessageBox.Show("Après écriture Param BD");
                            //on recharge les configurations de la BD
                            //Connexion con = new Connexion();
                            //MessageBox.Show("OK");
                            Connexion.resetConnexion();
                            Connexion con = Connexion.getConnexion();

                         //   MessageBox.Show("Avant écriture Param Appli");
                            demarrage.EcrireConfigurationApplication(nomSociete);
                         //   MessageBox.Show("Avant écriture Param Appli");
                            this.Visibility = System.Windows.Visibility.Hidden;

                            //on charge la page de connexion
                            SplashScreen screen = new SplashScreen();
                            screen.ShowDialog();

                            this.Close();

                        }
                        else
                            MessageBox.Show("Impossible d'écrire dans le fichier de configuration !", "School brain : Alerte", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                        MessageBox.Show("Impossible de se connecter à  ce serveur ! \n\n Vérifiez vos paramètres de connexion", "School brain : Alerte", MessageBoxButton.OK, MessageBoxImage.Error);

                }
                else
                    MessageBox.Show("Tous les champs doivent être rempli !", "School brain : Alerte", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            else
            {

                if ((txtNomSociete.Text != null && txtAdresseServeur.Text != null && txtUtilisateurBD.Text != null && passwdBD.Password != null && txtLogin.Text != null && pswdAdmin.Password != null && pswdAdminConfirmation.Password != null) &&
                    (txtNomSociete.Text != "" && txtAdresseServeur.Text != "" && txtUtilisateurBD.Text != "" && txtLogin.Text != "" && pswdAdmin.Password != "" && pswdAdminConfirmation.Password != ""))
                {

                    String nomSociete = txtNomSociete.Text;
                    String adresseServeur = txtAdresseServeur.Text;
                    String utilisateurBD = txtUtilisateurBD.Text;
                    String passwordBD = passwdBD.Password;

                    String loginAdmin = txtLogin.Text;
                    String passwordAdmin = pswdAdmin.Password;
                    String passWordAdminConfirmation = pswdAdminConfirmation.Password;

                    ////positionner le user en cours--MOI---------------------
                    //UtilisateurBE utilisateur = new UtilisateurBE();
                    //utilisateur.login = loginAdmin;
                    //Ecole.UI.ConnexionUI.utilisateur = utilisateur;

                   
                    //on teste la connexion avant d'écrire dans le fichier
                    //on teste la connexion à la BD


                    if (DemarrageBL.testConnexion(adresseServeur, utilisateurBD, passwordBD))
                    {
                        //on écrit le nom de la société et les paramètres du serveur dans le fichier de configuration app.Config
                      //  MessageBox.Show("Avant écriture Param BD2");
                        if (demarrage.EcrireConfigurationConnexionBD(adresseServeur, utilisateurBD, passwordBD))
                        {
                          //  MessageBox.Show("Après écriture Param BD2");
                            //on recharge les configurations de la BD
                            //Connexion con = new Connexion();
                            //MessageBox.Show("OK");
                            Connexion.resetConnexion();
                            Connexion con = Connexion.getConnexion();

                            //on vérifit si le mot de passe de l'administrateur et sa confirmation sont égale
                            if (passwordAdmin != "" && (passwordAdmin == passWordAdminConfirmation))
                            {

                                UtilisateurBE utilisateur = new UtilisateurBE(loginAdmin, "administrateur", passwordAdmin, loginAdmin);

                                if (!demarrage.enregistrerUtilisateur(utilisateur))
                                    MessageBox.Show("Impossible de se connecter à la Base de données ! \n\n Vérifier l'adresse du serveur et le mot de passe ", "School brain : Alerte", MessageBoxButton.OK, MessageBoxImage.Error);
                                else
                                {
                                    MessageBox.Show("Configuration sauvegardé avec Succès !", "School brain : Alerte", MessageBoxButton.OK, MessageBoxImage.Information);

                                    login = loginAdmin;
                                    role = "administrateur";

                                   // MessageBox.Show("Avant écriture Param Appli");
                                    demarrage.EcrireConfigurationApplication(nomSociete);
                                  //  MessageBox.Show("Arès écriture Param Appli");

                                    this.Visibility = System.Windows.Visibility.Hidden;

                                    //on charge la page de connexion
                                    ConnexionUI connexionUI = new ConnexionUI();
                                    connexionUI.ShowDialog();

                                    this.Close();
                                }

                            }
                            else
                                MessageBox.Show("Les mots de passe de l'administrateur ne correspondent pas !", "School brain : Alerte", MessageBoxButton.OK, MessageBoxImage.Error);

                        }
                        else
                            MessageBox.Show("Impossible d'écrire dans le fichier de configuration !", "School brain : Alerte", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                        MessageBox.Show("Impossible de se connecter à  ce serveur ! \n\n Vérifiez vos paramètres de connexion", "School brain : Alerte", MessageBoxButton.OK, MessageBoxImage.Error);

                }
                else
                    MessageBox.Show("Tous les champs doivent être rempli !", "School brain : Alerte", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void cmdAnnuler_Click(object sender, RoutedEventArgs e)
        {
            txtNomSociete.Text = "";
            txtAdresseServeur.Text = "";
            txtUtilisateurBD.Text = "";
            passwdBD.Password = "";
            txtLogin.Text = "";
            pswdAdmin.Password = "";
            pswdAdminConfirmation.Password = "";
        }

        private void passwdBD_KeyUp(object sender, KeyEventArgs e)
        {
           if (e.Key==Key.Enter)
           {
               if ((txtNomSociete.Text != null && txtAdresseServeur.Text != null && txtUtilisateurBD.Text != null) &&
                  (txtNomSociete.Text != "" && txtAdresseServeur.Text != "" && txtUtilisateurBD.Text != ""))
               {
                   String nomSociete = txtNomSociete.Text;
                   String adresseServeur = txtAdresseServeur.Text;
                   String utilisateurBD = txtUtilisateurBD.Text;
                   String passwordBD = passwdBD.Password;

                   if (DemarrageBL.testConnexion(adresseServeur, utilisateurBD, passwordBD))
                   {
                       //MessageBox.Show("Avant ecriture BD1");
                       if (demarrage.EcrireConfigurationConnexionBD(adresseServeur, utilisateurBD, passwordBD))
                       {
                          // MessageBox.Show("Après ecriture BD1");
                           //on vérifit si il ya deja un utilisateur dans la BD
                           UtilisateurDA utilisateurDA = new UtilisateurDA();
                           List<UtilisateurBE> LUtilisateur = utilisateurDA.listerTous();

                           
                           //si il n'existe pas d'utilisateur dans le système
                           if (LUtilisateur == null || LUtilisateur.Count == 0)
                           {
                               MessageBox.Show("Aucun utilisateur, \nVeuillez créer le compte d'Administration de School Brain", "School Brain : Alerte", MessageBoxButton.OK, MessageBoxImage.Information);

                               //on active la partie du formulaire qui demande les infos de connexion de l'admin
                               txtLogin.IsEnabled = true;
                               pswdAdmin.IsEnabled = true;
                               pswdAdminConfirmation.IsEnabled = true;

                               ExisterUtilisateur = false;
                               txtLogin.Focus();
                           }
                           else
                           {
                               ExisterUtilisateur = true;
                               MessageBox.Show("Connexion réussie sur le Serveur " + adresseServeur, "School Brain : Connexion au serveur de Données", MessageBoxButton.OK, MessageBoxImage.Information);

                           }
                       }
                   }

                   else
                   {
                       MessageBox.Show("Echec de connexion au Serveur " + adresseServeur + "\nVerifiez les informations entrees", "School Brain : Connexion au serveur de Données", MessageBoxButton.OK, MessageBoxImage.Error);

                   }
               }
           }
        }

        private void cmdFermer_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

              
        
    }
}
