using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Yeux : MonoBehaviour
{
    [SerializeField]
    bool forceStopBlink = false;
    [SerializeField]
    bool isRect = false;

    //int waitTime = 0;

    SpriteRenderer spriteRenderer = null;
    Image imageRenderer = null;

    IEnumerator coroutine = null;


    // -------------------------------------------------------------------
	// Use this for initialization
	void Start ()
    {
        if(isRect == true)
        {
            if (imageRenderer == null)
                imageRenderer = GetComponent<Image>();
        }
        else
        {
            if (spriteRenderer == null)
                spriteRenderer = GetComponent<SpriteRenderer>();
        }

        if (forceStopBlink == false)
        {
            coroutine = BlinkEye();
            StartCoroutine(coroutine);
        }
    }

    public void SetSprite(Sprite eyes)
    {
        if (isRect == true)
        {
            if (imageRenderer == null)
                imageRenderer = GetComponent<Image>();
            imageRenderer.sprite = eyes;
            imageRenderer.SetNativeSize();
        }
        else
        {
            if (spriteRenderer == null)
                spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = eyes;
        }
    }





    // -------------------------------------------------------------------
    public void StartBlink()
    {
        if(coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        coroutine = BlinkEye();
        StartCoroutine(coroutine);
    }

    public void StopBlink()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        coroutine = null;
    }

    private IEnumerator BlinkEye()
    {
        while(true)
        {
            if (isRect == true)
                imageRenderer.enabled = false;
            else
                spriteRenderer.enabled = false;

            yield return new WaitForSeconds(Random.Range(90, 600)/60f);

            if (isRect == true)
                imageRenderer.enabled = true;
            else
                spriteRenderer.enabled = true;

            yield return new WaitForSeconds(0.07f);
        }
    }





    // -------------------------------------------------------------------
    public void ChangeTint(Color newColor)
    {
        if (isRect == true)
        {
            if (imageRenderer == null)
                return;
            imageRenderer.color = newColor;
        }
        else
        {
            if (spriteRenderer == null)
                return;
            spriteRenderer.color = newColor;
        }

    }

    public void ChangeOrderInLayer(int newOrder)
    {
        if (isRect == true)
        {
            /*if (imageRenderer == null)
                imageRenderer = GetComponent<Image>();
            imageRenderer.sortingOrder = newOrder;*/
            return;
        }
        else
        {
            if (spriteRenderer == null)
                spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sortingOrder = newOrder;
        }
    }
}
