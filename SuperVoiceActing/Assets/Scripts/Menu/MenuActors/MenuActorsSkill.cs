/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Sirenix.OdinInspector;

namespace VoiceActing
{
    public class MenuActorsSkill: MenuScrollList
    {
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */
        [Title("Actors Skills")]
        [SerializeField]
        private SkillDatabase skillDatabase;
        [SerializeField]
        private SkillActorButton[] skillMovelistButtons;
        [SerializeField]
        private SkillActorButton[] skillPassiveButtons;


        [SerializeField]
        GameObject gameObjectDescription;
        [SerializeField]
        TextMeshProUGUI textSkillDescription;

        VoiceActor currentActor;

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

        public void DrawActorSkills(VoiceActor actor)
        {
            currentActor = actor;
            int relation = actor.Relation;
            SkillActorData skill;
            int indexPassive = 0;
            int indexMovelist = 0;
            int i = 0;
            while(relation >= 0 && i <= actor.Potentials.Length)
            {
                skill = (SkillActorData)skillDatabase.GetSkillData(actor.Potentials[i]);
                if (skill.IsPassive == true)
                {
                    skillPassiveButtons[indexPassive].gameObject.SetActive(true);
                    skillPassiveButtons[indexPassive].DrawSkillActor((SkillActorData)skillDatabase.GetSkillData(actor.Potentials[i]), relation, actor.FriendshipLevel[i]);
                    skillPassiveButtons[indexPassive].SetSkillNumber(i);
                    indexPassive += 1;
                }
                else
                {
                    skillMovelistButtons[indexMovelist].gameObject.SetActive(true);
                    skillMovelistButtons[indexMovelist].DrawSkillActor((SkillActorData)skillDatabase.GetSkillData(actor.Potentials[i]), relation, actor.FriendshipLevel[i]);
                    skillMovelistButtons[indexMovelist].SetSkillNumber(i);
                    indexMovelist += 1;
                }
                relation -= actor.FriendshipLevel[i];
                i += 1;
            }
            for(int j = indexPassive; j < skillPassiveButtons.Length; j++)
            {
                skillPassiveButtons[j].gameObject.SetActive(false);
            }
            for (int j = indexMovelist; j < skillMovelistButtons.Length; j++)
            {
                skillMovelistButtons[j].gameObject.SetActive(false);
            }
        }


        public void DrawSkillDescription(int i)
        {
            animatorSelection.gameObject.SetActive(true);
            animatorSelection.transform.position = buttonsList[i].transform.position;
            gameObjectDescription.gameObject.SetActive(true);
            textSkillDescription.text = skillDatabase.GetSkillData(currentActor.Potentials[i]).Description;
        }

        public void HideSkillDescription()
        {
            animatorSelection.gameObject.SetActive(false);
            gameObjectDescription.gameObject.SetActive(false);
        }

        protected override void BeforeSelection()
        {

        }

        protected override void AfterSelection()
        {

        }

        #endregion

    } 

} // #PROJECTNAME# namespace