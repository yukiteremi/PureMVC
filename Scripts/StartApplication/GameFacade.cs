using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFacade : Facade
{
    public GameFacade()
    {
    }

    public override void InitController()
    {
        base.InitController();
        RegisterCommand(GameSystemEvent.GAMESTART, typeof(GameFpsCommand));
    }

    public void GameStart(GameFpsStart start)
    {
        SendNotification(GameSystemEvent.GAMESTART, start);
    }
}
