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
	public class DoublageEventSetCharacter : DoublageEvent
	{
        [SerializeField]
        private StoryCharacterData character;
        public StoryCharacterData Character
        {
            get { return character; }
        }

        [SerializeField]
        private bool customPos = true;
        public bool CustomPos
        {
            get { return customPos; }
        }

        [ShowIf("customPos", true)]
        [SerializeField]
        private Vector3 position;
        public Vector3 Position
        {
            get { return position; }
        }

        [ShowIf("customPos", true)]
        [SerializeField]
        private Vector3 rotation;
        public Vector3 Rotation
        {
            get { return rotation; }
        }

        [HideIf("customPos", true)]
        [SerializeField]
        private int defaultPosID = 0;
        public int DefaultPosID
        {
            get { return defaultPosID; }
        }

        [SerializeField]
        private bool flipX;
        public bool FlipX
        {
            get { return flipX; }
        }

        /*[SerializeField]
        private int viewportID;
        public int ViewportID
        {
            get { return viewportID; }
        }*/


        [SerializeField]
        private int orderLayer;
        public int OrderLayer
        {
            get { return orderLayer; }
        }

        [SerializeField]
        private Color color;
        public Color Color
        {
            get { return color; }
        }





        private CharacterDialogueController FindInterlocutor(DoublageEventManager doublageEventManager)
        {
            for (int i = 0; i < doublageEventManager.Characters.Count; i++)
            {
                if (doublageEventManager.Characters[i].gameObject.activeInHierarchy == false)
                    continue;
                if (doublageEventManager.Characters[i].GetStoryCharacterData() == character)
                {
                    return doublageEventManager.Characters[i];
                }
            }
            return null;
        }


        public override IEnumerator ExecuteNodeCoroutine(DoublageEventManager eventManager)
        {
            CharacterDialogueController interloc = FindInterlocutor(eventManager);
            if (interloc == null)
            {
                eventManager.CreateCharacters(this); // Pas le choix on peut pas instancier
            }
            else
            {
                interloc.ChangeOrderInLayer(orderLayer);
                interloc.ChangeTint(color);
            }
            yield break;
        }


    } // DoublageEventSetCharacter class
	
}// #PROJECTNAME# namespace
