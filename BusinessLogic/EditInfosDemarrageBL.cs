using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Configuration;

using System.Windows;

using Ecole.DataAccess;
using Ecole.BusinessEntity;
using System.Xml;

using System.IO;
using MySql.Data.MySqlClient;
using System.Data;
using Ecole.Utilitaire;
namespace Ecole.BusinessLogic
{
    class EditInfosDemarrageBL
    {
        private UtilisateurDA utilisateurDA;
        private JournalDA journalDA;

        public EditInfosDemarrageBL()
        {
            this.utilisateurDA = new UtilisateurDA();
            this.journalDA = new JournalDA();           
        }

        public void updateConfigFile(string con)
        {
            //updating config file
            XmlDocument XmlDoc = new XmlDocument();
            //Loading the Config file
            XmlDoc.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
            foreach (XmlElement xElement in XmlDoc.DocumentElement)
            {
                if (xElement.Name == "connectionStrings")
                {
                    //setting the coonection string
                    xElement.FirstChild.Attributes[2].Value = con;
                }
            }
            //writing the connection string in config file
            XmlDoc.Save(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);

            journalDA.journaliser("modification de la chaine de connexion à la BD dans le fichier de configuration ");

        }

        public Boolean EcrireConfigurationConnexionBD(String adresseServeur, String utilisateur, String passwordBD)
        {
            //**************cryptage des infos de la BD
            //String adresseServeur1 = Cryptage.Encrypt(adresseServeur);
            //String utilisateur1 = Cryptage.Encrypt(utilisateur);
            //String passwordBD1 = Cryptage.Encrypt(passwordBD);

            // new xdoc instance 
            //XmlDocument xDoc = new XmlDocument();

            XmlDocument xDoc2 = new XmlDocument();

            //load up the xml from the location 
            //xDoc.Load("C:\\projet C#\\workspace\\Projet Ecole 2\\App.Config");
            //xDoc.Load(AppDomain.CurrentDomain.BaseDirectory.Replace("bin\\Debug\\", "App.Config"));

            xDoc2.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);

            //**************** DEBUT Partie qui écrit les paramètres de connexion à la BD

            //XmlNode GeneralInformationConnexionBD =
            //         xDoc.SelectSingleNode("/configuration/connectionStrings");

            XmlNode GeneralInformationConnexionBD2 =
                     xDoc2.SelectSingleNode("/configuration/connectionStrings");

            //for (int i = 0; i < GeneralInformationConnexionBD.ChildNodes.Count; i++)
            //{
            //    if (GeneralInformationConnexionBD.ChildNodes[i].Name.Equals("add"))
            //    {
            //        if (GeneralInformationConnexionBD.ChildNodes[i].Attributes["name"].Value.Equals("connexion"))
            //        {
            //            GeneralInformationConnexionBD.ChildNodes[i].Attributes["connectionString"].Value = "SERVER=" + adresseServeur + "; DATABASE=ecole; UID=" + utilisateur + "; PASSWORD=" + passwordBD;
            //        }
            //    }
            //}

            for (int i = 0; i < GeneralInformationConnexionBD2.ChildNodes.Count; i++)
            {
                if (GeneralInformationConnexionBD2.ChildNodes[i].Name.Equals("add"))
                {
                    if (GeneralInformationConnexionBD2.ChildNodes[i].Attributes["name"].Value.Equals("connexion"))
                    {
                        GeneralInformationConnexionBD2.ChildNodes[i].Attributes["connectionString"].Value = "SERVER=" + adresseServeur + "; DATABASE=ecole; UID=" + utilisateur + "; PASSWORD=" + passwordBD;
                    }
                }
            }

            updateConfigFile("SERVER=" + adresseServeur + "; DATABASE=" + utilisateur + "; UID=root; PASSWORD=" + passwordBD);

            ConfigurationManager.RefreshSection("connectionStrings");


            //*************** FIN Partie qui écrit les paramètres de connexion à la BD

            //xDoc.Save("C:\\projet C#\\workspace\\Projet Ecole 2\\App.Config");
           // xDoc.Save(AppDomain.CurrentDomain.BaseDirectory.Replace("bin\\Debug\\", "App.Config"));

            xDoc2.Save(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);

            this.utilisateurDA = new UtilisateurDA();

            journalDA.journaliser("modification de la chaine de connexion à la BD dans le fichier de configuration ");

            return true;
        }

        public Boolean EcrireConfigurationApplication(String nomSociete)
        {
            try
            {
                // new xdoc instance 
                XmlDocument xDoc = new XmlDocument();

                //load up the xml from the location 
                //xDoc.Load("C:\\projet C#\\workspace\\Projet Ecole 2\\App.Config");
                //xDoc.Load(AppDomain.CurrentDomain.BaseDirectory.Replace("bin\\Debug\\", "App.Config"));

                xDoc.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);

                //***************** Dans le cas on utilise la section "userSettings"

                //XmlNode GeneralInformationNode =
                //xDoc.SelectSingleNode("/configuration/userSettings/Ecole.Properties.Settings");

                //for (int i = 0; i < GeneralInformationNode.ChildNodes.Count; i++)
                //{
                //    if (GeneralInformationNode.ChildNodes[i].Attributes["name"].Equals("lancer"))
                //    {
                //        GeneralInformationNode.ChildNodes[i].FirstChild.InnerText = "OUI";
                //    }
                //    else if (GeneralInformationNode.ChildNodes[i].Attributes["name"].Equals("societe"))
                //    {
                //        GeneralInformationNode.ChildNodes[i].FirstChild.InnerText = nomSociete;
                //    }
                //}


                //***************** FIN utilisation la section "userSettings"

                //***************** Dans le cas on utilise la section "appSettings"

                XmlNode GeneralInformationNode =
                         xDoc.SelectSingleNode("/configuration/appSettings");

                for (int i = 0; i < GeneralInformationNode.ChildNodes.Count; i++)
                {
                    if (GeneralInformationNode.ChildNodes[i].Attributes["key"].Value.Equals("lancer"))
                    {
                        GeneralInformationNode.ChildNodes[i].Attributes["value"].Value = "OUI";
                    }
                    else if (GeneralInformationNode.ChildNodes[i].Attributes["key"].Value.Equals("societe"))
                    {
                        GeneralInformationNode.ChildNodes[i].Attributes["value"].Value = nomSociete;
                    }
                }

                //***************** FIN utilisation la section "appSettings"

                ConfigurationManager.RefreshSection("appSettings");

                //xDoc.Save("C:\\projet C#\\workspace\\Projet Ecole 2\\App.Config");
                //xDoc.Save(AppDomain.CurrentDomain.BaseDirectory.Replace("bin\\Debug\\", "App.Config"));


                xDoc.Save(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);

                this.utilisateurDA = new UtilisateurDA();

                journalDA.journaliser("modification du nom de la société dans le fichier de configuration ");

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        //********************** lire dans le fichier de configuration et retourne les paramètres de connexion à la BD
        public String getNomSociete() {

            try
            {
                String nomSociete = null;

                // new xdoc instance 
                XmlDocument xDoc = new XmlDocument();

                //load up the xml from the location 
                //xDoc.Load("C:\\projet C#\\workspace\\Projet Ecole 2\\App.Config");
                //xDoc.Load(AppDomain.CurrentDomain.BaseDirectory.Replace("bin\\Debug\\", "App.Config"));
                xDoc.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);

                //***************** Dans le cas on utilise la section "userSettings"

                //XmlNode GeneralInformationNode =
                //xDoc.SelectSingleNode("/configuration/userSettings/Ecole.Properties.Settings");

                //for (int i = 0; i < GeneralInformationNode.ChildNodes.Count; i++)
                //{
                //    if (GeneralInformationNode.ChildNodes[i].Attributes["name"].Equals("lancer"))
                //    {
                //        GeneralInformationNode.ChildNodes[i].FirstChild.InnerText = "OUI";
                //    }
                //    else if (GeneralInformationNode.ChildNodes[i].Attributes["name"].Equals("societe"))
                //    {
                //        GeneralInformationNode.ChildNodes[i].FirstChild.InnerText = nomSociete;
                //    }
                //}


                //***************** FIN utilisation la section "userSettings"

                //***************** Dans le cas on utilise la section "appSettings"

                XmlNode GeneralInformationNode =
                         xDoc.SelectSingleNode("/configuration/appSettings");

                for (int i = 0; i < GeneralInformationNode.ChildNodes.Count; i++)
                {
                    if (GeneralInformationNode.ChildNodes[i].Attributes["key"].Value.Equals("societe"))
                    {
                        nomSociete = GeneralInformationNode.ChildNodes[i].Attributes["value"].Value;

                        return nomSociete;
                    }
                }

                //***************** FIN utilisation la section "appSettings"

                return null; ;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        //********************** lire dans le fichier de configuration et retourne les paramètres de connexion à la BD
        public String[] getParametresConnexionBD(){

            try{
                string chaineCon = null;

                // new xdoc instance 
                XmlDocument xDoc = new XmlDocument();

                //load up the xml from the location 
                //xDoc.Load("C:\\projet C#\\workspace\\Projet Ecole 2\\App.Config");
                //xDoc.Load(AppDomain.CurrentDomain.BaseDirectory.Replace("bin\\Debug\\", "App.Config"));
                xDoc.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
                
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

                if (chaineCon != null) { 
                    String [] table = new String[3];
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
            catch(Exception ex){
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        internal bool enregistrerUtilisateur(UtilisateurBE utilisateur)
        {
            this.utilisateurDA = new UtilisateurDA();

            if (utilisateurDA.ajouter(utilisateur))
            {
                journalDA.journaliser("enregistrement d'un utilisateur de nom " + utilisateur.nom);
                return true;
            }
            return false;
        }

        public bool modifierUtilisateur(UtilisateurBE utilisateur, UtilisateurBE newUtilisateur) {
            
            this.utilisateurDA = new UtilisateurDA();

            if (utilisateurDA.modifier(utilisateur, newUtilisateur))
            {
                journalDA.journaliser("modification des informations de utilisateur de nom " + utilisateur.nom);
                return true;
            }
            return false;
        }

        //****************** fonction qui teste la connexion
        //------------- test de la connexion
        public static bool testConnexion(String serveur, String utilisateur, String password)
        {
            String chaineCon = "SERVER=" + serveur + "; DATABASE=ecole; UID=" + utilisateur + "; PASSWORD=" + password;
            MySqlConnection connexionTest = new MySqlConnection(chaineCon);
            //this.connexion.Open();

            try
            {
                connexionTest.Open();

                if (connexionTest.State == ConnectionState.Open)
                {
                    connexionTest.Close();

                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
