namespace Calculadora;
using System;
using System.Formats.Asn1;
using System.Globalization;
using System.Text.RegularExpressions;

public sealed class Symbols
{
    //as variaveis abaixo sao o nome do grupo e os simbolos dele, sao readonly pq so serao alteradas na criação q ja ocorre dentro da propria classe
    private readonly string m_Name;
    private readonly char[] m_Symbols;

    public Symbols(){}

    //construtor privado para que somente a propria classe possa se criar
    private Symbols(string Name, char[] Symbols)
    {
        this.m_Name = Name;
        this.m_Symbols = Symbols;
    }

    //abaixo a construção dos dois grupos de simbolos que serao usados

    public static Symbols Numbers = new Symbols
    (
        "Numbers",
        //Isso aqui é uma collection expression, funciona igual se usasse new[]{arguments} pra colocar um array;
        ['0', '1', '2', '3', '4', '5', '6', '7', '8', '9']
    );

    public static Symbols Operators = new Symbols
    (
        "Operators",
        ['+', '-', '/', '*']
    );
    //abaixo os dois metodos que checam numeros e operadores;
    public Boolean CheckForNumber(char Symbol)
    {
        if(Numbers.m_Symbols.Any(Symbol.Equals))
        {
            return true;
        }
        return false;     
    }

    public Boolean CheckForOperator(char Symbol)
    {
        if(Operators.m_Symbols.Any(Symbol.Equals))
        {
            return true;
        }
        return false;   
    }
    //metodo para guardo o numero

    public int StoreNumber(string Number)
    {
        int trueNumber = 0;
        try
        {
            trueNumber = int.Parse(Number);
        }
        catch(FormatException ex)
        {
            trueNumber = 0;
        }
        return trueNumber;
    }
    public int StoreNumber(string Number, bool Confirmation)
    {
        int trueNumber = 0;
        if(Confirmation)
        {
                try
                {
                    trueNumber = int.Parse(Number);
                    trueNumber = -trueNumber;
                }
                    catch(FormatException ex)
                {
                    trueNumber = 0;
                }
            Confirmation = false;
            return trueNumber;
        }
        try
        {
            trueNumber = int.Parse(Number);
        }
        catch(FormatException ex)
        {
            trueNumber = 0;
        }
        Confirmation = false;
        return trueNumber;
    }

    //metodo pro calculo (FINALMENTE)
    public int ProcessEquation(int FirstNumber, int SecondNumber, char OperatorChar)
    {
        int Result = 0;
        switch(OperatorChar)
        {
            case '+':
                Result = FirstNumber + SecondNumber;
            break;
            case '-':
                Result = FirstNumber - SecondNumber;
            break;
            case '/':
                Result = FirstNumber / SecondNumber;
            break;
            case '*':
                Result = FirstNumber * SecondNumber;
            break;
        }
        return Result;
    }

    //lendo a string que contem a equação e separando cada tipo de simbolo;
    public void ReadEquation(string Equation)
    {
        string NumbersConfirmed = "";
        string OperatorsConfirmed = "";
        string InvalidsConfirmed = "";

        string ActualNumbers = "";
        int TempNumber1 = 0;
        int TempNumber2 = 0;
        char NextOperation = ' ';
        int FinalResult = 0;
        bool Confirmation = false;

        //separando numeros de operadores de invalidos;
        foreach (char Symbol in Equation)
        {
            if(Symbol == ' ')
            {
                continue;
            }
            if(Numbers.CheckForNumber(Symbol) == true)
            {
                NumbersConfirmed += Symbol;
                ActualNumbers += Symbol;
            }
            else if(Operators.CheckForOperator(Symbol) == true)
            {
                if(ActualNumbers == "")
                {
                    Confirmation = true;
                }

                if(TempNumber1 != 0)
                {
                    TempNumber2 = StoreNumber(ActualNumbers);
                    TempNumber1 = ProcessEquation(TempNumber1, TempNumber2, NextOperation);
                    ActualNumbers = "";
                }
                else
                {
                    TempNumber1 = StoreNumber(ActualNumbers, Confirmation);
                    ActualNumbers = "";
                }
                NextOperation = Symbol;
                OperatorsConfirmed += Symbol;            
            }
            else
            {
                InvalidsConfirmed += Symbol;
                Console.WriteLine("Failure to execute, invalid character at: " + InvalidsConfirmed + ". Please, retry...");
                break;
            }
                
        }

        TempNumber2 = Int32.Parse(ActualNumbers);
        FinalResult = ProcessEquation(TempNumber1, TempNumber2, NextOperation);

        Console.WriteLine("FINAL RESULT = "+ FinalResult);
        //Console.WriteLine("Full Equation: "+ Equation +" Numbers Counted: " + NumbersConfirmed + " Operators Counted: " + OperatorsConfirmed);
    }
}