using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunkAttack : MonoBehaviour
{
    // public GameObject player;
    // public PlayerMovement p;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D col){
        if(col.gameObject.name == "player") {
          // Debug.Log("Die");
          // player.GetComponent<ParticleSystem>().Emit(25);
          // Destroy(col.gameObject, (float)0.5);

          Destroy(col.gameObject);
        }
    }
}
