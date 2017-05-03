using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Threading;

namespace Ecole.BusinessEntity
{
    public class EleveBE
    {
        public EleveBE()
        {
            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";
            Thread.CurrentThread.CurrentCulture = ci;

            this.acheters = new HashSet<AcheterBE>();
            this.appartenirs = new HashSet<AppartenirBE>();
            this.matricule = "";
            this.codePays = "";
            this.codeDept = "";
            this.codeRegion = "";
            this.nom = "";
            this.sexe = "";
            this.dateNaissance = DateTime.Today;

            this.dateNaissanceString = "";

            this.lieuNaissance = "";
            this.photo = "";
            this.nomPere = "";
            this.nomMere = "";
            this.telephone = "";
            this.telParent = "";
            this.email = "";
            this.adresse = "";
            this.diplome = "";
            this.langue = "";
            this.anneeDiplome = DateTime.Today.Year;


            this.categorie = "";

            this.fonctionPere = "";
            this.fonctionMere = "";
            this.situationMedicale = "";
            this.etat = "";
        }

        public EleveBE(String mat, String pays, String dept, String region, String nom, String sexe, DateTime date, String lieuNaiss, String langue,
            String photo, String pere, String mere, String tel, String telparent, String email, String adresse, String diplome, Int32 anneeDiplome)
        {
            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";
            Thread.CurrentThread.CurrentCulture = ci;

            this.acheters = new HashSet<AcheterBE>();
            this.appartenirs = new HashSet<AppartenirBE>();
            this.matricule = mat;
            this.codePays = pays;
            this.codeDept = dept;
            this.codeRegion = region;
            this.nom = nom;
            this.sexe = sexe;
            this.dateNaissance = date;

            this.dateNaissanceString = date.ToShortDateString();

            this.lieuNaissance = lieuNaiss;
            this.photo = photo;
            this.nomPere = pere;
            this.nomMere = mere;
            this.telephone = tel;
            this.telParent = telparent;
            this.email = email;
            this.adresse = adresse;
            this.diplome = diplome;
            this.anneeDiplome = anneeDiplome;
            this.langue = langue;
            this.etat = etat;
        }

        public string matricule { get; set; }
        public string codePays { get; set; }
        public string codeDept { get; set; }
        public string codeRegion { get; set; }
        public string nom { get; set; }
        public string sexe { get; set; }
        public System.DateTime dateNaissance { get; set; }

        public string dateNaissanceString { get; set; }

        public string lieuNaissance { get; set; }
        public string photo { get; set; }
        public string nomPere { get; set; }
        public string nomMere { get; set; }
        public string telephone { get; set; }
        public string telParent { get; set; }
        public string email { get; set; }
        public string adresse { get; set; }
        public string diplome { get; set; }
        public Int32 anneeDiplome { get; set; }
        public string langue { get; set; }


        public virtual int numero { get; set; } //le numéro de l'élève (il sera seulement utile pour l'affichage dans le datagrig)
        public virtual string categorie { get; set; } //la catégorie de l'élève (il sera seulement utile pour l'affichage dans le datagrig)        
        public virtual ICollection<AcheterBE> acheters { get; set; }
        public virtual ICollection<AppartenirBE> appartenirs { get; set; }
        public virtual DepartementBE departement { get; set; }

        public string fonctionPere { get; set; }
        public string fonctionMere { get; set; } 
        public string situationMedicale { get; set; }
        public string etat { get; set; }
    }
}
