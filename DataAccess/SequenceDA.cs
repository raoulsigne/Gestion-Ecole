using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Ecole.BusinessEntity;
using MySql.Data.MySqlClient;

namespace Ecole.DataAccess
{
    public class SequenceDA : DA<SequenceBE>
    {
        private Connexion con = Connexion.getConnexion();

        //----------------Ajout d'une nouvelle Sequence ------------------------------
        public override Boolean ajouter(SequenceBE S)
        {
            MySqlTransaction transaction = con.connexion.BeginTransaction();
            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "INSERT INTO Sequence (CODESEQ, NOMSEQ, CODETRIMESTRE) VALUES (@codeS, @nomS, @codeT)";

                // utilisation de l'objet voiture passé en paramètre
                cmd.Parameters.AddWithValue("@codeS", S.codeseq);
                cmd.Parameters.AddWithValue("@nomS", S.nomseq);
                cmd.Parameters.AddWithValue("@codeT", S.codetrimestre);

                cmd.Transaction = transaction;
                // Exécution de la commande SQL
                cmd.ExecuteNonQuery();
                cmd.Transaction.Commit();
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

        //--------------------------Suppression d'une Sequence ------

        public override Boolean supprimer(SequenceBE S)
        {
            MySqlTransaction transaction = con.connexion.BeginTransaction();
            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "DELETE FROM Sequence WHERE CODESEQ=@codeS";

                // utilisation de l'objet voiture passé en paramètre
                cmd.Parameters.AddWithValue("@codeS", S.codeseq);

                cmd.Transaction = transaction;
                // Exécution de la commande SQL
                cmd.ExecuteNonQuery();
                cmd.Transaction.Commit();
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

        //--------------------------Modification d'une Sequence -----------

        // mise à jour d'objet, parametre obj, retourne booléen
        public override Boolean modifier(SequenceBE S, SequenceBE newS) {
            MySqlTransaction transaction = con.connexion.BeginTransaction();
            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "UPDATE Sequence SET CODESEQ=@nouveaucodeS, NOMSEQ=@nomS, CODETRIMESTRE=@codeT WHERE CODESEQ=@anciencodeS";

                // utilisation de l'objet voiture passé en paramètre
                cmd.Parameters.AddWithValue("@nouveaucodeS", newS.codeseq);
                cmd.Parameters.AddWithValue("@anciencodeS", S.codeseq);
                cmd.Parameters.AddWithValue("@codeT", newS.codetrimestre);
                cmd.Parameters.AddWithValue("@nomS", newS.nomseq);

                cmd.Transaction = transaction;
                // Exécution de la commande SQL
                cmd.ExecuteNonQuery();
                cmd.Transaction.Commit();
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

        public Boolean modifier(SequenceBE S)
        {
            MySqlTransaction transaction = con.connexion.BeginTransaction();
            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "UPDATE Sequence SET NOMSEQ=@nomS, CODETRIMESTRE=@codeT WHERE CODESEQ=@codeS";

                // utilisation de l'objet voiture passé en paramètre
                cmd.Parameters.AddWithValue("@codeS", S.codeseq);
                cmd.Parameters.AddWithValue("@codeT", S.codetrimestre);
                cmd.Parameters.AddWithValue("@nomS", S.nomseq);

                cmd.Transaction = transaction;
                // Exécution de la commande SQL
                cmd.ExecuteNonQuery();
                cmd.Transaction.Commit();
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

        //---------------------------Fin Modification ------------------------

        public override SequenceBE rechercher(SequenceBE seq)
        {
            string nomseq;
            string codeTrimestre;
            SequenceBE S;

            try
            {
                // ouverture de la connexion
                // this.con.ouvrir();

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT * FROM Sequence WHERE CODESEQ=@codeS";
                cmd.Parameters.AddWithValue("@codeS", seq.codeseq);

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer la voiture à retourner
                    if (dataReader.Read())
                    {
                        nomseq = Convert.ToString(dataReader["NOMSEQ"]);
                        codeTrimestre = Convert.ToString(dataReader["CODETRIMESTRE"]);
                        S = new SequenceBE(seq.codeseq, nomseq, codeTrimestre);
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
        public override List<SequenceBE> listerTous()
        {
            List<SequenceBE> list = new List<SequenceBE>();
            String code;
            String nom;
            String codeTrimestre;
            SequenceBE s;

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT * FROM Sequence";

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet à retourner
                    while (dataReader.Read())
                    {
                        nom = Convert.ToString(dataReader["NOMSEQ"]);
                        code = Convert.ToString(dataReader["CODESEQ"]);
                        codeTrimestre = Convert.ToString(dataReader["CODETRIMESTRE"]);

                        s = new SequenceBE(code, nom, codeTrimestre);
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

        //liste les éléments respectant un critère passé en paramètre
        public override List<SequenceBE> listerSuivantCritere(string critere)
        {
            List<SequenceBE> list = new List<SequenceBE>();
            String code;
            String nom;
            String codeTrimestre;
            SequenceBE s;

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT * FROM Sequence WHERE " + critere;

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet à retourner
                    while (dataReader.Read())
                    {
                        nom = Convert.ToString(dataReader["NOMSEQ"]);
                        code = Convert.ToString(dataReader["CODESEQ"]);
                        codeTrimestre = Convert.ToString(dataReader["CODETRIMESTRE"]);

                        s = new SequenceBE(code, nom, codeTrimestre);
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

        //liste les valeurs d'une colonne            
        public override List<string> listerValeursColonne(string colonne)
        {
            List<String> list = new List<String>();
           
            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT * FROM Sequence ";

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
        //fonction qui compte le nombre d'évaluation d'une séquence pour une classe et une année
        public int getNombreEvaluationDuneSequence(String codeClasse, String codeSequence, int annee)
        {
            ClasseDA classeDA = new ClasseDA();

            ClasseBE classe = new ClasseBE();
            classe.codeClasse = codeClasse;

            List<MatiereBE> LMatiere = classeDA.ListeMatiereDuneClasse(classe, annee);

            if (LMatiere != null && LMatiere.Count != 0)
            {
                int nbEvaluation;

                String codeMatiete = LMatiere.ElementAt(0).codeMat;

                try
                {
                    // Création d'une commande SQL
                    MySqlCommand cmd = con.connexion.CreateCommand();
                    cmd.CommandText = "SELECT count(codeevaluation) FROM evaluer WHERE codemat = '" + codeMatiete + "' AND codeClasse = '" + codeClasse + "' AND codeSeq = '" + codeSequence + "' AND annee = '" + annee + "'";

                    using (MySqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        //fabriquer l'objet à retourner
                        if (dataReader.Read())
                        {
                            nbEvaluation = Convert.ToInt16(dataReader["count(codeevaluation)"]);

                            dataReader.Close();

                            return nbEvaluation;
                        }

                        return 0;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return 0;
                }

            }
            else return 0;

        }

        //fonction qui retourne la liste des codes d'évaluation d'une séquence, pour une classe et une année
        public List<String> getListCodeEvaluationDuneSequence(String codeClasse, String codeSequence, int annee)
        {

            ClasseDA classeDA = new ClasseDA();

            ClasseBE classe = new ClasseBE();
            classe.codeClasse = codeClasse;

            List<MatiereBE> LMatiere = classeDA.ListeMatiereDuneClasse(classe, annee);

            if (LMatiere != null && LMatiere.Count != 0)
            {
                String codeMatiete = LMatiere.ElementAt(0).codeMat;

                List<String> ListCodeEvaluation = new List<String>();
                String codeEvaluation;

                try
                {
                    // Création d'une commande SQL
                    MySqlCommand cmd = con.connexion.CreateCommand();
                    cmd.CommandText = "SELECT codeevaluation FROM evaluer WHERE codemat = '" + codeMatiete + "' AND codeClasse = '" + codeClasse + "' AND codeSeq = '" + codeSequence + "' AND annee = '" + annee + "' ORDER BY codeevaluation";

                    using (MySqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        //fabriquer l'objet à retourner
                        while (dataReader.Read())
                        {
                            codeEvaluation = Convert.ToString(dataReader["codeevaluation"]);

                            ListCodeEvaluation.Add(codeEvaluation);
                        }

                        dataReader.Close();

                        return ListCodeEvaluation;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }
            }
            else return null;

        }

        //fonction qui compte le nombre de  Séquence
        public int getNombreSequence()
        {

            int nbSequence;

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT count(*) FROM sequence";

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet à retourner
                    if (dataReader.Read())
                    {
                        nbSequence = Convert.ToInt16(dataReader["count(*)"]);

                        dataReader.Close();

                        return nbSequence;
                    }

                    return 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }

        //fonction qui retourne la liste des codes de séquence
        public List<String> getListCodeSequence()
        {

            List<String> ListCodeSequence = new List<String>();
            String codeSequence;

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT codeSeq FROM sequence ORDER BY codeSeq";

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet à retourner
                    while (dataReader.Read())
                    {
                        codeSequence = Convert.ToString(dataReader["codeSeq"]);

                        ListCodeSequence.Add(codeSequence);
                    }

                    dataReader.Close();

                    return ListCodeSequence;
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
