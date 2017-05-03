using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using Ecole.BusinessEntity;

namespace Ecole.DataAccess
{
    class StatistiqueClasseDA
    {
        private Connexion con = Connexion.getConnexion();

        //-------- méthodes qui recherche les statistiques d'une séquence d'une classe
        public StatistiqueClasseBE getStatistiqueDuneSequence(String codeClasse, int annee, String codeSequence) {
            //string codeClasse;
            int effectif;
            int nbAdmis;
            string pourcentageAdmis;
            int nbEchec;
            string pourcentageEchec;

            StatistiqueClasseBE stat;

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT e.codeClasse, e.effectif, r.nbAdmis, ((r.nbAdmis / e.effectif)*100) as PourcentageAdmis, "+
                                   "(e.effectif - r.nbAdmis) as nbEchec, (((e.effectif - r.nbAdmis) / e.effectif))*100 as PourcentageEchec "+
                                    "FROM   (SELECT codeClasse, count(*) as effectif "+
                                    "FROM inscrire "+
                                    "WHERE codeClasse = '"+codeClasse+"' AND annee = '"+annee+"' GROUP BY codeClasse) e, "+
                                    "(SELECT count(*) as nbAdmis "+
                                    "FROM resultat "+
                                    "WHERE matricule in (SELECT matricule FROM inscrire WHERE codeClasse = '"+codeClasse+"' AND annee = '"+annee+"') "+
                                    "AND codeSeq = '"+codeSequence+"' AND annee = '"+annee+"' AND decision = 'Admis') r;";

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet à retourner
                    if (dataReader.Read())
                    {
                        effectif = Convert.ToInt16(dataReader["effectif"]);
                        nbAdmis = Convert.ToInt16(dataReader["nbAdmis"]);
                        pourcentageAdmis = Convert.ToString(Math.Round(Convert.ToDecimal(dataReader["PourcentageAdmis"]), 2)) + "%";
                        nbEchec = Convert.ToInt16(dataReader["nbEchec"]);
                        pourcentageEchec = Convert.ToString(Math.Round(Convert.ToDecimal(dataReader["PourcentageEchec"]), 2)) + "%";

                        stat = new StatistiqueClasseBE(codeClasse, effectif, nbAdmis, pourcentageAdmis, nbEchec, pourcentageEchec);

                        return stat;
                    }

                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        //-------- méthodes qui recherche les statistiques d'un trimestre d'une classe
        public StatistiqueClasseBE getStatistiqueDunTrimestre(String codeClasse, int annee, String codeTrimestre)
        {
            //string codeClasse;
            int effectif;
            int nbAdmis;
            string pourcentageAdmis;
            int nbEchec;
            string pourcentageEchec;

            StatistiqueClasseBE stat;

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT e.codeClasse, e.effectif, r.nbAdmis, ((r.nbAdmis / e.effectif)*100) as PourcentageAdmis, " +
                                   "(e.effectif - r.nbAdmis) as nbEchec, (((e.effectif - r.nbAdmis) / e.effectif))*100 as PourcentageEchec " +
                                    "FROM   (SELECT codeClasse, count(*) as effectif " +
                                    "FROM inscrire " +
                                    "WHERE codeClasse = '" + codeClasse + "' AND annee = '" + annee + "' GROUP BY codeClasse) e, " +
                                    "(SELECT count(*) as nbAdmis " +
                                    "FROM resultattrimestriel " +
                                    "WHERE matricule in (SELECT matricule FROM inscrire WHERE codeClasse = '" + codeClasse + "' AND annee = '" + annee + "') " +
                                    "AND codeTrimestre = '" + codeTrimestre + "' AND annee = '" + annee + "' AND decision = 'Admis') r;";

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet à retourner
                    if (dataReader.Read())
                    {
                        effectif = Convert.ToInt16(dataReader["effectif"]);
                        nbAdmis = Convert.ToInt16(dataReader["nbAdmis"]);
                        pourcentageAdmis = Convert.ToString(Math.Round(Convert.ToDecimal(dataReader["PourcentageAdmis"]), 2)) + "%";
                        nbEchec = Convert.ToInt16(dataReader["nbEchec"]);
                        pourcentageEchec = Convert.ToString(Math.Round(Convert.ToDecimal(dataReader["PourcentageEchec"]), 2)) + "%";

                        stat = new StatistiqueClasseBE(codeClasse, effectif, nbAdmis, pourcentageAdmis, nbEchec, pourcentageEchec);
                        return stat;
                    }

                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        //-------- méthodes qui recherche les statistiques d'une année d'une classe
        public StatistiqueClasseBE getStatistiqueDuneAnnee(String codeClasse, int annee)
        {
            //string codeClasse;
            int effectif;
            int nbAdmis;
            string pourcentageAdmis;
            int nbEchec;
            string pourcentageEchec;

            StatistiqueClasseBE stat;

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT e.codeClasse, e.effectif, r.nbAdmis, ((r.nbAdmis / e.effectif)*100) as PourcentageAdmis, " +
                                   "(e.effectif - r.nbAdmis) as nbEchec, (((e.effectif - r.nbAdmis) / e.effectif))*100 as PourcentageEchec " +
                                    "FROM   (SELECT codeClasse, count(*) as effectif " +
                                    "FROM inscrire " +
                                    "WHERE codeClasse = '" + codeClasse + "' AND annee = '" + annee + "' GROUP BY codeClasse) e, " +
                                    "(SELECT count(*) as nbAdmis " +
                                    "FROM resultatannuel " +
                                    "WHERE matricule in (SELECT matricule FROM inscrire WHERE codeClasse = '" + codeClasse + "' AND annee = '" + annee + "') " +
                                    "AND annee = '" + annee + "' AND decision = 'Admis') r;";

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet à retourner
                    while (dataReader.Read())
                    {
                        //codeClasse = Convert.ToString(dataReader["codeclasse"]);
                        effectif = Convert.ToInt16(dataReader["effectif"]);
                        nbAdmis = Convert.ToInt16(dataReader["nbAdmis"]);
                        pourcentageAdmis = Convert.ToString(Math.Round(Convert.ToDecimal(dataReader["PourcentageAdmis"]), 2)) + "%";
                        nbEchec = Convert.ToInt16(dataReader["nbEchec"]);
                        pourcentageEchec = Convert.ToString(Math.Round(Convert.ToDecimal(dataReader["PourcentageEchec"]), 2)) + "%";

                        stat = new StatistiqueClasseBE(codeClasse, effectif, nbAdmis, pourcentageAdmis, nbEchec, pourcentageEchec);

                        return stat;
                    }

                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }


    }
}
