using System;
public class Program
{
    public static void Main()
    {
        //#### Daily Programmer challenge 379 ####
        //## https://www.reddit.com/r/dailyprogrammer/comments/cdieag/20190715_challenge_379_easy_progressive_taxation/
        //# Challenge: Given a whole number income up to 100,000,000, 
        //# find the amount of tax owed based on given brackets.
        //# find the percentage of total income given as tax.

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

        Console.Write("Please input income: ");

        int initialIncome = int.Parse(Console.ReadLine());
        int income = initialIncome;
        int tax = 0;
        double currentTaxableIncome = 0.0;


        if(income >= 100000)
        {
            currentTaxableIncome = income - 100000;
            tax += (int)Math.Round(Math.Floor(currentTaxableIncome * 0.4));

            currentTaxableIncome = 0.0;
            income = 100000;
        }

        if (income >= 30000)
        {
            currentTaxableIncome = income - 30000;
            tax += (int)Math.Round(Math.Floor(currentTaxableIncome * 0.25));

            currentTaxableIncome = 0.0;
            income = 30000;
        }

        if (income >= 10000)
        {
            currentTaxableIncome = income - 10000;
            tax += (int)Math.Round(Math.Floor(currentTaxableIncome * 0.1));
        }

        Console.WriteLine($"Tax: {tax} to pay.");

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