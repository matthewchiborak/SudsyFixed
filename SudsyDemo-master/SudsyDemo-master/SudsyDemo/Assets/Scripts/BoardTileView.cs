using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardTileView : SudsyView {

    public GameObject bgtile;
    public GameObject mudtile;
    public GameObject player_prefab;
    public GameObject clean_tile;
    public GameObject end_tile;
    public GameObject block_tile;

    int speed = 15;

    private List<List<GameObject>> clean_array;
    private List<List<GameObject>> mud_array;
    private List<GameObject> block_array;

    private GameObject player;

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            
        }

        updateMud();
        updateClean();
        updatePlayerPos();
        checkWin();
    }    

    public void init()
    {
        
        GameBoardModel gb = app.game_board_model;

        mud_array = new List<List<GameObject>>();
        clean_array = new List<List<GameObject>>();

        for (int col = 0; col < gb.width; col++)
        {

            List<GameObject> mud_row = new List<GameObject>();
            List<GameObject> clean_row = new List<GameObject>();

            for (int row = 0; row < gb.height; row++)
            {

                Instantiate(bgtile, new Vector3(row, col, 0), Quaternion.identity);
                mud_row.Add(Instantiate(mudtile, new Vector3(row, col, -.01f), Quaternion.identity));
                clean_row.Add(Instantiate(clean_tile, new Vector3(row, col, -.01f), Quaternion.identity));
            }

            mud_array.Add(mud_row);
            clean_array.Add(clean_row);

        }

        //draw the starting tile
        Tile t = gb.end_tile;
        Instantiate(end_tile, new Vector3(t.col, t.row, 0), Quaternion.identity);

        //draw the player
        player = Instantiate(player_prefab, new Vector3(gb.start_tile.col, gb.start_tile.row, -.05f), Quaternion.identity);

        //draw the blocks
        block_array = new List<GameObject>();
        foreach(Tile block in gb.blocks)
        {
            block_array.Add(Instantiate(block_tile, new Vector3(block.col, block.row, -.05f), Quaternion.identity));
        }
    }
    
    private bool checkWin()
    {
        GameBoardModel gb = app.game_board_model;
        if(gb.player.currentTile == null) return false;
        if (gb.player.currentTile.type != TileType.End) return false;

        for(int row = 0; row < gb.height; row++)
        {
            for(int col = 0; col < gb.width; col++)
            {
                if (gb.board[row][col].type == TileType.Dirty) return false;
            }
        }


        print("you win");
        return true;
    }

    private void updatePlayerPos()
    {
        GameBoardModel gb = app.game_board_model;
        Vector3 dest = new Vector3(gb.player.col, gb.player.row, -.05f);

        player.transform.position = Vector3.Lerp(player.transform.position, dest, speed * Time.deltaTime);
    }

    private void updateClean()
    {
        GameBoardModel gb = app.game_board_model;

        for (int col = 0; col < gb.width; col++)
        {

            for (int row = 0; row < gb.height; row++)
            {

                bool clean = (gb.board[row][col].type == TileType.Clean);
                clean_array[row][col].gameObject.GetComponent<Renderer>().enabled = clean;
            }

        }
    }

    private void updateMud()
    {
        GameBoardModel gb = app.game_board_model;

        for (int col = 0; col < gb.width; col++)
        {

            for (int row = 0; row < gb.height; row++)
            {

                bool muddy = (gb.board[row][col].type == TileType.Dirty);
                mud_array[row][col].gameObject.GetComponent<Renderer>().enabled = muddy;
            }

        }
    }
}
