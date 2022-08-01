using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Model : IModel
{
    //代理集合，每一个代理是一MVC原版里的M，掌握着数据
    public Dictionary<string, IProxy> m_proxyMap = new Dictionary<string, IProxy>();
    public static object objStatic = new object();
    public object obj = new object();
    public static Model instance;

    public Model()
    {

    }

    #region 代理Proxy相关
    //注册代理 所有代理创建都要来这里填入集合
    public void RegisterProxy(IProxy proxy)
    {
        lock (obj)
        {
            //使用代理名为键 代理类为值
            m_proxyMap[proxy.ProxyName] = proxy;
        }
        //代理的初始化 相当于Init
        proxy.OnRegister();
    }
    //移除代理  返回被移除的代理 相当于弹栈
    public IProxy RemoveProxy(string proxyName)
    {
        IProxy proxy = null;
        lock (obj)
        {
            if (m_proxyMap.ContainsKey(proxyName))
            {
                proxy = RetrieveProxy(proxyName);
                m_proxyMap.Remove(proxyName);
            }
        }
        if (proxy!=null)
        {
            //触发代理的销毁方法，对数据处理 相当于生命周期的Ondestroy
            proxy.OnRemove();
        }
        return proxy;
    }
    //判断库中是否拥有此代理 有返回true
    public bool HasProxy(string proxyName)
    {
        lock (obj)
        {
            return m_proxyMap.ContainsKey(proxyName);
        }
    }
    //判读库中是否拥有此代理 有返回该代理 无返回空
    public IProxy RetrieveProxy(string proxyName)
    {
        lock (obj)
        {
            if (!m_proxyMap.ContainsKey(proxyName))
            {
                return null;
            }
            return m_proxyMap[proxyName];
        }
    }
    #endregion

    //单例模式
    public static IModel Get()
    {
        if (instance == null)
        {
            lock (objStatic)
            {
                if (instance == null)
                {
                    instance = new Model();
                }
            }
        }
        return instance;
    }
}
