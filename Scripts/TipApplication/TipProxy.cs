using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipProxy : Proxy
{
    public const string NAME = "TipProxy";
    public BagItem Bagitem=null;
    public override string ProxyName => NAME;
    public TipProxy()
    {

    }
    public override void OnRegister()
    {
        
    }
    public void ChangeData()
    {
        if (Bagitem!=null)
        {
            Bagitem.num--;
        }
    }
}
