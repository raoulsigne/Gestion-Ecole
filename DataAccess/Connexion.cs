using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;
using System.Xml;
using Ecole.Utilitaire;
using System.Windows;


namespace Ecole.DataAccess
{
    public class Connexion
    {
        // private MySqlConnection connexion;
        public MySqlConnection connexion { get; set; }
       
        private static Connexion con = null;

        //rendre le constructeur privé ------------------------------------------------
        public Connexion()
        {
            try
            {
                //string chaineCon = "";

                //// new xdoc instance 
                //XmlDocument xDoc = new XmlDocument();

                ////load up the xml from the location  
                //xDoc.Load("C:\\projet C#\\workspace\\Projet Ecole 2\\App.Config");
                ////xDoc.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);

                ////**************** DEBUT Partie qui écrit les paramètres de connexion à la BD

                //XmlNode GeneralInformationConnexionBD =
                //         xDoc.SelectSingleNode("/configuration/connectionStrings");

                //for (int i = 0; i < GeneralInformationConnexionBD.ChildNodes.Count; i++)
                //{
                //    if (GeneralInformationConnexionBD.ChildNodes[i].Name.Equals("add"))
                //    {
                //        if (GeneralInformationConnexionBD.ChildNodes[i].Attributes["name"].Value.Equals("connexion"))
                //        {
                //            chaineCon = GeneralInformationConnexionBD.ChildNodes[i].Attributes["connectionString"].Value;
                //        }
                //    }
                //}



                //// lecture du fichier de configuration et difinition de la chaine de connexion
                //String config = ConfigurationManager.ConnectionStrings["connexion"].ToString();

                //String[] paramConnexion = getParametresConnexionBD();

                ////l'adresse du serveur contenant la BD
                //String serveur = Cryptage.Decrypt(config.Split(';')[0].Split('=')[1], Cryptage.clefDuCryptage);

                ////le nom d'utilisateur
                //String utilisateur = Cryptage.Decrypt(config.Split(';')[2].Split('=')[1], Cryptage.clefDuCryptage);

                ////le mot de passe de la BD
                //String password = Cryptage.Decrypt(config.Split(';')[3].Split('=')[1], Cryptage.clefDuCryptage);

                //String chaineCon = "SERVER=" + serveur + "; DATABASE=ecole; UID=" + utilisateur + "; PASSWORD=" + password;

                String chaineCon = ConfigurationManager.ConnectionStrings["connexion"].ToString();
                //MessageBox.Show(chaineCon);

                this.connexion = new MySqlConnection(chaineCon);
                //this.connexion.Open();

                if (this.connexion.State == ConnectionState.Closed)
                {
                    this.connexion.Open();
                }

            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
        }
        //------------------------------fin constructeur--------------------------------
        
        //----La méthode qui nous donne l'instance de la connexion courante-------------
        public static Connexion getConnexion()
        {
            
              // Si aucune connexion n'est en cours, on en crée
               if (con==null)
                {
                    con=new Connexion();
                }
               return con; 
        }
        //------------------------------fin getConnexion--------------------------------

        public static void resetConnexion() {
            con = null;
        }

        public String[] getParametresConnexionBD()
        {

            try
            {
                string chaineCon = null;

                // new xdoc instance 
                XmlDocument xDoc = new XmlDocument();

                //load up the xml from the location 
                //xDoc.Load("C:\\projet C#\\workspace\\Projet Ecole 2\\App.Config");
                xDoc.Load(AppDomain.CurrentDomain.BaseDirectory.Replace("bin\\Debug\\", "App.Config"));

                //**************** DEBUT Partie qui écrit les paramètres de connexion à la BD

                XmlNode GeneralInformationConnexionBD =
                         xDoc.SelectSingleNode("/configuration/connectionStrings");

                for (int i = 0; i < GeneralInformationConnexionBD.ChildNodes.Count; i++)
                {
                    if (GeneralInformationConnexionBD.ChildNodes[i].Name.Equals("add"))
                    {
                        if (GeneralInformationConnexionBD.ChildNodes[i].Attributes["name"].Value.Equals("connexion"))
                        {
                            chaineCon = GeneralInformationConnexionBD.ChildNodes[i].Attributes["connectionString"].Value;
                        }
                    }
                }

                if (chaineCon != null)
                {
                    String[] table = new String[3];
                    //l'adresse du serveur contenant la BD
                    table[0] = chaineCon.Split(';')[0].Split('=')[1];

                    //le nom d'utilisateur
                    table[1] = chaineCon.Split(';')[2].Split('=')[1];

                    //le mot de passe de la BD
                    table[2] = chaineCon.Split(';')[3].Split('=')[1];

                    return table;
                }

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

    }
}
