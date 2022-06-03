using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashScreen : MonoBehaviour
{
    public CanvasGroup canvas;

    private void Update()
    {
        if(canvas.alpha > 0)
        {
            canvas.alpha -= Time.deltaTime;
        }
        
    }
}
