using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    //Use a coroutine
    //Apply as VFX to any relevant event e.g. shoot, destroy asteroid, die, ...
    //Use Lerp?
    //--------------------------

    //Input: Duration, Intensity
    //Store original stats of camera

    //Do until exceed Duration:
    //- Pick a random direction
    //- Lerp in that direction/ Lerp scale based on the intensity
    //- Pick a new direction / scale camera

    //After exceed Duration:
    //- Reset camera

    Vector3 originalPos;
    Vector3 originalScale;

    private void Awake()
    {
        originalPos = transform.position;
        originalScale = transform.lossyScale;
        
    }
    public void ShakeCam(float duration, float intensity)
    {
        StartCoroutine(CameraShakeRoutine(duration, intensity));
    }

    IEnumerator CameraShakeRoutine(float duration, float intensity)
    {
        //Setup
        float timer = 0;
        Vector2 shakeDir;
        float changeX = 0;
        float changeY = 0;
        float stepDist = .5f;

        while (timer < duration)
        {
            //Pick random direction
            shakeDir.x = Random.Range(-intensity, intensity);
            shakeDir.y = Random.Range(-intensity, intensity);

            //Update Position
            changeX = Mathf.Lerp(transform.position.x, transform.position.x + shakeDir.x, stepDist);
            changeY = Mathf.Lerp(transform.position.y, transform.position.y + shakeDir.y, stepDist);

            //Debug.Log("X: " + transform.position.x + " Y: " + transform.position.y + " Z: " + transform.position.z);
            transform.position = new Vector3(changeX, changeY, transform.position.z);

            //Update Timer
            timer += Time.deltaTime;

            //Wait until end of the frame
            yield return new WaitForEndOfFrame();
        }
        //reset position
        transform.position = originalPos;
        
    }

}
