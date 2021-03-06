﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetPopUp : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Pop_StartPOS;
    public GameObject Pop_ActivePOS;
    public GameObject Screen_StartPOS;
    public GameObject Screen_ActivePOS;
    public GameObject Screen;
    public GameObject ResetPop;
    public Dropdown DiffDrop;

    void Start()
    {
        Screen.transform.position = Screen_StartPOS.transform.position;
        ResetPop.transform.position = Pop_StartPOS.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AskToReset() {
        Screen.transform.position = Screen_ActivePOS.transform.position;
        ResetPop.transform.position = Pop_ActivePOS.transform.position;
    }

    public void Reset()
    {
        PlayerGameManager.UpdateColor("Green");
        PlayerGameManager.SetPHS(Difficulty.Easy, 0);
        PlayerGameManager.SetPHS(Difficulty.Medium, 0);
        PlayerGameManager.SetPHS(Difficulty.Hard, 0);
        PlayerGameManager.SetPHS(Difficulty.Puzzle, 0);
        PlayerGameManager.SetPLS(Difficulty.Easy, 0);
        PlayerGameManager.SetPLS(Difficulty.Medium, 0);
        PlayerGameManager.SetPLS(Difficulty.Hard, 0);
        PlayerGameManager.SetPLS(Difficulty.Puzzle, 0);
        PlayerGameManager.SetPTP(Difficulty.Easy, 0);
        PlayerGameManager.SetPTP(Difficulty.Medium, 0);
        PlayerGameManager.SetPTP(Difficulty.Hard, 0);
        PlayerGameManager.SetPTP(Difficulty.Puzzle, 0);
        PlayerGameManager.UpdateDifficulty(Difficulty.Easy);
        PlayerGameManager.SetCurrentSeedEndless(-1);
        PlayerGameManager.SetNextSeedEndless(-1);
        PlayerGameManager.SetInitialSeedEndless(-1);
        PlayerGameManager.SetCurrentLevelEndless(0);
        PlayerGameManager.SetCurrentLevelTimePlayedEndless(0);
        PlayerGameManager.SetTotalTimePlayedEndless(0);
        Screen.transform.position = Screen_StartPOS.transform.position;
        ResetPop.transform.position = Pop_StartPOS.transform.position;
        DiffDrop.value = 0; //Easy
    }

    public void GoBack() {
        Screen.transform.position = Screen_StartPOS.transform.position;
        ResetPop.transform.position = Pop_StartPOS.transform.position;
    }
}
