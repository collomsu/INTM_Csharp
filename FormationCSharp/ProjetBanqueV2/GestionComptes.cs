using System;
using System.Collections.Generic;
using System.Globalization;

namespace ProjetBanqueV2
{
    public class GestionComptes
    {

        private List<Gestionnaire> _gestionnaires;
        private string _pathComptes;
        private string _pathTransactions;
        private string _pathGestionnaires;
        private string _pathStatutsTransations;
        private string _pathStatusOperations;
        private string _pathMetrologie;
        

        public GestionComptes(string pathComptes, string pathTransactions, string pathGestionnaires, string pathStatutsTransations, string pathStatusOperations, string pathMetrologie)
        {
            this._pathComptes = pathComptes;
            this._pathTransactions = pathTransactions;
            this._pathGestionnaires = pathGestionnaires;
            this._pathStatutsTransations = pathStatutsTransations;
            this._pathStatusOperations = pathStatusOperations;
            this._pathMetrologie = pathMetrologie;
            this._gestionnaires = new List<Gestionnaire>();
        }

        public void AddGestionnaire(Gestionnaire ges)
        {
            this._gestionnaires.Add(ges);
        }

        public void RemoveGestionnaire(Gestionnaire ges)
        {
            this._gestionnaires.Remove(ges);
        }

        /// <summary>
        /// Traite un fichier CSV d'entrée des comptes à ajouter
        /// Format d'entrée : numcpt;solde
        /// </summary>
        public void TraiterCompteEntree()
        {
            FichierEntree fe = new FichierEntree(_pathComptes, System.IO.FileMode.Open);
            FichierSortie fs = new FichierSortie(_pathStatusOperations, System.IO.FileMode.OpenOrCreate);
            fe.Open();
            fs.Open();

            string codeRetour;
            int idCpt;
            string idStr, dateStr, soldeStr, entreeStr, sortieStr;
            float solde;

            while (!fe.EndOfStream())
            {
                var line = fe.ReadLine();
                var values = line.Split(';');
                codeRetour = "KO";
                idStr = "";
                dateStr = "";
                soldeStr = "";
                entreeStr = "";
                sortieStr = "";

                try
                {
                    if (values.Length != 5)
                    {
                        throw new Exception("Formatage du fichier incorrect.");
                    }

                    idStr = values[0];
                    dateStr = values[1];
                    soldeStr = values[2];
                    entreeStr = values[3];
                    sortieStr = values[4];
                    solde = 0;

                    if (!int.TryParse(values[0], out idCpt))
                    {
                        throw new Exception("Formatage de l'identifiant incorrect.");
                    }

                    DateTime dt = ConvertStringToDateTime(dateStr);

                    if (DateTime.Compare(dt, default(DateTime)) == 0)
                    {
                        throw new Exception("Conversion de la date impossible.");
                    }

                    if (!String.IsNullOrEmpty(values[2]))
                    {
                        if (!float.TryParse(values[2], out solde))
                        {
                            throw new Exception("Formatage du solde incorrect.");
                        }
                        
                        if (solde < 0)
                        {
                            throw new Exception("Solde négatif.");
                        }
                    }

                    Gestionnaire ges;
                    int entreeParsed, sortieParsed;
                    bool entreeIsParsed = int.TryParse(values[3], out entreeParsed);
                    bool sortieIsParsed = int.TryParse(values[4], out sortieParsed);

                    switch ((entreeIsParsed, sortieIsParsed))
                    {
                        case (true, false):
                            ges = SearchGestionnaire(_gestionnaires, entreeParsed);
                            if (ges == null)
                            {
                                throw new Exception("Le gestionnaire est inconnu.");
                            }

                            codeRetour = ges.CreateCompte(idCpt, solde, dt);

                            if (codeRetour == "KO")
                            {
                                throw new Exception("La création du compte a échoué.");
                            }

                            break;
                        case (false, true):
                            ges = SearchGestionnaire(_gestionnaires, sortieParsed);

                            if (ges == null)
                            {
                                throw new Exception("Le gestionnaire est inconnu.");
                            }

                            Compte cptCloture = SearchCompte(ges.Comptes, idCpt);

                            if (cptCloture == null)
                            {
                                throw new Exception("Le compte est inconnu.");
                            }
                            codeRetour = ges.CloseCompte(cptCloture, dt);

                            if (codeRetour == "KO")
                            {
                                throw new Exception("La cloture du compte a échoué.");
                            }
                            break;
                        case (true, true):
                            Gestionnaire gesIn = SearchGestionnaire(_gestionnaires, entreeParsed);
                            Gestionnaire gesOut = SearchGestionnaire(_gestionnaires, sortieParsed);
                            if (gesIn == null || gesOut == null)
                            {
                                throw new Exception("Au moins un des deux gestionnaires est inconnu.");
                            }

                            Compte cptCession = SearchCompte(gesIn.Comptes, idCpt);

                            if (cptCession == null)
                            {
                                throw new Exception("Le compte est inconnu.");
                            }

                            codeRetour = gesIn.CessionCompte(cptCession, gesOut);

                            if (codeRetour == "KO")
                            {
                                throw new Exception("La cession du compte a échoué.");
                            }
                            break;
                        default:
                            throw new Exception("Aucune entrée ou sortie, traitement de l'opération impossible.");
                            break;
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erreur traitement compte : {ex}");
                }
                finally
                {
                    Console.WriteLine($"{idStr};{dateStr};{soldeStr};{entreeStr};{sortieStr};{codeRetour}");
                    fs.WriteLine($"{idStr};{dateStr};{soldeStr};{entreeStr};{sortieStr};{codeRetour}");
                }
            }
            fe.Close();
            fs.Close();

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

            string codeRetour, idStr, dateStr, montantStr, expStr, destStr;

            int idTrans, idExp, idDest;
            float montant;

            while (!fe.EndOfStream())
            {
                codeRetour = "KO";
                var line = fe.ReadLine();
                var values = line.Split(';');

                idStr = "";
                dateStr = "";
                montantStr = "";
                expStr = "";
                destStr = "";

                try
                {
                    if (values.Length != 5)
                    {
                        throw new Exception("Formatage du fichier incorrect.");
                    }

                    idStr = values[0];
                    dateStr = values[1];
                    montantStr = values[2];
                    expStr = values[3];
                    destStr = values[4];

                    if (!int.TryParse(idStr, out idTrans))
                    {
                        throw new Exception("Formatage de l'identifiant incorrect.");
                    }

                    DateTime dt = ConvertStringToDateTime(dateStr);

                    if (DateTime.Compare(dt, default(DateTime)) == 0)
                    {
                        throw new Exception("Conversion de la date impossible.");
                    }

                    if (!float.TryParse(montantStr, out montant))
                    {
                        throw new Exception("Formatage du montant incorrect.");
                    }

                    if (montant < 0)
                    {
                        throw new Exception("Montant négatif.");
                    }

                    if (!int.TryParse(expStr, out idExp))
                    {
                        throw new Exception("Formatage de l'expediteur incorrect.");
                    }

                    if (!int.TryParse(destStr, out idDest))
                    {
                        throw new Exception("Formatage du destinataire incorrect.");
                    }

                    Compte cptExp;
                    Compte cptDest;

                    int entreeParsed, sortieParsed;
                    bool entreeIsParsed = int.TryParse(values[3], out entreeParsed);
                    bool sortieIsParsed = int.TryParse(values[4], out sortieParsed);

                    switch ((idExp > 0, idDest > 0))
                    {
                        case (true, false):
                            cptExp = SearchCompteInGestionnaire(_gestionnaires, idExp);

                            if (cptExp == null)
                            {
                                throw new Exception("Le compte expediteurs est inconnu.");
                            }

                            if (DateTime.Compare(dt, cptExp.DateCreation) < 0)
                            {
                                throw new Exception("La date d'effet de la transaction opère avant la création du compte cible.");
                            }

                            if (DateTime.Compare(cptExp.DateResiliation, default(DateTime)) != 0 && DateTime.Compare(dt, cptExp.DateResiliation) >= 0)
                            {
                                throw new Exception("La date d'effet de la transaction opère après la résiliation du compte cible.");
                            }

                            codeRetour = Compte.RetirerArgent(cptExp, montant, idTrans);

                            if (codeRetour == "KO")
                            {
                                throw new Exception("Le retrait a échoué.");
                            }
                            break;
                        case (false, true):
                            cptDest = SearchCompteInGestionnaire(_gestionnaires, idDest);

                            if (cptDest == null)
                            {
                                throw new Exception("Le compte destinataire est inconnu.");
                            }

                            if (DateTime.Compare(dt, cptDest.DateCreation) < 0)
                            {
                                throw new Exception("La date d'effet de la transaction opère avant la création du compte cible.");
                            }

                            if (DateTime.Compare(cptDest.DateResiliation, default(DateTime)) != 0 && DateTime.Compare(dt, cptDest.DateResiliation) >= 0)
                            {
                                throw new Exception("La date d'effet de la transaction opère après la résiliation du compte cible.");
                            }

                            codeRetour = Compte.DepotArgent(cptDest, montant, idTrans);

                            if (codeRetour == "KO")
                            {
                                throw new Exception("Le dépot a échoué.");
                            }
                            break;
                        case (true, true):
                            cptExp = SearchCompteInGestionnaire(_gestionnaires, idExp);
                            cptDest = SearchCompteInGestionnaire(_gestionnaires, idDest);

                            if (cptExp == null || cptDest == null)
                            {
                                throw new Exception("L'un des compte expediteur ou destinataire est inconnu.");
                            }

                            if (DateTime.Compare(dt, cptExp.DateCreation) < 0)
                            {
                                throw new Exception("La date d'effet de la transaction opère avant la création du compte expediteur.");
                            }

                            if (DateTime.Compare(cptExp.DateResiliation, default(DateTime)) != 0 && DateTime.Compare(dt, cptExp.DateResiliation) >= 0)
                            {
                                throw new Exception("La date d'effet de la transaction opère après la résiliation du compte expediteur.");
                            }

                            if (DateTime.Compare(dt, cptDest.DateCreation) < 0)
                            {
                                throw new Exception("La date d'effet de la transaction opère avant la création du compte destinataire.");
                            }

                            if (DateTime.Compare(cptDest.DateResiliation, default(DateTime)) != 0 && DateTime.Compare(dt, cptDest.DateResiliation) >= 0)
                            {
                                throw new Exception("La date d'effet de la transaction opère après la résiliation du compte destinataire.");
                            }

                            codeRetour = Compte.Virement(cptExp, cptDest, montant, idTrans);

                            if (codeRetour == "KO")
                            {
                                throw new Exception("Le virement a échoué.");
                            }
                            break;
                        default:
                            throw new Exception("Aucun des destinataires et expediteurs n'est différent de 0.");
                            break;

                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erreur traitement compte : {ex}");
                }
                finally
                {
                    Console.WriteLine($"{idStr};{dateStr};{montantStr};{expStr};{destStr};{codeRetour}");
                    fs.WriteLine($"{idStr};{dateStr};{montantStr};{expStr};{destStr};{codeRetour}");
                }

                Console.WriteLine($"{idStr};{codeRetour} {GetAllSoldesComptes()}");
            }

            fe.Close();
            fs.Close();
        }

        public void TraiterGestionnairesEntree()
        {
            FichierEntree fe = new FichierEntree(_pathGestionnaires, System.IO.FileMode.Open);
            fe.Open();

            Gestionnaire.TypeGestionnaire typeGes;
            int idGes, nbrTransactions;
            string idStr, typeStr, nbrTransStr;
            
            while (!fe.EndOfStream())
            {
                string line = fe.ReadLine();
                string[] values = line.Split(';');

                idGes = 0;
                idStr = "";
                typeStr = "";
                nbrTransStr = "";

                try
                {
                    if (values.Length != 3)
                    {
                        throw new Exception("Formatage du fichier incorrect.");
                    }

                    idStr = values[0];
                    typeStr = values[1];
                    nbrTransStr = values[2];


                    if (!int.TryParse(values[0], out idGes))
                    {
                        throw new Exception("Formatage de l'identifiant incorrect.");
                    }

                    if (!int.TryParse(values[2], out nbrTransactions) && nbrTransactions > 0)
                    {
                        throw new Exception("Formatage du nombre de transactions incorrect.");
                    }

                    if (string.IsNullOrEmpty(typeStr))
                    {
                        throw new Exception("Type de gestionnaire manquant.");
                    }

                    switch (values[1])
                    {
                        case "Particulier":
                            typeGes = Gestionnaire.TypeGestionnaire.Particulier;
                            break;
                        case "Entreprise":
                            typeGes = Gestionnaire.TypeGestionnaire.Entreprise;
                            break;
                        default:
                            typeGes = Gestionnaire.TypeGestionnaire.None;
                            break;
                    }

                    if (typeGes == Gestionnaire.TypeGestionnaire.None)
                    {
                        throw new Exception("Formatage du type de gestionnaire incorrect.");
                    }

                    AddGestionnaire(new Gestionnaire(idGes, typeGes, nbrTransactions));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erreur traitement gestionnaire : {ex}");
                }
                finally
                {
                    Console.WriteLine($"{idStr};{typeStr};{nbrTransStr}");
                }
            }

            fe.Close();
        }

        /// <summary>
        /// Parcours les comptes de la banque pour renvoyer le compte ayant comme numero celui passé en paramètre.
        /// </summary>
        /// <param name="numero"></param>
        /// <returns>Compte</returns>
        public static Compte SearchCompte(List<Compte> lcpt ,int numero)
        {
            Compte compte_trouve = null;
            lcpt.ForEach(compte =>
            {
                if (compte.Numero == numero)
                {
                    compte_trouve = compte;
                    return;
                }
            });
            return compte_trouve;
        }

        public static Gestionnaire SearchGestionnaire(List<Gestionnaire> lges, int numero)
        {
            Gestionnaire gesTrouve = null;
            lges.ForEach(ges =>
            {
                if (ges.Numero == numero)
                {
                    gesTrouve = ges;
                    return;
                }
            });
            return gesTrouve;
        }

        public static Compte SearchCompteInGestionnaire(List<Gestionnaire> lges, int numero)
        {
            Compte compteTrouve = null;
            lges.ForEach(ges =>
            {
                Compte compte = SearchCompte(ges.Comptes, numero);
                if (compte != null)
                {
                    compteTrouve = compte;
                    return;
                }
            });
            return compteTrouve;
        }

        public String GetAllSoldesComptes()
        {
            string soldes = "";
            _gestionnaires.ForEach(ges =>
            {
                foreach (Compte cpt in ges.Comptes)
                {
                    soldes += $"{cpt.Solde} ";
                }
            });
            return soldes;
        }

        public void DisplayAllGestionnaires()
        {
            foreach(Gestionnaire ges in _gestionnaires)
            {
                string typeGes = (ges.Type == Gestionnaire.TypeGestionnaire.Particulier ? "Particulier" : "Entreprise");
                Console.WriteLine($"{ges.Numero};{typeGes};{ges.NbrTransactions}");
            }
        }

        public static DateTime ConvertStringToDateTime(string str)
        {
            DateTime dt = default(DateTime);
            try
            {
                dt = DateTime.ParseExact(str, "d/MM/yyyy", CultureInfo.InvariantCulture);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Format de la date incorrect : {e}");
            }
            return dt;
        }
    }
}
