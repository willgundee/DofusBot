namespace GofusSharp
{
    public class Sort
    {
        public enum nom_sort { felintion, chance_d_ecaflip, bond_du_felin, bluff, pile_ou_face, perception, contrecoup, griffe_invocatrice, tout_ou_rien, roulette, topkaj, langue_rapeuse, roue_de_la_fortune, esprit_felin, trefle, odorat, reflexes, griffe_joueuse, griffe_de_ceangal, rekopdestin_d_ecaflip, invocation_de_dopeul_ecaflip, mot_blessant, mot_alternatif, mot_d_amitie, mot_decisif, mot_interdit, mot_de_frayeur, mot_stimulant, mot_turbulent, mot_de_jouvence, mot_selectif, mot_eclatant, mot_de_prevention, mot_de_regeneration, mot_d_immobilisation, mot_deroutant, mot_tournoyant, mot_fracassant, mot_de_silence, mot_d_envol, mot_de_revitalisation, mot_de_reconstitution, invocation_de_dopeul_eniripsa, brokle, pression, bond, intimidation, vitalite, epee_divine, epeedesctructrice, poutch, souffle, concentration, couper, friction, duel, epee_du_jugement, puissance, precipitation, tempete_de_puissance, epee_celeste, epee_du_iop, epee_du_destin, colere_du_iop, invocation_de_dopeul_iop, fleche_de_dispersion, fleche_magique, fleche_empoisonne, fleche_de_recul, fleche_glacee, fleche_enflammee, tir_eloigne, fleche_d_expiation, œil_de_taupe, tir_critique, fleche_d_immobilisation, fleche_punitive, tir_puissant, fleche_harcelante, fleche_persecutrice, fleche_cinglante, fleche_destructrice, fleche_absorbante, fleche_ralentissante, fleche_explosive, maitrise_de_l_arc, invocation_de_dopeul_cra, mise_en_garde, aveuglement, attaque_naturelle, rempart, typhon, bulle, barricade, glyphe_agressif, lethargie, attaque_nuageuse, bastion, retour_du_baton, teleglyphe, glyphe_de_repulsion, treve, glyphe_d_aveuglement, frisson, glyphe_optique, glyphe_gravitationnel, glyphe_enflamme, bouclier_feca, invocation_de_dopeul_feca, douleur_partage, chatiment_force, attirance, pied_du_sacrieur, derobade, detour, assaut, chatiment_agile, dissolution, chatiment_ose, chatiment_spirituel, sacrifice, absorption, chatiment_vitalesque, cooperation, transposition, punition, furie, epee_volante, transfert_de_vie, folie_sanguinaire, invocation_de_dopeul_sacrieur, ronce, la_bloqueuse, poison_paralysant, sacrifice_poupesque, larme, la_folle, ronce_apaisante, puissance_sylvestre, la_sacrifiee, tremblement, connaissance_des_poupees, ronces_multiples, arbre, vent_empoisonne, la_gonflable, ronce_agressives, herbe_folle, feu_de_brousse, ronce_insolente, la_surpuissante, invocation_de_dopeul_sadida, laisse_spirituelle, griffe_spectrale, resistance_naturelle, invocation_de_tofu, beneditcion_animale, deplacement_felin, invocation_de_bouftou, crapaud, invocation_de_prespic, fouet, piqure_motivante, corbeau, griffe_cinglante, soin_animal, invocation_de_sanglier, frappe_du_craqueleur, cris_de_l_ours, invocation_de_bwork_mage, crocs_du_mulou, invocation_de_craqueleur, invocation_de_dragonnet_rouge, invocation_de_dopeul_osamodas, retraite_anticipee, sac_anime, lancer_de_pelle, lancer_de_pieces, pelle_fantomatique, acceleration, boite_de_pandore, remblai, cle_reductrice, force_de_l_age, pelle_animee, cupidite, roulage_de_pelle, maladresse, maladresse_de_masse, chance, pelle_de_jugement, pelle_massacrante, corruption, desinvocation, coffre_anime, invocation_de_dopeul_enutrof, poisse, sournoiserie, piege_sournois, invisibilite, poison_insidieux, fourvoiment, coup_sournois, double_sram, pulsion_de_chakra, piege_de_masse, invisibilite_d_autrui, piege_empoisonne, concentration_de_chakra, piege_d_immobilisation, piege_de_silence, piege_repulsif, peur, arnaque, reperage, attaque_mortelle, piege_mortel, invocation_de_dopeul_sram, ralentissement, rembobinage, aiguille, gelure, sablier_de_xelor, rayon_obscur, teleportation, fletrissement, flou, poussiere_temporelle, vol_du_temps, aiguille_chercheuse, devoument, fuite, demotivation, contre, momification, horloge, frappe_de_xelor, cadran_de_xelor, invocation_de_dopeul_xelor }
        public int IdSort { get; internal set; }
        public Effet[] TabEffets { get; internal set; }
        public string Nom { get; internal set; }
        public nom_sort VraiNom { get; internal set; }
        public bool LigneDeVue { get; internal set; }
        public bool PorteeModifiable { get; internal set; }
        public bool CelluleLibre { get; internal set; }
        public Zone ZonePortee { get; internal set; }
        public Zone ZoneEffet { get; internal set; }
        public int TauxDeRelance { get; internal set; } //3 = cooldown 3 tour // -3 = 3 lancer par tour max //0 infinie lancer
        public int CoutPA { get; internal set; }
        internal Sort(int IdSort, Effet[] TabEffets, string Nom, bool LigneDeVue, bool PorteeModifiable, bool CelluleLibre, Zone ZonePortee, Zone ZoneEffet, int TauxDeRelance, int CoutPA, nom_sort VraiNom)
        {
            this.IdSort = IdSort;
            this.TabEffets = TabEffets;
            this.Nom = Nom;
            this.LigneDeVue = LigneDeVue;
            this.PorteeModifiable = PorteeModifiable;
            this.CelluleLibre = CelluleLibre;
            this.ZonePortee = ZonePortee;
            this.ZoneEffet = ZoneEffet;
            this.TauxDeRelance = TauxDeRelance;
            this.CoutPA = CoutPA;
            this.VraiNom = VraiNom;
        }
    }
}
