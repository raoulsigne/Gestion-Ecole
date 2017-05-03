using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ecole.BusinessEntity
{
    public class ArticleQTBE
    {
        public ArticleQTBE()
        {
            this.composers = new HashSet<ComposerBE>();
            codeArticle = "";
            codeCatArticle = "";
            designation = "";
            quantite = 0;
        }

        public ArticleQTBE(String codeArticle, String codeCatArticle, String designation, int quantite)
        {
            this.composers = new HashSet<ComposerBE>();
            this.codeArticle = codeArticle;
            this.codeCatArticle = codeCatArticle;
            this.designation = designation;
            this.quantite = quantite;
        }

        public string codeArticle { get; set; }
        public string codeCatArticle { get; set; }
        public string designation { get; set; }
        public int quantite { get; set; }

        public virtual CategorieArticleBE categoriearticle { get; set; }
        public virtual ICollection<ComposerBE> composers { get; set; }
    }
}
