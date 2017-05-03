using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Ecole.BusinessEntity;
using MySql.Data.MySqlClient;
using System.Globalization;
using System.Threading;

namespace Ecole.DataAccess
{
    public class StockerDA : DA<StockerBE>
    {
        private Connexion con = Connexion.getConnexion();

        //----------------Ajout d'un nouveau Stock ------------------------------
        public override Boolean ajouter(StockerBE S)
        {
            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "yyyy-MM-dd";
            Thread.CurrentThread.CurrentCulture = ci;

            

            MySqlTransaction transaction = con.connexion.BeginTransaction();
            try
            {

                // Création d'un commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "INSERT INTO stocker (CODEMAGASIN, CODEARTICLE, STOCKDEBUT, QUANTITEACHETEE, QUANTITEVENDUE, DATEOPERATION, ANNEE, PUARTICLE, STOCKRESTANT) VALUES (@codeM, @codeA, @stockDebut, @qutAchetee, @qutVendue, @dateOperation, @annee, @puArticle, @stockRestant)";

                // utilisation de l'objet voiture passé en paramètre
                cmd.Parameters.AddWithValue("@codeM", S.codeMagasin);
                cmd.Parameters.AddWithValue("@codeA", S.codeArticle);
                cmd.Parameters.AddWithValue("@stockDebut", S.stockDebut);
                cmd.Parameters.AddWithValue("@qutAchetee", S.quantiteAchetee);
                cmd.Parameters.AddWithValue("@qutVendue", S.quantiteVendue);
                cmd.Parameters.AddWithValue("@dateOperation", S.dateOperation);
                cmd.Parameters.AddWithValue("@annee", S.annee);
                cmd.Parameters.AddWithValue("@puArticle", S.puArticle);
                cmd.Parameters.AddWithValue("@stockRestant", S.stockRestant);

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

        public Boolean ajouter(StockerBE S, int numerovente)
        {
            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "yyyy-MM-dd";
            Thread.CurrentThread.CurrentCulture = ci;
            MySqlTransaction transaction = con.connexion.BeginTransaction();
            try
            {
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "INSERT INTO stocker (CODEMAGASIN, CODEARTICLE, STOCKDEBUT, QUANTITEACHETEE, QUANTITEVENDUE, DATEOPERATION, ANNEE, PUARTICLE, STOCKRESTANT, NUMEROVENTE) "+
                    " VALUES (@codeM, @codeA, @stockDebut, @qutAchetee, @qutVendue, @dateOperation, @annee, @puArticle, @stockRestant, @numerovente)";
                cmd.Parameters.AddWithValue("@codeM", S.codeMagasin);
                cmd.Parameters.AddWithValue("@codeA", S.codeArticle);
                cmd.Parameters.AddWithValue("@stockDebut", S.stockDebut);
                cmd.Parameters.AddWithValue("@qutAchetee", S.quantiteAchetee);
                cmd.Parameters.AddWithValue("@qutVendue", S.quantiteVendue);
                cmd.Parameters.AddWithValue("@dateOperation", S.dateOperation);
                cmd.Parameters.AddWithValue("@annee", S.annee);
                cmd.Parameters.AddWithValue("@puArticle", S.puArticle);
                cmd.Parameters.AddWithValue("@stockRestant", S.stockRestant);
                cmd.Parameters.AddWithValue("@numerovente", numerovente);

                cmd.Transaction = transaction;
                cmd.ExecuteNonQuery();
                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                transaction.Rollback();
                return false;
            }
        }

        //--------------------------Fin ajout-----------------------------

        //--------------------------Suppression d'un Stock ------

        public override Boolean supprimer(StockerBE S)
        {
            MySqlTransaction transaction = con.connexion.BeginTransaction();
            try
            {

                // Création d'un commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "DELETE FROM stocker WHERE NUMERO=@numero";

                // utilisation de l'objet voiture passé en paramètre
                cmd.Parameters.AddWithValue("@numero", S.numero);

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

        //---------------Rechercher des informations sur un Stock spécifique---------------------------------

        public override StockerBE rechercher(StockerBE stocker)
        {
            string codeMagasin;
            string codeArticle;
            Int16 stockDebut;
            Int16 quantiteAchetee ;
            Int16 quantiteVendue;
            DateTime dateOperation ;
            int annee ;
            Int16 puArticle ;
            Int16 stockRestant ;
            StockerBE S;

            try
            {
                // ouverture de la connexion
                // this.con.ouvrir();

                // Création d'un commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT * FROM stocker WHERE NUMERO=@numero";
                cmd.Parameters.AddWithValue("@numero", stocker.numero);

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer la voiture à retourner
                    if (dataReader.Read())
                    {
                        codeMagasin = Convert.ToString(dataReader["CODEMAGASIN"]);
                        codeArticle = Convert.ToString(dataReader["CODEARTICLE"]);
                        stockDebut = Convert.ToInt16(dataReader["STOCKDEBUT"]);
                        quantiteAchetee = Convert.ToInt16(dataReader["QUANTITEACHETEE"]);
                        quantiteVendue = Convert.ToInt16(dataReader["QUANTITEVENDUE"]);
                        dateOperation = Convert.ToDateTime(dataReader["DATEOPERATION"]);
                        annee = Convert.ToInt16(dataReader["ANNEE"]);
                        puArticle = Convert.ToInt16(dataReader["PUARTICLE"]);
                        stockRestant = Convert.ToInt16(dataReader["STOCKRESTANT"]);

                        S = new StockerBE(stocker.codeMagasin, stocker.codeArticle, stocker.stockDebut, stocker.quantiteAchetee, stocker.quantiteVendue, stocker.dateOperation,
            stocker.annee, stocker.puArticle, stocker.stockRestant);
                        S.numero = Convert.ToInt16(dataReader["NUMERO"]);
                        dataReader.Close();
                        // this.con.fermer();
                        return S;
                    }


                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

            //-----------------------------Fin de la recherche----------
        }

        public List<StockerBE> rechercherParNumeroVente(int numerovente)
        {
            string codeMagasin;
            string codeArticle;
            Int16 stockDebut;
            Int16 quantiteAchetee;
            Int16 quantiteVendue;
            DateTime dateOperation;
            int annee;
            Int16 puArticle;
            Int16 stockRestant;
            StockerBE S;
            List<StockerBE> stockers = new List<StockerBE>();

            try
            {
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT * FROM stocker s where numerovente = @numero and quantitevendue > 0 "
                                  + " and dateoperation = (select max(dateoperation) from stocker where numerovente = @numero and quantitevendue > 0);";
                cmd.Parameters.AddWithValue("@numero", numerovente);

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        codeMagasin = Convert.ToString(dataReader["CODEMAGASIN"]);
                        codeArticle = Convert.ToString(dataReader["CODEARTICLE"]);
                        stockDebut = Convert.ToInt16(dataReader["STOCKDEBUT"]);
                        quantiteAchetee = Convert.ToInt16(dataReader["QUANTITEACHETEE"]);
                        quantiteVendue = Convert.ToInt16(dataReader["QUANTITEVENDUE"]);
                        dateOperation = Convert.ToDateTime(dataReader["DATEOPERATION"]);
                        annee = Convert.ToInt16(dataReader["ANNEE"]);
                        puArticle = Convert.ToInt16(dataReader["PUARTICLE"]);
                        stockRestant = Convert.ToInt16(dataReader["STOCKRESTANT"]);

                        S = new StockerBE(codeMagasin, codeArticle, stockDebut, quantiteAchetee, quantiteVendue, dateOperation,
                                    annee, puArticle, stockRestant, Convert.ToInt32(dataReader["NUMEROVENTE"]));
                        S.numero = Convert.ToInt16(dataReader["NUMERO"]);

                        stockers.Add(S);
                    }

                    return stockers;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        // modifier un objet
        public override Boolean modifier(StockerBE entity, StockerBE newEntity)
        {
            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "yyyy-MM-dd";
            Thread.CurrentThread.CurrentCulture = ci;

            MySqlTransaction transaction = con.connexion.BeginTransaction();
            try
            {

                // Création d'un commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "UPDATE stocker SET CODEARTICLE=@codeA, CODEMAGASIN=@codeM, STOCKDEBUT=@stockDebut, QUANTITEACHETEE=@qutAchetee, QUANTITEVENDUE=@qutVendue, "
                + "DATEOPERATION=@dateOpeartion, ANNEE=@annee, PUARTICLE=@puArticle, STOCKRESTANT=@stockRestant WHERE NUMERO=@numero";

                // utilisation de l'objet voiture passé en paramètre
                cmd.Parameters.AddWithValue("@codeA", newEntity.codeArticle);
                cmd.Parameters.AddWithValue("@codeM", newEntity.codeMagasin);

                cmd.Parameters.AddWithValue("@numero", entity.numero);

                cmd.Parameters.AddWithValue("@stockDebut", newEntity.stockDebut);
                cmd.Parameters.AddWithValue("@qutAchetee", newEntity.quantiteAchetee);
                cmd.Parameters.AddWithValue("@qutVendue", newEntity.quantiteVendue);
                cmd.Parameters.AddWithValue("@dateOpeartion", newEntity.dateOperation);
                cmd.Parameters.AddWithValue("@annee", newEntity.annee);
                cmd.Parameters.AddWithValue("@puArticle", newEntity.puArticle);
                cmd.Parameters.AddWithValue("@stockRestant", newEntity.stockRestant);
                // Exécution de la commande SQL
                cmd.Transaction = transaction;

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

        // retourner la liste de tout les objets
        public override List<StockerBE> listerTous()
        {
            List<StockerBE> list = new List<StockerBE>();
            string codeMagasin;
            string codeArticle;
            Int16 stockDebut;
            Int16 quantiteAchetee;
            Int16 quantiteVendue;
            DateTime dateOperation;
            int annee;
            Int16 puArticle;
            Int16 stockRestant;

            StockerBE c;

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT * FROM stocker";

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet à retourner
                    while (dataReader.Read())
                    {
                        codeMagasin = Convert.ToString(dataReader["CODEMAGASIN"]);
                        codeArticle = Convert.ToString(dataReader["CODEARTICLE"]);
                        stockDebut = Convert.ToInt16(dataReader["STOCKDEBUT"]);
                        quantiteAchetee = Convert.ToInt16(dataReader["QUANTITEACHETEE"]);
                        quantiteVendue = Convert.ToInt16(dataReader["QUANTITEVENDUE"]);
                        dateOperation = Convert.ToDateTime(dataReader["DATEOPERATION"]);
                        annee = Convert.ToInt16(dataReader["ANNEE"]);
                        puArticle = Convert.ToInt16(dataReader["PUARTICLE"]);
                        stockRestant = Convert.ToInt16(dataReader["STOCKRESTANT"]);


                        c = new StockerBE();
                        c.numero = Convert.ToInt16(dataReader["NUMERO"]);
                        c.codeMagasin = codeMagasin;
                        c.codeArticle = codeArticle;
                        c.stockDebut = stockDebut;
                        c.quantiteAchetee = quantiteAchetee;
                        c.quantiteVendue = quantiteVendue;
                        c.dateOperation = dateOperation;
                        c.annee = annee;
                        c.puArticle = puArticle;
                        c.stockRestant = stockRestant;
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

        // retourner la liste des objets qui correspondent à un certain critère
        public override List<StockerBE> listerSuivantCritere(string critere)
        {
            List<StockerBE> list = new List<StockerBE>();
            string codeMagasin;
            string codeArticle;
            Int16 stockDebut;
            Int16 quantiteAchetee;
            Int16 quantiteVendue;
            DateTime dateOperation;
            int annee;
            Int16 puArticle;
            Int16 stockRestant;

            StockerBE c;

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT * FROM stocker WHERE " + critere;

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet à retourner
                    while (dataReader.Read())
                    {
                        codeMagasin = Convert.ToString(dataReader["CODEMAGASIN"]);
                        codeArticle = Convert.ToString(dataReader["CODEARTICLE"]);
                        stockDebut = Convert.ToInt16(dataReader["STOCKDEBUT"]);
                        quantiteAchetee = Convert.ToInt16(dataReader["QUANTITEACHETEE"]);
                        quantiteVendue = Convert.ToInt16(dataReader["QUANTITEVENDUE"]);
                        dateOperation = Convert.ToDateTime(dataReader["DATEOPERATION"]);
                        annee = Convert.ToInt16(dataReader["ANNEE"]);
                        puArticle = Convert.ToInt16(dataReader["PUARTICLE"]);
                        stockRestant = Convert.ToInt16(dataReader["STOCKRESTANT"]);

                        c = new StockerBE();
                        c.numero = Convert.ToInt16(dataReader["NUMERO"]);
                        c.codeMagasin = codeMagasin;
                        c.codeArticle = codeArticle;
                        c.stockDebut = stockDebut;
                        c.quantiteAchetee = quantiteAchetee;
                        c.quantiteVendue = quantiteVendue;
                        c.dateOperation = dateOperation;
                        c.dateOperationString = dateOperation.ToShortDateString();
                        c.annee = annee;
                        c.puArticle = puArticle;
                        c.stockRestant = stockRestant;
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

        /*
         * lister tous les donnees d'une colonne de la table
         * @param colonne est le nom de la colonne à lister
         */
        public override List<String> listerValeursColonne(String colonne) { return null; }


        public Boolean modifier(StockerBE stocker)
        {
            MySqlTransaction transaction = con.connexion.BeginTransaction();
            try
            {

                // Création d'un commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "UPDATE stocker SET CODEARTICLE=@codeA, CODEMAGASIN=@codeM, STOCKDEBUT=@stockDebut, QUANTITEACHETEE=@qutAchetee, QUANTITEVENDUE=@qutVendue, "
                + "DATEOPERATION=@dateOpeartion, ANNEE=@annee, PUARTICLE=@puArticle, STOCKRESTANT=@stockRestant WHERE NUMERO=@numero";

                // utilisation de l'objet voiture passé en paramètre
                cmd.Parameters.AddWithValue("@codeA", stocker.codeArticle);
                cmd.Parameters.AddWithValue("@codeM", stocker.codeMagasin);
                cmd.Parameters.AddWithValue("@stockDebut", stocker.stockDebut);
                cmd.Parameters.AddWithValue("@qutAchetee", stocker.quantiteAchetee);
                cmd.Parameters.AddWithValue("@qutVendue", stocker.quantiteVendue);
                cmd.Parameters.AddWithValue("@dateOpeartion", stocker.dateOperation);
                cmd.Parameters.AddWithValue("@annee", stocker.annee);
                cmd.Parameters.AddWithValue("@puArticle", stocker.puArticle);
                cmd.Parameters.AddWithValue("@stockRestant", stocker.stockRestant);
                cmd.Parameters.AddWithValue("@numero", stocker.numero);

                // Exécution de la commande SQL
                cmd.Transaction = transaction;
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

        //***************** à reécrire par Raoul ******************

        public bool disponibiliteSetArticle(string codesetarticle, int quantite, int annee)
        {
            try
            {
                MySqlCommand cmd = con.connexion.CreateCommand();

                cmd.CommandText = "select s.codearticle, stockrestant, c.quantite*" + quantite + " as quantite from stocker s, composer c "
                                 + " where c.codesetarticle=" + "'" + codesetarticle + "' and s.codearticle=c.codearticle AND "
                                 + " c.annee = (select max(annee) from composer where codesetarticle = " + "'" + codesetarticle + "') AND "
                                 + " s.numero = (select max(numero) from stocker where s.codearticle=c.codearticle) ";

                int stock;
                int indice = 0;
                int nombre;
                string codearticle;
                bool resultat = true;
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        stock = Convert.ToInt32(dataReader["stockrestant"]);
                        nombre = Convert.ToInt32(dataReader["quantite"]);
                        codearticle = Convert.ToString(dataReader["codearticle"]);
                        indice++;
                        if (stock < nombre)
                            resultat = false;
                    }
                    dataReader.Close();
                }

                int nbArticle;
                ComposerDA composerDA = new ComposerDA();
                nbArticle = composerDA.listerSuivantCritere(" annee = (select max(annee) from composer where codesetarticle = " + "'" + codesetarticle + "') and codesetarticle = " + "'" + codesetarticle + "'").Count;
                if (nbArticle > indice)
                    resultat = false;

                return resultat;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public StockerBE rechercherDernierEnregistrement(StockerBE stocker)
        {
            string codeMagasin;
            string codeArticle;
            Int16 stockDebut;
            Int16 quantiteAchetee;
            Int16 quantiteVendue;
            DateTime dateOperation;
            int annee;
            Int16 puArticle;
            Int16 stockRestant;
            StockerBE S;

            try
            {
                // ouverture de la connexion
                // this.con.ouvrir();

                // Création d'un commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT * FROM stocker " +
                                  " WHERE CODEARTICLE=@code" +
                                  " AND NUMERO=(SELECT MAX(NUMERO) FROM stocker WHERE CODEARTICLE=@code)";
                cmd.Parameters.AddWithValue("@code", stocker.codeArticle);

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet à retourner
                    if (dataReader.Read())
                    {
                        codeMagasin = Convert.ToString(dataReader["CODEMAGASIN"]);
                        codeArticle = Convert.ToString(dataReader["CODEARTICLE"]);
                        stockDebut = Convert.ToInt16(dataReader["STOCKDEBUT"]);
                        quantiteAchetee = Convert.ToInt16(dataReader["QUANTITEACHETEE"]);
                        quantiteVendue = Convert.ToInt16(dataReader["QUANTITEVENDUE"]);
                        dateOperation = Convert.ToDateTime(dataReader["DATEOPERATION"]);
                        annee = Convert.ToInt16(dataReader["ANNEE"]);
                        puArticle = Convert.ToInt16(dataReader["PUARTICLE"]);
                        stockRestant = Convert.ToInt16(dataReader["STOCKRESTANT"]);

                        S = new StockerBE(codeMagasin, codeArticle, stockDebut, quantiteAchetee, quantiteVendue, dateOperation,annee,puArticle, stockRestant);
                        S.numero = Convert.ToInt32(dataReader["NUMERO"]);
                        dataReader.Close();
                        // this.con.fermer();
                        return S;
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

        /******************************** Fin Raoul ********************************/

        //fonction qui recherche le dernier enregistrement dans la table stocker
        public StockerBE dernierEnregistrement(string codeArticle, string codeMagasin)
        {
            int numero;
            //string codeMagasin;
            //string codeArticle;
            Int16 stockDebut;
            Int16 quantiteAchetee;
            Int16 quantiteVendue;
            DateTime dateOperation;
            int annee;
            Int16 puArticle;
            Int16 stockRestant;
            StockerBE S;

            try
            {
                // ouverture de la connexion
                // this.con.ouvrir();

                // Création d'un commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT * FROM stocker WHERE NUMERO = (SELECT MAX(NUMERO) FROM stocker WHERE codearticle = '"+codeArticle+"' AND codeMAgasin = '"+codeMagasin+"')";

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer la voiture à retourner
                    if (dataReader.Read())
                    {
                        codeMagasin = Convert.ToString(dataReader["CODEMAGASIN"]);
                        codeArticle = Convert.ToString(dataReader["CODEARTICLE"]);
                        stockDebut = Convert.ToInt16(dataReader["STOCKDEBUT"]);
                        quantiteAchetee = Convert.ToInt16(dataReader["QUANTITEACHETEE"]);
                        quantiteVendue = Convert.ToInt16(dataReader["QUANTITEVENDUE"]);
                        dateOperation = Convert.ToDateTime(dataReader["DATEOPERATION"]);
                        annee = Convert.ToInt16(dataReader["ANNEE"]);
                        puArticle = Convert.ToInt16(dataReader["PUARTICLE"]);
                        stockRestant = Convert.ToInt16(dataReader["STOCKRESTANT"]);

                        S = new StockerBE(codeMagasin, codeArticle, stockDebut, quantiteAchetee, quantiteVendue, dateOperation,
            annee, puArticle, stockRestant);
                        S.numero = Convert.ToInt16(dataReader["NUMERO"]);
                        dataReader.Close();
                        // this.con.fermer();
                        return S;
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

        //méthode qui permet d'obtenir les informations sur l'état des stocks
        // retourner la liste des objets qui correspondent à un certain critère
        public List<EtatStockBE> etatStock()
        {
            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "yyyy-MM-dd";
            Thread.CurrentThread.CurrentCulture = ci;

            List<EtatStockBE> list = new List<EtatStockBE>();
            string codeMagasin;
            string codeArticle;
            Int16 stockDebut;
            Int16 quantiteAchetee;
            Int16 quantiteVendue;
            DateTime dateOperation;
            int annee;
            Int16 puArticle;
            Int16 stockRestant;

            string designation;

            EtatStockBE c;

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT NUMERO, dateoperation, annee, codemagasin, s.codearticle, a.designation as designation, stockdebut, " +
                    "SUM(quantiteachetee) as quantiteachetee, " +
                    "SUM(quantitevendue) as quantitevendue, " +
                    "(SELECT PUARTICLE " +
                    "FROM stocker " +
                    "WHERE NUMERO = (SELECT MAX(NUMERO) FROM stocker WHERE codearticle = s.codearticle AND codeMAgasin = s.codeMagasin)) as PUARTICLE, " +
                    "(SELECT stockrestant " +
                    "FROM stocker " +
                    "WHERE NUMERO = (SELECT MAX(NUMERO) FROM stocker WHERE codearticle = s.codearticle AND codeMAgasin = s.codeMagasin AND dateOperation = s.dateOperation AND annee = s.annee GROUP BY codearticle, codemagasin, dateOperation, annee)) as stockrestant " +
                    "FROM stocker s, article a " +
                    "WHERE a.codearticle = s.codearticle " +
                    "GROUP BY s.codearticle, codemagasin, dateOperation, annee";

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet à retourner
                    while (dataReader.Read())
                    {
                        codeMagasin = Convert.ToString(dataReader["CODEMAGASIN"]);
                        codeArticle = Convert.ToString(dataReader["CODEARTICLE"]);
                        stockDebut = Convert.ToInt16(dataReader["STOCKDEBUT"]);
                        quantiteAchetee = Convert.ToInt16(dataReader["QUANTITEACHETEE"]);
                        quantiteVendue = Convert.ToInt16(dataReader["QUANTITEVENDUE"]);
                        dateOperation = Convert.ToDateTime(dataReader["DATEOPERATION"]);
                        annee = Convert.ToInt16(dataReader["ANNEE"]);
                        puArticle = Convert.ToInt16(dataReader["PUARTICLE"]);
                        stockRestant = Convert.ToInt16(dataReader["STOCKRESTANT"]);

                        designation = Convert.ToString(dataReader["designation"]);

                        c = new EtatStockBE();
                        c.numero = Convert.ToInt16(dataReader["NUMERO"]);
                        c.codeMagasin = codeMagasin;
                        c.codeArticle = codeArticle;
                        c.stockDebut = stockDebut;
                        c.quantiteAchetee = quantiteAchetee;
                        c.quantiteVendue = quantiteVendue;
                        c.dateOperation = dateOperation;
                        c.dateOperationString = dateOperation.ToShortDateString();
                        c.annee = annee;
                        c.puArticle = puArticle;
                        c.stockRestant = stockRestant;
                        c.designationArticle = designation;
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

        //méthode qui permet d'obtenir les informations sur l'état des stocks sur des types d'article particulier
        // retourner la liste des objets qui correspondent à un certain critère
        public List<EtatStockBE> etatStockSuivantCodeArticle(string codeArticle)
        {
            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "yyyy-MM-dd";
            Thread.CurrentThread.CurrentCulture = ci;

            List<EtatStockBE> list = new List<EtatStockBE>();
            int numero;
            string codeMagasin;
            //string codeArticle;
            Int16 stockDebut;
            Int16 quantiteAchetee;
            Int16 quantiteVendue;
            DateTime dateOperation;
            int annee;
            Int16 puArticle;
            Int16 stockRestant;

            string designation;

            EtatStockBE c;

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT NUMERO, dateoperation, annee, codemagasin, s.codearticle, a.designation as designation, stockdebut, SUM(quantiteachetee) as quantiteachetee, " +
                                   "SUM(quantitevendue) as quantitevendue, " +
                                   "(SELECT PUARTICLE " +
                                   "FROM stocker " +
                                   "WHERE NUMERO = (SELECT MAX(NUMERO) FROM stocker WHERE codearticle = s.codearticle AND codeMAgasin = s.codeMagasin)) as PUARTICLE, " +
                                   "(SELECT stockrestant " +
                                    "FROM stocker " +
                                    "WHERE NUMERO = (SELECT MAX(NUMERO) FROM stocker WHERE codearticle = s.codearticle AND codeMAgasin = s.codeMagasin AND dateOperation = s.dateOperation AND annee = s.annee GROUP BY codearticle, codemagasin, dateOperation, annee)) as stockrestant " +
                                   "FROM stocker s, article a " +
                                   "WHERE a.codearticle = s.codearticle AND s.codearticle = '" + codeArticle + "' " +
                                   "GROUP BY s.codearticle, codemagasin, dateOperation, annee";

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet à retourner
                    while (dataReader.Read())
                    {
                        codeMagasin = Convert.ToString(dataReader["CODEMAGASIN"]);
                        //codeArticle = Convert.ToString(dataReader["CODEARTICLE"]);
                        stockDebut = Convert.ToInt16(dataReader["STOCKDEBUT"]);
                        quantiteAchetee = Convert.ToInt16(dataReader["QUANTITEACHETEE"]);
                        quantiteVendue = Convert.ToInt16(dataReader["QUANTITEVENDUE"]);
                        dateOperation = Convert.ToDateTime(dataReader["DATEOPERATION"]);
                        annee = Convert.ToInt16(dataReader["ANNEE"]);
                        puArticle = Convert.ToInt16(dataReader["PUARTICLE"]);
                        stockRestant = Convert.ToInt16(dataReader["STOCKRESTANT"]);

                        designation = Convert.ToString(dataReader["designation"]);

                        c = new EtatStockBE();
                        c.numero = Convert.ToInt16(dataReader["NUMERO"]);
                        c.codeMagasin = codeMagasin;
                        c.codeArticle = codeArticle;
                        c.stockDebut = stockDebut;
                        c.quantiteAchetee = quantiteAchetee;
                        c.quantiteVendue = quantiteVendue;
                        c.dateOperation = dateOperation;
                        c.dateOperationString = dateOperation.ToShortDateString();
                        c.annee = annee;
                        c.puArticle = puArticle;
                        c.stockRestant = stockRestant;
                        c.designationArticle = designation;
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

        //méthode qui permet d'obtenir les informations sur l'état des stocks sur des types d'article particulier
        // retourner la liste des objets qui correspondent à un certain critère
        public List<EtatStockBE> etatStockSuivantCategorieArticle(string codeCatArticle)
        {
            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "yyyy-MM-dd";
            Thread.CurrentThread.CurrentCulture = ci;

            List<EtatStockBE> list = new List<EtatStockBE>();
            int numero;
            string codeMagasin;
            string codeArticle;
            Int16 stockDebut;
            Int16 quantiteAchetee;
            Int16 quantiteVendue;
            DateTime dateOperation;
            int annee;
            Int16 puArticle;
            Int16 stockRestant;

            string designation;

            EtatStockBE c;

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT NUMERO, dateoperation, annee, codemagasin, s.codearticle, a.designation as designation, stockdebut, SUM(quantiteachetee) as quantiteachetee, " +
                                   "SUM(quantitevendue) as quantitevendue, " +
                                   "(SELECT PUARTICLE " +
                                   "FROM stocker " +
                                   "WHERE NUMERO = (SELECT MAX(NUMERO) FROM stocker WHERE codearticle = s.codearticle AND codeMAgasin = s.codeMagasin)) as PUARTICLE, " +
                                   "(SELECT stockrestant " +
                                    "FROM stocker " +
                                    "WHERE NUMERO = (SELECT MAX(NUMERO) FROM stocker WHERE codearticle = s.codearticle AND codeMAgasin = s.codeMagasin AND dateOperation = s.dateOperation AND annee = s.annee GROUP BY codearticle, codemagasin, dateOperation, annee)) as stockrestant " +
                                   "FROM stocker s, article a " +
                                   "WHERE a.codearticle = s.codearticle AND s.codecatarticle = '" + codeCatArticle + "' " +
                                   "GROUP BY s.codearticle, codemagasin, dateOperation, annee";

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet à retourner
                    while (dataReader.Read())
                    {
                        codeMagasin = Convert.ToString(dataReader["CODEMAGASIN"]);
                        codeArticle = Convert.ToString(dataReader["CODEARTICLE"]);
                        stockDebut = Convert.ToInt16(dataReader["STOCKDEBUT"]);
                        quantiteAchetee = Convert.ToInt16(dataReader["QUANTITEACHETEE"]);
                        quantiteVendue = Convert.ToInt16(dataReader["QUANTITEVENDUE"]);
                        dateOperation = Convert.ToDateTime(dataReader["DATEOPERATION"]);
                        annee = Convert.ToInt16(dataReader["ANNEE"]);
                        puArticle = Convert.ToInt16(dataReader["PUARTICLE"]);
                        stockRestant = Convert.ToInt16(dataReader["STOCKRESTANT"]);

                        designation = Convert.ToString(dataReader["designation"]);

                        c = new EtatStockBE();
                        c.numero = Convert.ToInt16(dataReader["NUMERO"]);
                        c.codeMagasin = codeMagasin;
                        c.codeArticle = codeArticle;
                        c.stockDebut = stockDebut;
                        c.quantiteAchetee = quantiteAchetee;
                        c.quantiteVendue = quantiteVendue;
                        c.dateOperation = dateOperation;
                        c.dateOperationString = dateOperation.ToShortDateString();
                        c.annee = annee;
                        c.puArticle = puArticle;
                        c.stockRestant = stockRestant;
                        c.designationArticle = designation;
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

        //méthode qui permet d'obtenir les informations sur l'état des stocks sur des types d'article particulier
        // retourner la liste des objets qui correspondent à un certain critère
        public List<EtatStockBE> etatStockSuivantDateOperation(DateTime dateOp)
        {
            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "yyyy-MM-dd";
            Thread.CurrentThread.CurrentCulture = ci;

            List<EtatStockBE> list = new List<EtatStockBE>();
            int numero;
            string codeMagasin;
            string codeArticle;
            Int16 stockDebut;
            Int16 quantiteAchetee;
            Int16 quantiteVendue;
            DateTime dateOperation;
            int annee;
            Int16 puArticle;
            Int16 stockRestant;

            string designation;

            EtatStockBE c;

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT NUMERO, dateoperation, annee, codemagasin, s.codearticle, a.designation as designation, stockdebut, SUM(quantiteachetee) as quantiteachetee, " +
                                   "SUM(quantitevendue) as quantitevendue, " +
                                   "(SELECT PUARTICLE " +
                                   "FROM stocker " +
                                   "WHERE NUMERO = (SELECT MAX(NUMERO) FROM stocker WHERE codearticle = s.codearticle AND codeMAgasin = s.codeMagasin)) as PUARTICLE, " +
                                   "(SELECT stockrestant " +
                                    "FROM stocker " +
                                    "WHERE NUMERO = (SELECT MAX(NUMERO) FROM stocker WHERE codearticle = s.codearticle AND codeMAgasin = s.codeMagasin AND dateOperation = s.dateOperation AND annee = s.annee GROUP BY codearticle, codemagasin, dateOperation, annee)) as stockrestant " +
                                   "FROM stocker s, article a " +
                                   "WHERE a.codearticle = s.codearticle AND dateoperation = '" + dateOp + "' " +
                                   "GROUP BY s.codearticle, codemagasin, dateOperation, annee";

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer l'objet à retourner
                    while (dataReader.Read())
                    {
                        codeMagasin = Convert.ToString(dataReader["CODEMAGASIN"]);
                        codeArticle = Convert.ToString(dataReader["CODEARTICLE"]);
                        stockDebut = Convert.ToInt16(dataReader["STOCKDEBUT"]);
                        quantiteAchetee = Convert.ToInt16(dataReader["QUANTITEACHETEE"]);
                        quantiteVendue = Convert.ToInt16(dataReader["QUANTITEVENDUE"]);
                        dateOperation = Convert.ToDateTime(dataReader["DATEOPERATION"]);
                        annee = Convert.ToInt16(dataReader["ANNEE"]);
                        puArticle = Convert.ToInt16(dataReader["PUARTICLE"]);
                        stockRestant = Convert.ToInt16(dataReader["STOCKRESTANT"]);

                        designation = Convert.ToString(dataReader["designation"]);

                        c = new EtatStockBE();
                        c.numero = Convert.ToInt16(dataReader["NUMERO"]);
                        c.codeMagasin = codeMagasin;
                        c.codeArticle = codeArticle;
                        c.stockDebut = stockDebut;
                        c.quantiteAchetee = quantiteAchetee;
                        c.quantiteVendue = quantiteVendue;
                        c.dateOperation = dateOperation;
                        c.dateOperationString = dateOperation.ToShortDateString();
                        c.annee = annee;
                        c.puArticle = puArticle;
                        c.stockRestant = stockRestant;
                        c.designationArticle = designation;
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

    }
}
