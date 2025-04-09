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

        /// <summary>
        /// Retourne la matrice resultante de la fusion des deux vecteurs passés en paramètre.
        /// </summary>
        /// <param name="leftVector"></param>
        /// <param name="rightVector"></param>
        /// <returns></returns>
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


        /// <summary>
        /// Retourne la matrice resultante de l'addition des deux matrices passées en paramètre.
        /// </summary>
        /// <param name="leftVector"></param>
        /// <param name="rightVector"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Retourne la matrice resultante de la soustraction des deux matrices passées en paramètre.
        /// </summary>
        /// <param name="leftVector"></param>
        /// <param name="rightVector"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Retourne la matrice resultante de la multiplication des deux matrices passées en paramètre.
        /// </summary>
        /// <param name="leftVector"></param>
        /// <param name="rightVector"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Affiche la matrice passé en paramètre.
        /// </summary>
        /// <param name="matrix"></param>
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

        /// <summary>
        /// Retourne l'ensemble des nombres premiers compris entre 1 et n compris.
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
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

        // Exercice 4


        public struct Qcm
        {
            public string question;
            public string[] answers;
            public int solution;
            public int weight;

            public Qcm(string question, string[] answers, int solution, int weight)
            {
                this.question = question;
                this.answers = answers;
                this.solution = solution;
                this.weight = weight;
            }
        }

        /// <summary>
        /// Vérifie si le qcm est valide.
        /// </summary>
        /// <param name="qcm"></param>
        /// <returns></returns>
        public bool QcmValidity(Qcm qcm)
        {
            if (qcm.solution < 0 || qcm.solution >= qcm.answers.Length)
            {
                return false;
            }
            else if (qcm.weight <= 0)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Affiche le QCM et demande la réponse à l'utilisateur. Un score est retourné en fonction de la réponse donnée.
        /// </summary>
        /// <param name="qcm"></param>
        /// <returns></returns>
        public int AskQuestion(Qcm qcm)
        {
            if (!QcmValidity(qcm))
            {
                throw new ArgumentException();
            }

            int reponse = 0;
            int points = qcm.weight;

            Console.WriteLine();

            while (reponse != qcm.solution + 1)
            {
                Console.WriteLine(qcm.question);
                for (int i = 0; i < qcm.answers.Length; i++)
                {
                    Console.Write($"{i+1}. {qcm.answers[i]} ");
                }
                Console.Write("Réponse : ");
                reponse = int.Parse(Console.ReadLine());

                if (reponse != qcm.solution + 1)
                {
                    Console.WriteLine("Réponse invalide !");
                    points = 0;
                }
            }

            return points;
        }

        /// <summary>
        /// Pose les questions de chaque qcm de la liste, et affiche le score de l'utilisateur ainsi que le score maximum.
        /// </summary>
        /// <param name="qcm"></param>
        public void AskQuestions(Qcm[] qcm)
        {
            int maxScore = 0;
            int score = 0;
            for (int i = 0; i < qcm.Length; i++)
            {
                maxScore += qcm[i].weight;
                score += AskQuestion(qcm[i]);
            }
            Console.WriteLine($"Résultat du questionnaire : {score} / {maxScore}");
        }
    }
}