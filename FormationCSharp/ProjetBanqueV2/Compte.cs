﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetBanqueV2
{
    /// <summary>
    /// Compte composé d'un numéro d'identificaction unique, d'un solde, d'un montant de retrait maximum, et des transactions qu'il a effectué.
    /// </summary>
    public class Compte
    {
        private int _numero;
        private decimal _solde;
        private decimal _retraitMax;
        private List<Transaction> _transactions;
        private DateTime _dateCreation;
        private DateTime _dateResiliation;

        public Compte()
        {
            _numero = 0;
            _solde = 0;
            _retraitMax = 1000;
            _transactions = new List<Transaction>();
        }

        public Compte(int numero, decimal solde)
        {
            this._numero = numero;
            this._solde = solde;
            this._retraitMax = 1000;
            this._transactions = new List<Transaction>();
        }

        public Compte(int numero, decimal solde, decimal retraitMax) : this(numero, solde)
        {
            this._retraitMax = retraitMax;
            this._transactions = new List<Transaction>();
        }

        public Compte(int numero, decimal solde, decimal retraitMax, List<Transaction> transactions) : this(numero, solde, retraitMax)
        {
            this._retraitMax = retraitMax;
            this._transactions = transactions;
        }

        public Compte(int numero, decimal solde, decimal retraitMax, List<Transaction> transactions, DateTime dateCreation) : this(numero, solde, retraitMax, transactions)
        {
            this._dateCreation = dateCreation;
        }

        public Compte(int numero, decimal solde, decimal retraitMax, DateTime dateCreation, DateTime dateResiliation) : this(numero, solde, retraitMax)
        {
            this._dateCreation = dateCreation;
            this._dateResiliation = dateResiliation;
        }

        public decimal Solde
        {
            get => _solde;
            set => _solde = value;
        }

        public int Numero
        {
            get => _numero;
            set => _numero = value;
        }

        public decimal RetraitMax
        {
            get => _retraitMax;
            set => _retraitMax = value;
        }

        public List<Transaction> Transactions
        {
            get => _transactions;
            set => _transactions = value;
        }

        public DateTime DateCreation
        {
            get => _dateCreation;
            set => _dateCreation = value;
        }

        public DateTime DateResiliation
        {
            get => _dateResiliation;
            set => _dateResiliation = value;
        }

        public void AddTransaction(Transaction tx)
        {
            this._transactions.Add(tx);
        }

        public void RemoveTransaction(Transaction tx)
        {
            this._transactions.Remove(tx);
        }

        public void UpdateSolde(decimal montant)
        {
            this._solde += montant;
        }

        /// <summary>
        /// Retourne la somme des 10 derniers virements effectué par le compte passé en paramètre.
        /// </summary>
        /// <param name="compte"></param>
        /// <returns>decimal</returns>
        static decimal SommmeDixDernierVirement(Compte compte)
        {
            int nb_transactions = 0;
            decimal montant = 0;
            foreach (var transaction in compte.Transactions)
            {
                nb_transactions++;
                if (nb_transactions > 10)
                {
                    break;
                }

                if (transaction.Expediteur != null && transaction.Destinataire != null && transaction.Expediteur.Numero == compte.Numero)
                {
                    montant += transaction.Montant;
                }
            };

            return montant;
        }

        /// <summary>
        /// Réalise la demande de prélèvement et si celle-ci est possible, l'effectue et renvoie l'état de l'opération
        /// true si prélèvement effectué, false s'il n'a pas pu avoir lieu.
        /// </summary>
        /// <param name="compte"></param>
        /// <param name="montant"></param>
        /// <returns>bool</returns>
        public static bool DemandePrelevement(Compte compte, decimal montant)
        {
            if (montant > 0 && compte.Solde > montant && (SommmeDixDernierVirement(compte) + montant) <= 1000)
            {
                compte.UpdateSolde(-montant);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Traite l'opération du dépot d'argent sur un compte à partir du numéro de compte et du montant à .
        /// Le montant doit etre positif.
        /// </summary>
        /// <param name="cpt"></param>
        /// <param name="montant"></param>
        /// <param name="numTransaction"></param>
        public static string DepotArgent(Compte cpt, decimal montant, int numTransaction, DateTime date)
        {
            if (montant > 0 && cpt != null)
            {
                Transaction depot = new Transaction(numTransaction, null, cpt, montant, date);
                cpt.UpdateSolde(montant);
                cpt.AddTransaction(depot);
                return "OK";
            }
            else
            {
                return "KO";
            }
        }

        /// <summary>
        /// Traite l'opération de retrait d'argent sur un compte à partir du numéro de compte et du montant à retirer.
        /// Le montant doit etre positif et inférieur au solde du compte à retirer.
        /// </summary>
        /// <param name="cpt"></param>
        /// <param name="montant"></param>
        /// <param name="numTransaction"></param>
        public static string RetirerArgent(Compte cpt, decimal montant, int numTransaction, DateTime date)
        {
            if (montant > 0 && cpt != null && cpt.Solde > montant)
            {
                Transaction retrait = new Transaction(numTransaction, cpt, null, montant, date);
                cpt.UpdateSolde(-montant);
                cpt.AddTransaction(retrait);
                return "OK";
            }
            else
            {
                return "KO";
            }
        }

        /// <summary>
        /// Traite l'opération de virement entre deux compte d'un montant défini devant être positif.
        /// </summary>
        /// <param name="cptExp"></param>
        /// <param name="cptDest"></param>
        /// <param name="montant"></param>
        /// <param name="numTransaction"></param>
        public static string Virement(Compte cptExp, Compte cptDest, decimal montant, int numTransaction, DateTime date)
        {
            if (cptExp != null && cptDest != null && DemandePrelevement(cptExp, montant))
            {
                Transaction virement = new Transaction(numTransaction, cptExp, cptDest, montant, date);
                cptDest.UpdateSolde(montant);
                cptExp.AddTransaction(virement);
                cptDest.AddTransaction(virement);
                return "OK";
            }
            else
            {
                return "KO";
            }
        }
    }
}
