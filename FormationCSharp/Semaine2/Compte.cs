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
        private int _numero;
        private float _solde;
        private float _retraitMax;
        private List<Transaction> _transactions;

        public Compte()
        {
            _numero = 0;
            _solde = 0;
            _retraitMax = 1000;
            _transactions = new List<Transaction>();
        }

        public Compte(int numero, float solde)
        {
            this._numero = numero;
            this._solde = solde;
            this._retraitMax = 1000;
            this._transactions = new List<Transaction>();
        }

        public Compte(int numero, float solde, float retraitMax) : this(numero, solde)
        {
            this._retraitMax = retraitMax;
            this._transactions = new List<Transaction>();
        }

        public Compte(int numero, float solde, float retraitMax, List<Transaction> transactions) : this(numero, solde, retraitMax)
        {
            this._retraitMax = retraitMax;
            this._transactions = transactions;
        }

        public float Solde
        {
            get => _solde;
            set => _solde = value;
        }

        public int Numero
        {
            get => _numero;
            set => _numero = value;
        }

        public float RetraitMax
        {
            get => _retraitMax;
            set => _retraitMax = value;
        }

        public List<Transaction> Transactions
        {
            get => _transactions;
            set => _transactions = value;
        }

        public void AddTransaction(Transaction tx)
        {
            this._transactions.Add(tx);
        }

        public void RemoveTransaction(Transaction tx)
        {
            this._transactions.Remove(tx);
        }

        public void UpdateSolde(float montant)
        {
            this._solde += montant;
        }

        /// <summary>
        /// Retourne la somme des 10 derniers virements effectué par le compte passé en paramètre.
        /// </summary>
        /// <param name="compte"></param>
        /// <returns>float</returns>
        static float SommmeDixDernierVirement(Compte compte)
        {
            int nb_transactions = 0;
            float montant = 0;
            foreach (var transaction in compte.Transactions)
            {
                nb_transactions++;
                if (nb_transactions > 10)
                {
                    break;
                }

                if (transaction.Expediteur.Numero != 0 && transaction.Destinataire != null && transaction.Expediteur.Numero == compte.Numero)
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
        public static bool DemandePrelevement(Compte compte, float montant)
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
        public static string DepotArgent(Compte cpt, float montant, int numTransaction)
        {
            if (montant > 0 && cpt != null)
            {
                Transaction depot = new Transaction(numTransaction, null, cpt, montant);
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
        public static string RetirerArgent(Compte cpt, float montant, int numTransaction)
        {
            if (montant > 0 && cpt != null && cpt.Solde > montant)
            {
                if ((SommmeDixDernierVirement(cpt) + montant) <= 1000)
                {
                    Transaction retrait = new Transaction(numTransaction, cpt, null, montant);
                    cpt.UpdateSolde(-montant);
                    cpt.AddTransaction(retrait);
                    return "OK";
                }
                else
                {
                    return "KO";
                }
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
        public static string Virement(Compte cptExp, Compte cptDest, float montant, int numTransaction)
        {
            if (cptExp != null && cptDest != null && DemandePrelevement(cptExp, montant))
            {
                Transaction virement = new Transaction(numTransaction, cptExp, cptDest, montant);
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
