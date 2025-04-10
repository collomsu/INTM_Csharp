using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

/// <summary>
/// Summary description for Class1
/// </summary>
namespace Semaine1
{
    internal class Serie3
    {
        struct ResultatEtudiant
        {
            public string nom;
            public string matiere;
            public float note;

            public ResultatEtudiant(string nom, string matiere, float note)
            {
                this.nom = nom;
                this.matiere = matiere;
                this.note = note;
            }
        }

        // Exercice 1

        /// <summary>
        /// Traite les données csv en entrée, réalise le calcul des moyennes par matière et stock le résultat dans le fichier de sortie.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="output"></param>
        public void SchoolMeans(string input, string output)
        {
            FileStream fin = new FileStream(input, FileMode.Open, FileAccess.Read);
            FileStream fout = new FileStream(output, FileMode.Open, FileAccess.Write);

            StreamReader reader = new StreamReader(fin);
            StreamWriter writer = new StreamWriter(fout);


            List<ResultatEtudiant> resultats = new List<ResultatEtudiant> ();

            while (!reader.EndOfStream)
            {
                string[] resSplit = reader.ReadLine().Split(';');
                Console.WriteLine($"{resSplit[0]}, {resSplit[1]}, {double.Parse(resSplit[2], CultureInfo.InvariantCulture)}");
                resultats.Add(new ResultatEtudiant(resSplit[0], resSplit[1], float.Parse(resSplit[2], CultureInfo.InvariantCulture)));
            }

            reader.Close();

            List<string> matieres = new List<string> ();
            float moyenne = 0f;
            int nbNotes = 0;

            foreach (ResultatEtudiant re in resultats)
            {
                if (!matieres.Contains(re.matiere)) {
                    matieres.Add(re.matiere);
                    moyenne = 0f;
                    nbNotes = 0;
                    foreach (ResultatEtudiant re2 in resultats)
                    {
                        if (re2.matiere == re.matiere)
                        {
                            moyenne += re2.note;
                            nbNotes++;
                        }
                    }
                    writer.WriteLine($"{re.matiere};{moyenne /= nbNotes}");
                }
            }

            writer.Close();
        }

        // Exercice 2

        /// <summary>
        /// Tri par insertion du tableau en entrée.
        /// </summary>
        /// <param name="array"></param>
        public void InsertionSort(int[] array)
        {
            int x, j;
            for (int i = 1; i < array.Length; i++)
            {
                x = array[i];
                j = i;
                while (j > 0 && array[j - 1] > x)
                {
                    array[j] = array[j - 1];
                    j = j - 1;
                }
                array[j] = x;
            }
        }

        /// <summary>
        /// Tri par parition du tableau en entrée.
        /// </summary>
        /// <param name="array"></param>
        public void QuickSort(int[] array)
        {
            QuickSortRec(array, 0, array.Length - 1);
        }

        /// <summary>
        /// Algorithme de paritionnement du tableau passé en paramètre en fonction de l'index du premier element, du dernier element, et de l'index de l'element pivot.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="premier"></param>
        /// <param name="dernier"></param>
        /// <param name="pivot"></param>
        int Partitionner(int[] array, int premier, int dernier, int pivot)
        {
            Echanger(array, pivot, dernier);

            int j = premier;

            for (int i = premier; i < dernier - 1; i++)
            {
                if (array[i] <= array[dernier])
                {
                    Echanger(array, i, j);
                    j += 1;
                }
            }
            Echanger(array, dernier, j);

            return j;
        }

        /// <summary>
        /// Algorithme de tri par partition sur le tableau en entrée sur une zone délimitée par l'index du premier et dernier element.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="premier"></param>
        /// <param name="dernier"></param>
        void QuickSortRec(int[] array, int premier, int dernier)
        {
            int pivot = 0;

            if (premier < dernier)
            {
                pivot = premier;
                pivot = Partitionner(array, premier, dernier, pivot);
                QuickSortRec(array, premier, pivot - 1);
                QuickSortRec(array, pivot + 1, dernier);
            }

        }

        /// <summary>
        /// Fonction d'échange de deux éléments dans un tableau donnée.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="pos1"></param>
        /// <param name="pos2"></param>
        void Echanger(int[] array, int pos1, int pos2)
        {
            int x = array[pos1];
            array[pos1] = array[pos2];
            array[pos2] = x;
        }

        /// <summary>
        /// Retourne le temps de travail de la foncton InsertionSort.
        /// </summary>
        /// <param name="array"></param>
        public long UseInsertionSort(int[] array)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            InsertionSort(array);
            watch.Stop();
            return watch.ElapsedMilliseconds;
        }

        /// <summary>
        /// Retourne le temps de travail de la foncton QuickSort.
        /// </summary>
        /// <param name="array"></param>
        public long UseQuickSort(int[] array)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            QuickSort(array);
            watch.Stop();
            return watch.ElapsedMilliseconds;
        }

        /// <summary>
        /// Génère un binome de deux listes identiques d'entiers.
        /// </summary>
        /// <param name="size"></param>
        public List<int[]> ArraysGenerator(int size)
        {
            int[] tab1 = new int[size];
            int[] tab2 = new int[size];
            int resRnd = 0;

            Random rnd = new Random();

            for (int i = 0; i < size; i++)
            {
                resRnd = rnd.Next(-1000, 1000);
                tab1[i] = resRnd;
                tab2[i] = resRnd;
            }

            List<int[]> res = new List<int[]> ();
            res.Add(tab1);
            res.Add(tab2);

            return res;
        }

        public struct SortData
        {
            public double insertionMean;
            public double insertionStd;
            public double quickMean;
            public double quickStd;

            public SortData(double insertionMean, double insertionStd, double quickMean, double quickStd)
            {
                this.insertionMean = insertionMean;
                this.insertionStd = insertionStd;
                this.quickMean = quickMean;
                this.quickStd = quickStd;
            }
        }

        /// <summary>
        /// Renvoi les données de performances de moyenne et d'écart type pour les tri par insertion et par partitionnement sur des tableau de taille size, et d'un nombre de fois count.
        /// </summary>
        /// <param name="size"></param>
        /// <param name="count"></param>
        public SortData PerformanceTest(int size, int count)
        {
            SortData data = new SortData();
            List<int[]> list = new List<int[]>();
            List<long[]> listVals = new List<long[]>();
            listVals.Add(new long[count]);
            listVals.Add(new long[count]);

            long timeExec = 0;
            for (int i = 0; i < count; i++)
            {
                list = ArraysGenerator(size);

                timeExec = UseInsertionSort(list.ElementAt(0));
                listVals.ElementAt(0)[i] = timeExec;
                data.insertionMean += timeExec;


                timeExec = UseQuickSort(list.ElementAt(1));
                listVals.ElementAt(1)[i] = timeExec;
                data.quickMean += timeExec;
            }

            data.insertionMean /= count;
            data.quickMean /= count;

            for (int i = 0; i < count; i++)
            {
                data.insertionStd += Math.Pow(listVals.ElementAt(0)[i] - data.insertionMean, 2);
                data.quickStd += Math.Pow(listVals.ElementAt(1)[i] - data.quickMean, 2);
            }

            data.insertionStd = Math.Sqrt(data.insertionStd / count);
            data.quickStd = Math.Sqrt(data.quickStd / count);

            return data;
        }

        /// <summary>
        /// Renvoi une liste données de performances renvoyé par PerformanceTest pour chaque taille de liste à traiter un nombre de fois count.
        /// </summary>
        /// <param name="sizes"></param>
        /// <param name="count"></param>
        public List<SortData> PerformancesTest(List<int> sizes, int count)
        {
            List<SortData> sd = new List<SortData>();

            for (int i = 0; i < sizes.Count(); i++)
            {
                sd.Add(PerformanceTest(sizes[i], count));
            }

            return sd;
        }

        /// <summary>
        /// Affiche les résultats obtenu par PerformancesTest.
        /// </summary>
        /// <param name="sizes"></param>
        /// <param name="count"></param>
        public void DisplayPerformance(List<int> sizes, int count)
        {
            Console.WriteLine("n;MeanInsertion;StdInsertion;MeanQuick;StdQuick");
            List<SortData> sd = PerformancesTest(sizes, count);

            for (int i = 0; i < sd.Count(); i++)
            {
                Console.WriteLine($"{sizes[i]};{sd.ElementAt(i).insertionMean:0.##};{sd.ElementAt(i).insertionStd:0.##};{sd.ElementAt(i).quickMean:0.##};{sd.ElementAt(i).quickStd:0.##}");
            }
        }
    }
}