using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ecole.BusinessEntity
{
    public class ArticleBE
    {
        public ArticleBE()
        {
            this.composers = new HashSet<ComposerBE>();
            codeArticle = "";
            codeCatArticle = "";
            designation = "";

            this.quantiteSaisie = "";
            this.PuArticle = "";
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

        public virtual string quantiteSaisie { get; set; } //la quantité d'article saisie (seulement utiles pour l'aafiche du dataGrid dans le formulaire d'enregistrement d'article)
        public virtual string PuArticle { get; set; } //le prix unitaire de l'article (seulement utiles pour l'aafiche du dataGrid dans le formulaire d'enregistrement d'article)
        public virtual CategorieArticleBE categoriearticle { get; set; }
        public virtual ICollection<ComposerBE> composers { get; set; }
    }
}
