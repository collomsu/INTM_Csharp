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

        public Gestionnaire(int numero, TypeGestionnaire type, int nbrTransactions)
        {
            _numero = numero;
            _type = type;
            _nbrTransactions = nbrTransactions;
            _comptes = new List<Compte>();
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
    }
}
