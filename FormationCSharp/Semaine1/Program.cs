using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semaine1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Serie 1
            // Exercice 1
            BasicOperation(1, 2, '+');
            BasicOperation(1, 2, 'L');
            BasicOperation(1, 0, '/');

            IntegerDivision(8, 2);
            IntegerDivision(5, 2);
            IntegerDivision(8, 0);

            // Exercice 2
            Console.WriteLine(GoodDay(4));
            Console.WriteLine(GoodDay(7));
            Console.WriteLine(GoodDay(12));
            Console.WriteLine(GoodDay(15));
            Console.WriteLine(GoodDay(20));

            // Exercice 3
            PyramidConstruction(8, false);
            PyramidConstruction(12, true);

            // Exercice 4
            Console.WriteLine(Factorial(5));
            Console.WriteLine(FactorialRec(5));

            // Exercice 5
            DisplayPrimes();

            // Ecercice 6
            Console.WriteLine(Gcd(200, 50));
            Console.WriteLine(Gcd(485, 75));

            // Serie 2
            int[] tab = new int[] { 1, 4, 8, 2, 7 };
            int[] tab2 = new int[] { 1, 2, 8, 9, 12 };
            Serie2 serie2 = new Serie2();

            // Exercice 1
            Console.WriteLine(serie2.LinearSearch(tab, 2));
            Console.WriteLine(serie2.LinearSearch(tab, 12));

            Console.WriteLine(serie2.BinarySearch(tab2, 9));
            Console.WriteLine(serie2.BinarySearch(tab2, 15));

            Console.ReadKey();
        }

        // Exercice 1

        /// <summary>
        /// Réalisé l'opération donné par le caractère op entre deux valeurs a et b données.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="op"></param>
        static void BasicOperation(int a, int b, char op)
        {
            string res;

            switch(op)
            {
                case '+':
                    res = (a + b).ToString();
                    break;
                case '-':
                    res = (a - b).ToString();
                    break;
                case '*':
                    res = (a * b).ToString();
                    break;
                case '/':
                    if (b != 0)
                    {
                        res = (a / b).ToString();
                    }
                    else
                    {
                        res = "Opération invalide.";
                    }
                    break;
                default:
                    res = "Opération invalide.";
                    break;
            }
            Console.WriteLine($"{a} {op} {b} = {res}");
        }

        /// <summary>
        /// Effectue la division non nul entre deux entiers a et b donnés.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        static void IntegerDivision(int a, int b)
        {
            int r = a;
            int q = 0;

            if (b != 0)
            {
                while (r >= b)
                {
                    r -= b;
                    q++;
                }

                if (r > 0)
                {
                    Console.WriteLine($"{a} = {q} * {b} + {r}");
                }
                else
                {
                    Console.WriteLine($"{a} = {q} * {b}");
                }
            }
            else
            {
                Console.WriteLine($"{a} : {b} = Opération invalide");
            }
        }

        /// <summary>
        /// Affiche le calcul de l'entier a à la puissance b.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        static void Pow(int a, int b) 
        {
            if (b >= 0)
            {
                Console.WriteLine($"{a} ^ {b} = {Math.Pow(a,b)}");
            }
            else
            {
                Console.WriteLine("Opération invalide");
            }
        }

        // Exercice 2

        /// <summary>
        /// Renvoie l'heure de la journée avec un message personnalisé en fonction de la plage horaire.
        /// </summary>
        /// <param name="heure"></param>
        /// <returns></returns>
        static string GoodDay(int heure)
        {
            string message = $"Il est {heure} H, ";

            if (heure >= 0 && heure < 6)
            {
                message += "Merveilleuse nuit !";
            }
            else if (heure >= 6 &&  heure < 12)
            {
                message += "Bonne matinée !";
            }
            else if (heure == 12)
            {
                message += "Bon appétit !";
            }
            else if (heure >= 13 && heure <= 18)
            {
                message += "Profitez de votre après-midi !";
            }
            else
            {
                message += "Passez une bonne soirée !";
            }

            return message;
        }

        // Exercice 3

        /// <summary>
        /// Dessine une pyramide de hauteur n pouvant contenir des stries si isSmooth vaut vrai.
        /// </summary>
        /// <param name="n"></param>
        /// <param name="isSmooth"></param>
        static void PyramidConstruction(int n, bool isSmooth)
        {
            if (n > 0)
            {
                int j = 1;
                int gauche = 0, droite = 0;
                int baseN = 1 + (n - 1) * 2;
                char c = '+';

                do
                {
                    gauche = n - j;
                    droite = n - 1 + j;

                    if (isSmooth)
                    {
                        if (j % 2 == 0)
                        {
                            c = '-';
                        }
                        else
                        {
                            c = '+';
                        }
                    }

                    for (int i = 0; i < baseN; i++)
                    {
                        if (i >= gauche && i < droite)
                        {
                            Console.Write(c);
                        }
                        else
                        {
                            Console.Write(" ");
                        }
                    }
                    Console.WriteLine();

                    j++;
                } while (j <= n);
            }
            else
            {
                Console.WriteLine("n doit être supérieur à 0.");
            }
        }

        // Exercice 4

        /// <summary>
        /// Calcul la factorielle du nombre n positif.
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        static int Factorial(int n)
        {
            int res = 1;

            for (int i = 1; i <= n; i++)
            {
                res *= i;
            }

            return res;
        }

        /// <summary>
        /// Calcul récursivement la factorielle du nombre n positif.
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        static int FactorialRec(int n)
        {
            if (n == 0)
            {
                return 1;
            }
            else
            {
                return n * FactorialRec(n - 1);
            }
        }

        // Exerice 5

        /// <summary>
        /// Renvoie si le nombre passé en paramètre est premier ou non.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        static bool isPrime(int value)
        {
            int b = 2;
            for (; b <= Math.Sqrt(value); b++)
            {
                if (value % b == 0)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Affiche les nombres de 1 à 100 avec l'information de s'ils sont premier ou non.
        /// </summary>
        static void DisplayPrimes()
        {
            string prime;
            for (int i = 1; i <= 100; i++)
            {
                if (isPrime(i))
                {
                    prime = "est premier.";
                }
                else
                {
                    prime = "n'est pas premier.";
                }
                Console.WriteLine($"{i} {prime}");
            }
        }

        // Exercice 6

        /// <summary>
        /// Retourne le plus grand diviseur commun entre les nombres a et b.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        static int Gcd(int a, int b)
        {
            int r = 0;
            int q = 0;
            do
            {
                r = a % b;
                q = (a - r) / b;
                a = b;
                b = r;

            } while (r != 0);
            
            return a;
        }
    }
}
