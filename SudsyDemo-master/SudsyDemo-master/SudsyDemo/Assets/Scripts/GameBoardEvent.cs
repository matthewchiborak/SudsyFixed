using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets
{
    public class GameBoardEvent
    {

        public GameBoardEvent() { }

        public virtual Boolean doEvent(GameBoard gb)
        {
            return false;
        }

    }

    public class PlayerMoveEvent : GameBoardEvent
    {

        public PlayerMoveEvent() { }

    }

    //Undo move event
    public class GameBoardUndoEvent : GameBoardEvent
    {

        public override Boolean doEvent(GameBoard gb)
        {

            MoveHistory h = gb.getHistory();
            if (h == null) return true;

            gb.board[h.row][h.col] = h.tile;

            gb.player.currentTile = gb.board[h.row][h.col];
            gb.player.row = h.row;
            gb.player.col = h.col;

            return true;

        }

    }

    //move player up event
    public class PlayerMoveEventUp : PlayerMoveEvent
    {

        public override Boolean doEvent(GameBoard gb)
        {

            ActorPlayer p = gb.player;

            if (p.row <= 0) return false;

            Tile tile = gb.board[p.row - 1][p.col];

            return tile.doMoveEvent(p, gb);

        }

    }

    public class PlayerMoveEventLeft : PlayerMoveEvent
    {

        public override Boolean doEvent(GameBoard gb)
        {

            ActorPlayer p = gb.player;

            if (p.col <= 0) return false;

            Tile tile = gb.board[p.row][p.col - 1];

            return tile.doMoveEvent(p, gb);

        }

    }

    public class PlayerMoveEventDown : PlayerMoveEvent
    {

        public override Boolean doEvent(GameBoard gb)
        {

            ActorPlayer p = gb.player;

            if (p.row >= (gb.height - 1) ) return false;

            Tile tile = gb.board[p.row + 1][p.col];

            return tile.doMoveEvent(p, gb);

        }

    }

    public class PlayerMoveEventRight : PlayerMoveEvent
    {

        public override Boolean doEvent(GameBoard gb)
        {

            ActorPlayer p = gb.player;

            if (p.col >= (gb.width - 1) ) return false;

            Tile tile = gb.board[p.row][p.col + 1];

            return tile.doMoveEvent(p, gb);

        }

    }
}
