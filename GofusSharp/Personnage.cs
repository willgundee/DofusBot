﻿namespace GofusSharp
{
    public class Personnage : Entite
    {
        public Equipement[] TabEquipements { get; internal set; }
        internal Personnage(int IdEntite, Classe ClasseEntite, string Nom, float Experience, type Equipe, Liste<Statistique> ListStatistiques, Script ScriptEntite, Equipement[] TabEquipements, Terrain TerrainEntite) : base(IdEntite, ClasseEntite, Nom, Experience, Equipe, ListStatistiques, ScriptEntite, TerrainEntite, 0)
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
                        this.ListStatistiques.Add(stat_item);
                    }
                }
            }
        }

        public bool Attaquer(EntiteInconnu cible)
        {
            Arme arme = new Arme(0, null, "poing", Equipement.type.arme, new Effet[] { new Effet(Effet.type.ATT_neutre, 3, 5) }, new Zone(Zone.type.croix, 1, 1), new Zone(Zone.type.carre, 0, 0), Arme.typeArme.dague);
            foreach (Equipement invent in TabEquipements)
            {
                if (invent is Arme)
                {
                    arme = invent as Arme;
                    break;
                }
            }
            if (CaseEstDansZone(arme.ZonePortee.Type, arme.ZonePortee.PorteeMin, arme.ZonePortee.PorteeMax, Position, cible.Position))
            {
                foreach (Effet effet in arme.TabEffets)
                {
                    InfligerEffet(effet, arme.ZoneEffet, cible.Position);
                }
            }
            return false;
        }
        public bool Attaquer(Case cible)
        {
            Arme arme = new Arme(0, null, "poing", Equipement.type.arme, new Effet[] { new Effet(Effet.type.ATT_neutre, 3, 5) }, new Zone(Zone.type.croix, 1, 1), new Zone(Zone.type.carre, 0, 0), Arme.typeArme.dague);
            foreach (Equipement invent in TabEquipements)
            {
                if (invent is Arme)
                {
                    arme = invent as Arme;
                    break;
                }
            }
            if (CaseEstDansZone(arme.ZonePortee.Type, arme.ZonePortee.PorteeMin, arme.ZonePortee.PorteeMax, Position, cible))
            {
                foreach (Effet effet in arme.TabEffets)
                {
                    InfligerEffet(effet, arme.ZoneEffet, cible);
                }
            }
            return false;
        }
    }
}