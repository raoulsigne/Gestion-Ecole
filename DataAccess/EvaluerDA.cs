using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using Ecole.BusinessEntity;

namespace Ecole.DataAccess
{
    public class EvaluerDA : DA<EvaluerBE>
    {
        private Connexion con = Connexion.getConnexion();

        //----------------ajouter CategorieEleve -----------------//
        public override Boolean ajouter(EvaluerBE entity)
        {
            MySqlTransaction tx = con.connexion.BeginTransaction();
            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "INSERT INTO Evaluer (codeevaluation, codemat, codeclasse, poids, annee, codeseq) VALUES (@code, @codemat, @codeclasse, @poids, @annee, @codeseq)";
                cmd.Parameters.AddWithValue("@code", entity.codeEvaluation);
                cmd.Parameters.AddWithValue("@codemat", entity.codeMat);
                cmd.Parameters.AddWithValue("@codeclasse", entity.codeClasse);
                cmd.Parameters.AddWithValue("@poids", entity.poids);
                cmd.Parameters.AddWithValue("@annee", entity.annee);
                cmd.Parameters.AddWithValue("@codeseq", entity.codeSequence);
                // Exécution de la commande SQL
                cmd.Transaction = tx;
                cmd.ExecuteNonQuery();
                tx.Commit();

                return true;
            }
            catch (Exception ex)
            {
                tx.Rollback();
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        //----------------fin ajouter -------------------------------//

        //----------------debut supprimer -----------------//
        public override Boolean supprimer(EvaluerBE entity)
        {
            MySqlTransaction tx = con.connexion.BeginTransaction();
            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "DELETE FROM Evaluer WHERE codeevaluation = @codeeval AND codemat = @codemat AND codeclasse = @codeclasse AND codeseq = @codeseq";
                cmd.Parameters.AddWithValue("@codeeval", entity.codeEvaluation);
                cmd.Parameters.AddWithValue("@codemat", entity.codeMat);
                cmd.Parameters.AddWithValue("@codeclasse", entity.codeClasse);
                cmd.Parameters.AddWithValue("@codeseq", entity.codeSequence);

                // Exécution de la commande SQL
                cmd.Transaction = tx;
                cmd.ExecuteNonQuery();
                tx.Commit();

                return true;
            }
            catch (Exception ex)
            {
                tx.Rollback();
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        //----------------fin supprimer ---------------------//

        //----------------chercher Acheter -----------------//
        public override EvaluerBE rechercher(EvaluerBE entity)
        {
            String codeevaluation;
            String codemat;
            String codeclasse;
            int poids;
            int annee;
            String codeSequence;
            EvaluerBE e;

            try
            {
                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT * FROM Evaluer WHERE codeevaluation = @codeeval AND codemat = @codemat AND codeclasse = @codeclasse AND codeseq=@codeseq";
                cmd.Parameters.AddWithValue("@codeeval", entity.codeEvaluation);
                cmd.Parameters.AddWithValue("@codemat", entity.codeMat);
                cmd.Parameters.AddWithValue("@codeclasse", entity.codeClasse);
                cmd.Parameters.AddWithValue("@codeseq", entity.codeSequence);

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet acheter à retourner
                    if (dataReader.Read())
                    {
                        codeclasse = Convert.ToString(dataReader["codeclasse"]);
                        codemat = Convert.ToString(dataReader["codemat"]);
                        codeevaluation = Convert.ToString(dataReader["codeevaluation"]);
                        poids = Convert.ToInt32(dataReader["poids"]);
                        annee = Convert.ToInt32(dataReader["annee"]);
                        codeSequence = Convert.ToString(dataReader["codeseq"]);
                        e = new EvaluerBE(codeevaluation, codemat, codeclasse, poids, annee, codeSequence);
                        dataReader.Close();
                        return e;
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
        //----------------Fin chercher ------------------------------------//

        //----------------debut modifier ---------------//
        public override Boolean modifier(EvaluerBE entity, EvaluerBE newEntity)
        {
            MySqlTransaction tx = con.connexion.BeginTransaction();
            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                cmd.CommandText = "UPDATE evaluer SET codeevaluation=@nouveaucodeevaluation, codemat=@nouveaucodemat, codeclasse=@nouveaucodeclasse, codeseq=@nouveaucodeseq, poids=@poids, annee=@annee WHERE codeevaluation=@anciencodeevaluation and codemat=@anciencodemat and codeclasse=@anciencodeclasse and codeseq=@anciencodeseq";

                // utilisation de l'objet voiture passé en paramètre
                cmd.Parameters.AddWithValue("@nouveaucodeevaluation", newEntity.codeEvaluation);
                cmd.Parameters.AddWithValue("@nouveaucodemat", newEntity.codeMat);
                cmd.Parameters.AddWithValue("@nouveaucodeclasse", newEntity.codeClasse);
                cmd.Parameters.AddWithValue("@nouveaucodeseq", newEntity.codeSequence);
                cmd.Parameters.AddWithValue("@poids", newEntity.poids);
                cmd.Parameters.AddWithValue("@annee", newEntity.annee);

                cmd.Parameters.AddWithValue("@anciencodeevaluation", entity.codeEvaluation);
                cmd.Parameters.AddWithValue("@anciencodemat", entity.codeMat);
                cmd.Parameters.AddWithValue("@anciencodeclasse", entity.codeClasse);
                cmd.Parameters.AddWithValue("@anciencodeseq", entity.codeSequence);

                // Exécution de la commande SQL
                cmd.Transaction = tx;
                cmd.ExecuteNonQuery();
                tx.Commit();

                return true;
            }
            catch (Exception ex)
            {
                tx.Rollback();
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        //----------------fin modifier ------------------------------------//

        //----------------debut lister --------------------------------------------
        public override List<EvaluerBE> listerTous()
        {
            List<EvaluerBE> list = new List<EvaluerBE>();
            String codeevaluation;
            String codemat;
            String codeclasse;
            int poids;
            int annee;
            String codeSequence;
            EvaluerBE e;

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT * FROM Evaluer";

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet à retourner
                    while (dataReader.Read())
                    {
                        codeclasse = Convert.ToString(dataReader["codeclasse"]);
                        codemat = Convert.ToString(dataReader["codemat"]);
                        codeevaluation = Convert.ToString(dataReader["codeevaluation"]);
                        poids = Convert.ToInt32(dataReader["poids"]);
                        annee = Convert.ToInt32(dataReader["annee"]);
                        codeSequence = Convert.ToString(dataReader["codeseq"]);
                        e = new EvaluerBE(codeevaluation, codemat, codeclasse, poids, annee, codeSequence);
                        list.Add(e);
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
        //----------------fin lister --------------------------------------------

        public override List<EvaluerBE> listerSuivantCritere(string critere)
        {
            List<EvaluerBE> list = new List<EvaluerBE>();
            String codeevaluation;
            String codemat;
            String codeclasse;
            int poids;
            int annee;
            String codeSequence;
            EvaluerBE e;

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT * FROM Evaluer WHERE " + critere;

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet à retourner
                    while (dataReader.Read())
                    {
                        codeclasse = Convert.ToString(dataReader["codeclasse"]);
                        codemat = Convert.ToString(dataReader["codemat"]);
                        codeevaluation = Convert.ToString(dataReader["codeevaluation"]);
                        poids = Convert.ToInt32(dataReader["poids"]);
                        annee = Convert.ToInt32(dataReader["annee"]);
                        codeSequence = Convert.ToString(dataReader["codeseq"]);
                        e = new EvaluerBE(codeevaluation, codemat, codeclasse, poids, annee, codeSequence);
                        list.Add(e);
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
                cmd.CommandText = "SELECT * FROM Evaluer ";

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

        //-----------debut compter ----------------------------------------------
        public int compter()
        {
            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT COUNT(*) FROM Evaluer";

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
        //-----------------------------fin compter --------------------------------------

        public Boolean supprimerSuivantCritere(String critere)
        {
            MySqlTransaction tx = con.connexion.BeginTransaction();
            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "DELETE FROM Evaluer WHERE " + critere;

                // Exécution de la commande SQL
                cmd.Transaction = tx;
                cmd.ExecuteNonQuery();
                tx.Commit();

                return true;
            }
            catch (Exception ex)
            {
                tx.Rollback();
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public List<string> obtenirListerEvaluation(string classe, string matiere, string sequence, int annee)
        {
            List<string> list = new List<string>();
            
            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT codeevaluation FROM Evaluer WHERE codemat = "+"'"+matiere+"' and codeclasse = "+"'"+classe+"' and codeseq = "+"'"+sequence+"' and annee ="+"'"+annee+"'";

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet à retourner
                    while (dataReader.Read())
                    {
                        list.Add(Convert.ToString(dataReader["codeevaluation"]));
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

        //---lister les évaluations avec jointures-----------------------------------------

        public List<string[]> obtenirListerEvaluation(string classe, int annee, string sequence)
        {
            List<string[]> list = new List<string[]>();
            string[] ligne;
            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT distinct e.CODEEVALUATION, e.POIDS"
                                + " FROM evaluer e, classe c, programmer p"
                                + " WHERE p.codeclasse=c.codeclasse"
                                + " AND p.codeclasse=e.codeclasse"
                                + " AND p.codemat=e.codemat"
                                + " AND e.codeclasse= " + "'" + classe + "'"
                                + " AND p.annee= " + "'" + annee + "'"
                                + " AND e.codeseq= " + "'" + sequence + "'"
                                + " ORDER BY e.poids";
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet à retourner
                    while (dataReader.Read())
                    {
                        ligne = new string[2];
                        ligne[0] = dataReader["codeevaluation"].ToString();
                        ligne[1] = dataReader["poids"].ToString() + "%";
                        list.Add(ligne);
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
