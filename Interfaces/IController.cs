using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IController
{
    //执行事件
    void ExecuteCommand(INotification notification);
    bool HasCommand(string notificationName);
    void RegisterCommand(string notificationName, Type commandType);
    void RemoveCommand(string notificationName);
}
