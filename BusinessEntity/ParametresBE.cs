using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ecole.BusinessEntity
{
    public class ParametresBE
    {
        // définition des attributs -----------------------------------------
        // attribut idParamètre
        public int idParametre { get; set; }

        // attribut nomEcole
        public String nomEcole { get; set; }

        // attribut adresse
        public String adresse { get; set; }

        // attribut telephone
        public String tel { get; set; }

        // attribut fax
        public String fax { get; set; }

        // attribut email
        public String email { get; set; }

        // attribut siteWeb
        public String siteWeb { get; set; }

        // attribut directeur
        public String directeur { get; set; }

        // attribut pays
        public String pays { get; set; }

        // attribut region
        public String region { get; set; }
        public String regionA { get; set; }

        // attribut ministere
        public String ministere { get; set; }
        public String ministery { get; set; }

        // attribut country
        public String country { get; set; }

        // attribut annee
        public int annee { get; set; }

        // attribut département
        public String departement { get; set; }
        public String department { get; set; }

        // attribut ville
        public String ville { get; set; }

        // attribut titreDuChef
        public String titreDuChef { get; set; }

        // attribut titreDuChef
        public String titleOfChief { get; set; }

        //logo de l'etablissement
        public string logo { get; set; }

        //le chemin du repertoire des photos sur le serveur
        public string REPERTOIRE_PHOTO { get; set; }

        //le nom du fichier de langue a utiliser
        //public string FICHIER_LANGUE { get; set; }

         // constructeur de la classe -----------------------------------------
        public ParametresBE() { }

        // constructeur avec paramètres --------------------------------------
        public ParametresBE(int idParametre, String nomEcole, String adresse, String telephone, String fax, String email, String siteWeb, String directeur, String pays, 
            String region, String ministere, String ministery, String country, String regionA, int annee,
            String departement, String department, String ville, String titreDuChef, String titleOfChief, string logo, string REPERTOIRE_PHOTO)
        {
            this.idParametre = idParametre;
            this.nomEcole = nomEcole;
            this.adresse = adresse;
            this.tel = telephone;
            this.fax = fax;
            this.email = email;
            this.siteWeb = siteWeb;
            this.directeur = directeur;
            this.pays = pays;
            this.region = region;
            this.ministere = ministere;
            this.ministery = ministery;
            this.country = country;
            this.regionA = regionA;
            this.annee = annee;
            this.departement = departement;
            this.department = department;
            this.ville = ville;
            this.titreDuChef = titreDuChef;
            this.titleOfChief = titleOfChief;
            this.logo = logo;

            this.REPERTOIRE_PHOTO = REPERTOIRE_PHOTO;
        }

    }
}
