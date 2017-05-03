using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ecole.BusinessEntity
{
    public class OperationBE
    {
        // définition des attributs -----------------------------------------
        // attribut codeOperation
        public String codeOp { get; set; }

        // attribut codeTypeOperation
        public String codeTypeOp { get; set; }

        // attribut libelleOperation
        public String libelleOp { get; set; }

        // constructeur de la classe -----------------------------------------
        public OperationBE() { }

        // constructeur avec paramètres --------------------------------------
        public OperationBE(String codeOperation, String codeTypeOperation, String libelleOperation)
        {
            this.codeOp = codeOperation;
            this.codeTypeOp = codeTypeOperation;
            this.libelleOp = libelleOperation;
        }
    }
}
