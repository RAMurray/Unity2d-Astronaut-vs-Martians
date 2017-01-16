using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class GameMaster : MonoBehaviour
{

    public static GameMaster gm;

    [SerializeField]
    private int maxLives = 3;
    private static int _remainingLives;
    public static int RemainingLives
    {
        get { return _remainingLives; }
    }

    [SerializeField]
    private int nStartingMoney;
    public static int nMoney;
    
    void Awake()
    {
        if (gm == null)
        {
            gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        }
    }

    public Transform playerPrefab;
    public Transform spawnPoint;
    public float spawnDelay = 2;
    public Transform spawnPrefab;
    public string respawnSoundName = "RespawnCountdown";
    public string spawnSoundName = "Spawn";
    public string gameOverSoundName = "GameOver";

    public CameraShake cameraShake;

    [SerializeField]
    private GameObject gameOverUI;

    [SerializeField]
    private GameObject upgradeMenu;

    [SerializeField]
    private WaveSpawner oWaveSpawner;

    public delegate void UpgradeMenuCallback(bool bActive);
    public UpgradeMenuCallback onToggleUpgradeMenu;

    //Cache
    AudioManager oAudioManager;

    void Start()
    {
        if (cameraShake == null)
        {
            Debug.LogError("No camera shake referenced in GameMaster.");
        }

        _remainingLives = maxLives;
        nMoney = nStartingMoney;

        //chacing
        oAudioManager = AudioManager.instance;
        if(oAudioManager == null)
        {
            Debug.LogError("FREAK OUT! NO AudioManager found in scene. AHHH HELP ME BRACKEYS!!");
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.U))
        {
            ToggleUpgradeMenu();
        }
    }

    private void ToggleUpgradeMenu()
    {
        upgradeMenu.SetActive(!upgradeMenu.activeSelf);
        oWaveSpawner.enabled = !upgradeMenu.activeSelf;
        onToggleUpgradeMenu.Invoke(upgradeMenu.activeSelf);
    }

    public void EndGame()
    {
        oAudioManager.PlaySound(gameOverSoundName);
        Debug.Log("Game Over MAN!");
        gameOverUI.SetActive(true);
    }

    public IEnumerator _RespawnPlayer()
    {
        //GetComponent<AudioSource>().Play();
        oAudioManager.PlaySound(respawnSoundName);
        yield return new WaitForSeconds(spawnDelay);

        oAudioManager.PlaySound(spawnSoundName);
        Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
        GameObject clone = Instantiate(spawnPrefab, spawnPoint.position, spawnPoint.rotation) as GameObject;
        Destroy(clone, 3f);
    }

    public static void KillPlayer(Player player)
    {
        Destroy(player.gameObject);
        _remainingLives -= 1;
        if(_remainingLives <= 0)
        {
            gm.EndGame();
        }
        else {
            gm.StartCoroutine(gm._RespawnPlayer());
        }

    }

    public static void KillEnemy(Enemy enemy)
    {
        gm._KillEnemy(enemy);
    }

    public void _KillEnemy(Enemy _enemy)
    {
        // Explosion Sound effect
        oAudioManager.PlaySound(_enemy.sEnemyDeathSoundName);

        nMoney += _enemy.nMoneyDrop;
        oAudioManager.PlaySound("Money"); // Play that money sound

        // Add particles
        GameObject _clone = Instantiate(_enemy.deathParticles, _enemy.transform.position, Quaternion.identity) as GameObject;
        Destroy(_clone, 5f);

        // Camera Shake
        cameraShake.Shake(_enemy.shakeAmt, _enemy.shakeLength);
        Destroy(_enemy.gameObject);
    }

}
