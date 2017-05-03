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

using System.Collections.ObjectModel;
using System.Data;

using System.Globalization;
using System.Threading;

using Ecole.BusinessEntity;
using Ecole.BusinessLogic;
using Ecole.Utilitaire;

namespace Ecole.UI
{
    /// <summary>
    /// Interaction logic for WindowDefinirMentionUI.xaml
    /// </summary>
    public partial class WindowDefinirMentionUI : Window
    {
        DefinirMentionBL definirMentionBL;

        private int etat; // idique si nous sommes en création (0) ou en modification (1)

        MentionBE oldMention;

        private int etatGrid; // mémorise dans quel état se trouvait la liste (0 pour vide et 1 si elle contenait des infos)

        private int indexAmodifier; // garde l'index de l'élément à modifier

        // Définition d'une liste 'ListeMentions' observable de 'Mention'
        public ObservableCollection<MentionBE> ListeMentions { get; set; }

        //on défini une liste (pour pouvoir controler après que le min de i est égale au max de i-1)
        List<MentionBE> LMention;

        // Fonction permettant de remplir le DataGrid avec les informations de la base de données
        // @param : - listObjet : la liste des objets à afficher dans le DataGrid
        public void RemplirDataGrid(List<MentionBE> listObjet)
        {
            // Ajout de données dans la DataTable :
            var table = new DataTable();

            table.Columns.Add(new DataColumn("idMention", typeof(string)));
            table.Columns.Add(new DataColumn("noteMin", typeof(string)));
            table.Columns.Add(new DataColumn("noteMax", typeof(string)));
            table.Columns.Add(new DataColumn("mention", typeof(string)));
            
            if (listObjet != null)
            {

                for (int i = 0; i < listObjet.Count; i++)
                {
                    DataRow dr = table.NewRow();
                    dr["idMention"] = listObjet.ElementAt(i).idMention;
                    dr["noteMin"] = listObjet.ElementAt(i).noteMin;
                    dr["noteMax"] = listObjet.ElementAt(i).noteMax;
                    dr["mention"] = listObjet.ElementAt(i).mention;
                    
                    table.Rows.Add(dr);
                }
            }

            int vId = 0;
            double vNoteMin = 0;
            double vNoteMax = 0;
            string vMention = "";

            ListeMentions.Clear();

            foreach (DataRow row in table.Rows)
            {
                vId = Convert.ToInt16(row["idMention"]);
                vNoteMin = Convert.ToDouble(row["noteMin"]);
                vNoteMax = Convert.ToDouble(row["noteMax"]);
                vMention = Convert.ToString(row["mention"]);


                MentionBE mention = new MentionBE(vId, vNoteMin, vNoteMax, vMention);

                ListeMentions.Add(mention);

            }

            grdListeMention.ItemsSource = ListeMentions;
        }

        public WindowDefinirMentionUI()
        {
            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";
            Thread.CurrentThread.CurrentCulture = ci;

            InitializeComponent();

            definirMentionBL = new DefinirMentionBL();

            LMention = new List<MentionBE>();

            etat = 0;

            etatGrid = 0;
            // A mettre pour que le binding avec le DataGrid fonctionne !
            grdListeMention.DataContext = this;

            // Initialisation de la collection, qui va s'afficher dans la DataGrid :
            ListeMentions = new ObservableCollection<MentionBE>();

            oldMention = new MentionBE();

            //on charge la liste des mentions deja défines dans le dataGrid
            List<MentionBE> ListMention = definirMentionBL.listerToutesLesMentions();
            RemplirDataGrid(ListMention);

            LMention = ListMention;
        }

        private void txtNoteMin_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Utilitaires.IsTextAllowed(e.Text);
        }

        private void txtNoteMax_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Utilitaires.IsTextAllowed(e.Text);
        }

        private void cmdAnnuler_Click(object sender, RoutedEventArgs e)
        {
            txtNoteMin.Text = "";
            txtNoteMax.Text = "";
            txtMention.Text = "";

            etat = 0;
        }

        private void cmdAjouter_Click(object sender, RoutedEventArgs e)
        {
            if((txtNoteMin.Text != null && txtNoteMax.Text != null && txtMention.Text != null) && 
                (txtNoteMin.Text != "" && txtNoteMax.Text != "" && txtMention.Text != "")){

                MentionBE mention = new MentionBE(0, Convert.ToDouble(txtNoteMin.Text), Convert.ToDouble(txtNoteMax.Text), txtMention.Text);

                if (etat == 1)
                { //modification
                    //on vérifit si la noteMin est égale à la noteMax du dernier élément dans la liste
                    if (LMention != null)
                    {
                        if (LMention.Count == 0)
                        {
                            definirMentionBL.creerMention(mention.idMention, mention.noteMin, mention.noteMax, mention.mention);

                            txtNoteMin.Text = "";
                            txtNoteMax.Text = "";
                            txtMention.Text = "";

                            etat = 0;
                        }
                        else
                        {
                            int nb = LMention.Count; //le nombre d'élèment dans la liste
                            if (mention.noteMin == LMention.ElementAt(nb - 2).noteMax)
                            {
                                definirMentionBL.modifierMention(oldMention, mention);

                                txtNoteMin.Text = "";
                                txtNoteMax.Text = "";
                                txtMention.Text = "";

                                etat = 0;
                            }
                            else MessageBox.Show("Echec : 'Note Min' doit être égale à 'Note Max' du dernier enregistrement ! \n Soit : " + LMention.ElementAt(nb - 2).noteMax);
                        }
                    }
                    else
                    {
                        definirMentionBL.creerMention(mention.idMention, mention.noteMin, mention.noteMax, mention.mention);

                        txtNoteMin.Text = "";
                        txtNoteMax.Text = "";
                        txtMention.Text = "";

                        etat = 0;
                    }
                }
                else { //ajout
                    //on vérifit si la noteMin est égale à la noteMax du dernier élément dans la liste
                    if (LMention != null)
                    {
                        if (LMention.Count == 0)
                        {
                            definirMentionBL.creerMention(mention.idMention, mention.noteMin, mention.noteMax, mention.mention);

                            txtNoteMin.Text = "";
                            txtNoteMax.Text = "";
                            txtMention.Text = "";

                            etat = 0;
                        }
                        else
                        {
                            int nb = LMention.Count; //le nombre d'élèment dans la liste
                            if (mention.noteMin == LMention.ElementAt(nb - 1).noteMax)
                            {
                                definirMentionBL.creerMention(mention.idMention, mention.noteMin, mention.noteMax, mention.mention);

                                txtNoteMin.Text = "";
                                txtNoteMax.Text = "";
                                txtMention.Text = "";

                                etat = 0;
                            }
                            else MessageBox.Show("Echec : 'Note Min' doit être égale à 'Note Max' du dernier enregistrement ! \n Soit : " + LMention.ElementAt(nb - 1).noteMax);
                        }
                    }
                    else
                    {
                        definirMentionBL.creerMention(mention.idMention, mention.noteMin, mention.noteMax, mention.mention);

                        txtNoteMin.Text = "";
                        txtNoteMax.Text = "";
                        txtMention.Text = "";

                        etat = 0;
                    }
                }
                
                //on charge la liste des mentions deja défines dans le dataGrid
                List<MentionBE> ListMention = definirMentionBL.listerToutesLesMentions();
                RemplirDataGrid(ListMention);

                LMention = ListMention;
            }
        }

        private void cmdFermer_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void grdListeMention_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (grdListeMention.SelectedIndex != -1) {

                if (grdListeMention.SelectedIndex == ListeMentions.Count - 1)
                {
                    oldMention = ListeMentions.ElementAt(grdListeMention.SelectedIndex);

                    txtNoteMin.Text = Convert.ToString(oldMention.noteMin);
                    txtNoteMax.Text = Convert.ToString(oldMention.noteMax);
                    txtMention.Text = oldMention.mention;

                    etat = 1;
                }
                else MessageBox.Show("Impossible de modifier cet élément ! \n \n Vous ne pouvez modifier que le dernier élément de la liste !");

                grdListeMention.UnselectAll();
            }
        }

        private void grdListeMention_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                if (MessageBox.Show("Voulez-vous vraiment le supprimer ? ", "School : Confimation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    if (grdListeMention.SelectedIndex == ListeMentions.Count - 1)
                    {
                        MentionBE mention = ListeMentions.ElementAt(grdListeMention.SelectedIndex);

                        definirMentionBL.supprinerMention(mention);

                        List<MentionBE> List = definirMentionBL.listerToutesLesMentions();
                        RemplirDataGrid(List);

                        LMention = List;

                        txtNoteMin.Text = "";
                        txtNoteMax.Text = "";
                        txtMention.Text = "";
                    }
                    else MessageBox.Show("Impossible de supprimer cet élément ! \n \n Vous ne pouvez supprimer que le dernier élément de la liste !");

                }

                grdListeMention.UnselectAll();
            }
        }

        private void cmdImprimer_Click(object sender, RoutedEventArgs e)
        {
            CreerEtat etat = new CreerEtat("Mentions -" + DateTime.Today.ToShortDateString(), "Liste des Mentions");
            etat.obtenirEtat(grdListeMention);
        }
    }
}
