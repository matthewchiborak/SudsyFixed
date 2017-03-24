using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public enum TileType { Clean, Dirty, Block, Start, End }

class TileFactoryMethods
{

        

    public static Tile TileFactory(TileType t)
    {

        switch (t)
        {

            case TileType.Clean:
                return new Tile(t, new MoveBehaviorBlock());

            case TileType.Dirty:
                return new Tile(t, new MoveBehaviorDefult()); 

            case TileType.Block:
                return new MoveableTile(t, new MoveBehaviorBlock());

            case TileType.Start:
                return new Tile(t, new MoveBehaviorBlock());

            case TileType.End:
                return new Tile(t, new MoveBehaviorDefult());

            default:
                return new Tile(t, new MoveBehaviorDefult()); 

        }
           

    }

}

