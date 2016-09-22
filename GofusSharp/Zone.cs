using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GofusSharp
{
    public class Zone
    {
        private string Type { get; }
        private int PorteeMin { get; }
        private int PorteeMax { get; }
        public Zone(string Type, int PorteeMin, int PorteeMax)
        {
            this.Type = Type;
            this.PorteeMin = PorteeMin;
            this.PorteeMax = PorteeMax;
        }
    }
}
