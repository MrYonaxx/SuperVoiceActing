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
    [CreateAssetMenu(fileName = "StoryEventDatabase", menuName = "Database/StoryEventDatabase", order = 1)]
    public class StoryEventDatabase : ScriptableObject
    {

        [AssetList(AutoPopulate = true, Path = "/ScriptableObject/StoryEvent")]
        [SerializeField]
        private List<StoryEventData> storyEventsDatabase;
        public List<StoryEventData> StoryEventsDatabase
        {
            get { return storyEventsDatabase; }
        }


        [Button]
        private void DebugFunction()
        {
            /*for(int i = 0; i < storyEventsDatabase.Count; i++)
            {
                for (int j = 0; j < storyEventsDatabase[i].GetEventSize(); j++)
                {
                    StoryEvent node = storyEventsDatabase[i].GetEventNode(j);
                    if(node is StoryEventText)
                    {
                        StoryEventText node2 = (StoryEventText)node;
                        node2.interlocutor = FindCorrespondingActor(node2.GetInterlocuteur());
                    }
                    else if (node is StoryEventMoveCharacter)
                    {
                        StoryEventMoveCharacter node2 = (StoryEventMoveCharacter)node;
                        node2.characterMoving = FindCorrespondingActor(node2.CharacterToMove);
                    }
                }
                for (int j = 0; j < storyEventsDatabase[i].Characters.Length; j++)
                {
                    storyEventsDatabase[i].Characters[j].characterMoving = FindCorrespondingActor(storyEventsDatabase[i].Characters[j].CharacterToMove);
                }
            }*/
        }


        /*private VoiceActorData FindCorrespondingActor(StoryCharacterData chara)
        {
            if (chara == null)
                return null;
            for(int i = 0; i < voiceActorDatabase.VoiceActorsDatabase.Count; i++)
            {
                if(voiceActorDatabase.VoiceActorsDatabase[i].ActorName == chara.GetName())
                {
                    return voiceActorDatabase.VoiceActorsDatabase[i];
                }
            }
            return null;
        }*/

    }

} // #PROJECTNAME# namespace