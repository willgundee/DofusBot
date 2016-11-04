using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            Code = c[1];
            Nom = c[2];
            Uuid = c[3];
        }
    }
}
