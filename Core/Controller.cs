using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : IController
{
    public Dictionary<string, Type> CommandMap = new Dictionary<string, Type>();
    public static object objStatic = new object();
    public object obj = new object();
    public static Controller instance;
    public IView view;

    public Controller()
    {
        view = View.Get();
    }

    #region 命令Command相关
    //真正的命令执行 这里才是真正的一段交互正式开始
    //此处会由观察者从V层使用反射调用 
    public void ExecuteCommand(INotification notification)
    {
        Type type = null;
        lock (obj)
        {
            if (!CommandMap.ContainsKey(notification.Name))
            {
                return;
            }
            type = CommandMap[notification.Name];
        }
        object obj2 = Activator.CreateInstance(type);
        if (obj2 is ICommand)
        {
            ((ICommand)obj2).Execute(notification);
        }
    }
    //判断库中是否拥有此命令
    public bool HasCommand(string notificationName)
    {
        lock (obj)
        {
            return CommandMap.ContainsKey(notificationName);
        }
    }
    //注册命令 当收到命令是要去V层创建一个观察者Observers 
    //因为命令是和观察者绑定的  需要命令去命令观察者去使用反射来代用C层的监听的方法 
    //所以注册一个命令时要去往V层里塞一个观察者 当v层接收到了交互方便回来
    public void RegisterCommand(string notificationName, Type commandType)
    {
        lock (obj)
        {
            if (!CommandMap.ContainsKey(notificationName))
            {
                view.RegisterObserver(notificationName, new Observer("executeCommand", this));
            }
            CommandMap[notificationName] = commandType;
        }
    }
    //移除命令
    public void RemoveCommand(string notificationName)
    {
        lock (obj)
        {
            if (CommandMap.ContainsKey(notificationName))
            {
                view.RemoveObserver(notificationName, this);
                CommandMap.Remove(notificationName);
            }
        }
    }
    #endregion

    //单例模式
    public static IController Get()
    {
        if (instance==null)
        {
            lock (objStatic)
            {
                if (instance==null)
                {
                    instance = new Controller();
                }
            }
        }
        return instance;
    }
}
