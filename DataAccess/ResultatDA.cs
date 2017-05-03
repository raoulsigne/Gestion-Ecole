using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Ecole.BusinessEntity;
using MySql.Data.MySqlClient;
using Ecole.ClasseConception;

namespace Ecole.DataAccess
{
    public class ResultatDA : DA<ResultatBE>
    {
        private Connexion con = Connexion.getConnexion();

        //----------------Ajout d'un nouveau Resultat ------------------------------
        public override Boolean ajouter(ResultatBE R)
        {
            MySqlTransaction transaction = con.connexion.BeginTransaction();
            try
            {

                // Création d'un commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "INSERT INTO resultat (CODESEQ, MATRICULE, ANNEE, POINT, COEF, MOYENNE, RANG, MOYENNECLASSE, MENTION, REMARQUE, DECISION, APPRECIATION)  VALUES (@codeS, @mat, @annee, @point, @coef, @moy, @rang, @moyC, @mention,@remarque, @decision, @APPRECIATION)";

                // utilisation de l'objet voiture passé en paramètre
                cmd.Parameters.AddWithValue("@codeS", R.codeseq);
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

        public override Boolean supprimer(ResultatBE R)
        {
            MySqlTransaction transaction = con.connexion.BeginTransaction();
            try
            {

                // Création d'un commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "DELETE FROM resultat WHERE CODESEQ=@codeS AND MATRICULE=@mat AND ANNEE=@annee";

                // utilisation de l'objet voiture passé en paramètre
                cmd.Parameters.AddWithValue("@codeS", R.codeseq);
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
        public override Boolean modifier(ResultatBE R, ResultatBE newR)
        {
            MySqlTransaction transaction = con.connexion.BeginTransaction();
            try
            {

                // Création d'un commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "UPDATE resultat SET ANNEE=@nouveauannee, CODESEQ=@nouveaucodeS, MATRICULE=@nouveaumat, POINT=@point, COEF=@coef, MOYENNE=@moy, RANG=@rang, MOYENNECLASSE=@moyC, MENTION=@mention, REMARQUE=@remarque, DECISION=@decision, APPRECIATION=@APPRECIATION WHERE ANNEE=@ancienannee and CODESEQ=@anciencodeS and MATRICULE=@ancienmat";

                // utilisation de l'objet voiture passé en paramètre
                cmd.Parameters.AddWithValue("@nouveauannee", newR.annee);
                cmd.Parameters.AddWithValue("@nouveaucodeS", newR.codeseq);
                cmd.Parameters.AddWithValue("@nouveaumat", newR.matricule);

                cmd.Parameters.AddWithValue("@ancienannee", R.annee);
                cmd.Parameters.AddWithValue("@anciencodeS", R.codeseq);
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

        public Boolean modifier(ResultatBE R)
        {
            MySqlTransaction transaction = con.connexion.BeginTransaction();
            try
            {

                // Création d'un commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "UPDATE resultat SET POINT=@point, COEF=@coef, MOYENNE=@moy, RANG=@rang, REMARQUE=@remarque"
                 + " MOYENNECLASSE=@moyC, MENTION=@mention, DECISION=@decision, APPRECIATION=@APPRECIATION WHERE CODESEQ=@codeS and MATRICULE=@mat and ANNEE=@annee";

                // utilisation de l'objet voiture passé en paramètre
                cmd.Parameters.AddWithValue("@codeS", R.codeseq);
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
        public override ResultatBE rechercher(ResultatBE resultat)
        {
            string codeseq;
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

            ResultatBE R;

            try
            {
                // ouverture de la connexion
                // this.con.ouvrir();

                // Création d'un commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT * FROM resultat WHERE CODESEQ=@codeS and MATRICULE=@mat and ANNEE=@annee";
                cmd.Parameters.AddWithValue("@codeS", resultat.codeseq);
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

                        R = new ResultatBE(resultat.codeseq, resultat.matricule, Convert.ToInt32(resultat.annee), point, coef, moyenne, rang,
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
        public override List<ResultatBE> listerTous()
        {
            List<ResultatBE> list = new List<ResultatBE>();
            int annee;
            string codeseq;
            string matricule;
            double point;
            int coef;
            double moyenne;
            int rang;
            double moyenneclasse;
            string mention;
            string decision;
            string APPRECIATION;

            ResultatBE R;

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT * FROM Resultat";

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet acheter à retourner
                    while (dataReader.Read())
                    {
                        annee = Convert.ToInt32(dataReader["annee"]);
                        codeseq = Convert.ToString(dataReader["codeseq"]);
                        matricule = Convert.ToString(dataReader["matricule"]);
                        point = Convert.ToDouble(dataReader["POINT"]);
                        coef = Convert.ToInt32(dataReader["coef"]);
                        moyenne = Convert.ToDouble(dataReader["MOYENNE"]);
                        rang = Convert.ToInt32(dataReader["RANG"]);
                        moyenneclasse = Convert.ToDouble(dataReader["MOYENNECLASSE"]);
                        mention = Convert.ToString(dataReader["MENTION"]);
                        decision = Convert.ToString(dataReader["DECISION"]);
                        APPRECIATION = Convert.ToString(dataReader["APPRECIATION"]);
                        R = new ResultatBE(codeseq, matricule, annee, point, coef, moyenne, rang, moyenneclasse, mention, Convert.ToString(dataReader["remarque"]), decision, APPRECIATION);
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


        public override List<ResultatBE> listerSuivantCritere(string critere)
        {
            List<ResultatBE> list = new List<ResultatBE>();
            int annee;
            string codeseq;
            string matricule;
            double point;
            int coef;
            double moyenne;
            int rang;
            double moyenneclasse;
            string mention;
            string decision;
            string APPRECIATION;

            ResultatBE R;

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT * FROM Resultat WHERE " + critere;

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet acheter à retourner
                    while (dataReader.Read())
                    {
                        annee = Convert.ToInt32(dataReader["annee"]);
                        codeseq = Convert.ToString(dataReader["CODESEQ"]);
                        matricule = Convert.ToString(dataReader["matricule"]);
                        point = Convert.ToDouble(dataReader["POINT"]);
                        coef = Convert.ToInt32(dataReader["coef"]);
                        moyenne = Convert.ToDouble(dataReader["MOYENNE"]);
                        rang = Convert.ToInt32(dataReader["RANG"]);
                        moyenneclasse = Convert.ToDouble(dataReader["MOYENNECLASSE"]);
                        mention = Convert.ToString(dataReader["MENTION"]);
                        decision = Convert.ToString(dataReader["DECISION"]);
                        APPRECIATION = Convert.ToString(dataReader["APPRECIATION"]);

                        R = new ResultatBE(codeseq, matricule, annee, point, coef, moyenne, rang, moyenneclasse, mention, Convert.ToString(dataReader["remarque"]), decision, APPRECIATION);
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
                cmd.CommandText = "SELECT * FROM Resultat";

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
                cmd.CommandText = "SELECT COUNT(*) FROM Resultat";

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


        // Calcul des résultats Sequentiel des élèves
        public List<String[]> calculResultatSequentiel(String codeClasse, String codeSequence, int annee)
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
                cmd.CommandText = "SELECT matricule, codeSeq, SUM(moyenne * coef), SUM(coef), SUM(moyenne * coef)/SUM(coef), annee FROM (SELECT m.matricule, m.codemat, c.coef, m.codeseq, m.moyenne, m.annee FROM moyennes m, (SELECT codemat, coef, annee FROM programmer WHERE codeclasse = '" + codeClasse + "' AND annee = '" + annee + "') c WHERE m.codemat = c.codemat AND m.annee = c.annee AND m.annee = '" + annee + "' AND m.codeseq = '" + codeSequence + "') mt WHERE matricule in (SELECT matricule FROM inscrire Where codeclasse = '" + codeClasse + "' AND annee = '" + annee + "') GROUP BY matricule";

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {

                    //fabriquer la voiture à retourner
                    while (dataReader.Read())
                    {
                        matricule = Convert.ToString(dataReader["matricule"]);
                        moyenne = Convert.ToDouble(dataReader["SUM(moyenne * coef)/SUM(coef)"]);
                        totalPoint = Convert.ToDouble(dataReader["SUM(moyenne * coef)"]);
                        coef = Convert.ToInt16(dataReader["SUM(coef)"]);

                        String[] t = new String[6];
                        t[0] = matricule; // le matricule de l'élève
                        t[1] = codeSequence; // le code de la séquence
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


        // Calcul des résultats Sequentiel d'un élève particulier dans une classe donnée
        public String[] calculResultatSequentielDunEleve(String matricule, String codeClasse, String codeSequence, int annee)
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
                cmd.CommandText = "SELECT matricule, codeSeq, SUM(moyenne * coef), SUM(coef), SUM(moyenne * coef)/SUM(coef), annee FROM (SELECT m.matricule, m.codemat, c.coef, m.codeseq, m.moyenne, m.annee FROM moyennes m, (SELECT codemat, coef, annee FROM programmer WHERE codeclasse = '" + codeClasse + "' AND annee = '" + annee + "') c WHERE m.codemat = c.codemat AND m.annee = c.annee AND m.annee = '" + annee + "' AND m.codeseq = '" + codeSequence + "') mt WHERE matricule = '" + matricule + "' GROUP BY matricule";

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {

                    //fabriquer la voiture à retourner
                    while (dataReader.Read())
                    {
                        moyenne = Convert.ToDouble(dataReader["SUM(moyenne * coef)/SUM(coef)"]);
                        totalPoint = Convert.ToDouble(dataReader["SUM(moyenne * coef)"]);
                        coef = Convert.ToInt16(dataReader["SUM(coef)"]);

                        String[] t = new String[6];
                        t[0] = matricule; // le matricule de l'élève
                        t[1] = codeSequence; // le code de la séquence
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

        //recherche les résultats d'un élève pour une séquence et une année donnée
        public List<ResultatBE> resultatsSequentiellesEleve(String matricule, int annee, String codeSequence)
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

            List<ResultatBE> LResultat = new List<ResultatBE>();

            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT * FROM resultat WHERE codeseq='" + codeSequence + "' AND matricule='" + matricule + "' AND annee='" + annee + "'";
                //cmd.Parameters.AddWithValue("@codeSequence", codeSequence);
                //cmd.Parameters.AddWithValue("@matricule", matricule);
                //cmd.Parameters.AddWithValue("@annee", annee);

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer la voiture à retourner
                    while (dataReader.Read())
                    {
                        ResultatBE m;
                        point = Convert.ToDouble(dataReader["POINT"]);
                        coef = Convert.ToInt32(dataReader["coef"]);
                        moyenne = Convert.ToDouble(dataReader["MOYENNE"]);
                        rang = Convert.ToInt32(dataReader["RANG"]);
                        moyenneclasse = Convert.ToDouble(dataReader["MOYENNECLASSE"]);
                        mention = Convert.ToString(dataReader["MENTION"]);
                        remarque = Convert.ToString(dataReader["remarque"]);
                        decision = Convert.ToString(dataReader["decision"]);
                        APPRECIATION = Convert.ToString(dataReader["APPRECIATION"]);

                        m = new ResultatBE(codeSequence, matricule, annee, point, coef, moyenne, rang,
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

        //liste les résultat d'une séquence des élèves d'une classe à une année
        public List<ResultatBE> listerResultatsSequentielsDesElevesDuneClasse(String codeClasse, String codeSequence, int annee)
        {
            List<ResultatBE> list = new List<ResultatBE>();
            //int annee;
            string codeseq;
            string matricule;
            double point;
            int coef;
            double moyenne;
            int rang;
            double moyenneclasse;
            string mention;
            string decision;
            string APPRECIATION;

            ResultatBE R;

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT * FROM Resultat WHERE annee = '" + annee + "' AND matricule in (SELECT matricule FROM inscrire WHERE codeclasse LIKE '" + codeClasse + "' AND annee = '" + annee + "') AND codeSeq LIKE '" + codeSequence + "'";

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet acheter à retourner
                    while (dataReader.Read())
                    {
                        //annee = Convert.ToInt32(dataReader["annee"]);
                        codeseq = Convert.ToString(dataReader["CODESEQ"]);
                        matricule = Convert.ToString(dataReader["matricule"]);
                        point = Convert.ToDouble(dataReader["POINT"]);
                        coef = Convert.ToInt32(dataReader["coef"]);
                        moyenne = Convert.ToDouble(dataReader["MOYENNE"]);
                        rang = Convert.ToInt32(dataReader["RANG"]);
                        moyenneclasse = Convert.ToDouble(dataReader["MOYENNECLASSE"]);
                        mention = Convert.ToString(dataReader["MENTION"]);
                        decision = Convert.ToString(dataReader["DECISION"]);
                        APPRECIATION = Convert.ToString(dataReader["APPRECIATION"]);

                        R = new ResultatBE(codeseq, matricule, annee, point, coef, moyenne, rang, moyenneclasse, mention, Convert.ToString(dataReader["remarque"]), decision, APPRECIATION);
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


        internal bool enregistrerRemarque(string matricule, int annee, string sequence, string remarque)
        {
            MySqlTransaction transaction = con.connexion.BeginTransaction();
            ResultatBE resultat = new ResultatBE();
            try
            {

                // Création d'un commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                resultat.matricule = matricule;
                resultat.annee = annee;
                resultat.codeseq = sequence;
                resultat = rechercher(resultat);
                if (resultat == null)
                    cmd.CommandText = "INSERT INTO resultat (MATRICULE, ANNEE, CODESEQ, REMARQUE) VALUES (@mat, @annee, @codeS, @remarque)";
                else
                    cmd.CommandText = "UPDATE resultat SET REMARQUE=@remarque WHERE CODESEQ=@codeS and MATRICULE=@mat and ANNEE=@annee";

                // utilisation de l'objet voiture passé en paramètre
                cmd.Parameters.AddWithValue("@codeS", sequence);
                cmd.Parameters.AddWithValue("@mat", matricule);
                cmd.Parameters.AddWithValue("@annee", annee);
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

        //génère un objet contenant les infos qui doivent figurer dans le Bulletin Séquentiel d'un élève
        public List<LigneBulletinSequentiel> genererBulletinSequentielDunEleve(String matricule, int annee, String codeClasse, String codeSequence)
        {
            List<LigneBulletinSequentiel> list = new List<LigneBulletinSequentiel>();
            //String matricule; // le matricule de l'élève
            String codeMat; // le code de la matière
            String nomMat; // le nom de la matière
            String codeprof; // le code de l'enseignant
            String nomProf; // le nom de l'enseignant
            int coef; // le coeficient de la matière
            String codeGroupe; // le code du groupe de la matière
            String nomGroupe; // le nom du groupe de la matière
            String codeSeq; // le code de la séquence

            String codeEvaluation; //le code de l'évaluation
            String noteEvaluation; // la moyenne de l'évaluation
            double moyenneSequence; // la moyenne de la séquence
            int rangSequentiel;  // le rang séquentiel
            double moyenneSeqClasse; // la moyenne séquentielle de le classe
            double moyenneSeqMin; // la moyenne séquentielle minimale de le classe
            double moyenneSeqMax; // la moyenne séquentielle maximale de le classe

            String mention; // la mention de la moyenne de l'élève
            double totalPointGroupe; // le total des points du groupe
            double totalPointMaxGroupe; // le total des points Max du groupe        
            double totalCoefGroupe; // la total des coeficient du groupe
            double moyenneGroupe; // la moyenne du groupe de l'élève
            String appreciation;

            LigneBulletinSequentiel ligne;

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT m.matricule, p.codemat, p.nommat, p.codeprof, p.nomprof, p.coef, p.codegroupe, g.nomgroupe, " +
                    "m.codeSeq, m.codeevaluation, m.noteEvaluation, m.moyenneSequence, m.rangSequentiel, m.moyenneclasse, m.moyennemin, m.moyennemax, m.mention, m.appreciation, " +
                    "g.totalPointGroupe, g.totalPointMaxGroupe, g.totalCoefGroupe, g.moyenneGroupe " +
                    "FROM " +
                    "(SELECT m.codemat, m.nommat, p.codeprof, e.nomprof, p.coef, p.codegroupe " +
                    "FROM matiere m, programmer p, enseignant e " +
                    "WHERE p.codeclasse = '" + codeClasse + "' " +
                    "AND p.codemat = m.codemat " +
                    "AND p.codeprof = e.codeprof " +
                    "AND p.annee = '" + annee + "') p, " +
                    "(SELECT n.matricule, n.codemat, n.codeSeq, n.codeevaluation, n.note as noteEvaluation, ms.moyenne as moyenneSequence, ms.rang as rangSequentiel, ms.moyenneclasse, ms.moyennemin, ms.moyennemax, ms.mention, ms.appreciation as appreciation " +
                    "FROM notes n, moyennes ms " +
                    "WHERE n.matricule = ms.matricule " +
                    "AND n.codeSeq = ms.codeSeq " +
                    "AND n.codeSeq = '" + codeSequence + "' " +
                    "AND n.codeevaluation in (SELECT codeEvaluation FROM evaluer WHERE codemat = n.codemat AND codeClasse = '" + codeClasse + "' AND codeSeq = '" + codeSequence + "' AND annee = '" + annee + "') " +
                    "AND n.annee = ms.annee " +
                    "AND n.annee = '" + annee + "' " +
                    "AND n.codemat = ms.codemat " +
                    "AND ms.matricule = '" + matricule + "') m, " +
                    "(SELECT ms.matricule, p.codegroupe, g.nomgroupe, SUM(ms.moyenne * p.coef) totalPointGroupe, SUM(20 * p.coef) totalPointMaxGroupe, SUM(coef) totalCoefGroupe, " +
                    "SUM(ms.moyenne * p.coef) / SUM(coef) moyenneGroupe " +
                    "FROM moyennes ms, programmer p, groupematiere g " +
                    "WHERE p.codegroupe = g.codegroupe " +
                    "AND ms.matricule = '" + matricule + "' " +
                    "AND ms.codemat = p.codemat " +
                    "AND ms.annee = p.annee " +
                    "AND p.codeclasse = '" + codeClasse + "' " +
                    "AND ms.annee = '" + annee + "' " +
                    "AND ms.codeSeq = '" + codeSequence + "' " +
                    "GROUP BY p.codegroupe) g " +
                    "WHERE p.codemat = m.codemat " +
                    "AND p.codegroupe = g.codegroupe " +
                    "ORDER BY p.codegroupe, p.codemat, m.codeevaluation";

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

                        codeEvaluation = Convert.ToString(dataReader["codeevaluation"]);//le code de l'évaluation
                        try
                        {
                            noteEvaluation = Convert.ToString(dataReader["noteEvaluation"]);// la note de l'évaluation
                        }
                        catch (Exception)
                        {
                            noteEvaluation = null;
                        }
                        moyenneSequence = Convert.ToDouble(dataReader["moyenneSequence"]); // la moyenne de la séquence
                        rangSequentiel = Convert.ToInt16(dataReader["rangSequentiel"]);  // le rang séquentiel
                        moyenneSeqClasse = Convert.ToDouble(dataReader["moyenneclasse"]); // la moyenne séquentielle de le classe
                        moyenneSeqMin = Convert.ToDouble(dataReader["moyennemin"]); // la moyenne séquentielle minimale de le classe
                        moyenneSeqMax = Convert.ToDouble(dataReader["moyennemax"]); // la moyenne séquentielle maximale de le classe

                        mention = Convert.ToString(dataReader["mention"]); // la mention de la moyenne de l'élève
                        totalPointGroupe = Convert.ToDouble(dataReader["totalPointGroupe"]); // le total des points du groupe
                        totalPointMaxGroupe = Convert.ToDouble(dataReader["totalPointMaxGroupe"]);// le total des points Max du groupe        
                        totalCoefGroupe = Convert.ToDouble(dataReader["totalCoefGroupe"]); // la total des coeficient du groupe
                        moyenneGroupe = Convert.ToDouble(dataReader["moyenneGroupe"]); // la moyenne du groupe de l'élève
                        appreciation = Convert.ToString(dataReader["appreciation"]);

                        ligne = new LigneBulletinSequentiel(matricule, codeMat, nomMat, codeprof, nomProf, coef, codeGroupe, nomGroupe, codeSeq, codeEvaluation, noteEvaluation, moyenneSequence, rangSequentiel,
                                    moyenneSeqClasse, moyenneSeqMin, moyenneSeqMax, mention, totalPointGroupe, totalPointMaxGroupe, totalCoefGroupe, moyenneGroupe, appreciation);

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

        //retourne la moyenne minimale des élèves d'une classe pour une séquence et une année
        public double getMoynenneSequentielleMinimaleDuneClasse(String codeClasse, String codeSequence, int annee)
        {

            double moyenneMin;


            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT MIN(moyenne) FROM resultat WHERE annee = '" + annee + "' AND codeSeq = '" + codeSequence + "' AND matricule in (SELECT matricule FROM inscrire WHERE codeclasse = '" + codeClasse + "' AND annee='" + annee + "')";

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


        //retourne la moyenne maximale des élèves d'une classe pour une séquence et une année
        public double getMoynenneSequentielleMaximaleDuneClasse(String codeClasse, String codeSequence, int annee)
        {

            double moyenneMax;


            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT MAX(moyenne) FROM resultat WHERE annee = '" + annee + "' AND codeSeq = '" + codeSequence + "' AND matricule in (SELECT matricule FROM inscrire WHERE codeclasse = '" + codeClasse + "' AND annee='" + annee + "')";

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

        public double obtenirMoyenneSequentielleDuneClasse(string codeclasse, string codeseq, int annee)
        {
            try
            {
                MySqlCommand cmd = con.connexion.CreateCommand();

                cmd.CommandText = "SELECT distinct moyenneclasse FROM resultat r, inscrire i where i.matricule=r.matricule and i.codeclasse = '" + codeclasse + "' and i.annee='" + annee + "' and r.codeseq='" + codeseq + "' and  r.annee=i.annee;";

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

        //recherche les résultats d'un élève pour une séquence et une année donnée
        public ResultatBE resultatSequentielleEleve(String matricule, int annee, String codeSequence)
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

                cmd.CommandText = "SELECT * FROM resultat WHERE codeseq='" + codeSequence + "' AND matricule='" + matricule + "' AND annee='" + annee + "'";
                ResultatBE m = new ResultatBE();
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

                        m = new ResultatBE(codeSequence, matricule, annee, point, coef, moyenne, rang,
                            moyenneclasse, mention, remarque, decision, APPRECIATION);
                    }

                    dataReader.Close();
                }
                return m;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public Dictionary<List<string>, List<double>> listeResultatsSequentielEleve(string matricule, int annee)
        {
            Dictionary<List<string>, List<double>> moyennesSequentielles = new Dictionary<List<string>, List<double>>();
            List<string> keys = new List<string>();
            List<double> values = new List<double>();

            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT CODESEQ, MOYENNE FROM resultat WHERE matricule='" + matricule + "' AND annee='" + annee + "'";

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        keys.Add(Convert.ToString(dataReader["CODESEQ"]));
                        values.Add(Convert.ToDouble(dataReader["MOYENNE"]));
                    }

                    dataReader.Close();
                    moyennesSequentielles[keys] = values;

                    return moyennesSequentielles;
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
