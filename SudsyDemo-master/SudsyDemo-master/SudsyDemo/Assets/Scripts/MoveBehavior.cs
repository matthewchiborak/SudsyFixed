using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets
{

    //Maybe return a gamebehviour for updating/chaining?
    
    public class MoveBehavior
    {

        public virtual Boolean move(Actor actor, Tile block, GameBoard gb)
        {

            return false;

        }

    }

    public class MoveBehaviorDeafult : MoveBehavior
    {

        public override Boolean move(Actor actor, Tile block, GameBoard gb)
        {

            //Store the previous state info
            gb.addHistory(actor.row, actor.col, actor.currentTile);

            //Set the previous square to clean.
            gb.board[actor.row][actor.col] = TileFactoryMethods.TileFactory(TileType.Clean);

            //move the player
            actor.row = block.row;
            actor.col = block.col;
                  
            //update the players location
            actor.currentTile = block;

            return true;

        }

    }

    public class MoveBehaviorBlock : MoveBehavior
    {

        public override Boolean move(Actor actor, Tile block, GameBoard gb)
        {

            return false;

        }

    }
}
