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
using Microsoft.Win32;
using System.IO;
using Microsoft.VisualBasic.FileIO;
using System.Globalization;
using System.Threading;
using Ecole.Utilitaire;
using System.Drawing;
using System.Windows.Interop;

namespace Ecole.UI
{
    /// <summary>
    /// Interaction logic for EleveUI.xaml
    /// </summary>
    public partial class EleveUI : Window
    {
        static string TYPE_ENREGISTRER = "enregistrer";
        static string TYPE_MODIFIER = "modifier";
        //public static string DOSSIER_PHOTO = AppDomain.CurrentDomain.BaseDirectory.Replace("bin\\Debug\\","Photos\\");//"..\\..\\Photos\\";//Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)+"\\PhotosEtudiants";
        string typeValidation;
        GestionEleveBL eleveBL;
        List<string> classes,nomclasses;
        List<string> categories,nomcategories;
        List<string> sexes;
        List<string> etats;
        List<string> nationalites;
        List<string> langues;
        List<string> regions;
        List<string> departements;

        string matricule;
        string codeClasse;
        string codecat;
        string codeDept;
        string codeRegion;
        string nom;
        string sexe;
        DateTime dateNaissance;
        string lieuNaissance;
        string photo;
        string nomPere;
        string nomMere;
        string telephone;
        string telParent;
        string email;
        string adresse;
        string diplome;
        Int32 anneeDiplome, categorie, classe;
        string langue;
        public int annee { get; set; }
        public string nationalite { get; set; }

        public string fonctionPere { get; set; }
        public string fonctionMere { get; set; }
        public string particulariteMedicale { get; set; }

        public EleveUI()
        {
            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";
            Thread.CurrentThread.CurrentCulture = ci;

            classes = new List<string>();
            categories = new List<string>();
            nomclasses = new List<string>();
            nomcategories = new List<string>();
            
            sexes = new List<string>() { "Masculin","Feminin"};
            etats = new List<string>() { "CLASSE", "NON CLASSE" };
            nationalites = new List<string>();
            langues = new List<string> {"Français","Anglais","Espagnol","Allemand","Chinois","Japonais" };
            regions = new List<string>();
            departements = new List<string>();
            eleveBL = new GestionEleveBL ();
            Console.WriteLine(DateTime.Now.ToLongTimeString());
            matricule = "";
            photo = ""; //Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\PhotosEtudiants\\default.jpg";
            telephone = "";
            email = "";
            diplome = "";
            telParent = "";
            anneeDiplome = 0;
            typeValidation = TYPE_ENREGISTRER;

            InitializeComponent();

            classes = eleveBL.listerValeurColonneClasse("codeclasse");
            categories = eleveBL.listerValeurColonneCategorie("codecateleve");
            nomclasses = eleveBL.listerValeurColonneClasse("nomclasse");
            nomcategories = eleveBL.listerValeurColonneCategorie("nomcateleve");

            nationalites = eleveBL.listerValeurColonnePays("codepays");
            regions = eleveBL.listerValeurColonneRegion("coderegion");
            departements = eleveBL.listerValeurColonneDepartement("codedept");

            annee = eleveBL.AnneeEnCours();
            txtAnnee.Text = " / " + annee;
            txtAnneeScolaire.Text = (annee - 1).ToString();
            cmbCategorie.ItemsSource = categories;
            cmbCategorie.SelectedIndex = 0;
            cmbClasse.ItemsSource = classes;
            cmbClasse.SelectedIndex = 0;
            /*cmbDepartement.ItemsSource = departements;
            cmbDepartement.SelectedIndex = 0;*/
            cmbLangue.ItemsSource = langues;
            cmbLangue.SelectedIndex = 0;
            cmbNationalite.ItemsSource = nationalites;
            cmbNationalite.SelectedIndex = 0;
            /*cmbRegion.ItemsSource = regions;
            cmbRegion.SelectedIndex = 0;*/
            cmbSexe.ItemsSource = sexes;
            cmbSexe.SelectedIndex = 0;
            cmbEtat.ItemsSource = etats;
            cmbEtat.SelectedIndex = 0;
            radioEnregistrement.IsChecked = true;
            radioNonRedoublant.IsChecked = true;
            txtMatricule.IsEnabled = false;
            //-------------MOI----------------------------------------------------
            txtMatricule.Text = nouveauMatricule(eleveBL.getDernierMatricule());
            //--------------------------------------------------------------------

            fonctionPere = "";
            fonctionMere = "";
            particulariteMedicale = "";
            txtFonctionPere.Text = "";
            txtFonctionMere.Text = "";
            txtParticulariteMedicale.Text = "";
        }

        private void txtMatricule_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                string mat = txtMatricule.Text;
                EleveBE eleve = new EleveBE();
                eleve.matricule = mat;
                
                eleve = eleveBL.rechercherEleve(eleve);
                if (eleve != null)
                {
                    initialiserChamps(eleve);
                    typeValidation = TYPE_MODIFIER;
                }
                else
                {
                    MessageBox.Show("L'élève n'existe pas dans la base de données","School brain: Message d'Alerte");
                    typeValidation = TYPE_ENREGISTRER;
                    initialiserChamps(null);
                }
            }
        }

        private void cmdEnregistrer_Click(object sender, RoutedEventArgs e)
        {
            if (validerFormulaire())
            {
                EleveBE eleve = new EleveBE();
                InscrireBE inscrire = new InscrireBE();
                AppartenirBE appartenir = new AppartenirBE();
                if(txtTelephone.Text != null || txtTelephone.Text != "")
                    telephone = txtTelephone.Text;
                if (txtEmail.Text != null || txtEmail.Text != "")
                    email = txtEmail.Text;
                if (txtDiplome.Text != null || txtDiplome.Text != "")
                    diplome = txtDiplome.Text;
                if (txtTelephoneParent.Text != null || txtTelephoneParent.Text != "")
                    telParent = txtTelephoneParent.Text;
                if(txtAnneeObtention.Text != null & txtAnneeObtention.Text != "")
                    anneeDiplome = Convert.ToInt32(txtAnneeObtention.Text);
                
                adresse = txtAdresse.Text;
                lieuNaissance = txtLieu.Text;
                matricule = txtMatricule.Text;
                nom = txtNom.Text;
                nomMere = txtNomMere.Text;
                nomPere = txtNomPere.Text;
                codeClasse = cmbClasse.Text;
                codecat = cmbCategorie.Text;
                //codeDept = cmbDepartement.Text;
                codeDept = null;
                langue = cmbLangue.Text;
                nationalite = cmbNationalite.Text;
                //codeRegion = cmbRegion.Text;
                codeRegion = null;
                sexe = cmbSexe.Text;
                dateNaissance = Convert.ToDateTime(dpiDateNaissance.Text);

                fonctionPere = txtFonctionPere.Text;
                fonctionMere = txtFonctionMere.Text;
                particulariteMedicale = txtParticulariteMedicale.Text;

                //copie de la photo de l'etudiant
                try
                {
                    string destfile = System.IO.Path.Combine(ConnexionUI.DOSSIER_PHOTO, matricule + "." + photo.Split('.')[1]);
                    System.IO.File.Copy(photo, destfile, true);
                    photo = matricule + "." + photo.Split('.')[1];
                }
                catch (Exception)
                { }

                eleve = new EleveBE(matricule, nationalite, codeDept, codeRegion, nom, sexe, dateNaissance, lieuNaissance, langue, photo, nomPere, nomMere,
                        telephone, telParent, email, adresse, diplome, anneeDiplome);

                eleve.fonctionPere = fonctionPere;
                eleve.fonctionMere = fonctionMere;
                eleve.situationMedicale = particulariteMedicale;
                eleve.etat = cmbEtat.Text;

                if (typeValidation == TYPE_ENREGISTRER)
                {
                    inscrire = new InscrireBE(codeClasse, matricule, annee);
                    appartenir = new AppartenirBE(codecat, matricule, annee);

                    if (eleveBL.enregistrerEleve(eleve))
                        MessageBox.Show("Eleve enregistré avec succes");
                    else
                        MessageBox.Show("Eleve non enregistré");

                    if (eleveBL.enregistrerInscrire(inscrire))
                        Console.WriteLine("Inscription enregistrée avec succes");
                    else
                        MessageBox.Show("Inscription non enregistrée");
                    if (radioRedoublant.IsChecked == true)
                    {
                        inscrire = new InscrireBE(codeClasse, matricule, annee - 1);
                        eleveBL.enregistrerInscrire(inscrire);
                    }
                    
                    if (eleveBL.enregistrerAppartenir(appartenir))
                        Console.WriteLine("Enregistrement de la categorie réussi");
                    else
                        MessageBox.Show("Enregistrement de la categorie non réussi");

                    CreerEtat etat = new CreerEtat("inscription-"+eleve.matricule+""+DateTime.Today.ToShortDateString(),"Inscription");
                    etat.couponInscription(eleve, nomcategories.ElementAt(categories.IndexOf(cmbCategorie.SelectedValue.ToString())), nomclasses.ElementAt(classes.IndexOf(cmbClasse.SelectedValue.ToString())), annee);
                    initialiserChamps(null);                    
                }
                else
                {
                    EleveBE eleve_old = new EleveBE();
                    eleve_old.matricule = eleve.matricule;
                    eleve_old = eleveBL.rechercherEleve(eleve_old);
                    if (eleveBL.modifierEleve(eleve_old,eleve))
                        MessageBox.Show("Mise à jour réalisée avec succes");
                    else
                        MessageBox.Show("Mise à jour non réalisée", "Information", MessageBoxButton.OK, MessageBoxImage.Error);

                    if (radioRedoublant.IsChecked == true)
                    {
                        inscrire = new InscrireBE(codeClasse, matricule, annee - 1);
                        eleveBL.enregistrerInscrire(inscrire);
                    }
                    if (radioNonRedoublant.IsChecked == true)
                    {
                        inscrire = new InscrireBE(codeClasse, matricule, annee - 1);
                        eleveBL.supprimerInscrire(inscrire);
                    }
                    txtMatricule.IsEnabled = true;
                    cmbCategorie.IsEnabled = true;
                    cmbClasse.IsEnabled = true;
                    txtAnneeScolaire.IsEnabled = true;
                    CreerEtat etat = new CreerEtat("inscription-" + eleve.matricule + "" + DateTime.Today.ToShortDateString(), "Inscription");
                    etat.couponInscription(eleve, nomcategories.ElementAt(categories.IndexOf(cmbCategorie.SelectedValue.ToString())), nomclasses.ElementAt(classes.IndexOf(cmbClasse.SelectedValue.ToString())), annee);
                    initialiserChamps(null);
                }
            }
            else
                MessageBox.Show("Il existe des champs obligatoires non remplis champs suivis de (*), le numero de telephone doit avoir 9 chiffres","school brain : Alerte");
        }

        private void cmdAnnuler_Click(object sender, RoutedEventArgs e)
        {
            initialiserChamps(null);
            typeValidation = TYPE_ENREGISTRER;
        }

        private void cmdSupprimer_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Voulez vous supprimer cet étudiant?", "School : Confirmation", System.Windows.MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                if (txtMatricule.Text != null & txtMatricule.Text != "")
                {
                    string mat = txtMatricule.Text;
                    EleveBE eleve = new EleveBE();
                    eleve.matricule = mat;

                    eleve = eleveBL.rechercherEleve(eleve);
                    if (eleve != null)
                    {
                        eleveBL.supprimerEleve(eleve);
                        MessageBox.Show("élève supprimé dans la base de données", "School brain: Message d'Alerte");
                        initialiserChamps(null);
                    }
                    else
                    {
                        MessageBox.Show("L'élève n'existe pas dans la base de données", "School brain: Message d'Alerte");
                    }
                }
                else
                    MessageBox.Show("Veuillez renseigner le matricule");
            }
        }

        private void cmdFermer_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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

        //methode pour initialiser les champs en fonction d'un objet EleveBE
        private void initialiserChamps(EleveBE eleve)
        {
            if (eleve != null)
            {
                InscrireBE inscrire = new InscrireBE();
                AppartenirBE appartenir = new AppartenirBE();
                ClasseBE classe = new ClasseBE();
                int annee;
                
                inscrire.matricule = eleve.matricule;
                appartenir.matricule = eleve.matricule;
                annee = eleveBL.obtenirAnneeInscription(eleve);
                inscrire.annee = annee;
                appartenir.annee = annee;

                inscrire = eleveBL.rechercherInscrire(inscrire);
                appartenir = eleveBL.rechercherAppartenir(appartenir);
                if (inscrire == null)
                    MessageBox.Show("L'inscription n'a pas été faite en " + annee + ", veuillez renseigner le champ Année pour recommencer");
                else
                {
                    txtAdresse.Text = eleve.adresse;
                    txtAnneeObtention.Text = Convert.ToString(eleve.anneeDiplome);
                    txtDiplome.Text = eleve.diplome;
                    txtEmail.Text = eleve.email;
                    txtLieu.Text = eleve.lieuNaissance;
                    txtMatricule.Text = eleve.matricule;
                    txtNom.Text = eleve.nom;
                    txtNomMere.Text = eleve.nomMere;
                    txtNomPere.Text = eleve.nomPere;
                    txtTelephone.Text = eleve.telephone;
                    txtTelephoneParent.Text = eleve.telParent;
                    
                    try
                    {
                        photo = eleve.photo;
                        imgPhoto.Source = new BitmapImage(new Uri(ConnexionUI.DOSSIER_PHOTO + eleve.photo));
                    }
                    catch (Exception) { imgPhoto.Source = null; }

                    //MessageBox.Show(inscrire.annee+"");
                    dpiDateNaissance.SelectedDate = eleve.dateNaissance;
                    cmbClasse.SelectedIndex = cmbClasse.Items.IndexOf(inscrire.codeClasse);
                    txtAnneeScolaire.Text = Convert.ToString(inscrire.annee - 1);
                    cmbCategorie.SelectedIndex = cmbCategorie.Items.IndexOf(appartenir.codeCatEleve);
                    //cmbCategorie.Text = appartenir.codeCatEleve;
                    //cmbDepartement.SelectedIndex = cmbDepartement.Items.IndexOf(eleve.codeDept);
                    txtFonctionPere.Text = eleve.fonctionPere;
                    txtFonctionMere.Text = eleve.fonctionMere;
                    txtParticulariteMedicale.Text = eleve.situationMedicale;
                    cmbLangue.SelectedIndex = cmbLangue.Items.IndexOf(eleve.langue);
                    cmbNationalite.SelectedIndex = cmbNationalite.Items.IndexOf(eleve.codePays);
                    //cmbRegion.SelectedIndex = cmbRegion.Items.IndexOf(eleve.codeRegion);
                    cmbSexe.SelectedIndex = cmbSexe.Items.IndexOf(eleve.sexe);
                    cmbEtat.SelectedIndex = cmbEtat.Items.IndexOf(eleve.etat);

                    //on fixe les champs qui ne doivent pas etre modifies, informations scolaires
                    txtMatricule.IsEnabled = false;
                    cmbCategorie.IsEnabled = false;
                    cmbClasse.IsEnabled = false;
                    txtAnneeScolaire.IsEnabled = false;
                    classe.codeClasse = inscrire.codeClasse;
                    classe = eleveBL.rechercherClasse(classe);
                    if (eleveBL.estRedoublant(eleve, classe, annee))
                    {
                        radioNonRedoublant.IsChecked = false;
                        radioRedoublant.IsChecked = true;
                    }
                    else
                    {
                        radioNonRedoublant.IsChecked = true;
                        radioRedoublant.IsChecked = false;
                    }
                }
            }
            else
            {
                txtAdresse.Text = "";
                txtAnneeObtention.Text = "";
                txtDiplome.Text = "";
                txtEmail.Text = "";
                txtLieu.Text = "";
                txtMatricule.Text = nouveauMatricule(eleveBL.getDernierMatricule());
                txtNom.Text = "";
                txtNomMere.Text = "";
                txtNomPere.Text = "";
                txtTelephone.Text = "";
                txtTelephoneParent.Text = "";
                txtAnneeScolaire.Text = (eleveBL.AnneeEnCours() - 1).ToString();
                cmbCategorie.SelectedIndex = 0;
                cmbClasse.SelectedIndex = 0;
                //cmbDepartement.SelectedIndex = 0;
                txtFonctionPere.Text = "";
                txtFonctionMere.Text = "";
                txtParticulariteMedicale.Text = "";
                cmbLangue.SelectedIndex = 0;
                cmbNationalite.SelectedIndex = 0;
                //cmbRegion.SelectedIndex = 0;
                cmbSexe.SelectedIndex = 0;
                cmbEtat.SelectedIndex = 0;
                dpiDateNaissance.SelectedDate = DateTime.Today.Date;
                dpiDateNaissance.Text = DateTime.Today.Date.ToShortDateString();

                imgPhoto.Source = null;
                
                cmbCategorie.IsEnabled = true;
                cmbClasse.IsEnabled = true;
                txtAnneeScolaire.IsEnabled = true;
                radioEnregistrement.IsChecked = true;
                radioNonRedoublant.IsChecked = true;
            }
        }

        private bool validerFormulaire()
        {
            bool b = true;

            if (txtMatricule.Text == null || cmbCategorie.SelectedValue == null || cmbClasse.SelectedValue == null || txtAnneeScolaire.Text == null ||
                    txtNom.Text == null || txtNomPere.Text == null || txtNomMere.Text == null || cmbSexe.SelectedValue == null ||
                    dpiDateNaissance.SelectedDate == null || cmbNationalite.SelectedValue == null || txtLieu.Text == null || txtTelephoneParent.Text == null)
                b = false;
            else
                if (txtMatricule.Text == "" || cmbCategorie.SelectedValue.ToString() == "" || cmbClasse.SelectedValue.ToString() == "" || txtAnneeScolaire.Text == "" ||
                        txtNom.Text.ToString() == "" || txtNomPere.Text.ToString() == "" || txtNomMere.Text.ToString() == "" || cmbSexe.SelectedValue.ToString() == "" ||
                        cmbNationalite.SelectedValue.ToString() == "" || txtLieu.Text.ToString() == "" || txtTelephoneParent.Text == "")
                    b = false;
                else if (txtTelephoneParent.Text.Length != 9)
                    b = false;

            return b;
        }

        private void txtTelephone_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Utilitaires.formatTelephone(e.Text);
        }

        //-------------MOI----------------------------------------------------------------
        //--------------------------------------------------------------------------------
        //------générer nouveau matricule----------------------------
        private string nouveauMatricule(string dernierMatricule)
        {

            string matricule = "";
            int an;
            char lettre;
            int numero;

            if (dernierMatricule == "" || dernierMatricule == null)
            {
                matricule = DateTime.Today.Year.ToString().Substring(2, 2) + "A001";

                return matricule;
            }
            else
            {
                try
                {
                    // matricule = dernierMatricule.Substring(0, 2).ToString();
                    an = Convert.ToInt32(dernierMatricule.Substring(0, 2).ToString());
                    lettre = dernierMatricule.Substring(2, 1)[0];
                    numero = Convert.ToInt32(dernierMatricule.Substring(3, 3).ToString());

                    if (an != Convert.ToInt32(DateTime.Today.Year.ToString().Substring(2, 2)))
                        matricule = DateTime.Today.Year.ToString().Substring(2, 2) + "A001";

                    else
                    {
                        if (numero < 999)
                            numero = numero + 1;

                        else
                        {
                            numero = 1;
                            lettre = Convert.ToChar(((int)lettre) + 1);
                        }

                        matricule = DateTime.Today.Year.ToString().Substring(2, 2) + lettre + formaterNumeroMatricule(numero);
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

        private void radioEnregistrement_Checked(object sender, RoutedEventArgs e)
        {
            initialiserChamps(null);
            txtMatricule.IsEnabled = false;
            typeValidation = TYPE_ENREGISTRER;
        }

        private void radioModification_Checked(object sender, RoutedEventArgs e)
        {
            RechercherEleveUI dialog = new RechercherEleveUI();
            dialog.ShowDialog();
            EleveBE el = null;
            try
            {
                string matricule = dialog.matricule;
                el = new EleveBE();
                el.matricule = matricule;
                el = eleveBL.rechercherEleve(el);
                initialiserChamps(el);
                if (el != null)
                {
                    txtMatricule.IsEnabled = true;
                    typeValidation = TYPE_MODIFIER;
                }
            }
            catch (Exception)
            {
                initialiserChamps(null);
            }
        }

        private void txtAnneeScolaire_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                annee = Convert.ToInt32(txtAnneeScolaire.Text) + 1;
                txtAnnee.Text = " / " + annee.ToString();
            }
            catch (Exception)
            {
                MessageBox.Show("L'annee doit etre un nombre positif", "School brain:Alerte");
            }
        }

    }
}
