/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Sirenix.OdinInspector;

namespace VoiceActing
{
	public class VoxographyButton : MonoBehaviour
	{

        [SerializeField]
        ImageDictionnary imageDictionary;

        [SerializeField]
        TextMeshProUGUI textContractLeft;
        [SerializeField]
        TextMeshProUGUI textContractRight;

        [Space]
        [SerializeField]
        GameObject[] gameObjectRolesLeft;
        [SerializeField]
        Image[] imageRoleFaceLeft;
        [SerializeField]
        Image[] imageRoleEmotionLeft;
        [SerializeField]
        TextMeshProUGUI[] textRoleNameLeft;

        [Space]
        [SerializeField]
        GameObject[] gameObjectRolesRight;
        [SerializeField]
        Image[] imageRoleFaceRight;
        [SerializeField]
        Image[] imageRoleEmotionRight;
        [SerializeField]
        TextMeshProUGUI[] textRoleNameRight;

        public bool DrawVoxography(Voxography voxographyLeft, Voxography voxographyRight)
        {
            bool res = true;
            this.gameObject.SetActive(true);
            if (voxographyRight == null)
                res = false;
            else if (voxographyLeft.Roles.Count == 3 || voxographyRight.Roles.Count == 3)
                res = false;

            // Draw Left
            textContractLeft.text = voxographyLeft.ContractName;
            for (int i = 0; i < gameObjectRolesLeft.Length; i++)
            {
                if(i >= voxographyLeft.Roles.Count)
                {
                    gameObjectRolesLeft[i].SetActive(false);
                }
                else
                {
                    gameObjectRolesLeft[i].SetActive(true);
                    imageRoleFaceLeft[i].sprite = voxographyLeft.Roles[i].RoleSprite;
                    imageRoleEmotionLeft[i].sprite = imageDictionary.GetSprite((int)voxographyLeft.EmotionGained[i]);
                    textRoleNameLeft[i].text = voxographyLeft.Roles[i].Name;
                }
            }


            // Draw Right
            if(res == true)
            {
                textContractRight.gameObject.SetActive(true);
                textContractRight.text = voxographyRight.ContractName;
                for (int i = 0; i < gameObjectRolesRight.Length; i++)
                {
                    if (i >= voxographyRight.Roles.Count)
                    {
                        gameObjectRolesRight[i].SetActive(false);
                    }
                    else
                    {
                        gameObjectRolesRight[i].SetActive(true);
                        imageRoleFaceRight[i].sprite = voxographyRight.Roles[i].RoleSprite;
                        imageRoleEmotionRight[i].sprite = imageDictionary.GetSprite((int)voxographyRight.EmotionGained[i]);
                        textRoleNameRight[i].text = voxographyRight.Roles[i].Name;
                    }
                }
            }
            else
            {
                textContractRight.gameObject.SetActive(false);
            }

            return res;
        }
		
	} // VoxographyButton class
	
}// #PROJECTNAME# namespace
