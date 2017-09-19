using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaBetaPruning
{
    class GameField: BaseField
    {
        private List<Move> moves;
        private Dictionary<Move, IEnumerable<Move>> prevMoves;

        public GameField(int size, int winLength)
            :base(size, winLength)
        {
            moves = new List<Move>();
            prevMoves = new Dictionary<Move, IEnumerable<Move>>();
        }
        public GameField(int[,] field, int winLength)
           : base(field.Length, winLength)
        {
            moves = new List<Move>();
            prevMoves = new Dictionary<Move, IEnumerable<Move>>();
            //TODO: Parallel
            for (int i = 0; i < field.GetLength(0); i++)
            {
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    if (field[i, j] == 0)
                    {
                        continue;
                    }
                    DoMove(new Move(i, j), field[i, j]);
                }
            }
        }

        public override void DoMove(Move move, int player)
        {
            //base.DoMove(m, player);
            field[move.Row, move.Col] = player;
            var mvs = new List<Move>();
            var ngbrs = GetNeighboringMoves(move);
            //TODO: Parallel
            foreach (Move m in ngbrs)
            {
                if (!moves.Contains(m))
                {
                    moves.Add(m);
                    mvs.Add(m);
                }
            }
            prevMoves.Add(move, mvs);
            moves.Remove(move);
            CheckState(move, player);
        }

        public override void UndoMove(Move move, int player)
        {
            base.UndoMove(move, player);
            prevMoves.TryGetValue(move, out IEnumerable<Move> mvs);
            prevMoves.Remove(move);
            //TODO: there may be wrong ↓
            foreach (Move m in mvs)
            {
                moves.Remove(m);
            }
            moves.Add(move);
        }

        public IEnumerable<Move> GetNeighboringMoves(Move m)
        {
            var mvs = new List<Move>();
            int row;
            if (m.Row > 0) row = m.Row - 1;
            else row = 0;
            for (; row < GetRowCount() && row < (m.Row + 2); row++)
            {
                int col = (m.Col == 0) ? 0 : m.Col - 1;
                for (; col < GetColCount() && col < (m.Col + 2); col++)
                {
                    if (field[row, col] == 0)
                    {
                        mvs.Add(new Move(row, col));
                    }
                }
            }
            return mvs;
        }

        private void CheckState(Move m, int player)
        {
            foreach (Line line in Line.Values())
            {
                Score score = FieldScanner.ScoreLine(line, field, winLength, m, player);
                if (score.inrow >= winLength)
                {
                    FieldState curState = (player == PLAYER_X) ? FieldState.win_x : FieldState.win_o;
                    state = curState;
                    return;
                }
            }
            if (FieldScanner.IsFullField(field))
            {
                state = FieldState.draw;
                return;
            }
        }

        public override Move[] GetMoves(int player)
        {
            if (moves == null || moves.Count() == 0)
            {
                return base.GetMoves(player);
            }
            return moves.ToArray();
        }

    }

}
