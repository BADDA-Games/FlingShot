using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.ComponentModel;

public class MenuFunctions : MonoBehaviour
{
    private Color myColor;
    public static int tempCounter = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public Color TheColor {
        get { return myColor; }
        set {
            tempCounter++;
            if (myColor == value) { return; }
            myColor = value;
        }
    }

    public void BackButtonSetting_Scene() {
        SceneManager.UnloadSceneAsync("Settings");
    }
}
