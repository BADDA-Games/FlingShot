using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {
    private Vector3 pos;
    private float step;
    enum Direction {None, Up, Down, Left, Right}
    private Direction dir;
    public int health;
    public string collisionString;

    public Text currentLevelText;
    public int currentLevel;

    public Text timeRemainingText;
    public float timeRemaining;
    private int timeRemainingInt;

    private Vector2 touchOrigin = -Vector2.one;
    private Vector3 originalPos;

    public GameObject PlayerBody;
    private Animator animate;

    void updateLevelText(){
      if (currentLevel < 10){
        currentLevelText.text = "0" + currentLevel.ToString();
      }else{
        currentLevelText.text = currentLevel.ToString();
      }
      timeRemaining = 15;
      updateTimeText();

    }
    void updateTimeText(){
      timeRemainingInt = (int) timeRemaining;
      if(timeRemaining < 0){

      }
      else if (timeRemainingInt < 10 && timeRemainingInt >= 0 ){
        timeRemainingText.text = "0" + timeRemainingInt.ToString();
      }
      else{
        timeRemainingText.text = timeRemainingInt.ToString();
      }

    }

    void gameOver(){
      Debug.Log("Game Over");
    }

    void Start() {
        pos = transform.position;
        step = 1.0f;
        dir = Direction.None;

        originalPos = gameObject.transform.position;

        health = 3;
        timeRemaining = 15;
        currentLevel = 00;

        updateTimeText();
        updateLevelText();

        animate = gameObject.GetComponent<Animator>();
    }

    void gameObjectCollision(string collisionObject){
      int startHealth = health;
      // Debug.Log(collisionObject);
      // RectTransform rt = healthIndicator.GetComponent<RectTransform>();

      switch(collisionObject){
        case "enemy1HP":
          health--;
          // healthBar.text = "Count: " + health.ToString();
          break;
        case "end":
          // currentLevel ++;
          //CALL RYANS REMAP
          //reset player
          // dir = Direction.None;
          // gameObject.transform.position = originalPos;
          break;
        default:
          // Debug.Log("Obstical Not Known");
          break;
      }

      if(health != startHealth){
        if(health == 3){
        }else if(health == 2){
          // m_Image.sprite = m_Sprite;
          Debug.Log("You now have two health");
        }else if(health == 1){
          Debug.Log("You now have one health");
        }else if(health == 0){
          Debug.Log("You are dead");
        }
      }
      if(health == 0){
        //TRIGGER GAME OVER
        gameOver();
      }

    }

    void Update() {
      RaycastHit2D hit;

      timeRemaining -= Time.deltaTime;
      updateTimeText();
      if ( timeRemaining < 0 )
      {
        updateTimeText();
        gameOver();
      }

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
              else if(hit.collider.name == "walls"){
                dir = Direction.None;
                animate.Play("Body_Compress_Left");
              }
              else if(hit.collider.name == "end"){
                dir = Direction.None;
                gameObject.transform.position = originalPos;
                currentLevel ++;
                updateLevelText();
                pos = transform.position;
              }
              else{
                pos = transform.position + Vector3.left;
                hit.collider.gameObject.SetActive(false);
                gameObjectCollision(hit.collider.tag);

              }
              break;
          case Direction.Right:
              hit = Physics2D.Raycast(transform.position, Vector2.right, 1);
              if(hit.collider == null){
                pos = transform.position + Vector3.right;
                animate.Play("Moving");
              }
              else if(hit.collider.name == "walls"){
                dir = Direction.None;
                animate.Play("Body_Compress_Right");
              }
              else if(hit.collider.name == "end"){
                dir = Direction.None;
                gameObject.transform.position = originalPos;
                currentLevel ++;
                updateLevelText();
                pos = transform.position;
              }
              else{
                pos = transform.position + Vector3.right;
                hit.collider.gameObject.SetActive(false);
                gameObjectCollision(hit.collider.tag);
              }
              break;
          case Direction.Up:
              hit = Physics2D.Raycast(transform.position, Vector2.up, 1);

              if(hit.collider == null){
                pos = transform.position + Vector3.up;
                animate.Play("Moving");
              }
              else if(hit.collider.name == "walls"){
                dir = Direction.None;
                animate.Play("Body_Compress_Up");
              }
              else if(hit.collider.name == "end"){
                dir = Direction.None;
                gameObject.transform.position = originalPos;
                currentLevel ++;
                updateLevelText();
                pos = transform.position;
              }
              else{
                pos = transform.position + Vector3.up;
                hit.collider.gameObject.SetActive(false);
                gameObjectCollision(hit.collider.tag);
              }
              break;
          case Direction.Down:
              hit = Physics2D.Raycast(transform.position, Vector2.down, 1);
              if(hit.collider == null){
                pos = transform.position + Vector3.down;
                animate.Play("Moving");
              }
              else if(hit.collider.name == "walls"){
                dir = Direction.None;
                animate.Play("Body_Compress_Down");
              }
              else if(hit.collider.name == "end"){
                dir = Direction.None;
                gameObject.transform.position = originalPos;
                currentLevel ++;
                updateLevelText();
                pos = transform.position;
              }
              else{
                pos = transform.position + Vector3.down;
                hit.collider.gameObject.SetActive(false);
                gameObjectCollision(hit.collider.tag);
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
                animate.Play("Moving");
                pos = transform.position + Vector3.left;
              }
              else if(hit.collider.name == "walls"){
                dir = Direction.None;
                animate.Play("Body_Compress_Left");
              }
              else if(hit.collider.name == "end"){
                dir = Direction.None;
                gameObject.transform.position = originalPos;
                currentLevel ++;
                updateLevelText();
                pos = transform.position;
              }
              else{
                pos = transform.position + Vector3.left;
                hit.collider.gameObject.SetActive(false);
                gameObjectCollision(hit.collider.tag);
              }
              break;
          case Direction.Right:
              hit = Physics2D.Raycast(transform.position, Vector2.right, 1);
              if(hit.collider == null){
                animate.Play("Moving");
                pos = transform.position + Vector3.right;
              }
              else if(hit.collider.name == "walls"){
                dir = Direction.None;
                animate.Play("Body_Compress_Right");
              }
              else if(hit.collider.name == "end"){
                dir = Direction.None;
                gameObject.transform.position = originalPos;
                currentLevel ++;
                updateLevelText();
                pos = transform.position;
              }
              else{
                pos = transform.position + Vector3.right;
                hit.collider.gameObject.SetActive(false);
                gameObjectCollision(hit.collider.tag);
              }
              break;
          case Direction.Up:
              hit = Physics2D.Raycast(transform.position, Vector2.up, 1);
              if(hit.collider == null){
                animate.Play("Moving");
                pos = transform.position + Vector3.up;
              }
              else if(hit.collider.name == "walls"){
                dir = Direction.None;
                animate.Play("Body_Compress_Up");
              }
              else if(hit.collider.name == "end"){
                dir = Direction.None;
                gameObject.transform.position = originalPos;
                currentLevel ++;
                updateLevelText();
                pos = transform.position;
              }
              else{
                pos = transform.position + Vector3.up;
                hit.collider.gameObject.SetActive(false);
                gameObjectCollision(hit.collider.tag);
              }
              break;
          case Direction.Down:
              hit = Physics2D.Raycast(transform.position, Vector2.down, 1);
              if(hit.collider == null){
                animate.Play("Moving");
                pos = transform.position + Vector3.down;
              }
              else if(hit.collider.name == "walls"){
                dir = Direction.None;
                animate.Play("Body_Compress_Down");
              }
              else if(hit.collider.name == "end"){
                dir = Direction.None;
                gameObject.transform.position = originalPos;
                currentLevel ++;
                updateLevelText();
                pos = transform.position;
              }
              else{
                pos = transform.position + Vector3.down;
                hit.collider.gameObject.SetActive(false);
                gameObjectCollision(hit.collider.tag);
              }
          default:
              Debug.Log("Error no Direction assigned... mobile");
              break;
      }
      #endif
      transform.position = Vector3.MoveTowards(transform.position, pos, step);
    }
}