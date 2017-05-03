using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Threading;

namespace Ecole.BusinessEntity
{
    public class EtatStockBE
    {
        // declaration des éléments;
        public int numero { get; set; }
        public string codeMagasin { get; set; }
        public string codeArticle { get; set; }
        public int stockDebut { get; set; }
        public int quantiteAchetee { get; set; }
        public int quantiteVendue { get; set; }
        public DateTime dateOperation { get; set; }
        public string dateOperationString { get; set; } // la date de l'opération au format chaine de caractère
        public int annee { get; set; }
        public int puArticle { get; set; }
        public int stockRestant { get; set; }

        public virtual string designationArticle { get; set; }//est utilisé seulement pour l'affichage dans le datagarid

        public EtatStockBE()
        {
            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";
            Thread.CurrentThread.CurrentCulture = ci;

            this.codeMagasin = "";
            this.codeArticle = "";
            this.stockDebut = 0;
            this.quantiteAchetee = 0;
            this.quantiteVendue = 0;
            this.dateOperation = new DateTime();
            this.dateOperationString = "";
            this.annee = System.DateTime.Today.Year;
            this.puArticle = 0;
            this.stockRestant = 0;

            this.designationArticle = "";

        }


        public EtatStockBE(string codeMagasin, string codeArticle, int stockDebut, int quantiteAchetee, int quantiteVendue, DateTime dateOperation,
            int annee, int puArticle, int stockRestant)
        {
            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";
            Thread.CurrentThread.CurrentCulture = ci;

            this.codeMagasin = codeMagasin;
            this.codeArticle = codeArticle;
            this.stockDebut = stockDebut;
            this.quantiteAchetee = quantiteAchetee;
            this.quantiteVendue = quantiteVendue;
            this.dateOperation = dateOperation;
            this.dateOperationString = dateOperation.ToShortDateString();
            this.annee = annee;
            this.puArticle = puArticle;
            this.stockRestant = stockRestant;
        }
    }
}
