using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semaine2
{
    /// <summary>
    /// Transaction composé d'un numéro d'identification unique, d'un compte expéditeur, d'un compte destinataire, et d'un montant positif.
    /// </summary>
    public class Transaction
    {
        private int numero;
        private Compte expediteur;
        private Compte destinataire;
        private float montant;

        public Transaction(int numero, Compte expediteur, Compte destinataire, float montant)
        {
            this.numero = numero;
            this.expediteur = expediteur;
            this.destinataire = destinataire;
            this.montant = montant;
        }

        public int getNumero() { return numero; }
        public Compte getExpediteur() { return expediteur; }
        public Compte getDestinataire() { return destinataire; }
        public float getMontant() { return montant; }

        public void setNumero(int numero) { this.numero = numero; }
        public void setExpediteur(Compte expediteur) { this.expediteur = expediteur; }
        public void setDestinataire(Compte destinataire) { this.destinataire = destinataire; }
        public void setMontant(float montant) { this.montant = montant; }
    }
}
