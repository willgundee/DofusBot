using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GofusSharp
{
    public class Zone
    {
        private Effet[] TabEffets { get; }
        private string Type { get; }
        private int PorteeMin { get; }
        private int PorteeMax { get; }
        public Zone(Effet[] TabEffets, string Type, int PorteeMin, int PorteeMax)
        {
            this.TabEffets = TabEffets;
            this.Type = Type;
            this.PorteeMin = PorteeMin;
            this.PorteeMax = PorteeMax;
        }
    }
}
