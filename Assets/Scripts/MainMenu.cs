using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LoadLevel(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void OpenHelp()
    {
        SceneManager.LoadScene(Scenes.Help);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
