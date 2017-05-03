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

using System.Data;

namespace Ecole.UI
{
    /// <summary>
    /// Interaction logic for WindowEditInfosDemarrageUI.xaml
    /// </summary>
    public partial class WindowEditInfosDemarrageUI : Window
    {
        EditInfosDemarrageBL editInfosDemarrageBL;

        public WindowEditInfosDemarrageUI()
        {
            InitializeComponent();

            editInfosDemarrageBL = new EditInfosDemarrageBL();

            //on charge est ancienne configuration de demarrage de la BD sur le formulaire

            String nomSociete = editInfosDemarrageBL.getNomSociete();
            txtNomSociete.Text = nomSociete;

            String[] table = new String[3];
            table = editInfosDemarrageBL.getParametresConnexionBD();

            if (table != null) { 
                txtAdresseServeur.Text = table[0];
                txtUtilisateurBD.Text = table[1];
                passwdBD.Password = table[2];
            }

        }

        private void cmdValider_Click(object sender, RoutedEventArgs e)
        {
            if ((txtNomSociete.Text != null && txtAdresseServeur.Text != null && txtUtilisateurBD.Text != null && passwdBD.Password != null) &&
                (txtNomSociete.Text != "" && txtAdresseServeur.Text != "" && txtUtilisateurBD.Text != "" && passwdBD.Password != ""))
            {

                String nomSociete = txtNomSociete.Text;
                String adresseServeur = txtAdresseServeur.Text;
                String utilisateurBD = txtUtilisateurBD.Text;
                String passwordBD = passwdBD.Password;

                //****************** on teste la connexion avant d'écrire dans le fichier
                if (EditInfosDemarrageBL.testConnexion(adresseServeur, utilisateurBD, passwordBD))
                {

                    //on écrit le nom de la société et les paramètres du serveur dans le fichier de configuration app.Config
                    if (editInfosDemarrageBL.EcrireConfigurationConnexionBD(adresseServeur, utilisateurBD, passwordBD) && editInfosDemarrageBL.EcrireConfigurationApplication(nomSociete))
                    {
                        MessageBox.Show("Sauvegarde des configurations effectuée !", "School brain : Alerte");

                    }
                    else
                        MessageBox.Show("Impossible d'écrire dans le fichier de configuration !", "School brain : Alerte");

                }
                else MessageBox.Show("Impossible de se connecter à la Base de données avec les informations spécifié. \n \n veuillez changer les paramètres SVP ! \n Sinon la modification ne sera pas prise en compte ", "School brain : Alerte");
            }
            else
                MessageBox.Show("Tous les champs doivent être rempli !", "School brain : Alerte");
        }

        private void cmdAnnuler_Click(object sender, RoutedEventArgs e)
        {
            txtNomSociete.Text = "";
            txtAdresseServeur.Text = "";
            passwdBD.Password = "";
            txtUtilisateurBD.Text = "";
        }
    }
}
