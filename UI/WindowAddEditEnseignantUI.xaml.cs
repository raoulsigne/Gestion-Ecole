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

using Microsoft.Win32;

using Ecole.BusinessEntity;
using Ecole.BusinessLogic;
using Ecole.Utilitaire;

namespace Ecole.UI
{
    /// <summary>
    /// Interaction logic for WindowAddEditEnseignantUI.xaml
    /// </summary>
    public partial class WindowAddEditEnseignantUI : Window
    {
        CreerModifierEnseignantBL creerModifierEnseignantBL;
        private int etat; // indique si nous sommes en création (0) ou en modification (1)

        private EnseignantBE ancienEnseignant; //sera utile pour la mise à jour

        // Définition d'une liste 'ListeEnseignants' observable de 'Enseignant'
        public ObservableCollection<EnseignantBE> ListeEnseignants { get; set; }

        string photo;

        // Fonction permettant de remplir le DataGrid avec les informations de la base de données
        // @param : - listObjet : la liste des objets à afficher dans le DataGrid
        public void RemplirDataGrid(List<EnseignantBE> listObjet)
        {
            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "yyyy-MM-dd";
            Thread.CurrentThread.CurrentCulture = ci;

            dpkDateDepart.Text = System.DateTime.Now.ToString();

            // Ajout de données dans la DataTable :
            var table = new DataTable();

            table.Columns.Add(new DataColumn("codeprof", typeof(string)));
            table.Columns.Add(new DataColumn("nomProf", typeof(string)));
            table.Columns.Add(new DataColumn("dateNaissance", typeof(string)));
            table.Columns.Add(new DataColumn("dateNaissanceString", typeof(string)));
            table.Columns.Add(new DataColumn("tel", typeof(string)));
            table.Columns.Add(new DataColumn("email", typeof(string)));
            table.Columns.Add(new DataColumn("ville", typeof(string)));

            table.Columns.Add(new DataColumn("dateEmbauche", typeof(string)));
            table.Columns.Add(new DataColumn("dateDepart", typeof(string)));
            table.Columns.Add(new DataColumn("dateEmbaucheString", typeof(string)));
            table.Columns.Add(new DataColumn("dateDepartString", typeof(string)));
            table.Columns.Add(new DataColumn("statut", typeof(string)));

            if (listObjet != null)
            {

                for (int i = 0; i < listObjet.Count; i++)
                {
                    DataRow dr = table.NewRow();
                    dr["codeprof"] = listObjet.ElementAt(i).codeProf;
                    dr["nomProf"] = listObjet.ElementAt(i).nomProf;
                    dr["dateNaissance"] = listObjet.ElementAt(i).dateNaissance;
                    dr["dateNaissanceString"] = listObjet.ElementAt(i).dateNaissanceString;
                    dr["tel"] = listObjet.ElementAt(i).tel;
                    dr["email"] = listObjet.ElementAt(i).email;
                    dr["ville"] = listObjet.ElementAt(i).ville;

                    dr["dateEmbauche"] = listObjet.ElementAt(i).dateEmbauche;
                    dr["dateDepart"] = listObjet.ElementAt(i).dateDepart;

                    dr["dateEmbaucheString"] = listObjet.ElementAt(i).dateEmbaucheString;
                    dr["dateDepartString"] = listObjet.ElementAt(i).dateDepartString;

                    dr["statut"] = listObjet.ElementAt(i).statut;

                    table.Rows.Add(dr);
                }
            }

            string vCodeprof = txtCodeProf.Text;
            string vNom = "";
            string vDateNaiss = "";
            string vTel = "";
            string vEmail = "";
            string vVille = "";

            string vDateEmbauche ="" ;
            string vDateDepart = "";
            string vStatut = "";

            ListeEnseignants.Clear();

            //Personnes_Table = LoadDataTable();

            foreach (DataRow row in table.Rows)
            {
                vCodeprof = Convert.ToString(row["codeprof"]);
                vNom = Convert.ToString(row["nomProf"]);
                vDateNaiss = Convert.ToString(row["dateNaissance"]);
                vTel = Convert.ToString(row["tel"]);
                vEmail = Convert.ToString(row["email"]);
                vVille = Convert.ToString(row["ville"]);

                vDateEmbauche = Convert.ToString(row["dateEmbauche"]);
                vDateDepart = Convert.ToString(row["dateDepart"]);
                vStatut = Convert.ToString(row["statut"]);

                EnseignantBE enseignant = new EnseignantBE(vCodeprof, vNom, Convert.ToDateTime(vDateNaiss), vTel, vEmail, vVille, Convert.ToDateTime(vDateEmbauche), Convert.ToDateTime(vDateDepart));
                enseignant.statut = vStatut;

                ListeEnseignants.Add(enseignant);

            }

            grdListeEnseignant.ItemsSource = ListeEnseignants;
        }

        public WindowAddEditEnseignantUI()
        {
            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";
            Thread.CurrentThread.CurrentCulture = ci;

            InitializeComponent();

            creerModifierEnseignantBL = new CreerModifierEnseignantBL();

            ancienEnseignant = new EnseignantBE();

            etat = 0;          

            // Initialisation de la collection, qui va s'afficher dans la DataGrid :
            ListeEnseignants = new ObservableCollection<EnseignantBE>();
            List<EnseignantBE> LEnseignantBE = creerModifierEnseignantBL.listerTousLesEnseignants();

            dpkDateEmbauche.Text = Convert.ToString(System.DateTime.Today.Date);
            dpkdateNaiss.Text = Convert.ToString(System.DateTime.Today.Date);

            // on met la liste "LSerieBE" dans le DataGrid
            RemplirDataGrid(LEnseignantBE);

            //-------------MOI----------------------------------------------------
            txtCodeProf.IsEnabled = false;
            txtCodeProf.Text = nouveauMatricule(creerModifierEnseignantBL.getDernierMatricule());
            //--------------------------------------------------------------------

            List<String> ListStatut = new List<String>();
            ListStatut.Add("Permanent");
            ListStatut.Add("Vacataire");
            cmbStatut.ItemsSource = ListStatut;
            cmbStatut.Text = ListStatut.ElementAt(0);

            photo = ""; //Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\PhotosEnseignants\\default.jpg";
        }


        private void cmdParcourir_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Choisir la photo";
            openFileDialog.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                                    "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                                    "Portable Network Graphic (*.png)|*.png";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (openFileDialog.ShowDialog() == true)
            {
                photo = openFileDialog.FileName;
                try
                {
                    imgPhoto.Source = new BitmapImage(new Uri(photo));
                }
                catch (Exception) { imgPhoto.Source = null; }
            }

        }

        private void cmdEnregistrer_Click(object sender, RoutedEventArgs e)
        {
            if ((txtCodeProf.Text != null && txtNom.Text != null && txtTel.Text != null && cmbStatut.Text != null)
                && (txtCodeProf.Text != "" && txtNom.Text != "" && txtTel.Text != "" && cmbStatut.Text != ""))
            {
                EnseignantBE enseignant = new EnseignantBE();
                enseignant.codeProf = txtCodeProf.Text;
                enseignant.nomProf = txtNom.Text;
                enseignant.dateNaissance = Convert.ToDateTime(dpkdateNaiss.Text);
                
                enseignant.tel = txtTel.Text;
                enseignant.email = txtEmail.Text;
                enseignant.ville = txtVille.Text;
                enseignant.dateEmbauche = Convert.ToDateTime(dpkDateEmbauche.Text);
                enseignant.statut = cmbStatut.Text;

                //copie de la photo de l'enseignant
                try
                {
                    string destfile = System.IO.Path.Combine(ConnexionUI.DOSSIER_PHOTO, enseignant.codeProf + "." + photo.Split('.')[1]);
                    System.IO.File.Copy(photo, destfile, true);
                    photo = enseignant.codeProf + "." + photo.Split('.')[1];
                    enseignant.photo = photo;
                }
                catch (Exception)
                {
                    photo = "";
                    enseignant.photo = "";
                }

                if (dpkDateDepart.Text == null || dpkDateDepart.Text == "")
                    enseignant.dateDepart = Convert.ToDateTime(null);
                else
                    enseignant.dateDepart = Convert.ToDateTime(dpkDateDepart.Text);

                if (etat == 1)
                {
                    creerModifierEnseignantBL.modifierEnseignant(ancienEnseignant, enseignant);
                    List<EnseignantBE> LEnseignantBE = creerModifierEnseignantBL.listerTousLesEnseignants();
                    // on met la liste "listeEnseignant" dans le DataGrid
                    RemplirDataGrid(LEnseignantBE);
                    txtCodeProf.Text = "";
                    txtNom.Text = "";
                    //dpkdateNaiss.Text = null;
                    txtTel.Text = "";
                    txtEmail.Text = "";
                    txtVille.Text = "";
                    //dpkDateEmbauche.Text = null;
                    //dpkDateEmbauche.Text = Convert.ToString(System.DateTime.Today.Date);
                    //dpkDateDepart.Text = null;
                    //cmbStatut.Text = "";
                    imgPhoto.Source = null;
                    etat = 0;

                    //-------------MOI----------------------------------------------------
                    txtCodeProf.Text = nouveauMatricule(creerModifierEnseignantBL.getDernierMatricule());
                    //--------------------------------------------------------------------
                }
                else if (creerModifierEnseignantBL.rechercherEnseignant(enseignant) == null)
                {
                    if (creerModifierEnseignantBL.creerEnseignant(txtCodeProf.Text, txtNom.Text, Convert.ToDateTime(dpkdateNaiss.SelectedDate), txtTel.Text, txtEmail.Text, txtVille.Text, Convert.ToDateTime(dpkDateEmbauche.SelectedDate), Convert.ToDateTime(dpkDateDepart.SelectedDate), cmbStatut.Text, enseignant.photo))
                    {
                        MessageBox.Show("Opération réussie");
                        txtCodeProf.Text = "";
                        txtNom.Text = "";
                        //dpkdateNaiss.Text = null;
                        txtTel.Text = "";
                        txtEmail.Text = "";
                        txtVille.Text = "";

                        //dpkDateEmbauche.Text = null;
                        //dpkDateEmbauche.Text = Convert.ToString(System.DateTime.Today.Date);

                        //dpkDateDepart.Text = null;

                        //cmbStatut.Text = "";
                        imgPhoto.Source = null;

                        // Initialisation de la collection, qui va s'afficher dans la DataGrid :
                        List<EnseignantBE> LEnseignantBE = creerModifierEnseignantBL.listerTousLesEnseignants();
                        // on met la liste "LEnseignantBE" dans le DataGrid
                        RemplirDataGrid(LEnseignantBE);

                        //-------------MOI----------------------------------------------------
                        txtCodeProf.Text = nouveauMatricule(creerModifierEnseignantBL.getDernierMatricule());
                        //--------------------------------------------------------------------
                    }
                    else MessageBox.Show("Echec enregistrement : une erreure est survenue !");
                }
                else MessageBox.Show("Cet enseignant existe deja dans le système  !");
            }
            else MessageBox.Show("Tous les champs marqués par un Astérix '(*)' doivent êtres remplis !");
        }

        private void cmdAnnuler_Click(object sender, RoutedEventArgs e)
        {
            txtCodeProf.Text = "";
            txtNom.Text = "";
            dpkdateNaiss.Text = null;
            txtTel.Text = "";
            txtEmail.Text = "";
            txtVille.Text = "";

            cmbStatut.Text = "";
            imgPhoto.Source = null;

            //dpkDateEmbauche.Text = null;
            dpkDateEmbauche.Text = Convert.ToString(System.DateTime.Today.Date);

            dpkDateDepart.Text = null;

            etat = 0;
        }

        private void grdListeEnseignant_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                if (grdListeEnseignant.SelectedIndex != -1)
                {
                    if (MessageBox.Show("Voulez-vous vraiment le supprimer ? ", "School : Confimation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        if (creerModifierEnseignantBL.supprinerEnseignant(ListeEnseignants.ElementAt(grdListeEnseignant.SelectedIndex)))
                            ListeEnseignants.RemoveAt(grdListeEnseignant.SelectedIndex);
                        grdListeEnseignant.ItemsSource = ListeEnseignants;

                    }

                    grdListeEnseignant.UnselectAll();
                }

            }
        }

        private void grdListeEnseignant_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (grdListeEnseignant.SelectedIndex != -1)
            {
                etat = 1;
                EnseignantBE enseignant = new EnseignantBE();

                enseignant = creerModifierEnseignantBL.rechercherEnseignant(ListeEnseignants.ElementAt(grdListeEnseignant.SelectedIndex));
                // on charge les informations dans le formulaire
                //txtCode.Text = enseignant.id;
                txtCodeProf.Text = enseignant.codeProf;
                txtNom.Text = enseignant.nomProf;
                dpkdateNaiss.Text = Convert.ToString(enseignant.dateNaissance);
                txtTel.Text = enseignant.tel;
                txtEmail.Text = enseignant.email;
                txtVille.Text = enseignant.ville;
                dpkDateEmbauche.Text = Convert.ToString(enseignant.dateEmbauche);
                dpkDateDepart.Text = Convert.ToString(enseignant.dateDepart);

                cmbStatut.Text = enseignant.statut;

                try
                {
                    photo = enseignant.photo;
                    imgPhoto.Source = new BitmapImage(new Uri(ConnexionUI.DOSSIER_PHOTO + enseignant.photo));
                }
                catch (Exception) { imgPhoto.Source = null; }

                ancienEnseignant = enseignant;

                grdListeEnseignant.UnselectAll();
            }
        }

        private void txtTel_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Utilitaires.formatTelephone(e.Text);
        }

        private void cmdFermer_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cmdImprimer_Click(object sender, RoutedEventArgs e)
        {
            CreerEtat etat = new CreerEtat("Liste Enseignants -" + DateTime.Today.ToShortDateString(), "Liste des Enseignants");
            etat.obtenirEtat(grdListeEnseignant);
        }

        //-------------MOI----------------------------------------------------------------
        //--------------------------------------------------------------------------------
        //------générer nouveau matricule----------------------------
        private string nouveauMatricule(string dernierMatricule)
        {

            string matricule = "";
            int annee;
            string lettres;
            int numero;

            if (dernierMatricule == "" || dernierMatricule == null)
            {
                matricule = DateTime.Today.Year.ToString().Substring(2, 2) + "EN001";

                return matricule;
            }
            else
            {
                try
                {
                    // matricule = dernierMatricule.Substring(0, 2).ToString();
                    annee = Convert.ToInt32(dernierMatricule.Substring(0, 2).ToString());
                    lettres = dernierMatricule.Substring(2, 2).ToString();
                    numero = Convert.ToInt32(dernierMatricule.Substring(4, 3).ToString());

                    if (annee != Convert.ToInt32(DateTime.Today.Year.ToString().Substring(2, 2)))
                        matricule = DateTime.Today.Year.ToString().Substring(2, 2) + "EN001";

                    else
                    {
                        // if (numero < 999)
                        numero = numero + 1;

                        //else
                        //{
                        //    numero = 1;
                        //    lettres = Convert.ToString(int(lettres) + 1);
                        //}

                        matricule = DateTime.Today.Year.ToString().Substring(2, 2) + lettres + formaterNumeroMatricule(numero);
                    }


                    return matricule;

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }
            }
        }

        //-------------formater numero du matricule----------------

        private string formaterNumeroMatricule(int numero)
        {

            string numeroFormate = "";
            if (numero < 10)
                numeroFormate = "00" + numero.ToString();
            else if (numero > 9 && numero < 100)
                numeroFormate = "0" + numero.ToString();
            else if (numero > 99 && numero < 1000)
                numeroFormate = numero.ToString();

            return numeroFormate;
        }

    }
}
