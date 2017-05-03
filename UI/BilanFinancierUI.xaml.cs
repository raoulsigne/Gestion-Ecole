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
using Ecole.ClasseConception;
using System.Globalization;
using Ecole.Utilitaire;

namespace Ecole.UI
{
    /// <summary>
    /// Interaction logic for BilanFinancierUI.xaml
    /// </summary>
    public partial class BilanFinancierUI : Window
    {
        List<LigneBilan> lignes;
        GestionBilanFinancierBL bilanBL;
        int annee;
        double totalAPayer, totalPaye, totalRemise, totalReste;

        public BilanFinancierUI()
        {
            CultureInfo elGR = CultureInfo.CreateSpecificCulture("el-GR");
            bilanBL = new GestionBilanFinancierBL();
            lignes = new List<LigneBilan>();

            InitializeComponent();

            annee = bilanBL.anneeEnCours();
            txtAnnee.Text = " / " + annee;
            txtAnneeScolaire.Text = (annee - 1).ToString();

            lignes = bilanBL.obtenirBilanFinancier(annee);
            if (lignes != null)
            {
                grdBilanFinancer.ItemsSource = lignes;
                grdBilanFinancer.Items.Refresh();
                totalAPayer = 0; totalPaye = 0; totalRemise = 0; totalReste = 0;
                foreach (LigneBilan l in lignes)
                {
                    totalAPayer += l.APayer;
                    totalPaye += l.paye;
                    totalRemise += l.remise;
                    totalReste += l.reste;
                }
                txtTotalAPayer.Text = totalAPayer.ToString("0,0", elGR);
                txtTotalDejaPaye.Text = totalPaye.ToString("0,0", elGR);
                txtTotalRemise.Text = totalRemise.ToString("0,0", elGR);
                txtResteAPayer.Text = totalReste.ToString("0,0", elGR);
            }
        }

        private void cmdImprimer_Click(object sender, RoutedEventArgs e)
        {
            CreerEtat etat = new CreerEtat("bilan financier-" + annee, "Bilan financier année " + (annee - 1) + "/" + annee);
            etat.bilanFinancier(grdBilanFinancer, totalAPayer, totalPaye, totalRemise, totalReste);
        }

        private void cmdQuitter_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void txtAnneeScolaire_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Utilitaires.IsTextAllowed(e.Text);
        }

        private void txtAnneeScolaire_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                CultureInfo elGR = CultureInfo.CreateSpecificCulture("el-GR");
                try
                {
                    annee = Convert.ToInt32(txtAnneeScolaire.Text) + 1;
                    txtAnnee.Text = " / " + annee.ToString();

                    lignes = new List<LigneBilan>();
                    grdBilanFinancer.ItemsSource = lignes;
                    grdBilanFinancer.Items.Refresh();
                    totalAPayer = 0;
                    totalPaye = 0;
                    totalRemise = 0;
                    totalReste = 0;
                    txtTotalAPayer.Text = "";
                    txtTotalDejaPaye.Text = "";
                    txtTotalRemise.Text = "";
                    txtResteAPayer.Text = "";

                    lignes = bilanBL.obtenirBilanFinancier(annee);
                    if (lignes != null)
                    {
                        grdBilanFinancer.ItemsSource = lignes;
                        grdBilanFinancer.Items.Refresh();
                        totalAPayer = 0; totalPaye = 0; totalRemise = 0; totalReste = 0;
                        foreach (LigneBilan l in lignes)
                        {
                            totalAPayer += l.APayer;
                            totalPaye += l.paye;
                            totalRemise += l.remise;
                            totalReste += l.reste;
                        }
                        txtTotalAPayer.Text = totalAPayer.ToString("0,0", elGR);
                        txtTotalDejaPaye.Text = totalPaye.ToString("0,0", elGR);
                        txtTotalRemise.Text = totalRemise.ToString("0,0", elGR);
                        txtResteAPayer.Text = totalReste.ToString("0,0", elGR);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("L'annee doit etre un nombre positif", "School brain:Alerte");
                }
            }
        }
    }
}
