using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projet_Ecole.BusinessEntity
{
    class GroupeBE
    {
        public string role { get; set; }
        public string description { get; set; }

        public GroupeBE() {
            this.role = "";
            this.description = "";
        }

        public GroupeBE(String role, String desc)
        {
            this.role = role;
            this.description = desc;
        }
    }
}
