using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    public Text currentLevelText;
    public Timer timer;

    void Awake()
    {
        GameVariables.Reset(GameType.Puzzle);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateLevelText()
    {
        currentLevelText.text = GameVariables.CurrentLevel < 10 ?
            "0" + GameVariables.CurrentLevel.ToString() :
            GameVariables.CurrentLevel.ToString();
        timer.ResetLevelTime();
    }

    public void AdvanceLevel()
    {
        GameVariables.CurrentLevel++;
        UpdateLevelText();
    }
}
