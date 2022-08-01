using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class View : IView
{
    //中介者集合 中介者在这里充当与原MVC中的V 里边提供了对unity的组件进行操作的方法
    protected IDictionary<string, IMediator> MediatorMap = new Dictionary<string, IMediator>();
    //观察者集合 观察者在这里担任一个发起事件的人，比如点击按钮我们要创建一个观察者，之后不断调用其他方法
    protected IDictionary<string, IList<IObserver>> ObserverMap = new Dictionary<string, IList<IObserver>>();
    public static object objStatic = new object();
    public object obj = new object();
    public static View instance;

    #region 中介者Mediator相关
   
    //注册中介者 创建的中介者都要调用方法来到这里
    public void RegisterMediator(IMediator mediator)
    {
        lock (obj)
        {
            if (MediatorMap.ContainsKey(mediator.MediatorName))
            {
                return;
            }
            MediatorMap[mediator.MediatorName] = mediator;
            IList<string> list = mediator.ListNotificationInterests();
            if (list.Count>0)
            {
                IObserver observer = new Observer("handleNotification", mediator);
                for (int i = 0; i < list.Count; i++)
                {
                    RegisterObserver(list[i].ToString(), observer);
                }
            }
        }
        mediator.OnRegister();
    }
    //移除中介者 当中介者代表的panel（画布）已经被销毁来这里调用方法去去除 map库中的中介者
    public IMediator RemoveMediator(string mediatorName)
    {
        IMediator Context = null;
        lock (obj)
        {
            if (!MediatorMap.ContainsKey(mediatorName))
            {
                return null;
            }
            Context = MediatorMap[mediatorName];
            IList<string> list = Context.ListNotificationInterests();
            for (int i = 0; i < list.Count; i++)
            {
                RemoveObserver(list[i], Context);
            }
        }
        if (Context!=null)
        {
            Context.OnRemove();
        }
        return Context;
    }
    //判断是否拥有此中介者
    public bool HasMediator(string mediatorName)
    {
        lock (obj)
        {
            return MediatorMap.ContainsKey(mediatorName);
        }
    }
    //检索中介者 如果存在将中介者返回，不存在返回一个Null
    public IMediator RetrieveMediator(string mediatorName)
    {
        lock (obj)
        {
            if (!MediatorMap.ContainsKey(mediatorName))
            {
                return null;
            }
            return MediatorMap[mediatorName];
        }

    }
    #endregion

    #region 观察者Observers相关
    //观察者发出通知  交互时来到这里  通知从这里发出
    //但是这里并不是执行的地方 执行在C层里因为c层注册命令时候一起注册了观察者
    //这里观察者会使用反射的方式去调用C层里的ExecuteCommand方法
    //真正由C层发出了事件 （？！？！）
    public void NotifyObservers(INotification note)
    {
        IList<IObserver> list = null;
        lock (obj)
        {
            if (ObserverMap.ContainsKey(note.Name))
            {
                IList<IObserver> collection = ObserverMap[note.Name];
                list = new List<IObserver>(collection);
            }
            else
            {
                Debug.Log("没有"+ note.Name);
            }
        }
        if (list != null)
        {
            for (int i = 0; i < list.Count; i++)
            {
                list[i].NotifyObserver(note);
            }
        }
    }
    //注册观察者 相当于消息中心Addlistener
    public void RegisterObserver(string notificationName, IObserver observer)
    {
        lock (obj)
        {
            if (!ObserverMap.ContainsKey(notificationName))
            {
                ObserverMap[notificationName] = new List<IObserver>();
            }
            ObserverMap[notificationName].Add(observer);
        }
    }
    //移除观察者  相当于消息中心RemoveAddlistener
    public void RemoveObserver(string notificationName, object notifyContext)
    {
        lock (obj)
        {
            if (ObserverMap.ContainsKey(notificationName))
            {
                IList<IObserver> list = ObserverMap[notificationName];
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].CompareNotifyContext(notifyContext))
                    {
                        list.RemoveAt(i);
                        break;
                    }
                }
                if (list.Count == 0)
                {
                    ObserverMap.Remove(notificationName);
                }
            }
        }
    }
    #endregion

    //单例模式 
    public static IView Get()
    {
        if (instance == null)
        {
            lock (objStatic)
            {
                if (instance == null)
                {
                    instance = new View();
                }
            }
        }
        return instance;
    }
}
