using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Text;
using System;

public class LevelLoaderScript : MonoBehaviour {

    //Holds the prefab of each type of tile. Index cordesponds to the number in the level text file
    public GameObject[] tiles;

    //Reference to the image of the loading screen
    public Image loadingScreen;

    //References to the game tiles so can be destroyed
    GameObject[] tileRefs;

    void Start()
    {
        tileRefs = new GameObject[0];
    }

    //Build the indicated level
    public void loadLevel(int level)
    {
        //Hide the play area
        loadingScreen.enabled = true;

        //Destroy any previous existing game objects
        for (int i = 0; i < tileRefs.Length; i++)
        {
            Destroy(tileRefs[i]);
        }

        //Read the level file to find out what the level needs
        string assetText;

        using (var streamReader = new StreamReader("Assets/Levels/" + level.ToString() + ".txt", Encoding.UTF8))
        {
            assetText = streamReader.ReadToEnd();
        }

        var lines = assetText.Split('\n');

        tileRefs = new GameObject[lines.Length * lines[0].Split('~').Length];

        //Go through each row
        for (int i = 0; i < lines.Length; i++)
        {
            var singleTiles = lines[i].Split('~');

            //Go through each individual tile and create it
            for (int j = 0; j < singleTiles.Length; j++)
            {
                //Create the object and store the returned reference

                //Account for even or odd length play areas
                if(singleTiles.Length%2 == 1)
                {
                    tileRefs[i * singleTiles.Length + j] = Instantiate(tiles[Int32.Parse(singleTiles[j])], new Vector3(j - (singleTiles.Length / 2), -i + (lines.Length / 2) + 1, 0), Quaternion.identity);
                }
                else
                {
                    tileRefs[i * singleTiles.Length + j] = Instantiate(tiles[Int32.Parse(singleTiles[j])], new Vector3(j - (singleTiles.Length / 2) + 0.5f, -i + (lines.Length / 2) + 0.5f, 0), Quaternion.identity);
                }

                //Tell the tile what coordinates it is
                tileRefs[i * singleTiles.Length + j].GetComponent<TileInformation>().assignCordinates(j, lines.Length - 1 - i);

                //TODO communicate this with the backend
            }
        }
        
        //Loading has finished so release the play area
        loadingScreen.enabled = false;
    }
}
