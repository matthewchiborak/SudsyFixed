using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

//Maybe return a gamebehviour for updating/chaining?

public class MoveBehavior
{

    public virtual Boolean move(Actor actor, Tile block, GameBoardModel gb)
    {

        return false;

    }

}

public class MoveBehaviorDefult : MoveBehavior
{

    public override Boolean move(Actor actor, Tile block, GameBoardModel gb)
    {

        //Store the previous state info
        gb.history.Push(new MoveHistory(gb.player.clone(), block));        

        //move the player
        actor.row = block.row;
        actor.col = block.col;

        
        //Set the current tile to clean.
        Tile t = TileFactoryMethods.TileFactory(TileType.Clean);
        t.setPos(actor.row, actor.col);
        gb.board[actor.row][actor.col] = t;


        //update the players location
        actor.currentTile = block;

        return true;

    }

}

public class MoveBehaviorBlock : MoveBehavior
{

    public override Boolean move(Actor actor, Tile block, GameBoardModel gb)
    {

        return false;

    }

}

