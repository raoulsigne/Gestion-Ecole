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
using Ecole.Utilitaire;

namespace Ecole.UI
{
    /// <summary>
    /// Interaction logic for WindowGenererCertificatScolariteUI.xaml
    /// </summary>
    public partial class WindowGenererCertificatScolariteUI : Window
    {
        CreerCertificatScolariteBL creerCertificatScolariteBL;
        EleveBE eleve;
        List<string> eleves;
        List<string> classes;

        int annee;

        public WindowGenererCertificatScolariteUI()
        {
            InitializeComponent();
            eleve = new EleveBE();
            eleves = new List<string>();
            classes = new List<string>();

            creerCertificatScolariteBL = new CreerCertificatScolariteBL();

            //txtAnnee.Text = Convert.ToString(System.DateTime.Today.Year);

            classes = creerCertificatScolariteBL.listerValeurColonneClasse("codeclasse");

            ParametresBE param = creerCertificatScolariteBL.getParametres();
            if (param != null)
            {
                annee = param.annee;
                txtAnnee.Text = Convert.ToString(param.annee);
                txtAnneeScolaire.Text = (param.annee - 1).ToString();
            }
            else
            {
                txtAnnee.Text = "";
                txtAnneeScolaire.Text = "";

            }

        }

        private void txtAnnee_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Utilitaires.IsTextAllowed(e.Text);
        }

        private void txtMatricule_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (txtAnnee.Text != null && txtAnnee.Text != "") {

                    // on cherche l'inscription de l'élève pour l'année saisit
                    InscrireBE inscrire = creerCertificatScolariteBL.rechercherInscrireSuivantCritere("matricule = '" + txtMatricule.Text + "' AND annee = '" + txtAnnee.Text + "'");

                    if (inscrire != null)
                    {
                        EleveBE eleve = new EleveBE();
                        eleve.matricule = txtMatricule.Text;
                        eleve = creerCertificatScolariteBL.rechercherEleve(eleve);

                        if (eleve != null)
                        {
                            lblInfoEleve.Content = "[Nom :" + eleve.nom + ", Classe : " + inscrire.codeClasse + "]";
                            //imageEleve.Source = new BitmapImage(new Uri("../Images/"+ eleve2.photo+".jpg"));
                            if (eleve.photo != "")
                            {
                                try
                                {
                                    imageEleve.Source = new BitmapImage(new Uri(ConnexionUI.DOSSIER_PHOTO + eleve.photo));
                                }
                                catch (Exception) { imageEleve.Source = null; }
                            }
                            else imageEleve.Source = null;

                        }
                        else
                        {
                            lblInfoEleve.Content = "";
                            imageEleve.Source = null;
                            MessageBox.Show("Cette élève n'existe pas dans le système ! ", "School Brain : Alerte");
                        }

                    }
                    else {

                        lblInfoEleve.Content = "";
                        imageEleve.Source = null;
                        MessageBox.Show("Cette n'a pas éffectué d'inscription pour l'année choisi ! ", "School Brain : Alerte");

                    }

                }
                
            }
        }

        private void txtAnnee_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (txtMatricule.Text != null && txtMatricule.Text != "")
                {

                    // on cherche l'inscription de l'élève pour l'année saisit
                    InscrireBE inscrire = creerCertificatScolariteBL.rechercherInscrireSuivantCritere("matricule = '" + txtMatricule.Text + "' AND annee = '" + txtAnnee.Text + "'");

                    if (inscrire != null)
                    {

                        EleveBE eleve = new EleveBE();
                        eleve.matricule = txtMatricule.Text;

                        eleve = creerCertificatScolariteBL.rechercherEleve(eleve);

                        if (eleve != null)
                        {
                            lblInfoEleve.Content = "[Nom :" + eleve.nom + ", Classe : " + inscrire.codeClasse + "]";
                            //imageEleve.Source = new BitmapImage(new Uri("../Images/"+ eleve2.photo+".jpg"));
                            if (eleve.photo != "")
                            {
                                try
                                {
                                    imageEleve.Source = new BitmapImage(new Uri(ConnexionUI.DOSSIER_PHOTO + eleve.photo));
                                }
                                catch (Exception) { imageEleve.Source = null; }
                            }
                            else imageEleve.Source = null;

                        }
                        else
                        {
                            lblInfoEleve.Content = "";
                            imageEleve.Source = null;
                            MessageBox.Show("Cette élève n'est pas reconnu dans le système !  ", "School Brain : Alerte");
                        }

                    }
                    else
                    {
                        lblInfoEleve.Content = "";
                        imageEleve.Source = null;
                        MessageBox.Show("Cette élève n'a pas éffectué d'inscription pour l'année choisi ! ", "School Brain : Alerte");
                    }

                }

            }

        }

        private void txtAnneeScolaire_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (txtMatricule.Text != null && txtMatricule.Text != "")
                {

                    // on cherche l'inscription de l'élève pour l'année saisit
                    InscrireBE inscrire = creerCertificatScolariteBL.rechercherInscrireSuivantCritere("matricule = '" + txtMatricule.Text + "' AND annee = '" + txtAnnee.Text + "'");

                    if (inscrire != null)
                    {

                        EleveBE eleve = new EleveBE();
                        eleve.matricule = txtMatricule.Text;

                        eleve = creerCertificatScolariteBL.rechercherEleve(eleve);

                        if (eleve != null)
                        {
                            lblInfoEleve.Content = "[Nom :" + eleve.nom + ", Classe : " + inscrire.codeClasse + "]";
                            if (eleve.photo != "")
                            {
                                try
                                {
                                    imageEleve.Source = new BitmapImage(new Uri(ConnexionUI.DOSSIER_PHOTO + eleve.photo));
                                }
                                catch (Exception) { imageEleve.Source = null; }
                            }
                            else imageEleve.Source = null;

                        }
                        else
                        {
                            cmbEleve.Text = "";
                            lblInfoEleve.Content = "";
                            imageEleve.Source = null;
                            MessageBox.Show("Cette élève n'est pas reconnu dans le système !  ", "School Brain : Alerte");
                        }

                    }
                    else
                    {
                        cmbEleve.Text = "";
                        lblInfoEleve.Content = "";
                        imageEleve.Source = null;
                        MessageBox.Show("Cette élève n'a pas éffectué d'inscription pour l'année choisi ! ", "School Brain : Alerte");
                    }

                }

            }

        }

        private void cmdAnnuler_Click(object sender, RoutedEventArgs e)
        {
            ParametresBE param = creerCertificatScolariteBL.getParametres();
            if (param != null)
            {
                annee = param.annee;
                txtAnnee.Text = Convert.ToString(param.annee);
                txtAnneeScolaire.Text = (param.annee - 1).ToString();
            }
            else
            {
                txtAnnee.Text = "";
                txtAnneeScolaire.Text = "";

            }

            eleves = new List<string>();
            txtMatricule.Text = "";
            cmbEleve.Text = "";
            cmbClasse.Text = "";
            imageEleve.Source = null;
            lblInfoEleve.Content = "";
        }

        private void cmdValider_Click(object sender, RoutedEventArgs e)
        {
            if ((txtAnneeScolaire.Text != "" && txtMatricule.Text != "") && (txtAnneeScolaire.Text != null && txtMatricule.Text != null))
            {

                EleveBE eleve = new EleveBE();
                eleve.matricule = txtMatricule.Text;
                eleve = creerCertificatScolariteBL.rechercherEleve(eleve);

                if (eleve != null)
                {
                    // on cherche l'inscription de l'élève pour l'année saisit
                    InscrireBE inscrire = creerCertificatScolariteBL.rechercherInscrireSuivantCritere("matricule = '" + txtMatricule.Text + "' AND annee = '" + txtAnnee.Text + "'");

                    if (inscrire != null)
                    {
                        //on recherche la classe de l'élève
                        ClasseBE classe = new ClasseBE();
                        classe.codeClasse = inscrire.codeClasse;

                        classe = creerCertificatScolariteBL.rechercherClasse(classe);

                        ParametresBE parametre = creerCertificatScolariteBL.getParametres();

                        creerCertificatScolariteBL.etatCertificatScolarite(Convert.ToInt16(txtAnnee.Text), eleve, classe, inscrire, parametre);

                    }
                    else
                    {
                        lblInfoEleve.Content = "";
                        imageEleve.Source = null;

                        MessageBox.Show("cet élève n'a pas effectué d'inscription pour l'année choisi ! ", "School Brain alerte");
                    }
                }
                else
                {
                    lblInfoEleve.Content = "";
                    imageEleve.Source = null;
                    MessageBox.Show("Cette élève n'est pas reconnu dans le système ! ", "School Brain alerte");
                }
            }
            else {
                lblInfoEleve.Content = "";
                imageEleve.Source = null;
                MessageBox.Show("Tous les champs doivent êtres remplis ! ", "School Brain alerte"); 
            }
            
        }

        private void cmdFermer_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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

        //----------Raoul------------------
        private void cmbClasse_DropDownClosed(object sender, EventArgs e)
        {
            if (cmbClasse.Text != null && cmbClasse.Text != "")
            {
                //recherche des eleves inscrits  dans cette classe pour charger le combobox des eleves
                eleves = new List<string>();
                string codeclasse = cmbClasse.Text;
                List<EleveBE> listeleves = new List<EleveBE>();
                listeleves = creerCertificatScolariteBL.listerElevesDuneClasse(codeclasse, annee);
                if (listeleves != null)
                {
                    foreach (EleveBE el in listeleves)
                    {
                        eleves.Add(el.matricule + " - " + el.nom);
                    }
                }
                cmbEleve.ItemsSource = eleves;
                txtMatricule.Text = "";
            }
        }

        private void cmbEleve_DropDownClosed(object sender, EventArgs e)
        {
            if (cmbEleve.Text != null && cmbEleve.Text != "")
            {
                string nommat = cmbEleve.Text;
                txtMatricule.Text = nommat.Split('-')[0].Trim();
                eleve.matricule = nommat.Split('-')[0].Trim();
                eleve = creerCertificatScolariteBL.rechercherEleve(eleve);
                InscrireBE inscrire = creerCertificatScolariteBL.rechercherInscrireSuivantCritere("matricule = '" + txtMatricule.Text + "' AND annee = '" + txtAnnee.Text + "'");

                if (inscrire != null)
                {
                    eleve.matricule = txtMatricule.Text;
                    eleve = creerCertificatScolariteBL.rechercherEleve(eleve);
                    if (eleve != null)
                    {
                        lblInfoEleve.Content = "[Nom :" + eleve.nom + ", Classe : " + inscrire.codeClasse + "]";
                        if (eleve.photo != "")
                        {
                            try
                            {
                                imageEleve.Source = new BitmapImage(new Uri(ConnexionUI.DOSSIER_PHOTO + eleve.photo));
                            }
                            catch (Exception) { imageEleve.Source = null; }
                        }
                        else imageEleve.Source = null;
                    }
                    else
                    {
                        lblInfoEleve.Content = "";
                        imageEleve.Source = null;
                        MessageBox.Show("Cette élève n'existe pas dans le système ! ", "School Brain : Alerte");
                    }
                }
                else
                {
                    lblInfoEleve.Content = "";
                    imageEleve.Source = null;
                    MessageBox.Show("Cette n'a pas éffectué d'inscription pour l'année choisi ! ", "School Brain : Alerte");
                }
            }
        }

    }
}
