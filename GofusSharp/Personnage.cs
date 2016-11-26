using System.Linq;
using System.Windows;
using System.Windows.Threading;

namespace GofusSharp
{
    public class Personnage : Entite
    {
        public Liste<Equipement> ListEquipements { get; internal set; }

        internal Personnage() : base() { }
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
            if (!Debug.FCombat.Generation)
            {
                Debug.FCombat.Dispatcher.Invoke(Debug.FCombat.DelLog, new object[] { "\n" + Nom + " attaque " + cible.Nom + " avec " + arme.Nom });
            }
            if (PA < arme.CoutPA)
            {
                if (!Debug.FCombat.Generation)
                    Debug.FCombat.Dispatcher.Invoke(Debug.FCombat.DelLog, new object[] { "\n" + Nom + " n'a pas assez de PA pour utiliser " + arme.Nom });
                return false;
            }
            if (CaseEstDansZone(arme.ZonePortee.Type, arme.ZonePortee.PorteeMin, arme.ZonePortee.PorteeMax, Position, cible.Position))
            {
                PA -= arme.CoutPA;
                foreach (Effet effet in arme.ListEffets)
                {
                    InfligerEffet(effet, arme.ZoneEffet, cible.Position);
                }
                if (!Debug.FCombat.Generation)
                {
                    Debug.FCombat.Dispatcher.Invoke(Debug.FCombat.DelUpd);
                    System.Threading.Thread.Sleep((int)(1000 / Debug.FCombat.Speed));
                    Debug.FCombat.mrse.WaitOne();
                }
                return true;
            }
            if (!Debug.FCombat.Generation)
                Debug.FCombat.Dispatcher.Invoke(Debug.FCombat.DelLog, new object[] { "\n" + cible.Nom + " est hors de portée de l'arme " + arme.Nom });
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
                if (!Debug.FCombat.Generation)
                    Debug.FCombat.Dispatcher.Invoke(Debug.FCombat.DelLog, new object[] { "\n" + Nom + " n'a pas assez de PA pour utiliser " + arme.Nom });
                return false;
            }
            if (!Debug.FCombat.Generation)
                Debug.FCombat.Dispatcher.Invoke(Debug.FCombat.DelLog, new object[] { "\n" + Nom + " attaque à X: " + cible.X + " Y: " + cible.Y + " avec " + arme.Nom });
            if (CaseEstDansZone(arme.ZonePortee.Type, arme.ZonePortee.PorteeMin, arme.ZonePortee.PorteeMax, Position, cible))
            {
                PA -= arme.CoutPA;
                foreach (Effet effet in arme.ListEffets)
                {
                    InfligerEffet(effet, arme.ZoneEffet, cible);
                }
                if (!Debug.FCombat.Generation)
                {
                    Debug.FCombat.Dispatcher.Invoke(Debug.FCombat.DelUpd);
                    System.Threading.Thread.Sleep((int)(1000 / Debug.FCombat.Speed));
                    Debug.FCombat.mrse.WaitOne();
                }
                return true;
            }
            if (!Debug.FCombat.Generation)
                Debug.FCombat.Dispatcher.Invoke(Debug.FCombat.DelLog, new object[] { "\n" + cible.X + " Y: " + cible.Y + " est hors de portée de l'arme " + arme.Nom });
            return false;
        }

        public bool PeutAttaquer(EntiteInconnu cible)
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
                return false;
            if (CaseEstDansZone(arme.ZonePortee.Type, arme.ZonePortee.PorteeMin, arme.ZonePortee.PorteeMax, Position, cible.Position))
                return true;
            return false;
        }
        public bool PeutAttaquer(Case cible)
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
                return false;
            if (CaseEstDansZone(arme.ZonePortee.Type, arme.ZonePortee.PorteeMin, arme.ZonePortee.PorteeMax, Position, cible))
                return true;
            return false;
        }

        public Liste<Case> CasesPourAttaquer(EntiteInconnu cible)
        {
            Liste<Case> caseValide = new Liste<Case>();
            foreach (Case[] caseL in TerrainEntite.TabCases)
                foreach (Case CaseH in caseL)
                    if (PeutAttaquer(cible))
                        caseValide.Add(CaseH);
            return caseValide;
        }
        public Liste<Case> CasesPourAttaquer(Case cible)
        {
            Liste<Case> caseValide = new Liste<Case>();
            foreach (Case[] caseL in TerrainEntite.TabCases)
                foreach (Case CaseH in caseL)
                    if (PeutAttaquer(cible))
                        caseValide.Add(CaseH);
            return caseValide;
        }
    }
}