using UnityEngine;
using UnityEngine.UI;

public class StatusIndicator : MonoBehaviour {

	[SerializeField]
    private RectTransform healthBarRect;
    [SerializeField]
    private Text healthText;

    void Start()
    {
        if (healthBarRect == null)
        {
            Debug.LogError("STATUS INDICATOR: No health bar object referenced!");
        }
        if(healthText == null)
        {
            Debug.LogError("STATUS INDICATOR: No health text object referenced!");
        }

    }

    public void SetHealth(int nCur, int nMax)
    {
        float fValue = (float)nCur/nMax;

        healthBarRect.localScale = new Vector3(fValue, healthBarRect.localScale.y, healthBarRect.localScale.z);
        healthText.text = nCur + "/" + nMax + " HP";


    }

}
