using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gofus
{
    public class Statistique
    {
        public enum element
        {
            experience, vie, force, intelligence, chance, agilite, vitalite, sagesse, PA, PM, portee, invocation, prospection, initiative, fuite,
            DMG_neutre, DMG_feu, DMG_air, DMG_terre, DMG_eau, DMG_poussee, DMG_piege, RES_neutre, RES_feu, RES_air, RES_terre, RES_eau, RES_poussee, RES_Pourcent_neutre,
            RES_Pourcent_feu, RES_Pourcent_air, RES_Pourcent_terre, RES_Pourcent_eau, retrait_PA, retrait_PM, esquive_PA, esquive_PM, soin, renvoie_DMG, tacle, puissance,
            puissance_piege, reduction_physique, reduction_magique
        };//les types de stats possibles.
        public element Nom { get; set; }
        public string NomSimple { get; set; }
        public double Valeur { get; set; }
        private Dictionary<element, string> dictElement = new Dictionary<element, string>()//dictionnaire servant à transformer les nom de la bd en nom lisible pour l'homme
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
            {element.DMG_poussee,"Dommage poussée"},
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
            {element.RES_air,"Résistance air"},
            {element.RES_eau,"Résistance eau"},
            {element.RES_feu,"Résistance feu"},
            {element.RES_terre,"Résistance terre"},
            {element.RES_neutre,"Résistance neutre"},
            {element.RES_Pourcent_air,"% résistance air"},
            {element.RES_Pourcent_eau,"% résistance eau"},
            {element.RES_Pourcent_feu,"% résistance feu"},
            {element.RES_Pourcent_neutre,"% résistance neutre"},
            {element.RES_Pourcent_terre,"% résistance terre"},
            {element.RES_poussee,"Résistance poussée"},
            {element.retrait_PA,"Retraite de PA"},
            {element.retrait_PM,"Retraite de PM"},
            {element.sagesse,"Sagesse"},
            {element.soin,"Soin"},
            {element.tacle,"Tacle"},
            {element.vie,"Vie"},
            {element.vitalite,"Vitalité"}
        };
        #endregion
        public Dictionary<int, double> dictLvl = new Dictionary<int, double>()
            #region les levels
        {
{1,0},{2,110},{3,650},{4,1500},{5,2800},{6,4800},{7,7300},{8,10500},{9,14500},{10,19200},{11,25200},{12,32600},{13,41000},{14,50500},{15,61000},{16,75000},{17,91000},{18,115000},{19,142000},{20,171000},{21,202000},{22,235000},{23,270000},{24,310000},{25,353000},{26,398500},{27,448000},{28,503000},{29,561000},{30,621600},{31,687000},{32,755000},{33,829000},{34,910000},{35,1000000},{36,1100000},{37,1240000},{38,1400000},{39,1580000},{40,1780000},{41,2000000},{42,2250000},{43,2530000},{44,2850000},{45,3200000},{46,3570000},{47,3960000},{48,4400000},{49,4860000},{50,5350000},{51,5860000},{52,6390000},{53,6950000},{54,7530000},{55,8130000},{56,8765100},{57,9420000},{58,10150000},{59,10894000},{60,11655000},{61,12450000},{62,13280000},{63,14130000},{64,15170000},{65,16251000},{66,17377000},{67,18553000},{68,19778000},{69,21055000},{70,22385000},{71,23529000},{72,25209000},{73,26707000},{74,28264000},{75,29882000},{76,31563000},{77,33307000},{78,35118000},{79,36997000},{80,38945000},{81,40965000},{82,43059000},{83,45229000},{84,47476000},{85,49803000},{86,52211000},{87,54704000},{88,57284000},{89,59952000},{90,62712000},{91,65565000},{92,68514000},{93,71561000},{94,74710000},{95,77963000},{96,81323000},{97,84792000},{98,88374000},{99,92071000},{100,95886000},{101,99823000},{102,103885000},{103,108075000},{104,112396000},{105,116853000},{106,121447000},{107,126184000},{108,131066000},{109,136098000},{110,141283000},{111,146626000},{112,152130000},{113,157800000},{114,163640000},{115,169655000},{116,175848000},{117,182225000},{118,188791000},{119,195550000},{120,202507000},{121,209667000},{122,217037000},{123,224620000},{124,232424000},{125,240452000},{126,248712000},{127,257209000},{128,265949000},{129,274939000},{130,284186000},{131,293694000},{132,303473000},{133,313527000},{134,323866000},{135,334495000},{136,345423000},{137,356657000},{138,368206000},{139,380076000},{140,392278000},{141,404818000},{142,417706000},{143,430952000},{144,444564000},{145,458551000},{146,472924000},{147,487693000},{148,502867000},{149,518458000},{150,534476000},{151,551000000},{152,567839000},{153,585206000},{154,603047000},{155,621374000},{156,640199000},{157,659536000},{158,679398000},{159,699798000},{160,720751000},{161,742272000},{162,764374000},{163,787074000},{164,810387000},{165,834329000},{166,858917000},{167,884167000},{168,910098000},{169,936727000},{170,964073000},{171,992154000},{172,1020991000},{173,1050603000},{174,1081010000},{175,1112235000},{176,1144298000},{177,1177222000},{178,1211030000},{179,1245745000},{180,1281393000},{181,1317997000},{182,1355584000},{183,1404179000},{184,1463811000},{185,1534506000},{186,1616294000},{187,1709205000},{188,1813267000},{189,1928513000},{190,2054975000},{191,2192686000},{192,2341679000},{193,2501990000},{194,2673655000},{195,2856710000},{196,3051194000},{197,3257146000},{198,3474606000},{199,3703616000},{200,5555424000}        };
        #endregion

        /// <summary>
        /// Constructeur d'une statistique
        /// </summary>
        /// <param name="info">La requête</param>
        public Statistique(List<string> info)
        {
            Nom = (element)Enum.Parse(typeof(element), info[0], true);//convert string to enum;
            NomSimple = dictElement[Nom];
            Valeur = Convert.ToDouble(info[1]);
        }

        public int toLevel()
        {
            if (Nom == element.experience)
            {
                for (int i = 1; i < 200; i++)
                    if (Valeur >= dictLvl[i] && Valeur < dictLvl[i + 1])
                        return i;
                if (Valeur >= dictLvl[200])
                    return 200;
            }
            return 0;//si tout fucktop
        }

    }
}
