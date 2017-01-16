using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Platformer2DUserControl))]
public class Player : MonoBehaviour {

	public int fallBoundary = -20;
    public string sDeathSoundName = "PlayerDeathVoice";
    public string sDamageSoundName = "PlayerGrunt";

    AudioManager oAudioManager;

	[SerializeField]
	private StatusIndicator statusIndicator;

    private PlayerStats oStats;

	void Start()
	{
        oStats = PlayerStats.instance;

        oStats.curHealth = oStats.maxHealth;

		if (statusIndicator == null)
		{
			Debug.LogError("No status indicator referenced on Player");
		}
		else
		{
            statusIndicator.SetHealth(oStats.curHealth, oStats.maxHealth);
		}

        // Add method to delegate
        GameMaster.gm.onToggleUpgradeMenu += OnUpgradeMenuToggle;

        oAudioManager = AudioManager.instance;
        if (oAudioManager == null)
        {
            Debug.LogError("No audio manager found.");
        }

        InvokeRepeating("RegenHealth", 1f/oStats.healthRegenRate, 1f/oStats.healthRegenRate);
	}

	void Update () {
		if (transform.position.y <= fallBoundary)
			DamagePlayer (9999999);
	}

    void OnUpgradeMenuToggle (bool bActive)
    {
        // Handle what happens when upgrade menu is toggled
        GetComponent<Platformer2DUserControl>().enabled = !bActive;
        Weapon _weapon = GetComponentInChildren<Weapon>();

        if (_weapon != null)
            _weapon.enabled = !bActive;

    }

    void RegenHealth()
    {
        oStats.curHealth += 1;
        statusIndicator.SetHealth(oStats.curHealth, oStats.maxHealth);

    }

	public void DamagePlayer (int damage) {
        oStats.curHealth -= damage;
        if (oStats.curHealth <= 0)
		{
            // Play death yell
            oAudioManager.PlaySound(sDeathSoundName);
            
            //Kill Player
            GameMaster.KillPlayer(this);
		}
        else
        {
            // Play Damage sound
            oAudioManager.PlaySound(sDamageSoundName);
        }

        statusIndicator.SetHealth(oStats.curHealth, oStats.maxHealth);
	}

    void OnDestroy()
    {
        // Remove method to delegate when destroyed
        GameMaster.gm.onToggleUpgradeMenu -= OnUpgradeMenuToggle;
    }

}