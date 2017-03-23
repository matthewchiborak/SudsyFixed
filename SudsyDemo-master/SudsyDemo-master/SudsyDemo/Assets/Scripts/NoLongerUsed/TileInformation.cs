using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileInformation : MonoBehaviour {

    int xCord;
    int yCord;

    //Reference to the visual representation of the mud to enable or disable it during gameplay
    public GameObject mud;

    //Used when the level is loading to tell the tile what tile it is
    public void assignCordinates(int x, int y)
    {
        xCord = x;
        yCord = y;
    }

    public Vector2 getCordinates()
    {
        return new Vector2(xCord, yCord);
    }

    public void setMudState(bool state)
    {
        mud.SetActive(state);
    }
}
