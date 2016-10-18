using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test
{
    public class Script
    {
        public string Code { get; set; }
        public string Nom { get; set; }
        public Script(List<string> c)
        {
            Code = c[1];
            Nom = c[2];
        }
    }
}
