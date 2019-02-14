using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ACES
{
 

    class Analyzer
    {
        /// <summary>
        /// The current class being run
        /// </summary>
        public Class CurrentClass { get; private set; }

        /// <summary>
        /// System interface layer
        /// </summary>
        public SystemInterface CurrentSystem { get; private set; }

        public Analyzer ()
        {
            CurrentSystem = new SystemInterface();
        }


        

    }//class 
}//namespace
