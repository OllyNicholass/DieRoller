using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DieRoller
{
    class Program
    {
        static void Main(string[] args)
        {

            RollDie(true);
        }

        static void RollDie(bool firstRun)
        {
            Console.Write("What would you like to roll?");

            if (firstRun)
            {
                Console.WriteLine(" e.g. 2d6 (2 x 6 sided dice)");
            }
            else
            {
                Console.WriteLine();
            }

            Console.ForegroundColor = ConsoleColor.Blue;
            var input = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.White;

            // Expected input {1-100}{d}{1-100} (2d12 ...)
            var inputMatch = Regex.Matches(input, "^([0-9]{1,3})(d{1})([0-9]{1,3})$");

            if (inputMatch.Count > 0)
            {
                var diceString = inputMatch[0].Groups[1].Value;
                var sizeString = inputMatch[0].Groups[3].Value;

                var dice = Int32.Parse(diceString);
                var size = Int32.Parse(sizeString);
                var diceText = dice > 1 ? "dice" : "die";

                if (dice > 0 && size > 0)
                {
                    Console.WriteLine($"{dice} {size} sided {diceText} rolled.");

                    var dieList = new List<Die>();

                    for (var i = 0; i < dice; i++)
                    {
                        dieList.Add(new Die(size));
                    }

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(CalculateDiceTotal(dieList));

                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(": ");

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(ShowDieValues(dieList));

                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.WriteLine("That was pointless...");
                }

                RollDie(false);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Your entry did not match the correct syntax.");
                Console.ForegroundColor = ConsoleColor.White;
                RollDie(false);
            }
        }

        static string ShowDieValues(List<Die> dieList)
        {
            string dieValues = "";

            for (var i = 0; i < dieList.Count; i++)
            {
                var val = dieList[i].Value;

                if (i == 0)
                {
                    dieValues += val;
                }
                else
                {
                    dieValues += $", {val}";
                }
            }

            return dieValues;
        }

        static int CalculateDiceTotal(List<Die> dieList)
        {
            int totalVal = 0;

            for (var i = 0; i < dieList.Count; i++)
            {
                var val = dieList[i].Value;
                totalVal += val;
            }

            return totalVal;
        }
    }
}
