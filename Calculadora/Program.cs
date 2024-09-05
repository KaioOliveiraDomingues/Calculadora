using System.Globalization;
using System.Numerics;
using Calculadora;
Symbols symbols = new Symbols();

while(true)
{
    Console.Write("Digite a equação: ");
    string Texto = Console.ReadLine();
    symbols.ReadEquation(Texto);
}