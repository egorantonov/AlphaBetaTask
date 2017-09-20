using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CriticalSituations
{
    //Должен реализовывать интерфейс поля
    public class Field //:Field 
    {
        public int get(int row, int col)
        {
            return 0;
        }

        public int getRowCount()
        {
            return 0;
        }

        public int getColCount()
        {
            return 0;
        }
    }

    public class SituationsContext
    {
        public static List<ArrayList> criticalSituations;

        public SituationsContext(int winLength)
        {
            criticalSituations = new List<ArrayList>();
            for (int i = 0; i < winLength; i++)
            {
                criticalSituations.Add(new ArrayList(winLength));
                for (int j = 0; j < winLength; j++)
                    criticalSituations[i].Add(new ArrayList(winLength));
            }

            for (int i = 0; i < winLength; i++)
            {
                for (int j = 0; j < winLength; j++)
                    for (int k = 0; k < winLength; k++)
                        (criticalSituations[i][j] as ArrayList).Add(0);
            }

            //Критическая ситуация №1 - длина линии соперника = winLength - 2
            for (int i = 1; i < winLength - 1; i++)
                (criticalSituations[0][winLength / 2] as ArrayList)[i] = 1;

            //Критическая ситуация №2 - "Уголок"
            for (int i = 0; i < winLength - 3; i++)
            {
                (criticalSituations[1][0] as ArrayList)[i] = 1;
                (criticalSituations[1][i + 1] as ArrayList)[winLength - 4] = 1;
            }

            Random rnd = new Random(winLength);
            //Критическая ситуация №3 - Диагональ с выколотой точкой
            for (int i = 0; i < winLength && i != rnd.Next(); i++)
                (criticalSituations[2][i] as ArrayList)[i] = 1;

            //Критическая ситуация №4 - Горизонталь с выколотой точкой
            for (int i = 0; i < winLength && i != rnd.Next(); i++)
                (criticalSituations[2][0] as ArrayList)[i] = 1;

            //Критическая ситуация №5 - Вертикаль с выколотой точкой
            for (int i = 0; i < winLength && i != rnd.Next(); i++)
                (criticalSituations[2][i] as ArrayList)[0] = 1;
        }
    }

    public class CriticalSituations
    {
        //структура для удобства просмотра поля
        public struct Move
        {
            public int x;
            public int y;

            public Move(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }

        // protected ArrayList m_field = new ArrayList();
        int rowCount = 5;
        int colCount = 5;
        protected int winLength;
        int player;
        //public static int PLAYER_X = 1;
        //public static int PLAYER_O = -1;

        /*/занято ли поле соперником?
        private bool IsEnemy(int col, int row)
        {
            return (m_field[col] as ArrayList)[row] != 0 && (m_field[col] as ArrayList)[row] != player;
        }

        //есть ли вокруг клетки метка соперника?
        private Move isMoved(int col, int row)
        {
            for (int i = -1; i < 2; i++)
                for (int j = -1; j < 2; j++)
                {
                    if (IsEnemy(col + i, row + j))
                    {
                        return new Move(i, j);
                    }
                }
            return new Move(0, 0);
        }

        private bool is_critical(int col, int row)
        {
            bool flag = false;
            int winCount = 0;
            Move news = isMoved(col, row);
            Move needToMove = new Move(0, 0);
            if (news.x == 0 && news.y == 0) return false;
            for (int i = 1; i < winLength; i++)
            {
                if (!IsEnemy(col + news.x, row + news.y))
                {
                    if (flag) return true; //needToMove;
                    flag = true;
                    needToMove = new Move(col + news.x * i, row + news.y * i);
                    is_critical(col + news.x, row + news.y);
                }
                winCount++;
            }
            if (winCount == winLength - 1)
                return true; //new Move(col - news.x * winCount, row - news.y * winCount);
            return false; //needToMove;
        }*/

        public bool IsCritical(Field field)
        {
            //bool critical_coord = false;
            for (int x = winLength / 2; x < field.getRowCount() - winLength / 2; x++)
                for (int y = winLength / 2; y < field.getColCount() - winLength / 2; y++)
                {
                    ArrayList m_field = new ArrayList(winLength);
                    for (int k = 0; k < winLength; k++)
                        m_field.Add(new ArrayList(winLength));
                    for (int i = -winLength / 2; i < winLength / 2; i++)
                        for (int j = -winLength / 2; j < winLength / 2; j++)
                            (m_field[i] as ArrayList).Add(field.get(x + i, y + j));
                    if (SituationsContext.criticalSituations.Contains(m_field)) return true;
                    else
                    {
                        int count = 0;
                        foreach (ArrayList mass in SituationsContext.criticalSituations)
                        {
                            for (int i = 0; i < winLength; i++)
                                for (int j = 0; j < winLength; j++)
                                    if ((m_field[i] as ArrayList)[j] == (object)1 && (m_field[i] as ArrayList)[j] == (mass[i] as ArrayList)[j])
                                    {
                                        count++;
                                    }
                            if (count >= 3)
                            {
                                SituationsContext.criticalSituations.Add(m_field);
                                return true;
                            }
                        }
                    }
                    /*
                     rowCount = field.getRowCount();
                     colCount = field.getColCount();
                     player = field.player;
                     winLength = field.winLength;


                    for (int i = 0; i < rowCount; i++)
                    {
                        m_field[i] = new int[5];
                        for (int j = 0; j < colCount; j++)
                        {
                            m_field[i][j] = field.get(i, j);
                        }
                    }

                    for (int i = 0; i < rowCount; i++)
                        for (int j = 0; j < colCount; j++)
                        {
                            if (IsEnemy(i, j))
                            {
                                critical_coord = is_critical(i, j);
                            }
                        }*/
                }
            return false;
        }
    }
}
