using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaBetaPruning
{
    interface IRules
    {
        /// <summary>
        /// Returns pool of available moves
        /// </summary>
        /// <param name="player">Player ID</param>
        /// <returns>array of available moves</returns>
        Move[] GetMoves(int player);
    }
}
