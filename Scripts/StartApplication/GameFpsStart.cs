using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFpsStart : MonoBehaviour
{
    public LoginView login=null;
    public FPSview FPS = null;
    // Start is called before the first frame update
    void Start()
    {
        GameFacade facade = new GameFacade();
        facade.GameStart(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
