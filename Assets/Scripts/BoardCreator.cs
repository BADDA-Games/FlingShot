using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;

public class BoardCreator : MonoBehaviour{

    // static BoardCreator OnlyMap;

    public Vector3Int tmapSize;

    public Tilemap puzzle;
    public Tile wall;
    public Tile tBar;
    public Tile rBar;
    public Tile bBar;
    public Tile lBar;
    public Tile trBar;
    public Tile brBar;
    public Tile blBar;
    public Tile tlBar;
    public Tile tlrBar;
    public Tile tblBar;
    public Tile tbrBar;
    public Tile blrBar;
    public Tile tblrBar;
    public Tile trRound;
    public Tile brRound;
    public Tile tlRound;
    public Tile blRound;
    public Tile tRound;
    public Tile rRound;
    public Tile bRound;
    public Tile lRound;
    public Tile rtRound;
    public Tile lbRound;
    public Tile rbRound;
    public Tile ltRound;
    public Tile tbRound;
    public Tile tbBar;
    public Tile lrBar;
    public Tile tBarblRound;
    public Tile tBarbrRound;
    public Tile tBarbRound;
    public Tile rBartlRound;
    public Tile rBarblRound;
    public Tile rBarlRound;
    public Tile lBartrRound;
    public Tile lBarbrRound;
    public Tile lBarrRound;
    public Tile bBartrRound;
    public Tile bBartlRound;
    public Tile bBartRound;
    public Tile tlBarbrRound;
    public Tile trBarblRound;
    public Tile brBartlRound;
    public Tile blBartrRound;
    public Tile trblRound;
    public Tile tlbrRound;

    public Tile none;

    private Algorithm.Algorithm a;

    private int width;
    private int height;
    private int seed;
    private bool loaded;

    private Queue<Texture[,]> maps;
    private Mutex mutex;
    private Thread creator;

    enum Texture {tBar,rBar,bBar,lBar,trBar,brBar,
      blBar,tlBar,tlrBar,tblBar,tbrBar,blrBar,tblrBar,trRound,
      brRound,blRound,tlRound,tRound,rRound,bRound,lRound,rtRound,
      lbRound,rbRound,ltRound,tbRound,tbBar,lrBar,tBarblRound,
      tBarbrRound,tBarbRound,rBartlRound,rBarblRound,rBarlRound,
      lBartrRound,lBarbrRound,lBarrRound,bBartrRound,bBartlRound,
      bBartRound,tlBarbrRound,trBarblRound,brBartlRound,blBartrRound,trblRound,tlbrRound,none,empty}
      //finished: tBar,rBar,bBar,lBar,trBar,brBar,
      //blBar,tlBar,tlrBar,tblBar,tbrBar,blrBar,tblrBar,trRound,
      //brRound,blRound,tlRound,tRound,rRound,bRound,lRound,rtRound,
      //lbRound,rbRound,ltRound,tbRound,none
      //tbBar,lrBar,tBarblRound,tBarbrRound,tBarbRound,
      //rBartlRound,rBarblRound,rBarlRound,lBartrRound,lBarbrRound,
      //lBarrRound,bBartrRound,bBartlRound,bBartRound,tlBarbrRound,
      //trBarblRound,brBartlRound,blBartrRound,trblRound,tlbrRound
    private Texture[,] puzzleMap;

    private const int MAX_QUEUE_SIZE = 20;
    private const int THREAD_SLEEP_TIME = 25;


    public void NextLevel()
    {
        //Debug.Log(maps.Count);
        if(!loaded)
        {
            // Debug.Log(maps.Count);
            while(maps.Count <= 0)
            {
                Thread.Sleep(THREAD_SLEEP_TIME);
                //Wait for other thread produces a map
            }
            mutex.WaitOne();
            puzzleMap = maps.Dequeue();
            mutex.ReleaseMutex();
            PlacePuzzle(puzzleMap);
            loaded = true;
        }
    }

    public int getSeed()
    {
      return(a.GetInitSeed());
    }

    private void AddMazeToQueue()
    {
        while (true)
        {
            if (maps.Count < MAX_QUEUE_SIZE)
            {
                int[,] nextMap = a.Generate();
                Texture[,] textured = ConnectedTexture(nextMap);
                mutex.WaitOne();
                maps.Enqueue(textured);
                mutex.ReleaseMutex();
            }
            else
            {
                Thread.Sleep(THREAD_SLEEP_TIME);
                //Wait until other thread consumes a map
            }
        }

    }

    private void PlacePuzzle(Texture[,] map)
    {
        //TODO make algo that decides which wall texture to use
        //for loops check where the walls are and adds them to tilemap
        Vector3Int coordinate = puzzle.WorldToCell(transform.position);
        for(int i = 0; i < width; i++)
        {
            //resets position with respect to the world
            coordinate = puzzle.WorldToCell(transform.position);
            coordinate.x += i-5;
            coordinate.y += 10;
            for(int j = 0; j <= height; j++)
            {
                coordinate.y --;
                if(j == 0)
                {
                  if(i == 4)
                  {
                    puzzle.SetTile(coordinate, brRound);
                  }
                  else if(i == 5)
                  {
                    puzzle.SetTile(coordinate, bBar);
                  }
                  else if(i == 6)
                  {
                    puzzle.SetTile(coordinate, blRound);
                  }
                  else
                  {
                    puzzle.SetTile(coordinate, none);
                  }
                }
                else if(map[j-1,i] != Texture.empty)
                {
                  switch(map[j-1,i])
                  {
                    case Texture.tBar:
                      puzzle.SetTile(coordinate, tBar);
                      break;
                    case Texture.rBar:
                      puzzle.SetTile(coordinate, rBar);
                      break;
                    case Texture.bBar:
                      puzzle.SetTile(coordinate, bBar);
                      break;
                    case Texture.lBar:
                      puzzle.SetTile(coordinate, lBar);
                      break;
                    case Texture.trBar:
                      puzzle.SetTile(coordinate, trBar);
                      break;
                    case Texture.brBar:
                      puzzle.SetTile(coordinate, brBar);
                      break;
                    case Texture.blBar:
                      puzzle.SetTile(coordinate, blBar);
                      break;
                    case Texture.tlBar:
                      puzzle.SetTile(coordinate, tlBar);
                      break;
                    case Texture.tlrBar:
                      puzzle.SetTile(coordinate, tlrBar);
                      break;
                    case Texture.tblBar:
                      puzzle.SetTile(coordinate, tblBar);
                      break;
                    case Texture.tbrBar:
                      puzzle.SetTile(coordinate, tbrBar);
                      break;
                    case Texture.blrBar:
                      puzzle.SetTile(coordinate, blrBar);
                      break;
                    case Texture.tblrBar:
                      puzzle.SetTile(coordinate, tblrBar);
                      break;
                    case Texture.trRound:
                      puzzle.SetTile(coordinate, trRound);
                      break;
                    case Texture.brRound:
                      puzzle.SetTile(coordinate, brRound);
                      break;
                    case Texture.tlRound:
                      puzzle.SetTile(coordinate, tlRound);
                      break;
                    case Texture.blRound:
                      puzzle.SetTile(coordinate, blRound);
                      break;
                    case Texture.tRound:
                      puzzle.SetTile(coordinate, tRound);
                      break;
                    case Texture.rRound:
                      puzzle.SetTile(coordinate, rRound);
                      break;
                    case Texture.bRound:
                      puzzle.SetTile(coordinate, bRound);
                      break;
                    case Texture.lRound:
                      puzzle.SetTile(coordinate, lRound);
                      break;
                    case Texture.rtRound:
                      puzzle.SetTile(coordinate, rtRound);
                      break;
                    case Texture.lbRound:
                      puzzle.SetTile(coordinate, lbRound);
                      break;
                    case Texture.rbRound:
                      puzzle.SetTile(coordinate, rbRound);
                      break;
                    case Texture.ltRound:
                      puzzle.SetTile(coordinate, ltRound);
                      break;
                    case Texture.tbRound:
                      puzzle.SetTile(coordinate, tbRound);
                      break;
                    case Texture.tbBar:
                      puzzle.SetTile(coordinate, tbBar);
                      break;
                    case Texture.lrBar:
                      puzzle.SetTile(coordinate, lrBar);
                      break;
                    case Texture.tBarblRound:
                      puzzle.SetTile(coordinate, tBarblRound);
                      break;
                    case Texture.tBarbrRound:
                      puzzle.SetTile(coordinate, tBarbrRound);
                      break;
                    case Texture.tBarbRound:
                      puzzle.SetTile(coordinate, tBarbRound);
                      break;
                    case Texture.rBartlRound:
                      puzzle.SetTile(coordinate, rBartlRound);
                      break;
                    case Texture.rBarblRound:
                      puzzle.SetTile(coordinate, rBarblRound);
                      break;
                    case Texture.rBarlRound:
                      puzzle.SetTile(coordinate, rBarlRound);
                      break;
                    case Texture.lBartrRound:
                      puzzle.SetTile(coordinate, lBartrRound);
                      break;
                    case Texture.lBarbrRound:
                      puzzle.SetTile(coordinate, lBarbrRound);
                      break;
                    case Texture.lBarrRound:
                      puzzle.SetTile(coordinate, lBarrRound);
                      break;
                    case Texture.bBartrRound:
                      puzzle.SetTile(coordinate, bBartrRound);
                      break;
                    case Texture.bBartlRound:
                      puzzle.SetTile(coordinate, bBartlRound);
                      break;
                    case Texture.bBartRound:
                      puzzle.SetTile(coordinate, bBartRound);
                      break;
                    case Texture.tlBarbrRound:
                      puzzle.SetTile(coordinate, tlBarbrRound);
                      break;
                    case Texture.trBarblRound:
                      puzzle.SetTile(coordinate, trBarblRound);
                      break;
                    case Texture.brBartlRound:
                      puzzle.SetTile(coordinate, brBartlRound);
                      break;
                    case Texture.blBartrRound:
                      puzzle.SetTile(coordinate, blBartrRound);
                      break;
                    case Texture.trblRound:
                      puzzle.SetTile(coordinate, trblRound);
                      break;
                    case Texture.tlbrRound:
                      puzzle.SetTile(coordinate, tlbrRound);
                      break;
                    case Texture.none:
                      puzzle.SetTile(coordinate, none);
                      break;
                    default:
                      puzzle.SetTile(coordinate, wall);
                      break;
                  }
                }
            }
        }
    }

    void Start()
    {
        // if(OnlyMap != null){
        //   Destroy(this.gameObject);
        //   return;
        // }
        // OnlyMap = this;
        // DontDestroyOnLoad(this.gameObject);

        seed = PlayerGameManager.SeedValue;
        a = new Algorithm.Algorithm(seed);
        maps = new Queue<Texture[,]>(MAX_QUEUE_SIZE);
        mutex = new Mutex();
        creator = new Thread(AddMazeToQueue);
        creator.Start();
        Debug.Log(seed);
        width = 11;
        height = 18;
        loaded = true;
    }

    void Update()
    {
        // if(Input.GetMouseButtonDown(0)) //L click places map
        // {
        //     ClearMap(true);
        //     NextLevel();
        // }
        //if(Input.GetMouseButtonDown(1)) //R click clears map
        //{
        //    ClearMap(true);
        //}
    }

    public void ClearMap(bool complete)
    {
        puzzle.ClearAllTiles();
        loaded = false;
    }

    public bool MapLoaded()
    {
        return loaded;
    }

    private Texture[,] ConnectedTexture(int[,] nextMap)
    {
      Texture[,] textured = new Texture[height,width];
        for(int i = 0; i < width; i++)
        {
            for(int j = 0; j < height; j++)
            {
              if(nextMap[j,i] == 0)
              {
                textured[j,i] = Texture.empty;
              }
              else
              {
                bool[,] surrounding = new bool[3,3];
                for(int x = i-1; x <= i+1; x++)
                {
                  for(int y = j-1; y <= j+1; y++)
                  {
                    if(x >= width || x < 0 || y >= height || y < 0)
                    {
                      surrounding[y-j+1,x-i+1] = true;
                    }
                    //making an array for the surrounding walls
                    else if(nextMap[y,x] != 0)
                    {
                      surrounding[y-j+1,x-i+1] = true;
                    }
                    else
                    {
                      surrounding[y-j+1,x-i+1] = false;
                    }
                  }
                }
                if(!surrounding[0,1] && surrounding[1,0] && surrounding[2,0] && surrounding[2,1] &&
                surrounding[1,2] && surrounding[2,2])
                {
                  textured[j,i] = Texture.tBar;
                }
                else if(!surrounding[1,2] &&surrounding[0,0] && surrounding[0,1] && surrounding[1,0] &&
                surrounding[2,0] && surrounding[2,1])
                {
                  textured[j,i] = Texture.rBar;
                }
                else if(!surrounding[2,1] &&surrounding[0,0] && surrounding[1,0] && surrounding[0,1] &&
                surrounding[0,2] && surrounding[1,2])
                {
                  textured[j,i] = Texture.bBar;
                }
                else if(!surrounding[1,0] &&surrounding[0,1] && surrounding[0,2] && surrounding[1,2] &&
                surrounding[2,1] && surrounding[2,2])
                {
                  textured[j,i] = Texture.lBar;
                }
                else if(!surrounding[0,1] && !surrounding[1,2] && surrounding[1,0] && surrounding[2,0] &&
                surrounding[2,1])
                {
                  textured[j,i] = Texture.trBar;
                }
                else if(!surrounding[2,1] && !surrounding[1,2] && surrounding[0,0] && surrounding[0,1] &&
                surrounding[1,0])
                {
                  textured[j,i] = Texture.brBar;
                }
                else if(!surrounding[2,1] && !surrounding[1,0] && surrounding[0,1] && surrounding[0,2] &&
                surrounding[1,2])
                {
                  textured[j,i] = Texture.blBar;
                }
                else if(!surrounding[1,0] && !surrounding[0,1] && surrounding[1,2] && surrounding[2,1] &&
                surrounding[2,2])
                {
                  textured[j,i] = Texture.tlBar;
                }
                else if(!surrounding[1,0] && !surrounding[0,1] && !surrounding[1,2] && surrounding[2,1])
                {
                  textured[j,i] = Texture.tlrBar;
                }
                else if(!surrounding[1,0] && !surrounding[0,1] && !surrounding[2,1] && surrounding[1,2])
                {
                  textured[j,i] = Texture.tblBar;
                }
                else if(!surrounding[1,2] && !surrounding[0,1] && !surrounding[2,1] && surrounding[1,0])
                {
                  textured[j,i] = Texture.tbrBar;
                }
                else if(!surrounding[1,0] && !surrounding[1,2] && !surrounding[2,1] && surrounding[0,1])
                {
                  textured[j,i] = Texture.blrBar;
                }
                else if(!surrounding[1,2] && !surrounding[0,1] && !surrounding[2,1] && !surrounding[1,0])
                {
                  textured[j,i] = Texture.tblrBar;
                }
                else if(!surrounding[0,2] && surrounding[0,0] && surrounding[0,1] && surrounding[1,0]
                && surrounding[1,2] && surrounding[2,0] && surrounding[2,1] && surrounding[2,2])
                {
                  textured[j,i] = Texture.trRound;
                }
                else if(!surrounding[2,2] && surrounding[0,0] && surrounding[0,1] && surrounding[1,0]
                && surrounding[1,2] && surrounding[2,0] && surrounding[2,1] && surrounding[0,2])
                {
                  textured[j,i] = Texture.brRound;
                }
                else if(!surrounding[2,0] && surrounding[0,0] && surrounding[0,1] && surrounding[1,0]
                && surrounding[1,2] && surrounding[2,2] && surrounding[2,1] && surrounding[0,2])
                {
                  textured[j,i] = Texture.blRound;
                }
                else if(!surrounding[0,0] && surrounding[0,2] && surrounding[0,1] && surrounding[1,0]
                && surrounding[1,2] && surrounding[2,0] && surrounding[2,1] && surrounding[2,2])
                {
                  textured[j,i] = Texture.tlRound;
                }
                else if(!surrounding[0,2] && !surrounding[0,0] && surrounding[0,1] && surrounding[1,0]
                && surrounding[1,2] && surrounding[2,0] && surrounding[2,1] && surrounding[2,2])
                {
                  textured[j,i] = Texture.tRound;
                }
                else if(!surrounding[0,2] && surrounding[0,0] && surrounding[0,1] && surrounding[1,0]
                && surrounding[1,2] && surrounding[2,0] && surrounding[2,1] && !surrounding[2,2])
                {
                  textured[j,i] = Texture.rRound;
                }
                else if(surrounding[0,2] && surrounding[0,0] && surrounding[0,1] && surrounding[1,0]
                && surrounding[1,2] && !surrounding[2,0] && surrounding[2,1] && !surrounding[2,2])
                {
                  textured[j,i] = Texture.bRound;
                }
                else if(surrounding[0,2] && !surrounding[0,0] && surrounding[0,1] && surrounding[1,0]
                && surrounding[1,2] && !surrounding[2,0] && surrounding[2,1] && surrounding[2,2])
                {
                  textured[j,i] = Texture.lRound;
                }
                else if( !surrounding[0,0] && surrounding[0,1] && !surrounding[0,2] && surrounding[1,0]
                && surrounding[1,2] && surrounding[2,0] && surrounding[2,1] && !surrounding[2,2])
                {
                  textured[j,i] = Texture.rtRound;
                }
                else if( !surrounding[0,0] && surrounding[0,1] && surrounding[0,2] && surrounding[1,0]
                && surrounding[1,2] && !surrounding[2,0] && surrounding[2,1] && !surrounding[2,2])
                {
                  textured[j,i] = Texture.lbRound;
                }
                else if( surrounding[0,0] && surrounding[0,1] && !surrounding[0,2] && surrounding[1,0]
                && surrounding[1,2] && !surrounding[2,0] && surrounding[2,1] && !surrounding[2,2])
                {
                  textured[j,i] = Texture.rbRound;
                }
                else if( !surrounding[0,0] && surrounding[0,1] && !surrounding[0,2] && surrounding[1,0]
                && surrounding[1,2] && !surrounding[2,0] && surrounding[2,1] && surrounding[2,2])
                {
                  textured[j,i] = Texture.ltRound;
                }
                else if( !surrounding[0,0] && surrounding[0,1] && !surrounding[0,2] && surrounding[1,0]
                && surrounding[1,2] && !surrounding[2,0] && surrounding[2,1] && !surrounding[2,2])
                {
                  textured[j,i] = Texture.tbRound;
                }
                else if( !surrounding[0,1] && surrounding[1,0] && surrounding[1,2] && !surrounding[2,1])
                {
                  textured[j,i] = Texture.tbBar;
                }
                else if( surrounding[0,1] && !surrounding[1,0] && !surrounding[1,2] && surrounding[2,1])
                {
                  textured[j,i] = Texture.lrBar;
                }
                else if( !surrounding[0,1] && surrounding[1,0] && surrounding[1,2] && !surrounding[2,0]
                && surrounding[2,1] && surrounding[2,2])
                {
                  textured[j,i] = Texture.tBarblRound;
                }
                else if(!surrounding[0,1] && surrounding[1,0] && surrounding[1,2] && surrounding[2,0]
                && surrounding[2,1] && !surrounding[2,2])
                {
                  textured[j,i] = Texture.tBarbrRound;
                }
                else if(!surrounding[0,1] && surrounding[1,0] && surrounding[1,2] && !surrounding[2,0]
                && surrounding[2,1] && !surrounding[2,2])
                {
                  textured[j,i] = Texture.tBarbRound;
                }
                else if( !surrounding[0,0] && surrounding[0,1] && surrounding[1,0]
                && !surrounding[1,2] && surrounding[2,0] && surrounding[2,1])
                {
                  textured[j,i] = Texture.rBartlRound;
                }
                else if( surrounding[0,0] && surrounding[0,1] && surrounding[1,0]
                && !surrounding[1,2] && !surrounding[2,0] && surrounding[2,1])
                {
                  textured[j,i] = Texture.rBarblRound;
                }
                else if( !surrounding[0,0] && surrounding[0,1] && surrounding[1,0]
                && !surrounding[1,2] && !surrounding[2,0] && surrounding[2,1])
                {
                  textured[j,i] = Texture.rBarlRound;
                }
                else if( surrounding[0,1] && !surrounding[0,2] && !surrounding[1,0]
                && surrounding[1,2] && surrounding[2,1] && surrounding[2,2])
                {
                  textured[j,i] = Texture.lBartrRound;
                }
                else if( surrounding[0,1] && surrounding[0,2] && !surrounding[1,0]
                && surrounding[1,2] && surrounding[2,1] && !surrounding[2,2])
                {
                  textured[j,i] = Texture.lBarbrRound;
                }
                else if( surrounding[0,1] && !surrounding[0,2] && !surrounding[1,0]
                && surrounding[1,2] && surrounding[2,1] && !surrounding[2,2])
                {
                  textured[j,i] = Texture.lBarrRound;
                }
                else if( surrounding[0,0] && surrounding[0,1] && !surrounding[0,2]
                && surrounding[1,0] && surrounding[1,2] && !surrounding[2,1])
                {
                  textured[j,i] = Texture.bBartrRound;
                }
                else if( !surrounding[0,0] && surrounding[0,1] && surrounding[0,2]
                && surrounding[1,0] && surrounding[1,2] && !surrounding[2,1])
                {
                  textured[j,i] = Texture.bBartlRound;
                }
                else if( !surrounding[0,0] && surrounding[0,1] && !surrounding[0,2]
                && surrounding[1,0] && surrounding[1,2] && !surrounding[2,1])
                {
                  textured[j,i] = Texture.bBartRound;
                }
                else if( surrounding[0,0] && surrounding[0,1] && surrounding[0,2]
                && surrounding[1,0] && surrounding[1,2] && !surrounding[2,1])
                {
                  textured[j,i] = Texture.tlBarbrRound;
                }
                else if( !surrounding[0,1] && !surrounding[1,0] && surrounding[1,2]
                && surrounding[2,1] && !surrounding[2,2])
                {
                  textured[j,i] = Texture.tlBarbrRound;
                }
                else if( !surrounding[0,1] && surrounding[1,0] && !surrounding[1,2]
                && !surrounding[2,0] && surrounding[2,1])
                {
                  textured[j,i] = Texture.trBarblRound;
                }
                else if( !surrounding[0,0] && surrounding[0,1] && surrounding[1,0]
                && !surrounding[1,2] && !surrounding[2,1])
                {
                  textured[j,i] = Texture.brBartlRound;
                }
                else if( surrounding[0,1] && !surrounding[0,2] && !surrounding[1,0]
                && surrounding[1,2] && !surrounding[2,1])
                {
                  textured[j,i] = Texture.blBartrRound;
                }
                else if( surrounding[0,0] && surrounding[0,1] && !surrounding[0,2] && surrounding[1,0]
                && surrounding[1,2] && !surrounding[2,0] && surrounding[2,1] && surrounding[2,2] )
                {
                  textured[j,i] = Texture.trblRound;
                }
                else if( !surrounding[0,0] && surrounding[0,1] && surrounding[0,2] && surrounding[1,0]
                && surrounding[1,2] && surrounding[2,0] && surrounding[2,1] && !surrounding[2,2] )
                {
                  textured[j,i] = Texture.tlbrRound;
                }

                else if(surrounding[2,2] && surrounding[0,0] && surrounding[0,1] && surrounding[1,0]
                && surrounding[1,2] && surrounding[2,0] && surrounding[2,1] && surrounding[0,2])
                {
                  textured[j,i] = Texture.none;
                }
                else
                {
                  textured[j,i] = Texture.tblrBar;
                }

              }
            }
        }
      return(textured);
    }
}
