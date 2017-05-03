using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projet_Ecole.BusinessEntity
{
    class EnseignantBE
    {
        public EnseignantBE()
        {
            this.dirigers = new HashSet<DirigerBE>();
            this.codeProf = "";
            this.nomProf = "";
            this.dateNaissance = DateTime.Today;
            this.tel = "";
            this.email = "";
            this.ville = "";
        }

        public EnseignantBE(String code, String nom, DateTime dateNaiss, String tel, String email, String ville)
        {
            this.dirigers = new HashSet<DirigerBE>();
            this.codeProf = code;
            this.nomProf = nom;
            this.dateNaissance = dateNaiss;
            this.tel = tel;
            this.email = email;
            this.ville = ville;
        }
    
        public string codeProf { get; set; }
        public string nomProf { get; set; }
        public System.DateTime dateNaissance { get; set; }
        public string tel { get; set; }
        public string email { get; set; }
        public string ville { get; set; }
    
        public virtual ICollection<DirigerBE> dirigers { get; set; }
    }
}
