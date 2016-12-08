using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gofus
{

    /// <summary>
    /// Adversaire de base.
    /// à la base ils ne sont pas humain.
    /// </summary>
    public class Adversaire
    {
        public string nom { get; set; }
        public  int levelMin { get; set; }

        public int levelMax { get; set; }
        public Adversaire (string n,int lM,int lMX)
        {
            nom = n;
            levelMin = lM;
            levelMax = lMX;
        }
    }
}
