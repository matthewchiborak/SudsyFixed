using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.UI;
using UnityEngine;
using Assets.Scripts;

//The View object in MVC
public class GameView : MonoBehaviour
{

    //GameObject test;

    private List<List<GameObject>> mudTiles = new List<List<GameObject>>();
    private List<List<GameObject>> boardTiles = new List<List<GameObject>>();

    GameBoard gb;

    int height;
    int width;

    bool ready = false;

    private void Start()
    {
        if (!ready || (gb == null) ) return;
        
        print("why are you not drawn?");

        height = gb.height;
        width = gb.width;

        for (int col = 0; col < width; col++)
        {

            List<GameObject> bgTileRow = new List<GameObject>();
            List<GameObject> mudTileRow = new List<GameObject>();

            for (int row = 0; row < height; row++)
            {
                //Add a new BG tile
                //GameObject t = Resources.Load("TilePrefab") as GameObject;
                //GameObject obj = Instantiate(GameGraphics.BackGroundTile, new Vector3(row, col, -1), Quaternion.identity);
                //bgTileRow.Add(obj);

                //Add new mud tile
               // obj = Instantiate(GameGraphics.MudTile, new Vector3(row, col, -2), Quaternion.identity);
               // mudTileRow.Add(obj);
            }

            boardTiles.Add(bgTileRow);
            mudTiles.Add(mudTileRow);

        }

    }

    private void Update()
    {
        if (!ready) return;

        for (int row = 0; row < width; row++)
        {
            for (int col = 0; col < height; col++)
            {

                bool muddy = gb.board[row][col].type == TileType.Dirty;

                mudTiles[row][col].GetComponent<Renderer>().enabled = muddy;

            }
        }
    }

    //Attach gameview event queue

    //Attach GameBoard object
    public void attachGameBoard(GameBoard gb)
    {

        this.gb = gb;
        ready = true;
        print("attaching game board");

        this.Start();        

        
    }
}

