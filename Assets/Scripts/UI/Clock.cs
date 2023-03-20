using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// using TMPro;

public class Clock : MonoBehaviour
{
    public GameObject hand;
    string oldTime;
    public int hitTime = 5;
    // public TextMeshProUGUI clockText;
 
    // Start is called before the first frame update
    void Start()
    {

    }

    void Update() 
    {
        string time = ((int)GameManager.Instance.Time).ToString();
        int timeInt = ((int)GameManager.Instance.Time);

        if (time != oldTime)
            UpdateTimer();
        oldTime = time;

        // clockText.text = time.ToString();

        if (timeInt == hitTime) {
            //Camera shake
        }
    }

    void UpdateTimer() 
    {
        int time = ((int)GameManager.Instance.Time);
        iTween.RotateTo(hand, iTween.Hash("z", time * 12 * - 1, "time", 1, 
                        "easeType", "easeOutQuint"));
    }

}
