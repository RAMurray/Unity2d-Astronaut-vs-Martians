using UnityEngine;
using UnityEngine.UI;

public class WaveUI : MonoBehaviour {

    [SerializeField]
    WaveSpawner spawner;

    [SerializeField]
    Animator waveAnimator;

    [SerializeField]
    Text waveCountdownText;

    [SerializeField]
    Text waveCountText;

    private WaveSpawner.SpawnState previousState;

	// Use this for initialization
	void Start () {
	    if (spawner == null) {
            Debug.LogError("No spawner referenced!");
            this.enabled = false;
        }

        if (waveAnimator == null)
        {
            Debug.LogError("No waveAnimator referenced!");
            this.enabled = false;
        }

        if (waveCountdownText == null)
        {
            Debug.LogError("No waveCountdownText referenced!");
            this.enabled = false;
        }

        if (waveCountText == null)
        {
            Debug.LogError("No waveCountText referenced!");
            this.enabled = false;
        }
    }
	
	// Update is called once per frame
	void Update () {
        switch(spawner.State)
        {
            case WaveSpawner.SpawnState.Counting:
                UpdateCountingUI();
                break;
            case WaveSpawner.SpawnState.Spawning:
                UpdateSpawningUI();
                break;

        }
        previousState = spawner.State;
	
	}

    void UpdateCountingUI()
    {
        if(previousState != WaveSpawner.SpawnState.Counting)
        {
            //Debug.Log("Counting");
            waveAnimator.SetBool("bWaveIncoming", false);
            waveAnimator.SetBool("bWaveCountdown", true);

        }
        waveCountdownText.text = ((int)spawner.WaveCountdown).ToString();
    }

    void UpdateSpawningUI()
    {
        if (previousState != WaveSpawner.SpawnState.Spawning)
        {
            //Debug.Log("Spawning");
            waveAnimator.SetBool("bWaveIncoming", true);
            waveAnimator.SetBool("bWaveCountdown", false);

            waveCountText.text = spawner.NextWave.ToString();
        }
    }
}
