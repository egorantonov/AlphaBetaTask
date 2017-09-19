using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlphaBetaPruning;

namespace GameField
{
    public class Move
    {
        protected int row;
        protected int column;

        public int Row { get { return row; } }
        public int Col { get { return column; } }


        public Move()
        {

        }

        public Move(int row, int column)
        {
            this.row = row;
            this.column = column;
        }

        public int GetRow() => row;
        public int GetColumn() => column;

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (!(obj is Move)) return false;
            Move move = obj as Move;
            return this.row == move.row && this.column == move.column;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString() => string.Format("row {0} col {1}", row, column);
    }

    public class Field : IField
    {
        public enum FieldState
        {
            // Игра идет
            NONE,
            // Ничья
            DRAW,
            // Выиграли крестики
            WIN_X,
            // Выиграли нолики
            WIN_O
        }

        public int winLength { get; }

        private FieldState state;
        public FieldState State { get { return state; } }

        private int[,] field;
        private int size;

        
        private LinkedList<Move> moves;                             // Collection of available moves
        private Dictionary<Move, LinkedList<Move>> prevMoves;

        public Move[] getMoves(int player)
        {
            return moves.ToArray();
        }

        public void DoMove(Move move, int player)
        {
            //if (field[move.Row, move.Col] != 0)
            //{
            //    throw new IllegalMoveException(move.Row, move.Col, player);
            //}
            //if (State != FieldState.NONE)
            //{
            //    throw new IllegalMoveException("Игра уже закончена.", move.Row, move.Col, player);
            //}

            field[move.Row, move.Col] = player;

            LinkedList<Move> mvs = new LinkedList<Move>();
            ICollection<Move> ngbrs = getNeighboringMoves(move);

            foreach (Move m in ngbrs)
            {
                if (!moves.Contains(m))
                {
                    moves.AddFirst(m);
                    mvs.AddFirst(m);
                }
            }
            prevMoves.Add(move, mvs);
            moves.Remove(move);
            //reviewState(move, player);
        }


        public void UndoMove(Move m, int player)
        {
            field[m.Row, m.Col] = 0;
            //winLine = null;
            state = FieldState.NONE;

            LinkedList<Move> mvs;
            prevMoves.TryGetValue(m, out mvs);
            prevMoves.Remove(m);
            foreach (Move move in mvs)
            {
                moves.Remove(move);
            }
            moves.AddFirst(m);
        }

        public LinkedList<Move> getNeighboringMoves(Move m)
        {
            LinkedList<Move> mvs = new LinkedList<Move>();
            int row = (m.Row == 0) ? 0 : m.Row - 1;
            for (; row < getRowCount() && row < (m.Row + 2); row++)
            {
                int col = (m.Col == 0) ? 0 : m.Col - 1;
                for (; col < getColCount() && col < (m.Col + 2); col++)
                {
                    if (field[row, col] == 0)
                    {
                        mvs.AddFirst(new Move(row, col));
                    }
                }
            }
            return mvs;
        }

        public int getRowCount() { return size; }
        public int getColCount() { return size; }

        public int Get(int row, int col)
        {
            return field[row, col];
        }


        public bool IsGameOver()
        {
            return (state != FieldState.NONE);
        }

        public Field(int size, int winLength)
        {
            //if (size < winLength)
            //{
            //    throw new ArgumentException("Некорректная длина выигрышной длины.");
            //}
            this.winLength = winLength;
            this.size = size;
            field = new int[size, size];
            moves = new LinkedList<Move>();
            prevMoves = new Dictionary<Move, LinkedList<Move>>();
        }




        private void reviewState(Move m, int player)
        {
            foreach(Line line in Line.Values())
            {
                Score score = FieldScanner.scoreLine(line, field, winLength, m, player);
                if (score.inrow >= winLength)
                {
                    FieldState curState = (player == PLAYER_X) ? FieldState.WIN_X : FieldState.WIN_O;
                    state = curState;
                    return;
                }
            }
            if (FieldScanner.isFullField(field))
            {
                state = FieldState.DRAW;
                return;
            }
        }

    }
}
