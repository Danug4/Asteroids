using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenUI : MonoBehaviour
{
    public void ClickPlay()
    {
        SceneManager.LoadScene("Asteroids"); //This is the scene name
    }

    public void ClickPlayPlayground()
    {
        SceneManager.LoadScene("Playground");
    }

    public void ClickQuit()
    {
        Application.Quit();
    }

}
