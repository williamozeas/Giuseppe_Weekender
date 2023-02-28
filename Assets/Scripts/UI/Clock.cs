using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Clock : MonoBehaviour
{
    //TEMPORARY FOR TEXTBOX
    public TextMeshProUGUI clockText;
    //public gameObject clock;

    public int hitTime = 5;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        int time = ((int)Time.time);

        //TEMPORARY FOR TEXTBOX, FIX WHEN UI GRAPHIC ADDED
        clockText.text = time.ToString();

        if (time == hitTime) {
            //Camera shake
        }

    }
}
