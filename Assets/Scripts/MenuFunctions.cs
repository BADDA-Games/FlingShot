using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.ComponentModel;

public class MenuFunctions : MonoBehaviour
{
    //private AssetBundle myLoadedAssetBundle;
    //private string[] scenePaths;
    //private DataClass Data;
    private Color myColor;
    //public static event OnVariableChangeDelegate OnVariableChange;
    //public delegate void OnVariableChangeDelegate(Color newColor);
    public static int tempCounter = 0;
    //public GameObject S_Color_Dropdown;
    //public Dropdown S_Color_Dropdown;
    //public Canvas S_Canvas;
    //public Image panel;
    // Start is called before the first frame update
    void Start()
    {
        //Data = new DataClass();
        //myLoadedAssetBundle = AssetBundle.LoadFromFile("Assets/Scenes");
        //scenePaths = myLoadedAssetBundle.GetAllScenePaths();
        //TheColor = new Color(105, 224, 99, 255);
        //panel = S_Canvas.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        //panel.color = myColor;
    }

    public Color TheColor {
        get { return myColor; }
        set {
            //Debug.Log("SETTER: color: "+value.ToString());
            //Debug.Log(tempCounter.ToString());
            tempCounter++;
            if (myColor == value) { return; }
            myColor = value;
            //Data.TheColor = value;
        }
    }

    public void BackButtonSetting_Scene() {
        //TODO: Unload Setting scene
        //Debug.Log("")
        SceneManager.UnloadSceneAsync("Settings");
    }

    /*IEnumerator OpenSettings() {
        //TODO: Load Setting scene
        Debug.Log("Open Settings");
        yield return SceneManager.LoadSceneAsync("Settings", LoadSceneMode.Additive);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Settings"));
    }*/

    /*public static Color MyGetColor(string s) {
        if(s=="Red") {
            Debug.Log("Red");
            return new Color(226, 97, 97, 255);
        }
        else if(s=="Orange") {
            Debug.Log("Orange");
            return new Color(237, 151, 81, 255);
        }
        else if(s=="Yellow") {
            return new Color(229, 207, 83, 255);
        }
        else if(s=="Teal") {
            return new Color(99, 224, 220, 255);
        }
        else if(s=="Purple") {
            return new Color(172, 99, 224, 255);
        }
        else if(s=="Pink") {
            return new Color(239, 67, 202, 255);
        }
        else {
            Debug.Log("Green");
            return new Color(105, 224, 99, 255);
        }
    }*/

    /*public static void ChangeColor(string str) {
        //Text txt = S_Color_Dropdown.captionText;
        //string str = txt.text;
        TheColor = MyGetColor(str);
        //panel.color = myColor;
    }

    public static Color CurrentColor() {
        return myColor;
    }*/

}
