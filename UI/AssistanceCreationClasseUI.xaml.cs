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

namespace Ecole.UI
{
    /// <summary>
    /// Interaction logic for AssistanceCreationClasseUI.xaml
    /// </summary>
    public partial class AssistanceCreationClasseUI : Window
    {
        AssistanceCreationClasseBL assistanceBL;

        string codecycle;
        string nomcycle;
        string codeniveau;
        string nomniveau;
        int level;
        string codetypeclasse;
        string nomtypeclasse;
        decimal frais;
        string codeserie;
        string nomserie;

        Etape1CreationClasse etape1;
        Etape2CreationClasse etape2;
        Etape3CreationClasse etape3;
        Etape4CreationClasse etape4;
        Etape5CreationClasse etape5;
        int numeroEtape;

        public AssistanceCreationClasseUI()
        {
            InitializeComponent();
            assistanceBL = new AssistanceCreationClasseBL();
            codecycle = "";
            nomcycle = "";
            codeniveau = "";
            nomniveau = "";
            level = 0;
            codetypeclasse = "";
            nomtypeclasse = "";
            frais = 0;
            codeserie = "";
            nomserie = "";
            etape1 = new Etape1CreationClasse();
            etape2 = new Etape2CreationClasse();
            etape3 = new Etape3CreationClasse();
            etape4 = new Etape4CreationClasse();
            etape5 = new Etape5CreationClasse();
            numeroEtape = 1;
            panelForm.Children.Add(etape1);
            lblCycle.Background = Brushes.LightGray;
        }

        private void cmdFermer_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cmdPrecedent_Click(object sender, RoutedEventArgs e)
        {
            Color color = (Color)ColorConverter.ConvertFromString("#FF86B5E8");
            if (numeroEtape == 4)
                cmdSuivant.Content = "Terminer";
            else
                cmdSuivant.Content = "Suivant";

            switch (numeroEtape)
            {
                case 5:
                    panelForm.Children.Clear();
                    etape4 = new Etape4CreationClasse(codeserie, nomserie);
                    panelForm.Children.Add(etape4);
                    numeroEtape = 4;
                    lblFinition.Background = new System.Windows.Media.SolidColorBrush(color);
                    lblSerie.Background = Brushes.LightGray;
                    break;
                case 4:
                    panelForm.Children.Clear();
                    etape3 = new Etape3CreationClasse(codetypeclasse, nomtypeclasse, frais);
                    panelForm.Children.Add(etape3);
                    numeroEtape = 3;
                    lblSerie.Background = new System.Windows.Media.SolidColorBrush(color);
                    lblType.Background = Brushes.LightGray;
                    break;
                case 3:
                    numeroEtape = 2;
                    panelForm.Children.Clear();
                    etape2 = new Etape2CreationClasse(codeniveau, nomniveau, level);
                    panelForm.Children.Add(etape2);
                    lblType.Background = new System.Windows.Media.SolidColorBrush(color);
                    lblNiveau.Background = Brushes.LightGray;
                    break;
                case 2:
                    numeroEtape = 1;
                    panelForm.Children.Clear();
                    etape1 = new Etape1CreationClasse(codecycle, nomcycle);
                    panelForm.Children.Add(etape1);
                    lblNiveau.Background = new System.Windows.Media.SolidColorBrush(color);
                    lblCycle.Background = Brushes.LightGray;
                    break;
                default:
                    break;
            }
        }

        private void cmdSuivant_Click(object sender, RoutedEventArgs e)
        {
            Color color = (Color)ColorConverter.ConvertFromString("#FF86B5E8");
            if (numeroEtape == 4)
                cmdSuivant.Content = "Terminer";
            else
                cmdSuivant.Content = "Suivant";

            switch (numeroEtape)
            {
                case 1:
                    codecycle = etape1.codecycle;
                    nomcycle = etape1.nomcycle;
                    panelForm.Children.Clear();
                    if (codeniveau == "")
                        etape2 = new Etape2CreationClasse();
                    else
                        etape2 = new Etape2CreationClasse(codeniveau, nomniveau, level);
                    panelForm.Children.Add(etape2);
                    numeroEtape = 2;
                    lblCycle.Background = new System.Windows.Media.SolidColorBrush(color);
                    lblNiveau.Background = Brushes.LightGray;
                    break;
                case 2:
                    codeniveau = etape2.codeniveau;
                    nomniveau = etape2.nomniveau;
                    level = etape2.niveau;
                    panelForm.Children.Clear();
                    if (codetypeclasse == "")
                        etape3 = new Etape3CreationClasse();
                    else
                        etape3 = new Etape3CreationClasse(codetypeclasse, nomtypeclasse, frais);
                    panelForm.Children.Add(etape3);
                    numeroEtape = 3;
                    lblNiveau.Background = new System.Windows.Media.SolidColorBrush(color);
                    lblType.Background = Brushes.LightGray;
                    break;
                case 3:
                    codetypeclasse = etape3.codetype;
                    nomtypeclasse = etape3.nomtype;
                    frais = etape3.fraisInscription;
                    numeroEtape = 4;
                    panelForm.Children.Clear();
                    if (codeserie == "")
                        etape4 = new Etape4CreationClasse();
                    else
                        etape4 = new Etape4CreationClasse(codeserie, nomserie);
                    panelForm.Children.Add(etape4);
                    lblType.Background = new System.Windows.Media.SolidColorBrush(color);
                    lblSerie.Background = Brushes.LightGray;
                    break;
                case 4:
                    codeserie = etape4.codeserie;
                    nomserie = etape4.nomserie;
                    numeroEtape = 5;
                    panelForm.Children.Clear();
                    etape5 = new Etape5CreationClasse(etape1.codecycle, etape2.codeniveau, etape3.codetype, etape4.codeserie);
                    panelForm.Children.Add(etape5);
                    lblSerie.Background = new System.Windows.Media.SolidColorBrush(color);
                    lblFinition.Background = Brushes.LightGray;
                    break;
                case 5:
                    //si on cree un noueau cycle
                    CycleBE cycle = new CycleBE(etape1.codecycle, etape1.nomcycle);
                    assistanceBL.enregistrerCycle(cycle);

                    //si on cree un noueau niveau
                    NiveauBE niveau = new NiveauBE(etape2.codeniveau, etape2.nomniveau, etape2.niveau);
                    assistanceBL.enregistrerNiveau(niveau);

                    //si on cree un noueau type
                    TypeclasseBE type = new TypeclasseBE(etape3.codetype, etape3.nomtype, etape3.fraisInscription);
                    assistanceBL.enregistrerTypeClasse(type);

                    //si on cree une nouvelle serie
                    SerieBE serie = new SerieBE(etape4.codeserie, etape4.nomserie);
                    assistanceBL.enregistrerSerie(serie);

                    //enregistrement de la classe
                    ClasseBE classe = new ClasseBE(etape5.codeclasse, cycle.codeCycle, type.codetypeclasse, serie.codeserie, niveau.codeNiveau, etape5.nomclasse);
                    if (assistanceBL.enregistrerClasse(classe))
                        MessageBox.Show("School brain : Information", "Classe cree avec succes", MessageBoxButton.OK, MessageBoxImage.Information);
                    else
                        MessageBox.Show("School brain : Information", "Classe non enregistree", MessageBoxButton.OK, MessageBoxImage.Exclamation);

                    codecycle = "";
                    nomcycle = "";
                    codeniveau = "";
                    nomniveau = "";
                    level = 0;
                    codetypeclasse = "";
                    nomtypeclasse = "";
                    frais = 0;
                    codeserie = "";
                    nomserie = "";
                    numeroEtape = 1;
                    panelForm.Children.Clear();
                    etape1 = new Etape1CreationClasse();
                    panelForm.Children.Add(etape1);
                    lblFinition.Background = new System.Windows.Media.SolidColorBrush(color);
                    lblCycle.Background = Brushes.LightGray;

                    break;
            }
        }
    }
}
