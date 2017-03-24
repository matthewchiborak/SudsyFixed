using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class PlayerMoveView : SudsyView
{

    //Update is called once per frame   
    void Update()
    {
        
        if (Input.GetKeyDown("w") || Input.GetKeyDown(KeyCode.UpArrow))
        {
            app.Notify(SudysNotifications.UserMoveUp, this);
        }

        else if (Input.GetKeyDown("s") || Input.GetKeyDown(KeyCode.DownArrow))
        {
            app.Notify(SudysNotifications.UserMoveDown, this);
        }

        else if (Input.GetKeyDown("a") || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            app.Notify(SudysNotifications.UserMoveLeft, this);

        }                

        else if (Input.GetKeyDown("d") || Input.GetKeyDown(KeyCode.RightArrow))
        {
            app.Notify(SudysNotifications.UserMoveRight, this);

        }

        else if (Input.GetKeyDown(KeyCode.Backspace))
        {
            app.Notify(SudysNotifications.UserUndo, this);
        }

    }

}

