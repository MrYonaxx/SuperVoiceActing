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
    /// Definition of the TextJoyAppear class
    /// </summary>
    public class TextDisgustAppear : TextPerformanceAppear
    {
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */

        [SerializeField]
        protected float shake = 0.1f;

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

        protected override IEnumerator AnimateVertexColors()
        {

            // We force an update of the text object since it would only be updated at the end of the frame. Ie. before this code is executed on the first frame.
            // Alternatively, we could yield and wait until the end of the frame when the text object will be generated.

            textMeshPro.ForceMeshUpdate();

            TMP_TextInfo textInfo = textMeshPro.textInfo;

            Matrix4x4 matrix;

            int loopCount = 0;
            hasTextChanged = true;
            endLine = false;

            wordSelected = -1;
            // Create an Array which contains pre-computed Angle Ranges and Speeds for a bunch of characters.

            for (int i = 0; i < 1024; i++)
            {
                vertexAnim[i].damage = false;
                vertexAnim[i].selected = false;
                vertexAnim[i].offset = letterOffset * 2;
                vertexAnim[i].alpha = 0;
                vertexAnim[i].vertexPosition = Vector3.zero;
            }

            // Cache the vertex data of the text object as the Jitter FX is applied to the original position of the characters.
            cachedMeshInfo = textInfo.CopyMeshInfoVertexData();
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

            mouth.ActivateMouth();

            // ============   BOUCLE   =============== //
            while (true)
            {
                while (pauseText == true)
                    yield return null;

                textMeshPro.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
                // Get new copy of vertex data if the text has changed.
                if (hasTextChanged)
                {
                    // Update the copy of the vertex data for the text object.
                    textInfo = textMeshPro.textInfo;
                    cachedMeshInfo = textInfo.CopyMeshInfoVertexData();
                    hasTextChanged = false;
                }

                if (actualTime == letterInterval && characterCount < textInfo.characterCount)
                {
                    characterCount += 1;
                    actualTime = 0;
                }
                else if (characterCount < textInfo.characterCount)
                {
                    actualTime += 1;
                }

                if (characterCount == textInfo.characterCount)
                    mouth.DesactivateMouth();

                //int characterCount = textInfo.characterCount;

                // If No Characters then just yield and wait for some text to be added
                if (characterCount == 0)
                {
                    yield return new WaitForSeconds(0.25f);
                    continue;
                }

                if (size <= 0.15f)
                {
                    sizeRatio = 1;
                }
                else if (size >= 0.25f)
                {
                    sizeRatio = -1;
                }

                size += 0.01f * sizeRatio;
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

                    float angle = Mathf.SmoothStep(-shake, shake, Mathf.PingPong(loopCount / 25f * 0.5f, 1f));
                    float angle2 = Mathf.SmoothStep(-shake, shake, Mathf.PingPong(loopCount / 25f * 1f, 1f));
                    float angle3 = Mathf.SmoothStep(-shake, shake, Mathf.PingPong(loopCount / 25f * 0.75f, 1f));
                    float angle4 = Mathf.SmoothStep(-shake, shake, Mathf.PingPong(loopCount / 25f * 0.25f, 1f));
                    //Vector3 jitterOffset = new Vector3(Random.Range(-.25f, .25f), Random.Range(-.25f, .25f), 0);
                    //Vector3 position = new Vector3(Random.Range(-shake, shake), Random.Range(-shake, shake), 0);
                    Vector3 position = new Vector3(0, 0, 0);



                    if (vertexAnim[i].selected == true)
                    {

                        matrix = Matrix4x4.TRS(position, Quaternion.Euler(0, 0, angle), Vector3.one + new Vector3(size, size, size));
                    }
                    else
                    {
                        matrix = Matrix4x4.TRS(position, Quaternion.Euler(0, 0, angle), Vector3.one);
                    }
                    Matrix4x4 matrix2 = Matrix4x4.TRS(position, Quaternion.Euler(0, 0, angle2), Vector3.one);
                    Matrix4x4 matrix3 = Matrix4x4.TRS(position, Quaternion.Euler(0, 0, angle3), Vector3.one);
                    Matrix4x4 matrix4 = Matrix4x4.TRS(position, Quaternion.Euler(0, 0, angle4), Vector3.one);

                    destinationVertices[vertexIndex + 0] = matrix.MultiplyPoint3x4(destinationVertices[vertexIndex + 0]);
                    destinationVertices[vertexIndex + 1] = matrix2.MultiplyPoint3x4(destinationVertices[vertexIndex + 1]);
                    destinationVertices[vertexIndex + 2] = matrix3.MultiplyPoint3x4(destinationVertices[vertexIndex + 2]);
                    destinationVertices[vertexIndex + 3] = matrix4.MultiplyPoint3x4(destinationVertices[vertexIndex + 3]);

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

                    if (i == textInfo.characterCount - 1 && endLine == false)
                    {
                        if (particlesEndLine != null)
                        {
                            particlesEndLine.transform.localPosition = offset;
                            particlesEndLine.Play();
                        }
                        endLine = true;
                    }
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

    } // TextJoyAppear class

} // #PROJECTNAME# namespacePROJECTNAME# namespace