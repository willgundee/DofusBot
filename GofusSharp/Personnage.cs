namespace GofusSharp
{
    public class Personnage : Entite
    {
        public Equipement[] TabEquipements { get; internal set; }
        public Personnage(int IdEntite, Classe ClasseEntite, string Nom, float Experience, Case Position, Statistique[] TabStatistiques, Script ScriptEntite, Equipement[] TabEquipements, Terrain TerrainEntite) : base(IdEntite, ClasseEntite, Nom, Experience, Position, TabStatistiques, ScriptEntite, TerrainEntite)
        {
            this.TabEquipements = TabEquipements;
        }
    }
}
//csc /target:library /out:GofusSharp.dll FonctionUtilisateur.cs