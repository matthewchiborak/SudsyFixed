using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts;
using System.IO;
using SimpleJSON;

public class MoveHistory
{
    public ActorPlayer player;
    public Tile tile;

    public MoveHistory(ActorPlayer p, Tile t)
    {
        this.player = p;
        this.tile = t;
    }
}

public class GameBoardModel : SudsyModel
{

    public SafeQueue<GameBoardEvent> events = new SafeQueue<GameBoardEvent>();
    public ActorPlayer player = new ActorPlayer();

    public Stack<MoveHistory> history;
    public List<List<Tile>> board;
    public List<Tile> blocks;

    public Tile end_tile;
    public Tile start_tile;

    public int height, width;
    
    private void Start()
    {
        
    }

    private void Update()
    {
        if( (events != null) && (!events.isEmpty()))
        {
            GameBoardEvent e = events.Dequeue();
            e.doEvent();
        }
    }

    //How to pass level file?
    public string loadLevel(string fname)
    {
        //Hide the play area
        //loadingScreen.enabled = true;

        string result = "Error loading file.";

        history = new Stack<MoveHistory>();
        board = new List<List<Tile>>();

        using (var streamReader = new StreamReader("Assets/Levels/" + fname, Encoding.UTF8))
        {

            JSONNode json = JSON.Parse(streamReader.ReadToEnd());

            
            if (json["height"].IsNull)  return "Error loading height;";            
            height = json["height"].AsInt;
            

            if (json["width"].IsNull) return "Error loading width;";
            width = json["width"].AsInt;
            

            //Load the default gameboard
            for(int r = 0; r < height; r++)
            {

                List<Tile> tile_row = new List<Tile>();
                for (int c = 0; c < width; c++)
                {
                    Tile t = TileFactoryMethods.TileFactory(TileType.Dirty);
                    t.setPos(r, c);
                    tile_row.Add(t);
                    
                }

                board.Add(tile_row);
            }

            //Validate data
            if (json["start"].IsNull || json["start"]["row"].IsNull || json["start"]["col"].IsNull)
                return "Error loading level stating tile.";

            if (json["end"].IsNull || json["end"]["row"].IsNull || json["end"]["col"].IsNull)
                return "Error loading level end tile.";

            if ((json["start"]["row"] == json["end"]["row"]) && (json["start"]["col"] == json["end"]["col"]))
                return "Error: Start and end cannot be the same tile";

            int row, col;

            //Add the start tile
            row = json["start"]["row"].AsInt;
            col = json["start"]["col"].AsInt;

            if ((row < 0) || (row >= height) || (col < 0) || (col >= width))
                return "Start tile is not in board dimensions.";

            Tile tile = TileFactoryMethods.TileFactory(TileType.Start);
            tile.setPos(row, col);
            board[row][col] = tile;
            start_tile = tile;

            //Add the end tile
            row = json["end"]["row"].AsInt;
            col = json["end"]["col"].AsInt;

            if ((row < 0) || (row >= height) || (col < 0) || (col >= width))
                return "end tile is not in board dimensions.";

            tile = TileFactoryMethods.TileFactory(TileType.End);
            tile.setPos(row, col);
            board[row][col] = tile;
            end_tile = tile;

            //Load the special tiles
            blocks = new List<Tile>();
            JSONArray tiles = json["tiles"].AsArray;
            foreach(JSONObject j in tiles)
            {
                row = j["row"].AsInt;
                col = j["col"].AsInt;

                print(row + "," + col);

                tile = TileFactoryMethods.TileFactory(TileType.Block);
                tile.setPos(row, col);
                board[row][col] = tile;
                blocks.Add(tile);
            }

            result = "File Loaded";
        }

        return result;
    }
}

