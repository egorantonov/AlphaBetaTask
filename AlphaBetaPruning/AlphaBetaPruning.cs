using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaBetaPruning
{
    public class AlphaBetaPruning<T> where T:IField
    {
        #region fields
        private int player;
        private IRules rules;
        private T field;

        private int maxDepth;
        private IHeuristics<T> heuristics;
        #endregion

        public AlphaBetaPruning(IRules rules, int player, T field,
            IHeuristics<T> heuristics, int maxDepth)
        {
            this.rules = rules; //TODO: null
            this.player = player;
            this.field = field;
            this.heuristics = heuristics;
            this.maxDepth = maxDepth;
        }

        #region methods
        public Move GetBestMove()
        {
            Move[] moves = rules.GetMoves(player);
            var maxScore = int.MinValue;
            var bestMove = new Move();
            //TODO: parallel
            foreach (Move m in moves)
            {
                field.DoMove(m, player);
                var score = Score(field, m, -player);
                field.UndoMove(m, player);
                if (score > maxScore)
                {
                    bestMove = m;
                    maxScore = score;
                }
            }
            return bestMove;
        }

        public int Pruning(T field, Move move, int player, int alpha,
            int beta, int depth)
        {
            if (field.IsGameOver() || depth == maxDepth)
            {                
                //TODO: perhaps we should call Sonya's heuristics here instead below
                return heuristics.Score(field, move, player);
            }
            var score = int.MinValue;
            Move[] moves = rules.GetMoves(player);
            //TODO: parallel
            foreach (Move m in moves)
            {
                field.DoMove(m, player);
                var result = -Pruning(field, move, -player, -beta, -score, depth + 1);
                field.UndoMove(m, player);
                if (result > score)
                {
                    score = result;
                }
                if (score >= beta) //> or >= ???
                {
                    return score;
                }               
            }
            return score;
        }

        protected int Score(T field, Move move, int player)
        {
            return -Pruning(field, move, player, int.MinValue, int.MaxValue, 0);

        }
        #endregion


    }
}
