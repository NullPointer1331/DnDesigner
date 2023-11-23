namespace DnDesigner.Models
{
    /// <summary>
    /// An object used to roll dice in the game
    /// </summary>
    static class Dice
    {
        #region properties

        /// <summary>
        /// The random seed used to roll
        /// </summary>
        static Random roll = new();

        /// <summary>
        /// The upper limit to generate a random int from 1 to 4
        /// </summary>
        static int D4 = 5;

        /// <summary>
        /// The upper limit to generate a random int from 1 to 6
        /// </summary>
        static int D6 = 7;

        /// <summary>
        /// The upper limit to generate a random int from 1 to 8
        /// </summary>
        static int D8 = 9;

        /// <summary>
        /// The upper limit to generate a random int from 1 to 10
        /// </summary>
        static int D10 = 11;

        /// <summary>
        /// The upper limit to generate a random int from 1 to 12
        /// </summary>
        static int D12 = 13;

        /// <summary>
        /// The upper limit to generate a random int from 1 to 20
        /// </summary>
        static int D20 = 21;

        /// <summary>
        /// The upper limit to generate a random int from 1 to 100
        /// </summary>
        static int D100 = 101;

        #endregion


        /// <summary>
        /// Rolls a given size die a given number of times
        /// </summary>
        /// <param name="dieSize">The maximum number on the die</param>
        /// <param name="dieCount">How many dice will be rolled</param>
        /// <returns>The sum of the rolls</returns>
        public static string RollBasic(int dieSize, int dieCount)
        {
            int rollTotal = roll.Next(1, dieSize + 1);
            string resultString = rollTotal.ToString();

            // if die count is greater than 1, roll and concat string
            for (int i = 1; i < dieCount; i++)
            {
                int currRoll = roll.Next(1, dieSize + 1);
                rollTotal += currRoll;
                resultString += " + " + currRoll.ToString();
            }
            if (dieCount > 1)
            {
                resultString += " = " + rollTotal.ToString();
            }
            return resultString;
        }

        /// <summary>
        /// Rolls a given size die a given number of times
        /// </summary>
        /// <param name="dieSize">The maximum number on the die</param>
        /// <param name="dieCount">How many dice will be rolled</param>
        /// <param name="modifier">The characters modifier for this roll</param>
        /// <returns>The sum of the rolls</returns>
        public static string RollBasic(int dieSize, int dieCount, int modifier)
        {
            int rollTotal = roll.Next(1, dieSize + 1);
            string resultString = rollTotal.ToString();

            // if die count is greater than 1, roll and concat string
            for (int i = 1; i < dieCount; i++)
            {
                int currRoll = roll.Next(1, dieSize + 1);
                rollTotal += currRoll;
                resultString += " + " + currRoll.ToString();
            }

            // add on modifier
            rollTotal += modifier;
            resultString += " + " + modifier.ToString();
            resultString += " = " + rollTotal.ToString();
            return resultString;
        }

        /// <summary>
        /// Checks if a roll is a 20
        /// </summary>
        /// <param name="roll">The roll to check</param>
        /// <returns>Natural 20 or the original value</returns>
        public static string CheckNat20(int roll)
        {
            if (roll == 20)
            {
                return "Natural 20!";
            }
            return roll.ToString();
        }

        /// <summary>
        /// Rolls one d20
        /// </summary>
        /// <returns>The result of the roll</returns>
        public static string RollSingleD20()
        {
            int singleRoll = roll.Next(1, D20);
            return CheckNat20(singleRoll);
        }

        /// <summary>
        /// Rolls one d20
        /// </summary>
        /// <param name="modifier">The characters modifier for this roll</param>
        /// <returns>The result of the roll</returns>
        public static string RollSingleD20(int modifier)
        {
            int singleRoll = roll.Next(1, D20);
            return CheckNat20(singleRoll) + " Modified by " + modifier.ToString() +
                " = " + (singleRoll + modifier).ToString();
        }

        /// <summary>
        /// Rolls two d20 and returns the result
        /// </summary>
        /// <param name="rollType">True if roll is at advantage
        ///    , False if roll is at disadvantage</param>
        /// <returns>The result of the roll</returns>
        public static string RollAdvOrDis(bool rollType)
        {
            int roll1 = roll.Next(1, D20);
            int roll2 = roll.Next(1, D20);

            // Advantage, take the higher roll
            if (rollType)
            {
                return "You Keep A: " + CheckNat20(Math.Max(roll1, roll2)) +
                    ", Lower Roll: " + CheckNat20(Math.Min(roll1, roll2));
            }
            // Disadvantage, take the lower roll
            return "You Keep A: " + CheckNat20(Math.Min(roll1, roll2)) +
                ", Higher Roll: " + CheckNat20(Math.Max(roll1, roll2));
        }

        /// <summary>
        /// Rolls two d20 and returns the result
        /// </summary>
        /// <param name="rollType">True if roll is at advantage
        ///    , False if roll is at disadvantage</param>
        /// <param name="modifier">The characters modifier for this roll</param>
        /// <returns>The result of the roll</returns>
        public static string RollAdvOrDis(bool rollType, int modifier)
        {
            int roll1 = roll.Next(1, D20);
            int roll2 = roll.Next(1, D20);

            // Advantage, take the higher roll
            if (rollType)
            {
                return "You Keep A: " + CheckNat20(Math.Max(roll1, roll2)) +
                        " Modified by " + modifier.ToString() +
                        " = " + (Math.Max(roll1, roll2) + modifier).ToString() +
                    ", Lower Roll: " + CheckNat20(Math.Min(roll1, roll2));
            }
            // Disadvantage, take the lower roll
            return "You Keep A: " + CheckNat20(Math.Min(roll1, roll2)) +
                        " Modified by " + modifier.ToString() +
                        " = " + (Math.Min(roll1, roll2) + modifier).ToString() +
                ", Higher Roll: " + CheckNat20(Math.Max(roll1, roll2));
        }
    }    
}
