using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace ChatBot_.AnalisadorLexico
{
    public class Analisador
    {


        public string CheckSimilarity(String character) {




            return string.Empty;
        }

        public bool ValidateCharacter(String input) {
            //Regex rgx = new Regex(input);
            String alphabet = @"(?i)^[a-zA-Z0-9 .,;:?!""'()\[\]{}<>¨+=*/%çáéíóúâêôãõàèìòùäëïöüñ$&@#^~`´|\\_\t\n\r-]*$";
            if (Regex.IsMatch(input,alphabet))
            {
                return true;
            }
            else
            {
                List<char> invalidChars = new List<char>();
                foreach (char c in input) {
                    if (!Regex.IsMatch(c.ToString(),alphabet)) {
                        if (!invalidChars.Any(x => x.Equals(c))) { 
                            invalidChars.Add(c);
                        }
                    }
                }
                if (invalidChars.Count > 0) {
                    string InvalidCharacter = String.Join("",invalidChars.Select(c => $"[{c}]"));
                    Console.WriteLine($"Error: Invalid Character: {InvalidCharacter}");
                }
                return false;
            }
        }
    }
}
