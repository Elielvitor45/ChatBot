using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace ChatBot_.AnalisadorLexico
{
    public class Analisador
    {
        public List<string> symbolTable = new List<string>();

        public Queue<string> tokenQueue = new Queue<string>();


        public Analisador(string input)
        {
            if (String.IsNullOrEmpty(input))
            {
                // Texto para teste
                input = "Hoje, seu irmão estava conversando com ele sobre o lugar onde eles iriam passar o momento. Mas a hora já estava avançada e, porém, ainda não tinham decidido. Eles haviam ficado em dúvida se iriam para a nova cidade ou para o outro local.\r\n\r\nEla disse: “Isso é complicado, não sei como resolver”. Mas, se não fosse hoje, talvez houvera uma oportunidade mais tarde. Porém, não achava que seria possível. Quem sabe? Já não importa. O importante é que o tempo estava passando rápido e eles tinham que decidir o que fazer naquele momento. Lá na hora marcada, eles chegaram e houve uma mudança no lugar. Houveram alguns contratempos, mas houveram outros planos que ficaram para depois.";
            }
            // Teste do validador de alfabeto
            if (ValidateCharacter(input))
            {
                // Teste da checagem de similaridade
                if (CheckSimilarity("ABACATE", "abacatezz"))
                {
                    Console.WriteLine("Mesma palavra digitada incorretamente pelo usuario");
                }
                else
                {
                    Console.WriteLine("Palavra incompativeis");
                }

                PopulateSymbolTableAndQueueToken(input);
            }
        }
        public void PopulateSymbolTableAndQueueToken(String input)
        {

            List<string> Temptokenlist = new List<string>();

            //Fonte: https://learn.microsoft.com/en-us/dotnet/standard/base-types/regular-expression-language-quick-reference
            String pattern = @"\w+|[^\w\s]";

            //Fonte: https://learn.microsoft.com/pt-br/dotnet/api/system.text.regularexpressions.matchcollection?view=net-8.0
            MatchCollection matches = Regex.Matches(input, pattern);

            foreach (Match item in matches)
            {
                Temptokenlist.Add(item.Value);
            }
            Console.WriteLine("Tamanho total do texto: " + Temptokenlist.Count);
            StopWords stopWords = new StopWords();

            foreach (String token in Temptokenlist)
            {
                if (stopWords.CompareWithStopwords(token))
                {
                    symbolTable.Add(token);
                    tokenQueue.Enqueue(token);
                }
            }
            Console.WriteLine("Tamanho total da lista final: " + symbolTable.Count);
            Console.WriteLine("Tamanho total da fila final: " + tokenQueue.Count);
        }
        public bool CheckSimilarity(String input1, String input2)
        {
            int tam1 = input1.Length;
            int tam2 = input2.Length;
            int cont = 0;

            List<char> word1 = new List<char>();
            List<char> word2 = new List<char>();

            word1 = input1.ToList();
            word2 = input2.ToList();

            int smallSize = Math.Min(tam1, tam2); // Pega o menor tamanho entre as duas listas
            int diff = Math.Abs(tam1 - tam2); // Faz com que a diferença sempre seja positiva

            if (tam1 == 0) { return false; } else if (tam2 == 0) { return false; }
            if (diff > 2) { return false; }

            cont = diff;

            for (int i = 0; i < smallSize; i++)
            {
                if (cont <= 2)
                {
                    if (!(char.ToUpperInvariant(word1[i]) == char.ToUpperInvariant(word2[i])))
                    {
                        ++cont;
                    }
                }
                else
                {
                    return false;
                }
            }
            if (cont <= 2)
                return true;
            else
                return false;
        }

        public bool ValidateCharacter(String input)
        {
            String alphabet = @"(?i)^[a-zA-Z0-9 .,;:?!""“”'()\[\]{}<>¨+=*/%çáéíóúâêôãõàèìòùäëïöüñ$&@#^~`´|\\_\t\n\r-]*$";
            if (Regex.IsMatch(input, alphabet))
            {
                return true;
            }
            else
            {
                List<char> invalidChars = new List<char>();
                foreach (char c in input)
                {
                    if (!Regex.IsMatch(c.ToString(), alphabet))
                    {
                        if (!invalidChars.Any(x => x.Equals(c)))
                        {
                            invalidChars.Add(c);
                        }
                    }
                }
                if (invalidChars.Count > 0)
                {
                    string InvalidCharacter = String.Join("", invalidChars.Select(c => $"[{c}]"));
                    Console.WriteLine($"Error: Invalid Character: {InvalidCharacter}");
                }
                return false;
            }
        }
    }
}
