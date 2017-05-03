using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ecole.BusinessEntity;
using Ecole.DataAccess;

namespace Ecole.BusinessLogic
{
    class GestionBilanFinancierBL
    {
        ParametresDA parametreDA;
        PayerDA payerDA;

        public GestionBilanFinancierBL()
        {
            parametreDA = new ParametresDA();
            payerDA = new PayerDA();
        }

        internal int anneeEnCours()
        {
            return parametreDA.AnneeEnCours();
        }

        internal List<ClasseConception.LigneBilan> obtenirBilanFinancier(int annee)
        {
            return payerDA.obtenirBilanFinancier(annee);
        }
    }
}
