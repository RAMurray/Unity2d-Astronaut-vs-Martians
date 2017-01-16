using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class MoneyCounterUI : MonoBehaviour
{

    private Text MoneyText;

    // Use this for initialization
    void Awake()
    {
        MoneyText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        MoneyText.text = "MONEY: " + GameMaster.nMoney.ToString();
    }
}
