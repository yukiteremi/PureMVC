using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 中介者  所有新建立中介者需要继承此类
 */
public class Mediator : Notifier, IMediator
{
    //中介者名字
    private string mediatorName;
    //中介者脚本  
    private object viewComponent;

    public Mediator() : this("Mediator", null) { }
    public Mediator(string mediatorName) : this(mediatorName, null) { }

    ///<param name="mediatorName">中介者名字</param>
    ///<param name="viewComponent">中介者脚本</param>
    public Mediator(string mediatorName, object viewComponent)
    {
        this.mediatorName = mediatorName;
        this.viewComponent = viewComponent;
    }

    public string MediatorName => mediatorName;

    public object ViewComponent { get => viewComponent; set => viewComponent=value; }

    public virtual void HandleNotification(INotification notification)
    {
        
    }
    public virtual IList<string> ListNotificationInterests()
    {
        return new List<string>();
    }
    public virtual void OnRegister()
    {
    }
    public virtual void OnRemove()
    {
    }
}
