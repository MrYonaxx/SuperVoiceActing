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
        private SkillActorButton[] skillActorsButtons;


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
            for (int i = 0; i < skillActorsButtons.Length; i++)
            {
                if (actor.Potentials.Length <= i)
                {
                    skillActorsButtons[i].gameObject.SetActive(false);
                    continue;
                }

                if (relation < 0)
                {
                    skillActorsButtons[i].gameObject.SetActive(false);
                }
                else
                {
                    skillActorsButtons[i].gameObject.SetActive(true);
                    skillActorsButtons[i].DrawSkillActor((SkillActorData)skillDatabase.GetSkillData(actor.Potentials[i]), relation, actor.FriendshipLevel[i]);
                    relation -= actor.FriendshipLevel[i];
                }
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