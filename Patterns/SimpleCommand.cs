using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 命令基类 命令需要继承的基类
/// </summary>
public class SimpleCommand : Notifier, ICommand, INotifier
{
    public virtual void Execute(INotification notification)
    {
    }
}