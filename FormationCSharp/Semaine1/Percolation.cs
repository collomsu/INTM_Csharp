using System;
using System.Collections.Generic;

/// <summary>
/// Summary description for Class1
/// </summary>
namespace Semaine1
{
    internal class Percolation
    {
        public struct Perco
        {
            public bool[][] open;
            public bool[][] full;

            public Perco(bool[][] open, bool[][] full)
            {
                this.open = open;
                this.full = full;
            }
        }

        public Perco percolation;

        public Percolation(Perco percolation)
        {
            this.percolation = percolation;
        }

        /// <summary>
        /// Retourne si la case ciblée est ouverte ou non.
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns>bool</returns>
        public bool IsOpen(int i, int j)
        {
            return percolation.open[i][j];
        }

        /// <summary>
        /// Retourne si la case ciblée est pleine ou non.
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns>bool</returns>
        public bool isFull(int i, int j)
        {
            return percolation.full[i][j];
        }

        /// <summary>
        /// Retourne si la percolation a eu lieu ou non.
        /// </summary>
        /// <returns>bool</returns>
        public bool Percolate()
        {
            int lastIdx = percolation.full.Length - 1;

            for (int i = 0; i < percolation.full[lastIdx].Length; i++)
            {
                if (isFull(lastIdx, i))
                {
                    return true;
                }
            }

            return false;
        }

        public List<KeyValuePair<int, int>> CloseNeighbors(int i, int j)
        {
            List<KeyValuePair<int, int>> poss = new List<KeyValuePair<int, int>>();
            if (i > 0)
            {
                poss.Add(new KeyValuePair<int, int>(i-1, j));
            }

            if (i < percolation.open.Length - 1)
            {
                poss.Add(new KeyValuePair<int, int>(i + 1, j));
            }

            if (j > 0)
            {
                poss.Add(new KeyValuePair<int, int>(i, j - 1));
            }

            if (j < percolation.open[0].Length - 1)
            {
                poss.Add(new KeyValuePair<int, int>(i, j + 1));
            }

            return poss;
        }
    }
}