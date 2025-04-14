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
        public bool IsFull(int i, int j)
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
                if (IsFull(lastIdx, i))
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
                poss.Add(new KeyValuePair<int, int>(i - 1, j));
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

        // 3) a) Dans le pire cas on devra parcourir pratiquement tout notre matrice en parcourant plusieurs fois des cases voisines déjà explorées.
        // 3) b) Cela voudrait dire que pratiquement l'entièreté des cases sont ouverts et vide excepté pour la ligne des cases supérieure, ce qui est très peu probable.
        public void Open(int i, int j)
        {
            percolation.open[i][j] = true;

            List<KeyValuePair<int, int>> neighbors = CloseNeighbors(i, j);

            if (i == 0)
            {
                Flood(i, j);
            }
            else
            {
                foreach (KeyValuePair<int, int> pos in neighbors)
                {
                    if (IsFull(pos.Key, pos.Value))
                    {
                        Flood(i, j);
                        break;
                    }
                }
            }
        }

        public void Flood(int i, int j)
        {
            if (IsOpen(i, j) && !IsFull(i, j))
            {
                percolation.full[i][j] = true;
                foreach (KeyValuePair<int, int> pos in CloseNeighbors(i, j))
                {
                    Flood(pos.Key, pos.Value);
                }
            }
        }

        public struct PclData
        {
            public double mean;
            public double std;

            public PclData(double mean, double std)
            {
                this.mean = mean;
                this.std = std;
            }
        }

        public double PercolationValue(int size)
        {
            bool[][] open =
            {
                new bool[] { false, false, false, false, false, false },
                new bool[] { false, false, false, false, false, false },
                new bool[] { false, false, false, false, false, false },
                new bool[] { false, false, false, false, false, false },
                new bool[] { false, false, false, false, false, false },
                new bool[] { false, false, false, false, false, false }
            };
            bool[][] full =
            {
                new bool[] { false, false, false, false, false, false },
                new bool[] { false, false, false, false, false, false },
                new bool[] { false, false, false, false, false, false },
                new bool[] { false, false, false, false, false, false },
                new bool[] { false, false, false, false, false, false },
                new bool[] { false, false, false, false, false, false }
            };

            percolation = new Perco(open, full);

            Random rnd = new Random();

            int nbCasesOuvertes = 0;
            int x = 0;
            int y = 0;

            do
            {
                x = rnd.Next(0, size);
                y = rnd.Next(0, size);

                if (!IsOpen(x ,y))
                {
                    Open(x, y);
                    nbCasesOuvertes++;
                }
            } while (!Percolate());

            return (double)nbCasesOuvertes / (size * size);
        }

        public PclData MeanPercolationValue(int size, int t)
        {
            PclData pcl = new PclData(0, 0);
            List<double> list = new List<double>();
            double res = 0;

            for (int i = 0; i < t; i++)
            {
                res = PercolationValue(size);
                pcl.mean += res;
                list.Add(res);
            }

            pcl.mean /= t;

            foreach (double val in list)
            {
                pcl.std += Math.Pow(val - pcl.mean, 2);
            }

            pcl.std = Math.Sqrt(pcl.std / t);

            return pcl;
        }
    }
}