using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets
{

    public enum TileType { Clean, Dirty, Block }

    class TileFactoryMethods
    {

        

        public static Tile TileFactory(TileType t)
        {

            switch (t)
            {

                case TileType.Clean:
                    return new Tile(t, new MoveBehaviorBlock());

                case TileType.Dirty:
                   return new Tile(t, new MoveBehaviorDeafult()); 

                case TileType.Block:
                    return new MoveableTile(t, new MoveBehaviorBlock()); 

                default:
                    return new Tile(t, new MoveBehaviorDeafult()); 

            }
           

        }

    }
}
