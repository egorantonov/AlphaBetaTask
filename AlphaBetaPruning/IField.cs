using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaBetaPruning
{
    public interface IField
    {
        /// <summary>
        /// Get cell value
        /// </summary>
        /// <param name="row">row number</param>
        /// <param name="column">column number</param>
        /// <returns>Cell value</returns>
        int Get(int row, int column);

        /// <summary>
        /// Performs a move
        /// </summary>
        /// <param name="move">Move coordinates</param>
        /// <param name="player">Player ID</param>
        void DoMove(Move move, int player);

        /// <summary>
        /// Cancels a move
        /// </summary>
        /// <param name="move">Move coordinates</param>
        /// <param name="player">Player ID</param>
        void UndoMove(Move move, int player);

        /// <summary>
        /// Checks game state 
        /// </summary>
        /// <returns>true if game is over</returns>
        bool IsGameOver();
    }
}
