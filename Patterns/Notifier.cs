using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 继承了此类的类可以直接发送消息
 */
public class Notifier : INotifier
{
    public Facade facade = Facade.Get();
    public void SendNotification(string notificationName)
    {
        facade.SendNotification(notificationName);
    }

    public void SendNotification(string notificationName, object body)
    {
        facade.SendNotification(notificationName,body);
    }

    public void SendNotification(string notificationName, object body, string type)
    {
        facade.SendNotification(notificationName, body,type);
    }

}
