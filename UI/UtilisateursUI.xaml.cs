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
using Ecole.Utilitaire;

namespace Ecole.UI
{
    /// <summary>
    /// Interaction logic for UtilisateursUI.xaml
    /// </summary>
    public partial class UtilisateursUI : Window
    {
        GestionUtilisateurBL utilisateurBL;
        List<UtilisateurBE> utilisateurs;
        UtilisateurBE old_utilisateur;
        List<string> roles;
        static string TYPE_ENREGISTRER = "enregistrer";
        static string TYPE_MODIFIER = "modifier";
        string typeValidation;

        public UtilisateursUI()
        {
            InitializeComponent();

            utilisateurBL = new GestionUtilisateurBL();
            utilisateurs = new List<UtilisateurBE>();
            roles = new List<string>();

            typeValidation = TYPE_ENREGISTRER;
            utilisateurs = utilisateurBL.listerToutUtilisateur();
            roles = utilisateurBL.listerValeurColonneGroupe("role");
            grdListe.ItemsSource = utilisateurs;
            cmbGroupe.ItemsSource = roles;
            cmbGroupe.SelectedIndex = 0;
        }

        private void cmdEnregistrer_Click(object sender, RoutedEventArgs e)
        {
            if (validerFormulaire())
            {
                UtilisateurBE utilisateur = new UtilisateurBE(txtLogin.Text, cmbGroupe.SelectedValue.ToString(), pwdMotDePasse.Password, txtNom.Text);
                if (typeValidation == TYPE_ENREGISTRER)
                {
                    if (pwdMotDePasse.Password != "" && (pwdMotDePasse.Password == pwdConfirmerMotDePasse.Password))
                    {
                        if (utilisateurBL.enregistrerUtilisateur(utilisateur))
                            MessageBox.Show("Enregistrement effectué", "School brain : Alerte");
                        else
                            MessageBox.Show("Enregistrement échoué", "School brain : Alerte");
                    }
                    else
                        MessageBox.Show("Les mots de passe ne correspondent pas ", "School brain : alerte");
                }
                else
                {
                    if (utilisateurBL.modifierUtilisateur(old_utilisateur, utilisateur))
                        MessageBox.Show("Mise à jour effectuée", "School brain : Alerte");
                    else
                        MessageBox.Show("Mise à jour échouée", "School brain : Alerte");
                }

                utilisateurs = utilisateurBL.listerToutUtilisateur();
                grdListe.ItemsSource = utilisateurs;
                grdListe.Items.Refresh();
                txtNom.Clear();
                txtLogin.Clear();
                pwdMotDePasse.Password = "";
                pwdConfirmerMotDePasse.Password = "";
                typeValidation = TYPE_ENREGISTRER;
            }
            else
                MessageBox.Show("il y'a des champs vides, remplir tous les champs du formulaire", "School brain : alerte");
        }

        private bool validerFormulaire()
        {
            if (txtLogin.Text == "" || txtNom.Text == "" || cmbGroupe.SelectedValue == null)
                return false;
            else
                return true;
        }

        private void cmdAnnuler_Click(object sender, RoutedEventArgs e)
        {
            txtNom.Clear();
            txtLogin.Clear();
            pwdMotDePasse.Password = "";
            pwdConfirmerMotDePasse.Password = "";
            typeValidation = TYPE_ENREGISTRER;
            grdListe.UnselectAll();
        }

        private void cmdFermer_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cmdImprimer_Click(object sender, RoutedEventArgs e)
        {
            CreerEtat etat = new CreerEtat("Utilisateurs-" + DateTime.Today.ToShortDateString(), "Liste des utilisateurs");
            utilisateurBL.journaliser("Impression de la liste des utilisateurs");
            etat.obtenirEtat(grdListe);
        }

        private void grdListe_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                if (MessageBox.Show("Voulez vous supprimer?", "School : Confirmation", System.Windows.MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    UtilisateurBE utilisateur = new UtilisateurBE();
                    utilisateur = utilisateurs.ElementAt(grdListe.SelectedIndex);
                    utilisateurBL.supprimerUtilisateur(utilisateur);
                    utilisateurs.Remove(utilisateur);
                    grdListe.ItemsSource = utilisateurs;
                    grdListe.Items.Refresh();
                    txtNom.Clear();
                    txtLogin.Clear();
                    pwdMotDePasse.Password = "";
                    pwdConfirmerMotDePasse.Password = "";
                    typeValidation = TYPE_ENREGISTRER;
                }
            }
        }

        private void grdListe_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (grdListe.SelectedIndex >= 0)
            {
                old_utilisateur = utilisateurs.ElementAt(grdListe.SelectedIndex);
                txtLogin.Text = old_utilisateur.login;
                txtNom.Text = old_utilisateur.nom;
                cmbGroupe.SelectedIndex = cmbGroupe.Items.IndexOf(old_utilisateur.role);
                pwdConfirmerMotDePasse.Password = "";
                pwdMotDePasse.Password = "";
                typeValidation = TYPE_MODIFIER;
                grdListe.UnselectAll();
            }
        }
    }
}
