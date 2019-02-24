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

    [SerializeField]
    AudioSource voice = null;


    IEnumerator mouthCoroutine = null;


    protected void Start()
    {
        //ActivateMouth(speedMouth);
    }

    public void ActivateMouth(float speed = -1)
    {
        if(speed != -1)
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

        if(spriteRenderer != null)
            spriteRenderer.sprite = mouthmovement[0];

        if(soundVisualizer != null)
            soundVisualizer.StopVisualizer();
    }

    public void ShowMouth()
    {
        spriteRenderer.enabled = true;
    }

    public void HideMouth()
    {
        DesactivateMouth();
        spriteRenderer.enabled = false;
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
                if (soundVisualizer != null)
                    soundVisualizer.SetVisualizer();
            }
        }
    }

	public void changeMouthSprite(int index)
    {
        if (voice != null)
        {
            voice.pitch = Random.Range(0.95f, 1.05f);
            voice.Play();
        }
        if(index < mouthmovement.Length)
            spriteRenderer.sprite = mouthmovement[index];
	}

    public void ChangeOrderInLayer(int newOrder)
    {
        spriteRenderer.sortingOrder = newOrder;
    }

}
