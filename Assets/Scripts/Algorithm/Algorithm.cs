using System;
using System.Collections.Generic;
using Pair = System.Tuple<int, int>;
using PairList = System.Collections.Generic.List<System.Tuple<int, int>>;
using Directions = System.Collections.Generic.List<System.Collections.Generic.List<System.Collections.Generic.List<char>>>;
using Bools = System.Collections.Generic.List<System.Collections.Generic.List<bool>>;

namespace Algorithm
{
    public class Algorithm
    {
        // Initial seed, not current seed!
        public int Seed { get; }
        public int Level { get; set; }
        public int Difficulty { get; private set; }

        private int height;
        private int width;
        private GridGraph gg;
        private Random rand;

        public Algorithm()
        {
            System.Random sysrand = new System.Random();
            Seed = sysrand.Next(1, 99999989);
            Initialize();
        }

        public Algorithm(int seed)
        {
            Seed = seed;
            Initialize();
        }

        private void Initialize()
        {
            height = 16;
            width = 9;
            Level = 1;
            rand = new Random(Seed);
        }

        private int[,] ConvertArray()
        {
            int times = Level % 97;
            for (int i = 0; i < times; i++)
            {
                // This offsets the random number a different, fixed
                // amount of times. We do this so that if two seed paths
                // stumble upon the same seed and start producing the same maze
                // then the offset will prevent future concurrency
                rand.Generate(0, 1);
            }
            Level++;
            int[,] fullMap = new int[height + 2, width + 2];
            int[,] cellArray = gg.GetCellArray();

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    fullMap[i + 1, j + 1] = cellArray[j, i];
                }
            }
            for (int i = 0; i < width + 2; i++)
            {
                fullMap[0, i] = 1;
                fullMap[height + 1, i] = 1;
            }
            for (int j = 1; j < height + 1; j++)
            {
                fullMap[j, 0] = 1;
                fullMap[j, width + 1] = 1;
            }
            fullMap[0, width / 2 + 1] = 0;

            //+1 is offset for final move to get in goal
            Difficulty = gg.Difficulty() + 1;

            return fullMap;
        }

        private PairList MakeRanges(List<int> probabilities)
        {
            PairList ranges = new PairList();
            foreach (int p in probabilities)
            {
                if (ranges.Count == 0)
                {
                    ranges.Add(new Pair(0, probabilities[0]));
                }
                else
                {
                    int next = ranges[ranges.Count - 1].Item2 + 1;
                    ranges.Add(new Pair(next, next + p));
                }
            }
            return ranges;
        }

        private bool valid;
        private bool copy;

        private bool GoodMap(GridGraph g)
        {
            // Make static if this takes too long
            int min = 5;
            if(!g.Possible() || g.Difficulty() <= min)
            {
                return false;
            }
            if(g.CanGetStuck())
            {
                return false;
            }
            return true;
        }

        public int[,] Generate()
        {
            //gg = SkeletonMaze(5);
            gg = new GridGraph(width, height);
            Iterate();
            while (!GoodMap(gg)){
                gg = new GridGraph(width, height);
                Iterate();
            }
            gg.DetermineExtraPaths(rand);
            return ConvertArray();
        }

        // **RESOURCES**
        // Random - Generate, ChooseFrom
        // GridGraph - Copy Constructor, BuildPath, InDeg, OutDeg, TrapVertices,
        // CanGetStuck, FastestPath, FastestPathFrom, LongestPath,
        // VerticesInDirection, WallOf, Depth, Complexity, Difficulty,
        // BuiltDirections, MovableDirections, Possible

        private void Iterate()
        {
            void Process(GridGraph g)
            {
                valid = false;
                List<Tuple<Pair, int>> dists = g.Distance;
                while (!valid)
                {
                    if(dists.Count == 0)
                    {
                        //Console.WriteLine("ERROR - No good vertices.");
                        return;
                    }
                    List<int> probabilities = MapProbability(dists);
                    PairList ranges = MakeRanges(probabilities);
                    int choice = rand.ChooseFrom(ranges);
                    Pair vertex = dists[choice].Item1;
                    if(vertex != null)
                    {
                        if(TryBuild(g, vertex))
                        {
                            Check(g);
                        }
                        else
                        {
                            dists.RemoveAt(choice);
                        }
                    }
                    else
                    {
                        Console.WriteLine("ERROR - Could not choose a vertex.");
                        return;
                    }
                }
            }
            //TODO change to another looping condition
            for (int i = 0; i < 60; i++)
            {
                copy = false;
                GridGraph other = new GridGraph(gg);
                Process(other);
                if (copy)
                {
                    gg = other;
                }
            }
        }


        //TODO better probability function?
        private List<int> MapProbability(List<Tuple<Pair, int>> dists)
        {
            List<int> probabilities = new List<int>();
            foreach(Tuple<Pair, int> d in dists)
            {
                int l = d.Item2;
                probabilities.Add(1 + 2 * l * l);
            }
            return probabilities;
        }

        private bool TryBuild(GridGraph g, Pair v)
        {
            List<char> good = g.GoodDirections(v);
            List<int> probabilities = new List<int>();
            foreach (char c in good)
            {
                switch (c)
                {
                    case 'U':
                        probabilities.Add(3);
                        break;
                    case 'L':
                        probabilities.Add(2);
                        break;
                    case 'R':
                        probabilities.Add(2);
                        break;
                    case 'D':
                        probabilities.Add(1);
                        break;
                }
            }
            while (good.Count > 0)
            {
                PairList ranges = MakeRanges(probabilities);
                int choice = rand.ChooseFrom(ranges);
                char dir = good[choice]; // Ha!
                int max_length = g.LongestPath(v, dir, 1); //Change from 1?
                if (dir == 'D')
                {
                    //TODO more calculations into max length
                    max_length = Math.Min(max_length, 4);
                }
                else
                {
                    max_length = Math.Min(max_length, 6);
                }
                int length = rand.Generate(1, max_length);
                g.BuildPath(v, dir, length);
                return true;
                //TODO we want to try all directions, not just the one we first select
            }
            return false;
            //TODO we need to try again with different lengths and directions if it fails here

        }

        private void Check(GridGraph g)
        {
            Pair start = g.Start;
            int x = start.Item1;
            int y = start.Item2;
            bool left = g.is_wall[x - 1, y];
            bool right = g.is_wall[x + 1, y];
            bool up = g.is_wall[x, y - 1];
            if(!(left || right || up))
            {
                return;
            }
            if(g.Complexity() >= gg.Complexity())
            {
                valid = true;
                copy = true;
            }

        }

        //public GridGraph SkeletonMaze(int difficulty)
        //{
        //    gg = new GridGraph(width, height);
        //    SkeletonMazeRecursive(difficulty, difficulty, gg, gg.Start);
        //    return gg;
        //}

        //public void SkeletonMazeRecursive(int difficulty, int moves, GridGraph gg, Pair p)
        //{
        //    if(moves == 0)
        //    {
        //        return;
        //    }
        //    if(p == null)
        //    {
        //        return;
        //    }
        //    List<char> good = gg.GoodDirections(p);
        //    Pair next = null;
        //    switch (moves % 2)
        //    {
        //        case 0:
        //            if(good.Contains('L') && good.Contains('R'))
        //            {
        //                int lMax = gg.LongestPath(p, 'L', 10);
        //                int rMax = gg.LongestPath(p, 'R', 10);
        //                bool choice = rand.Generate(0, 1) != 0; //TODO update
        //                int length = choice ? rand.Generate(1, lMax) : rand.Generate(1, rMax);
        //                next = choice ? gg.BuildPath(p, 'L', length) : gg.BuildPath(p, 'R', length);
        //            }
        //            else if (good.Contains('L'))
        //            {
        //                int max = gg.LongestPath(p, 'L', 10);
        //                int length = rand.Generate(1, max);
        //                next = gg.BuildPath(p, 'L', length);
        //            }
        //            else if (good.Contains('R'))
        //            {
        //                int max = gg.LongestPath(p, 'R', 10);
        //                int length = rand.Generate(1, max);
        //                next = gg.BuildPath(p, 'R', length);
        //            }
        //            else
        //            {
        //                Debug.Log("Panic!");
        //            }
        //            break;
        //        case 1:
        //            if (good.Contains('U') && good.Contains('D'))
        //            {
        //                int uMax = gg.LongestPath(p, 'U', 10);
        //                int dMax = gg.LongestPath(p, 'D', 10);
        //                bool choice = rand.Generate(0, 1) != 0; //TODO change
        //                int length = choice ? rand.Generate(1, uMax) : rand.Generate(1, dMax);
        //                next = choice ? gg.BuildPath(p, 'U', length) : gg.BuildPath(p, 'D', length);
        //            }
        //            else if (good.Contains('U'))
        //            {
        //                int max = gg.LongestPath(p, 'U', 10);
        //                int length = rand.Generate(1, max);
        //                next = gg.BuildPath(p, 'U', length);
        //            }
        //            else if (good.Contains('D'))
        //            {
        //                int max = gg.LongestPath(p, 'D', 10);
        //                int length = rand.Generate(1, max);
        //                next = gg.BuildPath(p, 'D', length);
        //            }
        //            else
        //            {
        //                Debug.Log("Panic!");
        //            }
        //            break;
        //    }
        //    SkeletonMazeRecursive(difficulty, moves-1, gg, next);
        //}
    }
}
