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
using UnityEngine.Tilemaps;

namespace VoiceActing
{
    public class MenuResearchMovementManager: MonoBehaviour
    {
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */

        [Title("Data")]
        [SerializeField]
        int dungeonID = 0;
        [SerializeField]
        ResearchDungeonData[] dungeonLayouts;
        [SerializeField]
        ResearchEventDatabase researchEventDatabase;

        [SerializeField]
        Transform researchEventTransform;
        [SerializeField]
        Tilemap groundTilemap;
        [SerializeField]
        Tilemap explorationTilemap;
        [SerializeField]
        PlayerData playerData;
        public PlayerData PlayerData
        {
            get { return playerData; }
        }
        [SerializeField]
        InputController inputController;

        [SerializeField]
        List<ResearchEventData> researchEventList = new List<ResearchEventData>();
        public List<ResearchEventData> ResearchEventList
        {
            get { return researchEventList; }
        }
        



        [Title("Research")]
        [SerializeField]
        MenuRessourceResearch menuRessourceResearch;




        [Title("UI")]
        [SerializeField]
        TextMeshProUGUI textDungeonName;
        [SerializeField]
        TextMeshProUGUI explorationCount;




        [Title("Movement")]
        [SerializeField]
        float time = 1f;
        [SerializeField]
        float cellSize = 0.48f;
        [SerializeField]
        Transform character;




        [Title("Camera")]
        [SerializeField]
        GameObject cameraStudio;
        [SerializeField]
        GameObject cameraTilemap;




        [Title("Chest Open")]
        [SerializeField]
        InputController inputChest;
        [SerializeField]
        Animator animatorChest;
        [SerializeField]
        TextMeshProUGUI textResearchName;
        [SerializeField]
        TextMeshProUGUI textResearchDescription;

        private bool skipChest = false;




        private IEnumerator moveCoroutine;




        private int[,] currentEventLayout;

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

        private ResearchEvent GetFromResearchEventList(int eventID)
        {
            for(int i = 0; i < researchEventList.Count; i++)
            {
                if(researchEventList[i].eventID == eventID)
                {
                    return researchEventList[i].researchEvent;
                }
            }
            return null;
        }

        public string GetDungeonName(int i)
        {
            return dungeonLayouts[i].DungeonName;
        }



        // ================================================================================
        // R E N D E R    S E C T I O N

        public void SetDungeon(int i)
        {
            dungeonID = i;
            SetCharacter(playerData.ResearchExplorationDatas[dungeonID].ResearchPlayerPosition);
            if (moveCoroutine != null)
                StopCoroutine(moveCoroutine);
            CreateEvents();
            //RenderExplorationLayout();
            RenderDungeonTilemap();
            textDungeonName.text = "   https://" + dungeonLayouts[dungeonID].DungeonName + ".com";
            ExploreTile(playerData.ResearchExplorationDatas[dungeonID].ResearchPlayerPosition, false);
        }

        private void CreateEvents()
        {
            DestroyResearchEventList();
            if (dungeonLayouts[dungeonID].ResearchEventLayout == null)
                return;
            ResearchEvent currentEvent;
            int[,] eventLayout = dungeonLayouts[dungeonID].ResearchEventLayout;
            currentEventLayout = new int[eventLayout.GetLength(0), eventLayout.GetLength(1)];

            for (int x = 0; x < eventLayout.GetLength(0); x++)
            {
                for (int y = 0; y < eventLayout.GetLength(1); y++)
                {
                    if (eventLayout[x, y] > 0)
                    {
                        currentEvent = researchEventDatabase.GetResearchEvent(eventLayout[x, y]);
                        if (currentEvent != null)
                        {
                            researchEventList.Add(new ResearchEventData(eventLayout[x, y], Instantiate(currentEvent, researchEventTransform)));
                            researchEventList[researchEventList.Count - 1].researchEvent.transform.localPosition += new Vector3(cellSize * x + (cellSize / 2f), cellSize * (y + 1), 0);
                            researchEventList[researchEventList.Count - 1].researchEvent.SetActive(CheckResearchEventInPlayerData(eventLayout[x, y]));
                        }
                    }
                    currentEventLayout[x, y] = eventLayout[x, y];
                }
            }
        }

        protected bool CheckResearchEventInPlayerData(int eventID)
        {
            for (int i = 0; i < playerData.ResearchEventSaves.Count; i++)
            {
                if (playerData.ResearchEventSaves[i].EventID == eventID)
                {
                    return playerData.ResearchEventSaves[i].EventActive;
                }
            }
            return false;
        }

        private void DestroyResearchEventList()
        {
            for (int i = 0; i < researchEventList.Count; i++)
            {
                Destroy(researchEventList[i].researchEvent.gameObject);
            }
            researchEventList.Clear();
        }

        private void RenderDungeonTilemap()
        {
            groundTilemap.ClearAllTiles();
            explorationTilemap.ClearAllTiles();
            playerData.ResearchExplorationDatas[dungeonID].ResearchExplorationCount = 0;

            for (int x = 0; x < playerData.ResearchExplorationDatas[dungeonID].ResearchExplorationLayout.GetLength(0); x++)
            {
                for (int y = 0; y < playerData.ResearchExplorationDatas[dungeonID].ResearchExplorationLayout.GetLength(1); y++)
                {
                    if (playerData.ResearchExplorationDatas[dungeonID].ResearchExplorationLayout[x, y] == 2)
                    {
                        groundTilemap.SetTile(new Vector3Int(x, y, 0), dungeonLayouts[dungeonID].GetCorrespondingTile(x, y));
                        playerData.ResearchExplorationDatas[dungeonID].ResearchExplorationCount += 1;
                    }
                    else if (playerData.ResearchExplorationDatas[dungeonID].ResearchExplorationLayout[x, y] == 1)
                    {
                        explorationTilemap.SetTile(new Vector3Int(x, y, 0), dungeonLayouts[dungeonID].GetCorrespondingTile(x, y));
                    }
                    /*else if (dungeonLayouts[dungeonID].ResearchDungeonLayout[x, y] != 0)
                    {
                        explorationTilemap.SetTile(new Vector3Int(x, y, 0), dungeonLayouts[dungeonID].GetCorrespondingTile(x, y));
                    }*/
                    else
                    {
                        groundTilemap.SetTile(new Vector3Int(x, y, 0), null);
                        explorationTilemap.SetTile(new Vector3Int(x, y, 0), null);
                    }
                }
            }
            int percentage = (int)(((float)playerData.ResearchExplorationDatas[dungeonID].ResearchExplorationCount / playerData.ResearchExplorationDatas[dungeonID].ResearchExplorationTotal) * 100);
            explorationCount.text = percentage + "%";
        }

        // R E N D E R    S E C T I O N
        // ================================================================================







        private void OnEnable()
        {
            cameraStudio.SetActive(false);
            cameraTilemap.SetActive(true);

            menuRessourceResearch.DrawResearchPoint(playerData.ResearchPoint);
            SetDungeon(dungeonID);
        }

        public void QuitMenu()
        {
            cameraStudio.SetActive(true);
            cameraTilemap.SetActive(false);

            this.gameObject.SetActive(false);
        }

        public void ActivateInput(bool b)
        {
            inputController.gameObject.SetActive(b);
        }





        // ===============================================================================
        // M O V E M E N T    S E C T I O N

        public void SetCharacter(Vector2Int position)
        {
            character.transform.localPosition = new Vector3(cellSize * position.x, cellSize * position.y, 0);
            character.transform.localPosition += new Vector3(cellSize / 2f, cellSize / 2f, 0f);
        }

        public void Move(int direction)
        {
            if (moveCoroutine != null)
                return;
            switch(direction)
            {
                case 2:
                    StartCoroutine(Move(new Vector3(0, 0.48f, 0)));
                    break;
                case 4:
                    StartCoroutine(Move(new Vector3(-0.48f, 0, 0)));
                    break;
                case 6:
                    StartCoroutine(Move(new Vector3(0.48f, 0, 0)));
                    break;
                case 8:
                    StartCoroutine(Move(new Vector3(0, -0.48f, 0)));
                    break;
            }
        }



        public IEnumerator Move(Vector3 direction)
        {
            inputController.gameObject.SetActive(false);

            // Check si prochaine case est traversable
            Vector2Int directionNormalized = new Vector2Int((int)direction.normalized.x, (int)direction.normalized.y);
            Vector2Int playerPosition = playerData.ResearchExplorationDatas[dungeonID].ResearchPlayerPosition;
            ResearchEvent rsEvent = null;

            // Check si prochaine case est un event
            int caseEventID = currentEventLayout[playerPosition.x + directionNormalized.x, playerPosition.y + directionNormalized.y];
            if (caseEventID != 0)
            {
                rsEvent = GetFromResearchEventList(caseEventID);
                if (rsEvent.CanCollide() == true)
                {
                    yield return rsEvent.ApplyEvent(this, caseEventID);
                    ExploreTile(playerPosition + directionNormalized);
                    inputController.gameObject.SetActive(true);
                    yield break;
                }
            }

            // Check si la prochaine case n'est pas un mur
            if (dungeonLayouts[dungeonID].ResearchDungeonLayout[playerPosition.x + directionNormalized.x, playerPosition.y + directionNormalized.y] != 0)
            {
                if (playerData.ResearchPoint == 0 &&
                    playerData.ResearchExplorationDatas[dungeonID].ResearchExplorationLayout[playerPosition.x + directionNormalized.x, playerPosition.y + directionNormalized.y] < 2)
                {
                    inputController.gameObject.SetActive(true);
                    yield break;
                }
                else
                {
                    ExploreTile(playerPosition + directionNormalized, true);
                }

                // Deplace le perso
                moveCoroutine = MoveCoroutine(direction);
                playerData.ResearchExplorationDatas[dungeonID].ResearchPlayerPosition += directionNormalized;
            }

            yield return moveCoroutine;
            if (rsEvent != null)
                yield return rsEvent.ApplyEvent(this, caseEventID);
            moveCoroutine = null;
            inputController.gameObject.SetActive(true);
        }




        private void ExploreTile(Vector2Int position, bool researchLost = false)
        {
            // Check si la case n'est pas déjà exploré
            if (playerData.ResearchExplorationDatas[dungeonID].ResearchExplorationLayout[position.x, position.y] < 2)
            {
                groundTilemap.SetTile(new Vector3Int(position.x, position.y, 0), dungeonLayouts[dungeonID].GetCorrespondingTile(position.x, position.y));
                explorationTilemap.SetTile(new Vector3Int(position.x, position.y, 0), null);

                playerData.ResearchExplorationDatas[dungeonID].ResearchExplorationLayout[position.x, position.y] = 2;
                ExploreAdjacentTile(position + new Vector2Int(-1, 0));
                ExploreAdjacentTile(position + new Vector2Int(1, 0));
                ExploreAdjacentTile(position + new Vector2Int(0, -1));
                ExploreAdjacentTile(position + new Vector2Int(0, 1));
                if (researchLost == true)
                {
                    playerData.ResearchPoint -= 1;
                    menuRessourceResearch.DrawResearchPoint(playerData.ResearchPoint);
                    playerData.ResearchExplorationDatas[dungeonID].ResearchExplorationCount += 1;
                    int percentage = (int)(((float)playerData.ResearchExplorationDatas[dungeonID].ResearchExplorationCount / playerData.ResearchExplorationDatas[dungeonID].ResearchExplorationTotal) * 100);
                    explorationCount.text = percentage + "%";
                }
            }
        }

        private void ExploreAdjacentTile(Vector2Int position)
        {
            // Check si la case existe et n'a pas déjà été exploré
            if (playerData.ResearchExplorationDatas[dungeonID].ResearchExplorationLayout[position.x, position.y] == 0 && dungeonLayouts[dungeonID].ResearchDungeonLayout[position.x, position.y] != 0)
            {
                explorationTilemap.SetTile(new Vector3Int(position.x, position.y, 0), dungeonLayouts[dungeonID].GetCorrespondingTile(position.x, position.y));
                playerData.ResearchExplorationDatas[dungeonID].ResearchExplorationLayout[position.x, position.y] = 1;
            }
        }



        private IEnumerator MoveCoroutine(Vector3 destination)
        {
            Vector3 finalPosition = character.transform.localPosition + destination;
            Vector3 position = character.transform.localPosition;
            float t = 0f;
            while(t < 1f)
            {
                t += Time.deltaTime * (time / 60f);
                character.transform.localPosition = Vector3.Lerp(position, finalPosition, t);
                yield return null;
            }
        }


        public void SkipChest()
        {
            skipChest = true;
        }
        public IEnumerator DrawResearchText(ResearchData research)
        {
            animatorChest.gameObject.SetActive(true);
            animatorChest.SetBool("Appear", true);
            textResearchName.text = research.ResearchName;
            textResearchDescription.text = research.ResearchDescription;
            yield return new WaitForSeconds(0.6f);
            skipChest = false;
            while (skipChest == false)
            {
                yield return null;
            }
            skipChest = false;
            animatorChest.SetBool("Appear", false);
        }


        #endregion

    } 

} // #PROJECTNAME# namespace