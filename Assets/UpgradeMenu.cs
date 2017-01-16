using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UpgradeMenu : MonoBehaviour {

    [SerializeField]
    private Text healthText;

    [SerializeField]
    private Text speedText;

    [SerializeField]
    private float healthMultiplier = 1.3f;

    [SerializeField]
    private float speedMultiplier = 1.2f;

    [SerializeField]
    private int nUpgradeCost = 50;

    private PlayerStats oStats;


    void OnEnable()
    {
        oStats = PlayerStats.instance;
        UpdateValues();
    }

    void UpdateValues()
    {
        healthText.text = "HEALTH: " + oStats.maxHealth.ToString();
        speedText.text = "SPEED: " + oStats.maxMovementSpeed.ToString();
    }


    public void UpgradeHealth()
    {
        if(GameMaster.nMoney < nUpgradeCost)
        {
            AudioManager.instance.PlaySound("NoMoney"); //Play No Money Sound
            return;
        }
        oStats.maxHealth = (int)(oStats.maxHealth * healthMultiplier);
        GameMaster.nMoney -= nUpgradeCost;
        AudioManager.instance.PlaySound("Money"); //Play Spent Money Sound
        UpdateValues();
    }

    public void UpgradeSpeed()
    {
        if (GameMaster.nMoney < nUpgradeCost)
        {
            AudioManager.instance.PlaySound("NoMoney"); //Play No Money Sound
            return;
        }

        oStats.maxMovementSpeed = Mathf.Round(oStats.maxMovementSpeed * speedMultiplier);
        GameMaster.nMoney -= nUpgradeCost;
        AudioManager.instance.PlaySound("Money"); //Play Spent Money Sound
        UpdateValues();
    }

	// Update is called once per frame
	void Update () {
	
	}
}
