using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test
{
    public class Statistique
    {
        public enum element {experience, vie, force, intelligence, chance, agilite, vitalite, sagesse, PA, PM, portee, invocation, prospection, initiative, fuite,
            DMG_neutre, DMG_feu, DMG_air, DMG_terre, DMG_eau, DMG_poussee, DMG_piege, RES_neutre, RES_feu, RES_air, RES_terre, RES_eau, RES_poussee, RES_Pourcent_neutre,
            RES_Pourcent_feu, RES_Pourcent_air, RES_Pourcent_terre, RES_Pourcent_eau, retrait_PA, retrait_PM, esquive_PA, esquive_PM, soin, renvoie_DMG, tacle, puissance,
            puissance_piege, reduction_physique, reduction_magique };//les types de stats possibles.
        public element Nom { get; set; }
        public string NomSimple { get; set; }
        public float Valeur { get; set; }
        private Dictionary<element, string> dictElement = new Dictionary<element, string>()//dictionnaire servant a transformer les nom de la bd en nom lisible pour l'homme
            #region dict
        {

            {element.agilite,"Agilité"},
            {element.experience,"Expérience"},
            {element.chance,"Chance"},
            {element.DMG_air,"Dommage air"},
            {element.DMG_eau,"Dommage eau"},
            {element.DMG_feu,"Dommage feu"},
            {element.DMG_neutre,"Dommage neutre"},
            {element.DMG_piege,"Dommage aux pièges"},
            {element.DMG_poussee,"Dommage de poussé"},
            {element.DMG_terre,"Dommage terre"},
            {element.esquive_PA,"Esquive de PA"},
            {element.esquive_PM,"Esquive de PM"},
            {element.force,"Force"},
            {element.fuite,"Fuite"},
            {element.initiative,"Initiative"},
            {element.intelligence,"Intelligence"},
            {element.invocation,"Invocations"},
            {element.PA,"PA"},
            {element.PM,"PM"},
            {element.portee,"Portée"},
            {element.prospection,"Prospection"},
            {element.puissance,"Puissance"},
            {element.puissance_piege,"Puissance des pièges"},
            {element.reduction_magique,"Réduction magique"},
            {element.reduction_physique,"réduction physique"},
            {element.renvoie_DMG,"Renvoie de dommage"},
            {element.RES_air,"Résistance fixe air"},
            {element.RES_eau,"Résistance fixe eau"},
            {element.RES_feu,"Résistance fixe feu"},
            {element.RES_terre,"Résistance fixe terre"},
            {element.RES_neutre,"Résistance fixe neutre"},
            {element.RES_Pourcent_air,"% résistance air"},
            {element.RES_Pourcent_eau,"% résistance eau"},
            {element.RES_Pourcent_feu,"% résistance feu"},
            {element.RES_Pourcent_neutre,"% résistance neutre"},
            {element.RES_Pourcent_terre,"% résistance terre"},
            {element.RES_poussee,"Résistance au dommage de poussé"},
            {element.retrait_PA,"Retraite de PA"},
            {element.retrait_PM,"Retraite de PM"},
            {element.sagesse,"Sagesse"},
            {element.soin,"Soin"},
            {element.tacle,"Tacle"},
            {element.vie,"Vie"},
            {element.vitalite,"Vitalité"}
        };
        #endregion

        /// <summary>
        /// Constructeur d'une statistique
        /// </summary>
        /// <param name="info">La requête</param>
        public Statistique(List<string> info)
        {
            Nom = (element)Enum.Parse(typeof(element), info[0], true);//convert string to enum;
            NomSimple = dictElement[Nom];
            Valeur = Convert.ToInt64(info[1]);
        }
    }
}
