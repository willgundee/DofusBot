using System.Windows;
using System.Linq;

namespace GofusSharp
{
    public class Entite : EntiteInconnu
    {
        internal Script ScriptEntite { get; set; }
        internal Terrain TerrainEntite { get; set; }
        internal Liste<EntiteInconnu> ListEntites { get; set; }
        internal Entite(int IdEntite, Classe ClasseEntite, string Nom, float Experience, type Equipe, Liste<Statistique> ListStatistiques, Script ScriptEntite, Terrain TerrainEntite, int Proprietaire) : base(IdEntite, ClasseEntite, Nom, Experience, Equipe)
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
        internal Entite(EntiteInconnu EntiteInconnu, Script ScriptEntite, Terrain TerrainEntite, int Proprietaire) : base(EntiteInconnu.IdEntite, EntiteInconnu.ClasseEntite, EntiteInconnu.Nom, EntiteInconnu.Experience, EntiteInconnu.Equipe)
        {
            ListStatistiques = EntiteInconnu.ListStatistiques;
            foreach (Statistique stat in EntiteInconnu.ListStatistiques)
            {
                if (stat.Nom == Statistique.type.PA)
                    PA_MAX = stat.Valeur;
                if (stat.Nom == Statistique.type.PM)
                    PM_MAX = stat.Valeur;
            }
            this.ScriptEntite = ScriptEntite;
            ClasseEntite = EntiteInconnu.ClasseEntite;
            Nom = EntiteInconnu.Nom;
            Experience = EntiteInconnu.Experience;
            this.TerrainEntite = TerrainEntite;
            this.Proprietaire = Proprietaire;
        }

        public bool UtiliserSort(Sort sort, EntiteInconnu cible)
        {
            (Application.Current.Windows.Cast<Window>().First(x => x.GetType() == typeof(Combat)) as Combat).tb_Log.Text += "\n" + Nom + " lance " + sort.Nom + " sur " + cible.Nom;
            if (PA < sort.CoutPA)
            {
                (Application.Current.Windows.Cast<Window>().First(x => x.GetType() == typeof(Combat)) as Combat).tb_Log.Text += "\n" + Nom + " n'a pas assez de PA pour lancer " + sort.Nom;
                return false;
            }
            if (CaseEstDansZone(sort.ZonePortee.Type, sort.ZonePortee.PorteeMin, sort.ZonePortee.PorteeMax, Position, cible.Position))
            {
                PA -= sort.CoutPA;
                foreach (Effet effet in sort.TabEffets)
                {
                    InfligerEffet(effet, sort.ZoneEffet, cible.Position);
                }
                return true;
            }
            (Application.Current.Windows.Cast<Window>().First(x => x.GetType() == typeof(Combat)) as Combat).tb_Log.Text += "\n" + cible.Nom + " est hors de portée du sort " + sort.Nom;
            return false;
        }

        public bool UtiliserSort(Sort sort, Case cible)
        {
            (Application.Current.Windows.Cast<Window>().First(x => x.GetType() == typeof(Combat)) as Combat).tb_Log.Text += "\n" + Nom + " lance " + sort.Nom + " à X: " + cible.X.ToString() + " Y: " + cible.Y.ToString();
            if (PA < sort.CoutPA)
            {
                (Application.Current.Windows.Cast<Window>().First(x => x.GetType() == typeof(Combat)) as Combat).tb_Log.Text += "\n" + Nom + " n'a pas assez de PA pour lancer " + sort.Nom;
                return false;
            }
            if (CaseEstDansZone(sort.ZonePortee.Type, sort.ZonePortee.PorteeMin, sort.ZonePortee.PorteeMax, Position, cible))
            {
                PA -= sort.CoutPA;
                foreach (Effet effet in sort.TabEffets)
                {
                    InfligerEffet(effet, sort.ZoneEffet, cible);
                }
                return true;
            }
            (Application.Current.Windows.Cast<Window>().First(x => x.GetType() == typeof(Combat)) as Combat).tb_Log.Text += "\n" + cible.X + " Y: " + cible.Y + " est hors de portée du sort " + sort.Nom;
            return false;
        }

        public bool UtiliserSort(Sort.nom_sort vraiNom, EntiteInconnu cible)
        {
            Sort sort = null;
            try
            {
                sort = ClasseEntite.TabSorts.First(x => x.VraiNom == vraiNom);
            }
            catch (System.Exception)
            {
                (Application.Current.Windows.Cast<Window>().First(x => x.GetType() == typeof(Combat)) as Combat).tb_Log.Text += "\nle sort " + vraiNom.ToString() + " n'est pas à votre disposition";
                return false;
            }
            (Application.Current.Windows.Cast<Window>().First(x => x.GetType() == typeof(Combat)) as Combat).tb_Log.Text += "\n" + Nom + " lance " + sort.Nom + " sur " + cible.Nom;
            if (PA < sort.CoutPA)
            {
                (Application.Current.Windows.Cast<Window>().First(x => x.GetType() == typeof(Combat)) as Combat).tb_Log.Text += "\n" + Nom + " n'a pas assez de PA pour lancer " + sort.Nom;
                return false;
            }
            if (CaseEstDansZone(sort.ZonePortee.Type, sort.ZonePortee.PorteeMin, sort.ZonePortee.PorteeMax, Position, cible.Position))
            {
                PA -= sort.CoutPA;
                foreach (Effet effet in sort.TabEffets)
                {
                    InfligerEffet(effet, sort.ZoneEffet, cible.Position);
                }
                return true;
            }
            (Application.Current.Windows.Cast<Window>().First(x => x.GetType() == typeof(Combat)) as Combat).tb_Log.Text += "\n" + cible.Nom + " est hors de portée du sort " + sort.Nom;
            return false;
        }

        public bool UtiliserSort(Sort.nom_sort vraiNom, Case cible)
        {
            Sort sort = null;
            try
            {
                sort = ClasseEntite.TabSorts.First(x => x.VraiNom == vraiNom);
            }
            catch (System.Exception)
            {
                (Application.Current.Windows.Cast<Window>().First(x => x.GetType() == typeof(Combat)) as Combat).tb_Log.Text += "\nle sort " + vraiNom.ToString() + " n'est pas à votre disposition";
                return false;
            }
            (Application.Current.Windows.Cast<Window>().First(x => x.GetType() == typeof(Combat)) as Combat).tb_Log.Text += "\n" + Nom + " lance " + sort.Nom + " à X: " + cible.X.ToString() + " Y: " + cible.Y.ToString();
            if (PA < sort.CoutPA)
            {
                (Application.Current.Windows.Cast<Window>().First(x => x.GetType() == typeof(Combat)) as Combat).tb_Log.Text += "\n" + Nom + " n'a pas assez de PA pour lancer " + sort.Nom;
                return false;
            }
            if (CaseEstDansZone(sort.ZonePortee.Type, sort.ZonePortee.PorteeMin, sort.ZonePortee.PorteeMax, Position, cible))
            {
                PA -= sort.CoutPA;
                foreach (Effet effet in sort.TabEffets)
                {
                    InfligerEffet(effet, sort.ZoneEffet, cible);
                }
                return true;
            }
            (Application.Current.Windows.Cast<Window>().First(x => x.GetType() == typeof(Combat)) as Combat).tb_Log.Text += "\n" + cible.X + " Y: " + cible.Y + " est hors de portée du sort " + sort.Nom;
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
                    bool result = ChangerPosition(source);
                    if (result)
                    {
                        (Application.Current.Windows.Cast<Window>().First(x => x.GetType() == typeof(Combat)) as Combat).tb_Log.Text += "\n" + Nom + " s'est téléporté a X: " + source.X + " Y: " + source.Y;
                    }
                    return (result ? 1 : 0);
                case Effet.type.ATT_neutre:
                    foreach (EntiteInconnu entiteInconnu in ListEntites)
                    {
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
                            if (entiteInconnu.recevoirDommages((1 - (RES_Pourcent_neutre / 100)) * ((new System.Random().Next(effet.ValeurMin, effet.ValeurMax) * (100 + force + puissance) / 100 + DMG_neutre) - RES_neutre - reduction_physique)));
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
                    }
                    break;
                case Effet.type.ATT_air:
                    foreach (EntiteInconnu entiteInconnu in ListEntites)
                    {
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
                            if (entiteInconnu.recevoirDommages((1 - (RES_Pourcent_air / 100)) * ((new System.Random().Next(effet.ValeurMin, effet.ValeurMax) * (100 + agilite + puissance) / 100 + DMG_air) - RES_air - reduction_magique)))
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
                    }
                    break;
                case Effet.type.ATT_feu:
                    foreach (EntiteInconnu entiteInconnu in ListEntites)
                    {
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
                            if (entiteInconnu.recevoirDommages((1 - (RES_Pourcent_feu / 100)) * ((new System.Random().Next(effet.ValeurMin, effet.ValeurMax) * (100 + intelligence + puissance) / 100 + DMG_feu) - RES_feu - reduction_magique)))
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
                    }
                    break;
                case Effet.type.ATT_terre:
                    foreach (EntiteInconnu entiteInconnu in ListEntites)
                    {
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
                            if (entiteInconnu.recevoirDommages((1 - (RES_Pourcent_terre / 100)) * ((new System.Random().Next(effet.ValeurMin, effet.ValeurMax) * (100 + force + puissance) / 100 + DMG_terre) - RES_terre - reduction_physique)))
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
                    }
                    break;
                case Effet.type.ATT_eau:
                    foreach (EntiteInconnu entiteInconnu in ListEntites)
                    {
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
                            if (entiteInconnu.recevoirDommages((1 - (RES_Pourcent_eau / 100)) * ((new System.Random().Next(effet.ValeurMin, effet.ValeurMax) * (100 + chance + puissance) / 100 + DMG_eau) - RES_eau - reduction_magique)))
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
                    }
                    break;
                case Effet.type.envoutement:
                    foreach (EntiteInconnu entiteInconnu in ListEntites)
                    {
                        if (CaseEstDansZone(zoneEffet.Type, zoneEffet.PorteeMin, zoneEffet.PorteeMax, source, entiteInconnu.Position))
                        {
                            entiteInconnu.ListEnvoutements.Add(new Envoutement(effet.Stat, new System.Random().Next(effet.ValeurMin, effet.ValeurMax), effet.NbTour, IdEntite));
                        }
                    }
                    break;
                case Effet.type.pose_piege:
                    break;
                case Effet.type.pose_glyphe:
                    break;
                case Effet.type.invocation:
                    //PlaceHolder ListEntites.AjouterFin(new EntiteInconnu(effet.ValeurMax, new Classe(), "bouftou", 1000, source, Equipe, IdEntite);
                    break;
                case Effet.type.soin:
                    foreach (EntiteInconnu entiteInconnu in ListEntites)
                    {
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
                            entiteInconnu.PV += new System.Random().Next(effet.ValeurMin, effet.ValeurMax) * (100 + intelligence) / 100 + soin;
                        }
                    }
                    break;
                default:
                    break;
            }
            return 0;
        }

        internal bool CaseEstDansZone(Zone.type TypeZone, int porteeMin, int porteeMax, Case source, Case cible)
        {
            try
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
            catch (System.Exception e)
            {
                System.Windows.Forms.MessageBox.Show(string.Format("Érreur : {0}", e));
                return false;
            }
        }

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

        internal bool ChangerPosition(Case nextPosition)
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
    }
}
