using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using Ecole.BusinessEntity;

namespace Ecole.DataAccess
{
    public class ClasseDA : DA<ClasseBE> 
    {
        private Connexion con = Connexion.getConnexion();

        //----------------ajouter -----------------//
        public override Boolean ajouter(ClasseBE entity)
        {
            MySqlTransaction tx = con.connexion.BeginTransaction();
            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "INSERT INTO Classe (codeclasse, codecycle, codetypeClasse, codeserie, codeniveau, nomclasse) VALUES (@classe, @cycle, @typeClasse, @serie, @niveau, @nom)";
                cmd.Parameters.AddWithValue("@classe", entity.codeClasse);
                cmd.Parameters.AddWithValue("@cycle", entity.codeCycle);
                cmd.Parameters.AddWithValue("@typeclasse", entity.codeTypeClasse);
                cmd.Parameters.AddWithValue("@serie", entity.codeSerie);
                cmd.Parameters.AddWithValue("@niveau", entity.codeNiveau);
                cmd.Parameters.AddWithValue("@nom", entity.nomClasse);
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
        public override Boolean supprimer(ClasseBE entity)
        {
            MySqlTransaction tx = con.connexion.BeginTransaction();
            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "DELETE FROM Classe WHERE codeclasse = @code";
                cmd.Parameters.AddWithValue("@code", entity.codeClasse);

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
        public override ClasseBE rechercher(ClasseBE entity)
        {
            string codeClasse ;
            string codeTypeClasse ;
            string codeCycle ;
            string codeSerie ;
            string codeNiveau ;
            string nomClasse ;
            ClasseBE c;

            try
            {
                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT * FROM Classe WHERE codeclasse=@code";
                cmd.Parameters.AddWithValue("@code", entity.codeClasse);

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet acheter à retourner
                    if (dataReader.Read())
                    {
                        codeClasse = Convert.ToString(dataReader["codeclasse"]);
                        codeTypeClasse = Convert.ToString(dataReader["codetypeclasse"]);
                        codeCycle = Convert.ToString(dataReader["codecycle"]);
                        codeSerie = Convert.ToString(dataReader["codeserie"]);
                        codeNiveau = Convert.ToString(dataReader["codeniveau"]);
                        nomClasse = Convert.ToString(dataReader["nomclasse"]);

                        c = new ClasseBE(codeClasse, codeCycle, codeTypeClasse, codeSerie,codeNiveau,nomClasse);
                        dataReader.Close();
                        return c;
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
        public override Boolean modifier(ClasseBE entity, ClasseBE newEntity)
        {
            MySqlTransaction tx = con.connexion.BeginTransaction();
            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                cmd.CommandText = "UPDATE classe SET codeclasse=@classe, codecycle=@cycle, codetypeClasse=@typeclasse, codeserie=@serie, codeniveau=@niveau, nomclasse=@nom WHERE codeclasse=@codeclasse";

                // utilisation de l'objet voiture passé en paramètre
                cmd.Parameters.AddWithValue("@classe", newEntity.codeClasse);
                cmd.Parameters.AddWithValue("@cycle", newEntity.codeCycle);
                cmd.Parameters.AddWithValue("@typeclasse", newEntity.codeTypeClasse);
                cmd.Parameters.AddWithValue("@serie", newEntity.codeSerie);
                cmd.Parameters.AddWithValue("@niveau", newEntity.codeNiveau);
                cmd.Parameters.AddWithValue("@nom", newEntity.nomClasse);

                cmd.Parameters.AddWithValue("@codeclasse", entity.codeClasse);

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
        public override List<ClasseBE> listerTous()
        {
            List<ClasseBE> list = new List<ClasseBE>();
            string codeClasse;
            string codeTypeClasse;
            string codeCycle;
            string codeSerie;
            string codeNiveau;
            string nomClasse;
            ClasseBE c;

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT * FROM classe c, niveau n where c.codeniveau = n.codeniveau order by n.niveau;";

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet à retourner
                    while (dataReader.Read())
                    {
                        codeClasse = Convert.ToString(dataReader["codeclasse"]);
                        codeTypeClasse = Convert.ToString(dataReader["codetypeclasse"]);
                        codeCycle = Convert.ToString(dataReader["codecycle"]);
                        codeSerie = Convert.ToString(dataReader["codeserie"]);
                        codeNiveau = Convert.ToString(dataReader["codeniveau"]);
                        nomClasse = Convert.ToString(dataReader["nomclasse"]);

                        c = new ClasseBE(codeClasse, codeCycle, codeTypeClasse, codeSerie, codeNiveau, nomClasse);
                        list.Add(c);
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

        public List<ClasseBE> listerTousOrderByNiveau()
        {
            List<ClasseBE> list = new List<ClasseBE>();
            string codeClasse;
            string codeTypeClasse;
            string codeCycle;
            string codeSerie;
            string codeNiveau;
            string nomClasse;
            ClasseBE c;

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT * FROM Classe c, niveau n WHERE c.codeniveau = n.codeniveau ORDER BY n.niveau ASC";

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet à retourner
                    while (dataReader.Read())
                    {
                        codeClasse = Convert.ToString(dataReader["codeclasse"]);
                        codeTypeClasse = Convert.ToString(dataReader["codetypeclasse"]);
                        codeCycle = Convert.ToString(dataReader["codecycle"]);
                        codeSerie = Convert.ToString(dataReader["codeserie"]);
                        codeNiveau = Convert.ToString(dataReader["codeniveau"]);
                        nomClasse = Convert.ToString(dataReader["nomclasse"]);

                        c = new ClasseBE(codeClasse, codeCycle, codeTypeClasse, codeSerie, codeNiveau, nomClasse);
                        list.Add(c);
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

        public override List<ClasseBE> listerSuivantCritere(string critere)
        {
            List<ClasseBE> list = new List<ClasseBE>();
            string codeClasse;
            string codeTypeClasse;
            string codeCycle;
            string codeSerie;
            string codeNiveau;
            string nomClasse;
            ClasseBE c;

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT * FROM Classe WHERE " + critere;

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet à retourner
                    while (dataReader.Read())
                    {
                        codeClasse = Convert.ToString(dataReader["codeclasse"]);
                        codeTypeClasse = Convert.ToString(dataReader["codetypeclasse"]);
                        codeCycle = Convert.ToString(dataReader["codecycle"]);
                        codeSerie = Convert.ToString(dataReader["codeserie"]);
                        codeNiveau = Convert.ToString(dataReader["codeniveau"]);
                        nomClasse = Convert.ToString(dataReader["nomclasse"]);

                        c = new ClasseBE(codeClasse, codeCycle, codeTypeClasse, codeSerie, codeNiveau, nomClasse);
                        list.Add(c);
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
                cmd.CommandText = "SELECT * FROM Classe c, niveau n WHERE c.codeniveau=n.codeniveau ORDER BY niveau,codeclasse";

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
                cmd.CommandText = "SELECT COUNT(*) FROM Classe";

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

        // retourne la liste des enseignants d'une classe pour une année
        public List<EnseignantBE> listeEnseignants(ClasseBE classe, int annee)
        {
            List<EnseignantBE> list = new List<EnseignantBE>();
            string codeprof;
            string nomprof;
            DateTime dateNaiss;
            String telephone;
            String email;
            String ville;
            DateTime dateEmbauche;
            DateTime dateDepart;

            EnseignantBE e;

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT DISTINCT e.codeprof, e.nomprof, e.datenais, e.tel, e.email, e.ville, e.dateembauche, e.datedepart FROM Enseignant e, "
                                  + " Programmer p, Classe c WHERE p.codeprof = e.codeprof AND p.codeclasse = c.codeclasse AND c.codeclasse = @codeClasse AND p.annee = @annee"
                                  + " ORDER BY e.nomprof";

                cmd.Parameters.AddWithValue("@codeClasse", classe.codeClasse);
                cmd.Parameters.AddWithValue("@annee", annee);

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet à retourner
                    while (dataReader.Read())
                    {
                        codeprof = Convert.ToString(dataReader["codeprof"]);
                        nomprof = Convert.ToString(dataReader["nomprof"]);
                        dateNaiss = Convert.ToDateTime(dataReader["datenais"]);
                        email = Convert.ToString(dataReader["email"]);
                        ville = Convert.ToString(dataReader["ville"]);
                        telephone = Convert.ToString(dataReader["tel"]);
                        dateEmbauche = Convert.ToDateTime(dataReader["DATEEMBAUCHE"]);
                        dateDepart = Convert.ToDateTime(dataReader["DATEDEPART"]);

                        e = new EnseignantBE(codeprof, nomprof, dateNaiss, telephone, email, ville, dateEmbauche, dateDepart);
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

        public Boolean modifier(ClasseBE S)
        {
            try
            {

                // Création d'un  commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "UPDATE classe SET codetypeclasse=@codetypeclasse, codecycle=@codecycle, codeserie=@codeserie, codeniveau=@codeniveau, nomclasse=@nomclasse WHERE codeclasse=@codeclasse";

                // utilisation de l'objet voiture passé en paramètre
                cmd.Parameters.AddWithValue("@codetypeclasse", S.codeTypeClasse);
                cmd.Parameters.AddWithValue("@codecycle", S.codeCycle);
                cmd.Parameters.AddWithValue("@codeserie", S.codeSerie);
                cmd.Parameters.AddWithValue("@codeniveau", S.codeNiveau);
                cmd.Parameters.AddWithValue("@nomclasse", S.nomClasse);
                cmd.Parameters.AddWithValue("@codeclasse", S.codeClasse);

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

        //liste les matières d'une classe pour une année
        public List<MatiereBE> ListeMatiereDuneClasse(ClasseBE classe, int Annee)
        {
            List<MatiereBE> list = new List<MatiereBE>();
            string codemat;
            string nommat;
            string namemat;
            int annee;

            MatiereBE m;

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT m.codemat, m.nommat, m.namemat, m.annee FROM Classe c, Programmer p, Matiere m WHERE p.codeclasse = c.codeclasse AND p.codemat = m.codemat AND c.codeclasse = @codeClasse AND p.annee = @annee";

                cmd.Parameters.AddWithValue("@codeClasse", classe.codeClasse);
                cmd.Parameters.AddWithValue("@annee", Annee);

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet à retourner
                    while (dataReader.Read())
                    {
                        codemat = Convert.ToString(dataReader["codemat"]);
                        nommat = Convert.ToString(dataReader["nommat"]);
                        namemat = Convert.ToString(dataReader["namemat"]);
                        annee = Convert.ToInt16(dataReader["annee"]);

                        m = new MatiereBE(codemat, nommat, namemat, annee);
                        list.Add(m);
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

        // liste les élèves d'une classe à une année
        public List<EleveBE> listeEleves(ClasseBE classe, int annee)
        {
            List<EleveBE> list = new List<EleveBE>();
            string matricule;
            string codePays;
            String codeDept;
            String codeRegion;
            String nom;
            String sexe;
            DateTime dateNaissance;
            String lieuNaissance;
            String photo;
            String nomPere;
            String nomMere;
            String telephone;
            String telParent;
            String email;
            String adresse;
            String langue;
            String diplome;
            int anneeDiplome;
            String etat;

            EleveBE e;

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT e.matricule, e.codepays, e.codedept, e.coderegion, e.nom, e.sexe, e.datenaissance, e.lieunaissance, e.photo, e.nompere, e.nommere, e.telephone, e.telparent, e.email, e.adresse, e.langue, e.diplome, e.anneediplome, e.etat FROM Eleve e, Inscrire i, Classe c WHERE e.matricule = i.matricule AND i.codeclasse = c.codeclasse AND i.annee = @annee AND c.codeclasse = @codeClasse order by e.nom";

                cmd.Parameters.AddWithValue("@codeClasse", classe.codeClasse);
                cmd.Parameters.AddWithValue("@annee", annee);

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet à retourner
                    while (dataReader.Read())
                    {
                        matricule = Convert.ToString(dataReader["matricule"]);
                        codePays = Convert.ToString(dataReader["codepays"]);
                        codeDept = Convert.ToString(dataReader["codedept"]);
                        codeRegion = Convert.ToString(dataReader["coderegion"]);
                        nom = Convert.ToString(dataReader["nom"]);
                        sexe = Convert.ToString(dataReader["sexe"]);
                        dateNaissance = Convert.ToDateTime(dataReader["datenaissance"]);
                        lieuNaissance = Convert.ToString(dataReader["lieunaissance"]);
                        photo = Convert.ToString(dataReader["photo"]);
                        nomPere = Convert.ToString(dataReader["nompere"]);
                        nomMere = Convert.ToString(dataReader["nommere"]);
                        telephone = Convert.ToString(dataReader["telephone"]);
                        telParent = Convert.ToString(dataReader["telparent"]);
                        email = Convert.ToString(dataReader["email"]);
                        adresse = Convert.ToString(dataReader["adresse"]);

                        langue = Convert.ToString(dataReader["langue"]);
                        diplome = Convert.ToString(dataReader["diplome"]);
                        anneeDiplome = Convert.ToInt16(dataReader["anneediplome"]);
                        etat = Convert.ToString(dataReader["etat"]);

                        e = new EleveBE(matricule, codePays, codeDept, codeRegion, nom, sexe, dateNaissance, lieuNaissance, langue, photo, nomPere, nomMere, telephone, telParent,
                            email, adresse, diplome, anneeDiplome);

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

        // recherché la classe d'un élève à une année
        public String getClasseEleve(String matricule, int annee)
        {
            string codeClasse;
            
            InscrireBE c;

            try
            {
                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT * FROM inscrire WHERE matricule=@matricule AND annee=@annee";
                cmd.Parameters.AddWithValue("@matricule", matricule);
                cmd.Parameters.AddWithValue("@annee", annee);

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet acheter à retourner
                    if (dataReader.Read())
                    {
                        codeClasse = Convert.ToString(dataReader["codeclasse"]);

                        c = new InscrireBE(codeClasse, matricule, annee);
                        dataReader.Close();
                        return codeClasse;
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


        // rechercher une classe en fonction d'un niveau
        public List<String> getCodeClasseByNiveau(int niveau)
        {
            String codeClasse;
            List<String> list = new List<String>();

            try
            {
                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT c.codeclasse FROM classe c, niveau n WHERE c.codeniveau = n.codeniveau AND n.niveau = '"+niveau+"'";
                

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet acheter à retourner
                    while (dataReader.Read())
                    {
                        codeClasse = Convert.ToString(dataReader["codeclasse"]);
                        list.Add(codeClasse);
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

        public String getCodeProfTitulaireDuneClasse(String codeClasse, int annee)
        {
            String codeprof = null;

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT codeprof FROM diriger WHERE codeclasse = '" + codeClasse + "' AND annee ='" + annee + "'";

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet à retourner
                    if (dataReader.Read())
                    {
                        codeprof = Convert.ToString(dataReader["codeprof"]);

                        dataReader.Close();

                        return codeprof;
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

        // compte le nombre d'élèves d'une classe à une année
        public int getEffectifClasse(String codeClasse, int annee)
        {
            int nbre = 0;

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT Count(e.matricule) FROM Eleve e, Inscrire i, Classe c WHERE e.matricule = i.matricule AND i.codeclasse = c.codeclasse AND i.annee = @annee AND c.codeclasse = @codeClasse";

                cmd.Parameters.AddWithValue("@codeClasse", codeClasse);
                cmd.Parameters.AddWithValue("@annee", annee);

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet à retourner
                    if (dataReader.Read())
                    {
                        nbre = Convert.ToInt16(dataReader["Count(e.matricule)"]);

                        dataReader.Close();

                        return nbre;
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


        
    }
}
