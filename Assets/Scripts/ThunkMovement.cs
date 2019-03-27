using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunkMovement : MonoBehaviour {
  public Vector3 startPosition;
   public Vector3[] moveToPoints;
   public Vector3 currentPoint;

   public float moveSpeed;

   public int pointSelection;

   // Use this for initialization
   void Start () {

       //Sets the object to your starting point
       this.transform.position = startPosition;

   }

   // Update is called once per frame
   void Update () {

       Move ();

   }


   void Move(){

       //Starts to move the object towards the first "moveToPoint" you set in inspector
       this.transform.position = Vector3.MoveTowards (this.transform.position, currentPoint, Time.deltaTime * moveSpeed);

       //check to see if the object is at the next "moveToPoint"
       if (this.transform.position == currentPoint) {

           //if so it sets the next moveTo location
           pointSelection++;

           //if your object hits the last "moveToPoint it sends the object back to starting position to start the sequence over
           if (pointSelection == moveToPoints.Length){
               pointSelection = 0;

           }

           //sets the destination of the "moveToPoint" destination
           currentPoint = moveToPoints[pointSelection];
       }
   }
}

/*
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
          animate.Play("Moving");
        }
        else{
          dir = Direction.None;
          animate.Play("Body_Compress_Left");
        }
        break;
}

transform.position = Vector3.MoveTowards(transform.position, pos, step);

}*/
