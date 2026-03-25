using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFlash : MonoBehaviour
{
    public float flashDuration = 0.22f;
    private Image flashImage;
    private Color imageColor;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        flashImage = GetComponent<Image>();
        imageColor = flashImage.color;
    }
    
    private IEnumerator FlashRoutine()
    {
        float timer = 0f;
        float t = 0f;
        float alphaFrom = .4f; //Partially Solid
        float alphaTo = 0f;   //Transparent

        while (t < 1f)
        {
            timer += Time.deltaTime;
            t = Mathf.Clamp01(timer / flashDuration);
            float alpha = Mathf.Lerp(alphaFrom, alphaTo, t);
            Color col = imageColor;
            col.a = alpha;
            flashImage.color = col;
            yield return new WaitForEndOfFrame();
        }

        
    }
    public void Flash()
    {
        StartCoroutine(FlashRoutine());
    }
}
