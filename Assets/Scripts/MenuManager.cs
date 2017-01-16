using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    [SerializeField]
    string hoverOverSound = "ButtonHover";

    [SerializeField]
    string pressButtonSound = "ButtonPress";

    AudioManager audioManager;

    void Start()
    {
        audioManager = AudioManager.instance;
        if(audioManager == null)
        {
            Debug.LogError("No audio manager found.");
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("MainGame");
        audioManager.PlaySound(pressButtonSound);
    }

    public void Quit()
    {
        Debug.Log("Quiting the game.");
        audioManager.PlaySound(pressButtonSound);
        Application.Quit();
    }
	
    public void OnMouseOver()
    {

        if(audioManager == null)
        {
            Debug.LogError("No audio manager found.");
            audioManager = AudioManager.instance;
        }
        else
        {
            audioManager.PlaySound(hoverOverSound);

        }
        
        ///Debug.Log("Mouse is over button.");

    }
}
