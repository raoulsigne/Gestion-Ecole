using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ecole.BusinessEntity
{
    public class DisciplineBE
    {
        public string codeSanction { get; set; }
        public string nomSanction { get; set; }
        public string variable { get; set; }
        public string unite { get; set; }
        public int priorite { get; set; }

        public DisciplineBE()
        {
            this.codeSanction = "";
            this.nomSanction = "";
            this.variable = "";
            this.unite = "";
            this.priorite = 0;
        }

        public DisciplineBE(String code, String nom, String variable, String unite, int priorite)
        {
            this.codeSanction = code;
            this.nomSanction = nom;
            this.variable = variable;
            this.unite = unite;
            this.priorite = priorite;
        }
    }
}
