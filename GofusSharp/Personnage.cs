using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//csc /target:library /out:GofusSharp.dll FonctionUtilisateur.cs
namespace GofusSharp
{
    public class Personnage : Entite
    {
        private Equipement[] TabEquipements { get; }
        public Personnage(int IdEntite, Statistique[] TabStatistiques, Script ScriptEntite, Classe ClasseEntite, string Nom, float Experience, Equipement[] TabEquipements) : base(IdEntite, TabStatistiques, ScriptEntite, ClasseEntite, Nom, Experience)
        {
            this.TabEquipements = TabEquipements;
        }
    }
}
