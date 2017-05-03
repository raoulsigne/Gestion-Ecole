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

namespace Ecole.UI
{
    /// <summary>
    /// Interaction logic for ModifierPasswordUI.xaml
    /// </summary>
    public partial class ModifierPasswordUI : Window
    {
        GestionModifierPasswordBL modifierPasswordBL;
        UtilisateurBE utilisateur;

        public ModifierPasswordUI()
        {
            InitializeComponent();
            modifierPasswordBL = new GestionModifierPasswordBL();
            utilisateur = new UtilisateurBE();
        }

        private void cmdEnregistrer_Click(object sender, RoutedEventArgs e)
        {
            if (validerFormulaire())
            {
                utilisateur = new UtilisateurBE();
                utilisateur.login = txtLogin.Text;
                utilisateur.password = pwdAncien.Password;
                utilisateur = modifierPasswordBL.rechercherUtilisateur(utilisateur);
                if (utilisateur != null)
                {
                    UtilisateurBE user = new UtilisateurBE();
                    user.login = utilisateur.login;
                    user.nom = utilisateur.nom;
                    user.role = utilisateur.role;
                    user.password = pwdNouveau.Password;
                    if (modifierPasswordBL.modifierUtilisateur(utilisateur, user))
                    {
                        MessageBox.Show("Modification effectuée", "school brain : alerte");
                        txtLogin.Text = "";
                        pwdAncien.Password = "";
                        pwdNouveau.Password = "";
                        pwdNouveauConfimer.Password = "";
                    }
                    else
                        MessageBox.Show("Modification non effectuée", "school brain : alerte");
                }
                else
                {
                    pwdAncien.Password = "";
                    pwdNouveau.Password = "";
                    pwdNouveauConfimer.Password = "";
                    MessageBox.Show("L'utilisateur avec ces parametres n'existe pas encore, vérifier bien l'ancien mot de passe", "school brain : alerte");
                }
            }
        }

        private bool validerFormulaire()
        {
            if (txtLogin.Text == "" || pwdAncien.Password == "" || pwdNouveau.Password == "" || pwdNouveauConfimer.Password == "")
            {
                MessageBox.Show("Vous devez renseigner tous les champs", "school brain : alerte");
                return false;
            }
            else
            {
                if (pwdNouveau.Password != pwdNouveauConfimer.Password)
                {
                    MessageBox.Show("Les mots de passe saisies sont differents", "school brain : alerte");
                    return false;
                }
                else
                    return true;
            }
        }

        private void cmdAnnuler_Click(object sender, RoutedEventArgs e)
        {
            txtLogin.Text = "";
            pwdAncien.Password = "";
            pwdNouveau.Password = "";
            pwdNouveauConfimer.Password = "";
        }

        private void cmdFermer_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
