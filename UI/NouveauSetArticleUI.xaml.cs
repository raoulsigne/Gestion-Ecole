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

namespace Ecole.UI
{
    /// <summary>
    /// Interaction logic for NouveauSetArticleUI.xaml
    /// </summary>
    public partial class NouveauSetArticleUI : Window
    {
        public string code { get; set; }
        public string designation { get; set; }
        public decimal montant { get; set; }

        public NouveauSetArticleUI()
        {
            InitializeComponent();
        }

        private void cmdValider_Click(object sender, RoutedEventArgs e)
        {
            code = txtCode.Text;
            designation = txtDesignation.Text;
            montant = Convert.ToDecimal(txtMontant.Text);
            this.Close();
        }

        private void cmdAnnuler_Click(object sender, RoutedEventArgs e)
        {
            code = "";
            designation = "";
            montant = 0;
            txtDesignation.Text = "";
            txtMontant.Text = "";
            txtCode.Text = "";

            this.Close();
        }
        
    }
}
