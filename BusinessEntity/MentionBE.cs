using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ecole.BusinessEntity
{
    public class MentionBE
    {
        // définition des attributs -----------------------------------------
        // attribut idMention
        public int idMention { get; set; }      

        // attribut noteMin
        public Double noteMin { get; set; }       

        // attribut noteMax
        public Double noteMax { get; set; } 

        // attribut mention
        public String mention { get; set; }
     
        // constructeur de la classe -----------------------------------------
        public MentionBE() { }

        // constructeur avec paramètres --------------------------------------
        public MentionBE(int idMention, Double noteMin, Double noteMax, String mention) {
            this.idMention = idMention;
            this.noteMin = noteMin;
            this.noteMax = noteMax;
            this.mention = mention;
        }
    }
}
