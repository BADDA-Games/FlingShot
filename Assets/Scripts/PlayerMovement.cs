using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    // static PlayerMovement OnlyPlayer;

    private Vector3 pos;
    private float step;
    enum Direction { None, Up, Down, Left, Right }
    private Direction dir;

    public int health;
    public string collisionString;

    public bool gameOverBool;

    public Text currentLevelText;
    public int currentLevel;

    public Text timeRemainingText;
    public float timeRemaining;
    private int timeRemainingInt;

    public Text scoreText;
    public Text seedText;

    private Vector2 touchOrigin = -Vector2.one;
    private Vector3 originalPos;

    public GameObject PlayerBody;
    private Animator animate;
    private BoardCreator board;
    public GameObject GameOverUI;

    public int score;
    public int totalTimeTaken;

    private string levelType;


    void updateLevelText()
    {
        if (currentLevel < 10)
        {
            currentLevelText.text = "0" + currentLevel.ToString();
        }
        else
        {
            currentLevelText.text = currentLevel.ToString();
        }
        timeRemaining = 15;
        updateTimeText();

    }
    void updateTimeText()
    {

        timeRemainingInt = (int)timeRemaining;
        if (timeRemaining < 0)
        {

        }
        else if (timeRemainingInt < 10 && timeRemainingInt >= 0)
        {
            timeRemainingText.text = "0" + timeRemainingInt.ToString();
        }
        else
        {
            timeRemainingText.text = timeRemainingInt.ToString();
        }
    }

    void gameOver()
    {
        //TRIGGER END GAME MENU
        score = totalTimeTaken * currentLevel;
        scoreText.text = "Score: " + score.ToString();
        seedText.text ="Seed: "+ board.getSeed().ToString();
        // GameOverUI.endGame();

        PlayerGameManager.UpdateLastScore(score);

        if(PlayerGameManager.GetHighScore() < score){
          PlayerGameManager.UpdateHighScore(score);
        }




        GameOverUI.SetActive(true);
        gameOverBool = true;
        // Debug.Log("Game Over");
        // Debug.Log("Your score is:");
        // Debug.Log(score);
    }

    void Start()
    {
        // if(OnlyPlayer != null){
        //   Destroy(this.gameObject);
        //   return;
        // }
        // OnlyPlayer = this;
        // DontDestroyOnLoad(this.gameObject);
        // SceneManager.UnloadSceneAsync("BossSceneThunk");

        pos = transform.position;
        step = 1.0f;
        dir = Direction.None;
        levelType = "normal";
        gameOverBool = false;

        originalPos = gameObject.transform.position;

        health = 3;
        timeRemaining = 15;
        currentLevel = 00;

        updateTimeText();
        updateLevelText();

        animate = gameObject.GetComponent<Animator>();
        board = (BoardCreator)GameObject.Find("BoardCreator").GetComponent(typeof(BoardCreator));

    }

    void NextLevel()
    {
        dir = Direction.None;
        gameObject.transform.position = originalPos;
        currentLevel++;

        if(totalTimeTaken >= 0){
          totalTimeTaken = totalTimeTaken + timeRemainingInt;
        }
        // Debug.Log(totalTimeTaken);
        updateLevelText();
        pos = transform.position;
        board.ClearMap(true);

        if(currentLevel % 10 == 0){
          SceneManager.LoadSceneAsync("BossSceneThunk", LoadSceneMode.Additive);
          // SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetSceneByName("BossSceneThunk"));
          levelType = "boss";
        }
        else if((currentLevel % 10 == 1) && (currentLevel != 1)){
          SceneManager.UnloadSceneAsync("BossSceneThunk");
          // SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetSceneByName("GameScene"));
          board.NextLevel();
          levelType = "normal";
        }
        else{
          board.NextLevel();
        }


    }

    void gameObjectCollision(Collider2D collisionObject)
    {
        int startHealth = health;

        if (collisionObject.name == "end")
        {
            NextLevel();
            // collisionObject.gameObject.SetActive(false);
        }
        else
        {
            // Debug.Log(collisionObject);
            // RectTransform rt = healthIndicator.GetComponent<RectTransform>();

            switch (collisionObject.tag)
            {
                case "one_health_remove":
                    health--;
                    collisionObject.gameObject.SetActive(false);
                    // healthBar.text = "Count: " + health.ToString();
                    break;
                case "one_health_no_remove":
                    health--;
                    // collisionObject.gameObject.SetActive(false);
                    // healthBar.text = "Count: " + health.ToString();
                    break;
                default:
                    Debug.Log("Obsticle Not Known");
                    break;
            }
        }

        if (health != startHealth)
        {
            if (health == 3)
            {
            }
            else if (health == 2)
            {
                // m_Image.sprite = m_Sprite;
                Debug.Log("You now have two health");
            }
            else if (health == 1)
            {
                Debug.Log("You now have one health");
            }
            else if (health == 0)
            {
                Debug.Log("You are dead");
            }
        }
        if (health == 0)
        {
            //TRIGGER GAME OVER
            gameOver();
        }

    }

    void Update()
    {
        RaycastHit2D hit;

        timeRemaining -= Time.deltaTime;
        updateTimeText();
        if ((timeRemaining < 0))
        {
            if(levelType != "boss"){
              health--;
            }
            if(health <= 0){
              updateTimeText();
              gameOver();
            }
            else{
              NextLevel();
            }
        }

        #if UNITY_STANDALONE || UNITY_WEBPLAYER

                if (dir == Direction.None)
                {
                    if (Input.GetKeyDown(KeyCode.LeftArrow))
                    {
                        dir = Direction.Left;
                        // Debug.Log("Left");
                    }
                    else if (Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        dir = Direction.Right;
                        // Debug.Log("Right");
                    }
                    else if (Input.GetKeyDown(KeyCode.UpArrow))
                    {
                        dir = Direction.Up;
                        // Debug.Log("Up");
                    }
                    else if (Input.GetKeyDown(KeyCode.DownArrow))
                    {
                        dir = Direction.Down;
                        // Debug.Log("Down");
                    }
                }
                // if (Input.GetMouseButtonDown(0))
                //   gameOver();
        #elif UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE

              if(dir == Direction.None){
                Debug.Log("Waiting for touch");
                if (Input.touchCount > 0)
                      {
                          // Debug.Log("Test1");
                          Touch myTouch = Input.touches[0];

                          //Check if the phase of that touch equals Began
                          if (myTouch.phase == TouchPhase.Began)
                          {
                            // Debug.Log("Touch phase begin");
                            touchOrigin = myTouch.position;
                          }
                          else if (myTouch.phase == TouchPhase.Ended && touchOrigin.x >= 0)
                          {
                            // Debug.Log("touch release");
                              //Set touchEnd to equal the position of this touch
                              Vector2 touchEnd = myTouch.position;
                              //Calculate the difference between the beginning and end of the touch on the x and y axis.
                              float x = touchEnd.x - touchOrigin.x;
                              float y = touchEnd.y - touchOrigin.y;
                              //Set touchOrigin.x to -1 so that our else if statement will evaluate false and not repeat immediately.
                              touchOrigin.x = -1;
                              //Check if the difference along the x axis is greater than the difference along the y axis.
                              if (Mathf.Abs(x) > Mathf.Abs(y)){
                                // Debug.Log("Horizontal Movement");
                                  dir = x > 0 ? Direction.Right : Direction.Left;
                              }
                              else{
                                // Debug.Log("Vertical Movement");
                                  dir = y > 0 ? Direction.Up : Direction.Down;
                              }
                          }
                      }
              }
        #endif

        if (((board.MapLoaded()) || (levelType == "boss")) && gameOverBool == false)
        {
            switch (dir)
            {
                case Direction.Left:
                    hit = Physics2D.Raycast(transform.position, Vector2.left, 1);
                    // if (hit.collider != null)
                    // {
                    //   Debug.Log(hit.collider.name);
                    // }
                    if (hit.collider == null)
                    {
                        pos = transform.position + Vector3.left;
                        animate.Play("Moving");
                    }
                    else if (hit.collider.name == "walls")
                    {
                        dir = Direction.None;
                        animate.Play("Body_Compress_Left");
                    }
                    else
                    {
                        pos = transform.position + Vector3.left;
                        gameObjectCollision(hit.collider);
                    }
                    break;
                case Direction.Right:
                    hit = Physics2D.Raycast(transform.position, Vector2.right, 1);
                    // if (hit.collider != null)
                    // {
                    //   Debug.Log(hit.collider.name);
                    // }
                    if (hit.collider == null)
                    {
                        pos = transform.position + Vector3.right;
                        animate.Play("Moving");
                    }
                    else if (hit.collider.name == "walls")
                    {
                        dir = Direction.None;
                        animate.Play("Body_Compress_Right");
                    }
                    else
                    {
                        pos = transform.position + Vector3.right;
                        gameObjectCollision(hit.collider);
                    }
                    break;
                case Direction.Up:
                    hit = Physics2D.Raycast(transform.position, Vector2.up, 1);
                    // if (hit.collider != null)
                    // {
                    //   Debug.Log(hit.collider.name);
                    // }


                    if (hit.collider == null)
                    {
                        pos = transform.position + Vector3.up;
                        animate.Play("Moving");
                    }
                    else if (hit.collider.name == "walls")
                    {
                        dir = Direction.None;
                        animate.Play("Body_Compress_Up");
                    }
                    else
                    {
                        pos = transform.position + Vector3.up;
                        gameObjectCollision(hit.collider);
                    }
                    break;
                case Direction.Down:
                    hit = Physics2D.Raycast(transform.position, Vector2.down, 1);
                    // if (hit.collider != null)
                    // {
                    //   Debug.Log(hit.collider.name);
                    // }
                    if (hit.collider == null)
                    {
                        pos = transform.position + Vector3.down;
                        animate.Play("Moving");
                    }
                    else if (hit.collider.name == "walls")
                    {
                        dir = Direction.None;
                        animate.Play("Body_Compress_Down");
                    }
                    else
                    {
                        pos = transform.position + Vector3.down;
                        gameObjectCollision(hit.collider);
                    }
                    break;
                default:
                    //Debug.Log("Error no Direction assigned...");
                    break;
            }
        }
        else
        {
            dir = Direction.None;
        }

        transform.position = Vector3.MoveTowards(transform.position, pos, step);
    }
}
