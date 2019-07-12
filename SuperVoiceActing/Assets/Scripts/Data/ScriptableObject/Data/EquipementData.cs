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
        Accessoire
    }

    [CreateAssetMenu(fileName = "EquipementData", menuName = "EquipementData", order = 1)]
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
        [Header("Equipement Def (%)")]
        [HideLabel]
        [SerializeField]
        private EmotionStat defBonus;
        public EmotionStat DefBonus
        {
            get { return defBonus; }
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
