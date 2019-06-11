using System;
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

    public Text scoreText;
    public Text seedText;

    private Vector2 touchOrigin = -Vector2.one;
    private Vector3 originalPos;

    public GameObject PlayerBody;
    public GameObject goalObject;
    private Animator goalAnimate;
    private Animator animate;
    private BoardCreator board;
    public Timer timer;
    public GameObject GameOverUI;

    public Transform Goal;
    public Transform Pupil;

    public float eyeRadius;
    Vector3 eyeCenter;

    public int score;
    public int totalTimeTaken;

    private string levelType;

    public TrailRenderer tr;

    void UpdateLevelText()
    {
        if (currentLevel < 10)
        {
            currentLevelText.text = "0" + currentLevel.ToString();
        }
        else
        {
            currentLevelText.text = currentLevel.ToString();
        }
        timer.TimeRemaining = LevelTime();
        timer.UpdateTimeText();

    }

    private int LevelTime()
    {
        if(currentLevel == 0)
        {
            return Constants.INITIAL_LEVEL_TIME;
        }
        else if(currentLevel % Constants.BOSS_FREQUENCY == 0)
        {
            return Constants.BOSS_LEVEL_TIME;
        }
        else if(PlayerGameManager.GetDifficulty() == "Easy")
        {
            return Constants.EASY_TIME;
        }
        else if(PlayerGameManager.GetDifficulty() == "Medium")
        {
            return Constants.MEDIUM_TIME;
        }
        else if(PlayerGameManager.GetDifficulty() == "Hard")
        {
            return Constants.HARD_TIME;
        }
        else
        {
            Debug.Log("Cannot determine appropriate level timer!");
            // Default to the default time
            return Constants.MEDIUM_TIME;
        }
    }

    public void GameOver()
    {
        switch (PlayerGameManager.GetDifficulty())
        {
            case "Easy":
                score = Convert.ToInt32(totalTimeTaken * currentLevel * Constants.EASY_SCORE_MODIFIER);
                break;
            case "Hard":
                score = Convert.ToInt32(totalTimeTaken * currentLevel * Constants.HARD_SCORE_MODIFIER);
                break;
            default:
                score = Convert.ToInt32(totalTimeTaken * currentLevel * Constants.MEDIUM_SCORE_MODIFIER);
                break;
        }
        scoreText.text = "Score: " + score.ToString();
        seedText.text = "Seed: " + board.GetSeed().ToString();

        PlayerGameManager.UpdateLastScore(score);

        if(PlayerGameManager.GetHighScore() < score){
          PlayerGameManager.UpdateHighScore(score);
        }

        if(GameOverUI != null)
        {
            GameOverUI.SetActive(true);
        }
        gameOverBool = true;
    }

    void Start()
    {
        pos = transform.position;
        step = 1.0f;
        dir = Direction.None;
        levelType = "normal";
        gameOverBool = false;

        originalPos = gameObject.transform.position;

        health = 3;
        currentLevel = 00;

        UpdateLevelText();

        eyeCenter = Pupil.localPosition;
        eyeRadius = (float)0.25;

        animate = gameObject.GetComponent<Animator>();
        goalAnimate = goalObject.GetComponent<Animator>();

        board = (BoardCreator)GameObject.Find("BoardCreator").GetComponent(typeof(BoardCreator));
    }

    void NextLevel()
    {
        dir = Direction.None;
        gameObject.transform.position = originalPos;

        currentLevel++;

        if(totalTimeTaken >= 0){
            totalTimeTaken += (int)timer.TimeRemaining;
        }
        UpdateLevelText();
        pos = transform.position;
        board.ClearMap(true);

        if(currentLevel % Constants.BOSS_FREQUENCY == 0){
          SceneManager.LoadSceneAsync("BossSceneThunk", LoadSceneMode.Additive);
          levelType = "boss";
        }
        else if((currentLevel % Constants.BOSS_FREQUENCY == 1) && (currentLevel != 1)){
          SceneManager.UnloadSceneAsync("BossSceneThunk");
          board.NextLevel();
          goalObject.transform.position = goalObject.transform.position + Vector3.up;
          levelType = "normal";
        }
        else{
          board.NextLevel();
        }
    }

    IEnumerator PlayGoalAnimation() {
        dir = Direction.None;
        animate.SetBool("atGoal", true);
        // goalAnimate.SetBool("atGoal", true);
        animate.Play("Goal");
        yield return new WaitForSecondsRealtime(animate.GetCurrentAnimatorStateInfo(0).length + animate.GetCurrentAnimatorStateInfo(0).normalizedTime);
        NextLevel();
        animate.SetBool("atGoal", false);
        animate.Play("Start");
    }

    void GameObjectCollision(Collider2D collisionObject)
    {
        if (collisionObject.name == "goal")
        {
            if(levelType == "boss" && health < 3)
            {
              health++;
            }
            StartCoroutine(PlayGoalAnimation());
            // collisionObject.gameObject.SetActive(false);
        }
        else
        {
            switch (collisionObject.tag)
            {
                case "one_health_remove":
                    health--;
                    collisionObject.gameObject.SetActive(false);
                    break;
                case "one_health_no_remove":
                    health--;
                    break;
                default:
                    //Debug.Log("Obstacle Not Known");
                    break;
            }
        }
        if (health == 0)
        {
            GameOver();
        }
    }

    void Update()
    {
        Vector3 lookDir = (Goal.position - (Pupil.parent.position + Pupil.localPosition)).normalized;
        Pupil.localPosition = eyeCenter + (lookDir * eyeRadius);

        RaycastHit2D hit;

        //timeRemaining -= Time.deltaTime;
        timer.UpdateTimeText();
        if (timer.TimeRemaining < 0)
        {
            if(levelType != "boss" && !animate.GetBool("atGoal")){
                health--;
            }

            if(health <= 0){
                timer.UpdateTimeText();
                GameOver();
            }
            else {
                if (!animate.GetBool("atGoal") && levelType != "boss") {
                    NextLevel();
                }
            }
        }

        #if UNITY_STANDALONE || UNITY_WEBPLAYER

                if (dir == Direction.None)
                {
                    if (Input.GetKeyDown(KeyCode.LeftArrow))
                    {
                        dir = Direction.Left;
                    }
                    else if (Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        dir = Direction.Right;
                    }
                    else if (Input.GetKeyDown(KeyCode.UpArrow))
                    {
                        dir = Direction.Up;
                    }
                    else if (Input.GetKeyDown(KeyCode.DownArrow))
                    {
                        dir = Direction.Down;
                    }
                }

        #elif UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE

              if(dir == Direction.None){
                if (Input.touchCount > 0)
                      {
                          Touch myTouch = Input.touches[0];

                          //Check if the phase of that touch equals Began
                          if (myTouch.phase == TouchPhase.Began)
                          {
                            touchOrigin = myTouch.position;
                          }
                          else if (myTouch.phase == TouchPhase.Ended && touchOrigin.x >= 0)
                          {
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

        if ((board.MapLoaded() || (levelType == "boss")) && gameOverBool == false && !animate.GetBool("atGoal"))
        {
            switch (dir)
            {
                case Direction.Left:
                    hit = Physics2D.Raycast(transform.position + Vector3.left, Vector2.left, (float)0.1);
                    if (hit.collider == null)
                    {
                        pos = transform.position + Vector3.left;
                        animate.Play("Moving");
                        animate.ResetTrigger("left");
                    }
                    else if (hit.collider.name == "walls")
                    {
                        dir = Direction.None;
                        animate.Play("Body_Compress_Left");
                        animate.SetTrigger("left");
                    }
                    else
                    {
                        pos = transform.position + Vector3.left;
                        GameObjectCollision(hit.collider);
                    }
                    break;
                case Direction.Right:
                    hit = Physics2D.Raycast(transform.position + Vector3.right, Vector2.right, (float)0.1);
                    if (hit.collider == null)
                    {
                        pos = transform.position + Vector3.right;
                        animate.Play("Moving");
                        animate.ResetTrigger("right");
                    }
                    else if (hit.collider.name == "walls")
                    {
                        dir = Direction.None;
                        animate.Play("Body_Compress_Right");
                        animate.SetTrigger("right");
                    }
                    else
                    {
                        pos = transform.position + Vector3.right;
                        GameObjectCollision(hit.collider);
                    }
                    break;
                case Direction.Up:
                    hit = Physics2D.Raycast(transform.position + Vector3.up, Vector2.up, (float)0.1);
                    if (levelType == "boss" && (int) timer.TimeRemaining > 0)
                    {
                        dir = Direction.None;
                    }
                    else if (hit.collider == null)
                    {
                        pos = transform.position + Vector3.up;
                        animate.Play("Moving");
                        animate.ResetTrigger("up");
                        // animate.SetTrigger("move");
                    }
                    else if (hit.collider.name == "walls")
                    {
                        dir = Direction.None;
                        animate.Play("Body_Compress_Up");
                        // animate.ResetTrigger("move");
                        animate.SetTrigger("up");
                    }
                    else
                    {
                        pos = transform.position + Vector3.up;
                        GameObjectCollision(hit.collider);
                    }
                    break;
                case Direction.Down:
                    hit = Physics2D.Raycast(transform.position + Vector3.down, Vector2.down, (float)0.1);
                    if (hit.collider == null)
                    {
                        pos = transform.position + Vector3.down;
                        animate.Play("Moving");
                        animate.ResetTrigger("down");
                    }
                    else if (hit.collider.name == "walls")
                    {
                        dir = Direction.None;
                        animate.Play("Body_Compress_Down");
                        animate.SetTrigger("down");
                    }
                    else
                    {
                        pos = transform.position + Vector3.down;
                        GameObjectCollision(hit.collider);
                    }
                    break;
            }
        }
        else
        {
            dir = Direction.None;
        }

        transform.position = Vector3.MoveTowards(transform.position, pos, 100f * Time.deltaTime);
    }

    private void OnDestroy() {
      timer.UpdateTimeText();
      GameOver();
    }
}
