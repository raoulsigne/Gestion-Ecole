using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MySql.Data.MySqlClient;

using Ecole.BusinessEntity;
using Ecole.ClasseConception;

namespace Ecole.DataAccess
{
    public class MoyennesDA : DA<MoyennesBE>
    {
        private Connexion con = Connexion.getConnexion();

        //************************************ création d'objet, parametre obj, retourne booléen
        public override Boolean ajouter(MoyennesBE obj) {
            MySqlTransaction tx = con.connexion.BeginTransaction();
            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "INSERT INTO moyennes (codemat,codeseq,matricule,moyenne,annee,rang, moyenneclasse, mention, moyennemin, moyennemax, appreciation) VALUES (@codeMatricule, @codeSequence, @matricule, @moyenne, @annee,@rang, @moyenneclasse, @mention, @moyennemin, @moyennemax, @appreciation)";

                // utilisation de l'objet GroupePrivilegeBE passé en paramètre
                cmd.Parameters.AddWithValue("@codeMatricule", obj.codeMat);
                cmd.Parameters.AddWithValue("@codeSequence", obj.codeSeq);
                cmd.Parameters.AddWithValue("@matricule", obj.matricule);
                cmd.Parameters.AddWithValue("@moyenne", obj.moyenne);
                cmd.Parameters.AddWithValue("@annee", obj.annee);
                cmd.Parameters.AddWithValue("@rang", obj.rang);

                cmd.Parameters.AddWithValue("@moyenneclasse", obj.moyenneClasse);
                cmd.Parameters.AddWithValue("@mention", obj.mention);
                cmd.Parameters.AddWithValue("@moyennemin", obj.moyenneMin);
                cmd.Parameters.AddWithValue("@moyennemax", obj.moyenneMax);
                cmd.Parameters.AddWithValue("@appreciation", obj.appreciation);

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
                tx.Rollback();
                return false;
            }
        }
        //************************************ FIN création d'objet, parametre obj, retourne booléen
        
        //************************************ suppression d'objet, parametre obj, retourne booléen
        public override Boolean supprimer(MoyennesBE obj) {
            MySqlTransaction tx = con.connexion.BeginTransaction();
            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "DELETE FROM moyennes WHERE codemat=@codeMatricule AND codeseq=@codeSequence AND matricule=@matricule AND annee=@annee";

                // utilisation de l'objet GroupePrivilegeBE passé en paramètre
                cmd.Parameters.AddWithValue("@codeMatricule", obj.codeMat);
                cmd.Parameters.AddWithValue("@codeSequence", obj.codeSeq);
                cmd.Parameters.AddWithValue("@matricule", obj.matricule);
                cmd.Parameters.AddWithValue("@annee", obj.annee);

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
                tx.Rollback();
                return false;
            }
        }
        //************************************ FIN suppression d'objet, parametre obj, retourne booléen
        
        //*************************** mise à jour d'objet, parametre obj, retourne booléen
        public override Boolean modifier(MoyennesBE obj, MoyennesBE newobj) {
            supprimer(obj);
            ajouter(newobj);
            return true;
        }

        public Boolean modifier(MoyennesBE obj)
        {
            MySqlTransaction tx = con.connexion.BeginTransaction();
            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "UPDATE moyennes SET moyenne=@moyenne, rang=@rang, moyenneclasse=@moyenneclasse, mention=@mention, moyennemin=@moyennemin, moyennemax=@moyennemax, appreciation=@appreciation WHERE codemat=@codeMatricule AND codeseq=@codeSequence AND matricule=@matricule AND annee=@annee";

                // utilisation de l'objet GroupePrivilegeBE passé en paramètre
                cmd.Parameters.AddWithValue("@codeMatricule", obj.codeMat);
                cmd.Parameters.AddWithValue("@codeSequence", obj.codeSeq);
                cmd.Parameters.AddWithValue("@matricule", obj.matricule);
                cmd.Parameters.AddWithValue("@moyenne", obj.moyenne);
                cmd.Parameters.AddWithValue("@annee", obj.annee);
                cmd.Parameters.AddWithValue("@rang", obj.rang);

                cmd.Parameters.AddWithValue("@moyenneclasse", obj.moyenneClasse);
                cmd.Parameters.AddWithValue("@mention", obj.mention);
                cmd.Parameters.AddWithValue("@moyennemin", obj.moyenneMin);
                cmd.Parameters.AddWithValue("@moyennemax", obj.moyenneMax);
                cmd.Parameters.AddWithValue("@appreciation", obj.appreciation);


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
                tx.Rollback();
                return false;
            }
        }
        //*************************** FIN mise à jour d'objet, parametre obj, retourne booléen
        
        public override MoyennesBE rechercher(MoyennesBE moyenn) {
            string codeMatiere;
            string codeSequence;
            string matricule;
            double moyenne;
            int annee;
            double moyenneclasse;
            string mention;
            double moyennemin;
            double moyennemax;
            string appreciation;

            MoyennesBE m;
            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT * FROM moyennes WHERE codemat=@codeMatiere AND codeseq=@codeSequence AND matricule=@matricule AND annee=@annee";
                cmd.Parameters.AddWithValue("@codeMatiere", moyenn.codeMat);
                cmd.Parameters.AddWithValue("@codeSequence", moyenn.codeSeq);
                cmd.Parameters.AddWithValue("@matricule", moyenn.matricule);
                cmd.Parameters.AddWithValue("@annee", moyenn.annee);

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer la voiture à retourner
                    if (dataReader.Read())
                    {
                        codeMatiere = Convert.ToString(dataReader["codemat"]);
                        codeSequence = Convert.ToString(dataReader["codeseq"]);
                        matricule = Convert.ToString(dataReader["matricule"]);
                        moyenne = Convert.ToDouble(dataReader["moyenne"]);
                        annee = Convert.ToInt16(dataReader["annee"]);

                        moyenneclasse = Convert.ToDouble(dataReader["moyenneclasse"]);
                        mention = Convert.ToString(dataReader["mention"]);
                        moyennemin = Convert.ToDouble(dataReader["moyennemin"]);
                        moyennemax = Convert.ToInt16(dataReader["moyennemax"]);

                        appreciation = Convert.ToString(dataReader["appreciation"]);

                        m = new MoyennesBE(codeMatiere, codeSequence, matricule, moyenne, annee, Convert.ToInt32(dataReader["rang"]), moyenneclasse, mention, moyennemin, moyennemax, appreciation);
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
        public override List<MoyennesBE> listerTous() {
            try
            {
                List<MoyennesBE> listMoyBE = new List<MoyennesBE>();

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT * FROM moyennes";

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //Read the data and store them in the list
                    while (dataReader.Read())
                    {
                        MoyennesBE moyBE = new MoyennesBE(Convert.ToString(dataReader["codemat"]), Convert.ToString(dataReader["codeseq"]), Convert.ToString(dataReader["matricule"]),
                            Convert.ToDouble(dataReader["moyenne"]), Convert.ToInt16(dataReader["annee"]), Convert.ToInt32(dataReader["rang"]), Convert.ToDouble(dataReader["moyenneclasse"]),
                            Convert.ToString(dataReader["mention"]), Convert.ToDouble(dataReader["moyennemin"]), Convert.ToDouble(dataReader["moyennemax"]), Convert.ToString(dataReader["appreciation"]));
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
        public override List<MoyennesBE> listerSuivantCritere(String critere)
        {
            try
            {
                List<MoyennesBE> listobjBE = new List<MoyennesBE>();

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT * FROM moyennes WHERE " + critere;

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //Read the data and store them in the list
                    while (dataReader.Read())
                    {
                        MoyennesBE objBE = new MoyennesBE(Convert.ToString(dataReader["codemat"]), Convert.ToString(dataReader["codeseq"]), Convert.ToString(dataReader["matricule"]),
                            Convert.ToDouble(dataReader["moyenne"]), Convert.ToInt16(dataReader["annee"]), Convert.ToInt32(dataReader["rang"]), Convert.ToDouble(dataReader["moyenneclasse"]),
                            Convert.ToString(dataReader["mention"]), Convert.ToDouble(dataReader["moyennemin"]), Convert.ToDouble(dataReader["moyennemax"]), Convert.ToString(dataReader["appreciation"]));
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
                cmd.CommandText = "SELECT * FROM moyennes";

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
                cmd.CommandText = "SELECT COUNT(*) FROM moyennes";

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


        // Calcul de la moyenne séquentielle des élèves (pour l'ensemble des élèves d'une classe donné)
        public List<String[]> calculMoyenneSequentielle(String codeClasse, String codeMatiere, String codeSequence, int annee)
        {
            string codemat;
            string codeseq;
            string matricule;
            double moyenne;

            List<String[]> Lmoyenne = new List<string[]>();
            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT matricule, codeMat, codeSeq, if(SUM(note * poids/totalpoids) is null, 0 , SUM(note * poids/totalpoids)) as sum_note " +
                                    "FROM " +
                                    "(SELECT P1.matricule, P1.codeMat, P1.codeSeq, P1.annee, P1.codeevaluation, if(P1.note is null, 0 ,P1.poids) poids, " +
                                     "p2.totalpoids, P1.note " +
                                            "FROM " +
                                            "(SELECT n.matricule, n.codeMat, n.codeSeq, n.annee, e.codeevaluation, if(n.note is null, 0 ,e.poids) poids, n.note " +
                                                "FROM notes n, (SELECT codeMat, codeevaluation, poids, codeSeq, codeClasse, annee " +
                                    "FROM evaluer " +
                                    "WHERE codeClasse = '" + codeClasse + "' AND codeSeq = '" + codeSequence + "' AND annee = '" + annee + "') e " +
                                    "WHERE e.annee = n.annee AND e.codeMat = n.codeMat AND e.codeSeq = n.codeSeq AND n.codeevaluation = e.codeevaluation " +
                                    "AND n.matricule in (SELECT matricule FROM inscrire WHERE codeClasse = '" + codeClasse + "' AND annee = '" + annee + "')) p1, " +
                                    "(SELECT n.matricule, n.codeMat, n.codeSeq, n.annee, if(n.note is null, 0, e.poids) poids, " +
                                    "SUM(if(n.note is null,0,e.poids)) totalpoids,  n.note " +
                                    "FROM notes n, (SELECT codeMat, codeevaluation, poids, codeSeq, codeClasse, annee " +
                                    "FROM evaluer " +
                                    "WHERE codeClasse = '" + codeClasse + "' AND codeSeq = '" + codeSequence + "' AND annee = '" + annee + "') e " +
                                    "WHERE e.annee = n.annee AND e.codeMat = n.codeMat AND e.codeSeq = n.codeSeq AND n.codeevaluation = e.codeevaluation " +
                                    "AND n.matricule in (SELECT matricule FROM inscrire WHERE codeClasse = '" + codeClasse + "' AND annee = '" + annee + "') " +
                                    "GROUP BY n.codemat,n.matricule ) p2 " +
                                     "WHERE p1.matricule = p2.matricule " +
                                     "AND p1.codemat = p2.codemat " +
                                     "AND p1.codeseq = p2.codeseq " +
                                     "AND p1.annee = p2.annee " +
                                     "ORDER BY matricule, codemat, codeevaluation) t WHERE codeMat = '" + codeMatiere + "' " +
                                      " GROUP BY matricule, codeMat";

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer la voiture à retourner
                    while (dataReader.Read())
                    {
                        matricule = Convert.ToString(dataReader["matricule"]);
                        codemat = Convert.ToString(dataReader["codemat"]);
                        codeseq = Convert.ToString(dataReader["codeSeq"]);
                        moyenne = Convert.ToDouble(dataReader["sum_note"]);

                        String[] t = new String[4];
                        t[0] = matricule;
                        t[1] = codemat;
                        t[2] = codeseq;
                        t[3] = Convert.ToString(moyenne);

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

        // Calcul de la moyenne séquentielle d'un élève particulier dans une classe donnée
        public String[] calculMoyenneSequentielleDunEleve(String matricule, String codeClasse, String codeMatiere, String codeSequence, int annee)
        {
            string codemat;
            string codeseq;
            double moyenne;

            List<String[]> Lmoyenne = new List<string[]>();
            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT matricule, codeMat, codeSeq, SUM(note * poids/100) FROM (SELECT n.matricule, n.codeMat, n.codeSeq, n.annee, e.codeevaluation, e.poids, n.note FROM notes n, (SELECT codeMat, codeevaluation, poids, codeSeq, codeClasse, annee FROM evaluer WHERE codeClasse = '" + codeClasse + "' AND codeSeq = '" + codeSequence + "' AND annee = '" + annee + "') e WHERE e.annee = n.annee AND e.codeMat = n.codeMat AND e.codeSeq = n.codeSeq AND n.codeevaluation = e.codeevaluation) t WHERE codeMat = '" + codeMatiere + "' AND matricule = '" + matricule + "' GROUP BY matricule, codeMat";

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer la voiture à retourner
                    while (dataReader.Read())
                    {
                        codemat = Convert.ToString(dataReader["codemat"]);
                        codeseq = Convert.ToString(dataReader["codeSeq"]);
                        moyenne = Convert.ToDouble(dataReader["SUM(note * poids/100)"]);

                        String[] t = new String[4];
                        t[0] = matricule;
                        t[1] = codemat;
                        t[2] = codeseq;
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

        //recherche les moyennes d'un élève pour une séquence et une année donnée
        public List<MoyennesBE> moyennesSequentiellesEleve(String matricule, int annee, String codeSequence)
        {
            string codeMatiere;
            double moyenne;
            double moyenneclasse;
            string mention;
            double moyennemin;
            double moyennemax;
            string appreciation;

            List<MoyennesBE> LMoyenne = new List<MoyennesBE>();
            
            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT * FROM moyennes WHERE codeseq='"+codeSequence+"' AND matricule='"+matricule+"' AND annee='"+annee+"'";
                //cmd.Parameters.AddWithValue("@codeSequence", codeSequence);
                //cmd.Parameters.AddWithValue("@matricule", matricule);
                //cmd.Parameters.AddWithValue("@annee", annee);

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer la voiture à retourner
                    while (dataReader.Read())
                    {
                        MoyennesBE m;
                        codeMatiere = Convert.ToString(dataReader["codemat"]);
                        //codeSequence = Convert.ToString(dataReader["codeseq"]);
                        //matricule = Convert.ToString(dataReader["matricule"]);
                        moyenne = Convert.ToDouble(dataReader["moyenne"]);
                        //annee = Convert.ToInt16(dataReader["annee"]);

                        moyenneclasse = Convert.ToDouble(dataReader["moyenneclasse"]);
                        mention = Convert.ToString(dataReader["mention"]);
                        moyennemin = Convert.ToDouble(dataReader["moyennemin"]);
                        moyennemax = Convert.ToInt16(dataReader["moyennemax"]);
                        appreciation = Convert.ToString(dataReader["appreciation"]);
                        m = new MoyennesBE(codeMatiere, codeSequence, matricule, moyenne, annee, Convert.ToInt32(dataReader["rang"]), moyenneclasse, mention, moyennemin, moyennemax, appreciation);
                        // this.con.fermer();
                        LMoyenne.Add(m);
                    }

                    dataReader.Close();

                    return LMoyenne;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        //recherche toutes les moyennes séquentielle d'un élève avec son résultats annuels (pour le profil académique)
        public List<LigneProfilAcademique> infosProfilAcademique(String matricule)
        {
            //String matricule; // le maatricule de l'élève
            int annee;
            String codeClasse;
            String codeMatiere;
            String codeSequence;
            String codeTrimestre;
            double moyenne; //La moyenne séquentielle de la matière
            String mention;
            int coef;
            int rang;
            double moyenneClasse;
            double moyenneMin;
            double moyenneMax;
            double totalPoint; //la somme totale des point (pour toutes les matières) de l'élève pour l'année
            double moyenneAnnuelle;
            String mentionAnnuelle;
            int rangAnnuel;
            double moyenneAnnuelleClasse;

            List<LigneProfilAcademique> LLigneProfilAcademique = new List<LigneProfilAcademique>();

            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT m.matricule, m.annee, i.codeclasse, m.codemat, s.codetrimestre, m.codeseq, m.moyenne, m.mention, p.coef, " +
                    "m.rang, m.moyenneclasse, m.moyenneMin, m.moyenneMax, r.point as totalpoint, r.moyenne as moyenneAnnuelle, " +
                    "r.mention as mentionAnnuelle, r.rang as rangAnnuel, r.moyenneclasse as moyenneAnnuelleClasse " +
                    "FROM moyennes m, sequence s, inscrire i, programmer p, resultat r, niveau n, classe c " +
                    "WHERE m.matricule = i.matricule " +
                    "AND i.annee = m.annee " +
                    "AND m.codeSeq = s.codeSeq " +
                    "AND m.matricule = '" + matricule + "' " +
                    "AND p.codeClasse = i.codeClasse " +
                    "AND p.codeMat = m.codemat " +
                    "AND p.annee = m.annee " +
                    "AND r.annee = m.annee " +
                    "AND r.matricule = m.matricule " +
                    "AND n.codeniveau = c.codeniveau " +
                    "AND c.codeclasse = i.codeclasse " +
                    "ORDER BY m.annee, n.codeniveau,codetrimestre,codeseq, codemat ASC";


                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer la voiture à retourner
                    while (dataReader.Read())
                    {
                        LigneProfilAcademique ligne;
                        annee = Convert.ToInt16(dataReader["annee"]);

                        codeClasse = Convert.ToString(dataReader["codeclasse"]);
                        codeMatiere = Convert.ToString(dataReader["codemat"]);
                        codeSequence = Convert.ToString(dataReader["codeseq"]);
                        codeTrimestre = Convert.ToString(dataReader["codetrimestre"]);
                        moyenne = Convert.ToDouble(dataReader["moyenne"]);
                        mention = Convert.ToString(dataReader["mention"]);

                        coef = Convert.ToInt16(dataReader["coef"]);
                        rang = Convert.ToInt16(dataReader["rang"]);
                        moyenneClasse = Convert.ToDouble(dataReader["moyenneclasse"]);
                        moyenneMin = Convert.ToDouble(dataReader["moyenneMin"]);
                        moyenneMax = Convert.ToDouble(dataReader["moyenneMax"]);
                        totalPoint = Convert.ToDouble(dataReader["totalpoint"]);
                        moyenneAnnuelle = Convert.ToDouble(dataReader["moyenneAnnuelle"]);
                        mentionAnnuelle = Convert.ToString(dataReader["mentionAnnuelle"]);
                        rangAnnuel = Convert.ToInt16(dataReader["rangAnnuel"]);
                        moyenneAnnuelleClasse = Convert.ToDouble(dataReader["moyenneAnnuelleClasse"]);

                        ligne = new LigneProfilAcademique(matricule, annee, codeClasse, codeMatiere, codeSequence, codeTrimestre, moyenne, mention, coef, rang,
                            moyenneClasse, moyenneMin, moyenneMax, totalPoint, moyenneAnnuelle, mentionAnnuelle, rangAnnuel, moyenneAnnuelleClasse);
                        // this.con.fermer();
                        LLigneProfilAcademique.Add(ligne);
                    }

                    dataReader.Close();

                    return LLigneProfilAcademique;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        //liste les moyennes séquentielles des élèves d'une classe pour une matière et une année
        public List<MoyennesBE> listerMoyennesSequentielleDesElevesDuneClasse(String codeClasse, String codeMatiere, String codeSequence, int annee)
        {
            try
            {
                List<MoyennesBE> listobjBE = new List<MoyennesBE>();

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT * FROM moyennes WHERE matricule in (SELECT matricule FROM inscrire WHERE codeclasse = '" + codeClasse + "' AND annee = '" + annee + "') AND codeSeq = '" + codeSequence + "' AND codeMat = '" + codeMatiere + "' ";

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //Read the data and store them in the list
                    while (dataReader.Read())
                    {
                        MoyennesBE objBE = new MoyennesBE(Convert.ToString(dataReader["codemat"]), Convert.ToString(dataReader["codeseq"]), Convert.ToString(dataReader["matricule"]),
                            Convert.ToDouble(dataReader["moyenne"]), Convert.ToInt16(dataReader["annee"]), Convert.ToInt32(dataReader["rang"]), Convert.ToDouble(dataReader["moyenneclasse"]),
                            Convert.ToString(dataReader["mention"]), Convert.ToDouble(dataReader["moyennemin"]), Convert.ToDouble(dataReader["moyennemax"]), Convert.ToString(dataReader["appreciation"]));
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

        public LigneRecapitulatif recapitulatifSequentielEleve(EleveBE eleve, string codeclasse, string codesequence, int annee)
        {
            LigneRecapitulatif ligne = new LigneRecapitulatif();
            
            #region recuperation des infos sur les moyennes des groupes et le rang ainsi que la moyenne sequentielle
            try
            {
                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT ms.matricule, p.codegroupe,SUM(ms.moyenne * p.coef) / SUM(coef) moyenne "
                                  +"  FROM moyennes ms, programmer p "
                                  +"  WHERE ms.codemat = p.codemat "
                                  +"  AND ms.annee = p.annee "
                                  +"  AND p.codeclasse = "+"'"+ codeclasse +"'"
                                  + "  AND ms.annee = " + "'" + annee + "'"
                                  + "  AND ms.codeseq = " + "'" + codesequence + "'"
                                  + "  AND ms.matricule = " + "'" + eleve.matricule + "'"
                                  +"  GROUP BY p.codegroupe "
                                  +"  order by p.codegroupe";
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    Dictionary<string, double> dict = new Dictionary<string, double>();
                    while (dataReader.Read())
                    {
                        dict.Add(Convert.ToString(dataReader["codegroupe"]),Convert.ToDouble(dataReader["moyenne"]));
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
                cmd.CommandText = "SELECT ms.matricule, ms.rang as rang, ms.moyenne as moyenne, ms.codemat, ms.codeSeq "
                                  +" FROM moyennes ms, programmer p "
                                  +"  WHERE ms.codemat = p.codemat "
                                  +"  AND ms.annee = p.annee "
                                  + "  AND p.codeclasse = " + "'" + codeclasse + "'"
                                  + "  AND ms.annee = " + "'" + annee + "'"
                                  + "  AND ms.codeseq = " + "'" + codesequence + "'"
                                  + "  AND ms.matricule = " + "'" + eleve.matricule + "'"
                                  +" ;";

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
                cmd.CommandText = "SELECT rang, moyenne, point as total, mention FROM resultat "
                                   + " WHERE matricule = " + "'" + eleve.matricule + "'"
                                   + " AND codeseq = " + "'" + codesequence + "'"
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

        public LigneRecapSeq recapitulatifSequentielEleve_new(EleveBE eleve, string codeclasse, string codesequence, int annee)
        {
            LigneRecapSeq ligne = new LigneRecapSeq();
            int nbsousmoyenne = 0;

            #region recuperation des infos sur les sanctions
            List<SanctionnerBE> listSanctionSequentiels = new List<SanctionnerBE>();
            SanctionnerDA sanctionnerDA = new SanctionnerDA();
            listSanctionSequentiels = sanctionnerDA.getListSanctionSequentielleEleve(eleve.matricule, annee, codesequence);
            Dictionary<string, int> dico = new Dictionary<string, int>();
            dico.Add("absi", 0); 
            dico.Add("absj", 0);
            foreach (SanctionnerBE sanction in listSanctionSequentiels)
            {
                if (sanction.codesanction == "abs")
                {
                    if (sanction.etat == "JUSTIFIEE")
                        dico["absj"] = sanction.quantité;
                    else
                        dico["absi"]= sanction.quantité;
                }
                else
                    dico.Add(sanction.codesanction, sanction.quantité);
            }
            dico.Add("abs", (dico["absi"] - dico["absj"]));
            ligne.sanctions = new Dictionary<string, int>(dico);
            #endregion

            #region recuperation des infos sur les notes sequentielle des matieres
            try
            {
                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT ms.matricule, ms.rang as rang, ms.moyenne as moyenne, ms.codemat, ms.codeSeq "
                                  + " FROM moyennes ms, programmer p "
                                  + "  WHERE ms.codemat = p.codemat "
                                  + "  AND ms.annee = p.annee "
                                  + "  AND p.codeclasse = " + "'" + codeclasse + "'"
                                  + "  AND ms.annee = " + "'" + annee + "'"
                                  + "  AND ms.codeseq = " + "'" + codesequence + "'"
                                  + "  AND ms.matricule = " + "'" + eleve.matricule + "'"
                                  + " ;";

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    Dictionary<string, double> dict = new Dictionary<string, double>();
                    while (dataReader.Read())
                    {
                        double moyenne = Convert.ToDouble(dataReader["moyenne"]);
                        dict.Add(Convert.ToString(dataReader["codemat"]), moyenne);
                        if (moyenne < 10)
                            nbsousmoyenne++;
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
                cmd.CommandText = "SELECT rang, moyenne, point as total, mention FROM resultat "
                                   + " WHERE matricule = " + "'" + eleve.matricule + "'"
                                   + " AND codeseq = " + "'" + codesequence + "'"
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

            string redoublant = "";
            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT * FROM inscrire WHERE annee=@annee AND matricule=@matricule and codeclasse=@codeclasse";
                cmd.Parameters.AddWithValue("@codeclasse", codeclasse);
                cmd.Parameters.AddWithValue("@annee", annee - 1);
                cmd.Parameters.AddWithValue("@matricule", eleve.matricule);

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer la voiture à retourner
                    if (dataReader.Read())
                    {
                        redoublant = "OUI";
                    }
                    else
                        redoublant = "NON";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            ligne.sexe_redoub = eleve.sexe.Substring(0, 1) + " / " + redoublant;
            ligne.nb_sous_moyenne = nbsousmoyenne;
            #endregion

            return ligne;
        }

        //recherche toutes les moyennes d'un élève
        public List<MoyennesBE> listeMoyennesEleve(String matricule)
        {
            string codeMatiere;
            string codeSequence;
            int annee;
            double moyenne;
            double moyenneclasse;
            string mention;
            double moyennemin;
            double moyennemax;

            List<MoyennesBE> LMoyenne = new List<MoyennesBE>();

            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT * FROM moyennes WHERE matricule='" + matricule + "' ORDER BY annee ASC";
                //cmd.Parameters.AddWithValue("@codeSequence", codeSequence);
                //cmd.Parameters.AddWithValue("@matricule", matricule);
                //cmd.Parameters.AddWithValue("@annee", annee);

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer la voiture à retourner
                    while (dataReader.Read())
                    {
                        MoyennesBE m;
                        codeMatiere = Convert.ToString(dataReader["codemat"]);
                        codeSequence = Convert.ToString(dataReader["codeseq"]);
                        matricule = Convert.ToString(dataReader["matricule"]);
                        moyenne = Convert.ToDouble(dataReader["moyenne"]);
                        annee = Convert.ToInt16(dataReader["annee"]);

                        moyenneclasse = Convert.ToDouble(dataReader["moyenneclasse"]);
                        mention = Convert.ToString(dataReader["mention"]);
                        moyennemin = Convert.ToDouble(dataReader["moyennemin"]);
                        moyennemax = Convert.ToInt16(dataReader["moyennemax"]);

                        m = new MoyennesBE(codeMatiere, codeSequence, matricule, moyenne, annee, Convert.ToInt32(dataReader["rang"]), moyenneclasse, mention, moyennemin, moyennemax, Convert.ToString(dataReader["appreciation"]));
                        // this.con.fermer();
                        LMoyenne.Add(m);
                    }

                    dataReader.Close();

                    return LMoyenne;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }



        internal List<KeyValuePair<string, int>> effectifValidationMatiereSequentielClasse(string codeclasse, int annee, string codesequence)
        {
            List<KeyValuePair<string, int>> resultat = new List<KeyValuePair<string, int>>();

            try
            {
                List<MoyennesAnnuellesBE> listMoyBE = new List<MoyennesAnnuellesBE>();
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT p.codemat matiere, if(t1.effectif is null,0,t1.effectif) effectif "
                                  + " FROM "
                                  + "   (SELECT codemat, count(*) effectif, m.annee "
                                    + "  FROM moyennes m, inscrire i "
                                    + "  WHERE i.codeclasse = '" + codeclasse + "' and i.annee = '" + annee + "' and i.matricule = m.matricule "
                                    + "  and m.moyenne > 10 and m.codeseq = '" + codesequence + "' and i.annee=m.annee group by m.codemat) t1 "
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

        internal List<KeyValuePair<string, int>> effectifValidationResultatSequentielClasse(int annee, string codesequence)
        {
            List<KeyValuePair<string, int>> resultat = new List<KeyValuePair<string, int>>();

            try
            {
                List<MoyennesAnnuellesBE> listMoyBE = new List<MoyennesAnnuellesBE>();
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT classe, effectif, pourcentage"
                                  +"  FROM"
                                  +"  (SELECT codeclasse classe, count(r.matricule) effectif, "
                                  + "  count(r.matricule)/(SELECT count(matricule) FROM inscrire WHERE codeClasse = classe AND annee = '" + annee + "')*100 pourcentage"
                                  +"  FROM resultat r RIGHT JOIN inscrire i"
                                  +"  ON r.matricule=i.matricule  "
                                  +"  AND r.annee=i.annee "
                                  + "  AND i.annee = '" + annee + "'"
                                  +"  AND r.decision='Admis'"
                                  + "  AND r.codeseq='" + codesequence + "'"
                                  +"  group by i.codeclasse) r, niveau n, classe c"
                                  +"  WHERE r.classe=c.codeclasse"
                                  +"  AND c.codeniveau=n.codeniveau"
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

        internal List<KeyValuePair<string, int>> effectifValidationResultatSequentielNiveau(int annee, string codesequence)
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
                                  +"  FROM resultat r RIGHT JOIN inscrire i "
                                  +"  ON r.matricule=i.matricule  "
                                  +"  AND r.annee=i.annee "
                                  + "  AND i.annee = '" + annee + "' "
                                  +"  AND r.decision='Admis' "
                                  + "  AND r.CODESEQ='" + codesequence + "'"
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

        internal List<KeyValuePair<string, int>> progressionSequentielClasse(string codeclasse, int annee)
        {
            List<KeyValuePair<string, int>> resultat = new List<KeyValuePair<string, int>>();

            try
            {
                List<MoyennesAnnuellesBE> listMoyBE = new List<MoyennesAnnuellesBE>();
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT s.codeSeq sequence,if(effectif is null,0,effectif) effectif,if(pourcentage is null,0,pourcentage) pourcentage "
                                  +"  FROM "
                                  +"  (SELECT codeSeq, count(matricule) effectif, "
                                  + "         count(matricule)/(SELECT count(matricule) FROM inscrire WHERE codeClasse = '" + codeclasse + "' AND annee = '" + annee + "')*100 pourcentage "
                                  +"  FROM resultat "
                                  + "  WHERE ANNEE='" + annee + "' "
                                  +"  AND DECISION='Admis' "
                                  +"  AND MATRICULE IN (SELECT matricule "
		                          +"            FROM inscrire "
                                  + "            WHERE codeClasse = '" + codeclasse + "' "
                                  + "                    AND annee = '" + annee + "') "
                                  +"  Group by codeseq) r RIGHT JOIN (SELECT * FROM Sequence) s on (s.codeSeq=r.codeSeq)";

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        resultat.Add(new KeyValuePair<string, int>(Convert.ToString(dataReader["sequence"]), Convert.ToInt32(dataReader["pourcentage"])));
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

        public Synthese syntheseSequentielleClasse(string codeclasse, string codesequence, int annee)
        {
            Synthese synthese = new Synthese();
            Dictionary<string, double> dico = new Dictionary<string, double>();

            //recuperation des 10 premiers
            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT e.nom, r.moyenne FROM Resultat r, Eleve e WHERE annee =  '" + annee + "'" 
                                   +"  AND r.matricule in (SELECT matricule FROM inscrire WHERE "
                                   + "  codeclasse LIKE  '" + codeclasse + "' AND annee =  '" + annee + "') AND codeSeq LIKE  '" + codesequence + "' AND e.matricule=r.matricule "
                                   +"  order by r.moyenne DESC LIMIT 10;";

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet acheter à retourner
                    while (dataReader.Read())
                    {
                        dico.Add(Convert.ToString(dataReader["nom"]), Convert.ToDouble(dataReader["moyenne"]));
                    }
                    synthese.synthese_premiers = new Dictionary<string, double>(dico);
                    dataReader.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                synthese.synthese_premiers = new Dictionary<string, double>(0);
                return null;
            }

            //recuperation des 10 derniers
            dico = new Dictionary<string, double>();
            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT e.nom, r.moyenne FROM Resultat r, Eleve e WHERE annee =  '" + annee + "'"
                                   + "  AND r.matricule in (SELECT matricule FROM inscrire WHERE "
                                   + "  codeclasse LIKE  '" + codeclasse + "' AND annee =  '" + annee + "') AND codeSeq LIKE  '" + codesequence + "' AND e.matricule=r.matricule "
                                   + "  order by r.moyenne ASC LIMIT 10;";

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet acheter à retourner
                    while (dataReader.Read())
                    {
                        dico.Add(Convert.ToString(dataReader["nom"]), Convert.ToDouble(dataReader["moyenne"]));
                    }
                    synthese.synthese_derniers = new Dictionary<string, double>(dico);
                    dataReader.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                synthese.synthese_derniers = new Dictionary<string, double>(0);
                return null;
            }

            //recuperation du bilan des garcons et filles
            synthese.synthese_garcon = new Dictionary<string, double>(bilangarcons(codeclasse, codesequence, annee));
            synthese.synthese_fille = new Dictionary<string, double>(bilanfilles(codeclasse, codesequence, annee));

            StatistiqueClasseBE stat = new StatistiqueClasseBE();
            StatistiqueClasseDA statistiqueDA = new StatistiqueClasseDA ();
            stat = statistiqueDA.getStatistiqueDuneSequence(codeclasse, annee, codesequence);
            dico = new Dictionary<string, double>();
            double effectif = 0;
            try
            {
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "select count(e.matricule) as effectif from inscrire i, eleve e where i.codeclasse='" + codeclasse + "'"
                                    + " and e.matricule=i.matricule and i.annee='" + annee + "'";

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    if (dataReader.Read())
                    {
                        effectif = Convert.ToDouble(dataReader["effectif"]);
                        dico.Add("EFFECTIF", Convert.ToDouble(dataReader["effectif"]));
                    }
                    dataReader.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                dico.Add("EFFECTIF", 0);
            }
            dico.Add("PRESENTS", stat.effectif);
            dico.Add("ABSENT", effectif - stat.effectif);
            dico.Add("MOYENNE DE LA CLASSE", getMoyennegeneraleClasse(codeclasse, codesequence, annee));
            dico.Add("NOMBRE DE MOYENNE", stat.nbAdmis);
            dico.Add("POURCENTAGE DE REUSSITE", Convert.ToDouble(stat.pourcentageAdmis.Split('%')[0]));

            synthese.synthese_classe = new Dictionary<string, double>(dico);

            return synthese;
        }

        private Dictionary<string, double> bilangarcons(string codeclasse, string codesequence, int annee)
        {
            Dictionary<string, double> dico = new Dictionary<string, double>();
            int nbpresent=0, nbmoyenne=0;
            try
            {
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "select count(e.matricule) as effectif from inscrire i, eleve e where i.codeclasse='" + codeclasse + "'"
                                    + " and e.matricule=i.matricule and i.annee='" + annee + "' and e.sexe LIKE 'Masculin'";

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    if (dataReader.Read())
                        dico.Add("EFFECTIF GARCONS", Convert.ToDouble(dataReader["effectif"]));
                    dataReader.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                dico.Add("EFFECTIF GARCONS", 0);
            }

            try
            {
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "select count(*) as effectif from resultat r, inscrire i, eleve e "
                                  + "  where r.annee='" + annee + "' and r.matricule=i.matricule and r.matricule=e.matricule and e.sexe='Masculin' "
                                  + "  and r.codeseq='" + codesequence + "' and i.matricule in (select matricule from inscrire where codeclasse='" + codeclasse + "' and annee='" + annee + "');";

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    if (dataReader.Read())
                    {
                        nbpresent = Convert.ToInt32(dataReader["effectif"]);
                        dico.Add("GARCONS AYANT COMPOSES", Convert.ToDouble(dataReader["effectif"]));
                    }
                    dataReader.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                dico.Add("GARCONS AYANT COMPOSES", 0);
            }

            try
            {
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "select count(*) as effectif from resultat r, inscrire i, eleve e "
                                  + "  where r.annee='" + annee + "' and r.matricule=i.matricule and r.matricule=e.matricule and e.sexe='Masculin' and r.moyenne>10 "
                                  + "  and r.codeseq='" + codesequence + "' and i.matricule in (select matricule from inscrire where codeclasse='" + codeclasse + "' and annee='" + annee + "');";

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    if (dataReader.Read())
                    {
                        nbmoyenne = Convert.ToInt32(dataReader["effectif"]);
                        dico.Add("NOMBRE DE MOYENNE GARCONS", Convert.ToDouble(dataReader["effectif"]));
                    }
                    dataReader.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                dico.Add("NOMBRE DE MOYENNE GARCONS", 0);
            }

            if(nbpresent > 0)
                dico.Add("POURCENTAGE DE REUSSITE GARCONS", ((nbmoyenne*100)/nbpresent));
            else
                dico.Add("POURCENTAGE DE REUSSITE GARCONS", 0);

            return dico;
        }

        private Dictionary<string, double> bilanfilles(string codeclasse, string codesequence, int annee)
        {
            Dictionary<string, double> dico = new Dictionary<string, double>();
            int nbpresent = 0, nbmoyenne = 0;
            try
            {
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "select count(e.matricule) as effectif from inscrire i, eleve e where i.codeclasse='" + codeclasse + "'"
                                    + " and e.matricule=i.matricule and i.annee='" + annee + "' and e.sexe LIKE 'Feminin'";

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    if (dataReader.Read())
                        dico.Add("EFFECTIF FILLES", Convert.ToDouble(dataReader["effectif"]));
                    dataReader.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                dico.Add("EFFECTIF FILLES", 0);
            }

            try
            {
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "select count(*) as effectif from resultat r, inscrire i, eleve e "
                                  + "  where r.annee='" + annee + "' and r.matricule=i.matricule and r.matricule=e.matricule and e.sexe='Feminin' "
                                  + "  and r.codeseq='" + codesequence + "' and i.matricule in (select matricule from inscrire where codeclasse='" + codeclasse + "' and annee='" + annee + "');";

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    if (dataReader.Read())
                    {
                        nbpresent = Convert.ToInt32(dataReader["effectif"]);
                        dico.Add("FILLES AYANT COMPOSES", Convert.ToDouble(dataReader["effectif"]));
                    }
                    dataReader.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                dico.Add("FILLES AYANT COMPOSES", 0);
            }

            try
            {
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "select count(*) as effectif from resultat r, inscrire i, eleve e "
                                  + "  where r.annee='" + annee + "' and r.matricule=i.matricule and r.matricule=e.matricule and e.sexe='Feminin' and r.moyenne>10 "
                                  + "  and r.codeseq='" + codesequence + "' and i.matricule in (select matricule from inscrire where codeclasse='" + codeclasse + "' and annee='" + annee + "');";

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    if (dataReader.Read())
                    {
                        nbmoyenne = Convert.ToInt32(dataReader["effectif"]);
                        dico.Add("NOMBRE DE MOYENNE FILLES", Convert.ToDouble(dataReader["effectif"]));
                    }
                    dataReader.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                dico.Add("NOMBRE DE MOYENNE FILLES", 0);
            }

            if (nbpresent > 0)
                dico.Add("POURCENTAGE DE REUSSITE FILLES", ((nbmoyenne * 100) / nbpresent));
            else
                dico.Add("POURCENTAGE DE REUSSITE FILLES", 0);

            return dico;
        }

        private double getMoyennegeneraleClasse(string codeclasse, string codesequence, int annee)
        {
            double moyenne = 0;

            try
            {
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT distinct moyenneclasse FROM resultat where codeseq='" + codesequence + "' and annee='" + annee + "'"
                                    +" and matricule in (select matricule from inscrire where codeclasse='" + codeclasse + "' and annee='" + annee + "');";

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    if (dataReader.Read())
                        moyenne = Convert.ToDouble(dataReader["moyenneclasse"]);
                    dataReader.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return moyenne;
        }
    }

    
}
