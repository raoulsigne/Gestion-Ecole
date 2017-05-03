using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using MySql.Data.MySqlClient;

using Ecole.BusinessEntity;
using Ecole.ClasseConception;

namespace Ecole.DataAccess
{
    class MoyennesTrimestrielsDA : DA<MoyennesTrimestrielsBE>
    {
        private Connexion con = Connexion.getConnexion();

        //************************************ création d'objet, parametre obj, retourne booléen
        public override Boolean ajouter(MoyennesTrimestrielsBE obj)
        {
            MySqlTransaction transaction = con.connexion.BeginTransaction();

            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "INSERT INTO moyennestrimestriels (codemat,codetrimestre,matricule,moyenne,annee,rang, moyenneclasse, mention, moyennemin, moyennemax, appreciation) VALUES (@codeMat, @codetrimestre, @matricule, @moyenne, @annee,@rang, @moyenneclasse, @mention, @moyennemin, @moyennemax, @appreciation)";

                // utilisation de l'objet GroupePrivilegeBE passé en paramètre
                cmd.Parameters.AddWithValue("@codeMat", obj.codeMat);
                cmd.Parameters.AddWithValue("@codetrimestre", obj.codeTrimestre);
                cmd.Parameters.AddWithValue("@matricule", obj.matricule);
                cmd.Parameters.AddWithValue("@moyenne", obj.moyenne);
                cmd.Parameters.AddWithValue("@annee", obj.annee);
                cmd.Parameters.AddWithValue("@rang", obj.rang);

                cmd.Parameters.AddWithValue("@moyenneclasse", obj.moyenneClasse);
                cmd.Parameters.AddWithValue("@mention", obj.mention);
                cmd.Parameters.AddWithValue("@moyennemin", obj.moyenneMin);
                cmd.Parameters.AddWithValue("@moyennemax", obj.moyenneMax);
                cmd.Parameters.AddWithValue("@appreciation", obj.appreciation);

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
        //************************************ FIN création d'objet, parametre obj, retourne booléen

        //************************************ suppression d'objet, parametre obj, retourne booléen
        public override Boolean supprimer(MoyennesTrimestrielsBE obj)
        {
            MySqlTransaction transaction = con.connexion.BeginTransaction();
            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "DELETE FROM moyennestrimestriels WHERE codemat=@codeMat AND codetrimestre=@codetrimestre AND matricule=@matricule AND annee=@annee";

                // utilisation de l'objet GroupePrivilegeBE passé en paramètre
                cmd.Parameters.AddWithValue("@codeMat", obj.codeMat);
                cmd.Parameters.AddWithValue("@codetrimestre", obj.codeTrimestre);
                cmd.Parameters.AddWithValue("@matricule", obj.matricule);
                cmd.Parameters.AddWithValue("@annee", obj.annee);

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
        //************************************ FIN suppression d'objet, parametre obj, retourne booléen

        //*************************** mise à jour d'objet, parametre obj, retourne booléen
        public override Boolean modifier(MoyennesTrimestrielsBE obj, MoyennesTrimestrielsBE newobj)
        {
            supprimer(obj);
            ajouter(newobj);
            return true;
        }

        public Boolean modifier(MoyennesTrimestrielsBE obj)
        {
            MySqlTransaction transaction = con.connexion.BeginTransaction();
            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "UPDATE moyennestrimestriels SET moyenne=@moyenne, rang=@rang, moyenneclasse=@moyenneclasse, mention=@mention, moyennemin=@moyennemin, moyennemax=@moyennemax, appreciation=@appreciation WHERE codemat=@codeMat AND codetrimestre=@codetrimestre AND matricule=@matricule AND annee=@annee";

                // utilisation de l'objet GroupePrivilegeBE passé en paramètre
                cmd.Parameters.AddWithValue("@codeMat", obj.codeMat);
                cmd.Parameters.AddWithValue("@codetrimestre", obj.codeTrimestre);
                cmd.Parameters.AddWithValue("@matricule", obj.matricule);
                cmd.Parameters.AddWithValue("@moyenne", obj.moyenne);
                cmd.Parameters.AddWithValue("@annee", obj.annee);
                cmd.Parameters.AddWithValue("@rang", obj.rang);

                cmd.Parameters.AddWithValue("@moyenneclasse", obj.moyenneClasse);
                cmd.Parameters.AddWithValue("@mention", obj.mention);
                cmd.Parameters.AddWithValue("@moyennemin", obj.moyenneMin);
                cmd.Parameters.AddWithValue("@moyennemax", obj.moyenneMax);
                cmd.Parameters.AddWithValue("@appreciation", obj.appreciation);

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
        //*************************** FIN mise à jour d'objet, parametre obj, retourne booléen

        public override MoyennesTrimestrielsBE rechercher(MoyennesTrimestrielsBE moyenn)
        {
            string codeMatiere;
            string codetrimestre;
            string matricule;
            double moyenne;
            int annee;
            double moyenneclasse;
            string mention;
            double moyennemin;
            double moyennemax;
            string appreciation;

            MoyennesTrimestrielsBE m;
            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT * FROM moyennestrimestriels WHERE codemat=@codeMat AND codetrimestre=@codetrimestre AND matricule=@matricule AND annee=@annee";
                cmd.Parameters.AddWithValue("@codeMat", moyenn.codeMat);
                cmd.Parameters.AddWithValue("@codetrimestre", moyenn.codeTrimestre);
                cmd.Parameters.AddWithValue("@matricule", moyenn.matricule);
                cmd.Parameters.AddWithValue("@annee", moyenn.annee);

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer la voiture à retourner
                    if (dataReader.Read())
                    {
                        codeMatiere = Convert.ToString(dataReader["codemat"]);
                        codetrimestre = Convert.ToString(dataReader["codetrimestre"]);
                        matricule = Convert.ToString(dataReader["matricule"]);
                        moyenne = Convert.ToDouble(dataReader["moyenne"]);
                        annee = Convert.ToInt16(dataReader["annee"]);

                        moyenneclasse = Convert.ToDouble(dataReader["moyenneclasse"]);
                        mention = Convert.ToString(dataReader["mention"]);
                        moyennemin = Convert.ToDouble(dataReader["moyennemin"]);
                        moyennemax = Convert.ToInt16(dataReader["moyennemax"]);

                        appreciation = Convert.ToString(dataReader["mention"]);
                        m = new MoyennesTrimestrielsBE(codeMatiere, codetrimestre, matricule, moyenne, annee, Convert.ToInt32(dataReader["rang"]), moyenneclasse, mention, moyennemin, moyennemax, appreciation);
                        dataReader.Close();
                        // this.con.fermer();
                        return m;
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
        //*********************************** FIN rechercher d'un objet à partir de ses codes, parametre (code1, code2), retourne l'objet

        //************************************** retourner la liste de tout les objets
        public override List<MoyennesTrimestrielsBE> listerTous()
        {
            try
            {
                List<MoyennesTrimestrielsBE> listMoyBE = new List<MoyennesTrimestrielsBE>();

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT * FROM moyennestrimestriels";

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //Read the data and store them in the list
                    while (dataReader.Read())
                    {
                        MoyennesTrimestrielsBE moyBE = new MoyennesTrimestrielsBE(Convert.ToString(dataReader["codemat"]), Convert.ToString(dataReader["codetrimestre"]), Convert.ToString(dataReader["matricule"]),
                            Convert.ToDouble(dataReader["moyenne"]), Convert.ToInt16(dataReader["annee"]), Convert.ToInt32(dataReader["rang"]), Convert.ToDouble(dataReader["moyenneclasse"]),
                            Convert.ToString(dataReader["mention"]), Convert.ToDouble(dataReader["moyennemin"]), Convert.ToDouble(dataReader["moyennemax"]), Convert.ToString(dataReader["mention"]));
                        listMoyBE.Add(moyBE);
                    }

                    //close Data Reader
                    dataReader.Close();

                    //return list to be displayed
                    if (listMoyBE.Count != 0)
                        return listMoyBE;
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
        public override List<MoyennesTrimestrielsBE> listerSuivantCritere(String critere)
        {
            try
            {
                List<MoyennesTrimestrielsBE> listobjBE = new List<MoyennesTrimestrielsBE>();

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT * FROM moyennestrimestriels WHERE " + critere;

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //Read the data and store them in the list
                    while (dataReader.Read())
                    {
                        MoyennesTrimestrielsBE objBE = new MoyennesTrimestrielsBE(Convert.ToString(dataReader["codemat"]), Convert.ToString(dataReader["codetrimestre"]), Convert.ToString(dataReader["matricule"]),
                            Convert.ToDouble(dataReader["moyenne"]), Convert.ToInt16(dataReader["annee"]), Convert.ToInt32(dataReader["rang"]), Convert.ToDouble(dataReader["moyenneclasse"]),
                            Convert.ToString(dataReader["mention"]), Convert.ToDouble(dataReader["moyennemin"]), Convert.ToDouble(dataReader["moyennemax"]), Convert.ToString(dataReader["mention"]));
                        listobjBE.Add(objBE);
                    }

                    //close Data Reader
                    dataReader.Close();

                    //return list to be displayed
                    //return list to be displayed
                    if (listobjBE.Count != 0)
                        return listobjBE;
                    else return null;
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
                cmd.CommandText = "SELECT * FROM moyennestrimestriels";

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
                cmd.CommandText = "SELECT COUNT(*) FROM moyennestrimestriels";

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

        // Calcul de la moyenne trimestrielle des élèves
        public List<String[]> calculMoyenneTrimestrielle(String codeClasse, String codeMatiere, String codeTrimestre, int annee)
        {
            string codemat;
            string matricule;
            double moyenne;

            // ------------------ DEBUT calcul des moyennes trimestrielles des matières
            List<String[]> Lmoyenne = new List<string[]>();
            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT matricule, codemat, AVG(moyenne) FROM moyennes m WHERE codemat = '" + codeMatiere + "' AND matricule in (SELECT matricule FROM inscrire WHERE codeclasse = '" + codeClasse + "' AND annee = '" + annee + "') AND annee = '" + annee + "' AND codeseq in (SELECT codeseq FROM sequence WHERE codeTrimestre = '" + codeTrimestre + "') GROUP BY matricule";

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {

                    //fabriquer la voiture à retourner
                    while (dataReader.Read())
                    {
                        matricule = Convert.ToString(dataReader["matricule"]);
                        codemat = Convert.ToString(dataReader["codemat"]);
                        moyenne = Convert.ToDouble(dataReader["AVG(moyenne)"]);

                        String[] t = new String[3];
                        t[0] = matricule;
                        t[1] = codeMatiere;
                        t[2] = Convert.ToString(moyenne);

                        Lmoyenne.Add(t);
                    }

                    dataReader.Close();

                    if (Lmoyenne.Count != 0)
                        return Lmoyenne;
                    else return null;

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        // Calcul de la moyenne trimestrielle d'un élève
        public String[] calculMoyenneTrimestrielleDunEleve(String matricule, String codeClasse, String codeMatiere, String codeTrimestre, int annee)
        {
            string codemat;
            //string codetrimestre;
            double moyenne;

            // ------------------ DEBUT calcul des moyennes trimestrielles des matières
            List<String[]> Lmoyenne = new List<string[]>();
            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT matricule, codemat, AVG(moyenne) FROM moyennes m WHERE codemat = '" + codeMatiere + "' AND matricule in (SELECT matricule FROM inscrire WHERE codeclasse = '" + codeClasse + "' AND annee = '" + annee + "') AND annee = '" + annee + "' AND codeseq in (SELECT codeseq FROM sequence WHERE codeTrimestre = '" + codeTrimestre + "') AND matricule = '" + matricule + "' GROUP BY matricule";

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {

                    //fabriquer la voiture à retourner
                    while (dataReader.Read())
                    {
                        codemat = Convert.ToString(dataReader["codemat"]);
                        moyenne = Convert.ToDouble(dataReader["AVG(moyenne)"]);

                        String[] t = new String[4];
                        t[0] = matricule;
                        t[1] = codemat;
                        t[2] = codeTrimestre;
                        t[3] = Convert.ToString(moyenne);

                        Lmoyenne.Add(t);
                    }

                    dataReader.Close();

                    if (Lmoyenne.Count != 0)
                        return Lmoyenne.ElementAt(0);
                    else return null;

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        //recherche les moyennes d'un élève pour un Trimestre et une année donnée
        public List<MoyennesTrimestrielsBE> moyennesTrimestriellesEleve(String matricule, int annee, String codeTrimestre)
        {
            string codeMatiere;
            double moyenne;
            double moyenneclasse;
            string mention;
            double moyennemin;
            double moyennemax;

            string appreciation;

            List<MoyennesTrimestrielsBE> LMoyenneTrimestrielle = new List<MoyennesTrimestrielsBE>();

            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT * FROM moyennestrimestriels WHERE codeTrimestre='" + codeTrimestre + "' AND matricule='" + matricule + "' AND annee='" + annee + "'";
                //cmd.Parameters.AddWithValue("@codeSequence", codeSequence);
                //cmd.Parameters.AddWithValue("@matricule", matricule);
                //cmd.Parameters.AddWithValue("@annee", annee);

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer la voiture à retourner
                    while (dataReader.Read())
                    {
                        MoyennesTrimestrielsBE m;
                        codeMatiere = Convert.ToString(dataReader["codemat"]);
                        //codeSequence = Convert.ToString(dataReader["codeseq"]);
                        //matricule = Convert.ToString(dataReader["matricule"]);
                        moyenne = Convert.ToDouble(dataReader["moyenne"]);
                        //annee = Convert.ToInt16(dataReader["annee"]);

                        moyenneclasse = Convert.ToDouble(dataReader["moyenneclasse"]);
                        mention = Convert.ToString(dataReader["mention"]);
                        moyennemin = Convert.ToDouble(dataReader["moyennemin"]);
                        moyennemax = Convert.ToInt16(dataReader["moyennemax"]);

                        appreciation = Convert.ToString(dataReader["mention"]);
                        m = new MoyennesTrimestrielsBE(codeMatiere, codeTrimestre, matricule, moyenne, annee, Convert.ToInt32(dataReader["rang"]), moyenneclasse, mention, moyennemin, moyennemax, appreciation);
                        // this.con.fermer();
                        LMoyenneTrimestrielle.Add(m);
                    }

                    dataReader.Close();

                    return LMoyenneTrimestrielle;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        //liste les moyennes trimestrielles d'une matière des élèves d'une classe à une année
        public List<MoyennesTrimestrielsBE> listerMoyennesTrimestrielleDesElevesDuneClasse(String codeClasse, String codeMatiere, String codeTrimestre, int annee)
        {
            try
            {
                List<MoyennesTrimestrielsBE> listobjBE = new List<MoyennesTrimestrielsBE>();

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT * FROM moyennestrimestriels WHERE matricule in (SELECT matricule FROM inscrire WHERE codeclasse = '" + codeClasse + "' AND annee = '" + annee + "') AND codeTrimestre = '" + codeTrimestre + "' AND codeMat = '" + codeMatiere + "'";

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //Read the data and store them in the list
                    while (dataReader.Read())
                    {
                        MoyennesTrimestrielsBE objBE = new MoyennesTrimestrielsBE(Convert.ToString(dataReader["codemat"]), Convert.ToString(dataReader["codetrimestre"]), Convert.ToString(dataReader["matricule"]),
                            Convert.ToDouble(dataReader["moyenne"]), Convert.ToInt16(dataReader["annee"]), Convert.ToInt32(dataReader["rang"]), Convert.ToDouble(dataReader["moyenneclasse"]),
                            Convert.ToString(dataReader["mention"]), Convert.ToDouble(dataReader["moyennemin"]), Convert.ToDouble(dataReader["moyennemax"]), Convert.ToString(dataReader["mention"]));
                        listobjBE.Add(objBE);
                    }

                    //close Data Reader
                    dataReader.Close();

                    //return list to be displayed
                    //return list to be displayed
                    if (listobjBE.Count != 0)
                        return listobjBE;
                    else return null;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public LigneRecapitulatif recapitulatifTrimestrielEleve(EleveBE eleve, string codeclasse, string codetrimestre, int annee)
        {
            LigneRecapitulatif ligne = new LigneRecapitulatif();

            #region recuperation des infos sur les moyennes des groupes et le rang ainsi que la moyenne trimestrielle
            try
            {
                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT ms.matricule, p.codegroupe,SUM(ms.moyenne * p.coef) / SUM(coef) moyenne "
                                  + "  FROM moyennestrimestriels ms, programmer p "
                                  + "  WHERE ms.codemat = p.codemat "
                                  + "  AND ms.annee = p.annee "
                                  + "  AND p.codeclasse = " + "'" + codeclasse + "'"
                                  + "  AND ms.annee = " + "'" + annee + "'"
                                  + "  AND ms.codetrimestre = " + "'" + codetrimestre + "'"
                                  + "  AND ms.matricule = " + "'" + eleve.matricule + "'"
                                  + "  GROUP BY p.codegroupe "
                                  + "  order by p.codegroupe";
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    Dictionary<string, double> dict = new Dictionary<string, double>();
                    while (dataReader.Read())
                    {
                        dict.Add(Convert.ToString(dataReader["codegroupe"]), Convert.ToDouble(dataReader["moyenne"]));
                    }
                    ligne.moyennesGroupe = new Dictionary<string, double>(dict);

                    dataReader.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            #endregion

            #region recuperation des infos sur les notes trimestrielles des matieres
            try
            {
                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT ms.matricule, ms.rang as rang, ms.moyenne as moyenne, ms.codemat, ms.codetrimestre "
                                  + " FROM moyennestrimestriels ms, programmer p "
                                  + "  WHERE ms.codemat = p.codemat "
                                  + "  AND ms.annee = p.annee "
                                  + "  AND p.codeclasse = " + "'" + codeclasse + "'"
                                  + "  AND ms.annee = " + "'" + annee + "'"
                                  + "  AND ms.codetrimestre = " + "'" + codetrimestre + "'"
                                  + "  AND ms.matricule = " + "'" + eleve.matricule + "'"
                                  + ";";

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    Dictionary<string, double> dict = new Dictionary<string, double>();
                    while (dataReader.Read())
                    {
                        dict.Add(Convert.ToString(dataReader["codemat"]), Convert.ToDouble(dataReader["moyenne"]));
                    }
                    ligne.moyennesSequentielles = new Dictionary<string, double>(dict);

                    dataReader.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            #endregion

            #region reste des informations concernant la ligne
            ligne.nom = eleve.nom;
            try
            {
                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT rang, moyenne, point as total, mention FROM resultattrimestriel "
                                   + " WHERE matricule = " + "'" + eleve.matricule + "'"
                                   + " AND codetrimestre = " + "'" + codetrimestre + "'"
                                   + " AND annee = " + "'" + annee + "'";

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        ligne.rang = Convert.ToInt32(dataReader["rang"]);
                        ligne.moyenne = Convert.ToDouble(dataReader["moyenne"]);
                        ligne.total = Convert.ToDouble(dataReader["total"]);
                        ligne.mention = Convert.ToString(dataReader["mention"]);
                    }

                    dataReader.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            #endregion

            return ligne;
        }

        public LigneRecapitulatif recapitulatifAnnuelEleve(EleveBE eleve, string codeclasse, int annee)
        {
            LigneRecapitulatif ligne = new LigneRecapitulatif();

            #region recuperation des infos sur les moyennes des groupes et le rang ainsi que la moyenne sequentielle
            try
            {
                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT ms.matricule, p.codegroupe,SUM(ms.moyenne * p.coef) / SUM(coef) moyenne "
                                  + "  FROM moyennesannuelles ms, programmer p "
                                  + "  WHERE ms.codemat = p.codemat "
                                  + "  AND ms.annee = p.annee "
                                  + "  AND p.codeclasse = " + "'" + codeclasse + "'"
                                  + "  AND ms.annee = " + "'" + annee + "'"
                                  + "  AND ms.matricule = " + "'" + eleve.matricule + "'"
                                  + "  GROUP BY p.codegroupe "
                                  + "  order by p.codegroupe";
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    Dictionary<string, double> dict = new Dictionary<string, double>();
                    while (dataReader.Read())
                    {
                        dict.Add(Convert.ToString(dataReader["codegroupe"]), Convert.ToDouble(dataReader["moyenne"]));
                    }
                    ligne.moyennesGroupe = new Dictionary<string, double>(dict);

                    dataReader.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            #endregion

            #region recuperation des infos sur les notes sequentielle des matieres
            try
            {
                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT ms.matricule, ms.rang as rang, ms.moyenne as moyenne, ms.codemat "
                                  + " FROM moyennesannuelles ms, programmer p "
                                  + "  WHERE ms.codemat = p.codemat "
                                  + "  AND ms.annee = p.annee "
                                  + "  AND p.codeclasse = " + "'" + codeclasse + "'"
                                  + "  AND ms.annee = " + "'" + annee + "'"
                                  + "  AND ms.matricule = " + "'" + eleve.matricule + "'"
                                  + "  GROUP BY p.codegroupe;";

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    Dictionary<string, double> dict = new Dictionary<string, double>();
                    while (dataReader.Read())
                    {
                        dict.Add(Convert.ToString(dataReader["codemat"]), Convert.ToDouble(dataReader["moyenne"]));
                    }
                    ligne.moyennesSequentielles = new Dictionary<string, double>(dict);

                    dataReader.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            #endregion

            #region reste des informations concernant la ligne
            ligne.nom = eleve.nom;
            try
            {
                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT rang, moyenne, point as total FROM resultattrimestriel "
                                   + " WHERE matricule = " + "'" + eleve.matricule + "'"
                                   + " AND annee = " + "'" + annee + "'";

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        ligne.rang = Convert.ToInt32(dataReader["rang"]);
                        ligne.moyenne = Convert.ToDouble(dataReader["moyenne"]);
                        ligne.total = Convert.ToDouble(dataReader["total"]);
                    }

                    dataReader.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            #endregion

            return ligne;
        }

        internal List<KeyValuePair<string, int>> effectifValidationMatiereTrimestrielClasse(string codeclasse, int annee, string codetrimestre)
        {
            List<KeyValuePair<string, int>> resultat = new List<KeyValuePair<string, int>>();

            try
            {
                List<MoyennesAnnuellesBE> listMoyBE = new List<MoyennesAnnuellesBE>();
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT p.codemat matiere, if(t1.effectif is null,0,t1.effectif) effectif "
                                  + " FROM "
                                  + "   (SELECT codemat, count(*) effectif, m.annee "
                                    + "  FROM moyennestrimestriels m, inscrire i "
                                    + "  WHERE i.codeclasse = '" + codeclasse + "' and i.annee = '" + annee + "' and i.matricule = m.matricule "
                                    + "  and m.moyenne > 10 and m.codetrimestre = '" + codetrimestre + "' and i.annee=m.annee group by m.codemat) t1 "
                                    + " RIGHT JOIN (SELECT * FROM Programmer Where annee= '" + annee + "' and codeclasse= '" + codeclasse + "') p on (p.codemat=t1.codemat) "
                                    + " ORDER BY matiere";


                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        resultat.Add(new KeyValuePair<string, int>(Convert.ToString(dataReader["matiere"]), Convert.ToInt32(dataReader["effectif"])));
                    }
                    dataReader.Close();
                    return resultat;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        internal List<KeyValuePair<string, int>> effectifValidationResultatTrimestrielClasse(int annee, string codetrimestre)
        {
            List<KeyValuePair<string, int>> resultat = new List<KeyValuePair<string, int>>();

            try
            {
                List<MoyennesAnnuellesBE> listMoyBE = new List<MoyennesAnnuellesBE>();
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT classe, effectif, pourcentage "
                                  +"  FROM "
                                  +"  (SELECT codeclasse classe, count(r.matricule) effectif,  "
                                  + "  count(r.matricule)/(SELECT count(matricule) FROM inscrire WHERE codeClasse = classe  AND annee = '" + annee + "')*100 pourcentage "
                                  +"  FROM resultattrimestriel r RIGHT JOIN inscrire i "
                                  +"  ON r.matricule=i.matricule "  
                                  +"  AND r.annee=i.annee "
                                  + "  AND i.annee = '" + annee + "' "
                                  +"  AND r.decision='Admis' "
                                  + "  AND r.CODETRIMESTRE='" + codetrimestre + "'  "
                                  +"  group by i.codeclasse) r, niveau n, classe c "
                                  +"  WHERE r.classe=c.codeclasse "
                                  + "  AND c.codeniveau=n.codeniveau "
                                  +"  ORDER by niveau";

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        resultat.Add(new KeyValuePair<string, int>(Convert.ToString(dataReader["classe"]), Convert.ToInt32(dataReader["pourcentage"])));
                    }
                    dataReader.Close();
                    return resultat;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        internal List<KeyValuePair<string, int>> effectifValidationResultatTrimestrielNiveau(int annee, string codetrimestre)
        {
            List<KeyValuePair<string, int>> resultat = new List<KeyValuePair<string, int>>();

            try
            {
                List<MoyennesAnnuellesBE> listMoyBE = new List<MoyennesAnnuellesBE>();
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT n.CODENIVEAU codniv, sum(effectif) effectif, "
                                  +"         sum(effectif)/(SELECT count(matricule) "
                                  +"                             FROM inscrire i, classe c,niveau n "
                                  +"                             WHERE i.codeClasse = c.codeClasse "
			                      +"                 AND c.codeniveau = n.codeniveau "
                                  +"                             AND n.codeniveau = codniv "
                                  + "                             AND i.annee = '" + annee + "')*100 pourcentage "
                                  +"  FROM "
                                  +"  (SELECT codeclasse classe, count(r.matricule) effectif "
                                  +"  FROM resultattrimestriel r RIGHT JOIN inscrire i "
                                  +"  ON r.matricule=i.matricule  "
                                  +"  AND r.annee=i.annee "
                                  + "  AND i.annee = '" + annee + "' "
                                  +"  AND r.decision='Admis' "
                                  + "  AND r.CODETRIMESTRE='" + codetrimestre + "'" 
                                  +"  group by i.codeclasse) r, niveau n, classe c "
                                  +"  WHERE r.classe=c.codeclasse "
                                  +"  AND c.codeniveau=n.codeniveau "
                                  +"  GROUP by n.CODENIVEAU "
                                  +"  ORDER by niveau";

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        resultat.Add(new KeyValuePair<string, int>(Convert.ToString(dataReader["codniv"]), Convert.ToInt32(dataReader["pourcentage"])));
                    }
                    dataReader.Close();
                    return resultat;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        internal List<KeyValuePair<string, int>> progressionTrimestrielClasse(string codeclasse, int annee)
        {
            List<KeyValuePair<string, int>> resultat = new List<KeyValuePair<string, int>>();

            try
            {
                List<MoyennesAnnuellesBE> listMoyBE = new List<MoyennesAnnuellesBE>();
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT t.CODETRIMESTRE Trimestre,if(effectif is null,0,effectif) effectif,if(pourcentage is null,0,pourcentage) pourcentage "
                                  +"  FROM "
                                  +"  (SELECT CODETRIMESTRE, count(matricule) effectif, "
                                  + "         count(matricule)/(SELECT count(matricule) FROM inscrire WHERE codeClasse = '" + codeclasse + "' AND annee = '" + annee + "')*100 pourcentage "
                                  +"   FROM resultattrimestriel "
                                  + "  WHERE ANNEE='" + annee + "' "
                                  +"  AND DECISION='Admis' "
                                  +"  AND MATRICULE IN (SELECT matricule "
		                          +"            FROM inscrire "
                                  + "            WHERE codeClasse = '" + codeclasse + "' "
                                  + "                    AND annee = '" + annee + "')"
                                  +"  Group by CODETRIMESTRE) r RIGHT JOIN (SELECT * FROM trimestre) t on (t.CODETRIMESTRE=r.CODETRIMESTRE)";

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        resultat.Add(new KeyValuePair<string, int>(Convert.ToString(dataReader["Trimestre"]), Convert.ToInt32(dataReader["pourcentage"])));
                    }
                    dataReader.Close();
                    return resultat;
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
