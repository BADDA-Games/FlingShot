using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    private Vector3 pos;
    private float step;
    enum Direction {None, Up, Down, Left, Right}
    private Direction dir;


    private Vector2 touchOrigin = -Vector2.one;

    public GameObject PlayerBody;

    private Animator animate;

    void Start() {
        pos = transform.position;
        step = 1.0f;
        dir = Direction.None;

        animate = gameObject.GetComponent<Animator>();
    }

    void Update() {
      RaycastHit2D hit;

      //Check if we are running either in the Unity editor or in a standalone build.
      #if UNITY_STANDALONE || UNITY_WEBPLAYER

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
                animate.Play("Moving");
              }
              else{
                dir = Direction.None;
                animate.Play("Body_Compress_Left");
              }
              break;
          case Direction.Right:
              hit = Physics2D.Raycast(transform.position, Vector2.right, 1);
              if(hit.collider == null){
                pos = transform.position + Vector3.right;
                animate.Play("Moving");
              }
              else{
                dir = Direction.None;
                animate.Play("Body_Compress_Right");
              }
              break;
          case Direction.Up:
              hit = Physics2D.Raycast(transform.position, Vector2.up, 1);
              if(hit.collider == null){
                pos = transform.position + Vector3.up;
                animate.Play("Moving");
              }
              else{
                dir = Direction.None;
                animate.Play("Body_Compress_Up");
              }
              break;
          case Direction.Down:
              hit = Physics2D.Raycast(transform.position, Vector2.down, 1);
              if(hit.collider == null){
                pos = transform.position + Vector3.down;
                animate.Play("Moving");
              }
              else{
                dir = Direction.None;
                animate.Play("Body_Compress_Down");
              }
              break;
          default:
              Debug.Log("Error no Direction assigned... standalone/webplayer");
              break;
      }

      //Check if we are running on iOS, Android, Windows Phone 8 or Unity iPhone
      #elif UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE
      switch(dir) {
        case Direction.None:
          Debug.Log("Waiting for touch");
          if (Input.touchCount > 0)
                {
                    Debug.Log("Test1");
                    Touch myTouch = Input.touches[0];

                    //Check if the phase of that touch equals Began
                    if (myTouch.phase == TouchPhase.Began)
                    {
                      Debug.Log("Touch phase begin");
                      touchOrigin = myTouch.position;
                    }
                    else if (myTouch.phase == TouchPhase.Ended && touchOrigin.x >= 0)
                    {
                      Debug.Log("touch release");
                        //Set touchEnd to equal the position of this touch
                        Vector2 touchEnd = myTouch.position;

                        //Calculate the difference between the beginning and end of the touch on the x and y axis.
                        float x = touchEnd.x - touchOrigin.x;
                        float y = touchEnd.y - touchOrigin.y;

                        //Set touchOrigin.x to -1 so that our else if statement will evaluate false and not repeat immediately.
                        touchOrigin.x = -1;

                        //Check if the difference along the x axis is greater than the difference along the y axis.
                        if (Mathf.Abs(x) > Mathf.Abs(y)){
                          Debug.Log("Horizontal Movement");
                            dir = x > 0 ? Direction.Right : Direction.Left;
                        }
                        else{
                          Debug.Log("Vertical Movement");
                            dir = y > 0 ? Direction.Up : Direction.Down;
                        }
                    }
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
              Debug.Log("Error no Direction assigned... mobile");
              break;
      }

      #endif

      transform.position = Vector3.MoveTowards(transform.position, pos, step);
    }
}