using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MySql.Data.MySqlClient;

using Ecole.BusinessEntity;

namespace Ecole.DataAccess
{
    public class NotesDA : DA<NotesBE>
    {
        private Connexion con = Connexion.getConnexion();

        //****************************** création d'objet, parametre obj, retourne booléen
        public override Boolean ajouter(NotesBE obj) {
            MySqlTransaction tx = con.connexion.BeginTransaction();
            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "INSERT INTO notes (matricule,codemat,codeseq,codeevaluation,note,annee,anonymat) VALUES (@matricule, @codeMatiere, @codeSequence, @codeEvaluation, @note, @annee, @anonymat)";

                // utilisation de l'objet NotesBE passé en paramètre
                cmd.Parameters.AddWithValue("@matricule", obj.matricule);
                cmd.Parameters.AddWithValue("@codeMatiere", obj.codeMat);
                cmd.Parameters.AddWithValue("@codeSequence", obj.codeSeq);
                cmd.Parameters.AddWithValue("@codeEvaluation", obj.codeEvaluation);
                cmd.Parameters.AddWithValue("@note", obj.note);
                cmd.Parameters.AddWithValue("@annee", obj.annee);
                cmd.Parameters.AddWithValue("@anonymat", obj.anonymat);

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
        //****************************** FIN création d'objet, parametre obj, retourne booléen
       
        //**************************** suppression d'objet, parametre obj, retourne booléen
        public override Boolean supprimer(NotesBE obj) {
            MySqlTransaction tx = con.connexion.BeginTransaction();
            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "DELETE FROM notes WHERE matricule=@matricule AND codemat=@codeMatiere AND codeseq=@codeSequence AND codeevaluation=@codeEvaluation AND annee=@annee";

                // utilisation de l'objet NotesBE passé en paramètre
                cmd.Parameters.AddWithValue("@codeMatiere", obj.codeMat);
                cmd.Parameters.AddWithValue("@codeSequence", obj.codeSeq);
                cmd.Parameters.AddWithValue("@codeEvaluation", obj.codeEvaluation);
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
                tx.Rollback();
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        //**************************** FIN suppression d'objet, parametre obj, retourne booléen
        
        //****************************** mise à jour d'objet, parametre obj, retourne booléen
        public override Boolean modifier(NotesBE obj, NotesBE newobj) {
            MySqlTransaction tx = con.connexion.BeginTransaction();
            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "UPDATE notes SET note=@note, annee=@annee WHERE matricule=@matricule AND codemat=@codeMatiere AND codeseq=@codeSequence AND codeevaluation=@codeEvaluation AND anonymat=@anonymat";

                // utilisation de l'objet Notes passé en paramètre
                cmd.Parameters.AddWithValue("@codeMatiere", obj.codeMat);
                cmd.Parameters.AddWithValue("@codeSequence", obj.codeSeq);
                cmd.Parameters.AddWithValue("@codeEvaluation", obj.codeEvaluation);
                cmd.Parameters.AddWithValue("@matricule", obj.matricule);
                cmd.Parameters.AddWithValue("@note", newobj.note);
                cmd.Parameters.AddWithValue("@annee", obj.annee);
                cmd.Parameters.AddWithValue("@anonymat", obj.anonymat);

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

        public Boolean modifier(NotesBE obj)
        {
            MySqlTransaction tx = con.connexion.BeginTransaction();
            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                if(obj.note != -1)
                    cmd.CommandText = "REPLACE INTO notes (matricule,codemat,codeseq,codeevaluation,note,annee,anonymat) VALUES (@matricule, @codeMatiere, @codeSequence, @codeEvaluation, @note, @annee, @anonymat)";
                else
                    cmd.CommandText = "REPLACE INTO notes (matricule,codemat,codeseq,codeevaluation,annee,anonymat) VALUES (@matricule, @codeMatiere, @codeSequence, @codeEvaluation, @annee, @anonymat)";

                // utilisation de l'objet Notes passé en paramètre
                cmd.Parameters.AddWithValue("@codeMatiere", obj.codeMat);
                cmd.Parameters.AddWithValue("@codeSequence", obj.codeSeq);
                cmd.Parameters.AddWithValue("@codeEvaluation", obj.codeEvaluation);
                cmd.Parameters.AddWithValue("@matricule", obj.matricule);
                cmd.Parameters.AddWithValue("@annee", obj.annee);
                cmd.Parameters.AddWithValue("@note", obj.note);
                cmd.Parameters.AddWithValue("@anonymat", obj.anonymat);

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
        //****************************** FIN mise à jour d'objet, parametre obj, retourne booléen
        
        //*********************** rechercher d'un objet à partir de l'id, parametre obj, retourne l'objet
       
        //*********************************** rechercher d'un objet à partir de ses codes, parametre (code1, code2), retourne l'objet
        public override NotesBE rechercher(NotesBE notes) {
            string matricule;
            string codeMatiere;
            string codeSequence;
            string codeEvaluation;
            double note;
            int annee;

            NotesBE m;
            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT * FROM notes WHERE matricule=@matricule AND codemat=@matiere AND codeseq=@codeSequence AND codeevaluation=@codeEvaluation AND annee=@annee";
                cmd.Parameters.AddWithValue("@matricule", notes.matricule);
                cmd.Parameters.AddWithValue("@matiere", notes.codeMat);
                cmd.Parameters.AddWithValue("@codeSequence", notes.codeSeq);
                cmd.Parameters.AddWithValue("@codeEvaluation", notes.codeEvaluation);
                cmd.Parameters.AddWithValue("@annee", notes.annee);

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer la voiture à retourner
                    if (dataReader.Read())
                    {
                        matricule = Convert.ToString(dataReader["matricule"]);
                        codeMatiere = Convert.ToString(dataReader["codemat"]);
                        codeSequence = Convert.ToString(dataReader["codeseq"]);
                        codeEvaluation = Convert.ToString(dataReader["codeevaluation"]);
                        note = Convert.ToDouble(dataReader["note"]);
                        annee = Convert.ToInt16(dataReader["annee"]);
                        m = new NotesBE(matricule, codeMatiere, codeSequence, codeEvaluation, note, annee,Convert.ToString(dataReader["anonymat"]));
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

        //********************************** retourner la liste de tout les objets
        public override List<NotesBE> listerTous()
        {
            try
            {
                List<NotesBE> listNoteBE = new List<NotesBE>();

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT * FROM notes";

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //Read the data and store them in the list
                    while (dataReader.Read())
                    {
                        NotesBE noteBE = new NotesBE(Convert.ToString(dataReader["matricule"]), Convert.ToString(dataReader["codemat"]), Convert.ToString(dataReader["codeseq"]),
                            Convert.ToString(dataReader["codeevaluation"]), Convert.ToDouble(dataReader["note"]), Convert.ToInt16(dataReader["annee"]), Convert.ToString(dataReader["anonymat"]));
                        listNoteBE.Add(noteBE);
                    }

                    //close Data Reader
                    dataReader.Close();

                    //return list to be displayed
                    if (listNoteBE.Count != 0)
                        return listNoteBE;
                    else return null;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        //********************************** FIN retourner la liste de tout les objets
        
        //*************************** retourner la liste des objets qui correspondent à un certain critère
        public override List<NotesBE> listerSuivantCritere(String critere)
        {
            try
            {
                List<NotesBE> listNoteBE = new List<NotesBE>();
                double note;
                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT * FROM notes WHERE " + critere + " order by anonymat";

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //Read the data and store them in the list
                    while (dataReader.Read())
                    {
                        try
                        {
                            note = Convert.ToDouble(dataReader["note"]);
                            NotesBE noteBE = new NotesBE(Convert.ToString(dataReader["matricule"]), Convert.ToString(dataReader["codemat"]), Convert.ToString(dataReader["codeseq"]),
                                Convert.ToString(dataReader["codeevaluation"]), note, Convert.ToInt16(dataReader["annee"]), Convert.ToString(dataReader["anonymat"]));
                            listNoteBE.Add(noteBE);
                        }
                        catch (Exception)
                        {
                            NotesBE noteBE = new NotesBE(Convert.ToString(dataReader["matricule"]), Convert.ToString(dataReader["codemat"]), Convert.ToString(dataReader["codeseq"]),
                                Convert.ToString(dataReader["codeevaluation"]), -1, Convert.ToInt16(dataReader["annee"]), Convert.ToString(dataReader["anonymat"]));
                            listNoteBE.Add(noteBE);
                        }
                    }

                    //close Data Reader
                    dataReader.Close();

                    //return list to be displayed
                    if (listNoteBE.Count != 0)
                        return listNoteBE;
                    else return null;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        //*************************** retourner la liste des objets qui correspondent à un certain critère

        public override List<string> listerValeursColonne(string colonne)
        {
            List<String> list = new List<String>();

            try
            {
                // Création d'une commande SQL
                MySqlCommand cmd = con.connexion.CreateCommand();
                cmd.CommandText = "SELECT * FROM notes";

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
                cmd.CommandText = "SELECT COUNT(*) FROM notes";

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


        public NotesBE rechercherEleve(NotesBE notes)
        {
            string matricule;
            string codeMatiere;
            string codeSequence;
            string codeEvaluation;
            double note;
            int annee;

            NotesBE m;
            try
            {

                // Création d'une commande SQL 
                MySqlCommand cmd = con.connexion.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT * FROM notes WHERE matricule=@matricule AND annee=@annee";
                cmd.Parameters.AddWithValue("@matricule", notes.matricule);
                cmd.Parameters.AddWithValue("@annee", notes.annee);

                // Exécution de la commande SQL
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    //fabriquer la voiture à retourner
                    if (dataReader.Read())
                    {
                        matricule = Convert.ToString(dataReader["matricule"]);
                        codeMatiere = Convert.ToString(dataReader["codemat"]);
                        codeSequence = Convert.ToString(dataReader["codeseq"]);
                        codeEvaluation = Convert.ToString(dataReader["codeevaluation"]);
                        note = Convert.ToDouble(dataReader["note"]);
                        annee = Convert.ToInt16(dataReader["annee"]);
                        m = new NotesBE(matricule, codeMatiere, codeSequence, codeEvaluation, note, annee, Convert.ToString(dataReader["anonymat"]));
                        dataReader.Close();
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
    }
}
