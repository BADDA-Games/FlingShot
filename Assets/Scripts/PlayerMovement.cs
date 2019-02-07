using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    private Vector3 pos;
    private float step;
    enum Direction {None, Up, Down, Left, Right}
    private Direction dir;

    void Start() {
        pos = transform.position;
        step = 1.0f;
        dir = Direction.None;
    }

    void Update() {
      RaycastHit2D hit;

      switch(dir) {
          case Direction.None:
              if(Input.GetKeyDown(KeyCode.LeftArrow)) {
                dir = Direction.Left;
              }
              else if(Input.GetKeyDown(KeyCode.RightArrow)) {
                dir = Direction.Right;
              }
              else if(Input.GetKeyDown(KeyCode.UpArrow)) {
                dir = Direction.Up;
              }
              else if(Input.GetKeyDown(KeyCode.DownArrow)) {
                dir = Direction.Down;
              }
              break;
          case Direction.Left:
              hit = Physics2D.Raycast(transform.position, Vector2.left, 1);
              if(hit.collider == null){
                pos = transform.position + Vector3.left;
              }
              else{
                dir = Direction.None;
              }
              break;
          case Direction.Right:
              hit = Physics2D.Raycast(transform.position, Vector2.right, 1);
              if(hit.collider == null){
                pos = transform.position + Vector3.right;
              }
              else{
                dir = Direction.None;
              }
              break;
          case Direction.Up:
              hit = Physics2D.Raycast(transform.position, Vector2.up, 1);
              if(hit.collider == null){
                pos = transform.position + Vector3.up;
              }
              else{
                dir = Direction.None;
              }
              break;
          case Direction.Down:
              hit = Physics2D.Raycast(transform.position, Vector2.down, 1);
              if(hit.collider == null){
                pos = transform.position + Vector3.down;
              }
              else{
                dir = Direction.None;
              }
              break;
          default:
              Debug.Log("Error no Direction assigned...");
              break;
      }

      transform.position = Vector3.MoveTowards(transform.position, pos, step);
    }
}
