/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoiceActing
{
	public class MenuVoxography : MonoBehaviour
	{
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */

        [SerializeField]
        VoxographyButton voxographyPrefab;
        [SerializeField]
        RectTransform transformScroll;

        [Space]
        [SerializeField]
        GameObject panelVoxography;
        [SerializeField]
        GameObject panelStat;
        [SerializeField]
        GameObject panelSkill;

        int indexVoxography = 0;
        List<VoxographyButton> voxographyButtons = new List<VoxographyButton>();

        #endregion


        #region Functions 

        /* ======================================== *\
         *                FUNCTIONS                 *
        \* ======================================== */

        public void ShowVoxographyPanel(bool b)
        {
            this.gameObject.SetActive(b);
            panelVoxography.gameObject.SetActive(b);
            panelStat.gameObject.SetActive(!b);
            panelSkill.gameObject.SetActive(!b);
        }


        public void DrawVoxography(List<Voxography> voxography)
        {
            indexVoxography = 0;
            for (int i = 0; i < voxography.Count; i++)
            {
                if (indexVoxography >= voxographyButtons.Count)
                {
                    CreateVoxographyButton();
                }

                
                if (i + 1 >= voxography.Count) // On est à la fin donc pas de i+1 sinon error
                {
                    voxographyButtons[indexVoxography].DrawVoxography(voxography[i], null);
                }
                else
                {
                    if (voxographyButtons[indexVoxography].DrawVoxography(voxography[i], voxography[i + 1]))
                    {
                        i += 1;
                    }
                }
                indexVoxography += 1;
            }
            for (int i = indexVoxography; i < voxographyButtons.Count; i++)
            {
                voxographyButtons[i].gameObject.SetActive(false);
            }
        }



        public void CreateVoxographyButton()
        {
            voxographyButtons.Add(Instantiate(voxographyPrefab, transformScroll));
        }
        
        #endregion
		
	} // MenuVoxography class
	
}// #PROJECTNAME# namespace
