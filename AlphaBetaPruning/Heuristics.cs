using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaBetaPruning
{
    class Heuristics: IHeuristics<GameField>
    {
        private readonly int player;

        public Heuristics(int player)
        {
            this.player = player;
        }

        /// <summary>
        /// Check combinations based on field heuristics
        /// </summary>
        /// <param name="field"></param>
        /// <param name="m"></param>
        /// <param name="player"></param>
        /// <returns></returns>
        public int Score(GameField field, Move m, int player)
        {
            int res = 0;
            foreach (Line line in Line.Values())
            {
                Score score = FieldScanner.ScoreLine(line, field.field, field.winLength, m,
                        player);
                if (score.investigated < field.winLength)
                {
                    continue;
                }
                if (score.inrow >= field.winLength)
                {
                    res = int.MaxValue;
                    return (player == this.player) ? res : -res;
                }
                res += G(score.inrow) + score.count;
            }
            /* Оценка длины ряда для противника */
            foreach (Line line in Line.Values())
            {
                Score score = FieldScanner.ScoreLine(line, field.field, field.winLength, m,
                        -player);
                if (score.investigated < field.winLength)
                {
                    continue;
                }
                res += Q(score.inrow) + score.count;
            }
            if (player == this.player) return res;
            else return -res;
        }

        private int G(int k)
        {
            return F(k + 2);
        }

        private int Q(int k)
        {
            return F(k + 2);
        }

        private int F(int k)
        {
            if (k < 0)
            {
                //TODO: Exception?
            }
            if (k == 1)
                return k;
            return k * F(k - 1);
        }

        
    }
}
