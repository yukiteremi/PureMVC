using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        if (notification.Type.Equals("TipOpen"))
        {
            BagItem bagItem = notification.Body as BagItem;
            SendNotification(BagSystemEvent.CHANGETIPDATA, bagItem, "CHANGETIPDATA");
        }
        else if (notification.Type.Equals("Close"))
        {

        }
    }
}
