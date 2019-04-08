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

  Algorithm.Algorithm a = new Algorithm.Algorithm(1345);

  int width;
  int height;

  public void doSim()
  {
    //clearMap(false);
    width = 11;
    height = 18;
    if(puzzleMap == null)
    {
      Vector3Int coordinate = puzzle.WorldToCell(transform.position);
      puzzleMap = a.Generate();
      //TODO make algo that decides which wall texture to use
      //for loops check where the walls are and adds them to tilemap
      for(int i = 0; i < width; i++)
      {
        coordinate = puzzle.WorldToCell(transform.position); //resets possition with respect to the world
        coordinate.x = coordinate.x+i-5;
        coordinate.y += 9;
        for(int j = 0; j < height; j++)
        {
          coordinate.y --;
          Debug.Log(j+" "+i);
          if(puzzleMap[j,i] == 1)
          {
            puzzle.SetTile(coordinate, wall);
          }
        }
      }
    }
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
