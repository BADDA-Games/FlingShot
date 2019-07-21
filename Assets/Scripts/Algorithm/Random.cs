using System;
using System.Collections.Generic;
using Pair = System.Tuple<int, int>;
using PairList = System.Collections.Generic.List<System.Tuple<int, int>>;
using Directions = System.Collections.Generic.List<System.Collections.Generic.List<System.Collections.Generic.List<char>>>;
using Bools = System.Collections.Generic.List<System.Collections.Generic.List<bool>>;

namespace Algorithm
{
    public class Random
    {
        public long Seed { get; set; }
        public long InitialSeed { get; }

        public Random(int seed)
        {
            Seed = seed;
        }

        public int Generate(long low, long high)
        {
            long mod = high - low + 1;
            if(Seed < 1)
            {
                Seed = (low + high - Seed + 1999) * 1582307 % 55555333;
            }
            Seed = (3515366 * Seed + 12345) % 99999989;
            int value = (int)((Seed % mod) + low);
            return value;
        }

        public int ChooseFrom(PairList ranges)
        {
            int choice = Generate(ranges[0].Item1, ranges[ranges.Count-1].Item2);
            for(int i = 0; i < ranges.Count; i++)
            {
                if(Util.Between(choice, ranges[i]))
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
