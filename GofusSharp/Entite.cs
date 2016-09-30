namespace GofusSharp
{
    public class Entite : EntiteInconnu
    {
        public Script ScriptEntite { get; internal set; }
        public Terrain TerrainEntite { get; internal set; }
        public ListeChainee<EntiteInconnu> ListEntites { get; internal set; }
        internal Entite(int IdEntite, Classe ClasseEntite, string Nom, float Experience, Case Position, type Equipe, ListeChainee<Statistique> ListStatistiques, Script ScriptEntite, Terrain TerrainEntite, int Proprietaire) : base(IdEntite, ClasseEntite, Nom, Experience, Position, Equipe)
        {
            this.ListStatistiques = ListStatistiques;
            foreach (Statistique stat in ListStatistiques)
            {
                if (stat.Nom == Statistique.type.PA)
                    PA_MAX = stat.Valeur;
                if (stat.Nom == Statistique.type.PM)
                    PM_MAX = stat.Valeur;
            }
            this.ScriptEntite = ScriptEntite;
            this.ClasseEntite = ClasseEntite;
            this.Nom = Nom;
            this.Experience = Experience;
            this.TerrainEntite = TerrainEntite;
            this.Proprietaire = Proprietaire;
        }

        public bool UtiliserSort(Sort sort, EntiteInconnu cible)
        {
            if (CaseEstDansZone(sort.ZonePortee.Type, sort.ZonePortee.PorteeMin, sort.ZonePortee.PorteeMax, Position, cible.Position))
            {
                foreach (Effet effet in sort.TabEffets)
                {
                    InfligerEffet(effet, sort.ZoneEffet, cible.Position);
                }
            }
            return false;
        }

        public bool UtiliserSort(Sort sort, Case cible)
        {
            if (CaseEstDansZone(sort.ZonePortee.Type, sort.ZonePortee.PorteeMin, sort.ZonePortee.PorteeMax, Position, cible))
            {
                foreach (Effet effet in sort.TabEffets)
                {
                    InfligerEffet(effet, sort.ZoneEffet, cible);
                }
            }
            return false;
        }

        internal int InfligerEffet(Effet effet, Zone zoneEffet, Case source)
        {
            switch (effet.Nom)
            {
                case Effet.type.pousse:
                    break;
                case Effet.type.pousse_lanceur:
                    break;
                case Effet.type.tire:
                    break;
                case Effet.type.tire_lanceur:
                    break;
                case Effet.type.teleportation:
                    return (ChangerPosition(source) ? 1 : 0);
                case Effet.type.ATT_neutre:
                    Noeud<EntiteInconnu> entiteInconnu = ListEntites.First;
                    while (entiteInconnu != null)
                    {
                        if (CaseEstDansZone(zoneEffet.Type, zoneEffet.PorteeMin, zoneEffet.PorteeMax, source, entiteInconnu.Valeur.Position))
                        {
                            int force = 0;
                            int puissance = 0;
                            int DMG_neutre = 0;
                            int RES_neutre = 0;
                            int RES_Pourcent_neutre = 0;
                            int reduction_physique = 0;
                            foreach (Statistique stat in ListStatistiques)
                            {
                                if (stat.Nom == Statistique.type.force)
                                    force = stat.Valeur;
                                if (stat.Nom == Statistique.type.puissance)
                                    puissance = stat.Valeur;
                                if (stat.Nom == Statistique.type.DMG_neutre)
                                    DMG_neutre = stat.Valeur;
                            }
                            Noeud<Envoutement> envoutement = ListEnvoutements.First;
                            while (envoutement != null)
                            {
                                if (envoutement.Valeur.Stat == Statistique.type.force)
                                    force += envoutement.Valeur.Valeur;
                                if (envoutement.Valeur.Stat == Statistique.type.puissance)
                                    puissance += envoutement.Valeur.Valeur;
                                if (envoutement.Valeur.Stat == Statistique.type.DMG_neutre)
                                    DMG_neutre += envoutement.Valeur.Valeur;
                                envoutement = envoutement.Next;
                            }
                            if (force < 0)
                                force = 0;
                            if (puissance < 0)
                                puissance = 0;
                            if (DMG_neutre < 0)
                                DMG_neutre = 0;
                            Noeud<Statistique> stat_def = entiteInconnu.Valeur.ListStatistiques.First;
                            while (stat_def != null)
                            {
                                if (stat_def.Valeur.Nom == Statistique.type.RES_neutre)
                                    RES_neutre = stat_def.Valeur.Valeur;
                                if (stat_def.Valeur.Nom == Statistique.type.RES_Pourcent_neutre)
                                    RES_Pourcent_neutre = stat_def.Valeur.Valeur;
                                if (stat_def.Valeur.Nom == Statistique.type.reduction_physique)
                                    reduction_physique = stat_def.Valeur.Valeur;
                            }
                            Noeud<Envoutement> envoutement_def = entiteInconnu.Valeur.ListEnvoutements.First;
                            while (envoutement_def != null)
                            {
                                if (envoutement_def.Valeur.Stat == Statistique.type.RES_neutre)
                                    RES_neutre += envoutement_def.Valeur.Valeur;
                                if (envoutement_def.Valeur.Stat == Statistique.type.RES_Pourcent_neutre)
                                    RES_Pourcent_neutre += envoutement_def.Valeur.Valeur;
                                if (envoutement_def.Valeur.Stat == Statistique.type.reduction_physique)
                                    reduction_physique += envoutement_def.Valeur.Valeur;
                                envoutement_def = envoutement_def.Next;
                            }
                            if (RES_neutre < 0)
                                RES_neutre = 0;
                            if (RES_Pourcent_neutre < 0)
                                RES_Pourcent_neutre = 0;
                            if (reduction_physique < 0)
                                reduction_physique = 0;
                            entiteInconnu.Valeur.PV -= (1 - (RES_Pourcent_neutre / 100)) * ((new System.Random().Next(effet.ValeurMin, effet.ValeurMax) * (100 + force + puissance) / 100 + DMG_neutre) - RES_neutre - reduction_physique);
                        }
                        entiteInconnu = entiteInconnu.Next;
                    }
                    break;
                case Effet.type.ATT_air:
                    Noeud<EntiteInconnu> entiteInconnu2 = ListEntites.First;
                    while (entiteInconnu2 != null)
                    {
                        if (CaseEstDansZone(zoneEffet.Type, zoneEffet.PorteeMin, zoneEffet.PorteeMax, source, entiteInconnu2.Valeur.Position))
                        {
                            int agilite = 0;
                            int puissance = 0;
                            int DMG_air = 0;
                            int RES_air = 0;
                            int RES_Pourcent_air = 0;
                            int reduction_magique = 0;
                            foreach (Statistique stat in ListStatistiques)
                            {
                                if (stat.Nom == Statistique.type.agilite)
                                    agilite = stat.Valeur;
                                if (stat.Nom == Statistique.type.puissance)
                                    puissance = stat.Valeur;
                                if (stat.Nom == Statistique.type.DMG_air)
                                    DMG_air = stat.Valeur;
                            }
                            Noeud<Envoutement> envoutement = ListEnvoutements.First;
                            while (envoutement != null)
                            {
                                if (envoutement.Valeur.Stat == Statistique.type.agilite)
                                    agilite += envoutement.Valeur.Valeur;
                                if (envoutement.Valeur.Stat == Statistique.type.puissance)
                                    puissance += envoutement.Valeur.Valeur;
                                if (envoutement.Valeur.Stat == Statistique.type.DMG_air)
                                    DMG_air += envoutement.Valeur.Valeur;
                                envoutement = envoutement.Next;
                            }
                            if (agilite < 0)
                                agilite = 0;
                            if (puissance < 0)
                                puissance = 0;
                            if (DMG_air < 0)
                                DMG_air = 0;
                            Noeud<Statistique> stat_def = entiteInconnu2.Valeur.ListStatistiques.First;
                            while (stat_def != null)
                            {
                                if (stat_def.Valeur.Nom == Statistique.type.RES_air)
                                    RES_air = stat_def.Valeur.Valeur;
                                if (stat_def.Valeur.Nom == Statistique.type.RES_Pourcent_air)
                                    RES_Pourcent_air = stat_def.Valeur.Valeur;
                                if (stat_def.Valeur.Nom == Statistique.type.reduction_magique)
                                    reduction_magique = stat_def.Valeur.Valeur;
                            }
                            Noeud<Envoutement> envoutement_def = entiteInconnu2.Valeur.ListEnvoutements.First;
                            while (envoutement_def != null)
                            {
                                if (envoutement_def.Valeur.Stat == Statistique.type.RES_air)
                                    RES_air += envoutement_def.Valeur.Valeur;
                                if (envoutement_def.Valeur.Stat == Statistique.type.RES_Pourcent_air)
                                    RES_Pourcent_air += envoutement_def.Valeur.Valeur;
                                if (envoutement_def.Valeur.Stat == Statistique.type.reduction_magique)
                                    reduction_magique += envoutement_def.Valeur.Valeur;
                                envoutement_def = envoutement_def.Next;
                            }
                            if (RES_air < 0)
                                RES_air = 0;
                            if (RES_Pourcent_air < 0)
                                RES_Pourcent_air = 0;
                            if (reduction_magique < 0)
                                reduction_magique = 0;
                            entiteInconnu2.Valeur.PV -= (1 - (RES_Pourcent_air / 100)) * ((new System.Random().Next(effet.ValeurMin, effet.ValeurMax) * (100 + agilite + puissance) / 100 + DMG_air) - RES_air - reduction_magique);
                        }
                        entiteInconnu2 = entiteInconnu2.Next;
                    }
                    break;
                case Effet.type.ATT_feu:
                    Noeud<EntiteInconnu> entiteInconnu3 = ListEntites.First;
                    while (entiteInconnu3 != null)
                    {
                        if (CaseEstDansZone(zoneEffet.Type, zoneEffet.PorteeMin, zoneEffet.PorteeMax, source, entiteInconnu3.Valeur.Position))
                        {
                            int intelligence = 0;
                            int puissance = 0;
                            int DMG_feu = 0;
                            int RES_feu = 0;
                            int RES_Pourcent_feu = 0;
                            int reduction_magique = 0;
                            foreach (Statistique stat in ListStatistiques)
                            {
                                if (stat.Nom == Statistique.type.intelligence)
                                    intelligence = stat.Valeur;
                                if (stat.Nom == Statistique.type.puissance)
                                    puissance = stat.Valeur;
                                if (stat.Nom == Statistique.type.DMG_feu)
                                    DMG_feu = stat.Valeur;
                            }
                            Noeud<Envoutement> envoutement = ListEnvoutements.First;
                            while (envoutement != null)
                            {
                                if (envoutement.Valeur.Stat == Statistique.type.intelligence)
                                    intelligence += envoutement.Valeur.Valeur;
                                if (envoutement.Valeur.Stat == Statistique.type.puissance)
                                    puissance += envoutement.Valeur.Valeur;
                                if (envoutement.Valeur.Stat == Statistique.type.DMG_feu)
                                    DMG_feu += envoutement.Valeur.Valeur;
                                envoutement = envoutement.Next;
                            }
                            if (intelligence < 0)
                                intelligence = 0;
                            if (puissance < 0)
                                puissance = 0;
                            if (DMG_feu < 0)
                                DMG_feu = 0;
                            Noeud<Statistique> stat_def = entiteInconnu3.Valeur.ListStatistiques.First;
                            while (stat_def != null)
                            {
                                if (stat_def.Valeur.Nom == Statistique.type.RES_feu)
                                    RES_feu = stat_def.Valeur.Valeur;
                                if (stat_def.Valeur.Nom == Statistique.type.RES_Pourcent_feu)
                                    RES_Pourcent_feu = stat_def.Valeur.Valeur;
                                if (stat_def.Valeur.Nom == Statistique.type.reduction_magique)
                                    reduction_magique = stat_def.Valeur.Valeur;
                            }
                            Noeud<Envoutement> envoutement_def = entiteInconnu3.Valeur.ListEnvoutements.First;
                            while (envoutement_def != null)
                            {
                                if (envoutement_def.Valeur.Stat == Statistique.type.RES_feu)
                                    RES_feu += envoutement_def.Valeur.Valeur;
                                if (envoutement_def.Valeur.Stat == Statistique.type.RES_Pourcent_feu)
                                    RES_Pourcent_feu += envoutement_def.Valeur.Valeur;
                                if (envoutement_def.Valeur.Stat == Statistique.type.reduction_magique)
                                    reduction_magique += envoutement_def.Valeur.Valeur;
                                envoutement_def = envoutement_def.Next;
                            }
                            if (RES_feu < 0)
                                RES_feu = 0;
                            if (RES_Pourcent_feu < 0)
                                RES_Pourcent_feu = 0;
                            if (reduction_magique < 0)
                                reduction_magique = 0;
                            entiteInconnu3.Valeur.PV -= (1 - (RES_Pourcent_feu / 100)) * ((new System.Random().Next(effet.ValeurMin, effet.ValeurMax) * (100 + intelligence + puissance) / 100 + DMG_feu) - RES_feu - reduction_magique);
                        }
                        entiteInconnu3 = entiteInconnu3.Next;
                    }
                    break;
                case Effet.type.ATT_terre:
                    Noeud<EntiteInconnu> entiteInconnu4 = ListEntites.First;
                    while (entiteInconnu4 != null)
                    {
                        if (CaseEstDansZone(zoneEffet.Type, zoneEffet.PorteeMin, zoneEffet.PorteeMax, source, entiteInconnu4.Valeur.Position))
                        {
                            int force = 0;
                            int puissance = 0;
                            int DMG_terre = 0;
                            int RES_terre = 0;
                            int RES_Pourcent_terre = 0;
                            int reduction_physique = 0;
                            foreach (Statistique stat in ListStatistiques)
                            {
                                if (stat.Nom == Statistique.type.force)
                                    force = stat.Valeur;
                                if (stat.Nom == Statistique.type.puissance)
                                    puissance = stat.Valeur;
                                if (stat.Nom == Statistique.type.DMG_terre)
                                    DMG_terre = stat.Valeur;
                            }
                            Noeud<Envoutement> envoutement = ListEnvoutements.First;
                            while (envoutement != null)
                            {
                                if (envoutement.Valeur.Stat == Statistique.type.force)
                                    force += envoutement.Valeur.Valeur;
                                if (envoutement.Valeur.Stat == Statistique.type.puissance)
                                    puissance += envoutement.Valeur.Valeur;
                                if (envoutement.Valeur.Stat == Statistique.type.DMG_terre)
                                    DMG_terre += envoutement.Valeur.Valeur;
                                envoutement = envoutement.Next;
                            }
                            if (force < 0)
                                force = 0;
                            if (puissance < 0)
                                puissance = 0;
                            if (DMG_terre < 0)
                                DMG_terre = 0;
                            Noeud<Statistique> stat_def = entiteInconnu4.Valeur.ListStatistiques.First;
                            while (stat_def != null)
                            {
                                if (stat_def.Valeur.Nom == Statistique.type.RES_terre)
                                    RES_terre = stat_def.Valeur.Valeur;
                                if (stat_def.Valeur.Nom == Statistique.type.RES_Pourcent_terre)
                                    RES_Pourcent_terre = stat_def.Valeur.Valeur;
                                if (stat_def.Valeur.Nom == Statistique.type.reduction_physique)
                                    reduction_physique = stat_def.Valeur.Valeur;
                            }
                            Noeud<Envoutement> envoutement_def = entiteInconnu4.Valeur.ListEnvoutements.First;
                            while (envoutement_def != null)
                            {
                                if (envoutement_def.Valeur.Stat == Statistique.type.RES_terre)
                                    RES_terre += envoutement_def.Valeur.Valeur;
                                if (envoutement_def.Valeur.Stat == Statistique.type.RES_Pourcent_terre)
                                    RES_Pourcent_terre += envoutement_def.Valeur.Valeur;
                                if (envoutement_def.Valeur.Stat == Statistique.type.reduction_physique)
                                    reduction_physique += envoutement_def.Valeur.Valeur;
                                envoutement_def = envoutement_def.Next;
                            }
                            if (RES_terre < 0)
                                RES_terre = 0;
                            if (RES_Pourcent_terre < 0)
                                RES_Pourcent_terre = 0;
                            if (reduction_physique < 0)
                                reduction_physique = 0;
                            entiteInconnu4.Valeur.PV -= (1 - (RES_Pourcent_terre / 100)) * ((new System.Random().Next(effet.ValeurMin, effet.ValeurMax) * (100 + force + puissance) / 100 + DMG_terre) - RES_terre - reduction_physique);
                        }
                        entiteInconnu4 = entiteInconnu4.Next;
                    }
                    break;
                case Effet.type.ATT_eau:
                    Noeud<EntiteInconnu> entiteInconnu5 = ListEntites.First;
                    while (entiteInconnu5 != null)
                    {
                        if (CaseEstDansZone(zoneEffet.Type, zoneEffet.PorteeMin, zoneEffet.PorteeMax, source, entiteInconnu5.Valeur.Position))
                        {
                            int chance = 0;
                            int puissance = 0;
                            int DMG_eau = 0;
                            int RES_eau = 0;
                            int RES_Pourcent_eau = 0;
                            int reduction_magique = 0;
                            foreach (Statistique stat in ListStatistiques)
                            {
                                if (stat.Nom == Statistique.type.chance)
                                    chance = stat.Valeur;
                                if (stat.Nom == Statistique.type.puissance)
                                    puissance = stat.Valeur;
                                if (stat.Nom == Statistique.type.DMG_eau)
                                    DMG_eau = stat.Valeur;
                            }
                            Noeud<Envoutement> envoutement = ListEnvoutements.First;
                            while (envoutement != null)
                            {
                                if (envoutement.Valeur.Stat == Statistique.type.chance)
                                    chance += envoutement.Valeur.Valeur;
                                if (envoutement.Valeur.Stat == Statistique.type.puissance)
                                    puissance += envoutement.Valeur.Valeur;
                                if (envoutement.Valeur.Stat == Statistique.type.DMG_eau)
                                    DMG_eau += envoutement.Valeur.Valeur;
                                envoutement = envoutement.Next;
                            }
                            if (chance < 0)
                                chance = 0;
                            if (puissance < 0)
                                puissance = 0;
                            if (DMG_eau < 0)
                                DMG_eau = 0;
                            Noeud<Statistique> stat_def = entiteInconnu5.Valeur.ListStatistiques.First;
                            while (stat_def != null)
                            {
                                if (stat_def.Valeur.Nom == Statistique.type.RES_eau)
                                    RES_eau = stat_def.Valeur.Valeur;
                                if (stat_def.Valeur.Nom == Statistique.type.RES_Pourcent_eau)
                                    RES_Pourcent_eau = stat_def.Valeur.Valeur;
                                if (stat_def.Valeur.Nom == Statistique.type.reduction_magique)
                                    reduction_magique = stat_def.Valeur.Valeur;
                            }
                            Noeud<Envoutement> envoutement_def = entiteInconnu5.Valeur.ListEnvoutements.First;
                            while (envoutement_def != null)
                            {
                                if (envoutement_def.Valeur.Stat == Statistique.type.RES_eau)
                                    RES_eau += envoutement_def.Valeur.Valeur;
                                if (envoutement_def.Valeur.Stat == Statistique.type.RES_Pourcent_eau)
                                    RES_Pourcent_eau += envoutement_def.Valeur.Valeur;
                                if (envoutement_def.Valeur.Stat == Statistique.type.reduction_magique)
                                    reduction_magique += envoutement_def.Valeur.Valeur;
                                envoutement_def = envoutement_def.Next;
                            }
                            if (RES_eau < 0)
                                RES_eau = 0;
                            if (RES_Pourcent_eau < 0)
                                RES_Pourcent_eau = 0;
                            if (reduction_magique < 0)
                                reduction_magique = 0;
                            entiteInconnu5.Valeur.PV -= (int)(1 - (/*(float)*/RES_Pourcent_eau / 100)) * ((new System.Random().Next(effet.ValeurMin, effet.ValeurMax) * (100 + chance + puissance) / 100 + DMG_eau) - RES_eau - reduction_magique);
                        }
                        entiteInconnu5 = entiteInconnu5.Next;
                    }
                    break;
                case Effet.type.envoutement:
                    Noeud<EntiteInconnu> entiteInconnu6 = ListEntites.First;
                    while (entiteInconnu6 != null)
                    {
                        if (CaseEstDansZone(zoneEffet.Type, zoneEffet.PorteeMin, zoneEffet.PorteeMax, source, entiteInconnu6.Valeur.Position))
                        {
                            entiteInconnu6.Valeur.ListEnvoutements.AjouterFin(new Envoutement(effet.Stat, new System.Random().Next(effet.ValeurMin, effet.ValeurMax), effet.NbTour, IdEntite));
                        }
                        entiteInconnu = entiteInconnu6.Next;
                    }
                    break;
                case Effet.type.pose_piege:
                    break;
                case Effet.type.pose_glyphe:
                    break;
                case Effet.type.invocation:
                    //PlaceHolder ListEntites.AjouterFin(new EntiteInconnu(effet.ValeurMax, Classe.type.osamoda, "bouftou", 1000, source, Equipe, IdEntite);
                    break;
                case Effet.type.soin:
                    Noeud<EntiteInconnu> entiteInconnu7 = ListEntites.First;
                    while (entiteInconnu7 != null)
                    {
                        if (CaseEstDansZone(zoneEffet.Type, zoneEffet.PorteeMin, zoneEffet.PorteeMax, source, entiteInconnu7.Valeur.Position))
                        {
                            int intelligence = 0;
                            int soin = 0;
                            foreach (Statistique stat in ListStatistiques)
                            {
                                if (stat.Nom == Statistique.type.intelligence)
                                    intelligence = stat.Valeur;
                                if (stat.Nom == Statistique.type.soin)
                                    soin = stat.Valeur;
                            }
                            foreach (Envoutement envoutement in ListEnvoutements)
                            {
                                if (envoutement.Stat == Statistique.type.intelligence)
                                    intelligence = envoutement.Valeur;
                                if (envoutement.Stat == Statistique.type.soin)
                                    soin = envoutement.Valeur;
                            }
                            entiteInconnu7.Valeur.PV += new System.Random().Next(effet.ValeurMin, effet.ValeurMax) * (100 + intelligence) / 100 + soin;
                        }
                        entiteInconnu7 = entiteInconnu7.Next;
                    }
                    break;
                default:
                    break;
            }
            return 0;
        }

        private bool CaseEstDansZone(Zone.type TypeZone, int porteeMin, int porteeMax, Case source, Case cible)
        {
            switch (TypeZone)
            {
                case Zone.type.cercle:
                    int portee = TerrainEntite.DistanceEntreCases(source, cible);
                    if (portee >= porteeMin && portee <= porteeMax)
                    {
                        return true;
                    }
                    break;
                case Zone.type.ligne_verticale:
                    if (cible.X == source.X && Math.Abs(cible.Y - source.Y) >= porteeMin && Math.Abs(cible.Y - source.Y) <= porteeMax && (source.X > Position.X && source.X <= cible.X || source.X < Position.X && source.X >= cible.X) || cible.Y == source.Y && Math.Abs(cible.X - source.X) >= porteeMin && Math.Abs(cible.X - source.X) <= porteeMax && (source.Y > Position.Y && source.Y <= cible.Y || source.Y < Position.Y && source.Y >= cible.Y))
                    {
                        return true;
                    }
                    break;
                case Zone.type.ligne_horizontale:
                    if (cible.X == source.X && Math.Abs(cible.Y - source.Y) >= porteeMin && Math.Abs(cible.Y - source.Y) <= porteeMax && source.Y == Position.Y || cible.Y == source.Y && Math.Abs(cible.X - source.X) >= porteeMin && Math.Abs(cible.X - source.X) <= porteeMax && source.X == Position.X)
                    {
                        return true;
                    }
                    break;
                case Zone.type.carre:
                    if (Math.Abs(cible.Y - source.Y) >= porteeMin && Math.Abs(cible.Y - source.Y) <= porteeMax && Math.Abs(cible.X - source.X) >= porteeMin && Math.Abs(cible.X - source.X) <= porteeMax)
                    {
                        return true;
                    }
                    break;
                case Zone.type.croix:
                    if ((cible.X == source.X && Math.Abs(cible.Y - source.Y) >= porteeMin && Math.Abs(cible.Y - source.Y) <= porteeMax) || (cible.Y == source.Y && Math.Abs(cible.X - source.X) >= porteeMin && Math.Abs(cible.X - source.X) <= porteeMax))
                    {
                        return true;
                    }
                    break;
                case Zone.type.X:
                    if (cible.X - cible.Y == source.X - source.Y || cible.X + cible.Y == source.X + source.Y)
                    {
                        return true;
                    }
                    break;
                case Zone.type.T:
                    if ((cible.X == source.X && Math.Abs(cible.Y - source.Y) >= porteeMin && Math.Abs(cible.Y - source.Y) <= porteeMax) || (cible.Y == source.Y && Math.Abs(cible.X - source.X) >= porteeMin && Math.Abs(cible.X - source.X) <= porteeMax))
                    {
                        if (cible != Position)
                            return true;
                    }
                    break;
                case Zone.type.demi_cercle:
                    break;
                case Zone.type.cone:
                    break;
                case Zone.type.tous:
                    return true;
                default:
                    break;
            }
            return false;
        }

        public int AvancerVers(EntiteInconnu cible)
        {
            int PM_Debut = PM;
            while (PM > 0)
            {
                if (Math.Abs(Position.X - cible.Position.X) >= Math.Abs(Position.Y - cible.Position.Y))
                {
                    if (!ChangerPosition(TerrainEntite.TabCases[Position.X + (cible.Position.X - Position.X > 0 ? 1 : -1)][Position.Y]))
                    {
                        if (!ChangerPosition(TerrainEntite.TabCases[Position.X][Position.Y + (cible.Position.Y - Position.Y > 0 ? 1 : -1)]))
                        {
                            return PM_Debut - PM;
                        }
                    }
                }
                else
                {
                    if (!ChangerPosition(TerrainEntite.TabCases[Position.X][Position.Y + (cible.Position.Y - Position.Y > 0 ? 1 : -1)]))
                    {
                        if (!ChangerPosition(TerrainEntite.TabCases[Position.X + (cible.Position.X - Position.X > 0 ? 1 : -1)][Position.Y]))
                        {
                            return PM_Debut - PM;
                        }
                    }
                }
                PM--;
            }
            return PM_Debut - PM;
        }
        public int AvancerVers(EntiteInconnu cible, int PM_Alouer)
        {
            int PM_Debut = PM;
            while (PM > 0 && PM_Alouer > 0)
            {
                if (Math.Abs(Position.X - cible.Position.X) >= Math.Abs(Position.Y - cible.Position.Y))
                {
                    if (!ChangerPosition(TerrainEntite.TabCases[Position.X + (cible.Position.X - Position.X > 0 ? 1 : -1)][Position.Y]))
                    {
                        if (!ChangerPosition(TerrainEntite.TabCases[Position.X][Position.Y + (cible.Position.Y - Position.Y > 0 ? 1 : -1)]))
                        {
                            return PM_Debut - PM;
                        }
                    }
                }
                else
                {
                    if (!ChangerPosition(TerrainEntite.TabCases[Position.X][Position.Y + (cible.Position.Y - Position.Y > 0 ? 1 : -1)]))
                    {
                        if (!ChangerPosition(TerrainEntite.TabCases[Position.X + (cible.Position.X - Position.X > 0 ? 1 : -1)][Position.Y]))
                        {
                            return PM_Debut - PM;
                        }
                    }
                }
                PM--;
                PM_Alouer--;
            }
            return PM_Debut - PM;
        }

        public int AvancerVers(Case cible)
        {
            int PM_Debut = PM;
            while (PM > 0)
            {
                if (Math.Abs(Position.X - cible.X) >= Math.Abs(Position.Y - cible.Y))
                {
                    if (!ChangerPosition(TerrainEntite.TabCases[Position.X + (cible.X - Position.X > 0 ? 1 : -1)][Position.Y]))
                    {
                        if (!ChangerPosition(TerrainEntite.TabCases[Position.X][Position.Y + (cible.Y - Position.Y > 0 ? 1 : -1)]))
                        {
                            return PM_Debut - PM;
                        }
                    }
                }
                else
                {
                    if (!ChangerPosition(TerrainEntite.TabCases[Position.X][Position.Y + (cible.Y - Position.Y > 0 ? 1 : -1)]))
                    {
                        if (!ChangerPosition(TerrainEntite.TabCases[Position.X + (cible.X - Position.X > 0 ? 1 : -1)][Position.Y]))
                        {
                            return PM_Debut - PM;
                        }
                    }
                }
                PM--;
            }
            return PM_Debut - PM;
        }
        public int AvancerVers(Case cible, int PM_Alouer)
        {
            int PM_Debut = PM;
            while (PM > 0 && PM_Alouer > 0)
            {
                if (Math.Abs(Position.X - cible.X) >= Math.Abs(Position.Y - cible.Y))
                {
                    if (!ChangerPosition(TerrainEntite.TabCases[Position.X + (cible.X - Position.X > 0 ? 1 : -1)][Position.Y]))
                    {
                        if (!ChangerPosition(TerrainEntite.TabCases[Position.X][Position.Y + (cible.Y - Position.Y > 0 ? 1 : -1)]))
                        {
                            return PM_Debut - PM;
                        }
                    }
                }
                else
                {
                    if (!ChangerPosition(TerrainEntite.TabCases[Position.X][Position.Y + (cible.Y - Position.Y > 0 ? 1 : -1)]))
                    {
                        if (!ChangerPosition(TerrainEntite.TabCases[Position.X + (cible.X - Position.X > 0 ? 1 : -1)][Position.Y]))
                        {
                            return PM_Debut - PM;
                        }
                    }
                }
                PM--;
                PM_Alouer--;
            }
            return PM_Debut - PM;
        }

        internal bool ChangerPosition(Case nextPosition)
        {
            if (nextPosition.Contenu == Case.type.vide)
            {
                Position.Contenu = Case.type.vide;
                nextPosition.Contenu = Case.type.joueur;
                Position = nextPosition;
                Noeud<EntiteInconnu> entite = ListEntites.First;
                while (entite.Valeur.IdEntite != IdEntite)
                    entite = entite.Next;
                entite.Valeur.Position = nextPosition;
                return true;
            }
            return false;
        }
    }
}
