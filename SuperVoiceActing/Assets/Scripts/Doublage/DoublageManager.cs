/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using UnityEngine;
using System.Collections;

namespace VoiceActing
{
    /// <summary>
    /// Definition of the DoublageManager class
    /// </summary>
    public class DoublageManager : MonoBehaviour
    {
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */

        [Header("Controllers")]
        [SerializeField]
        CameraController cameraController;
        [SerializeField]
        InputController inputController;

        [SerializeField]
        EmotionAttackManager emotionAttackManager;
        [SerializeField]
        EnemyManager enemyManager;

        [SerializeField]
        TextPerformanceAppear textPerformanceAppear;


        //[Header("Contrat")]

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


        ////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Start is called on the frame when a script is enabled just before any of the Update methods is called the first time.
        /// </summary>
        protected virtual void Start()
        {
            StartCoroutine(IntroductionSequence());
        }
        
        private IEnumerator IntroductionSequence()
        {
            //
            yield return null;
            emotionAttackManager.SwitchCardTransformIntro();
            inputController.enabled = true;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Update is called once per frame.
        /// </summary>
        protected void Update()
        {
            
        }


        public void Attack()
        {
            textPerformanceAppear.ExplodeLetter(enemyManager.DamagePhrase(emotionAttackManager.GetComboEmotion(), textPerformanceAppear.GetWordSelected()));
        }

        #endregion

    } // DoublageManager class

} // #PROJECTNAME# namespace