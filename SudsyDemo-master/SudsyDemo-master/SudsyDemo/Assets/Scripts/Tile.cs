using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets
{


    public class Tile
    {

        //insert move behaviour
        public int row, col;
        public TileType type ;
        private MoveBehavior moveBehaviour;

        public Tile(TileType type, MoveBehavior mvb)
        {
            this.type = type;
            moveBehaviour = mvb;

        }

        public Boolean doMoveEvent(Actor act, GameBoard gb)
        {

            return moveBehaviour.move(act, this, gb);

        }

        public Boolean setPos(int row, int col)
        {

            this.row = row;
            this.col = col;

            return true;

        }

    }


    public class MoveableTile : Tile
    {
        int moves = 0;

        public MoveableTile(TileType type, MoveBehavior mvb) : base(type, mvb)
        {

        }

        public Boolean isMoveable()
        {
            return moves > 0;
        }

        public Boolean setMoves(int moves)
        {
            this.moves = moves;

            return true;
        }

    }

}
