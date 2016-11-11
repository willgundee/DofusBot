﻿using System.Collections.Generic;

namespace Gofus
{
    public class Script
    {
        public string Code { get; set; }
        public string Nom { get; set; }
        public string Uuid { get; set; }
        /// <summary>
        /// Constructeur d'un script
        /// </summary>
        /// <param name="c">La requête</param>
        public Script(List<string> c)
        {
            Code = c[0];
            Nom = c[1];
            Uuid = c[2];
        }
    }
}
