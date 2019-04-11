using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;

public class BoardCreator : MonoBehaviour{

    private int[,] puzzleMap;
    public Vector3Int tmapSize;

    public Tilemap puzzle;
    public Tile wall;

    private Algorithm.Algorithm a;

    private int width;
    private int height;
    private int seed;
    private bool loaded;

    private Queue<int[,]> maps;
    private Mutex mutex;
    private Thread creator;

    private const int MAX_QUEUE_SIZE = 10;


    public void NextLevel()
    {
        //Debug.Log(maps.Count);
        if(puzzleMap == null)
        {
            while(maps.Count <= 0)
            {
                //Wait for other thread to produce a map
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
                mutex.WaitOne();
                maps.Enqueue(nextMap);
                mutex.ReleaseMutex();
            }
            else
            {
                //Wait until other thread to consume a map
            }
        }

    }

    private void PlacePuzzle(int[,] map)
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
                if(map[j,i] == 1)
                {
                    puzzle.SetTile(coordinate, wall);
                }
            }
        }
    }

    void Start()
    {
        a = new Algorithm.Algorithm();
        maps = new Queue<int[,]>(MAX_QUEUE_SIZE);
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
            NextLevel();
        }
        if(Input.GetMouseButtonDown(1)) //R click clears map
        {
            ClearMap(true);
        }
    }

    public void ClearMap(bool complete)
    {
        puzzle.ClearAllTiles();
        if(complete)
        {
            puzzleMap = null;
        }
        loaded = false;
    }

    public bool MapLoaded()
    {
        return loaded;
    }
}
