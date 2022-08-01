using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Facade : IFacade
{
    //facade在pureMvc中担任着控制3层MVC的责任，puremvc将原mvc从1个组件3个mvc变成了将mvc分别塞给自己
    //的主单例模式类中集合里，facade是一个全局调用点，所有人都要来到这里去寻找要去的地方
    public static object objStatic = new object();
    public object obj = new object();
    public static Facade instance;
    public IView i_View;
    public IModel i_Model;
    public IController i_Controller;
    /*
     防止忘记 补充一下xx相关分别在哪层
     Model层 拥有 代理Proxy （数据部分）
     View层 拥有 观察者Observer（使用反射去调用C层执行方法）  中介者Mediator（页面部分）
     Controller层 拥有 命令Command （方法部分）
     */
    public Facade()
    {
        Init();
    }

    #region MVC初始化方法
    public virtual void Init()
    {
        InitModel();
        InitController();
        InitView();
        //按照M->C->V的顺序去初始化
    }
    public virtual void InitView()
    {
        i_View = View.Get();
    }
    public virtual void InitController()
    {
        i_Controller = Controller.Get();
    }
    public virtual void InitModel()
    {
        i_Model = Model.Get();
    }
    #endregion

    #region 观察者Observer相关
    public void NotifyObservers(INotification note)
    {
        i_View.NotifyObservers(note);
    }
    #endregion

    #region 命令Command相关
    public bool HasCommand(string notificationName)
    {
       return i_Controller.HasCommand(notificationName);
    }
    public void RegisterCommand(string notificationName, Type commandType)
    {
        i_Controller.RegisterCommand(notificationName,commandType);
    }
    public void RemoveCommand(string notificationName)
    {
        i_Controller.RemoveCommand(notificationName);
    }

    #endregion

    #region 中介者Mediator相关
    public bool HasMediator(string mediatorName)
    {
        return i_View.HasMediator(mediatorName);
    }
    public IMediator RetrieveMediator(string mediatorName)
    {
        return i_View.RetrieveMediator(mediatorName);
    }

    public void RegisterMediator(IMediator mediator)
    {
        i_View.RegisterMediator(mediator);
    }
    public IMediator RemoveMediator(string mediatorName)
    {
        return i_View.RemoveMediator(mediatorName);
    }
    #endregion

    #region 代理Proxy相关
    public void RegisterProxy(IProxy proxy)
    {
        i_Model.RegisterProxy(proxy);
    }
    public bool HasProxy(string proxyName)
    {
        return i_Model.HasProxy(proxyName);
    }
    public IProxy RemoveProxy(string proxyName)
    {
        return i_Model.RemoveProxy(proxyName);
    }
    public IProxy RetrieveProxy(string proxyName)
    {
        return i_Model.RetrieveProxy(proxyName);
    }

    #endregion

    #region 发送消息相关
    public void SendNotification(string notificationName)
    {
        NotifyObservers(new Notification(notificationName));
    }

    public void SendNotification(string notificationName, object body)
    {
        NotifyObservers(new Notification(body, notificationName));
    }

    public void SendNotification(string notificationName, object body, string type)
    {
        NotifyObservers(new Notification(body, notificationName,type));
    }
    #endregion

    public static Facade Get()
    {
        if (instance == null)
        {
            lock (objStatic)
            {
                if (instance == null)
                {
                    instance = new Facade();
                }
            }
        }
        return instance;
    }
}
