using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal sealed class ActivateDisplays : MonoBehaviour
{
    [SerializeField] private GameObject _canvas2paint;

    void Awake()
    {
                Canvas theCanvas = _canvas2paint.GetComponent<Canvas>();
                RectTransform canvasTransform = theCanvas.GetComponent<RectTransform>();

                for (int i = 0; i < Display.displays.Length; i++)
                {
                    Resolution screenRes = Screen.currentResolution;

                    int screenWidth = screenRes.width;
                    int screenHeight = screenRes.height;

                    Display.displays[i].Activate(screenWidth, screenHeight, screenRes.refreshRate);
                    Debug.Log("Screen Width: " + screenWidth + " Screen Height: " + screenHeight);
                    if (i == 0)
                    {
                         // 0- means Display 1 (the main one)
                        float canvasWidth = canvasTransform.rect.width;
                        float canvasHeight = canvasTransform.rect.height;
                        Debug.Log("canvas Width: " + canvasWidth + " canvas Height: " + canvasHeight);

                        canvasWidth = (float)screenWidth;
                        canvasHeight = (float)screenHeight;
                        Debug.Log("new canvas Width: " + canvasWidth + " new canvas Height: " + canvasHeight);
                    }

                }

    }
}
