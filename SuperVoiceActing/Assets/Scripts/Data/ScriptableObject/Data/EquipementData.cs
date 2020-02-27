/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace VoiceActing
{
    public enum EquipementCategory
    {
        Microphone,
        Enceinte,
        TabledeMixage,
        Accessoire,
        All
    }

    [CreateAssetMenu(fileName = "EquipementData", menuName = "ItemData/EquipementData", order = 1)]
    public class EquipementData : ScriptableObject
	{
        [HorizontalGroup("General")]
        [SerializeField]
        private string equipmentName;
        public string EquipmentName
        {
            get { return equipmentName; }
        }
        [HorizontalGroup("General")]
        [HideLabel]
        [SerializeField]
        private EquipementCategory equipementCategory;
        public EquipementCategory EquipementCategory
        {
            get { return equipementCategory; }
        }

        [HorizontalGroup("Prix")]
        [SerializeField]
        private int price;
        public int Price
        {
            get { return price; }
        }
        [HorizontalGroup("Prix")]
        [SerializeField]
        private int maintenance;
        public int Maintenance
        {
            get { return maintenance; }
        }
        [SerializeField]
        private string description;
        public string Description
        {
            get { return description; }
        }

        [Space]
        [Space]
        [SerializeField]
        private int flatAtkBoost;
        public int FlatAtkBoost
        {
            get { return flatAtkBoost; }
        }
        [SerializeField]
        private int criticalPercentageBoost;
        public int CriticalPercentageBoost
        {
            get { return criticalPercentageBoost; }
        }

        [Header("Equipement Atk (%)")]
        [HideLabel]
        [SerializeField]
        private EmotionStat atkBonus;
        public EmotionStat AtkBonus
        {
            get { return atkBonus; }
        }

        [Space]
        [Space]

        [SerializeField]
        private int flatDefenseBoost;
        public int FlatDefenseBoost
        {
            get { return flatDefenseBoost; }
        }
        [Header("Equipement Def (%)")]
        [HideLabel]
        [SerializeField]
        private EmotionStat defBonus;
        public EmotionStat DefBonus
        {
            get { return defBonus; }
        }

        [Title("Autres")]
        [SerializeField]
        private int scoreBonus;
        public int ScoreBonus
        {
            get { return scoreBonus; }
        }
        [SerializeField]
        private int expRate;
        public int ExpRate
        {
            get { return expRate; }
        }
        [SerializeField]
        private int turnBonus;
        public int TurnBonus
        {
            get { return turnBonus; }
        }
        [SerializeField]
        private SkillData immuneState;
        public SkillData ImmuneState
        {
            get { return immuneState; }
        }

        [Title("Ingenieur du Son")]
        [SerializeField]
        private int soundEngiMixingBonus;
        public int SoundEngiMixingBonus
        {
            get { return soundEngiMixingBonus; }
        }
        [SerializeField]
        private int soundEngiPowerBonus;
        public int SoundEngiPowerBonus
        {
            get { return soundEngiPowerBonus; }
        }
        [SerializeField]
        private int soundEngiTurnBonus;
        public int SoundEngiTurnBonus
        {
            get { return soundEngiTurnBonus; }
        }

        public void Equip(PlayerData playerData)
        {
            playerData.AtkBonus.Add(atkBonus);
            playerData.DefBonus.Add(defBonus);
            playerData.Maintenance += maintenance;
        }

        public void Unequip(PlayerData playerData)
        {
            playerData.AtkBonus.Add(atkBonus.Reverse());
            playerData.DefBonus.Add(defBonus.Reverse());
            playerData.Maintenance -= maintenance;
        }


    } // EquipementData class
	
}// #PROJECTNAME# namespace
