using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.ComponentModel;
using UnityEngine.UI;

public class bodycolor : MonoBehaviour
{
    // Start is called before the first frame update
    public SpriteRenderer PlayerBody;
    public SpriteRenderer PlayerLid;
    public Image Hearts;
    public TrailRenderer tr;


    void Start()
    {
        Color color = PlayerGameManager.GetColor();
        tr = GetComponent<TrailRenderer>();

        PlayerBody.color = color;
        PlayerLid.color = color;
        Hearts.color = color;
        tr.startColor = color;

        // PlayerBody.color = PlayerGameManager.GetColor();
        // Debug.Log(PlayerGameManager.GetColor());
        // Debug.Log(hit.collider.name)
    }

    // Update is called once per frame
    void Update()
    {

    }
}
