using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FeedbackSon : MonoBehaviour {

    [SerializeField]
    float intensityMin = 0.5f;
    [SerializeField]
    float intensityMax = 1f;
    [SerializeField]
	float speed = 0.02f;

    [SerializeField]
    List<RectTransform> visualizers;

    List<RectTransform> activeVisualizers = null;
    List<float> scaleTarget = null;

    private IEnumerator coroutine = null;


    private IEnumerator VisualizerCoroutine()
    {
        activeVisualizers = new List<RectTransform>(visualizers);
        scaleTarget = new List<float>(activeVisualizers.Count);

        for (int i = 0; i < activeVisualizers.Count; i++)
        {
            scaleTarget.Add(Random.Range(intensityMin, intensityMax));
        }

        while (activeVisualizers.Count != 0)
        {
            for (int i = 0; i < activeVisualizers.Count; i++)
            {
                if (scaleTarget[i] != 0)
                {
                    if (activeVisualizers[i].localScale.y < scaleTarget[i])
                    {
                        activeVisualizers[i].localScale += new Vector3(0, speed, 0);
                    }
                    else
                    {
                        scaleTarget[i] = 0;
                    }
                }
                else
                {
                    if (activeVisualizers[i].localScale.y > scaleTarget[i])
                    {
                        activeVisualizers[i].localScale -= new Vector3(0, speed, 0);
                    }
                    else
                    {
                        activeVisualizers.Remove(activeVisualizers[i]);
                        scaleTarget.Remove(scaleTarget[i]);
                    }
                }
            }
            yield return null;
        }
        coroutine = null;
    }


    public void SetVisualizer()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            scaleTarget.Clear();
        }
        coroutine = VisualizerCoroutine();
        StartCoroutine(coroutine);
    }

    public void StopVisualizer()
    {
        for (int i = 0; i < scaleTarget.Count; i++)
        {
            scaleTarget[i] = 0;
        }
    }



}
