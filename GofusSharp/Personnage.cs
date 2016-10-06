namespace GofusSharp
{
    public class Personnage : Entite
    {
        public Equipement[] TabEquipements { get; internal set; }
        public Personnage(int IdEntite, Classe ClasseEntite, string Nom, float Experience, type Equipe, ListeChainee<Statistique> ListStatistiques, Script ScriptEntite, Equipement[] TabEquipements, Terrain TerrainEntite) : base(IdEntite, ClasseEntite, Nom, Experience, Equipe, ListStatistiques, ScriptEntite, TerrainEntite, 0)
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

        public bool Attaquer(EntiteInconnu cible)
        {
            /*Arme arme = new Arme(0,null,"poing",Equipement.type.arme,new Effet[] { new Effet(Effet.type.ATT_neutre, 3, 5) },new Zone(Zone.type.croix,1,1), new Zone(Zone.type.carre, 0, 0), Arme.typeArme.dague);
            foreach (Equipement invent in TabEquipements)
            {
                if (invent is Arme)
                {
                    break;
                }
            }
            if (CaseEstDansZone(sort.ZonePortee.Type, sort.ZonePortee.PorteeMin, sort.ZonePortee.PorteeMax, Position, cible.Position))
            {
                foreach (Effet effet in sort.TabEffets)
                {
                    InfligerEffet(effet, sort.ZoneEffet, cible.Position);
                }
            }*/
            return false;
        }
    }
}