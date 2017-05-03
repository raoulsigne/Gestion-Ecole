using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using Ecole.BusinessEntity;
using System.Globalization;
using System.Threading;
using Ecole.UI;


namespace Ecole.DataAccess
{
    public class EnseignantDA : DA<EnseignantBE>
    {
        private Connexion con = Connexion.getConnexion();

        //----------------ajouter -----------------//
        public override Boolean ajouter(EnseignantBE entity)
        {
            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "yyyy-MM-dd";
            Thread.CurrentThread.CurrentCulture = ci;

            MySqlTransaction tx = con.connexion.BeginTransaction();
            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "INSERT INTO Enseignant (codeprof, nomprof, datenais, tel, email, ville, dateembauche, datedepart, statut, PHOTO) VALUES (@code, @nom, @dateNaiss, @tel, @email, @ville, @dateembauche, @datedepart, @statut, @photo)";
                cmd.Parameters.AddWithValue("@code", entity.codeProf);
                cmd.Parameters.AddWithValue("@nom", entity.nomProf);
                cmd.Parameters.AddWithValue("@dateNaiss", entity.dateNaissance);
                cmd.Parameters.AddWithValue("@tel", entity.tel);
                cmd.Parameters.AddWithValue("@email", entity.email); 
                cmd.Parameters.AddWithValue("@ville", entity.ville);
                cmd.Parameters.AddWithValue("@dateembauche", entity.dateEmbauche);
                cmd.Parameters.AddWithValue("@datedepart", entity.dateDepart);
                cmd.Parameters.AddWithValue("@statut", entity.statut);
                cmd.Parameters.AddWithValue("@photo", entity.photo);

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
        public override Boolean supprimer(EnseignantBE entity)
        {
            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "DELETE FROM Enseignant WHERE codeprof = @code";
                cmd.Parameters.AddWithValue("@code", entity.codeProf);

                // Exécution de la commande SQL
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        //----------------fin supprimer ---------------------//

        //----------------chercher Acheter -----------------//
        public override EnseignantBE rechercher(EnseignantBE entity)
        {
            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "yyyy-MM-dd";
            Thread.CurrentThread.CurrentCulture = ci; 

            string codeprof;
            string nomprof;
            DateTime dateNaiss;
            String telephone;
            String email;
            String ville;
            DateTime dateEmbauche;
            DateTime dateDepart;
            String statut;
            String photo;
            EnseignantBE e;

            try
            {
                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT * FROM Enseignant WHERE codeprof=@code";
                cmd.Parameters.AddWithValue("@code", entity.codeProf);

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet acheter à retourner
                    if (dataReader.Read())
                    {
                        codeprof = Convert.ToString(dataReader["codeprof"]);
                        nomprof = Convert.ToString(dataReader["nomprof"]);
                        dateNaiss = Convert.ToDateTime(dataReader["datenais"]);
                        email = Convert.ToString(dataReader["email"]);
                        ville = Convert.ToString(dataReader["ville"]);
                        telephone = Convert.ToString(dataReader["tel"]);
                        dateEmbauche = Convert.ToDateTime(dataReader["DATEEMBAUCHE"]);
                        dateDepart = Convert.ToDateTime(dataReader["DATEDEPART"]);
                        statut = Convert.ToString(dataReader["statut"]);
                        photo = Convert.ToString(dataReader["PHOTO"]);

                        e = new EnseignantBE(codeprof, nomprof, dateNaiss, telephone, email, ville, dateEmbauche, dateDepart);

                        e.statut = statut;
                        e.photo = photo;

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
        public override Boolean modifier(EnseignantBE entity, EnseignantBE newEntity)
        {
            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "yyyy-MM-dd";
            Thread.CurrentThread.CurrentCulture = ci;

            MySqlTransaction tx = con.connexion.BeginTransaction();
            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                cmd.CommandText = "UPDATE enseignant SET codeprof=@code, nomprof=@nom, datenais=@dateNaiss, tel=@tel, email=@email, ville=@ville, dateembauche=@dateembauche, datedepart=@datedepart, statut=@statut, PHOTO=@photo WHERE codeprof=@codeprof";

                // utilisation de l'objet voiture passé en paramètre
                cmd.Parameters.AddWithValue("@code", newEntity.codeProf);
                cmd.Parameters.AddWithValue("@nom", newEntity.nomProf);
                cmd.Parameters.AddWithValue("@dateNaiss", newEntity.dateNaissance);
                cmd.Parameters.AddWithValue("@tel", newEntity.tel);
                cmd.Parameters.AddWithValue("@email", newEntity.email);
                cmd.Parameters.AddWithValue("@ville", newEntity.ville);
                cmd.Parameters.AddWithValue("@dateembauche", newEntity.dateEmbauche);
                cmd.Parameters.AddWithValue("@datedepart", newEntity.dateDepart);
                cmd.Parameters.AddWithValue("@statut", newEntity.statut);
                cmd.Parameters.AddWithValue("@photo", newEntity.photo);

                cmd.Parameters.AddWithValue("@codeprof", entity.codeProf);

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
        public override List<EnseignantBE> listerTous()
        {
            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "yyyy-MM-dd";
            Thread.CurrentThread.CurrentCulture = ci;

            List<EnseignantBE> list = new List<EnseignantBE>();
            string codeprof;
            string nomprof;
            DateTime dateNaiss;
            String telephone;
            String email;
            String ville;
            DateTime dateEmbauche;
            DateTime dateDepart;
            String statut;
            String photo;

            EnseignantBE e;

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT * FROM Enseignant ORDER BY nomprof";

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
                        statut = Convert.ToString(dataReader["statut"]);
                        photo = Convert.ToString(dataReader["PHOTO"]);

                        e = new EnseignantBE(codeprof, nomprof, dateNaiss, telephone, email, ville, dateEmbauche, dateDepart);
                        e.statut = statut;
                        e.photo = photo;

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

        public override List<EnseignantBE> listerSuivantCritere(string critere)
        {
            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "yyyy-MM-dd";
            Thread.CurrentThread.CurrentCulture = ci;

            List<EnseignantBE> list = new List<EnseignantBE>();
            string codeprof;
            string nomprof;
            DateTime dateNaiss;
            String telephone;
            String email;
            String ville;
            DateTime dateEmbauche;
            DateTime dateDepart;
            String statut;
            String photo;
            EnseignantBE e;

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT * FROM Enseignant WHERE " + critere;

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
                        statut = Convert.ToString(dataReader["statut"]);
                        photo = Convert.ToString(dataReader["PHOTO"]);

                        e = new EnseignantBE(codeprof, nomprof, dateNaiss, telephone, email, ville, dateEmbauche, dateDepart);
                        e.statut = statut;
                        e.photo = photo;
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
                cmd.CommandText = "SELECT * FROM Enseignant ";

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
                cmd.CommandText = "SELECT COUNT(*) FROM Enseignant";

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

        public Boolean modifier(EnseignantBE obj)
        {
            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "yyyy-MM-dd";
            Thread.CurrentThread.CurrentCulture = ci;

            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "UPDATE enseignant SET nomprof=@nomprof, datenais=@datenais, tel=@tel, email=@email, ville=@ville, DATEEMBAUCHE=@dateEmbauche, DATEDEPART=@dateDepart, statut=@statut, PHOTO=@photo WHERE codeprof=@codeprof";

                // utilisation de l'objet NiveauBE passé en paramètre
                cmd.Parameters.AddWithValue("@nomprof", obj.nomProf);
                cmd.Parameters.AddWithValue("@datenais", obj.dateNaissance);
                cmd.Parameters.AddWithValue("@tel", obj.tel);
                cmd.Parameters.AddWithValue("@email", obj.email);
                cmd.Parameters.AddWithValue("@ville", obj.ville);
                cmd.Parameters.AddWithValue("@codeprof", obj.codeProf);
                cmd.Parameters.AddWithValue("@dateEmbauche", obj.dateEmbauche);
                cmd.Parameters.AddWithValue("@dateDepart", obj.dateDepart);
                cmd.Parameters.AddWithValue("@statut", obj.statut);
                cmd.Parameters.AddWithValue("@photo", obj.photo);

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

        //----------------------------------MOI-------------------------------------------------------
        //-----------retourner le dernier matricule généré--------------------------------------------

        public string getDernierMatricule()
        {
            string matricule = "";
            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT id,CODEPROF FROM enseignant WHERE id = (SELECT MAX(id) FROM enseignant)";

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet à retourner
                    while (dataReader.Read())
                    {
                        matricule = Convert.ToString(dataReader["CODEPROF"]);
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

        internal Dictionary<string, string> contactEnseignant(string codeprof)
        {
            try
            {
                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT tel, email FROM Enseignant "
                                  + "  WHERE codeprof = " + "'" + codeprof + "'";

                Dictionary<string, string> dict = new Dictionary<string, string>();
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        dict.Add(GestionDesNotificationsUI.NUMERO, Convert.ToString(dataReader["tel"]));
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

        internal Dictionary<string, string> contactEnseignants()
        {
            try
            {
                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT tel, email FROM Enseignant ";

                Dictionary<string, string> dict = new Dictionary<string, string>();
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        dict.Add(GestionDesNotificationsUI.NUMERO, Convert.ToString(dataReader["tel"]));
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
    }
}
