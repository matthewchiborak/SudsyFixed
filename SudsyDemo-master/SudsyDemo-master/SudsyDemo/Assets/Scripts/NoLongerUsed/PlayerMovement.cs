using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Assets.Scripts;


public class PlayerMovement : MonoBehaviour
{

    ////Shared event queues. Attach them to other objects
    //SafeQueue<GameBoardEvent> gbEventQueue;


    //GameBoard gb;
    //GameView gv;

    void Start()
    {
        //gb = gameObject.AddComponent<GameBoard>();

        //gv = gameObject.AddComponent<GameView>();
        //gv.attachGameBoard(gb);

        ////initilize and attach the gameboardevent queue to the gameboard
        //gbEventQueue = new SafeQueue<GameBoardEvent>();
        //gb.addGameBoardEventQueue(gbEventQueue);

        //Camera.main.transform.Translate(gb.width / 2 - 0.5f, gb.height / 2, -7.5f);
    }

    // Update is called once per frame
    void Update()
    {        

        if (Input.GetKeyDown("w") || Input.GetKeyDown(KeyCode.UpArrow) )
        {
            print("w key was pressed");
            //gbEventQueue.Enqueue(new ActorMoveEventUp(gb, gb.player));

        }
        else if (Input.GetKeyDown("a") || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            print("a key was pressed");
           // gbEventQueue.Enqueue(new ActorMoveEventLeft(gb, gb.player));
        }

        else if (Input.GetKeyDown("s") || Input.GetKeyDown(KeyCode.DownArrow))
        {
            print("s key was pressed");
            //gbEventQueue.Enqueue(new ActorMoveEventDown(gb, gb.player));
        }

        else if (Input.GetKeyDown("d") || Input.GetKeyDown(KeyCode.RightArrow))
        {
            print("d key was pressed");
           // gbEventQueue.Enqueue(new ActorMoveEventRight(gb, gb.player));
        }
        else if (Input.GetKeyDown(KeyCode.Backspace))
        {
            print("backspace key was pressed");
           // gbEventQueue.Enqueue(new GameBoardUndoEvent(gb));
        }

    }

}
