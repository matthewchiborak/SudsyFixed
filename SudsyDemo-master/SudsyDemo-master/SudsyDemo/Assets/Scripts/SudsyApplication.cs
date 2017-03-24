using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SudsyElement : MonoBehaviour
{

    // Gives access to the application and all instances.
    public SudsyApplication app { get { return GameObject.FindObjectOfType<SudsyApplication>(); } }
}



public class SudsyApplication : MonoBehaviour
{

    //For access
    public GameBoardModel game_board_model;
    public BoardTileView board_tile_view;

    // Iterates all Controllers and delegates the notification data
    // This method can easily be found because every class is “BounceElement” and has an “app” 
    // instance.
    public void Notify(string p_event_path, Object p_target, params object[] p_data)
    {
        SudsyController[] controller_list = GetAllControllers();
        foreach (SudsyController c in controller_list)
        {
            c.OnNotification(p_event_path, p_target, p_data);
        }
    }

    // Fetches all scene Controllers.
    public SudsyController[] GetAllControllers()
    {
        return GameObject.FindObjectsOfType<SudsyController>();
    }

    // Use this for initialization
    void Start()
    {

        //Pass string somehow?
        print(game_board_model.loadLevel("3.txt"));
        board_tile_view.init();

        Vector3 newCamPos = new Vector3(game_board_model.width / 2 - 0.5f, game_board_model.height / 2, -7.5f);
        Camera.main.transform.position = (newCamPos);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

