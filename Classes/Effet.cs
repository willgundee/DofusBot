using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test
{
    public class Effet
    {
        public enum effet { pousse, pousse_lanceur, tire, tire_lanceur, teleportation, ATT_neutre, ATT_air, ATT_feu, ATT_terre, ATT_eau, envoutement, pose_piege, pose_glyphe, invocation, soin }
        private Dictionary<effet, string> dictSimple = new Dictionary<effet, string>()
        #region ddt
        {
            {effet.ATT_air,"Dommage air"},
            {effet.ATT_eau,"Domage eau"},
            {effet.ATT_feu,"Dommage feu"},
            {effet.ATT_neutre,"Dommage neutre"},
            {effet.ATT_terre,"Dommage terre"},
            {effet.envoutement,"Envoutement"},
            {effet.invocation,"Invocation"},
            {effet.pose_glyphe,"Poser un glyphe"},
            {effet.pose_piege,"Poser un piège"},
            {effet.pousse,"Pousser la cible"},
            {effet.pousse_lanceur,"Pousse le lanceur"},
            {effet.soin,"Soingne"},
            {effet.teleportation,"Téléportation"},
            {effet.tire,"Attire la cible"},
            {effet.tire_lanceur,"Attire le lanceur"}
        };
        #endregion
        public effet Nom { get; set; }
        public string NomSimplifier { get; set; }
        public int DmgMin { get; set; }
        public int DmgMax { get; set; }
        /// <summary>
        /// Constructeur d'un effet
        /// </summary>
        /// <param name="ls">La requête</param>
        public Effet(List<string> ls)
        {
            Nom = (effet)Enum.Parse(typeof(effet), ls[0], true);//convert string to enum
            NomSimplifier = dictSimple[Nom];
            DmgMin =Convert.ToInt32(ls[1]);
            DmgMax =Convert.ToInt32(ls[2]);

        }
    }
}
