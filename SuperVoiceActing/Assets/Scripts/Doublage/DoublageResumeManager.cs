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
    public class DoublageResumeManager: MonoBehaviour
    {
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */
        [SerializeField]
        TextAppearManager textAppearManager;
        [SerializeField]
        CharacterDoublageManager characterDoublageManager;

        [SerializeField]
        int resumeLetterSpeed = 3;

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

        private void DebugSetup(Contract contract)
        {

        }
            
        public IEnumerator ResumeMainCoroutine(Contract contract)
        {

            int currentInterlocutor = 0;

            DebugSetup(contract);
            yield return new WaitForSeconds(1);
            for (int i = 0; i < contract.CurrentLine; i++)
            {
                if (currentInterlocutor != contract.TextData[i].Interlocuteur)
                {
                    currentInterlocutor = contract.TextData[i].Interlocuteur;
                    characterDoublageManager.SwitchActorsSpeed(currentInterlocutor);
                }
                yield return new WaitForSeconds(0.6f);
                textAppearManager.SetMouth(characterDoublageManager.GetCharacter(contract.TextData[i].Interlocuteur));
                characterDoublageManager.GetCharacter(contract.TextData[i].Interlocuteur).ChangeEmotion(contract.EmotionsUsed[i].emotions[0]);
                textAppearManager.NewPhrase(contract.TextData[i].Text, contract.EmotionsUsed[i].emotions[0], false);
                textAppearManager.ApplyDamage(100);
                textAppearManager.SetLetterSpeed(resumeLetterSpeed);
                while (textAppearManager.GetEndLine() == false)
                    yield return null;
                yield return new WaitForSeconds(0.8f);
            }
            yield return null;
            textAppearManager.SetLetterSpeed(2);
            textAppearManager.NewPhrase(contract.TextData[contract.CurrentLine].Text, Emotion.Neutre, true);
        }

        #endregion

    } 

} // #PROJECTNAME# namespace