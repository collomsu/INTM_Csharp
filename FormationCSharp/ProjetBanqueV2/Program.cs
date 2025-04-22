using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetBanqueV2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string fInComptes = @"C:\Users\wksadmin\source\repos\collomsu\INTM_Csharp\FormationCSharp\ProjetBanqueV2\Jeux_tests_2\Comptes_6.txt";
            string fInTransactions = @"C:\Users\wksadmin\source\repos\collomsu\INTM_Csharp\FormationCSharp\ProjetBanqueV2\Jeux_tests_2\Transactions_6.txt";
            string fInGestionnaires = @"C:\Users\wksadmin\source\repos\collomsu\INTM_Csharp\FormationCSharp\ProjetBanqueV2\Jeux_tests_2\Gestionnaires_6.txt";
            string fOutStatutTrans = @"C:\Users\wksadmin\source\repos\collomsu\INTM_Csharp\FormationCSharp\ProjetBanqueV2\statutTransactions.csv";
            string fOutStatusOperations = @"C:\Users\wksadmin\source\repos\collomsu\INTM_Csharp\FormationCSharp\ProjetBanqueV2\statutOperations.csv";
            string fOutMetrologie = @"C:\Users\wksadmin\source\repos\collomsu\INTM_Csharp\FormationCSharp\ProjetBanqueV2\metrologie.csv";

            GestionComptes gc = new GestionComptes(fInComptes, fInTransactions, fInGestionnaires, fOutStatutTrans, fOutStatusOperations, fOutMetrologie);
            gc.TraiterGestionnairesEntree();

            gc.TraiterCompteEntree();
            gc.TraiterTransactionEntree();

            Console.ReadKey();
        }
    }
}
