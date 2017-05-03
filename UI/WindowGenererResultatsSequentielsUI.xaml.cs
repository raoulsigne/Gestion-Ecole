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

using Ecole.BusinessLogic;
using Ecole.BusinessEntity;

namespace Ecole.UI
{
    /// <summary>
    /// Interaction logic for WindowGenererResultatsSequentielsUI.xaml
    /// </summary>
    public partial class WindowGenererResultatsSequentielsUI : Window
    {
        GenererResultatsSequentielsBL genererResultatsBL;

        int annee;

        private MoyennesBE ancienObjet; //garde l'ancien état de l'objet, qui sera utilisé pour la modification


        // Définition d'une liste 'ListeMoyennes' observable de 'Moyenne'
        public ObservableCollection<MoyennesBE> ListeMoyennes { get; set; }

        // Fonction permettant de remplir le DataGrid avec les informations de la base de données
        // @param : - listObjet : la liste des objets à afficher dans le DataGrid
        public void RemplirDataGrid(List<MoyennesBE> listObjet)
        {
            // Ajout de données dans la DataTable :
            var table = new DataTable();

            table.Columns.Add(new DataColumn("codeMat", typeof(string)));
            table.Columns.Add(new DataColumn("codeSeq", typeof(string)));
            table.Columns.Add(new DataColumn("matricule", typeof(string)));
            table.Columns.Add(new DataColumn("moyenne", typeof(string)));
            table.Columns.Add(new DataColumn("annee", typeof(string)));
            table.Columns.Add(new DataColumn("rang", typeof(string)));

            table.Columns.Add(new DataColumn("moyenneClasse", typeof(string)));
            table.Columns.Add(new DataColumn("mention", typeof(string)));
            table.Columns.Add(new DataColumn("moyenneMin", typeof(string)));
            table.Columns.Add(new DataColumn("moyenneMax", typeof(string)));
            table.Columns.Add(new DataColumn("appreciation", typeof(string)));

            if (listObjet != null)
            {

                for (int i = 0; i < listObjet.Count; i++)
                {
                    DataRow dr = table.NewRow();
                    dr["codeMat"] = listObjet.ElementAt(i).codeMat;
                    dr["codeSeq"] = listObjet.ElementAt(i).codeSeq;
                    dr["matricule"] = listObjet.ElementAt(i).matricule;
                    dr["moyenne"] = listObjet.ElementAt(i).moyenne;
                    dr["annee"] = listObjet.ElementAt(i).annee;
                    dr["rang"] = listObjet.ElementAt(i).rang;

                    dr["moyenneClasse"] = listObjet.ElementAt(i).moyenneClasse;
                    dr["mention"] = listObjet.ElementAt(i).mention;
                    dr["moyenneMin"] = listObjet.ElementAt(i).moyenneMin;
                    dr["moyenneMax"] = listObjet.ElementAt(i).moyenneMax;
                    dr["appreciation"] = listObjet.ElementAt(i).appreciation;

                    table.Rows.Add(dr);
                }
            }

            string vcodeMat = "";
            string vcodeSeq = "";
            string vmatricule = "";
            Double vmoyenne = 0;
            int vannee = 0;
            int vrang = 0;

            double vMoyenneClasse = 0;
            string vMention = "";
            double vMoyenneMin = 0;
            double vMoyenneMax = 0;
            string vAppreciation = "";

            ListeMoyennes.Clear();

            //Personnes_Table = LoadDataTable();

            foreach (DataRow row in table.Rows)
            {
                vcodeMat = Convert.ToString(row["codeMat"]);
                vcodeSeq = Convert.ToString(row["codeSeq"]);
                vmatricule = Convert.ToString(row["matricule"]);
                vmoyenne = Convert.ToDouble(row["moyenne"]);
                vannee = Convert.ToInt16(row["annee"]);
                vrang = Convert.ToInt16(row["rang"]);

                vMoyenneClasse = Convert.ToDouble(row["moyenneClasse"]);
                vMention = Convert.ToString(row["mention"]);
                vMoyenneMin = Convert.ToDouble(row["moyenneMin"]); 
                vMoyenneMax = Convert.ToDouble(row["moyenneMax"]);
                vAppreciation = Convert.ToString(row["appreciation"]);

                ListeMoyennes.Add(new MoyennesBE(vcodeMat, vcodeSeq, vmatricule, vmoyenne, vannee, vrang, vMoyenneClasse, vMention, vMoyenneMin, vMoyenneMax, vAppreciation));

            }
        }

        //********************************** DEBUT des éléments utiles pour la barre de progréssion

        //Create a Delegate that matches 
        //the Signature of the ProgressBar's SetValue method
        private delegate void UpdateProgressBarDelegate(
                System.Windows.DependencyProperty dp, Object value);

        //********************************** Fin des éléments utiles pour la barre de progréssion

        public WindowGenererResultatsSequentielsUI()
        {
            InitializeComponent();

            genererResultatsBL = new GenererResultatsSequentielsBL();

            ancienObjet = new MoyennesBE();

            // Initialisation de la collection, qui va s'afficher dans la DataGrid :
            ListeMoyennes = new ObservableCollection<MoyennesBE>();
            List<MoyennesBE> LMoyennesBE = genererResultatsBL.listerToutesLesMoyennes();
            // on met la liste "ListeMoyennes" dans le DataGrid
            RemplirDataGrid(LMoyennesBE);

            List<ClasseBE> LClasse = genererResultatsBL.listerToutesLesClasses();
            // ------------------- Chargement de la liste des codes de Cycle dans le comboBox de la fenêtre 
            //(utile pour le filtre)
            cmbClasse.ItemsSource = genererResultatsBL.getListCodeClasse(LClasse);

            List<SequenceBE> LSequence = genererResultatsBL.listerToutesLesSequences();
            // ------------------- Chargement de la liste des noms de Cycle dans le comboBox de la fenêtre 
            //(utile pour le filtre)
            cmbSequence.ItemsSource = genererResultatsBL.getListCodeSequence2(LSequence);

            annee = genererResultatsBL.getAnneeEnCours();
            txtAnnee.Text = Convert.ToString(annee);
            txtAnneeScolaire.Text = (annee - 1).ToString();

            //on cache la barre de progression
            ProgressBar1.Visibility = System.Windows.Visibility.Hidden;
        }

        private void cmdFermer_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cmdAnnuler_Click(object sender, RoutedEventArgs e)
        {
            cmbClasse.Text = null;
            cmbSequence.Text = null;

            annee = genererResultatsBL.getAnneeEnCours();
            txtAnnee.Text = Convert.ToString(annee);
            txtAnneeScolaire.Text = (annee - 1).ToString();
        }

        private void cmdValider_Click(object sender, RoutedEventArgs e)
        {
            //on vérifit si tous les champs ont été corectement rempli
            if ((cmbClasse.Text != null && txtAnneeScolaire.Text != null && cmbSequence.Text != null) && (cmbClasse.Text != "" && txtAnneeScolaire.Text != "" && cmbSequence.Text != ""))
            {
                //si on a choisi <Toutes les classes>
                if (cmbClasse.Text.Equals("<Toutes Les Classes>"))
                {
                    if (cmbSequence.Text.Equals("<Toutes Les Séquences>"))
                    {
                        //--------------------------- Action pour toutes les classes et toutes les séquences

                        //on liste toutes les classes
                        List<ClasseBE> LClasse = genererResultatsBL.listerToutesLesClasses();

                        //on liste toutes les séquences
                        List<SequenceBE> LSequence = genererResultatsBL.listerToutesLesSequences();

                        //Configure the ProgressBar
                        ProgressBar1.Minimum = 0;
                        ProgressBar1.Maximum = LClasse.Count;
                        ProgressBar1.Value = 0;

                        //Stores the value of the ProgressBar
                        double value = 0;

                        //Create a new instance of our ProgressBar Delegate that points
                        // to the ProgressBar's SetValue method.
                        UpdateProgressBarDelegate updatePbDelegate =
                            new UpdateProgressBarDelegate(ProgressBar1.SetValue);

                        
                        //on affiche la barre de progression
                        ProgressBar1.Visibility = System.Windows.Visibility.Visible;

                        for (int i = 0; i < LClasse.Count; i++)
                        {
                            for (int j = 0; j < LSequence.Count; j++)
                            {
                                // ***********calcul des moyennes                            
                                //remplissage de la table "moyennes"
                                genererResultatsBL.calculerMoyenne(LClasse.ElementAt(i).codeClasse, LSequence.ElementAt(j).codeseq, Convert.ToInt16(txtAnnee.Text));

                                //calcul des résultats
                                //remplissage de la table "resultats"
                                genererResultatsBL.calculerResultats(LClasse.ElementAt(i).codeClasse, LSequence.ElementAt(j).codeseq, Convert.ToInt16(txtAnnee.Text));

                                value += 1;

                                Dispatcher.Invoke(updatePbDelegate,
                                    System.Windows.Threading.DispatcherPriority.Background,
                                    new object[] { ProgressBar.ValueProperty, value });
                            }
                        }

                        MessageBox.Show("Opération Terminée !! ");

                        //on cache la barre de progression
                        ProgressBar1.Visibility = System.Windows.Visibility.Hidden;
                    }
                    else
                    {
                        //--------------------------- Action pour toutes les classes et une séquence particulière

                        //on liste toutes les classes
                        List<ClasseBE> LClasse = genererResultatsBL.listerToutesLesClasses();

                        //Configure the ProgressBar
                        ProgressBar1.Minimum = 0;
                        ProgressBar1.Maximum = LClasse.Count;
                        ProgressBar1.Value = 0;

                        //Stores the value of the ProgressBar
                        double value = 0;

                        //Create a new instance of our ProgressBar Delegate that points
                        // to the ProgressBar's SetValue method.
                        UpdateProgressBarDelegate updatePbDelegate =
                            new UpdateProgressBarDelegate(ProgressBar1.SetValue);

                        //Tight Loop: Loop until the ProgressBar.Value reaches the max


                        //****************

                        //on affiche la barre de progression
                        ProgressBar1.Visibility = System.Windows.Visibility.Visible;

                        for (int i = 0; i < LClasse.Count; i++)
                        {
                            // ***********calcul des moyennes                            
                            //remplissage de la table "moyennes"
                            genererResultatsBL.calculerMoyenne(LClasse.ElementAt(i).codeClasse, cmbSequence.Text, Convert.ToInt16(txtAnnee.Text));

                            //calcul des résultats
                            //remplissage de la table "resultats"
                            genererResultatsBL.calculerResultats(LClasse.ElementAt(i).codeClasse, cmbSequence.Text, Convert.ToInt16(txtAnnee.Text));

                            value += 1;

                            Dispatcher.Invoke(updatePbDelegate,
                                System.Windows.Threading.DispatcherPriority.Background,
                                new object[] { ProgressBar.ValueProperty, value });
                        }

                        MessageBox.Show("Opération Terminée !! ");

                        //on cache la barre de progression
                        ProgressBar1.Visibility = System.Windows.Visibility.Hidden;
                    }
                }
                else {

                    if (cmbSequence.Text.Equals("<Toutes Les Séquences>"))
                    {
                        //--------------------------- Action pour une classe particulière et toutes les séquences

                        //on liste toutes les séquences
                        List<SequenceBE> LSequence = genererResultatsBL.listerToutesLesSequences();

                        //Configure the ProgressBar
                        ProgressBar1.Minimum = 0;
                        ProgressBar1.Maximum = LSequence.Count;
                        ProgressBar1.Value = 0;

                        //Stores the value of the ProgressBar
                        double value = 0;

                        //Create a new instance of our ProgressBar Delegate that points
                        // to the ProgressBar's SetValue method.
                        UpdateProgressBarDelegate updatePbDelegate =
                            new UpdateProgressBarDelegate(ProgressBar1.SetValue);

                        //Tight Loop: Loop until the ProgressBar.Value reaches the max

                        //on affiche la barre de progression
                        ProgressBar1.Visibility = System.Windows.Visibility.Visible;

                        for (int j = 0; j < LSequence.Count; j++)
                        {
                            // ***********calcul des moyennes                            
                            //remplissage de la table "moyennes"
                            genererResultatsBL.calculerMoyenne(cmbClasse.Text, LSequence.ElementAt(j).codeseq, Convert.ToInt16(txtAnnee.Text));

                            //calcul des résultats
                            //remplissage de la table "resultats"
                            genererResultatsBL.calculerResultats(cmbClasse.Text, LSequence.ElementAt(j).codeseq, Convert.ToInt16(txtAnnee.Text));

                            value += 1;

                            Dispatcher.Invoke(updatePbDelegate,
                                System.Windows.Threading.DispatcherPriority.Background,
                                new object[] { ProgressBar.ValueProperty, value });
                        }


                        MessageBox.Show("Opération Terminée !! ");

                        //on cache la barre de progression
                        ProgressBar1.Visibility = System.Windows.Visibility.Hidden;
                    }
                    else
                    {
                        //--------------------- Action pour une classe particulière et une séquence particulière

                        //Configure the ProgressBar
                        ProgressBar1.Minimum = 0;
                        ProgressBar1.Maximum = 1;
                        ProgressBar1.Value = 0;

                        //Stores the value of the ProgressBar
                        double value = 0;

                        //Create a new instance of our ProgressBar Delegate that points
                        // to the ProgressBar's SetValue method.
                        UpdateProgressBarDelegate updatePbDelegate =
                            new UpdateProgressBarDelegate(ProgressBar1.SetValue);

                        //Tight Loop: Loop until the ProgressBar.Value reaches the max

                        //on affiche la barre de progression
                        ProgressBar1.Visibility = System.Windows.Visibility.Visible;

                        // ***********calcul des moyennes                            
                        //remplissage de la table "moyennes"
                        genererResultatsBL.calculerMoyenne(cmbClasse.Text, cmbSequence.Text, Convert.ToInt16(txtAnnee.Text));

                        value += 1;

                        Dispatcher.Invoke(updatePbDelegate,
                            System.Windows.Threading.DispatcherPriority.Background,
                            new object[] { ProgressBar.ValueProperty, value });

                        //calcul des résultats
                        //remplissage de la table "resultats"
                        genererResultatsBL.calculerResultats(cmbClasse.Text, cmbSequence.Text, Convert.ToInt16(txtAnnee.Text));

                        MessageBox.Show("Opération Terminée !! ");

                        //on cache la barre de progression
                        ProgressBar1.Visibility = System.Windows.Visibility.Hidden;

                    }
                }


                List<MoyennesBE> LMoyenne = genererResultatsBL.listerToutesLesMoyennes();

                RemplirDataGrid(LMoyenne);
            }
            else MessageBox.Show("Tous les champs doivent êtres remplis !! ");
        }

        private void txtAnnee_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Utilitaires.IsTextAllowed(e.Text);
        }

        private void txtAnneeScolaire_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                annee = Convert.ToInt32(txtAnneeScolaire.Text) + 1;
                txtAnnee.Text = annee.ToString();

            }
            catch (Exception)
            {
                MessageBox.Show("L'annee doit etre un nombre positif", "School brain:Alerte");
            }
        }

        private void txtAnneeScolaire_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Utilitaires.IsTextAllowed(e.Text);
        }

    }
}
