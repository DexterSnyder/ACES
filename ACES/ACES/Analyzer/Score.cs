using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACES
{
    /// <summary>
    /// Small data structure to hold a students score
    /// </summary>
    public class Score
    {
        /// <summary>
        /// The total number of tests that the student passed
        /// </summary>
        public int NumberCorrect;

        /// <summary>
        /// The total number of tests that the student failed
        /// </summary>
        public int NumberIncorrect;

        /// <summary>
        /// Default constructor
        /// </summary>
        public Score()
        {
            NumberCorrect = 0;
            NumberIncorrect = 0;
        }

        /// <summary>
        /// Overwritten so that it displays correctly in the GUI
        /// </summary>
        /// <returns>A string in the format of correct / total</returns>
        public override string ToString()
        {
            return NumberCorrect + " / " + (NumberCorrect + NumberIncorrect);
        }
    }
}
