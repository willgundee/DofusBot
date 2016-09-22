namespace GofusSharp
{
    public class Personnage : Entite
    {
        public Equipement[] TabEquipements { get; private set; }
        public Personnage(int IdEntite, ListeChainee LstStatistiques, Script ScriptEntite, Classe ClasseEntite, string Nom, float Experience, Equipement[] TabEquipements) : base(IdEntite, LstStatistiques, ScriptEntite, ClasseEntite, Nom, Experience)
        {
            this.TabEquipements = TabEquipements;
        }
    }
}
//csc /target:library /out:GofusSharp.dll FonctionUtilisateur.cs