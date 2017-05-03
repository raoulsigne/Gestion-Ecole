using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projet_Ecole.BusinessEntity
{
    class CategorieArticleBE
    {
        public CategorieArticleBE()
        {
            this.articles = new HashSet<ArticleBE>();
            codeCatArticle = "";
            nomCatArticle = "";
        }

        public CategorieArticleBE(String code, String nom)
        {
            this.articles = new HashSet<ArticleBE>();
            this.codeCatArticle = code;
            this.nomCatArticle = nom;
        }

        public string codeCatArticle { get; set; }
        public string nomCatArticle { get; set; }

        public virtual ICollection<ArticleBE> articles { get; set; }

    }
}
