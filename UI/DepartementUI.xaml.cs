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
    /// Interaction logic for DepartementUI.xaml
    /// </summary>
    public partial class DepartementUI : Window
    {

        GestionDepartementBL departementBL;
        List<DepartementBE> departements;
        DepartementBE objet_departement;
        bool doubleclick;

        public DepartementUI()
        {
            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";
            Thread.CurrentThread.CurrentCulture = ci;
            
            departementBL = new GestionDepartementBL();
            departements = new List<DepartementBE>();
            objet_departement = new DepartementBE();
            doubleclick = false;

            InitializeComponent();

            grdListDepartements.DataContext = this;
            departements = departementBL.listerToutDepartement();
            grdListDepartements.ItemsSource = departements;
        }

        private void cmdValider_Click(object sender, RoutedEventArgs e)
        {
            String code, nom;
            DepartementBE  d;

            if (validerFormulaire())
            {
                code = txtCodeDepartement.Text;
                nom = txtNom.Text;
                d = new DepartementBE(code, nom);
                if (doubleclick) //c'est une modification
                {
                    //suppression de l'ancienne valeur
                    if (departementBL.modifierDepartement(objet_departement, d))
                    {
                        departements.Remove(objet_departement);
                        departements.Add(d);
                        grdListDepartements.Items.Refresh();
                        // txtCodeDepartement.IsEnabled = true;
                        txtCodeDepartement.Clear();
                        txtNom.Clear();
                        doubleclick = false;
                    }
                    else
                        MessageBox.Show("Modification impossible, verifier si le code ne se repete pas","School brain:Alerte");
                }
                //ajout de la nouvelle valeur
                else if (departementBL.enregistrerDepartement(d))
                {
                    MessageBox.Show("Departement enregistrer avec succès");
                    departements = departementBL.listerToutDepartement();
                    grdListDepartements.ItemsSource = departements;
                    txtCodeDepartement.Clear();
                    txtNom.Clear();
                }
                else
                    MessageBox.Show("Enregistrement non effectué");
            }
            else
                MessageBox.Show("Veuillez remplir tous les champs", "School brain : Message d'alerte");
        }

        private void cmdAnuler_Click(object sender, RoutedEventArgs e)
        {
            txtCodeDepartement.Clear();
            txtNom.Clear();
            // txtCodeDepartement.IsEnabled = true;
            doubleclick = false;
            grdListDepartements.UnselectAll();
        }

        private void cmdFermer_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void grdListDepartements_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                if (MessageBox.Show("Voulez vous supprimer?", "School : Confirmation", System.Windows.MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    if (grdListDepartements.SelectedIndex != -1)
                    {
                        DepartementBE d = new DepartementBE();
                        d = departements.ElementAt(grdListDepartements.SelectedIndex);
                        departementBL.supprimerDepartement(d);
                        departements.Remove(d);
                        grdListDepartements.ItemsSource = departements;
                        grdListDepartements.Items.Refresh();
                    }
                    else
                        MessageBox.Show("Aucune ligne sélectionnée", "School brain:Alerte");
                }
            }
        }

        private void grdListDepartements_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (grdListDepartements.SelectedIndex >= 0)
            {
                objet_departement = departements.ElementAt(grdListDepartements.SelectedIndex);
                txtNom.Text = objet_departement.nomDept;
                txtCodeDepartement.Text = objet_departement.codeDept;
                // txtCodeDepartement.IsEnabled = false;
                grdListDepartements.UnselectAll();
                doubleclick = true;
            }
        }

        private void cmdImprimer_Click(object sender, RoutedEventArgs e)
        {
            CreerEtat etat = new CreerEtat("Departements-" + DateTime.Today.ToShortDateString(), "Liste des departements");
            departementBL.journaliser("Impression de la liste des departements");
            etat.obtenirEtat(grdListDepartements);
        }

        private bool validerFormulaire()
        {
            bool b = true;

            if (txtCodeDepartement.Text == null || txtNom.Text == null)
                b = false;
            else if (txtCodeDepartement.Text == "" || txtNom.Text == "")
                b = false;

            return b;
        }
    }
}
