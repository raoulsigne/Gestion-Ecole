using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using Ecole.BusinessEntity;

namespace Ecole.DataAccess
{
    public class DisciplineDA : DA<DisciplineBE>
    {
        private Connexion con = Connexion.getConnexion();

        //----------------ajouter -----------------//
        public override Boolean ajouter(DisciplineBE entity)
        {
            MySqlTransaction tx = con.connexion.BeginTransaction();
            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "INSERT INTO Discipline (codeSanction, nomSanction,variable,unite, priorite) VALUES (@code, @nom, @variable,@unite, @priorite)";
                cmd.Parameters.AddWithValue("@code", entity.codeSanction);
                cmd.Parameters.AddWithValue("@nom", entity.nomSanction);
                cmd.Parameters.AddWithValue("@variable", entity.variable);
                cmd.Parameters.AddWithValue("@unite", entity.unite);
                cmd.Parameters.AddWithValue("@priorite", entity.priorite);

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
        public override Boolean supprimer(DisciplineBE entity)
        {
            MySqlTransaction tx = con.connexion.BeginTransaction();
            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "DELETE FROM Discipline WHERE codesanction = @code";
                cmd.Parameters.AddWithValue("@code", entity.codeSanction);

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
        public override DisciplineBE rechercher(DisciplineBE entity)
        {
            String codeSanction;
            String nomSanction;
            String variable;
            String unite;
            int priorite;

            DisciplineBE d;

            try
            {
                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT * FROM Discipline WHERE codesanction=@code";
                cmd.Parameters.AddWithValue("@code", entity.codeSanction);

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet acheter à retourner
                    if (dataReader.Read())
                    {
                        codeSanction = Convert.ToString(dataReader["codesanction"]);
                        nomSanction = Convert.ToString(dataReader["nomsanction"]);
                        variable = Convert.ToString(dataReader["variable"]); 
                        unite = Convert.ToString(dataReader["unite"]);
                        priorite = Convert.ToInt16(dataReader["priorite"]);

                        d = new DisciplineBE(codeSanction, nomSanction, variable, unite, priorite);
                        dataReader.Close();
                        return d;
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

        //----------------chercher Acheter -----------------//
        public DisciplineBE rechercherByNom(String nomSanction)
        {
            String codeSanction;
            //String nomSanction;
            String variable;
            String unite;
            int priorite;

            DisciplineBE d;

            try
            {
                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT * FROM Discipline WHERE nomsanction LIKE '%" + nomSanction + "%'";
                //cmd.Parameters.AddWithValue("@nomsanction", "'%"+nomSanction+"%'");

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet acheter à retourner
                    if (dataReader.Read())
                    {
                        codeSanction = Convert.ToString(dataReader["codesanction"]);
                        //nomSanction = Convert.ToString(dataReader["nomsanction"]);
                        variable = Convert.ToString(dataReader["variable"]); 
                        unite = Convert.ToString(dataReader["unite"]);
                        priorite = Convert.ToInt16(dataReader["priorite"]);

                        d = new DisciplineBE(codeSanction, nomSanction, variable, unite, priorite);
                        dataReader.Close();
                        return d;
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
        public override Boolean modifier(DisciplineBE entity, DisciplineBE newEntity)
        {
            MySqlTransaction tx = con.connexion.BeginTransaction();
            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                cmd.CommandText = "UPDATE discipline SET codesanction=@codesanction, nomsanction=@nomsanction, variable=@variable, unite=@unite, priorite=@priorite WHERE codesanction=@code";

                // utilisation de l'objet voiture passé en paramètre
                cmd.Parameters.AddWithValue("@codesanction", newEntity.codeSanction);
                cmd.Parameters.AddWithValue("@nomsanction", newEntity.nomSanction);
                cmd.Parameters.AddWithValue("@variable", newEntity.variable);
                cmd.Parameters.AddWithValue("@unite", newEntity.unite);
                cmd.Parameters.AddWithValue("@priorite", newEntity.priorite);

                cmd.Parameters.AddWithValue("@code", entity.codeSanction);
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
        public override List<DisciplineBE> listerTous()
        {
            List<DisciplineBE> list = new List<DisciplineBE>();
            String codeSanction;
            String nomSanction;
            String variable;
            String unite;
            int priorite;

            DisciplineBE d;

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT * FROM Discipline ORDER BY priorite";

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet à retourner
                    while (dataReader.Read())
                    {
                        codeSanction = Convert.ToString(dataReader["codesanction"]);
                        nomSanction = Convert.ToString(dataReader["nomsanction"]);
                        variable = Convert.ToString(dataReader["variable"]); 
                        unite = Convert.ToString(dataReader["unite"]);
                        priorite = Convert.ToInt16(dataReader["priorite"]);

                        d = new DisciplineBE(codeSanction, nomSanction, variable, unite, priorite);
                        list.Add(d);
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

        public override List<DisciplineBE> listerSuivantCritere(string critere)
        {
            List<DisciplineBE> list = new List<DisciplineBE>();
            String codeSanction;
            String nomSanction;
            String variable;
            String unite;
            int priorite;

            DisciplineBE d;

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT * FROM Discipline WHERE " + critere;

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet à retourner
                    while (dataReader.Read())
                    {
                        codeSanction = Convert.ToString(dataReader["codesanction"]);
                        nomSanction = Convert.ToString(dataReader["nomsanction"]);
                        variable = Convert.ToString(dataReader["variable"]); 
                        unite = Convert.ToString(dataReader["unite"]);
                        priorite = Convert.ToInt16(dataReader["priorite"]);

                        d = new DisciplineBE(codeSanction, nomSanction, variable, unite, priorite);
                        list.Add(d);
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
                cmd.CommandText = "SELECT * FROM Discipline ";

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
                cmd.CommandText = "SELECT COUNT(*) FROM Discipline";

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
        //-----------fin compter ----------------------------------------------
    }
}
