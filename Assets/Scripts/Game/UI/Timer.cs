using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timeRemainingText;
    public float TimeRemaining { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        TimeRemaining = Constants.INITIAL_LEVEL_TIME;
    }

    // Update is called once per frame
    void Update()
    {
        TimeRemaining -= Time.deltaTime;
    }

    public void UpdateTimeText()
    {
        int remaining = (int)TimeRemaining;
        if(TimeRemaining < 0)
        {
            return;
        }
        if(0 <= remaining && remaining < 10)
        {
            timeRemainingText.text = "0" + remaining.ToString();
        }
        else
        {
            timeRemainingText.text = remaining.ToString();
        }
    }
}
