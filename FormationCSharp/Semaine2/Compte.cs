using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semaine2
{
    /// <summary>
    /// Compte composé d'un numéro d'identificaction unique, d'un solde, d'un montant de retrait maximum, et des transactions qu'il a effectué.
    /// </summary>
    public class Compte
    {
        private int numero;
        private float solde;
        private float retrait_max;
        private List<Transaction> transactions;

        public Compte()
        {
            numero = 0;
            solde = 0;
            retrait_max = 1000;
            transactions = new List<Transaction>();
        }

        public Compte(int numero, float solde)
        {
            this.numero = numero;
            this.solde = solde;
            retrait_max = 1000;
            transactions = new List<Transaction>();
        }

        public Compte(int numero, float solde, float retrait_max, List<Transaction> transactions)
        {
            this.numero = numero;
            this.solde = solde;
            this.retrait_max = retrait_max;
            this.transactions = transactions;
        }


        public int GetNumero() { return numero; }
        public float GetSolde() { return solde; }
        public float GetRetraitMax() { return retrait_max; }
        public List<Transaction> GetTransactions() { return transactions; }

        public void SetNumero(int numero) { this.numero = numero; }
        public void SetSolde(float solde) { this.solde = solde; }
        public void SetRetraitMax(float retrait_max) { this.retrait_max = retrait_max; }

        public void AddTransaction(Transaction tx)
        {
            this.transactions.Add(tx);
        }

        public void RemoveTransaction(Transaction tx)
        {
            this.transactions.Remove(tx);
        }

        public void UpdateSolde(float montant)
        {
            this.solde += montant;
        }

    }
}
