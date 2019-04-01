using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HamMenuMovement : MonoBehaviour
{

    public GameObject H_Menu_StartPOS;
    public GameObject H_Menu_ActivePOS;
    public GameObject H_Menu_Panel;

    public bool Move_H_Menu_Active;
    public bool Move_H_Menu_Back;

    public float Move_Speed=0.5f;
    public float tempMenuPos;

    // Start is called before the first frame update
    void Start()
    {
        H_Menu_Panel.transform.position = H_Menu_StartPOS.transform.position;
        //Move_H_Menu_Back = true;
        //Move_H_Menu_Active = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Move_H_Menu_Active) {
            Debug.Log("Move Active");
            H_Menu_Panel.transform.position = Vector3.Lerp(H_Menu_Panel.transform.position, H_Menu_ActivePOS.transform.position, Move_Speed = Time.deltaTime*20);
            if(H_Menu_Panel.transform.localPosition.x == tempMenuPos) {
                Move_H_Menu_Active = false;
                H_Menu_Panel.transform.position = H_Menu_ActivePOS.transform.position;
                tempMenuPos = -99999999999999.99f; //TODO: Why this number? double check this number
            }
            if(Move_H_Menu_Active) {
                tempMenuPos = H_Menu_Panel.transform.position.x;
            }
        }
        if(Move_H_Menu_Back) {
            H_Menu_Panel.transform.position = Vector3.Lerp(H_Menu_Panel.transform.position, H_Menu_StartPOS.transform.position, Move_Speed = Time.deltaTime*20);
            if(H_Menu_Panel.transform.localPosition.x == tempMenuPos) {
                Move_H_Menu_Back = false;
                H_Menu_Panel.transform.position = H_Menu_StartPOS.transform.position;
                tempMenuPos = -99999999999999.99f; //TODO: Why this number? double check this number
            }
            if(Move_H_Menu_Back) {
                tempMenuPos = H_Menu_Panel.transform.position.x;
            }
        }
    }

    public void MoveHMenuActive() {
        Move_H_Menu_Back = false;
        Move_H_Menu_Active = true;
    }

    public void MoveHMenuBack() {
        Move_H_Menu_Active = false;
        Move_H_Menu_Back = true;
    }
}
