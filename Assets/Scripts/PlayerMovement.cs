using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // //public float speed;
    // private Rigidbody2D PlayerBody;
    //
    // KeyCode LeftArrow;
    // KeyCode RightArrow;
    // KeyCode UpArrow;
    // KeyCode DownArrow;
    //
    // private float speed;
    // private Vector3 pos;
    // private Transform tr;
    //
    // private void Start() {
    //   PlayerBody = GetComponent<Rigidbody2D>();
    //   PlayerBody.freezeRotation = true;
    //
    //   pos = position.transform;
    //   tr = transform;
    //   speed = 3.0;
    // }
    //
    // private void FixedUpdate()
    // {
    //     // if (Input.GetKeyDown(KeyCode.LeftArrow) && PlayerBody.isKinematic == true)
    //     // {
    //     //     PlayerBody.isKinematic = false;
    //     //     PlayerBody.velocity = new Vector3(0, 0, speed);
    //     //     PlayerBody.angularDrag = 1;
    //     // }
    //     // if (Input.GetKeyDown(KeyCode.RightArrow) && PlayerBody.isKinematic == true)
    //     // {
    //     //     PlayerBody.isKinematic = false;
    //     //     PlayerBody.velocity = new Vector3(0, 0, -speed);
    //     //     PlayerBody.angularDrag = 2;
    //     // }
    //     // if (Input.GetKeyDown(KeyCode.UpArrow) && PlayerBody.isKinematic == true)
    //     // {
    //     //     PlayerBody.isKinematic = false;
    //     //     PlayerBody.velocity = new Vector3(speed, 0, 0);
    //     //     PlayerBody.angularDrag = 3;
    //     // }
    //     // if (Input.GetKeyDown(KeyCode.DownArrow) && PlayerBody.isKinematic == true)
    //     // {
    //     //     PlayerBody.isKinematic = false;
    //     //     PlayerBody.velocity = new Vector3(-speed, 0, 0);
    //     //     PlayerBody.angularDrag = 4;
    //     // }
    //
    //     // PlayerBody.isKinematic = false;
    //     // float moveHorzontal = Input.GetAxis("Horizontal");
    //     // float moveVertical = Input.GetAxis("Vertical");
    //     // Vector2 movement = new Vector2(moveHorzontal, moveVertical);
    //     // PlayerBody.AddForce(movement * speed);
    //
    //     if (Input.GetKeyDown(KeyCode.D) && tr.position == pos) {
    //          pos += Vector3.right;
    //      }
    //      else if (Input.GetKeyDown(KeyCode.A) && tr.position == pos) {
    //          pos += Vector3.left;
    //      }
    //      else if (Input.GetKeyDown(KeyCode.W) && tr.position == pos) {
    //          pos += Vector3.up;
    //      }
    //      else if (Input.GetKeyDown(KeyCode.S) && tr.position == pos) {
    //          pos += Vector3.down;
    //      }
    //
    //      transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * speed);
    // }
    //
    // void OnCollisionEnter(Collision collision)
    // {
    //     // if(collision.gameObject.name == "YourWallName") {  // or if(gameObject.CompareTag("YourWallTag"))
    //     //              PlayerBody.velocity = Vector3.zero;
    //     // }
    //     PlayerBody.isKinematic = true;
    // }

    private Vector3 pos;                                // For movement
    private float speed;                         // Speed of movement

    void Start () {
        pos = transform.position;          // Take the initial position
        speed = 5.0f;
    }

    void FixedUpdate () {
        if(Input.GetKeyDown(KeyCode.LeftArrow) && transform.position == pos) {
          RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, 1);
          if(hit.collider == null) {
            pos += Vector3.left;
          }
        }
        else if(Input.GetKeyDown(KeyCode.RightArrow) && transform.position == pos) {
          RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, 1);
          if(hit.collider  == null) {
            pos += Vector3.right;
          }
        }
        else if(Input.GetKeyDown(KeyCode.UpArrow) && transform.position == pos) {
          RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, 1);
          if(hit.collider  == null) {
            pos += Vector3.up;
          }
        }
        else if(Input.GetKeyDown(KeyCode.DownArrow) && transform.position == pos) {
          RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1);
          if(hit.collider  == null) {
            pos += Vector3.down;
          }
        }

        transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * speed);
    }
}
