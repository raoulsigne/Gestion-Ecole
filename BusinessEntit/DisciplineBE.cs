using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projet_Ecole.BusinessEntity
{
    class DisciplineBE
    {
        public string codeSanction { get; set; }
        public string nomSanction { get; set; }
        public string variable { get; set; }
        public string unite { get; set; }

        public DisciplineBE()
        {
            this.codeSanction = "";
            this.nomSanction = "";
            this.variable = "";
            this.unite = "";
        }

        public DisciplineBE(String code, String nom, String variable, String unite)
        {
            this.codeSanction = code;
            this.nomSanction = nom;
            this.variable = variable;
            this.unite = unite;
        }
    }
}
