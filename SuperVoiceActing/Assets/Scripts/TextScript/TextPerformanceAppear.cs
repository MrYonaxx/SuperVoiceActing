﻿/*****************************************************************
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
        /*[Header("Mouth")]
        [SerializeField]*/
        protected CharacterDialogueController mouth = null;

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

        /*[Header("Feedback")]
        [SerializeField]*/
        protected ParticleSystem particlesEndLine = null;
        protected ParticleSystem particlesLineDead = null;
        protected ParticleSystem particlesLineDead2 = null;


        [SerializeField]
        protected Color32 damageColor = new Color32(255, 255, 0, 0);
        [SerializeField]
        protected Color32 undamageColor = new Color32(255, 255, 255, 0);


        protected TMP_Text textMeshPro;

        protected bool hasTextChanged = false;
        protected bool endLine = false;
        protected int coloredCharacterCount = 1;
        protected bool endDamage = false;
        protected bool pauseText = false;

        protected float actualTime = 0;

        protected int characterCount = 0;
        protected int loopCount = 0;

        protected VertexAnim[] vertexAnim = new VertexAnim[1024];
        protected TMP_TextInfo textInfo;
        protected TMP_MeshInfo[] cachedMeshInfo;

        protected float size = 0;
        protected float sizeRatio = 0;

        protected int wordSelected = -1;

        protected IEnumerator coroutine = null;

        /// <summary>
        /// Structure to hold pre-computed animation data.
        /// </summary>
        protected struct VertexAnim
        {
            public bool feedback;
            public bool damage;
            public bool selected;
            public float offset;
            public float alpha;
            public Vector3 vertexPosition;
            public Vector3 vertexScale;
            public float angle;
        }

        #endregion

        #region GettersSetters 

        /* ======================================== *\
         *           GETTERS AND SETTERS            *
        \* ======================================== */


        public int GetWordSelected()
        {
            return wordSelected;
        }

        public bool GetEndLine()
        {
            return endLine;
        }

        public bool GetEndDamage()
        {
            return endDamage;
        }

        public void SetPauseText(bool b)
        {
            pauseText = b;
            mouth.DesactivateMouth();
            if (characterCount < textMeshPro.text.Length && b == false)
                mouth.ActivateMouth();
        }

        public void SetParticle(ParticleSystem par, ParticleSystem par2, ParticleSystem par3)
        {
            particlesEndLine = par;
            particlesLineDead = par2;
            particlesLineDead2 = par3;
        }

        public void SetLetterSpeed(int newValue)
        {
            letterInterval = newValue;
        }



        #endregion

        #region Functions 

        /* ======================================== *\
         *                FUNCTIONS                 *
        \* ======================================== */

        protected void Awake()
        {
            textMeshPro = GetComponent<TMP_Text>();
        }

        public void Stop()
        {
            if (coroutine != null)
                StopCoroutine(coroutine);
        }

        public bool PrintAllText()
        {
            if (characterCount < textMeshPro.text.Length)
            {
                characterCount = textMeshPro.text.Length;
                return false;
            }
            return true;

        }

        public void NewMouthAnim(CharacterDialogueController newMouth)
        {
            if(mouth != null)
                mouth.DesactivateMouth();
            mouth = newMouth;
        }

        public void NewPhrase(string newText, int count = 1)
        {
            if (textMeshPro.text == newText)
                return;

            textMeshPro.text = newText;

            if (coroutine != null)
                StopCoroutine(coroutine);
            coroutine = AnimateVertexColors(count);
            StartCoroutine(coroutine);
            //ReprintText();
        }


        private void SetLetterTransparent()
        {

            for (int i = 0; i < textMeshPro.textInfo.characterCount; i++)
            {
                int vertexIndex = textMeshPro.textInfo.characterInfo[i].vertexIndex;

                int materialIndex = textMeshPro.textInfo.characterInfo[i].materialReferenceIndex;

                Color32[] newVertexColors = textMeshPro.textInfo.meshInfo[materialIndex].colors32;

                Color32 colorAlpha = new Color32(undamageColor.r, undamageColor.g, undamageColor.b, 0);
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
            if (coroutine != null)
                StopCoroutine(coroutine);
            coroutine = AnimateVertexColors();
            StartCoroutine(coroutine);
        }

        public void SetWordFeedback(int wordSelected)
        {
            if (wordSelected < 0 || wordSelected >= textMeshPro.textInfo.wordCount)
            {
                for (int i = 0; i < textMeshPro.textInfo.characterCount; i++)
                {
                    vertexAnim[i].feedback = false;
                }
                return;
            }
            int firstCharacter = textMeshPro.textInfo.wordInfo[wordSelected].firstCharacterIndex;
            int lastCharacter = textMeshPro.textInfo.wordInfo[wordSelected].lastCharacterIndex;
            for (int i = 0; i < textMeshPro.textInfo.characterCount; i++)
            {
                if (i < firstCharacter || lastCharacter < i)
                    vertexAnim[i].feedback = false;
                else
                    vertexAnim[i].feedback = true;
            }
            Debug.Log("Ham");
        }



        public void ApplyDamage(float percentage)
        {
            float damage = (textMeshPro.textInfo.characterCount) * (percentage / 100);
            for(int i = 0; i < textMeshPro.textInfo.characterCount; i++)
            {
                if(i < damage)
                    vertexAnim[i].damage = true;
                else
                    vertexAnim[i].damage = false;
            }
            CalculateDamageColor(percentage);
            //ReprintText();
        }

        public void CalculateDamageColor(float percentage)
        {
            coloredCharacterCount = (int) ((textMeshPro.textInfo.characterCount) * (percentage / 100));
            //Debug.Log("Colored Character Count : " + coloredCharacterCount);
            if (coloredCharacterCount <= 0)
            {
                coloredCharacterCount = 1;
            }
        }

        [ContextMenu("a")]
        public void SelectWord()
        {
            SelectWord(5);
        }

        public void SelectWordLeft()
        {
            wordSelected -= 1;
            if (wordSelected == -2)
            {
                wordSelected = textMeshPro.textInfo.wordCount - 1;
            }
            SelectWord(wordSelected);
        }

        public void SelectWordRight()
        {
            wordSelected += 1;
            if (wordSelected == textMeshPro.textInfo.wordCount + 1)
            {
                wordSelected = 0;
            }
            SelectWord(wordSelected);
        }

        public void SelectWord(int wordSelected)
        {
            if(wordSelected < 0 || wordSelected >= textMeshPro.textInfo.wordCount)
            {
                for (int i = 0; i < textMeshPro.textInfo.characterCount; i++)
                {
                    vertexAnim[i].selected = false;
                }
                return;
            }
            int firstCharacter = textMeshPro.textInfo.wordInfo[wordSelected].firstCharacterIndex;
            int lastCharacter = textMeshPro.textInfo.wordInfo[wordSelected].lastCharacterIndex;
            for (int i = 0; i < textMeshPro.textInfo.characterCount; i++)
            {
                if (i < firstCharacter || lastCharacter < i)
                    vertexAnim[i].selected = false;
                else
                {
                    vertexAnim[i].selected = true;
                }
                
            }
        }


        // Create an Array which contains pre-computed Angle Ranges and Speeds for a bunch of characters.
        protected virtual void InitializeVertex()
        {
            for (int i = 0; i < 255; i++)
            {
                vertexAnim[i].feedback = false;
                vertexAnim[i].damage = false;
                vertexAnim[i].selected = false;
                vertexAnim[i].offset = letterOffset;
                vertexAnim[i].alpha = 0;
            }
        }

        protected virtual Color32[] InitializeVertexColor()
        {
            Color32[] newVertexColors = null;
            for (int i = 0; i < textInfo.characterCount; i++)
            {
                int vertexIndex = textInfo.characterInfo[i].vertexIndex;

                int materialIndex = textInfo.characterInfo[i].materialReferenceIndex;

                newVertexColors = textInfo.meshInfo[materialIndex].colors32;

                Color32 colorAlpha = new Color32(undamageColor.r, undamageColor.g, undamageColor.b, 0);
                newVertexColors[vertexIndex + 0] = colorAlpha;
                newVertexColors[vertexIndex + 1] = colorAlpha;
                newVertexColors[vertexIndex + 2] = colorAlpha;
                newVertexColors[vertexIndex + 3] = colorAlpha;
            }
            return newVertexColors;
        }


        protected virtual IEnumerator AnimateVertexColors(int count = 1)
        {

            // We force an update of the text object since it would only be updated at the end of the frame. Ie. before this code is executed on the first frame.
            // Alternatively, we could yield and wait until the end of the frame when the text object will be generated.
            textMeshPro.ForceMeshUpdate();

            textInfo = textMeshPro.textInfo;
            Matrix4x4 matrix;

            loopCount = 0;

            hasTextChanged = true;
            endLine = false;
            endDamage = false;
            characterCount = count;
            wordSelected = -1;

            // Cache the vertex data of the text object as the Jitter FX is applied to the original position of the characters.
            cachedMeshInfo = textInfo.CopyMeshInfoVertexData();

            // Create an Array which contains pre-computed Angle Ranges and Speeds for a bunch of characters.
            InitializeVertex();
            Color32[] newVertexColors = InitializeVertexColor();

            if(mouth != null)
                mouth.ActivateMouth();

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

                if(characterCount == textInfo.characterCount)
                    mouth.DesactivateMouth();

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


                    // ====================== Position ====================== //
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

                    Vector3 position = ModifyPosition(i);
                    Vector3 eulerAngle = ModifyRotation(i);
                    Vector3 scale = ModifyScale(i);





                    if (vertexAnim[i].selected == true)
                    {
                        matrix = Matrix4x4.TRS(position, Quaternion.Euler(eulerAngle.x, eulerAngle.y, eulerAngle.z), scale + new Vector3(size, size, size));
                    }
                    else
                    {
                        matrix = Matrix4x4.TRS(position, Quaternion.Euler(eulerAngle.x, eulerAngle.y, eulerAngle.z), scale);
                    }

                    MoveVertices(destinationVertices, vertexIndex, matrix);

                    destinationVertices[vertexIndex + 0] += offset;
                    destinationVertices[vertexIndex + 1] += offset;
                    destinationVertices[vertexIndex + 2] += offset;
                    destinationVertices[vertexIndex + 3] += offset;
                    // ====================================================== //



                    // ====================== Couleur ====================== //
                    if (vertexAnim[i].alpha != 255)
                    {
                        vertexAnim[i].alpha += alphaSpeed;
                        if (vertexAnim[i].alpha > 255f)
                        {
                            vertexAnim[i].alpha = 255f;
                        }
                        newVertexColors = textInfo.meshInfo[materialIndex].colors32;

                        Color32 colorAlpha = ModifyVertexColor(i);

                        newVertexColors[vertexIndex + 0] = colorAlpha;
                        newVertexColors[vertexIndex + 1] = colorAlpha;
                        newVertexColors[vertexIndex + 2] = colorAlpha;
                        newVertexColors[vertexIndex + 3] = colorAlpha;
                    }
                    // ====================================================== //



                    // ====================== Particle ====================== //
                    // Bug si coloredCharacterCount est un espace donc je check sur 2i mais ça peut ne pas régler à 100% le problème
                    if ((i == coloredCharacterCount-1 || i == coloredCharacterCount) && endDamage == false)
                    {
                        //Debug.Log("Hey");
                        endDamage = true;
                    }

                    if (i == textInfo.characterCount - 1 && endLine == false)
                    {
                        PlayParticle(offset, vertexAnim[i].damage);
                        endLine = true;
                    }
                    // ====================================================== //
                    if(i == characterCount-1 && vertexAnim[i].feedback == true)
                    {
                        if (particlesEndLine != null)
                        {
                            ParticleSystem par = Instantiate(particlesEndLine, this.transform);
                            par.transform.localPosition = offset;
                            var particleColor = par.main;
                            particleColor.startColor = (Color)damageColor;
                            par.Play();
                            vertexAnim[i].feedback = false;
                        }
                    }
                    //vertexAnim[i] = vertAnim;

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

        protected virtual void MoveVertices(Vector3[] destinationVertices, int vertexIndex, Matrix4x4 matrix)
        {
            destinationVertices[vertexIndex + 0] = matrix.MultiplyPoint3x4(destinationVertices[vertexIndex + 0]);
            destinationVertices[vertexIndex + 1] = matrix.MultiplyPoint3x4(destinationVertices[vertexIndex + 1]);
            destinationVertices[vertexIndex + 2] = matrix.MultiplyPoint3x4(destinationVertices[vertexIndex + 2]);
            destinationVertices[vertexIndex + 3] = matrix.MultiplyPoint3x4(destinationVertices[vertexIndex + 3]);
        }

        protected virtual Vector3 ModifyPosition(int vertexIndex)
        {
            Vector3 position = new Vector3(0, vertexAnim[vertexIndex].offset, 0);
            vertexAnim[vertexIndex].offset *= letterCurveSpeed;
            return position;
        }

        protected virtual Vector3 ModifyRotation(int vertexIndex)
        {
            Vector3 rotation = Vector3.zero;
            return rotation;
        }

        protected virtual Vector3 ModifyScale(int vertexIndex)
        {
            Vector3 scale = Vector3.one;
            return scale;
        }

        protected virtual Color32 ModifyVertexColor(int vertexIndex)
        {
            if (vertexAnim[vertexIndex].damage == true)
            {
                return new Color32(damageColor.r, damageColor.g, damageColor.b, (byte)vertexAnim[vertexIndex].alpha);
            }
            return new Color32(undamageColor.r, undamageColor.g, undamageColor.b, (byte)vertexAnim[vertexIndex].alpha);
        }



        protected virtual void PlayParticle(Vector3 offset, bool dead)
        {
            if(dead)
            {
                if (particlesLineDead != null)
                {
                    particlesLineDead.transform.localPosition = offset;
                    var particleColor = particlesLineDead.main;
                    particleColor.startColor = (Color) damageColor;
                    particlesLineDead.Play();
                }
                if (particlesLineDead2 != null)
                {
                    particlesLineDead2.transform.localPosition = offset;
                    var particleColor = particlesLineDead2.main;
                    particleColor.startColor = (Color) damageColor;
                    particlesLineDead2.Play();
                }
            }
            else if (particlesEndLine != null)
            {
                particlesEndLine.transform.localPosition = offset;
                particlesEndLine.Play();
            }
        }







        public void ExplodeLetter(float damage, float time)
        {
            StopCoroutine(coroutine);
            coroutine = ExplosionVertex(damage, time);
            StartCoroutine(coroutine);
            endLine = false;
            endDamage = false;
        }

        protected IEnumerator ExplosionVertex(float damage, float time)
        {
            yield return null;
            textMeshPro.ForceMeshUpdate();

            //textMeshPro.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
            textInfo = textMeshPro.textInfo;
            //Matrix4x4 matrix;
            TMP_MeshInfo[] cachedMeshInfo = textInfo.CopyMeshInfoVertexData();
            Vector3 scale = new Vector3(1f, 1f, 1f);

            float[] randomParameter = new float[characterCount];
            for (int i = 0; i < characterCount; i++)
            {
                randomParameter[i] = Random.Range(0.01f, 0.1f);//0.2f
            }
            
            float coef = 0;

            while (time != 0)
            {
                scale += new Vector3(0.001f, 0.001f, 0.001f);
                for (int i = 0; i < characterCount; i++)
                {
                    TMP_CharacterInfo charInfo = textInfo.characterInfo[i];
                    int materialIndex = textInfo.characterInfo[i].materialReferenceIndex;
                    int vertexIndex = textInfo.characterInfo[i].vertexIndex;

                    Vector3[] sourceVertices = cachedMeshInfo[materialIndex].vertices;
                    Vector2 charMidBasline = (sourceVertices[vertexIndex + 0] + sourceVertices[vertexIndex + 2]) / 2;
                        
                    Vector3 offset = charMidBasline;
                    offset *= randomParameter[i] + (coef);

                    Vector3[] destinationVertices = textInfo.meshInfo[materialIndex].vertices;

                    Matrix4x4 matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0, 0, 0), scale);
                    MoveVertices(destinationVertices, vertexIndex, matrix);

                    destinationVertices[vertexIndex + 0] += offset;
                    destinationVertices[vertexIndex + 1] += offset;
                    destinationVertices[vertexIndex + 2] += offset;
                    destinationVertices[vertexIndex + 3] += offset;


                }
                coef += 0.01f;
                // Push changes into meshes
                for (int i = 0; i < textInfo.meshInfo.Length; i++)
                {
                    textInfo.meshInfo[i].mesh.vertices = textInfo.meshInfo[i].vertices;
                    textMeshPro.UpdateGeometry(textInfo.meshInfo[i].mesh, i);
                }
                time -= 1;
                yield return null;
            }

            /*coroutine = AnimateVertexColors();
            StartCoroutine(coroutine);
            ApplyDamage(damage);*/

        }







        public void TextPop(int time)
        {
            StopCoroutine(coroutine);
            StartCoroutine(ExplosionVertex(100, time));
        }

        protected IEnumerator PopVertex()
        {
            for (int i = 0; i < characterCount; i++)
            {
                TMP_CharacterInfo charInfo = textInfo.characterInfo[i];
                int materialIndex = textInfo.characterInfo[i].materialReferenceIndex;
                int vertexIndex = textInfo.characterInfo[i].vertexIndex;

                Vector3[] sourceVertices = cachedMeshInfo[materialIndex].vertices;
                Vector2 charMidBasline = (sourceVertices[vertexIndex + 0] + sourceVertices[vertexIndex + 2]) / 2;
                Vector3 offset = charMidBasline;

                ParticleSystem par = Instantiate(particlesEndLine, this.transform);
                par.transform.localPosition = offset;
                par.Play();
                yield return null;
            }
        }




            #endregion

    } // TextPerformanceAppear class

} // #PROJECTNAME# namespace