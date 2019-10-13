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
    [CreateAssetMenu(fileName = "SeiyuuuData", menuName = "SeiyuuDensetsu/SeiyuuData", order = 1)]
    public class SeiyuuData: ScriptableObject
    {
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */

        [SerializeField]
        private VoiceActor voiceActor;
        public VoiceActor VoiceActor
        {
            get { return voiceActor; }
        }

        /*[SerializeField]
        private List<SeiyuuAction> voiceActor;
        public VoiceActor VoiceActor
        {
            get { return voiceActor; }
        }*/

        [SerializeField]
        private Date date;
        public Date Date
        {
            get { return date; }
        }

        [SerializeField]
        private Season season;
        public Season Season
        {
            get { return season; }
        }

        [SerializeField]
        private int bill;
        public int Bill
        {
            get { return bill; }
            set { bill = value; }
        }

        [SerializeField]
        private int money;
        public int Money
        {
            get { return money; }
            set { money = value; }
        }

        [SerializeField]
        private int moneyGain;
        public int MoneyGain
        {
            get { return moneyGain; }
            set { moneyGain = value; }
        }

        [SerializeField]
        private List<Contract> contractProposal;
        public List<Contract> ContractProposal
        {
            get { return contractProposal; }
            set { contractProposal = value; }
        }

        [SerializeField]
        private List<Contract> contractAccepted;
        public List<Contract> ContractAccepted
        {
            get { return contractAccepted; }
            set { contractAccepted = value; }
        }

        [SerializeField]
        private List<int> roleID;
        public List<int> RoleID
        {
            get { return roleID; }
            set { roleID = value; }
        }


        #endregion

        #region GettersSetters 

        /* ======================================== *\
         *           GETTERS AND SETTERS            *
        \* ======================================== */

        #endregion

        #region Functions 

        /* ======================================== *\
         *                FUNCTIONS                 *
        \* ======================================== */

        public SeiyuuData(VoiceActorData voiceActorData, Date initialDate, Season initialSeason, int initialMoney, int initialBill)
        {
            voiceActor = new VoiceActor(voiceActorData);
            date = new Date( initialDate.week, initialDate.month, initialDate.year);
            season = initialSeason;
            money = initialMoney;
            bill = initialBill;       
        }

        public void CreateSeiyuuData(VoiceActorData voiceActorData, Date initialDate, Season initialSeason, int initialMoney, int initialBill)
        {
            voiceActor = new VoiceActor(voiceActorData);
            date = new Date(initialDate.week, initialDate.month, initialDate.year);
            season = initialSeason;
            money = initialMoney;
            bill = initialBill;

            contractProposal = new List<Contract>();
            contractAccepted = new List<Contract>();
            roleID = new List<int>();
        }

        public void NextWeek(CalendarData calendar)
        {
            date.NextWeek(calendar);
        }

        #endregion

    } 

} // #PROJECTNAME# namespace