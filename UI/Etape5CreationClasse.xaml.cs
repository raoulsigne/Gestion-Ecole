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

namespace Ecole.UI
{
    /// <summary>
    /// Interaction logic for Etape5CreationClasse.xaml
    /// </summary>
    public partial class Etape5CreationClasse : UserControl
    {
        AssistanceCreationClasseBL assistanceBL; 

        public string codeclasse { get; set; }
        public string nomclasse { get; set; }
        public string codeserie { get; set; }
        public string codecycle { get; set; }
        public string codeniveau { get; set; }
        public string codetypeclasse { get; set; }

        public Etape5CreationClasse()
        {
            InitializeComponent();
            assistanceBL = new AssistanceCreationClasseBL();
        }

        public Etape5CreationClasse(string codecycle, string codeniveau, string codeserie, string codetype)
        {
            InitializeComponent();
            assistanceBL = new AssistanceCreationClasseBL();
            txtCodeCycle.Text = codecycle;
            txtCodeNiveau.Text = codeniveau;
            txtCodeSerie.Text = codeserie;
            txtCodeType.Text = codetype;
        }

        private void txtCodeClasse_LostFocus(object sender, RoutedEventArgs e)
        {
            codeclasse = txtCodeClasse.Text;
        }

        private void txtNomClasse_LostFocus(object sender, RoutedEventArgs e)
        {
            nomclasse = txtNomClasse.Text;
        }
    }
}
