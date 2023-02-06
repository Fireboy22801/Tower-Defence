using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool GameIsOver;

    public GameObject gameOverUI;
    public GameObject buttonsUI;

    private void Start()
    {
        GameIsOver = false;
    }

    private void Update()
    {
        if (GameIsOver)
            return;

        if (PlayerStats.Lives <= 0)
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        Time.timeScale = 1;
        GameIsOver = true;
        buttonsUI.SetActive(false);
        gameOverUI.SetActive(true);
    }

    public void SwitchTimeSpeed()
    {
        if (Time.timeScale == 1)
            Time.timeScale = 3;
        else
            Time.timeScale = 1;
    }

}
