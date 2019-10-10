using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameOverUi : MonoBehaviour {

    [SerializeField]
    string mouseHoverSoundName = "ButtonHover";

    [SerializeField]
    string buttonPressSoundName = "ButtonPress";

    AudioManager audioManager;

    void Start()
    {
        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            Debug.LogError("No audio manager found.");
        }
    }

    public void Quit()
    {
        audioManager.PlaySound(buttonPressSoundName);
        Debug.Log("APPLICATION QUIT");
        Application.Quit();
    }

    public void Retry ()
    {
        audioManager.PlaySound(buttonPressSoundName);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnMouseOver()
    {
        audioManager.PlaySound(mouseHoverSoundName);
    }
}
