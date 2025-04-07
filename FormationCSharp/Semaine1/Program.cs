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
            BasicOperation(1, 2, '+');
            BasicOperation(1, 2, 'L');
            BasicOperation(1, 0, '/');

            IntegerDivision(8, 2);
            IntegerDivision(5, 2);
            IntegerDivision(8, 0);


            Console.WriteLine(GoodDay(4));
            Console.WriteLine(GoodDay(7));
            Console.WriteLine(GoodDay(12));
            Console.WriteLine(GoodDay(15));
            Console.WriteLine(GoodDay(20));
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
    }
}
