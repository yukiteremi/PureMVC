using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 此类为通知类  用于保存通知数据 传递通知
 */
public class Notification : INotification
{
    private object body;
    private string name;
    private string type;

    public Notification(string name):this(null,name,null)
    {

    }

    public Notification(object body, string name) : this(body, name, null)
    {
    }

    public Notification(object body, string name, string type)
    {
        this.body = body;
        this.name = name;
        this.type = type;
    }
    public override string ToString()
    {
        return ((("Notification Name: " + this.Name) + "\nBody:" + ((this.Body == null) ? "null" : this.Body.ToString())) + "\nType:" + ((this.Type == null) ? "null" : this.Type));
    }
    public object Body { get => body; set => body = value; }

    public string Name { get => name; }

    public string Type { get => type; set => type = value; }
    }
