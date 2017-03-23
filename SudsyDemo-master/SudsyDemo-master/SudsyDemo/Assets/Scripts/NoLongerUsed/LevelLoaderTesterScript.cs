using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoaderTesterScript : MonoBehaviour {

    public GameBoard scriptToBeTested;
    
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            scriptToBeTested.loadLevel(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            scriptToBeTested.loadLevel(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            scriptToBeTested.loadLevel(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            scriptToBeTested.loadLevel(4);
        }
    }
}
