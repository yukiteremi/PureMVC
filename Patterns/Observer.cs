using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;

public class Observer : IObserver
{
    private object m_NotifyContext;
    private string m_NotifyMethod;
    public object obj = new object();

    public Observer(string notifyMethod, object notifyContext)
    {
        m_NotifyContext = notifyContext;
        m_NotifyMethod = notifyMethod;
    }

    public object NotifyContext
    {
        set { m_NotifyContext = value; }
        get { return m_NotifyContext; }
    }
    public string NotifyMethod {
        set { m_NotifyMethod = value; }
        get { return m_NotifyMethod; }  
    }

    public bool CompareNotifyContext(object obj)
    {
        lock (obj)
        {
            return NotifyContext.Equals(obj);
        }
    }

    public void NotifyObserver(INotification notification)
    {
        //反射
        object Context;
        lock (obj)
        {
            Context = NotifyContext;
        }
        Type type = Context.GetType();
        BindingFlags bindingAttr = BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase;
        MethodInfo method = type.GetMethod(NotifyMethod, bindingAttr);
        method.Invoke(Context,new object[] { notification });
    }
}
