using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaBetaPruning
{
    public class Score
    {
        public int investigated;
        public int count;
        public int inrow;

        public Score()
        {
        }

        public Score(int investigated, int count, int inrow)
        {
            this.investigated = investigated;
            this.count = count;
            this.inrow = inrow;
        }
    }

    public struct Line
    {
        //ROW (0, -1),
        //COLUMN(-1, 0),
        //DIAGONAL_1(-1, -1),
        //DIAGONAL_2(-1, 1);

        Line(int rowInc, int colInc)
        {
            RowInc = rowInc;
            ColInc = colInc;
        }

        public int RowInc { get; }
        public int ColInc { get; }

        public static Line[] Values()
        {
            Line[] values = new Line[4];
            values[0] = new Line(0, -1);
            values[1] = new Line(-1, 0);
            values[2] = new Line(-1, -1);
            values[3] = new Line(-1, 1);
            return values;
        }
    }


    public class FieldScanner
    {

        public static bool IsFullField(int[,] field)
        {
            foreach (int i in field)
            {
                if (i == 0) return false;
            }
            return true;
        }

        //searching a win combination in the row

        public static List<Cell> ScanRow(int[,] field, int winLength, int index)
        {
            return ScanLine(field, index, 0, 0, 1, winLength);
        }

        //searching a win combination in the diagonal (upper)
        public static List<Cell> ScanUpDiagonal(bool direct, int[,] field, int winLength, int index)
        {
            if (direct)
            {
                return ScanLine(field, 0, index, 1, 1, winLength);
            }
            else
            {
                return ScanLine(field, 0, index, 1, -1, winLength);
            }
        }

        //searching a win combination in the diagonal (lower)
        public static List<Cell> ScanDownDiagonal(bool direct, int[,] field, int winLength, int index)
        {
            if (direct)
            {
                return ScanLine(field, field.GetLength(0) - 1, index, -1, 1, winLength);
            }
            else
            {
                return ScanLine(field, field.GetLength(0) - 1, index, -1, -1, winLength);
            }
        }

        //searching a win combination in the column
        public static List<Cell> ScanCol(int[,] field, int winLength, int index)
        {
            return ScanLine(field, 0, index, 1, 0, winLength);
        }

        // Считает количество занятых клеток в линии, в которой находится клетка хода, в
        // дипазоне x2 выйгрышной длины. Клетка хода учитывается. Подсчет ведется до ближайшей
        // клетки противника или границы поля.

        public static Score ScoreLine(Line line, int[,] field, int winLength, Move move, int player)
        {
            Score res1 = ScoreHalfLine(field, winLength, move, player, line.RowInc, line.ColInc);
            Score res2 = ScoreHalfLine(field, winLength, move, player, -line.RowInc, -line.ColInc);

            /* Количество своих клеток */
            res1.count = res1.count + res2.count - 1;
            /* Полное количество исследованных клеток */
            res1.investigated = res1.investigated + res2.investigated - 1;
            /* Количество своих клеток идущих подряд */
            res1.inrow = res1.inrow + res2.inrow - 1;

            return res1;
        }


        // Считает количество занятых клеток в линии, в которой находится клетка хода, в
        // дипазоне выйгрышной длины. Клетка хода учитывается в каждом допустимом направлении.

        private static Score ScoreHalfLine(int[,] field, int winLength, Move move, int player, int rowInc, int colInc)
        {
            int r = move.GetRow() + rowInc;
            int c = move.GetColumn() + colInc;
            Score res = new Score
            {
                /* Берем в расчет начальную клетку */
                count = 1,
                inrow = 1,
                investigated = 1
            };
            /* Ключ указывающий на то, что "свои" клетки идут подряд */
            bool inRow = true;
            /*
             * Пока индексы в пределах поля, не встречена чужая клетка и длинна иследуемого
             * ряда не превышает winLength...
             */
            while ((res.investigated < winLength) && (r >= 0) && (r < field.GetLength(0))
                    && (c >= 0) && (c < field.GetLength(0))
                    && (field[r, c] == player || field[r, c] == 0))
            {
                if (field[r, c] == player)
                {
                    res.count++;
                    if (inRow)
                        res.inrow++;
                }
                else
                {
                    inRow = false;
                }
                res.investigated++;
                r += rowInc;
                c += colInc;
            }
            return res;
        }


        // Ищет в линии выигрышную последовательность заполненных клеток.

        private static List<Cell> ScanLine(int[,] field, int r, int c, int rInc, int cInc, int winLength)
        {
            int point = field[r, c];
            var list = new List<Cell>();

            while ((r >= 0) && (r < field.GetLength(0)) && (c >= 0) && (c < field.GetLength(1)))
            {
                /*
                 * Если встретилась пустая клетка, сбор выигрышных клеток начинается сначала
                 */
                if (field[r, c] == 0)
                {
                    list = new List<Cell>();
                }
                /*
                 * Если текущая клетка занята тем же игроком, что и предыдущая, добавляем ее в
                 * список выигрышных
                 */
                else if (field[r, c] == point)
                {
                    list.Add(new Cell(r, c));
                    // XXX из-за этого кода выигрышная последовательность может быть не полной
                    if (list.Count >= winLength)
                    {
                        return list;
                    }
                }
                /*
                 * Иначе запоминаем метку игрока с текущей клетки и начинаем формировать
                 * выигрышный список снова.
                 */
                else
                {
                    point = field[r, c];
                    list = new List<Cell>();
                    list.Add(new Cell(r, c));
                }
                r += rInc;
                c += cInc;
            }
            return null;
        }
    }
}
