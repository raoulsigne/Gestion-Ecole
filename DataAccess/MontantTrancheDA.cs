using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MySql.Data.MySqlClient;

using Ecole.BusinessEntity;

namespace Ecole.DataAccess
{
    public class MontantTrancheDA : DA<MontantTrancheBE>
    {
        private Connexion con = Connexion.getConnexion();

        //************************************ création d'objet, parametre obj, retourne booléen
        public override Boolean ajouter(MontantTrancheBE obj) {
            MySqlTransaction tx = con.connexion.BeginTransaction();
            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "INSERT INTO montanttranche (codetranche,codecateleve,codeprestation,montant,annee,delai) VALUES (@codeTranche, @codeCatEleve, @codePrestation, @montant, @annee, @delai)";

                // utilisation de l'objet GroupePrivilegeBE passé en paramètre
                cmd.Parameters.AddWithValue("@codeTranche", obj.codeTranche);
                cmd.Parameters.AddWithValue("@codeCatEleve", obj.codeCatEleve);
                cmd.Parameters.AddWithValue("@codePrestation", obj.codePrestation);
                cmd.Parameters.AddWithValue("@montant", obj.montant);
                cmd.Parameters.AddWithValue("@annee", obj.annee);
                cmd.Parameters.AddWithValue("@delai", obj.delai);

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
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        //************************************ FIN création d'objet, parametre obj, retourne booléen
        
        //************************************ suppression d'objet, parametre obj, retourne booléen
        public override Boolean supprimer(MontantTrancheBE obj) {
            MySqlTransaction tx = con.connexion.BeginTransaction();
            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "DELETE FROM montanttranche WHERE codetranche=@codeTranche AND codecateleve=@codeCatEleve AND codeprestation=@codePrestation";

                // utilisation de l'objet GroupePrivilegeBE passé en paramètre
                cmd.Parameters.AddWithValue("@codeTranche", obj.codeTranche);
                cmd.Parameters.AddWithValue("@codeCatEleve", obj.codeCatEleve);
                cmd.Parameters.AddWithValue("@codePrestation", obj.codePrestation);

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
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        //************************************ FIN suppression d'objet, parametre obj, retourne booléen
        
        //************************************ mise à jour d'objet, parametre obj, retourne booléen
        public override Boolean modifier(MontantTrancheBE obj, MontantTrancheBE newobj) {
            MySqlTransaction tx = con.connexion.BeginTransaction();
            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "UPDATE montanttranche SET montant=@montant, annee=@annee, delai=@delai, codetranche=@codeTranche, codecateleve=@codeCatEleve, codeprestation=@codePrestation "
                    +" WHERE codetranche=@codeT AND codecateleve=@codeCat AND codeprestation=@codeP";

                // utilisation de l'objet GroupePrivilegeBE passé en paramètre
                cmd.Parameters.AddWithValue("@montant", newobj.montant);
                cmd.Parameters.AddWithValue("@annee", newobj.annee);
                cmd.Parameters.AddWithValue("@delai", newobj.delai);
                cmd.Parameters.AddWithValue("@codeTranche", newobj.codeTranche);
                cmd.Parameters.AddWithValue("@codeCatEleve", newobj.codeCatEleve);
                cmd.Parameters.AddWithValue("@codePrestation", newobj.codePrestation);

                cmd.Parameters.AddWithValue("@codeT", obj.codeTranche);
                cmd.Parameters.AddWithValue("@codeCat", obj.codeCatEleve);
                cmd.Parameters.AddWithValue("@codeP", obj.codePrestation);
                
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
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        //************************************ FIN mise à jour d'objet, parametre obj, retourne booléen
        
       
        //******************************** rechercher d'un objet à partir de son code, parametre obj, retourne l'objet
       public override MontantTrancheBE rechercher(MontantTrancheBE montantTranche) {
            string codeTranche;
            string codeCatEleve;
            string codePrestation;
            double montant;
            int annee;
            DateTime delai;
            MontantTrancheBE m;
            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT * FROM montanttranche WHERE codetranche=@codeTranche AND codecateleve=@codeCatEleve AND codeprestation=@codePrestation and annee=@annee";
                cmd.Parameters.AddWithValue("@codeTranche", montantTranche.codeTranche);
                cmd.Parameters.AddWithValue("@codeCatEleve", montantTranche.codeCatEleve);
                cmd.Parameters.AddWithValue("@codePrestation", montantTranche.codePrestation);
                cmd.Parameters.AddWithValue("@annee", montantTranche.annee);

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer la voiture à retourner
                    if (dataReader.Read())
                    {
                        codeTranche = Convert.ToString(dataReader["codetranche"]);
                        codeCatEleve = Convert.ToString(dataReader["codecateleve"]);
                        codePrestation = Convert.ToString(dataReader["codeprestation"]);
                        montant = Convert.ToDouble(dataReader["montant"]);
                        annee = Convert.ToInt16(dataReader["annee"]);
                        delai = Convert.ToDateTime(dataReader["delai"]);
                        m = new MontantTrancheBE(codeTranche, codeCatEleve, codePrestation, montant, annee, delai.Date);
                        dataReader.Close();
                        return m;
                    }
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
        //******************************** FIN rechercher d'un objet à partir de son code, parametre obj, retourne l'objet
        
        //******************************* retourner la liste de tout les objets
        public override List<MontantTrancheBE> listerTous() {
            try
            {
                List<MontantTrancheBE> listMontBE = new List<MontantTrancheBE>();

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT * FROM montanttranche order by codecateleve, codeprestation, codetranche";

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //Read the data and store them in the list
                    while (dataReader.Read())
                    {
                        MontantTrancheBE montBE = new MontantTrancheBE(Convert.ToString(dataReader["codetranche"]), Convert.ToString(dataReader["codecateleve"]), Convert.ToString(dataReader["codeprestation"]), Convert.ToDouble(dataReader["montant"]), Convert.ToInt16(dataReader["annee"]), Convert.ToDateTime(dataReader["delai"]).Date);
                        listMontBE.Add(montBE);
                    }

                    //close Data Reader
                    dataReader.Close();

                    //return list to be displayed
                    if (listMontBE.Count != 0)
                        return listMontBE;
                    else return null;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        //******************************* FIN retourner la liste de tout les objets
       
        //******************************* retourner la liste des objets qui correspondent à un certain critère
        public override List<MontantTrancheBE> listerSuivantCritere(String critere)
        {
            List<MontantTrancheBE> listobjBE = new List<MontantTrancheBE>();
            try
            {
                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT * FROM montanttranche WHERE " + critere + "order by codecateleve, codeprestation, codetranche";
                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //Read the data and store them in the list
                    while (dataReader.Read())
                    {
                        MontantTrancheBE objBE = new MontantTrancheBE(Convert.ToString(dataReader["codetranche"]), Convert.ToString(dataReader["codecateleve"]), Convert.ToString(dataReader["codeprestation"]), Convert.ToDouble(dataReader["montant"]), Convert.ToInt16(dataReader["annee"]), Convert.ToDateTime(dataReader["delai"]).Date);                       
                        listobjBE.Add(objBE);
                    }
                    dataReader.Close();
                }
                return listobjBE;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        //******************************* FIN retourner la liste des objets qui correspondent à un certain critère

        public override List<string> listerValeursColonne(string colonne)
        {
            List<String> list = new List<String>();

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT * FROM montanttranche";

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
                cmd.CommandText = "SELECT COUNT(*) FROM montanttranche";

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

        public List<PayerBE> tranchesNonReglees(string categorie, string matricule, string annee)
        {
            List<PayerBE> listes = new List<PayerBE>();
            try
            {
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT * FROM ((SELECT p.codetranche, '', p.codeprestation, m.montant, p.annee, m.delai, pr.priorite, p.montant as montantPaye, p.remise"
                                  + " FROM payer p, prestation pr, montanttranche m "
                                  + " WHERE p.matricule = '" + matricule + "' AND p.observation = 'Incomplet' and pr.codeprestation=p.codeprestation and p.codeprestation=m.codeprestation and m.codetranche=p.codetranche"
                                  + " and m.annee=p.annee and p.annee='" + annee + "' and m.codecateleve='" + categorie + "') "
                                  + " UNION "
                                  + " (select *, 0 as montantPaye,0 as remise "
                                  + " FROM ((SELECT mt.*, p.priorite "
                                  + " FROM montanttranche mt, prestation p "
                                  + " WHERE mt.codecateleve = '" + categorie + "'  and mt.codeprestation=p.codeprestation)) t"
                                  + " where (t.codeprestation,codetranche)"
                                  + " NOT IN"
                                  + " (SELECT codeprestation,codetranche"
                                  + " FROM payer WHERE matricule = '" + matricule + "' and annee='" + annee + "')"
                                  + " and annee ='" + annee + "')) tb"
                                  + " order by tb.codetranche , tb.priorite";
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        PayerBE payer = new PayerBE();
                        payer.annee = Convert.ToInt32(annee);
                        payer.codePrestation = Convert.ToString(dataReader["codeprestation"]);
                        payer.codeTranche = Convert.ToString(dataReader["codetranche"]);
                        payer.datePaiement = DateTime.Today;
                        payer.login = "";
                        payer.matricule = matricule;
                        payer.montant = Convert.ToDouble(dataReader["montantPaye"]);
                        payer.observation = "Incomplet";
                        payer.remise = Convert.ToDecimal(dataReader["remise"]);

                        listes.Add(payer);
                    }
                    dataReader.Close();

                    return listes;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public List<MontantTrancheBE> montantTrancheNonPayeesParEleve(string categorie, string matricule, string annee)
        {
            try
            {
                List<MontantTrancheBE> listobjBE = new List<MontantTrancheBE>();

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "select * "
                                + "FROM ((SELECT mt.*, p.priorite "
                                + "  FROM montanttranche mt, prestation p"
                                + "  WHERE mt.codecateleve = '" + categorie + "'"
                                + "  and mt.codeprestation=p.codeprestation)) t"
                               + "   where (t.codeprestation,codetranche) NOT IN"
                               + "     (SELECT codeprestation,codetranche"
                               + "       FROM payer WHERE matricule = '" + matricule + "' and annee=" + annee + ")"
                               + "   and t.codeprestation =t.codeprestation and annee =" + annee + ""
                               + " order by t.codetranche , priorite";
                                                    
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        MontantTrancheBE objBE = new MontantTrancheBE(Convert.ToString(dataReader["codetranche"]), 
                        Convert.ToString(dataReader["codecateleve"]), Convert.ToString(dataReader["codeprestation"]), 
                        Convert.ToDouble(dataReader["montant"]), Convert.ToInt16(dataReader["annee"]), Convert.ToDateTime(dataReader["delai"]).Date);                       
                        listobjBE.Add(objBE);
                    }
                    dataReader.Close();

                    return listobjBE;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        internal decimal montantPrestation(string p,string categorie, int annee)
        {
            try
            {
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT SUM(montant) FROM MontantTranche WHERE codeprestation = "+"'"+ p +"' and codecateleve = "+"'"+ categorie +"' and annee = "+"'"+annee+"'";
                decimal somme = Convert.ToDecimal(cmd.ExecuteScalar());
                return somme;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -1;
            }
        }
    }
}