/// <summary>
/// Summary description for Class1
/// </summary>
using System;
using System.Collections.Generic;

namespace Semaine1
{
    internal class Serie4
    {

        // Exercice 1
        // 1) Ce couple clé, valeur va nous permettre d'avoir une clé composé d'un code morse ("==.==.=="), et de sa valeur qui est un caractère l'alphabet ('a', 'b', ...).
        // Elle permettra alors facilement de retrouver la lettre correspondant a notre code grace à la méthode d'indexation dans le dictionnaire.

        public Dictionary<string, char> dicoMorse;
        public Dictionary<char, string> dicoAlphabet;

        public Serie4()
        {
            dicoMorse = new Dictionary<string, char>();
            dicoAlphabet = new Dictionary<char, string>();
            dicoPhone = new Dictionary<string, string>();

            string[] morseAlph = { "=.==", "==.=.=.=", "==.=.==.=", "==.=.=", "=", "=.=.==.=", "==.==.=", "=.=.=.=", "=.=", "=.==.==.==", "==.=.==", "=.==.=.=", "==.==", "==.=", "==.==.==", "=.==.==.=", "==.==.=.==", "=.==.=", "=.=.=", "==", "=.=.==", "=.=.=.==", "=.==.==", "==.=.=.==", "==.=.==.==", "==.==.=.=" };
            char[] alphabetAlph = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };

            for (int i = 0; i < morseAlph.Length; i++)
            {
                dicoMorse.Add(morseAlph[i], alphabetAlph[i]);
                dicoAlphabet.Add(alphabetAlph[i], morseAlph[i]);
            }
        }

        public void CheckString(string code)
        {
            for (int i = 0; i < code.Length; i++)
            {
                if (code[i] != '=' && code[i] != '.')
                {
                    throw new ArgumentException();
                }
            }
        }

        public int LettersCount(string code)
        {
            CheckString(code);
            int nbLettres = 0;

            string[] mots = code.Split(new string[] { "......." }, StringSplitOptions.None);
            List<string[]> listLettres = new List<string[]>();

            foreach (string s in mots)
            {
                listLettres.Add(s.Split(new string[] { "..." }, StringSplitOptions.None));
            }

            foreach (var lettres in listLettres)
            {
                nbLettres += lettres.Length;
            }

            return nbLettres;
        }

        public int WordsCount(string code)
        {
            CheckString(code);
            return code.Split(new string[] { "......." }, StringSplitOptions.None).Length;
        }

        public string MorseTranslation(string code)
        {
            CheckString(code);
            string res = "";

            foreach (var mot in code.Split(new string[] { "......." }, StringSplitOptions.None))
            {
                foreach (var lettre in mot.Split(new string[] { "..." }, StringSplitOptions.None))
                {
                    if (dicoMorse.ContainsKey(lettre))
                    {
                        res += dicoMorse[lettre];
                    }
                    else
                    {
                        res += '+';
                    }
                }
                res += " ";
            }

            return res;
        }

        public string EfficientMorseTranslation(string code)
        {
            string res = "";
            string l;

            foreach (var mot in code.Split(new string[] { "......." }, StringSplitOptions.None))
            {
                foreach (var lettre in mot.TrimStart('.').Split(new string[] { "..." }, StringSplitOptions.None))
                {
                    l = lettre.TrimEnd('.');
                    l = CheckLettre(l.TrimStart('.'));

                    if (dicoMorse.ContainsKey(l))
                    {
                        res += dicoMorse[l];
                    }
                    else
                    {
                        res += '+';
                    }

                    
                }
                res += " ";
            }

            return res;
        }

        public string CheckLettre(string lettre)
        {
            char lastLetter = '=';
            for (int i = 0; i < lettre.Length; i++)
            {
                if (lastLetter == '.' && lettre[i] == '.')
                {
                    lettre = lettre.Remove(i, 1);
                    lastLetter = lettre[i-1];
                }
                else
                {
                    lastLetter = lettre[i];
                }
            }

            return lettre;
        }

        public string MorseEncryption(string sentence)
        {
            string morse = "";
            for (int i = 0; i < sentence.Length; i++)
            {
                if (sentence[i] == ' ')
                {
                    morse += ".......";
                }
                else
                {
                    morse += dicoAlphabet[sentence[i]];

                    if (i != sentence.Length - 1)
                    {
                        morse += "...";
                    }
                }
            }

            return morse;
        }

        // Exercice 2

        // 1) Nous voulons traité les données une a une en fonction de l'ordre dans lequel elles ont été rentré.
        // Une fois qu'une donnée a été traité nous n'avons plus besoin de revenir dessus, et l'ont veux les traiter méthode FIFO.
        // Pour cela la structure la plus adapté est une Queue fonctionnement en FIFO.

        public bool BracketsControls(string sentence)
        {
            Queue<char> queue = new Queue<char>();
            foreach (char c in sentence)
            {
                queue.Enqueue(c);
            }

            char ch = queue.Dequeue();

            return TraitementBracket(queue, ch);
        }

        public bool TraitementBracket(Queue<char> q, char lastc)
        {
            char c = q.Dequeue();
            switch (c)
            {
                case '(':
                case '{':
                case '[':
                    return TraitementBracket(q, c);
                    break;
                case ')':
                    return lastc == '(';
                    break;
                case '}':
                    return lastc == '{';
                    break;
                case ']':
                    return lastc == '[';
                    break;
                default:
                    return TraitementBracket(q, lastc);
                    break;
            }
        }

        // Exercice 3

        // 1) L'annuaire téléphonique demandant une association numero, nom je pense que le choix d'un Dictionary me parait être le plus adéquat.
        // Il va nous permettre d'avoir cette assoication en imposant que les numéros soit unique et qu'il soit toujours associé à un nom.

        public Dictionary<string, string> dicoPhone;

        public bool IsValidPhoneNumber(string phoneNumber)
        {
            if (phoneNumber != null & phoneNumber.Length == 10)
            {
                for (int i = 0; i < phoneNumber.Length; i++)
                {
                    switch (i)
                    {
                        case 0:
                            if (char.GetNumericValue(phoneNumber[i]) != 0)
                            {
                                return false;
                            }
                            break;
                        case 1:
                            if (char.GetNumericValue(phoneNumber[i]) <= 0)
                            {
                                return false;
                            }
                            break;
                        default:
                            if (char.GetNumericValue(phoneNumber[i]) < 0)
                            {
                                return false;
                            }
                            break;
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ContainsPhoneContact(string phoneNumber)
        {
            return dicoPhone.ContainsKey(phoneNumber);
        }

        public void PhoneContact(string phoneNumber)
        {
            Console.WriteLine(dicoPhone[phoneNumber]);
        }

        public bool AddPhoneNumber(string phoneNumber, string name)
        {
            if (!IsValidPhoneNumber(phoneNumber))
            {
                return false;
            }
            else
            {
                dicoPhone.Add(phoneNumber, name);
                return true;
            }
        }

        public bool DeletePhoneNumber(string phoneNumber)
        {
            return dicoPhone.Remove(phoneNumber);
        }

        public void DisplayPhoneBook()
        {
            if (dicoPhone.Count == 0)
            {
                Console.WriteLine("Pas de numéros téléponiques");
            }
            else
            {
                Console.WriteLine("Annuaire téléphonique :");
                Console.WriteLine("-----------------------");

                foreach (var obj in dicoPhone)
                {
                    Console.WriteLine($"{obj.Key} : {obj.Value}");
                }

                Console.WriteLine("-----------------------");
            }
        }
    }
}
