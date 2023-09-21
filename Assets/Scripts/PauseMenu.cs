using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private Transform pausePanel;

    public void Awake()
    {
        pausePanel = transform.Find("PausePanel");
    }

    public void Pause()
    {
        Time.timeScale = 0;
        pausePanel.gameObject.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1;
        pausePanel.gameObject.SetActive(false);
    }

    public void Restart()
    {
        Resume();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
