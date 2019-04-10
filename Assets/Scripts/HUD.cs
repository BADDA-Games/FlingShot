using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
  public Sprite[] HeartSprites;
  public Image HeartUI;
  public PlayerMovement player;

  void Start()
  {
        player = GameObject.FindGameObjectWithTag("player").GetComponent<PlayerMovement>();
  }

  void Update()
  {
        if(player.health >= 0)
        {
            HeartUI.sprite = HeartSprites[player.health];
        }
  }



}
