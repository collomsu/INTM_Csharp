using System;
using System.Collections.Generic;

namespace ProjetBanqueV2
{
    public class Gestionnaire
    {
        public enum TypeGestionnaire
        {
            Particulier,
            Entreprise,
            None
        }

        private int _numero;
        private TypeGestionnaire _type;
        private int _nbrTransactions;
        private List<Compte> _comptes;
        private decimal _fraisTotaux;

        public Gestionnaire(int numero, TypeGestionnaire type, int nbrTransactions)
        {
            _numero = numero;
            _type = type;
            _nbrTransactions = nbrTransactions;
            _comptes = new List<Compte>();
            _fraisTotaux = 0;
        }

        public int Numero
        {
            get => _numero;
            set => _numero = value;
        }

        public TypeGestionnaire Type
        {
            get => _type;
            set => _type = value;
        }

        public int NbrTransactions
        {
            get => _nbrTransactions;
            set => _nbrTransactions = value;
        }

        public List<Compte> Comptes
        {
            get => _comptes;
        }

        public decimal FraisTotaux
        {
            get => _fraisTotaux;
            set => _fraisTotaux = value;
        }

        public void UpdateFraisTotaux(decimal frais)
        {
            _fraisTotaux += frais;
        }

        public string AddCompte(Compte cpt)
        {
            if (!_comptes.Contains(cpt))
            {
                _comptes.Add(cpt);
                return "OK";
            }
            else
            {
                return "KO";
            }
        }

        /// <summary>
        /// Créer un nouveau compte s'il n'existe pas déjà.
        /// </summary>
        /// <param name="numero"></param>
        /// <param name="solde"></param>
        /// <param name="retraitMax"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public string CreateCompte(int numero, decimal solde, DateTime dateCrea, DateTime dateResi = default(DateTime))
        {
            if (GestionComptes.SearchCompte(_comptes, numero) == null && solde >= 0)
            {
                _comptes.Add(new Compte(numero, solde, 2000, dateCrea, dateResi));
                return "OK";
            }
            else
            {
                return "KO";
            }
        }

        /// <summary>
        /// Cloture du compte du gestionnaire courant si le compte existe et qu'il est actif.
        /// </summary>
        /// <param name="cpt"></param>
        /// <returns></returns>
        public string CloseCompte(Compte cpt, DateTime dateResiliation)
        {
            if (cpt != null && DateTime.Compare(cpt.DateCreation, dateResiliation) < 0)
            {
                _comptes[_comptes.IndexOf(cpt)].DateResiliation = dateResiliation;
                return "OK";
            }
            else
            {
                return "KO";
            }
        }

        /// <summary>
        /// Cède un compte à une autre gestionnaire si celui-ci et le prochain gestionnaire existe. 
        /// </summary>
        /// <param name="cpt"></param>
        /// <param name="gesReceveur"></param>
        /// <returns></returns>
        public string CessionCompte(Compte cpt, Gestionnaire gesReceveur)
        {
            if (cpt != null && gesReceveur != null && DateTime.Compare(cpt.DateCreation, DateTime.Today) <= 0 && (DateTime.Compare(cpt.DateResiliation, default(DateTime)) == 0 || DateTime.Compare(cpt.DateResiliation, DateTime.Today) > 0))
            {
                if (Gestionnaire.ReceiveCompte(cpt, gesReceveur) == "OK")
                {
                    _comptes.Remove(cpt);
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
        /// Ajoute le compte à sa liste de compte qu'il gère si le compte et le gestionnaire cible existe.
        /// </summary>
        /// <param name="cpt"></param>
        /// <param name="gesReceveur"></param>
        /// <returns></returns>
        public static string ReceiveCompte(Compte cpt, Gestionnaire gesReceveur)
        {
            if (cpt != null && gesReceveur != null && DateTime.Compare(cpt.DateCreation, DateTime.Today) <= 0 && (DateTime.Compare(cpt.DateResiliation, default(DateTime)) == 0 || DateTime.Compare(cpt.DateResiliation, DateTime.Today) > 0))
            {
                return gesReceveur.AddCompte(cpt);
            }
            else
            {
                return "KO";
            }
        }

        public string AutoriseTransaction(Compte cpt, DateTime dt, decimal montant)
        {
            decimal montantTransactions = montant;
            int nbTransTraite = 0;

            if (NbrTransactions != 0)
            {
                for (int i = cpt.Transactions.Count - 1; i >= 0; i--)
                {
                    if (cpt.Transactions[i].Expediteur != null && cpt.Transactions[i].Expediteur.Numero == cpt.Numero)
                    {
                        nbTransTraite++;
                        montantTransactions += cpt.Transactions[i].Montant;
                    }

                    if (nbTransTraite >= NbrTransactions)
                    {
                        break;
                    }
                }

                if (montantTransactions > 1000)
                {
                    return "KO";
                }
            }

            montantTransactions = montant;

            foreach (Transaction t in cpt.Transactions)
            {
                if (t.Expediteur != null && t.Expediteur.Numero == cpt.Numero && (dt - t.Date).Days < 7)
                {
                    montantTransactions += t.Montant;
                }
            }

            if (montantTransactions > 2000)
            {
                Console.WriteLine("trop de moula consommé");
                return "KO";
            }

            return "OK";
        }
    }
}
