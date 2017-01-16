using UnityEngine;
using System.Collections;

[RequireComponent(typeof(EnemyAI))]
public class Enemy : MonoBehaviour {

	[System.Serializable]
    public class EnemyStats
    {
        public int maxHealth = 100;
       // public float fStartPercentHealth = 1f;
        private int _curHealth;
        public int curHealth
        {
            get { return _curHealth; }
            set { _curHealth = Mathf.Clamp(value, 0, maxHealth); }
        }

        public int damage = 40;

        public void Init()
        {
            curHealth = maxHealth;
        }

    }

    public EnemyStats stats = new EnemyStats();

    public Transform deathParticles;
    public float shakeAmt = 0.1f;
    public float shakeLength = 0.1f;

    public string sEnemyDeathSoundName = "Explosion";
    public int nMoneyDrop = 10;

    [Header("Optional: ")]
    [SerializeField]
    private StatusIndicator statusIndicator;

    void Start()
    {
        stats.Init();
        if (statusIndicator != null)
        {
            statusIndicator.SetHealth(stats.curHealth, stats.maxHealth);
        }

        // Add method to delegate
        GameMaster.gm.onToggleUpgradeMenu += OnUpgradeMenuToggle;


        if(deathParticles == null)
        {
            Debug.LogError("No Death Particles on Enemey.");
        }

    }

    void OnUpgradeMenuToggle(bool bActive)
    {
        // Handle what happens when upgrade menu is toggled
        GetComponent<EnemyAI>().enabled = !bActive;

    }

    public void DamageEnemy(int nDamage)
    {
        stats.curHealth -= nDamage;
        if (stats.curHealth <= 0)
        {
            GameMaster.KillEnemy(this);
        }

        if (statusIndicator != null)
        {
            statusIndicator.SetHealth(stats.curHealth, stats.maxHealth);
        }
    }

    void OnCollisionEnter2D(Collision2D _colInfo)
    {
        Player _player = _colInfo.collider.GetComponent<Player>();

        if (_player != null)
        {
            _player.DamagePlayer(stats.damage);
            DamageEnemy(999999);
        }


    }

    void OnDestroy()
    {
        // Remove method to delegate when destroyed
        GameMaster.gm.onToggleUpgradeMenu -= OnUpgradeMenuToggle;
    }
}
