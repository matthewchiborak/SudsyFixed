using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets;
using Assets.Scripts;
using UnityEngine.UI;
using System.IO;
using System.Text;
using System;


public class GameBoard : MonoBehaviour {

    //Shared event queues
    SafeQueue<GameBoardEvent> gbEventQueue;


    public int width = 6;
    public int height = 6;

    Stack<MoveHistory> history;

    public ActorPlayer player = new ActorPlayer();


    public List<List<Tile>> board = new List<List<Tile>>();
    private List<List<GameObject>> boardObj = new List<List<GameObject>>();
    private List<List<GameObject>> boardTiles = new List<List<GameObject>>();

    public Image loadingScreen;

    public GameBoard()
    {

        
    }


    // Use this for initialization
    void Start () {



        //Initialize the board
        for (int i = 0; i < height; i++)
        {
            List<Tile> row = new List<Tile>(width);
            List<GameObject> tileRow = new List<GameObject>(width);            

            for(int j = 0; j < width; j++)
            {
                Tile tile = TileFactoryMethods.TileFactory(TileType.Dirty);
                tile.setPos(i, j);
                row.Add(tile);

                //tileRow.Add(Instantiate(GameGraphics.BackGroundTile, new Vector3(i, j, .01f), Quaternion.identity));
            }

            board.Add(row);
            boardTiles.Add(tileRow);
        }

        //Add the player for testing purposes.
        Tile t = TileFactoryMethods.TileFactory(TileType.Block);
        t.setPos(2, 2);
        board[2][2] = t;

        player.currentTile = board[0][0];

        //Initialize the History stack
        history = new Stack<MoveHistory>();

        //Draw screen


        for (int col = 0; col < width; col++)
        {
            List<GameObject> objrow = new List<GameObject>();

            for (int row = 0; row < height; row++)
            {
                //GameObject obj = Instantiate(GameGraphics.MudTile, new Vector3(row, col, 0), Quaternion.identity);
                //objrow.Add(obj);

            }
            boardObj.Add(objrow);

        }
    }


    public MoveHistory getHistory()
    {
        if (history.Count == 0) return null;
        return history.Pop();
    }

	// Update is called once per frame
	void Update ()
    {
        if ( (gbEventQueue != null) && (!gbEventQueue.isEmpty()) )
        {
            GameBoardEvent cur = gbEventQueue.Dequeue();
            this.doGameEvent(cur);
        }

        for (int row = 0; row < board.Count; row++)
        {
            for (int col = 0; col < board[row].Count; col++)
            {

                bool muddy = board[row][col].type == TileType.Dirty;

                boardObj[row][col].GetComponent<Renderer>().enabled = muddy;

            }
        }

        //TESTING THE LEVEL LOADING
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            loadLevel(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            loadLevel(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            loadLevel(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            loadLevel(4);
        }
    }

    public void doGameEvent(GameBoardEvent ev)
    {
        bool res = ev.doEvent();
        print(res);
        printPlayerPos();
    }

    private void printPlayerPos()
    {
        print("Player Position: (" + player.row + ", " + player.col + ")");
    }

    //Build the indicated level
    public void loadLevel(int level)
    {
        //Hide the play area
        loadingScreen.enabled = true;

        //Destroy any previous existing game objects
        for (int i = 0; i < boardObj.Count; i++)
        {
            for (int j = 0; j < boardObj[i].Count; j++)
            {
                Destroy(boardObj[i][j]);
                Destroy(boardTiles[i][j]);
            }
        }

        //Reset the references to the objects
        board = new List<List<Tile>>();
        boardObj = new List<List<GameObject>>();
        boardTiles = new List<List<GameObject>>();

        //Read the level file to find out what the level needs
        string assetText;

        using (var streamReader = new StreamReader("Assets/Levels/" + level.ToString() + ".txt", Encoding.UTF8))
        {
            assetText = streamReader.ReadToEnd();
        }

        var lines = assetText.Split('\n');
        
        width = lines[0].Split('~').Length;
        height = lines.Length;

        //Go through each row
        for (int i = 0; i < height; i++)
        {
            var singleTiles = lines[i].Split('~');
            List<Tile> row = new List<Tile>(width);
            List<GameObject> tileRow = new List<GameObject>(width);

            //Go through each individual tile and create it
            for (int j = 0; j < width; j++)
            {
                Tile tile = TileFactoryMethods.TileFactory((TileType)Int32.Parse(singleTiles[j]));

                tile.setPos(i, j);
                row.Add(tile);

                //Create the object and store the returned reference
                //tileRow.Add(Instantiate(GameGraphics.BackGroundTile, new Vector3(i, j, .01f), Quaternion.identity));
            }

            board.Add(row);
            boardTiles.Add(tileRow);
        }

        //Reset the player
        player = new ActorPlayer();
        player.currentTile = board[0][0];

        //Reset the history stack
        history = new Stack<MoveHistory>();

        //Draw Screen
        for (int col = width - 1; col >= 0; col--)
        {
            List<GameObject> objRow = new List<GameObject>();

            for (int row = 0; row < height; row++)
            {
                //GameObject obj = Instantiate(GameGraphics.MudTile, new Vector3(row, col, 0), Quaternion.identity);
                //objRow.Add(obj);
            }

            boardObj.Add(objRow);
        }

        //Reset the camera
        //This works for even numbered grids
        Vector3 newCamPos = new Vector3(width / 2 - 0.5f, height / 2, -7.5f);

        if (width%2 == 1)
        {
            newCamPos.x += 0.5f;
        }
        if (height % 2 == 1)
        {
            newCamPos.y += 0.5f;
        }

        Camera.main.transform.position = (newCamPos);

        //Loading has finished so release the play area
        loadingScreen.enabled = false;
    }

    public void addGameBoardEventQueue(SafeQueue<GameBoardEvent> q)
    {
        gbEventQueue = q;
    }
}
