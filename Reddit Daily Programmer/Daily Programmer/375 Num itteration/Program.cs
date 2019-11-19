using System;
public class Program
{
    public static void Main()
    {
        //https://www.reddit.com/r/dailyprogrammer/comments/aphavc/20190211_challenge_375_easy_print_a_new_number_by/\

        //Challenge #375 [Easy] Print a new number by adding one to each of its digit

        //Description
        //A number is input in computer then a new no should get printed by adding one to each of its digit.
        //If you encounter a 9, insert a 10(don't carry over, just shift things around).


        //For example, 998 becomes 10109.

        //Bonus
        //This challenge is trivial to do if you map it to a string to iterate over the input, operate, and then cast it back.
        // Instead, try doing it without casting it as a string at any point, keep it numeric(int, float if you need it) only.

        long input = long.Parse(Console.ReadLine());

        var outPut = Itterate(input);

        Console.WriteLine(outPut);
    }

    public static long Itterate(long input)
    {
        if (input < 10)
        {
            return input + 1;
        }

        long a = input % 10 + 1;
        long b = Itterate(input / 10) * 10;

        if (a == 10)
        {
            return ((b + 1) * 10);
        }
        else
        {
            return b + a;
        }
    }
}