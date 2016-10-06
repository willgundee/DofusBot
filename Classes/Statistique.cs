using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test
{
    public class Statistique
    {
        public enum element { vie, force, intelligence, chance, agilite, vitalite, sagesse, PA, PM, portee, invocation, prospection, initiative, fuite, DMG_neutre, DMG_feu, DMG_air, DMG_terre, DMG_eau, DMG_poussee, DMG_piege, RES_neutre, RES_feu, RES_air, RES_terre, RES_eau, RES_poussee, RES_Pourcent_neutre, RES_Pourcent_feu, RES_Pourcent_air, RES_Pourcent_terre, RES_Pourcent_eau, retrait_PA, retrait_PM, esquive_PA, esquive_PM, soin, renvoie_DMG, tacle, puissance, puissance_piege, reduction_physique, reduction_magique };
        public element Nom { get; set; }
        public int Valeur { get; set; }
        public Dictionary<string, element> dictElement = new Dictionary<string, element>()
            #region dict
        {
            {"Agilité", element.agilite},
            {"Chance", element.chance},
            {"Dommage air", element.DMG_air},
            {"Dommage eau", element.DMG_eau},
            {"Dommage feu", element.DMG_feu},
            {"Dommage neutre", element.DMG_neutre},
            {"Dommage aux pièges", element.DMG_piege},
            {"Dommage de poussé", element.DMG_poussee},
            {"Dommage terre", element.DMG_terre},
            {"Esquive de PA", element.esquive_PA},
            {"Esquive de PM", element.esquive_PM},
            {"Force", element.force},
            {"Fuite", element.fuite},
            {"Initiative", element.initiative},
            {"Intelligence", element.intelligence},
            {"Invocation", element.invocation},
            {"PA", element.PA},
            {"PM", element.PM},
            {"Portée", element.portee},
            {"Prospection", element.prospection},
            {"Puissance", element.puissance},
            {"Puissance des pièges", element.puissance_piege},
            {"Réduction magique", element.reduction_magique},
            {"réduction physique", element.reduction_physique},
            {"Renvoie de dommage", element.renvoie_DMG},
            {"Résistance fixe air", element.RES_air},
            {"Résistance fixe eau", element.RES_eau},
            {"Résistance fixe feu", element.RES_feu},
            {"Résistance fixe terre", element.RES_terre},
            {"Résistance fixe neutre", element.RES_neutre},
            {"Pourcentage de résistance air", element.RES_Pourcent_air},
            {"Pourcentage de résistance eau ", element.RES_Pourcent_eau},
            {"Pourcentage de résistance feu", element.RES_Pourcent_feu},
            {"Pourcentage de résistance neutre", element.RES_Pourcent_neutre},
            {"Pourcentage de résistance terre", element.RES_Pourcent_terre},
            {"Résistance au dommage de poussé", element.RES_poussee},
            {"Retraite de PA", element.retrait_PA},
            {"Retraite de PM", element.retrait_PM},
            {"Sagesse", element.sagesse},
            {"Soin", element.soin},
            {"Tacle", element.tacle},
            {"Vie", element.vie},
            { "Vitalité", element.vitalite}
        };
            #endregion

        public Statistique(string t, int i)
        {
            Nom = (element)Enum.Parse(typeof(element), t, true);//convert string to enum;
            Valeur = i;
        }
    }
}
