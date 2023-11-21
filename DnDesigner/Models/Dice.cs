namespace DnDesigner.Models
{
    /// <summary>
    /// An object used to roll dice in the game
    /// </summary>
    public class Dice
    {
        #region properties

        /// <summary>
        /// The random seed used to roll
        /// </summary>
        Random roll = new();

        /*
        public int D4 { get { return 5; } }

        public int D6 { get { return 7; } }

        public int D8 { get { return 9; } }

        public int D10 { get { return 11; } }

        public int D12 { get { return 13; } }

        public int D20 { get { return 21; } }

        public int D100 { get { return 101; } }
        */

        #endregion

        /// <summary>
        /// Checks if a roll is a 20
        /// </summary>
        /// <param name="roll">The roll to check</param>
        /// <returns>Natural 20 or the original value</returns>
        public string CheckNat20(int roll)
        {
            if(roll == 20)
            {
                return "Natural 20!";
            }
            return roll.ToString();
        }

        /// <summary>
        /// Rolls a given size die a given number of times 
        /// </summary>
        /// <param name="dieSize">The maximum number on the die</param>
        /// <param name="dieCount">How many dice will be rolled</param>
        /// <returns>The sum of the rolls</returns>
        public string RollBasic(int dieSize, int dieCount)
        {
            int result = 0;
            for (int i = 0; i < dieCount; i++)
            {
                result += roll.Next(1, dieSize + 1);
            }
            return result.ToString();
        }

        /// <summary>
        /// Rolls one d20
        /// </summary>
        /// <returns>The result of the roll</returns>
        public string RollSingleD20()
        {
            int singleRoll = roll.Next(1, 21);
            return CheckNat20(singleRoll);
        }

        /// <summary>
        /// Rolls two d20 and returns the result
        /// </summary>
        /// <param name="rollType">True if roll is at advantage
        ///    , False if roll is at disadvantage</param>
        /// <returns>The result of the roll</returns>
        public string RollAdvOrDis(bool rollType)
        {
            int roll1 = roll.Next(1, 21);
            int roll2 = roll.Next(1, 21);
            if (rollType)
            {
                return "You Keep A: " + CheckNat20(Math.Max(roll1, roll2)) + ", Lower Roll: " + CheckNat20(Math.Min(roll1, roll2));
            }
            return "You Keep A: " + CheckNat20(Math.Min(roll1, roll2)) + ", Higher Roll: " + CheckNat20(Math.Max(roll1, roll2));
        }
    }    
}
