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
//csc /target:library /out:GofusSharp.dll FonctionUtilisateur.cs