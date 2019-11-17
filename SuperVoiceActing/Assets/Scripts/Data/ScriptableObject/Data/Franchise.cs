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
    public class Franchise
    {
        [SerializeField]
        public bool Enabled;

        [Space]
        [HorizontalGroup("FranchiseSeries")]
        [SerializeField]
        public bool isSeries = false;
        [Space]
        [HorizontalGroup("FranchiseSeries")]
        [SerializeField]
        public bool canScale = true;



        [SerializeField]
        [HorizontalGroup("FranchiseRandomStart")]
        public int canStartFranchiseAtMin = 0;
        [SerializeField]
        [HideLabel]
        [HorizontalGroup("FranchiseRandomStart")]
        public int canStartAtFranchiseAtMax = 0;

        [SerializeField]
        [HorizontalGroup("FranchiseRandomIncrease")]
        public int franchiseCountIncreaseMin = 1;
        [SerializeField]
        [HideLabel]
        [HorizontalGroup("FranchiseRandomIncrease")]
        public int franchiseCountIncreaseMax = 1;


        [Space]
        [HorizontalGroup("FranchiseReboot")]
        [ShowIf("isSeries", true)]
        [VerticalGroup("FranchiseReboot/Left")]
        [SerializeField]
        public bool canReboot = false;
        [ShowIf("canReboot", true)]
        [VerticalGroup("FranchiseReboot/Left")]
        [SerializeField]
        public int chanceOfRebootAtEachIteration = 0;
        [ShowIf("canReboot", true)]
        [SerializeField]
        [VerticalGroup("FranchiseReboot/Left")]
        public bool alwaysRebootAtEnd = true;

        [Space]
        [ShowIf("isSeries", true)]
        [SerializeField]
        [VerticalGroup("FranchiseReboot/Right")]
        public bool canRemaster = false;
        [ShowIf("canRemaster", true)]
        [SerializeField]
        [VerticalGroup("FranchiseReboot/Right")]
        public int chanceOfRemasterAtEachIteration = 100;
        [ShowIf("canRemaster", true)]
        [SerializeField]
        [VerticalGroup("FranchiseReboot/Right")]
        public bool alwaysRemasterAtEnd = true;


        [ShowIf("canRemaster", true)]
        [VerticalGroup("FranchiseReboot/Right")]
        [SerializeField]
        public string remasterSuffix = "";
        [ShowIf("canRemaster", true)]
        [VerticalGroup("FranchiseReboot/Right")]
        [SerializeField]
        public bool prefix = false;


        [Space]
        [HorizontalGroup("Franchise")]
        [VerticalGroup("Franchise/Left")]
        [SerializeField]
        public bool priorityScaling = false;
        [VerticalGroup("Franchise/Left")]
        [SerializeField]
        public bool priorityOrder = false;
        [VerticalGroup("Franchise/Left")]
        [SerializeField]
        public bool priorityOrderNoLoop = false;
        [VerticalGroup("Franchise/Left")]
        [SerializeField]
        public bool priorityRandom = false;

        [Space]
        [VerticalGroup("Franchise/Right")]
        [SerializeField]
        public List<ContractData> contractDatas;





        public int GetSeriesIndex(int franchiseCount, int playerLevel)
        {
            int selectIndex = 0;
            if (priorityScaling == true) // Select the hardest contract for the current playerLevel
            {
                int bestLevel = playerLevel;
                for (int i = 0; i < contractDatas.Count; i++)
                {
                    if (contractDatas[i].Level > bestLevel && contractDatas[i].Level < playerLevel)
                    {
                        bestLevel = contractDatas[i].Level;
                        selectIndex = i + 1;
                    }
                }
            }
            else if (priorityOrder == true) // Select the next contract of the list
            {
                selectIndex = franchiseCount % (contractDatas.Count + 1);
            }
            else if (priorityOrderNoLoop == true) // Select the next contract of the list but never go back to the first one
            {
                selectIndex = Mathf.Clamp(franchiseCount, 0, contractDatas.Count);
            }
            else if (priorityRandom == true) // Select un contrat au pif
            {
                selectIndex += Random.Range(1, contractDatas.Count + 1);
                selectIndex = selectIndex % (contractDatas.Count + 1);
            }
            return selectIndex;
        }




        public bool FranchiseDirection(FranchiseSave franchiseSave)
        {
            int chanceReboot = 0;
            int chanceRemaster = 0;

            if (canReboot == true && franchiseSave.CurrentFranchiseNumber >= (contractDatas.Count + 1)) 
            {
                if(alwaysRebootAtEnd == true)
                {
                    chanceReboot = 100;
                }
                else
                {
                    chanceReboot = chanceOfRebootAtEachIteration;
                }
            }

            if (canRemaster == true && franchiseSave.CurrentFranchiseNumber >= (contractDatas.Count + 1) && franchiseSave.IsRemaster == false)
            {
                if (alwaysRemasterAtEnd == true)
                {
                    chanceRemaster = 100;
                }
                else
                {
                    chanceRemaster = chanceOfRemasterAtEachIteration;
                }
            }

            int r = Random.Range(0, 100);
            if(r < chanceReboot) // Reboot
            {               
                franchiseSave.IsRemaster = false;
                franchiseSave.CurrentFranchiseNumber = 0;
                franchiseSave.RebootFranchise();
                return false;
            }
            else if (r < chanceRemaster) // Remaster
            {            
                franchiseSave.IsRemaster = true;
                return true;
            }
            else // Sequel
            {
                return true;
            }
        }


    }

} // #PROJECTNAME# namespace