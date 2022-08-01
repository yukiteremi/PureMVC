using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipMediator : Mediator
{
    private TipProxy proxy;
    public const string NAME = "TipMediator";

    private TipView View
    {
        get { return (TipView)ViewComponent; }
    }
    
    public TipMediator(TipView view) : base(NAME, view)
    {
        view.UseAction += data => { SendNotification(BagSystemEvent.BAGITEMUSE, data, "BagItemUse"); };
        view.CloseAction += ()=> { SendNotification(BagSystemEvent.TIPCLOSE); };
    }
    public override void OnRegister()
    {
        //proxy = facade.RetrieveProxy(TipProxy.NAME) as TipProxy;
        View.Init();
    }
    public override IList<string> ListNotificationInterests()
    {
        IList<string> notifications = new List<string>();
        notifications.Add(BagSystemEvent.CHANGETIPDATA);
        notifications.Add(BagSystemEvent.TIPCLOSE);
        notifications.Add(BagSystemEvent.BAGITEMUSE);
        return notifications;
    }
    public override void HandleNotification(INotification notification)
    {
        BagItem bagItem = notification.Body as BagItem;
        if (notification.Name== BagSystemEvent.CHANGETIPDATA)
        {
            View.Change(bagItem);
        }
        else if (notification.Name == BagSystemEvent.TIPCLOSE)
        {
            View.ShowDown();
        }
        else if (notification.Name == BagSystemEvent.BAGITEMUSE)
        {
            SendNotification(BagSystemEvent.BAGITEMUSEOVER, bagItem, "BagItemUse");
        }
    }
}
