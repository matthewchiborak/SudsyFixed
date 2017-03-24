using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class Actor
{

    public int row, col;
    public List<String> type;
    public Tile currentTile;

    //Deafult Constructor
    public Actor()
    {
        row = col = 0;
        type = new List<String>();
    }

    //Make a deep copy
    public Actor clone()
    {
        Actor result = new Actor();

        result.row = this.row;
        result.col = this.col;
        result.type = this.type;
        result.currentTile = this.currentTile;

        return result;
    }
}

public class ActorPlayer : Actor
{

    public ActorPlayer()
    {

        type.Add("player");

    }

    public new ActorPlayer clone()
    {
        ActorPlayer result = new ActorPlayer();

        result.row = this.row;
        result.col = this.col;
        result.type = this.type;
        result.currentTile = this.currentTile;

        return result;
    }
}

public class ActorEnemy : Actor
{

    public ActorEnemy() { }
}

