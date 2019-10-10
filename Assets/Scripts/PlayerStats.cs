using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour {

    public static PlayerStats instance;

    public int maxHealth = 100;

    private int _curHealth;
    public int curHealth
    {
        get { return _curHealth; }
        set { _curHealth = Mathf.Clamp(value, 0, maxHealth); }
    }

    public float healthRegenRate = 2f;

    public float maxMovementSpeed = 10f;

    public bool LaserRifleUnlocked;

    public Sprite LaserRifleSprite;

    public void Awake()
    {
       if(instance == null)
       {
           instance = this;
       }

       curHealth = maxHealth;
    }
    

}
