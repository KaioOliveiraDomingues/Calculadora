using System.Globalization;
using System.Numerics;
using Calculadora;

Calculo calculo = new Calculo();
Symbols symbols = new Symbols();

string Texto = Console.ReadLine();

symbols.ReadEquation(Texto);