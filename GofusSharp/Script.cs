using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GofusSharp
{
    public class Script
    {
        private int IdScript { get; }
        private string Texte { get; }
        public Script(int IdScript, string Texte)
        {
            this.IdScript = IdScript;
            this.Texte = Texte;
        }
    }
}
