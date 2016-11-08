﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gofus
{
    public class Zone
    {
        public enum type { cercle, ligne_verticale, ligne_horizontale, carre, croix, T, X, demi_cercle, cone, tous } // les types de zones possibles
        public type Nom { get; set; }
        public int PorteeMin { get; set; }
        public int PorteeMax { get; set; }
        /// <summary>
        /// Constructeur d'une Zone
        /// </summary>
        /// <param name="t">La requête</param>
        public Zone(List<string> t)
        {
            Nom = (type)Enum.Parse(typeof(type), t[0], true);//convert string to enum;
            PorteeMin = Convert.ToInt32(t[1]);
            PorteeMax = Convert.ToInt32(t[2]);
        }
    }
}
