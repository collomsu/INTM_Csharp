using System;
using System.Collections.Generic;

namespace Semaine2
{
    public class GestionComptes
    {

        private List<Compte> _comptes;
        private string _pathComptes;
        private string _pathTransactions;
        private string _pathStatutsTransations;

        public GestionComptes(string pathComptes, string pathTransactions, string pathStatutsTransations)
        {
            this._pathComptes = pathComptes;
            this._pathTransactions = pathTransactions;
            this._pathStatutsTransations = pathStatutsTransations;
            this._comptes = new List<Compte>();
        }

        public void AddCompte(Compte cpt)
        {
            this._comptes.Add(cpt);
        }

        public void RemoveCompte(Compte cpt)
        {
            this._comptes.Remove(cpt);
        }

        /// <summary>
        /// Traite un fichier CSV d'entrée des comptes à ajouter
        /// Format d'entrée : numcpt;solde
        /// </summary>
        public void TraiterCompteEntree()
        {
            FichierEntree fe = new FichierEntree(_pathComptes, System.IO.FileMode.Open);
            fe.Open();
            while (!fe.EndOfStream())
            {
                var line = fe.ReadLine();
                var values = line.Split(';');

                float solde = 0;

                if (values.Length > 1 && !String.IsNullOrEmpty(values[1]))
                {
                    Console.WriteLine(values[1]);
                    solde = (int)Convert.ToDouble(values[1]);
                }

                if (solde >= 0) {
                    AddCompte(new Compte(int.Parse(values[0]), solde));
                    Console.WriteLine("Compte inséré !");
                    Console.WriteLine(values.Length);
                }
                else
                {
                    Console.WriteLine("Compte NON inséré : montant < 0");
                }
            }
            fe.Close();

            Console.WriteLine(GetAllSoldesComptes());
        }

        /// <summary>
        /// Traite un fichier CSV d'entrée des transactions a traiter, et affiche le resultats des transactions traitées dans un fichier CSV de sortie.
        /// Format d'entrée : numtransac;montant;cptexp;cptdest
        /// </summary>
        public void TraiterTransactionEntree()
        {
            FichierEntree fe = new FichierEntree(_pathTransactions, System.IO.FileMode.Open);
            FichierSortie fs = new FichierSortie(_pathStatutsTransations, System.IO.FileMode.OpenOrCreate);
            fe.Open();
            fs.Open();

            string codeRetour = "";

            while (!fe.EndOfStream())
            {
                var line = fe.ReadLine();
                var values = line.Split(';');

                int num_transaction = int.Parse(values[0]);
                float montant_transaction = (int)Convert.ToDouble(values[1]);
                int cpt_exp = int.Parse(values[2]);
                int cpt_dest = int.Parse(values[3]);

                if (cpt_exp == 0 && cpt_dest != 0)
                {
                    codeRetour = Compte.DepotArgent(SearchCompte(cpt_dest), montant_transaction, num_transaction);
                }
                else if (cpt_exp != 0 && cpt_dest == 0)
                {
                    codeRetour = Compte.RetirerArgent(SearchCompte(cpt_exp), montant_transaction, num_transaction);
                }
                else
                {
                    codeRetour = Compte.Virement(SearchCompte(cpt_exp), SearchCompte(cpt_dest), montant_transaction, num_transaction);
                }

                fs.WriteLine($"{num_transaction};{codeRetour}");
                Console.WriteLine($"{num_transaction};{codeRetour} {GetAllSoldesComptes()}");
            }

            fe.Close();
            fs.Close();
        }

        /// <summary>
        /// Parcours les comptes de la banque pour renvoyer le compte ayant comme numero celui passé en paramètre.
        /// </summary>
        /// <param name="numero"></param>
        /// <returns>Compte</returns>
        public Compte SearchCompte(int numero)
        {
            Compte compte_trouve = null;
            _comptes.ForEach(compte =>
            {
                if (compte.Numero == numero)
                {
                    compte_trouve = compte;
                    return;
                }
            });
            return compte_trouve;
        }

        public String GetAllSoldesComptes()
        {
            string soldes = "";

            foreach (Compte cpt in _comptes)
            {
                soldes += $"{cpt.Solde} ";
            }

            return soldes;
        }

    }
}
