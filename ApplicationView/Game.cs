using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlphaBetaPruning;

namespace ApplicationView
{
    class Game
    {

        private IRules rules;
        private GameField field;
        private AlphaBetaPruning<GameField> algorythm;
        //TODO: view?
        private int size;
        private int winLength;
        private int maxDepth;

        private  const int PLAYER = BaseField.PLAYER_X;
        private const int AI = BaseField.PLAYER_O; 

        public Game(/*view*/int size, int winLength, int depth)
        {
            //this.view = view;
            this.size = size;
            this.maxDepth = depth;
            this.winLength = winLength;
        }

        public void NewGame()
        {
            field = new GameField(size, winLength);
            rules = field;
            var heuristics = new Heuristics(PLAYER);
            algorythm = new AlphaBetaPruning<GameField>(rules, PLAYER, field, heuristics, maxDepth);
        }

        public void MovePlayer(Move m)
        {
            if (field.IsGameOver()) return;
            field.DoMove(m, PLAYER);            
        }

        public void MoveAI()
        {
            if (field.IsGameOver()) return;
            var m = algorythm.GetBestMove();
            field.DoMove(m, AI);
            
        }
    }
}
