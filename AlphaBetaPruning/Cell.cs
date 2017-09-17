using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaBetaPruning
{
    public class Cell
    {
        #region fields;
        public int row;
        public int column;
        #endregion;

        public Cell(int row, int column)
        {
            this.row = row;
            this.column = column;

        }
    }
}
