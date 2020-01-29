using System;
using System.Collections.Generic;
using System.Text;

namespace DieRoller
{
    public class Die
    {
        public Die(int sides)
        {
            RollDie(sides);
        }

        public int Value { get; set; }

        public void RollDie(int sides)
        {
            Value = RandomNumber(1, sides);
        }

        public int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max + 1);
        }
    }
}
