using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFpsCommand : SimpleCommand
{
    
    public override void Execute(INotification notification)
    {

        FPSProxy fps = new FPSProxy();
        facade.RegisterProxy(fps);

        GameFpsStart mainUI = notification.Body as GameFpsStart;

        if (null == mainUI)
            throw new Exception("程序启动失败..");
        facade.RegisterMediator(new LoginMediator(mainUI.login));
        facade.RegisterMediator(new FPSMediator(mainUI.FPS));
        facade.RegisterCommand(GameSystemEvent.GAMEFPSSTART, typeof(FPSStartCommand));
    }
}
