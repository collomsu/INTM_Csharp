using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

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
    }
}