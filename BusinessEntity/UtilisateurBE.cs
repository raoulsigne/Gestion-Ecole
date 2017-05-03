using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ecole.BusinessEntity
{
    public class UtilisateurBE
    {
            // declaration des éléments;
        public string login { get; set; }
        public string role { get; set; }
        public string password { get; set; }
        public string nom { get; set; }

        public UtilisateurBE()
        {
            this.login = "";
            this.role = "";
            this.password = "";
            this.nom = "";
        }
       
        public UtilisateurBE(string login, string role, string password, string nom)
        {
            this.login = login;
            this.role = role;
            this.password = password;
            this.nom = nom;
           
        }
    }
}
