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
using Ecole.Utilitaire;
using System.Globalization;
using System.Threading;

namespace Ecole.UI
{
    /// <summary>
    /// Interaction logic for GroupeUtilisateursUI.xaml
    /// </summary>
    public partial class GroupeUtilisateursUI : Window
    {
        static string TYPE_ENREGISTRER = "enregistrer";
        static string TYPE_MODIFIER = "modifier";
        GestionGroupeUtilisateurBL groupeBL;
        List<GroupeBE> groupes;
        GroupeBE old_groupe;
        string typeValidation;

        public GroupeUtilisateursUI()
        {
            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "yyyy-MM-dd";
            Thread.CurrentThread.CurrentCulture = ci;

            InitializeComponent();
            groupeBL = new GestionGroupeUtilisateurBL();
            groupes = new List<GroupeBE>();
            old_groupe = new GroupeBE();
            typeValidation = TYPE_ENREGISTRER;

            groupes = groupeBL.listerToutGroupe();
            grdListe.ItemsSource = groupes;
        }

        private void cmdEnregistrer_Click(object sender, RoutedEventArgs e)
        {
            if (validerFormulaire())
            {
                GroupeBE groupe = new GroupeBE();
                groupe.role = txtRole.Text;
                groupe.description = txtDescription.Text;
                if (typeValidation == TYPE_ENREGISTRER)
                {
                    if (groupeBL.enregistrer(groupe))
                        MessageBox.Show("Enregistrement effectué","School brain : Alerte");
                    else
                        MessageBox.Show("Enregistrement échoué", "School brain : Alerte");
                }
                else
                {
                    if(groupeBL.modifierGroupe(old_groupe,groupe))
                        MessageBox.Show("Mise à jour effectuée", "School brain : Alerte");
                    else
                        MessageBox.Show("Mise à jour échouée", "School brain : Alerte");
                }

                groupes = groupeBL.listerToutGroupe();
                grdListe.ItemsSource = groupes;
                grdListe.Items.Refresh();
                txtRole.Clear();
                txtDescription.Clear();
                typeValidation = TYPE_ENREGISTRER;
            }
        }

        
        private void cmdAnnuler_Click(object sender, RoutedEventArgs e)
        {
            txtRole.Clear();
            txtDescription.Clear();
            typeValidation = TYPE_ENREGISTRER;
            grdListe.UnselectAll();
        }

        private void cmdFermer_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cmdImprimer_Click(object sender, RoutedEventArgs e)
        {
            CreerEtat etat = new CreerEtat("Groupes utilisateurs-" + DateTime.Today.ToShortDateString(), "Liste des groupes d'utilisateurs");
            groupeBL.journaliser("Impression de la liste des groupes d'utilisateurs ");
            etat.obtenirEtat(grdListe);
        }

        private bool validerFormulaire()
        {
            if (txtDescription.Text != "" && txtRole.Text != null)
                return true;
            else
                return false;
        }

        private void grdListeRegions_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (grdListe.SelectedIndex >= 0)
            {
                old_groupe = groupes.ElementAt(grdListe.SelectedIndex);
                txtRole.Text = old_groupe.role;
                txtDescription.Text = old_groupe.description;
                typeValidation = TYPE_MODIFIER;
                grdListe.UnselectAll();
            }
        }

        private void grdListeRegions_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                if (MessageBox.Show("Voulez vous supprimer?", "School : Confirmation", System.Windows.MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    if (grdListe.SelectedIndex != -1)
                    {
                        GroupeBE groupe = new GroupeBE();
                        groupe = groupes.ElementAt(grdListe.SelectedIndex);
                        groupeBL.supprimerGroupe(groupe);
                        groupes.Remove(groupe);
                        grdListe.ItemsSource = groupes;
                        grdListe.Items.Refresh();
                        txtRole.Clear();
                        txtDescription.Clear();
                        typeValidation = TYPE_ENREGISTRER;
                    }
                    else
                        MessageBox.Show("Aucune ligne sélectionnée", "School brain:Alerte");
                }
            }
        }

    }
}
