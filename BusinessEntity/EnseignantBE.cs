using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Threading;

namespace Ecole.BusinessEntity
{
    public class EnseignantBE
    {
        public EnseignantBE()
        {
            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";
            Thread.CurrentThread.CurrentCulture = ci;

            this.dirigers = new HashSet<DirigerBE>();
            this.codeProf = "";
            this.nomProf = "";
            this.dateNaissance = DateTime.Today;

            this.dateNaissanceString = "";

            this.tel = "";
            this.email = "";
            this.ville = "";

            this.dateEmbauche = DateTime.Today; 
            this.dateDepart = DateTime.Today;

            this.statut = "";
            this.photo = "";

            this.dateDepartString = "";
            this.dateEmbaucheString = "";
        }

        public EnseignantBE(String code, String nom, DateTime dateNaiss, String tel, String email, String ville, DateTime dateEmbauche, DateTime dateDepart)
        {
            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";
            Thread.CurrentThread.CurrentCulture = ci;

            this.dirigers = new HashSet<DirigerBE>();
            this.codeProf = code;
            this.nomProf = nom;
            this.dateNaissance = dateNaiss;

            this.dateNaissanceString = dateNaiss.ToShortDateString();

            this.tel = tel;
            this.email = email;
            this.ville = ville;
            this.dateEmbauche = dateEmbauche;
            this.dateDepart = dateDepart;

            this.dateDepartString = dateDepart.ToShortDateString();

            if (dateEmbauche == Convert.ToDateTime(null))
                this.dateEmbaucheString = "";
            else
                this.dateEmbaucheString = dateEmbauche.ToShortDateString();
        }
    
        public string codeProf { get; set; }
        public string nomProf { get; set; }
        public System.DateTime dateNaissance { get; set; }
        public string dateNaissanceString { get; set; }
        public string tel { get; set; }
        public string email { get; set; }
        public string ville { get; set; }

        public System.DateTime dateEmbauche { get; set; }
        public System.DateTime dateDepart { get; set; }

        public string dateEmbaucheString { get; set; }
        public string dateDepartString { get; set; }
        public string statut { get; set; }
        public string photo { get; set; }

        public virtual ICollection<DirigerBE> dirigers { get; set; }
    }
}
