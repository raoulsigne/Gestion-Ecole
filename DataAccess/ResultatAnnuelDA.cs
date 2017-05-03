using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Ecole.BusinessEntity;
using MySql.Data.MySqlClient;
using Ecole.ClasseConception;

namespace Ecole.DataAccess
{
    class ResultatAnnuelDA : DA<ResultatAnnuelBE>
    {
        private Connexion con = Connexion.getConnexion();

        //----------------Ajout d'un nouveau Resultat ------------------------------
        public override Boolean ajouter(ResultatAnnuelBE R)
        {
            MySqlTransaction transaction = con.connexion.BeginTransaction();
            try
            {

                // Création d'un commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "INSERT INTO resultatannuel (MATRICULE, ANNEE, POINT, COEF, MOYENNE, RANG, MOYENNECLASSE, MENTION, REMARQUE, DECISION, NEWNIVEAU, APPRECIATION)  VALUES (@mat, @annee, @point, @coef, @moy, @rang, @moyC, @mention,@remarque, @decision, @newNiveau, @APPRECIATION)";

                // utilisation de l'objet voiture passé en paramètre
                
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
                cmd.Parameters.AddWithValue("@newNiveau", R.newNiveau);
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

        public override Boolean supprimer(ResultatAnnuelBE R)
        {
            MySqlTransaction transaction = con.connexion.BeginTransaction();
            try
            {

                // Création d'un commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "DELETE FROM resultatannuel WHERE MATRICULE=@mat AND ANNEE=@annee";

                // utilisation de l'objet voiture passé en paramètre
                
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
        public override Boolean modifier(ResultatAnnuelBE R, ResultatAnnuelBE newR)
        {
            MySqlTransaction transaction = con.connexion.BeginTransaction();
            try
            {

                // Création d'un commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "UPDATE resultatannuel SET ANNEE=@nouveauannee, MATRICULE=@nouveaumat, POINT=@point, COEF=@coef, MOYENNE=@moy, RANG=@rang, MOYENNECLASSE=@moyC, MENTION=@mention, REMARQUE=@remarque, DECISION=@decision, NEWNIVEAU=@newNiveau, APPRECIATION=@APPRECIATION WHERE ANNEE=@ancienannee and CODETRIMESTRE=@anciencodeT and MATRICULE=@ancienmat";

                // utilisation de l'objet voiture passé en paramètre
                cmd.Parameters.AddWithValue("@nouveauannee", newR.annee);
                
                cmd.Parameters.AddWithValue("@nouveaumat", newR.matricule);

                cmd.Parameters.AddWithValue("@ancienannee", R.annee);
               
                cmd.Parameters.AddWithValue("@ancienmat", R.matricule);

                cmd.Parameters.AddWithValue("@point", newR.point);
                cmd.Parameters.AddWithValue("@coef", newR.coef);
                cmd.Parameters.AddWithValue("@moy", newR.moyenne);
                cmd.Parameters.AddWithValue("@rang", newR.rang);
                cmd.Parameters.AddWithValue("@moyC", newR.moyenneclasse);
                cmd.Parameters.AddWithValue("@mention", newR.mention);
                cmd.Parameters.AddWithValue("@remarque", newR.remarque);
                cmd.Parameters.AddWithValue("@decision", newR.decision);
                cmd.Parameters.AddWithValue("@newNiveau", newR.newNiveau);
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

        public Boolean modifier(ResultatAnnuelBE R)
        {
            MySqlTransaction transaction = con.connexion.BeginTransaction();
            try
            {

                // Création d'un commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "UPDATE resultatannuel SET POINT=@point, COEF=@coef, MOYENNE=@moy, RANG=@rang, REMARQUE=@remarque"
                 + " MOYENNECLASSE=@moyC, MENTION=@mention, DECISION=@decision, NEWNIVEAU=@newNiveau, APPRECIATION=@APPRECIATION WHERE MATRICULE=@mat and ANNEE=@annee";

                // utilisation de l'objet voiture passé en paramètre
                
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
                cmd.Parameters.AddWithValue("@newNiveau", R.newNiveau);
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
        public override ResultatAnnuelBE rechercher(ResultatAnnuelBE resultat)
        {
           
            string matricule;
            int annee;
            double point;
            int coef;
            double moyenne;
            int rang;
            double moyenneclasse;
            string mention;
            string remarque;
            string decison;
            int newNiveau;
            string APPRECIATION;

            ResultatAnnuelBE R;

            try
            {
                // ouverture de la connexion
                // this.con.ouvrir();

                // Création d'un commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT * FROM resultatannuel WHERE MATRICULE=@mat and ANNEE=@annee";
                
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
                        decison = Convert.ToString(dataReader["decision"]); 
                        newNiveau = Convert.ToInt16(dataReader["NEWNIVEAU"]);
                        APPRECIATION = Convert.ToString(dataReader["APPRECIATION"]);

                        R = new ResultatAnnuelBE(resultat.matricule, Convert.ToInt32(resultat.annee), point, coef, moyenne, rang,
                            moyenneclasse, mention, remarque, decison, newNiveau, APPRECIATION);
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
        public override List<ResultatAnnuelBE> listerTous()
        {
            List<ResultatAnnuelBE> list = new List<ResultatAnnuelBE>();
            int annee;
           
            string matricule;
            double point;
            int coef;
            double moyenne;
            int rang;
            double moyenneclasse;
            string mention;
            string decision;
            int newNiveau;
            string APPRECIATION;

            ResultatAnnuelBE R;

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT * FROM resultatannuel";

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet acheter à retourner
                    while (dataReader.Read())
                    {
                        annee = Convert.ToInt32(dataReader["annee"]);
                        
                        matricule = Convert.ToString(dataReader["matricule"]);
                        point = Convert.ToDouble(dataReader["POINT"]);
                        coef = Convert.ToInt32(dataReader["coef"]);
                        moyenne = Convert.ToDouble(dataReader["MOYENNE"]);
                        rang = Convert.ToInt32(dataReader["RANG"]);
                        moyenneclasse = Convert.ToDouble(dataReader["MOYENNECLASSE"]);
                        mention = Convert.ToString(dataReader["MENTION"]); 
                        decision = Convert.ToString(dataReader["DECISION"]);
                        newNiveau = Convert.ToInt16(dataReader["NEWNIVEAU"]);
                        APPRECIATION = Convert.ToString(dataReader["APPRECIATION"]);

                        R = new ResultatAnnuelBE(matricule, annee, point, coef, moyenne, rang, moyenneclasse, mention, Convert.ToString(dataReader["remarque"]), decision, newNiveau, APPRECIATION);
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


        public override List<ResultatAnnuelBE> listerSuivantCritere(string critere)
        {
            List<ResultatAnnuelBE> list = new List<ResultatAnnuelBE>();
            int annee;
            
            string matricule;
            double point;
            int coef;
            double moyenne;
            int rang;
            double moyenneclasse;
            string mention;
            string decision;
            int newNiveau;
            string APPRECIATION;

            ResultatAnnuelBE R;

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT * FROM resultatannuel WHERE " + critere;

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet acheter à retourner
                    while (dataReader.Read())
                    {
                        annee = Convert.ToInt32(dataReader["annee"]);
                        
                        matricule = Convert.ToString(dataReader["matricule"]);
                        point = Convert.ToDouble(dataReader["POINT"]);
                        coef = Convert.ToInt32(dataReader["coef"]);
                        moyenne = Convert.ToDouble(dataReader["MOYENNE"]);
                        rang = Convert.ToInt32(dataReader["RANG"]);
                        moyenneclasse = Convert.ToDouble(dataReader["MOYENNECLASSE"]);
                        mention = Convert.ToString(dataReader["MENTION"]);
                        decision = Convert.ToString(dataReader["DECISION"]); 
                        newNiveau = Convert.ToInt16(dataReader["NEWNIVEAU"]);
                        APPRECIATION = Convert.ToString(dataReader["APPRECIATION"]);

                        R = new ResultatAnnuelBE(matricule, annee, point, coef, moyenne, rang, moyenneclasse, mention, Convert.ToString(dataReader["remarque"]), decision, newNiveau, APPRECIATION);
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
                cmd.CommandText = "SELECT * FROM resultatannuel";

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
                cmd.CommandText = "SELECT COUNT(*) FROM resultatannuel";

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

         // Calcul des résultats résultats annuels des élèves
        public List<String[]> calculResultatAnnuels(String codeClasse, int annee)
        {
            string matricule;
            double moyenne;
            double totalPoint;
            int coef;

            // ------------------ DEBUT calcul des moyennes annuels des matières
            List<String[]> LResultat = new List<string[]>();
            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                //cmd.CommandText = "SELECT matricule, annee, SUM(moyenne * coef), SUM(coef), SUM(moyenne)/(SELECT COUNT( distinct codeTrimestre) FROM moyennestrimestriels WHERE annee = '" + annee + "') FROM resultattrimestriel WHERE annee = '" + annee + "' GROUP BY matricule;";
                cmd.CommandText = "SELECT matricule, annee, SUM(moyenne * coef), SUM(coef), AVG(moyenne) FROM resultattrimestriel WHERE annee = '" + annee + "' AND matricule in (SELECT matricule FROM inscrire Where codeclasse = '" + codeClasse + "' AND annee = '" + annee + "') GROUP BY matricule;";

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

                        String[] t = new String[5];
                        t[0] = matricule; // le matricule de l'élève
                        t[1] = Convert.ToString(annee); //l'année
                        t[2] = Convert.ToString(totalPoint); // la somme totale des points de l'élève
                        t[3] = Convert.ToString(moyenne);
                        t[4] = Convert.ToString(coef);

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

        // Calcul des résultats résultats annuels d'un élève
        public String[] calculResultatAnnuelsDunEleve(String matricule, String codeClasse, int annee)
        {
            double moyenne;
            double totalPoint;
            int coef;

            // ------------------ DEBUT calcul des moyennes annuels des matières
            List<String[]> LResultat = new List<string[]>();
            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                //cmd.CommandText = "SELECT matricule, annee, SUM(moyenne * coef), SUM(coef), SUM(moyenne)/(SELECT COUNT( distinct codeTrimestre) FROM moyennestrimestriels WHERE annee = '" + annee + "') FROM resultattrimestriel WHERE annee = '" + annee + "' AND matricule = '"+matricule+"' GROUP BY matricule;";
                cmd.CommandText = "SELECT matricule, annee, SUM(moyenne * coef), SUM(coef), AVG(moyenne) FROM resultattrimestriel WHERE annee = '" + annee + "' AND matricule = '"+matricule+"' GROUP BY matricule;";

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {

                    //fabriquer la voiture à retourner
                    while (dataReader.Read())
                    {
                        moyenne = Convert.ToDouble(dataReader["AVG(moyenne)"]);
                        totalPoint = Convert.ToDouble(dataReader["SUM(moyenne * coef)"]);
                        coef = Convert.ToInt16(dataReader["SUM(coef)"]);

                        String[] t = new String[5];
                        t[0] = matricule; // le matricule de l'élève
                        t[1] = Convert.ToString(annee); //l'année
                        t[2] = Convert.ToString(totalPoint); // la somme totale des points de l'élève
                        t[3] = Convert.ToString(moyenne);
                        t[4] = Convert.ToString(coef);

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

        //liste les résultats des élèves d'une classe
        public List<ResultatAnnuelBE> listerResultatsAnnuelsDesElevesDuneClasse(String codeClasse, int annee)
        {
            List<ResultatAnnuelBE> list = new List<ResultatAnnuelBE>();
            //int annee;

            string matricule;
            double point;
            int coef;
            double moyenne;
            int rang;
            double moyenneclasse;
            string mention;
            string decision;
            int newNiveau;
            string APPRECIATION;

            ResultatAnnuelBE R;

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT * FROM resultatannuel WHERE annee = '" + annee + "' AND matricule in (SELECT matricule FROM inscrire WHERE codeclasse LIKE '" + codeClasse + "' AND annee = '" + annee + "')";

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet acheter à retourner
                    while (dataReader.Read())
                    {
                        //annee = Convert.ToInt32(dataReader["annee"]);

                        matricule = Convert.ToString(dataReader["matricule"]);
                        point = Convert.ToDouble(dataReader["POINT"]);
                        coef = Convert.ToInt32(dataReader["coef"]);
                        moyenne = Convert.ToDouble(dataReader["MOYENNE"]);
                        rang = Convert.ToInt32(dataReader["RANG"]);
                        moyenneclasse = Convert.ToDouble(dataReader["MOYENNECLASSE"]);
                        mention = Convert.ToString(dataReader["MENTION"]);
                        decision = Convert.ToString(dataReader["DECISION"]); 
                        newNiveau = Convert.ToInt16(dataReader["NEWNIVEAU"]);
                        APPRECIATION = Convert.ToString(dataReader["APPRECIATION"]);

                        R = new ResultatAnnuelBE(matricule, annee, point, coef, moyenne, rang, moyenneclasse, mention, Convert.ToString(dataReader["remarque"]), decision, newNiveau, APPRECIATION);
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

        //recherche le dernier résultat annuel d'un élève
        public ResultatAnnuelBE RechercherResultatsAnnuelsDunEleve(String matricule)
        {
            List<ResultatAnnuelBE> list = new List<ResultatAnnuelBE>();
            int annee;

            //string matricule;
            double point;
            int coef;
            double moyenne;
            int rang;
            double moyenneclasse;
            string mention;
            string decision;
            int newNiveau;
            string APPRECIATION;

            ResultatAnnuelBE R;

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT * FROM resultatannuel WHERE matricule = '"+matricule+"' ORDER By annee DESC";

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet acheter à retourner
                    while (dataReader.Read())
                    {
                        annee = Convert.ToInt32(dataReader["annee"]);

                        //matricule = Convert.ToString(dataReader["matricule"]);
                        point = Convert.ToDouble(dataReader["POINT"]);
                        coef = Convert.ToInt32(dataReader["coef"]);
                        moyenne = Convert.ToDouble(dataReader["MOYENNE"]);
                        rang = Convert.ToInt32(dataReader["RANG"]);
                        moyenneclasse = Convert.ToDouble(dataReader["MOYENNECLASSE"]);
                        mention = Convert.ToString(dataReader["MENTION"]);
                        decision = Convert.ToString(dataReader["DECISION"]); 
                        newNiveau = Convert.ToInt16(dataReader["NEWNIVEAU"]);
                        APPRECIATION = Convert.ToString(dataReader["APPRECIATION"]);

                        R = new ResultatAnnuelBE(matricule, annee, point, coef, moyenne, rang, moyenneclasse, mention, Convert.ToString(dataReader["remarque"]), decision, newNiveau, APPRECIATION);
                        list.Add(R);
                    }
                    dataReader.Close();

                    if (list.Count != 0)
                        return list.ElementAt(0);
                    else return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        //génère un objet contenant les infos qui doivent figurer dans le Bulletin Annuel d'un élève
        public List<LigneBulletinAnnuel> genererBulletinAnnuelDunEleve(String matricule, int annee, String codeClasse)
        {
            List<LigneBulletinAnnuel> list = new List<LigneBulletinAnnuel>();
            //String matricule; // le matricule de l'élève
            String codeMat; // le code de la matière
            String nomMat; // le nom de la matière
            String codeprof; // le code de l'enseignant
            String nomProf; // le nom de l'enseignant
            int coef; // le coeficient de la matière
            String codeGroupe; // le code du groupe de la matière
            String nomGroupe; // le nom du groupe de la matière
            String codeSequence; // le code du trimestre
            double moyenneSequence; // la moyenne du Trilestre
            double moyenneAnnuelle; // la moyenne annuelle
            int rangAnnuel; // le rang annuel
            double moyenneClasseAnnuelle; // la moyenne annuelle de le classe
            double moyenneMinClasseAnnuelle; // la moyenne annuelle minimale de la classe
            double moyenneMaxClasseAnnuelle; // la moyenne annuelle de le classe
            String mention; // la mention de la moyenne de l'élève
            double totalPointGroupe; // le total des points du groupe
            double totalPointMaxGroupe; // le total des points Max du groupe        
            double totalCoefGroupe; // la total des coeficient du groupe
            double moyenneGroupe; // la moyenne du groupe de l'élève
            string appreciation;

            LigneBulletinAnnuel ligne;

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT m.matricule, p.codemat, p.nommat, p.codeprof, p.nomprof, p.coef, p.codegroupe, g.nomgroupe, " +
                    "m.codeSeq, m.moyenneSequence, m.moyenneAnnuelle, m.rangAnnuel, m.moyenneclasse, m.moyennemin, m.moyennemax, m.mention, m.appreciation, " +
                    "g.totalPointGroupe, g.totalPointMaxGroupe, g.totalCoefGroupe, g.moyenneGroupe " +
                    "FROM " +
                    "(SELECT m.codemat, m.nommat, p.codeprof, e.nomprof, p.coef, p.codegroupe " +
                    "FROM matiere m, programmer p, enseignant e " +
                    "WHERE p.codeclasse = '" + codeClasse + "' " +
                    "AND p.codemat = m.codemat " +
                    "AND p.codeprof = e.codeprof " +
                    "AND p.annee = '" + annee + "') p, " +
                    "(SELECT ms.matricule, ms.codemat, ms.codeSeq, ms.moyenne as moyenneSequence, ma.moyenne as moyenneAnnuelle, ma.rang as rangAnnuel, ma.moyenneclasse, ma.moyennemin, ma.moyennemax, ma.mention, ma.appreciation as appreciation " +
                    "FROM moyennes ms, moyennesannuelles ma " +
                    "WHERE ms.matricule = ma.matricule " +
                    "AND ms.annee = ma.annee " +
                    "AND ma.annee = '" + annee + "' " +
                    "AND ms.codemat = ma.codemat " +
                    "AND ma.matricule = '" + matricule + "') m, " +
                    "(SELECT ma.matricule, p.codegroupe, g.nomgroupe, SUM(ma.moyenne * p.coef) totalPointGroupe, SUM(20 * p.coef) totalPointMaxGroupe, SUM(coef) totalCoefGroupe, " +
                    "SUM(ma.moyenne * p.coef) / SUM(coef) moyenneGroupe " +
                    "FROM moyennesannuelles ma, programmer p, groupematiere g " +
                    "WHERE p.codegroupe = g.codegroupe " +
                    "AND ma.matricule = '" + matricule + "' " +
                    "AND ma.codemat = p.codemat " +
                    "AND ma.annee = p.annee " +
                    "AND p.codeclasse = '" + codeClasse + "' " +
                    "AND ma.annee = '" + annee + "' " +
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
                        codeSequence = Convert.ToString(dataReader["codeSeq"]); // le code de la séquence
                        moyenneSequence = Convert.ToDouble(dataReader["moyenneSequence"]); // la moyenne de la séquence
                        moyenneAnnuelle = Convert.ToDouble(dataReader["moyenneAnnuelle"]); // le moyenne annuelle
                        rangAnnuel = Convert.ToInt16(dataReader["rangAnnuel"]); // le rang annuel
                        moyenneClasseAnnuelle = Convert.ToDouble(dataReader["moyenneclasse"]); // la moyenne annuelle de le classe
                        moyenneMinClasseAnnuelle = Convert.ToDouble(dataReader["moyennemin"]); // la moyenne annuelle minimale de la classe
                        moyenneMaxClasseAnnuelle = Convert.ToDouble(dataReader["moyennemax"]); // la moyenne annuelle de le classe
                        mention = Convert.ToString(dataReader["mention"]); // la mention de la moyenne de l'élève
                        totalPointGroupe = Convert.ToDouble(dataReader["totalPointGroupe"]); // le total des points du groupe
                        totalPointMaxGroupe = Convert.ToDouble(dataReader["totalPointMaxGroupe"]);// le total des points Max du groupe        
                        totalCoefGroupe = Convert.ToDouble(dataReader["totalCoefGroupe"]); // la total des coeficient du groupe
                        moyenneGroupe = Convert.ToDouble(dataReader["moyenneGroupe"]); // la moyenne du groupe de l'élève
                        appreciation = Convert.ToString(dataReader["appreciation"]);

                        ligne = new LigneBulletinAnnuel(matricule, codeMat, nomMat, codeprof, nomProf, coef, codeGroupe, nomGroupe, codeSequence, moyenneSequence, moyenneAnnuelle, rangAnnuel,
                                    moyenneClasseAnnuelle, moyenneMinClasseAnnuelle, moyenneMaxClasseAnnuelle, mention, totalPointGroupe, totalPointMaxGroupe, totalCoefGroupe, moyenneGroupe, appreciation);

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



        //retourne la moyenne minimale des élèves d'une classe pour une année
        public double getMoynenneAnnuelleMinimaleDuneClasse(String codeClasse, int annee)
        {

            double moyenneMin;


            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT MIN(moyenne) FROM resultatannuel WHERE annee = '" + annee + "' AND matricule in (SELECT matricule FROM inscrire WHERE codeclasse = '" + codeClasse + "' AND annee='" + annee + "')";

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
        public double getMoynenneAnnuelleMaximaleDuneClasse(String codeClasse, int annee)
        {

            double moyenneMax;


            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT MAX(moyenne) FROM resultatannuel WHERE annee = '" + annee + "' AND matricule in (SELECT matricule FROM inscrire WHERE codeclasse = '" + codeClasse + "' AND annee='" + annee + "')";

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

        internal bool enregistrerRemarque(string matricule, int annee, string remarque)
        {
            MySqlTransaction transaction = con.connexion.BeginTransaction();
            ResultatAnnuelBE resultat = new ResultatAnnuelBE ();
            try
            {

                // Création d'un commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();
                resultat.matricule = matricule;
                resultat.annee = annee;
                resultat = rechercher(resultat);
                if(resultat == null)
                    cmd.CommandText = "INSERT INTO resultatannuel (MATRICULE, ANNEE, REMARQUE) VALUES (@mat, @annee, @remarque)";
                else
                    cmd.CommandText = "UPDATE resultatannuel SET REMARQUE=@remarque WHERE MATRICULE=@mat and ANNEE=@annee";

                // utilisation de l'objet voiture passé en paramètre

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

        public double obtenirMoyenneAnnuelleDuneClasse(string codeclasse, int annee)
        {
            try
            {
                MySqlCommand cmd = con.connexion.CreateCommand();

                cmd.CommandText = "SELECT distinct moyenneclasse FROM resultatannuel r, inscrire i where i.matricule=r.matricule and i.codeclasse = '" + codeclasse + "' and i.annee='" + annee + "' and r.annee=i.annee;";

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

        public ResultatAnnuelBE resultatAnnuelDunEleve(String matricule, int annee)
        {
            double point;
            int coef;
            double moyenne;
            int rang;
            double moyenneclasse;
            string mention;
            string decision;
            int newNiveau;
            string APPRECIATION;

            ResultatAnnuelBE R = new ResultatAnnuelBE();

            try
            {
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT * FROM resultatannuel WHERE matricule = '" + matricule + "' AND annee='" + annee + "' ORDER By annee DESC";

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
                        decision = Convert.ToString(dataReader["DECISION"]);
                        newNiveau = Convert.ToInt16(dataReader["NEWNIVEAU"]);
                        APPRECIATION = Convert.ToString(dataReader["APPRECIATION"]);

                        R = new ResultatAnnuelBE(matricule, annee, point, coef, moyenne, rang, moyenneclasse, mention, Convert.ToString(dataReader["remarque"]), decision, newNiveau, APPRECIATION);
                    }
                    dataReader.Close();

                    return R;
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
