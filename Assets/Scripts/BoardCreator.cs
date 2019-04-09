using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;

public class BoardCreator : MonoBehaviour{

  private int[,] puzzleMap;
  public Vector3Int tmapSize;

  public Tilemap puzzle;
  public Tile wall;

  private Algorithm.Algorithm a;

  int width;
  int height;
  int seed;

  public void doSim()
  {
    //clearMap(false);
    if(puzzleMap == null)
    {
      Vector3Int coordinate = puzzle.WorldToCell(transform.position);
      puzzleMap = a.Generate();
      //TODO make algo that decides which wall texture to use
      //for loops check where the walls are and adds them to tilemap
      for(int i = 0; i < width; i++)
      {
        coordinate = puzzle.WorldToCell(transform.position); //resets position with respect to the world
        coordinate.x = coordinate.x+i-5;
        coordinate.y += 9;
        for(int j = 0; j < height; j++)
        {
          coordinate.y --;
          if(puzzleMap[j,i] == 1)
          {
            puzzle.SetTile(coordinate, wall);
          }
        }
      }
      // coordinate = puzzle.WorldToCell(transform.position);
      // coordinate.x +=4-5;
      // coordinate.y += 6;
      // puzzle.SetTile(coordinate, wall);
    }
  }

  void Start()
  {
      a = new Algorithm.Algorithm();
      seed = a.Seed;
      Debug.Log(seed);
      width = 11;
      height = 18;
  }

  void Update()
  {
    if(Input.GetMouseButtonDown(0)) //L click places map
    {
      doSim();
    }
    if(Input.GetMouseButtonDown(1)) //R click clears map
    {
      clearMap(true);
    }
  }

  public void clearMap(bool complete)
  {
    puzzle.ClearAllTiles();
    if(complete)
    {
      puzzleMap = null;
    }
  }
}
