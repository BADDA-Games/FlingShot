using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.ComponentModel;
using UnityEngine.UI;

public class bodycolor : MonoBehaviour
{
    // Start is called before the first frame update
    public Image PlayerBody;

    void Start()
    {
        PlayerBody.color = PlayerGameManager.GetColor();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
