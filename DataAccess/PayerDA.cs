using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MySql.Data.MySqlClient;

using Ecole.BusinessEntity;
using Ecole.ClasseConception;

namespace Ecole.DataAccess
{
    public class PayerDA : DA<PayerBE>
    {
        private Connexion con = Connexion.getConnexion();

        //**************************** création d'objet, parametre obj, retourne booléen
        public override Boolean ajouter(PayerBE obj) {
            MySqlTransaction tx = con.connexion.BeginTransaction();
            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "INSERT INTO payer (matricule,login,codeprestation,codetranche,montant,datepaiement,annee,observation,remise) VALUES (@matricule, @login, @codeprestation, @codetranche, @montant, @datepaiement, @annee, @observation,@remise)";

                // utilisation de l'objet PayerBE passé en paramètre
                cmd.Parameters.AddWithValue("@matricule", obj.matricule);
                cmd.Parameters.AddWithValue("@login", obj.login);
                cmd.Parameters.AddWithValue("@codeprestation", obj.codePrestation);
                cmd.Parameters.AddWithValue("@codetranche", obj.codeTranche);
                cmd.Parameters.AddWithValue("@montant", obj.montant);
                cmd.Parameters.AddWithValue("@datepaiement", obj.datePaiement);
                cmd.Parameters.AddWithValue("@annee", obj.annee);
                cmd.Parameters.AddWithValue("@observation", obj.observation);
                cmd.Parameters.AddWithValue("@remise", obj.remise);

                // Exécution de la commande SQL
                cmd.Transaction = tx;
                cmd.ExecuteNonQuery();

                // Création d'une commande SQL 
                cmd = con.connexion.CreateCommand();
                cmd.CommandText = "INSERT INTO payer_historique (matricule,login,codeprestation,codetranche,montant,datepaiement,annee,observation,remise) VALUES (@matricule, @login, @codeprestation, @codetranche, @montant, @datepaiement, @annee, @observation,@remise)";

                // utilisation de l'objet PayerBE passé en paramètre
                cmd.Parameters.AddWithValue("@matricule", obj.matricule);
                cmd.Parameters.AddWithValue("@login", obj.login);
                cmd.Parameters.AddWithValue("@codeprestation", obj.codePrestation);
                cmd.Parameters.AddWithValue("@codetranche", obj.codeTranche);
                cmd.Parameters.AddWithValue("@montant", obj.montant);
                cmd.Parameters.AddWithValue("@datepaiement", obj.datePaiement);
                cmd.Parameters.AddWithValue("@annee", obj.annee);
                cmd.Parameters.AddWithValue("@observation", obj.observation);
                cmd.Parameters.AddWithValue("@remise", obj.remise);

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
        //**************************** FIN création d'objet, parametre obj, retourne booléen
        
        //*********************************** suppression d'objet, parametre obj, retourne booléen
        public override Boolean supprimer(PayerBE obj) {
            MySqlTransaction tx = con.connexion.BeginTransaction();
            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "DELETE FROM payer WHERE matricule=@matricule AND codeprestation=@codeprestation AND codetranche=@codetranche";
                cmd.Parameters.AddWithValue("@matricule", obj.matricule);
                cmd.Parameters.AddWithValue("@codeprestation", obj.codePrestation);
                cmd.Parameters.AddWithValue("@codetranche", obj.codeTranche);
                cmd.Transaction = tx;
                cmd.ExecuteNonQuery();

                cmd = con.connexion.CreateCommand();
                cmd.CommandText = "DELETE FROM payer_historique WHERE matricule=@matricule AND codeprestation=@codeprestation AND codetranche=@codetranche";
                cmd.Parameters.AddWithValue("@matricule", obj.matricule);
                cmd.Parameters.AddWithValue("@codeprestation", obj.codePrestation);
                cmd.Parameters.AddWithValue("@codetranche", obj.codeTranche);
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
        //*********************************** FIN suppression d'objet, parametre obj, retourne booléen
        
        //*************************** mise à jour d'objet, parametre obj, retourne booléen
        public Boolean modifier_old(PayerBE obj, PayerBE newobj) {
            MySqlTransaction tx = con.connexion.BeginTransaction();
            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "UPDATE payer SET montant=@montant, datepaiement=@datepaiement, annee=@annee, observation=@observation, remise=@remise, matricule=@matricule, codeprestation=@codeprestation, codetranche=@codetranche, annee=@annee "
                        + " WHERE matricule=@mat AND codeprestation=@codep AND codetranche=@codet and annee=@an";

                // utilisation de l'objet GroupePrivilegeBE passé en paramètre
                cmd.Parameters.AddWithValue("@matricule", newobj.matricule);
                cmd.Parameters.AddWithValue("@login", newobj.login);
                cmd.Parameters.AddWithValue("@codeprestation", newobj.codePrestation);
                cmd.Parameters.AddWithValue("@codetranche", newobj.codeTranche);
                cmd.Parameters.AddWithValue("@montant", newobj.montant);
                cmd.Parameters.AddWithValue("@datepaiement", newobj.datePaiement);
                cmd.Parameters.AddWithValue("@annee", newobj.annee);
                cmd.Parameters.AddWithValue("@observation", newobj.observation);
                cmd.Parameters.AddWithValue("@remise", newobj.remise);

                cmd.Parameters.AddWithValue("@mat", obj.matricule);
                cmd.Parameters.AddWithValue("@codep", obj.codePrestation);
                cmd.Parameters.AddWithValue("@codet", obj.codeTranche);
                cmd.Parameters.AddWithValue("@an", obj.annee);

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
        //*************************** FIN mise à jour d'objet, parametre obj, retourne booléen

        //**************************** modification d'objet, parametre obj, retourne booléen
        public override Boolean modifier(PayerBE obj, PayerBE net_payer)
        {
            MySqlTransaction tx = con.connexion.BeginTransaction();
            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "REPLACE INTO payer (matricule,login,codeprestation,codetranche,montant,datepaiement,annee,observation,remise) VALUES (@matricule, @login, @codeprestation, @codetranche, @montant, @datepaiement, @annee, @observation,@remise)";

                // utilisation de l'objet PayerBE passé en paramètre
                cmd.Parameters.AddWithValue("@matricule", obj.matricule);
                cmd.Parameters.AddWithValue("@login", obj.login);
                cmd.Parameters.AddWithValue("@codeprestation", obj.codePrestation);
                cmd.Parameters.AddWithValue("@codetranche", obj.codeTranche);
                cmd.Parameters.AddWithValue("@montant", obj.montant);
                cmd.Parameters.AddWithValue("@datepaiement", obj.datePaiement);
                cmd.Parameters.AddWithValue("@annee", obj.annee);
                cmd.Parameters.AddWithValue("@observation", obj.observation);
                cmd.Parameters.AddWithValue("@remise", obj.remise);

                // Exécution de la commande SQL
                cmd.Transaction = tx;
                cmd.ExecuteNonQuery();

                //// Création d'une commande SQL 
                cmd = con.connexion.CreateCommand();
                cmd.CommandText = "INSERT INTO payer_historique (matricule,login,codeprestation,codetranche,montant,datepaiement,annee,observation,remise) VALUES (@matricule, @login, @codeprestation, @codetranche, @montant, @datepaiement, @annee, @observation,@remise)";

                // utilisation de l'objet PayerBE passé en paramètre
                cmd.Parameters.AddWithValue("@matricule", net_payer.matricule);
                cmd.Parameters.AddWithValue("@login", net_payer.login);
                cmd.Parameters.AddWithValue("@codeprestation", net_payer.codePrestation);
                cmd.Parameters.AddWithValue("@codetranche", net_payer.codeTranche);
                cmd.Parameters.AddWithValue("@montant", net_payer.montant);
                cmd.Parameters.AddWithValue("@datepaiement", net_payer.datePaiement);
                cmd.Parameters.AddWithValue("@annee", net_payer.annee);
                cmd.Parameters.AddWithValue("@observation", net_payer.observation);
                cmd.Parameters.AddWithValue("@remise", net_payer.remise);

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
        //**************************** FIN création d'objet, parametre obj, retourne booléen

        //*********************************** rechercher d'un objet à partir de ses codes, parametre (code1, code2), retourne l'objet
        // param : - code1 : le matricule de l'élève
                    //- code 2 : le login
                    //- code 3 : le code de la prestation
                    //- code 4 : le code de la tranche 
        public override PayerBE rechercher(PayerBE payer) {
            string matricule;
            string login;
            string codePrestation;
            string codeTranche;
            double montant;
            DateTime datePaiement;
            int annee;
            string observation;

            PayerBE paye;
            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT * FROM payer WHERE matricule=@matricule AND codeprestation=@codeprestation AND codetranche=@codetranche and annee=@annee";
                cmd.Parameters.AddWithValue("@matricule", payer.matricule);
                cmd.Parameters.AddWithValue("@codeprestation", payer.codePrestation);
                cmd.Parameters.AddWithValue("@codetranche", payer.codeTranche);
                cmd.Parameters.AddWithValue("@annee", payer.annee);

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer la voiture à retourner
                    if (dataReader.Read())
                    {
                        matricule = Convert.ToString(dataReader["matricule"]);
                        login = Convert.ToString(dataReader["login"]);
                        codePrestation = Convert.ToString(dataReader["codeprestation"]);
                        codeTranche = Convert.ToString(dataReader["codetranche"]);
                        montant = Convert.ToDouble(dataReader["montant"]);
                        datePaiement = Convert.ToDateTime(dataReader["datepaiement"]);
                        annee = Convert.ToInt16(dataReader["annee"]);
                        observation = Convert.ToString(dataReader["observation"]);
                        paye = new PayerBE(matricule, login, codePrestation, codeTranche, montant, datePaiement, annee, observation,Convert.ToDecimal(dataReader["remise"]));
                        dataReader.Close();
                        return paye;
                        // this.con.fermer();
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

        public int rechercherNumeroPayer(PayerBE payer)
        {
            try
            {
                MySqlCommand cmd = con.connexion.CreateCommand();

                cmd.CommandText = "SELECT * FROM payer WHERE matricule=@matricule AND codeprestation=@codeprestation AND codetranche=@codetranche and annee=@annee";
                cmd.Parameters.AddWithValue("@matricule", payer.matricule);
                cmd.Parameters.AddWithValue("@codeprestation", payer.codePrestation);
                cmd.Parameters.AddWithValue("@codetranche", payer.codeTranche);
                cmd.Parameters.AddWithValue("@annee", payer.annee);

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    if (dataReader.Read())
                    {
                        return Convert.ToInt32(dataReader["numero"]);
                    }
                    else
                        return -1;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -1;
            }
        }


        //*********************************** FIN rechercher d'un objet à partir de ses codes, parametre (code1, code2), retourne l'objet
        
        //*********************************** retourner la liste de tout les objets
        public override List<PayerBE> listerTous()
        {
            try
            {
                List<PayerBE> listpayerBE = new List<PayerBE>();

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT * FROM payer";

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //Read the data and store them in the list
                    while (dataReader.Read())
                    {
                        PayerBE payerBE = new PayerBE(Convert.ToString(dataReader["matricule"]), Convert.ToString(dataReader["login"]),
                            Convert.ToString(dataReader["codeprestation"]), Convert.ToString(dataReader["codetranche"]), 
                            Convert.ToDouble(dataReader["montant"]), Convert.ToDateTime(dataReader["datepaiement"]),
                            Convert.ToInt16(dataReader["annee"]), Convert.ToString(dataReader["observation"]), Convert.ToDecimal(dataReader["remise"]));
                        listpayerBE.Add(payerBE);
                    }

                    //close Data Reader
                    dataReader.Close();

                    //return list to be displayed
                    if (listpayerBE.Count != 0)
                        return listpayerBE;
                    else return null;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        //*********************************** FIN retourner la liste de tout les objets
         
        //****************************** retourner la liste des objets qui correspondent à un certain critère
        public override List<PayerBE> listerSuivantCritere(String critere)
        {
            List<PayerBE> listobjBE = new List<PayerBE>();
            try
            {
                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT * FROM payer WHERE " + critere;

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //Read the data and store them in the list
                    while (dataReader.Read())
                    {
                        PayerBE objBE = new PayerBE(Convert.ToString(dataReader["matricule"]), Convert.ToString(dataReader["login"]),
                        Convert.ToString(dataReader["codeprestation"]), Convert.ToString(dataReader["codetranche"]),
                        Convert.ToDouble(dataReader["montant"]), Convert.ToDateTime(dataReader["datepaiement"]),
                        Convert.ToInt16(dataReader["annee"]), Convert.ToString(dataReader["observation"]), Convert.ToDecimal(dataReader["remise"])); 
                        listobjBE.Add(objBE);
                    }

                    //close Data Reader
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

        public List<PayerBE> listerSuivantCritere_historique(String critere)
        {
            List<PayerBE> listobjBE = new List<PayerBE>();
            try
            {
                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT * FROM payer_historique WHERE " + critere;

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //Read the data and store them in the list
                    while (dataReader.Read())
                    {
                        PayerBE objBE = new PayerBE(Convert.ToString(dataReader["matricule"]), Convert.ToString(dataReader["login"]),
                        Convert.ToString(dataReader["codeprestation"]), Convert.ToString(dataReader["codetranche"]),
                        Convert.ToDouble(dataReader["montant"]), Convert.ToDateTime(dataReader["datepaiement"]),
                        Convert.ToInt16(dataReader["annee"]), Convert.ToString(dataReader["observation"]), Convert.ToDecimal(dataReader["remise"]));
                        listobjBE.Add(objBE);
                    }

                    //close Data Reader
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

        public List<LigneVersement> listerSuivantCriterePayers_versement(String critere)
        {
            List<LigneVersement> listobjBE = new List<LigneVersement>();
            try
            {
                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT * FROM payer_versement WHERE " + critere;

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //Read the data and store them in the list
                    while (dataReader.Read())
                    {
                        LigneVersement objBE = new LigneVersement(Convert.ToString(dataReader["matricule"]), Convert.ToString(dataReader["libelle"]),
                        Convert.ToDouble(dataReader["montant"]), Convert.ToDateTime(dataReader["datepaiement"]), Convert.ToInt16(dataReader["annee"]));
                        listobjBE.Add(objBE);
                    }

                    //close Data Reader
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

        //****************************** FIN retourner la liste des objets qui correspondent à un certain critère


        public override List<string> listerValeursColonne(string colonne)
        {
            List<String> list = new List<String>();

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT * FROM payer";

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
                cmd.CommandText = "SELECT COUNT(*) FROM payer";

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

        internal List<ClasseConception.LigneBilan> obtenirBilanFinancier(int annee)
        {
            List<ClasseConception.LigneBilan> liste = new List<ClasseConception.LigneBilan>();
            try
            {
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT ps.*,tb.* FROM ((SELECT t1.codeprestation, t1.A_Payer A_Payer , if(isnull(t2.paye),0,t2.paye) Paye, if(isnull(t2.remise),0,t2.remise) Remise, (t1.A_payer-if(isnull(t2.paye),0,t2.paye)-if(isnull(t2.remise),0,t2.remise)) Reste_A_Payer "
                                 + " FROM"
                                 + "  ((SELECT a.matricule, m.codecateleve, m.codeprestation, sum(montant) A_payer, a.annee"
                                 + "  FROM montanttranche m, appartenir a "
                                 + "  WHERE a.codecateleve=m.codecateleve "
                                 + "   AND a.annee=m.annee "
                                 + "   AND a.annee='" + annee + "'"
                                 + "   GROUP BY codeprestation "
                                 + "   order by codeprestation) "
                                 + "   UNION "
                                 + "   (SELECT i.matricule,a.codecateleve,(SELECT CODEPRESTATION From PRESTATION where NOMPRESTATION LIKE " + "'%inscription%" + "'),sum(t.fraisinscription) A_payer,i.annee "
                                 + "   FROM `typeclasse` t, inscrire i, classe c, appartenir a, PRESTATION "
                                 + "   WHERE i.codeclasse=c.codeclasse "
                                 + "   AND c.codetypeclasse=t.codetypeclasse "
                                 + "   AND i.matricule=a.matricule "
                                 + "   and i.annee=a.annee "
                                 + "  and i.annee='" + annee + "' GROUP BY CODEPRESTATION) "
                                 + "   order by matricule) t1 LEFT JOIN (SELECT matricule,codeprestation,sum(montant) paye, sum(remise) remise, annee "
                                 + "   FROM payer p WHERE annee='" + annee + "' GROUP BY codeprestation) t2  "
                                 + "   ON (t1.codeprestation=t2.codeprestation AND t1.annee=t2.annee) "
                                 + "  WHERE t1.matricule in (select matricule from inscrire where annee='" + annee + "') "
                                 + "   order by codeprestation) "
                                 + "   UNION "
                                 + "   (SELECT " + "' TOTAUX " + "', SUM(tab.A_Payer), SUM(tab.Paye), SUM(tab.Remise), SUM(tab.Reste_A_Payer) "
                                 + "    FROM (SELECT t1.codeprestation, t1.A_Payer A_Payer, if(isnull(t2.paye),0,t2.paye) Paye, if(isnull(t2.remise),0,t2.remise) Remise, (t1.A_payer-if(isnull(t2.paye),0,t2.paye)-if(isnull(t2.remise),0,t2.remise)) Reste_A_Payer "
                                 + "   FROM "
                                 + "   ((SELECT a.matricule, m.codecateleve, m.codeprestation, sum(montant) A_payer, a.annee "
                                 + "   FROM montanttranche m, appartenir a "
                                 + "   WHERE a.codecateleve=m.codecateleve "
                                 + "   AND a.annee=m.annee "
                                 + "   AND a.annee='" + annee + "' "
                                 + "   GROUP BY codeprestation "
                                 + "   order by codeprestation) "
                                 + "   UNION "
                                 + "   (SELECT i.matricule,a.codecateleve,(SELECT CODEPRESTATION From PRESTATION where NOMPRESTATION LIKE " + "'%inscription%" + "'),sum(t.fraisinscription) A_payer,i.annee "
                                 + "   FROM `typeclasse` t, inscrire i, classe c, appartenir a, PRESTATION "
                                 + "   WHERE i.codeclasse=c.codeclasse "
                                 + "   AND c.codetypeclasse=t.codetypeclasse "
                                 + "   AND i.matricule=a.matricule "
                                 + "   and i.annee=a.annee "
                                 + "   and i.annee='" + annee + "' GROUP BY CODEPRESTATION) "
                                 + "   order by matricule) t1 LEFT JOIN (SELECT matricule,codeprestation,sum(montant) paye, sum(remise) remise, annee "
                                 + "   FROM payer p WHERE annee='" + annee + "' GROUP BY codeprestation) t2 "
                                 + "   ON (t1.codeprestation=t2.codeprestation AND t1.annee=t2.annee) "
                                 + "   WHERE t1.matricule in (select matricule from inscrire where annee='" + annee + "') "
                                 + "   order by codeprestation) tab)) tb LEFT JOIN prestation ps ON tb.CODEPRESTATION=ps.CODEPRESTATION";
                int i = 1;

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        ClasseConception.LigneBilan bilan = new ClasseConception.LigneBilan(i++, Convert.ToString(dataReader["NOMPRESTATION"]), Convert.ToDouble(dataReader["A_Payer"]),
                                                Convert.ToDouble(dataReader["Paye"]), Convert.ToDouble(dataReader["remise"]), Convert.ToDouble(dataReader["Reste_A_Payer"]));
                        liste.Add(bilan);
                    }

                    dataReader.Close();
                    for (i = 0; i < 1; i++)
                        liste.RemoveAt(liste.Count - 1);

                    return liste;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        internal bool enregistrer_versement(string matricule, string libelle, double montant, DateTime date, int annee)
        {
            MySqlTransaction tx = con.connexion.BeginTransaction();
            try
            {
                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "INSERT INTO payer_versement (matricule, libelle, montant, datepaiement,annee) VALUES (@matricule, @libelle, @montant, @datepaiement, @annee)";

                // utilisation de l'objet PayerBE passé en paramètre
                cmd.Parameters.AddWithValue("@matricule", matricule);
                cmd.Parameters.AddWithValue("@libelle", libelle);
                cmd.Parameters.AddWithValue("@montant", montant);
                cmd.Parameters.AddWithValue("@datepaiement", date);
                cmd.Parameters.AddWithValue("@annee", annee);

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
    }
}
