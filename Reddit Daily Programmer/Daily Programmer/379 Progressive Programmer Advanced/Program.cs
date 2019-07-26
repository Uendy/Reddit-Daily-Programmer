using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class Program
{
    public static void Main()
    {
        //#### Daily Programmer challenge 379 ####
        //## https://www.reddit.com/r/dailyprogrammer/comments/cdieag/20190715_challenge_379_easy_progressive_taxation/
        //# Challenge: Given a whole number income up to 100,000,000, 
        //# find the amount of tax owed based on given brackets.
        //# find the percentage of total income given as tax.

        //Advanced: 
        //# let the user input the tax brackets and percents he wants to.

        #region
        //The nation of Examplania has the following income tax brackets:

        //income cap      marginal tax rate
        //  ¤10,000           0.00(0 %)
        //  ¤30,000           0.10(10 %)
        // ¤100,000           0.25(25 %)
        //    --              0.40(40 %)
        //
        //Given a whole - number income amount up to ¤100,000,000, find the amount of tax owed in Examplania.
        //Round down to a whole number of ¤.
        //
        //Examples
        //tax(0) => 0
        //tax(10000) => 0
        //tax(10009) => 0
        //tax(10010) => 1
        //tax(12000) => 200
        //tax(56789) => 8697
        //tax(1234567) => 473326
        #endregion

        //fetching previously saved brackets and taxRates and displaying them to user to see if still valid.
        string fileName = "Brackets.txt";
        var currentTaxSettings = File.ReadAllLines(fileName);

        foreach (var line in currentTaxSettings)
        {
            var lineTokens = line.Split(' ');
            Console.WriteLine($"¤{lineTokens[0]} - {lineTokens[1]}");
        }

        Console.Write($"Should we keep these brackets? (True/False): ");
        string userInput = Console.ReadLine().ToLower();
        Console.WriteLine();

        if (userInput == "false")
        {
            Console.Write("Number of brackets? ");
            int numberOfBrackets = int.Parse(Console.ReadLine());

            //check if they input any
            bool atleastOne = numberOfBrackets > 0;
            if (!atleastOne)
            {
                Console.WriteLine("Invalid number of brackets");
                Environment.Exit(0);
            }

            Console.WriteLine("Input each bracket minimum with their corresponding tax rate seperated by a space.");
            Console.WriteLine("Ex: 100000-0.4");
            Console.WriteLine();

            //inputing new brackets and their rates (key, value)
            var bracketsAndRates = new Dictionary<int, double>();
            for (int i = 0; i < numberOfBrackets; i++)
            {
                var currentLineTokens = Console.ReadLine().Split('-').ToArray();
                int bracket = int.Parse(currentLineTokens[0]);
                double taxRate = double.Parse(currentLineTokens[1]);

                bracketsAndRates[bracket] = taxRate;
            }
            bracketsAndRates = bracketsAndRates.OrderByDescending(x => x.Key).ToDictionary(x => x.Key, y => y.Value);

            Console.WriteLine();

            TaxCalcAndPrint(bracketsAndRates);

            //saving new brackets to the document
            File.WriteAllText(fileName, string.Empty);
            foreach (var kvp in bracketsAndRates)
            {
                File.AppendAllText(fileName, $"{kvp.Key} {kvp.Value}\n");
            }
        }
        else if (userInput == "true") // continue with current brakcets and tax rates
        {
            var bracketsAndRates = new Dictionary<int, double>();
            foreach (var line in currentTaxSettings)
            {
                var lineTokens = line.Split(' ').ToArray();
                var bracket = int.Parse(lineTokens[0]);
                var taxRate = double.Parse(lineTokens[1]);

                bracketsAndRates[bracket] = taxRate;
            }

            TaxCalcAndPrint(bracketsAndRates);
        }
        else
        {
            Console.WriteLine("Invalid input!");
        }
    }

    // To accept and verify income
    // To calculate the total amount of tax and sshow it as a percent of the total income
    public static void TaxCalcAndPrint(Dictionary<int, double> bracketsAndRates)
    {
        Console.Write("Please input income: ");

        int initialIncome = int.Parse(Console.ReadLine());

        bool aboveIncomeCap = initialIncome > 100000000;
        if (aboveIncomeCap)
        {
            Console.WriteLine($"{initialIncome} is too big");
            Environment.Exit(0);
        }

        int income = initialIncome;
        int tax = 0;
        double currentTaxableIncome = 0.0;

        foreach (var kvp in bracketsAndRates)
        {
            int bracket = kvp.Key;
            double taxRate = kvp.Value;

            bool eligableForCurrentBracket = income >= bracket;
            if (eligableForCurrentBracket)
            {
                currentTaxableIncome = income - bracket;
                tax += (int)Math.Round(Math.Floor(currentTaxableIncome * taxRate));

                currentTaxableIncome = 0.0;
                income = bracket;
            }
        }

        //printing results
        Console.WriteLine($"Tax: ¤{tax} to pay.");

        double totalTaxPercent = Math.Round((double)tax / (initialIncome / 100), 2);

        bool notZero = totalTaxPercent >= 0.00 && totalTaxPercent <= 100.00;
        if (notZero)
        {
            Console.WriteLine($"Which is {totalTaxPercent}% of your income.");
        }
        else // 0 %
        {
            Console.WriteLine($"Which is 0% of your income.");
        }
    }
}