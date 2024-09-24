using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("MainScene");  // Replace "GameScene" with the actual name of your game scene
    }

    public void OpenOptions()
    {
        SceneManager.LoadScene("Settings");  // Replace with the correct options scene
    }

    public void OpenLearningMode()
    {
        SceneManager.LoadScene("Learning Scene");  // Replace with the correct learning mode scene
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
