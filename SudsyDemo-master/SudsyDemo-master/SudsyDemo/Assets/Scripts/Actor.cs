using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets
{
    

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


    }

    public class ActorPlayer : Actor
    {

        public ActorPlayer()
        {

            type.Add("player");

        }

    }

    public class ActorEnemy : Actor
    {

        public ActorEnemy() { }
    }
}
