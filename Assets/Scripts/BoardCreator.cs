﻿using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;

public class BoardCreator : MonoBehaviour{


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
      lbRound,rbRound,ltRound,tbRound,none,empty}
      //finished: tBar,rBar,bBar,lBar,trBar,brBar,
      //blBar,tlBar,tlrBar,tblBar,tbrBar,blrBar,tblrBar,trRound,
      //brRound,blRound,tlRound,tRound,rRound,bRound,lRound,rtRound,
      //lbRound,rbRound,ltRound,tbRound,none
    private Texture[,] puzzleMap;

    private const int MAX_QUEUE_SIZE = 20;
    private const int THREAD_SLEEP_TIME = 25;


    public void NextLevel()
    {
        //Debug.Log(maps.Count);
        if(!loaded)
        {
            Debug.Log(maps.Count);
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
            coordinate.y += 9;
            for(int j = 0; j < height; j++)
            {
                coordinate.y --;
                if(map[j,i] != Texture.empty)
                {
                  switch(map[j,i])
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
        a = new Algorithm.Algorithm();
        maps = new Queue<Texture[,]>(MAX_QUEUE_SIZE);
        mutex = new Mutex();
        creator = new Thread(AddMazeToQueue);
        creator.Start();
        seed = a.Seed;
        Debug.Log(seed);
        width = 11;
        height = 18;
        loaded = true;
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0)) //L click places map
        {
            ClearMap(true);
            NextLevel();
        }
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
                && surrounding[1,2] && surrounding[2,0] && surrounding[2,1] && !surrounding[2,2])
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
