using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets;
using UnityEngine.UI;
using System.IO;
using System.Text;
using System;

//Later probably save deep copy of player object for items etc
public class MoveHistory
{
    public int row, col;
    public Tile tile;

    public MoveHistory(int row, int col, Tile t)
    {
        this.row = row;
        this.col = col;
        tile = t;
    }
}

public class GameBoard : MonoBehaviour {

    public int width = 6;
    public int height = 6;

    Stack<MoveHistory> history;
    
    public GameObject mudOverlay;
    public GameObject bgTile;

    public ActorPlayer player = new ActorPlayer();


    public List<List<Tile>> board = new List<List<Tile>>();
    private List<List<GameObject>> boardObj = new List<List<GameObject>>();
    private List<List<GameObject>> boardTiles = new List<List<GameObject>>();

    public Image loadingScreen;

    //Deafult constructor, private as it is a singleton class
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

                tileRow.Add(Instantiate(bgTile, new Vector3(i, j, .01f), Quaternion.identity));
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

        
            for (int col = width - 1; col >= 0; col--)
            {
                List<GameObject> objRow = new List<GameObject>();

            for (int row = 0; row < height; row++)
            {
                GameObject obj = Instantiate(mudOverlay, new Vector3(row, col, 0), Quaternion.identity);
                objRow.Add(obj);

            }
            boardObj.Add(objRow);

        }
    }


    public void addHistory(int x, int y, Tile t)
    {
        MoveHistory h = new MoveHistory(x, y, t);
        history.Push(h);
    }

    public MoveHistory getHistory()
    {
        if (history.Count == 0) return null;
        return history.Pop();
    }

	// Update is called once per frame
	void Update ()
    {
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
        bool res = ev.doEvent(this);
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
                tileRow.Add(Instantiate(bgTile, new Vector3(i, j, .01f), Quaternion.identity));
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
                GameObject obj = Instantiate(mudOverlay, new Vector3(row, col, 0), Quaternion.identity);
                objRow.Add(obj);
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
}
