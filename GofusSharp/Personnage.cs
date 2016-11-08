﻿using System.Linq;
using System.Windows;

namespace GofusSharp
{
    public class Personnage : Entite
    {
        public Liste<Equipement> ListEquipements { get; internal set; }

        internal Personnage(Gofus.Entite entite, type Equipe, Terrain TerrainEntite) :base(entite, Equipe, TerrainEntite)
        {
            ListEquipements = new Liste<Equipement>();
            foreach (Gofus.Equipement equip in entite.LstEquipements)
            {
                if (equip.EstArme)
                    ListEquipements.Add(new Arme(equip));
                else
                    ListEquipements.Add(new Equipement(equip));
            }
        }
        internal Personnage(int IdEntite, Classe ClasseEntite, string Nom, float Experience, type Equipe, Liste<Statistique> ListStatistiques, string ScriptEntite, Liste<Equipement> ListEquipements, Terrain TerrainEntite) : base(IdEntite, ClasseEntite, Nom, Experience, Equipe, ListStatistiques, ScriptEntite, TerrainEntite, 0)
        {
            this.ListEquipements = ListEquipements;
            foreach (Equipement item in ListEquipements)
            {
                foreach (Statistique stat_item in item.ListStatistiques)
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
            Arme arme = new Arme(null, "poing", Equipement.type.arme, new Liste<Effet> { new Effet(Effet.type.ATT_neutre, 3, 5) }, new Zone(Zone.type.croix, 1, 1), new Zone(Zone.type.carre, 0, 0), Arme.typeArme.dague, 3);

            foreach (Equipement invent in ListEquipements)
            {
                if (invent is Arme)
                {
                    arme = invent as Arme;
                    break;
                }
            }
            (Application.Current.Windows.Cast<Window>().First(x => x.GetType() == typeof(Combat)) as Combat).tb_Log.Text += "\n" + Nom + " attaque " + cible.Nom + " avec " + arme.Nom;
            if (PA < arme.CoutPA)
            {
                (Application.Current.Windows.Cast<Window>().First(x => x.GetType() == typeof(Combat)) as Combat).tb_Log.Text += "\n" + Nom + " n'a pas assez de PA pour utiliser " + arme.Nom;
                return false;
            }
            if (CaseEstDansZone(arme.ZonePortee.Type, arme.ZonePortee.PorteeMin, arme.ZonePortee.PorteeMax, Position, cible.Position))
            {
                PA -= arme.CoutPA;
                foreach (Effet effet in arme.ListEffets)
                {
                    InfligerEffet(effet, arme.ZoneEffet, cible.Position);
                }
                return true;
            }
            (Application.Current.Windows.Cast<Window>().First(x => x.GetType() == typeof(Combat)) as Combat).tb_Log.Text += "\n" + cible.Nom + " est hors de portée de l'arme " + arme.Nom;
            return false;
        }
        public bool Attaquer(Case cible)
        {
            Arme arme = new Arme(null, "poing", Equipement.type.arme, new Liste<Effet> { new Effet(Effet.type.ATT_neutre, 3, 5) }, new Zone(Zone.type.croix, 1, 1), new Zone(Zone.type.carre, 0, 0), Arme.typeArme.dague, 3);
            foreach (Equipement invent in ListEquipements)
            {
                if (invent is Arme)
                {
                    arme = invent as Arme;
                    break;
                }
            }
            if (PA < arme.CoutPA)
            {
                (Application.Current.Windows.Cast<Window>().First(x => x.GetType() == typeof(Combat)) as Combat).tb_Log.Text += "\n" + Nom + " n'a pas assez de PA pour utiliser " + arme.Nom;
                return false;
            }
            (Application.Current.Windows.Cast<Window>().First(x => x.GetType() == typeof(Combat)) as Combat).tb_Log.Text += "\n" + Nom + " attaque à X: " + cible.X + " Y: " + cible.Y + " avec " + arme.Nom;
            if (CaseEstDansZone(arme.ZonePortee.Type, arme.ZonePortee.PorteeMin, arme.ZonePortee.PorteeMax, Position, cible))
            {
                PA -= arme.CoutPA;
                foreach (Effet effet in arme.ListEffets)
                {
                    InfligerEffet(effet, arme.ZoneEffet, cible);
                }
                return true;
            }
            (Application.Current.Windows.Cast<Window>().First(x => x.GetType() == typeof(Combat)) as Combat).tb_Log.Text += "\n" + cible.X + " Y: " + cible.Y + " est hors de portée de l'arme " + arme.Nom;
            return false;
        }
    }
}