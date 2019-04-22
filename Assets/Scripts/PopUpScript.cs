using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpScript : MonoBehaviour
{
    public GameObject Pop_StartPOS;
    public GameObject Pop_ActivePOS;
    public GameObject Screen_StartPOS;
    public GameObject Screen_ActivePOS;
    public GameObject Screen;
    public GameObject Pop_Instr_Panel;
    public GameObject Pop_Rules_Panel;
    public GameObject Pop_About_Panel;


    // Start is called before the first frame update
    void Start()
    {
        Screen.transform.position = Screen_StartPOS.transform.position;
        Pop_Instr_Panel.transform.position = Pop_StartPOS.transform.position;
        Pop_Rules_Panel.transform.position = Pop_StartPOS.transform.position;
        Pop_About_Panel.transform.position = Pop_StartPOS.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InstPanelActive() {
        Screen.transform.position = Screen_ActivePOS.transform.position;
        Pop_Instr_Panel.transform.position = Pop_ActivePOS.transform.position;
    }

    public void InstPanelBack() {
        Screen.transform.position = Screen_StartPOS.transform.position;
        Pop_Instr_Panel.transform.position = Pop_StartPOS.transform.position;
    }

    public void RulesPanelActive() {
        Screen.transform.position = Screen_ActivePOS.transform.position;
        Pop_Rules_Panel.transform.position = Pop_ActivePOS.transform.position;
    }

    public void RulesPanelBack() {
        Screen.transform.position = Screen_StartPOS.transform.position;
        Pop_Rules_Panel.transform.position = Pop_StartPOS.transform.position;
    }

    public void AboutPanelActive() {
        Screen.transform.position = Screen_ActivePOS.transform.position;
        Pop_About_Panel.transform.position = Pop_ActivePOS.transform.position;
    }

    public void AboutPanelBack() {
        Screen.transform.position = Screen_StartPOS.transform.position;
        Pop_About_Panel.transform.position = Pop_StartPOS.transform.position;
    }
}
