using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 代理基类  所有数据类均需要继承此类
/// </summary>
public class Proxy : Notifier, IProxy, INotifier
{
    public object data;
    public string name;

    /// <summary>
    /// 数据类 后面需要拆包成具体类
    /// </summary>
    public object Data { get => data; set => data=value; }
    /// <summary>
    /// 数据类的名字
    /// </summary>
    public virtual string ProxyName => name;
    public Proxy() : this("Proxy", null)
    {

    }
    public Proxy(string proxyName) : this(proxyName, null)
    {
    }

    public Proxy(string proxyName, object data)
    {
        name = (proxyName != null) ? proxyName : "Proxy";
        if (data != null)
        {
            this.data = data;
        }
    }
    public virtual void OnRegister()
    {
    }

    public virtual void OnRemove()
    {
    }
}
