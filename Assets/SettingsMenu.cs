using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    public AudioSource musicSource;  // The AudioSource that plays the background music
    public Button musicToggleButton;  // The button to toggle music
    private bool isMusicOn;

    void Start()
    {
        // Load the saved music state (1 = enabled, 0 = disabled)
        isMusicOn = PlayerPrefs.GetInt("musicEnabled", 1) == 1;
        musicSource.mute = !isMusicOn;
        UpdateButtonLabel();

        // Add listener to the button
        musicToggleButton.onClick.AddListener(ToggleMusic);
    }

    // Method to enable or disable the music
    public void ToggleMusic()
    {
        isMusicOn = !isMusicOn;  // Flip the music state
        musicSource.mute = !isMusicOn;  // Mute or unmute the music

        // Save the music state (1 = enabled, 0 = disabled)
        PlayerPrefs.SetInt("musicEnabled", isMusicOn ? 1 : 0);

        // Update the button label
        UpdateButtonLabel();
    }

    // Method to update the button label text
    private void UpdateButtonLabel()
    {
        if (isMusicOn)
        {
            musicToggleButton.GetComponentInChildren<Text>().text = "Disable Music";
        }
        else
        {
            musicToggleButton.GetComponentInChildren<Text>().text = "Enable Music";
        }
    }

    // Method to return to the main menu
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");  // Replace with your main menu scene name
    }
}
