using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ecole.BusinessEntity
{
    public class ComposerBE
    {
        public string codeArticle { get; set; }
        public string codeSetArticle { get; set; }
        public int annee { get; set; }
        public int quantite { get; set; }

        public virtual ArticleBE article { get; set; }

        public ComposerBE()
        {
            this.codeArticle = "";
            this.codeSetArticle = "";
            this.annee = DateTime.Today.Year;
            this.quantite = 0;
        }

        public ComposerBE(String article, String setarticle, int annee, int qte)
        {
            this.codeArticle = article;
            this.codeSetArticle = setarticle;
            this.annee = annee;
            this.quantite = qte;
        }
    }
}
