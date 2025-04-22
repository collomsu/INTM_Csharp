using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace ProjetBanqueV2
{
    /// <summary>
    /// Transaction composé d'un numéro d'identification unique, d'un compte expéditeur, d'un compte destinataire, et d'un montant positif.
    /// </summary>
    public class Transaction
    {
        private int _numero;
        private Compte _expediteur;
        private Compte _destinataire;
        private float _montant;

        public Transaction(int numero, Compte expediteur, Compte destinataire, float montant)
        {
            this._numero = numero;
            this._expediteur = expediteur;
            this._destinataire = destinataire;
            this._montant = montant;
        }

        public int Numero
        {
            get => _numero;
            set => _numero = value;
        }

        public Compte Expediteur
        {
            get => _expediteur;
            set => _expediteur = value;
        }

        public Compte Destinataire
        {
            get => _destinataire;
            set => _destinataire = value;
        }

        public float Montant
        {
            get => _montant;
            set => _montant = value;
        }
    }
}
