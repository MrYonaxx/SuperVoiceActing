using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yeux : MonoBehaviour
{
    [SerializeField]
    bool forceStopBlink = false;

	int waitTime = 0;
    SpriteRenderer spriteRenderer;
    IEnumerator coroutine = null;

	// Use this for initialization
	void Start ()
    {
        if(spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();
        if (forceStopBlink == false)
        {
            coroutine = BlinkEye();
            StartCoroutine(coroutine);
        }

    }

    public void SetSprite(Sprite eyes)
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = eyes;
    }

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
            waitTime -= 1;
            if (waitTime == 0)
            {
                spriteRenderer.enabled = true;
            }
            else if (waitTime == -4)
            {
                spriteRenderer.enabled = false;
                waitTime = Random.Range(90, 600);
            }
            yield return null;
        }
    }

    public void ChangeTint(Color newColor)
    {
        if (spriteRenderer == null)
            return;
        spriteRenderer.color = newColor;
    }

    public void ChangeOrderInLayer(int newOrder)
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sortingOrder = newOrder;
    }
}
