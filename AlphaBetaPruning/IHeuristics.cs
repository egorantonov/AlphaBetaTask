using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaBetaPruning
{
    public interface IHeuristics<T> where T: IField
    {
        /// <summary>
        /// Returns heuristic result of the field
        /// </summary>
        /// <param name="Field"></param>
        /// <param name="move"></param>
        /// <param name="player"></param>
        /// <returns></returns>
        int Score(T Field, Move move, int player);
    }
}
