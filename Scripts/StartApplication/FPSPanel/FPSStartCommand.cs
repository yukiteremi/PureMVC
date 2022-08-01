using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSStartCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        SendNotification(GameSystemEvent.LOGINCLOSE);
        SendNotification(GameSystemEvent.OPENFPS);
    }
}
