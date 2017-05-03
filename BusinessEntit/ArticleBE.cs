using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projet_Ecole.BusinessEntity
{
    class ArticleBE
    {
        public ArticleBE()
        {
            this.composers = new HashSet<ComposerBE>();
            codeArticle = "";
            codeCatArticle = "";
            designation = "";
        }

        public ArticleBE(String codeArticle, String codeCatArticle, String designation)
        {
            this.composers = new HashSet<ComposerBE>();
            this.codeArticle = codeArticle;
            this.codeCatArticle = codeCatArticle;
            this.designation = designation;
        }

        public string codeArticle { get; set; }
        public string codeCatArticle { get; set; }
        public string designation { get; set; }

        public virtual CategorieArticleBE categoriearticle { get; set; }
        public virtual ICollection<ComposerBE> composers { get; set; }
    }
}
