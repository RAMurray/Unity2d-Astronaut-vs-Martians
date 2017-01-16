using UnityEngine;

[System.Serializable]
public class Sound 
{
    
    public string Name;
    public AudioClip Clip;

    [Range(0f, 1f)]
    public float Volume = 0.7f;
    [Range(0.5f, 1.5f)]
    public float Pitch = 1f;

    [Range(0f, 0.5f)]
    public float randomVolume = 0.1f;
    [Range(0f, 0.5f)]
    public float randomPitch = 0.1f;

    public bool Loop = false;

    private AudioSource Source;

    public void SetSource(AudioSource oSource)
    {
        Source = oSource;
        Source.clip = Clip;
        Source.loop = Loop;
    }

    public void Play()
    {
        Source.volume = Volume * (1 + Random.Range(-randomVolume / 2f, randomVolume / 2f));
        Source.pitch = Pitch * (1 + Random.Range(-randomPitch / 2f, randomPitch / 2f));
        Source.Play();
    }

    public void Stop()
    {
        Source.Stop();
    }

}

public class AudioManager : MonoBehaviour {

    public static AudioManager instance;

    [SerializeField]
    Sound[] SoundEfxs;

    void Awake()
    {
        if (instance != null)
        {
            //Debug.LogError("More than one AudioManager in the scene.");
            if(instance != this)
            {
                Destroy(this.gameObject);
            }
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }

    void Start ()
    {
        for(int i = 0; i < SoundEfxs.Length; i++)
        {
            GameObject oGame = new GameObject("Sound_" + i + "_" + SoundEfxs[i].Name);
            oGame.transform.SetParent(this.transform);
            SoundEfxs[i].SetSource (oGame.AddComponent<AudioSource>());
        }
        PlaySound("Music");

    }

    public void PlaySound (string oName)
    {
        for(int i = 0; i < SoundEfxs.Length; i++)
        {
            if (SoundEfxs[i].Name == oName)
            {
                SoundEfxs[i].Play();
                return;
            }
        }
        // No sound with name was found.
        Debug.LogWarning("AudioManager: Sound not found in list: " + oName);
    }

    public void StopSound(string oName)
    {
        for (int i = 0; i < SoundEfxs.Length; i++)
        {
            if (SoundEfxs[i].Name == oName)
            {
                SoundEfxs[i].Stop();
                return;
            }
        }
        // No sound with name was found.
        Debug.LogWarning("AudioManager: Sound not found in list: " + oName);
    }

}
