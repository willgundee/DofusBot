﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gofus
{
    public class Rapport
    {
        public string msg { get; set; }

        public string temps { get; set; }

        public string titre { get; set; }

        public string type { get; set; }

        public Rapport(string M,string T,string Titre, string ty)
        {
            msg = M;
            temps = T;
            titre = Titre;
            type = ty;
        }


      


    }
}
