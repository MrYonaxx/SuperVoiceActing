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
        List<Animator> researchEventList = new List<Animator>();

        [SerializeField]
        Tilemap explorationTilemap;
        [SerializeField]
        Tile tileUnexplored;

        [SerializeField]
        PlayerData playerData;
        [SerializeField]
        InputController inputController;


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

        private IEnumerator moveCoroutine;

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

        /*[Button("Test")]
        private void CreateExplorationLayout()
        {
            explorationLayout.dungeonLayout = new int[debugLayout.dungeonLayout.GetLength(0), debugLayout.dungeonLayout.GetLength(1)];
            for (int x = 0; x < debugLayout.dungeonLayout.GetLength(0); x++)
            {
                for (int y = 0; y < debugLayout.dungeonLayout.GetLength(1); y++)
                {
                    if (debugLayout.dungeonLayout[x, y] != 0)
                        explorationLayout.dungeonLayout[x, y] = 1;
                    else
                        explorationLayout.dungeonLayout[x, y] = 0;
                }
            }
        }

        [Button("Test2")]
        private void RenderExplorationLayout()
        {
            for (int x = 0; x < explorationLayout.dungeonLayout.GetLength(0); x++)
            {
                for (int y = 0; y < explorationLayout.dungeonLayout.GetLength(1); y++)
                {
                    if (explorationLayout.dungeonLayout[x, y] > 0)
                        explorationTilemap.SetTile(new Vector3Int(x, y, 0), tileUnexplored);
                }
            }
        }*/


        private void CreateEvents()
        {
            ResearchEvent currentEvent;
            int[,] eventLayout = dungeonLayouts[dungeonID].ResearchEventLayout;

            for (int x = 0; x < eventLayout.GetLength(0); x++)
            {
                for (int y = 0; y < eventLayout.GetLength(1); y++)
                {
                    if (eventLayout[x, y] > 0)
                    {
                        currentEvent = researchEventDatabase.GetResearchEvent(eventLayout[x, y]);
                        if(currentEvent != null)
                        {
                            if (currentEvent.CanInstantiate() == true)
                            {
                                researchEventList.Add(currentEvent.InstantiateEvent(researchEventTransform, cellSize, x, y));
                            }
                        }
                    }
                }
            }
            // Note les obstacles des interrupteurs doivent etre avant les switch dans la database 
        }

        private void OnEnable()
        {
            cameraStudio.SetActive(false);
            cameraTilemap.SetActive(true);

            SetCharacter(playerData.ResearchExplorationDatas[dungeonID].ResearchPlayerPosition);
            if (moveCoroutine != null)
                StopCoroutine(moveCoroutine);
            CreateEvents();
        }

        public void QuitMenu()
        {
            cameraStudio.SetActive(true);
            cameraTilemap.SetActive(false);

            this.gameObject.SetActive(false);
        }




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
                    Move(new Vector3(0, 0.48f, 0));
                    break;
                case 4:
                    Move(new Vector3(-0.48f, 0, 0));
                    break;
                case 6:
                    Move(new Vector3(0.48f, 0, 0));
                    break;
                case 8:
                    Move(new Vector3(0, -0.48f, 0));
                    break;

            }

        }

        public void Move(Vector3 direction)
        {
            // Check si prochaine case est traversable
            Vector2Int directionNormalized = new Vector2Int((int)direction.normalized.x, (int)direction.normalized.y);
            Vector2Int playerPosition = playerData.ResearchExplorationDatas[dungeonID].ResearchPlayerPosition;

            // Check si prochaine case est un event
            int caseEventID = dungeonLayouts[dungeonID].ResearchEventLayout[playerPosition.x + directionNormalized.x, playerPosition.y + directionNormalized.y];
            if (caseEventID != 0)
            {
                if (researchEventDatabase.GetResearchEvent(caseEventID) != null)
                {
                    // Active research event animator

                    // Active research event dans le playerData et Apply research event effect
                    researchEventDatabase.GetResearchEvent(caseEventID).ApplyEvent(playerData, caseEventID);
                }

                if (researchEventDatabase.GetCanCollide(caseEventID) == true)
                {
                    return;
                }
            }

            if (playerData.ResearchPoint == 0)
                return;

            // Check si la prochaine case n'est pas un mur
            if (dungeonLayouts[dungeonID].ResearchDungeonLayout[playerPosition.x + directionNormalized.x, playerPosition.y + directionNormalized.y] != 0)
            {
                moveCoroutine = MoveCoroutine(direction);
                StartCoroutine(moveCoroutine);

                // Deplace la position du perso dans le player data
                playerData.ResearchExplorationDatas[dungeonID].ResearchPlayerPosition += new Vector2Int(directionNormalized.x, directionNormalized.y);
                playerPosition = playerData.ResearchExplorationDatas[dungeonID].ResearchPlayerPosition;
            }


            // Ajoute la case decouverte au pourcentage
            if (playerData.ResearchExplorationDatas[dungeonID].ResearchExplorationLayout[playerPosition.x, playerPosition.y] > 0)
            {
                explorationTilemap.SetTile(new Vector3Int(playerPosition.x, playerPosition.y, 0), null);
                playerData.ResearchExplorationDatas[dungeonID].ResearchExplorationLayout[playerPosition.x, playerPosition.y] = -1;
                //playerData.ResearchPoint -= 1;
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
            moveCoroutine = null;
        }


        #endregion

    } 

} // #PROJECTNAME# namespace