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
        static void Main(string[] args)
        {
            string fInComptes = @"C:\Users\wksadmin\source\repos\collomsu\INTM_Csharp\FormationCSharp\Semaine2\comptes.csv";
            string fInTransactions = @"C:\Users\wksadmin\source\repos\collomsu\INTM_Csharp\FormationCSharp\Semaine2\transactions.csv";
            string fOutStatutTrans = @"C:\Users\wksadmin\source\repos\collomsu\INTM_Csharp\FormationCSharp\Semaine2\statutTransactions.csv";

            GestionComptes gc = new GestionComptes(fInComptes, fInTransactions, fOutStatutTrans);
            gc.TraiterCompteEntree();
            gc.TraiterTransactionEntree();

            Console.ReadKey();
        }
    }
}
