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

namespace VoiceActing
{

    public enum SortActorOptions
    {

    }

	public class SortManager : MonoBehaviour
	{
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */

        [SerializeField]
        Animator animatorPanelSort;
        [SerializeField]
        RectTransform selectionSort;
        [SerializeField]
        RectTransform[] rectTransformsSorts;

        [SerializeField]
        TextMeshProUGUI textCroissant;
        [SerializeField]
        TextMeshProUGUI textDecroissant;
        [SerializeField]
        TextMeshProUGUI textDisponible;

        [SerializeField]
        Color colorActive;
        [SerializeField]
        Color colorUnactive;

        [SerializeField]
        MenuActorsManager menuActorsManager;



        // ===========================
        // Menu Input
        [Header("Menu Input")]
        [SerializeField]
        private InputController inputController;
        [SerializeField]
        private int timeBeforeRepeat = 10;
        [SerializeField]
        private int repeatInterval = 3;

        private int currentTimeBeforeRepeat = -1;
        private int currentRepeatInterval = -1;
        private int lastDirection = 0; // 2 c'est bas, 8 c'est haut (voir numpad)
        private int indexSortSelected = 0;
        // Menu Input
        // ===========================






        Dictionary<int, string> sortOptions = new Dictionary<int, string>
        {
            { 1, "Nom" },
            { 2, "Level" },
            { 3, "HP" },
            { 4, "HP%" },
            { 5, "Fan" },
            { 6, "Prix" },
            { 7, "Relation" },
            { 8, "Joie" },
            { 9, "Tristesse" },
            { 10, "Dégoût" },
            { 11, "Colère" },
            { 12, "Surprise" },
            { 13, "Douceur" },
            { 14, "Peur" },
            { 15, "Confiance" }
        };

        /*bool sortDecroissant = true;
        bool sortAvailability = false;*/
        
        #endregion


        #region Functions 

        /* ======================================== *\
         *                FUNCTIONS                 *
        \* ======================================== */

        public List<VoiceActor> SortActorsList(List<VoiceActor> voiceActors)
        {
            switch(indexSortSelected)
            {
                case 0: // Nom
                    voiceActors.Sort(CompareByName);
                    break;
                case 1: // Level
                    voiceActors.Sort(CompareByLevel);
                    break;
                case 2: // HP
                    voiceActors.Sort(CompareByHealth);
                    break;
                case 3: // HP%
                    voiceActors.Sort(CompareByLevel);
                    break;
                case 4: // Fan
                    voiceActors.Sort(CompareByFan);
                    break;
                case 5: // Prix
                    voiceActors.Sort(CompareByPrice);
                    break;
                case 6: // Relation
                    //voiceActors.Sort(CompareByLevel);
                    break;

                case 7: // Joie
                    voiceActors.Sort(CompareByJoy);
                    break;
                case 8: // Tristesse
                    voiceActors.Sort(CompareBySadness);
                    break;
                case 9: // Dégoût
                    voiceActors.Sort(CompareByDisgust);
                    break;
                case 10: // Colère
                    voiceActors.Sort(CompareByAnger);
                    break;
                case 11: // Surprise
                    voiceActors.Sort(CompareBySurprise);
                    break;
                case 12: // Douceur
                    voiceActors.Sort(CompareBySweetness);
                    break;
                case 13: // Peur
                    voiceActors.Sort(CompareByFear);
                    break;
                case 14: // Confiance
                    voiceActors.Sort(CompareByTrust);
                    break;
            }

            /*if (sortAvailability == true)
                //voiceActors.Sort(CompareByAvailability);*/

            /*if (sortDecroissant == true)
                voiceActors.Reverse();*/

            return voiceActors;
        }


        private int CompareByName(VoiceActor a, VoiceActor b)
        {
            return a.VoiceActorName.CompareTo(b.VoiceActorName);

        }

        private int CompareByHealth(VoiceActor b, VoiceActor a)
        {
            return a.Hp.CompareTo(b.Hp);
        }

        private int CompareByLevel(VoiceActor b, VoiceActor a)
        {
            return a.Level.CompareTo(b.Level);
        }

        private int CompareByFan(VoiceActor b, VoiceActor a)
        {
            return a.Fan.CompareTo(b.Fan);
        }

        private int CompareByPrice(VoiceActor b, VoiceActor a)
        {
            return a.Price.CompareTo(b.Price);
        }


        private int CompareByJoy(VoiceActor b, VoiceActor a)
        {
            return a.Statistique.Joy.CompareTo(b.Statistique.Joy);
        }
        private int CompareBySadness(VoiceActor b, VoiceActor a)
        {
            return a.Statistique.Sadness.CompareTo(b.Statistique.Sadness);
        }
        private int CompareByDisgust(VoiceActor b, VoiceActor a)
        {
            return a.Statistique.Disgust.CompareTo(b.Statistique.Disgust);
        }
        private int CompareByAnger(VoiceActor b, VoiceActor a)
        {
            return a.Statistique.Anger.CompareTo(b.Statistique.Anger);
        }
        private int CompareBySurprise(VoiceActor b, VoiceActor a)
        {
            return a.Statistique.Surprise.CompareTo(b.Statistique.Surprise);
        }
        private int CompareBySweetness(VoiceActor b, VoiceActor a)
        {
            return a.Statistique.Sweetness.CompareTo(b.Statistique.Sweetness);
        }
        private int CompareByFear(VoiceActor b, VoiceActor a)
        {
            return a.Statistique.Fear.CompareTo(b.Statistique.Fear);
        }
        private int CompareByTrust(VoiceActor b, VoiceActor a)
        {
            return a.Statistique.Trust.CompareTo(b.Statistique.Trust);
        }



        private int CompareByAvailability(VoiceActor a, VoiceActor b)
        {
            return a.Availability.CompareTo(b.Availability);
        }








        private IEnumerator ShakeCoroutine(Transform transform, float power, int time)
        {
            Vector3 start = transform.localPosition;
            float speed = power / time;
            while (time != 0)
            {
                power -= speed;
                transform.localPosition = new Vector3(start.x /*+ Random.Range(-power, power)*/, start.y + Random.Range(-power, power), start.z /*+ Random.Range(-power, power)*/);
                time -= 1;
                yield return null;
            }
            transform.localPosition = start;
        }








        public void MoveSortSelection()
        {
            selectionSort.position = rectTransformsSorts[indexSortSelected].transform.position;
            StartCoroutine(ShakeCoroutine(rectTransformsSorts[indexSortSelected], 1, 10));
        }



        public void ShowSortPanel()
        {
            animatorPanelSort.SetTrigger("Appear");
            inputController.gameObject.SetActive(true);
        }

        public void HideSortPanel()
        {
            animatorPanelSort.SetTrigger("Disappear");
            inputController.gameObject.SetActive(false);
        }






        // ==================================================================================
        // ==================================================================================
        // ==================================================================================
        public void Validate()
        {
            /*if(indexSortSelected == sortOptions.Count) // Croissant
            {
                sortDecroissant = !sortDecroissant;
                if (sortDecroissant == true)
                {
                    textDecroissant.color = colorActive;
                    textCroissant.color = colorUnactive;
                }
                else
                {
                    textDecroissant.color = colorUnactive;
                    textCroissant.color = colorActive;
                }
            }
            else if (indexSortSelected == sortOptions.Count + 1) // Decroissant
            {
                sortDecroissant = !sortDecroissant;
                if (sortDecroissant == true)
                {
                    textDecroissant.color = colorActive;
                    textCroissant.color = colorUnactive;
                }
                else
                {
                    textDecroissant.color = colorUnactive;
                    textCroissant.color = colorActive;
                }
            }*/
            /*else if (indexSortSelected == sortOptions.Count + 2) // Disponibilité
            {
                sortAvailability = !sortAvailability;
                if (sortAvailability == true)
                    textDisponible.color = colorActive;
                else
                    textDisponible.color = colorUnactive;
            }*/
            menuActorsManager.SortActorsListFast();
        }


        public void SelectSortOptionUp()
        {
            if (lastDirection != 8)
            {
                StopRepeat();
                lastDirection = 8;
            }
            if (CheckRepeat() == false)
                return;

            indexSortSelected -= 1;
            if (indexSortSelected <= -1)
            {
                indexSortSelected = sortOptions.Count-1;
            }
            MoveSortSelection();
        }

        public void SelectSortOptionDown()
        {
            if (lastDirection != 2)
            {
                StopRepeat();
                lastDirection = 2;
            }
            if (CheckRepeat() == false)
                return;

            indexSortSelected += 1;
            if (indexSortSelected >= sortOptions.Count)
            {
                indexSortSelected = 0;
            }
            MoveSortSelection();
        }

        // ==================================================================================
        // Check si on peut repeter l'input
        private bool CheckRepeat()
        {
            if (currentRepeatInterval == -1)
            {
                if (currentTimeBeforeRepeat == -1)
                {
                    currentTimeBeforeRepeat = timeBeforeRepeat;
                    return true;
                }
                else if (currentTimeBeforeRepeat == 0)
                {
                    currentRepeatInterval = repeatInterval;
                }
                else
                {
                    currentTimeBeforeRepeat -= 1;
                }
            }
            else if (currentRepeatInterval == 0)
            {
                currentRepeatInterval = repeatInterval;
                return true;
            }
            else
            {
                currentRepeatInterval -= 1;
            }
            return false;
        }

        public void StopRepeat()
        {
            currentRepeatInterval = -1;
            currentTimeBeforeRepeat = -1;
        }
        // ==================================================================================

        #endregion

    } // SortManager class

}// #PROJECTNAME# namespace
