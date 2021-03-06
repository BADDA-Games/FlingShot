using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private Vector3 pos;

    private Direction dir;
    private Direction previousDir;

    private LevelType levelType;

    public string collisionString;

    private Vector2 touchOrigin = -Vector2.one;
    private Vector3 originalPos;

    public GameObject PlayerBody;
    public GameObject goalObject;
    private Animator goalAnimate;
    private Animator animate;
    public BoardCreator board;
    public Timer timer;
    public Level level;
    public GameOverMenu gameOverMenu;
    public TrailRenderer tr;

    public Transform Goal;
    public Transform Pupil;

    public float eyeRadius;
    Vector3 eyeCenter;

    public int score;

    void Start()
    {
        pos = transform.position;
        dir = Direction.None;
        previousDir = Direction.None;
        levelType = LevelType.Normal;

        originalPos = gameObject.transform.position;

        level.UpdateLevelText();

        eyeCenter = Pupil.localPosition;
        eyeRadius = (float) 0.25;

        animate = gameObject.GetComponent<Animator>();
        goalAnimate = goalObject.GetComponent<Animator>();
    }

    void NextLevel()
    {
        dir = Direction.None;
        previousDir = Direction.None;
        gameObject.transform.position = originalPos;
        if(levelType == LevelType.Normal)
        {
            if (GameVariables.TotalTimeTaken >= 0)
            {
                GameVariables.TotalTimeTaken += (int)GameVariables.TimeRemaining;
                if(GameVariables.GameType == GameType.Endless)
                {
                    PlayerGameManager.SetTotalTimePlayedEndless(GameVariables.TotalTimeTaken);
                }
            }
        }
        level.AdvanceLevel();
        pos = transform.position;
        board.ClearMap();
        if(GameVariables.GameType == GameType.Standard || GameVariables.GameType == GameType.Puzzle)
        {
            if (GameVariables.CurrentLevel % Constants.BOSS_FREQUENCY == 0)
            {
                SceneManager.LoadSceneAsync("BossSceneThunk", LoadSceneMode.Additive);
                levelType = LevelType.Boss;
                return;
            }
            if ((GameVariables.CurrentLevel % Constants.BOSS_FREQUENCY == 1) && (GameVariables.CurrentLevel != 1))
            {
                SceneManager.UnloadSceneAsync("BossSceneThunk");
                board.NextLevel();
                goalObject.transform.position = goalObject.transform.position + Vector3.up;
                levelType = LevelType.Normal;
                return;
            }
        }
        board.NextLevel();
        if(GameVariables.GameType == GameType.Endless)
        {
            PlayerGameManager.IncrementCurrentLevelEndless();
        }
    }

    IEnumerator PlayGoalAnimation() {
        dir = Direction.None;
        animate.SetBool("atGoal", true);
        animate.Play("Goal");
        float normalizedTime = Math.Min(animate.GetCurrentAnimatorStateInfo(0).normalizedTime, Constants.MAX_NORMALIZED_TIME);
        yield return new WaitForSecondsRealtime(1 + normalizedTime);
        NextLevel();
        animate.SetBool("atGoal", false);
        animate.Play("Start");
    }

    void GameObjectCollision(Collider2D collisionObject)
    {
        if (collisionObject.name == "goal")
        {
            if (levelType == LevelType.Boss && GameVariables.Health < 3)
            {
                GameVariables.Health++;
            }
            StartCoroutine(PlayGoalAnimation());
            // collisionObject.gameObject.SetActive(false);
        }
        else
        {
            switch (collisionObject.tag)
            {
                case "one_health_remove":
                    GameVariables.Health--;
                    collisionObject.gameObject.SetActive(false);
                    break;
                case "one_health_no_remove":
                    GameVariables.Health--;
                    break;
                default:
                    //Debug.Log("Obstacle Not Known");
                    break;
            }
        }
        if (GameVariables.Health == 0)
        {
            gameOverMenu.TriggerGameOver();
        }
    }

    void Update()
    {
        Vector3 lookDir = (Goal.position - (Pupil.parent.position + Pupil.localPosition)).normalized;
        Pupil.localPosition = eyeCenter + (lookDir * eyeRadius);

        if ((GameVariables.GameType == GameType.Standard && GameVariables.TimeRemaining < 0) ||
            (GameVariables.GameType == GameType.Puzzle && GameVariables.TimeRemaining <= 0))
        {
            if(levelType != LevelType.Boss && !animate.GetBool("atGoal")){
                GameVariables.Health--;
            }

            if(GameVariables.Health <= 0){
                timer.UpdateTimeText();
                gameOverMenu.TriggerGameOver();
            }
            else {
                if (!animate.GetBool("atGoal") && levelType != LevelType.Boss) {
                    NextLevel();
                }
            }
        }

        if (GameVariables.IgnoreNextFrame)
        {
            GameVariables.IgnoreNextFrame = false;
            return;
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

            if (dir == Direction.None && !GameVariables.IsPaused){
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

        if ((board.MapLoaded() || (levelType == LevelType.Boss)) && GameVariables.IsGameOver == false && !animate.GetBool("atGoal"))
        {
            RaycastHit2D hit;
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
                        if (GameVariables.GameType == GameType.Puzzle && dir != previousDir)
                        {
                            GameVariables.TimeRemaining--;
                            timer.UpdateTimeText();
                            previousDir = dir;
                        }
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
                        if (GameVariables.GameType == GameType.Puzzle && dir != previousDir)
                        {
                            GameVariables.TimeRemaining--;
                            timer.UpdateTimeText();
                            previousDir = dir;
                        }
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
                    if (levelType == LevelType.Boss && (int) GameVariables.TimeRemaining > 0)
                    {
                        dir = Direction.None;
                    }
                    else if (hit.collider == null)
                    {
                        pos = transform.position + Vector3.up;
                        animate.Play("Moving");
                        animate.ResetTrigger("up");
                    }
                    else if (hit.collider.name == "walls")
                    {
                        if (GameVariables.GameType == GameType.Puzzle && dir != previousDir)
                        {
                            GameVariables.TimeRemaining--;
                            timer.UpdateTimeText();
                            previousDir = dir;
                        }
                        dir = Direction.None;
                        animate.Play("Body_Compress_Up");
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
                        if (GameVariables.GameType == GameType.Puzzle && dir != previousDir && levelType != LevelType.Boss)
                        {
                            GameVariables.TimeRemaining--;
                            timer.UpdateTimeText();
                            previousDir = dir;
                        }
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
        GameVariables.IgnoreNextFrame = false;
    }

    private void OnDestroy() {
        timer.UpdateTimeText();
        if(levelType == LevelType.Boss)
        {
            gameOverMenu.TriggerGameOver();
        }

    }
}