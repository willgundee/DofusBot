using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GofusSharp
{
    public class Terrain
    {
        private Case[][] TabCases { get; }
        public Terrain(Case[][] TabCases)
        {
            this.TabCases = TabCases;
        }
    }
}
