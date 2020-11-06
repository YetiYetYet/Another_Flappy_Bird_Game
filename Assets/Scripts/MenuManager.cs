using Audio;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject settingsMenu;
    public GameObject creditMenu;

    public void StartGame()
    {
        AudioManager.Instance.Play("StartButton");
        SceneManager.LoadScene("Game");
    }

    public void SwapMenu(string menu)
    {
        DisableAllMenu();
        switch (menu)
        {
            case "Main" :
                mainMenu.SetActive(true);
                break;
            case "Settings" : 
                settingsMenu.SetActive(true);
                break;
            case "Credits" :
                creditMenu.SetActive(true);
                break;
            default:
                Debug.Log("Wrong Argument");
                break;
        }
        AudioManager.Instance.Play("BackButton");
    }

    public void SwitchAudio(string audioName)
    {
        AudioManager.Instance.Play("BackButton");
        switch (audioName)
        {
            case "Music":
                AudioManager.Instance.ToggleAudioMusic();
                break;
            case "Other":
                AudioManager.Instance.ToggleAudioSfx();
                AudioManager.Instance.ToggleAudioUi();
                break;
            default:
                Debug.LogError("audio name " + audioName + " not recognize");
                break;
        }
    }

    private void DisableAllMenu()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(false);
        creditMenu.SetActive(false);
    }
}
