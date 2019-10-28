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
    [CreateAssetMenu(fileName = "DatabaseCharacter", menuName = "Database/DatabaseCharacter", order = 1)]
    public class CharacterSpriteDatabase: ScriptableObject
    {
        [AssetList(AutoPopulate = true)]
        [SerializeField]
        StoryCharacterData[] characterDatabase;

    } 

} // #PROJECTNAME# namespace