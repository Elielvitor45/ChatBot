using ChatBot_.AnalisadorLexico;

namespace ChatBot_
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Digite a frase a ser validada. ");
            String input = Console.ReadLine();
            //Compilar com o input nulo ou vazio irá utilizar o texto padrão de teste
            Analisador analisador = new Analisador(input);
        }
    }
}