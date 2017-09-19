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
        protected internal int Row;
        protected internal int Col;        
        #endregion

        public Move()
        {

        }

        public Move (int row, int column)
        {
            Row = row;
            Col = column;
        }

        #region methods
        public int GetRow() => Row;
        public int GetColumn() => Col;
        #endregion

        #region overriding
        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (!(obj is Move)) return false;
            Move move = obj as Move;
            return Row == move.Row && Col == move.Col;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString() => string.Format("row {0} col {1}", Row, Col);
        #endregion

    }

}
