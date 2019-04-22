using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPopUp : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Pop_StartPOS;
    public GameObject Pop_ActivePOS;
    public GameObject Screen_StartPOS;
    public GameObject Screen_ActivePOS;
    public GameObject Screen;
    public GameObject ResetPop;

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
        PlayerGameManager.UpdateHighScore(0);
        PlayerGameManager.UpdateLastScore(0);
        PlayerGameManager.UpdateTimesPlayed(0);
        Screen.transform.position = Screen_StartPOS.transform.position;
        ResetPop.transform.position = Pop_StartPOS.transform.position;
    }

    public void GoBack() {
        Screen.transform.position = Screen_StartPOS.transform.position;
        ResetPop.transform.position = Pop_StartPOS.transform.position;
    }
}
