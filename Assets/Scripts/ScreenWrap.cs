using UnityEngine;

public class ScreenWrap : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private bool beenVisibleBefore = false;


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (beenVisibleBefore == false && spriteRenderer.isVisible)
        {
            beenVisibleBefore = true;
        }
        if (beenVisibleBefore == false)
        {
            return;
        }

        Vector2 screenPos = Camera.main.WorldToScreenPoint(transform.position);

        Vector2 newScreenPos = screenPos;

        if (screenPos.x < 0)
        {
            newScreenPos.x = Screen.width;
        }
        else if (screenPos.x > Screen.width)
        {
            newScreenPos.x = 0;
        }

        if (screenPos.y < 0)
        {
            newScreenPos.y = Screen.height;
        }
        else if (screenPos.y > Screen.height)
        {
            newScreenPos.y = 0;
        }
        
        //If updated screen position, apply to world position
        if (newScreenPos != screenPos)
        {
            Vector2 newWorldPos = Camera.main.ScreenToWorldPoint(newScreenPos);
            transform.position = newWorldPos;
        }
    }
}
