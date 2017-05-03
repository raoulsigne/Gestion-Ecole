using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Ecole.BusinessEntity;

namespace Ecole.ClasseConception
{
    class ProfilAcademiqueBE
    {
        public ProfilAcademiqueBE()
        {
            
        }

        public ProfilAcademiqueBE(EleveBE eleve, ClasseBE classe, MoyennesBE moyenne)
        {
            this.eleve = eleve;
            this.classe = classe;
            this.moyenne = moyenne;
        }

        public EleveBE eleve { get; set; }
        public ClasseBE classe { get; set; }
        public MoyennesBE moyenne { get; set; }
    }
}
