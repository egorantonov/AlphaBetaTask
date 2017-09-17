using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaBetaPruning
{
    class BaseField: IField, IRules
    {
        private int winLength;
        private Status status = Status.active;
        private List<Cell> winLine;

        protected int[][] field;

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

        public BaseField(int size, int winLength)
        {
            //TODO: Fill
        }

        public int Get(int row, int column) => field[row][column];
        
        public void DoMove(Move m,int player)
        {
            //TODO: Fill
        }

        public void UndoMove(Move m, int player)
        {
            //TODO: Fill
        }

        public bool IsGameOver() => status != Status.active;

        public Move[] GetMoves(int player)
        {
            //TODO: Fill
            return null;
        }


    }
}
