using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ecole.DataAccess
{
    public abstract class DA<T>
    {
        /*
         *ajouter un element avec un objet en parametre 
         *entity est l'objet a ajouter
         */
        public abstract Boolean ajouter(T entity);

        /*
         * supprimer un element en le prenant en parametre
         * entity est l'objet a supprimer
         */
        public abstract Boolean supprimer(T entity);

        /*
         * Rechercher un element en le prenant en parametre
         * entity est l'objet a rechercher
         */
        public abstract T rechercher(T entity);

        /*
         * modifier un element en prenant l'ancien et le nouveau
         * entity est le nouveau et newEntity est le nouveau a enregistrer
         */
        public abstract Boolean modifier(T entity, T newEntity);

        /*
         * Lister tous les elements de la table
         */ 
        public abstract List<T> listerTous();

        /*
         * retourner la liste des objets qui correspondent à un certain critère
         * @critere est un parametre de la forme nomchamp=valeur and|or... 
         * exemple: pour la table cycle on peut avoir : listerSuivantCritere("codecycle='valeurcodecycle' AND nomcycle='valnomcycle'")
         */ 
        public abstract List<T> listerSuivantCritere(string critere);

        /*
         * lister tous les donnees d'une colonne de la table
         * @param colonne est le nom de la colonne à lister
         */
        public abstract List<String> listerValeursColonne(String colonne);

    }
}
