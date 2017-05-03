using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Ecole.BusinessEntity;
using MySql.Data.MySqlClient; 

namespace Ecole.DataAccess
{
    public class SanctionnerDA : DA<SanctionnerBE>
    {
        private Connexion con = Connexion.getConnexion();

        //----------------Ajout d'une nouvelle Sanction ------------------------------
        public override Boolean ajouter(SanctionnerBE S)
        {
            MySqlTransaction transaction = con.connexion.BeginTransaction();

            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "REPLACE INTO sanctionner (CODESANCTION, MATRICULE, ANNEE, QUANTITE, DATESANCTION, SEQUENCE,ETAT)" +
                " VALUES (@codeS, @mat, @annee, @qte, @dateS, @sequence,@etat)";

                // utilisation de l'objet voiture passé en paramètre
                cmd.Parameters.AddWithValue("@codeS", S.codesanction);
                cmd.Parameters.AddWithValue("@mat", S.matricule);
                cmd.Parameters.AddWithValue("@annee", S.annee);
                cmd.Parameters.AddWithValue("@qte", S.quantité);
                cmd.Parameters.AddWithValue("@dateS", S.datesanction);
                cmd.Parameters.AddWithValue("@sequence", S.sequence);
                cmd.Parameters.AddWithValue("@etat", S.etat);

                cmd.Transaction = transaction;

                // Exécution de la commande SQL
                cmd.ExecuteNonQuery();

                transaction.Commit();

                return true;
                // Fermeture de la connexion
                //  this.con.fermer();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                transaction.Rollback();

                return false;
            }
        }

        //--------------------------Fin ajout-----------------------------

        //--------------------------Suppression d'une Sanction ------

        public override Boolean supprimer(SanctionnerBE S)
        {
            MySqlTransaction transaction = con.connexion.BeginTransaction();

            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "DELETE FROM sanctionner WHERE CODESANCTION=@codeS and MATRICULE=@mat and annee=@annee and datesanction=@datesanction and sequence=@seq and quantite=@quantite and etat=@etat";

                // utilisation de l'objet voiture passé en paramètre
                cmd.Parameters.AddWithValue("@codeS", S.codesanction);
                cmd.Parameters.AddWithValue("@mat", S.matricule);
                cmd.Parameters.AddWithValue("@seq", S.sequence);
                cmd.Parameters.AddWithValue("@annee", S.annee);
                cmd.Parameters.AddWithValue("@datesanction", S.datesanction);
                cmd.Parameters.AddWithValue("@quantite", S.quantité);
                cmd.Parameters.AddWithValue("@etat", S.etat);

                cmd.Transaction = transaction;

                // Exécution de la commande SQL
                cmd.ExecuteNonQuery();

                transaction.Commit();

                return true;
                // Fermeture de la connexion
                //  this.con.fermer();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                transaction.Rollback();

                return false;
            }
        }

        public Boolean supprimerSuivantTouslesCriteresSaufLaDate(SanctionnerBE S)
        {
            MySqlTransaction transaction = con.connexion.BeginTransaction();

            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "DELETE FROM sanctionner WHERE CODESANCTION=@codeS and MATRICULE=@mat and annee=@annee and sequence=@seq";

                // utilisation de l'objet voiture passé en paramètre
                cmd.Parameters.AddWithValue("@codeS", S.codesanction);
                cmd.Parameters.AddWithValue("@mat", S.matricule);
                cmd.Parameters.AddWithValue("@seq", S.sequence);
                cmd.Parameters.AddWithValue("@annee", S.annee);

                cmd.Transaction = transaction;

                // Exécution de la commande SQL
                cmd.ExecuteNonQuery();

                transaction.Commit();

                return true;
                // Fermeture de la connexion
                //  this.con.fermer();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                transaction.Rollback();

                return false;
            }
        }

        //--------------------------Fin Suppression-----------------------------

        //--------------------------Modification d'une Sanction -----------

        // mise à jour d'objet, parametre obj, retourne booléen
        public override Boolean modifier(SanctionnerBE S, SanctionnerBE newS)
        {
            MySqlTransaction transaction = con.connexion.BeginTransaction();

            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "UPDATE sanctionner SET CODESANCTION=@codeS1, MATRICULE=@mat1, ANNEE=@annee, QUANTITE=@qte, DATESANCTION=@dateS1, SEQUENCE=@sequence1, ETAT=@etat"
                 + " WHERE CODESANCTION=@codeS and MATRICULE=@mat and SEQUENCE=@sequence and DATESANCTION=@dateS";

                // utilisation de l'objet voiture passé en paramètre
                cmd.Parameters.AddWithValue("@codeS1", newS.codesanction);
                cmd.Parameters.AddWithValue("@mat1", newS.matricule);
                cmd.Parameters.AddWithValue("@sequence1", newS.sequence);
                cmd.Parameters.AddWithValue("@dateS1", newS.datesanction.Date);

                cmd.Parameters.AddWithValue("@codeS", S.codesanction);
                cmd.Parameters.AddWithValue("@mat", S.matricule);
                cmd.Parameters.AddWithValue("@annee", newS.annee);
                cmd.Parameters.AddWithValue("@qte", newS.quantité);
                cmd.Parameters.AddWithValue("@dateS", S.datesanction.Date);
                cmd.Parameters.AddWithValue("@sequence", S.sequence);
                cmd.Parameters.AddWithValue("@etat", newS.etat);

                cmd.Transaction = transaction;

                // Exécution de la commande SQL
                cmd.ExecuteNonQuery();

                transaction.Commit();

                return true;
                // Fermeture de la connexion
                //  this.con.fermer();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                transaction.Rollback();

                return false;
            }
        }

        //---------------------------Fin Modification --------------------------

        public override SanctionnerBE rechercher(SanctionnerBE sanctionner)
        {
            int annee;
            int quantité;
            DateTime datesanction;
            SanctionnerBE S;

            try
            {
                // ouverture de la connexion
                // this.con.ouvrir();

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT * FROM sanctionner WHERE CODESANCTION=@codeS and MATRICULE=@mat and sequence=@sequence and annee=@annee and datesanction=@datesanction";
                cmd.Parameters.AddWithValue("@codeS", sanctionner.codesanction);
                cmd.Parameters.AddWithValue("@mat", sanctionner.matricule);
                cmd.Parameters.AddWithValue("@sequence", sanctionner.sequence);
                cmd.Parameters.AddWithValue("@annee", sanctionner.annee);
                cmd.Parameters.AddWithValue("@datesanction", sanctionner.datesanction);

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer la voiture à retourner
                    if (dataReader.Read())
                    {
                        annee = Convert.ToInt32(dataReader["ANNEE"]);
                        quantité = Convert.ToInt32(dataReader["QUANTITE"]);
                        datesanction = Convert.ToDateTime(dataReader["DATESANCTION"]);
                        
                        S = new SanctionnerBE(sanctionner.codesanction, sanctionner.matricule, annee, quantité, datesanction.Date,
                                        Convert.ToString(dataReader["sequence"]),Convert.ToString(dataReader["etat"]));
                        dataReader.Close();
                        // this.con.fermer();
                        return S;
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

        //fonction qui recherche un élèment suivant tous les champs clé, sauf la date
        public SanctionnerBE rechercherSuivantTousLesChampsSaufLaDate(SanctionnerBE sanctionner)
        {
            int annee;
            int quantité;
            DateTime datesanction;
            SanctionnerBE S;

            try
            {
                // ouverture de la connexion
                // this.con.ouvrir();

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT * FROM sanctionner WHERE CODESANCTION=@codeS and MATRICULE=@mat and sequence=@sequence and annee=@annee";
                cmd.Parameters.AddWithValue("@codeS", sanctionner.codesanction);
                cmd.Parameters.AddWithValue("@mat", sanctionner.matricule);
                cmd.Parameters.AddWithValue("@sequence", sanctionner.sequence);
                cmd.Parameters.AddWithValue("@annee", sanctionner.annee);

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer la voiture à retourner
                    if (dataReader.Read())
                    {
                        annee = Convert.ToInt32(dataReader["ANNEE"]);
                        quantité = Convert.ToInt32(dataReader["QUANTITE"]);
                        datesanction = Convert.ToDateTime(dataReader["DATESANCTION"]);

                        S = new SanctionnerBE(sanctionner.codesanction, sanctionner.matricule, annee, quantité, datesanction.Date,
                                        Convert.ToString(dataReader["sequence"]), Convert.ToString(dataReader["etat"]));
                        dataReader.Close();
                        // this.con.fermer();
                        return S;
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


            //-----------------------------Fin de la recherche----------


        //----------------debut lister --------------------------------------------
        public override List<SanctionnerBE> listerTous()
        {
            List<SanctionnerBE> list = new List<SanctionnerBE>();
            String code;
            String matricule;
            int annee;
            int quantite;
            DateTime date;
            SanctionnerBE s;

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT * FROM Sanctionner";

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet acheter à retourner
                    while (dataReader.Read())
                    {
                        code = Convert.ToString(dataReader["codesanction"]);
                        matricule = Convert.ToString(dataReader["matricule"]);
                        annee = Convert.ToInt32(dataReader["annee"]);
                        quantite = Convert.ToInt32(dataReader["quantite"]);
                        date = Convert.ToDateTime(dataReader["datesanction"]);
                        s = new SanctionnerBE(code, matricule, annee, quantite, date,
                                        Convert.ToString(dataReader["sequence"]), Convert.ToString(dataReader["etat"]));
                        list.Add(s);
                    }
                    dataReader.Close();

                    if (list.Count > 0)
                        return list;
                    else
                        return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        //----------------fin lister --------------------------------------------


        public override List<SanctionnerBE> listerSuivantCritere(string critere)
        {
            List<SanctionnerBE> list = new List<SanctionnerBE>();
            String code;
            String matricule;
            int annee;
            int quantite;
            DateTime date;
            SanctionnerBE s;

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT * FROM Sanctionner WHERE " + critere;

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet acheter à retourner
                    while (dataReader.Read())
                    {
                        code = Convert.ToString(dataReader["codesanction"]);
                        matricule = Convert.ToString(dataReader["matricule"]);
                        annee = Convert.ToInt32(dataReader["annee"]);
                        quantite = Convert.ToInt32(dataReader["quantite"]);
                        date = Convert.ToDateTime(dataReader["datesanction"]);
                        s = new SanctionnerBE(code, matricule, annee, quantite, date, Convert.ToString(dataReader["sequence"]), Convert.ToString(dataReader["etat"]));
                        list.Add(s);
                    }
                    dataReader.Close();

                    return list;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public override List<string> listerValeursColonne(string colonne)
        {
            List<String> list = new List<String>();

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT * FROM Sanctionner";

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet à retourner
                    while (dataReader.Read())
                    {
                        list.Add(Convert.ToString(dataReader[colonne]));
                    }
                    dataReader.Close();

                    return list;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        //-----------debut compter -----------------
        public int compter()
        {
            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT COUNT(*) FROM Sanctionner";

                // Exécution de la commande SQL
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -1;
            }
        }

        public List<SanctionnerBE> elevesSanctionner_PourUneSanction_DansUneClasse(string classe, string sanction, int annee, string sequence, string str_date)
        {
            List<SanctionnerBE> list = new List<SanctionnerBE>();
            String matricule;
            int quantite;
            DateTime date;
            SanctionnerBE s;

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT s.* FROM Sanctionner s, Inscrire i WHERE s.datesanction = "+ "'" + str_date + "' and s.annee = " + annee +
                                    " and s.matricule = i.matricule and i.codeclasse = "+"'"+classe+"' and s.sequence = " +"'"+ sequence +"' and codesanction = " + "'" + sanction + "'";

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet acheter à retourner
                    while (dataReader.Read())
                    {
                        matricule = Convert.ToString(dataReader["matricule"]);
                        annee = Convert.ToInt32(dataReader["annee"]);
                        quantite = Convert.ToInt32(dataReader["quantite"]);
                        date = Convert.ToDateTime(dataReader["datesanction"]);
                        s = new SanctionnerBE(sanction, matricule, annee, quantite, date, Convert.ToString(dataReader["sequence"]), Convert.ToString(dataReader["etat"]));
                        list.Add(s);
                    }
                    dataReader.Close();

                    if (list.Count > 0)
                        return list;
                    else
                        return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        internal List<SanctionnerBE> elevesSanctionner_PourUneSanction_DansUneClasse(string classe, string sanction, int annee, string sequence)
        {
            List<SanctionnerBE> list = new List<SanctionnerBE>();
            String matricule;
            int quantite;
            DateTime date;
            SanctionnerBE s;

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT s.* FROM Sanctionner s, Inscrire i WHERE s.quantite != " + "'"+ 0 +"' and s.annee = " + annee +
                                    " and s.matricule = i.matricule and i.codeclasse = " + "'" + classe + "' and s.sequence = " + "'" + sequence + "' and codesanction = " + "'" + sanction + "'";

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet acheter à retourner
                    while (dataReader.Read())
                    {
                        matricule = Convert.ToString(dataReader["matricule"]);
                        annee = Convert.ToInt32(dataReader["annee"]);
                        quantite = Convert.ToInt32(dataReader["quantite"]);
                        date = Convert.ToDateTime(dataReader["datesanction"]);
                        s = new SanctionnerBE(sanction, matricule, annee, quantite, date, Convert.ToString(dataReader["sequence"]), Convert.ToString(dataReader["etat"]));
                        list.Add(s);
                    }
                    dataReader.Close();

                    if (list.Count > 0)
                        return list;
                    else
                        return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        //************************ liste les sanctions d'un élève pour une séquence
        public List<SanctionnerBE> getListSanctionSequentielleEleve(String matricule, int annee, String codeSequence)
        {
            //String matricule; 
            String codesanction;
            DateTime dateSanction;
            String etat;
            int quantite;

            List<SanctionnerBE> List = new List<SanctionnerBE>();
            SanctionnerBE santion;

            try
            {
                // ouverture de la connexion
                // this.con.ouvrir();

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT matricule, codeSanction, annee, SUM(quantite), datesanction, sequence, etat " +
                    "FROM sanctionner " +
                    "WHERE matricule = '" + matricule + "' " +
                    "AND annee = '" + annee + "' " +
                    "AND sequence = '" + codeSequence + "' " +
                    "GROUP BY matricule, codesanction, etat";

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer la voiture à retourner
                    while (dataReader.Read())
                    {
                        etat = Convert.ToString(dataReader["etat"]);
                        codesanction = Convert.ToString(dataReader["codeSanction"]);
                        quantite = Convert.ToInt16(dataReader["SUM(quantite)"]);
                        dateSanction = Convert.ToDateTime(dataReader["datesanction"]);

                        santion = new SanctionnerBE(codesanction, matricule, annee, quantite, dateSanction, codeSequence, etat);
                        // this.con.fermer();
                        List.Add(santion);
                    }

                    dataReader.Close();

                    return List;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        //************************ liste les sanctions d'un élève pour un trimestre
        public List<SanctionnerBE> getListSanctionTrimestrielleEleve(String matricule, int annee, String codeTrimestre)
        {
            //String matricule; 
            String codesanction;
            DateTime dateSanction;
            String codeSequence;
            String etat;
            int quantite;

            List<SanctionnerBE> List = new List<SanctionnerBE>();
            SanctionnerBE santion;

            try
            {
                // ouverture de la connexion
                // this.con.ouvrir();

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT matricule, codeSanction, annee, SUM(quantite), datesanction, sequence, etat " +
                    "FROM sanctionner " +
                    "WHERE matricule = '" + matricule + "' " +
                    "AND annee = '" + annee + "' " +
                    "AND sequence in (SELECT codeseq FROM sequence WHERE codetrimestre = '" + codeTrimestre + "') " +
                    "GROUP BY matricule, codesanction, etat";

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer la voiture à retourner
                    while (dataReader.Read())
                    {
                        etat = Convert.ToString(dataReader["etat"]);
                        codesanction = Convert.ToString(dataReader["codeSanction"]);
                        quantite = Convert.ToInt16(dataReader["SUM(quantite)"]);
                        dateSanction = Convert.ToDateTime(dataReader["datesanction"]);
                        codeSequence = Convert.ToString(dataReader["sequence"]);

                        santion = new SanctionnerBE(codesanction, matricule, annee, quantite, dateSanction, codeSequence, etat);
                        // this.con.fermer();
                        List.Add(santion);
                    }

                    dataReader.Close();

                    return List;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }


        //************************ liste les sanctions d'un élève pour une Année
        public List<SanctionnerBE> getListSanctionAnuelleEleve(String matricule, int annee)
        {
            //String matricule; 
            String codesanction;
            DateTime dateSanction;
            String codeSequence;
            String etat;
            int quantite;

            List<SanctionnerBE> List = new List<SanctionnerBE>();
            SanctionnerBE santion;

            try
            {
                // ouverture de la connexion
                // this.con.ouvrir();

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT matricule, codeSanction, annee, SUM(quantite), datesanction, sequence, etat " +
                    "FROM sanctionner " +
                    "WHERE matricule = '" + matricule + "' " +
                    "AND annee = '" + annee + "' " +
                    "GROUP BY matricule, codesanction, etat";

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer la voiture à retourner
                    while (dataReader.Read())
                    {
                        etat = Convert.ToString(dataReader["etat"]);
                        codesanction = Convert.ToString(dataReader["codeSanction"]);
                        quantite = Convert.ToInt16(dataReader["SUM(quantite)"]);
                        dateSanction = Convert.ToDateTime(dataReader["datesanction"]);
                        codeSequence = Convert.ToString(dataReader["sequence"]);

                        santion = new SanctionnerBE(codesanction, matricule, annee, quantite, dateSanction, codeSequence, etat);
                        // this.con.fermer();
                        List.Add(santion);
                    }

                    dataReader.Close();

                    return List;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }


        //fonction qui liste les codes des sanctions
        public List<String> listerCodeSanction()
        {
            List<String> list = new List<String>();
            String code;

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT codeSanction FROM Sanctionner ORDER BY codeSanction";

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet acheter à retourner
                    while (dataReader.Read())
                    {
                        code = Convert.ToString(dataReader["codesanction"]);
                        list.Add(code);
                    }
                    dataReader.Close();

                    return list;
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
