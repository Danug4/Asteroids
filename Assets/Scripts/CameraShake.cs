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
    void ShakeCam(float duration, float intensity)
    {
        StartCoroutine(CameraShakeRoutine(duration, intensity));
    }

    IEnumerator CameraShakeRoutine(float duration, float intensity)
    {
        yield return null;
    }

}
