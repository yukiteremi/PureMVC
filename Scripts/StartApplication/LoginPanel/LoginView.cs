using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class LoginView : MonoBehaviour
{
    public Button btn;
    public Button Btn
    {
        get
        {
            if (btn == null)
            {
                btn = transform.Find("Button").GetComponent<Button>();
            }
            return btn;
        }
    }
    public UnityAction Open = null;

    // Start is called before the first frame update
    void Start()
    {
        Btn.onClick.AddListener(()=> {
            Open();
        });
    }
    public void Show()
    {
        transform.position = new Vector3(200, 150, 0);
    }
    public void Close()
    {
        transform.position = new Vector3(-1000,-1000,0);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
