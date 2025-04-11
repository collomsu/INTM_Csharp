using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
            Console.WriteLine();
            Console.WriteLine(serie2.LinearSearch(tab, 2));
            Console.WriteLine(serie2.LinearSearch(tab, 12));

            Console.WriteLine(serie2.BinarySearch(tab2, 9));
            Console.WriteLine(serie2.BinarySearch(tab2, 15));

            // Exercice 2
            int[] m1 = new int[] { 1, 2, 3 };
            int[] m2 = new int[] { -1, -4, 0 };

            int[] m3 = new int[] { 3, -4, 5 };
            int[] m4 = new int[] { 1, 2, 8 };

            int[][] m5 = serie2.BuildingMatrix(m1, m2);
            int[][] m6 = serie2.BuildingMatrix(m3, m4);

            serie2.displayMatrix(m5);
            serie2.displayMatrix(m6);

            serie2.displayMatrix(serie2.Addition(m5, m6));
            serie2.displayMatrix(serie2.Substraction(m5, m6));

            serie2.displayMatrix(serie2.Multiplication(m5, m6));

            // Exercice 3

            int[] ex3 = serie2.EratosthenesSieve(100);

            for (int i = 0; i < ex3.Length; i++)
            {
                Console.Write($"{ex3[i]} ");
            }

            // Exercice 4
            Serie2.Qcm qcm1 = new Serie2.Qcm("Quel est le résultat de 3² ?", new string[] { "6", "9", "30" }, 1, 2);
            Serie2.Qcm qcm2 = new Serie2.Qcm("Quel est le résultat de 2² ?", new string[] { "4", "9", "30" }, 0, 2);
            Console.WriteLine(serie2.AskQuestion(qcm1));

            Serie2.Qcm[] qcms = new Serie2.Qcm[] { qcm1, qcm2 };
            serie2.AskQuestions(qcms);


            //StringBuilder sb = new StringBuilder();

            //for (int i = 1; i <= 100; i++)
            //{
            //    sb.Append(i);
            //    if (i < 100)
            //    {
            //        sb.Append('-');
            //    }
            //}

            //Console.WriteLine(sb.ToString());

            //string path = "C:\\Users\\wksadmin\\Documents\\fichier.txt";
            //FileStream fs = new FileStream(path, FileMode.Open, FileAccess.ReadWrite);
            //StreamReader reader = new StreamReader(fs);
            //while (!reader.EndOfStream)
            //{
            //    Console.WriteLine(reader.ReadLine());
            //}

            //reader.Close();

            //StreamWriter writer = new StreamWriter(path);
            //string input = Console.ReadLine();
            //while (input != "")
            //{
            //    writer.WriteLine(input);
            //    input = Console.ReadLine();
            //}
            //writer.Close();


            // Serie 3
            Serie3 serie3 = new Serie3();

            // Exercice 1
            Console.WriteLine();
            Console.WriteLine("=== Serie 3 : Exerice 1 ===");
            string input = "C:\\Users\\wksadmin\\Documents\\moyeleve.csv";
            string output = "C:\\Users\\wksadmin\\Documents\\moymatiere.csv";

            serie3.SchoolMeans(input, output);

            // Exercice 2
            Console.WriteLine();
            Console.WriteLine("=== Serie 3 : Exerice 2 ===");
            int[] arr1 = new int[] { 5, 25, 12, 2, 1, 3, 5, 2, 56, 97, 45, 20, 1, 4 };
            int[] arr2 = new int[] { 5, 25, 12, 2, 1, 3, 5, 2, 56, 97, 45, 20, 1, 4 };

            Console.WriteLine(serie3.UseInsertionSort(arr1));
            Console.WriteLine(serie3.UseQuickSort(arr2));

            List<int[]> larr = serie3.ArraysGenerator(10000);

            Console.WriteLine(serie3.UseInsertionSort(larr.ElementAt(0)));
            Console.WriteLine(serie3.UseInsertionSort(larr.ElementAt(1)));


            Serie3.SortData data = serie3.PerformanceTest(10000, 10);

            Console.WriteLine($"insertionMean = {data.insertionMean}, insertionStd = {data.insertionStd}, quickMean = {data.quickMean}, quickStd = {data.quickStd}");

            List<int> li = new List<int>() { 2000, 5000, 10000, 20000, 50000, 100000 };
            List<int> liShort = new List<int>() { 2000, 5000, 10000, 20000 };

            //serie3.DisplayPerformance(liShort, 50);

            // Serie 4
            Serie4 serie4 = new Serie4();

            // Exercice 1
            Console.WriteLine();
            Console.WriteLine("=== Serie 4 : Exerice 1 ===");
            string code = "==.=.==.=...==.==.==...==.=.=...=.......==.==...==.==.==...=.==.=...=.=.=...=";
            Console.WriteLine($"Nombre de lettres : {serie4.LettersCount(code)}");
            Console.WriteLine($"Nombre de mots : {serie4.WordsCount(code)}");
            Console.WriteLine(serie4.MorseTranslation(code));

            string codeCorrupt = "==.=..==..=....==.==.==...==.=.=...=..........==.==....==.==..==....=.==..=...=.=.=....=....===..=";
            Console.WriteLine(serie4.EfficientMorseTranslation(codeCorrupt));

            Console.WriteLine(serie4.MorseEncryption("WESH"));

            // Exerice 2
            Console.WriteLine();
            Console.WriteLine("=== Serie 4 : Exerice 2 ===");
            string sentence = "(a{}[])";
            string sentence2 = "(a{)}[])";
            Console.WriteLine(serie4.BracketsControls(sentence));
            Console.WriteLine(serie4.BracketsControls(sentence2));

            // Exercice 3
            Console.WriteLine();
            Console.WriteLine("=== Serie 4 : Exerice 3 ===");

            Console.WriteLine(serie4.AddPhoneNumber("0684444464", "Papa"));
            Console.WriteLine(serie4.AddPhoneNumber("0668021201", "Maman"));
            Console.WriteLine(serie4.AddPhoneNumber("1668081201", "Erreur"));
            Console.WriteLine(serie4.AddPhoneNumber("0068021201", "Erreur"));

            serie4.PhoneContact("0668021201");
            Console.WriteLine(serie4.ContainsPhoneContact("0684447964"));

            serie4.DisplayPhoneBook();

            serie4.DeletePhoneNumber("0668021201");
            serie4.DeletePhoneNumber("0684447964");

            serie4.DisplayPhoneBook();

            // Exercice 4
            Console.WriteLine();
            Console.WriteLine("=== Serie 4 : Exerice 4 ===");

            Console.WriteLine(serie4.isEmpty());
            serie4.SetRangeOfDates(new DateTime(2020, 01, 01), new DateTime(2030, 12, 31));
            Console.WriteLine(serie4.isEmpty());

            serie4.DisplayMeetings();
            Console.WriteLine(serie4.AddBusinessMeeting(new DateTime(2020, 02, 10), new TimeSpan(4, 0, 0)));
            Console.WriteLine(serie4.AddBusinessMeeting(new DateTime(2020, 02, 10), new TimeSpan(4, 0, 0)));
            Console.WriteLine(serie4.AddBusinessMeeting(new DateTime(2020, 02, 11), new TimeSpan(4, 0, 0)));
            Console.WriteLine(serie4.AddBusinessMeeting(new DateTime(2020, 02, 11, 5, 0, 0), new TimeSpan(4, 0, 0)));
            Console.WriteLine(serie4.AddBusinessMeeting(new DateTime(2020, 02, 11, 9, 0, 1), new TimeSpan(2, 0, 0)));
            Console.WriteLine(serie4.AddBusinessMeeting(new DateTime(2020, 02, 11, 11, 0, 1), new TimeSpan(2, 0, 0)));


            serie4.DisplayMeetings();
            Console.WriteLine(serie4.DeleteBusinessMeeting(new DateTime(2020, 02, 11), new TimeSpan(4, 0, 0)));
            Console.WriteLine(serie4.AddBusinessMeeting(new DateTime(2020, 02, 11), new TimeSpan(4, 0, 0)));
            serie4.DisplayMeetings();

            Console.WriteLine(serie4.ClearMeetingPeriod(new DateTime(2020, 02, 11, 3, 0, 0), new DateTime(2020, 02, 11, 10, 0, 0)));
            serie4.DisplayMeetings();

            // Percolation
            Console.WriteLine();
            Console.WriteLine("=== Percolation ===");

            bool[][] open =
            {
                new bool[] { true, false, true, true, true, true, },
                new bool[] { true, false, false, true, false, false, },
                new bool[] { false, false, false, true, false, false, },
                new bool[] { false, false, true, true, true, false, },
                new bool[] { true, true, false, true, true, false, },
                new bool[] { true, true, false, true, false, false, }
            };
            bool[][] full =
            {
                new bool[] { true, false, true, true, true, true, },
                new bool[] { true, false, false, true, false, false, },
                new bool[] { false, false, false, true, false, false, },
                new bool[] { false, false, true, true, true, false, },
                new bool[] { false, false, false, true, true, false, },
                new bool[] { false, false, false, true, false, false, }
            };

            Percolation perco = new Percolation(new Percolation.Perco(open, full));
            Console.WriteLine(perco.Percolate());

            List<KeyValuePair<int, int>> poss = perco.CloseNeighbors(1, 2);

            foreach (var pos in poss)
            {
                Console.WriteLine($"x: {pos.Key} , y: {pos.Value}");
            }

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
