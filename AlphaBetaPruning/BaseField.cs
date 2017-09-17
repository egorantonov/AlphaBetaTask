using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaBetaPruning
{
    class BaseField: IField, IRules
    {
        //players IDs
        public const int XS = 1;
        public const int OS = -1;

        /// <summary>
        /// game status
        /// </summary>
        public enum Status
        {
            active, draw, xswin, oswin
        }

        public BaseField(int size, int )


    }
}
