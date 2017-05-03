using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Ecole.BusinessEntity;
using MySql.Data.MySqlClient;
using Ecole.ClasseConception;

namespace Ecole.DataAccess
{
    class ResultatTrimestrielDA : DA<ResultatTrimestrielBE>
    {
        private Connexion con = Connexion.getConnexion();

        //----------------Ajout d'un nouveau Resultat ------------------------------
        public override Boolean ajouter(ResultatTrimestrielBE R)
        {
            MySqlTransaction transaction = con.connexion.BeginTransaction();
            try
            {

                // Création d'un commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "INSERT INTO resultattrimestriel (CODETRIMESTRE, MATRICULE, ANNEE, POINT, COEF, MOYENNE, RANG, MOYENNECLASSE, MENTION, REMARQUE, DECISION, APPRECIATION) VALUES (@codeT, @mat, @annee, @point, @coef, @moy, @rang, @moyC, @mention, @remarque, @decision, @APPRECIATION)";

                // utilisation de l'objet voiture passé en paramètre
                cmd.Parameters.AddWithValue("@codeT", R.codeTrimestre);
                cmd.Parameters.AddWithValue("@mat", R.matricule);
                cmd.Parameters.AddWithValue("@annee", R.annee);
                cmd.Parameters.AddWithValue("@point", R.point);
                cmd.Parameters.AddWithValue("@coef", R.coef);
                cmd.Parameters.AddWithValue("@moy", R.moyenne);
                cmd.Parameters.AddWithValue("@rang", R.rang);
                cmd.Parameters.AddWithValue("@moyC", R.moyenneclasse);
                cmd.Parameters.AddWithValue("@mention", R.mention);
                cmd.Parameters.AddWithValue("@remarque", R.remarque);
                cmd.Parameters.AddWithValue("@decision", R.decision);
                cmd.Parameters.AddWithValue("@APPRECIATION", R.appreciation);

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

        //--------------------------Suppression d'un Resultat ------

        public override Boolean supprimer(ResultatTrimestrielBE R)
        {
            MySqlTransaction transaction = con.connexion.BeginTransaction();
            try
            {

                // Création d'un commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "DELETE FROM resultattrimestriel WHERE CODETRIMESTRE=@codeT AND MATRICULE=@mat AND ANNEE=@annee";

                // utilisation de l'objet voiture passé en paramètre
                cmd.Parameters.AddWithValue("@codeT", R.codeTrimestre);
                cmd.Parameters.AddWithValue("@mat", R.matricule);
                cmd.Parameters.AddWithValue("@annee", R.annee);

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

        //--------------------------Modification d'un Resultat -----------

        // mise à jour d'objet, parametre obj, retourne booléen
        public override Boolean modifier(ResultatTrimestrielBE R, ResultatTrimestrielBE newR)
        {
            MySqlTransaction transaction = con.connexion.BeginTransaction();
            try
            {

                // Création d'un commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "UPDATE resultattrimestriel SET ANNEE=@nouveauannee, CODETRIMESTRE=@nouveaucodeT, MATRICULE=@nouveaumat, POINT=@point, COEF=@coef, MOYENNE=@moy, RANG=@rang, MOYENNECLASSE=@moyC, MENTION=@mention, REMARQUE=@remarque, DECISION=@decision, APPRECIATION=@APPRECIATION WHERE ANNEE=@ancienannee and CODETRIMESTRE=@anciencodeT and MATRICULE=@ancienmat";

                // utilisation de l'objet voiture passé en paramètre
                cmd.Parameters.AddWithValue("@nouveauannee", newR.annee);
                cmd.Parameters.AddWithValue("@nouveaucodeT", newR.codeTrimestre);
                cmd.Parameters.AddWithValue("@nouveaumat", newR.matricule);

                cmd.Parameters.AddWithValue("@ancienannee", R.annee);
                cmd.Parameters.AddWithValue("@anciencodeT", R.codeTrimestre);
                cmd.Parameters.AddWithValue("@ancienmat", R.matricule);

                cmd.Parameters.AddWithValue("@point", newR.point);
                cmd.Parameters.AddWithValue("@coef", newR.coef);
                cmd.Parameters.AddWithValue("@moy", newR.moyenne);
                cmd.Parameters.AddWithValue("@rang", newR.rang);
                cmd.Parameters.AddWithValue("@moyC", newR.moyenneclasse);
                cmd.Parameters.AddWithValue("@mention", newR.mention);
                cmd.Parameters.AddWithValue("@remarque", newR.remarque);
                cmd.Parameters.AddWithValue("@decision", newR.decision);
                cmd.Parameters.AddWithValue("@APPRECIATION", newR.appreciation);

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

        public Boolean modifier(ResultatTrimestrielBE R)
        {
            MySqlTransaction transaction = con.connexion.BeginTransaction();
            try
            {

                // Création d'un commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "UPDATE resultattrimestriel SET POINT=@point, COEF=@coef, MOYENNE=@moy, RANG=@rang, REMARQUE=@remarque"
                 + " MOYENNECLASSE=@moyC, MENTION=@mention, DECISION=@decision, APPRECIATION=@APPRECIATION WHERE CODETRIMESTRE=@codeT and MATRICULE=@mat and ANNEE=@annee";

                // utilisation de l'objet voiture passé en paramètre
                cmd.Parameters.AddWithValue("@codeT", R.codeTrimestre);
                cmd.Parameters.AddWithValue("@mat", R.matricule);
                cmd.Parameters.AddWithValue("@annee", R.annee);
                cmd.Parameters.AddWithValue("@point", R.point);
                cmd.Parameters.AddWithValue("@coef", R.coef);
                cmd.Parameters.AddWithValue("@moy", R.moyenne);
                cmd.Parameters.AddWithValue("@rang", R.rang);
                cmd.Parameters.AddWithValue("@moyC", R.moyenneclasse);
                cmd.Parameters.AddWithValue("@mention", R.mention);
                cmd.Parameters.AddWithValue("@remarque", R.remarque);
                cmd.Parameters.AddWithValue("@decision", R.decision);
                cmd.Parameters.AddWithValue("@APPRECIATION", R.appreciation);

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

        //---------------Rechercher des informations sur une Resultat spécifique---------------------------------
        public override ResultatTrimestrielBE rechercher(ResultatTrimestrielBE resultat)
        {
            string codetrimestre;
            string matricule;
            int annee;
            double point;
            int coef;
            double moyenne;
            int rang;
            double moyenneclasse;
            string mention;
            string remarque;
            string decision;
            string APPRECIATION;

            ResultatTrimestrielBE R;

            try
            {
                // ouverture de la connexion
                // this.con.ouvrir();

                // Création d'un commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT * FROM resultattrimestriel WHERE CODETRIMESTRE=@codeT and MATRICULE=@mat and ANNEE=@annee";
                cmd.Parameters.AddWithValue("@codeT", resultat.codeTrimestre);
                cmd.Parameters.AddWithValue("@mat", resultat.matricule);
                cmd.Parameters.AddWithValue("@annee", resultat.annee);

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer la voiture à retourner
                    if (dataReader.Read())
                    {
                        point = Convert.ToDouble(dataReader["POINT"]);
                        coef = Convert.ToInt32(dataReader["coef"]);
                        moyenne = Convert.ToDouble(dataReader["MOYENNE"]);
                        rang = Convert.ToInt32(dataReader["RANG"]);
                        moyenneclasse = Convert.ToDouble(dataReader["MOYENNECLASSE"]);
                        mention = Convert.ToString(dataReader["MENTION"]);
                        remarque = Convert.ToString(dataReader["remarque"]); 
                        decision = Convert.ToString(dataReader["decision"]);
                        APPRECIATION = Convert.ToString(dataReader["APPRECIATION"]);

                        R = new ResultatTrimestrielBE(resultat.codeTrimestre, resultat.matricule, Convert.ToInt32(resultat.annee), point, coef, moyenne, rang,
                            moyenneclasse, mention, remarque, decision, APPRECIATION);
                        dataReader.Close();
                        // this.con.fermer();
                        return R;
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
        public override List<ResultatTrimestrielBE> listerTous()
        {
            List<ResultatTrimestrielBE> list = new List<ResultatTrimestrielBE>();
            int annee;
            string codetrimestre;
            string matricule;
            double point;
            int coef;
            double moyenne;
            int rang;
            double moyenneclasse;
            string mention;
            string decision;
            string APPRECIATION;

            ResultatTrimestrielBE R;

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT * FROM resultattrimestriel";

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet acheter à retourner
                    while (dataReader.Read())
                    {
                        annee = Convert.ToInt32(dataReader["annee"]);
                        codetrimestre = Convert.ToString(dataReader["CODETRIMESTRE"]);
                        matricule = Convert.ToString(dataReader["matricule"]);
                        point = Convert.ToDouble(dataReader["POINT"]);
                        coef = Convert.ToInt32(dataReader["coef"]);
                        moyenne = Convert.ToDouble(dataReader["MOYENNE"]);
                        rang = Convert.ToInt32(dataReader["RANG"]);
                        moyenneclasse = Convert.ToDouble(dataReader["MOYENNECLASSE"]);
                        mention = Convert.ToString(dataReader["MENTION"]); 
                        decision = Convert.ToString(dataReader["DECISION"]);
                        APPRECIATION = Convert.ToString(dataReader["APPRECIATION"]);

                        R = new ResultatTrimestrielBE(codetrimestre, matricule, annee, point, coef, moyenne, rang, moyenneclasse, mention, Convert.ToString(dataReader["remarque"]), decision, APPRECIATION);
                        list.Add(R);
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


        public override List<ResultatTrimestrielBE> listerSuivantCritere(string critere)
        {
            List<ResultatTrimestrielBE> list = new List<ResultatTrimestrielBE>();
            int annee;
            string codetrimestre;
            string matricule;
            double point;
            int coef;
            double moyenne;
            int rang;
            double moyenneclasse;
            string mention;
            string decision;
            string APPRECIATION;

            ResultatTrimestrielBE R;

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT * FROM resultattrimestriel WHERE " + critere;

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet acheter à retourner
                    while (dataReader.Read())
                    {
                        annee = Convert.ToInt32(dataReader["annee"]);
                        codetrimestre = Convert.ToString(dataReader["CODETRIMESTRE"]);
                        matricule = Convert.ToString(dataReader["matricule"]);
                        point = Convert.ToDouble(dataReader["POINT"]);
                        coef = Convert.ToInt32(dataReader["coef"]);
                        moyenne = Convert.ToDouble(dataReader["MOYENNE"]);
                        rang = Convert.ToInt32(dataReader["RANG"]);
                        moyenneclasse = Convert.ToDouble(dataReader["MOYENNECLASSE"]);
                        mention = Convert.ToString(dataReader["MENTION"]); 
                        decision = Convert.ToString(dataReader["DECISION"]);
                        APPRECIATION = Convert.ToString(dataReader["APPRECIATION"]);

                        R = new ResultatTrimestrielBE(codetrimestre, matricule, annee, point, coef, moyenne, rang, moyenneclasse, mention, Convert.ToString(dataReader["remarque"]), decision, APPRECIATION);
                        list.Add(R);
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
                cmd.CommandText = "SELECT * FROM resultattrimestriel";

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
                cmd.CommandText = "SELECT COUNT(*) FROM resultattrimestriel";

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


        // Calcul des résultats trimestriels des élèves
        public List<String[]> calculResultatTrimestriel(String codeClasse, String codeTrimestre, int annee)
        {
            string matricule;
            double moyenne;
            double totalPoint;
            int coef;

            // ------------------ DEBUT calcul des moyennes trimestrielles des matières
            List<String[]> LResultat = new List<string[]>();
            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
               

               // cmd.CommandText = "SELECT matricule, annee, SUM(moyenne * coef), SUM(coef), AVG(moyenne) FROM resultat WHERE annee = '" + annee + "' AND codeseq in (SELECT codeSeq FROM Sequence WHERE codetrimestre = '" + codeTrimestre + "') AND matricule in (SELECT matricule FROM inscrire Where codeclasse = '" + codeClasse + "' AND annee = '" + annee + "') GROUP BY matricule";

                cmd.CommandText = "SELECT r.matricule, r.annee, tr.totalTrim, tr.somCoef, AVG(moyenne), tr.totalTrim/tr.somCoef moyenne " +
                    "FROM resultat r, (SELECT matricule, codeTrimestre, SUM(moyenne * coef) totalTrim, SUM(coef) somCoef, annee " +
                    "FROM (SELECT m.matricule, m.codemat, c.coef, m.codeTrimestre, m.moyenne, m.annee FROM moyennestrimestriels m, (SELECT codemat, coef, annee FROM programmer WHERE codeclasse = '" + codeClasse + "' AND annee = '" + annee + "') c WHERE m.codemat = c.codemat AND m.annee = c.annee AND m.annee = '" + annee + "' AND m.codeTrimestre = '" + codeTrimestre + "') mt " +
                    "WHERE matricule in (SELECT matricule FROM inscrire Where codeclasse = '" + codeClasse + "' AND annee = '" + annee + "') " +
                    "GROUP BY matricule) tr " +
                    "WHERE r.matricule=tr.matricule " +
                    "AND r.annee=tr.annee " +
                    "AND r.annee = '" + annee + "' " +
                    "AND codeseq in (SELECT codeSeq FROM Sequence WHERE codetrimestre = '" + codeTrimestre + "') " +
                    "AND r.matricule in (SELECT matricule FROM inscrire Where codeclasse = '" + codeClasse + "' AND annee = '" + annee + "') GROUP BY matricule";

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {

                    //fabriquer la voiture à retourner
                    while (dataReader.Read())
                    {
                        //matricule = Convert.ToString(dataReader["matricule"]);
                        //moyenne = Convert.ToDouble(dataReader["AVG(moyenne)"]);
                        //totalPoint = Convert.ToDouble(dataReader["SUM(moyenne * coef)"]);
                        //coef = Convert.ToInt16(dataReader["SUM(coef)"]);

                        matricule = Convert.ToString(dataReader["matricule"]);
                        moyenne = Convert.ToDouble(dataReader["moyenne"]);
                        totalPoint = Convert.ToDouble(dataReader["totalTrim"]);
                        coef = Convert.ToInt16(dataReader["somCoef"]);

                        String[] t = new String[6];
                        t[0] = matricule; // le matricule de l'élève
                        t[1] = codeTrimestre; // le code du trimestre
                        t[2] = Convert.ToString(annee); //l'année
                        t[3] = Convert.ToString(totalPoint); // la somme totale des points de l'élève
                        t[4] = Convert.ToString(moyenne);
                        t[5] = Convert.ToString(coef);

                        LResultat.Add(t);
                    }

                    dataReader.Close();

                    if (LResultat.Count != 0)
                        return LResultat;
                    else return null;

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }


        // Calcul des résultats Trimestriel d'un élève particulier dans une classe donnée
        public String[] calculResultatTrimestrielDunEleve(String matricule, String codeClasse, String codeTrimestre, int annee)
        {
            double moyenne;
            double totalPoint;
            int coef;

            // ------------------ DEBUT calcul des moyennes trimestrielles des matières
            List<String[]> LResultat = new List<string[]>();
            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                //cmd.CommandText = "SELECT matricule, annee, SUM(moyenne * coef), SUM(coef), SUM(moyenne)/(SELECT COUNT(codeTrimestre) FROM sequence WHERE codeTrimestre = '" + codeTrimestre + "') FROM resultat WHERE annee = '" + annee + "' AND matricule = '" + matricule + "'  GROUP BY matricule";
                cmd.CommandText = "SELECT matricule, annee, SUM(moyenne * coef), SUM(coef), AVG(moyenne) FROM resultat WHERE annee = '" + annee + "' AND matricule = '" + matricule + "' AND codeseq in (SELECT codeSeq FROM Sequence WHERE codetrimestre = '" + codeTrimestre + "') GROUP BY matricule";

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {

                    //fabriquer la voiture à retourner
                    while (dataReader.Read())
                    {
                        matricule = Convert.ToString(dataReader["matricule"]);
                        moyenne = Convert.ToDouble(dataReader["AVG(moyenne)"]);
                        totalPoint = Convert.ToDouble(dataReader["SUM(moyenne * coef)"]);
                        coef = Convert.ToInt16(dataReader["SUM(coef)"]);

                        String[] t = new String[6];
                        t[0] = matricule; // le matricule de l'élève
                        t[1] = codeTrimestre; // le code du trimestre
                        t[2] = Convert.ToString(annee); //l'année
                        t[3] = Convert.ToString(totalPoint); // la somme totale des points de l'élève
                        t[4] = Convert.ToString(moyenne);
                        t[5] = Convert.ToString(coef);

                        LResultat.Add(t);
                    }

                    dataReader.Close();

                    if (LResultat.Count != 0)
                        return LResultat.ElementAt(0);
                    else return null;

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

        }


        //recherche les résultats d'un élève pour un Trimestre et une année donnée
        public List<ResultatTrimestrielBE> resultatsTrimestrielsEleve(String matricule, int annee, String codeTrimestre)
        {
            double point;
            int coef;
            double moyenne;
            int rang;
            double moyenneclasse;
            string mention;
            string remarque;
            string decision;
            string APPRECIATION;

            List<ResultatTrimestrielBE> LResultat = new List<ResultatTrimestrielBE>();

            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT * FROM resultattrimestriel WHERE codeTrimestre='" + codeTrimestre + "' AND matricule='" + matricule + "' AND annee='" + annee + "'";
                //cmd.Parameters.AddWithValue("@codeSequence", codeSequence);
                //cmd.Parameters.AddWithValue("@matricule", matricule);
                //cmd.Parameters.AddWithValue("@annee", annee);

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer la voiture à retourner
                    while (dataReader.Read())
                    {
                        ResultatTrimestrielBE m;
                        point = Convert.ToDouble(dataReader["POINT"]);
                        coef = Convert.ToInt32(dataReader["coef"]);
                        moyenne = Convert.ToDouble(dataReader["MOYENNE"]);
                        rang = Convert.ToInt32(dataReader["RANG"]);
                        moyenneclasse = Convert.ToDouble(dataReader["MOYENNECLASSE"]);
                        mention = Convert.ToString(dataReader["MENTION"]);
                        remarque = Convert.ToString(dataReader["remarque"]); 
                        decision = Convert.ToString(dataReader["decision"]);
                        APPRECIATION = Convert.ToString(dataReader["APPRECIATION"]);

                        m = new ResultatTrimestrielBE(codeTrimestre, matricule, annee, point, coef, moyenne, rang,
                            moyenneclasse, mention, remarque, decision, APPRECIATION);
                        // this.con.fermer();
                        LResultat.Add(m);
                    }

                    dataReader.Close();

                    return LResultat;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        //liste les résultats trimestriels des élèves d'une classe pour une matière et une année
        public List<ResultatTrimestrielBE> listerResultatsTrimestrielDesElevesDuneClasse(String codeClasse, String codeTrimestre, int annee)
        {
            List<ResultatTrimestrielBE> list = new List<ResultatTrimestrielBE>();
            //int annee;
            string codetrimestre;
            string matricule;
            double point;
            int coef;
            double moyenne;
            int rang;
            double moyenneclasse;
            string mention;
            string decision;
            string APPRECIATION;

            ResultatTrimestrielBE R;

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT * FROM resultattrimestriel WHERE annee = '" + annee + "' AND matricule in (SELECT matricule FROM inscrire WHERE codeclasse LIKE '" + codeClasse + "' AND annee = '" + annee + "') AND codeTrimestre LIKE '" + codeTrimestre + "'";

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet acheter à retourner
                    while (dataReader.Read())
                    {
                        annee = Convert.ToInt32(dataReader["annee"]);
                        codetrimestre = Convert.ToString(dataReader["CODETRIMESTRE"]);
                        matricule = Convert.ToString(dataReader["matricule"]);
                        point = Convert.ToDouble(dataReader["POINT"]);
                        coef = Convert.ToInt32(dataReader["coef"]);
                        moyenne = Convert.ToDouble(dataReader["MOYENNE"]);
                        rang = Convert.ToInt32(dataReader["RANG"]);
                        moyenneclasse = Convert.ToDouble(dataReader["MOYENNECLASSE"]);
                        mention = Convert.ToString(dataReader["MENTION"]); 
                        decision = Convert.ToString(dataReader["DECISION"]);
                        APPRECIATION = Convert.ToString(dataReader["APPRECIATION"]);

                        R = new ResultatTrimestrielBE(codetrimestre, matricule, annee, point, coef, moyenne, rang, moyenneclasse, mention, Convert.ToString(dataReader["remarque"]), decision, APPRECIATION);
                        list.Add(R);
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


        internal bool enregistrerRemarque(string matricule, int annee, string trimestre, string remarque)
        {
            MySqlTransaction transaction = con.connexion.BeginTransaction();
            ResultatTrimestrielBE resultat = new ResultatTrimestrielBE();
            try
            {

                // Création d'un commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();
                resultat.matricule = matricule;
                resultat.annee = annee;
                resultat.codeTrimestre = trimestre;
                resultat = rechercher(resultat);
                if (resultat == null)
                    cmd.CommandText = "INSERT INTO resultattrimestriel (MATRICULE, ANNEE, CODETRIMESTRE, REMARQUE) VALUES (@mat, @annee, @codeT, @remarque)";
                else
                    cmd.CommandText = "UPDATE resultattrimestriel SET REMARQUE=@remarque WHERE CODETRIMESTRE=@codeT and MATRICULE=@mat and ANNEE=@annee";

                // utilisation de l'objet voiture passé en paramètre
                cmd.Parameters.AddWithValue("@mat", matricule);
                cmd.Parameters.AddWithValue("@annee", annee);
                cmd.Parameters.AddWithValue("@codeT", trimestre);
                cmd.Parameters.AddWithValue("@remarque", remarque);

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

        //génère un objet contenant les infos qui doivent figurer dans le Bulletin Trimestriel d'un élève
        public List<LigneBulletinTrimestriel> genererBulletinTrimestrielDunEleve(String matricule, int annee, String codeClasse, String codeTrimestre)
        {
            List<LigneBulletinTrimestriel> list = new List<LigneBulletinTrimestriel>();
            //String matricule; // le matricule de l'élève
            String codeMat; // le code de la matière
            String nomMat; // le nom de la matière
            String codeprof; // le code de l'enseignant
            String nomProf; // le nom de l'enseignant
            int coef; // le coeficient de la matière
            String codeGroupe; // le code du groupe de la matière
            String nomGroupe; // le nom du groupe de la matière
            String codeSeq; // le code de la séquence
            double moyenneSeq; // la moyenne de la séquence
            double moyenneTrim; // le moyenne du trimestre
            int rangTrim; // le rang trimestriel
            double moyenneClasseTrim; // la moyenne trimestrielle de le classe
            double moyenneMinClasseTrim; // la moyenne trimestrielle minimale de la classe
            double moyenneMaxClasseTrim; // la moyenne trimestrielle de le classe
            String mention; // la mention de la moyenne de l'élève
            double totalPointGroupe; // le total des points du groupe
            double totalPointMaxGroupe; // le total des points Max du groupe        
            double totalCoefGroupe; // la total des coeficient du groupe
            double moyenneGroupe; // la moyenne du groupe de l'élève
            String appreciation;

            LigneBulletinTrimestriel ligne;

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT m.matricule, p.codemat, p.nommat, p.codeprof, p.nomprof, p.coef, p.codegroupe, g.nomgroupe, " +
                                  "m.codeSeq, m.moyenneSeq, m.moyenneTrim, m.rangTrim, m.moyenneclasse, m.moyennemin, m.moyennemax, m.mention, m.appreciation, " +
                                  "g.totalPointGroupe, g.totalPointMaxGroupe, g.totalCoefGroupe, g.moyenneGroupe " +
                                  "FROM " +
                                  "(SELECT m.codemat, m.nommat, p.codeprof, e.nomprof, p.coef, p.codegroupe " +
                                  "FROM matiere m, programmer p, enseignant e " +
                                  "WHERE p.codeclasse = '" + codeClasse + "' " +
                                  "AND p.codemat = m.codemat " +
                                  "AND p.codeprof = e.codeprof " +
                                  "AND p.annee = '" + annee + "') p, " +
                                  "(SELECT ms.matricule, ms.codemat, ms.codeSeq, ms.moyenne as moyenneSeq, mt.moyenne as moyenneTrim, mt.rang as rangTrim, mt.moyenneclasse, mt.moyennemin, mt.moyennemax, mt.mention, mt.appreciation as appreciation " +
                                  "FROM moyennes ms, moyennestrimestriels mt " +
                                  "WHERE ms.matricule = mt.matricule " +
                                  "AND mt.codetrimestre = '" + codeTrimestre + "' " +
                                  "AND ms.codeSeq in (SELECT codeSeq FROM sequence WHERE codeTrimestre = '" + codeTrimestre + "') " +
                                  "AND ms.annee = mt.annee " +
                                  "AND ms.annee = '" + annee + "' " +
                                  "AND ms.codemat = mt.codemat " +
                                  "AND mt.matricule = '" + matricule + "') m, " +
                                  "(SELECT mt.matricule, p.codegroupe, g.nomgroupe, SUM(mt.moyenne * p.coef) totalPointGroupe, SUM(20 * p.coef) totalPointMaxGroupe, SUM(coef) totalCoefGroupe, " +
                                  "SUM(mt.moyenne * p.coef) / SUM(coef) moyenneGroupe " +
                                  "FROM moyennestrimestriels mt, programmer p, groupematiere g " +
                                  "WHERE p.codegroupe = g.codegroupe " +
                                  "AND mt.matricule = '" + matricule + "' " +
                                  "AND mt.codemat = p.codemat " +
                                  "AND mt.annee = p.annee " +
                                  "AND p.codeclasse = '" + codeClasse + "' " +
                                  "AND mt.annee = '" + annee + "' " +
                                  "AND mt.codetrimestre = '" + codeTrimestre + "' " +
                                  "GROUP BY p.codegroupe) g " +
                                  "WHERE p.codemat = m.codemat " +
                                  "AND p.codegroupe = g.codegroupe " +
                                  "ORDER BY p.codegroupe, p.codemat, m.codeSeq";

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet acheter à retourner
                    while (dataReader.Read())
                    {


                        //annee = Convert.ToInt32(dataReader["annee"]);

                        codeMat = Convert.ToString(dataReader["codemat"]); // le code de la matière
                        nomMat = Convert.ToString(dataReader["nommat"]); // le nom de la matière
                        codeprof = Convert.ToString(dataReader["codeprof"]); // le code de l'enseignant
                        nomProf = Convert.ToString(dataReader["nomprof"]); // le nom de l'enseignant
                        coef = Convert.ToInt16(dataReader["coef"]); // le coeficient de la matière
                        codeGroupe = Convert.ToString(dataReader["codegroupe"]); // le code du groupe de la matière
                        nomGroupe = Convert.ToString(dataReader["nomgroupe"]); // le nom du groupe de la matière
                        codeSeq = Convert.ToString(dataReader["codeSeq"]); // le code de la séquence
                        moyenneSeq = Convert.ToDouble(dataReader["moyenneSeq"]); // la moyenne de la séquence
                        moyenneTrim = Convert.ToDouble(dataReader["moyenneTrim"]); // le moyenne du trimestre
                        rangTrim = Convert.ToInt16(dataReader["rangTrim"]); // le rang trimestriel
                        moyenneClasseTrim = Convert.ToDouble(dataReader["moyenneclasse"]); // la moyenne trimestrielle de le classe
                        moyenneMinClasseTrim = Convert.ToDouble(dataReader["moyennemin"]); // la moyenne trimestrielle minimale de la classe
                        moyenneMaxClasseTrim = Convert.ToDouble(dataReader["moyennemax"]); // la moyenne trimestrielle de le classe
                        mention = Convert.ToString(dataReader["mention"]); // la mention de la moyenne de l'élève
                        totalPointGroupe = Convert.ToDouble(dataReader["totalPointGroupe"]); // le total des points du groupe
                        totalPointMaxGroupe = Convert.ToDouble(dataReader["totalPointMaxGroupe"]);// le total des points Max du groupe        
                        totalCoefGroupe = Convert.ToDouble(dataReader["totalCoefGroupe"]); // la total des coeficient du groupe
                        moyenneGroupe = Convert.ToDouble(dataReader["moyenneGroupe"]); // la moyenne du groupe de l'élève
                        appreciation = Convert.ToString(dataReader["appreciation"]);
 
                        ligne = new LigneBulletinTrimestriel(matricule, codeMat, nomMat, codeprof, nomProf, coef, codeGroupe, nomGroupe, codeSeq, moyenneSeq, moyenneTrim, rangTrim,
                                    moyenneClasseTrim, moyenneMinClasseTrim, moyenneMaxClasseTrim, mention, totalPointGroupe, totalPointMaxGroupe, totalCoefGroupe, moyenneGroupe, appreciation);

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


        //retourne la moyenne minimale des élèves d'une classe pour un trimestre et une année
        public double getMoynenneTrimestrielleMinimaleDuneClasse(String codeClasse, String codeTrimestre, int annee)
        {

            double moyenneMin;


            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT MIN(moyenne) FROM resultattrimestriel WHERE annee = '" + annee + "' AND codeTrimestre = '" + codeTrimestre + "' AND matricule in (SELECT matricule FROM inscrire WHERE codeclasse = '" + codeClasse + "' AND annee='" + annee + "')";

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet acheter à retourner
                    if (dataReader.Read())
                    {
                        moyenneMin = Convert.ToDouble(dataReader["MIN(moyenne)"]);
                        dataReader.Close();

                        return moyenneMin;
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


        //retourne la moyenne maximale des élèves d'une classe pour un trimestre et une année
        public double getMoynenneTrimestrielleMaximaleDuneClasse(String codeClasse, String codeTrimestre, int annee)
        {

            double moyenneMax;


            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT MAX(moyenne) FROM resultattrimestriel WHERE annee = '" + annee + "' AND codeTrimestre = '" + codeTrimestre + "' AND matricule in (SELECT matricule FROM inscrire WHERE codeclasse = '" + codeClasse + "' AND annee='" + annee + "')";

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet acheter à retourner
                    if (dataReader.Read())
                    {
                        moyenneMax = Convert.ToDouble(dataReader["MAX(moyenne)"]);
                        dataReader.Close();

                        return moyenneMax;
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

        public double obtenirMoyenneTrimestrielleDuneClasse(string codeclasse, string codetrim, int annee)
        {
            try
            {
                MySqlCommand cmd = con.connexion.CreateCommand();

                cmd.CommandText = "SELECT distinct moyenneclasse FROM resultattrimestriel r, inscrire i where i.matricule=r.matricule and i.codeclasse = '" + codeclasse + "' and i.annee='" + annee + "' and r.codetrimestre='" + codetrim + "' and r.annee=i.annee;";

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    if (dataReader.Read())
                    {
                        return Convert.ToDouble(dataReader["moyenneclasse"]);
                    }
                    return -1;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -1;
            }
        }

        public ResultatTrimestrielBE resultatTrimestrielEleve(String matricule, int annee, String codeTrimestre)
        {
            double point;
            int coef;
            double moyenne;
            int rang;
            double moyenneclasse;
            string mention;
            string remarque;
            string decision;
            string APPRECIATION;

            try
            {

                MySqlCommand cmd = con.connexion.CreateCommand();

                cmd.CommandText = "SELECT * FROM resultattrimestriel WHERE codeTrimestre='" + codeTrimestre + "' AND matricule='" + matricule + "' AND annee='" + annee + "'";
                ResultatTrimestrielBE m = new ResultatTrimestrielBE();
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    if (dataReader.Read())
                    {
                        point = Convert.ToDouble(dataReader["POINT"]);
                        coef = Convert.ToInt32(dataReader["coef"]);
                        moyenne = Convert.ToDouble(dataReader["MOYENNE"]);
                        rang = Convert.ToInt32(dataReader["RANG"]);
                        moyenneclasse = Convert.ToDouble(dataReader["MOYENNECLASSE"]);
                        mention = Convert.ToString(dataReader["MENTION"]);
                        remarque = Convert.ToString(dataReader["remarque"]);
                        decision = Convert.ToString(dataReader["decision"]);
                        APPRECIATION = Convert.ToString(dataReader["APPRECIATION"]);

                        m = new ResultatTrimestrielBE(codeTrimestre, matricule, annee, point, coef, moyenne, rang,
                            moyenneclasse, mention, remarque, decision, APPRECIATION);
                    }

                    dataReader.Close();

                    return m;
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
