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
using System.Timers;
using System.Windows.Threading;
using Ecole.BusinessLogic;
using Ecole.UI;
using Ecole.DataAccess;
using Ecole.BusinessEntity;
using Ecole.Utilitaire;


namespace Ecole
{
    /// <summary>
    /// Interaction logic for SplashScreen.xaml
    /// </summary>
    public partial class SplashScreen : Window
    {
        DispatcherTimer monTimer = new DispatcherTimer();
        ConnexionUI frmConnexion;
        DemarrageBL demarreur = new DemarrageBL();
        public SplashScreen()
        {

            InitializeComponent();
            lblSociete.Content = demarreur.lireValeurClefSection("appSettings", "societe").ToString().ToUpper();
            gererTimer();

            //************ Debut de la partie qui charge le fichier de langue à utiliser
            //ParametresDA parametresDA = new ParametresDA();
            //ParametresBE parametresBE = parametresDA.getParametre();
            //if (parametresBE != null) {
            //    ChoixLangue.fichierLangue = parametresBE.FICHIER_LANGUE;
            //}
            //************ Fin de la partie qui charge le fichier de langue à utiliser
        }


        public void gererTimer()
        {

            // défini l'interval du Timer (durée de l'affichage)
            monTimer.Interval = new TimeSpan(0, 0, 3);
            // lévènement à déclencher
            monTimer.Tick += new EventHandler(traiterEvenementTimer);

            // lancer mon timer
            monTimer.Start();


        }

        private void traiterEvenementTimer(Object myObject, EventArgs myEventArgs)
        {


            monTimer.Stop();
            frmConnexion = new ConnexionUI();
            this.Close();
            frmConnexion.ShowDialog();
        }


    }
}
