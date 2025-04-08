using System;
using System.Collections.Generic;

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
                if(i == (tete + queue) / 2)
                {
                    i--;
                }
                else
                {
                    i = (tete + queue) / 2;
                }

                if (valeur == i)
                {
                    return i;
                }
                else if (valeur < i)
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
    }
}