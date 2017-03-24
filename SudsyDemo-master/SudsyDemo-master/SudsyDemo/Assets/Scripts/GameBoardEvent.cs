using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class GameBoardEvent : SudsyElement
{
    protected GameBoardModel gb;

    public GameBoardEvent()
    {
        
    }

    private void Start()
    {
        gb = app.game_board_model;
    }

    public virtual Boolean doEvent()
    {
        return false;
    }

}

//Undo move event
public class GameBoardUndoEvent : GameBoardEvent
{

    public GameBoardUndoEvent() : base() { }

    public override Boolean doEvent()
    {

        if (gb.history.Count == 0) return false;

        MoveHistory h = gb.history.Pop();

        Tile t = h.tile;

        gb.board[t.row][t.col] = t;

        gb.player = h.player;

        return false;

    }

}


public class ActorMoveEvent : GameBoardEvent
{
    protected Actor act;

    public ActorMoveEvent() : base()
    {         }

    public void addActor(Actor act)
    {
        this.act = act;
    }

    public override Boolean doEvent()
    {
        return false;
    }

}


public class ActorMoveEventUp : ActorMoveEvent
{

    public ActorMoveEventUp() : base() { }

    public override Boolean doEvent()
    {

        if (act.row >= (gb.height - 1)) return false;

        Tile tile = gb.board[act.row + 1][act.col];

        return tile.doMoveEvent(act, gb);

    }

}

public class ActorMoveEventDown : ActorMoveEvent
{

    public ActorMoveEventDown() : base() { }

    public override Boolean doEvent()
    {

        if (act.row <= 0) return false;

        Tile tile = gb.board[act.row - 1][act.col];

        return tile.doMoveEvent(act, gb);

    }

}

public class ActorMoveEventLeft : ActorMoveEvent
{
    public ActorMoveEventLeft() : base() { }

    public override Boolean doEvent()
    {

        if (act.col <= 0) return false;

        Tile tile = gb.board[act.row][act.col - 1];

        return tile.doMoveEvent(act, gb);

    }

}

public class ActorMoveEventRight : ActorMoveEvent
{

    public ActorMoveEventRight() : base() { }

    public override Boolean doEvent()
    {

        if (act.col >= (gb.width - 1)) return false;

        Tile tile = gb.board[act.row][act.col + 1];

        return tile.doMoveEvent(act, gb);

    }

}

