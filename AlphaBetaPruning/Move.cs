using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaBetaPruning
{
    
    public class Move
    {
        #region fields
        protected int row;
        protected int column;        
        #endregion

        public Move()
        {

        }

        public Move (int row, int column)
        {
            this.row = row;
            this.column = column;
        }

        #region methods
        public int GetRow() => row;
        public int GetColumn() => column;
        #endregion

        #region overriding
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
        #endregion

    }
}
