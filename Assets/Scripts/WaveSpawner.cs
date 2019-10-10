using UnityEngine;
using System.Collections;

public class WaveSpawner : MonoBehaviour {

    public enum SpawnState { Spawning, Waiting, Counting } ;

    [System.Serializable]
    public class Wave
    {
        public string Name;
        public Transform Enemy;
        public int Count;
        public float Rate;
    }

    public Wave[] Waves;
    private int nextWave = 0;
    public int NextWave
    {
        get { return nextWave + 1; }
    }

    public Transform[] spawnPoints;


    public float timeBetweenWaves = 3f;
    private float waveCountdown;
    public float WaveCountdown
    {
        get { return waveCountdown + 1; }
    }

    private float searchCountdown = 1f;

    private SpawnState oState = SpawnState.Counting;
    public SpawnState State
    {
        get { return oState; }

    }

    void Start()
    {
        if (spawnPoints.Length == 0)
        {
            Debug.LogError("No spawn points referenced.");
        }

        waveCountdown = timeBetweenWaves;
    }

    void Update()
    {
        if(oState == SpawnState.Waiting)
        {
            //Check if enemies are still alive
            if (!EnemyIsAlive())
            {
                // Begin a new Wave round.
                WaveCompleted();
            }
            else
            {
                return;
            }
        }

        if (waveCountdown <= 0)
        {
            if (oState != SpawnState.Spawning)
            {
                // StartCoroutine is how you call IEnumerator methods.
                StartCoroutine(SpawnWave(Waves[nextWave]));
            }
        }
        else
        {
            //Delta Time makes it time to actual time instead of Frames Per Second.
            waveCountdown -= Time.deltaTime;
        }
            
        
    }

    void WaveCompleted()
    {
        Debug.Log("Wave Completed.");

        oState = SpawnState.Counting;
        waveCountdown = timeBetweenWaves;

        if (nextWave + 1 > Waves.Length - 1)
        {
            nextWave = 0;
            Debug.Log("ALL WAVES COMPLETE! Looping...");
        }
        else
        {
            nextWave++;
        }

    }

    bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;

        if (searchCountdown <= 0f)
        {
            searchCountdown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }
        }

        return true;

    }

    IEnumerator SpawnWave(Wave _wave)
    {
        Debug.Log("Spwaning Wave: " + _wave.Name);
        oState = SpawnState.Spawning;

        for (int i = 0; i < _wave.Count; i++ )
        {
            SpawnEnemy(_wave.Enemy);
            //Can only use in IEnumerator methods.
            yield return new WaitForSeconds(1f / _wave.Rate);
        }

        oState = SpawnState.Waiting;

        // End IEnumerator methods with this.
        yield break;
    }

    void SpawnEnemy (Transform _enemy)
    {
        // Spawn Enemy
        Debug.Log("Spawning Enemy: " + _enemy.name);
        Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(_enemy, _sp.position, _sp.rotation);
    }
}
