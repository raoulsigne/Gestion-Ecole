using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using Ecole.BusinessEntity;
using Ecole.ClasseConception;
using Ecole.UI;
using System.Globalization;
using System.Threading;

namespace Ecole.DataAccess
{
    public class EleveDA : DA<EleveBE>
    {
        private Connexion con = Connexion.getConnexion();

        //----------------ajouter -----------------//
        public override Boolean ajouter(EleveBE entity)
        {
            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "yyyy-MM-dd";
            Thread.CurrentThread.CurrentCulture = ci;

            MySqlTransaction tx = con.connexion.BeginTransaction();
            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "INSERT INTO Eleve (matricule, codepays, codedept, coderegion, nom, sexe, datenaissance, lieunaissance, photo, nompere, nommere, telephone, telparent, email, adresse,diplome,langue,anneediplome, FONCTION_PERE, FONCTION_MERE, SITUATION_MEDICALE, etat) VALUES (@mat, @pays, @dept, @region, @nom, @sexe, @dateNaiss, @lieuNaiss,@photo, @pere, @mere, @tel, @telparent, @email, @adresse,@diplome,@langue,@anneeDiplome, @fonctionPere, @fonctionMere, @situationMedicale, @etat)";
                cmd.Parameters.AddWithValue("@mat", entity.matricule);
                cmd.Parameters.AddWithValue("@pays", entity.codePays);
                cmd.Parameters.AddWithValue("@dept", entity.codeDept);
                cmd.Parameters.AddWithValue("@region", entity.codeRegion);
                cmd.Parameters.AddWithValue("@nom", entity.nom);
                cmd.Parameters.AddWithValue("@sexe", entity.sexe);
                cmd.Parameters.AddWithValue("@dateNaiss", entity.dateNaissance);
                cmd.Parameters.AddWithValue("@lieuNaiss", entity.lieuNaissance);
                cmd.Parameters.AddWithValue("@photo", entity.photo);
                cmd.Parameters.AddWithValue("@pere", entity.nomPere);
                cmd.Parameters.AddWithValue("@mere", entity.nomMere);
                cmd.Parameters.AddWithValue("@tel", entity.telephone);
                cmd.Parameters.AddWithValue("@telparent", entity.telParent);
                cmd.Parameters.AddWithValue("@email", entity.email);
                cmd.Parameters.AddWithValue("@adresse", entity.adresse);
                cmd.Parameters.AddWithValue("@diplome", entity.diplome);
                cmd.Parameters.AddWithValue("@langue", entity.langue);
                cmd.Parameters.AddWithValue("@anneeDiplome", entity.anneeDiplome);
                cmd.Parameters.AddWithValue("@fonctionPere", entity.fonctionPere);
                cmd.Parameters.AddWithValue("@fonctionMere", entity.fonctionMere);
                cmd.Parameters.AddWithValue("@situationMedicale", entity.situationMedicale);
                cmd.Parameters.AddWithValue("@etat", entity.etat);

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
        public override Boolean supprimer(EleveBE entity)
        {
            MySqlTransaction tx = con.connexion.BeginTransaction();
            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "DELETE FROM Eleve WHERE matricule = @matricule";
                cmd.Parameters.AddWithValue("@matricule", entity.matricule);

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
        public override EleveBE rechercher(EleveBE entity)
        {
            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "yyyy-MM-dd";
            Thread.CurrentThread.CurrentCulture = ci;

            string matricule;
            string codepays;
            string codedept;
            string coderegion;
            string nom;
            string sexe;
            DateTime date;
            String lieuNaiss;
            String photo;
            String nomPere;
            String nomMere;
            String telephone;
            String telParent;
            String email;
            String adresse;

            String fonctionPere;
            String fonctionMere;
            String situationMedicale;
            String etat;

            EleveBE e;

            try
            {
                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT * FROM Eleve WHERE matricule=@mat";
                cmd.Parameters.AddWithValue("@mat", entity.matricule);

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet acheter à retourner
                    if (dataReader.Read())
                    {
                        matricule = Convert.ToString(dataReader["matricule"]);
                        codepays = Convert.ToString(dataReader["codepays"]);
                        codedept = Convert.ToString(dataReader["codedept"]);
                        coderegion = Convert.ToString(dataReader["coderegion"]);
                        nom = Convert.ToString(dataReader["nom"]);
                        sexe = Convert.ToString(dataReader["sexe"]);
                        date = Convert.ToDateTime(dataReader["datenaissance"]);
                       // date = Convert.ToDateTime((Convert.ToDateTime(dataReader["datenaissance"])).ToShortDateString());
                        lieuNaiss = Convert.ToString(dataReader["lieunaissance"]);
                        photo = Convert.ToString(dataReader["photo"]);
                        nomPere = Convert.ToString(dataReader["nompere"]);
                        nomMere = Convert.ToString(dataReader["nommere"]);
                        telephone = Convert.ToString(dataReader["telephone"]);
                        telParent = Convert.ToString(dataReader["telparent"]);
                        email = Convert.ToString(dataReader["email"]);
                        adresse = Convert.ToString(dataReader["adresse"]);

                        fonctionPere = Convert.ToString(dataReader["FONCTION_PERE"]); 
                        fonctionMere = Convert.ToString(dataReader["FONCTION_MERE"]);
                        situationMedicale = Convert.ToString(dataReader["SITUATION_MEDICALE"]);
                        etat = Convert.ToString(dataReader["etat"]);

                        e = new EleveBE(matricule, codepays, codedept, coderegion, nom, sexe, date, lieuNaiss, Convert.ToString(dataReader["langue"]), photo, nomPere, nomMere, telephone, telParent,
                            email, adresse, Convert.ToString(dataReader["diplome"]), Convert.ToInt32(dataReader["anneediplome"]));

                        e.fonctionPere = fonctionPere;
                        e.fonctionMere = fonctionMere;
                        e.situationMedicale = situationMedicale;
                        e.etat = etat;

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
        public override Boolean modifier(EleveBE entity, EleveBE newEntity)
        {
            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "yyyy-MM-dd";
            Thread.CurrentThread.CurrentCulture = ci;

            MySqlTransaction tx = con.connexion.BeginTransaction();
            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                cmd.CommandText = "UPDATE eleve SET codepays=@codepays, codedept=@codedept, coderegion=@coderegion, nom=@nom, sexe=@sexe,datenaissance=@datenaissance, "
                                  + "lieunaissance=@lieunaissance, photo=@photo, nompere=@nompere, nommere=@nommere, telephone=@telephone, telparent=@telparent, email=@email, "
                                  + "adresse=@adresse, langue=@langue, diplome=@diplome, anneediplome=@anneediplome, FONCTION_PERE=@fonctionPere, FONCTION_MERE=@fonctionMere, SITUATION_MEDICALE=@situationMedicale, etat=@etat WHERE matricule=@matricule";

                // utilisation de l'objet voiture passé en paramètre
                cmd.Parameters.AddWithValue("@matricule", entity.matricule);
                cmd.Parameters.AddWithValue("@codepays", newEntity.codePays);
                cmd.Parameters.AddWithValue("@codedept", newEntity.codeDept);
                cmd.Parameters.AddWithValue("@coderegion", newEntity.codeRegion);
                cmd.Parameters.AddWithValue("@nom", newEntity.nom);
                cmd.Parameters.AddWithValue("@sexe", newEntity.sexe);
                cmd.Parameters.AddWithValue("@datenaissance", newEntity.dateNaissance);
                cmd.Parameters.AddWithValue("@lieunaissance", newEntity.lieuNaissance);
                cmd.Parameters.AddWithValue("@photo", newEntity.photo);
                cmd.Parameters.AddWithValue("@nompere", newEntity.nomPere);
                cmd.Parameters.AddWithValue("@nommere", newEntity.nomMere);
                cmd.Parameters.AddWithValue("@telephone", newEntity.telephone);
                cmd.Parameters.AddWithValue("@telparent", newEntity.telParent);
                cmd.Parameters.AddWithValue("@email", newEntity.email);
                cmd.Parameters.AddWithValue("@adresse", newEntity.adresse);
                cmd.Parameters.AddWithValue("@langue", newEntity.langue);
                cmd.Parameters.AddWithValue("@diplome", newEntity.diplome);
                cmd.Parameters.AddWithValue("@anneediplome", newEntity.anneeDiplome);
                cmd.Parameters.AddWithValue("@fonctionPere", newEntity.fonctionPere);
                cmd.Parameters.AddWithValue("@fonctionMere", newEntity.fonctionMere);
                cmd.Parameters.AddWithValue("@situationMedicale", newEntity.situationMedicale);
                cmd.Parameters.AddWithValue("@etat", newEntity.etat);

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
        public override List<EleveBE> listerTous()
        {
            List<EleveBE> list = new List<EleveBE>();
            string matricule;
            string codepays;
            string codedept;
            string coderegion;
            string nom;
            string sexe;
            DateTime date;
            String lieuNaiss;
            String photo;
            String nomPere;
            String nomMere;
            String telephone;
            String telParent;
            String email;
            String adresse;
            String fonctionPere;
            String fonctionMere;
            String situationMedicale;
            String etat;

            EleveBE e;

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT * FROM Eleve ORDER BY nom";

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet à retourner
                    while (dataReader.Read())
                    {
                        matricule = Convert.ToString(dataReader["matricule"]);
                        codepays = Convert.ToString(dataReader["codepays"]);
                        codedept = Convert.ToString(dataReader["codedept"]);
                        coderegion = Convert.ToString(dataReader["coderegion"]);
                        nom = Convert.ToString(dataReader["nom"]);
                        sexe = Convert.ToString(dataReader["sexe"]);
                        date = Convert.ToDateTime(dataReader["datenaissance"]);
                        lieuNaiss = Convert.ToString(dataReader["lieunaissance"]);
                        photo = Convert.ToString(dataReader["photo"]);
                        nomPere = Convert.ToString(dataReader["nompere"]);
                        nomMere = Convert.ToString(dataReader["nommere"]);
                        telephone = Convert.ToString(dataReader["telephone"]);
                        telParent = Convert.ToString(dataReader["telparent"]);
                        email = Convert.ToString(dataReader["email"]);
                        adresse = Convert.ToString(dataReader["adresse"]);

                        fonctionPere = Convert.ToString(dataReader["FONCTION_PERE"]);
                        fonctionMere = Convert.ToString(dataReader["FONCTION_MERE"]);
                        situationMedicale = Convert.ToString(dataReader["SITUATION_MEDICALE"]);
                        etat = Convert.ToString(dataReader["etat"]);

                        e = new EleveBE(matricule, codepays, codedept, coderegion, nom, sexe, date, lieuNaiss, Convert.ToString(dataReader["langue"]), photo, nomPere, nomMere, telephone, telParent,
                            email, adresse, Convert.ToString(dataReader["diplome"]), Convert.ToInt32(dataReader["anneediplome"]));

                        e.fonctionPere = fonctionPere;
                        e.fonctionMere = fonctionMere;
                        e.situationMedicale = situationMedicale;
                        e.etat = etat;

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

        public override List<EleveBE> listerSuivantCritere(string critere)
        {
            List<EleveBE> list = new List<EleveBE>();
            string matricule;
            string codepays;
            string codedept;
            string coderegion;
            string nom;
            string sexe;
            DateTime date;
            String lieuNaiss;
            String photo;
            String nomPere;
            String nomMere;
            String telephone;
            String telParent;
            String email;
            String adresse;
            String fonctionPere;
            String fonctionMere;
            String situationMedicale;
            String etat;

            EleveBE e;

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT * FROM Eleve WHERE " + critere;

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet à retourner
                    while (dataReader.Read())
                    {
                        matricule = Convert.ToString(dataReader["matricule"]);
                        codepays = Convert.ToString(dataReader["codepays"]);
                        codedept = Convert.ToString(dataReader["codedept"]);
                        coderegion = Convert.ToString(dataReader["coderegion"]);
                        nom = Convert.ToString(dataReader["nom"]);
                        sexe = Convert.ToString(dataReader["sexe"]);
                        date = Convert.ToDateTime(dataReader["datenaissance"]);
                        lieuNaiss = Convert.ToString(dataReader["lieunaissance"]);
                        photo = Convert.ToString(dataReader["photo"]);
                        nomPere = Convert.ToString(dataReader["nompere"]);
                        nomMere = Convert.ToString(dataReader["nommere"]);
                        telephone = Convert.ToString(dataReader["telephone"]);
                        telParent = Convert.ToString(dataReader["telparent"]);
                        email = Convert.ToString(dataReader["email"]);
                        adresse = Convert.ToString(dataReader["adresse"]);

                        fonctionPere = Convert.ToString(dataReader["FONCTION_PERE"]);
                        fonctionMere = Convert.ToString(dataReader["FONCTION_MERE"]);
                        situationMedicale = Convert.ToString(dataReader["SITUATION_MEDICALE"]);
                        etat = Convert.ToString(dataReader["etat"]);

                        e = new EleveBE(matricule, codepays, codedept, coderegion, nom, sexe, date, lieuNaiss, Convert.ToString(dataReader["langue"]), photo, nomPere, nomMere, telephone, telParent,
                            email, adresse, Convert.ToString(dataReader["diplome"]), Convert.ToInt32(dataReader["anneediplome"]));

                        e.fonctionPere = fonctionPere;
                        e.fonctionMere = fonctionMere;
                        e.situationMedicale = situationMedicale;
                        e.etat = etat;

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
                cmd.CommandText = "SELECT * FROM Eleve ";

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

        //----------------debut compter ---------------------------------------
        public int compter()
        {
            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT COUNT(*) FROM Eleve";

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
        //-----------fin compter -------------------------------------------------

        //recherche et retourne le niveau d'un élève
        public int rechercherNiveau(EleveBE eleve, int annee)
        {
            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT n.niveau FROM niveau n, classe c, inscrire i, eleve e WHERE e.matricule = i.matricule AND i.annee = '" + annee + "' AND i.codeclasse = c.codeclasse AND c.codeniveau = n.codeniveau AND e.matricule = '" + eleve.matricule + "'";

                int niveau = -1;

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet à retourner
                    while (dataReader.Read())
                    {
                        niveau = Convert.ToInt16(dataReader[0]);
                    }
                    dataReader.Close();

                    return niveau;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -1;
            }

        }

        //-----------retourner le dernier matricule généré--------------------------------------------
        public string getDernierMatricule()
        {
            string matricule="";
            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT id,matricule FROM eleve WHERE id = (SELECT MAX(id) FROM eleve)";

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet à retourner
                    while (dataReader.Read())
                    {
                        matricule = Convert.ToString(dataReader["matricule"]);
                    }
                    dataReader.Close();

                    return matricule;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

        }

        //-----------retourner la situation finaciere d'un élève------------------------------
        public List<LigneSituationFinanciere> getSituationFinanciereEleve(string varMatricule, int annee)
        {
            List<LigneSituationFinanciere> listSituation = new List<LigneSituationFinanciere>();
            int numero = 0;
            string nom = "";
            string matricule = "";
            string categorie = "";
            string prestation = "";
            double aPayer = 0;
            double paye = 0;
            double remise = 0;
            double resteApayer = 0;

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();

                cmd.CommandText = "SELECT t1.matricule, t1.codecateleve, t1.codeprestation, t1.A_Payer , if(isnull(t2.payé),0,t2.payé) Payé, "
                                    + "if(isnull(t2.remise),0,t2.remise) Remise, (t1.A_payer-if(isnull(t2.payé),0,t2.payé)-if(isnull(t2.remise),0,t2.remise)) Reste_A_Payer "
                                    + "FROM "
                                    + "((SELECT a.matricule, m.codecateleve, m.codeprestation, sum(montant) A_payer, a.annee "
                                    + "FROM montanttranche m, appartenir a "
                                    + "WHERE a.codecateleve=m.codecateleve "
                                    + "AND a.annee=m.annee "
                                    + "AND a.annee=" + annee + " "
                                    + "GROUP BY a.matricule,m.codecateleve,codeprestation "
                                    + "ORDER BY matricule,codeprestation) "
                                    + "UNION "
                                    + "(SELECT i.matricule,a.codecateleve,(SELECT CODEPRESTATION From PRESTATION where NOMPRESTATION LIKE  '%inscription%' ),t.fraisinscription,i.annee "
                                    + "FROM `typeclasse` t, inscrire i, classe c, appartenir a "
                                    + "WHERE i.codeclasse=c.codeclasse "
                                    + "AND c.codetypeclasse=t.codetypeclasse "
                                    + "AND i.matricule=a.matricule "
                                    + "and i.annee=a.annee "
                                    + "and i.annee=" + annee + ") "
                                    + "ORDER BY matricule) t1 LEFT JOIN (SELECT matricule,codeprestation,sum(montant) payé, sum(remise) remise, annee "
                                    + "FROM payer p WHERE annee= " + annee + " GROUP BY matricule,codeprestation) t2 "
                                    + "ON (t1.matricule=t2.matricule AND t1.codeprestation=t2.codeprestation AND t1.annee=t2.annee) "
                                    + "WHERE t1.matricule = '" + varMatricule + "'";


                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet à retourner
                    while (dataReader.Read())
                    {
                        numero = 0;
                        nom = "";
                        matricule = Convert.ToString(dataReader["matricule"]);
                        categorie = Convert.ToString(dataReader["codecateleve"]);
                        prestation = Convert.ToString(dataReader["codeprestation"]);
                        aPayer = Convert.ToDouble(dataReader["A_Payer"]);
                        paye = Convert.ToDouble(dataReader["Payé"]);
                        remise = Convert.ToDouble(dataReader["Remise"]);
                        resteApayer = Convert.ToDouble(dataReader["Reste_A_Payer"]);
                        LigneSituationFinanciere situation = new LigneSituationFinanciere(numero, nom, matricule, categorie, prestation, aPayer, remise, paye, resteApayer);
                        listSituation.Add(situation);
                    }
                    dataReader.Close();

                    return listSituation;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

        }

        //---------------------------MOI--------------------------------------------------------------------
        //-----------retourner la liste des élèves avec leur statut de solvabilité---------------------------
        public List<LigneInsolvable> getListeInsolvable(string classe, int annee, string date)
        {
            List<LigneInsolvable> listLigneInsolvable = new List<LigneInsolvable>();
            int numero = 0;
            string nom = "";
            string matricule = "";
            string categorie = "";
            double aPayer = 0;
            double paye = 0;
            double remise = 0;
            double resteApayer = 0;
            string observation = "";

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();

                cmd.CommandText = "SELECT upper(e.nom) nom,tab.matricule, tab.codecateleve, SUM(tab.A_Payer) A_Payer, SUM(tab.Paye) Paye, SUM(tab.Remise) Remise, SUM(tab.Reste_A_Payer) Reste_A_Payer, IF(CAST( SUM(tab.Reste_A_Payer) AS DECIMAL)>0,'Insolvable','OK') Situation "
                                   + "FROM (SELECT t1.matricule, t1.codecateleve, t1.codeprestation, t1.A_Payer , if(isnull(t2.paye),0,t2.paye) Paye, "
                                   + "if(isnull(t2.remise),0,t2.remise) Remise, if((t1.A_payer-if(isnull(t2.paye),0,t2.paye)-if(isnull(t2.remise),0,t2.remise))<0,0,(t1.A_payer-if(isnull(t2.paye),0,t2.paye)-if(isnull(t2.remise),0,t2.remise))) Reste_A_Payer "
                                   + "FROM "
                                   + "((SELECT a.matricule, m.codecateleve, m.codeprestation, sum(montant) A_payer, a.annee "
                                   + "FROM montanttranche m, appartenir a "
                                   + "WHERE a.codecateleve=m.codecateleve "
                                    + "AND a.annee=m.annee "
                                    + "AND a.annee=" + annee + " "
                                    + "and m.delai<='" + date + "' "
                                    + "GROUP BY a.matricule,m.codecateleve,codeprestation "
                                    + "order by matricule,codeprestation) "
                                    + "UNION "
                                    + "(SELECT i.matricule,a.codecateleve,(SELECT CODEPRESTATION From PRESTATION where NOMPRESTATION LIKE '%inscription%' ),t.fraisinscription,i.annee "
                                    + "FROM `typeclasse` t, inscrire i, classe c, appartenir a "
                                    + "WHERE i.codeclasse=c.codeclasse "
                                    + "AND c.codetypeclasse=t.codetypeclasse "
                                    + "AND i.matricule=a.matricule "
                                    + "and i.annee=a.annee "
                                    + "and i.annee=" + annee + ") "
                                    + "order by matricule) t1 LEFT JOIN (SELECT matricule,codeprestation,sum(montant) paye, sum(remise) remise, annee "
                                    + "FROM payer p WHERE annee=" + annee + " GROUP BY matricule,codeprestation) t2 "
                                    + "ON (t1.matricule=t2.matricule AND t1.codeprestation=t2.codeprestation AND t1.annee=t2.annee) "
                                    + "WHERE t1.matricule in (select matricule from inscrire where codeclasse='" + classe + "' and annee=" + annee + ")) tab, eleve e "
                                    + "WHERE tab.matricule=e.matricule "
                                    + "GROUP by tab.matricule "
                                    + "ORDER BY nom";


                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet à retourner
                    while (dataReader.Read())
                    {
                        numero = numero + 1; ;
                        nom = Convert.ToString(dataReader["nom"]); ;
                        matricule = Convert.ToString(dataReader["matricule"]);
                        categorie = Convert.ToString(dataReader["codecateleve"]);
                        aPayer = Convert.ToDouble(dataReader["A_Payer"]);
                        paye = Convert.ToDouble(dataReader["Paye"]);
                        remise = Convert.ToDouble(dataReader["Remise"]);
                        resteApayer = Convert.ToDouble(dataReader["Reste_A_Payer"]);
                        observation = Convert.ToString(dataReader["Situation"]);
                        // //--------Moi---------------------
                        //  if (resteApayer > 0)
                        //      observation = "Insolvable";
                        //  else
                        //      observation = "OK";
                        ////--------------------------------
                        LigneInsolvable situation = new LigneInsolvable(numero, nom, matricule, categorie, aPayer, remise, paye, resteApayer, observation);
                        listLigneInsolvable.Add(situation);
                    }
                    dataReader.Close();

                    return listLigneInsolvable;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

        }

        //-----------------------------FIN MOI---------------------------------------------

        //fonction qui détermine si un élève est redoublant ou pas
        public bool estRedoublant(EleveBE eleve, ClasseBE classeActuelle, int anneeScolaire) {
            /*string codeClasse;
            string matricule;
            int annee;
            InscrireBE inscr;*/
            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT * FROM inscrire WHERE annee=@annee AND matricule=@matricule and codeclasse=@codeclasse";
                cmd.Parameters.AddWithValue("@codeclasse", classeActuelle.codeClasse);
                cmd.Parameters.AddWithValue("@annee", anneeScolaire - 1);
                cmd.Parameters.AddWithValue("@matricule", eleve.matricule);

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer la voiture à retourner
                    if (dataReader.Read())
                    {
                        return true;
                    }
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        internal Dictionary<string, string> contactParent(string matricule)
        {
            try
            {
                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT telparent, email FROM Eleve "
                                  + "  WHERE matricule = " + "'" + matricule + "'";

                Dictionary<string, string> dict = new Dictionary<string, string>();
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        dict.Add(GestionDesNotificationsUI.NUMERO, Convert.ToString(dataReader["telparent"]));
                        dict.Add(GestionDesNotificationsUI.EMAIL, Convert.ToString(dataReader["email"]));
                    }
                    dataReader.Close();
                }
                return dict;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        internal Dictionary<string, string> contactParents(string codeclasse)
        {
            try
            {
                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT telparent, email FROM Eleve e, Inscrire i "
                                  + "  WHERE e.matricule = i.matricule and i.codeclasse like " + "'" + codeclasse + "' and i.annee = (select max(annee) from inscrire)";

                Dictionary<string, string> dict = new Dictionary<string, string>();
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        dict.Add(GestionDesNotificationsUI.NUMERO, Convert.ToString(dataReader["telparent"]));
                        dict.Add(GestionDesNotificationsUI.EMAIL, Convert.ToString(dataReader["email"]));
                    }
                    dataReader.Close();
                }
                return dict;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public List<EleveBE> listeEleveDunNiveau(string codeniveau, int annee)
        {
            List<EleveBE> list = new List<EleveBE>();
            string matricule;
            string codepays;
            string codedept;
            string coderegion;
            string nom;
            string sexe;
            DateTime date;
            String lieuNaiss;
            String photo;
            String nomPere;
            String nomMere;
            String telephone;
            String telParent;
            String email;
            String adresse;

            String fonctionPere;
            String fonctionMere;
            String situationMedicale;
            String etat;

            EleveBE e;

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "select e.* from eleve e, inscrire i, classe c "
                                 + " where e.matricule = i.matricule AND i.annee = '" + annee + "' and i.codeclasse = c.codeclasse and c.codeniveau=" + "'" + codeniveau + "'"; ;

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet à retourner
                    while (dataReader.Read())
                    {
                        matricule = Convert.ToString(dataReader["matricule"]);
                        codepays = Convert.ToString(dataReader["codepays"]);
                        codedept = Convert.ToString(dataReader["codedept"]);
                        coderegion = Convert.ToString(dataReader["coderegion"]);
                        nom = Convert.ToString(dataReader["nom"]);
                        sexe = Convert.ToString(dataReader["sexe"]);
                        date = Convert.ToDateTime(dataReader["datenaissance"]);
                        lieuNaiss = Convert.ToString(dataReader["lieunaissance"]);
                        photo = Convert.ToString(dataReader["photo"]);
                        nomPere = Convert.ToString(dataReader["nompere"]);
                        nomMere = Convert.ToString(dataReader["nommere"]);
                        telephone = Convert.ToString(dataReader["telephone"]);
                        telParent = Convert.ToString(dataReader["telparent"]);
                        email = Convert.ToString(dataReader["email"]); 
                        adresse = Convert.ToString(dataReader["adresse"]);

                        fonctionPere = Convert.ToString(dataReader["FONCTION_PERE"]);
                        fonctionMere = Convert.ToString(dataReader["FONCTION_MERE"]);
                        situationMedicale = Convert.ToString(dataReader["SITUATION_MEDICALE"]);
                        etat = Convert.ToString(dataReader["etat"]);

                        e = new EleveBE(matricule, codepays, codedept, coderegion, nom, sexe, date, lieuNaiss, Convert.ToString(dataReader["langue"]), photo, nomPere, nomMere, telephone, telParent,
                            email, adresse, Convert.ToString(dataReader["diplome"]), Convert.ToInt32(dataReader["anneediplome"]));

                        e.fonctionPere = fonctionPere;
                        e.fonctionMere = fonctionMere;
                        e.situationMedicale = situationMedicale;
                        e.etat = etat;

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

        public List<EleveBE> listeEleveDunCycle(string codecycle, int annee)
        {
            List<EleveBE> list = new List<EleveBE>();
            string matricule;
            string codepays;
            string codedept;
            string coderegion;
            string nom;
            string sexe;
            DateTime date;
            String lieuNaiss;
            String photo;
            String nomPere;
            String nomMere;
            String telephone;
            String telParent;
            String email;
            String adresse;

            String fonctionPere;
            String fonctionMere;
            String situationMedicale;
            String etat;

            EleveBE e;

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "select e.* from eleve e, inscrire i, classe c "
                                 + " where e.matricule = i.matricule AND i.annee = '" + annee + "' and i.codeclasse = c.codeclasse and c.codecycle=" + "'" + codecycle + "'"; ;

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet à retourner
                    while (dataReader.Read())
                    {
                        matricule = Convert.ToString(dataReader["matricule"]);
                        codepays = Convert.ToString(dataReader["codepays"]);
                        codedept = Convert.ToString(dataReader["codedept"]);
                        coderegion = Convert.ToString(dataReader["coderegion"]);
                        nom = Convert.ToString(dataReader["nom"]);
                        sexe = Convert.ToString(dataReader["sexe"]);
                        date = Convert.ToDateTime(dataReader["datenaissance"]);
                        lieuNaiss = Convert.ToString(dataReader["lieunaissance"]);
                        photo = Convert.ToString(dataReader["photo"]);
                        nomPere = Convert.ToString(dataReader["nompere"]);
                        nomMere = Convert.ToString(dataReader["nommere"]);
                        telephone = Convert.ToString(dataReader["telephone"]);
                        telParent = Convert.ToString(dataReader["telparent"]);
                        email = Convert.ToString(dataReader["email"]);
                        adresse = Convert.ToString(dataReader["adresse"]);

                        fonctionPere = Convert.ToString(dataReader["FONCTION_PERE"]);
                        fonctionMere = Convert.ToString(dataReader["FONCTION_MERE"]);
                        situationMedicale = Convert.ToString(dataReader["SITUATION_MEDICALE"]);
                        etat = Convert.ToString(dataReader["etat"]);

                        e = new EleveBE(matricule, codepays, codedept, coderegion, nom, sexe, date, lieuNaiss, Convert.ToString(dataReader["langue"]), photo, nomPere, nomMere, telephone, telParent,
                            email, adresse, Convert.ToString(dataReader["diplome"]), Convert.ToInt32(dataReader["anneediplome"]));

                        e.fonctionPere = fonctionPere;
                        e.fonctionMere = fonctionMere;
                        e.etat = etat;

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

        public List<EleveBE> listeEleveDuneSerie(string codeserie, int annee)
        {
            List<EleveBE> list = new List<EleveBE>();
            string matricule;
            string codepays;
            string codedept;
            string coderegion;
            string nom;
            string sexe;
            DateTime date;
            String lieuNaiss;
            String photo;
            String nomPere;
            String nomMere;
            String telephone;
            String telParent;
            String email;
            String adresse;

            String fonctionPere;
            String fonctionMere;
            String situationMedicale;
            String etat;

            EleveBE e;

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "select e.* from eleve e, inscrire i, classe c "
                                 + " where e.matricule = i.matricule AND i.annee = '" + annee + "' and i.codeclasse = c.codeclasse and c.codeserie=" + "'" + codeserie + "'"; ;

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet à retourner
                    while (dataReader.Read())
                    {
                        matricule = Convert.ToString(dataReader["matricule"]);
                        codepays = Convert.ToString(dataReader["codepays"]);
                        codedept = Convert.ToString(dataReader["codedept"]);
                        coderegion = Convert.ToString(dataReader["coderegion"]);
                        nom = Convert.ToString(dataReader["nom"]);
                        sexe = Convert.ToString(dataReader["sexe"]);
                        date = Convert.ToDateTime(dataReader["datenaissance"]);
                        lieuNaiss = Convert.ToString(dataReader["lieunaissance"]);
                        photo = Convert.ToString(dataReader["photo"]);
                        nomPere = Convert.ToString(dataReader["nompere"]);
                        nomMere = Convert.ToString(dataReader["nommere"]);
                        telephone = Convert.ToString(dataReader["telephone"]);
                        telParent = Convert.ToString(dataReader["telparent"]);
                        email = Convert.ToString(dataReader["email"]);
                        adresse = Convert.ToString(dataReader["adresse"]);

                        fonctionPere = Convert.ToString(dataReader["FONCTION_PERE"]);
                        fonctionMere = Convert.ToString(dataReader["FONCTION_MERE"]);
                        situationMedicale = Convert.ToString(dataReader["SITUATION_MEDICALE"]);
                        etat = Convert.ToString(dataReader["etat"]);

                        e = new EleveBE(matricule, codepays, codedept, coderegion, nom, sexe, date, lieuNaiss, Convert.ToString(dataReader["langue"]), photo, nomPere, nomMere, telephone, telParent,
                            email, adresse, Convert.ToString(dataReader["diplome"]), Convert.ToInt32(dataReader["anneediplome"]));

                        e.fonctionPere = fonctionPere;
                        e.fonctionMere = fonctionMere;
                        e.etat = etat;

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

        public List<EleveBE> listeEleveDuneClasse(string codeclasse, int annee)
        {
            List<EleveBE> list = new List<EleveBE>();
            string matricule;
            string codepays;
            string codedept;
            string coderegion;
            string nom;
            string sexe;
            DateTime date;
            String lieuNaiss;
            String photo;
            String nomPere;
            String nomMere;
            String telephone;
            String telParent;
            String email;
            String adresse;

            String fonctionPere;
            String fonctionMere;
            String situationMedicale;
            String etat;

            EleveBE e;

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "select e.* from eleve e, inscrire i, classe c "
                                 + " where e.matricule = i.matricule AND i.annee = '" + annee + "' and i.codeclasse = c.codeclasse and c.codeclasse=" + "'" + codeclasse + "'";

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet à retourner
                    while (dataReader.Read())
                    {
                        matricule = Convert.ToString(dataReader["matricule"]);
                        codepays = Convert.ToString(dataReader["codepays"]);
                        codedept = Convert.ToString(dataReader["codedept"]);
                        coderegion = Convert.ToString(dataReader["coderegion"]);
                        nom = Convert.ToString(dataReader["nom"]);
                        sexe = Convert.ToString(dataReader["sexe"]);
                        date = Convert.ToDateTime(dataReader["datenaissance"]);
                        lieuNaiss = Convert.ToString(dataReader["lieunaissance"]);
                        photo = Convert.ToString(dataReader["photo"]);
                        nomPere = Convert.ToString(dataReader["nompere"]);
                        nomMere = Convert.ToString(dataReader["nommere"]);
                        telephone = Convert.ToString(dataReader["telephone"]);
                        telParent = Convert.ToString(dataReader["telparent"]);
                        email = Convert.ToString(dataReader["email"]);
                        adresse = Convert.ToString(dataReader["adresse"]);

                        fonctionPere = Convert.ToString(dataReader["FONCTION_PERE"]);
                        fonctionMere = Convert.ToString(dataReader["FONCTION_MERE"]);
                        situationMedicale = Convert.ToString(dataReader["SITUATION_MEDICALE"]);
                        etat = Convert.ToString(dataReader["etat"]);

                        e = new EleveBE(matricule, codepays, codedept, coderegion, nom, sexe, date, lieuNaiss, Convert.ToString(dataReader["langue"]), photo, nomPere, nomMere, telephone, telParent,
                            email, adresse, Convert.ToString(dataReader["diplome"]), Convert.ToInt32(dataReader["anneediplome"]));

                        e.fonctionPere = fonctionPere;
                        e.fonctionMere = fonctionMere;
                        e.situationMedicale = situationMedicale;
                        e.etat = etat;

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

        public List<EleveBE> listeEleveDuneAnnee(int annee)
        {
            List<EleveBE> list = new List<EleveBE>();
            string matricule;
            string codepays;
            string codedept;
            string coderegion;
            string nom;
            string sexe;
            DateTime date;
            String lieuNaiss;
            String photo;
            String nomPere;
            String nomMere;
            String telephone;
            String telParent;
            String email;
            String adresse;

            String fonctionPere;
            String fonctionMere;
            String situationMedicale;
            String etat;

            EleveBE e;

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "select e.* from eleve e, inscrire i"
                                 + " where e.matricule = i.matricule AND i.annee = '" + annee + "'";

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet à retourner
                    while (dataReader.Read())
                    {
                        matricule = Convert.ToString(dataReader["matricule"]);
                        codepays = Convert.ToString(dataReader["codepays"]);
                        codedept = Convert.ToString(dataReader["codedept"]);
                        coderegion = Convert.ToString(dataReader["coderegion"]);
                        nom = Convert.ToString(dataReader["nom"]);
                        sexe = Convert.ToString(dataReader["sexe"]);
                        date = Convert.ToDateTime(dataReader["datenaissance"]);
                        lieuNaiss = Convert.ToString(dataReader["lieunaissance"]);
                        photo = Convert.ToString(dataReader["photo"]);
                        nomPere = Convert.ToString(dataReader["nompere"]);
                        nomMere = Convert.ToString(dataReader["nommere"]);
                        telephone = Convert.ToString(dataReader["telephone"]);
                        telParent = Convert.ToString(dataReader["telparent"]);
                        email = Convert.ToString(dataReader["email"]);
                        adresse = Convert.ToString(dataReader["adresse"]);

                        fonctionPere = Convert.ToString(dataReader["FONCTION_PERE"]);
                        fonctionMere = Convert.ToString(dataReader["FONCTION_MERE"]);
                        situationMedicale = Convert.ToString(dataReader["SITUATION_MEDICALE"]);
                        etat = Convert.ToString(dataReader["etat"]);

                        e = new EleveBE(matricule, codepays, codedept, coderegion, nom, sexe, date, lieuNaiss, Convert.ToString(dataReader["langue"]), photo, nomPere, nomMere, telephone, telParent,
                            email, adresse, Convert.ToString(dataReader["diplome"]), Convert.ToInt32(dataReader["anneediplome"]));

                        e.fonctionPere = fonctionPere;
                        e.fonctionMere = fonctionMere;
                        e.situationMedicale = situationMedicale;
                        e.etat = etat;

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
    }

}
