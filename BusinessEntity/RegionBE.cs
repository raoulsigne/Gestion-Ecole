using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ecole.BusinessEntity
{
    public class RegionBE
    {
            // declaration des éléments;
        public string coderegion { get; set; }
        public string nomregion { get; set; }


        public RegionBE()
        {
            this.coderegion = "";
            this.nomregion = "";

        }    
       
        public RegionBE(string coderegion, string nomregion)
        {
            this.coderegion = coderegion;
            this.nomregion = nomregion;
            
        }
    }
}
