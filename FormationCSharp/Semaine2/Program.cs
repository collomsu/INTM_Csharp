using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semaine2
{
    internal class Program
    {
        List<Compte> comptes = new List<Compte>();
        Compte banque = new Compte(0, 999999999);

        int last_numero_transaction = 0;
        int last_numero_compte = 0;

        string code_retour = "OK";
        string fInComptes = @"C:\Users\wksadmin\source\repos\collomsu\INTM_Csharp\FormationCSharp\Semaine2\comptes.csv";
        string fInTransactions = @"C:\Users\wksadmin\source\repos\collomsu\INTM_Csharp\FormationCSharp\Semaine2\transactions.csv";
        string fOutStatutTrans = @"C:\Users\wksadmin\source\repos\collomsu\INTM_Csharp\FormationCSharp\Semaine2\statutTransactions.csv";

        static void Main(string[] args)
        {
            var prog = new Program();
            prog.TraiterCompteEntree();
            prog.TraiterTransactionEntree();

            Console.ReadKey();
        }

        /// <summary>
        /// Traite l'opération du dépot d'argent sur un compte à partir du numéro de compte et du montant à .
        /// Le montant doit etre positif.
        /// </summary>
        /// <param name="numero_cpt"></param>
        /// <param name="montant"></param>
        public void DepotArgent(int numero_cpt, float montant)
        {
            if (montant > 0)
            {
                Compte compte = SearchCompte(numero_cpt);
                last_numero_transaction++;
                Transaction depot = new Transaction(last_numero_transaction, banque, SearchCompte(numero_cpt), montant);
                TraiterTransaction(banque, SearchCompte(numero_cpt), montant);
                compte.AddTransaction(depot);
                code_retour = "OK";
            }
            else
            {
                Console.WriteLine("Erreur : " + montant + " non positif");
                code_retour = "KO";
            }
        }

        /// <summary>
        /// Traite l'opération de retrait d'argent sur un compte à partir du numéro de compte et du montant à retirer.
        /// Le montant doit etre positif et inférieur au solde du compte à retirer.
        /// </summary>
        /// <param name="numero_cpt"></param>
        /// <param name="montant"></param>
        public void RetirerArgent(int numero_cpt, float montant)
        {
            if (montant > 0)
            {
                Compte compte = SearchCompte(numero_cpt);
                if (compte.GetSolde() > montant)
                {
                    Compte cpt = SearchCompte(numero_cpt);
                    if ((SommmeDixDernierVirement(cpt) + montant) <= 1000)
                    {
                        last_numero_transaction++;
                        Transaction retrait = new Transaction(last_numero_transaction, SearchCompte(numero_cpt), banque, montant);
                        TraiterTransaction(SearchCompte(numero_cpt), banque, montant);
                        compte.AddTransaction(retrait);
                        code_retour = "OK";
                    }
                    else
                    {
                        Console.WriteLine("Erreur : " + montant + " ferait dépasser la somme maximale pouvant être retirer");
                        code_retour = "KO";
                    }
                }
                else
                {
                    Console.WriteLine("Erreur : " + montant + " superieur au solde du compte");
                    code_retour = "KO";
                }
            }
            else
            {
                Console.WriteLine("Erreur : " + montant + " non positif");
                code_retour = "KO";
            }
        }

        /// <summary>
        /// Traite l'opération de virement entre deux compte d'un montant défini devant être positif.
        /// </summary>
        /// <param name="numero_cpt_exp"></param>
        /// <param name="numero_cpt_dest"></param>
        /// <param name="montant"></param>
        public void Virement(int numero_cpt_exp, int numero_cpt_dest, float montant)
        {
            Compte cpt_exp = SearchCompte(numero_cpt_exp);
            Compte cpt_dest = SearchCompte(numero_cpt_dest);

            if (DemandePrelevement(cpt_exp, montant))
            {
                last_numero_transaction++;
                Transaction virement = new Transaction(last_numero_transaction, cpt_exp, cpt_dest, montant);
                cpt_dest.UpdateSolde(montant);
                cpt_exp.AddTransaction(virement);
                cpt_dest.AddTransaction(virement);
                code_retour = "OK";
            }
            else
            {
                Console.WriteLine("Erreur : Lors du prélèvement sur le compte expéditeur.");
                code_retour = "KO";
            }
        }

        /// <summary>
        /// Parcours les comptes de la banque pour renvoyer le compte ayant comme numero celui passé en paramètre.
        /// </summary>
        /// <param name="numero"></param>
        /// <returns>Compte</returns>
        public Compte SearchCompte(int numero)
        {
            Compte compte_trouve = banque;
            comptes.ForEach(compte =>
            {
                if (compte.GetNumero() == numero)
                {
                    compte_trouve = compte;
                    return;
                }
            });
            return compte_trouve;
        }

        /// <summary>
        /// Traite le transfert d'argent donné par le paramètre montant.
        /// Déduit ce montant du solde du compte expediteur et l'ajoute au compte destinataire.
        /// </summary>
        /// <param name="compte_exp"></param>
        /// <param name="compte_dest"></param>
        /// <param name="montant"></param>
        public void TraiterTransaction(Compte compte_exp, Compte compte_dest, float montant)
        {
            compte_exp.UpdateSolde(-montant);
            compte_dest.UpdateSolde(montant);
        }

        /// <summary>
        /// Prend en entrée un fichier CSV des comptes à ajouter
        /// Format d'entrée : numcpt;solde
        /// </summary>
        public void TraiterCompteEntree()
        {
            using (var reader = new StreamReader(fInComptes))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(';');

                    float solde = 0;

                    if (values.Length > 1 && !String.IsNullOrEmpty(values[1]))
                    {
                        Console.WriteLine(values[1]);
                        solde = (int)Convert.ToDouble(values[1]);
                    }
                    comptes.Add(new Compte(int.Parse(values[0]), solde));

                    Console.WriteLine("Compte inséré !");
                    Console.WriteLine(values.Length);
                }
            }
        }

        /// <summary>
        /// Prend en entrée un fichier CSV des transactions a traiter.
        /// Format d'entrée : numtransac;montant;cptexp;cptdest
        /// </summary>
        public void TraiterTransactionEntree()
        {
            FileStream fout = new FileStream(fOutStatutTrans, FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter statWriter = new StreamWriter(fout);
            using (var reader = new StreamReader(fInTransactions))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(';');

                    int num_transaction = int.Parse(values[0]);
                    float montant_transaction = (int)Convert.ToDouble(values[1]);
                    int cpt_exp = int.Parse(values[2]);
                    int cpt_dest = int.Parse(values[3]);

                    if (cpt_exp == 0 && cpt_dest != 0)
                    {
                        DepotArgent(cpt_dest, montant_transaction);
                    }
                    else if (cpt_exp != 0 && cpt_dest == 0)
                    {
                        RetirerArgent(cpt_exp, montant_transaction);
                    }
                    else
                    {
                        Virement(cpt_exp, cpt_dest, montant_transaction);
                    }

                    statWriter.WriteLine(num_transaction + ";" + code_retour);
                    Console.WriteLine(num_transaction + ";" + code_retour);
                }
                reader.Close();
            }
            statWriter.Close();


        }

        /// <summary>
        /// Retourne la somme des 10 derniers virements effectué par le compte passé en paramètre.
        /// </summary>
        /// <param name="compte"></param>
        /// <returns>float</returns>
        public float SommmeDixDernierVirement(Compte compte)
        {
            int nb_transactions = 0;
            float montant = 0;
            foreach (var transaction in compte.GetTransactions())
            {
                nb_transactions++;
                if (nb_transactions > 10)
                {
                    break;
                }

                if (transaction.getExpediteur().GetNumero() != 0 && transaction.getDestinataire().GetNumero() != 0 && transaction.getExpediteur().GetNumero() == compte.GetNumero())
                {
                    montant += transaction.getMontant();
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
        public bool DemandePrelevement(Compte compte, float montant)
        {
            if (montant > 0 && compte.GetSolde() > montant && (SommmeDixDernierVirement(compte) + montant) <= 1000)
            {
                compte.UpdateSolde(-montant);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
