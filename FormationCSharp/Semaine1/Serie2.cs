using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Summary description for Serie2
/// </summary>
namespace Semaine1
{
    internal class Serie2
    {
        // Exerice 1


        /// <summary>
        /// Recherche linéaire de l'entier valeur dans un tableau donné.
        /// </summary>
        /// <param name="tableau"></param>
        /// <param name="valeur"></param>
        public int LinearSearch(int[] tableau, int valeur)
        {
            if (tableau == null || tableau.Length == 0)
            {
                return -1;
            }

            for (int i = 0; i < tableau.Length; i++)
            {
                if (tableau[i] == valeur)
                {
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// Rechaire dichotomique de l'entier valeur dans un tableau donné.
        /// </summary>
        /// <param name="tableau"></param>
        /// <param name="valeur"></param>
        public int BinarySearch(int[] tableau, int valeur)
        {
            if (tableau == null || tableau.Length == 0)
            {
                return -1;
            }

            int tete = 0, queue = tableau.Length - 1, i = 0;

            while (queue - tete >= 1)
            {

                if ((queue + tete) / 2 == i)
                {
                    i++;
                }
                else
                {
                    i = (queue + tete) / 2;
                }
                
                if (valeur == tableau[i])
                {
                    return i;
                }
                else if (valeur < tableau[i])
                {
                    queue = i;
                }
                else
                {
                    tete = i;
                }
            }

            return -1;
        }

        // Exercice 2

        public int[][] BuildingMatrix(int[] leftVector, int[] rightVector) 
        {
            int[][] res = new int[leftVector.Length][];
            for (int i = 0; i < res.Length; i++)
            {
                res[i] = new int[rightVector.Length];
            }

            for (int i = 0; i < leftVector.Length; i++)
            {
                for (int j = 0; j < rightVector.Length; j++)
                {
                    res[i][j] = leftVector[i] * rightVector[j];
                    Console.WriteLine($"{res[i][j]}");
                }
            }

            return res;
        }

        public int[][] Addition(int[][] leftVector, int[][] rightVector)
        {
            int[][] res = new int[leftVector.Length][];
            for (int i = 0; i < res.Length; i++)
            {
                res[i] = new int[rightVector.Length];
            }

            for (int i = 0; i < leftVector.Length; i++)
            {
                for (int j = 0; j < leftVector[i].Length; j++)
                {
                    res[i][j] = leftVector[i][j] + rightVector[i][j];
                }
            }

            return res;
        }

        public int[][] Substraction(int[][] leftVector, int[][] rightVector)
        {
            int[][] res = new int[leftVector.Length][];
            for (int i = 0; i < res.Length; i++)
            {
                res[i] = new int[rightVector.Length];
            }

            for (int i = 0; i < leftVector.Length; i++)
            {
                for (int j = 0; j < leftVector[i].Length; j++)
                {
                    res[i][j] = leftVector[i][j] - rightVector[i][j];
                }
            }

            return res;
        }

        public int[][] Multiplication(int[][] leftVector, int[][] rightVector)
        {
            int[][] res = new int[leftVector.Length][];
            for (int i = 0; i < res.Length; i++)
            {
                res[i] = new int[rightVector.Length];
            }

            for (int i = 0; i < leftVector.Length; i++)
            {
                for (int j = 0; j < leftVector[i].Length; j++)
                {
                    
                    for (int k = 0; k < rightVector[j].Length; k++)
                    {
                        res[i][k] += leftVector[i][j] * rightVector[j][k];
                    }
                }
            }

            return res;
        }

        public void displayMatrix(int[][] matrix)
        {
            for (int i = 0; i < matrix.Length; i++)
            {
                Console.Write("{ ");
                for (int j = 0; j < matrix[i].Length; j++)
                {
                    Console.Write($"{matrix[i][j]} ");
                }
                Console.WriteLine("}");
            }
            Console.WriteLine();
        }

        // Exercice 3

        public int[] EratosthenesSieve(int n)
        {
            if (n < 2)
            {
                return null;
            }

            List<int> bres = new List<int>();

            for (int j = 2; j < n; j++)
            {
                bres.Add(j);
            }

            int i = 1;
            int m = bres.ElementAt(bres.Count - 1);
            
            while (i <= Math.Sqrt(m))
            {
                i++;
                for (int j = 0; j < bres.Count; j++)
                {
                    if (bres.ElementAt(j) != i && bres.ElementAt(j) % i == 0)
                    {
                        bres.Remove(bres.ElementAt(j));
                    }
                }
            }

            return bres.ToArray();
        }
    }
}