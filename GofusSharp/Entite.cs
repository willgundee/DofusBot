using System.Windows;
using System.Linq;

namespace GofusSharp
{
    public class Entite : EntiteInconnu
    {
        #region propriété

        internal string ScriptEntite { get; set; }
        internal Terrain TerrainEntite { get; set; }
        internal Liste<EntiteInconnu> ListEntites { get; set; }

        #endregion

        #region constucteur

        internal Entite() : base() { }

        internal Entite(Gofus.Entite entite, type Equipe, Terrain TerrainEntite) : base(entite, Equipe)
        {
            ScriptEntite = entite.ScriptEntite.Code;
            this.TerrainEntite = TerrainEntite;
        }

        #endregion

        #region utilitaire

        public EntiteInconnu EnnemiLePlusProche(Liste<EntiteInconnu> ListEntites)
        {
            int min = Math.Abs(ListEntites.First(x => x.Equipe != Equipe).Position.X - Position.X) + Math.Abs(ListEntites.First(x => x.Equipe != Equipe).Position.Y - Position.Y);
            EntiteInconnu eMin = ListEntites.First(x => x.Equipe != Equipe);
            foreach (EntiteInconnu x in ListEntites)
            {
                if (x.Equipe != Equipe && Math.Abs(x.Position.X - Position.X) + Math.Abs(x.Position.Y - Position.Y) < min)
                {
                    min = Math.Abs(x.Position.X - Position.X) + Math.Abs(x.Position.Y - Position.Y);
                    eMin = x;
                }
            }
            return eMin;
        }
        public EntiteInconnu AllieLePlusProche(Liste<EntiteInconnu> ListEntites)
        {
            int min = Math.Abs(ListEntites.First(x => x.Equipe == Equipe).Position.X - Position.X) + Math.Abs(ListEntites.First(x => x.Equipe == Equipe).Position.Y - Position.Y);
            EntiteInconnu eMin = ListEntites.First(x => x.Equipe == Equipe);
            foreach (EntiteInconnu x in ListEntites)
            {
                if (x.Equipe == Equipe && Math.Abs(x.Position.X - Position.X) + Math.Abs(x.Position.Y - Position.Y) < min)
                {
                    min = Math.Abs(x.Position.X - Position.X) + Math.Abs(x.Position.Y - Position.Y);
                    eMin = x;
                }
            }
            return eMin;
        }

        #endregion

        #region utilisation de sort

        #region UtiliserSort
        public bool UtiliserSort(Sort sort, EntiteInconnu cible)
        {
            if (!Debug.FCombat.Generation)
                Debug.FCombat.Dispatcher.Invoke(Debug.FCombat.DelLog, new object[] { "\n" + Nom + " lance " + sort.Nom + " sur " + cible.Nom });
            if (PA < sort.CoutPA)
            {
                if (!Debug.FCombat.Generation)
                    Debug.FCombat.Dispatcher.Invoke(Debug.FCombat.DelLog, new object[] { "\n" + Nom + " n'a pas assez de PA pour lancer " + sort.Nom });
                return false;
            }
            if (CaseEstDansZone(sort.ZonePortee.Type, sort.ZonePortee.PorteeMin, sort.ZonePortee.PorteeMax, Position, cible.Position))
            {
                PA -= sort.CoutPA;
                foreach (Effet effet in sort.ListEffets)
                {
                    InfligerEffet(effet, sort.ZoneEffet, cible.Position);
                }
                return true;
            }
            if (!Debug.FCombat.Generation)
                Debug.FCombat.Dispatcher.Invoke(Debug.FCombat.DelLog, new object[] { "\n" + cible.Nom + " est hors de portée du sort " + sort.Nom });
            return false;
        }
        public bool UtiliserSort(Sort sort, Case cible)
        {
            if (!Debug.FCombat.Generation)
                Debug.FCombat.Dispatcher.Invoke(Debug.FCombat.DelLog, new object[] { "\n" + Nom + " lance " + sort.Nom + " à X: " + cible.X.ToString() + " Y: " + cible.Y.ToString() });
            if (PA < sort.CoutPA)
            {
                if (!Debug.FCombat.Generation)
                    Debug.FCombat.Dispatcher.Invoke(Debug.FCombat.DelLog, new object[] { "\n" + Nom + " n'a pas assez de PA pour lancer " + sort.Nom });
                return false;
            }
            if (CaseEstDansZone(sort.ZonePortee.Type, sort.ZonePortee.PorteeMin, sort.ZonePortee.PorteeMax, Position, cible))
            {
                PA -= sort.CoutPA;
                foreach (Effet effet in sort.ListEffets)
                {
                    InfligerEffet(effet, sort.ZoneEffet, cible);
                }
                return true;
            }
            if (!Debug.FCombat.Generation)
                Debug.FCombat.Dispatcher.Invoke(Debug.FCombat.DelLog, new object[] { "\n" + cible.X + " Y: " + cible.Y + " est hors de portée du sort " + sort.Nom });
            return false;
        }
        public bool UtiliserSort(Sort.nom_sort vraiNom, EntiteInconnu cible)
        {
            Sort sort = null;
            try
            {
                sort = ClasseEntite.ListSorts.First(x => x.VraiNom == vraiNom);
            }
            catch (System.Exception)
            {
                if (!Debug.FCombat.Generation)
                    Debug.FCombat.Dispatcher.Invoke(Debug.FCombat.DelLog, new object[] { "\nle sort " + vraiNom.ToString() + " n'est pas à votre disposition" });
                return false;
            }
            if (!Debug.FCombat.Generation)
                Debug.FCombat.Dispatcher.Invoke(Debug.FCombat.DelLog, new object[] { "\n" + Nom + " lance " + sort.Nom + " sur " + cible.Nom });
            if (PA < sort.CoutPA)
            {
                if (!Debug.FCombat.Generation)
                    Debug.FCombat.Dispatcher.Invoke(Debug.FCombat.DelLog, new object[] { "\n" + Nom + " n'a pas assez de PA pour lancer " + sort.Nom });
                return false;
            }
            if (CaseEstDansZone(sort.ZonePortee.Type, sort.ZonePortee.PorteeMin, sort.ZonePortee.PorteeMax, Position, cible.Position))
            {
                PA -= sort.CoutPA;
                foreach (Effet effet in sort.ListEffets)
                {
                    InfligerEffet(effet, sort.ZoneEffet, cible.Position);
                }
                return true;
            }
            if (!Debug.FCombat.Generation)
                Debug.FCombat.Dispatcher.Invoke(Debug.FCombat.DelLog, new object[] { "\n" + cible.Nom + " est hors de portée du sort " + sort.Nom });
            return false;
        }
        public bool UtiliserSort(Sort.nom_sort vraiNom, Case cible)
        {
            Sort sort = null;
            try
            {
                sort = ClasseEntite.ListSorts.First(x => x.VraiNom == vraiNom);
            }
            catch (System.Exception)
            {
                if (!Debug.FCombat.Generation)
                    Debug.FCombat.Dispatcher.Invoke(Debug.FCombat.DelLog, new object[] { "\nle sort " + vraiNom.ToString() + " n'est pas à votre disposition" });
                return false;
            }
            if (!Debug.FCombat.Generation)
                Debug.FCombat.Dispatcher.Invoke(Debug.FCombat.DelLog, new object[] { "\n" + Nom + " lance " + sort.Nom + " à X: " + cible.X.ToString() + " Y: " + cible.Y.ToString() });
            if (PA < sort.CoutPA)
            {
                if (!Debug.FCombat.Generation)
                    Debug.FCombat.Dispatcher.Invoke(Debug.FCombat.DelLog, new object[] { "\n" + Nom + " n'a pas assez de PA pour lancer " + sort.Nom });
                return false;
            }
            if (CaseEstDansZone(sort.ZonePortee.Type, sort.ZonePortee.PorteeMin, sort.ZonePortee.PorteeMax, Position, cible))
            {
                PA -= sort.CoutPA;
                foreach (Effet effet in sort.ListEffets)
                {
                    InfligerEffet(effet, sort.ZoneEffet, cible);
                }
                return true;
            }
            if (!Debug.FCombat.Generation)
                Debug.FCombat.Dispatcher.Invoke(Debug.FCombat.DelLog, new object[] { "\n" + cible.X + " Y: " + cible.Y + " est hors de portée du sort " + sort.Nom });
            return false;
        }
        #endregion

        #region PeutUtiliserSort
        public bool PeutUtiliserSort(Sort sort, EntiteInconnu cible)
        {
            if (PA < sort.CoutPA)
                return false;
            if (CaseEstDansZone(sort.ZonePortee.Type, sort.ZonePortee.PorteeMin, sort.ZonePortee.PorteeMax, Position, cible.Position))
                return true;
            return false;
        }
        public bool PeutUtiliserSort(Sort sort, Case cible)
        {
            if (PA < sort.CoutPA)
                return false;
            if (CaseEstDansZone(sort.ZonePortee.Type, sort.ZonePortee.PorteeMin, sort.ZonePortee.PorteeMax, Position, cible))
                return true;
            return false;
        }
        public bool PeutUtiliserSort(Sort.nom_sort vraiNom, EntiteInconnu cible)
        {
            Sort sort = null;
            try
            {
                sort = ClasseEntite.ListSorts.First(x => x.VraiNom == vraiNom);
            }
            catch (System.Exception)
            {
                return false;
            }
            if (PA < sort.CoutPA)
                return false;
            if (CaseEstDansZone(sort.ZonePortee.Type, sort.ZonePortee.PorteeMin, sort.ZonePortee.PorteeMax, Position, cible.Position))
                return true;
            return false;
        }
        public bool PeutUtiliserSort(Sort.nom_sort vraiNom, Case cible)
        {
            Sort sort = null;
            try
            {
                sort = ClasseEntite.ListSorts.First(x => x.VraiNom == vraiNom);
            }
            catch (System.Exception)
            {
                return false;
            }
            if (PA < sort.CoutPA)
                return false;
            if (CaseEstDansZone(sort.ZonePortee.Type, sort.ZonePortee.PorteeMin, sort.ZonePortee.PorteeMax, Position, cible))
                return true;
            return false;
        }
        public bool PeutUtiliserSort(Sort sort, EntiteInconnu cible, Case source)
        {
            if (PA < sort.CoutPA)
                return false;
            if (CaseEstDansZone(sort.ZonePortee.Type, sort.ZonePortee.PorteeMin, sort.ZonePortee.PorteeMax, source, cible.Position))
                return true;
            return false;
        }
        public bool PeutUtiliserSort(Sort sort, Case cible, Case source)
        {
            if (PA < sort.CoutPA)
                return false;
            if (CaseEstDansZone(sort.ZonePortee.Type, sort.ZonePortee.PorteeMin, sort.ZonePortee.PorteeMax, source, cible))
                return true;
            return false;
        }
        public bool PeutUtiliserSort(Sort.nom_sort vraiNom, EntiteInconnu cible, Case source)
        {
            Sort sort = null;
            try
            {
                sort = ClasseEntite.ListSorts.First(x => x.VraiNom == vraiNom);
            }
            catch (System.Exception)
            {
                return false;
            }
            if (PA < sort.CoutPA)
                return false;
            if (CaseEstDansZone(sort.ZonePortee.Type, sort.ZonePortee.PorteeMin, sort.ZonePortee.PorteeMax, source, cible.Position))
                return true;
            return false;
        }
        public bool PeutUtiliserSort(Sort.nom_sort vraiNom, Case cible, Case source)
        {
            Sort sort = null;
            try
            {
                sort = ClasseEntite.ListSorts.First(x => x.VraiNom == vraiNom);
            }
            catch (System.Exception)
            {
                return false;
            }
            if (PA < sort.CoutPA)
                return false;
            if (CaseEstDansZone(sort.ZonePortee.Type, sort.ZonePortee.PorteeMin, sort.ZonePortee.PorteeMax, source, cible))
                return true;
            return false;
        }
        #endregion

        #region CasesPourUtiliserSort
        public Liste<Case> CasesPourUtiliserSort(Sort sort, EntiteInconnu cible)
        {
            Liste<Case> caseValide = new Liste<Case>();
            foreach (Case[] caseL in TerrainEntite.TabCases)
                foreach (Case CaseH in caseL)
                    if (PeutUtiliserSort(sort, cible))
                        caseValide.Add(CaseH);
            return caseValide;
        }
        public Liste<Case> CasesPourUtiliserSort(Sort.nom_sort sort, EntiteInconnu cible)
        {
            Liste<Case> caseValide = new Liste<Case>();
            foreach (Case[] caseL in TerrainEntite.TabCases)
                foreach (Case CaseH in caseL)
                    if (PeutUtiliserSort(sort, cible))
                        caseValide.Add(CaseH);
            return caseValide;
        }
        public Liste<Case> CasesPourUtiliserSort(Sort sort, Case cible)
        {
            Liste<Case> caseValide = new Liste<Case>();
            foreach (Case[] caseL in TerrainEntite.TabCases)
                foreach (Case CaseH in caseL)
                    if (PeutUtiliserSort(sort, cible))
                        caseValide.Add(CaseH);
            return caseValide;
        }
        public Liste<Case> CasesPourUtiliserSort(Sort.nom_sort sort, Case cible)
        {
            Liste<Case> caseValide = new Liste<Case>();
            foreach (Case[] caseL in TerrainEntite.TabCases)
                foreach (Case CaseH in caseL)
                    if (PeutUtiliserSort(sort, cible))
                        caseValide.Add(CaseH);
            return caseValide;
        }
        #endregion

        internal int InfligerEffet(Effet effet, Zone zoneEffet, Case source)
        {
            foreach (EntiteInconnu entiteInconnu in ListEntites)
            {
                try
                {
                    switch (effet.Nom)
                    {
                        case Effet.type.pousse:
                            if (CaseEstDansZone(zoneEffet.Type, zoneEffet.PorteeMin, zoneEffet.PorteeMax, source, entiteInconnu.Position))
                            {
                                int magnitude = Debug.FCombat.CombatCourant.Seed.Next(effet.ValeurMin, effet.ValeurMax);
                                int direction = 0;
                                string axe = "";
                                if (Position.X - entiteInconnu.Position.X != 0)
                                {
                                    direction = Position.X - entiteInconnu.Position.X;
                                    axe = "X";
                                }
                                else
                                {
                                    direction = Position.Y - entiteInconnu.Position.Y;
                                    axe = "Y";
                                }
                                int caseTraversee;
                                for (caseTraversee = 0; caseTraversee < magnitude; caseTraversee++)
                                {
                                    try
                                    {
                                        if (axe == "X")
                                        {
                                            if (!entiteInconnu.ChangerPosition(TerrainEntite.TabCases[entiteInconnu.Position.X - direction][entiteInconnu.Position.Y]))
                                                break;
                                        }
                                        else
                                        {
                                            if (!entiteInconnu.ChangerPosition(TerrainEntite.TabCases[entiteInconnu.Position.X][entiteInconnu.Position.Y - direction]))
                                                break;
                                        }
                                    }
                                    catch (System.Exception)
                                    {
                                        break;
                                    }
                                }
                                if (magnitude - caseTraversee == 0)
                                {
                                    if (!Debug.FCombat.Generation)
                                        Debug.FCombat.Dispatcher.Invoke(Debug.FCombat.DelLog, new object[] { "\n" + entiteInconnu.Nom + " recule de " + caseTraversee + " cases " });
                                    continue;
                                }
                                if (!Debug.FCombat.Generation)
                                    Debug.FCombat.Dispatcher.Invoke(Debug.FCombat.DelLog, new object[] { "\n" + entiteInconnu.Nom + " recule de " + caseTraversee + " cases et se cogne contre un obstacle" });
                                int DMG_poussee = 0;
                                foreach (Statistique stat in ListStatistiques)
                                {
                                    if (stat.Nom == Statistique.type.DMG_poussee)
                                        DMG_poussee = stat.Valeur;
                                }
                                foreach (Envoutement envoutement in ListEnvoutements)
                                {
                                    if (envoutement.Stat == Statistique.type.DMG_poussee)
                                        DMG_poussee = envoutement.Valeur;
                                }
                                int RES_poussee = 0;
                                foreach (Statistique stat in entiteInconnu.ListStatistiques)
                                {
                                    if (stat.Nom == Statistique.type.RES_poussee)
                                        RES_poussee = stat.Valeur;
                                }
                                foreach (Envoutement envoutement in entiteInconnu.ListEnvoutements)
                                {
                                    if (envoutement.Stat == Statistique.type.RES_poussee)
                                        RES_poussee = envoutement.Valeur;
                                }
                                if (entiteInconnu.recevoirDommages((8 + Debug.FCombat.CombatCourant.Seed.Next(1, 8) * RetourneNiveau() / 50) * (magnitude - caseTraversee) + DMG_poussee - RES_poussee))
                                {
                                    foreach (EntiteInconnu invoc in ListEntites)
                                    {
                                        if (invoc.Proprietaire == entiteInconnu.IdEntite)
                                        {
                                            invoc.Etat = typeEtat.mort;
                                            invoc.Position.Contenu = Case.type.vide;
                                            invoc.Position = null;
                                        }
                                    }
                                }
                            }
                            break;
                        case Effet.type.repousse:
                            break;
                        case Effet.type.repousse_lanceur:
                            break;
                        case Effet.type.tire:
                            if (CaseEstDansZone(zoneEffet.Type, zoneEffet.PorteeMin, zoneEffet.PorteeMax, source, entiteInconnu.Position))
                            {
                                int magnitude = Debug.FCombat.CombatCourant.Seed.Next(effet.ValeurMin, effet.ValeurMax);
                                int caseTraversee;
                                for (caseTraversee = 0; caseTraversee < magnitude; caseTraversee++)
                                {
                                    try
                                    {
                                        if (Position.X - entiteInconnu.Position.X != 0)
                                        {
                                            if (!entiteInconnu.ChangerPosition(TerrainEntite.TabCases[entiteInconnu.Position.X - (Position.X - entiteInconnu.Position.X > 0 ? 1 : -1)][entiteInconnu.Position.Y]))
                                                break;
                                        }
                                        else
                                        {
                                            if (!entiteInconnu.ChangerPosition(TerrainEntite.TabCases[entiteInconnu.Position.X][entiteInconnu.Position.Y - (Position.Y - entiteInconnu.Position.Y > 0 ? -1 : 1)]))
                                                break;
                                        }
                                    }
                                    catch (System.Exception)
                                    {
                                        break;
                                    }
                                }
                                if (!Debug.FCombat.Generation)
                                    Debug.FCombat.Dispatcher.Invoke(Debug.FCombat.DelLog, new object[] { "\n" + entiteInconnu.Nom + " est attiré de " + caseTraversee + " cases vers " + Nom });
                            }
                            break;
                        case Effet.type.tire_lanceur:
                            if (CaseEstDansZone(zoneEffet.Type, zoneEffet.PorteeMin, zoneEffet.PorteeMax, source, entiteInconnu.Position))
                            {
                                int magnitude = Debug.FCombat.CombatCourant.Seed.Next(effet.ValeurMin, effet.ValeurMax);
                                int caseTraversee;
                                for (caseTraversee = 0; caseTraversee < magnitude; caseTraversee++)
                                {
                                    try
                                    {
                                        if (entiteInconnu.Position.X - Position.X != 0)
                                        {
                                            if (!ChangerPosition(TerrainEntite.TabCases[entiteInconnu.Position.X - (entiteInconnu.Position.X - Position.X > 0 ? 1 : -1)][entiteInconnu.Position.Y]))
                                                break;
                                        }
                                        else
                                        {
                                            if (!ChangerPosition(TerrainEntite.TabCases[entiteInconnu.Position.X][entiteInconnu.Position.Y - (entiteInconnu.Position.Y - Position.Y > 0 ? -1 : 1)]))
                                                break;
                                        }
                                    }
                                    catch (System.Exception)
                                    {
                                        break;
                                    }
                                }
                                if (!Debug.FCombat.Generation)
                                    Debug.FCombat.Dispatcher.Invoke(Debug.FCombat.DelLog, new object[] { "\n" + entiteInconnu.Nom + " est attiré de " + caseTraversee + " cases vers " + Nom });
                            }
                            break;
                        case Effet.type.teleportation:
                            bool result = ChangerPosition(source);
                            if (result)
                            {
                                if (!Debug.FCombat.Generation)
                                    Debug.FCombat.Dispatcher.Invoke(Debug.FCombat.DelLog, new object[] { "\n" + Nom + " s'est téléporté a X: " + source.X + " Y: " + source.Y });
                            }
                            return (result ? 1 : 0);
                        case Effet.type.ATT_neutre:
                            if (CaseEstDansZone(zoneEffet.Type, zoneEffet.PorteeMin, zoneEffet.PorteeMax, source, entiteInconnu.Position))
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
                                foreach (Envoutement envoutement in ListEnvoutements)
                                {
                                    if (envoutement.Stat == Statistique.type.force)
                                        force += envoutement.Valeur;
                                    if (envoutement.Stat == Statistique.type.puissance)
                                        puissance += envoutement.Valeur;
                                    if (envoutement.Stat == Statistique.type.DMG_neutre)
                                        DMG_neutre += envoutement.Valeur;
                                }
                                if (force < 0)
                                    force = 0;
                                if (puissance < 0)
                                    puissance = 0;
                                if (DMG_neutre < 0)
                                    DMG_neutre = 0;
                                foreach (Statistique stat_def in entiteInconnu.ListStatistiques)
                                {
                                    if (stat_def.Nom == Statistique.type.RES_neutre)
                                        RES_neutre = stat_def.Valeur;
                                    if (stat_def.Nom == Statistique.type.RES_Pourcent_neutre)
                                        RES_Pourcent_neutre = stat_def.Valeur;
                                    if (stat_def.Nom == Statistique.type.reduction_physique)
                                        reduction_physique = stat_def.Valeur;
                                }
                                foreach (Envoutement envoutement_def in entiteInconnu.ListEnvoutements)
                                {
                                    if (envoutement_def.Stat == Statistique.type.RES_neutre)
                                        RES_neutre += envoutement_def.Valeur;
                                    if (envoutement_def.Stat == Statistique.type.RES_Pourcent_neutre)
                                        RES_Pourcent_neutre += envoutement_def.Valeur;
                                    if (envoutement_def.Stat == Statistique.type.reduction_physique)
                                        reduction_physique += envoutement_def.Valeur;
                                }
                                if (RES_neutre < 0)
                                    RES_neutre = 0;
                                if (RES_Pourcent_neutre < 0)
                                    RES_Pourcent_neutre = 0;
                                if (reduction_physique < 0)
                                    reduction_physique = 0;
                                if (entiteInconnu.recevoirDommages((1 - (RES_Pourcent_neutre / 100)) * ((Debug.FCombat.CombatCourant.Seed.Next(effet.ValeurMin, effet.ValeurMax) * (100 + force + puissance) / 100 + DMG_neutre) - RES_neutre - reduction_physique)))
                                {
                                    foreach (EntiteInconnu invoc in ListEntites)
                                    {
                                        if (invoc.Proprietaire == entiteInconnu.IdEntite)
                                        {
                                            invoc.Etat = typeEtat.mort;
                                            invoc.Position.Contenu = Case.type.vide;
                                            invoc.Position = null;
                                        }
                                    }
                                }
                            }
                            break;
                        case Effet.type.ATT_air:
                            if (CaseEstDansZone(zoneEffet.Type, zoneEffet.PorteeMin, zoneEffet.PorteeMax, source, entiteInconnu.Position))
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
                                foreach (Envoutement envoutement in ListEnvoutements)
                                {
                                    if (envoutement.Stat == Statistique.type.agilite)
                                        agilite += envoutement.Valeur;
                                    if (envoutement.Stat == Statistique.type.puissance)
                                        puissance += envoutement.Valeur;
                                    if (envoutement.Stat == Statistique.type.DMG_air)
                                        DMG_air += envoutement.Valeur;
                                }
                                if (agilite < 0)
                                    agilite = 0;
                                if (puissance < 0)
                                    puissance = 0;
                                if (DMG_air < 0)
                                    DMG_air = 0;
                                foreach (Statistique stat_def in entiteInconnu.ListStatistiques)
                                {
                                    if (stat_def.Nom == Statistique.type.RES_air)
                                        RES_air = stat_def.Valeur;
                                    if (stat_def.Nom == Statistique.type.RES_Pourcent_air)
                                        RES_Pourcent_air = stat_def.Valeur;
                                    if (stat_def.Nom == Statistique.type.reduction_magique)
                                        reduction_magique = stat_def.Valeur;
                                }
                                foreach (Envoutement envoutement_def in entiteInconnu.ListEnvoutements)
                                {
                                    if (envoutement_def.Stat == Statistique.type.RES_air)
                                        RES_air += envoutement_def.Valeur;
                                    if (envoutement_def.Stat == Statistique.type.RES_Pourcent_air)
                                        RES_Pourcent_air += envoutement_def.Valeur;
                                    if (envoutement_def.Stat == Statistique.type.reduction_magique)
                                        reduction_magique += envoutement_def.Valeur;
                                }
                                if (RES_air < 0)
                                    RES_air = 0;
                                if (RES_Pourcent_air < 0)
                                    RES_Pourcent_air = 0;
                                if (reduction_magique < 0)
                                    reduction_magique = 0;
                                if (entiteInconnu.recevoirDommages((1 - (RES_Pourcent_air / 100)) * ((Debug.FCombat.CombatCourant.Seed.Next(effet.ValeurMin, effet.ValeurMax) * (100 + agilite + puissance) / 100 + DMG_air) - RES_air - reduction_magique)))
                                {
                                    foreach (EntiteInconnu invoc in ListEntites)
                                    {
                                        if (invoc.Proprietaire == entiteInconnu.IdEntite)
                                        {
                                            invoc.Etat = typeEtat.mort;
                                            invoc.Position.Contenu = Case.type.vide;
                                            invoc.Position = null;
                                        }
                                    }
                                }
                            }
                            break;
                        case Effet.type.ATT_feu:
                            if (CaseEstDansZone(zoneEffet.Type, zoneEffet.PorteeMin, zoneEffet.PorteeMax, source, entiteInconnu.Position))
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
                                foreach (Envoutement envoutement in ListEnvoutements)
                                {
                                    if (envoutement.Stat == Statistique.type.intelligence)
                                        intelligence += envoutement.Valeur;
                                    if (envoutement.Stat == Statistique.type.puissance)
                                        puissance += envoutement.Valeur;
                                    if (envoutement.Stat == Statistique.type.DMG_feu)
                                        DMG_feu += envoutement.Valeur;
                                }
                                if (intelligence < 0)
                                    intelligence = 0;
                                if (puissance < 0)
                                    puissance = 0;
                                if (DMG_feu < 0)
                                    DMG_feu = 0;
                                foreach (Statistique stat_def in entiteInconnu.ListStatistiques)
                                {
                                    if (stat_def.Nom == Statistique.type.RES_feu)
                                        RES_feu = stat_def.Valeur;
                                    if (stat_def.Nom == Statistique.type.RES_Pourcent_feu)
                                        RES_Pourcent_feu = stat_def.Valeur;
                                    if (stat_def.Nom == Statistique.type.reduction_magique)
                                        reduction_magique = stat_def.Valeur;
                                }
                                foreach (Envoutement envoutement_def in entiteInconnu.ListEnvoutements)
                                {
                                    if (envoutement_def.Stat == Statistique.type.RES_feu)
                                        RES_feu += envoutement_def.Valeur;
                                    if (envoutement_def.Stat == Statistique.type.RES_Pourcent_feu)
                                        RES_Pourcent_feu += envoutement_def.Valeur;
                                    if (envoutement_def.Stat == Statistique.type.reduction_magique)
                                        reduction_magique += envoutement_def.Valeur;
                                }
                                if (RES_feu < 0)
                                    RES_feu = 0;
                                if (RES_Pourcent_feu < 0)
                                    RES_Pourcent_feu = 0;
                                if (reduction_magique < 0)
                                    reduction_magique = 0;
                                if (entiteInconnu.recevoirDommages((1 - (RES_Pourcent_feu / 100)) * ((Debug.FCombat.CombatCourant.Seed.Next(effet.ValeurMin, effet.ValeurMax) * (100 + intelligence + puissance) / 100 + DMG_feu) - RES_feu - reduction_magique)))
                                {
                                    foreach (EntiteInconnu invoc in ListEntites)
                                    {
                                        if (invoc.Proprietaire == entiteInconnu.IdEntite)
                                        {
                                            invoc.Etat = typeEtat.mort;
                                            invoc.Position.Contenu = Case.type.vide;
                                            invoc.Position = null;
                                        }
                                    }
                                }
                            }
                            break;
                        case Effet.type.ATT_terre:
                            if (CaseEstDansZone(zoneEffet.Type, zoneEffet.PorteeMin, zoneEffet.PorteeMax, source, entiteInconnu.Position))
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
                                foreach (Envoutement envoutement in ListEnvoutements)
                                {
                                    if (envoutement.Stat == Statistique.type.force)
                                        force += envoutement.Valeur;
                                    if (envoutement.Stat == Statistique.type.puissance)
                                        puissance += envoutement.Valeur;
                                    if (envoutement.Stat == Statistique.type.DMG_terre)
                                        DMG_terre += envoutement.Valeur;
                                }
                                if (force < 0)
                                    force = 0;
                                if (puissance < 0)
                                    puissance = 0;
                                if (DMG_terre < 0)
                                    DMG_terre = 0;
                                foreach (Statistique stat_def in entiteInconnu.ListStatistiques)
                                {
                                    if (stat_def.Nom == Statistique.type.RES_terre)
                                        RES_terre = stat_def.Valeur;
                                    if (stat_def.Nom == Statistique.type.RES_Pourcent_terre)
                                        RES_Pourcent_terre = stat_def.Valeur;
                                    if (stat_def.Nom == Statistique.type.reduction_physique)
                                        reduction_physique = stat_def.Valeur;
                                }
                                foreach (Envoutement envoutement_def in entiteInconnu.ListEnvoutements)
                                {
                                    if (envoutement_def.Stat == Statistique.type.RES_terre)
                                        RES_terre += envoutement_def.Valeur;
                                    if (envoutement_def.Stat == Statistique.type.RES_Pourcent_terre)
                                        RES_Pourcent_terre += envoutement_def.Valeur;
                                    if (envoutement_def.Stat == Statistique.type.reduction_physique)
                                        reduction_physique += envoutement_def.Valeur;
                                }
                                if (RES_terre < 0)
                                    RES_terre = 0;
                                if (RES_Pourcent_terre < 0)
                                    RES_Pourcent_terre = 0;
                                if (reduction_physique < 0)
                                    reduction_physique = 0;
                                if (entiteInconnu.recevoirDommages((1 - (RES_Pourcent_terre / 100)) * ((Debug.FCombat.CombatCourant.Seed.Next(effet.ValeurMin, effet.ValeurMax) * (100 + force + puissance) / 100 + DMG_terre) - RES_terre - reduction_physique)))
                                {
                                    foreach (EntiteInconnu invoc in ListEntites)
                                    {
                                        if (invoc.Proprietaire == entiteInconnu.IdEntite)
                                        {
                                            invoc.Etat = typeEtat.mort;
                                            invoc.Position.Contenu = Case.type.vide;
                                            invoc.Position = null;
                                        }
                                    }
                                }
                            }
                            break;
                        case Effet.type.ATT_eau:
                            if (CaseEstDansZone(zoneEffet.Type, zoneEffet.PorteeMin, zoneEffet.PorteeMax, source, entiteInconnu.Position))
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
                                foreach (Envoutement envoutement in ListEnvoutements)
                                {
                                    if (envoutement.Stat == Statistique.type.chance)
                                        chance += envoutement.Valeur;
                                    if (envoutement.Stat == Statistique.type.puissance)
                                        puissance += envoutement.Valeur;
                                    if (envoutement.Stat == Statistique.type.DMG_eau)
                                        DMG_eau += envoutement.Valeur;
                                }
                                if (chance < 0)
                                    chance = 0;
                                if (puissance < 0)
                                    puissance = 0;
                                if (DMG_eau < 0)
                                    DMG_eau = 0;
                                foreach (Statistique stat_def in entiteInconnu.ListStatistiques)
                                {
                                    if (stat_def.Nom == Statistique.type.RES_eau)
                                        RES_eau = stat_def.Valeur;
                                    if (stat_def.Nom == Statistique.type.RES_Pourcent_eau)
                                        RES_Pourcent_eau = stat_def.Valeur;
                                    if (stat_def.Nom == Statistique.type.reduction_magique)
                                        reduction_magique = stat_def.Valeur;
                                }
                                foreach (Envoutement envoutement_def in entiteInconnu.ListEnvoutements)
                                {
                                    if (envoutement_def.Stat == Statistique.type.RES_eau)
                                        RES_eau += envoutement_def.Valeur;
                                    if (envoutement_def.Stat == Statistique.type.RES_Pourcent_eau)
                                        RES_Pourcent_eau += envoutement_def.Valeur;
                                    if (envoutement_def.Stat == Statistique.type.reduction_magique)
                                        reduction_magique += envoutement_def.Valeur;
                                }
                                if (RES_eau < 0)
                                    RES_eau = 0;
                                if (RES_Pourcent_eau < 0)
                                    RES_Pourcent_eau = 0;
                                if (reduction_magique < 0)
                                    reduction_magique = 0;
                                if (entiteInconnu.recevoirDommages((1 - (RES_Pourcent_eau / 100)) * ((Debug.FCombat.CombatCourant.Seed.Next(effet.ValeurMin, effet.ValeurMax) * (100 + chance + puissance) / 100 + DMG_eau) - RES_eau - reduction_magique)))
                                {
                                    foreach (EntiteInconnu invoc in ListEntites)
                                    {
                                        if (invoc.Proprietaire == entiteInconnu.IdEntite)
                                        {
                                            invoc.Etat = typeEtat.mort;
                                            invoc.Position.Contenu = Case.type.vide;
                                            invoc.Position = null;
                                        }
                                    }
                                }
                            }
                            break;
                        case Effet.type.envoutement:
                            if (CaseEstDansZone(zoneEffet.Type, zoneEffet.PorteeMin, zoneEffet.PorteeMax, source, entiteInconnu.Position))
                            {
                                entiteInconnu.ListEnvoutements.Add(new Envoutement(effet.Stat, Debug.FCombat.CombatCourant.Seed.Next(effet.ValeurMin, effet.ValeurMax), effet.NbTour.GetValueOrDefault(), IdEntite));
                            }
                            break;
                        case Effet.type.pose_piege:
                            break;
                        case Effet.type.pose_glyphe:
                            break;
                        case Effet.type.invocation:
                            //ListEntites.Add(new EntiteInconnu(effet.ValeurMax, new Classe(48,), "bouftou", 1000, source, Equipe, IdEntite));
                            break;
                        case Effet.type.soin:
                            if (CaseEstDansZone(zoneEffet.Type, zoneEffet.PorteeMin, zoneEffet.PorteeMax, source, entiteInconnu.Position))
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
                                entiteInconnu.PV += Debug.FCombat.CombatCourant.Seed.Next(effet.ValeurMin, effet.ValeurMax) * (100 + intelligence) / 100 + soin;
                            }
                            break;
                        default:
                            break;
                    }
                }
                catch (System.Exception)
                {
                    continue;
                }
            }
            return 0;
        }
        private void castShadow(Case source, int x, int y, int[][] tabCase)
        {
            if (tabCase[x][y] != 1)
                return;
            double px = source.X + 0.5;
            double py = source.Y + 0.5;

            double pente_min = Pente(px, py, x, y);
            double pente_max = Pente(px, py, x, y);

            for (int i = x; i < x + 2; i++)
                for (int j = y; j < y + 2; j++)
                    if (pente_max < Pente(px, py, i, j))
                        pente_max = Pente(px, py, i, j);
                    else if (pente_min > Pente(px, py, i, j))
                        pente_min = Pente(px, py, i, j);

            for (int cx = 0; cx <= tabCase.Length; cx++)
            {
                for (int cy = 0; cy <= tabCase[0].Length; cy++)
                {
                    if (tabCase[cx][cy] == -1)
                        continue;
                    if (cx != x || cy != y)
                    {
                        double pente = Pente(px, py, cx, cy);
                        if (((pente > pente_min) && (pente < pente_max)) || (pente == 999999 && ((source.Y < y && y < cy) || (source.Y > y && y > cy)) && Pente(source.X, source.Y, x, y) == 999999))
                        {
                            tabCase[cx][cy] = -1;
                        }
                    }
                }
            }
        }
        private double Pente(double x1, double y1, double x2, double y2)
        {
            if (x1 - x2 == 0)
                return 999999;
            return (y1 - y2) / (x1 - x2);
        }
        public bool EstEnLigneDeVue(Case source, Case cible)
        {
            int[][] tabCase = new int[TerrainEntite.Largeur][];
            for (int i = 0; i < TerrainEntite.Largeur; i++)
            {
                tabCase[i] = new int[TerrainEntite.Hauteur];
            }
            for (int i = 0; i < TerrainEntite.Largeur; i++)
            {
                for (int j = 0; j < TerrainEntite.Hauteur; j++)
                {
                    switch (TerrainEntite.TabCases[i][j].Contenu)
                    {
                        case Case.type.joueur:
                        case Case.type.obstacle:
                            tabCase[i][j] = 1;
                            break;
                        default:
                            tabCase[i][j] = 0;
                            break;
                    }
                }
            }
            for (int i = 0; i < TerrainEntite.Largeur; i++)
            {
                for (int j = 0; j < TerrainEntite.Hauteur; j++)
                {
                    castShadow(source, i, j, tabCase);
                }
            }
            if (tabCase[cible.X][cible.Y] != 0)
            {
                return false;
            }
            return true;
        }

        internal bool CaseEstDansZone(Zone.type TypeZone, int porteeMin, int porteeMax, Case source, Case cible, bool ligneDeVue = false)
        {
            try
            {
                if (ligneDeVue)
                {
                    if (!EstEnLigneDeVue(source, cible))
                    {
                        return false;
                    }
                }
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
                        if ((Math.Abs(cible.Y - source.Y) >= porteeMin && Math.Abs(cible.Y - source.Y) <= porteeMax &&  Math.Abs(cible.X - source.X) <= porteeMax) || (Math.Abs(cible.X - source.X) >= porteeMin && Math.Abs(cible.X - source.X) <= porteeMax && Math.Abs(cible.Y - source.Y) <= porteeMax))
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
            catch (System.Exception e)
            {
                //System.Windows.Forms.MessageBox.Show(string.Format("Érreur : {0}", e));
                return false;
            }
        }

        #endregion

        #region deplacement

        public int AvancerVers(EntiteInconnu cible)
        {
            int PM_Debut = PM;
            while (PM > 0 && Position != cible.Position)
            {
                if (Position.X - cible.Position.X == 0)
                {
                    if (!ChangerPosition(TerrainEntite.TabCases[Position.X][Position.Y + (cible.Position.Y - Position.Y > 0 ? 1 : -1)]))
                    {
                        return PM_Debut - PM;
                    }
                }
                else if (Position.Y - cible.Position.Y == 0)
                {
                    if (!ChangerPosition(TerrainEntite.TabCases[Position.X + (cible.Position.X - Position.X > 0 ? 1 : -1)][Position.Y]))
                    {
                        return PM_Debut - PM;
                    }
                }
                else if (Math.Abs(Position.X - cible.Position.X) >= Math.Abs(Position.Y - cible.Position.Y))
                {
                    if (!ChangerPosition(TerrainEntite.TabCases[Position.X + (cible.Position.X - Position.X >= 0 ? 1 : -1)][Position.Y]))
                    {
                        if (!ChangerPosition(TerrainEntite.TabCases[Position.X][Position.Y + (cible.Position.Y - Position.Y >= 0 ? 1 : -1)]))
                        {
                            return PM_Debut - PM;
                        }
                    }
                }
                else
                {
                    if (!ChangerPosition(TerrainEntite.TabCases[Position.X][Position.Y + (cible.Position.Y - Position.Y >= 0 ? 1 : -1)]))
                    {
                        if (!ChangerPosition(TerrainEntite.TabCases[Position.X + (cible.Position.X - Position.X >= 0 ? 1 : -1)][Position.Y]))
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
            while (PM > 0 && PM_Alouer > 0 && Position != cible.Position)
            {
                if (Position.X - cible.Position.X == 0)
                {
                    if (!ChangerPosition(TerrainEntite.TabCases[Position.X][Position.Y + (cible.Position.Y - Position.Y > 0 ? 1 : -1)]))
                    {
                        return PM_Debut - PM;
                    }
                }
                else if (Position.Y - cible.Position.Y == 0)
                {
                    if (!ChangerPosition(TerrainEntite.TabCases[Position.X + (cible.Position.X - Position.X > 0 ? 1 : -1)][Position.Y]))
                    {
                        return PM_Debut - PM;
                    }
                }
                else if (Math.Abs(Position.X - cible.Position.X) >= Math.Abs(Position.Y - cible.Position.Y))
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
            while (PM > 0 && Position != cible)
            {
                if (Position.X - cible.X == 0)
                {
                    if (!ChangerPosition(TerrainEntite.TabCases[Position.X][Position.Y + (cible.Y - Position.Y > 0 ? 1 : -1)]))
                    {
                        return PM_Debut - PM;
                    }
                }
                else if (Position.Y - cible.Y == 0)
                {
                    if (!ChangerPosition(TerrainEntite.TabCases[Position.X + (cible.X - Position.X > 0 ? 1 : -1)][Position.Y]))
                    {
                        return PM_Debut - PM;
                    }
                }
                else if (Math.Abs(Position.X - cible.X) >= Math.Abs(Position.Y - cible.Y))
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
                if (Position == cible)
                    return PM_Debut - PM;
            }
            return PM_Debut - PM;
        }
        public int AvancerVers(Case cible, int PM_Alouer)
        {
            int PM_Debut = PM;
            while (PM > 0 && PM_Alouer > 0 && Position != cible)
            {
                if (Position.X - cible.X == 0)
                {
                    if (!ChangerPosition(TerrainEntite.TabCases[Position.X][Position.Y + (cible.Y - Position.Y > 0 ? 1 : -1)]))
                    {
                        return PM_Debut - PM;
                    }
                }
                else if (Position.Y - cible.Y == 0)
                {
                    if (!ChangerPosition(TerrainEntite.TabCases[Position.X + (cible.X - Position.X > 0 ? 1 : -1)][Position.Y]))
                    {
                        return PM_Debut - PM;
                    }
                }
                else if (Math.Abs(Position.X - cible.X) >= Math.Abs(Position.Y - cible.Y))
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
                if (Position == cible)
                    return PM_Debut - PM;
            }
            return PM_Debut - PM;
        }

        public int SEloignerDe(Case cible)
        {
            int PM_Debut = PM;
            try
            {
                Case Rcible = new Case(0, 0, Case.type.vide);
                while (PM > 0)
                {
                    Rcible.X = Position.X - (cible.X - Position.X);
                    Rcible.Y = Position.Y - (cible.Y - Position.Y);

                    if (Position.X - Rcible.X == 0)
                    {
                        int direction = Debug.FCombat.CombatCourant.Seed.Next(-1, 1);
                        while (direction == 0)
                            direction = Debug.FCombat.CombatCourant.Seed.Next(-1, 1);
                        if (Position.X + direction < 0 || Position.X + direction >= TerrainEntite.TabCases.Length || TerrainEntite.TabCases[Position.X + direction][Position.Y] == null || !ChangerPosition(TerrainEntite.TabCases[Position.X + direction][Position.Y]))
                        {
                            direction *= -1;
                            if (Position.X + direction < 0 || Position.X + direction >= TerrainEntite.TabCases.Length || TerrainEntite.TabCases[Position.X + direction][Position.Y] == null || !ChangerPosition(TerrainEntite.TabCases[Position.X + direction][Position.Y]))
                            {
                                if (Position.Y + (Rcible.Y - Position.Y > 0 ? 1 : -1) < 0 || Position.Y + (Rcible.Y - Position.Y > 0 ? 1 : -1) >= TerrainEntite.TabCases[0].Length || TerrainEntite.TabCases[Position.X][Position.Y + (Rcible.Y - Position.Y > 0 ? 1 : -1)] == null || !ChangerPosition(TerrainEntite.TabCases[Position.X][Position.Y + (Rcible.Y - Position.Y > 0 ? 1 : -1)]))
                                {
                                    return PM_Debut - PM;
                                }
                            }
                        }
                    }
                    else if (Position.Y - Rcible.Y == 0)
                    {
                        int direction = Debug.FCombat.CombatCourant.Seed.Next(-1, 1);
                        while (direction == 0)
                            direction = Debug.FCombat.CombatCourant.Seed.Next(-1, 1);
                        if (Position.Y + direction < 0 || Position.Y + direction >= TerrainEntite.TabCases[0].Length || TerrainEntite.TabCases[Position.X][Position.Y + direction] == null || !ChangerPosition(TerrainEntite.TabCases[Position.X][Position.Y + direction]))
                        {
                            direction *= -1;
                            if (Position.Y + direction < 0 || Position.Y + direction >= TerrainEntite.TabCases[0].Length || TerrainEntite.TabCases[Position.X][Position.Y + direction] == null || !ChangerPosition(TerrainEntite.TabCases[Position.X][Position.Y + direction]))
                            {
                                if (Position.X + (Rcible.X - Position.X > 0 ? 1 : -1) < 0 || Position.X + (Rcible.X - Position.X > 0 ? 1 : -1) >= TerrainEntite.TabCases.Length || TerrainEntite.TabCases[Position.X + (Rcible.X - Position.X > 0 ? 1 : -1)][Position.Y] == null || !ChangerPosition(TerrainEntite.TabCases[Position.X + (Rcible.X - Position.X > 0 ? 1 : -1)][Position.Y]))
                                {
                                    return PM_Debut - PM;
                                }
                            }
                        }
                    }
                    else if (Math.Abs(Position.X - Rcible.X) < Math.Abs(Position.Y - Rcible.Y))
                    {
                        if (Position.X + (Rcible.X - Position.X > 0 ? 1 : -1) < 0 || Position.X + (Rcible.X - Position.X > 0 ? 1 : -1) >= TerrainEntite.TabCases.Length || TerrainEntite.TabCases[Position.X + (Rcible.X - Position.X > 0 ? 1 : -1)][Position.Y] == null || !ChangerPosition(TerrainEntite.TabCases[Position.X + (Rcible.X - Position.X > 0 ? 1 : -1)][Position.Y]))
                        {
                            if (Position.Y + (Rcible.Y - Position.Y > 0 ? 1 : -1) < 0 || Position.Y + (Rcible.Y - Position.Y > 0 ? 1 : -1) >= TerrainEntite.TabCases[0].Length || TerrainEntite.TabCases[Position.X][Position.Y + (Rcible.Y - Position.Y > 0 ? 1 : -1)] == null || !ChangerPosition(TerrainEntite.TabCases[Position.X][Position.Y + (Rcible.Y - Position.Y > 0 ? 1 : -1)]))
                            {
                                return PM_Debut - PM;
                            }
                        }
                    }
                    else
                    {
                        if (Position.Y + (Rcible.Y - Position.Y > 0 ? 1 : -1) < 0 || Position.Y + (Rcible.Y - Position.Y > 0 ? 1 : -1) >= TerrainEntite.TabCases[0].Length || TerrainEntite.TabCases[Position.X][Position.Y + (Rcible.Y - Position.Y > 0 ? 1 : -1)] == null || !ChangerPosition(TerrainEntite.TabCases[Position.X][Position.Y + (Rcible.Y - Position.Y > 0 ? 1 : -1)]))
                        {
                            if (Position.X + (Rcible.X - Position.X > 0 ? 1 : -1) < 0 || Position.X + (Rcible.X - Position.X > 0 ? 1 : -1) >= TerrainEntite.TabCases.Length || TerrainEntite.TabCases[Position.X + (Rcible.X - Position.X > 0 ? 1 : -1)][Position.Y] == null || !ChangerPosition(TerrainEntite.TabCases[Position.X + (Rcible.X - Position.X > 0 ? 1 : -1)][Position.Y]))
                            {
                                return PM_Debut - PM;
                            }
                        }
                    }
                    PM--;
                }
                return PM_Debut - PM;
            }
            catch (System.Exception e)
            {
                if (!Debug.FCombat.Generation)
                    System.Windows.Forms.MessageBox.Show(string.Format("Érreur : {0}", e));
                return PM_Debut - PM;
            }
        }
        public int SEloignerDe(Case cible, int PM_Alouer)
        {
            int PM_Debut = PM;
            try
            {
                Case Rcible = new Case(0, 0, Case.type.vide);
                while (PM > 0 && PM_Alouer > 0)
                {
                    Rcible.X = Position.X - (cible.X - Position.X);
                    Rcible.Y = Position.Y - (cible.Y - Position.Y);

                    if (Position.X - Rcible.X == 0)
                    {
                        int direction = Debug.FCombat.CombatCourant.Seed.Next(-1, 1);
                        while (direction == 0)
                            direction = Debug.FCombat.CombatCourant.Seed.Next(-1, 1);
                        if (Position.X + direction < 0 || Position.X + direction >= TerrainEntite.TabCases.Length || TerrainEntite.TabCases[Position.X + direction][Position.Y] == null || !ChangerPosition(TerrainEntite.TabCases[Position.X + direction][Position.Y]))
                        {
                            direction *= -1;
                            if (Position.X + direction < 0 || Position.X + direction >= TerrainEntite.TabCases.Length || TerrainEntite.TabCases[Position.X + direction][Position.Y] == null || !ChangerPosition(TerrainEntite.TabCases[Position.X + direction][Position.Y]))
                            {
                                if (Position.Y + (Rcible.Y - Position.Y > 0 ? 1 : -1) < 0 || Position.Y + (Rcible.Y - Position.Y > 0 ? 1 : -1) >= TerrainEntite.TabCases[0].Length || TerrainEntite.TabCases[Position.X][Position.Y + (Rcible.Y - Position.Y > 0 ? 1 : -1)] == null || !ChangerPosition(TerrainEntite.TabCases[Position.X][Position.Y + (Rcible.Y - Position.Y > 0 ? 1 : -1)]))
                                {
                                    return PM_Debut - PM;
                                }
                            }
                        }
                    }
                    else if (Position.Y - Rcible.Y == 0)
                    {
                        int direction = Debug.FCombat.CombatCourant.Seed.Next(-1, 1);
                        while (direction == 0)
                            direction = Debug.FCombat.CombatCourant.Seed.Next(-1, 1);
                        if (Position.Y + direction < 0 || Position.Y + direction >= TerrainEntite.TabCases[0].Length || TerrainEntite.TabCases[Position.X][Position.Y + direction] == null || !ChangerPosition(TerrainEntite.TabCases[Position.X][Position.Y + direction]))
                        {
                            direction *= -1;
                            if (Position.Y + direction < 0 || Position.Y + direction >= TerrainEntite.TabCases[0].Length || TerrainEntite.TabCases[Position.X][Position.Y + direction] == null || !ChangerPosition(TerrainEntite.TabCases[Position.X][Position.Y + direction]))
                            {
                                if (Position.X + (Rcible.X - Position.X > 0 ? 1 : -1) < 0 || Position.X + (Rcible.X - Position.X > 0 ? 1 : -1) >= TerrainEntite.TabCases.Length || TerrainEntite.TabCases[Position.X + (Rcible.X - Position.X > 0 ? 1 : -1)][Position.Y] == null || !ChangerPosition(TerrainEntite.TabCases[Position.X + (Rcible.X - Position.X > 0 ? 1 : -1)][Position.Y]))
                                {
                                    return PM_Debut - PM;
                                }
                            }
                        }
                    }
                    else if (Math.Abs(Position.X - Rcible.X) < Math.Abs(Position.Y - Rcible.Y))
                    {
                        if (Position.X + (Rcible.X - Position.X > 0 ? 1 : -1) < 0 || Position.X + (Rcible.X - Position.X > 0 ? 1 : -1) >= TerrainEntite.TabCases.Length || TerrainEntite.TabCases[Position.X + (Rcible.X - Position.X > 0 ? 1 : -1)][Position.Y] == null || !ChangerPosition(TerrainEntite.TabCases[Position.X + (Rcible.X - Position.X > 0 ? 1 : -1)][Position.Y]))
                        {
                            if (Position.Y + (Rcible.Y - Position.Y > 0 ? 1 : -1) < 0 || Position.Y + (Rcible.Y - Position.Y > 0 ? 1 : -1) >= TerrainEntite.TabCases[0].Length || TerrainEntite.TabCases[Position.X][Position.Y + (Rcible.Y - Position.Y > 0 ? 1 : -1)] == null || !ChangerPosition(TerrainEntite.TabCases[Position.X][Position.Y + (Rcible.Y - Position.Y > 0 ? 1 : -1)]))
                            {
                                return PM_Debut - PM;
                            }
                        }
                    }
                    else
                    {
                        if (Position.Y + (Rcible.Y - Position.Y > 0 ? 1 : -1) < 0 || Position.Y + (Rcible.Y - Position.Y > 0 ? 1 : -1) >= TerrainEntite.TabCases[0].Length || TerrainEntite.TabCases[Position.X][Position.Y + (Rcible.Y - Position.Y > 0 ? 1 : -1)] == null || !ChangerPosition(TerrainEntite.TabCases[Position.X][Position.Y + (Rcible.Y - Position.Y > 0 ? 1 : -1)]))
                        {
                            if (Position.X + (Rcible.X - Position.X > 0 ? 1 : -1) < 0 || Position.X + (Rcible.X - Position.X > 0 ? 1 : -1) >= TerrainEntite.TabCases.Length || TerrainEntite.TabCases[Position.X + (Rcible.X - Position.X > 0 ? 1 : -1)][Position.Y] == null || !ChangerPosition(TerrainEntite.TabCases[Position.X + (Rcible.X - Position.X > 0 ? 1 : -1)][Position.Y]))
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
            catch (System.Exception e)
            {
                if (!Debug.FCombat.Generation)
                    System.Windows.Forms.MessageBox.Show(string.Format("Érreur : {0}", e));
                return PM_Debut - PM;
            }
        }
        public int SEloignerDe(EntiteInconnu cible)
        {
            int PM_Debut = PM;
            try
            {
                Case Rcible = new Case(0, 0, Case.type.vide);
                while (PM > 0)
                {
                    Rcible.X = Position.X - (cible.Position.X - Position.X);
                    Rcible.Y = Position.Y - (cible.Position.Y - Position.Y);

                    if (Position.X - Rcible.X == 0)
                    {
                        int direction = Debug.FCombat.CombatCourant.Seed.Next(-1, 1);
                        while (direction == 0)
                            direction = Debug.FCombat.CombatCourant.Seed.Next(-1, 1);
                        if (Position.X + direction < 0 || Position.X + direction >= TerrainEntite.TabCases.Length || TerrainEntite.TabCases[Position.X + direction][Position.Y] == null || !ChangerPosition(TerrainEntite.TabCases[Position.X + direction][Position.Y]))
                        {
                            direction *= -1;
                            if (Position.X + direction < 0 || Position.X + direction >= TerrainEntite.TabCases.Length || TerrainEntite.TabCases[Position.X + direction][Position.Y] == null || !ChangerPosition(TerrainEntite.TabCases[Position.X + direction][Position.Y]))
                            {
                                if (Position.Y + (Rcible.Y - Position.Y > 0 ? 1 : -1) < 0 || Position.Y + (Rcible.Y - Position.Y > 0 ? 1 : -1) >= TerrainEntite.TabCases[0].Length || TerrainEntite.TabCases[Position.X][Position.Y + (Rcible.Y - Position.Y > 0 ? 1 : -1)] == null || !ChangerPosition(TerrainEntite.TabCases[Position.X][Position.Y + (Rcible.Y - Position.Y > 0 ? 1 : -1)]))
                                {
                                    return PM_Debut - PM;
                                }
                            }
                        }
                    }
                    else if (Position.Y - Rcible.Y == 0)
                    {
                        int direction = Debug.FCombat.CombatCourant.Seed.Next(-1, 1);
                        while (direction == 0)
                            direction = Debug.FCombat.CombatCourant.Seed.Next(-1, 1);
                        if (Position.Y + direction < 0 || Position.Y + direction >= TerrainEntite.TabCases[0].Length || TerrainEntite.TabCases[Position.X][Position.Y + direction] == null || !ChangerPosition(TerrainEntite.TabCases[Position.X][Position.Y + direction]))
                        {
                            direction *= -1;
                            if (Position.Y + direction < 0 || Position.Y + direction >= TerrainEntite.TabCases[0].Length || TerrainEntite.TabCases[Position.X][Position.Y + direction] == null || !ChangerPosition(TerrainEntite.TabCases[Position.X][Position.Y + direction]))
                            {
                                if (Position.X + (Rcible.X - Position.X > 0 ? 1 : -1) < 0 || Position.X + (Rcible.X - Position.X > 0 ? 1 : -1) >= TerrainEntite.TabCases.Length || TerrainEntite.TabCases[Position.X + (Rcible.X - Position.X > 0 ? 1 : -1)][Position.Y] == null || !ChangerPosition(TerrainEntite.TabCases[Position.X + (Rcible.X - Position.X > 0 ? 1 : -1)][Position.Y]))
                                {
                                    return PM_Debut - PM;
                                }
                            }
                        }
                    }
                    else if (Math.Abs(Position.X - Rcible.X) < Math.Abs(Position.Y - Rcible.Y))
                    {
                        if (Position.X + (Rcible.X - Position.X > 0 ? 1 : -1) < 0 || Position.X + (Rcible.X - Position.X > 0 ? 1 : -1) >= TerrainEntite.TabCases.Length || TerrainEntite.TabCases[Position.X + (Rcible.X - Position.X > 0 ? 1 : -1)][Position.Y] == null || !ChangerPosition(TerrainEntite.TabCases[Position.X + (Rcible.X - Position.X > 0 ? 1 : -1)][Position.Y]))
                        {
                            if (Position.Y + (Rcible.Y - Position.Y > 0 ? 1 : -1) < 0 || Position.Y + (Rcible.Y - Position.Y > 0 ? 1 : -1) >= TerrainEntite.TabCases[0].Length || TerrainEntite.TabCases[Position.X][Position.Y + (Rcible.Y - Position.Y > 0 ? 1 : -1)] == null || !ChangerPosition(TerrainEntite.TabCases[Position.X][Position.Y + (Rcible.Y - Position.Y > 0 ? 1 : -1)]))
                            {
                                return PM_Debut - PM;
                            }
                        }
                    }
                    else
                    {
                        if (Position.Y + (Rcible.Y - Position.Y > 0 ? 1 : -1) < 0 || Position.Y + (Rcible.Y - Position.Y > 0 ? 1 : -1) >= TerrainEntite.TabCases[0].Length || TerrainEntite.TabCases[Position.X][Position.Y + (Rcible.Y - Position.Y > 0 ? 1 : -1)] == null || !ChangerPosition(TerrainEntite.TabCases[Position.X][Position.Y + (Rcible.Y - Position.Y > 0 ? 1 : -1)]))
                        {
                            if (Position.X + (Rcible.X - Position.X > 0 ? 1 : -1) < 0 || Position.X + (Rcible.X - Position.X > 0 ? 1 : -1) >= TerrainEntite.TabCases.Length || TerrainEntite.TabCases[Position.X + (Rcible.X - Position.X > 0 ? 1 : -1)][Position.Y] == null || !ChangerPosition(TerrainEntite.TabCases[Position.X + (Rcible.X - Position.X > 0 ? 1 : -1)][Position.Y]))
                            {
                                return PM_Debut - PM;
                            }
                        }
                    }
                    PM--;
                }
                return PM_Debut - PM;
            }
            catch (System.Exception e)
            {
                if (!Debug.FCombat.Generation)
                    System.Windows.Forms.MessageBox.Show(string.Format("Érreur : {0}", e));
                return PM_Debut - PM;
            }
        }
        public int SEloignerDe(EntiteInconnu cible, int PM_Alouer)
        {
            int PM_Debut = PM;
            try
            {
                Case Rcible = new Case(0, 0, Case.type.vide);
                while (PM > 0 && PM_Alouer > 0)
                {
                    Rcible.X = Position.X - (cible.Position.X - Position.X);
                    Rcible.Y = Position.Y - (cible.Position.Y - Position.Y);

                    if (Position.X - Rcible.X == 0)
                    {
                        int direction = Debug.FCombat.CombatCourant.Seed.Next(-1, 1);
                        while (direction == 0)
                            direction = Debug.FCombat.CombatCourant.Seed.Next(-1, 1);
                        if (Position.X + direction < 0 || Position.X + direction >= TerrainEntite.TabCases.Length || TerrainEntite.TabCases[Position.X + direction][Position.Y] == null || !ChangerPosition(TerrainEntite.TabCases[Position.X + direction][Position.Y]))
                        {
                            direction *= -1;
                            if (Position.X + direction < 0 || Position.X + direction >= TerrainEntite.TabCases.Length || TerrainEntite.TabCases[Position.X + direction][Position.Y] == null || !ChangerPosition(TerrainEntite.TabCases[Position.X + direction][Position.Y]))
                            {
                                if (Position.Y + (Rcible.Y - Position.Y > 0 ? 1 : -1) < 0 || Position.Y + (Rcible.Y - Position.Y > 0 ? 1 : -1) >= TerrainEntite.TabCases[0].Length || TerrainEntite.TabCases[Position.X][Position.Y + (Rcible.Y - Position.Y > 0 ? 1 : -1)] == null || !ChangerPosition(TerrainEntite.TabCases[Position.X][Position.Y + (Rcible.Y - Position.Y > 0 ? 1 : -1)]))
                                {
                                    return PM_Debut - PM;
                                }
                            }
                        }
                    }
                    else if (Position.Y - Rcible.Y == 0)
                    {
                        int direction = Debug.FCombat.CombatCourant.Seed.Next(-1, 1);
                        while (direction == 0)
                            direction = Debug.FCombat.CombatCourant.Seed.Next(-1, 1);
                        if (Position.Y + direction < 0 || Position.Y + direction >= TerrainEntite.TabCases[0].Length || TerrainEntite.TabCases[Position.X][Position.Y + direction] == null || !ChangerPosition(TerrainEntite.TabCases[Position.X][Position.Y + direction]))
                        {
                            direction *= -1;
                            if (Position.Y + direction < 0 || Position.Y + direction >= TerrainEntite.TabCases[0].Length || TerrainEntite.TabCases[Position.X][Position.Y + direction] == null || !ChangerPosition(TerrainEntite.TabCases[Position.X][Position.Y + direction]))
                            {
                                if (Position.X + (Rcible.X - Position.X > 0 ? 1 : -1) < 0 || Position.X + (Rcible.X - Position.X > 0 ? 1 : -1) >= TerrainEntite.TabCases.Length || TerrainEntite.TabCases[Position.X + (Rcible.X - Position.X > 0 ? 1 : -1)][Position.Y] == null || !ChangerPosition(TerrainEntite.TabCases[Position.X + (Rcible.X - Position.X > 0 ? 1 : -1)][Position.Y]))
                                {
                                    return PM_Debut - PM;
                                }
                            }
                        }
                    }
                    else if (Math.Abs(Position.X - Rcible.X) < Math.Abs(Position.Y - Rcible.Y))
                    {
                        if (Position.X + (Rcible.X - Position.X > 0 ? 1 : -1) < 0 || Position.X + (Rcible.X - Position.X > 0 ? 1 : -1) >= TerrainEntite.TabCases.Length || TerrainEntite.TabCases[Position.X + (Rcible.X - Position.X > 0 ? 1 : -1)][Position.Y] == null || !ChangerPosition(TerrainEntite.TabCases[Position.X + (Rcible.X - Position.X > 0 ? 1 : -1)][Position.Y]))
                        {
                            if (Position.Y + (Rcible.Y - Position.Y > 0 ? 1 : -1) < 0 || Position.Y + (Rcible.Y - Position.Y > 0 ? 1 : -1) >= TerrainEntite.TabCases[0].Length || TerrainEntite.TabCases[Position.X][Position.Y + (Rcible.Y - Position.Y > 0 ? 1 : -1)] == null || !ChangerPosition(TerrainEntite.TabCases[Position.X][Position.Y + (Rcible.Y - Position.Y > 0 ? 1 : -1)]))
                            {
                                return PM_Debut - PM;
                            }
                        }
                    }
                    else
                    {
                        if (Position.Y + (Rcible.Y - Position.Y > 0 ? 1 : -1) < 0 || Position.Y + (Rcible.Y - Position.Y > 0 ? 1 : -1) >= TerrainEntite.TabCases[0].Length || TerrainEntite.TabCases[Position.X][Position.Y + (Rcible.Y - Position.Y > 0 ? 1 : -1)] == null || !ChangerPosition(TerrainEntite.TabCases[Position.X][Position.Y + (Rcible.Y - Position.Y > 0 ? 1 : -1)]))
                        {
                            if (Position.X + (Rcible.X - Position.X > 0 ? 1 : -1) < 0 || Position.X + (Rcible.X - Position.X > 0 ? 1 : -1) >= TerrainEntite.TabCases.Length || TerrainEntite.TabCases[Position.X + (Rcible.X - Position.X > 0 ? 1 : -1)][Position.Y] == null || !ChangerPosition(TerrainEntite.TabCases[Position.X + (Rcible.X - Position.X > 0 ? 1 : -1)][Position.Y]))
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
            catch (System.Exception e)
            {
                if (!Debug.FCombat.Generation)
                    System.Windows.Forms.MessageBox.Show(string.Format("Érreur : {0}", e));
                return PM_Debut - PM;
            }
        }

        internal new bool ChangerPosition(Case nextPosition)
        {
            if (nextPosition.Contenu == Case.type.vide)
            {
                Position.Contenu = Case.type.vide;
                nextPosition.Contenu = Case.type.joueur;
                Position = nextPosition;
                foreach (EntiteInconnu entite in ListEntites)
                {
                    if (entite.IdEntite == IdEntite)
                    {
                        entite.Position = nextPosition;
                        break;
                    }
                }
                return true;
            }
            return false;
        }

        #endregion
    }
}
