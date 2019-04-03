using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACES
{
    public class Score
    {
        public int numberCorrect;
        public int numberIncorrect;
        public Score()
        {
            numberCorrect = 0;
            numberIncorrect = 0;
        }

        public override string ToString()
        {
            return numberCorrect + " / " + (numberCorrect + numberIncorrect);
        }
    }
}
