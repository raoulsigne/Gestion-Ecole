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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Ecole.BusinessLogic;
using Ecole.BusinessEntity;

namespace Ecole.UI
{
    /// <summary>
    /// Interaction logic for Etape1CreationClasse.xaml
    /// </summary>
    public partial class Etape1CreationClasse : UserControl
    {
        AssistanceCreationClasseBL assistanceBL;

        public string choix { get; set; }
        public string codecycle { get; set; }
        public string nomcycle { get; set; }

        List<string> listeDeChoix;

        public Etape1CreationClasse()
        {
            InitializeComponent();
            txtCode.IsEnabled = false;
            txtNom.IsEnabled = false;
            assistanceBL = new AssistanceCreationClasseBL();
            listeDeChoix = new List<string>();
            listeDeChoix = assistanceBL.listerValeurColonneCycle("codecycle");
            listeDeChoix.Add(AssistanceCreationClasseBL.CHOIX_NOUVEAU);
            cmbChoisir.ItemsSource = listeDeChoix;
        }

        public Etape1CreationClasse(string codecycle, string nomcycle)
        {
            InitializeComponent();
            listeDeChoix = new List<string>();
            assistanceBL = new AssistanceCreationClasseBL();

            listeDeChoix = assistanceBL.listerValeurColonneCycle("codecycle");
            listeDeChoix.Add(AssistanceCreationClasseBL.CHOIX_NOUVEAU);
            cmbChoisir.ItemsSource = listeDeChoix;
            txtCode.Text = codecycle;
            txtNom.Text = nomcycle;
            cmbChoisir.Text = codecycle;
            this.nomcycle = nomcycle;
            this.codecycle = codecycle;

            if (!listeDeChoix.Contains(codecycle))
            {
                cmbChoisir.Text = AssistanceCreationClasseBL.CHOIX_NOUVEAU;
                txtCode.IsEnabled = true;
                txtNom.IsEnabled = true;
            }
            else
            {
                cmbChoisir.Text = codecycle;
                txtCode.IsEnabled = false;
                txtNom.IsEnabled = false;
            }
        }

        private void cmbChoisir_DropDownClosed(object sender, EventArgs e)
        {
            if (cmbChoisir.Text == AssistanceCreationClasseBL.CHOIX_NOUVEAU)
            {
                txtCode.IsEnabled = true;
                txtNom.IsEnabled = true;
                txtCode.Text = "";
                txtNom.Text = "";
            }
            else
            {
                codecycle = cmbChoisir.Text;
                CycleBE cycle = new CycleBE();
                cycle.codeCycle = codecycle;
                cycle = assistanceBL.rechercherCycle(cycle);
                if (cycle != null)
                {
                    txtCode.Text = cycle.codeCycle;
                    txtNom.Text = cycle.nomCycle;
                    nomcycle = cycle.nomCycle;
                    txtCode.IsEnabled = false;
                    txtNom.IsEnabled = false;
                }
            }
        }

        private void txtCode_LostFocus(object sender, RoutedEventArgs e)
        {
            codecycle = txtCode.Text;
        }

        private void txtNom_LostFocus(object sender, RoutedEventArgs e)
        {
            nomcycle = txtNom.Text;
        }
    }
}
