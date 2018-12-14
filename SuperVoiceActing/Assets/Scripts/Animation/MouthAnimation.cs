/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MouthAnimation : MonoBehaviour
{


    [SerializeField]
    SpriteRenderer spriteRenderer;

    [SerializeField]
    float speedMouth = 5;

    [SerializeField]
	Sprite[] mouthmovement;

    [SerializeField]
    FeedbackSon soundVisualizer;


    IEnumerator mouthCoroutine = null;


    protected void Start()
    {
        ActivateMouth(speedMouth);
    }

    public void ActivateMouth(float speed = 12)
    {
        speedMouth = speed;
        if (mouthCoroutine != null)
            StopCoroutine(mouthCoroutine);
        mouthCoroutine = MouthAnim();
        StartCoroutine(mouthCoroutine);
    }

    public void DesactivateMouth()
    {
        if (mouthCoroutine != null)
            StopCoroutine(mouthCoroutine);
        spriteRenderer.sprite = mouthmovement[0];
        soundVisualizer.StopVisualizer();
    }

    private IEnumerator MouthAnim()
    {
        int i = 0;
        float speed = speedMouth;
        while (speed > 0)
        {
            speed -= 1;
            yield return null;
            if (speed == 0)
            {
                i += Random.Range(1,2);
                if (i >= mouthmovement.Length)
                    i = 0;
                changeMouthSprite(i);
                speed = speedMouth;
                soundVisualizer.SetVisualizer();
            }
        }
    }

	public void changeMouthSprite(int index)
    {
        spriteRenderer.sprite = mouthmovement[index];
	}
}
