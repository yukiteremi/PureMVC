using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicationFacade : Facade
{
    public override void InitController()
    {
        base.InitController();
        Debug.Log("!");
    }
}
