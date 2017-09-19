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
        private State state = State.none;
        private List<Cell> winLine;

        public int[,] field;

        //players IDs
        public const int PLAYER_X = 1;
        public const int PLAYER_O = -1;           

        /// <summary>
        /// Game status: active, draw, status
        /// </summary>
        public enum State
        {
            none, draw, win_x, win_o
        }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="size"></param>
        /// <param name="winLength"></param>
        /// 

        public BaseField(int size, int winLength) : this(new int[size, size], winLength)
        {

        }

        public BaseField( int [,] field ,int winLength)
        {
            if(field.Length == 0 || field.GetLength(0)!= field.GetLength(1))
            {
                //TODO: Exception?
            }
            if(field.Length < winLength)
            {
                //TODO: Exception?
            }
            this.winLength = winLength;
            Array.Copy(field, this.field, field.Length);
            //ReviewState();

        }

        /// <summary>
        /// Get cell's content
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public int Get(int row, int column) => field[row,column];

        public State GetState() => state;

        public void SetState(State state) => this.state = state;

        /// <summary>
        /// Check field state condition
        /// </summary>
        public void CheckState()
        {
            var line = GetWinLine();
            if (line != null)
            {
                int r = line[0].row;
                int c = line[0].column;
                if (field[r, c] == PLAYER_X)
                {
                    SetState(State.win_x);
                }
                else if (field[r, c] == PLAYER_O)
                {
                    SetState(State.win_o);
                }
                else
                {
                    //TODO: Exception?
                }
            }
        }

        public List<Cell> GetWinLine()
        {
            if (winLine == null)
            {
                winLine = GetWinLine(field);
            }
            return winLine;
        }

        private List<Cell> GetWinLine(int [,] field)
        {
            //check rows
            var check = CheckRows(field);
            if ((check != null) && (check.Count() >= winLength))
            {
                return check;
            }
            //if not check columns
            check = CheckColumns(field);
            if ((check != null) && (check.Count() >= winLength))
            {
                return check;
            }
            //if not check diagonals
            if ((check != null)&&(check.Count()>= winLength))
            {
                return check;
            }
            //if not return null
            return null;
        }

        /// <summary>
        /// Check for winning hand in rows
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        private List<Cell> CheckRows(int [,] field)
        {
            //TODO: Parallel
            for (int i = 0; i < field.GetLength(0); i++)
            {
                var line = FieldScanner.ScanRow(field, winLength, i);
                if (line != null) { return line; }
            }
            return null;
        }

        /// <summary>
        /// Check for winning hand in columns
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        private List<Cell> CheckColumns(int [,] field)
        {
            //TODO: Parallel
            for (int i =0; i < field.GetLength(1); i++)
            {
                var line = FieldScanner.ScanCol(field, winLength, i);
                if (line != null) { return line; }
            }
            return null;
        }

        /// <summary>
        /// Check winning hand in diagonals
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        private List<Cell> CheckDiagonals(int [,] field)
        {
            //TODO: Parallel.
            //Do we really need it? ↓
            for (int i = 0; i < field.Length; i++)
            {
                
                List<Cell> line = FieldScanner.ScanUpDiagonal(true, field, winLength, i);
                if (line != null)
                {
                    return line;
                }

                line = FieldScanner.ScanDownDiagonal(true, field, winLength, i);
                if (line != null)
                {
                    return line;
                }
                //???
                line = FieldScanner.ScanUpDiagonal(false, field, winLength, i);
                if (line != null)
                {
                    return line;
                }
                //???
                line = FieldScanner.ScanDownDiagonal(false, field, winLength, i);
                if (line != null)
                {
                    return line;
                }
            }
            return null;
        }

        public void DoMove(Move m,int player)
        {
            if (field[m.Row, m.Col] != 0)
            {
                //TODO: Exception?
            }
            if (GetState() != State.none)
            {
                //TODO: Exception?
            }
            field[m.Row, m.Col] = player;
            //reviewState();
        }

        public void UndoMove(Move m, int player)
        {
            field[m.Row, m.Col] = 0;
            winLine = null;
            //setState(State.none);
        }

        public bool IsGameOver() => state != State.none;

        /// <summary>
        /// Return current winner based on field state
        /// </summary>
        /// <returns></returns>
        public int GetWinner()
        {
            switch (state)
            {
                case State.win_x: return PLAYER_X;
                case State.win_o: return PLAYER_O;
                default: return 0;
            }
        }

        /// <summary>
        /// Return field dimesion
        /// </summary>
        /// <returns></returns>
        public int GetRowCount() => field.GetLength(1);
        public int GetColCount() => field.GetLength(0);

        /// <summary>
        /// Return available moves for current player
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public Move[] GetMoves(int player)
        {
            var availableMoves = new List<Move>();
            //TODO: Parallel
            for (int i=0; i<field.GetLength(0); i++)
            {
                for (int j=0; j<field.GetLength(0); j++)
                {
                    if (field[i,j] == 0)
                    {
                        availableMoves.Add(new Move(i, j));
                    }
                }
            }
            return availableMoves.ToArray();
        }


    }
}
