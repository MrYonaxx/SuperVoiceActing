/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using UnityEngine;
using TMPro;
using System.Collections;

namespace VoiceActing

{
    /// <summary>
    /// Definition of the TextPerformanceAppear class
    /// </summary>
    public class TextPerformanceAppear : MonoBehaviour
    {
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */
        [Header("Mouth")]
        [SerializeField]
        MouthAnimation mouth = null;

        [Header("Letter Parameter")]
        [SerializeField]
        protected float letterOffset = 3;
        [SerializeField]
        protected float letterCurveSpeed = 0.8f;

        [Header("Letter Appearance")]
        [SerializeField]
        protected float letterInterval = 4f;
        [SerializeField]
        protected float alphaSpeed = 5f;


        private Color32 damageColor = new Color32(255, 255, 0, 0);


        private TMP_Text textMeshPro;

        bool hasTextChanged = false;

        float actualTime = 0;

        int characterCount = 0;

        VertexAnim[] vertexAnim = new VertexAnim[1024];

        #endregion

        #region GettersSetters 

        /* ======================================== *\
         *           GETTERS AND SETTERS            *
        \* ======================================== */

        /// <summary>
        /// Structure to hold pre-computed animation data.
        /// </summary>
        private struct VertexAnim
        {
            public bool damage;
            public bool selected;
            public float offset;
            public float alpha;
            //public Color32 
        }




        void Awake()
        {
            textMeshPro = GetComponent<TMP_Text>();
            
        }


        void Start()
        {
            StartCoroutine(AnimateVertexColors());
        }





        #endregion

        #region Functions 

        /* ======================================== *\
         *                FUNCTIONS                 *
        \* ======================================== */

        private void SetLetterTransparent()
        {

            for (int i = 0; i < textMeshPro.textInfo.characterCount; i++)
            {
                int vertexIndex = textMeshPro.textInfo.characterInfo[i].vertexIndex;

                int materialIndex = textMeshPro.textInfo.characterInfo[i].materialReferenceIndex;

                Color32[] newVertexColors = textMeshPro.textInfo.meshInfo[materialIndex].colors32;

                Color32 colorAlpha = new Color32(255, 255, 255, 0);
                newVertexColors[vertexIndex + 0] = colorAlpha;
                newVertexColors[vertexIndex + 1] = colorAlpha;
                newVertexColors[vertexIndex + 2] = colorAlpha;
                newVertexColors[vertexIndex + 3] = colorAlpha;

                textMeshPro.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
            }
        }





        [ContextMenu("Reset")]
        public void ReprintText()
        {
            // ================================
            TMP_TextInfo textInfo = textMeshPro.textInfo;
            Color32[] newVertexColors;
            for (int i = 0; i < textInfo.characterCount; i++)
            {
                int vertexIndex = textInfo.characterInfo[i].vertexIndex;

                int materialIndex = textInfo.characterInfo[i].materialReferenceIndex;

                newVertexColors = textInfo.meshInfo[materialIndex].colors32;
                Color32 colorAlpha;
                if (vertexAnim[i].damage == true)
                {
                    colorAlpha = damageColor;
                }
                else
                {
                    colorAlpha = new Color32(255, 255, 255, 0);
                }

                newVertexColors[vertexIndex + 0] = colorAlpha;
                newVertexColors[vertexIndex + 1] = colorAlpha;
                newVertexColors[vertexIndex + 2] = colorAlpha;
                newVertexColors[vertexIndex + 3] = colorAlpha;

                vertexAnim[i].offset = letterOffset;
                vertexAnim[i].alpha = 0;

            }
            mouth.ActivateMouth();
            characterCount = 1;
            actualTime = 0;
            // ================================
        }





        public void ApplyDamage(float percentage)
        {
            float damage = (textMeshPro.textInfo.characterCount-1) * (percentage / 100);
            for(int i = 0; i < textMeshPro.textInfo.characterCount-1; i++)
            {
                if(i < damage)
                    vertexAnim[i].damage = true;
                else
                    vertexAnim[i].damage = false;
            }
            ReprintText();
        }

        public void SelectWord(int wordSelected)
        {
            int firstCharacter = textMeshPro.textInfo.wordInfo[wordSelected].firstCharacterIndex;
            for (int i = firstCharacter; i < textMeshPro.textInfo.wordInfo[wordSelected].lastCharacterIndex+1; i++)
            {
                vertexAnim[i].damage = true;
                vertexAnim[i].alpha = 254;
            }
            //ReprintText();
        }



        IEnumerator AnimateVertexColors()
        {

            // We force an update of the text object since it would only be updated at the end of the frame. Ie. before this code is executed on the first frame.
            // Alternatively, we could yield and wait until the end of the frame when the text object will be generated.

            textMeshPro.ForceMeshUpdate();

            TMP_TextInfo textInfo = textMeshPro.textInfo;

            Matrix4x4 matrix;

            int loopCount = 0;
            hasTextChanged = true;

            // Create an Array which contains pre-computed Angle Ranges and Speeds for a bunch of characters.
            
            for (int i = 0; i < 1024; i++)
            {
                vertexAnim[i].damage = false;
                vertexAnim[i].offset = letterOffset;
                vertexAnim[i].alpha = 0;
            }

            // Cache the vertex data of the text object as the Jitter FX is applied to the original position of the characters.
            TMP_MeshInfo[] cachedMeshInfo = textInfo.CopyMeshInfoVertexData();
            characterCount = 1;
            Color32[] newVertexColors;




            for (int i = 0; i < textInfo.characterCount; i++)
            {
                int vertexIndex = textInfo.characterInfo[i].vertexIndex;

                int materialIndex = textInfo.characterInfo[i].materialReferenceIndex;

                newVertexColors = textInfo.meshInfo[materialIndex].colors32;

                Color32 colorAlpha = new Color32(255, 255, 255, 0);
                newVertexColors[vertexIndex + 0] = colorAlpha;
                newVertexColors[vertexIndex + 1] = colorAlpha;
                newVertexColors[vertexIndex + 2] = colorAlpha;
                newVertexColors[vertexIndex + 3] = colorAlpha;

            }




            while (true)
            {
                textMeshPro.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
                // Get new copy of vertex data if the text has changed.
                if (hasTextChanged)
                {
                    // Update the copy of the vertex data for the text object.
                    cachedMeshInfo = textInfo.CopyMeshInfoVertexData();

                    hasTextChanged = false;
                }


                actualTime += 1;
                if (actualTime == letterInterval && characterCount < textInfo.characterCount)
                {
                    characterCount += 1;
                    actualTime = 0;
                }

                if(characterCount == textInfo.characterCount)
                    mouth.DesactivateMouth();

                //int characterCount = textInfo.characterCount;

                // If No Characters then just yield and wait for some text to be added
                if (characterCount == 0)
                {
                    yield return new WaitForSeconds(0.25f);
                    continue;
                }


                // =======================================================

                for (int i = 0; i < characterCount; i++)
                {
                    TMP_CharacterInfo charInfo = textInfo.characterInfo[i];

                    // Skip characters that are not visible and thus have no geometry to manipulate.
                    if (!charInfo.isVisible)
                        continue;

                    // Retrieve the pre-computed animation data for the given character.
                    VertexAnim vertAnim = vertexAnim[i];

                    // Get the index of the material used by the current character.
                    int materialIndex = textInfo.characterInfo[i].materialReferenceIndex;

                    // Get the index of the first vertex used by this text element.
                    int vertexIndex = textInfo.characterInfo[i].vertexIndex;

                    // Get the cached vertices of the mesh used by this text element (character or sprite).
                    Vector3[] sourceVertices = cachedMeshInfo[materialIndex].vertices;

                    // Determine the center point of each character at the baseline.
                    //Vector2 charMidBasline = new Vector2((sourceVertices[vertexIndex + 0].x + sourceVertices[vertexIndex + 2].x) / 2, charInfo.baseLine);

                    // Determine the center point of each character.
                    Vector2 charMidBasline = (sourceVertices[vertexIndex + 0] + sourceVertices[vertexIndex + 2]) / 2;

                    // Need to translate all 4 vertices of each quad to aligned with middle of character / baseline.
                    // This is needed so the matrix TRS is applied at the origin for each character.
                    Vector3 offset = charMidBasline;


                    Vector3[] destinationVertices = textInfo.meshInfo[materialIndex].vertices;

                    destinationVertices[vertexIndex + 0] = sourceVertices[vertexIndex + 0] - offset;
                    destinationVertices[vertexIndex + 1] = sourceVertices[vertexIndex + 1] - offset;
                    destinationVertices[vertexIndex + 2] = sourceVertices[vertexIndex + 2] - offset;
                    destinationVertices[vertexIndex + 3] = sourceVertices[vertexIndex + 3] - offset;

                    //vertAnim.angle = Mathf.SmoothStep(-vertAnim.angleRange, vertAnim.angleRange, Mathf.PingPong(loopCount / 25f * vertAnim.speed, 1f));
                    //Vector3 jitterOffset = new Vector3(Random.Range(-.25f, .25f), Random.Range(-.25f, .25f), 0);

                    Vector3 position = new Vector3(0, vertAnim.offset, 0);
                    matrix = Matrix4x4.TRS(position, Quaternion.identity, Vector3.one);

                    vertAnim.offset *= letterCurveSpeed;

                    destinationVertices[vertexIndex + 0] = matrix.MultiplyPoint3x4(destinationVertices[vertexIndex + 0]);
                    destinationVertices[vertexIndex + 1] = matrix.MultiplyPoint3x4(destinationVertices[vertexIndex + 1]);
                    destinationVertices[vertexIndex + 2] = matrix.MultiplyPoint3x4(destinationVertices[vertexIndex + 2]);
                    destinationVertices[vertexIndex + 3] = matrix.MultiplyPoint3x4(destinationVertices[vertexIndex + 3]);

                    destinationVertices[vertexIndex + 0] += offset;
                    destinationVertices[vertexIndex + 1] += offset;
                    destinationVertices[vertexIndex + 2] += offset;
                    destinationVertices[vertexIndex + 3] += offset;



                    // ====================== Couleur ====================== //
                    if (vertAnim.alpha != 255)
                    {
                        vertAnim.alpha += alphaSpeed;
                        if (vertAnim.alpha > 255f)
                        {
                            vertAnim.alpha = 255f;
                        }

                        newVertexColors = textInfo.meshInfo[materialIndex].colors32;
                        Color32 colorAlpha;
                        if (vertexAnim[i].damage == true)
                            colorAlpha = new Color32(damageColor.r, damageColor.g, damageColor.b, (byte)vertexAnim[i].alpha);
                        else
                            colorAlpha = new Color32(255, 255, 255, (byte)vertexAnim[i].alpha);
                        newVertexColors[vertexIndex + 0] = colorAlpha;
                        newVertexColors[vertexIndex + 1] = colorAlpha;
                        newVertexColors[vertexIndex + 2] = colorAlpha;
                        newVertexColors[vertexIndex + 3] = colorAlpha;
                    }

                    //textMeshPro.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
                    // ====================== Couleur ====================== //



                    vertexAnim[i] = vertAnim;
                }

                // Push changes into meshes
                for (int i = 0; i < textInfo.meshInfo.Length; i++)
                {
                    textInfo.meshInfo[i].mesh.vertices = textInfo.meshInfo[i].vertices;
                    textMeshPro.UpdateGeometry(textInfo.meshInfo[i].mesh, i);
                }

                loopCount += 1;

                yield return null;//new WaitForSeconds(0.1f);
            }
        }

        #endregion

    } // TextPerformanceAppear class

} // #PROJECTNAME# namespace