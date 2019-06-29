using UnityEngine;
using System.Collections;

public class FPSDisplay : MonoBehaviour
{
    float deltaTime = 0.0f;

    void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
    }

    void OnGUI()
    {
        int w = Screen.width, h = Screen.height;

        GUIStyle style = new GUIStyle();

        Rect rect = new Rect(0, 0, w, h * 2 / 100);
        style.alignment = TextAnchor.UpperLeft;
        style.fontSize = h * 1 / 50;
        style.normal.textColor = new Color(1f, 1f, 0f, 1.0f);
        float msec = deltaTime * 1000.0f;
        float fps = 1.0f / deltaTime;
        string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
        GUI.Label(rect, text, style);


        //rect = new Rect(200, 0, w, h * 2 / 100);
        //GUI.Label(rect, "Build Alpha 0.01f", style);
        /*rect = new Rect(1000, 0, w, h * 2 / 100);
        GUI.Label(rect, "Ça fait trop professionnel", style);*/
    }
}