using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projet_Ecole.BusinessEntity
{
    class Eleve
    {
        public Eleve()
        {
            this.acheters = new HashSet<AcheterBE>();
            this.appartenirs = new HashSet<AppartenirBE>();
            this.matricule = "";
            this.codePays = "";
            this.codeDept = "";
            this.codeRegion = "";
            this.nom = "";
            this.sexe = "";
            this.dateNaissance = DateTime.Today;
            this.lieuNaissance = "";
            this.photo = "";
            this.nomPere = "";
            this.nomMere = "";
            this.telephone = "";
            this.telParent = "";
            this.email = "";
            this.adresse = "";
        }

        public Eleve(String mat, String pays, String dept, String region, String nom, String sexe, DateTime date, String lieuNaiss,
            String photo, String pere, String mere, String tel, String telparent, String email, String adresse)
        {
            this.acheters = new HashSet<AcheterBE>();
            this.appartenirs = new HashSet<AppartenirBE>();
            this.matricule = mat;
            this.codePays = pays;
            this.codeDept = dept;
            this.codeRegion = region;
            this.nom = nom;
            this.sexe = sexe;
            this.dateNaissance = date;
            this.lieuNaissance = lieuNaiss;
            this.photo = photo;
            this.nomPere = pere;
            this.nomMere = mere;
            this.telephone = tel;
            this.telParent = telparent;
            this.email = email;
            this.adresse = adresse;
        }

        public string matricule { get; set; }
        public string codePays { get; set; }
        public string codeDept { get; set; }
        public string codeRegion { get; set; }
        public string nom { get; set; }
        public string sexe { get; set; }
        public System.DateTime dateNaissance { get; set; }
        public string lieuNaissance { get; set; }
        public string photo { get; set; }
        public string nomPere { get; set; }
        public string nomMere { get; set; }
        public string telephone { get; set; }
        public string telParent { get; set; }
        public string email { get; set; }
        public string adresse { get; set; }
    
        public virtual ICollection<AcheterBE> acheters { get; set; }
        public virtual ICollection<AppartenirBE> appartenirs { get; set; }
        public virtual DepartementBE departement { get; set; }
    }
}
