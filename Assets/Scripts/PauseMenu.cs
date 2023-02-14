using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    public GameObject UI;
    public GameObject GameOverUI;
    public GameObject buttonsUI;
    public SceneFader sceneFader;
    public string menuSceneName = "MainMenu";

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyUp(KeyCode.P))
        {
            if (!GameOverUI.activeSelf)
                Toggle();
        }
    }

    public void Toggle()
    {
        UI.SetActive(!UI.activeSelf);
        buttonsUI.SetActive(!buttonsUI.activeSelf);
        if (UI.activeSelf)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void Retry()
    {
        Toggle();

        sceneFader.FadeTo(SceneManager.GetActiveScene().name);
    }

    public void Menu()
    {
        Toggle();
        sceneFader.FadeTo(menuSceneName);
    }
}
