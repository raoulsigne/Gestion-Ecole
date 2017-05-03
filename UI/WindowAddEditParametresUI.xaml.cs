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
using Microsoft.Win32;

namespace Ecole.UI
{
    /// <summary>
    /// Interaction logic for WindowAddEditParametresUI.xaml
    /// </summary>
    public partial class WindowAddEditParametresUI : Window
    {
        CreerModifierParametresBL creerModifierParametresBL;
        private int etat; // idique si nous sommes en création (0) ou en modification (1)

        private ParametresBE ancienObjet; //garde l'ancien état de l'objet, qui sera utilisé pour la modification
        private string logo;
        private string nomLogo;

        // Définition d'une liste 'ListeCycles' observable de 'Paramètres'
        public ObservableCollection<ParametresBE> ListeParametres { get; set; }

        // Fonction permettant de remplir le DataGrid avec les informations de la base de données
        // @param : - listObjet : la liste des objets à afficher dans le DataGrid
        public void RemplirDataGrid(List<ParametresBE> listObjet)
        {
            // Ajout de données dans la DataTable :
            var table = new DataTable();

            table.Columns.Add(new DataColumn("idParametre", typeof(string)));
            table.Columns.Add(new DataColumn("nomEcole", typeof(string)));
            table.Columns.Add(new DataColumn("adresse", typeof(string)));
            table.Columns.Add(new DataColumn("tel", typeof(string)));
            table.Columns.Add(new DataColumn("fax", typeof(string)));
            table.Columns.Add(new DataColumn("email", typeof(string)));
            table.Columns.Add(new DataColumn("siteWeb", typeof(string)));
            table.Columns.Add(new DataColumn("directeur", typeof(string)));
            table.Columns.Add(new DataColumn("pays", typeof(string)));
            table.Columns.Add(new DataColumn("region", typeof(string)));
            table.Columns.Add(new DataColumn("ministere", typeof(string)));
            table.Columns.Add(new DataColumn("ministery", typeof(string)));
            table.Columns.Add(new DataColumn("country", typeof(string)));
            table.Columns.Add(new DataColumn("regionA", typeof(string)));
            table.Columns.Add(new DataColumn("annee", typeof(string)));
            table.Columns.Add(new DataColumn("departement", typeof(string)));
            table.Columns.Add(new DataColumn("department", typeof(string)));
            table.Columns.Add(new DataColumn("ville", typeof(string)));
            table.Columns.Add(new DataColumn("titreDuChef", typeof(string)));
            table.Columns.Add(new DataColumn("titleOfChief", typeof(string)));
            table.Columns.Add(new DataColumn("logo", typeof(string)));
            table.Columns.Add(new DataColumn("REPERTOIRE_PHOTO", typeof(string)));

            if (listObjet != null)
            {

                for (int i = 0; i < listObjet.Count; i++)
                {
                    DataRow dr = table.NewRow();
                    dr["idParametre"] = listObjet.ElementAt(i).idParametre;
                    dr["nomEcole"] = listObjet.ElementAt(i).nomEcole;
                    dr["adresse"] = listObjet.ElementAt(i).adresse;
                    dr["tel"] = listObjet.ElementAt(i).tel;
                    dr["fax"] = listObjet.ElementAt(i).fax;
                    dr["email"] = listObjet.ElementAt(i).email;
                    dr["siteWeb"] = listObjet.ElementAt(i).siteWeb;
                    dr["directeur"] = listObjet.ElementAt(i).directeur;
                    dr["pays"] = listObjet.ElementAt(i).pays;
                    dr["region"] = listObjet.ElementAt(i).region;
                    dr["ministere"] = listObjet.ElementAt(i).ministere;
                    dr["ministery"] = listObjet.ElementAt(i).ministery;
                    dr["country"] = listObjet.ElementAt(i).country;
                    dr["regionA"] = listObjet.ElementAt(i).regionA;
                    dr["annee"] = listObjet.ElementAt(i).annee;

                    dr["departement"] = listObjet.ElementAt(i).departement;
                    dr["department"] = listObjet.ElementAt(i).department;
                    dr["ville"] = listObjet.ElementAt(i).ville;
                    dr["titreDuChef"] = listObjet.ElementAt(i).titreDuChef;
                    dr["titleOfChief"] = listObjet.ElementAt(i).titleOfChief;
                    dr["logo"] = listObjet.ElementAt(i).logo;
                    dr["REPERTOIRE_PHOTO"] = listObjet.ElementAt(i).REPERTOIRE_PHOTO;
                    
                    table.Rows.Add(dr);
                }
            }

            string vID = "";
            string vnomEcole = "";
            string vadresse = "";
            string vtel = "";
            string vFax = "";
            string vemail = "";
            string vsiteWeb = "";
            string vdirecteur = "";
            string vpays = "";
            string vregion = "";
            string vministere = "";
            string vministery = "";
            string vcountry = "";
            string vregionA = "";
            string vannee = "";

            string vDepartement = "";
            string vDepartment = "";
            string vVille = "";
            string vTitreDuChef = "";
            string vTitleOfChief = "";
            string vLogo = "";
            string vREPERTOIRE_PHOTO = "";

            ListeParametres.Clear();

            //Personnes_Table = LoadDataTable();

            foreach (DataRow row in table.Rows)
            {
                vID = Convert.ToString(row["idParametre"]);
                vnomEcole = Convert.ToString(row["nomEcole"]);
                vadresse = Convert.ToString(row["adresse"]);
                vtel = Convert.ToString(row["tel"]);
                vFax = Convert.ToString(row["fax"]);
                vemail = Convert.ToString(row["email"]);
                vsiteWeb = Convert.ToString(row["siteWeb"]);
                vdirecteur = Convert.ToString(row["directeur"]);
                vpays = Convert.ToString(row["pays"]);
                vregion = Convert.ToString(row["region"]);
                vministere = Convert.ToString(row["ministere"]);
                vministery = Convert.ToString(row["ministery"]);
                vcountry = Convert.ToString(row["country"]);
                vregionA = Convert.ToString(row["regionA"]);
                vannee = Convert.ToString(row["annee"]);

                vDepartement = Convert.ToString(row["departement"]);
                vDepartment = Convert.ToString(row["department"]);
                vVille = Convert.ToString(row["ville"]);
                vTitreDuChef = Convert.ToString(row["titreDuChef"]);
                vTitleOfChief = Convert.ToString(row["titleOfChief"]); 
                vLogo = Convert.ToString(row["logo"]);
                vREPERTOIRE_PHOTO = Convert.ToString(row["REPERTOIRE_PHOTO"]);

                ListeParametres.Add(new ParametresBE(Convert.ToInt16(vID), vnomEcole, vadresse, vtel, vFax, vemail, vsiteWeb, vdirecteur, vpays, vregionA, vministere, vministery,
                    vcountry, vregionA, Convert.ToInt16(vannee), vDepartement, vDepartment, vVille, vTitreDuChef, vTitleOfChief, vLogo, vREPERTOIRE_PHOTO));

            }
        }

        public WindowAddEditParametresUI()
        {
            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";
            Thread.CurrentThread.CurrentCulture = ci;

            logo = "";
            nomLogo = "";
            InitializeComponent();

            creerModifierParametresBL = new CreerModifierParametresBL();

            etat = 0;

            ancienObjet = new ParametresBE();

            // A mettre pour que le binding avec le DataGrid fonctionne !
            grdListeParametre.DataContext = this;

            if (creerModifierParametresBL.listerTousLesParametres() != null && creerModifierParametresBL.listerTousLesParametres().Count != 0)
            {
                ParametresBE parametre = creerModifierParametresBL.listerTousLesParametres()[0];
                txtNomEcole.Text = parametre.nomEcole;
                txtAdresse.Text = parametre.adresse;
                txtTelephone.Text = parametre.tel;
                txtFax.Text = parametre.fax;
                txtEmail.Text = parametre.email;
                txtSiteWeb.Text = parametre.siteWeb;
                txtDirecteur.Text = parametre.directeur;
                txtPays.Text = parametre.pays;
                txtRegion.Text = parametre.region;
                txtMinistere.Text = parametre.ministere;
                txtMinistery.Text = parametre.ministery;
                txtCountry.Text = parametre.country;
                txtRegionA.Text = parametre.regionA;
                txtAnnee.Text = Convert.ToString(parametre.annee);
                txtAnneeScolaire.Text = (parametre.annee - 1) + " / " + parametre.annee;
                txtDepartemant.Text = parametre.departement;
                txtDepartment.Text = parametre.department;
                txtVille.Text = parametre.ville;
                txtTitreDuChef.Text = parametre.titreDuChef;
                txtTitleOfChief.Text = parametre.titleOfChief;

                txtRepertoirePhotos.Text = parametre.REPERTOIRE_PHOTO;

                ancienObjet = parametre;

                logo = parametre.logo;
                lblCheminLogo.Content = logo;
                //lblCheminLogo.PointFromScreen = System.p
                nomLogo = logo;

                try
                {
                    imgLogo.Source = new BitmapImage(new Uri(ConnexionUI.DOSSIER_IMAGES + parametre.logo));
                }
                catch (Exception) { imgLogo.Source = null; }

                etat = 1;
            }

            // Initialisation de la collection, qui va s'afficher dans la DataGrid :
            ListeParametres  = new ObservableCollection<ParametresBE>();
            List<ParametresBE> LParametresBE = creerModifierParametresBL.listerTousLesParametres();
            // on met la liste "LParametresBE" dans le DataGrid
            RemplirDataGrid(LParametresBE);

        }


        private void cmdFermer_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cmdValider_Click(object sender, RoutedEventArgs e)
        {
            if ((txtNomEcole.Text != null && txtAdresse.Text != null && txtTelephone.Text != null && txtFax.Text != null && txtAnnee.Text != null && txtEmail.Text != null &&
                txtDirecteur.Text != null && txtPays.Text != null && txtCountry.Text != null && txtDepartemant.Text != null && txtDepartment.Text != null && txtVille.Text != null
                && txtRegion.Text != null && txtRegionA.Text != null && txtMinistere.Text != null && txtMinistery.Text != null && txtSiteWeb.Text !=null && txtTitreDuChef.Text != null && txtTitleOfChief.Text != null && txtRepertoirePhotos.Text != null) &&
                (txtNomEcole.Text != "" && txtAdresse.Text != "" && txtTelephone.Text != "" && txtFax.Text != "" && txtAnnee.Text != "" && txtEmail.Text != "" && txtDirecteur.Text != "" &&
                txtPays.Text != "" && txtCountry.Text != "" && txtDepartemant.Text != "" && txtDepartment.Text != "" && txtVille.Text != "" && txtRegion.Text != "" && txtRegionA.Text != "" &&
                txtMinistere.Text != "" && txtMinistery.Text != "" && txtSiteWeb.Text != "" && txtTitreDuChef.Text != "" && txtTitleOfChief.Text != "" && txtRepertoirePhotos.Text != ""))
            { // si tous les champs sont non vides

                ParametresBE parametre = new ParametresBE();
                parametre.idParametre = 0;
                parametre.nomEcole = txtNomEcole.Text;
                parametre.adresse = txtAdresse.Text;
                parametre.tel = txtTelephone.Text;
                parametre.fax = txtFax.Text;
                parametre.email = txtEmail.Text;
                parametre.siteWeb = txtSiteWeb.Text;
                parametre.directeur = txtDirecteur.Text;
                parametre.pays = txtPays.Text;
                parametre.country = txtCountry.Text;
                parametre.region = txtRegion.Text;
                parametre.ministere = txtMinistere.Text;
                parametre.ministery = txtMinistery.Text;
                parametre.regionA = txtRegionA.Text;
                parametre.annee = Convert.ToInt16(txtAnnee.Text);

                parametre.departement = txtDepartemant.Text;
                parametre.department = txtDepartment.Text;
                parametre.ville = txtVille.Text;
                parametre.titreDuChef = txtTitreDuChef.Text;
                parametre.titleOfChief = txtTitleOfChief.Text;

                parametre.logo = nomLogo;

                parametre.REPERTOIRE_PHOTO = txtRepertoirePhotos.Text;

               
                ////***************** debut création du repertoire image
                try
                {
                    GestionRepertoire.CreateDirectory(ConnexionUI.DOSSIER_IMAGES);
                    GestionRepertoire.CreateDirectory(ConnexionUI.DOSSIER_PHOTO);
                    GestionRepertoire.CreateDirectory(ConnexionUI.DOSSIER_ETATS);
                    GestionRepertoire.CreateDirectory(ConnexionUI.DOSSIER_BULLETINS);


                    //GestionRepertoire.DirectoryCopy(@"\\YUYA-PC\\Users\\YUYA\\Desktop\\Photos2\\Images", @"" + txtRepertoirePhotos.Text, true);
                    //GestionRepertoire.DirectoryCopy(@"\\YUYA-PC\\Users\\YUYA\\Desktop\\Photos2\\Photos", @"" + txtRepertoirePhotos.Text, true);
                    //GestionRepertoire.DirectoryCopy(@"\\YUYA-PC\\Users\\YUYA\\Desktop\\Photos2\\Etats", @"" + txtRepertoirePhotos.Text, true);
                }
                catch (Exception) { }
                ////***************** FIN création du repertoire image


                ////copie du logo de l'etablissement
                try
                {
                    string destfile = System.IO.Path.Combine(ConnexionUI.DOSSIER_IMAGES, "logo." + logo.Split('.')[1]);
                    imgLogo.Source = null;
                    //imgLogo = new Image();
                    //System.IO.File.Delete(destfile);
                    System.IO.File.Copy(logo, destfile, true);
                    logo = "logo." + logo.Split('.')[1];
                    parametre.logo = logo;
                }
                catch (Exception ex)
                { MessageBox.Show(ex.Message); 
                }

                

                if (etat == 1)
                {
                    if (ancienObjet != null) {
                        if (ancienObjet.REPERTOIRE_PHOTO != parametre.REPERTOIRE_PHOTO)
                        {
                            try
                            {

                                //****************on teste si le répertoire photo là existe
                                if (GestionRepertoire.existDirectory(@"" + txtRepertoirePhotos.Text))
                                {
                                    //si le chemin du repertoire photo a changé (on déplace le repertoire)
                                    //***************** debut création du repertoire image

                                    //ConnexionUI.DOSSIER_PHOTO = ancienObjet.REPERTOIRE_PHOTO + "Photos";
                                    //ConnexionUI.DOSSIER_IMAGES = ancienObjet.REPERTOIRE_PHOTO + "Images";
                                    //ConnexionUI.DOSSIER_ETATS = ancienObjet.REPERTOIRE_PHOTO + "Etats";

                                    //GestionRepertoire.CreateDirectory(ConnexionUI.DOSSIER_IMAGES);
                                    //GestionRepertoire.CreateDirectory(ConnexionUI.DOSSIER_PHOTO);
                                    //GestionRepertoire.CreateDirectory(ConnexionUI.DOSSIER_ETATS);

                                    GestionRepertoire.DirectoryCopy(@"" + ancienObjet.REPERTOIRE_PHOTO, @"" + txtRepertoirePhotos.Text, true);
                                    GestionRepertoire.DirectoryCopy(@"" + ancienObjet.REPERTOIRE_PHOTO, @"" + txtRepertoirePhotos.Text, true);
                                    GestionRepertoire.DirectoryCopy(@"" + ancienObjet.REPERTOIRE_PHOTO, @"" + txtRepertoirePhotos.Text, true);

                                    //DEBUT copie du logo de l'etablissement
                                    string destfile = System.IO.Path.Combine(ConnexionUI.DOSSIER_IMAGES, "logo." + logo.Split('.')[1]);
                                    imgLogo.Source = null;
                                    //imgLogo = new Image();
                                    //System.IO.File.Delete(destfile);
                                    System.IO.File.Copy(logo, destfile, true);
                                    logo = "logo." + logo.Split('.')[1];
                                    parametre.logo = logo;
                                    //FIN copie du logo de l'etablissement

                                    

                                    //***************** FIN création du repertoire image
                                }
                                else MessageBox.Show("Le Répertoire des Images spécifié n'existe pas ! ", "School Brain : Alerte", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                            catch (Exception ex) { }

                        }


                        creerModifierParametresBL.modifierParametre(ancienObjet, parametre);
                        List<ParametresBE> LParametresBE = creerModifierParametresBL.listerTousLesParametres();
                        // on met la liste "LParametresBE" dans le DataGrid
                        RemplirDataGrid(LParametresBE);

                        txtNomEcole.Text = "";
                        txtAdresse.Text = "";
                        txtTelephone.Text = "";
                        txtFax.Text = "";
                        txtEmail.Text = "";
                        txtSiteWeb.Text = "";
                        txtDirecteur.Text = "";
                        txtPays.Text = "";
                        txtRegion.Text = "";
                        txtMinistere.Text = "";
                        txtCountry.Text = "";
                        txtRegionA.Text = "";
                        txtAnnee.Text = "";

                        txtDepartemant.Text = "";
                        txtVille.Text = "";
                        txtTitreDuChef.Text = "";

                        txtTitleOfChief.Text = "";
                        txtDepartment.Text = "";
                        txtMinistery.Text = "";

                        lblCheminLogo.Content = "";

                        txtRepertoirePhotos.Text = "";

                        imgLogo.Source = null;
                        //etat = 0;

                    }

                }
                else if ( ! creerModifierParametresBL.existParametre())
                { // si un paramètre n'existe pas deja dans la BD

                    try
                    {
                        if (GestionRepertoire.existDirectory(@"" + txtRepertoirePhotos.Text)) {
                            if (creerModifierParametresBL.creerParametre(parametre.idParametre, parametre.nomEcole, parametre.adresse,
                        parametre.tel, parametre.fax, parametre.email, parametre.siteWeb, parametre.directeur, parametre.pays, parametre.region, parametre.ministere, parametre.ministery, parametre.country, parametre.regionA, parametre.annee,
                        parametre.departement, parametre.department, parametre.ville, parametre.titreDuChef, parametre.titleOfChief, parametre.logo, txtRepertoirePhotos.Text))
                            { // si l'nregistrement a réussi

                                MessageBox.Show("Enregistrement Paramètre [" + parametre.idParametre + ", " + parametre.nomEcole + ", " + parametre.adresse + ", " + parametre.tel + ", ...] " + " : Opération réussie");
                                txtNomEcole.Text = "";
                                txtAdresse.Text = "";
                                txtTelephone.Text = "";
                                txtFax.Text = "";
                                txtEmail.Text = "";
                                txtSiteWeb.Text = "";
                                txtDirecteur.Text = "";
                                txtPays.Text = "";
                                txtRegion.Text = "";
                                txtMinistere.Text = "";
                                txtCountry.Text = "";
                                txtRegionA.Text = "";
                                txtAnnee.Text = "";

                                txtDepartemant.Text = "";
                                txtVille.Text = "";
                                txtTitreDuChef.Text = "";

                                txtTitleOfChief.Text = "";
                                txtDepartment.Text = "";
                                txtMinistery.Text = "";

                                lblCheminLogo.Content = "";
                                imgLogo.Source = null;

                                txtRepertoirePhotos.Text = "";

                                //***************** debut création du repertoire image
                                try
                                {
                                    //ConnexionUI.DOSSIER_PHOTO = ancienObjet.REPERTOIRE_PHOTO + "Photos";
                                    //ConnexionUI.DOSSIER_IMAGES = ancienObjet.REPERTOIRE_PHOTO + "Images";
                                    //ConnexionUI.DOSSIER_ETATS = ancienObjet.REPERTOIRE_PHOTO + "Etats";

                                    GestionRepertoire.CreateDirectory(ConnexionUI.DOSSIER_IMAGES);
                                    GestionRepertoire.CreateDirectory(ConnexionUI.DOSSIER_PHOTO);
                                    GestionRepertoire.CreateDirectory(ConnexionUI.DOSSIER_ETATS);
                                    GestionRepertoire.CreateDirectory(ConnexionUI.DOSSIER_BULLETINS);

                                    //GestionRepertoire.DirectoryCopy(@"\\YUYA-PC\\Users\\YUYA\\Desktop\\Photos2\\Images", @"\\SIGNE-PC\\Users\\Raoul\\Desktop\\School_Brain\\Images\\", true);
                                    //GestionRepertoire.DirectoryCopy(@"\\YUYA-PC\\Users\\YUYA\\Desktop\\Photos2\\Photos", @"\\SIGNE-PC\\Users\\Raoul\\Desktop\\School_Brain\\Photos\\", true);
                                    //GestionRepertoire.DirectoryCopy(@"\\YUYA-PC\\Users\\YUYA\\Desktop\\Photos2\\Etats", @"\\SIGNE-PC\\Users\\Raoul\\Desktop\\School_Brain\\Etats\\", true);

                                    //DEBUT copie du logo de l'etablissement
                                    string destfile = System.IO.Path.Combine(ConnexionUI.DOSSIER_IMAGES, "logo." + logo.Split('.')[1]);
                                    imgLogo.Source = null;
                                    //imgLogo = new Image();
                                    //System.IO.File.Delete(destfile);
                                    System.IO.File.Copy(logo, destfile, true);
                                    logo = "logo." + logo.Split('.')[1];
                                    parametre.logo = logo;
                                    //FIN copie du logo de l'etablissement
                                }
                                catch (Exception ex) { }
                                //***************** FIN création du repertoire image


                                List<ParametresBE> LParametresBE = creerModifierParametresBL.listerTousLesParametres();
                                //on rafraichir le DataGrid
                                RemplirDataGrid(LParametresBE);

                            }
                            else MessageBox.Show("Echec enregistrement : une erreure est survenue !");
                        }
                        else MessageBox.Show("Le Répertoire des images spécifié n'existe pas ! ", "School Brain : Alerte", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception ex) { 
                    }
                    
                }
                else MessageBox.Show("Un enregistrement de Paramètre existe deja dans la Base de donnée. \n\n Pour enregistrer un nouveau paramètre, bous devez supprimer l'ancien ou bien tout simplement l'éditer !!");
            }
            else MessageBox.Show("Tous les champs Marqués par un astérix \"(*)\" doivent pas être remplis !");
        }

        private void cmdAnnuler_Click(object sender, RoutedEventArgs e)
        {
            txtNomEcole.Text = "";
            txtAdresse.Text = "";
            txtTelephone.Text = "";
            txtFax.Text = "";
            txtEmail.Text = "";
            txtSiteWeb.Text = "";
            txtDirecteur.Text = "";
            txtPays.Text = "";
            txtRegion.Text = "";
            txtMinistere.Text = "";
            txtCountry.Text = "";
            txtRegionA.Text = "";
            txtAnnee.Text = "";

            txtDepartemant.Text = "";
            txtVille.Text = "";
            txtTitreDuChef.Text = "";

            txtTitleOfChief.Text = "";
            txtDepartment.Text = "";
            txtMinistery.Text = "";

            lblCheminLogo.Content = "";
            imgLogo.Source = null;

            txtRepertoirePhotos.Text = "";

            etat = 0;
        }

       /* private void grdListeParametre_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                if (MessageBox.Show("Voulez-vous vraiment le supprimer ? ", "School : Confimation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    if (creerModifierParametresBL.supprinerParametre(ListeParametres.ElementAt(grdListeParametre.SelectedIndex)))
                        ListeParametres.RemoveAt(grdListeParametre.SelectedIndex);

                    grdListeParametre.ItemsSource = ListeParametres;

                }

                grdListeParametre.UnselectAll();
            }
        }*/

        private void grdListeParametre_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (grdListeParametre.SelectedIndex != -1)
            {
                etat = 1;
                ParametresBE parametre = new ParametresBE();

                parametre = creerModifierParametresBL.rechercherParametre(ListeParametres.ElementAt(grdListeParametre.SelectedIndex));
                // on charge les informations dans le formulaire
                txtNomEcole.Text = parametre.nomEcole;
                txtAdresse.Text = parametre.adresse;
                txtTelephone.Text = parametre.tel;
                txtFax.Text = parametre.fax;
                txtEmail.Text = parametre.email;
                txtSiteWeb.Text = parametre.siteWeb;
                txtDirecteur.Text = parametre.directeur;
                txtPays.Text = parametre.pays;
                txtRegion.Text = parametre.region;
                txtMinistere.Text = parametre.ministere;
                txtCountry.Text = parametre.country;
                txtRegionA.Text = parametre.regionA;
                txtAnnee.Text = Convert.ToString(parametre.annee);

                txtDepartemant.Text = parametre.departement;
                txtVille.Text = parametre.ville;
                txtTitreDuChef.Text = parametre.titreDuChef;

                txtTitleOfChief.Text = parametre.titleOfChief;
                txtDepartment.Text = parametre.department;
                txtMinistery.Text = parametre.ministery;

                txtRepertoirePhotos.Text = parametre.REPERTOIRE_PHOTO;

                try
                {
                    imgLogo.Source = new BitmapImage(new Uri(ConnexionUI.DOSSIER_IMAGES + parametre.logo));
                }
                catch (Exception) { imgLogo.Source = null; }

                ancienObjet = parametre;

                logo = parametre.logo;

                lblCheminLogo.Content = parametre.logo;

                nomLogo = parametre.logo;

                grdListeParametre.UnselectAll();
            }
        }

        private void txtTelephone_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Utilitaires.IsTextAllowed(e.Text);
        }

        private void txtAnnee_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Utilitaires.IsTextAllowed(e.Text);
        }

        private void cmdImprimer_Click(object sender, RoutedEventArgs e)
        {
            CreerEtat etat = new CreerEtat("Paramètres -" + DateTime.Today.ToShortDateString(), "Liste des Paramètres ");

            etat.obtenirEtatModePaysage(grdListeParametre);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

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
                logo = openFileDialog.FileName;
                try
                {
                    imgLogo.Source = null;

                    imgLogo.Source = new BitmapImage(new Uri(logo));
                    lblCheminLogo.Content = logo;
                }
                catch (Exception) { imgLogo.Source = null; }
            }

        }

        private void txtAnnee_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                int annee = Convert.ToInt32(txtAnnee.Text);
                txtAnneeScolaire.Text = (annee - 1) + " / " + annee;
            }
            catch (Exception) 
            {
                MessageBox.Show("Le champ Année doit être un entier","School brain:Alerte");
            }
        }
    }
}
