using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Slider healthSlider;
    public Slider ammoSlider;

    SpaceShip spaceShip;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spaceShip = FindFirstObjectByType<SpaceShip>().GetComponent<SpaceShip>();
    }
    public void SetupSliders(float healthMaxValue, float healthStartValue, float ammoMaxValue, float ammoStartValue)
    {
        //Setup Health Slider Values
        healthSlider.maxValue = healthMaxValue;
        healthSlider.value = healthStartValue;

        ammoSlider.maxValue = ammoMaxValue;
        ammoSlider.value = ammoStartValue;
    }
    public void UpdateHealthSlider(float value)
    {
        healthSlider.value = value;
    }
    public void UpdateAmmoSlider(float value)
    {
        ammoSlider.value = value;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
