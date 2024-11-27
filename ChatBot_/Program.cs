using ChatBot_.AnalisadorLexico;

namespace ChatBot_
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Digite a frase a ser validada. ");
            String input = Console.ReadLine();
            Analisador analisador = new Analisador();
            analisador.ValidateCharacter(input);

        }
    }
}