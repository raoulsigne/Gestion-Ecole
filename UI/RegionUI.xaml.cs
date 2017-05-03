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
    /// Interaction logic for RegionUI.xaml
    /// </summary>
    public partial class RegionUI : Window
    {
        GestionRegionBL regionBL;
        List<RegionBE> regions;
        bool doubleclick;
        RegionBE objet_region;

        public RegionUI()
        {
            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";
            Thread.CurrentThread.CurrentCulture = ci;
            
            regionBL = new GestionRegionBL();
            regions = new List<RegionBE>();
            objet_region = new RegionBE();
            doubleclick = false;

            InitializeComponent();

            regions = regionBL.listerTousRegion();
            grdListeRegions.DataContext = this;
            grdListeRegions.ItemsSource = regions;
        }

        private void cmdValider_Click(object sender, RoutedEventArgs e)
        {
            String code, nom;
            RegionBE r;
            if (validerFormulaire() == false)
                MessageBox.Show("Veuillez remplir tous les champs", "School brain : Message d'alerte");
            else
            {
                code = txtCodeRegion.Text;
                nom = txtNom.Text;
                r = new RegionBE(code, nom);
                if (doubleclick) //c'est une modification
                {
                    //suppression de l'ancienne valeur
                    if (regionBL.modiferRegion(objet_region, r))
                    {
                        regions.Remove(objet_region);
                        regions.Add(r);
                        grdListeRegions.Items.Refresh();
                        doubleclick = false;
                        txtCodeRegion.Clear();
                        txtNom.Clear();
                    }
                    else
                        MessageBox.Show("Modification impossible, verifier si le code ne se repete pas", "School brain:Alerte");
                    //txtCodeRegion.IsEnabled = true;
                }
                //ajout de la nouvelle region
                else if (regionBL.enregistrerRegioin(r))
                {
                    MessageBox.Show("Region enregistrer avec succès");
                    regions = regionBL.listerTousRegion();
                    grdListeRegions.ItemsSource = regions;
                    txtCodeRegion.Clear();
                    txtNom.Clear();
                }
                else
                    MessageBox.Show("Enregistrement non effectué");
            }
        }

        private void cmdAnuler_Click(object sender, RoutedEventArgs e)
        {
            txtCodeRegion.Clear();
            txtNom.Clear();
            doubleclick = false;
            //txtCodeRegion.IsEnabled = true;
            grdListeRegions.UnselectAll();
        }

        private void cmdFermer_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void dataGrid1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                if (MessageBox.Show("Voulez vous supprimer?", "School : Confirmation", System.Windows.MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    if (grdListeRegions.SelectedIndex != -1)
                    {
                        RegionBE r = new RegionBE();
                        r = regions.ElementAt(grdListeRegions.SelectedIndex);
                        regionBL.supprimerRegion(r);
                        regions.Remove(r);
                        grdListeRegions.ItemsSource = regions;
                        grdListeRegions.Items.Refresh();
                    }
                    else
                        MessageBox.Show("Aucune ligne sélectionnée", "School brain:Alerte");
                }
            }
        }

        private void dataGrid1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (grdListeRegions.SelectedIndex >= 0)
            {
                objet_region = regions.ElementAt(grdListeRegions.SelectedIndex);
                txtNom.Text = objet_region.nomregion;
                txtCodeRegion.Text = objet_region.coderegion;
                doubleclick = true;
                //txtCodeRegion.IsEnabled = false;
                grdListeRegions.UnselectAll();
            }
        }

        private void cmdImprimer_Click(object sender, RoutedEventArgs e)
        {
            CreerEtat etat = new CreerEtat("Regions-" + DateTime.Today.ToShortDateString(), "Liste des regions");
            regionBL.journaliser("Impression de la liste des regions");
            etat.obtenirEtat(grdListeRegions);
        }

        private bool validerFormulaire()
        {
            bool b = true;
            if (txtCodeRegion.Text == null || txtNom.Text == null)
                b = false;
            else if (txtNom.Text == "" || txtCodeRegion.Text == "")
                b = false;

            return b;
        }

    }
}
