using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Slider healthSlider;
    public Slider ammoSlider;

    SpaceShip spaceShip;

    public TMP_Text inGameScoreTextBox, scoreTextBox, hiscoreTextBox;
    public GameObject scorePanel, celebrate;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Hide();
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

    //------------------------------------------------------------------------------
    void Update()
    {
        if (spaceShip != null)
        {
            inGameScoreTextBox.text = spaceShip.score.ToString();
        }
    }

    public void Show(bool celebrateHiscore)
    {
        scoreTextBox.text = "Score: " + spaceShip.score.ToString();
        hiscoreTextBox.text = "High Score: " + spaceShip.GetHighScore().ToString();

        scorePanel.SetActive(true);
        celebrate.SetActive(celebrateHiscore);
    }

    public void Hide()
    {
        scorePanel.SetActive(false);
    }

    public void ClickedPlayAgain()
    {
        SceneManager.LoadScene("SurvivalGame");
    }

    public void ClickedMainMenu()
    {
        SceneManager.LoadScene("TitleScreen");
    }





}
