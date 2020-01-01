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
    public class BuffWindow : MonoBehaviour
    {
        [SerializeField]
        TextMeshProUGUI textBuffName;

        [SerializeField]
        TextMeshProUGUI textBuffTimer;

        [SerializeField]
        Color colorBuff;
        [SerializeField]
        Color colorDebuff;


        public void DrawBuff(Buff buff)
        {
            //textBuffName.text = buff.BuffData.BuffName;
            textBuffTimer.text = buff.Turn.ToString();
        }

    } 

} // #PROJECTNAME# namespace