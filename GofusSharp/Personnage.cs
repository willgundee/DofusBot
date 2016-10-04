namespace GofusSharp
{
    public class Personnage : Entite
    {
        public Equipement[] TabEquipements { get; internal set; }
        public Personnage(int IdEntite, Classe ClasseEntite, string Nom, float Experience, Case Position, type Equipe, ListeChainee<Statistique> ListStatistiques, Script ScriptEntite, Equipement[] TabEquipements, Terrain TerrainEntite) : base(IdEntite, ClasseEntite, Nom, Experience, Position, Equipe, ListStatistiques, ScriptEntite, TerrainEntite, 0)
        {
            this.TabEquipements = TabEquipements;
            foreach (Equipement item in TabEquipements)
            {
                foreach (Statistique stat_item in item.TabStatistiques)
                {
                    bool existe = false;
                    foreach (Statistique stat in this.ListStatistiques)
                    {
                        if (stat.Nom == stat_item.Nom)
                        {
                            stat.Valeur += stat_item.Valeur;
                            existe = true;
                        }
                    }
                    if (!existe)
                    {
                        this.ListStatistiques.AjouterFin(stat_item);
                    }
                }
            }
        }
    }
}