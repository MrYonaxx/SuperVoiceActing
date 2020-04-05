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
    [System.Serializable]
    public class Equipement
    {
        [FoldoutGroup("$equipmentName")]
        [SerializeField]
        private string equipmentName;
        public string EquipmentName
        {
            get { return equipmentName; }
        }
        [FoldoutGroup("$equipmentName")]
        [HideLabel]
        [SerializeField]
        private EquipementCategory equipementCategory;
        public EquipementCategory EquipementCategory
        {
            get { return equipementCategory; }
        }

        [FoldoutGroup("$equipmentName")]
        [SerializeField]
        private int price;
        public int Price
        {
            get { return price; }
        }
        [FoldoutGroup("$equipmentName")]
        [SerializeField]
        private int maintenance;
        public int Maintenance
        {
            get { return maintenance; }
        }
        [FoldoutGroup("$equipmentName")]
        [TextArea(1, 2)]
        [SerializeField]
        private string description;
        public string Description
        {
            get { return description; }
        }

        [FoldoutGroup("$equipmentName")]
        [Title("Statistique")]
        [SerializeField]
        private int soundQuality;
        public int SoundQuality
        {
            get { return soundQuality; }
        }
        [FoldoutGroup("$equipmentName")]
        [Title("Offensive Bonus")]
        [SerializeField]
        private int flatAtkBoost;
        public int FlatAtkBoost
        {
            get { return flatAtkBoost; }
        }
        [FoldoutGroup("$equipmentName")]
        [SerializeField]
        private int criticalPercentageBoost;
        public int CriticalPercentageBoost
        {
            get { return criticalPercentageBoost; }
        }
        [FoldoutGroup("$equipmentName")]
        [HideLabel]
        [SerializeField]
        private EmotionStat atkBonus;
        public EmotionStat AtkBonus
        {
            get { return atkBonus; }
        }

        [Space]
        [Space]
        [FoldoutGroup("$equipmentName")]
        [SerializeField]
        private int flatDefenseBoost;
        public int FlatDefenseBoost
        {
            get { return flatDefenseBoost; }
        }
        [HideLabel]
        [FoldoutGroup("$equipmentName")]
        [SerializeField]
        private EmotionStat defBonus;
        public EmotionStat DefBonus
        {
            get { return defBonus; }
        }

        [FoldoutGroup("$equipmentName")]
        [Title("Autres")]
        [SerializeField]
        private int scoreBonus;
        public int ScoreBonus
        {
            get { return scoreBonus; }
        }
        [FoldoutGroup("$equipmentName")]
        [SerializeField]
        private int expRate;
        public int ExpRate
        {
            get { return expRate; }
        }
        [FoldoutGroup("$equipmentName")]
        [SerializeField]
        private int turnBonus;
        public int TurnBonus
        {
            get { return turnBonus; }
        }
        [FoldoutGroup("$equipmentName")]
        [SerializeField]
        private SkillData[] immuneState;
        public SkillData[] ImmuneState
        {
            get { return immuneState; }
        }
        [FoldoutGroup("$equipmentName")]
        [Title("Ingenieur du Son")]
        [SerializeField]
        private int soundEngiMixingBonus;
        public int SoundEngiMixingBonus
        {
            get { return soundEngiMixingBonus; }
        }
        [FoldoutGroup("$equipmentName")]
        [SerializeField]
        private int soundEngiPowerBonus;
        public int SoundEngiPowerBonus
        {
            get { return soundEngiPowerBonus; }
        }
        [FoldoutGroup("$equipmentName")]
        [SerializeField]
        private int soundEngiTurnBonus;
        public int SoundEngiTurnBonus
        {
            get { return soundEngiTurnBonus; }
        }



        public Equipement(EquipementData data)
        {
            equipmentName = data.EquipmentName;
            equipementCategory = data.EquipementCategory;
            price = data.Price;
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

    } 

} // #PROJECTNAME# namespace