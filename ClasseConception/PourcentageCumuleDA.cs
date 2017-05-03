using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using Ecole.BusinessEntity;
using Ecole.ClasseConception;
using Ecole.UI;
using System.Globalization;
using System.Threading;

namespace Ecole.DataAccess
{
    public class PourcentageCumuleDA
    {
        private Connexion con = Connexion.getConnexion();

        // fonctionne qui retourne la moyenne de Classe sur une matière donnée
        public Double getMoyenneDeClassePrUneSequence(string codeClasse, string codeMatiere, string codeSeq, int annee)
        {
            
            Double m = 0;

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT DISTINCT m.moyenneClasse as  moyenneClasse FROM moyennes m " +
                    "WHERE m.annee = " + annee + " and m.codeMat =  '" + codeMatiere + "' and m.codeSeq = '" + codeSeq + "' and m.matricule in (SELECT matricule FROM inscrire WHERE annee = " + annee + " and codeClasse = '"+codeClasse+"')";

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet à retourner
                    while (dataReader.Read())
                    {
                        m = Convert.ToDouble(dataReader["moyenneClasse"]);
                    }
                    dataReader.Close();

                    return m;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return m;
            }
        }

        // fonctionne qui retourne la moyenne de Classe sur une matière donnée
        public Double getMoyenneDeClassePrUnTrimestre(string codeClasse, string codeMatiere, string codeTrimestre, int annee)
        {

            Double m = 0;

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT DISTINCT m.moyenneClasse as  moyenneClasse FROM moyennestrimestriels m " +
                    "WHERE m.annee = " + annee + " and m.codeMat =  '" + codeMatiere + "' and m.codeTrimestre = '" + codeTrimestre + "' and m.matricule in (SELECT matricule FROM inscrire WHERE annee = " + annee + " and codeClasse = '" + codeClasse + "')";

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet à retourner
                    while (dataReader.Read())
                    {
                        m = Convert.ToDouble(dataReader["moyenneClasse"]);
                    }
                    dataReader.Close();

                    return m;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return m;
            }
        }

        // fonctionne qui retourne la moyenne de Classe sur une matière donnée
        public Double getMoyenneDeClassePrUneAnnee(string codeClasse, string codeMatiere, int annee)
        {

            Double m = 0;

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT DISTINCT m.moyenneClasse as  moyenneClasse FROM moyennesannuelles m " +
                    "WHERE m.annee = " + annee + " and m.codeMat =  '" + codeMatiere + "' and m.matricule in (SELECT matricule FROM inscrire WHERE annee = " + annee + " and codeClasse = '" + codeClasse + "')";

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet à retourner
                    while (dataReader.Read())
                    {
                        m = Convert.ToDouble(dataReader["moyenneClasse"]);
                    }
                    dataReader.Close();

                    return m;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return m;
            }
        }

        //fonction qui retourne le nombre de personne ayant une note comprise ds l'intervalle pour une matière 
        // d'un classe donné et une séquence donné
        public Double getNbrPersonneAyantUneMoyenneDsUnIntervalPourUneSequence(string codeClasse, string codeMatiere, string codeSeq, int annee, int min, int max)
        {

            int m = 0;

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT count(*) as nombre FROM moyennes WHERE codeMat = '"+codeMatiere+"' and codeSeq = '"+codeSeq+"' and annee = "+annee+" and moyenne > "+min+" and moyenne <= "+max+" "+
                " and matricule in (SELECT matricule FROM inscrire WHERE codeClasse = '"+codeClasse+"' and annee = "+annee+")";

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet à retourner
                    while (dataReader.Read())
                    {
                        m = Convert.ToInt16(dataReader["nombre"]);
                    }
                    dataReader.Close();

                    return m;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return m;
            }
        }

        //fonction qui retourne le nombre de personne ayant une note comprise ds l'intervalle pour une matière 
        // d'un classe donné et une séquence donné
        public Double getNbrPersonneAyantUneMoyenneDsUnIntervalPourUnTrimestre(string codeClasse, string codeMatiere, string codeTrimestre, int annee, int min, int max)
        {

            int m = 0;

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT count(*) as nombre FROM moyennestrimestriels WHERE codeMat = '" + codeMatiere + "' and codeTrimestre = '" + codeTrimestre + "' and annee = " + annee + " and moyenne > " + min + " and moyenne <= " + max + " " +
                " and matricule in (SELECT matricule FROM inscrire WHERE codeClasse = '" + codeClasse + "' and annee = " + annee + ")";

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet à retourner
                    while (dataReader.Read())
                    {
                        m = Convert.ToInt16(dataReader["nombre"]);
                    }
                    dataReader.Close();

                    return m;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return m;
            }
        }

        //fonction qui retourne le nombre de personne ayant une note comprise ds l'intervalle pour une matière 
        // d'un classe donné et une séquence donné
        public Double getNbrPersonneAyantUneMoyenneDsUnIntervalPourUneAnnee(string codeClasse, string codeMatiere, int annee, int min, int max)
        {

            int m = 0;

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT count(*) as nombre FROM moyennesannuelles WHERE codeMat = '" + codeMatiere + "' and annee = " + annee + " and moyenne > " + min + " and moyenne <= " + max + " " +
                " and matricule in (SELECT matricule FROM inscrire WHERE codeClasse = '" + codeClasse + "' and annee = " + annee + ")";

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet à retourner
                    while (dataReader.Read())
                    {
                        m = Convert.ToInt16(dataReader["nombre"]);
                    }
                    dataReader.Close();

                    return m;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return m;
            }
        }


        // fonction qui retourne le nombre de personne ayant composé à un matière donné pour une classe, une séquence et une année donné
        public int getEffectifPourUneSequence(string codeClasse, string codeMatiere, string codeSeq, int annee)
        {

            int m = 0;

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT count(*) as nombre FROM moyennes WHERE codeMat = '"+codeMatiere+"' and codeSeq = '"+codeSeq+"' and annee = "+annee+
   " and matricule in (SELECT matricule FROM inscrire WHERE codeClasse = '"+codeClasse+"' and annee = "+annee+")";

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet à retourner
                    while (dataReader.Read())
                    {
                        m = Convert.ToInt16(dataReader["nombre"]);
                    }
                    dataReader.Close();

                    return m;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return m;
            }
        }

        // fonction qui retourne le nombre de personne ayant composé à un matière donné pour une classe, une séquence et une année donné
        public int getEffectifPourUnTrimestre(string codeClasse, string codeMatiere, string codeTrimestre, int annee)
        {

            int m = 0;

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT count(*) as nombre FROM moyennestrimestriels WHERE codeMat = '" + codeMatiere + "' and codeTrimestre = '" + codeTrimestre + "' and annee = " + annee +
   " and matricule in (SELECT matricule FROM inscrire WHERE codeClasse = '" + codeClasse + "' and annee = " + annee + ")";

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet à retourner
                    while (dataReader.Read())
                    {
                        m = Convert.ToInt16(dataReader["nombre"]);
                    }
                    dataReader.Close();

                    return m;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return m;
            }
        }


        // fonction qui retourne le nombre de personne ayant composé à un matière donné pour une classe, une séquence et une année donné
        public int getEffectifPourUneAnnee(string codeClasse, string codeMatiere, int annee)
        {

            int m = 0;

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT count(*) as nombre FROM moyennesannuelles WHERE codeMat = '" + codeMatiere + "' and annee = " + annee +
   " and matricule in (SELECT matricule FROM inscrire WHERE codeClasse = '" + codeClasse + "' and annee = " + annee + ")";

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet à retourner
                    while (dataReader.Read())
                    {
                        m = Convert.ToInt16(dataReader["nombre"]);
                    }
                    dataReader.Close();

                    return m;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return m;
            }
        }


        // fonction qui retourne le nom de l'enseignant d'une matière
        public String getNomEnseignantDuneMatiere(string codeClasse, string codeMatiere, int annee)
        {

            String nom = "";

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT e.nomProf FROM programmer p, enseignant e "+
                                " WHERE p.codeProf = e.codeProf and p.annee = "+annee+" and p.codeClasse = '"+codeClasse+"' and p.codeMat = '"+codeMatiere+"';";

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet à retourner
                    while (dataReader.Read())
                    {
                        nom = Convert.ToString(dataReader["nomProf"]);
                    }
                    dataReader.Close();

                    return nom;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return nom;
            }
        }

        // fonction qui retourne le nombre de sanction pour une discipline donné
        public int getNombreSanctionDuneDiscipline(string codeClasse, string codeSanction, string codeSequence, int annee, string etat)
        {

            int quantite = 0;

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT COUNT(t.quantite) as quantite"+
                                    " FROM (SELECT s.codeSanction, d.nomSanction, s.sequence, s.matricule, count(s.quantite) as quantite "+
                                    " FROM discipline d, sanctionner s "+
                                    " WHERE s.codeSanction = d.codeSanction and s.annee = "+annee+" and s.sequence = '"+codeSequence+"' and s.codeSanction = '"+codeSanction+"' and s.etat = '"+etat+"' "+
                                    " and s.matricule in (SELECT matricule from inscrire WHERE annee = 2016 and codeClasse = '"+codeClasse+"') "+
                                    " GROUP BY s.matricule) t; ";

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet à retourner
                    while (dataReader.Read())
                    {
                        quantite = Convert.ToInt16(dataReader["quantite"]);
                    }
                    dataReader.Close();

                    return quantite;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return quantite;
            }
        }

        // fonction qui retourne le nombre de personne ayant eu une moyenne supérieure à 10/20
        public int getNombreAyantUneMoyenneSupA10(string codeClasse, string codeSequence, int annee)
        {

            int quantite = 0;

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT COUNT(*) as quantite FROM resultat " +
                                    " WHERE moyenne >= 10 and annee = " + annee + " and codeSeq = '" + codeSequence + "' " +
                                    " and matricule in (SELECT matricule FROM inscrire WHERE annee = "+annee+" and codeClasse = '"+codeClasse+"');";

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet à retourner
                    while (dataReader.Read())
                    {
                        quantite = Convert.ToInt16(dataReader["quantite"]);
                    }
                    dataReader.Close();

                    return quantite;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return quantite;
            }
        }

        // fonction qui retourne le nombre de personne ayant eu une moyenne inférieure à 10/20
        public int getNombreAyantUneMoyenneInfA10(string codeClasse, string codeSequence, int annee)
        {

            int quantite = 0;

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT COUNT(*) as quantite FROM resultat " +
                                    " WHERE moyenne < 10 and annee = " + annee + " and codeSeq = '" + codeSequence + "' " +
                                    " and matricule in (SELECT matricule FROM inscrire WHERE annee = " + annee + " and codeClasse = '" + codeClasse + "');";

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet à retourner
                    while (dataReader.Read())
                    {
                        quantite = Convert.ToInt16(dataReader["quantite"]);
                    }
                    dataReader.Close();

                    return quantite;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return quantite;
            }
        }

        // fonction qui retourne le nombre de personne ayant eu une moyenne inférieure à 10/20
        public int getMoyenneClassePourUnResultatSequentiel(string codeClasse, string codeSequence, int annee)
        {

            int quantite = 0;

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT DISTINCT moyenneClasse FROM resultat " +
                                    " WHERE annee = "+annee+" and codeSeq = '" + codeSequence + "' " +
                                    " and matricule in (SELECT matricule FROM inscrire WHERE annee = " + annee + " and codeClasse = '" + codeClasse + "');";

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet à retourner
                    while (dataReader.Read())
                    {
                        quantite = Convert.ToInt16(dataReader["moyenneClasse"]);
                    }
                    dataReader.Close();

                    return quantite;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return quantite;
            }
        }

        // fonction qui retourne la moyenne minimale de la classe
        public double getResultatSequentielMinimal(string codeClasse, string codeSequence, int annee)
        {

            double quantite = 0;

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT Min(moyenne) as moyenne FROM resultat " +
                                    " WHERE annee = " + annee + " and codeSeq = '" + codeSequence + "' " +
                                    " and matricule in (SELECT matricule FROM inscrire WHERE annee = " + annee + " and codeClasse = '" + codeClasse + "');";

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet à retourner
                    while (dataReader.Read())
                    {
                        quantite = Convert.ToDouble(dataReader["moyenne"]);
                    }
                    dataReader.Close();

                    return quantite;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return quantite;
            }
        }

        // fonction qui retourne la moyenne maximale de la classe
        public double getResultatSequentielMaximal(string codeClasse, string codeSequence, int annee)
        {

            double quantite = 0;

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT Max(moyenne) as moyenne FROM resultat " +
                                    " WHERE annee = " + annee + " and codeSeq = '" + codeSequence + "' " +
                                    " and matricule in (SELECT matricule FROM inscrire WHERE annee = " + annee + " and codeClasse = '" + codeClasse + "');";

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet à retourner
                    while (dataReader.Read())
                    {
                        quantite = Convert.ToDouble(dataReader["moyenne"]);
                    }
                    dataReader.Close();

                    return quantite;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return quantite;
            }
        }

        // fonction qui retourne l'écart type pour une classe, une séquence et une année donné
        public double getEcartTypePourUneSequence(string codeClasse, string codeSeq, int annee, int effectifTotal)
        {

            double m = 0;

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT SUM((moyenne - moyenneClasse) * (moyenne - moyenneClasse)) as nombre"+
                                " FROM resultat "+
                                " WHERE annee = 2016 and codeSeq = '"+codeSeq+"' "+
                                " and matricule in (SELECT matricule FROM inscrire WHERE annee = "+annee+" and codeClasse = '"+codeClasse+"');";

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet à retourner
                    while (dataReader.Read())
                    {
                        m = Convert.ToDouble(dataReader["nombre"]);

                        m = Math.Round(Math.Sqrt(m / effectifTotal), 2);
                    }
                    dataReader.Close();

                    return m;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return m;
            }
        }
        
    }

}
