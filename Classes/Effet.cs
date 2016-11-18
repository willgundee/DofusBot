using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Gofus
{
    public class Effet
    {
        public enum effet { pousse, repousse, repousse_lanceur, tire, tire_lanceur, teleportation, ATT_neutre, ATT_air, ATT_feu, ATT_terre, ATT_eau, envoutement, pose_piege, pose_glyphe, invocation, soin }
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
            {effet.repousse_lanceur,"Repousse le lanceur"},
            {effet.repousse,"Repousse les cibles"},
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
        public int? NbTour { get; set; }
        public Statistique.element? Stat { get; set; }
        BDService bd = new BDService();
        
        [JsonConstructor]
        public Effet(effet Nom, string NomSimplifier, int DmgMin, int DmgMax, int? NbTour, Statistique.element? Stat)
        {
            this.Nom = Nom;
            this.NomSimplifier = NomSimplifier;
            this.DmgMin = DmgMin;
            this.DmgMax = DmgMax;
            this.NbTour = NbTour;
            this.Stat = Stat;
        }

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
            if (ls.Count > 3 && ls[3] != "")
            {
                NbTour = Convert.ToInt16(ls[3]);
                Stat = (Statistique.element)Enum.Parse(typeof(Statistique.element), bd.selection("SELECT nom FROM TypesStatistiques WHERE idTypeStatistique = " + ls[4])[0][0], true);//convert string to enum;

            }
            else
            {
                NbTour = null;
                Stat = null;
            }

        }
    }
}
