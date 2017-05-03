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
using System.Globalization;
using System.Threading;

namespace Ecole.UI
{
    /// <summary>
    /// Interaction logic for PaysUI.xaml
    /// </summary>
    public partial class PaysUI : Window
    {
        GestionPaysBL paysBL;
        List<PaysBE> pays;
        bool doubleclick;
        PaysBE objet_pays;

        public PaysUI()
        {
            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";
            Thread.CurrentThread.CurrentCulture = ci;
            
            paysBL = new GestionPaysBL();
            pays = new List<PaysBE>();
            objet_pays = new PaysBE();
            doubleclick = false;

            InitializeComponent();
            pays = paysBL.listerToutPays();
            grdListePays.DataContext = this;
            grdListePays.ItemsSource = pays;
        }

        private void cmdValider_Click(object sender, RoutedEventArgs e)
        {
            String code, nom, nationalite;
            PaysBE p;

            if (validerFormulaire() == false)
                MessageBox.Show("Veuillez remplir tous les champs", "School brain : Message d'alerte");
            else
            {
                code = txtCodePays.Text;
                nationalite = txtNationalite.Text;
                nom = txtNom.Text;
                p = new PaysBE(code, nom, nationalite);
                if (doubleclick) //c'est une modification
                {
                    //suppression de l'ancienne valeur
                    if (paysBL.modifierPays(objet_pays, p))
                    {
                        pays.Remove(objet_pays);
                        pays.Add(p);
                        grdListePays.Items.Refresh();
                        doubleclick = false;
                        txtCodePays.Clear();
                        txtNationalite.Clear();
                        txtNom.Clear();
                    }
                    else
                        MessageBox.Show("Modification impossible, verifier si le code ne se repete pas", "School brain:Alerte");
                    //txtCodePays.IsEnabled = true;
                }
                //ajout de la nouvelle valeur
                else if (paysBL.enregistrerPays(p))
                {
                    MessageBox.Show("Pays enregistrer avec succès");
                    pays = paysBL.listerToutPays();
                    grdListePays.ItemsSource = pays;
                    txtCodePays.Clear();
                    txtNationalite.Clear();
                    txtNom.Clear();
                }
                else
                    MessageBox.Show("Enregistrement non effectué");
            }

        }

        private void cmdAnuler_Click(object sender, RoutedEventArgs e)
        {
            txtCodePays.Clear();
            txtNationalite.Clear();
            txtNom.Clear();
            doubleclick = false;
            //txtCodePays.IsEnabled = true;
            grdListePays.UnselectAll();
        }

        private void cmdFermer_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cmdImprimer_Click(object sender, RoutedEventArgs e)
        {
            CreerEtat etat = new CreerEtat("Pays-" + DateTime.Today.ToShortDateString(), "Liste des pays");
            paysBL.journaliser("Impression de la liste des pays");
            etat.obtenirEtat(grdListePays);
        }

        private void grdListePays_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                if (MessageBox.Show("Voulez vous supprimer?", "School : Confirmation", System.Windows.MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    if (grdListePays.SelectedIndex != -1)
                    {
                        PaysBE p = new PaysBE();
                        p = pays.ElementAt(grdListePays.SelectedIndex);
                        paysBL.supprimerPays(p);
                        pays.Remove(p);
                        grdListePays.ItemsSource = pays;
                        grdListePays.Items.Refresh();
                    }
                    else
                        MessageBox.Show("Aucune ligne sélectionnée", "School brain:Alerte");
                }
            }
        }

        private void grdListePays_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (grdListePays.SelectedIndex >= 0)
            {
                objet_pays = pays.ElementAt(grdListePays.SelectedIndex);
                txtNom.Text = objet_pays.nomPays;
                txtNationalite.Text = objet_pays.nationalite;
                txtCodePays.Text = objet_pays.codePays;
                //txtCodePays.IsEnabled = false;
                doubleclick = true;
                grdListePays.UnselectAll();
            }
        }

        private bool validerFormulaire()
        {
            bool b = true;
            if (txtCodePays.Text == null || txtNom.Text == null || txtNationalite.Text == null)
                b = false;
            else if (txtNom.Text == "" || txtCodePays.Text == "" || txtNationalite.Text == "")
                b = false;

            return b;
        }
    }
}
