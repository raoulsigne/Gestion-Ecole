using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Ecole.BusinessEntity;
using MySql.Data.MySqlClient;

namespace Ecole.DataAccess
{
    public class GroupePrivilegeDA : DA<GroupePrivilegeBE>
    {
        private Connexion con = Connexion.getConnexion();



        //------------Moi---liste les privilèges d'un groupe d'utilisateurs à partir du role------

        public List<String> listerPrivilegeDunRole(String role)
        {
            List<String> listPrivilege = new List<string>();
            String privilege;

            try
            {
                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT p.nomprivilege "
                                 + "FROM groupeprivilege gp, privilege p "
                                 + "WHERE gp.codeprivilege=p.codeprivilege "
                                 + "AND gp.role='" + role + "'";

                //cmd.CommandText = "SELECT codeprivilege "
                //                 + "FROM groupeprivilege "
                //                 + "WHERE role='" + role + "'";

                // cmd.Parameters.AddWithValue("@role", role);

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer la liste à retourner
                    while (dataReader.Read())
                    {
                        privilege = Convert.ToString(dataReader["nomprivilege"]);
                        listPrivilege.Add(privilege);
                    }

                    dataReader.Close();
                    return listPrivilege;

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }


        //----------------Ajout de GroupePrivilege ------------------------------
        public override Boolean ajouter(GroupePrivilegeBE gp)
        {

            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "INSERT INTO groupeprivilege (codeprivilege,role,annee) VALUES (@codePrivilege, @role, @annee)";

                // utilisation de l'objet GroupePrivilegeBE passé en paramètre
                cmd.Parameters.AddWithValue("@codePrivilege", gp.codePrivilege);
                cmd.Parameters.AddWithValue("@role", gp.role);
                cmd.Parameters.AddWithValue("@annee", gp.annee);

                // Exécution de la commande SQL
                cmd.ExecuteNonQuery();
                return true;
                // Fermeture de la connexion
                //  this.con.fermer();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        //--------------------------Fin ajout-----------------------------

        //-----------------------supprimer tous les privileges d'un role----------------------

        public Boolean supprimerTousDunRole(String role)
        {
            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "DELETE FROM groupeprivilege WHERE role=@role";

                // utilisation de l'objet GroupePrivilegeBE passé en paramètre
                cmd.Parameters.AddWithValue("@role", role);

                // Exécution de la commande SQL
                cmd.ExecuteNonQuery();
                return true;
                // Fermeture de la connexion
                //  this.con.fermer();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }


        //------------------------fin supprimer--------------------------------------

        //----------------------------------------------------------------------
        //-------------------Fin MOI--------------------------------------------

        //---------------Rechercher GroupePrivilege---------------------------------

        public override GroupePrivilegeBE rechercher(GroupePrivilegeBE groupe)
        {
            string code;
            string role;
            int annee;
            GroupePrivilegeBE gp;
            try
            {
                // ouverture de la connexion
                // this.con.ouvrir();

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT * FROM groupeprivilege WHERE codeprivilege=@codeprivilege AND role=@role";
                cmd.Parameters.AddWithValue("@codeprivilege", groupe.codePrivilege);
                cmd.Parameters.AddWithValue("@role", groupe.role);

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer la voiture à retourner
                    if (dataReader.Read())
                    {
                        dataReader.Read();
                        code = Convert.ToString(dataReader["codeprivilege"]);
                        role = Convert.ToString(dataReader["role"]);
                        annee = Convert.ToInt16(dataReader["annee"]);
                        gp = new GroupePrivilegeBE(code, role, annee);
                        dataReader.Close();
                        // this.con.fermer();
                        return gp;
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

  
        public override Boolean supprimer(GroupePrivilegeBE gp)
        {
            MySqlTransaction tx = con.connexion.BeginTransaction();
            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "DELETE FROM groupeprivilege WHERE codeprivilege=@codeprivilege AND role=@role";

                // utilisation de l'objet GroupePrivilegeBE passé en paramètre
                cmd.Parameters.AddWithValue("@codePrivilege", gp.codePrivilege);
                cmd.Parameters.AddWithValue("@role", gp.role);

                // Exécution de la commande SQL
                cmd.Transaction = tx;
                cmd.ExecuteNonQuery();
                tx.Commit();

                return true;
                // Fermeture de la connexion
                //  this.con.fermer();
            }
            catch (Exception ex)
            {
                tx.Rollback();
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        // mise à jour d'objet, parametre obj, retourne booléen
        public override Boolean modifier(GroupePrivilegeBE gp, GroupePrivilegeBE newgp)
        {
            MySqlTransaction tx = con.connexion.BeginTransaction();
            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "UPDATE groupeprivilege SET annee=@annee,codeprivilege=@codeprivilege,role=@role WHERE codeprivilege=@code AND role=@rol";

                // utilisation de l'objet GroupePrivilegeBE passé en paramètre
                cmd.Parameters.AddWithValue("@codePrivilege", newgp.codePrivilege);
                cmd.Parameters.AddWithValue("@role", newgp.role);
                cmd.Parameters.AddWithValue("@annee", newgp.annee);
                cmd.Parameters.AddWithValue("@code", gp.codePrivilege);
                cmd.Parameters.AddWithValue("@rol", gp.role);
               
                // Exécution de la commande SQL
                cmd.Transaction = tx;
                cmd.ExecuteNonQuery();
                tx.Commit();

                return true;
                // Fermeture de la connexion
                //  this.con.fermer();
            }
            catch (Exception ex)
            {
                tx.Rollback();
                Console.WriteLine(ex.Message);
                return false;
            }
        }


        // retourner la liste de tout les objets
        public override List<GroupePrivilegeBE> listerTous() {
            try
            {
                List<GroupePrivilegeBE> listGpBE = new List<GroupePrivilegeBE>();

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT * FROM groupeprivilege";
                
                 // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //Read the data and store them in the list
                    while (dataReader.Read())
                    {
                        GroupePrivilegeBE gpBE = new GroupePrivilegeBE(Convert.ToString(dataReader["codeprivilege"]), Convert.ToString(dataReader["role"]), Convert.ToInt16(dataReader["annee"]));
                        listGpBE.Add(gpBE);
                    }

                    //close Data Reader
                    dataReader.Close();

                    //return list to be displayed
                    if (listGpBE.Count != 0)
                        return listGpBE;
                    else return null;
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

        }


        public override List<GroupePrivilegeBE> listerSuivantCritere(string critere)
        {
            try
            {
                List<GroupePrivilegeBE> listGpBE = new List<GroupePrivilegeBE>();

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT * FROM groupeprivilege";

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //Read the data and store them in the list
                    while (dataReader.Read())
                    {
                        GroupePrivilegeBE gpBE = new GroupePrivilegeBE(Convert.ToString(dataReader["codeprivilege"]), Convert.ToString(dataReader["role"]), Convert.ToInt16(dataReader["annee"]));
                        listGpBE.Add(gpBE);
                    }

                    //close Data Reader
                    dataReader.Close();

                    //return list to be displayed
                    if (listGpBE.Count != 0)
                        return listGpBE;
                    else return null;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        // retourner la liste des objets qui correspondent à un certain critère
        public override List<String> listerValeursColonne(String colonne)
        {
            List<String> list = new List<String>();

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT * FROM groupeprivilege";

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
                cmd.CommandText = "SELECT COUNT(*) FROM groupeprivilege";

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
    }
}
