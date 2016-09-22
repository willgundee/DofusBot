using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GofusSharp
{
    public class Partie
    {
        private int IdPartie { get; }
        private Terrain TerrainPartie { get; }
        private Entite[] TabAttaquants { get; }
        private Entite[] TabDefendants { get; }
        private int Seed { get; }
        public Partie()
        {

        }
    }
}
