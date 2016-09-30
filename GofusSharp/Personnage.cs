namespace GofusSharp
{
    public class Personnage : Entite
    {
        public Equipement[] TabEquipements { get; internal set; }
        public Personnage(int IdEntite, Classe ClasseEntite, string Nom, float Experience, Case Position, type Equipe, ListeChainee<Statistique> ListStatistiques, Script ScriptEntite, Equipement[] TabEquipements, Terrain TerrainEntite) : base(IdEntite, ClasseEntite, Nom, Experience, Position, Equipe, ListStatistiques, ScriptEntite, TerrainEntite, 0)
        {
            this.TabEquipements = TabEquipements;
        }
    }
}
//csc /target:library /out:GofusSharp.dll FonctionUtilisateur.cs